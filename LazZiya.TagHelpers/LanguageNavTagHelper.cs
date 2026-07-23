using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// creates a language navigation menu, depends on supported cultures
    /// </summary>
    public class LanguageNavTagHelper : TagHelper
    {
        /// <summary>
        /// optional: manually specify list of supported cultures
        /// <example>
        /// en-US,tr, ar
        /// </example>
        /// </summary>
        public string SupportedCultures { get; set; }

        /// <summary>
        /// optional: what to display as label for language dropdown
        /// default: LanguageLabel.EnglishName
        /// </summary>
        public LanguageLabel LanguageLabel { get; set; } = LanguageLabel.EnglishName;

        /// <summary>
        /// <para>ViewContext property is not required to be passed as parameter, it will be auto assigned by the tag helpoer.</para>
        /// <para>current view context to access RouteData.Values and Request.Query collection</para>
        /// </summary>
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Choose render mode: classis for regular dropdown list, Bootstrap4 for HTML5 div with Bootstrap4 style.
        /// </summary>
        public RenderMode RenderMode { get; set; } = RenderMode.Bootstrap;

        /// <summary>
        /// Set the handler url for setting culture cookie on language change
        /// </summary>
        public string CookieHandlerUrl { get; set; }

        /// <summary>
        /// The url to redirect to on langugae change. 
        /// The url must have at one place holder for culture value. 
        /// e.g. /{0}/Home
        /// </summary>
        public string RedirectToUrl { get; set; }

        /// <summary>
        /// Show relevant country flag for specific culture. 
        /// Flags will be shown only if the culture is country specific. 
        /// e.g. "tr" will not render any flag, but "tr-sy" will render Turkish flag. 
        /// Required reference to flag-icon-css
        /// </summary>
        public bool Flags { get; set; } = false;

        /// <summary>
        /// true: Show flags in squared images, 
        /// false: Show flags in rounded images, 
        /// </summary>
        public bool FlagsSquared { get; set; } = false;

        /// <summary>
        /// Whether show border or not
        /// </summary>
        public bool Border { get; set; } = true;

        /// <summary>
        /// required for listing supported cultures. 
        /// The handler must contain two place holders for culture name and return url. 
        /// e.g.: <![CDATA[SetCookieCulture?cltr={0}&returnUrl={1}]]>
        /// </summary>
        private readonly IOptions<RequestLocalizationOptions> _ops;

        /// <summary>
        /// creates a language navigation menu, depends on supported cultures
        /// </summary>
        /// <param name="ops"></param>
        public LanguageNavTagHelper(IOptions<RequestLocalizationOptions> ops)
        {
            _ops = ops;
        }

        /// <summary>
        /// start creating the language navigation dropdown
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var langDictionary = CreateNavDictionary();

            switch (RenderMode)
            {
                case RenderMode.Bootstrap:
                    CreateBootstrapItems(ref output, langDictionary);
                    break;
                case RenderMode.Bootstrap5:
                    CreateBootstrap5Items(ref output, langDictionary);
                    break;
                case RenderMode.Classic:
                    CreateClassicItems(ref output, langDictionary);
                    break;
                case RenderMode.FormControl:
                    CreateFormControlItems(ref output, langDictionary);
                    break;
            }
        }

        /// <summary>
        /// create classic list items list
        /// <example><![CDATA[<option value="/en-US/Index">English</option>]]></example>
        /// </summary>
        /// <param name="langDictionary">language name-URL dictionary</param>
        /// <param name="output">reference to TagHelperOuput</param>
        /// <returns></returns>
        private void CreateClassicItems(ref TagHelperOutput output, List<LanguageItem> langDictionary)
        {
            output.TagName = "select";
            output.Attributes.Add("onchange", "this.options[this.selectedIndex].value && (window.location = this.options[this.selectedIndex].value);");

            foreach (var lang in langDictionary.OrderBy(x => x.DisplayText))
            {
                var option = new TagBuilder("option");
                option.Attributes.Add("value", lang.Url);
                option.InnerHtml.AppendHtml(lang.DisplayText);

                if (CultureInfo.CurrentCulture.Name == lang.Name)
                    option.Attributes.Add("selected", "selected");

                output.Content.AppendHtml(option);
            }
        }

        /// <summary>
        /// create a dropdown form control
        /// <example><![CDATA[<option value="/en-US/Index">English</option>]]></example>
        /// </summary>
        /// <param name="langDictionary">language name-URL dictionary</param>
        /// <param name="output">reference to TagHelperOuput</param>
        /// <returns></returns>
        private void CreateFormControlItems(ref TagHelperOutput output, List<LanguageItem> langDictionary)
        {
            output.TagName = "select";

            foreach (var lang in langDictionary.OrderBy(x => x.DisplayText))
            {
                var option = new TagBuilder("option");
                option.Attributes.Add("value", lang.Name);
                option.InnerHtml.AppendHtml(lang.DisplayText);

                if (CultureInfo.CurrentCulture.Name == lang.Name)
                    option.Attributes.Add("selected", "selected");

                output.Content.AppendHtml(option);
            }
        }

        /// <summary>
        /// create classic list items list
        /// <example><![CDATA[<a href="/en-US/Index" class="itemxyz">English</a>]]></example>
        /// </summary>
        /// <param name="langDictionary">language name-URL dictionary</param>
        /// <param name="output">reference to TagHelperOuput</param>
        /// <returns></returns>
        private void CreateBootstrapItems(ref TagHelperOutput output, List<LanguageItem> langDictionary)
        {
            var div = new TagBuilder("div");

            if (CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
                div.AddCssClass("dropdown-menu dropdown-menu-left");
            else
                div.AddCssClass("dropdown-menu dropdown-menu-right");

            div.Attributes.Add("aria-labeledby", "dropdownlang");

            foreach (var lang in langDictionary.Where(x => x.Name != CultureInfo.CurrentCulture.Name).OrderBy(x => x.DisplayText))
            {

                var a = new TagBuilder("a");
                a.AddCssClass("dropdown-item small");
                a.Attributes.Add("href", lang.Url);

                if (Flags)
                {
                    var flagName = lang.Name.Split('-');
                    if (flagName.Length == 2)
                    {

                        if (FlagsSquared)
                            a.InnerHtml.AppendHtml($"<span class=\"flag-icon flag-icon-{flagName[1].ToLowerInvariant()} flag-icon-squared\"></span>&nbsp;");
                        else
                            a.InnerHtml.AppendHtml($"<span class=\"flag-icon flag-icon-{flagName[1].ToLowerInvariant()}\"></span>&nbsp;");
                    }
                }

                a.InnerHtml.Append(lang.DisplayText);

                div.InnerHtml.AppendHtml(a);
            }

            output.TagName = "div";
            output.Attributes.Add("class", "dropdown");

            var toggle = CreateToggle();
            output.Content.AppendHtml(toggle);

            output.Content.AppendHtml(div);
        }

        /// <summary>
        /// create classic list items list
        /// <example><![CDATA[<a href="/en-US/Index" class="itemxyz">English</a>]]></example>
        /// </summary>
        /// <param name="langDictionary">language name-URL dictionary</param>
        /// <param name="output">reference to TagHelperOuput</param>
        /// <returns></returns>
        private void CreateBootstrap5Items(ref TagHelperOutput output, List<LanguageItem> langDictionary)
        {
            var ul = new TagBuilder("ul");

            if (CultureInfo.CurrentCulture.TextInfo.IsRightToLeft)
                ul.AddCssClass("dropdown-menu dropdown-menu-left");
            else
                ul.AddCssClass("dropdown-menu dropdown-menu-right");

            ul.Attributes.Add("aria-labeledby", "dropdownlang");

            foreach (var lang in langDictionary.Where(x => x.Name != CultureInfo.CurrentCulture.Name).OrderBy(x => x.DisplayText))
            {
                var li = new TagBuilder("li");
                var a = new TagBuilder("a");
                a.AddCssClass("dropdown-item small");
                a.Attributes.Add("href", lang.Url);

                if (Flags)
                {
                    var flagName = lang.Name.Split('-');
                    if (flagName.Length == 2)
                    {
                        if (FlagsSquared)
                            a.InnerHtml.AppendHtml($"<span class=\"flag-icon flag-icon-{flagName[1].ToLowerInvariant()} flag-icon-squared\"></span>&nbsp;");
                        else
                            a.InnerHtml.AppendHtml($"<span class=\"flag-icon flag-icon-{flagName[1].ToLowerInvariant()}\"></span>&nbsp;");
                    }
                }

                a.InnerHtml.Append(lang.DisplayText);
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }

            output.TagName = "div";
            output.Attributes.Add("class", "dropdown");

            var toggle = CreateToggle();
            output.Content.AppendHtml(toggle);

            output.Content.AppendHtml(ul);
        }

        /// <summary>
        /// create dictonary for all supported cultures
        /// <para>Key: Language display name for label</para>
        /// <para>Value: Navigation URL</para>
        /// </summary>
        /// <returns></returns>
        private List<LanguageItem> CreateNavDictionary()
        {
            var dic = new List<LanguageItem>();
            var cultures = GetSupportedCultures();

            foreach (var cul in cultures)
            {
                var redUrl = RedirectToUrl ?? "/{0}";

                var url = string.Format(Uri.UnescapeDataString(redUrl), cul.Name);

                if (!string.IsNullOrWhiteSpace(CookieHandlerUrl))
                {
                    url = string.Format(Uri.UnescapeDataString(CookieHandlerUrl), cul.Name, url);
                }

                var label = GetLanguageLabel(cul);
                dic.Add(new LanguageItem { Name = cul.Name, DisplayText = label, Url = url });
            }

            return dic;
        }

        /// <summary>
        /// private list of supported CultureInfo,
        /// </summary>
        private IEnumerable<CultureInfo> GetSupportedCultures()
        {
            // if the user didn't specify manually list of supported cultures, 
            // then create cultures list with reference to supported cultures defined in localization settings in startup</para>
            if (string.IsNullOrWhiteSpace(SupportedCultures))
                return _ops.Value.SupportedCultures;

            //if the user will specify supported cultures manually, then this list will be created accordingly
            var cList = new List<CultureInfo>();
            foreach (var c in SupportedCultures.Split(new[] { ',', '|', ';', ' ' }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                cList.Add(new CultureInfo(c));
            }

            return cList;
        }

        private string GetLanguageLabel(CultureInfo cul)
        {
            switch (LanguageLabel)
            {
                case LanguageLabel.Name: return cul.Name;
                case LanguageLabel.DisplayName: return cul.DisplayName;
                case LanguageLabel.EnglishName: return cul.EnglishName;
                case LanguageLabel.NativeName: return cul.NativeName;
                case LanguageLabel.TwoLetterISOLanguageName: return cul.TwoLetterISOLanguageName;

                default: return cul.EnglishName;
            }
        }

        private TagBuilder CreateToggle()
        {
            var toggle = new TagBuilder("a");

            var cssBuilder = new StringBuilder("btn-sm btn-default border-secondary dropdown-toggle");

            if (Border) cssBuilder.Append(" border");
            toggle.AddCssClass(cssBuilder.ToString());

            toggle.Attributes.Add("id", "dropdownLang");
            toggle.Attributes.Add("href", "#");
            toggle.Attributes.Add("role", "button");

            if (RenderMode == RenderMode.Bootstrap5)
                toggle.Attributes.Add("data-bs-toggle", "dropdown");
            else
                toggle.Attributes.Add("data-toggle", "dropdown");

            toggle.Attributes.Add("aria-haspopup", "true");
            toggle.Attributes.Add("aria-expanded", "false");

            var labelTxt = GetLanguageLabel(CultureInfo.CurrentCulture);

            if (Flags)
            {
                var flagName = CultureInfo.CurrentCulture.Name.Split('-');
                if (flagName.Length == 2)
                {
                    if (FlagsSquared)
                        toggle.InnerHtml.AppendHtml($"<span class=\"flag-icon flag-icon-{flagName[1].ToLowerInvariant()} flag-icon-squared\"></span>&nbsp;");
                    else
                        toggle.InnerHtml.AppendHtml($"<span class=\"flag-icon flag-icon-{flagName[1].ToLowerInvariant()}\"></span>&nbsp;");
                }
            }

            toggle.InnerHtml.AppendHtml(labelTxt);

            return toggle;
        }
        /*
        private TagBuilder CreateToggle5()
        {
            var toggle = new TagBuilder("a");
            toggle.AddCssClass("btn-sm btn-default border border-secondary dropdown-toggle");
            toggle.Attributes.Add("id", "dropdownLang");
            toggle.Attributes.Add("href", "#");
            toggle.Attributes.Add("role", "button");
            toggle.Attributes.Add("data-bs-toggle", "dropdown");
            toggle.Attributes.Add("aria-haspopup", "true");
            toggle.Attributes.Add("aria-expanded", "false");

            var labelTxt = GetLanguageLabel(CultureInfo.CurrentCulture);

            if (Flags)
            {
                var flagName = CultureInfo.CurrentCulture.Name.Split('-');
                if (flagName.Length == 2)
                {
                    if (FlagsSquared)
                        toggle.InnerHtml.AppendHtml($"<span class=\"flag-icon flag-icon-{flagName[1].ToLowerInvariant()} flag-icon-squared\"></span>&nbsp;");
                    else
                        toggle.InnerHtml.AppendHtml($"<span class=\"flag-icon flag-icon-{flagName[1].ToLowerInvariant()}\"></span>&nbsp;");
                }
            }

            toggle.InnerHtml.AppendHtml(labelTxt);

            return toggle;
        }*/
    }
}
