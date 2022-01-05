using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Exchange.TradingExecutor.Core.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this IEnumerable<T> src)
        {
            try
            {
                return JsonConvert.SerializeObject(src ?? Array.Empty<T>());
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static IEnumerable<T> FromJson<T>(this string src)
        {
            try
            {
                return string.IsNullOrEmpty(src)
                    ? Array.Empty<T>()
                    : JsonConvert.DeserializeObject<T[]>(src);
            }
            catch (Exception)
            {
                return Array.Empty<T>();
            }
        }
    }
}