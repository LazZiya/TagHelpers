using System;

namespace LazZiya.TagHelpers
{
    /// <summary>
    /// The label to display for language dropdown list on language names
    /// </summary>
    public enum LanguageLabel
    {
        /// <summary>
        /// Culture name
        /// </summary>
        Name,

        /// <summary>
        /// Culture display name
        /// </summary>
        DisplayName,

        /// <summary>
        /// Culture English name
        /// </summary>
        EnglishName,

        /// <summary>
        /// Culture native name
        /// </summary>
        NativeName,

        /// <summary>
        /// Two letter ISO language name
        /// </summary>
        TwoLetterISOLanguageName
    }

    internal class LanguageItem
    {
        public string Name { get; set; }
        public string DisplayText { get; set; }
        public string Url { get; set; }
    }
}
