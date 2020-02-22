using System.Collections.Generic;
using System.Linq;

namespace LazZiya.TagHelpers.Utilities
{
    /// <summary>
    /// Number formats for different cultures.
    /// <para>https://github.com/unicode-cldr/cldr-core/blob/master/supplemental/numberingSystems.json</para>
    /// </summary>
    public static class NumberFormats
    {
        /// <summary>
        /// Receives a number in system format, and converts it to any other format.
        /// See <see cref="NumberFormats"/>
        /// </summary>
        /// <param name="number"></param>
        /// <param name="targetFormat"></param>
        /// <returns></returns>
        public static string ToNumberFormat(this int number, string targetFormat)
        {
            string _str = string.Empty;
            switch (targetFormat)
            {
                case NumberFormats.Default: _str = number.ToString("N0"); break;
                case NumberFormats.Hex: _str = number.ToString("X"); break;
                case NumberFormats.Roman: _str = ((uint)number).ToRoman(); break;
                default:
                    var numberStr = number.ToString();
                    var newNum = string.Empty;

                    for (int i = 0; i < numberStr.Length; i++)
                        newNum += targetFormat.Split(' ')[int.Parse(numberStr[i].ToString())];

                    _str = string.Join("", newNum);
                    break;
            }

            return _str;
        }

        /// <summary>
        /// System default numbering format
        /// </summary>
        public const string Default = "default";

        /// <summary>
        /// 0123456789
        /// </summary>
        public const string Arabic = "0 1 2 3 4 5 6 7 8 9";

        /// <summary>
        /// Use hexadecimal numbering system
        /// </summary>
        public const string Hex = "hex";

        /// <summary>
        /// I II III IV V VI
        /// </summary>
        public const string Roman = "roman";

        /// <summary>
        /// ٠١٢٣٤٥٦٧٨٩
        /// </summary>
        public const string Hindi = "٠ ١ ٢ ٣ ٤ ٥ ٦ ٧ ٨ ٩";

        /// <summary>
        /// 𑁦𑁧𑁨𑁩𑁪𑁫𑁬𑁭𑁮𑁯
        /// </summary>
        public const string Brah = "𑁦 𑁧 𑁨 𑁩 𑁪 𑁫 𑁬 𑁭 𑁮 𑁯";

        /// <summary>
        /// ০১২৩৪৫৬৭৮৯
        /// </summary>
        public const string Beng = "০ ১ ২ ৩ ৪ ৫ ৬ ৭ ৮ ৯";

        /// <summary>
        /// ०१२३४५६७८९
        /// </summary>
        public const string Deva = "० १ २ ३ ४ ५ ६ ७ ८ ९";

        /// <summary>
        /// ۰۱۲۳۴۵۶۷۸۹
        /// </summary>
        public const string Farsi = "۰ ۱ ۲ ۳ ۴ ۵ ۶ ۷ ۸ ۹";

        /// <summary>
        /// ０１２３４５６７８９
        /// </summary>
        public const string Fullwide = "０ １ ２ ３ ４ ５ ６ ７ ８ ９";

        /// <summary>
        /// ೦೧೨೩೪೫೬೭೮೯
        /// </summary>
        public const string Knda = "೦ ೧ ೨ ೩ ೪ ೫ ೬ ೭ ೮ ೯";

        /// <summary>
        /// ૦૧૨૩૪૫૬૭૮૯
        /// </summary>
        public const string Gujr = "૦ ૧ ૨ ૩ ૪ ૫ ૬ ૭ ૮ ૯";

        /// <summary>
        /// ੦੧੨੩੪੫੬੭੮੯
        /// </summary>
        public const string Guru = "੦ ੧ ੨ ੩ ੪ ੫ ੬ ੭ ੮ ੯";

        /// <summary>
        /// 〇一二三四五六七八九
        /// </summary>
        public const string Hanidec = "〇 一 二 三 四 五 六 七 八 九";

        /// <summary>
        /// ꧐꧑꧒꧓꧔꧕꧖꧗꧘꧙
        /// </summary>
        public const string Java = "꧐ ꧑ ꧒ ꧓ ꧔ ꧕ ꧖ ꧗ ꧘ ꧙";

        /// <summary>
        /// ០១២៣៤៥៦៧៨៩
        /// </summary>
        public const string Khmr = "០ ១ ២ ៣ ៤ ៥ ៦ ៧ ៨ ៩";

        /// <summary>
        /// ໐໑໒໓໔໕໖໗໘໙
        /// </summary>
        public const string Laoo = "໐ ໑ ໒ ໓ ໔ ໕ ໖ ໗ ໘ ໙";

        /// <summary>
        /// 0123456789
        /// </summary>
        public const string Latin = "0 1 2 3 4 5 6 7 8 9";

        /// <summary>
        /// 𝟎𝟏𝟐𝟑𝟒𝟓𝟔𝟕𝟖𝟗
        /// </summary>
        public const string Mathbold = "𝟎 𝟏 𝟐 𝟑 𝟒 𝟓 𝟔 𝟕 𝟖 𝟗";

        /// <summary>
        /// 𝟘𝟙𝟚𝟛𝟜𝟝𝟞𝟟𝟠𝟡
        /// </summary>
        public const string Mathborder = "𝟘 𝟙 𝟚 𝟛 𝟜 𝟝 𝟞 𝟟 𝟠 𝟡";

        /// <summary>
        /// 𝟶𝟷𝟸𝟹𝟺𝟻𝟼𝟽𝟾𝟿
        /// </summary>
        public const string Mathmono = "𝟶 𝟷 𝟸 𝟹 𝟺 𝟻 𝟼 𝟽 𝟾 𝟿";

        /// <summary>
        /// 𝟬𝟭𝟮𝟯𝟰𝟱𝟲𝟳𝟴𝟵
        /// </summary>
        public const string Mathanb = "𝟬 𝟭 𝟮 𝟯 𝟰 𝟱 𝟲 𝟳 𝟴 𝟵";

        /// <summary>
        /// 𝟢𝟣𝟤𝟥𝟦𝟧𝟨𝟩𝟪𝟫
        /// </summary>
        public const string Mathsans = "𝟢 𝟣 𝟤 𝟥 𝟦 𝟧 𝟨 𝟩 𝟪 𝟫";

        /// <summary>
        /// ൦൧൨൩൪൫൬൭൮൯
        /// </summary>
        public const string Mlym = "൦ ൧ ൨ ൩ ൪ ൫ ൬ ൭ ൮ ൯";

        /// <summary>
        /// ᠐᠑᠒᠓᠔᠕᠖᠗᠘᠙
        /// </summary>
        public const string Mong = "᠐ ᠑ ᠒ ᠓ ᠔ ᠕ ᠖  ᠗ ᠘ ᠙";

        /// <summary>
        /// ၀၁၂၃၄၅၆၇၈၉
        /// </summary>
        public const string Mymr = "၀ ၁ ၂ ၃ ၄ ၅ ၆ ၇ ၈ ၉";

        /// <summary>
        /// ႐႑႒႓႔႕႖႗႘႙
        /// </summary>
        public const string Mymrshan = "႐ ႑ ႒ ႓ ႔ ႕ ႖ ႗ ႘ ႙";

        /// <summary>
        /// ꧰꧱꧲꧳꧴꧵꧶꧷꧸꧹
        /// </summary>
        public const string Mymtlng = "꧰ ꧱ ꧲ ꧳ ꧴ ꧵ ꧶ ꧷ ꧸ ꧹";

        /// <summary>
        /// ߀߁߂߃߄߅߆߇߈߉
        /// </summary>
        public const string Nkoo = "߀ ߁ ߂ ߃ ߄ ߅ ߆ ߇ ߈ ߉";

        /// <summary>
        /// ᱐᱑᱒᱓᱔᱕᱖᱗᱘᱙
        /// </summary>
        public const string Olck = "᱐ ᱑ ᱒ ᱓ ᱔ ᱕ ᱖ ᱗ ᱘ ᱙";

        /// <summary>
        /// ୦୧୨୩୪୫୬୭୮୯
        /// </summary>
        public const string Orya = "୦ ୧ ୨ ୩ ୪ ୫ ୬ ୭ ୮ ୯";

        /// <summary>
        /// 𐒠𐒡𐒢𐒣𐒤𐒥𐒦𐒧𐒨𐒩
        /// </summary>
        public const string Osma = "𐒠 𐒡 𐒢 𐒣 𐒤 𐒥 𐒦 𐒧 𐒨 𐒩";

        /// <summary>
        /// ෦෧෨෩෪෫෬෭෮෯
        /// </summary>
        public const string Sinh = "෦ ෧ ෨ ෩ ෪ ෫ ෬ ෭ ෮ ෯";

        /// <summary>
        /// ᧐᧑᧒᧓᧔᧕᧖᧗᧘᧙
        /// </summary>
        public const string Talu = "᧐ ᧑ ᧒ ᧓ ᧔ ᧕ ᧖ ᧗ ᧘ ᧙";

        /// <summary>
        /// ௦௧௨௩௪௫௬௭௮௯
        /// </summary>
        public const string Tamldec = "௦ ௧ ௨ ௩ ௪ ௫ ௬ ௭ ௮ ௯";

        /// <summary>
        /// ౦౧౨౩౪౫౬౭౮౯
        /// </summary>
        public const string Telu = "౦ ౧ ౨ ౩ ౪ ౫ ౬ ౭ ౮ ౯";

        /// <summary>
        /// ๐๑๒๓๔๕๖๗๘๙
        /// </summary>
        public const string Thai = "๐ ๑ ๒ ๓ ๔ ๕ ๖ ๗ ๘ ๙";

        /// <summary>
        /// ༠༡༢༣༤༥༦༧༨༩
        /// </summary>
        public const string Tibt = "༠ ༡ ༢ ༣ ༤ ༥ ༦ ༧ ༨ ༩";

        /// <summary>
        /// ꘠꘡꘢꘣꘤꘥꘦꘧꘨꘩
        /// </summary>
        public const string Vaii = "꘠ ꘡ ꘢ ꘣ ꘤ ꘥ ꘦ ꘧ ꘨ ꘩";
    }
}
