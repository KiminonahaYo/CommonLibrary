using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CommonLibrary.Extension
{
    /// <summary>
    ///     日付型拡張メソッド
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        ///     Windowsの設定に左右されないToShortDateStringメソッドです。
        /// </summary>
        /// <param name="obj">日付型</param>
        /// <returns>YYYY/MM/DD</returns>
        public static string ToShortDateStringDefault(this DateTime obj)
        {
            return obj.ToString("yyyy/MM/dd");
        }

        /// <summary>
        ///     Windowsの設定に左右されない和暦を取得するメソッドです。
        /// </summary>
        /// <param name="obj">日付型</param>
        /// <returns>和暦</returns>
        public static string ToWarekiDateStringDefault(this DateTime obj)
        {
            CultureInfo culture = new CultureInfo("ja-JP");
            JapaneseCalendar calendar = new JapaneseCalendar();
            culture.DateTimeFormat.Calendar = calendar;

            return obj.ToString("gy年MM月dd日", culture);
        }
    }
}
