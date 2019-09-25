using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;
#if NETCOREAPP3_0
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

namespace LazZiya.TagHelpers.Utilities
{
    /// <summary>
    /// Generic extension to TempData for adding complex object and fix serialization problem
    /// </summary>
    /// <![CDATA[https://stackoverflow.com/a/35042391/5519026]]>
    public static class GenericTempDataExtensions
    {
        /// <summary>
        /// Add object to temp data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
#if NETCOREAPP3_0
            tempData[key] = JsonSerializer.Serialize(value);
#else
            tempData[key] = JsonConvert.SerializeObject(value);
#endif
        }

        /// <summary>
        /// Read object from temp data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);

#if NETCOREAPP3_0
            var obj = JsonSerializer.Deserialize<T>((string)o);
#else
            var obj = JsonConvert.DeserializeObject<T>((string)o);
#endif
            return o == null ? null : obj;
        }
    }
}
