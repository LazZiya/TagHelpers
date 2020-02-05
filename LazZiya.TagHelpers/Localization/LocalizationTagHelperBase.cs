using LazZiya.Common;
using LazZiya.TagHelpers.Properties;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace LazZiya.TagHelpers.Localization
{
    /// <summary>
    /// base class for localize tag helpers
    /// </summary>
    public class LocalizationTagHelperBase : TagHelper
    {
        private readonly ISharedCultureLocalizer _loc;

        /// <summary>
        /// pass array of objects for arguments
        /// </summary>
        [HtmlAttributeName("localize-args")]
        public object[] Args { get; set; }

        /// <summary>
        /// localization string with reference to the specified culture
        /// </summary>
        [HtmlAttributeName("localize-culture")]
        public string Culture { get; set; } = string.Empty;

        /// <summary>
        /// type of the source of localized resources file that containes the local culture strings
        /// </summary>
        [HtmlAttributeName("localize-source")]
        public Type ResourceSource { get; set; }

        /// <summary>
        /// inject SharedCultureLocalizer
        /// </summary>
        /// <param name="provider"></param>
        public LocalizationTagHelperBase(IServiceProvider provider)
        {
            var loc = provider.GetService(typeof(ISharedCultureLocalizer));
            
            if (loc == null)
                throw new NullReferenceException(Resources.LocalizationServiceNull);

            _loc = (ISharedCultureLocalizer)loc;
        }

        /// <summary>
        /// process localize tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();

            if (!string.IsNullOrWhiteSpace(content.GetContent()))
            {
                var str = content.GetContent().Trim();

                LocalizedHtmlString _localStr;

                if (!string.IsNullOrEmpty(Culture) && ResourceSource != null)
                    //localize from specified culture and resource type
                    _localStr = _loc.GetLocalizedHtmlString(ResourceSource, Culture, str, Args);

                else if (!string.IsNullOrEmpty(Culture) && ResourceSource == null)
                    //localize from specified culture and default view localization resource 
                    //type that is defined in startup in .AddExpressLocalization<T1, T2> 
                    //where T2 is the view localization resource
                    _localStr = _loc.GetLocalizedHtmlString(Culture, str, Args);

                else if (string.IsNullOrEmpty(Culture) && ResourceSource != null)
                    //localize from specified resource type using CultureInfo.CurrentCulture.Name
                    _localStr = _loc.GetLocalizedHtmlString(ResourceSource, str, Args);
                else
                    //use default localization
                    _localStr = _loc.GetLocalizedHtmlString(str, Args);

                output.Content.SetHtmlContent(_localStr);
            }
        }
    }
}
