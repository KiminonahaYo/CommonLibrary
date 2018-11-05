using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace CommonLibrary.Win32
{

    /// <summary>
    ///     INIファイルを管理するクラスです。
    ///     文字列、byte配列（文字コード）、整数、実数、文字型、論理型、日付型に対応しています。
    /// </summary>
    /// <remarks>
    ///     使用法例：
    ///     変数 = (IniFileインスタンス).Keys(セクション, キー).ReadToxxx(デフォルト値);
    ///     (IniFileインスタンス).Keys(セクション, キー).Write(値);
    /// </remarks>
    public class IniFile
    {
        #region private変数
        /// <summary>
        ///     INIファイルパス
        /// </summary>
        private string filePath;
        #endregion

        #region DLL関数
        [DllImport("kernel32.dll")]
        public static extern uint
            GetPrivateProfileString(
                string lpAppName,
                string lpKeyName,
                string lpDefault,
                StringBuilder lpReturnedString,
                uint nSize,
                string lpFileName
            );

        [DllImport("kernel32.dll")]
        public static extern uint
            WritePrivateProfileString(
                string lpAppName,
                string lpKeyName,
                string lpString,
                string lpFileName
            );
        #endregion

        #region ★IniFileSectionKeyクラス★
        /// <summary>
        ///     SectionとKeyの情報を持ったクラスです。
        ///     実際にはこのクラスでINIファイルの読み書きを行います。
        /// </summary>
        public class IniFileSectionKey
        {
            #region プロパティとprivate変数
            /// <summary>
            ///     DLL関数に渡すセクション名
            /// </summary>
            private string section;

            /// <summary>
            ///     セクション名プロパティ
            /// </summary>
            public string Section
            {
                get { return this.section; }
            }

            /// <summary>
            ///     DLL関数に渡すキー名
            /// </summary>
            private string key;

            /// <summary>
            ///     キー名プロパティ
            /// </summary>
            public string Key
            {
                get { return this.key; }
            }

            /// <summary>
            ///     ファイル名
            /// </summary>
            private string filePath;

            /// <summary>
            ///     ファイル名プロパティ
            /// </summary>
            public string FilePath
            {
                get { return this.filePath; }
            }
            #endregion

            #region コンストラクタ
            /// <summary>
            ///     コンストラクタ（セクション、キー指定）
            /// </summary>
            /// <param name="section_"></param>
            /// <param name="key_"></param>
            public IniFileSectionKey(string filePath_, string section_, string key_)
            {
                this.filePath = filePath_;
                this.section = section_;
                this.key = key_;
            }
            #endregion

            #region 書き込み
            #region 文字列
            public void Write(string value)
            {
                WritePrivateProfileString(this.section, this.key, value, this.filePath);
            }
            #endregion

            #region Byte配列
            public void Write(byte[] value, Encoding enc)
            {
                this.Write(enc.GetString(value));
            }
            #endregion

            #region 数値
            #region 整数
            public void Write(int value)
            {
                this.Write(value.ToString());
            }
            public void Write(uint value)
            {
                this.Write(value.ToString());
            }
            public void Write(short value)
            {
                this.Write(value.ToString());
            }
            public void Write(ushort value)
            {
                this.Write(value.ToString());
            }
            public void Write(long value)
            {
                this.Write(value.ToString());
            }
            public void Write(ulong value)
            {
                this.Write(value.ToString());
            }
            public void Write(byte value)
            {
                this.Write(value.ToString());
            }
            public void Write(sbyte value)
            {
                this.Write(value.ToString());
            }
            #endregion
            #region 実数
            public void Write(float value)
            {
                this.Write(value.ToString());
            }
            public void Write(double value)
            {
                this.Write(value.ToString());
            }
            public void Write(decimal value)
            {
                this.Write(value.ToString());
            }
            #endregion
            #endregion

            #region 文字型
            public void Write(char value)
            {
                this.Write(value.ToString());
            }
            #endregion
            
            #region 論理型
            public void Write(bool value)
            {
                this.Write(value.ToString());
            }
            #endregion
            
            #region 日付型
            public void Write(DateTime value)
            {
                this.Write(value.ToString());
            }
            #endregion
            #endregion

            #region 読み込み
            #region 文字列
            public string ReadToString()
            {
                return this.ReadToString("");
            }

            public string ReadToString(string strDefault)
            {
                return this.ReadToString(strDefault, 256u);
            }

            public string ReadToString(string strDefault, uint bufSize)
            {
                StringBuilder str = new StringBuilder((int)bufSize);
                GetPrivateProfileString(this.section, this.key, strDefault, str, bufSize, this.filePath);
                return str.ToString();
            }
            #endregion

            #region byte配列
            public byte[] ReadToByteArray(Encoding enc)
            {
                return ReadToByteArray(enc, "");
            }

            public byte[] ReadToByteArray(Encoding enc, string strDefault)
            {
                return ReadToByteArray(enc, strDefault, 256u);
            }

            public byte[] ReadToByteArray(Encoding enc, string strDefault, uint bufSize)
            {
                string str;
                str = this.ReadToString(strDefault, bufSize);
                return enc.GetBytes(str.ToCharArray());
            }
            #endregion

            #region 数値
            #region 整数
            #region int
            public int ReadToInt()
            {
                return this.ReadToInt((int)0);
            }
            public int ReadToInt(int valueDefault)
            {
                int valueRet;

                return int.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (int)0;
            }
            #endregion
            #region uint
            public uint ReadToUInt()
            {
                return this.ReadToUInt((uint)0);
            }
            public uint ReadToUInt(uint valueDefault)
            {
                uint valueRet;

                return uint.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (uint)0;
            }
            #endregion
            #region long
            public long ReadToLong()
            {
                return this.ReadToLong((long)0);
            }
            public long ReadToLong(long valueDefault)
            {
                long valueRet;

                return long.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (long)0;
            }
            #endregion
            #region ulong
            public ulong ReadToULong()
            {
                return this.ReadToULong((ulong)0);
            }
            public ulong ReadToULong(ulong valueDefault)
            {
                ulong valueRet;

                return ulong.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (ulong)0;
            }
            #endregion
            #region short
            public short ReadToShort()
            {
                return this.ReadToShort((short)0);
            }
            public short ReadToShort(short valueDefault)
            {
                short valueRet;

                return short.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (short)0;
            }
            #endregion
            #region ushort
            public ushort ReadToUShort()
            {
                return this.ReadToUShort((ushort)0);
            }
            public ushort ReadToUShort(ushort valueDefault)
            {
                ushort valueRet;

                return ushort.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (ushort)0;
            }
            #endregion
            #region byte
            public byte ReadToByte()
            {
                return this.ReadToByte((byte)0);
            }
            public byte ReadToByte(byte valueDefault)
            {
                byte valueRet;

                return byte.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (byte)0;
            }
            #endregion
            #region sbyte
            public sbyte ReadToSByte()
            {
                return this.ReadToSByte((sbyte)0);
            }
            public sbyte ReadToSByte(sbyte valueDefault)
            {
                sbyte valueRet;

                return sbyte.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (sbyte)0;
            }
            #endregion
            #endregion

            #region 実数
            #region float
            public float ReadToFloat()
            {
                return this.ReadToFloat((float)0);
            }
            public float ReadToFloat(float valueDefault)
            {
                float valueRet;

                return float.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (float)0;
            }
            #endregion
            #region double
            public double ReadToDouble()
            {
                return this.ReadToDouble((double)0);
            }
            public double ReadToDouble(double valueDefault)
            {
                double valueRet;

                return double.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (double)0;
            }
            #endregion
            #region decimal
            public decimal ReadToDecimal()
            {
                return this.ReadToDecimal((decimal)0);
            }
            public decimal ReadToDecimal(decimal valueDefault)
            {
                decimal valueRet;

                return decimal.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (decimal)0;
            }
            #endregion
            #endregion
            #endregion

            #region 文字型
            #region char
            public char ReadToChar(char valueDefault)
            {
                char valueRet;

                return char.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : (char)0;
            }
            #endregion
            #endregion

            #region 論理型
            #region bool
            public bool ReadToBool()
            {
                return this.ReadToBool(false);
            }

            public bool ReadToBool(bool valueDefault)
            {
                bool valueRet;

                return bool.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : false;
            }
            #endregion
            #endregion

            #region 日付型
            #region DateTime
            public DateTime ReadToDateTime(DateTime valueDefault)
            {
                DateTime valueRet;

                return DateTime.TryParse(this.ReadToString(valueDefault.ToString()), out valueRet) ? valueRet : new DateTime(0);
            }
            #endregion
            #endregion
            #endregion
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        ///     コンストラクタ（カレントディレクトリにSettings.iniを作成）
        /// </summary>
        public IniFile()
        {
            this.filePath = Directory.GetCurrentDirectory() + @"\Settings.ini";
        }

        /// <summary>
        ///     コンストラクタ（ファイルパス指定）
        /// </summary>
        /// <param name="filePath_"></param>
        public IniFile(string filePath_)
        {
            FileInfo fi = new FileInfo(filePath_);
            this.filePath = fi.FullName;
        }
        #endregion

        #region プロパティ
        /// <summary>
        ///     ファイルパスの設定/取得を行います。
        /// </summary>
        public string FilePath
        {
            get { return this.filePath; }
            set
            {
                FileInfo fi = new FileInfo(value);
                this.filePath = fi.FullName;
            }
        }
        #endregion

        #region section, keyを持ったクラスを取得（そのクラスでiniファイルを読み書きする）
        /// <summary>
        ///     確保済みKeyオブジェクト格納Dictionary（オブジェクトを増加させないため）
        /// </summary>
        private Dictionary<string, Dictionary<string, IniFileSectionKey>> diKeys = null;
        /// <summary>
        ///     Keyクラスを取得します。
        ///     iniファイルの読み書きはKeyクラスで行います。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">キー名</param>
        /// <returns>Keyクラス</returns>
        public IniFileSectionKey Keys(string section, string key)
        {
            IniFileSectionKey keyRet;

            //Dictionaryが確保されていない
            if (this.diKeys == null)
            {
                this.diKeys = new Dictionary<string, Dictionary<string, IniFileSectionKey>>();
            }

            //sectionがDictionaryに存在しない
            if (!this.diKeys.ContainsKey(section))
            {
                this.diKeys.Add(section, new Dictionary<string, IniFileSectionKey>());
            }

            //keyがDictionaryに存在しない
            if (!this.diKeys[section].ContainsKey(key))
            {
                //Dictionaryに追加して、それを返却する。
                IniFileSectionKey keyClass = new IniFileSectionKey(this.filePath, section, key);
                this.diKeys[section].Add(key, keyClass);
                keyRet = keyClass;
            }
            //section, keyがDictionaryに存在する
            else
            {
                keyRet = this.diKeys[section][key];
            }

            return keyRet;
        }
        #endregion
    }
}

