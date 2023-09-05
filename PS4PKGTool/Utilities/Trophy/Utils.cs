using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;
using Ionic.Zip;
using System.Drawing.Imaging;
using System.Linq;
using static DDSReader.DDSImage;
using System.IO.Compression;
using ZipFile = Ionic.Zip.ZipFile;

namespace PS4_Trophy_xdpx
{
    internal class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] Hex2Binary(string hex)
        {
            var chars = hex.ToCharArray();
            var bytes = new List<byte>();
            for (int index = 0; index < chars.Length; index += 2)
            {
                var chunk = new string(chars, index, 2);
                bytes.Add(byte.Parse(chunk, NumberStyles.AllowHexSpecifier));
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// Converts a Littel Endian Hex Decimal value to a Integer Decimal value
        /// </summary>
        /// <param name="Hex">The byte[] Array to convert from</param>
        /// <param name="reverse">Defines if a array is Little Endian and should be reversed first</param>
        /// <returns>The converted Integer Decimal value</returns>
        public static long HexToDec(byte[] Hex, string reverse = "")
        {
            if (reverse == "reverse")
            {
                Array.Reverse(Hex);
            }

            string bufferString = BitConverter.ToString(Hex).Replace("-", "");
            long bufferInteger = Convert.ToInt32(bufferString, 16);
            return bufferInteger;
        }

        /// <summary>
        /// Kombinated Command for Read or Write Binary or Integer Data
        /// </summary>
        /// <param name="fileToUse">The File that will be used to Read from or to Write to it</param>
        /// <param name="fileToUse2">This is used for the "both" methode. fileToUse will be the file to read from and fileToUse2 will be the file to write to it.</param>
        /// <param name="methodReadOrWriteOrBoth">Defination for Read "r" or Write "w" or if you have big data just use Both "b"</param>
        /// <param name="methodBinaryOrInteger">Defination for Binary Data (bi) or Integer Data (in) when write to a file</param>
        /// <param name="binData">byte array of the binary data to read or write</param>
        /// <param name="binData2">integer array of the integer data to read or write</param>
        /// <param name="offset">Otional, used for the "both" methode to deffine a offset to start to read from a file. If you do not wan't to read from the begin use this var to tell the Routine to jump to your deffined offset.</param>
        /// <param name="count">Optional, also used for the "both" methode to deffine to only to read a specific byte count and not till the end of the file.</param>
        public static void ReadWriteData(string fileToUse, string fileToUse2 = "", string methodReadOrWriteOrBoth = "", string methodBinaryOrInteger = "", byte[] binData = null, int binData2 = 0, long offset = 0, long count = 0)
        {
            byte[] readBuffer;
            string caseSwitch = methodReadOrWriteOrBoth;
            switch (caseSwitch)
            {
                case "r":
                    {
                        FileInfo fileInfo = new FileInfo(fileToUse);
                        readBuffer = new byte[fileInfo.Length];
                        using (BinaryReader b = new BinaryReader(new FileStream(fileToUse, FileMode.Open, FileAccess.Read)))
                        {
                            b.Read(readBuffer, 0, readBuffer.Length);
                            b.Close();
                        }
                    }
                    break;
                case "w":
                    {
                        using (BinaryWriter b = new BinaryWriter(new FileStream(fileToUse, FileMode.Append, FileAccess.Write)))
                        {
                            caseSwitch = methodBinaryOrInteger;
                            switch (caseSwitch)
                            {
                                case "bi":
                                    {
                                        b.Write(binData, 0, binData.Length);
                                        b.Close();
                                    }
                                    break;
                                case "in":
                                    {
                                        b.Write(binData2);
                                        b.Close();
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case "b":
                    {   // For data that will cause a buffer overflow we use this method. We read from a Input File and Write to a Output File with the help of a Buffer till the end of file or the specified length is reached.
                        using (BinaryReader br = new BinaryReader(new FileStream(fileToUse, FileMode.Open, FileAccess.Read)))
                        {
                            using (BinaryWriter bw = new BinaryWriter(new FileStream(fileToUse2, FileMode.Append, FileAccess.Write)))
                            {
                                // this is a variable for the Buffer size. Play arround with it and maybe set a new size to get better result's
                                int workingBufferSize = 4096; // high
                                // int workingBufferSize = 2048; // middle
                                // int workingBufferSize = 1024; // default
                                // int workingBufferSize = 128;  // minimum

                                // Do we read data that is smaller then our working buffer size?
                                if (count < workingBufferSize)
                                {
                                    workingBufferSize = (int)count;
                                }

                                byte[] buffer = new byte[workingBufferSize];
                                int len;

                                // Do we use a specific offset?
                                if (offset != 0)
                                {
                                    br.BaseStream.Seek(offset, SeekOrigin.Begin);
                                }

                                // Run the process in a loop
                                while ((len = br.Read(buffer, 0, workingBufferSize)) != 0)
                                {
                                    bw.Write(buffer, 0, len);

                                    // Do we read a specific length?
                                    if (count != 0)
                                    {
                                        // Subtract the working buffer size from the byte count to read/write.
                                        count -= workingBufferSize;

                                        // Stop the loop when the specified byte count to read/write is reached.
                                        if (count == 0)
                                        {
                                            break;
                                        }

                                        // When the count value is lower then the working buffer size we set the working buffer to the value of the count variable to not read more data as wanted
                                        if (count < workingBufferSize)
                                        {
                                            workingBufferSize = (int)count;
                                        }
                                    }
                                }
                                bw.Close();
                            }
                            br.Close();
                        }
                    }
                    break;
            }
        }

        public static bool CompareBytes(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }


        public static void ExtractFileToDirectory(string zipFileName, string outputDirectory)
        {
            ZipFile zip = ZipFile.Read(zipFileName);
            Directory.CreateDirectory(outputDirectory);
            foreach (ZipEntry e in zip)
            {
                // check if you want to extract e or not
                if (e.FileName == "TheFileToExtract")
                    e.Extract(outputDirectory, ExtractExistingFileAction.OverwriteSilently);
            }
        }



        /*converts byte to encrypted string*/
        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /*VB Basics Method not needed as c# has this function already*/
        //public static uint NewReadUInt32(object stream)
        //{
        //    byte[] array = new byte[4];
        //    Type type = null;
        //    string memberName = "Read";
        //    object[] array2 = new object[]
        //    {
        //        array,
        //        0,
        //        4
        //    };
        //    object[] arguments = array2;
        //    string[] argumentNames = null;
        //    Type[] typeArguments = null;
        //    bool[] array3 = new bool[]
        //    {
        //        true,
        //        false,
        //        false
        //    };
        //    NewLateBinding.LateCall(stream, type, memberName, arguments, argumentNames, typeArguments, array3, true);
        //    if (array3[0])
        //    {
        //        array = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(byte[]));
        //    }
        //    Array.Reverse(array, 0, 4);
        //    return BitConverter.ToUInt32(array, 0);

        //    //string temp = "";
        //    //BinaryReader reader = (BinaryReader)stream;
        //    ////throw new Exception("sfasda");
        //    //return reader.ReadUInt32();
        //}

        public static uint ReadUInt32(object stream)
        {
            byte[] array = new byte[4];
            Type type = null;
            string memberName = "Read";
            object[] array2 = new object[]
            {
                array,
                0,
                4
            };
            object[] arguments = array2;
            string[] argumentNames = null;
            Type[] typeArguments = null;
            bool[] array3 = new bool[]
            {
                true,
                false,
                false
            };
            //NewLateBinding.LateCall(stream, type, memberName, arguments, argumentNames, typeArguments, array3, true);
            //if (array3[0])
            //{
            //    array = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(byte[]));
            //}

            array = ((BinaryReader)stream).ReadBytes(array.Length);

            Array.Reverse(array, 0, 4);
            return BitConverter.ToUInt32(array, 0);

            //string temp = "";
            //BinaryReader reader = (BinaryReader)stream;
            ////throw new Exception("sfasda");
            //return reader.ReadUInt32();
        }


        public static ushort ReadUInt16(object stream)
        {
            byte[] array = new byte[4];
            Type type = null;
            string memberName = "Read";
            object[] array2 = new object[]
            {
                array,
                0,
                2
            };
            object[] arguments = array2;
            string[] argumentNames = null;
            Type[] typeArguments = null;
            bool[] array3 = new bool[]
            {
                true,
                false,
                false
            };
            //NewLateBinding.LateCall(stream, type, memberName, arguments, argumentNames, typeArguments, array3, true);
            //if (array3[0])
            //{
            //    array = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(byte[]));
            //}
            array = ((BinaryReader)stream).ReadBytes(array.Length);
            Array.Reverse(array, 0, 2);
            return BitConverter.ToUInt16(array, 0);

            ////we need to make sure this all works 
            //string temp = "";
            //BinaryReader reader = (BinaryReader)stream;
            ////throw new Exception("sfasda");
            //return reader.ReadUInt16();
        }


        public static string ReadASCIIString(object stream, int legth)
        {
            byte[] array = new byte[checked(legth - 1 + 1)];
            Type type = null;
            string memberName = "Read";
            object[] array2 = new object[]
            {
                array,
                0,
                array.Length
            };
            object[] arguments = array2;
            string[] argumentNames = null;
            Type[] typeArguments = null;
            bool[] array3 = new bool[]
            {
                true,
                false,
                false
            };
            //NewLateBinding.LateCall(stream, type, memberName, arguments, argumentNames, typeArguments, array3, true);
            //if (array3[0])
            //{
            //    array = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(byte[]));
            //}

            array = ((BinaryReader)stream).ReadBytes(array.Length);
            return Encoding.ASCII.GetString(array);
        }

        public static string ReadUTF8String(object stream, int legth)
        {
            byte[] array = new byte[checked(legth - 1 + 1)];
            Type type = null;
            string memberName = "Read";
            object[] array2 = new object[]
            {
                array,
                0,
                array.Length
            };
            object[] arguments = array2;
            string[] argumentNames = null;
            Type[] typeArguments = null;
            bool[] array3 = new bool[]
            {
                true,
                false,
                false
            };
            //NewLateBinding.LateCall(stream, type, memberName, arguments, argumentNames, typeArguments, array3, true);
            //if (array3[0])
            //{
            //    array = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(byte[]));
            //}

            array = ((BinaryReader)stream).ReadBytes(array.Length);
            return Encoding.UTF8.GetString(array);
        }

        public static byte[] ReadByte(object stream, int legth)
        {



            byte[] array = new byte[checked(legth - 1 + 1)];
            Type type = null;
            string memberName = "Read";
            object[] array2 = new object[]
            {
                array,
                0,
                array.Length
            };
            object[] arguments = array2;
            string[] argumentNames = null;
            Type[] typeArguments = null;
            bool[] array3 = new bool[]
            {
                true,
                false,
                false
            };
            //NewLateBinding.LateCall(stream, type, memberName, arguments, argumentNames, typeArguments, array3, true);
            //if (array3[0])
            //{
            //    array = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(byte[]));

            //}
            array = ((BinaryReader)stream).ReadBytes(array.Length);
            //File.WriteAllBytes(@"C:\Temp\Testing\tropy.trp", array);
            return array;
        }

        public static System.Drawing.Bitmap BytesToBitmap(byte[] ImgBytes)
        {
            System.Drawing.Bitmap result = null;
            if (ImgBytes != null)
            {
                MemoryStream stream = new MemoryStream(ImgBytes);
                result = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(stream);
            }
            return result;
        }

        public static bool isLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public static bool Contain(byte[] a, byte[] b)
        {
            checked
            {
                if (a != null)
                {
                    if (b != null)
                    {
                        if (a.Length > 0 && b.Length > 0)
                        {
                            if (a.Length != b.Length)
                            {
                                return false;
                            }
                            int num = 0;
                            int num2 = a.Length - 1;
                            for (int i = num; i <= num2; i++)
                            {
                                if (a[i] != b[i])
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public static string HexToString(string hex)
        {
            //StringBuilder stringBuilder = new StringBuilder(hex.Length / 2);
            //int num = 0;
            //checked
            //{
            //    int num2 = hex.Length - 2;
            //    for (int i = num; i <= num2; i += 2)
            //    {
            //        stringBuilder.Append(Strings.Chr((int)Convert.ToByte(hex.Substring(i, 2), 16)));
            //    }
            //    return stringBuilder.ToString();
            //}
            var bytes = new byte[hex.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return Encoding.ASCII.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"

        }

        public static string Hex(byte Byte)
        {
            return Byte.ToString("X");
        }


        public static long byteArrayToLittleEndianInteger(byte[] bits)
        {
            return (long)((ulong)(bits[0] | (byte)(bits[1] << 0) | (byte)(bits[2] << 0) | (byte)(bits[3] << 0)));
        }

        public static bool ByteArraysEqual(byte[] first, byte[] second)
        {
            if (first == second)
            {
                return true;
            }
            if (first == null || second == null)
            {
                return false;
            }
            if (first.Length != second.Length)
            {
                return false;
            }
            int num = 0;
            checked
            {
                int num2 = first.Length - 1;
                for (int i = num; i <= num2; i++)
                {
                    if (first[i] != second[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static string byteArrayToUTF8String(byte[] _byte)
        {
            return Encoding.UTF8.GetString(_byte);
        }

        public static string byteArrayToHexString(byte[] bytes_Input)
        {
            StringBuilder stringBuilder = new StringBuilder(checked(bytes_Input.Length * 2));
            foreach (byte number in bytes_Input)
            {
                if (Utils.Hex(number).Length == 1)
                {
                    stringBuilder.Append("0" + Utils.Hex(number));
                }
                else
                {
                    stringBuilder.Append("" + Utils.Hex(number));
                }
            }
            return stringBuilder.ToString();
            //StringBuilder hex = new StringBuilder(bytes_Input.Length * 2);

            //foreach (byte b in bytes_Input)
            //{
            //    hex.AppendFormat("{0:x2}", b);
            //}
            //return hex.ToString();
        }

        public static long hexStringToLong(string strHex)
        {
            checked
            {
                return (long)HexLiteral2Unsigned(strHex);
            }
        }

        public static ulong HexLiteral2Unsigned(string hex)
        {
            if (string.IsNullOrEmpty(hex)) throw new ArgumentException("hex");

            int i = hex.Length > 1 && hex[0] == '0' && (hex[1] == 'x' || hex[1] == 'X') ? 2 : 0;
            ulong value = 0;

            while (i < hex.Length)
            {
                uint x = hex[i++];

                if (x >= '0' && x <= '9') x = x - '0';
                else if (x >= 'A' && x <= 'F') x = (x - 'A') + 10;
                else if (x >= 'a' && x <= 'f') x = (x - 'a') + 10;
                else throw new ArgumentOutOfRangeException("hex");

                value = 16 * value + x;

            }

            return value;
        }


        public static T CreateJaggedArray<T>(params int[] lengths)
        {
            return (T)InitializeJaggedArray(typeof(T).GetElementType(), 0, lengths);
        }
        private static object InitializeJaggedArray(Type type, int index, int[] lengths)
        {
            Array array = Array.CreateInstance(type, lengths[index]);

            Type elementType = type.GetElementType();
            if (elementType == null) return array;

            for (int i = 0; i < lengths[index]; i++)
            {
                array.SetValue(InitializeJaggedArray(elementType, index + 1, lengths), i);
            }

            return array;
        }

        [MethodImpl(256)]
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        [MethodImpl(256)]
        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        [MethodImpl(256)]
        public static short Clamp16(int value)
        {
            if (value > short.MaxValue)
                return short.MaxValue;
            if (value < short.MinValue)
                return short.MinValue;
            return (short)value;
        }

        public static sbyte Clamp4(int value)
        {
            if (value > 7)
                return 7;
            if (value < -8)
                return -8;
            return (sbyte)value;
        }


    }

    /// <summary>
    ///     Taken from System.Net in 4.0, useful until we move to .NET 4.0 - needed for Client Profile
    /// </summary>
    internal static class WebUtility
    {
        // Fields
        private static char[] _htmlEntityEndingChars = new char[] { ';', '&' };

        // Methods
        public static string HtmlDecode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            if (value.IndexOf('&') < 0)
            {
                return value;
            }
            StringWriter output = new StringWriter(CultureInfo.InvariantCulture);
            HtmlDecode(value, output);
            return output.ToString();
        }

        public static void HtmlDecode(string value, TextWriter output)
        {
            if (value != null)
            {
                if (output == null)
                {
                    throw new ArgumentNullException("output");
                }
                if (value.IndexOf('&') < 0)
                {
                    output.Write(value);
                }
                else
                {
                    int length = value.Length;
                    for (int i = 0; i < length; i++)
                    {
                        char ch = value[i];
                        if (ch == '&')
                        {
                            int num3 = value.IndexOfAny(_htmlEntityEndingChars, i + 1);
                            if ((num3 > 0) && (value[num3] == ';'))
                            {
                                string entity = value.Substring(i + 1, (num3 - i) - 1);
                                if ((entity.Length > 1) && (entity[0] == '#'))
                                {
                                    ushort num4;
                                    if ((entity[1] == 'x') || (entity[1] == 'X'))
                                    {
                                        ushort.TryParse(entity.Substring(2), NumberStyles.AllowHexSpecifier, (IFormatProvider)NumberFormatInfo.InvariantInfo, out num4);
                                    }
                                    else
                                    {
                                        ushort.TryParse(entity.Substring(1), NumberStyles.Integer, (IFormatProvider)NumberFormatInfo.InvariantInfo, out num4);
                                    }
                                    if (num4 != 0)
                                    {
                                        ch = (char)num4;
                                        i = num3;
                                    }
                                }
                                else
                                {
                                    i = num3;
                                    char ch2 = HtmlEntities.Lookup(entity);
                                    if (ch2 != '\0')
                                    {
                                        ch = ch2;
                                    }
                                    else
                                    {
                                        output.Write('&');
                                        output.Write(entity);
                                        output.Write(';');
                                        goto Label_0117;
                                    }
                                }
                            }
                        }
                        output.Write(ch);
                    Label_0117:;
                    }
                }
            }
        }

        public static string HtmlEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            if (IndexOfHtmlEncodingChars(value, 0) == -1)
            {
                return value;
            }
            StringWriter output = new StringWriter(CultureInfo.InvariantCulture);
            HtmlEncode(value, output);
            return output.ToString();
        }

        public static unsafe void HtmlEncode(string value, TextWriter output)
        {
            if (value != null)
            {
                if (output == null)
                {
                    throw new ArgumentNullException("output");
                }
                int num = IndexOfHtmlEncodingChars(value, 0);
                if (num == -1)
                {
                    output.Write(value);
                }
                else
                {
                    int num2 = value.Length - num;
                    fixed (char* str = value)
                    {
                        char* chPtr = str;
                        char* chPtr2 = chPtr;
                        while (num-- > 0)
                        {
                            chPtr2++;
                            output.Write(chPtr2[0]);
                        }
                        while (num2-- > 0)
                        {
                            chPtr2++;
                            char ch = chPtr2[0];
                            if (ch <= '>')
                            {
                                switch (ch)
                                {
                                    case '&':
                                        {
                                            output.Write("&amp;");
                                            continue;
                                        }
                                    case '\'':
                                        {
                                            output.Write("&#39;");
                                            continue;
                                        }
                                    case '"':
                                        {
                                            output.Write("&quot;");
                                            continue;
                                        }
                                    case '<':
                                        {
                                            output.Write("&lt;");
                                            continue;
                                        }
                                    case '>':
                                        {
                                            output.Write("&gt;");
                                            continue;
                                        }
                                }
                                output.Write(ch);
                                continue;
                            }
                            if ((ch >= '\x00a0') && (ch < 'Ā'))
                            {
                                output.Write("&#");
                                output.Write(((int)ch).ToString(NumberFormatInfo.InvariantInfo));
                                output.Write(';');
                            }
                            else
                            {
                                output.Write(ch);
                            }
                        }
                    }
                }
            }
        }

        private static unsafe int IndexOfHtmlEncodingChars(string s, int startPos)
        {
            int num = s.Length - startPos;
            fixed (char* str = s)
            {
                char* chPtr = str;
                char* chPtr2 = chPtr + startPos;
                while (num > 0)
                {
                    char ch = chPtr2[0];
                    if (ch <= '>')
                    {
                        switch (ch)
                        {
                            case '&':
                            case '\'':
                            case '"':
                            case '<':
                            case '>':
                                return (s.Length - num);

                            case '=':
                                goto Label_0086;
                        }
                    }
                    else if ((ch >= '\x00a0') && (ch < 'Ā'))
                    {
                        return (s.Length - num);
                    }
                Label_0086:
                    chPtr2++;
                    num--;
                }
            }
            return -1;
        }

        // Nested Types
        private static class HtmlEntities
        {
            // Fields
            private static string[] _entitiesList = new string[] {
        "\"-quot", "&-amp", "'-apos", "<-lt", ">-gt", "\x00a0-nbsp", "\x00a1-iexcl", "\x00a2-cent", "\x00a3-pound", "\x00a4-curren", "\x00a5-yen", "\x00a6-brvbar", "\x00a7-sect", "\x00a8-uml", "\x00a9-copy", "\x00aa-ordf",
        "\x00ab-laquo", "\x00ac-not", "\x00ad-shy", "\x00ae-reg", "\x00af-macr", "\x00b0-deg", "\x00b1-plusmn", "\x00b2-sup2", "\x00b3-sup3", "\x00b4-acute", "\x00b5-micro", "\x00b6-para", "\x00b7-middot", "\x00b8-cedil", "\x00b9-sup1", "\x00ba-ordm",
        "\x00bb-raquo", "\x00bc-frac14", "\x00bd-frac12", "\x00be-frac34", "\x00bf-iquest", "\x00c0-Agrave", "\x00c1-Aacute", "\x00c2-Acirc", "\x00c3-Atilde", "\x00c4-Auml", "\x00c5-Aring", "\x00c6-AElig", "\x00c7-Ccedil", "\x00c8-Egrave", "\x00c9-Eacute", "\x00ca-Ecirc",
        "\x00cb-Euml", "\x00cc-Igrave", "\x00cd-Iacute", "\x00ce-Icirc", "\x00cf-Iuml", "\x00d0-ETH", "\x00d1-Ntilde", "\x00d2-Ograve", "\x00d3-Oacute", "\x00d4-Ocirc", "\x00d5-Otilde", "\x00d6-Ouml", "\x00d7-times", "\x00d8-Oslash", "\x00d9-Ugrave", "\x00da-Uacute",
        "\x00db-Ucirc", "\x00dc-Uuml", "\x00dd-Yacute", "\x00de-THORN", "\x00df-szlig", "\x00e0-agrave", "\x00e1-aacute", "\x00e2-acirc", "\x00e3-atilde", "\x00e4-auml", "\x00e5-aring", "\x00e6-aelig", "\x00e7-ccedil", "\x00e8-egrave", "\x00e9-eacute", "\x00ea-ecirc",
        "\x00eb-euml", "\x00ec-igrave", "\x00ed-iacute", "\x00ee-icirc", "\x00ef-iuml", "\x00f0-eth", "\x00f1-ntilde", "\x00f2-ograve", "\x00f3-oacute", "\x00f4-ocirc", "\x00f5-otilde", "\x00f6-ouml", "\x00f7-divide", "\x00f8-oslash", "\x00f9-ugrave", "\x00fa-uacute",
        "\x00fb-ucirc", "\x00fc-uuml", "\x00fd-yacute", "\x00fe-thorn", "\x00ff-yuml", "Œ-OElig", "œ-oelig", "Š-Scaron", "š-scaron", "Ÿ-Yuml", "ƒ-fnof", "ˆ-circ", "˜-tilde", "Α-Alpha", "Β-Beta", "Γ-Gamma",
        "Δ-Delta", "Ε-Epsilon", "Ζ-Zeta", "Η-Eta", "Θ-Theta", "Ι-Iota", "Κ-Kappa", "Λ-Lambda", "Μ-Mu", "Ν-Nu", "Ξ-Xi", "Ο-Omicron", "Π-Pi", "Ρ-Rho", "Σ-Sigma", "Τ-Tau",
        "Υ-Upsilon", "Φ-Phi", "Χ-Chi", "Ψ-Psi", "Ω-Omega", "α-alpha", "β-beta", "γ-gamma", "δ-delta", "ε-epsilon", "ζ-zeta", "η-eta", "θ-theta", "ι-iota", "κ-kappa", "λ-lambda",
        "μ-mu", "ν-nu", "ξ-xi", "ο-omicron", "π-pi", "ρ-rho", "ς-sigmaf", "σ-sigma", "τ-tau", "υ-upsilon", "φ-phi", "χ-chi", "ψ-psi", "ω-omega", "ϑ-thetasym", "ϒ-upsih",
        "ϖ-piv", " -ensp", " -emsp", " -thinsp", "‌-zwnj", "‍-zwj", "‎-lrm", "‏-rlm", "–-ndash", "—-mdash", "‘-lsquo", "’-rsquo", "‚-sbquo", "“-ldquo", "”-rdquo", "„-bdquo",
        "†-dagger", "‡-Dagger", "•-bull", "…-hellip", "‰-permil", "′-prime", "″-Prime", "‹-lsaquo", "›-rsaquo", "‾-oline", "⁄-frasl", "€-euro", "ℑ-image", "℘-weierp", "ℜ-real", "™-trade",
        "ℵ-alefsym", "←-larr", "↑-uarr", "→-rarr", "↓-darr", "↔-harr", "↵-crarr", "⇐-lArr", "⇑-uArr", "⇒-rArr", "⇓-dArr", "⇔-hArr", "∀-forall", "∂-part", "∃-exist", "∅-empty",
        "∇-nabla", "∈-isin", "∉-notin", "∋-ni", "∏-prod", "∑-sum", "−-minus", "∗-lowast", "√-radic", "∝-prop", "∞-infin", "∠-ang", "∧-and", "∨-or", "∩-cap", "∪-cup",
        "∫-int", "∴-there4", "∼-sim", "≅-cong", "≈-asymp", "≠-ne", "≡-equiv", "≤-le", "≥-ge", "⊂-sub", "⊃-sup", "⊄-nsub", "⊆-sube", "⊇-supe", "⊕-oplus", "⊗-otimes",
        "⊥-perp", "⋅-sdot", "⌈-lceil", "⌉-rceil", "⌊-lfloor", "⌋-rfloor", "〈-lang", "〉-rang", "◊-loz", "♠-spades", "♣-clubs", "♥-hearts", "♦-diams"
     };
            private static Dictionary<string, char> _lookupTable = GenerateLookupTable();

            // Methods
            private static Dictionary<string, char> GenerateLookupTable()
            {
                Dictionary<string, char> dictionary = new Dictionary<string, char>(StringComparer.Ordinal);
                foreach (string str in _entitiesList)
                {
                    dictionary.Add(str.Substring(2), str[0]);
                }
                return dictionary;
            }

            public static char Lookup(string entity)
            {
                char ch;
                _lookupTable.TryGetValue(entity, out ch);
                return ch;
            }
        }
    }

    public static class StreamExtensions
    {
        public static void WriteUInt16LE(this Stream s, ushort i)
        {
            byte[] tmp = new byte[2];
            tmp[0] = (byte)(i & 0xFF);
            tmp[1] = (byte)((i >> 8) & 0xFF);
            s.Write(tmp, 0, 2);
        }

        public static void WriteUInt32LE(this Stream s, uint i)
        {
            byte[] tmp = new byte[4];
            tmp[0] = (byte)(i & 0xFF);
            tmp[1] = (byte)((i >> 8) & 0xFF);
            tmp[2] = (byte)((i >> 16) & 0xFF);
            tmp[3] = (byte)((i >> 24) & 0xFF);
            s.Write(tmp, 0, 4);
        }

        public static void WriteUInt64LE(this Stream s, ulong i)
        {
            byte[] tmp = new byte[8];
            tmp[0] = (byte)(i & 0xFF);
            tmp[1] = (byte)((i >> 8) & 0xFF);
            tmp[2] = (byte)((i >> 16) & 0xFF);
            tmp[3] = (byte)((i >> 24) & 0xFF);
            i >>= 32;
            tmp[4] = (byte)(i & 0xFF);
            tmp[5] = (byte)((i >> 8) & 0xFF);
            tmp[6] = (byte)((i >> 16) & 0xFF);
            tmp[7] = (byte)((i >> 24) & 0xFF);
            s.Write(tmp, 0, 8);
        }

        public static void WriteInt16LE(this Stream s, short i)
        {
            s.WriteUInt16LE(unchecked((ushort)i));
        }

        public static void WriteInt32LE(this Stream s, int i)
        {
            s.WriteUInt32LE(unchecked((uint)i));
        }

        public static void WriteInt64LE(this Stream s, long i)
        {
            s.WriteUInt64LE(unchecked((ulong)i));
        }

        public static void WriteLE(this Stream s, ushort i) => s.WriteUInt16LE(i);
        public static void WriteLE(this Stream s, uint i) => s.WriteUInt32LE(i);
        public static void WriteLE(this Stream s, ulong i) => s.WriteUInt64LE(i);
        public static void WriteLE(this Stream s, short i) => s.WriteInt16LE(i);
        public static void WriteLE(this Stream s, int i) => s.WriteInt32LE(i);
        public static void WriteLE(this Stream s, long i) => s.WriteInt64LE(i);

        public static uint ReadUInt32LE(this Stream s) => unchecked((uint)s.ReadInt32LE());


        /// <summary>
        /// Read a signed 32-bit little-endian integer from the stream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ReadInt32LE(this Stream s)
        {
            int ret;
            byte[] tmp = new byte[4];
            s.Read(tmp, 0, 4);
            ret = tmp[0] & 0x000000FF;
            ret |= (tmp[1] << 8) & 0x0000FF00;
            ret |= (tmp[2] << 16) & 0x00FF0000;
            ret |= (tmp[3] << 24);
            return ret;
        }

        /// <summary>
        /// Read a null-terminated ASCII string from the given stream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReadASCIINullTerminated(this Stream s, int limit = -1)
        {
            StringBuilder sb = new StringBuilder(255);
            int cur;
            while ((limit == -1 || sb.Length < limit) && (cur = s.ReadByte()) > 0)
            {
                sb.Append((char)cur);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Read a given number of bytes from a stream into a new byte array.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="count">Number of bytes to read (maximum)</param>
        /// <returns>New byte array of size &lt;=count.</returns>
        public static byte[] ReadBytes(this Stream s, int count)
        {
            // Size of returned array at most count, at least difference between position and length.
            int realCount = (int)((s.Position + count > s.Length) ? (s.Length - s.Position) : count);
            byte[] ret = new byte[realCount];
            s.Read(ret, 0, realCount);
            return ret;
        }

        /// <summary>
        /// Read a signed 64-bit little-endian integer from the stream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ReadInt64LE(this Stream s)
        {
            long ret;
            byte[] tmp = new byte[8];
            s.Read(tmp, 0, 8);
            ret = tmp[4] & 0x000000FFL;
            ret |= (tmp[5] << 8) & 0x0000FF00L;
            ret |= (tmp[6] << 16) & 0x00FF0000L;
            ret |= (tmp[7] << 24) & 0xFF000000L;
            ret <<= 32;
            ret |= tmp[0] & 0x000000FFL;
            ret |= (tmp[1] << 8) & 0x0000FF00L;
            ret |= (tmp[2] << 16) & 0x00FF0000L;
            ret |= (tmp[3] << 24) & 0xFF000000L;
            return ret;
        }

        /// <summary>
        /// Read an unsigned 64-bit little-endian integer from the stream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ulong ReadUInt64LE(this Stream s) => unchecked((ulong)s.ReadInt64LE());


        /// <summary>
        /// Read an unsigned 16-bit little-endian integer from the stream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ushort ReadUInt16LE(this Stream s) => unchecked((ushort)s.ReadInt16LE());

        /// <summary>
        /// Read a signed 16-bit little-endian integer from the stream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short ReadInt16LE(this Stream s)
        {
            int ret;
            byte[] tmp = new byte[2];
            s.Read(tmp, 0, 2);
            ret = tmp[0] & 0x00FF;
            ret |= (tmp[1] << 8) & 0xFF00;
            return (short)ret;
        }

        /// <summary>
        /// Read a signed 8-bit integer from the stream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static sbyte ReadInt8(this Stream s) => unchecked((sbyte)s.ReadUInt8());

        /// <summary>
        /// Read an unsigned 8-bit integer from the stream.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ReadUInt8(this Stream s)
        {
            byte ret;
            byte[] tmp = new byte[1];
            s.Read(tmp, 0, 1);
            ret = tmp[0];
            return ret;
        }


    }

    public static class ImageExtensions
    {
        static readonly Byte[] _Data;
        static readonly int _Width;
        static readonly int _Height;
        public static void CopyBlockTo(int x, int y, Byte[] block, out int mask)
        {
            mask = 0;

            int targetPixelIdx = 0;

            for (int py = 0; py < 4; ++py)
            {
                for (int px = 0; px < 4; ++px)
                {
                    // get the source pixel in the image
                    int sx = x + px;
                    int sy = y + py;

                    // enable if we're in the image
                    if (sx < _Width && sy < _Height)
                    {
                        // copy the rgba value
                        int sourcePixelIdx = 4 * (_Width * sy + sx);

                        for (int i = 0; i < 4; ++i) block[targetPixelIdx++] = _Data[sourcePixelIdx++];

                        // enable this pixel
                        mask |= (1 << (4 * py + px));
                    }
                    else
                    {
                        // skip this pixel as its outside the image
                        targetPixelIdx += 4;
                    }
                }
            }
        }
        public static byte[] ToByteArray(this System.Drawing.Image image, ImageFormat format)
        {
            if (image == null)
            {
                return new byte[1];
            }
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }

    static class ConstantsExtensions
    {
        public static CompressionOptions FixFlags(this CompressionOptions flags)
        {
            // grab the flag bits            
            var fit = flags & (CompressionOptions.ColourIterativeClusterFit | CompressionOptions.ColourClusterFit | CompressionOptions.ColourRangeFit | CompressionOptions.ColourClusterFitAlt);
            var metric = flags & (CompressionOptions.ColourMetricPerceptual | CompressionOptions.ColourMetricUniform);
            var extra = flags & (CompressionOptions.WeightColourByAlpha | CompressionOptions.UseParallelProcessing);

            // set defaults            
            if (fit == 0) fit = CompressionOptions.ColourClusterFit;
            if (metric == 0) metric = CompressionOptions.ColourMetricPerceptual;

            // done
            return fit | metric | extra;
        }
    }

    public static class HexBinTemp
    {

        public static int DivideByRoundUp(this int value, int divisor)
        {
            return (value + divisor - 1) / divisor;
        }

        public static T[][] DeInterleave<T>(this T[] input, int interleaveSize, int outputCount, int outputSize = -1)
        {
            if (input.Length % outputCount != 0)
                throw new ArgumentOutOfRangeException(nameof(outputCount), outputCount,
                    $"The input array length ({input.Length}) must be divisible by the number of outputs.");

            int inputSize = input.Length / outputCount;
            if (outputSize == -1)
                outputSize = inputSize;

            int inBlockCount = inputSize.DivideByRoundUp(interleaveSize);
            int outBlockCount = outputSize.DivideByRoundUp(interleaveSize);
            int lastInputInterleaveSize = inputSize - (inBlockCount - 1) * interleaveSize;
            int lastOutputInterleaveSize = outputSize - (outBlockCount - 1) * interleaveSize;
            int blocksToCopy = Math.Min(inBlockCount, outBlockCount);

            var outputs = new T[outputCount][];
            for (int i = 0; i < outputCount; i++)
            {
                outputs[i] = new T[outputSize];
            }

            for (int b = 0; b < blocksToCopy; b++)
            {
                int currentInputInterleaveSize = b == inBlockCount - 1 ? lastInputInterleaveSize : interleaveSize;
                int currentOutputInterleaveSize = b == outBlockCount - 1 ? lastOutputInterleaveSize : interleaveSize;
                int bytesToCopy = Math.Min(currentInputInterleaveSize, currentOutputInterleaveSize);

                for (int o = 0; o < outputCount; o++)
                {
                    Array.Copy(input, interleaveSize * b * outputCount + currentInputInterleaveSize * o, outputs[o],
                        interleaveSize * b, bytesToCopy);
                }
            }

            return outputs;
        }

        public static byte[][] DeInterleave(this Stream input, int length, int interleaveSize, int outputCount, int outputSize = -1)
        {
            if (input.CanSeek)
            {
                long remainingLength = input.Length - input.Position;
                if (remainingLength < length)
                {
                    throw new ArgumentOutOfRangeException(nameof(length), length,
                        "Specified length is greater than the number of bytes remaining in the Stream");
                }
            }

            if (length % outputCount != 0)
                throw new ArgumentOutOfRangeException(nameof(outputCount), outputCount,
                    $"The input length ({length}) must be divisible by the number of outputs.");

            int inputSize = length / outputCount;
            if (outputSize == -1)
                outputSize = inputSize;

            int inBlockCount = inputSize.DivideByRoundUp(interleaveSize);
            int outBlockCount = outputSize.DivideByRoundUp(interleaveSize);
            int lastInputInterleaveSize = inputSize - (inBlockCount - 1) * interleaveSize;
            int lastOutputInterleaveSize = outputSize - (outBlockCount - 1) * interleaveSize;
            int blocksToCopy = Math.Min(inBlockCount, outBlockCount);

            var outputs = new byte[outputCount][];
            for (int i = 0; i < outputCount; i++)
            {
                outputs[i] = new byte[outputSize];
            }

            for (int b = 0; b < blocksToCopy; b++)
            {
                int currentInputInterleaveSize = b == inBlockCount - 1 ? lastInputInterleaveSize : interleaveSize;
                int currentOutputInterleaveSize = b == outBlockCount - 1 ? lastOutputInterleaveSize : interleaveSize;
                int bytesToCopy = Math.Min(currentInputInterleaveSize, currentOutputInterleaveSize);

                for (int o = 0; o < outputCount; o++)
                {
                    input.Read(outputs[o], interleaveSize * b, bytesToCopy);
                    if (bytesToCopy < currentInputInterleaveSize)
                    {
                        input.Position += currentInputInterleaveSize - bytesToCopy;
                    }
                }
            }

            return outputs;
        }

        public static short[][] InterleavedByteToShort(this byte[] input, int outputCount)
        {
            int itemCount = input.Length / 2 / outputCount;
            var output = new short[outputCount][];
            for (int i = 0; i < outputCount; i++)
            {
                output[i] = new short[itemCount];
            }

            for (int i = 0; i < itemCount; i++)
            {
                for (int o = 0; o < outputCount; o++)
                {
                    int offset = (i * outputCount + o) * 2;
                    output[o][i] = (short)(input[offset] | (input[offset + 1] << 8));
                }
            }

            return output;
        }

        public static byte[] ShortToInterleavedByte(this short[][] input)
        {
            int inputCount = input.Length;
            int length = input[0].Length;
            var output = new byte[inputCount * length * 2];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < inputCount; j++)
                {
                    int offset = (i * inputCount + j) * 2;
                    output[offset] = (byte)input[j][i];
                    output[offset + 1] = (byte)(input[j][i] >> 8);
                }
            }

            return output;
        }

        public static void Interleave(this byte[][] inputs, Stream output, int interleaveSize, int outputSize = -1)
        {
            int inputSize = inputs[0].Length;
            if (outputSize == -1)
                outputSize = inputSize;

            if (inputs.Any(x => x.Length != inputSize))
                throw new ArgumentOutOfRangeException(nameof(inputs), "Inputs must be of equal length");

            int inputCount = inputs.Length;
            int inBlockCount = inputSize.DivideByRoundUp(interleaveSize);
            int outBlockCount = outputSize.DivideByRoundUp(interleaveSize);
            int lastInputInterleaveSize = inputSize - (inBlockCount - 1) * interleaveSize;
            int lastOutputInterleaveSize = outputSize - (outBlockCount - 1) * interleaveSize;
            int blocksToCopy = Math.Min(inBlockCount, outBlockCount);

            for (int b = 0; b < blocksToCopy; b++)
            {
                int currentInputInterleaveSize = b == inBlockCount - 1 ? lastInputInterleaveSize : interleaveSize;
                int currentOutputInterleaveSize = b == outBlockCount - 1 ? lastOutputInterleaveSize : interleaveSize;
                int bytesToCopy = Math.Min(currentInputInterleaveSize, currentOutputInterleaveSize);

                for (int i = 0; i < inputCount; i++)
                {
                    output.Write(inputs[i], interleaveSize * b, bytesToCopy);
                    if (bytesToCopy < currentOutputInterleaveSize)
                    {
                        output.Position += currentOutputInterleaveSize - bytesToCopy;
                    }
                }
            }

            //Simply setting the position past the end of the stream doesn't expand the stream,
            //so we do that manually if necessary
            output.SetLength(Math.Max(outputSize * inputCount, output.Length));
        }
    }

}
