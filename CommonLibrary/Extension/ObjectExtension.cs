using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Extension
{
    /// <summary>
    ///     オブジェクト型の拡張メソッド
    /// </summary>
    public static class ObjectExtension
    {
        private static T ChangeTypeInner<T>(object obj)
        {
            if (obj == null) return default(T);

            return (T)Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        ///     任意の型変換を行う。
        /// </summary>
        /// <typeparam name="T">変換する型</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <returns>変換後型</returns>
        public static T ChangeType<T>(this object obj)
        {
            if (obj == null) return default(T);

            Type tFrom = obj.GetType();
            Type tTo = typeof(T);

            if (tFrom.IsEnum && tTo.IsEnum)
            {
                //列挙体同士の変換
                return (T)Enum.ToObject(tTo, ChangeTypeInner<ulong>(obj));
            }
            else if (!tFrom.IsEnum && tTo.IsEnum)
            {
                //非列挙体→列挙体
                if (tFrom.IsNumber())
                {
                    //数値→列挙体
                    return (T)Enum.ToObject(tTo, ChangeTypeInner<ulong>(obj));
                }
                else if (tFrom.IsString())
                {
                    //文字列→列挙体
                    return (T)Enum.Parse(typeof(T), obj.ToString());
                }
                else
                {
                    return default(T);
                }
            }
            else if (tFrom.IsEnum && !tTo.IsEnum)
            {
                //列挙体→非列挙体
                if (tTo.IsNumber())
                {
                    //列挙体→数値
                    return ChangeTypeInner<T>(obj);
                }
                else if (tTo.IsString())
                {
                    //列挙体→文字列
                    return (T)(object)obj.ToString();
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                return ChangeTypeInner<T>(obj);
            }
        }

        /// <summary>
        ///     任意の型変換を行う。（null許容型）
        /// </summary>
        /// <typeparam name="T">変換する型</typeparam>
        /// <param name="obj">オブジェクト</param>
        /// <returns>変換後型</returns>
        public static T? ChangeTypeToNullable<T>(this object obj) where T: struct
        {
            if (obj == null) return null;

            return (T?)ChangeType<T>(obj);
        }
    }
}
