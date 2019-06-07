namespace LazZiya.TagHelpers.Alerts
{
    /// <summary>
    /// Define alert style depending on Bootstrap4.x alert classes
    /// </summary>
    internal enum AlertStyle {
        /// <summary>
        /// alert-primary
        /// </summary>
        Primary,

        /// <summary>
        /// alert-secondary
        /// </summary>
        Secondary,

        /// <summary>
        /// alert-success
        /// </summary>
        Success,

        /// <summary>
        /// alert-danger
        /// </summary>
        Danger,

        /// <summary>
        /// alert-warning
        /// </summary>
        Warning,

        /// <summary>
        /// alert-info
        /// </summary>
        Info,

        /// <summary>
        /// alert-light
        /// </summary>
        Light,

        /// <summary>
        /// alert-dark
        /// </summary>
        Dark
    }

    /// <summary>
    /// Alert item that can be created in the backend manually for pushing alert to themp data
    /// </summary>
    internal class Alert
    {
        /// <summary>
        /// Key to find alerts in TempData dictionary
        /// </summary>
        public const string TempDataKey = "TempDataAlert";

        /// <summary>
        /// Alert style depending on Bootstrap 4.x classes
        /// </summary>
        public AlertStyle AlertStyle { get; set; } = AlertStyle.Primary;

        /// <summary>
        /// Header text for the alert message
        /// </summary>
        public string AlertHeading { get; set; }

        /// <summary>
        /// Alert message body
        /// </summary>
        public string AlertMessage { get; set; }

        /// <summary>
        /// true for dismissable alert
        /// </summary>
        public bool Dismissable { get; set; } = true;
    }
}
