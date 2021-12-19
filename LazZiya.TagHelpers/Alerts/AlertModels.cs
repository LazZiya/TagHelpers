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
        /// Bootstrap5
        /// </summary>
        Bootstrap5,

        /// <summary>
        /// FontAwesome
        /// </summary>
        FontAwesome
    }

    internal struct Bootstrap5Icons
    {
        internal const string Success = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" fill=\"currentColor\" class=\"bi bi-check-circle-fill flex-shrink-0 me-2\" viewBox=\"0 0 16 16\" role=\"img\" aria-label=\"Success:\"><path d=\"M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z\"/></svg>";
        
        internal const string Warning = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" fill=\"currentColor\" class=\"bi bi-exclamation-triangle-fill flex-shrink-0 me-2\" viewBox=\"0 0 16 16\" role=\"img\" aria-label=\"Warning:\"><path d=\"M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z\"/></svg>";
        
        internal const string Info = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" fill=\"currentColor\" class=\"bi bi-info-fill flex-shrink-0 me-2\" viewBox=\"0 0 16 16\" role=\"img\" aria-label=\"Info:\"><path d=\"M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z\"/></svg>";
        
        internal const string Danger = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" fill=\"currentColor\" class=\"bi bi-exclamation-triangle-fill flex-shrink-0 me-2\" viewBox=\"0 0 16 16\" role=\"img\" aria-label=\"Danger:\"><path d=\"M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z\"/></svg>";
        
        internal const string Default = Info;
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
