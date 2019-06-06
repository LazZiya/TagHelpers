using System;
using System.Collections.Generic;
using System.Text;

namespace LazZiya.TagHelpers
{
    public enum AlertStyle { Primary, Secondary, Success, Danger, Warning, Info, Light, Dark }
    public class Alert
    {
        public const string TempDataKey = "TempDataAlert";
        public AlertStyle AlertStyle { get; set; } = AlertStyle.Primary;
        public string AlertHeading { get; set; }
        public string AlertMessage { get; set; }
        public bool Dismissable { get; set; } = true;
    }
}
