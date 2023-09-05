using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TRPViewer;

namespace PS4_Trophy_xdpx
{
    public class Trophy_File
    {
        /*SHA1*/
        private string SHA1;//SHA1 PlaceHolder
        private byte[] Bytes;//Bytes Placeholder

        private bool Readbytes; //Bool Read Bytes

        TrophyHeader trphy = new TrophyHeader(); //Trophy Header Object

        /// <summary>
        /// How Many Files Are In The Trophy File
        /// </summary>
        public int FileCount
        {
            get
            {
                return checked((int)Utils.byteArrayToLittleEndianInteger(trphy.files_count));
            }
        }

        /// <summary>
        /// Version of the Trophy File
        /// </summary>
        public int Version
        {
            get
            {
                return checked((int)Utils.byteArrayToLittleEndianInteger(trphy.version));
            }
        }

        /// <summary>
        /// Trophy header Structure
        /// </summary>
        public struct TrophyHeader
        {
            public byte[] magic;//Magic

            public byte[] version;//Version Of Trophy Header File

            public byte[] file_size;//File Size

            public byte[] files_count;//File Counts

            public byte[] element_size;//Elements Size

            public byte[] dev_flag;//Is a Dev Trophy File

            public byte[] sha1;//SHA1 Hash

            public byte[] padding;//Padding 
        }
        /*Trophy items*/
        public class TrophyItem
        {
            public TrophyItem(int Index, string Name, uint Offset, ulong Size, byte[] TotalBytes)
            {
                this.Index = Index;
                this.Name = Name;
                this.Size = checked((long)Size);
                this.Offset = (long)((ulong)Offset);
                this.TotalBytes = TotalBytes;
            }
            /*Index number of trophy item*/
            public int Index;
            /*Name of trophy item*/
            public string Name;
            /*offset as long*/
            public long Offset;
            /*Size*/
            public long Size;
            /*Total Bytes*/
            public byte[] TotalBytes;
        }

        /*Trophy Files have multiple Items*/
        public List<TrophyItem> trophyItemList = new List<TrophyItem>();
        private bool _iserror;
        private string _error;
        private string _inputfile;
        private string _calculatedsha1;

        private TrophyHeader LoadHeader(Stream fs)
        {
            TrophyHeader hdr = default(TrophyHeader);
            hdr.magic = new byte[4];
            hdr.version = new byte[4];
            hdr.file_size = new byte[8];
            hdr.files_count = new byte[4];
            hdr.element_size = new byte[4];
            hdr.dev_flag = new byte[4];
            hdr.sha1 = new byte[20];
            hdr.padding = new byte[36];
            fs.Read(hdr.magic, 0, hdr.magic.Length);
            fs.Read(hdr.version, 0, hdr.version.Length);
            fs.Read(hdr.file_size, 0, hdr.file_size.Length);
            fs.Read(hdr.files_count, 0, hdr.files_count.Length);
            fs.Read(hdr.element_size, 0, hdr.element_size.Length);
            fs.Read(hdr.dev_flag, 0, hdr.dev_flag.Length);
            long num = Utils.byteArrayToLittleEndianInteger(hdr.version);
            if (num <= 3L && num >= 1L)
            {
                switch ((int)(num - 1L))
                {
                    case 0:
                        fs.Read(hdr.padding, 0, hdr.padding.Length);
                        break;
                    case 1:
                        fs.Read(hdr.sha1, 0, hdr.sha1.Length);
                        hdr.padding = new byte[16];
                        fs.Read(hdr.padding, 0, hdr.padding.Length);
                        break;
                    case 2:
                        fs.Read(hdr.sha1, 0, hdr.sha1.Length);
                        hdr.padding = new byte[48];
                        fs.Read(hdr.padding, 0, hdr.padding.Length);
                        break;
                }
            }
            return hdr;
        }

        private void ReadContent(Stream fs)
        {
            byte[] array = new byte[36];
            byte[] array2 = new byte[4];
            byte[] array3 = new byte[8];
            byte[] array4 = new byte[4];
            int num = 0;
            checked
            {
                int num2 = this.FileCount - 1;
                int i = num;
                while (i <= num2)
                {
                    fs.Read(array, 0, array.Length);
                    fs.Read(array2, 0, array2.Length);
                    fs.Read(array3, 0, array3.Length);
                    fs.Read(array4, 0, array4.Length);
                    fs.Seek(12L, SeekOrigin.Current);
                    long position = fs.Position;
                    string name = Utils.byteArrayToUTF8String(array).Replace("\0", null);
                    long num3 = Utils.hexStringToLong(Utils.byteArrayToHexString(array2));
                    long num4 = Utils.hexStringToLong(Utils.byteArrayToHexString(array3));
                    if (this.Readbytes)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(this.Bytes))
                        {
                            byte[] array5 = new byte[(int)(num4 - 1L) + 1];
                            memoryStream.Seek(num3, SeekOrigin.Begin);
                            memoryStream.Read(array5, 0, array5.Length);
                            this.trophyItemList.Add(new TrophyItem(i, name, (uint)num3, (ulong)num4, array5));
                            goto IL_124;
                        }
                        goto IL_10C;
                    }
                    goto IL_10C;
                IL_124:
                    i++;
                    continue;
                IL_10C:
                    this.trophyItemList.Add(new TrophyItem(i, name, (uint)num3, (ulong)num4, null));
                    goto IL_124;
                }
            }
        }

        private string CalculateSHA1Hash()
        {
            checked
            {
                if (this.Version > 1)
                {
                    byte[] array = new byte[28];
                    SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
                    MemoryStream memoryStream = new MemoryStream();
                    using (MemoryStream memoryStream2 = new MemoryStream(this.Bytes))
                    {
                        memoryStream2.Read(array, 0, array.Length);
                        memoryStream.Write(array, 0, array.Length);
                        array = new byte[1];
                        int num = 0;
                        do
                        {
                            memoryStream.Write(array, 0, array.Length);
                            num++;
                        }
                        while (num <= 19);
                        memoryStream2.Seek(48L, SeekOrigin.Begin);
                        array = new byte[(int)(memoryStream2.Length - 48L - 1L) + 1];
                        memoryStream2.Read(array, 0, array.Length);
                        memoryStream.Write(array, 0, array.Length);
                    }
                    byte[] array2 = sha1CryptoServiceProvider.ComputeHash(memoryStream.ToArray());
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (byte b in array2)
                    {
                        stringBuilder.Append(b.ToString("X2"));
                    }
                    return stringBuilder.ToString();
                }
                return null;
            }
        }

        /// <summary>
        /// This method should create a blank trophy file
        /// </summary>
        public Trophy_File()
        {
            /*Load a blank tropy file ?*/
        }

        /// <summary>
        /// Method Will Create a Trohy File From a File Path
        /// </summary>
        /// <param name="FilePath">File Location on disk</param>
        public Trophy_File(string FilePath)
        {
            this.SHA1 = "";
            this.trophyItemList = new List<TrophyItem>();
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fileStream.Read(Bytes, 0, checked((int)fileStream.Length));
                fileStream.Seek(0L, SeekOrigin.Begin);
                TrophyHeader hdr = LoadHeader(fileStream);
                trphy = hdr;
                if (Utils.ByteArraysEqual(hdr.magic, new byte[] { 220, 162, 77, 0 }))
                {
                    throw new Exception("This file is not supported!");
                }
                ReadContent(fileStream);
                if (Version > 1)
                {
                    SHA1 = CalculateSHA1Hash();
                }
            }
            //ShowInformation(this._error, "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static long byteArrayToLittleEndianInteger(byte[] bits)
        {
            return (long)((uint)bits[0] | (uint)(byte)((uint)bits[1] << 0) | (uint)(byte)((uint)bits[2] << 0) | (uint)(byte)((uint)bits[3] << 0));
        }

        public static string byteArrayToUTF8String(byte[] _byte)
        {
            return Encoding.UTF8.GetString(_byte);
        }

        public static long hexStringToLong(string strHex)
        {
            long num;
            try
            {
                num = checked((long)Math.Round(Conversion.Val("&H" + strHex + "&")));
            }
            catch (Exception ex)
            {
                num = checked((long)Math.Round(Conversion.Val("&H" + strHex)));
            }
            return num;
        }

        private List<Archiver> _trophyList;

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
            switch (byteArrayToLittleEndianInteger(trpHeader.version))
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

        private TRPReader.TRPHeader _hdr;
        private bool _throwerror;
        internal bool IsError;

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
                    //if (!this.ByteArraysEqual(this._hdr.magic, this._hdrmagic))
                    //    throw new Exception("This file is not supported!");
                    this.ReadContent(fs);
                    if (this.Version > 1)
                        this._calculatedsha1 = this.CalculateSHA1Hash();
                }

            }
            catch (Exception ex)
            {
                Exception exception = ex;
                this._iserror = true;
                this._error = exception.Message;
            }
            if (this._iserror && this._throwerror)
                throw new Exception(this._error);
            if (!this._iserror || this._throwerror)
                return;


        }

        public static string byteArrayToHexString(byte[] bytes_Input)
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



        public Trophy_File Load(byte[] bytes)
        {
            Trophy_File rtn = new Trophy_File();
            try
            {
                this.trophyItemList = new List<TrophyItem>();
                this.Bytes = bytes;
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    TrophyHeader hdr = LoadHeader(memoryStream);
                    trphy = hdr;
                    if (!Utils.ByteArraysEqual(hdr.magic, new byte[] { 220, 162, 77, 0 }))
                    {
                        throw new Exception("This file is not supported!");
                    }
                    ReadContent(memoryStream);
                    if (Version > 1)
                    {
                        SHA1 = CalculateSHA1Hash();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            rtn.Bytes = Bytes;
            rtn.SHA1 = SHA1;
            rtn.trphy = trphy;
            rtn.trophyItemList = trophyItemList;
            return rtn;
        }

        public byte[] ExtractFileToMemory(string filename)
        {
            byte[] result = null;
            //TrophyItem archiver = this.trophyItemList.Find((TrophyItem b) => Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Mid(b.Name.ToUpper(), 1, Microsoft.VisualBasic.Strings.Len(filename.ToUpper())), filename.ToUpper(), false) == 0);
            TrophyItem archiver = this.trophyItemList.Find((TrophyItem b) => b.Name == filename);

            if (archiver != null)
            {
                byte[] array = new byte[checked((int)(archiver.Size - 1L) + 1)];
                using (MemoryStream memoryStream = new MemoryStream(this.Bytes))
                {
                    memoryStream.Seek(archiver.Offset, SeekOrigin.Begin);
                    memoryStream.Read(array, 0, array.Length);
                    using (MemoryStream memoryStream2 = new MemoryStream())
                    {
                        memoryStream2.Write(array, 0, array.Length);
                        result = memoryStream2.ToArray();
                    }
                }
            }
            return result;
        }

    }

}
