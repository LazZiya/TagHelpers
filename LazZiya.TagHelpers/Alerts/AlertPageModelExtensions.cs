using System.Collections.Generic;
using LazZiya.TagHelpers.Utilities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
#if NETCOREAPP3_0 || NETCOREAPP3_1
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

namespace LazZiya.TagHelpers.Alerts
{
    /// <summary>
    /// Extensions for TempData for creating coder friendly alerts easily
    /// </summary>
    public static class TempDataExtensions
    {
        /// <summary>
        /// Create primary alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="alertMessage">message body</param>
        /// <param name="alertHeader">message header</param>
        /// <param name="dismissable">Show closing button</param>
        public static void Primary(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Primary, alertMessage, alertHeader, dismissable);
        }

        /// <summary>
        /// Create secondary alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="alertMessage">message body</param>
        /// <param name="alertHeader">message header</param>
        /// <param name="dismissable">Show closing button</param>
        public static void Secondary(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Secondary, alertMessage, alertHeader, dismissable);
        }

        /// <summary>
        /// Create success alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="alertMessage">message body</param>
        /// <param name="alertHeader">message header</param>
        /// <param name="dismissable">Show closing button</param>
        public static void Success(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Success, alertMessage, alertHeader, dismissable);
        }

        /// <summary>
        /// Create danger alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="alertMessage">message body</param>
        /// <param name="alertHeader">message header</param>
        /// <param name="dismissable">Show closing button</param>
        public static void Danger(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Danger, alertMessage, alertHeader, dismissable);
        }

        /// <summary>
        /// Create warning alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="alertMessage">message body</param>
        /// <param name="alertHeader">message header</param>
        /// <param name="dismissable">Show closing button</param>
        public static void Warning(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Warning, alertMessage, alertHeader, dismissable);
        }

        /// <summary>
        /// Create info alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="alertMessage">message body</param>
        /// <param name="alertHeader">message header</param>
        /// <param name="dismissable">Show closing button</param>
        public static void Info(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Info, alertMessage, alertHeader, dismissable);
        }

        /// <summary>
        /// Create light alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="alertMessage">message body</param>
        /// <param name="alertHeader">message header</param>
        /// <param name="dismissable">Show closing button</param>
        public static void Light(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Light, alertMessage, alertHeader, dismissable);
        }

        /// <summary>
        /// Create dark alert
        /// </summary>
        /// <param name="tempData">TempData</param>
        /// <param name="alertMessage">message body</param>
        /// <param name="alertHeader">message header</param>
        /// <param name="dismissable">Show closing button</param>
        public static void Dark(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Dark, alertMessage, alertHeader, dismissable);
        }

        private static void AddAlert(ITempDataDictionary tempData, AlertStyle alertStyle, string message, string header, bool dismissable)
        {
#if NETCOREAPP3_0 || NETCOREAPP3_1
            var alerts = tempData.ContainsKey(Alert.TempDataKey)
                ? JsonSerializer.Deserialize<List<Alert>>(tempData[Alert.TempDataKey].ToString())
                : new List<Alert>();
#else
            var alerts = tempData.ContainsKey(Alert.TempDataKey)
                ? JsonConvert.DeserializeObject<List<Alert>>(tempData[Alert.TempDataKey].ToString())
                : new List<Alert>();
#endif
            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                AlertHeading = header,
                AlertMessage = message,
                Dismissable = dismissable
            });

            tempData.Put<List<Alert>>(Alert.TempDataKey, alerts);
        }
    }
}
