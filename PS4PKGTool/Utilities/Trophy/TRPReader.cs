// Decompiled with JetBrains decompiler
// Type: TRPViewer.TRPReader
// Assembly: TRPViewer, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC455631-E5E4-4FE8-BC3B-A8476C3FB343
// Assembly location: C:\Users\pearlxcore\Desktop\TRPViewer(1).exe

using DarkUI.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PS4PKGTool.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static PS4PKGTool.Utilities.PS4PKGToolHelper.Helper;

namespace TRPViewer
{
    public class TRPReader
    {
        private TRPReader.TRPHeader _hdr;
        private List<Archiver> _trophyList;
        private byte[] _hdrmagic;
        private bool _iserror;
        private bool _readbytes;
        private bool _throwerror;
        private string _error;
        private string _calculatedsha1;
        private string _inputfile;
        private string _titlename;
        private string _npcommid;

        public TRPReader()
        {
            this._hdr = new TRPReader.TRPHeader();
            this._trophyList = new List<Archiver>();
            this._hdrmagic = new byte[4]
            {
        (byte) 220,
        (byte) 162,
        (byte) 77,
        (byte) 0
            };
            this._throwerror = true;
        }

        public void Load(string filename)
        {
            try
            {
                this._iserror = false;
                this._inputfile = filename;
                this._calculatedsha1 = (string)null;
                this._trophyList = new List<Archiver>();
                using (FileStream fs = new FileStream(this._inputfile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    this.ReadHeader(fs);
                    if (!this.ByteArraysEqual(this._hdr.magic, this._hdrmagic))
                        throw new Exception("This file is not supported!");
                    this.ReadContent(fs);
                    if (this.Version > 1)
                        this._calculatedsha1 = this.CalculateSHA1Hash();
                }
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                this._iserror = true;
                this._error = exception.Message;
                ProjectData.ClearProjectError();
            }
            if (this._iserror && this._throwerror)
                throw new Exception(this._error);
            if (!this._iserror || this._throwerror)
                return;
            int num = (int)ShowError(this._error, true);
        }

        private void ReadHeader(FileStream fs)
        {
            TRPReader.TRPHeader trpHeader = new TRPReader.TRPHeader();
            trpHeader.magic = new byte[4];
            trpHeader.version = new byte[4];
            trpHeader.file_size = new byte[8];
            trpHeader.files_count = new byte[4];
            trpHeader.element_size = new byte[4];
            trpHeader.dev_flag = new byte[4];
            trpHeader.sha1 = new byte[20];
            trpHeader.padding = new byte[36];
            fs.Read(trpHeader.magic, 0, trpHeader.magic.Length);
            fs.Read(trpHeader.version, 0, trpHeader.version.Length);
            fs.Read(trpHeader.file_size, 0, trpHeader.file_size.Length);
            fs.Read(trpHeader.files_count, 0, trpHeader.files_count.Length);
            fs.Read(trpHeader.element_size, 0, trpHeader.element_size.Length);
            fs.Read(trpHeader.dev_flag, 0, trpHeader.dev_flag.Length);
            switch (TRPReader.byteArrayToLittleEndianInteger(trpHeader.version))
            {
                case 1:
                    fs.Read(trpHeader.padding, 0, trpHeader.padding.Length);
                    break;
                case 2:
                    fs.Read(trpHeader.sha1, 0, trpHeader.sha1.Length);
                    trpHeader.padding = new byte[16];
                    fs.Read(trpHeader.padding, 0, trpHeader.padding.Length);
                    break;
                case 3:
                    fs.Read(trpHeader.sha1, 0, trpHeader.sha1.Length);
                    trpHeader.padding = new byte[48];
                    fs.Read(trpHeader.padding, 0, trpHeader.padding.Length);
                    break;
            }
            this._hdr = trpHeader;
        }

        private void ReadContent(FileStream fs)
        {
            byte[] numArray1 = new byte[36];
            byte[] numArray2 = new byte[4];
            byte[] numArray3 = new byte[8];
            byte[] array = new byte[4];
            int num1 = checked(this.FileCount - 1);
            int m_Index = 0;
            while (m_Index <= num1)
            {
                fs.Read(numArray1, 0, numArray1.Length);
                fs.Read(numArray2, 0, numArray2.Length);
                fs.Read(numArray3, 0, numArray3.Length);
                fs.Read(array, 0, array.Length);
                fs.Seek(12L, SeekOrigin.Current);
                string m_Name = TRPReader.byteArrayToUTF8String(numArray1).Replace("\0", (string)null);
                long offset = TRPReader.hexStringToLong(TRPReader.byteArrayToHexString(numArray2));
                long num2 = TRPReader.hexStringToLong(TRPReader.byteArrayToHexString(numArray3));
                if (this._readbytes)
                {
                    using (FileStream fileStream = new FileStream(this._inputfile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        byte[] numArray4 = new byte[checked((int)(num2 - 1L) + 1)];
                        fileStream.Seek(offset, SeekOrigin.Begin);
                        fileStream.Read(numArray4, 0, numArray4.Length);
                        this._trophyList.Add(new Archiver(m_Index, m_Name, checked((uint)offset), checked((ulong)num2), numArray4));
                    }
                }
                else
                    this._trophyList.Add(new Archiver(m_Index, m_Name, checked((uint)offset), checked((ulong)num2), (byte[])null));
                checked { ++m_Index; }
            }
        }

        public bool ReadBytes
        {
            get
            {
                return this._readbytes;
            }
            set
            {
                this._readbytes = value;
            }
        }

        public List<Archiver> TrophyList
        {
            get
            {
                return this._trophyList;
            }
        }

        public int FileSize
        {
            get
            {
                return checked((int)this.byte_to_qword(this._hdr.file_size, 0));
            }
        }

        public int FileCount
        {
            get
            {
                return checked((int)TRPReader.byteArrayToLittleEndianInteger(this._hdr.files_count));
            }
        }

        public int Version
        {
            get
            {
                return checked((int)TRPReader.byteArrayToLittleEndianInteger(this._hdr.version));
            }
        }

        public string SHA1
        {
            get
            {
                if (this.Version <= 1)
                    return (string)null;
                return TRPReader.byteArrayToHexString(this._hdr.sha1);
            }
        }

        public string CalculatedSHA1
        {
            get
            {
                return this._calculatedsha1;
            }
        }

        public bool IsError
        {
            get
            {
                return this._iserror;
            }
        }

        public bool ThrowError
        {
            get
            {
                return this._throwerror;
            }
            set
            {
                this._throwerror = value;
            }
        }

        public string TitleName
        {
            get
            {
                return this._titlename;
            }
            set
            {
                this._titlename = value;
            }
        }

        public string NPCommId
        {
            get
            {
                return this._npcommid;
            }
            set
            {
                this._npcommid = value;
            }
        }

        private bool ByteArraysEqual(byte[] first, byte[] second)
        {
            if (first == second)
                return true;
            if (first == null || second == null || first.Length != second.Length)
                return false;
            int num = checked(first.Length - 1);
            int index = 0;
            while (index <= num)
            {
                if ((int)first[index] != (int)second[index])
                    return false;
                checked { ++index; }
            }
            return true;
        }

        private ulong byte_to_qword(byte[] buf, int index)
        {
            Array.Reverse((Array)buf, index, 8);
            return BitConverter.ToUInt64(buf, index);
        }

        private static long byteArrayToLittleEndianInteger(byte[] bits)
        {
            return (long)((uint)bits[0] | (uint)(byte)((uint)bits[1] << 0) | (uint)(byte)((uint)bits[2] << 0) | (uint)(byte)((uint)bits[3] << 0));
        }

        private static string byteArrayToUTF8String(byte[] _byte)
        {
            return Encoding.UTF8.GetString(_byte);
        }

        private static string byteArrayToHexString(byte[] bytes_Input)
        {
            StringBuilder stringBuilder = new StringBuilder(checked(bytes_Input.Length * 2));
            byte[] numArray = bytes_Input;
            int index = 0;
            while (index < numArray.Length)
            {
                byte Number = numArray[index];
                if (Conversion.Hex(Number).Length == 1)
                    stringBuilder.Append("0" + Conversion.Hex(Number));
                else
                    stringBuilder.Append("" + Conversion.Hex(Number));
                checked { ++index; }
            }
            return stringBuilder.ToString();
        }

        private static long hexStringToLong(string strHex)
        {
            long num;
            try
            {
                num = checked((long)Math.Round(Conversion.Val("&H" + strHex + "&")));
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                num = checked((long)Math.Round(Conversion.Val("&H" + strHex)));
                ProjectData.ClearProjectError();
            }
            return num;
        }

        public void Extract(string outputpath)
        {
            Tool.CreateDirectoryIfNotExists(outputpath);
            List<Archiver>.Enumerator enumerator;
            try
            {
                enumerator = this.TrophyList.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Archiver current = enumerator.Current;
                    byte[] array = new byte[checked((int)(current.Size - 1L) + 1)];
                    using (FileStream fileStream1 = new FileStream(this._inputfile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        fileStream1.Seek(current.Offset, SeekOrigin.Begin);
                        fileStream1.Read(array, 0, array.Length);
                        using (FileStream fileStream2 = new FileStream(outputpath + Conversions.ToString(Path.DirectorySeparatorChar) + current.Name, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                            fileStream2.Write(array, 0, array.Length);
                    }
                }
            }
            finally
            {
                //enumerator.Dispose();
            }
        }

        public void ExtractFile(string filename, string outputpath, string customename = null)
        {
            Archiver archiver = this.TrophyList.Find((Predicate<Archiver>)(b => Operators.CompareString(Strings.Mid(b.Name.ToUpper(), 1, Strings.Len(filename.ToUpper())), filename.ToUpper(), false) == 0));
            if (archiver == null)
                return;
            Tool.CreateDirectoryIfNotExists(outputpath);
            byte[] array = new byte[checked((int)(archiver.Size - 1L) + 1)];
            using (FileStream fileStream1 = new FileStream(this._inputfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fileStream1.Seek(archiver.Offset, SeekOrigin.Begin);
                fileStream1.Read(array, 0, array.Length);
                using (FileStream fileStream2 = new FileStream(outputpath + Conversions.ToString(Path.DirectorySeparatorChar) + (customename != null ? customename : archiver.Name), FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    fileStream2.Write(array, 0, array.Length);
            }
        }

        public void ExtractFileToMemory(string filename, ref byte[] outputBytes)
        {
            Archiver archiver = this.TrophyList.Find((Predicate<Archiver>)(b => Operators.CompareString(Strings.Mid(b.Name.ToUpper(), 1, Strings.Len(filename.ToUpper())), filename.ToUpper(), false) == 0));
            if (archiver == null)
                return;
            byte[] numArray = new byte[checked((int)(archiver.Size - 1L) + 1)];
            using (FileStream fileStream = new FileStream(this._inputfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fileStream.Seek(archiver.Offset, SeekOrigin.Begin);
                fileStream.Read(numArray, 0, numArray.Length);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Write(numArray, 0, numArray.Length);
                    outputBytes = memoryStream.ToArray();
                }
            }
        }

        public byte[] ExtractFileToMemory(string filename)
        {
            byte[] numArray1 = (byte[])null;
            Archiver archiver = this.TrophyList.Find((Predicate<Archiver>)(b => Operators.CompareString(Strings.Mid(b.Name.ToUpper(), 1, Strings.Len(filename.ToUpper())), filename.ToUpper(), false) == 0));
            if (archiver != null)
            {
                byte[] numArray2 = new byte[checked((int)(archiver.Size - 1L) + 1)];
                using (FileStream fileStream = new FileStream(this._inputfile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    fileStream.Seek(archiver.Offset, SeekOrigin.Begin);
                    fileStream.Read(numArray2, 0, numArray2.Length);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        memoryStream.Write(numArray2, 0, numArray2.Length);
                        numArray1 = memoryStream.ToArray();
                    }
                }
            }
            return numArray1;
        }

        public string CalculateSHA1Hash()
        {
            if (this.Version <= 1)
                return (string)null;
            byte[] numArray1 = new byte[28];
            SHA1CryptoServiceProvider cryptoServiceProvider = new SHA1CryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            using (FileStream fileStream = new FileStream(this._inputfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fileStream.Read(numArray1, 0, numArray1.Length);
                memoryStream.Write(numArray1, 0, numArray1.Length);
                byte[] buffer = new byte[1];
                int num = 0;
                do
                {
                    memoryStream.Write(buffer, 0, buffer.Length);
                    checked { ++num; }
                }
                while (num <= 19);
                fileStream.Seek(48L, SeekOrigin.Begin);
                byte[] numArray2 = new byte[checked((int)(fileStream.Length - 48L - 1L) + 1)];
                fileStream.Read(numArray2, 0, numArray2.Length);
                memoryStream.Write(numArray2, 0, numArray2.Length);
            }
            byte[] hash = cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
            StringBuilder stringBuilder = new StringBuilder();
            byte[] numArray3 = hash;
            int index = 0;
            while (index < numArray3.Length)
            {
                byte num = numArray3[index];
                stringBuilder.Append(num.ToString("X2"));
                checked { ++index; }
            }
            return stringBuilder.ToString();
        }

        public struct TRPHeader
        {
            public byte[] magic;
            public byte[] version;
            public byte[] file_size;
            public byte[] files_count;
            public byte[] element_size;
            public byte[] dev_flag;
            public byte[] sha1;
            public byte[] padding;
        }
    }
}
