using System;
using JetBrains.Annotations;
using UtilityFramework.Services.Vimeo.Core.Net;

namespace UtilityFramework.Services.Vimeo.Core.Exceptions
{
    internal class VimeoUploadException : VimeoApiException
    {
        [PublicAPI]
        public VimeoUploadException()
        {
        }

        [PublicAPI]
        public VimeoUploadException(IUploadRequest request)
        {
            Request = request;
        }

        [PublicAPI]
        public VimeoUploadException(string message)
            : base(message)
        {
        }

        [PublicAPI]
        public VimeoUploadException(string message, IUploadRequest request)
            : base(message)
        {
            Request = request;
        }

        [PublicAPI]
        public VimeoUploadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [PublicAPI]
        public VimeoUploadException(string message, IUploadRequest request, Exception innerException)
            : base(message, innerException)
        {
            Request = request;
        }

        [PublicAPI]
        public IUploadRequest Request { get; set; }
    }
}