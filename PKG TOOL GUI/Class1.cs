using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace PKG_TOOL_GUI
{
    class Class1
    {
        

        static byte[] PKGHeader = new byte[16];

        public static byte[] GETPKGHeader(string dump)
        {
            using (BinaryReader b = new BinaryReader(new FileStream(dump, FileMode.Open)))
            {
                b.BaseStream.Seek(0x0, SeekOrigin.Begin);
                b.Read(PKGHeader, 0, 16);
                return PKGHeader;


            }
        }
    }
}
