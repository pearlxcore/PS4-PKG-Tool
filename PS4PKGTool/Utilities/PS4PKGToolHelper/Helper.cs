using Microsoft.Win32;
using Newtonsoft.Json;
using PS4_Tools.LibOrbis.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TRPViewer;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Net;
using System.ComponentModel;
using System.Windows.Forms;
using PS4_Tools.LibOrbis.PKG;
using Microsoft.VisualBasic;
using System.Security.Principal;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using PS4PKGTool.Util;
using System.Reflection;
using System.Net.Http;
using DarkUI.Forms;
using DarkUI.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PS4PKGTool.Utilities.PS4PKGToolHelper
{
    public class Helper
    {
        public static bool FirstLaunch { get; set; } = true;
        
        public static bool FinalizePkgProcess = true;

        public static string PS4PKGToolTempDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\PS4PKGToolTemp\";
        public static string OrbisPubCmd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\PS4PKGToolTemp\orbis-pub-cmd.exe";
        public static string Ps5BcJsonFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\PS4PKGToolTemp\ps5bc.json";
        public static string PS4PKGToolLogFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\PS4PKGToolTemp\PS4PKGToolLog.txt";

        public static string RoundBytes(long num)
        {
            if (num < 1024L)
                return Conversions.ToString(num) + " bytes";
            if (num < 1048576.0)
                return Conversions.ToString(Math.Round(num / 1024.0, 2)) + " KB";
            if (num < 1073741824.0)
                return Conversions.ToString(Math.Round(num / 1048576.0, 2)) + " MB";
            if (num < 1099511627776.0)
                return Conversions.ToString(Math.Round(num / 1073741824.0, 2)) + " GB";
            if (num < 1.12589990684262E+15)
                return Conversions.ToString(Math.Round(num / 1099511627776.0, 2)) + " TB";
            return Conversions.ToString(num);
        }

        public static void ExtractResources()
        {
           
        }

        public static  class Backport
        {
            public class BackportInfo
            {
                public string FilePath { get; set; }
                public string Backported { get; set; }
            }

            public static string BackportInfoFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\PS4PKGToolTemp\backport.json";
            private static List<BackportInfo> pkgFileList = new List<BackportInfo>();

            public static string CheckPKGBackported(string pkgFile)
            {
                // Load data from JSON file into pkgFileList
                string json = File.ReadAllText(BackportInfoFile);
                pkgFileList = JsonConvert.DeserializeObject<List<BackportInfo>>(json);

                BackportInfo matchingFile = pkgFileList.FirstOrDefault(file => file.FilePath.Equals(pkgFile, StringComparison.OrdinalIgnoreCase));

                if (matchingFile != null)
                {
                    return matchingFile.Backported;
                }

                return null;
            }

            public static void SaveData(DarkDataGridView dataGridView)
            {
                List<BackportInfo> updatedPkgFileList = new List<BackportInfo>();

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    string filePath = row.Cells[12].Value.ToString() + @"\" + row.Cells[0].Value.ToString(); 
                    string backported = (row.Cells[13].Value?.ToString() == "No") ? "No": row.Cells[13].Value?.ToString(); 

                    updatedPkgFileList.Add(new BackportInfo
                    {
                        FilePath = filePath,
                        Backported = backported
                    }) ;
                }

                string json = JsonConvert.SerializeObject(updatedPkgFileList, Formatting.Indented);
                File.WriteAllText(BackportInfoFile, json);
            }
        }

        public class Bitmap
        {
            private static PictureBox pic0_ = new PictureBox();
            private static PictureBox pic1_ = new PictureBox();

            public static PictureBox pic0
            {
                get
                {
                    return pic0_;
                }
                set
                {
                    pic0_ = value;
                }
            }

            public static PictureBox pic1
            {
                get
                {
                    return pic1_;
                }
                set
                {
                    pic1_ = value;
                }
            }
            private static string FailExtractImageList_;

            public static string FailExtractImageList
            {
                get
                {
                    return FailExtractImageList_;
                }
                set
                {
                    FailExtractImageList_ = value;
                }
            }

            public static System.Drawing.Bitmap BytesToBitmap(byte[] ImgBytes)
            {
                System.Drawing.Bitmap result = null;
                if (ImgBytes != null)
                {
                    MemoryStream stream = new MemoryStream(ImgBytes);
                    result = (System.Drawing.Bitmap)Image.FromStream(stream);
                }
                return result;
            }

            public static Image BytesToImage(byte[] ImageBytes)
            {
                return Image.FromStream(new MemoryStream(ImageBytes));
            }

            public static byte[] ImageToBytes(Image Image)
            {
                MemoryStream memoryStream = new MemoryStream();
                Image.Save(memoryStream, Image.RawFormat);
                return memoryStream.GetBuffer();
            }

            public static Image GetImage(byte[] fbytes)
            {
                return BytesToImage(fbytes);
            }

            public static bool IsAdmin()
            {
                bool flag = false;
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                if (((WindowsPrincipal)Thread.CurrentPrincipal).IsInRole(WindowsBuiltInRole.Administrator))
                    flag = true;
                return flag;
            }
        }

        public class Passcode
        {
            private static string passcode_;
            public static string passcode
            {
                get { return passcode_; }
                set { passcode_ = value; }
            }
        }

        public class Update
        {
            public static WebClient client;

            private static int Part_;
            public static int Part
            {
                get { return Part; }
                set { Part_ = value; }
            }

            private static string URL_;
            public static string URL
            {
                get { return URL_; }
                set { URL_ = value; }
            }
            private static string PART_;
            public static string PART
            {
                get { return PART_; }
                set { PART_ = value; }
            }
            private static string SIZE_;
            public static string SIZE
            {
                get { return SIZE_; }
                set { SIZE_ = value; }
            }

            private static string Downloading_ = "no";
            public static string Downloading
            {
                get { return Downloading_; }
                set { Downloading_ = value; }
            }
        }

        public class Entry
        {
            public static byte[] Sha256(byte[] buffer, int offset, int length)
            {
                var sha = new SHA256Managed();
                sha.TransformFinalBlock(buffer, offset, length);
                return sha.Hash;
            }

            public static byte[] DecryptAes(byte[] key, byte[] iv, byte[] data)
            {
                var aes = new AesCryptoServiceProvider();
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.Zeros;
                aes.BlockSize = 128;

                return aes.CreateDecryptor(key, iv).TransformFinalBlock(data, 0, data.Length);
            }

            public struct PackageEntry
            {
                public uint type;
                public uint unk1;
                public uint flags1;
                public uint flags2;
                public uint offset;
                public uint size;
                public byte[] padding;

                public uint key_index;
                public bool is_encrypted;

                public byte[] ToArray()
                {
                    var ms = new MemoryStream();
                    var writer = new EndianWriter(ms, EndianType.BigEndian);

                    writer.Write(type);
                    writer.Write(unk1);
                    writer.Write(flags1);
                    writer.Write(flags2);
                    writer.Write(offset);
                    writer.Write(size);
                    writer.Write(padding);

                    writer.Close();

                    return ms.ToArray();
                }
            };

            static RSAParameters param = new RSAParameters()
            {

                /*
                        public struct RSAParameters
                        {
                            public byte[] D;
                            public byte[] DP;
                            public byte[] DQ;
                            public byte[] Exponent;
                            public byte[] InverseQ;
                            public byte[] Modulus;
                            public byte[] P;
                            public byte[] Q;
                        }
                */

                D = new byte[256] {
                0x32, 0xd9, 0x03, 0x90, 0x8f, 0xbd, 0xb0, 0x8f, 0x57, 0x2b, 0x28, 0x5e,
                0x0b, 0x8d, 0xb3, 0xea, 0x5c, 0xd1, 0x7e, 0xa8, 0x90, 0x88, 0x8c, 0xdd,
                0x6a, 0x80, 0xbb, 0xb1, 0xdf, 0xc1, 0xf7, 0x0d, 0xaa, 0x32, 0xf0, 0xb7,
                0x7c, 0xcb, 0x88, 0x80, 0x0e, 0x8b, 0x64, 0xb0, 0xbe, 0x4c, 0xd6, 0x0e,
                0x9b, 0x8c, 0x1e, 0x2a, 0x64, 0xe1, 0xf3, 0x5c, 0xd7, 0x76, 0x01, 0x41,
                0x5e, 0x93, 0x5c, 0x94, 0xfe, 0xdd, 0x46, 0x62, 0xc3, 0x1b, 0x5a, 0xe2,
                0xa0, 0xbc, 0x2d, 0xeb, 0xc3, 0x98, 0x0a, 0xa7, 0xb7, 0x85, 0x69, 0x70,
                0x68, 0x2b, 0x64, 0x4a, 0xb3, 0x1f, 0xcc, 0x7d, 0xdc, 0x7c, 0x26, 0xf4,
                0x77, 0xf6, 0x5c, 0xf2, 0xae, 0x5a, 0x44, 0x2d, 0xd3, 0xab, 0x16, 0x62,
                0x04, 0x19, 0xba, 0xfb, 0x90, 0xff, 0xe2, 0x30, 0x50, 0x89, 0x6e, 0xcb,
                0x56, 0xb2, 0xeb, 0xc0, 0x91, 0x16, 0x92, 0x5e, 0x30, 0x8e, 0xae, 0xc7,
                0x94, 0x5d, 0xfd, 0x35, 0xe1, 0x20, 0xf8, 0xad, 0x3e, 0xbc, 0x08, 0xbf,
                0xc0, 0x36, 0x74, 0x9f, 0xd5, 0xbb, 0x52, 0x08, 0xfd, 0x06, 0x66, 0xf3,
                0x7a, 0xb3, 0x04, 0xf4, 0x75, 0x29, 0x5d, 0xe9, 0x5f, 0xaa, 0x10, 0x30,
                0xb2, 0x0f, 0x5a, 0x1a, 0xc1, 0x2a, 0xb3, 0xfe, 0xcb, 0x21, 0xad, 0x80,
                0xec, 0x8f, 0x20, 0x09, 0x1c, 0xdb, 0xc5, 0x58, 0x94, 0xc2, 0x9c, 0xc6,
                0xce, 0x82, 0x65, 0x3e, 0x57, 0x90, 0xbc, 0xa9, 0x8b, 0x06, 0xb4, 0xf0,
                0x72, 0xf6, 0x77, 0xdf, 0x98, 0x64, 0xf1, 0xec, 0xfe, 0x37, 0x2d, 0xbc,
                0xae, 0x8c, 0x08, 0x81, 0x1f, 0xc3, 0xc9, 0x89, 0x1a, 0xc7, 0x42, 0x82,
                0x4b, 0x2e, 0xdc, 0x8e, 0x8d, 0x73, 0xce, 0xb1, 0xcc, 0x01, 0xd9, 0x08,
                0x70, 0x87, 0x3c, 0x44, 0x08, 0xec, 0x49, 0x8f, 0x81, 0x5a, 0xe2, 0x40,
                0xff, 0x77, 0xfc, 0x0d
            },

                DP = new byte[128] {
                0x52, 0xCC, 0x2D, 0xA0, 0x9C, 0x9E, 0x75, 0xE7, 0x28, 0xEE, 0x3D, 0xDE,
                0xE3, 0x45, 0xD1, 0x4F, 0x94, 0x1C, 0xCC, 0xC8, 0x87, 0x29, 0x45, 0x3B,
                0x8D, 0x6E, 0xAB, 0x6E, 0x2A, 0xA7, 0xC7, 0x15, 0x43, 0xA3, 0x04, 0x8F,
                0x90, 0x5F, 0xEB, 0xF3, 0x38, 0x4A, 0x77, 0xFA, 0x36, 0xB7, 0x15, 0x76,
                0xB6, 0x01, 0x1A, 0x8E, 0x25, 0x87, 0x82, 0xF1, 0x55, 0xD8, 0xC6, 0x43,
                0x2A, 0xC0, 0xE5, 0x98, 0xC9, 0x32, 0xD1, 0x94, 0x6F, 0xD9, 0x01, 0xBA,
                0x06, 0x81, 0xE0, 0x6D, 0x88, 0xF2, 0x24, 0x2A, 0x25, 0x01, 0x64, 0x5C,
                0xBF, 0xF2, 0xD9, 0x99, 0x67, 0x3E, 0xF6, 0x72, 0xEE, 0xE4, 0xE2, 0x33,
                0x5C, 0xF8, 0x00, 0x40, 0xE3, 0x2A, 0x9A, 0xF4, 0x3D, 0x22, 0x86, 0x44,
                0x3C, 0xFB, 0x0A, 0xA5, 0x7C, 0x3F, 0xCC, 0xF5, 0xF1, 0x16, 0xC4, 0xAC,
                0x88, 0xB4, 0xDE, 0x62, 0x94, 0x92, 0x6A, 0x13
            },

                DQ = new byte[128] {
                0x7C, 0x9D, 0xAD, 0x39, 0xE0, 0xD5, 0x60, 0x14, 0x94, 0x48, 0x19, 0x7F,
                0x88, 0x95, 0xD5, 0x8B, 0x80, 0xAD, 0x85, 0x8A, 0x4B, 0x77, 0x37, 0x85,
                0xD0, 0x77, 0xBB, 0xBF, 0x89, 0x71, 0x4A, 0x72, 0xCB, 0x72, 0x68, 0x38,
                0xEC, 0x02, 0xC6, 0x7D, 0xC6, 0x44, 0x06, 0x33, 0x51, 0x1C, 0xC0, 0xFF,
                0x95, 0x8F, 0x0D, 0x75, 0xDC, 0x25, 0xBB, 0x0B, 0x73, 0x91, 0xA9, 0x6D,
                0x42, 0xD8, 0x03, 0xB7, 0x68, 0xD4, 0x1E, 0x75, 0x62, 0xA3, 0x70, 0x35,
                0x79, 0x78, 0x00, 0xC8, 0xF5, 0xEF, 0x15, 0xB9, 0xFC, 0x4E, 0x47, 0x5A,
                0xC8, 0x70, 0x70, 0x5B, 0x52, 0x98, 0xC0, 0xC2, 0x58, 0x4A, 0x70, 0x96,
                0xCC, 0xB8, 0x10, 0xE1, 0x2F, 0x78, 0x8B, 0x2B, 0xA1, 0x7F, 0xF9, 0xAC,
                0xDE, 0xF0, 0xBB, 0x2B, 0xE2, 0x66, 0xE3, 0x22, 0x92, 0x31, 0x21, 0x57,
                0x92, 0xC4, 0xB8, 0xF2, 0x3E, 0x76, 0x20, 0x37
            },

                Exponent = new byte[04] {
              0x00, 0x01, 0x00, 0x01 /*, 0x5A, 0x65, 0x72, 0x30, 0x78, 0x46, 0x46, 0x5F, 0x6C, 0x69, 0x6B, 0x65, 0x5F, 0x64, 0x69, 0x6B */
            },

                InverseQ = new byte[128] {
                0x45, 0x97, 0x55, 0xD4, 0x22, 0x08, 0x5E, 0xF3, 0x5C, 0xB4, 0x05, 0x7A,
                0xFD, 0xAA, 0x42, 0x42, 0xAD, 0x9A, 0x8C, 0xA0, 0x6C, 0xBB, 0x1D, 0x68,
                0x54, 0x54, 0x6E, 0x3E, 0x32, 0xE3, 0x53, 0x73, 0x76, 0xF1, 0x3E, 0x01,
                0xEA, 0xD3, 0xCF, 0xEB, 0xEB, 0x23, 0x3E, 0xC0, 0xBE, 0xCE, 0xEC, 0x2C,
                0x89, 0x5F, 0xA8, 0x27, 0x3A, 0x4C, 0xB7, 0xE6, 0x74, 0xBC, 0x45, 0x4C,
                0x26, 0xC8, 0x25, 0xFF, 0x34, 0x63, 0x25, 0x37, 0xE1, 0x48, 0x10, 0xC1,
                0x93, 0xA6, 0xAF, 0xEB, 0xBA, 0xE3, 0xA2, 0xF1, 0x3D, 0xEF, 0x63, 0xD8,
                0xF4, 0xFD, 0xD3, 0xEE, 0xE2, 0x5D, 0xE9, 0x33, 0xCC, 0xAD, 0xBA, 0x75,
                0x5C, 0x85, 0xAF, 0xCE, 0xA9, 0x3D, 0xD1, 0xA2, 0x17, 0xF3, 0xF6, 0x98,
                0xB3, 0x50, 0x8E, 0x5E, 0xF6, 0xEB, 0x02, 0x8E, 0xA1, 0x62, 0xA7, 0xD6,
                0x2C, 0xEC, 0x91, 0xFF, 0x15, 0x40, 0xD2, 0xE3
            },


                Modulus = new byte[256] {
                0xd2, 0x12, 0xfc, 0x33, 0x5f, 0x6d, 0xdb, 0x83, 0x16, 0x09, 0x62, 0x8b,
                0x03, 0x56, 0x27, 0x37, 0x82, 0xd4, 0x77, 0x85, 0x35, 0x29, 0x39, 0x2d,
                0x52, 0x6b, 0x8c, 0x4c, 0x8c, 0xfb, 0x06, 0xc1, 0x84, 0x5b, 0xe7, 0xd4,
                0xf7, 0xbc, 0xd2, 0x4e, 0x62, 0x45, 0xcd, 0x2a, 0xbb, 0xd7, 0x77, 0x76,
                0x45, 0x36, 0x55, 0x27, 0x3f, 0xb3, 0xf5, 0xf9, 0x8e, 0xda, 0x4b, 0xef,
                0xaa, 0x59, 0xae, 0xb3, 0x9b, 0xea, 0x54, 0x98, 0xd2, 0x06, 0x32, 0x6a,
                0x58, 0x31, 0x2a, 0xe0, 0xd4, 0x4f, 0x90, 0xb5, 0x0a, 0x7d, 0xec, 0xf4,
                0x3a, 0x9c, 0x52, 0x67, 0x2d, 0x99, 0x31, 0x8e, 0x0c, 0x43, 0xe6, 0x82,
                0xfe, 0x07, 0x46, 0xe1, 0x2e, 0x50, 0xd4, 0x1f, 0x2d, 0x2f, 0x7e, 0xd9,
                0x08, 0xba, 0x06, 0xb3, 0xbf, 0x2e, 0x20, 0x3f, 0x4e, 0x3f, 0xfe, 0x44,
                0xff, 0xaa, 0x50, 0x43, 0x57, 0x91, 0x69, 0x94, 0x49, 0x15, 0x82, 0x82,
                0xe4, 0x0f, 0x4c, 0x8d, 0x9d, 0x2c, 0xc9, 0x5b, 0x1d, 0x64, 0xbf, 0x88,
                0x8b, 0xd4, 0xc5, 0x94, 0xe7, 0x65, 0x47, 0x84, 0x1e, 0xe5, 0x79, 0x10,
                0xfb, 0x98, 0x93, 0x47, 0xb9, 0x7d, 0x85, 0x12, 0xa6, 0x40, 0x98, 0x2c,
                0xf7, 0x92, 0xbc, 0x95, 0x19, 0x32, 0xed, 0xe8, 0x90, 0x56, 0x0d, 0x65,
                0xc1, 0xaa, 0x78, 0xc6, 0x2e, 0x54, 0xfd, 0x5f, 0x54, 0xa1, 0xf6, 0x7e,
                0xe5, 0xe0, 0x5f, 0x61, 0xc1, 0x20, 0xb4, 0xb9, 0xb4, 0x33, 0x08, 0x70,
                0xe4, 0xdf, 0x89, 0x56, 0xed, 0x01, 0x29, 0x46, 0x77, 0x5f, 0x8c, 0xb8,
                0xa9, 0xf5, 0x1e, 0x2e, 0xb3, 0xb9, 0xbf, 0xe0, 0x09, 0xb7, 0x8d, 0x28,
                0xd4, 0xa6, 0xc3, 0xb8, 0x1e, 0x1f, 0x07, 0xeb, 0xb4, 0x12, 0x0b, 0x95,
                0xb8, 0x85, 0x30, 0xfd, 0xdc, 0x39, 0x13, 0xd0, 0x7c, 0xdc, 0x8f, 0xed,
                0xf9, 0xc9, 0xa3, 0xc1
            },

                P = new byte[128] {
                0xF9, 0x67, 0xAD, 0x99, 0x12, 0x31, 0x0C, 0x56, 0xA2, 0x2E, 0x16, 0x1C, 0x46, 0xB3, 0x4D, 0x5B,
                0x43, 0xBE, 0x42, 0xA2, 0xF6, 0x86, 0x96, 0x80, 0x42, 0xC3, 0xC7, 0x3F, 0xC3, 0x42, 0xF5, 0x87,
                0x49, 0x33, 0x9F, 0x07, 0x5D, 0x6E, 0x2C, 0x04, 0xFD, 0xE3, 0xE1, 0xB2, 0xAE, 0x0A, 0x0C, 0xF0,
                0xC7, 0xA6, 0x1C, 0xA1, 0x63, 0x50, 0xC8, 0x09, 0x9C, 0x51, 0x24, 0x52, 0x6C, 0x5E, 0x5E, 0xBD,
                0x1E, 0x27, 0x06, 0xBB, 0xBC, 0x9E, 0x94, 0xE1, 0x35, 0xD4, 0x6D, 0xB3, 0xCB, 0x3C, 0x68, 0xDD,
                0x68, 0xB3, 0xFE, 0x6C, 0xCB, 0x8D, 0x82, 0x20, 0x76, 0x23, 0x63, 0xB7, 0xE9, 0x68, 0x10, 0x01,
                0x4E, 0xDC, 0xBA, 0x27, 0x5D, 0x01, 0xC1, 0x2D, 0x80, 0x5E, 0x2B, 0xAF, 0x82, 0x6B, 0xD8, 0x84,
                0xB6, 0x10, 0x52, 0x86, 0xA7, 0x89, 0x8E, 0xAE, 0x9A, 0xE2, 0x89, 0xC6, 0xF7, 0xD5, 0x87, 0xFB,
/*              0x5A, 0x65, 0x72, 0x30, 0x78, 0x46, 0x46, 0x5F, 0x6C, 0x69, 0x6B, 0x65, 0x5F, 0x64, 0x69, 0x6B */
            },

                Q = new byte[128] {
                0xD7, 0xA1, 0x0F, 0x9A, 0x8B, 0xF2, 0xC9, 0x11, 0x95, 0x32, 0x9A, 0x8C, 0xF0, 0xD9, 0x40, 0x47,
                0xF5, 0x68, 0xA0, 0x0D, 0xBD, 0xC1, 0xFC, 0x43, 0x2F, 0x65, 0xF9, 0xC3, 0x61, 0x0F, 0x25, 0x77,
                0x54, 0xAD, 0xD7, 0x58, 0xAC, 0x84, 0x40, 0x60, 0x8D, 0x3F, 0xF3, 0x65, 0x89, 0x75, 0xB5, 0xC6,
                0x2C, 0x51, 0x1A, 0x2F, 0x1F, 0x22, 0xE4, 0x43, 0x11, 0x54, 0xBE, 0xC9, 0xB4, 0xC7, 0xB5, 0x1B,
                0x05, 0x0B, 0xBC, 0x56, 0x9A, 0xCD, 0x4A, 0xD9, 0x73, 0x68, 0x5E, 0x5C, 0xFB, 0x92, 0xB7, 0x8B,
                0x0D, 0xFF, 0xF5, 0x07, 0xCA, 0xB4, 0xC8, 0x9B, 0x96, 0x3C, 0x07, 0x9E, 0x3E, 0x6B, 0x2A, 0x11,
                0xF2, 0x8A, 0xB1, 0x8A, 0xD7, 0x2E, 0x1B, 0xA5, 0x53, 0x24, 0x06, 0xED, 0x50, 0xB8, 0x90, 0x67,
                0xB1, 0xE2, 0x41, 0xC6, 0x92, 0x01, 0xEE, 0x10, 0xF0, 0x61, 0xBB, 0xFB, 0xB2, 0x7D, 0x4A, 0x73,
/*              0x5A, 0x65, 0x72, 0x30, 0x78, 0x46, 0x46, 0x5F, 0x6C, 0x69, 0x6B, 0x65, 0x5F, 0x64, 0x69, 0x6B */
            }
            };

            public static byte[] Decrypt(byte[] data)
            {
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    // Import the RSA key information.
                    // This needs to include private key information.
                    rsa.ImportParameters(param);
                    return rsa.Decrypt(data, false);
                }
            }
            
            public static Dictionary<string, string> EntryIdNameDictionary = new Dictionary<string, string>();
            public static Dictionary<string, string> EncryptedEntryOffsetNameDictionary = new Dictionary<string, string>();
        }

        public class TreeView
        {
            public class TreeNodeInfo
            {
                public TreeNode Node { get; set; }
                public string Path { get; set; }
            }

            public TreeView(System.Windows.Forms.ListView listView, ImageList imageList)
            {
                _listView=listView;
                _imageList=imageList;
            }

            public static TreeNode currentNode;
            public static List<TreeNode> rootNodes; // Keep track of the root nodes (Image0 and Sc0)

            private static string Nodename_;
            private readonly System.Windows.Forms.ListView _listView;
            private readonly ImageList _imageList;

            public static string Nodename
            {
                get
                {
                    return Nodename_;
                }
                set
                {
                    Nodename_ = value;
                }
            }

            private void HandleParentDirectoryNavigation()
            {
                if (currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                }
                else if (currentNode != null && !rootNodes.Contains(currentNode))
                {
                    currentNode = null;
                }
                else
                {
                    currentNode = null;
                }

                UpdateNodeFullPathAndListView();
            }

            private void HandleDirectorySelection(TreeNode selectedNode)
            {
                currentNode = selectedNode;
                UpdateNodeFullPathAndListView();
            }

            private void UpdateNodeFullPathAndListView(bool isRoot = false)
            {
                PKG.NodeFullPath = isRoot ? "" : currentNode.FullPath;
                PopulateListView(isRoot);
            }

            private ListViewItem PopulateListView(bool showRootNodes = false)
            {
                _listView.Items.Clear();
                ListViewItem listViewItem = null;
                // Add the "..." row to navigate to the parent directory
                if (currentNode != null && !showRootNodes)
                {
                    TreeNodeInfo parentItem = new TreeNodeInfo
                    {
                        Node = currentNode.Parent != null ? currentNode.Parent : null,
                        Path = "..."
                    };
                    ListViewItem parentListViewItem = new ListViewItem("...");
                    parentListViewItem.Tag = parentItem;
                    parentListViewItem.ImageIndex = 2; // Set the appropriate image index for the "parent folder" icon
                    _listView.Items.Add(parentListViewItem);
                }

                _listView.SmallImageList = _imageList;

                List<TreeNode> list;
                if (currentNode != null)
                {
                    list = currentNode.Nodes.Cast<TreeNode>().ToList();
                }
                else if (showRootNodes)
                {
                    list = rootNodes;
                }
                else
                {
                    return null; // If currentNode is null and showRootNodes is false, do not populate the ListView.
                }

                foreach (var item in list)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item.Text);
                    string dir = Path.GetDirectoryName(item.FullPath);
                    bool isDirectory = item.Nodes.Count > 0;

                    TreeNodeInfo treeNodeInfo = new TreeNodeInfo
                    {
                        Node = item,
                        Path = item.FullPath
                    };

                    listViewItem = new ListViewItem(isDirectory ? "Directory" : "File");
                    listViewItem.Text = item.Text;
                    listViewItem.SubItems.Add(isDirectory ? "Directory" : Path.GetExtension(item.Text).Replace(".", ""));
                    listViewItem.SubItems.Add(dir);
                    listViewItem.Tag = treeNodeInfo;
                    listViewItem.ImageIndex = isDirectory ? 0 : 1;
                }
                return listViewItem;
            }
        }

        public class PKG
        {
            public static BackgroundWorker ExtractPKGBackgroundWorker;

            public static bool isDeletingPkg { get; set; }
            private static string Passcode_;
            public static string Passcode
            {
                get
                {
                    return Passcode_;
                }
                set
                {
                    Passcode_ = value;
                }
            }
            
            private static string NodeFullPath_;
            public static string NodeFullPath
            {
                get
                {
                    return NodeFullPath_;
                }
                set
                {
                    NodeFullPath_ = value;
                }
            }

            public static bool CancelExtract { get; set; }

            private static int CountFailRename_;
            private static string ListFailRename_;
            public static int CountFailRename
            {
                get
                {
                    return CountFailRename_;
                }
                set
                {
                    CountFailRename_ = value;
                }
            }
            public static string ListFailRename
            {
                get
                {
                    return ListFailRename_;
                }
                set
                {
                    ListFailRename_ = value;
                }
            }

            private static int CountFailMove_;
            private static string ListFailMove_;
            public static int CountFailMove
            {
                get
                {
                    return CountFailMove_;
                }
                set
                {
                    CountFailMove_ = value;
                }
            }
            public static string ListFailMove
            {
                get
                {
                    return ListFailMove_;
                }
                set
                {
                    ListFailMove_ = value;
                }
            }

            public static bool IsPkgGamePatchAppUnknown(Unprotected_PKG ps4pkg)
            {
                if (ps4pkg.PKG_Type.ToString() != "Addon")
                {
                    return true;
                }

                return false;
            }

            private static string CurrentPKGTitle_;
            public static string CurrentPKGTitle
            {
                get
                {
                    return CurrentPKGTitle_;
                }
                set
                {
                    CurrentPKGTitle_ = value;
                }
            }
            private static string CurrentPKGType_;
            public static string CurrentPKGType
            {
                get
                {
                    return CurrentPKGType_;
                }
                set
                {
                    CurrentPKGType_ = value;
                }
            }
            private static string SelectedPKGFilename_;
            public static string SelectedPKGFilename
            {
                get { return SelectedPKGFilename_; }
                set { SelectedPKGFilename_ = value; }
            }
            private static List<PS4_Tools.PKG.Official.StoreItems> storeitems_;
            public static List<PS4_Tools.PKG.Official.StoreItems> StoreItems
            {
                get { return storeitems_; }
                set { storeitems_ = value; }
            }

            private static byte[] bufferA = new byte[16];

            private static List<string> validPS4PKG_ = new List<string>();
            private static List<string> idEntryList_ = new List<string>();
            private static List<string> nameEntryList_ = new List<string>();
            private static List<string> scannedPKG_ = new List<string>();
            private static List<string> extracted_item_ = new List<string>();
            private static int fake_;
            private static int patch_;
            private static int game_;
            private static int Addon_;
            private static int Unknown_;
            private static int App_;
            private static int Official_;
            private static int Addon_Unlocker_;
            private static int count_;
            //private static int Total_;

            //public static int totalPkg
            //{
            //    get { return Total_; }
            //    set { Total_ = value; }
            //}

            public static int unknown
            {
                get { return Unknown_; }
                set { Unknown_ = value; }
            }

            public static int app
            {
                get { return App_; }
                set { App_ = value; }
            }

            public static int patch
            {
                get { return patch_; }
                set { patch_ = value; }
            }

            public static int official
            {
                get { return Official_; }
                set { Official_ = value; }
            }

            public static int game
            {
                get { return game_; }
                set { game_ = value; }
            }

            public static int fake
            {
                get { return fake_; }
                set { fake_ = value; }
            }

            public static int pkgCount
            {
                get { return count_; }
                set { count_ = value; }
            }

            public static int addon
            {
                get { return Addon_; }
                set { Addon_ = value; }
            }

            public static int unlockerAddon
            {
                get { return Addon_Unlocker_; }
                set { Addon_Unlocker_ = value; }
            }

            public static List<string> VerifiedPs4PkgList
            {
                get { return validPS4PKG_; }
                set { validPS4PKG_ = value; }
            }
            public static List<string> EntryIdList
            {
                get { return idEntryList_; }
                set { idEntryList_ = value; }
            }
            public static List<string> EntryNameList
            {
                get { return nameEntryList_; }
                set { nameEntryList_ = value; }
            }
            public static List<string> ExtractedItemList
            {
                get { return extracted_item_; }
                set { extracted_item_ = value; }
            }


            private static byte[] PKGHeader_ = new byte[16]
            {
            0x7F, 0x43, 0x4E, 0x54, 0x83, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F,
             };
            private static byte[] PKGHeader1_ = new byte[16]
            {
            0x7F, 0x43, 0x4E, 0x54, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F,
            };
            private static byte[] PKGHeader2_ = new byte[16]
            {
            0x7F, 0x43, 0x4E, 0x54, 0x81, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F,
            };
            private static byte[] PKGHeader3_ = new byte[16]
            {
            0x7F, 0x43, 0x4E, 0x54, 0x40, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F,
            };
            private static byte[] PKGHeader4_ = new byte[16]
            {
            0x7F, 0x43, 0x4E, 0x54, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0C,
            };

            public static byte[] PkgHeader
            {
                get
                {
                    return PKGHeader_;
                }
            }

            public static byte[] PkgHeader1
            {
                get
                {
                    return PKGHeader1_;
                }
            }

            public static byte[] PkgHeader2
            {
                get
                {
                    return PKGHeader2_;
                }
            }

            public static byte[] PkgHeader3
            {
                get
                {
                    return PKGHeader3_;
                }
            }

            public static byte[] PkgHeader4
            {
                get
                {
                    return PKGHeader4_;
                }
            }

            public static byte[] GetPkgHeaderBuffer(string dump)
            {
                using (BinaryReader b = new BinaryReader(new FileStream(dump, FileMode.Open, FileAccess.Read)))
                {
                    bufferA = new byte[16];

                    b.BaseStream.Seek(0x0, SeekOrigin.Begin);
                    b.Read(bufferA, 0, 16);
                    return bufferA;
                }
            }

            public static bool CompareBytes(byte[] bA1, byte[] bA2)
            {
                int s = 0;
                for (int z = 0; z < bA1.Length; z++)
                {
                    if (bA1[z] != bA2[z])
                    {
                        s++;
                    }
                }

                if (s == 0)
                {
                    return true;
                }
                return false;
            }

        }


        public class BGM
        {
            public static System.Media.SoundPlayer At9Player = new System.Media.SoundPlayer();

            private static string passcode;
            private static bool extractAT9Done_ = false;
            private static bool isBGMPlaying_ = false;
            private static List<string> allAT9_ = new List<string>();

            public static List<string> allAT9
            {
                get { return allAT9_; }
                set { allAT9_ = value; }
            }

            public static bool extractAt9Done
            {
                get
                {
                    return extractAT9Done_;
                }
                set
                {
                    extractAT9Done_ = value;
                }
            }

            public static bool isBGMPlaying
            {
                get
                {
                    return isBGMPlaying_;
                }
                set
                {
                    isBGMPlaying_ = value;
                }
            }

            public static void ExtractBgm()
            {
                try
                {
                    string bgmPath = Path.Combine(PS4PKGToolTempDirectory, "BGM");
                    if(!Directory.Exists(bgmPath))
                        Directory.CreateDirectory(bgmPath);

                    foreach (var pkgFile in PKG.VerifiedPs4PkgList)
                    {
                        Unprotected_PKG pkg = Read_PKG(pkgFile);
                        string filteredTitle = pkg.Param.Title.Replace(":", " -");
                        string finalTitle = filteredTitle.Replace("  -", " -");
                        var fileExists = Path.Combine(bgmPath, finalTitle + ".AT9");
                        if (File.Exists(fileExists))
                            break;
                        using (var fileStream = File.OpenRead(pkgFile))
                        {
                            LibOrbisPkg.PKG.PkgReader pkgReader = new LibOrbisPkg.PKG.PkgReader(fileStream);
                            LibOrbisPkg.PKG.Pkg pkgData = pkgReader.ReadPkg();
                            var metasWithIndices = pkgData.Metas.Metas.Zip(Enumerable.Range(0, pkgData.Metas.Metas.Count), (meta, index) => new { Meta = meta, Index = index });

                            foreach (var metaWithIndex in metasWithIndices)
                            {
                                LibOrbisPkg.PKG.MetaEntry meta = metaWithIndex.Meta;
                                var index = metaWithIndex.Index;
                                var name = meta.id;

                                if (name.ToString().ToUpper().EndsWith("_AT9"))
                                {
                                    var outputPath = Path.Combine(bgmPath, name.ToString().Replace("_AT9", ".AT9").Replace("SND0", finalTitle));
                                    allAT9.Add(outputPath);

                                    using (var pkgFileStream = File.OpenRead(pkgFile))
                                    using (var outputFileStream = File.Create(outputPath))
                                    {
                                        if (index < 0 || index >= pkgData.Metas.Metas.Count)
                                        {
                                            // DarkShowInformationInformation("Error: entry number out of range");
                                        }
                                        else
                                        {
                                            var entry = new SubStream(pkgFileStream, meta.DataOffset, meta.DataSize);
                                            outputFileStream.SetLength(meta.DataSize);

                                            if (meta.Encrypted)
                                            {
                                                if (passcode == null)
                                                {
                                                    // DarkShowInformationInformation("Warning: Entry is encrypted but no passcode was provided! Saving encrypted bytes.");
                                                }
                                                else
                                                {
                                                    var entryBytes = new byte[entry.Length];
                                                    entry.Read(entryBytes, 0, entryBytes.Length);
                                                    entryBytes = LibOrbisPkg.PKG.Entry.Decrypt(entryBytes, pkgData.Header.content_id, passcode, meta);
                                                    outputFileStream.Write(entryBytes, 0, (int)meta.DataSize);
                                                    return;
                                                }
                                            }

                                            entry.CopyTo(outputFileStream);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                }

            }

            public static void PlayAt9(string pkg)
            {
                try
                {
                    Unprotected_PKG PS4_PKG = Read_PKG(pkg);

                    if (extractAt9Done)
                    {
                        if (isBGMPlaying)
                        {
                            At9Player.Stop();
                        }

                        string BGM_path = Path.Combine(PS4PKGToolTempDirectory, "BGM");
                        string at9Path = Path.Combine(BGM_path, PS4_PKG.PS4_Title + ".AT9");

                        if (File.Exists(at9Path))
                        {
                            try
                            {
                                byte[] at9Data = PS4_Tools.Media.Atrac9.LoadAt9(at9Path);
                                At9Player = new System.Media.SoundPlayer(new MemoryStream(at9Data));
                                At9Player.Play();
                                isBGMPlaying = true;
                            }
                            catch (Exception ex)
                            {
                                Logger.LogError("Error playing AT9 file: " + ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error loading package: " + ex.Message);
                }
            }
        }

        public class NodeJsHttpServer
        {
            private static string HttpServerModulePath_ = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\npm\node_modules\http-server";

            public static string HttpServerModulePath
            {
                get
                {
                    return HttpServerModulePath_;
                }
            }


            public static bool IsSoftwareInstalled(string softwareName)
            {
                var registryUninstallPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                var registryUninstallPathFor32BitOn64Bit = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

                if (Is32BitWindows())
                    return IsSoftwareInstalled(softwareName, RegistryView.Registry32, registryUninstallPath);

                var is64BitSoftwareInstalled = IsSoftwareInstalled(softwareName, RegistryView.Registry64, registryUninstallPath);
                var is32BitSoftwareInstalled = IsSoftwareInstalled(softwareName, RegistryView.Registry64, registryUninstallPathFor32BitOn64Bit);
                return is64BitSoftwareInstalled || is32BitSoftwareInstalled;
            }

            private static bool Is32BitWindows() => Environment.Is64BitOperatingSystem == false;

            private static bool IsSoftwareInstalled(string softwareName, RegistryView registryView, string installedProgrammsPath)
            {
                var uninstallKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView)
                                                      .OpenSubKey(installedProgrammsPath);

                if (uninstallKey == null)
                    return false;

                return uninstallKey.GetSubKeyNames()
                                   .Select(installedSoftwareString => uninstallKey.OpenSubKey(installedSoftwareString))
                                   .Select(installedSoftwareKey => installedSoftwareKey.GetValue("DisplayName") as string)
                                   .Any(installedSoftwareName => installedSoftwareName != null && installedSoftwareName.Contains(softwareName));
            }
        }

        public class Tool
        {
            public static bool IsRootDrive(string path)
            {
                string rootPath = Path.GetPathRoot(path);
                return !string.IsNullOrEmpty(rootPath) && rootPath == path;
            }

            public static void CreateDirectoryIfNotExists(string directory)
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
            }

            public static async Task DownloadFileFromUrlAsync(string url, string saveFilePath)
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // Save content to file
                    File.WriteAllText(saveFilePath, content);
                }
            }

            public static void OpenWebLink(string url)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $"/c start {url}",
                    CreateNoWindow = true,
                    UseShellExecute = false
                }); return;
            }
            public static bool IsAppInstalled(string softwareName)
            {
                var registryUninstallPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                var registryUninstallPathFor32BitOn64Bit = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

                if (Is32BitWindows())
                    return IsSoftwareInstalled(softwareName, RegistryView.Registry32, registryUninstallPath);

                var is64BitSoftwareInstalled = IsSoftwareInstalled(softwareName, RegistryView.Registry64, registryUninstallPath);
                var is32BitSoftwareInstalled = IsSoftwareInstalled(softwareName, RegistryView.Registry64, registryUninstallPathFor32BitOn64Bit);
                return is64BitSoftwareInstalled || is32BitSoftwareInstalled;
            }

            private static bool Is32BitWindows() => Environment.Is64BitOperatingSystem == false;

            private static bool IsSoftwareInstalled(string softwareName, RegistryView registryView, string installedProgrammsPath)
            {
                var uninstallKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView)
                                                      .OpenSubKey(installedProgrammsPath);

                if (uninstallKey == null)
                    return false;

                return uninstallKey.GetSubKeyNames()
                                   .Select(installedSoftwareString => uninstallKey.OpenSubKey(installedSoftwareString))
                                   .Select(installedSoftwareKey => installedSoftwareKey.GetValue("DisplayName") as string)
                                   .Any(installedSoftwareName => installedSoftwareName != null && installedSoftwareName.Contains(softwareName));
            }

            public static void KillNodeJS()
            {
                foreach (var process in Process.GetProcessesByName("node"))
                {
                    process.Kill();
                }
            }

            public static bool CheckForPS4Connection(string host)
            {
                try
                {
                    using (Ping myPing = new Ping())
                    {
                        PingReply reply = myPing.Send(host, 1000);
                        return reply.Status == IPStatus.Success;
                    }
                }
                catch (PingException)
                {
                    return false;
                }
            }

            public static bool CheckLocalServerConnection(string host)
            {
                try
                {
                    using (Ping myPing = new Ping())
                    {
                        PingReply reply = myPing.Send(host, 1000);

                        return reply.Status == IPStatus.Success;
                    }
                }
                catch (PingException)
                {
                    return false;
                }
            }

            public static bool CheckForInternetConnection(string url = null)
            {
                try
                {
                    Ping myPing = new Ping();

                    string host = (url != null)? url:"google.com";
                    byte[] buffer = new byte[32];
                    int timeout = 1000;
                    PingOptions pingOptions = new PingOptions();
                    PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                    else if (reply.Status == IPStatus.TimedOut)
                    {
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public class PKGSENDER
        {
            public class JSON
            {
                public class STOPTASK
                {
                    private static string status_;
                    public static string status
                    {
                        get
                        {
                            return status_;
                        }
                        set
                        {
                            status_ = value;
                        }
                    }
                }

                public class SENDPKG
                {
                    private static string status_;
                    private static string task_id_;
                    private static string title_;
                    private static string title_id_;
                    private static string error_;


                    public static string status
                    {
                        get
                        {
                            return status_;
                        }
                        set
                        {
                            status_ = value;
                        }
                    }
                    public static string task_id
                    {
                        get
                        {
                            return task_id_;
                        }
                        set
                        {
                            task_id_ = value;
                        }
                    }
                    public static string title
                    {
                        get
                        {
                            return title_;
                        }
                        set
                        {
                            title_ = value;
                        }
                    }

                    public static string title_id
                    {
                        get
                        {
                            return title_id_;
                        }
                        set
                        {
                            title_id_ = value;
                        }
                    }

                    public static string error
                    {
                        get
                        {
                            return error_;
                        }
                        set
                        {
                            error_ = value;
                        }
                    }
                }

                public class UNINTSALLAPP
                {
                    private static string status_;
                    public static string status
                    {
                        get
                        {
                            return status_;
                        }
                        set
                        {
                            status_ = value;
                        }
                    }
                }

                public class UNINTSALLPATCH
                {
                    private static string status_;
                    public static string status
                    {
                        get
                        {
                            return status_;
                        }
                        set
                        {
                            status_ = value;
                        }
                    }
                }

                public class UNINTSALLADDON
                {
                    private static string status_;
                    public static string status
                    {
                        get
                        {
                            return status_;
                        }
                        set
                        {
                            status_ = value;
                        }
                    }
                }

                public class UNINTSALLTHEME
                {
                    private static string status_;
                    public static string status
                    {
                        get
                        {
                            return status_;
                        }
                        set
                        {
                            status_ = value;
                        }
                    }
                }

                public class CHECKAPPEXISTS
                {

                    private static string status_;
                    private static string exists_;
                    private static bool baseAppExist_;

                    public static bool baseAppExist
                    {
                        get
                        {
                            return baseAppExist_;
                        }
                        set
                        {
                            baseAppExist_ = value;
                        }
                    }

                    public static string status
                    {
                        get
                        {
                            return status_;
                        }
                        set
                        {
                            status_ = value;
                        }
                    }

                    public static string exists
                    {
                        get
                        {
                            return exists_;
                        }
                        set
                        {
                            exists_ = value;
                        }
                    }
                }

                public class MONITORTASK
                {
                    private static string packageFilesizeTotal_;
                    private static string packageTransferredTotal_;
                    private static string TimeRemainingTotal_;
                    private static int packagePreparingTotal_;
                    private static string status_;

                    public static string packageFilesizeTotal
                    {
                        get
                        {
                            return packageFilesizeTotal_;
                        }
                        set
                        {
                            packageFilesizeTotal_ = value;
                        }
                    }

                    public static string packageTransferredTotal
                    {
                        get
                        {
                            return packageTransferredTotal_;
                        }
                        set
                        {
                            packageTransferredTotal_ = value;
                        }
                    }

                    public static string TimeRemainingTotal
                    {
                        get
                        {
                            return TimeRemainingTotal_;
                        }
                        set
                        {
                            TimeRemainingTotal_ = value;
                        }
                    }
                    public static int packagePreparingTotal
                    {
                        get
                        {
                            return packagePreparingTotal_;
                        }
                        set
                        {
                            packagePreparingTotal_ = value;
                        }
                    }

                    public static string status
                    {
                        get
                        {
                            return status_;
                        }
                        set
                        {
                            status_ = value;
                        }
                    }
                }


            }
            public static BackgroundWorker MonitorPkgSenderTaskBackgroundWorker;

            private static bool PKGSenderisStopped_ { get; set; }

            private static bool taskmonitoriscancelling_ { get; set; }
            private static bool isPreparing_ { get; set; }
            private static bool PKGSenderisDone_ { get; set; }


            public static bool taskMonitorIsCancelling
            {
                get
                {
                    return taskmonitoriscancelling_;
                }
                set
                {
                    taskmonitoriscancelling_ = value;
                }
            }

            public static bool isPreparing
            {
                get
                {
                    return isPreparing_;
                }
                set
                {
                    isPreparing_ = value;
                }
            }

            public static bool pkgSendDone
            {
                get
                {
                    return PKGSenderisDone_;
                }
                set
                {
                    PKGSenderisDone_ = value;
                }
            }

            public static bool pkgSendStopped
            {
                get
                {
                    return PKGSenderisStopped_;
                }
                set
                {
                    PKGSenderisStopped_ = value;
                }
            }

            public static string CheckRequirement()
            {
                if (appSettings_.NodeJsInstalled == false || appSettings_.HttpServerInstalled == false)
                    return "Node.js or https server module is not installed.";

                StackTrace stackTrace = new StackTrace();
                if (stackTrace.GetFrame(1).GetMethod().Name == "SendPKG") // run this checks only if this method called from SendPKG()
                {
                    //check if pkg is splitted _0 (not supoorted atm)
                    if (PKG.SelectedPKGFilename.Contains("_0.pkg"))
                        return "Splitted PKG update is not supported at this moment.";
                }

                //check if curl.exe exists
                if (!File.Exists(PS4PKGToolTempDirectory + @"curl.exe"))
                    return "Missing curl.exe in PS4PKGToolTemp";


                //return if server and ps4 is set up
                if (appSettings_.Ps4Ip == string.Empty || appSettings_.LocalServerIp == string.Empty)
                    return "PS4 IP address or Server IP address has not been set. Set the IP address in Settings.";


                //check if ps4 ip valid
                if (Tool.CheckForPS4Connection(appSettings_.Ps4Ip) != true)
                    return "PS4 not detected. Make sure PS4 is discoverable and check if the IP is correct.";

                //checked if server ip valid
                if (Tool.CheckLocalServerConnection(appSettings_.LocalServerIp) != true)
                    return "Server IP not valid. Update the new server IP in program settings.";

                return "OK";
            }

            public static dynamic CheckIfPkgInstalled(Param_SFO.PARAM_SFO psfo)
            {
                dynamic json = null;

                try
                {

                    Process checkapp = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = PS4PKGToolTempDirectory + @"curl.exe",
                            Arguments = "curl --data {\"\"\"title_id\"\"\":\"\"\"" + psfo.TITLEID + "\"\"\"} http://" + appSettings_.Ps4Ip + ":12800/api/is_exists",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    checkapp.Start();
                    checkapp.WaitForExit(7000); // 2 seconds timeout
                    while (!checkapp.StandardOutput.EndOfStream)
                    {
                        string line = checkapp.StandardOutput.ReadLine();
                        if (line != null)
                        {
                            json = JsonConvert.DeserializeObject(line);
                        }

                    }
                }
                catch { }


                return json;
            }

            public static dynamic StopTask()
            {
                dynamic json = null;

                try
                {
                    taskMonitorIsCancelling = true;
                    Process stopTask = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = PS4PKGToolTempDirectory + @"curl.exe",
                            Arguments = "curl -v http://" + appSettings_.Ps4Ip + ":12800/api/stop_task --data {\"\"\"task_id\"\"\":" + JSON.SENDPKG.task_id + "}",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    stopTask.Start();
                    stopTask.WaitForExit(7000); // 2 seconds timeout
                    while (!stopTask.StandardOutput.EndOfStream)
                    {
                        string line = stopTask.StandardOutput.ReadLine();
                        if (line != string.Empty)
                        {
                            json = JsonConvert.DeserializeObject(line);

                        }
                    }
                }
                catch
                {

                }


                return json;
            }

            public static dynamic UninstallAddonTheme(Param_SFO.PARAM_SFO psfo)
            {
                dynamic json = null;
                StackTrace stackTrace = new StackTrace();

                try
                {
                    Process uninstallappp = new Process();
                    uninstallappp.StartInfo.FileName = PS4PKGToolTempDirectory + @"curl.exe";
                    if (stackTrace.GetFrame(1).GetMethod().Name == "uninstallAddonPkgFromPs4")
                    {
                        uninstallappp.StartInfo.Arguments = "curl -v http://" + appSettings_.Ps4Ip + ":12800/api/uninstall_ac --data {\"\"\"content_id\"\"\":\"\"\"" + psfo.ContentID + "\"\"\"}";
                    }
                    else
                    {
                        uninstallappp.StartInfo.Arguments = "curl -v http://" + appSettings_.Ps4Ip + ":12800/api/uninstall_theme --data {\"\"\"content_id\"\"\":\"\"\"" + psfo.ContentID + "\"\"\"}";
                    }
                    uninstallappp.StartInfo.UseShellExecute = false;
                    uninstallappp.StartInfo.RedirectStandardOutput = true;
                    uninstallappp.StartInfo.CreateNoWindow = true;

                    uninstallappp.Start();
                    uninstallappp.WaitForExit(); // cant set timeout, uninstall time is vary

                    while (!uninstallappp.StandardOutput.EndOfStream)
                    {
                        string line = uninstallappp.StandardOutput.ReadLine();
                        if (line != string.Empty)
                        {
                            json = JsonConvert.DeserializeObject(line);
                        }
                    }
                }
                catch { }

                return json;
            }

            public static dynamic UninstallPatch(Param_SFO.PARAM_SFO psfo)
            {
                dynamic json = null;

                try
                {
                    Process uninstallappp = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = PS4PKGToolTempDirectory + @"curl.exe",
                            Arguments = "curl -v http://" + appSettings_.Ps4Ip + ":12800/api/uninstall_patch --data {\"\"\"title_id\"\"\":\"\"\"" + psfo.TITLEID + "\"\"\"}",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    uninstallappp.Start();
                    uninstallappp.WaitForExit(); // cant set timeout, uninstall time is vary

                    while (!uninstallappp.StandardOutput.EndOfStream)
                    {
                        string line = uninstallappp.StandardOutput.ReadLine();
                        if (line != string.Empty)
                        {
                            json = JsonConvert.DeserializeObject(line);

                        }
                    }
                }
                catch { }

                return json;
            }

            public static dynamic GetTaskProgress()
            {
                dynamic json = null;

                try
                {
                    Process taskProgress = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = PS4PKGToolTempDirectory + @"curl.exe",
                            Arguments = "curl -v http://" + appSettings_.Ps4Ip + ":12800/api/get_task_progress --data {\"\"\"task_id\"\"\":" + JSON.SENDPKG.task_id + "}",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    taskProgress.Start();
                    taskProgress.WaitForExit();
                    while (!taskProgress.StandardOutput.EndOfStream)
                    {
                        string line = taskProgress.StandardOutput.ReadLine();
                        if (line != string.Empty)
                        {
                            json = JsonConvert.DeserializeObject(line);

                        }
                    }
                }
                catch { }

                return json;
            }

            public static void RunServer(string directory)
            {
                Process server = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden; startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C " + "http-server \"" + directory + "\"";
                server.StartInfo = startInfo;
                server.Start();
            }

            public static dynamic SendPKG(string tempFilename)
            {
                dynamic json = null;
                Process sendPKG = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = PS4PKGToolTempDirectory + @"curl.exe",
                        Arguments = "curl -v http://" + appSettings_.Ps4Ip + ":12800/api/install --data {\"\"\"type\"\"\":\"\"\"direct\"\"\",\"\"\"packages\"\"\":[\"\"\"http://" + appSettings_.LocalServerIp + ":8080/" + tempFilename + "\"\"\"]}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                sendPKG.Start();
                sendPKG.WaitForExit(7000); // 2 seconds timeout

                while (!sendPKG.StandardOutput.EndOfStream)
                {
                    string text = sendPKG.StandardOutput.ReadLine();
                    if (text != string.Empty)
                    {
                        json = JsonConvert.DeserializeObject(text);
                    }
                }

                return json;
            }

            public static dynamic UninstallGame(Param_SFO.PARAM_SFO psfo)
            {
                dynamic json = null;
                try
                {
                    Process uninstallappp = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = PS4PKGToolTempDirectory + @"curl.exe",
                            Arguments = "curl -v http://" + appSettings_.Ps4Ip + ":12800/api/uninstall_game --data {\"\"\"title_id\"\"\":\"\"\"" + psfo.TitleID + "\"\"\"}",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    uninstallappp.Start();

                    while (!uninstallappp.StandardOutput.EndOfStream)
                    {
                        string line = uninstallappp.StandardOutput.ReadLine();
                        if (line != string.Empty)
                        {
                            json = JsonConvert.DeserializeObject(line);

                        }
                    }
                }
                catch { }

                return json;

            }


        }

        public class Trophy
        {
            public static TRPReader trophy;
            private static List<string> idEntryList_ = new List<string>();
            private static List<string> nameEntryList_ = new List<string>();
            private static List<Image> imageToExtract_ = new List<Image>();
            private static List<string> NameToExtract_ = new List<string>();
            public static string TrophyTempFolder = PS4PKGToolTempDirectory + @"TrophyFile\";

            private static string outPath_;
            public static string outPath
            {
                get { return outPath_; }
                set { outPath_ = value; }
            }
            public static List<Image> ImageToExtractList
            {
                get { return imageToExtract_; }
                set { imageToExtract_ = value; }
            }

            public static List<string> TrophyFilenameToExtractList
            {
                get { return NameToExtract_; }
                set { NameToExtract_ = value; }
            }

            public static List<string> idEntryList
            {
                get { return idEntryList_; }
                set { idEntryList_ = value; }
            }

            public static List<string> nameEntryList
            {
                get { return nameEntryList_; }
                set { nameEntryList_ = value; }
            }

            public static System.Drawing.Bitmap ResizeImage(Image image, int width, int height)
            {
                var destRect = new Rectangle(0, 0, width, height);
                var destImage = new System.Drawing.Bitmap(width, height);

                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (var graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                return destImage;
            }
        }
    }
}
