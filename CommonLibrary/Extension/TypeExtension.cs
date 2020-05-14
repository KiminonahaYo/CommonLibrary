
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Extension
{
    public static class TypeExtension
    {
        /// <summary>
        ///     数値かどうか判定する。
        /// </summary>
        /// <typeparam name="T">型名</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <returns>数値かどうか</returns>
        public static bool IsNumber(this Type obj)
        {
            if (obj == null) return false;

            return obj.IsInteger() || obj.IsRealNumber();
        }

        /// <summary>
        ///     整数かどうか判定する。
        /// </summary>
        /// <typeparam name="T">型名</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <returns>整数かどうか</returns>
        public static bool IsInteger(this Type obj)
        {
            if (obj == null) return false;

            return obj == typeof(int) || obj == typeof(uint) || obj == typeof(short) || obj == typeof(ushort) || obj == typeof(byte) || obj == typeof(sbyte) || obj == typeof(long) || obj == typeof(ulong);
        }

        /// <summary>
        ///     実数かどうか判定する。
        /// </summary>
        /// <typeparam name="T">型名</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <returns>実数かどうか</returns>
        public static bool IsRealNumber(this Type obj)
        {
            if (obj == null) return false;

            return obj == typeof(float) || obj == typeof(double) || obj == typeof(decimal);
        }

        /// <summary>
        ///     文字列かどうか判定する。
        /// </summary>
        /// <typeparam name="T">型名</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <returns>文字列かどうか</returns>
        public static bool IsString(this Type obj)
        {
            if (obj == null) return false;

            return obj == typeof(string);
        }

        /// <summary>
        ///     文字かどうか判定する。
        /// </summary>
        /// <typeparam name="T">型名</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <returns>文字かどうか</returns>
        public static bool IsChar(this Type obj)
        {
            if (obj == null) return false;

            return obj == typeof(char);
        }

        /// <summary>
        ///     日付型かどうか判定する。
        /// </summary>
        /// <typeparam name="T">型名</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <returns>日付型かどうか</returns>
        public static bool IsDateTime(this Type obj)
        {
            if (obj == null) return false;

            return obj == typeof(DateTime);
        }
    }
}
