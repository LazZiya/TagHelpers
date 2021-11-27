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
    /// Choose where to get alert icons from
    /// </summary>
    public enum IconsSource
    {
        /// <summary>
        /// Bootstrap
        /// </summary>
        Bootstrap,

        /// <summary>
        /// FontAwesome
        /// </summary>
        FontAwesome
    }

    internal struct BootstrapIcons
    {
        internal const string Success = "bi bi-check-circle-fill";
        internal const string Warning = "bi bi-exclamation-triangle-fill";
        internal const string Info = "bi bi-info-circle-fill";
        internal const string Danger = "bi bi-x-circle-fill";
        internal const string Default = "bi bi-info-circle";
    }
    
    internal struct FontAwesomeIcons
    {
        internal const string Success = "fas fa-check-circle";
        internal const string Warning = "fas fa-exclamation-triangle";
        internal const string Info = "fas fa-info-circle";
        internal const string Danger = "fas fa-times-circle";
        internal const string Default = "fas fa-chevron-circle-right";
    }

    /// <summary>
    /// Alert item that can be created in the backend manually for pushing alert to the temp data
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
