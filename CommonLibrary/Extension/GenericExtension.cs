using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CommonLibrary.Extension
{
    /// <summary>
    ///     ジェネリック型の拡張メソッド
    /// </summary>
    public static class GenericExtension
    {
        /// <summary>
        ///     ディープコピーを行う。
        /// </summary>
        /// <typeparam name="T">コピーするオブジェクトの型</typeparam>
        /// <param name="obj">コピー元</param>
        /// <returns>コピーされたオブジェクト</returns>
        public static T DeepCopy<T>(this T obj)
        {
            if (obj == null) return default(T);

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
