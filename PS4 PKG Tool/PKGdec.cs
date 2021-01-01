using DarkUI.Forms;
using PS4_PKG_Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4_PKG_Tool
{
    public partial class pkgdec : Form
    {
        static OpenFileDialog ofd = new OpenFileDialog();

        public pkgdec()
        {
            InitializeComponent();


        }

        public string filenames { get; set; }

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

            D = new Byte[256] {
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
        string itemIndex = "";

        private byte[] Decrypt(byte[] data)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                // Import the RSA key information.
                // This needs to include private key information.
                rsa.ImportParameters(param);
                return rsa.Decrypt(data, false);
            }
        }

        private byte[] Encrypt(byte[] data)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(param);
                return rsa.Encrypt(data, false);
            }
        }

        private struct PackageEntry
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

        private static byte[] DecryptAes(byte[] key, byte[] iv, byte[] data)
        {
            var aes = new AesCryptoServiceProvider();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.Zeros;
            aes.BlockSize = 128;

            return aes.CreateDecryptor(key, iv).TransformFinalBlock(data, 0, data.Length);
        }

        private static byte[] Sha256(byte[] buffer, int offset, int length)
        {
            var sha = new SHA256Managed();
            sha.TransformFinalBlock(buffer, offset, length);
            return sha.Hash;
        }

        public static BigInteger FromBigEndian(byte[] p)
        {
            Array.Reverse(p);
            if (p[p.Length - 1] > 127)
            {
                Array.Resize(ref p, p.Length + 1);
                p[p.Length - 1] = 0;
            }
            return new BigInteger(p);
        }

        public static BigInteger ExtendedEuclidGcd(BigInteger a, BigInteger b,
            out BigInteger lastx, out BigInteger lasty)
        {
            var x = BigInteger.Zero;
            lastx = BigInteger.One;

            var y = BigInteger.One;
            lasty = BigInteger.Zero;

            while (!b.IsZero)
            {
                BigInteger remainder;
                BigInteger q = BigInteger.DivRem(a, b, out remainder);

                a = b;
                b = remainder;
                var t = x;
                x = lastx - q * x;
                lastx = t;
                t = y;
                y = lasty - q * y;
                lasty = t;
            }

            return a;
        }

        public static BigInteger ModularInverse(BigInteger a, BigInteger n)
        {
            BigInteger d, x, y;
            d = ExtendedEuclidGcd(a, n, out x, out y);

            if (d.IsOne)
            {
                // Always return the least positive value
                return (x + n) % n;
            }
            else
            {
                throw new ArgumentException("the arguments must be relatively prime, i.e. their gcd must be 1");
            }
        }

        static byte[] ToByteArrayBE(BigInteger b)
        {
            var x = b.ToByteArray(); // x is little-endian
            Array.Reverse(x);        // now it is big-endian

            if (x[0] == 0)
            {
                var newarray = new byte[x.Length - 1];
                Array.Copy(x, 1, newarray, 0, newarray.Length);
                return newarray;
            }
            else
            {
                return x;
            }
        }


        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //load pkg file
                itemIndex = "";
                var IO = new EndianIO(filenames, EndianType.BigEndian, true);
                textBox1.Text = " ";
                textBox1.AppendText("\r\n\r\n" + System.IO.Path.GetFileName(filenames));
                long file_length = IO.Length;
                textBox1.AppendText("\r\nfile size:  " + file_length + " [0x" + file_length.ToString("X") + "]");
                if (file_length < 0x1C)
                {
                    textBox1.AppendText("\r\n\r\nwarning, file truncated\r\nfile closed");
                    IO.Close();
                    return;
                }

                //set output path for extracted files
                string path2pkg = Path.GetDirectoryName(filenames);
                string fullpkgpath = Path.GetFullPath(filenames);
                string pkgbasename = Path.GetFileNameWithoutExtension(filenames);
                string pkgfilename = Path.GetFileName(filenames);
                string outputpath = fbd.SelectedPath; // Path.Combine(path2pkg, pkgbasename);
                // textBox1.AppendText("\r\n\r\npath2pkg:   " + path2pkg);     //  C:\Downloads\ps4packages\
                // textBox1.AppendText("\r\nfullpkgpath:   " + fullpkgpath);   //  C:\Downloads\ps4packages\Up1018...V0100.pkg
                // textBox1.AppendText("\r\npkgbasename:   " + pkgbasename);   //  Up1018...V0100
                // textBox1.AppendText("\r\npkgfilename:   " + pkgfilename);   //  Up1018...V0100.pkg
                //textBox1.AppendText("\r\n\r\noutput path:\r\n" + outputpath); //  C:\Downloads\ps4packages\Up1018...V0100 
                if (!Directory.Exists(outputpath))
                {
                    textBox1.AppendText("\r\n\r\ncreate output folder:\r\n" + outputpath);
                    Directory.CreateDirectory(outputpath);
                }
                else
                {
                    textBox1.AppendText("\r\n\r\noutput path:\r\n" + outputpath); //  C:\Downloads\ps4packages\Up1018...V0100
                }

                //read and decrypt part 1 of key seed
                if (file_length < (0x2400 + 0x100))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                IO.SeekTo(0x2400);
                byte[] data = Decrypt(IO.In.ReadBytes(256));
                textBox1.AppendText("\r\n\r\ndecrypting key seed at 0x2400 \r\ndecrypted length 0x" + data.Length.ToString("X"));
                for (int j = 0; j < data.Length; j++)
                {
                    if ((j % 0x10) == 0) textBox1.AppendText("\r\n");
                    textBox1.Text = textBox1.Text + data[j].ToString("X2");
                }

                //read file entry table
                uint entry_count = IO.In.SeekNReadUInt32(0x10);
                textBox1.AppendText("\r\n\r\nentry count:   " + entry_count);
                uint file_table_offset = IO.In.SeekNReadUInt32(0x18);
                uint padded_size;

                uint strtab_count = 0;
                uint strtab_offset = 0;
                uint strtab_size = 0;

                if (file_length < (file_table_offset + (0x20 * entry_count)))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                IO.SeekTo(file_table_offset);
                PackageEntry[] entry = new PackageEntry[entry_count];
                for (int i = 0; i < entry_count; i++)
                {
                    entry[i].type = IO.In.ReadUInt32();
                    entry[i].unk1 = IO.In.ReadUInt32();
                    entry[i].flags1 = IO.In.ReadUInt32();
                    entry[i].flags2 = IO.In.ReadUInt32();
                    entry[i].offset = IO.In.ReadUInt32();
                    entry[i].size = IO.In.ReadUInt32();
                    entry[i].padding = IO.In.ReadBytes(8);

                    //set key index, encryption flag, string table properties
                    entry[i].key_index = ((entry[i].flags2 & 0xF000) >> 12);
                    entry[i].is_encrypted = ((entry[i].flags1 & 0x80000000) != 0) ? true : false;
                    if (entry[i].unk1 != 0) strtab_count++;
                    if (entry[i].type == 0x200)
                    {
                        strtab_offset = entry[i].offset;
                        strtab_size = entry[i].size;
                    }
                }

                //read strtab
                if (file_length < (strtab_offset + strtab_size))
                {
                    textBox1.AppendText("\r\n\r\nwarning, string table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                string[] entry_name = new string[entry_count];
                if (strtab_count > 0)
                {
                    textBox1.AppendText("\r\n\r\nstrtab count:   " + strtab_count);
                    IO.SeekTo(strtab_offset);
                    byte[] string_table = IO.In.ReadBytes(strtab_size);
                    for (int i = 0; i < entry_count - 1; i++)
                    {
                        if (entry[i].unk1 != 0x00)
                        { //has strtab entry
                            entry_name[i] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[i].unk1), (Convert.ToInt32(entry[i + 1].unk1) - 1) - Convert.ToInt32(entry[i].unk1));
                            textBox1.AppendText("\r\n" + entry_name[i]);
                        }
                        else
                        {
                            entry_name[i] = "";
                        }
                    }
                    if (entry[entry_count - 1].unk1 != 0x00)
                    {
                        entry_name[entry_count - 1] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[entry_count - 1].unk1), (Convert.ToInt32(strtab_size) - 1) - Convert.ToInt32(entry[entry_count - 1].unk1));
                        textBox1.AppendText("\r\n" + entry_name[entry_count - 1]);
                    }
                    else
                    {
                        entry_name[entry_count - 1] = "";
                    }
                }
                else
                {
                    for (int i = 0; i < entry_count; i++) entry_name[i] = "";
                }

                textBox1.AppendText("\r\n\r\nFiles Listed by Number:");
                for (int i = 0; i < entry_count; i++)
                {
                    string savepath;
                    string savename;
                    string extrasavepath;

                    if (file_length < (entry[i].offset + entry[i].size))
                    {
                        textBox1.AppendText("\r\n\r\nwarning, file " + i + " out of range\r\nfile closed");
                        IO.Close();
                        return;
                    }

                    if (entry[i].is_encrypted != false)
                    {
                        //print file attributes
                        textBox1.AppendText("\r\n");
                        textBox1.AppendText("\r\n" + i.ToString("D"));
                        if (entry_name[i] != "") textBox1.AppendText("\r\n" + entry_name[i]);
                        textBox1.AppendText("\r\nType:       " + entry[i].type.ToString("X8"));
                        textBox1.AppendText("    Strtab Index:  " + entry[i].unk1.ToString("X8"));
                        textBox1.AppendText("\r\nFlags1:     " + entry[i].flags1.ToString("X8"));
                        textBox1.AppendText("    Flags2:        " + entry[i].flags2.ToString("X8"));
                        textBox1.AppendText("\r\nOffset:     " + entry[i].offset.ToString("X8"));
                        textBox1.AppendText("    Size:          " + entry[i].size.ToString("X8"));
                        textBox1.AppendText("\r\nKey Index:  " + entry[i].key_index.ToString("X8"));
                        if ((entry[i].flags1 & 0x80000000) != 0) textBox1.AppendText("    Encrypted:         " + entry[i].is_encrypted.ToString());
                        else textBox1.AppendText("    Encrypted:        " + entry[i].is_encrypted.ToString());

                        //combine file entry and rsa decrypted data to form key seed
                        byte[] entry_data = new byte[0x40];
                        Array.Copy(entry[i].ToArray(), entry_data, 0x20);
                        Array.Copy(data, 0, entry_data, 0x20, 0x20);

                        //use sha256 to transform seed into aes iv and key
                        byte[] iv = new byte[0x10], key = new byte[0x10];
                        byte[] hash = Sha256(entry_data, 0, entry_data.Length);
                        Array.Copy(hash, 0, iv, 0, 0x10);
                        Array.Copy(hash, 0x10, key, 0, 0x10);

                        //output aes key and iv for current file
                        textBox1.AppendText("\r\nKey:        ");
                        for (int k = 0; k < 0x10; k++) textBox1.AppendText(key[k].ToString("X2"));
                        textBox1.AppendText("\r\nIV:         ");
                        for (int l = 0; l < 0x10; l++) textBox1.AppendText(iv[l].ToString("X2"));

                        //read and decrypt current file
                        IO.In.BaseStream.Position = entry[i].offset;
                        if ((entry[i].size % 16) != 0)
                            padded_size = entry[i].size + (16 - (entry[i].size % 16));
                        else padded_size = entry[i].size;

                        //decrypt file
                        byte[] file_data = DecryptAes(key, iv, IO.In.ReadBytes(padded_size));

                        //save decrypted data to file
                        if (entry_name[i] == "")
                        {
                            savename = pkgbasename + "_" + entry[i].type.ToString("X") + ".bin";
                            savepath = Path.Combine(outputpath, savename);
                        }
                        else
                        {
                            entry_name[i] = entry_name[i].Replace("/", "\\");
                            savename = Path.GetFileName(entry_name[i]);
                            extrasavepath = Path.Combine(outputpath, Path.GetDirectoryName(entry_name[i]));
                            if (!Directory.Exists(extrasavepath))
                            {
                                textBox1.AppendText("\r\n\r\ncreate output folder:\r\n" + extrasavepath);
                                Directory.CreateDirectory(extrasavepath);
                            }
                            savepath = Path.Combine(extrasavepath, savename);
                        }
                        textBox1.AppendText("\r\nsave path: \r\n" + savepath);

                        Array.Resize(ref file_data, Convert.ToInt32(entry[i].size));
                        File.WriteAllBytes(savepath, file_data);  //closes after write

                    } //if is encrypted
                }
                IO.Close();  //close pkg file
                textBox1.Select(0, 0);
                textBox1.ScrollToCaret();
            }



        }


        private void btnProperties_Click(object sender, EventArgs e)
        {

            itemIndex = "";
            ofd.Filter = "package|*.pkg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //load pkg file
                var IO = new EndianIO(filenames, EndianType.BigEndian, true);
                textBox1.Text = " ";
                textBox1.AppendText("\r\n\r\n" + System.IO.Path.GetFileName(filenames));
                long file_length = IO.Length;
                textBox1.AppendText("\r\nfile size:  " + file_length + " [0x" + file_length.ToString("X") + "]");
                if (file_length < 0x1C)
                {
                    textBox1.AppendText("\r\n\r\nwarning, file truncated\r\nfile closed");
                    IO.Close();
                    return;
                }
                uint fmagic = IO.In.SeekNReadUInt32(0x00);
                if (fmagic != 0x7F434E54)
                {
                    textBox1.AppendText("\r\n\r\nwarning 8675309, file magic\r\nunsupported file type\r\nfile closed");
                    IO.Close();
                    filenames = "";
                    return;
                }
                uint entry_count = IO.In.SeekNReadUInt32(0x10);

                //read and decrypt part 1 of key seed
                if (file_length < (0x2400 + 0x100))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                IO.SeekTo(0x2400);
                byte[] data = Decrypt(IO.In.ReadBytes(256));
                textBox1.AppendText("\r\n\r\ndecrypting key seed at 0x2400 \r\ndecrypted length 0x" + data.Length.ToString("X"));
                for (int j = 0; j < data.Length; j++)
                {
                    if ((j % 0x10) == 0) textBox1.AppendText("\r\n");
                    textBox1.Text = textBox1.Text + data[j].ToString("X2");
                }

                //read file entry table
                // uint entry_count = IO.In.SeekNReadUInt32(0x10);
                textBox1.AppendText("\r\n\r\nentry count:   " + entry_count);
                uint file_table_offset = IO.In.SeekNReadUInt32(0x18);

                uint strtab_count = 0;
                uint strtab_offset = 0;
                uint strtab_size = 0;

                if (file_length < (file_table_offset + (0x20 * entry_count)))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                IO.SeekTo(file_table_offset);
                PackageEntry[] entry = new PackageEntry[entry_count];
                for (int i = 0; i < entry_count; i++)
                {
                    entry[i].type = IO.In.ReadUInt32();
                    entry[i].unk1 = IO.In.ReadUInt32();
                    entry[i].flags1 = IO.In.ReadUInt32();
                    entry[i].flags2 = IO.In.ReadUInt32();
                    entry[i].offset = IO.In.ReadUInt32();
                    entry[i].size = IO.In.ReadUInt32();
                    entry[i].padding = IO.In.ReadBytes(8);

                    //set key index, encryption flag, string table properties
                    entry[i].key_index = ((entry[i].flags2 & 0xF000) >> 12);
                    entry[i].is_encrypted = ((entry[i].flags1 & 0x80000000) != 0) ? true : false;
                    if (entry[i].unk1 != 0) strtab_count++;
                    if (entry[i].type == 0x200)
                    {
                        strtab_offset = entry[i].offset;
                        strtab_size = entry[i].size;
                    }
                }

                //read strtab
                if (file_length < (strtab_offset + strtab_size))
                {
                    textBox1.AppendText("\r\n\r\nwarning, string table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                string[] entry_name = new string[entry_count];
                if (strtab_count > 0)
                {
                    textBox1.AppendText("\r\n\r\nstring table count:   " + strtab_count);
                    IO.SeekTo(strtab_offset);
                    byte[] string_table = IO.In.ReadBytes(strtab_size);
                    for (int i = 0; i < entry_count - 1; i++)
                    {
                        if (entry[i].unk1 != 0x00)  //file has strtab entry
                        {
                            entry_name[i] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[i].unk1), (Convert.ToInt32(entry[i + 1].unk1) - 1) - Convert.ToInt32(entry[i].unk1));
                            textBox1.AppendText("\r\n" + entry_name[i]);
                        }
                        else  //file doesn't have strtab entry
                        {
                            entry_name[i] = "";
                        }
                    }
                    if (entry[entry_count - 1].unk1 != 0x00)  //for last entry use size of strtab as endpoint instead of index of next entry
                    {
                        entry_name[entry_count - 1] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[entry_count - 1].unk1), (Convert.ToInt32(strtab_size) - 1) - Convert.ToInt32(entry[entry_count - 1].unk1));
                        textBox1.AppendText("\r\n" + entry_name[entry_count - 1]);
                    }
                    else
                    {
                        entry_name[entry_count - 1] = "";
                    }
                }
                else  //no strtab
                {
                    for (int i = 0; i < entry_count; i++) entry_name[i] = "";
                }

                //print file attributes
                textBox1.AppendText("\r\n\r\nFiles Listed by Number:");

                DataTable tb = new DataTable();
                tb.Columns.Add("index");
                tb.Columns.Add("entryName");
                tb.Columns.Add("entryType");
                tb.Columns.Add("unk1");
                tb.Columns.Add("flags1");
                tb.Columns.Add("flags2");
                tb.Columns.Add("offset");
                tb.Columns.Add("size");
                tb.Columns.Add("keyIndex");
                tb.Columns.Add("isEncrypted");
                tb.Columns.Add("key_k");
                tb.Columns.Add("iv_l");

                for (int i = 0; i < entry_count; i++)
                {
                    if (file_length < (entry[i].offset + entry[i].size))
                    {
                        textBox1.AppendText("\r\n\r\nwarning, file " + i + " out of range\r\nfile closed");
                        IO.Close();
                        return;
                    }
                    textBox1.AppendText("\r\n\r\n" + i.ToString("D"));
                    if (entry_name[i] != "") textBox1.AppendText("\r\n" + entry_name[i]);
                    textBox1.AppendText("\r\nType:       " + entry[i].type.ToString("X8"));
                    textBox1.AppendText("    Strtab Index:  " + entry[i].unk1.ToString("X8"));
                    textBox1.AppendText("\r\nFlags1:     " + entry[i].flags1.ToString("X8"));
                    textBox1.AppendText("    Flags2:        " + entry[i].flags2.ToString("X8"));
                    textBox1.AppendText("\r\nOffset:     " + entry[i].offset.ToString("X8"));
                    textBox1.AppendText("    Size:          " + entry[i].size.ToString("X8"));
                    textBox1.AppendText("\r\nKey Index:  " + entry[i].key_index.ToString("X8"));
                    if ((entry[i].flags1 & 0x80000000) != 0) textBox1.AppendText("    Encrypted:         " + entry[i].is_encrypted.ToString());
                    else textBox1.AppendText("    Encrypted:        " + entry[i].is_encrypted.ToString());

                    StringBuilder builtString_k = new StringBuilder();
                    StringBuilder builtString_l = new StringBuilder();

                    if (entry[i].is_encrypted != false)  //key and iv for encrypted files
                    {
                        //combine file entry and rsa decrypted data to form key seed
                        byte[] entry_data = new byte[0x40];
                        Array.Copy(entry[i].ToArray(), entry_data, 0x20);
                        Array.Copy(data, 0, entry_data, 0x20, 0x20);

                        //use sha256 to transform seed into aes iv and key
                        byte[] iv = new byte[0x10], key = new byte[0x10];
                        byte[] hash = Sha256(entry_data, 0, entry_data.Length);
                        Array.Copy(hash, 0, iv, 0, 0x10);
                        Array.Copy(hash, 0x10, key, 0, 0x10);
                        textBox1.AppendText("\r\nKey:        ");
                        for (int k = 0; k < 0x10; k++)
                        {
                            builtString_k.Append(key[k].ToString("X2"));
                            textBox1.AppendText(key[k].ToString("X2"));
                        }
                        textBox1.AppendText("\r\nIV:         ");
                        for (int l = 0; l < 0x10; l++)
                        {
                            builtString_l.Append(iv[l].ToString("X2"));
                            textBox1.AppendText(iv[l].ToString("X2"));
                        }

                    }

                    string index = i.ToString("D");
                    string entryName = entry_name[i];
                    string entryType = entry[i].type.ToString("X8");
                    string unk1 = entry[i].unk1.ToString("X8");
                    string flags1 = entry[i].flags1.ToString("X8");
                    string flags2 = entry[i].flags2.ToString("X8");
                    string offset = entry[i].offset.ToString("X8");
                    string size = entry[i].size.ToString("X8");
                    string keyIndex = entry[i].key_index.ToString("X8");
                    string isEncrypted = entry[i].is_encrypted.ToString();

                    if (entryName.ToString() == "")
                    {
                        entryName = "N/A";

                    }

                    if (builtString_k.ToString() == "")
                    {
                        builtString_k.Append("N/A");

                    }

                    if (builtString_l.ToString() == "")
                    {
                        builtString_l.Append("N/A");

                    }

                    tb.Rows.Add(index, entryName, entryType, unk1, flags1, flags2, offset, size, keyIndex, isEncrypted, builtString_k, builtString_l);
                    builtString_k.Clear();
                    builtString_l.Clear();
                }  //print file attributes
                dataGridView1.DataSource = tb;


                IO.Close();  //close pkg file
                textBox1.Select(0, 0);
                textBox1.ScrollToCaret();
            }
        }  //btnProperties_Click

        private void btnExtractAll_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //load pkg file
                itemIndex = "";
                var IO = new EndianIO(filenames, EndianType.BigEndian, true);
                textBox1.Text = " ";
                textBox1.AppendText("\r\n\r\n" + System.IO.Path.GetFileName(filenames));
                long file_length = IO.Length;
                textBox1.AppendText("\r\nfile size:  " + file_length + " [0x" + file_length.ToString("X") + "]");
                if (file_length < 0x1C)
                {
                    textBox1.AppendText("\r\n\r\nwarning, file truncated\r\nfile closed");
                    IO.Close();
                    return;
                }

                //set output path for extracted files
                string path2pkg = Path.GetDirectoryName(filenames);
                string fullpkgpath = Path.GetFullPath(filenames);
                string pkgbasename = Path.GetFileNameWithoutExtension(filenames);
                string pkgfilename = Path.GetFileName(filenames);
                string outputpath = fbd.SelectedPath; // Path.Combine(path2pkg, pkgbasename);
                // textBox1.AppendText("\r\n\r\npath2pkg:   " + path2pkg);     //  C:\Downloads\ps4packages\
                // textBox1.AppendText("\r\nfullpkgpath:   " + fullpkgpath);   //  C:\Downloads\ps4packages\Up1018...V0100.pkg
                // textBox1.AppendText("\r\npkgbasename:   " + pkgbasename);   //  Up1018...V0100
                // textBox1.AppendText("\r\npkgfilename:   " + pkgfilename);   //  Up1018...V0100.pkg
                //textBox1.AppendText("\r\n\r\noutput path:\r\n" + outputpath); //  C:\Downloads\ps4packages\Up1018...V0100 
                if (!Directory.Exists(outputpath))
                {
                    textBox1.AppendText("\r\n\r\ncreate output folder:\r\n" + outputpath);
                    Directory.CreateDirectory(outputpath);
                }
                else
                {
                    textBox1.AppendText("\r\n\r\noutput path:\r\n" + outputpath); //  C:\Downloads\ps4packages\Up1018...V0100
                }

                //read and decrypt part 1 of key seed
                if (file_length < (0x2400 + 0x100))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                IO.SeekTo(0x2400);
                byte[] data = Decrypt(IO.In.ReadBytes(256));
                textBox1.AppendText("\r\n\r\ndecrypting key seed at 0x2400 \r\ndecrypted length 0x" + data.Length.ToString("X"));
                for (int j = 0; j < data.Length; j++)
                {
                    if ((j % 0x10) == 0) textBox1.AppendText("\r\n");
                    textBox1.Text = textBox1.Text + data[j].ToString("X2");
                }

                //read file entry table
                uint entry_count = IO.In.SeekNReadUInt32(0x10);
                textBox1.AppendText("\r\n\r\nentry count:   " + entry_count);
                uint file_table_offset = IO.In.SeekNReadUInt32(0x18);
                uint padded_size;

                uint strtab_count = 0;
                uint strtab_offset = 0;
                uint strtab_size = 0;

                if (file_length < (file_table_offset + (0x20 * entry_count)))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                IO.SeekTo(file_table_offset);
                PackageEntry[] entry = new PackageEntry[entry_count];
                for (int i = 0; i < entry_count; i++)
                {
                    entry[i].type = IO.In.ReadUInt32();
                    entry[i].unk1 = IO.In.ReadUInt32();
                    entry[i].flags1 = IO.In.ReadUInt32();
                    entry[i].flags2 = IO.In.ReadUInt32();
                    entry[i].offset = IO.In.ReadUInt32();
                    entry[i].size = IO.In.ReadUInt32();
                    entry[i].padding = IO.In.ReadBytes(8);

                    //set key index, encryption flag, string table properties
                    entry[i].key_index = ((entry[i].flags2 & 0xF000) >> 12);
                    entry[i].is_encrypted = ((entry[i].flags1 & 0x80000000) != 0) ? true : false;
                    if (entry[i].unk1 != 0) strtab_count++;
                    if (entry[i].type == 0x200)
                    {
                        strtab_offset = entry[i].offset;
                        strtab_size = entry[i].size;
                    }
                }

                //read strtab
                if (file_length < (strtab_offset + strtab_size))
                {
                    textBox1.AppendText("\r\n\r\nwarning, string table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                string[] entry_name = new string[entry_count];
                if (strtab_count > 0)
                {
                    textBox1.AppendText("\r\n\r\nstrtab count:   " + strtab_count);
                    IO.SeekTo(strtab_offset);
                    byte[] string_table = IO.In.ReadBytes(strtab_size);
                    for (int i = 0; i < entry_count - 1; i++)
                    {
                        if (entry[i].unk1 != 0x00) //file has strtab entry
                        {
                            entry_name[i] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[i].unk1), (Convert.ToInt32(entry[i + 1].unk1) - 1) - Convert.ToInt32(entry[i].unk1));
                            textBox1.AppendText("\r\n" + entry_name[i]);
                        }
                        else  //file doesn't have strtab entry
                        {
                            entry_name[i] = "";
                        }
                    }
                    if (entry[entry_count - 1].unk1 != 0x00)
                    {
                        entry_name[entry_count - 1] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[entry_count - 1].unk1), (Convert.ToInt32(strtab_size) - 1) - Convert.ToInt32(entry[entry_count - 1].unk1));
                        textBox1.AppendText("\r\n" + entry_name[entry_count - 1]);
                    }
                    else
                    {
                        entry_name[entry_count - 1] = "";
                    }
                }
                else  //no strtab
                {
                    for (int i = 0; i < entry_count; i++) entry_name[i] = "";
                }

                textBox1.AppendText("\r\n\r\nFiles Listed by Number:");
                for (int i = 0; i < entry_count; i++)
                {
                    if (file_length < (entry[i].offset + entry[i].size))
                    {
                        textBox1.AppendText("\r\n\r\nwarning, file " + i + " out of range\r\nfile closed");
                        IO.Close();
                        return;
                    }
                    //print file attributes
                    textBox1.AppendText("\r\n");
                    textBox1.AppendText("\r\n" + i.ToString("D"));
                    if (entry_name[i] != "") textBox1.AppendText("\r\n" + entry_name[i]);
                    textBox1.AppendText("\r\nType:       " + entry[i].type.ToString("X8"));
                    textBox1.AppendText("    Strtab Index:  " + entry[i].unk1.ToString("X8"));
                    textBox1.AppendText("\r\nFlags1:     " + entry[i].flags1.ToString("X8"));
                    textBox1.AppendText("    Flags2:        " + entry[i].flags2.ToString("X8"));
                    textBox1.AppendText("\r\nOffset:     " + entry[i].offset.ToString("X8"));
                    textBox1.AppendText("    Size:          " + entry[i].size.ToString("X8"));
                    textBox1.AppendText("\r\nKey Index:  " + entry[i].key_index.ToString("X8"));
                    if ((entry[i].flags1 & 0x80000000) != 0) textBox1.AppendText("    Encrypted:         " + entry[i].is_encrypted.ToString());
                    else textBox1.AppendText("    Encrypted:        " + entry[i].is_encrypted.ToString());

                    string savepath;
                    string savename;
                    string extrasavepath;

                    if (entry[i].is_encrypted != false)
                    {
                        //combine file entry and rsa decrypted data to form key seed
                        byte[] entry_data = new byte[0x40];
                        Array.Copy(entry[i].ToArray(), entry_data, 0x20);
                        Array.Copy(data, 0, entry_data, 0x20, 0x20);

                        //use sha256 to transform seed into aes iv and key
                        byte[] iv = new byte[0x10], key = new byte[0x10];
                        byte[] hash = Sha256(entry_data, 0, entry_data.Length);
                        Array.Copy(hash, 0, iv, 0, 0x10);
                        Array.Copy(hash, 0x10, key, 0, 0x10);

                        //output aes key and iv for current file
                        textBox1.AppendText("\r\nKey:        ");
                        for (int k = 0; k < 0x10; k++) textBox1.AppendText(key[k].ToString("X2"));
                        textBox1.AppendText("\r\nIV:         ");
                        for (int l = 0; l < 0x10; l++) textBox1.AppendText(iv[l].ToString("X2"));

                        //read and decrypt current file
                        IO.In.BaseStream.Position = entry[i].offset;
                        if ((entry[i].size % 16) != 0)
                            padded_size = entry[i].size + (16 - (entry[i].size % 16));
                        else padded_size = entry[i].size;

                        //decrypt file
                        byte[] file_data = DecryptAes(key, iv, IO.In.ReadBytes(padded_size));

                        //save decrypted data to file
                        if (entry_name[i] == "")  //build entry name from pkg name and file type
                        {
                            savename = pkgbasename + "_" + entry[i].type.ToString("X") + ".bin";
                            savepath = Path.Combine(outputpath, savename);
                        }
                        else  //file name from strtab and output folder
                        {
                            entry_name[i] = entry_name[i].Replace("/", "\\");
                            savename = Path.GetFileName(entry_name[i]);
                            extrasavepath = Path.Combine(outputpath, Path.GetDirectoryName(entry_name[i]));
                            if (!Directory.Exists(extrasavepath))
                            {
                                textBox1.AppendText("\r\n\r\ncreate output folder:\r\n" + extrasavepath);
                                Directory.CreateDirectory(extrasavepath);
                            }
                            savepath = Path.Combine(extrasavepath, savename);
                        }
                        textBox1.AppendText("\r\nsave path: \r\n" + savepath);
                        Array.Resize(ref file_data, Convert.ToInt32(entry[i].size));
                        File.WriteAllBytes(savepath, file_data);  //closes after write
                    }
                    else if (entry[i].is_encrypted != true)
                    {
                        //read current file
                        IO.In.BaseStream.Position = entry[i].offset;
                        byte[] file_data2 = IO.In.ReadBytes(entry[i].size);
                        if (entry_name[i] == "")
                        {
                            savename = pkgbasename + "_" + entry[i].type.ToString("X") + ".bin";
                            savepath = Path.Combine(outputpath, savename);
                        }
                        else
                        {
                            entry_name[i] = entry_name[i].Replace("/", "\\");
                            savename = Path.GetFileName(entry_name[i]);
                            extrasavepath = Path.Combine(outputpath, Path.GetDirectoryName(entry_name[i]));
                            if (!Directory.Exists(extrasavepath))
                            {
                                textBox1.AppendText("\r\n\r\ncreate output folder:\r\n" + extrasavepath);
                                Directory.CreateDirectory(extrasavepath);
                            }
                            savepath = Path.Combine(extrasavepath, savename);
                        }
                        textBox1.AppendText("\r\nsave path: \r\n" + savepath);
                        File.WriteAllBytes(savepath, file_data2);  //closes after write
                    }
                }
                IO.Close();  //close pkg file
                textBox1.Select(0, 0);
                textBox1.ScrollToCaret();
            }




        }

        private void btnExtractFile_Click(object sender, EventArgs e)
        {

            if (filenames == "")
            {
                DarkMessageBox.ShowInformation("Load a PS4 game pkg to use this function.", "Load Package");
                btnProperties.Focus();
                return;
            }
            if (itemIndex == "")
            {
                DarkMessageBox.ShowInformation("Select file.", "Choose File");
                return;
            }
            if ((itemIndex == " ") || (itemIndex == "  ") || (itemIndex == "   ") || (itemIndex == "    ")
                 || (itemIndex == "     ") || (itemIndex == "      ") || (itemIndex == "       ") || (itemIndex == "        "))
            {
                DarkMessageBox.ShowInformation("Enter a file number.", "Choose File");

                return;
            }

            /*
            for (int i = 0; i < itemIndex.Length; i++)
            {
                if (System.Char.IsNumber(itemIndex, i) == false)
                {
                    MessageBox.Show("Enter a valid file number from the list of files.", "Choose File");

                    return;
                }
            }
            */

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {

                //load pkg file
                itemIndex = itemIndex.Replace(",", "");
                itemIndex = itemIndex.Replace("$", "");
                Int32 file_index = Convert.ToInt32(itemIndex);
                var IO = new EndianIO(filenames, EndianType.BigEndian, true);
                long file_length = IO.Length;
                if (file_length < 0x1C)
                {
                    textBox1.AppendText("\r\n\r\nwarning, file truncated\r\nfile closed");
                    IO.Close();
                    return;
                }

                //read file entry table
                uint entry_count = IO.In.SeekNReadUInt32(0x10);
                if ((file_index < 0) || (file_index >= entry_count))
                {
                    DarkMessageBox.ShowInformation("Enter a file number from 0 to" + (entry_count - 1).ToString("D") + ".", "Choose File");
                    IO.Close();  //close pkg file

                    return;
                }
                textBox1.Text = " ";
                textBox1.AppendText("\r\n\r\n" + System.IO.Path.GetFileName(filenames));
                textBox1.AppendText("\r\nfile size:  " + file_length + " [0x" + file_length.ToString("X") + "]");
                textBox1.AppendText("\r\n\r\nentry count:   " + entry_count.ToString("D"));
                uint file_table_offset = IO.In.SeekNReadUInt32(0x18);

                //set output path for extracted files
                string path2pkg = Path.GetDirectoryName(filenames);
                string fullpkgpath = Path.GetFullPath(filenames);
                string pkgbasename = Path.GetFileNameWithoutExtension(filenames);
                string pkgfilename = Path.GetFileName(filenames);
                string outputpath = fbd.SelectedPath; // Path.Combine(path2pkg, pkgbasename);
                //textBox1.AppendText("\r\n\r\npath2pkg:   " + path2pkg);     //  C:\Downloads\ps4packages\
                //textBox1.AppendText("\r\nfullpkgpath:   " + fullpkgpath);   //  C:\Downloads\ps4packages\Up1018...V0100.pkg
                //textBox1.AppendText("\r\npkgbasename:   " + pkgbasename);   //  Up1018...V0100
                //textBox1.AppendText("\r\npkgfilename:   " + pkgfilename);   //  Up1018...V0100.pkg
                //textBox1.AppendText("\r\noutput path:\r\n" + outputpath); //  C:\Downloads\ps4packages\Up1018...V0100 
                if (!Directory.Exists(outputpath))
                {
                    textBox1.AppendText("\r\n\r\ncreate output folder:\r\n" + outputpath);
                    Directory.CreateDirectory(outputpath);
                }
                else
                {
                    textBox1.AppendText("\r\n\r\noutput path:\r\n" + outputpath); //  C:\Downloads\ps4packages\Up1018...V0100
                }

                //read and decrypt part 1 of key seed
                if (file_length < (0x2400 + 0x100))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                IO.SeekTo(0x2400);
                byte[] data = Decrypt(IO.In.ReadBytes(256));
                textBox1.AppendText("\r\n\r\ndecrypting key seed at 0x2400 \r\ndecrypted length 0x" + data.Length.ToString("X"));
                for (int j = 0; j < data.Length; j++)
                {
                    if ((j % 0x10) == 0) textBox1.AppendText("\r\n");
                    textBox1.Text = textBox1.Text + data[j].ToString("X2");
                }

                uint padded_size;
                uint strtab_count = 0;
                uint strtab_offset = 0;
                uint strtab_size = 0;

                if (file_length < (file_table_offset + (0x20 * entry_count)))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                IO.SeekTo(file_table_offset);
                PackageEntry[] entry = new PackageEntry[entry_count];
                for (int i = 0; i < entry_count; i++)
                {
                    entry[i].type = IO.In.ReadUInt32();
                    entry[i].unk1 = IO.In.ReadUInt32();
                    entry[i].flags1 = IO.In.ReadUInt32();
                    entry[i].flags2 = IO.In.ReadUInt32();
                    entry[i].offset = IO.In.ReadUInt32();
                    entry[i].size = IO.In.ReadUInt32();
                    entry[i].padding = IO.In.ReadBytes(8);

                    //set key index, encryption flag, string table properties
                    entry[i].key_index = ((entry[i].flags2 & 0xF000) >> 12);
                    entry[i].is_encrypted = ((entry[i].flags1 & 0x80000000) != 0) ? true : false;
                    if (entry[i].unk1 != 0) strtab_count++;
                    if (entry[i].type == 0x200)
                    {
                        strtab_offset = entry[i].offset;
                        strtab_size = entry[i].size;
                    }
                }

                //read strtab
                if (file_length < (strtab_offset + strtab_size))
                {
                    textBox1.AppendText("\r\n\r\nwarning, string table out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                string[] entry_name = new string[entry_count];
                if (strtab_count > 0)
                {
                    textBox1.AppendText("\r\n\r\nstring table count:   " + strtab_count);
                    IO.SeekTo(strtab_offset);
                    byte[] string_table = IO.In.ReadBytes(strtab_size);
                    for (int i = 0; i < entry_count - 1; i++)
                    {
                        if (entry[i].unk1 != 0x00)  //file has strtab entry
                        {
                            entry_name[i] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[i].unk1), (Convert.ToInt32(entry[i + 1].unk1) - 1) - Convert.ToInt32(entry[i].unk1));
                            textBox1.AppendText("\r\n" + entry_name[i]);
                        }
                        else  //file doesn't have strtab entry
                        {
                            entry_name[i] = "";
                        }
                    }
                    if (entry[entry_count - 1].unk1 != 0x00)  //for last entry use size of strtab as endpoint instead of index of next entry
                    {
                        entry_name[entry_count - 1] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[entry_count - 1].unk1), (Convert.ToInt32(strtab_size) - 1) - Convert.ToInt32(entry[entry_count - 1].unk1));
                        textBox1.AppendText("\r\n" + entry_name[entry_count - 1]);
                    }
                    else
                    {
                        entry_name[entry_count - 1] = "";
                    }
                }
                else  //no strtab
                {
                    for (int i = 0; i < entry_count; i++) entry_name[i] = "";
                }

                textBox1.AppendText("\r\n\r\n" + file_index.ToString("D"));
                if (entry_name[file_index] != "") textBox1.AppendText("\r\n" + entry_name[file_index]);
                textBox1.AppendText("\r\nType:       " + entry[file_index].type.ToString("X8"));
                textBox1.AppendText("    Strtab Index:  " + entry[file_index].unk1.ToString("X8"));
                textBox1.AppendText("\r\nFlags1:     " + entry[file_index].flags1.ToString("X8"));
                textBox1.AppendText("    Flags2:        " + entry[file_index].flags2.ToString("X8"));
                textBox1.AppendText("\r\nOffset:     " + entry[file_index].offset.ToString("X8"));
                textBox1.AppendText("    Size:          " + entry[file_index].size.ToString("X8"));
                textBox1.AppendText("\r\nKey Index:  " + entry[file_index].key_index.ToString("X8"));
                if ((entry[file_index].flags1 & 0x80000000) != 0) textBox1.AppendText("    Encrypted:         " + entry[file_index].is_encrypted.ToString());
                else textBox1.AppendText("    Encrypted:        " + entry[file_index].is_encrypted.ToString());

                if (file_length < (entry[file_index].offset + entry[file_index].size))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file " + file_index + " out of range\r\nfile closed");
                    IO.Close();

                    return;
                }

                string savepath;
                string savename;
                string extrasavepath;

                if (entry[file_index].is_encrypted != false)
                {
                    //combine file entry and rsa decrypted data to form key seed
                    byte[] entry_data = new byte[0x40];
                    Array.Copy(entry[file_index].ToArray(), entry_data, 0x20);
                    Array.Copy(data, 0, entry_data, 0x20, 0x20);

                    //use sha256 to transform seed into aes iv and key
                    byte[] iv = new byte[0x10], key = new byte[0x10];
                    byte[] hash = Sha256(entry_data, 0, entry_data.Length);
                    Array.Copy(hash, 0, iv, 0, 0x10);
                    Array.Copy(hash, 0x10, key, 0, 0x10);

                    //print aes key and iv for current file
                    textBox1.AppendText("\r\nKey:        ");
                    for (int k = 0; k < 0x10; k++) textBox1.AppendText(key[k].ToString("X2"));
                    textBox1.AppendText("\r\nIV:         ");
                    for (int l = 0; l < 0x10; l++) textBox1.AppendText(iv[l].ToString("X2"));

                    //read and decrypt current file
                    IO.In.BaseStream.Position = entry[file_index].offset;
                    if ((entry[file_index].size % 16) != 0)
                        padded_size = entry[file_index].size + (16 - (entry[file_index].size % 16));
                    else padded_size = entry[file_index].size;

                    //decrypt file
                    byte[] file_data = DecryptAes(key, iv, IO.In.ReadBytes(padded_size));

                    //save decrypted data to file
                    if (entry_name[file_index] == "")
                    {
                        savename = pkgbasename + "_" + entry[file_index].type.ToString("X") + ".bin";
                        savepath = Path.Combine(outputpath, savename);
                    }
                    else
                    {
                        entry_name[file_index] = entry_name[file_index].Replace("/", "\\");
                        savename = Path.GetFileName(entry_name[file_index]);
                        extrasavepath = Path.Combine(outputpath, Path.GetDirectoryName(entry_name[file_index]));
                        if (!Directory.Exists(extrasavepath))
                        {
                            textBox1.AppendText("\r\n\r\ncreate output folder:\r\n" + extrasavepath);
                            Directory.CreateDirectory(extrasavepath);
                        }
                        savepath = Path.Combine(extrasavepath, savename);
                    }
                    textBox1.AppendText("\r\nsave path: \r\n" + savepath);
                    Array.Resize(ref file_data, Convert.ToInt32(entry[file_index].size));
                    File.WriteAllBytes(savepath, file_data);  //closes after write
                }
                else if (entry[file_index].is_encrypted != true)
                {
                    //read current file
                    IO.In.BaseStream.Position = entry[file_index].offset;
                    byte[] file_data2 = IO.In.ReadBytes(entry[file_index].size);
                    if (entry_name[file_index] == "")
                    {
                        savename = pkgbasename + "_" + entry[file_index].type.ToString("X") + ".bin";
                        savepath = Path.Combine(outputpath, savename);
                    }
                    else
                    {
                        entry_name[file_index] = entry_name[file_index].Replace("/", "\\");
                        savename = Path.GetFileName(entry_name[file_index]);
                        extrasavepath = Path.Combine(outputpath, Path.GetDirectoryName(entry_name[file_index]));
                        if (!Directory.Exists(extrasavepath))
                        {
                            textBox1.AppendText("\r\n\r\ncreate output folder:\r\n" + extrasavepath);
                            Directory.CreateDirectory(extrasavepath);
                        }
                        savepath = Path.Combine(extrasavepath, savename);
                    }
                    textBox1.AppendText("\r\nsave path: \r\n" + savepath);
                    File.WriteAllBytes(savepath, file_data2);  //closes after write
                }
                IO.Close();  //close pkg file
                textBox1.Select(0, 0);
                textBox1.ScrollToCaret();

            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dataGridView1.MouseClick -= dataGridView1_MouseClick;
            }
            else
            {
                dataGridView1.MouseClick -= dataGridView1_MouseClick;

            }
            dataGridView1.MouseClick += dataGridView1_MouseClick;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var hti = dataGridView1.HitTest(e.X, e.Y);

                if (hti.RowIndex != -1)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hti.RowIndex].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[hti.RowIndex].Cells[0];
                }
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {

                    try
                    {
                        //get each selected pkg full path
                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                            itemIndex = selectedRow.Cells[0].Value.ToString();
                        }
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        DarkMessageBox.ShowError("The Clipboard could not be accessed. Please try again.", "PS4 PKG Tool");
                    }
                }
            }
            //what happen when right clicked on pkg item

        }

        private void pkgdec_Load(object sender, EventArgs e)
        {
            //load pkg file
            var IO = new EndianIO(filenames, EndianType.BigEndian, true);
            textBox1.Text = " ";
            textBox1.AppendText("\r\n\r\n" + System.IO.Path.GetFileName(filenames));
            long file_length = IO.Length;
            textBox1.AppendText("\r\nfile size:  " + file_length + " [0x" + file_length.ToString("X") + "]");
            if (file_length < 0x1C)
            {
                textBox1.AppendText("\r\n\r\nwarning, file truncated\r\nfile closed");
                IO.Close();
                return;
            }
            uint fmagic = IO.In.SeekNReadUInt32(0x00);
            if (fmagic != 0x7F434E54)
            {
                textBox1.AppendText("\r\n\r\nwarning 8675309, file magic\r\nunsupported file type\r\nfile closed");
                IO.Close();
                filenames = "";
                return;
            }
            uint entry_count = IO.In.SeekNReadUInt32(0x10);

            //read and decrypt part 1 of key seed
            if (file_length < (0x2400 + 0x100))
            {
                textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                IO.Close();
                return;
            }
            IO.SeekTo(0x2400);
            byte[] data = Decrypt(IO.In.ReadBytes(256));
            textBox1.AppendText("\r\n\r\ndecrypting key seed at 0x2400 \r\ndecrypted length 0x" + data.Length.ToString("X"));
            for (int j = 0; j < data.Length; j++)
            {
                if ((j % 0x10) == 0) textBox1.AppendText("\r\n");
                textBox1.Text = textBox1.Text + data[j].ToString("X2");
            }

            //read file entry table
            // uint entry_count = IO.In.SeekNReadUInt32(0x10);
            textBox1.AppendText("\r\n\r\nentry count:   " + entry_count);
            uint file_table_offset = IO.In.SeekNReadUInt32(0x18);

            uint strtab_count = 0;
            uint strtab_offset = 0;
            uint strtab_size = 0;

            if (file_length < (file_table_offset + (0x20 * entry_count)))
            {
                textBox1.AppendText("\r\n\r\nwarning, file table out of range\r\nfile closed");
                IO.Close();
                return;
            }
            IO.SeekTo(file_table_offset);
            PackageEntry[] entry = new PackageEntry[entry_count];
            for (int i = 0; i < entry_count; i++)
            {
                entry[i].type = IO.In.ReadUInt32();
                entry[i].unk1 = IO.In.ReadUInt32();
                entry[i].flags1 = IO.In.ReadUInt32();
                entry[i].flags2 = IO.In.ReadUInt32();
                entry[i].offset = IO.In.ReadUInt32();
                entry[i].size = IO.In.ReadUInt32();
                entry[i].padding = IO.In.ReadBytes(8);

                //set key index, encryption flag, string table properties
                entry[i].key_index = ((entry[i].flags2 & 0xF000) >> 12);
                entry[i].is_encrypted = ((entry[i].flags1 & 0x80000000) != 0) ? true : false;
                if (entry[i].unk1 != 0) strtab_count++;
                if (entry[i].type == 0x200)
                {
                    strtab_offset = entry[i].offset;
                    strtab_size = entry[i].size;
                }
            }

            //read strtab
            if (file_length < (strtab_offset + strtab_size))
            {
                textBox1.AppendText("\r\n\r\nwarning, string table out of range\r\nfile closed");
                IO.Close();
                return;
            }
            string[] entry_name = new string[entry_count];
            if (strtab_count > 0)
            {
                textBox1.AppendText("\r\n\r\nstring table count:   " + strtab_count);
                IO.SeekTo(strtab_offset);
                byte[] string_table = IO.In.ReadBytes(strtab_size);
                for (int i = 0; i < entry_count - 1; i++)
                {
                    if (entry[i].unk1 != 0x00)  //file has strtab entry
                    {
                        entry_name[i] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[i].unk1), (Convert.ToInt32(entry[i + 1].unk1) - 1) - Convert.ToInt32(entry[i].unk1));
                        textBox1.AppendText("\r\n" + entry_name[i]);
                    }
                    else  //file doesn't have strtab entry
                    {
                        entry_name[i] = "";
                    }
                }
                if (entry[entry_count - 1].unk1 != 0x00)  //for last entry use size of strtab as endpoint instead of index of next entry
                {
                    entry_name[entry_count - 1] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[entry_count - 1].unk1), (Convert.ToInt32(strtab_size) - 1) - Convert.ToInt32(entry[entry_count - 1].unk1));
                    textBox1.AppendText("\r\n" + entry_name[entry_count - 1]);
                }
                else
                {
                    entry_name[entry_count - 1] = "";
                }
            }
            else  //no strtab
            {
                for (int i = 0; i < entry_count; i++) entry_name[i] = "";
            }

            //print file attributes
            textBox1.AppendText("\r\n\r\nFiles Listed by Number:");

            DataTable tb = new DataTable();
            tb.Columns.Add("Index");
            tb.Columns.Add("Entry Name");
            tb.Columns.Add("Entry Type");
            tb.Columns.Add("Unk1");
            tb.Columns.Add("Flags 1");
            tb.Columns.Add("Flags 2");
            tb.Columns.Add("Offset");
            tb.Columns.Add("Size");
            tb.Columns.Add("Key Index");
            tb.Columns.Add("Encypted?");
            tb.Columns.Add("Key");
            tb.Columns.Add("IV");

            for (int i = 0; i < entry_count; i++)
            {
                if (file_length < (entry[i].offset + entry[i].size))
                {
                    textBox1.AppendText("\r\n\r\nwarning, file " + i + " out of range\r\nfile closed");
                    IO.Close();
                    return;
                }
                textBox1.AppendText("\r\n\r\n" + i.ToString("D"));
                if (entry_name[i] != "") textBox1.AppendText("\r\n" + entry_name[i]);
                textBox1.AppendText("\r\nType:       " + entry[i].type.ToString("X8"));
                textBox1.AppendText("    Strtab Index:  " + entry[i].unk1.ToString("X8"));
                textBox1.AppendText("\r\nFlags1:     " + entry[i].flags1.ToString("X8"));
                textBox1.AppendText("    Flags2:        " + entry[i].flags2.ToString("X8"));
                textBox1.AppendText("\r\nOffset:     " + entry[i].offset.ToString("X8"));
                textBox1.AppendText("    Size:          " + entry[i].size.ToString("X8"));
                textBox1.AppendText("\r\nKey Index:  " + entry[i].key_index.ToString("X8"));
                if ((entry[i].flags1 & 0x80000000) != 0) textBox1.AppendText("    Encrypted:         " + entry[i].is_encrypted.ToString());
                else textBox1.AppendText("    Encrypted:        " + entry[i].is_encrypted.ToString());

                StringBuilder builtString_k = new StringBuilder();
                StringBuilder builtString_l = new StringBuilder();

                if (entry[i].is_encrypted != false)  //key and iv for encrypted files
                {
                    //combine file entry and rsa decrypted data to form key seed
                    byte[] entry_data = new byte[0x40];
                    Array.Copy(entry[i].ToArray(), entry_data, 0x20);
                    Array.Copy(data, 0, entry_data, 0x20, 0x20);

                    //use sha256 to transform seed into aes iv and key
                    byte[] iv = new byte[0x10], key = new byte[0x10];
                    byte[] hash = Sha256(entry_data, 0, entry_data.Length);
                    Array.Copy(hash, 0, iv, 0, 0x10);
                    Array.Copy(hash, 0x10, key, 0, 0x10);
                    textBox1.AppendText("\r\nKey:        ");
                    for (int k = 0; k < 0x10; k++)
                    {
                        builtString_k.Append(key[k].ToString("X2"));
                        textBox1.AppendText(key[k].ToString("X2"));
                    }
                    textBox1.AppendText("\r\nIV:         ");
                    for (int l = 0; l < 0x10; l++)
                    {
                        builtString_l.Append(iv[l].ToString("X2"));
                        textBox1.AppendText(iv[l].ToString("X2"));
                    }

                }

                string index = i.ToString("D");
                string entryName = entry_name[i];
                string entryType = entry[i].type.ToString("X8");
                string unk1 = entry[i].unk1.ToString("X8");
                string flags1 = entry[i].flags1.ToString("X8");
                string flags2 = entry[i].flags2.ToString("X8");
                string offset = entry[i].offset.ToString("X8");
                string size = entry[i].size.ToString("X8");
                string keyIndex = entry[i].key_index.ToString("X8");
                string isEncrypted = entry[i].is_encrypted.ToString();

                /*
                if (entryName.ToString() == "")
                {
                    entryName = "N/A";

                }
                */



                if (builtString_k.ToString() == "")
                {
                    builtString_k.Append("N/A");

                }

                if (builtString_l.ToString() == "")
                {
                    builtString_l.Append("N/A");

                }

                if (isEncrypted == "True")
                {
                    tb.Rows.Add(index, entryName.ToUpper().Replace(".", "_"), entryType, unk1, flags1, flags2, offset, size, keyIndex, isEncrypted, builtString_k, builtString_l);

                }

                builtString_k.Clear();
                builtString_l.Clear();
            }  //print file attributes
            dataGridView1.DataSource = tb;


            IO.Close();  //close pkg file
            textBox1.Select(0, 0);
            textBox1.ScrollToCaret();


        }
    }
}
