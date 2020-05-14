using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary.Extension;

namespace CommonLibraryTestConsole
{
    class Program
    {
        public enum Enum1
        {
            ItemA = 1,
            ItemB = 2,
            ItemC = 4,
            ItemD = 8
        }

        public enum Enum2
        {
            ItemX = 1,
            ItemY = 2,
            ItemZ = 8
        }

        [Serializable]
        public class Class1
        {
            public string str1;
            public string str2;
        }

        static void Main(string[] args)
        {
            Enum1 en = Enum1.ItemD;
            Console.WriteLine(en.ChangeType<int>());

            int i = 8;
            Console.WriteLine(i.ChangeType<Enum1>());

            string s = "ItemZ";
            Console.WriteLine(s.ChangeType<Enum2>().ChangeType<int>());

            Console.WriteLine(en.ChangeType<string>());

            Class1 class1 = new Class1();
            class1.str1 = "a";
            class1.str2 = "b";
            Console.WriteLine("str1 = {0}, str2 = {1}", class1.str1, class1.str2);

            Class1 class12 = class1.DeepCopy();
            Console.WriteLine("str1 = {0}, str2 = {1}", class12.str1, class12.str2);

            class1.str2 = "c";
            Console.WriteLine("str1 = {0}, str2 = {1}", class12.str1, class12.str2);

            Console.WriteLine(DateTime.Now.ToWarekiDateStringDefault());

            Console.WriteLine("★★★★★test★★★★★★★★★".Chop("★★"));

            Console.ReadLine();
        }
    }
}
