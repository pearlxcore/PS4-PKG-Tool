using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS4_PKG_Tool
{
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
            return System.Text.Encoding.Default.GetString(ps5bc);
        }
    }
}
