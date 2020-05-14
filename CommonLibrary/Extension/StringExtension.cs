using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Extension
{
    /// <summary>
    ///     文字列拡張メソッド
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        ///     最初と最後の文字を切り落とす。
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="strChop">切り落とす文字列</param>
        /// <returns>加工後文字列</returns>
        public static string Chop(this string str, string strChop)
        {
            return ChopLeft(ChopRight(str, strChop), strChop);
        }

        /// <summary>
        ///     最初の文字を切り落とす。
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="strChop">切り落とす文字列</param>
        /// <returns>加工後文字列</returns>
        public static string ChopLeft(this string str, string strChop)
        {
            if (str == null) return "";

            while (str.StartsWith(strChop))
            {
                str = str.Substring(strChop.Length);
            }

            return str;
        }

        /// <summary>
        ///     最後の文字を切り落とす。
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="strChop">切り落とす文字列</param>
        /// <returns>加工後文字列</returns>
        public static string ChopRight(this string str, string strChop)
        {
            if (str == null) return "";

            while(str.EndsWith(strChop))
            {
                str = str.Substring(0, str.Length - strChop.Length);
            }

            return str;
        }
    }
}
