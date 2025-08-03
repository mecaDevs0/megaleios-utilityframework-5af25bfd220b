using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

using AutoMapper;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Style;

using RestSharp;
using RestSharp.Extensions;

using UtilityFramework.Application.Core;
using UtilityFramework.Application.Core.ViewModels;
using Microsoft.Extensions.Logging;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace UtilityFramework.Application.Core
{
    /// <summary>
    ///  CLASS com metodos auxiliares
    /// </summary>
    public static class Utilities
    {
        private static readonly Random Random = new Random();
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static IHttpContextAccessor HttpContextAccessor;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        private static readonly char[] SeparatorChars = { ';', '|', '\t', ',' };



        /// <summary>
        /// METODO PARA BUSCAR ROTAS ENTRE 2 ENDEREÇOS
        /// </summary>
        /// <param name="latSource"></param>
        /// <param name="lngSource"></param>
        /// <param name="latDest"></param>
        /// <param name="lngDest"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<GmapsResult> GetDirections(double latSource, double lngSource, double latDest, double lngDest, string key = null)
        {
            try
            {
                var client = new RestClient("https://maps.googleapis.com/maps/api/directions/json");

                var request = new RestRequest(Method.GET);

                if (string.IsNullOrEmpty(key))
                {
                    key = GetConfigurationRoot().GetValue<string>("googleMapsKey");

                    if (string.IsNullOrEmpty(key))
                        throw new Exception("Informe a googleMapsKey no settings/Config.json");
                }

                request.AddParameter("key", key, ParameterType.QueryString);
                request.AddParameter("destination", $"{latDest.ToString(CultureInfo.InvariantCulture)},{lngDest.ToString(CultureInfo.InvariantCulture)}", ParameterType.QueryString);
                request.AddParameter("origin", $"{latSource.ToString(CultureInfo.InvariantCulture)},{lngSource.ToString(CultureInfo.InvariantCulture)}", ParameterType.QueryString);
                request.AddParameter("language", "pt-BR", ParameterType.QueryString);
                request.AddParameter("units", "metric", ParameterType.QueryString);

                var response = await client.ExecuteAsync<GmapsResult>(request);
                if (string.IsNullOrEmpty(response?.Data?.ErroMessage) == false)
                    response.Data.HasError = true;

                if (response.Data != null && (response.Data.Status == "ZERO_RESULTS" || response.Data.Status == "NOT_FOUND" || response.Data.Routes.Count() == 0))
                {
                    response.Data.HasError = true;
                    response.Data.ErroMessage = "Nenhuma rota encontrada";
                }

                return response?.Data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// METODO PARA CONVERTER DOUBLE PARA STRING R$ #,##
        /// </summary>
        /// <param name="value"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToBrazilianReal(this double value, string culture = "pt-BR")
            => value.ToString("C", new CultureInfo(culture));

        /// <summary>
        /// METODO PARA CONVERTER DOUBLE PARA STRING R$ #,##
        /// </summary>
        /// <param name="value"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToBrazilianReal(this double? value, string culture = "pt-BR", string defaultValue = "")
            => value != null ? value.GetValueOrDefault().ToString("C", new CultureInfo(culture)) : defaultValue;


        /// <summary>
        ///  METODO PARA OBTER O DEFINIR CURRENT HTTPCONTEXT
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void SetHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        ///  RETORNAR URL COM PROTOCOLO (HTTP/HTTPS) DA CHAMADA REALIZADA
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static string SetCurrentProtocol(this string url)
        {
            if (string.IsNullOrEmpty(url) || HttpContextAccessor?.HttpContext == null)
                return url;

            var protocol = HttpContextAccessor.HttpContext.Request.Scheme;
            var protocolIndex = url.IndexOf(':');

            return $"{protocol}:{url.Substring(protocolIndex + 1)}";
        }

        /// <summary>
        /// CONVERTE CENTAVOS EM REAL
        /// </summary>
        /// <param name="cent"></param>
        /// <returns></returns>
        public static double ToReal(this long cent)
        {
            return cent / 100D;
        }
        /// <summary>
        /// CONVERTE CENTAVOS EM REAL
        /// </summary>
        /// <param name="cent"></param>
        /// <returns></returns>
        public static double ToReal(this int cent)
        {
            return cent / 100D;
        }
        /// <summary>
        /// CONVERTE CENTAVOS EM REAL
        /// </summary>
        /// <param name="cent"></param>
        /// <returns></returns>
        public static double ToDouble(this long cent)
        {
            return cent / 100D;
        }
        /// <summary>
        /// CONVERTE CENTAVOS EM REAL
        /// </summary>
        /// <param name="cent"></param>
        /// <returns></returns>
        public static double ToDouble(this int cent)
        {
            return cent / 100D;
        }

        /// <summary>
        ///  SETAR MASCAR CUSTOM EM STRING
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="mask"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string SetCustomMask(this string txt, string mask, string pattern)
        {
            try
            {
                return string.IsNullOrEmpty(txt) ? txt : Regex.Replace(txt, pattern, mask);
            }
            catch (Exception)
            {
                /*UNUSED*/
            }

            return txt;
        }

        /// <summary>
        ///  REPASSE DE TAG POR VALOR
        /// </summary>
        /// <param name="txt">TEXTO</param>
        /// <param name="substituicao">CHAVES</param>
        /// <returns></returns>
        public static string ReplaceTag(this string txt, IDictionary<string, string> substituicao)
        {
            if (string.IsNullOrEmpty(txt) == false)
                txt = substituicao?.Aggregate(txt, (current, item) => current.Replace(item.Key, item.Value));
            return txt;
        }


        /// <summary>
        /// VALOR RANDOMICO
        /// </summary>
        /// <param name="min">mínimo</param>
        /// <param name="max">máximo</param>
        /// <returns></returns>
        public static async Task<int> TrueRamdom(int min = 1, int max = 1)
        {
            try
            {
                var client =
                    new RestClient(
                        $"http://www.random.org/integers/?num=1&min={min}&max={max}&col=1&base=10&format=plain&rnd=new");
                var request = new RestRequest(Method.GET);

                var response = await client.ExecuteAsync(request);

                int.TryParse(response.Content, out var random);

                return random;
            }
            catch (Exception)
            {
                return int.Parse(RandomInt(max.ToString().Length));
            }
        }

        /// <summary>
        ///  SETAR VALORES DEFAULT EM CAMPOS NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T RemoveAllNull<T>(this T entity) where T : class
        {
            PropertyInfo[] array = entity.GetType().GetProperties();
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo prop = array[i];
                try
                {
                    var value = prop.GetValue(entity, null);
                    if (value == null)
                    {
                        if (prop.PropertyType == typeof(string))
                            SetPropertyValue(entity, prop.Name, "");
                        if (prop.PropertyType == typeof(int?))
                            SetPropertyValue(entity, prop.Name, default(int));

                        if (prop.PropertyType == typeof(long?))
                            SetPropertyValue(entity, prop.Name, default(long));
                        if (prop.PropertyType == typeof(double?))
                            SetPropertyValue(entity, prop.Name, default(double));
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return entity;
        }


        /// <summary>
        ///  REMOVER ACENTOS
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// COMPARAR OBJ OU STRING COM IGNORE CASE
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool Equals(this object obj1, object obj2, bool ignoreCase = false)
            => ignoreCase == false ? object.Equals(obj1, obj2) : EqualsIgnoreCase(obj1?.ToString(), obj2?.ToString());

        /// <summary>
        /// TRANSFORMA EM UM ARRAY
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Array ToAnonymousArray(this object data)
            => data is string ? new object[] { data } : data as Array ?? (data as IEnumerable)?.Cast<object>().ToArray() ?? new object[] { data };

        /// <summary>
        /// VERIFICA SE É UMA LISTA
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool CheckIsCollection(this object data)
        {
            try
            {
                if (data == null)
                    return false;

                var type = data.GetType();

                return ((data is string) == false &&
                        (typeof(Array).IsAssignableFrom(type) ||
                        typeof(IEnumerable).IsAssignableFrom(type) ||
                        typeof(IList).IsAssignableFrom(type)));
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// OBTER PROPRIEDADE POR NOME
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static string GetOptionalProperty<T>(this T data, string property, string defaultValue = "") where T : class
        {
            var response = defaultValue;
            try
            {
                if (data != null)
                    response = GetValueByProperty(data, property)?.ToString();
            }
            catch (Exception) {/*UNUSED*/}

            return response;
        }

        /// <summary>
        ///  OBTER VALOR DE UMA PROP / RETORNA NULL CASO NÃO EXISTA
        /// </summary>
        /// <param name="x"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetValueByProperty(this object x, string propName)
        {
            PropertyInfo property = null;
            try
            {
                if (string.IsNullOrEmpty(propName))
                    throw new Exception("Informe o nome da propriede para obter o valor");

                if (x == null)
                    return null;

                var type = x.GetType();

                if (typeof(Array).IsAssignableFrom(type) ||
                    typeof(IEnumerable).IsAssignableFrom(type) ||
                    typeof(IList).IsAssignableFrom(type))
                    throw new Exception("Não e possível obter o valor de um campo em uma lista de objetos");

                string[] fieldNames = propName.Split('.');

                for (int i = 0; i < fieldNames.Length; i++)
                {
                    var fieldName = fieldNames[i];

                    if (typeof(Array).IsAssignableFrom(type) ||
                        typeof(IEnumerable).IsAssignableFrom(type) ||
                        typeof(IList).IsAssignableFrom(type))
                        break;

                    property = type.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (property == null)
                        return null;

                    type = property.PropertyType;
                    x = property.GetValue(x, null);

                    if (fieldNames.Length > 1 && x == null)
                        return x;
                }
                return x;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        ///  OBTER VALOR DE UMA PROP / RETORNA NULL CASO NÃO EXISTA
        /// </summary>
        /// <param name="x"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static T GetValueByProperty<T>(this object x, string propName)
            => x == null ? default : (T)x.GetValueByProperty(propName);

        /// <summary>
        ///  RETORNA OBJ APENAS COM CAMPOS ALTERADOS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="entity"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        public static object OnlyChanged<T, Y>(T entity, Y newValues)
        {
            var properties = new Dictionary<string, object>();

            var listProperties = entity.GetType().GetProperties().ToList();

            for (int i = 0; i < listProperties.Count; i++)
            {
                var propertyItem = listProperties[i];

                var propValue = GetValueByProperty(entity, propertyItem.Name);
                var newValue = GetValueByProperty(newValues, propertyItem.Name);

                if (Equals(propValue, newValue) == false && Equals(JsonConvert.SerializeObject(propValue), JsonConvert.SerializeObject(newValue)) == false)
                    properties.Add(
                        $"{((JsonPropertyAttribute)propertyItem.GetCustomAttributes(typeof(JsonPropertyAttribute), false)?.FirstOrDefault())?.PropertyName ?? propertyItem.Name}",
                        newValue);

            }


            return ToGenericData(properties);
        }


        /// <summary>
        ///  GERAR OBJ DINAMYC ANONIMO PARA INCLUIR PROPS MANUALMENTE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                return JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  GERAR OBJ DINAMYC ANONIMO PARA INCLUIR PROPS MANUALMENTE
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static dynamic ToGenericData(Dictionary<string, object> dict)
        {
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

            foreach (var kvp in dict)
                eoColl.Add(kvp);
            dynamic data = eo;
            return data;
        }

        /// <summary>
        ///  GERAR OBJ DINAMYC ANONIMO PARA INCLUIR PROPS MANUALMENTE
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static dynamic ToDynamicFirebase<T>(this T model)
        {
            if (model == null)
                return null;

            var json = JsonConvert.SerializeObject(model, Formatting.None);
            var custonData = JsonConvert.DeserializeObject(json);

            var dictionary = custonData.ToDictionary<object>();
            var dataAnonymous = new ExpandoObject();
            var valuePairs = (ICollection<KeyValuePair<string, object>>)dataAnonymous;

            foreach (var kvp in dictionary)
                valuePairs.Add(kvp);

            dynamic anonymous = dataAnonymous;

            return anonymous;
        }

        /// <summary>
        ///  SETA VALOR EM UMA PROP POR NAME
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(object obj, string propName, object value)
        {
            obj.GetType().GetProperty(propName)?.SetValue(obj, value, null);
        }


        /// <summary>
        ///  METODO PARA OBTER OS CAMPOS QUE FORAM RECEBIDOS NO JSON  UTILIZADO EM CONJUNTO COM SETIFDIFERENT PARA SIMULAR
        ///  METODO PUT
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string[] GetFieldsFromBody(this HttpContext httpContext)
        {
            var dic = new string[] { };
            try
            {
                if (httpContext.Request.Body.CanSeek)
                {
                    httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                    using var reader = new StreamReader(httpContext.Request.Body);
                    var body = reader.ReadToEnd();
                    dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(body)
                        .Select(x => x.Key?.UppercaseFirst()).ToArray();
                }
            }
            catch (Exception)
            {
                //ignored
            }

            return dic;
        }

        /// <summary>
        ///  METODO PARA OBTER OS CAMPOS QUE FORAM RECEBIDOS NO JSON  UTILIZADO EM CONJUNTO COM SETIFDIFERENT PARA SIMULAR
        ///  METODO PUT
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string[] GetFieldsFromBody(this IHttpContextAccessor httpContext)
        {
            var dic = new string[] { };
            try
            {
                if (httpContext.HttpContext.Request.Body.CanSeek)
                {
                    httpContext.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                    using var reader = new StreamReader(httpContext.HttpContext.Request.Body);
                    var body = reader.ReadToEnd();
                    dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(body)
                        .Select(x => x.Key?.UppercaseFirst()).ToArray();
                }
            }
            catch (Exception)
            {
                //ignored
            }

            return dic;
        }

        /// <summary>
        ///  METODO PARA ALTERAR HORARIO DE UM DATETIME
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTime ChangeTime(this DateTime dateTime, int hours = 0, int minutes = 0, int seconds = 0,
            int milliseconds = 0)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }

        /// <summary>
        ///  OBTER NOME DOS CAMPOS ALTERADOS
        /// </summary>
        public static List<string> GetDiferentFields<T, TY>(this T target, TY source) where T : class
        where TY : class
        {
            var response = new List<string>();
            try
            {
                var listProperties = source.GetType().GetProperties().ToList();
                for (int i = 0; i < listProperties.Count(); i++)
                {
                    var prop = listProperties[i];
                    var targetValue = GetValueByProperty(target, prop.Name);

                    var sourceValue = GetValueByProperty(source, prop.Name);

                    if (Equals(targetValue, sourceValue) == false)
                        response.Add(prop.Name);
                }
            }
            catch (Exception)
            {
                /*unused*/
            }

            return response;
        }


        /// <summary>
        ///  UPDATE GENERICO POR PROP ALTERADAS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TY"></typeparam>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static T SetIfDifferent<T, TY>(this T destination, TY source, IMapper mapper = null) where T : class
            where TY : class
        {
            var listProperties = source.GetType().GetProperties().ToList();
            var slnName = Assembly.GetEntryAssembly().GetName().Name?.Split('.')[0];

            for (int i = 0; i < listProperties.Count; i++)
            {
                var prop = listProperties[i];
                var custonTargetPropertie = (DataPropertie)prop.GetCustomAttributes(typeof(DataPropertie), false).FirstOrDefault();

                var destinationPropName = custonTargetPropertie?.PropertieName ?? prop.Name;

                var targetValue = GetValueByProperty(destination, destinationPropName);

                var sourceValue = GetValueByProperty(source, prop.Name);

                var isClass = prop.PropertyType.GetTypeInfo().IsClass && (prop.IsDefined(typeof(IsClass)) || prop.PropertyType.FullName.ContainsIgnoreCase(slnName));

                //CASO MESMO VALOR OU ATRIBUTO APENAS LEITURA
                if (Equals(targetValue, sourceValue) || prop.IsDefined(typeof(IsReadOnly)))
                    continue;

                if (isClass && mapper != null)
                {
                    try
                    {
                        var destinationProp = destination.GetType().GetProperty(destinationPropName);

                        sourceValue = mapper.Map(sourceValue, prop.PropertyType, destinationProp.PropertyType);
                    }
                    catch (Exception) { }
                }

                // VERIFICA SE EXISTE VALOR OU ACEITA NULL
                if (Equals(sourceValue, null) == false || prop.IsDefined(typeof(IsNotNull)) == false)
                    SetPropertyValue(destination, custonTargetPropertie?.PropertieName ?? prop.Name, sourceValue);
            }

            return destination;
        }

        /// <summary>
        ///  UPDATE GENERICO POR PROP ALTERADAS COM CAMPOS INFORMADOS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TY"></typeparam>
        /// <param name="fields"></param>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static T SetIfDifferent<T, TY>(this T destination, TY source, List<string> fields, IMapper mapper = null) where T : class
            where TY : class
        {

            var slnName = Assembly.GetEntryAssembly().GetName().Name?.Split('.')[0];

            for (var i = 0; i < fields.Count; i++)
            {
                var prop = source.GetType().GetProperty(fields[i]);

                if (prop == null)
                    continue;

                var custonTargetPropertie =
                    (DataPropertie)prop.GetCustomAttributes(typeof(DataPropertie), false).FirstOrDefault();

                var destinationValue = GetValueByProperty(destination, custonTargetPropertie?.PropertieName ?? prop.Name);

                var sourceValue = GetValueByProperty(source, prop.Name);

                var isClass = prop.PropertyType.GetTypeInfo().IsClass && (prop.IsDefined(typeof(IsClass)) || prop.PropertyType.FullName.ContainsIgnoreCase(slnName));

                //CASO MESMO VALOR OU ATRIBUTO APENAS LEITURA
                if (Equals(destinationValue, sourceValue) || prop.IsDefined(typeof(IsReadOnly)))
                    continue;

                if (isClass && mapper != null)
                {
                    try
                    {
                        var destinationProp = destination.GetType().GetProperty(fields[i]);

                        sourceValue = mapper.Map(sourceValue, prop.PropertyType, destinationProp.PropertyType);
                    }
                    catch (Exception) { }
                }

                // VERIFICA SE EXISTE VALOR OU ACEITA NULL
                if (Equals(sourceValue, null) == false || prop.IsDefined(typeof(IsNotNull)) == false)
                    SetPropertyValue(destination, custonTargetPropertie?.PropertieName ?? prop.Name, sourceValue);
            }

            return destination;
        }

        /// <summary>
        ///  UPDATE GENERICO POR PROP ALTERADAS COM CAMPOS INFORMADOS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TY"></typeparam>
        /// <param name="fields"></param>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static T SetIfDifferent<T, TY>(this T destination, TY source, string[] fields, IMapper mapper = null) where T : class
            where TY : class
        {

            var slnName = Assembly.GetEntryAssembly().GetName().Name?.Split('.')[0];

            for (var i = 0; i < fields.Length; i++)
            {
                var prop = source.GetType().GetProperty(fields[i]);

                if (prop == null)
                    continue;

                var custonTargetPropertie =
                    (DataPropertie)prop.GetCustomAttributes(typeof(DataPropertie), false).FirstOrDefault();

                var destinationValue = GetValueByProperty(destination, custonTargetPropertie?.PropertieName ?? prop.Name);

                var sourceValue = GetValueByProperty(source, prop.Name);

                var isClass = prop.PropertyType.GetTypeInfo().IsClass && (prop.IsDefined(typeof(IsClass)) || prop.PropertyType.FullName.ContainsIgnoreCase(slnName));

                //CASO MESMO VALOR OU ATRIBUTO APENAS LEITURA
                if (Equals(destinationValue, sourceValue) || prop.IsDefined(typeof(IsReadOnly)))
                    continue;

                if (isClass && mapper != null)
                {
                    try
                    {
                        var destinationProp = destination.GetType().GetProperty(fields[i]);

                        sourceValue = mapper.Map(sourceValue, prop.PropertyType, destinationProp.PropertyType);
                    }
                    catch (Exception) { }
                }

                // VERIFICA SE EXISTE VALOR OU ACEITA NULL
                if (Equals(sourceValue, null) == false || prop.IsDefined(typeof(IsNotNull)) == false)
                    SetPropertyValue(destination, custonTargetPropertie?.PropertieName ?? prop.Name, sourceValue);
            }

            return destination;
        }

        /// <summary>
        ///  SALVAR IMAGEM DA ROTA PELO PATH
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="pathRoute"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task SaveImageToRoute(string imagePath, string pathRoute, int width = 750, int height = 360,
            string type = "roadmap", string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = GetConfigurationRoot().GetValue<string>("googleMapsKey");

                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a googleMapsKey no settings/Config.json");
            }

            if (string.IsNullOrEmpty(imagePath))
                throw new Exception("Informe o caminho onde será salvo o arquivo");
            if (string.IsNullOrEmpty(pathRoute))
                throw new Exception("Informe o path da rota realizada");

            var baseUrl =
                $"https://maps.googleapis.com/maps/api/staticmap?size={width}x{height}&maptype={type}&path=weight:3%7Ccolor:0x000000ff%7Cenc:{pathRoute}&key={key}";

            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

            using Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                  stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true);

            await contentStream.CopyToAsync(stream);


        }

        /// <summary>
        ///  CONVERTER PARA DICIONARIO
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, TValue> ToDictionary<TValue>(this object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
                return dictionary;
            }
            catch (Exception)
            {
                return new Dictionary<string, TValue>();
            }
        }
        /// <summary>
        /// APLICAR MASCARA EM STRING
        /// A-Z= VALUE | ""
        /// 9= VALUE | ""
        /// 0= VALUE | "0"
        /// D= VALUE | "0"
        /// </summary>
        /// <param name="str">TEXTO</param>
        /// <param name="mask">PATTER EX: 00000-000</param>
        /// <param name="reverse">PREENCIMENTO REVERSO DIREITA PARA ESQUERDA</param>
        /// <param name="maskBank">CASO VERDADEIRO CASO MASK CONTENHA - ADICIONA -0 NO FIM DO TEXTO</param>
        /// <returns></returns>
        public static string ApplyMask(this string str, string mask, bool reverse = false, bool maskBank = false)
        {
            try
            {
                if (string.IsNullOrEmpty(mask))
                    throw new Exception("Informe uma mascara a ser aplicada, ex: 00000-000");

                if (string.IsNullOrEmpty(str))
                    return str;

                if (maskBank && !reverse && mask.IndexOf('-') != -1 && str.IndexOf('-') == -1)
                    str += "-0";

                var partsMask = mask.Select(x => x.ToString()).ToList();
                var partsStr = str.Select(x => x.ToString()).ToList();
                var partsMaskCount = partsMask.Count();
                var result = new string[partsMaskCount];

                var toReplace = new HashSet<string>() { "D", "A", "0", "9" };

                if (reverse)
                {
                    for (int i = partsMaskCount - 1; i >= 0; i--)
                    {
                        var partMask = partsMask[i];
                        var partsStrIndex = partsStr.Count - 1;

                        if (partsStrIndex < 0 && string.IsNullOrEmpty(partMask.Trim()))
                            break;

                        if (toReplace.Contains(partMask) == false || (maskBank && partsStrIndex < 0))
                        {
                            result[i] = partMask;
                            continue;
                        }

                        if (partsStrIndex >= 0)
                        {
                            result[i] = partsStr[partsStrIndex];
                            partsStr.RemoveAt(partsStrIndex);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < partsMaskCount; i++)
                    {
                        var partMask = partsMask[i];

                        if (partsStr.Count == 0)
                            break;

                        if (toReplace.Contains(partMask) == false)
                        {
                            result[i] = partMask;
                            continue;
                        }

                        result[i] = partsStr[0];
                        partsStr.RemoveAt(0);
                    }
                }

                return string.Join("", result);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string ReplacePart(this List<string> partsStr, string type, int i, bool maskBank)
        {
            try
            {
                var value = "";
                type = type?.ToUpper();

                switch (type)
                {
                    case var t when new Regex(@"[A-Z]", RegexOptions.IgnoreCase).IsMatch(t.ToString()):
                        value = i >= 0 && partsStr.ElementAtOrDefault(i) != null ? partsStr[i].ToString() : "";
                        break;
                    case "0":
                        value = i >= 0 && partsStr.ElementAtOrDefault(i) != null ? partsStr[i].OnlyNumbers() : "0";
                        break;
                    case "9":
                        value = i >= 0 && partsStr.ElementAtOrDefault(i) != null ? partsStr[i].OnlyNumbers() : "";
                        break;
                    default:
                        value = type;
                        break;
                }

                if (string.IsNullOrEmpty(value) && (type == "D" && maskBank || type == "0"))
                    value = "0";

                return value;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        ///  GERATE TIME SPAM USE LIB TOKEN
        /// </summary>
        /// <param name="from"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan GerateTimeSpan(this string from, double value)
        {
            switch (from.ToLower())
            {
                case "fromdays":
                    return TimeSpan.FromDays(value);
                case "fromhours":
                    return TimeSpan.FromHours(value);
                case "frommilliseconds":
                    return TimeSpan.FromMilliseconds(value);
                case "fromseconds":
                    return TimeSpan.FromSeconds(value);
                case "fromminutes":
                    return TimeSpan.FromMinutes(value);
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        ///  CALCULAR IDADE
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public static int CalculeAge(this long birthDate)
        {
            var birthdate = birthDate.TimeStampToDateTime();

            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate > today.AddYears(-age))
                age--;

            return age;
        }

        /// <summary>
        ///  CALCULAR IDADE
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public static int CalculeAge(this DateTime birthDate)
        {
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthDate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthDate > today.AddYears(-age))
                age--;

            return age;
        }

        /// <summary>
        ///  gerar hash em Md5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GerarHashMd5(string input)
        {
            if (string.IsNullOrEmpty(BaseConfig.SecretKey))
                throw new Exception("Informe o secretKey");

            var md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            var sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                byte t = data[i];
                sBuilder.Append(t.ToString("x2"));
            }

            return $"{sBuilder}{BaseConfig.SecretKey}";
        }

        /// <summary>
        ///  CALCULAR DISTANCIA
        /// </summary>
        /// <param name="origemLat"></param>
        /// <param name="origemLng"></param>
        /// <param name="destinoLat"></param>
        /// <param name="destinoLng"></param>
        /// <param name="unit">VALUES K = [Kilometers] , M = [Miles], N  = [Nautical Miles]| DEFAULT = K</param>
        /// <returns></returns>
        public static double GetDistance(double origemLat, double origemLng, double destinoLat, double destinoLng,
            char unit = 'K')
        {
            var rlat1 = Math.PI * origemLat / 180;
            var rlat2 = Math.PI * destinoLat / 180;
            var theta = origemLng - destinoLng;
            var rtheta = Math.PI * theta / 180;
            var dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }

        /// <summary>
        ///  VALIDAR CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static bool ValidCnpj(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return false;

            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            // valida 111.111.111-11
            if (cnpj.Distinct().Count() == 1)
                return false;

            var tempCnpj = cnpj.Substring(0, 12);
            var soma = 0;
            for (var i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (var i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cnpj.EndsWith(digito);
        }

        /// <summary>
        ///  VALIDAR CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool ValidCpf(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.Trim().OnlyNumbers();

            // valida 111.111.111-11
            if (cpf.Distinct().Count() == 1)
                return false;

            if (cpf.Length != 11)
                return false;

            var mt1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var mt2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;

            var tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cpf.EndsWith(digito);
        }

        /// <summary>
        ///  VALIDAR EMAIL
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool ValidEmail(this string email)
        {
            try
            {
                var validEmail = new EmailAddressAttribute();

                return validEmail.IsValid(email);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  PARSE STRING dd/MM/yyyy HH:mm:ss TO DATETIME
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static DateTime TryParseAnyDate(this string dateValue)
        {
            try
            {
                IFormatProvider cultureInfo = new CultureInfo("pt-BR");

                if (DateTime.TryParse(dateValue, cultureInfo, DateTimeStyles.None, out var result))
                    return result;

                throw new Exception("Verifique a data informada");
            }
            catch (Exception ex)
            {
                throw new Exception("Verifique a data informada", ex);
            }
        }

        /// <summary>
        ///  remove all spaces
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string TrimSpaces(this string text)
        {
            return text?.Replace(" ", "");
        }

        /// <summary>
        ///  GENERATE RANDOM STRING
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYtZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// JUNTAR LISTA DE OBJ POR CAMPO STRING EM UMA STRING = RESULT (MAÇA, BANANA, ETC)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="field"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static string CustomJoin<T>(this List<T> list, string field, string aggregate = ", ") where T : class
        {
            try
            {
                return string.Join(aggregate, list.Select(x => GetValueByProperty(x, field)))?.Trim()?.TrimEnd(',');
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// JUNTAR LISTA DE STRING EM UMA STRING = RESULT (MAÇA, BANANA, ETC)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static string CustomJoin(this List<string> list, string aggregate = ", ")
        {
            try
            {
                return string.Join(aggregate, list)?.Trim()?.TrimEnd(',');
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// JUNTAR LISTA DE STRING EM UMA STRING = RESULT (MAÇA, BANANA, ETC)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="aggregate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string CustomJoin<T>(this List<T> list, string aggregate = ", ") where T : new()
        {
            try
            {
                return string.Join(aggregate, list.Select(x => x.ToString()))?.Trim()?.TrimEnd(',');
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// RETORNA RAIO EM KM
        /// </summary>
        /// <param name="radius">VALUE IN KM</param>
        /// <returns></returns>
        public static double CalculateRadius(double radius)
            => Math.Abs(radius) < 0 ? 0 : radius / 111.12;


        /// <summary>
        ///  GENERATE RANDOM STRING
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomInt(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        ///  CONVERTE TIMESTAMP EM DATETIME
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime TimeStampToDateTime(this long unixTimeStamp)
        {
            try
            {
                if (unixTimeStamp == 0)
                    throw new Exception("Informe um timestamp valido");
                return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).DateTime.ToLocalTime();
            }
            catch (Exception)
            {
                throw new Exception("Informe um timestamp valido");
            }
        }

        /// <summary>
        ///  RETORNA APENAS NUMEROS
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string OnlyNumbers(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            text = Regex.Replace(text, @"[^\w\s]", "");

            if (string.IsNullOrEmpty(text))
                return text;
            text = Regex.Replace(text, @"[^\d$]", "");

            return text.TrimSpaces();
        }

        /// <summary>
        ///  OBTER INFORMAÇÕES SOBRE LAT LNG INFORMADA
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="key">CUSTOM KEY GMAPS</param>
        /// <returns></returns>
        public async static Task<AddressViewModel> GetInfoLocation(double lat, double lng, string key = "")
        {

            if (string.IsNullOrEmpty(key))
            {
                key = GetConfigurationRoot().GetValue<string>("googleMapsKey");
                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a googleMapsKey no settings/Config.json");
            }
            if (Math.Abs(lat) < 0 || Math.Abs(lng) < 0)
                return new AddressViewModel() { Erro = true };

            var _return = new AddressViewModel();
            var latitude = lat.ToString(CultureInfo.InvariantCulture).Replace(',', '.');
            var longitude = lng.ToString(CultureInfo.InvariantCulture).Replace(',', '.');

            var client = new RestClient($"https://maps.googleapis.com/maps/api/geocode/json?sensor=false&language=pt-br&latlng={latitude},{longitude}&key={key}");

            var request = new RestRequest(Method.GET);

            var response = await client.ExecuteAsync<GmapsResult>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return new AddressViewModel { Erro = true, ErroMessage = response?.Data?.ErroMessage };

            if (response.Data.Results.Count() == 0)
                return new AddressViewModel { Erro = true, ErroMessage = response?.Data?.ErroMessage };

            var location = response.Data.Results[0];

            var addressComp = location.AddressComponents;
            _return.FormatedAddress = location.FormattedAddress;
            _return.Street = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "route")?.LongName;
            _return.Number = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "street_number")
            ?.ShortName;
            _return.City = addressComp
            .FirstOrDefault(x => x.Types.Any() && x.Types.First() == "administrative_area_level_2")?.ShortName;
            _return.State = addressComp
            .FirstOrDefault(x => x.Types.Any() && x.Types.First() == "administrative_area_level_1")?.ShortName;
            _return.Country = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "country")?.ShortName;
            _return.Neighborhood = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "political")
            ?.ShortName;
            _return.PostalCode = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "postal_code")
            ?.ShortName;
            _return.ErroMessage = response.Data?.ErroMessage;
            return _return;
        }

        /// <summary>
        ///  OBTER INFORMAÇÕES DE UM ENDEREÇO
        /// </summary>
        /// <param name="address"></param>
        /// <param name="key">KEY GMAPS</param>
        /// <returns></returns>
        public async static Task<AddressViewModel> GetInfoFromAdressLocation(string address, string key = "")
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = GetConfigurationRoot().GetValue<string>("googleMapsKey");

                    if (string.IsNullOrEmpty(key))
                        throw new Exception("Informe a googleMapsKey no settings/Config.json");
                }

                var _return = new AddressViewModel();

                var client = new RestClient($"https://maps.googleapis.com/maps/api/geocode/json?new_forward_geocoder=true&address={address}&key={key}");

                var request = new RestRequest(Method.GET);

                var response = await client.ExecuteAsync<GmapsResult>(request);

                if (response.StatusCode != HttpStatusCode.OK)
                    return new AddressViewModel { Erro = true, ErroMessage = response?.Data?.ErroMessage };

                if (response.Data.Results.Any() == false)
                    return new AddressViewModel { Erro = true, ErroMessage = response?.Data?.ErroMessage };

                var location = response.Data.Results.First();
                var addressComp = location.AddressComponents;

                _return.AddressComponents = location.AddressComponents;
                _return.FormatedAddress = location.FormattedAddress;
                _return.Street = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "route")?.LongName;
                _return.Number = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "street_number")
                    ?.ShortName;
                _return.City = addressComp
                    .FirstOrDefault(x => x.Types.Any() && x.Types.First() == "administrative_area_level_2")?.ShortName;
                _return.State = addressComp
                    .FirstOrDefault(x => x.Types.Any() && x.Types.First() == "administrative_area_level_1")?.ShortName;
                _return.Country = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "country")?.ShortName;
                _return.Neighborhood = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "political")
                    ?.ShortName;
                _return.PostalCode = addressComp.FirstOrDefault(x => x.Types.Any() && x.Types.First() == "postal_code")
                    ?.ShortName;
                _return.Geometry = location.Geometry;
                _return.ErroMessage = response.ErrorMessage;
                return _return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  OBTER NOME UNICO PARA ARQUIVO
        /// </summary>
        /// <param name="name"></param>
        /// <param name="savePath"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetUniqueFileName(string name, string savePath, string ext)
        {
            name = Path.GetFileNameWithoutExtension(name);

            if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                name = Regex.Replace(name, @"[^\w\s]", "");
            }

            var newName = name;
            var i = 0;
            if (File.Exists($"{savePath}{newName}{ext}"))
            {
                do
                {
                    i++;
                    newName = $"{name}_{i}";
                } while (File.Exists($"{savePath}{newName}{ext}"));
            }
            return newName;
        }
        /// <summary>
        ///  OBTER NOME UNICO PARA ARQUIVO
        /// </summary>
        /// <param name="fullPathFile"></param>
        /// <returns></returns>
        public static string GetUniqueFileName(string fullPathFile)
        {
            var sourceName = Path.GetFileNameWithoutExtension(fullPathFile);
            var savePath = Path.GetDirectoryName(fullPathFile);
            var ext = Path.GetExtension(fullPathFile);

            if (sourceName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                sourceName = Regex.Replace(sourceName, @"[^\w\s]", "");
            }

            var newName = sourceName;
            var i = 0;
            if (File.Exists($"{savePath}{newName}{ext}"))
            {
                do
                {
                    i++;
                    newName = $"{sourceName}_{i}";
                } while (File.Exists($"{savePath}{newName}{ext}"));
            }
            return newName;
        }

        /// <summary>
        ///     MASCARA DE TELEFONE
        /// </summary>
        /// <param name="telefone"></param>
        /// <param name="showZero"></param>
        /// <returns></returns>
        public static string MaskTelefone(this string telefone, bool showZero = false)
        {
            if (string.IsNullOrEmpty(telefone))
                return telefone;

            telefone = telefone.OnlyNumbers();

            while (telefone.StartsWith("0"))
                telefone = telefone.TrimStart('0');

            if (telefone.Length < 11)
            {
                if (showZero)
                    telefone = $"0{telefone}";

                return showZero
                    ? Convert.ToUInt64(telefone).ToString(@"(000) 0000\-0000")
                    : Convert.ToUInt64(telefone).ToString(@"(00) 0000\-0000");
            }

            if (telefone.Length > 12)
                return telefone;

            if (telefone.Length > 11)
                telefone = telefone.Substring(0, 11);

            if (showZero)
                telefone = $"0{telefone}";

            return showZero
                ? Convert.ToUInt64(telefone).ToString(@"(000) 00000\-0000")
                : Convert.ToUInt64(telefone).ToString(@"(00) 00000\-0000");
        }

        /// <summary>
        ///  RETORNO DE SUCESS
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ReturnViewModel ReturnSuccess(string message = "Sucesso", object data = null)
        {
            return new ReturnViewModel
            {
                Data = data,
                Message = message
            };
        }

        /// <summary>
        ///  RETORNO DE ERRO COM EXCEPTION
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="errors"></param>
        /// <param name="responseList"></param>
        /// <returns></returns>
        public static ReturnViewModel ReturnErro(this Exception ex,
            string message = "Ocorreu um erro, verifique e tente novamente", object data = null, object errors = null,
            bool responseList = false)
        {
            if (ex is TimeoutException)
            {
                message = "Serviço temporariamente indisponível.";
            }

            var messageEx = $"{ex.InnerException} {ex.Message}";

            return new ReturnViewModel
            {
                Data = responseList == false ? data : new List<object>(),
                Erro = true,
                Errors = errors,
                MessageEx = messageEx,
                Message = message
            };
        }

        /// <summary>
        ///  RETORNO DE ERRO FORÇADO
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="errors"></param>
        /// <param name="responseList"></param>
        /// <param name="messageEx"></param>
        /// <returns></returns>
        public static ReturnViewModel ReturnErro(string message = "Ocorreu um erro, verifique e tente novamente",
            object data = null, object errors = null, bool responseList = false, string messageEx = null)
        {
            return new ReturnViewModel
            {
                Data = responseList == false ? data : new List<object>(),
                Erro = true,
                Errors = errors,
                Message = message,
                MessageEx = messageEx
            };
        }

        /// <summary>
        ///  RETORNA ULTIMO NOME
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetLastName(this string text)
        {
            var parts = text.Trim()?.Split(' ') ?? new string[] { };
            return parts.Length == 0 ? "" : string.Join(" ", parts.Skip(1).ToList());
        }

        /// <summary>
        ///  RETORNA SOBRENOME COMPLETO
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetFirstName(this string text)
        {
            var parts = text?.Trim().Split(' ') ?? new string[] { };
            return parts.Length > 0 ? parts[0] : text?.Trim();
        }

        /// <summary>
        ///  REMOVE ESPAÇOS CAMPOS STRING
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="input"></param>
        /// <param name="removeEmoji"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static TSelf TrimStringProperties<TSelf>(this TSelf input, bool removeEmoji = false, string regex = null)
        {
            if (input == null)
                throw new Exception("Verifique os dados informados. json inválido");

            var stringProperties = input.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string)).ToList();

            stringProperties.ForEach(stringProperty =>
            {
                var currentValue = (string)stringProperty.GetValue(input, null);
                if (currentValue != null)
                {
                    if (removeEmoji && string.IsNullOrEmpty(currentValue) == false)
                        currentValue = Regex.Replace(currentValue, @"\p{Cs}", "");
                    if (string.IsNullOrEmpty(regex) == false && string.IsNullOrEmpty(currentValue) == false)
                        currentValue = Regex.Replace(currentValue, regex, "");
                }

                stringProperty.SetValue(input, currentValue?.Trim(), null);
            });
            return input;
        }

        /// <summary>
        ///  VALIDATE LATITUDE FORMAT VALUE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool LatIsValid(this double value, double epsilon = 0.000001)
        {
            return value >= (-90.0 - epsilon) && value <= (90.0 + epsilon);
        }

        /// <summary>
        ///  VALIDADE LONGITUDE FORMAT VALUE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool LngIsValid(this double value, double epsilon = 0.000001)
        {
            return value >= (-180.0 - epsilon) && value <= (180.0 + epsilon);
        }

        /// <summary>
        ///  CAST STRING VALUE TO ENUM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        ///  VALIDA CAMPOS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyValid"></param>
        /// <returns></returns>
        [Obsolete("Please use ValidModelState")]
        public static string ValidModel(this object model, string[] propertyValid)
        {
            var message = "Verifique os dados informados e tente novamente.";
            propertyValid = propertyValid ?? new string[] { };
            if (model == null)
                return message;

            var propRequired = model.GetType().GetProperties().Where(x => propertyValid.Contains(x.Name)).ToList();

            message = "Informe os campos: " + (from pi in propRequired
                                               where pi.PropertyType == typeof(string)
                                               let name = pi.Name
                                               let value = (string)pi.GetValue(model)
                                               where string.IsNullOrEmpty(value)
                                               select name).Aggregate("", (current, name) => current + $" {name.LowercaseFirst()}; ");

            return message == "Informe os campos: " ? null : message.Trim().TrimEnd(';');
        }


        /// <summary>
        ///  valid model from data anotations
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="ignoreFields">properties ignore</param>
        /// <param name="message">message retrun</param>
        /// <returns></returns>
        public static ReturnViewModel ValidModelState(this ModelStateDictionary modelState,
                                                      string[] ignoreFields = null,
                                                      string message = null)
        {
            try
            {
                if (modelState.IsValid)
                    return null;

                Dictionary<string, List<string>> errors;

                if (ignoreFields == null || ignoreFields.Length == 0)
                {
                    errors = modelState.Keys
                        .ToDictionary(item => item.LowercaseFirst(), item => modelState[item].Errors.Select(x => x.ErrorMessage).ToList());
                }
                else
                {
                    errors = modelState.Keys
                        .Where(field => ignoreFields.Count(fieldIgnored => field.EqualsIgnoreCase(fieldIgnored)) == 0 && modelState[field].Errors.Count > 0)
                        .ToDictionary(item => item.LowercaseFirst(), item => modelState[item].Errors.Select(x => x.ErrorMessage).ToList());
                }

                if (errors.Count == 0)
                    return null;

                if (string.IsNullOrEmpty(message) == false)
                {
                    return new ReturnViewModel
                    {
                        Errors = errors,
                        Erro = true,
                        Message = message
                    };
                }

                message = errors.Values.FirstOrDefault()?.FirstOrDefault() ?? "";

                return new ReturnViewModel
                {
                    Errors = errors,
                    Erro = true,
                    Message = message
                };
            }
            catch (Exception e)
            {
                return e.ReturnErro("Ocorreu um erro inesperado");
            }
        }

        /// <summary>
        ///  VALIDAÇÃO DE MODEL VIA DATA ANOTATIONS
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="ignoredFields"></param>
        /// <returns></returns>
        public static ReturnViewModel ValidModelState(this ModelStateDictionary modelState,
                                                      params string[] ignoredFields)
        {
            try
            {
                if (modelState.IsValid)
                    return null;

                Dictionary<string, List<string>> errors;

                if (ignoredFields == null || ignoredFields.Length == 0)
                {
                    errors = modelState.Keys
                        .ToDictionary(item => item.LowercaseFirst(), item => modelState[item].Errors.Select(x => x.ErrorMessage).ToList());
                }
                else
                {
                    errors = modelState.Keys
                        .Where(field => ignoredFields.Count(fieldIgnored => field.EqualsIgnoreCase(fieldIgnored)) == 0 && modelState[field].Errors.Count > 0)
                        .ToDictionary(item => item.LowercaseFirst(), item => modelState[item].Errors.Select(x => x.ErrorMessage).ToList());
                }

                if (errors.Count == 0)
                    return null;

                var message = errors.Values.FirstOrDefault()?.FirstOrDefault() ?? "";

                return new ReturnViewModel
                {
                    Errors = errors,
                    Erro = true,
                    Message = message
                };
            }
            catch (Exception e)
            {
                return e.ReturnErro("Ocorreu um erro inesperado");
            }
        }

        /// <summary>
        ///  VALID STATE MODEL ONLY SELECTED FIELDS
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="validOnlyFields"></param>
        /// <returns></returns>
        public static ReturnViewModel ValidModelStateOnlyFields(this ModelStateDictionary modelState,
                                                                params string[] validOnlyFields)
        {
            try
            {
                if (modelState.IsValid)
                    return null;

                Dictionary<string, List<string>> errors;

                if (validOnlyFields == null || validOnlyFields.Length == 0)
                {
                    errors = modelState.Keys
                        .ToDictionary(item => item.LowercaseFirst(), item => modelState[item].Errors.Select(x => x.ErrorMessage).ToList());
                }
                else
                {
                    errors = modelState.Keys
                        .Where(field => validOnlyFields.Count(checkField => field.EqualsIgnoreCase(checkField)) > 0 && modelState[field].Errors.Count > 0)
                        .ToDictionary(item => item.LowercaseFirst(), item => modelState[item].Errors.Select(x => x.ErrorMessage).ToList());
                }

                if (errors.Count == 0)
                    return null;

                var message = errors.Values.FirstOrDefault()?.FirstOrDefault() ?? "";

                return new ReturnViewModel
                {
                    Errors = errors,
                    Erro = true,
                    Message = message
                };
            }
            catch (Exception e)
            {
                return e.ReturnErro("Ocorreu um erro inesperado");
            }
        }

        /// <summary>
        ///  CLEAR INVALID PROPERTIES IN MODELSTATE
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="properties"></param>
        public static void ClearInvalidProperties(this ModelStateDictionary modelState, params string[] properties)
        {
            foreach (var t in properties)
            {
                modelState[t].Errors.Clear();
                modelState[t].ValidationState = ModelValidationState.Valid;
            }
        }

        /// <summary>
        ///  DIFERENÇA ENTRE UNIX DATE EM STRING
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string TimeAgo(this long dateTime)
        {
            string result;
            var timeSpan = DateTime.Now.Subtract(dateTime.TimeStampToDateTime().ToLocalTime());

            if (timeSpan <= TimeSpan.FromSeconds(60))
                result = $"{timeSpan.Seconds} segundos";
            else if (timeSpan <= TimeSpan.FromMinutes(60))
                result = timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutos" : "1 minuto";
            else if (timeSpan <= TimeSpan.FromHours(24))
                result = timeSpan.Hours > 1 ? $"{timeSpan.Hours} horas" : "1 hora";
            else if (timeSpan <= TimeSpan.FromDays(30))
                result = timeSpan.Days > 1 ? $"{timeSpan.Days} dias" : "Ontem";
            else if (timeSpan <= TimeSpan.FromDays(365))
                result = timeSpan.Days > 59 ? $"{timeSpan.Days / 30} meses" : "1 mês";
            else
                result = timeSpan.Days > 729 ? $"{timeSpan.Days / 365} anos" : "1 ano";

            return result;
        }

        /// <summary>
        ///  DIFERENÇA ENTRE HORARIOS EM STRING
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string TimeAgo(this DateTime dateTime)
        {
            string result;
            var timeSpan = DateTime.Now.Subtract(dateTime.ToLocalTime());

            if (timeSpan <= TimeSpan.FromSeconds(60))
                result = $"{timeSpan.Seconds} segundos";
            else if (timeSpan <= TimeSpan.FromMinutes(60))
                result = timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutos" : "1 minuto";
            else if (timeSpan <= TimeSpan.FromHours(24))
                result = timeSpan.Hours > 1 ? $"{timeSpan.Hours} horas" : "1 hora";
            else if (timeSpan <= TimeSpan.FromDays(30))
                result = timeSpan.Days > 1 ? $"{timeSpan.Days} dias" : "Ontem";
            else if (timeSpan <= TimeSpan.FromDays(365))
                result = timeSpan.Days > 59 ? $"{timeSpan.Days / 30} meses" : "1 mês";
            else
                result = timeSpan.Days > 729 ? $"{timeSpan.Days / 365} anos" : "1 ano";

            return result;
        }


        /// <summary>
        ///  OBTEM VALOR DA CLAIM POR NOME
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="pCampo"></param>
        /// <returns></returns>
        public static string GetClaim(this ClaimsPrincipal principal, string pCampo)
        {
            var vClaims = principal.Identities.First();
            try
            {
                return vClaims.Claims.FirstOrDefault(p => p.Type == pCampo)?.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///  SET MASK PHONE
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static string SetMaskPhone(this string strNumber)
        {
            try
            {
                var strOnlyNumber = strNumber.OnlyNumbers();
                // por omissão tem 10 ou menos dígitos
                var strMask = "{0:(00) 0000-0000}";
                // converter o texto em número
                var number = Convert.ToInt64(strOnlyNumber);

                var length = strOnlyNumber.Length;

                switch (length)
                {
                    case 11:
                        strMask = "{0:(00) 00000-0000}";
                        break;
                    case 12:
                        strMask = "{0:+00 (00) 0000-0000}";
                        break;
                    case 13:
                        strMask = "{0:+00 (00) 00000-0000}";
                        break;
                }

                return string.Format(strMask, number);
            }
            catch (Exception)
            {
                return strNumber;
            }
        }


        /// <summary>
        ///  RETORNA FOTO DO FACEBOOK DEACORDO COM FACEBOOKID
        /// </summary>
        /// <param name="facebookId"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string GetPhotoFacebookGraph(this string facebookId, int width = 300)
        {
            return string.IsNullOrEmpty(facebookId)
                ? null
                : $"https://graph.facebook.com/{facebookId}/picture?width={width}";
        }

        /// <summary>
        ///  RETORNA FOTO DO GOOGLE+ DE ACORDO COM GOOGLEID
        /// </summary>
        /// <param name="googleId"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string GetPhotoGooglePlus(this string googleId, int width = 300)
        {
            return string.IsNullOrEmpty(googleId)
                ? null
                : $"https://plus.google.com/s2/photos/profile/{googleId}?sz={width}";
        }

        /// <summary>
        ///  GET CONFIGURAÇÃO ROOT
        /// </summary>
        /// <param name="path">APOS O ROOT</param>
        /// <param name="fileName">NOME DO ARQUIVO.JSON</param>
        /// <param name="environment"></param>
        /// <returns></returns>
#pragma warning disable CS0618 // Type or member is obsolete
        public static IConfigurationRoot GetConfigurationRoot(string path = "Settings", string fileName = "Config", IHostingEnvironment environment = null)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            var fullPath = $"{Directory.GetCurrentDirectory()}/{path}";

            if (environment != null)
            {
                fullPath = Directory.GetCurrentDirectory();
                fileName = $"appsettings.{environment.EnvironmentName}";
            }

            if (Directory.Exists(fullPath) == false)
                throw new Exception($"Arquivo Settings/{fileName}.json não encontrado");

            var builder = new ConfigurationBuilder()
                .SetBasePath(fullPath)
                .AddJsonFile($"{fileName}.json");

            return builder.Build();
        }

        /// <summary>
        ///  CONVERT DATETIME TO UNIX EPOCH SECONDS
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnix(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }
        /// <summary>
        ///  CONVERT DATETIME TO UNIXEPOCH SECONDS
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimeUnix(DateTime dateTime = default)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

        /// <summary>
        ///  PARA USO EM STR COM R$ OU BRL
        /// </summary>
        /// <param name="balanceAvailableForWithdraw"></param>
        /// <returns></returns>
        public static double ToDouble(this string balanceAvailableForWithdraw)
        {
            if (string.IsNullOrEmpty(balanceAvailableForWithdraw) == false)
            {
                string cleanedValue = Regex.Replace(balanceAvailableForWithdraw, @"[^\d,.]+", "").Replace(",", ".");
                cleanedValue = Regex.Replace(cleanedValue, @"(?<=\d)\.(?=\d*?\.)", "");

                if (double.TryParse(cleanedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    return value;
                }
            }
            return 0.0;
        }
        /// <summary>
        /// CONVERTER REAIS EM CENTAVOS
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static int ToCent(this decimal price)
        {
            return Convert.ToInt32($"{price * 100:0}");
        }
        /// <summary>
        /// CONVERTER REAIS EM CENTAVOS
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static int ToCent(this double price)
        {
            return Convert.ToInt32($"{price * 100:0}");
        }

        /// <summary>
        /// CONVERTER REAIS EM CENTAVOS
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static long ToCentLong(this decimal price)
        {
            return Convert.ToInt64($"{price * 100:0}");
        }

        /// <summary>
        /// CONVERTER REAIS EM CENTAVOS
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static long ToCentLong(this double price)
        {
            return Convert.ToInt64($"{price * 100:0}");
        }

        /// <summary>
        ///  PRIMEIRA LETRA MAIUSCULA
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UppercaseFirst(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : char.ToUpper(text[0]) + text.Substring(1);
        }

        /// <summary>
        /// PRIMEIRA LETRA MINUSCULA
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string LowercaseFirst(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : char.ToLower(text[0]) + text.Substring(1);
        }

        /// <summary>
        /// OBTER PRIMEIRO DIA DO MÊS
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        }

        /// <summary>
        /// OBTER ULTIMO DIA DO MÊS
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            var firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// ADICIONAR TAXAS TRANSACIONAIS
        /// </summary>
        /// <param name="value"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static double AddTransactionalFee(this double value, double percent)
            => ToDivide(value, ToSubtract(1, ToDivide(percent, 100)));
        /// <summary>
        /// OBTER VALOR DAS TAXAS TRANSACIONAIS
        /// </summary>
        /// <param name="value"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static double GetTransactionalFee(this double value, double percent)
            => ToSubtract(ToDivide(value, ToSubtract(1, ToDivide(percent, 100))), value);

        /// <summary>
        ///  OBTER VALOR DE UMA PORCENTAGEM
        /// </summary>
        /// <param name="totalValue"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static double GetValueOfPercent(this double totalValue, double percent)
            => ToDivide(ToMultiply(percent, totalValue), 100);

        /// <summary>
        ///  OBTER PORCENTAGEM DE UM VALOR
        /// </summary>
        /// <param name="totalValue"></param>
        /// <param name="partOfValue"></param>
        /// <returns></returns>
        public static double GetPercentOfValue(this double totalValue, double partOfValue)
            => ToMultiply(ToDivide(partOfValue, totalValue), 100);

        /// <summary>
        /// TRANSFORMA DECIMAL EM 2 CASAS SEM ARREDONDAR
        /// </summary>
        /// <param name="price">value for notAround</param>
        /// <returns></returns>
        public static decimal NotAround(this decimal price)
        {
            return Math.Truncate(price * 100) / 100;
        }

        /// <summary>
        ///  TRANSFORMA DOUBLE EM 2 CASAS SEM ARREDONDAR
        /// </summary>
        /// <param name="price">value for notAround</param>
        /// <returns></returns>
        public static double NotAround(this double price)
        {
            return ToDivide(Math.Truncate(ToMultiply(price, 100)), 100);
        }

        /// <summary>
        /// TRANSFORMA DOUBLE EM N CASAS SEM ARREDONDAR
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static double NotAround(this double value, int precision = 2)
        {
            double factorMultiply = Math.Pow(10, precision);
            value = Math.Truncate(ToMultiply(factorMultiply, value));
            return ToDivide(value, factorMultiply);
        }
        /// <summary>
        /// TRANSFORMA DOUBLE EM N CASAS SEM ARREDONDAR
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static decimal NotAround(this decimal value, int precision = 2)
        {
            decimal factorMultiply = (decimal)Math.Pow(10.0, precision);
            value = Math.Truncate(factorMultiply * value);
            return value / factorMultiply;
        }
        /// <summary>
        /// ARREDONDAR DEACORDO COM REGRA ABNT
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static double AroundABNT(this double value, int precision = 2)
        {
            var factorMultiply = Math.Pow(10, precision);
            var tempValue = Math.Round(ToMultiply(factorMultiply, value));
            return ToDivide(tempValue, factorMultiply);
        }
        /// <summary>
        /// ARREDONDAR DEACORDO COM REGRA ABNT
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static decimal AroundABNT(this decimal value, int precision = 2)
        {
            var factorMultiply = (decimal)Math.Pow(10, precision);
            var tempValue = Math.Round(factorMultiply * value);
            return tempValue / factorMultiply;
        }

        /// <summary>
        /// SOMA COM PRECISÃO DE DECIMAL
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double ToSum(double first, double second)
            => (double)((decimal)first + (decimal)second);
        /// <summary>
        /// SUBTRAÇÃO COM PRECISÃO DE DECIMAL
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double ToSubtract(double first, double second)
            => (double)((decimal)first - (decimal)second);
        /// <summary>
        /// MULTIPLICAÇÃO COM PRECISÃO DE DECIMAL
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double ToMultiply(double first, double second)
            => (double)((decimal)first * (decimal)second);
        /// <summary>
        /// DIVISÃO COM PRECISÃO DE DECIMAL
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double ToDivide(double first, double second)
            => (double)((decimal)first / (decimal)second);

        /// <summary>
        /// METODO PARA OBTER A PORCENTAGEM DE AUMENTO/REDUÇÃO
        /// </summary>
        /// <param name="initialValue"></param>
        /// <param name="finalValue"></param>
        /// <returns></returns>
        public static double CalculatePercentage(double initialValue, double finalValue)
        {
            try
            {
                double diff = ToSubtract(initialValue, finalValue);

                double percent = ToMultiply(ToDivide(diff, Math.Abs(initialValue)), 100);

                return percent;
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }

        /// <summary>
        /// VERIFICA SE É UM JSON VALIDO
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static bool IsJsonValid(this string jsonString)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonString))
                    return false;

                JToken.Parse(jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// INDENTA STRING JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string JsonPrettify(this string json)
        {
            try
            {
                using var stringReader = new StringReader(json);
                using var stringWriter = new StringWriter();
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
            catch (Exception)
            {
                return json;
            }
        }

        /// <summary>
        ///  GET ROOT NAME OFF XML
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static string GetRootName(this string xmlString)
        {
            var xdoc = XDocument.Parse(xmlString);
            return xdoc.Root?.Name.ToString();
        }

        /// <summary>
        ///  MAP XML DYNAMIC
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T MapXmlElement<T>(this string xmlString) where T : class
        {
            try
            {
                var rootName = xmlString.GetRootName();

                var serializerCreate = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName ?? "response"));

                T mapXmlElement = null;
                using var reader = new StringReader(xmlString);
                mapXmlElement = (T)serializerCreate.Deserialize(reader);

                return mapXmlElement;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao converter o retorno.", e);
            }
        }

        /// <summary>
        ///  CREATED LOG FILE
        /// </summary>
        /// <param name="data"></param>
        /// <param name="root"></param>
        /// <param name="ex"></param>
        public static void LogFile(object data, string root, Exception ex = null)
        {
            Task.Run(() =>
            {
                try
                {
                    var log = new StringBuilder("");
                    var date = DateTime.Now;
                    log.AppendLine($"{date:dd/MM/yyyy HH:mm:ss}");

                    var jsonDataData = JsonConvert.SerializeObject(data, Formatting.Indented);

                    log.AppendLine($"Data : {jsonDataData}").AppendLine();

                    if (ex != null)
                        log.AppendLine($"Exception : {ex.InnerException} {ex.Message}".Trim());

                    if (!Directory.Exists(root))
                        Directory.CreateDirectory(root);

                    File.WriteAllText(root, log.ToString());
                }
                catch (Exception)
                {
                    //ignored
                }
            });
        }

        /// <summary>
        ///  SET PATH IN IMAGE FOR PROFILE
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="facebookId"></param>
        /// <param name="googleId"></param>
        /// <param name="instagramId"></param>
        /// <param name="twitterId"></param>
        /// <param name="customBaseUrl"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string SetPhotoProfile(this string photo, string facebookId = null, string googleId = null,
            string instagramId = null, string twitterId = null, string customBaseUrl = null, int width = 600)
        {
            var baseUrl = string.IsNullOrEmpty(customBaseUrl) ? BaseConfig.BaseUrl : customBaseUrl;

            var regex = new Regex(@"^(http|https):");
            if (string.IsNullOrEmpty(photo) == false && regex.IsMatch(photo))
                return photo;
            if (string.IsNullOrEmpty(photo) && !string.IsNullOrEmpty(facebookId))
                return facebookId.GetPhotoFacebookGraph(width);
            if (string.IsNullOrEmpty(photo) && !string.IsNullOrEmpty(googleId))
                return googleId.GetPhotoGooglePlus(width);
            if (string.IsNullOrEmpty(photo) && !string.IsNullOrEmpty(instagramId))
                return $"https://avatars.io/instagram/{instagramId}/{width}";
            if (string.IsNullOrEmpty(photo) && !string.IsNullOrEmpty(twitterId))
                return $"https://avatars.io/twitter/{twitterId}/{width}";
            if (string.IsNullOrEmpty(photo))
                return BaseConfig.DefaultUrl.SetCurrentProtocol();
            return $"{baseUrl}{photo.Replace(baseUrl, "")}".SetCurrentProtocol();
        }

        /// <summary>
        ///  REMOVE PATH IMAGE
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string RemovePathImage(this string image)
        {
            return image?.Split('/')?.LastOrDefault();
        }

        /// <summary>
        ///  SET PATH IMAGE
        /// </summary>
        /// <param name="image"></param>
        /// <param name="customBaseUrl"></param>
        /// <returns></returns>
        public static string SetPathImage(this string image, string customBaseUrl = null)
        {
            if (string.IsNullOrEmpty(image))
                return BaseConfig.DefaultUrl.SetCurrentProtocol();

            var regex = new Regex(@"^(http|https):");
            if (regex.IsMatch(image))
                return image;

            var baseUrl = (string.IsNullOrEmpty(customBaseUrl) ? BaseConfig.BaseUrl : customBaseUrl);

            return (string.IsNullOrEmpty(image) ? BaseConfig.DefaultUrl : $"{baseUrl}{image.Replace(baseUrl, "")}").SetCurrentProtocol();
        }

        /// <summary>
        ///  SHOW TIME IN FORMAT TIME AGO
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableString(this TimeSpan span)
        {
            var formatted =
                $"{(span.Duration().Days > 0 ? $"{span.Days:0} dia{(span.Days == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Hours > 0 ? $"{span.Hours:0} hora{(span.Hours == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Minutes > 0 ? $"{span.Minutes:0} minuto{(span.Minutes == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Seconds > 0 ? $"{span.Seconds:0} segundo{(span.Seconds == 1 ? string.Empty : "s")}" : string.Empty)}";

            if (formatted.EndsWith(", "))
                formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted))
                formatted = "0 segundos";

            return formatted;
        }

        /// <summary>
        ///  CONVERTER STRING EM ENUM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string str)
        {
            var enumType = typeof(T);
            string[] array = Enum.GetNames(enumType);
            for (int i = 0; i < array.Length; i++)
            {
                string name = array[i];
                var enumMemberAttribute =
                    ((EnumMemberAttribute[])enumType.GetField(name)
                        .GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == str)
                    return (T)Enum.Parse(enumType, name);
            }

            //throw exception or whatever handling you want or
            return default;
        }

        /// <summary>
        ///  OBTER VALOR DE UMA COLUNA DO EXCEL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col"></param>
        /// <param name="tnew"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool SetValueCustom<T>(this ExportInfo col, T tnew, ExcelRange val)
        {
            if (val.Value == null)
            {
                col.Property.SetValue(tnew, null);
                return true;
            }


            if (col.Property.PropertyType == typeof(bool))
            {
                col.Property.SetValue(tnew, val.GetValue<bool>());
                return true;
            }

            if (col.Property.PropertyType == typeof(int))
            {
                col.Property.SetValue(tnew, val.GetValue<int>());
                return true;
            }

            if (col.Property.PropertyType == typeof(long))
            {
                col.Property.SetValue(tnew, val.GetValue<long>());
                return true;
            }

            if (col.Property.PropertyType == typeof(double))
            {
                col.Property.SetValue(tnew, val.GetValue<double>());
                return true;
            }

            if (col.Property.PropertyType == typeof(decimal))
            {
                col.Property.SetValue(tnew, val.GetValue<decimal>());
                return true;
            }

            if (col.Property.PropertyType == typeof(DateTime))
            {
                col.Property.SetValue(tnew, val.GetValue<DateTime>());
                return true;
            }

            if (col.Property.PropertyType == typeof(TimeSpan))
            {
                col.Property.SetValue(tnew, val.GetValue<TimeSpan>());
                return true;
            }

            if (col.Property.PropertyType == typeof(string))
            {
                col.Property.SetValue(tnew, val.GetValue<string>());
                return true;
            }

            if (col != null && col.Property.PropertyType.GetTypeInfo().IsEnum)
            {
                col.Property.SetValue(tnew, GetEnumFromDescription(val.GetValue<string>(), col.Property.PropertyType));
                return true;
            }

            return false;
        }


        /// <summary>
        ///  METODO PARA ADICIONAR (...)EM TEXTO LONGO
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxChars"></param>
        /// <returns></returns>
        public static string Truncate(this string text, int maxChars)
        {
            return string.IsNullOrEmpty(text) || text.Length <= maxChars || maxChars == 0
                ? text
                : text.Substring(0, maxChars - 3) + "...";
        }


        /// <summary>
        ///  OBTER SEPARADOR CSV OU TXT
        /// </summary>
        /// <param name="csvFilePath"></param>
        /// <returns></returns>
        public static char DetectSeparator(string csvFilePath)
        {
            var lines = File.ReadAllLines(csvFilePath);
            return DetectSeparator(lines);
        }

        /// <summary>
        ///  OBTER SEPARADOR CSV OU TXT
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static char DetectSeparator(string[] lines)
        {
            var q = SeparatorChars.Select(sep => new
            { Separator = sep, Found = lines.GroupBy(line => line.Count(ch => ch == sep)) })
                .OrderByDescending(res => res.Found.Count(grp => grp.Key > 0))
                .ThenBy(res => res.Found.Count())
                .First();

            return q.Separator;
        }
        /// <summary>
        /// BUSCAR ENDEREÇO PELO CEP
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public static async Task<BaseAddressViewModel> GetInfoZipCode(string zipCode)
        {
            try
            {
                var zipCodeFormat = zipCode?.OnlyNumbers().PadLeft(8, '0');

                if (string.IsNullOrEmpty(zipCodeFormat))
                    throw new Exception("Informe um CEP");

                var client = new RestClient("https://api.mecabr.com");
                var request = new RestRequest($"/api/v1/City/GetInfoFromZipCode/{zipCodeFormat}", Method.GET);

                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");

                var response = await client.ExecuteAsync<ReturnViewModel>(request);

                if (response.StatusCode != HttpStatusCode.BadRequest && response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Ocorreu um erro ao informações do CEP {zipCode}");

                if (response.Data == null || response.Data.Erro)
                    throw new Exception(response.Data?.Message ?? $"CEP {zipCode} não encontrado");

                return JsonConvert.DeserializeObject<BaseAddressViewModel>(JsonConvert.SerializeObject(response.Data.Data));

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// FORMATAR ENDEREÇO  (RUA,NUMERO - BAIRRO, CIDADE - UF, CEP)
        /// </summary>
        /// <param name="streetAddress"></param>
        /// <param name="number"></param>
        /// <param name="complement"></param>
        /// <param name="district"></param>
        /// <param name="cityName"></param>
        /// <param name="stateUf"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public static string FormatAddress(string streetAddress, string number, string complement = null, string district = null, string cityName = null, string stateUf = null, string zipCode = null)
        {
            string formatedAddress = string.Empty;

            try
            {

                if (string.IsNullOrEmpty(streetAddress) == false)
                    formatedAddress += streetAddress;

                if (string.IsNullOrEmpty(number) == false)
                    formatedAddress += $", {number}";

                if (string.IsNullOrEmpty(complement) == false)
                    formatedAddress += $" {complement}";

                if (string.IsNullOrEmpty(district) == false)
                    formatedAddress += $" - {district}";

                if (string.IsNullOrEmpty(cityName) == false)
                    formatedAddress += $", {cityName}";

                if (string.IsNullOrEmpty(stateUf) == false)
                    formatedAddress += $" - {stateUf}";

                if (string.IsNullOrEmpty(zipCode) == false)
                    formatedAddress += $", {zipCode}";
            }
            catch (Exception) { }

            return formatedAddress;
        }

        /// <summary>
        ///  OBTER CAMPO COM ERRO NA LEITURA DO CSV OU TXT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string GetFieldError<T>(int column, int line)
        {
            try
            {
                var propertyInfo = typeof(T).GetProperties()
                    .FirstOrDefault(x => x.GetCustomAttribute<Column>()?.ColumnIndex == column);

                if (propertyInfo == null)
                    return $"O valor da coluna {column} na linha {line} está inválido";

                return
                    $"O valor do campo \"{propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name ?? propertyInfo.Name}\" está inválido";
            }
            catch (Exception ex)
            {
                return $"{ex.InnerException} {ex.Message}".TrimEnd();
            }
        }


        /// <summary>
        ///  METODO PARA EXPORTAR PARA CSV
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itens"></param>
        /// <param name="delimiter"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static string ExportToCsv<T>(this IList<T> itens, string delimiter = ";", CultureInfo cultureInfo = null)
            where T : class
        {
            var type = typeof(T);

            var proprerties = type.GetProperties();

            var columnHeaders = proprerties.Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null).Select(x =>
                new
                {
                    Order = x.GetCustomAttribute<JsonPropertyAttribute>()?.Order ?? 0,
                    Prop = x.GetCustomAttribute<DisplayAttribute>(false)?.Name ?? x.Name
                }).OrderBy(x => x.Order).ToList();

            var stBuilder = new StringBuilder();

            stBuilder.AppendLine(string.Join(delimiter, columnHeaders.Select(x => x.Prop)));

            for (var i = 0; i < itens.Count; i++)
            {
                var line = new List<object>();
                var columnValues = itens[i].GetType().GetProperties()
                    .Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null).Select(x =>
                        new
                        {
                            Order = x.GetCustomAttribute<JsonPropertyAttribute>()?.Order ?? 0,
                            Value = x.GetValue(itens[i], null),
                            Type = x.PropertyType
                        }).OrderBy(x => x.Order).ToList();

                var valueLine = string.Empty;
                for (int j = 0; j < columnValues.Count; j++)
                {
                    var val = columnValues[j];
                    if (val.Value != null)
                    {
                        var _val = "";
                        if (cultureInfo != null)
                        {
                            if (val.Type == typeof(double))
                                _val = double.Parse(val.Value.ToString(), cultureInfo.NumberFormat)
                                    .ToString(cultureInfo);
                            else if (val.Type == typeof(decimal))
                                _val = decimal.Parse(val.Value.ToString(), cultureInfo.NumberFormat)
                                    .ToString(cultureInfo);
                            else
                                _val = val.Value.ToString();
                        }
                        else
                        {
                            _val = val.Value.ToString();
                        }

                        //Escape quotes
                        _val = _val.Replace("\"", "\"\"");

                        //Check if the value contains a delimiter and place it in quotes if so
                        if (_val.Contains(delimiter))
                            _val = string.Concat("\"", _val, "\"");

                        //Replace any \r or \n special characters from a new line with a space
                        if (_val.Contains("\r"))
                            _val = _val.Replace("\r", " ");
                        if (_val.Contains("\n"))
                            _val = _val.Replace("\n", " ");

                        valueLine = string.Concat(valueLine, _val, delimiter);
                    }
                    else
                    {
                        valueLine = string.Concat(valueLine, string.Empty, delimiter);
                    }
                }

                stBuilder.AppendLine(valueLine.Remove(valueLine.Length - delimiter.Length));
            }

            return stBuilder.ToString();
        }


        /// <summary>
        ///  MAPEAR BOLEANO EM STRING - SIM / NÃO  PARA EXCEL
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultResponse"></param>
        /// <returns></returns>
        public static string MapBoolean(this bool? value, string defaultResponse = "Não informado")
        {
            if (value == null)
                return defaultResponse;

            return value == false ? "NÃO" : "SIM";
        }
        /// <summary>
        ///  MAPEAR BOLEANO EM STRING - SIM / NÃO  PARA EXCEL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MapBoolean(this bool value)
            => value == false ? "NÃO" : "SIM";


        /// <summary>
        ///  FORMATAR VALORES EM REAIS PARA EXCEL
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <param name="defaultResponse"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static string MapMoney(this double? value, string format = "C", string defaultResponse = "Não informado", string cultureInfo = "pt-BR")
        {
            if (value == null)
                return defaultResponse;

            return value.GetValueOrDefault().ToString(format, new CultureInfo(cultureInfo));
        }

        /// <summary>
        ///  FORMATAR VALORES EM REAIS PARA EXCEL
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <param name="defaultResponse"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static string MapMoney(this double value, string format = "C", string defaultResponse = "Não informado", string cultureInfo = "pt-BR")
            => value.ToString(format, new CultureInfo(cultureInfo));


        /// <summary>
        ///  OBTER IP REMOTO
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            return HttpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }


        /// <summary>
        ///  Formatar data como string DD/MM/YYYY HH:MM
        /// </summary>
        /// <param name="unixTime"></param>
        /// <param name="format"></param>
        /// <param name="defaultResponse"></param>
        /// <returns></returns>
        public static string MapUnixTime(this long? unixTime, string format = "dd/MM/yyyy HH:mm",
            string defaultResponse = "Não informado")
        {
            if (unixTime == null || unixTime == 0)
                return defaultResponse;

            return unixTime.GetValueOrDefault().TimeStampToDateTime().ToString(format);
        }

        /// <summary>
        ///  Formatar data como string DD/MM/YYYY HH:MM
        /// </summary>
        /// <param name="unixTime"></param>
        /// <param name="format"></param>
        /// <param name="defaultResponse"></param>
        /// <returns></returns>
        public static string MapUnixTime(this long unixTime, string format = "dd/MM/yyyy HH:mm",
            string defaultResponse = "Não informado")
        {
            if (unixTime == 0)
                return defaultResponse;

            return unixTime.TimeStampToDateTime().ToString(format);
        }

        /// <summary>
        ///  Formatar data como string DD/MM/YYYY HH:MM
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <param name="defaultResponse"></param>
        /// <returns></returns>
        public static string MapDateTime(this DateTime? date, string format = "dd/MM/yyyy HH:mm",
            string defaultResponse = "Não informado")
        {
            if (date == null || date == default)
                return defaultResponse;

            return date.GetValueOrDefault().ToString(format);
        }

        /// <summary>
        ///  Formatar data como string DD/MM/YYYY HH:MM
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <param name="defaultResponse"></param>
        /// <returns></returns>
        public static string MapDateTime(this DateTime date, string format = "dd/MM/yyyy HH:mm",
            string defaultResponse = "Não informado")
        {
            if (date == default)
                return defaultResponse;

            return date.ToString(format);
        }

        /// <summary>
        ///  METODO PARA LER XLSX E CONVERTER PARA LISTA DE CLASS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        public static IList<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet) where T : new()
        {
            try
            {

                var columns = typeof(T)
                            .GetProperties()
                            .Select((p, i) => new ExportInfo
                            {
                                Property = p,
                                Column = p.GetCustomAttributes<Column>().FirstOrDefault()?.ColumnIndex ?? (i + 1) //safe because if where above
                            })
                            .ToList();

                var totalRows = worksheet.Cells
                    .Select(cell => cell.Start.Row)
                    .Distinct()
                    .Count();

                var totalColumns = columns.Count();

                //Create the collection container
                var collection = new List<T>();

                for (var i = 2; i <= totalRows; i++)
                {
                    var tnew = new T();
                    var hasEntityValue = false;
                    for (var c = 0; c < totalColumns; c++)
                    {
                        var col = columns[c];
                        var val = worksheet.Cells[i, col.Column];

                        try
                        {
                            if (string.IsNullOrEmpty(val.Value?.ToString()))
                                continue;

                            var res = col.SetValueCustom(tnew, val);
                            hasEntityValue = true;
                        }
                        catch (Exception)
                        {
                            var celula = val?.Address ?? $"linha: {i}, coluna: {c}";

                            throw new Exception($"O valor da célula {celula} é inválido.");
                        }
                    }

                    if (hasEntityValue)
                        collection.Add(tnew);
                }

                return collection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  METODO PARA OBTER VALOR DE UM HEADER
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHeaderValue(this IHeaderDictionary headers, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a key");
                headers.TryGetValue(key, out var headerValue);

                return headerValue.ToString();
            }
            catch (Exception)
            {
                //unused
            }

            return null;
        }

        /// <summary>
        ///  METODO PARA REMOVER ACENTOS
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveAccents(this string text)
        {
            var sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            for (int i = 0; i < arrayText.Length; i++)
            {
                char letter = arrayText[i];
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }

            return sbReturn.ToString();
        }

        /// <summary>
        ///  METODO PARA OBTER VALOR INT DE UM ENUM PELO DESCRIPTION DELE
        /// </summary>
        /// <param name="enumVal">VALOR DO ENUM</param>
        /// <param name="enumType">TIPO DO ENUM</param>
        /// <returns></returns>
        public static object GetEnumFromDescription(string enumVal, Type enumType)
        {
            var memInfo = enumType.GetMembers().ToList();
            var attr = memInfo.Find(x => x.GetCustomAttribute<EnumMemberAttribute>(false)?.Value == enumVal);
            return ((FieldInfo)attr)?.GetValue(enumType);
        }

        /// <summary>
        ///  METODO PARA OBTER ENUM PELO MEMBER
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }

        /// <summary>
        /// METODO PARA OBTER O ATRIBUTO MEMBERVALUE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeEnum"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetEnumMemberValue<T>(this Type typeEnum, T value)
        {
            if (typeEnum.GetTypeInfo().IsEnum == false)
                throw new Exception("Informe typo Enum");

            return typeEnum
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }

        /// <summary>
        /// METODO PARA OBTER O ATRIBUTO MEMBERVALUE
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetEnumMemberValueWithDefault<T>(this T value, string defaultValue = "")
        {

            if (Equals(value, null))
                return defaultValue;

            var type = typeof(T);

            if ((type.GetTypeInfo().IsEnum || Nullable.GetUnderlyingType(type)?.GetTypeInfo().IsEnum == true) == false)
                throw new Exception(string.Format("Informe typo Enum, {0}", type.Name));

            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }


        /// <summary>
        ///  EXPORT LIST ENTITY TO TABLE IN EXCEL FILE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <param name="path"></param>
        /// <param name="workSheetName">NOME DA WORKSHEET</param>
        /// <param name="fileName"></param>
        /// <param name="hexBColor"></param>
        /// <param name="hexTxtColor"></param>
        /// <param name="autoFit">AUTO RESIZE DA COLUNA</param>
        /// <param name="ext"></param>
        /// <param name="addWorksheet">NOVA WORKSHEET EM ARQUIVO EXISTENTE</param>
        /// <param name="forceText">FORÇAR FORMATAÇÃO TEXTO EM TODOS CAMPOS</param>
        public static void ExportToExcel<T>(List<T> entitys, string path, string workSheetName = "Result",
            string fileName = "Export", string hexBColor = null, string hexTxtColor = null, bool autoFit = true,
            string ext = ".xlsx", bool addWorksheet = false, bool forceText = false, bool autoFilter = false)
        {
            var bColor = GetColorFromHex(Color.FromArgb(68, 114, 196), hexBColor);
            var txtColor = GetColorFromHex(Color.White, hexTxtColor);

            var sFileName = $"{fileName.Split('.')[0]}{ext}";

            #region FilePrepare

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var file = new FileInfo(Path.Combine(path, sFileName));
            if (file.Exists && addWorksheet == false)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(path, sFileName));
            }

            #endregion

            using var package = new ExcelPackage(file);
            var worksheet = package.Workbook.Worksheets.Add(workSheetName);

            //HEADER TABLE
            var t = typeof(T);
            var headings = t.GetProperties();
            for (var i = 0; i < headings.Count(); i++)
            {
                var column = i + 1;
                var name = headings[i].GetCustomAttribute<DisplayAttribute>();
                worksheet.Cells[1, column].Value = name?.Name ?? headings[i].Name;

                var dropDownExcel = headings[i].GetCustomAttribute<DropDownExcel>();


                if (forceText)
                    worksheet.Cells[ExcelCellBase.GetAddress(2, column, ExcelPackage.MaxRows, column)].Style.Numberformat.Format = "@";


                if (dropDownExcel != null)
                {
                    var enumValues = Enum.GetValues(dropDownExcel.Options);

                    var options = new List<string>();

                    IList list = enumValues;
                    for (int i1 = 0; i1 < list.Count; i1++)
                    {
                        int value = (int)list[i1];
                        options.Add(dropDownExcel.Options.GetEnumMemberValue(Enum.GetName(dropDownExcel.Options, value)));
                    }

                    var range = ExcelCellBase.GetAddress(2, column, ExcelPackage.MaxRows, column);

                    var validation = worksheet.DataValidations.AddListValidation(range);

                    validation.ShowErrorMessage = true;
                    validation.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                    validation.ErrorTitle = "Valor inválido";
                    validation.Error = "Selecione um item da lista";

                    for (int item = 0; item < options.Count(); item++)
                        validation.Formula.Values.Add(options[item]);

                    validation.AllowBlank = dropDownExcel.AllowBlank;
                    validation.Validate();
                }
            }

            var address = worksheet.Cells[1, headings.Count()]?.Address;

            //BODY DA TABLE
            if (entitys.Any())
                worksheet.Cells["A2"].LoadFromCollection(entitys);

            using (var rng = worksheet.Cells[$"A1:{address ?? "AD1"}"])
            {
                rng.Style.Font.Bold = true;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(bColor);
                rng.Style.Font.Color.SetColor(txtColor);
            }

            if (autoFit)
                worksheet.Cells.AutoFitColumns();

            if (autoFilter)
                worksheet.Cells.AutoFilter = autoFilter;

            package.Save(); //Save the workbook.
        }

        /// <summary>
        ///  GET COLOR ENTITY FOR STRING HEX OF COLOR
        /// </summary>
        /// <param name="defaultColor"></param>
        /// <param name="hexColor"></param>
        /// <returns></returns>
        public static Color GetColorFromHex(Color? defaultColor, string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor))
                return defaultColor ?? default;

            if (hexColor.Contains("#") == false)
                hexColor = $"#{hexColor}";

            try
            {
                return ColorTranslator.FromHtml(hexColor);
            }
            catch (Exception)
            {
                return defaultColor ?? default;
            }
        }

        /// <summary>
        ///  DISTINCTY BY PROPERTY
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
                if (seenKeys.Add(keySelector(element)))
                    yield return element;
        }

        /// <summary>
        ///  ADD PARAMETER QUERY STRING IN URL WITH CLASS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="entityParameters"></param>
        /// <returns></returns>
        public static string AddQueryStringParameters<T>(this string url, T entityParameters)
        {
            var parameters = "";
            try
            {
                if (Equals(entityParameters, null))
                    return url;

                if (string.IsNullOrEmpty(url))
                    throw new Exception("Informe o campo URL para incluir os parametros na url");

                var listProperties = typeof(T).GetProperties();
                for (int i = 0; i < listProperties.Length; i++)
                {
                    PropertyInfo item = listProperties[i];
                    var value = item.GetValue(entityParameters, null);
                    if (Equals(value, null) == false)
                    {
                        var propName = item.GetAttribute<JsonPropertyAttribute>()?.PropertyName ?? item.Name?.LowercaseFirst();

                        if (string.IsNullOrEmpty(propName) == false)
                        {
                            parameters += $"{propName}={value}&";
                        }
                    }
                }
                url += $"?{parameters}".TrimEnd('&', '?').Trim();
            }
            catch (Exception)
            {
                throw;
            }
            return url;
        }



        /// <summary>
        ///  METODO PARA ATUALIZAR ARQUIVO JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="locationAndFileName"></param>
        public static void AddOrUpdatePropertyInFileJson<T>(string key, T value,
            string locationAndFileName = "appSettings.json")
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, locationAndFileName);
            var json = File.ReadAllText(filePath);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            var sectionPath = key.Split(':')[0];
            if (!string.IsNullOrEmpty(sectionPath))
            {
                var keyPath = key.Split(':')[1];
                jsonObj[sectionPath][keyPath] = value;
            }
            else
            {
                jsonObj[sectionPath] = value; // if no sectionpath just set the value
            }

            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(filePath, output);
        }

        /// <summary>
        ///  LER JSON E MAPEAR EM UMA CLASS
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadJson<T>(this string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        /// <summary>
        ///  IF STRING CONTAINS CASEINSENSITIVE
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string txt, string search)
            => string.IsNullOrEmpty(txt) == false && txt.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

        /// <summary>
        ///  COMPARAR STRING COM IGNORE CASE
        /// </summary>
        /// <param name="first">primeiro texto</param>
        /// <param name="second">segundo texto</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string first, string second)
            => string.Equals(first, second, StringComparison.OrdinalIgnoreCase);


        /// <summary>
        /// METODO PARA PRINTAR CAMPOS ENVIADO VIA FORM DATA
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static string PrintFormCollection(this IFormCollection form)
        {
            var fields = string.Join(Environment.NewLine, form.SelectMany(kv => kv.Value.Select((v, i) => $"{kv.Key}:{v}")));
            var files = string.Join(Environment.NewLine, form.Files.Select((formFile, i) => $"{formFile.Name}:{formFile.FileName}").ToList());
            return $"{Environment.NewLine}{fields}{Environment.NewLine}{files}";
        }

        /// <summary>
        ///  IFORMCOLLECTION TO JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JObject ToJson(this IFormCollection obj)
        {
            dynamic json = new JObject();

            try
            {
                if (obj.Files.Count() > 0)
                    for (var i = 0; i < obj.Files.Count(); i++)
                    {
                        var file = obj.Files[i];
                        var countSameName = obj.Files.Count(x => x.Name == file.Name);

                        json.Add(countSameName <= 1 ? file.Name : $"{file.Name}[{i}]", file.FileName);
                    }

                if (obj.Keys.Count() > 0)
                    foreach (var key in obj.Keys)
                        //check if the value is an array
                        if (obj[key].Count > 1)
                        {
                            var array = new JArray();
                            for (var i = 0; i < obj[key].Count; i++)
                                array.Add(obj[key][i]);
                            json.Add(key, array);
                        }
                        else
                        {
                            var value = obj[key][0];
                            json.Add(key, value);
                        }
            }
            catch (Exception)
            {
                /*unused*/
            }

            return json;
        }

        /// <summary>
        ///  DD/MM/YYYY TO UNIX DATE
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long? ToUnix(this string date)
        {
            long? response = null;
            try
            {
                if (string.IsNullOrEmpty(date) == false)
                {
                    date = date.Split(' ').FirstOrDefault();

                    var datetime = $"{date} 00:00:00".TryParseAnyDate();
                    response = new DateTimeOffset(datetime).ToUnixTimeSeconds();
                }
            }
            catch (Exception)
            {
                return response;
            }

            return response;
        }

        /// <summary>
        ///  DD/MM/YYYY TO UNIX DATE
        /// </summary>
        /// <param name="date"></param>
        /// <param name="toEndDay"></param>
        /// <returns></returns>
        public static long? ToUnix(this string date, bool toEndDay = false)
        {
            long? response = null;
            try
            {
                if (string.IsNullOrEmpty(date) == false)
                {
                    date = date.Split(' ').FirstOrDefault();

                    var datetime = $"{date} {(toEndDay ? "23:59:59" : "00:00:00")}".TryParseAnyDate();
                    response = new DateTimeOffset(datetime).ToUnixTimeSeconds();
                }
            }
            catch (Exception)
            {
                return response;
            }

            return response;
        }


        /// <summary>
        /// BUSCA O PRÓXIMA OU ÚLTIMA OCORRENCIA DO DIA DA SEMANA ESPECIFICADO
        /// </summary>
        /// <param name="today">data e hora atual</param>
        /// <param name="dayOfWeek">dia da semana</param>
        /// <param name="before">próxima ou última = default próximo</param>
        /// <returns></returns>
        public static DateTime NextOrLastDayOfWeek(DateTime today, DayOfWeek dayOfWeek, bool before = false)
        {

            if (before == false)
            {
                /*NEXT*/
                if (today.DayOfWeek == dayOfWeek)
                    return today;

                while (today.DayOfWeek != dayOfWeek)
                    today = today.AddDays(1);

            }
            else
            {
                /*LAST*/
                if (today.DayOfWeek == dayOfWeek)
                    return today;

                while (today.DayOfWeek != dayOfWeek)
                    today = today.AddDays(-1);

            }
            return today;
        }

        /// <summary>
        ///  METODO PARA CRIPTOGRAFAR UM TEXTO
        /// </summary>
        /// <param name="key"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptString(string key, string plainText)
        {
            var iv = new byte[16];
            byte[] array;

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        /// <summary>
        ///  METODO PARA DESCRIPTOGRAFAR UM TEXTO
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptString(string key, string cipherText)
        {
            var iv = new byte[16];
            var buffer = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// CHAMADA DE API EXTERNA
        /// </summary>
        /// <param name="model">DADOS</param>
        /// <returns></returns>
        public async static Task<ReturnApiViewModel> CallApi(CallApiViewModel model, ILogger logger = null, bool jsonPretty = false, JsonSerializerSettings jsonSettings = null, int? timeout = null)
        {
            try
            {
                string jsonBody = null;
                var restClient = new RestClient(model.Url);

                if (timeout != null)
                    restClient.Timeout = timeout.GetValueOrDefault();

                var restRequest = new RestRequest(model.Method)
                {
                    AlwaysMultipartFormData = model.AlwaysMultipartFormData,
                };

                var contentType = model.Headers?.FirstOrDefault(x => x.Key.EqualsIgnoreCase("content-type")).Value;

                if (string.IsNullOrEmpty(contentType))
                {
                    contentType = "application/json";
                    restRequest.AddHeader("Content-Type", contentType);
                }

                if (model.Headers?.Count > 0)
                {
                    foreach (var item in model.Headers)
                        restRequest.AddHeader(item.Key, item.Value);
                }

                if (model.Body != null)
                {
                    switch (contentType)
                    {
                        case "multipart/form-data":
                        case "application/x-www-form-urlencoded":
                            var paramsBody = model.Body.ToDictionary<string>();
                            foreach (var item in paramsBody)
                                restRequest.AddParameter(item.Key, item.Value);

                            model.PathFiles.ForEach(imagePath => restRequest.AddFile("file", imagePath));

                            break;
                        default:
                            jsonBody = JsonConvert.SerializeObject(model.Body, jsonSettings ?? new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = jsonPretty ? Formatting.Indented : Formatting.None });
                            restRequest.AddParameter(contentType, jsonBody, ParameterType.RequestBody);
                            break;
                    }
                }

                var restResponse = await restClient.ExecuteAsync(restRequest).ConfigureAwait(false);

                if (restResponse != null && logger != null)
                {
                    restResponse.LogRequest(logger, model.Method, model.Url, restResponse.Content?.UnprettyJson(), jsonBody?.UnprettyJson());
                }

                return new ReturnApiViewModel()
                {
                    Content = restResponse?.Content,
                    StatusCode = restResponse != null ? restResponse.StatusCode : HttpStatusCode.BadRequest
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// CHAMADA DE API EXTERNA
        /// </summary>
        /// <param name="model">DADOS</param>
        /// <returns></returns>
        public async static Task<ReturnGenericApiViewModel<T>> CallApi<T>(CallApiViewModel model, ILogger logger = null, bool jsonPretty = false, JsonSerializerSettings jsonSettings = null, int? timeout = null)
        {
            try
            {
                string jsonBody = null;
                var restClient = new RestClient(model.Url);

                if (timeout != null)
                    restClient.Timeout = timeout.GetValueOrDefault();


                var restRequest = new RestRequest(model.Method)
                {
                    AlwaysMultipartFormData = model.AlwaysMultipartFormData
                };

                var contentType = model.Headers?.FirstOrDefault(x => x.Key.EqualsIgnoreCase("content-type")).Value;

                if (string.IsNullOrEmpty(contentType))
                {
                    contentType = "application/json";
                    restRequest.AddHeader("Content-Type", contentType);
                }

                if (model.Headers?.Count > 0)
                {
                    foreach (var item in model.Headers)
                        restRequest.AddHeader(item.Key, item.Value);
                }

                if (model.Body != null)
                {
                    switch (contentType)
                    {
                        case "multipart/form-data":
                        case "application/x-www-form-urlencoded":
                            var paramsBody = model.Body.ToDictionary<string>();
                            foreach (var item in paramsBody)
                                restRequest.AddParameter(item.Key, item.Value);

                            model.PathFiles.ForEach(imagePath => restRequest.AddFile("file", imagePath));

                            break;
                        default:
                            jsonBody = JsonConvert.SerializeObject(model.Body, jsonSettings ?? new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = jsonPretty ? Formatting.Indented : Formatting.None });
                            restRequest.AddParameter(contentType, jsonBody, ParameterType.RequestBody);
                            break;
                    }
                }

                var restResponse = await restClient.ExecuteAsync(restRequest);

                if (restResponse != null && logger != null)
                {
                    restResponse.LogRequest(logger, model.Method, model.Url, restResponse.Content.UnprettyJson(), jsonBody.UnprettyJson());
                }

                var result = new ReturnGenericApiViewModel<T>()
                {
                    Content = restResponse?.Content,
                    StatusCode = restResponse != null ? restResponse.StatusCode : HttpStatusCode.BadRequest,
                };

                try
                {
                    result.Data = JsonConvert.DeserializeObject<T>(restResponse.Content);
                }
                catch (Exception) {/*UNUSED*/}

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// METODO PARA FORMATAR JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PrettyJson(this string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                    return json;

                var jsonObj = JsonConvert.DeserializeObject(json);

                return JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

            }
            catch (Exception)
            {
                return json;
            }
        }
        /// <summary>
        /// METODO PARA REMOVER FORMATAÇÃO DE STRING JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string UnprettyJson(this string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                    return json;

                var jsonObj = JsonConvert.DeserializeObject(json);

                return JsonConvert.SerializeObject(jsonObj, Formatting.None);

            }
            catch (Exception)
            {
                return json;
            }
        }

        /// <summary>
        /// VERIFICA SE É UM TIPO PRIMITIVO
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsPrimitive(Type t)
        {
            return new[] {
                typeof(string),
                typeof(char),
                typeof(byte),
                typeof(sbyte),
                typeof(ushort),
                typeof(short),
                typeof(uint),
                typeof(int),
                typeof(ulong),
                typeof(long),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(DateTime),
            }.Contains(t);
        }

        public static void LogRequest(this RestResponse restResponse, ILogger _logger, Method method, string uri, string response, string body = null, bool delimiterStartAndEnd = false)
        {
            try
            {

                var message = new StringBuilder();
                message.AppendLine();

                if (delimiterStartAndEnd)
                    message.AppendLine("########################## START #############################");
                message.AppendLine($"INTERNAL REQUEST");
                message.AppendLine($"{method} {uri}");

                if (restResponse != null)
                {
                    var listHeaders = restResponse.Request.Parameters.Where(x => x.Type == ParameterType.HttpHeader).Select(x => $"\" {x.Name}\":\"{x.Value}\"").ToList();

                    var headers = string.Join(",", listHeaders).Trim().TrimEnd(',');
                    message.AppendLine("Headers: {" + headers + "}");
                }


                if (string.IsNullOrEmpty(body) == false)
                    message.AppendLine($"BODY: {body}");

                message.AppendLine($"RESPONSE: {response}");

                if (delimiterStartAndEnd)
                    message.AppendLine("########################### END ##############################");

                _logger.LogWarning(message.ToString());
            }
            catch (Exception)
            {
                /*UNUSED*/
            }
        }
        public static void LogRequest(this IRestResponse restResponse, ILogger _logger, Method method, string uri, string response, string body = null, bool delimiterStartAndEnd = false)
        {
            try
            {
                var message = new StringBuilder();

                message.AppendLine();

                if (delimiterStartAndEnd)
                    message.AppendLine("########################## START #############################");

                message.AppendLine($"INTERNAL REQUEST");
                message.AppendLine($"{method} {uri}");

                if (restResponse != null)
                {
                    var listHeaders = restResponse.Request.Parameters.Where(x => x.Type == ParameterType.HttpHeader).Select(x => $"\" {x.Name}\":\"{x.Value}\"").ToList();

                    var headers = string.Join(",", listHeaders).Trim().TrimEnd(',');
                    message.AppendLine("Headers: {" + headers + "}");
                }

                if (string.IsNullOrEmpty(body) == false)
                    message.AppendLine($"BODY: {body}");

                message.AppendLine($"RESPONSE: {response}");

                if (delimiterStartAndEnd)
                    message.AppendLine("########################### END ##############################");

                _logger.LogWarning(message.ToString());
            }
            catch (Exception)
            {
                /*UNUSED*/
            }
        }

        /// <summary>
        /// METODO PARA GERAR PDF APARTIR DE UM HML
        /// </summary>
        /// <param name="html"></param>
        /// <param name="outputPath"></param>
        public static string SavePdfToFile(this IConverter converter, string html, string outputPath, string fileName = null, HtmlToPdfDocument htmlToPdf = null)
        {
            var pdfBytes = converter.Convert(htmlToPdf ?? new HtmlToPdfDocument
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            });

            fileName = GetUniqueFileName(fileName ?? Path.GetFileNameWithoutExtension(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()), outputPath, ".pdf").ToLower();

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            var fullPath = Path.Combine(outputPath, $"{fileName}.pdf");

            File.WriteAllBytes(fullPath, pdfBytes);

            return fullPath;
        }

        /// <summary>
        /// METODO PARA GERAR PDF APARTIR DE UM HML
        /// </summary>
        /// <param name="html"></param>
        /// <param name="outputPath"></param>
        public static string SavePdfToFile(this IConverter converter, HtmlToPdfDocument document, string outputPath, string fileName = null)
        {
            var pdfBytes = converter.Convert(document);

            fileName = GetUniqueFileName(fileName ?? Path.GetFileNameWithoutExtension(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()), outputPath, ".pdf").ToLower();

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            var fullPath = Path.Combine(outputPath, $"{fileName}.pdf");

            File.WriteAllBytes(fullPath, pdfBytes);

            return fullPath;
        }

        /// <summary>
        /// METODO PARA CONVERTER ARQUIVO PARA BASE64
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ConvertFileToBase64(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Arquivo não encontrado.", filePath);

            byte[] fileBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(fileBytes);
        }
        /// <summary>
        /// Metodo para aplicar censura a json ocultando dados sensiveis
        /// </summary>
        /// <param name="body"></param>
        /// <param name="censoredWords"></param>
        /// <returns></returns>
        public static string CensorJson(this string body, List<string> censoredWords)
        {
            if (string.IsNullOrEmpty(body) || censoredWords?.Count == 0)
                return string.Empty;

            for (int i = 0; i < censoredWords.Count; i++)
            {
                string field = censoredWords[i];
                if (body.Contains(':'))
                {
                    body = Regex.Replace(
                        body,
                        $@"""({field})""\s*:\s*(""(?:\\""|[^""])*""|\d+|true|false|null)",
                        @"""$1"":""***""",
                        RegexOptions.IgnoreCase
                    );
                }

                if (body.Contains('=') || body.ContainsIgnoreCase("name=\""))
                {
                    body = Regex.Replace(
                        body,
                        $@"({field})=[^&]*",
                        @"$1=***",
                        RegexOptions.IgnoreCase
                    );
                    body = Regex.Replace(
                        body,
                        $@"(Content-Disposition:\s*form-data[^\r\n]*\sname\s*=\s*""{Regex.Escape(field)}""[^\r\n]*(?:\r?\n[^\r\n]*)*\r?\n\r?\n)([^\r\n]*)",
                        "$1***",
                        RegexOptions.IgnoreCase
                    );
                }
            }

            return body;
        }

        /// <summary>
        /// ADICIONAR PATH AO ARQUIVO
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ResolveFilePath(this string fileName, string relativePath = "Content\\Upload")
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return fileName;

            if (Path.IsPathRooted(fileName))
                return fileName;

            return Path.Combine(Directory.GetCurrentDirectory(), relativePath, fileName);
        }
        /// <summary>
        /// METODO PARA OBTER LAT/LNG DO ENDEREÇO
        /// </summary>
        /// <param name="formartedAddress"></param>
        /// <returns></returns>
        public async static Task<OpenStreetAddressViewModel> GetOpenStreetAddress(string formartedAddress)
        {
            var response = await Utilities.CallApi<OpenStreetAddressViewModel>(new()
            {
                Url = $"https://nominatim.openstreetmap.org/search?q={formartedAddress}&format=json&addressdetails=1&limit=1",
                Method = RestSharp.Method.GET
            });

            return response.Data;
        }

        /// <summary>
        /// METODO PARA SOMAR VALORES DOUBLES CORRIGINDO A CASA DECIMAL
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double ToSum(params double[] values)
        {
            if (values == null || values.Length == 0)
                return 0;
            decimal result = 0;
            foreach (var val in values)
                result += (decimal)val;
            return (double)result;
        }
        /// <summary>
        /// METODO PARA SUBTRAIR VALORES DOUBLES CORRIGINDO A CASA DECIMAL
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double ToSubtract(params double[] values)
        {
            if (values == null || values.Length == 0)
                return 0;
            decimal result = (decimal)values[0];
            for (int i = 1; i < values.Length; i++)
                result -= (decimal)values[i];
            return (double)result;
        }

        /// <summary>
        /// METODO PARA MULTIPLICAR VALORES DOUBLES CORRIGINDO A CASA DECIMAL
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double ToMultiply(params double[] values)
        {
            if (values == null || values.Length == 0)
                return 0;
            decimal result = 1;
            foreach (var val in values)
                result *= (decimal)val;
            return (double)result;
        }

        /// <summary>
        /// METODO PARA DIVIDIR VALORES DOUBLES CORRIGINDO A CASA DECIMAL
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>/
        public static double ToDivide(params double[] values)
        {
            if (values == null || values.Length == 0)
                return 0;
            decimal result = (decimal)values[0];
            for (int i = 1; i < values.Length; i++)
                result /= (decimal)values[i];
            return (double)result;
        }
    }
}
