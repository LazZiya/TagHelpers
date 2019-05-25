#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// defines location to load localization valdiation scripts
    /// </summary>
    public enum ScriptSource {
        /// <summary>
        /// valdiation scripts are located under wwwroot/lib folder
        /// </summary>
        Local,

        /// <summary>
        /// valdiation scripts will be loaded from jsdelivr
        /// </summary>
        JsDeliver
    }

    /// <summary>
    /// creates localization validation scripts tag to place validiation scripts inside
    /// </summary>
    [HtmlTargetElement("localization-validation-scripts")]
    public class ExpressLocalizationTagHelperComponentTagHelper : TagHelperComponentTagHelper
    {
        public ScriptSource Source { get; set; } = ScriptSource.JsDeliver;

        public ExpressLocalizationTagHelperComponentTagHelper(ITagHelperComponentManager manager, ILoggerFactory loggerFactory) : base(manager, loggerFactory)
        {
        }
    }
}
#endif