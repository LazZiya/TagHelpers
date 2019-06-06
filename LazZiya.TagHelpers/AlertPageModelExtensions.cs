using System.Collections.Generic;

#if NETCOREAPP1_0 || NETCOREAPP1_1
#else
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LazZiya.TagHelpers
{
    public static class Alerts
    {
        public static void Primary(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Primary, alertMessage, alertHeader, dismissable);
        }

        public static void Secondary(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Secondary, alertMessage, alertHeader, dismissable);
        }

        public static void Success(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Success, alertMessage, alertHeader, dismissable);
        }

        public static void Danger(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Danger, alertMessage, alertHeader, dismissable);
        }

        public static void Warning(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Warning, alertMessage, alertHeader, dismissable);
        }

        public static void Info(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Info, alertMessage, alertHeader, dismissable);
        }

        public static void Light(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Light, alertMessage, alertHeader, dismissable);
        }

        public static void Dark(this ITempDataDictionary tempData, string alertMessage, string alertHeader = "", bool dismissable = true)
        {
            AddAlert(tempData, AlertStyle.Dark, alertMessage, alertHeader, dismissable);
        }

        private static void AddAlert(ITempDataDictionary tempData, AlertStyle alertStyle, string message, string header, bool dismissable)
        {
            var alerts = tempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)tempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                AlertHeading = header,
                AlertMessage = message,
                Dismissable = dismissable
            });

            tempData[Alert.TempDataKey] = alerts;
        }
    }
}
#endif
