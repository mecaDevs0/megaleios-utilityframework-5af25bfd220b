using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace UtilityFramework.Application.Core.Middleware
{
    public class ImageResizerMiddleware
    {
        struct ResizeParams
        {
            public bool HasParams;
            public int W;
            public int H;
            public bool Autorotate;
            public int Quality; // 0 - 100
            public string Format; // png, jpg, jpeg
            public string Mode; // pad, max, crop, stretch

            public static readonly string[] Modes = ["pad", "max", "crop", "stretch"];

            public override readonly string ToString()
            {
                var sb = new StringBuilder();
                sb.Append($"w: {W}, ");
                sb.Append($"h: {H}, ");
                sb.Append($"autorotate: {Autorotate}, ");
                sb.Append($"quality: {Quality}, ");
                sb.Append($"format: {Format}, ");
                sb.Append($"mode: {Mode}");

                return sb.ToString();
            }
        }

        private readonly RequestDelegate _next;
        private readonly ILogger<ImageResizerMiddleware> _logger;
        private readonly IHostingEnvironment _env;
        private readonly IMemoryCache _memoryCache;

        private static readonly string[] Suffixes = [
            ".png",
            ".jpg",
            ".jpeg"
        ];

        public ImageResizerMiddleware(RequestDelegate next, IHostingEnvironment env, ILogger<ImageResizerMiddleware> logger, IMemoryCache memoryCache)
        {
            _next = next;
            _env = env;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            // hand to next middleware if we are not dealing with an image
            if (context.Request.Query.Count == 0 || !IsImagePath(path))
            {
                await _next.Invoke(context);
                return;
            }

            // hand to next middleware if we are dealing with an image but it doesn't have any usable resize querystring params
            var resizeParams = GetResizeParams(path, context.Request.Query);
            if (!resizeParams.HasParams || resizeParams.W == 0 && resizeParams.H == 0)
            {
                await _next.Invoke(context);
                return;
            }

            // if we got this far, resize it
            _logger.LogInformation($"Resizing {path.Value} with params {resizeParams}");

            // get the image location on disk
            //var imagePath = Path.Combine(
            //    BaseConfig.BaseUrl,
            //    path.Value.Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar));
            var imagePath = Path.Combine(
                _env.ContentRootPath,
                path.Value.Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar));

            // check file lastwrite
            var lastWriteTimeUtc = File.GetLastWriteTimeUtc(imagePath);
            if (lastWriteTimeUtc.Year == 1601) // file doesn't exist, pass to next middleware
            {
                await _next.Invoke(context);
                return;
            }

            var imageData = GetImageData(imagePath, resizeParams, lastWriteTimeUtc);

            // write to stream
            context.Response.ContentType = resizeParams.Format == "png" ? "image/png" : "image/jpeg";
            context.Response.ContentLength = imageData.Size;
            await context.Response.Body.WriteAsync(imageData.ToArray(), 0, (int)imageData.Size);

            // cleanup
            imageData.Dispose();

        }

        private SKData GetImageData(string imagePath, ResizeParams resizeParams, DateTime lastWriteTimeUtc)
        {
            // check cache and return if cached
            long cacheKey;
            unchecked
            {
                cacheKey = imagePath.GetHashCode() + lastWriteTimeUtc.ToBinary() + resizeParams.ToString().GetHashCode();
            }

            //byte[] imageBytes;
            //bool isCached = _memoryCache.TryGetValue<byte[]>(cacheKey, out imageBytes);
            //if (isCached)
            //{
            //    _logger.LogInformation("Serving from cache");
            //    return SKData.CreateCopy(imageBytes);
            //}

            var bitmap = LoadBitmap(File.OpenRead(imagePath), out var origin); // always load as 32bit (to overcome issues with indexed color)

            // if autorotate = true, and origin isn't correct for the rotation, rotate it
            if (resizeParams.Autorotate && origin != SKCodecOrigin.TopLeft)
                bitmap = RotateAndFlip(bitmap, origin);

            // if either w or h is 0, set it based on ratio of original image
            if (resizeParams.H == 0)
                resizeParams.H = (int)Math.Round(bitmap.Height * (float)resizeParams.W / bitmap.Width);
            else if (resizeParams.W == 0)
                resizeParams.W = (int)Math.Round(bitmap.Width * (float)resizeParams.H / bitmap.Height);

            // if we need to crop, crop the original before resizing
            if (resizeParams.Mode == "crop")
                bitmap = Crop(bitmap, resizeParams);

            // store padded height and width
            var paddedHeight = resizeParams.H;
            var paddedWidth = resizeParams.W;

            // if we need to pad, or max, set the height or width according to ratio
            if (resizeParams.Mode == "pad" || resizeParams.Mode == "max")
            {
                var bitmapRatio = (float)bitmap.Width / bitmap.Height;
                var resizeRatio = (float)resizeParams.W / resizeParams.H;

                if (bitmapRatio > resizeRatio) // original is more "landscape"
                    resizeParams.H = (int)Math.Round(bitmap.Height * ((float)resizeParams.W / bitmap.Width));
                else
                    resizeParams.W = (int)Math.Round(bitmap.Width * ((float)resizeParams.H / bitmap.Height));
            }

            // resize
            var resizedImageInfo = new SKImageInfo(resizeParams.W, resizeParams.H, SKImageInfo.PlatformColorType, bitmap.AlphaType);
            var resizedBitmap = bitmap.Resize(resizedImageInfo, SKBitmapResizeMethod.Lanczos3);

            // optionally pad
            if (resizeParams.Mode == "pad")
                resizedBitmap = Pad(resizedBitmap, paddedWidth, paddedHeight, resizeParams.Format != "png");

            // encode
            var resizedImage = SKImage.FromBitmap(resizedBitmap);
            var encodeFormat = resizeParams.Format == "png" ? SKEncodedImageFormat.Png : SKEncodedImageFormat.Jpeg;
            var imageData = resizedImage.Encode(encodeFormat, resizeParams.Quality);

            // cache the result
            _memoryCache.Set(cacheKey, imageData.ToArray());

            // cleanup
            resizedImage.Dispose();
            bitmap.Dispose();
            resizedBitmap.Dispose();

            return imageData;
        }

        private SKBitmap RotateAndFlip(SKBitmap original, SKCodecOrigin origin)
        {
            // these are the origins that represent a 90 degree turn in some fashion
            var differentOrientations = new[]
            {
                SKCodecOrigin.LeftBottom,
                SKCodecOrigin.LeftTop,
                SKCodecOrigin.RightBottom,
                SKCodecOrigin.RightTop
            };

            // check if we need to turn the image
            var isDifferentOrientation = differentOrientations.Any(o => o == origin);

            // define new width/height
            var width = isDifferentOrientation ? original.Height : original.Width;
            var height = isDifferentOrientation ? original.Width : original.Height;

            var bitmap = new SKBitmap(width, height, original.AlphaType == SKAlphaType.Opaque);

            // todo: the stuff in this switch statement should be rewritten to use pointers
            switch (origin)
            {
                case SKCodecOrigin.LeftBottom:

                    for (var x = 0; x < original.Width; x++)
                        for (var y = 0; y < original.Height; y++)
                            bitmap.SetPixel(y, original.Width - 1 - x, original.GetPixel(x, y));
                    break;

                case SKCodecOrigin.RightTop:

                    for (var x = 0; x < original.Width; x++)
                        for (var y = 0; y < original.Height; y++)
                            bitmap.SetPixel(original.Height - 1 - y, x, original.GetPixel(x, y));
                    break;

                case SKCodecOrigin.RightBottom:

                    for (var x = 0; x < original.Width; x++)
                        for (var y = 0; y < original.Height; y++)
                            bitmap.SetPixel(original.Height - 1 - y, original.Width - 1 - x, original.GetPixel(x, y));

                    break;

                case SKCodecOrigin.LeftTop:

                    for (var x = 0; x < original.Width; x++)
                        for (var y = 0; y < original.Height; y++)
                            bitmap.SetPixel(y, x, original.GetPixel(x, y));
                    break;

                case SKCodecOrigin.BottomLeft:

                    for (var x = 0; x < original.Width; x++)
                        for (var y = 0; y < original.Height; y++)
                            bitmap.SetPixel(x, original.Height - 1 - y, original.GetPixel(x, y));
                    break;

                case SKCodecOrigin.BottomRight:

                    for (var x = 0; x < original.Width; x++)
                        for (var y = 0; y < original.Height; y++)
                            bitmap.SetPixel(original.Width - 1 - x, original.Height - 1 - y, original.GetPixel(x, y));
                    break;

                case SKCodecOrigin.TopRight:

                    for (var x = 0; x < original.Width; x++)
                        for (var y = 0; y < original.Height; y++)
                            bitmap.SetPixel(original.Width - 1 - x, y, original.GetPixel(x, y));
                    break;

            }

            original.Dispose();

            return bitmap;


        }

        private SKBitmap LoadBitmap(Stream stream, out SKCodecOrigin origin)
        {
            using var s = new SKManagedStream(stream);
            using var codec = SKCodec.Create(s);
            origin = codec.Origin;
            var info = codec.Info;
            var bitmap = new SKBitmap(info.Width, info.Height, SKImageInfo.PlatformColorType, info.IsOpaque ? SKAlphaType.Opaque : SKAlphaType.Premul);

            var result = codec.GetPixels(bitmap.Info, bitmap.GetPixels(out _));
            if (result == SKCodecResult.Success || result == SKCodecResult.IncompleteInput)
            {
                return bitmap;
            }
            else
            {
                throw new ArgumentException("Unable to load bitmap from provided data");
            }
        }

        private SKBitmap Crop(SKBitmap original, ResizeParams resizeParams)
        {
            var cropSides = 0;
            var cropTopBottom = 0;

            // calculate amount of pixels to remove from sides and top/bottom
            if ((float)resizeParams.W / original.Width < resizeParams.H / original.Height) // crop sides
                cropSides = original.Width - (int)Math.Round((float)original.Height / resizeParams.H * resizeParams.W);
            else
                cropTopBottom = original.Height - (int)Math.Round((float)original.Width / resizeParams.W * resizeParams.H);

            // setup crop rect
            var cropRect = new SKRectI
            {
                Left = cropSides / 2,
                Top = cropTopBottom / 2,
                Right = original.Width - cropSides + cropSides / 2,
                Bottom = original.Height - cropTopBottom + cropTopBottom / 2
            };

            // crop
            var bitmap = new SKBitmap(cropRect.Width, cropRect.Height);
            original.ExtractSubset(bitmap, cropRect);
            original.Dispose();

            return bitmap;
        }

        private SKBitmap Pad(SKBitmap original, int paddedWidth, int paddedHeight, bool isOpaque)
        {
            // setup new bitmap and optionally clear
            var bitmap = new SKBitmap(paddedWidth, paddedHeight, isOpaque);
            var canvas = new SKCanvas(bitmap);
            canvas.Clear(isOpaque ? new SKColor(255, 255, 255) : SKColor.Empty);

            // find co-ords to draw original at
            var left = original.Width < paddedWidth ? (paddedWidth - original.Width) / 2 : 0;
            var top = original.Height < paddedHeight ? (paddedHeight - original.Height) / 2 : 0;

            var drawRect = new SKRectI
            {
                Left = left,
                Top = top,
                Right = original.Width + left,
                Bottom = original.Height + top
            };

            // draw original onto padded version
            canvas.DrawBitmap(original, drawRect);
            canvas.Flush();

            canvas.Dispose();
            original.Dispose();

            return bitmap;
        }

        private bool IsImagePath(PathString path)
        {
            if (path == null || !path.HasValue)
                return false;

            return Suffixes.Any(x => x.EndsWith(x, StringComparison.OrdinalIgnoreCase));
        }

        private ResizeParams GetResizeParams(PathString path, IQueryCollection query)
        {
            var resizeParams = new ResizeParams();

            // before we extract, do a quick check for resize params
            resizeParams.HasParams =
                resizeParams.GetType().GetTypeInfo()
                .GetFields().Where(f => f.Name != "hasParams")
                .Any(f => query.ContainsKey(f.Name));

            // if no params present, bug out
            if (!resizeParams.HasParams)
                return resizeParams;

            // extract resize params

            if (query.ContainsKey("format"))
                resizeParams.Format = query["format"];
            else
                resizeParams.Format = path.Value.Substring(path.Value.LastIndexOf('.') + 1);

            if (query.ContainsKey("autorotate"))
                bool.TryParse(query["autorotate"], out resizeParams.Autorotate);

            var quality = 100;
            if (query.ContainsKey("quality"))
                int.TryParse(query["quality"], out quality);
            resizeParams.Quality = quality;

            var w = 0;
            if (query.ContainsKey("w") || query.ContainsKey("width"))
            {
                var width = query.ContainsKey("w") ? query["w"].ToString() : query["width"].ToString();

                int.TryParse(width, out w);
                resizeParams.W = w;
            }


            var h = 0;

            if (query.ContainsKey("h") || query.ContainsKey("height"))
            {
                var height = query.ContainsKey("h") ? query["h"].ToString() : query["height"].ToString();

                int.TryParse(height, out h);
                resizeParams.H = h;
            }

            resizeParams.Mode = "max";
            // only apply mode if it's a valid mode and both w and h are specified
            if (h != 0 && w != 0 && query.ContainsKey("mode") && ResizeParams.Modes.Any(m => query["mode"] == m))
                resizeParams.Mode = query["mode"];

            return resizeParams;
        }
    }
}