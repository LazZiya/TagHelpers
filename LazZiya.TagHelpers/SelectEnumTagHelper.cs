using LazZiya.Common;
using LazZiya.TagHelpers.Properties;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// creates a dropdown list from custom enum with supports for localization
    /// </summary>
    public class SelectEnumTagHelper : TagHelper
    {
        private readonly ISharedCultureLocalizer _loc;

        /// <summary>
        /// (int)MyEnum.ValueName
        /// </summary>
        public int SelectedValue { get; set; }

        /// <summary>
        /// typeof(MyEnum)
        /// </summary>
        public Type EnumType { get; set; }

        /// <summary>
        /// This property is not in use! <see cref="ISharedCultureLocalizer"/>
        /// </summary>
        [Obsolete("This property is not in use and will be removed in a feature release. Use an implemetation of ISharedCultureLocalizer instead.")]
        public Func<string, string> TextLocalizerDelegate { get; set; }

        /// <summary>
        /// Initialize a new instance of SelectEnum taghelper
        /// </summary>
        /// <param name="provider"></param>
        public SelectEnumTagHelper(IServiceProvider provider)
        {
            var loc = provider.GetService(typeof(ISharedCultureLocalizer));
            
            if (loc == null)
                throw new NullReferenceException(Resources.LocalizationServiceNull);

            _loc = (ISharedCultureLocalizer)loc;
        }

        /// <summary>
        /// start creating select-enum tag helper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";

            foreach (int e in Enum.GetValues(EnumType))
            {
                var op = new TagBuilder("option");

                op.Attributes.Add("value", $"{e}");

                var displayText = _loc == null
                    ? GetEnumFieldDisplayName(e)
                    : GetEnumFieldLocalizedDisplayName(e);

                op.InnerHtml.Append(displayText);

                if (e == SelectedValue)
                    op.Attributes.Add("selected", "selected");

                output.Content.AppendHtml(op);
            }
        }

        private string GetEnumFieldDisplayName(int value)
        {
            // get enum field name
            var fieldName = Enum.GetName(EnumType, value);

            //get Display(Name = "Field name")
            var displayName = EnumType.GetField(fieldName).GetCustomAttributes(false).OfType<DisplayAttribute>().SingleOrDefault()?.Name;

            return displayName ?? fieldName;
        }

        private string GetEnumFieldLocalizedDisplayName(int value)
        {
            var text = GetEnumFieldDisplayName(value);

            return _loc[text];
        }
    }
}
