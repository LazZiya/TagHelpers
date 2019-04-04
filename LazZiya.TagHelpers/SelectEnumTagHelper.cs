using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#if NETCOREAPP1_0 || NETCOREAPP1_1
using System.Reflection;
#endif

namespace LazZiya.TagHelpers
{
    public class SelectEnumTagHelper : TagHelper
    {
        /// <summary>
        /// (int)MyEnum.ValueName
        /// </summary>
        public int SelectedValue { get; set; }

        /// <summary>
        /// typeof(MyEnum)
        /// </summary>
        public Type EnumType { get; set; }

        /// <summary>
        /// localization method as delegate
        /// e.g. delegate(string s) { return _localizaer["My value"]; }
        /// </summary>
        public Func<string, string> TextLocalizerDelegate { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";

            foreach (int e in Enum.GetValues(EnumType))
            {
                var op = new TagBuilder("option");

                op.Attributes.Add("value", $"{e}");

                var displayText = TextLocalizerDelegate == null 
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

            return TextLocalizerDelegate(text);
        }
    }
}
