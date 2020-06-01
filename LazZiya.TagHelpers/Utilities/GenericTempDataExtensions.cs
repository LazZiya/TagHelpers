using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
using Newtonsoft.Json;
#else
using System.Text.Json;
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
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
            tempData[key] = JsonConvert.SerializeObject(value);
#else
            tempData[key] = JsonSerializer.Serialize(value);
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
            tempData.TryGetValue(key, out object o);

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
            var obj = JsonConvert.DeserializeObject<T>((string)o);
#else
            var obj = JsonSerializer.Deserialize<T>((string)o);
#endif
            return o == null ? null : obj;
        }
    }
}
