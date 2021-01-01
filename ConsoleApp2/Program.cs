using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> test = new List<string>();
            test.Add("F     91258880 2018-02-26 03:18:39 Image0/DataPS4_ACVI_TitleScreen.forge");
            test.Add("F          512 2018-02-26 03:18:39 Sc0/license.info");
            //test.Add("D            0                     Sc0/trophy");

            List<string> sizeList = new List<string>();
            List<string> fileList = new List<string>();

            foreach (var item in test)
            {
                var replace = item.Substring(14, 21); // datetime

                var final = item.Replace(replace, "");
                string _1stPlace = final.Substring(1, final.Length - 1);
                string removewhitespace = _1stPlace.Replace(" ", String.Empty);
                string magicWord;
                if (removewhitespace.Contains("Image0"))
                    magicWord = "Image0";
                else
                    magicWord = "Sc0";
                int charLocation = removewhitespace.IndexOf(magicWord, StringComparison.Ordinal);
                string size = "";
                if (charLocation > 0)
                {
                    size = removewhitespace.Substring(0, charLocation);
                    if(size != "0")
                    {
                        sizeList.Add(size);
                        var file = removewhitespace.Replace(size, "");
                        fileList.Add(file);
                    }
                }
                
            }

            Console.WriteLine("file");
            foreach (var item in fileList)
                Console.WriteLine(item);

            Console.WriteLine("size");
            foreach (var item in sizeList)
                Console.WriteLine(item);
            Console.ReadLine();
        }
    }

    public class ps5bc
    {
        private static dynamic json_;
        public static dynamic ps5bcjson
        {
            get { return json_; }
            set { json_ = value; }
        }

        public static string parseJson()
        {
            var ps5bc = Properties.Resources.ps5bc;
            var str = System.Text.Encoding.Default.GetString(ps5bc);
            return str = System.Text.Encoding.Default.GetString(ps5bc);
        }
    }
}
