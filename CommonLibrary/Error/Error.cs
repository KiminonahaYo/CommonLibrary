using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace CommonLibrary.Error
{
    public delegate void OutputMethod(ExceptionInfo[] eArray);

    #region 例外情報構造体（ExceptionInfo）
    /// <summary>
    ///     例外情報構造体
    /// </summary>
    public struct ExceptionInfo
    {
        /// <summary>例外クラス</summary>
        private Exception e;

        /// <summary>
        ///     例外クラスプロパティ
        /// </summary>
        public Exception Value
        {
            get { return e; }
            set { e = value; }
        }

        /// <summary>メモ（備考）</summary>
        private string memo;

        /// <summary>
        ///     メモ（備考）プロパティ
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }

        /// <summary>
        ///     例外とメモ（備考）からExceptionInfo構造体を生成します。
        /// </summary>
        /// <param name="e">例外クラス</param>
        /// <param name="memo_">メモ（備考）</param>
        /// <returns>ExceptionInfo構造体</returns>
        public static ExceptionInfo Parse(Exception e_, string memo_)
        {
            ExceptionInfo eInfo;
            eInfo.e = e_;
            eInfo.memo = memo_;
            return eInfo;
        }
    }
    #endregion

    //エラーバッファ
    public static class ErrorBuffer
    {
        #region private変数
        private static List<ExceptionInfo> eList = new List<ExceptionInfo>();
        #endregion

        #region バッファの処理
        /// <summary>
        ///     例外を追加する（溜める）
        /// </summary>
        /// <param name="e">追加する例外クラス</param>
        public static void Add(Exception e)
        {
            Add(e, "");
        }

        /// <summary>
        ///     例外を追加する（メモ（備考）追加版）
        /// </summary>
        /// <param name="e">例外クラス</param>
        /// <param name="memo_">例外に関するメモ（備考）</param>
        public static void Add(Exception e, string memo_)
        {
            eList.Add(ExceptionInfo.Parse(e, memo_));
        }

        /// <summary>
        ///     例外を追加する（例外情報構造体（ExceptionInfo）指定版
        /// </summary>
        /// <param name="eInfo"></param>
        public static void Add(ExceptionInfo eInfo)
        {
            eList.Add(eInfo);
        }

        /// <summary>
        ///     例外をクリアする
        /// </summary>
        public static void Clear()
        {
            eList.Clear();
        }
        #endregion

        #region 情報の取得
        /// <summary>
        ///     エラーバッファの指定の要素番号の例外クラスを取得する。
        ///     静的クラスではインデクサは使用できないため、このようなメソッドを作成した。
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">引数indexで指定した要素番号が範囲外の場合発生します。</exception>
        /// <param name="index">エラーバッファの要素番号</param>
        /// <returns>例外情報構造体（このクラス内のExceptionInfo構造体）</returns>
        public static ExceptionInfo Item(int index)
        {
            if (!(0 <= index && index < eList.Count))
                throw new ArgumentOutOfRangeException("index", index, "引数indexで指定した要素番号が範囲外です。");

            return eList[index];
        }

        /// <summary>
        ///     foreachで回すためにコレクションで取得できる
        /// </summary>
        /// <returns>コレクション</returns>
        public static IEnumerable<ExceptionInfo> GetCollection()
        {
            foreach (ExceptionInfo eInfo in eList)
                yield return eInfo;
        }
        #endregion

        #region 出力メソッド
        /// <summary>
        ///     例外をユーザー定義の方法で出力する
        /// </summary>
        /// <param name="outMethod">ユーザー定義の例外出力デリゲートメソッド</param>
        public static void Output(OutputMethod outMethod)
        {
            if (eList.Count > 0)
                outMethod(eList.ToArray());
        }

        #region 指定形式出力支援
        #region テキストベース出力支援
        #region private変数
        /// <summary>
        ///     複数出力用の文字列を生成するStringBuilder
        ///     メソッド内でNewするとオブジェクトが増えるので、private変数とする
        /// </summary>
        private static StringBuilder strOut = new StringBuilder();

        /// <summary>
        ///     単数出力用の文字列を生成するStringBuilder
        ///     メソッド内でNewするとオブジェクトが増えるので、private変数とする
        /// </summary>
        private static StringBuilder strOutSingle = new StringBuilder();
        #endregion

        #region メソッド
        /// <summary>
        ///     テキストベースで出力するための文字列を生成し、出力の支援を行います。（配列指定版）
        ///     通常、このメソッドの引数はOutputメソッドに指定するデリゲートメソッドで取得した配列を渡します。
        /// </summary>
        /// <param name="eArray">例外情報構造体の配列</param>
        /// <returns>テキストベースで出力するための文字列</returns>
        public static string GetTextbaseString(ExceptionInfo[] eArray)
        {
            int iCnt = 1;

            strOut.Clear();

            foreach (ExceptionInfo e in eList)
            {
                strOut.AppendLine("●例外No." + iCnt + "：" + e.Value.GetType());
                strOut.Append(GetTextbaseString(e));
                iCnt++;
            }

            return strOut.ToString();
        }

        /// <summary>
        ///     テキストベースで出力するための文字列を生成し、出力の支援を行います。（単数）
        /// </summary>
        /// <param name="eArray">例外情報構造体の配列</param>
        /// <returns>テキストベースで出力するための文字列</returns>
        public static string GetTextbaseString(ExceptionInfo e)
        {
            strOutSingle.Clear();

            strOutSingle.AppendLine("----------------------------------------------------------------");
            strOutSingle.AppendLine(e.Value.Message);
            strOutSingle.AppendLine("----------------------------------------------------------------");
            strOutSingle.AppendLine("メモ：");
            strOutSingle.AppendLine("----------------------------------------------------------------");
            strOutSingle.AppendLine(e.Memo);
            strOutSingle.AppendLine("----------------------------------------------------------------");
            strOutSingle.AppendLine("Source : " + e.Value.Source);
            strOutSingle.AppendLine("TargetSite : " + e.Value.TargetSite);
            strOutSingle.AppendLine("StackTrace : ");
            strOutSingle.AppendLine("----------------------------------------------------------------");
            strOutSingle.AppendLine(e.Value.StackTrace);
            strOutSingle.AppendLine("----------------------------------------------------------------");
            strOutSingle.AppendLine("InnerException : " + e.Value.InnerException);
            strOutSingle.AppendLine("HResult : " + e.Value.HResult);
            strOutSingle.AppendLine("HelpLink : " + e.Value.HelpLink);
            strOutSingle.AppendLine("----------------------------------------------------------------");
            strOutSingle.AppendLine();

            return strOutSingle.ToString();
        }
        #endregion
        #endregion
        #endregion
        #endregion

        #region ※旧コード※
        ////テキストに書き込む
        //public static void WriteText()
        //{
        //    string strFilePath;
        //    string strFileName;
        //    string strFolderPath;
        //    int filePathCnt;

        //    //フォルダパスを取得する
        //    strFolderPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString() + @"\" +
        //                    @"ErrorLogs\" +
        //                    DateTime.Now.ToShortDateString() + @"\";
        //    strFolderPath = strFolderPath.Replace("/", "");

        //    strFileName = "Errors_" + DateTime.Now.ToLongTimeString() + ".log";
        //    strFileName = strFileName.Replace(":", "");
        //    strFilePath = strFolderPath + strFileName;

        //    //フォルダを作成する
        //    MyFolder.Create(strFolderPath);

        //    //ファイルが存在したら連番で保存する
        //    filePathCnt = 2;
        //    while (File.Exists(strFilePath) == true)
        //    {
        //        strFileName = "Errors_" + DateTime.Now.ToShortTimeString() + "(" + filePathCnt + ").log";
        //        strFileName = strFileName.Replace(":", "");
        //        strFilePath = strFolderPath + strFileName;
        //    }

        //    //ファイルにエラーを書き込む
        //    using (StreamWriter sw = new StreamWriter(strFilePath))
        //    {
        //        int iCnt = 1;

        //        foreach (Exception e in eList)
        //        {
        //            sw.WriteLine("エラーNo." + iCnt + " " + e.ToString());
        //            sw.WriteLine("----------------------------------------------------------------");
        //            sw.WriteLine(e.Message);
        //            sw.WriteLine("----------------------------------------------------------------");
        //            sw.WriteLine("Source : " + e.Source);
        //            sw.WriteLine("TargetSite : " + e.TargetSite);
        //            sw.WriteLine("StackTrace : ---------------------------------------------------");
        //            sw.WriteLine(e.StackTrace);
        //            sw.WriteLine("----------------------------------------------------------------");
        //            sw.WriteLine("InnerException : " + e.InnerException);
        //            sw.WriteLine("HResult : " + e.HResult);
        //            sw.WriteLine("HelpLink : " + e.HelpLink);
        //            sw.WriteLine("----------------------------------------------------------------");
        //            sw.WriteLine();
        //            iCnt++;
        //        }
        //    }
        //}
        #endregion
    }
}
