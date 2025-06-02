using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UtilityFramework.Application.Core3
{
    public class ToUpperCase : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                return reader.Value?.ToString().ToUpper();
            }
            catch (Exception)
            {

                return reader.Value;
            }

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var t = JToken.FromObject(value?.ToString().ToUpper());

                t.WriteTo(writer);
            }
            catch (Exception)
            {
                throw;
            }


        }
    }

    public class RemovePathImage : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                return reader.Value?.ToString().Split('/').LastOrDefault();
            }
            catch (Exception)
            {

                return reader.Value;
            }

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var t = JToken.FromObject(value?.ToString());

                t.WriteTo(writer);
            }
            catch (Exception)
            {
                throw;
            }


        }
    }

    public class PathImage : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string) || objectType == typeof(List<string>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                if (objectType == typeof(List<string>))
                {
                    if (reader.TokenType == JsonToken.Null)
                        return null;

                    List<string> list = new();

                    reader.Read();
                    while (reader.TokenType != JsonToken.EndArray && reader.Value != null)
                    {
                        list.Add((reader.Value?.ToString()).RemovePathImage());
                        reader.Read();
                    }

                    return list;
                }
                else
                {
                    return (reader.Value?.ToString()).RemovePathImage();
                }
            }
            catch (Exception)
            {

                return reader.Value;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                JToken t = null;
                if (value is List<string>)
                {
                    var listPhotos = value != null ? (List<string>)value : new List<string>();

                    t = JToken.FromObject(listPhotos.Select(x => x.SetPathImage()));
                }
                else
                {
                    t = JToken.FromObject((value?.ToString()).SetPathImage());
                }

                t.WriteTo(writer);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class OnlyNumber : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                return (reader.Value?.ToString()).OnlyNumbers();
            }
            catch (Exception)
            {

                return reader.Value;
            }

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var t = JToken.FromObject(value?.ToString());

                t.WriteTo(writer);
            }
            catch (Exception)
            {
                throw;
            }


        }
    }

    public class ToLowerCase : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                return reader.Value?.ToString().ToLower();
            }
            catch (Exception)
            {
                return reader.Value;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var t = JToken.FromObject(value?.ToString().ToLower());

                t.WriteTo(writer);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class RemoveEmoji : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                return string.IsNullOrEmpty(reader.Value?.ToString())
                    ? null
                    : Regex.Replace(reader.Value?.ToString(), @"\p{Cs}", "");
            }
            catch (Exception)
            {
                return reader.Value;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                var t = JToken.FromObject(string.IsNullOrEmpty(value?.ToString())
                    ? null
                    : Regex.Replace(value?.ToString(), @"\p{Cs}", ""));

                t.WriteTo(writer);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
