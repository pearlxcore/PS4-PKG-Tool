// Decompiled with JetBrains decompiler
// Type: TRPViewer.TRPCreator
// Assembly: TRPViewer, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC455631-E5E4-4FE8-BC3B-A8476C3FB343
// Assembly location: C:\Users\pearlxcore\Desktop\TRPViewer(1).exe

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TRPViewer
{
  public class TRPCreator
  {
    private TRPCreator.TRPHeader _hdr;
    private List<Archiver> _trophyList;
    private byte[] _hdrmagic;
    private bool _iserror;
    private int _setversion;

    public int SetVersion
    {
      get
      {
        return this._setversion;
      }
      set
      {
        this._setversion = value;
      }
    }

    public TRPCreator()
    {
      this._hdr = new TRPCreator.TRPHeader();
      this._trophyList = new List<Archiver>();
      this._hdrmagic = new byte[4]
      {
        (byte) 220,
        (byte) 162,
        (byte) 77,
        (byte) 0
      };
    }

    public void Create(string filename, ArrayList contents)
    {
      if (this._setversion < 1 | this._setversion > 3)
        throw new Exception("File version must be one of these { 1, 2, 3 }.");
      this._trophyList.Clear();
      contents = this.SortList(contents);
      MemoryStream memoryStream = new MemoryStream();
      int num1 = 0;
      int count = contents.Count;
      ulong num2 = checked ((ulong) (64 * contents.Count));
      int num3 = checked (contents.Count - 1);
      int m_Index = 0;
      while (m_Index <= num3)
      {
        string path = contents[m_Index].ToString();
        string fileName = Path.GetFileName(path);
        byte[] m_Bytes = File.ReadAllBytes(path);
        ulong length = checked ((ulong) m_Bytes.Length);
        short pads = this.GetPads(checked ((long) length), (short) 16);
        checked { num1 += (int) pads; }
        this._trophyList.Add(new Archiver(m_Index, fileName, Convert.ToUInt32(Decimal.Add(new Decimal(num2), new Decimal(this._setversion == 3 ? 96 : 64))), length, m_Bytes));
        num2 = Convert.ToUInt64(Decimal.Add(new Decimal(num2), Decimal.Add(new Decimal(length), new Decimal((int) pads))));
        checked { ++m_Index; }
      }
      ulong size = this.GetSize();
      byte[] header = this.GetHeader(this._setversion, checked ((long) Convert.ToUInt64(Decimal.Add(Decimal.Add(new Decimal((unchecked (this._setversion == 3) ? 96 : 64) + this.GetHeaderFiles().Length), new Decimal(size)), new Decimal(num1)))), count, 64, 0, (string) null);
      memoryStream.Write(header, 0, header.Length);
      byte[] headerFiles = this.GetHeaderFiles();
      memoryStream.Write(headerFiles, 0, headerFiles.Length);
      byte[] bytes1 = this.GetBytes();
      memoryStream.Write(bytes1, 0, bytes1.Length);
      if (this._setversion > 1)
      {
        byte[] bytes2 = this.HexStringToBytes(this.CalculateSHA1Hash(memoryStream.ToArray()));
        memoryStream.Seek(28L, SeekOrigin.Begin);
        memoryStream.Write(bytes2, 0, bytes2.Length);
      }
      File.WriteAllBytes(filename, memoryStream.ToArray());
    }

    public void CreateFromList(string filename, List<Archiver> contents)
    {
      if (this._setversion < 1 | this._setversion > 3)
        throw new Exception("File version must be one of these { 1, 2, 3 }.");
      this._trophyList.Clear();
      MemoryStream memoryStream = new MemoryStream();
      int num1 = 0;
      int count = contents.Count;
      ulong num2 = checked ((ulong) (64 * contents.Count));
      int num3 = checked (contents.Count - 1);
      int m_Index = 0;
      while (m_Index <= num3)
      {
        string name = contents[m_Index].Name;
        byte[] bytes = contents[m_Index].Bytes;
        ulong size = checked ((ulong) contents[m_Index].Size);
        short pads = this.GetPads(checked ((long) size), (short) 16);
        checked { num1 += (int) pads; }
        this._trophyList.Add(new Archiver(m_Index, name, Convert.ToUInt32(Decimal.Add(new Decimal(num2), new Decimal(this._setversion == 3 ? 96 : 64))), size, bytes));
        num2 = Convert.ToUInt64(Decimal.Add(new Decimal(num2), Decimal.Add(new Decimal(size), new Decimal((int) pads))));
        checked { ++m_Index; }
      }
      ulong size1 = this.GetSize();
      byte[] header = this.GetHeader(this._setversion, checked ((long) Convert.ToUInt64(Decimal.Add(Decimal.Add(new Decimal((unchecked (this._setversion == 3) ? 96 : 64) + this.GetHeaderFiles().Length), new Decimal(size1)), new Decimal(num1)))), count, 64, 0, (string) null);
      memoryStream.Write(header, 0, header.Length);
      byte[] headerFiles = this.GetHeaderFiles();
      memoryStream.Write(headerFiles, 0, headerFiles.Length);
      byte[] bytes1 = this.GetBytes();
      memoryStream.Write(bytes1, 0, bytes1.Length);
      if (this._setversion > 1)
      {
        byte[] bytes2 = this.HexStringToBytes(this.CalculateSHA1Hash(memoryStream.ToArray()));
        memoryStream.Seek(28L, SeekOrigin.Begin);
        memoryStream.Write(bytes2, 0, bytes2.Length);
      }
      File.WriteAllBytes(filename, memoryStream.ToArray());
    }

    private ArrayList SortList(ArrayList alist)
    {
      string[] strArray1 = new string[8]
      {
        "TROPCONF.(E?)SFM",
        "TROP.(E?)SFM",
        "TROP_\\d+.(E?)SFM",
        "ICON0.PNG",
        "ICON0_\\d+.PNG",
        "GR\\d+.PNG",
        "GR\\d+_\\d+.PNG",
        "TROP\\d+.PNG"
      };
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      ArrayList arrayList3 = new ArrayList();
      ArrayList arrayList4 = new ArrayList();
      ArrayList arrayList5 = new ArrayList();
      string[] strArray2 = strArray1;
      int index1 = 0;
      while (index1 < strArray2.Length)
      {
        string pattern = strArray2[index1];
        int num = checked (alist.Count - 1);
        int index2 = 0;
        while (index2 <= num)
        {
          if (Regex.Match(Path.GetFileName(alist[index2].ToString()), pattern, RegexOptions.IgnoreCase).Success)
          {
            if (Path.GetFileName(alist[index2].ToString()).ToUpper().StartsWith("TROPCONF"))
              arrayList1.Add((object) alist[index2].ToString());
            else if (Path.GetFileName(alist[index2].ToString()).ToUpper().EndsWith("SFM"))
              arrayList2.Add((object) alist[index2].ToString());
            else if (Path.GetFileName(alist[index2].ToString()).ToUpper().StartsWith("ICON"))
              arrayList3.Add((object) alist[index2].ToString());
            else if (Path.GetFileName(alist[index2].ToString()).ToUpper().StartsWith("GR"))
              arrayList4.Add((object) alist[index2].ToString());
            else if (Path.GetFileName(alist[index2].ToString()).ToUpper().EndsWith("PNG"))
              arrayList5.Add((object) alist[index2].ToString());
          }
          checked { ++index2; }
        }
        checked { ++index1; }
      }
      arrayList2.Sort();
      arrayList3.Sort();
      arrayList4.Sort();
      arrayList5.Sort();
      arrayList1.AddRange((ICollection) arrayList2);
      arrayList1.AddRange((ICollection) arrayList3);
      arrayList1.AddRange((ICollection) arrayList4);
      arrayList1.AddRange((ICollection) arrayList5);
      return arrayList1;
    }

    private byte[] GetHeaderFiles()
    {
      byte[] buffer1 = new byte[16]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 1,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
      byte[] buffer2 = new byte[16]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 3,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
      MemoryStream memoryStream = new MemoryStream();
      int num = checked (this._trophyList.Count - 1);
      int index = 0;
      while (index <= num)
      {
        byte[] bytes1 = Encoding.ASCII.GetBytes(this._trophyList[index].Name);
        Array.Resize<byte>(ref bytes1, 32);
        memoryStream.Write(bytes1, 0, bytes1.Length);
        byte[] bytes2 = BitConverter.GetBytes(this._trophyList[index].Offset);
        Array.Reverse((Array) bytes2);
        memoryStream.Write(bytes2, 0, bytes2.Length);
        byte[] bytes3 = BitConverter.GetBytes(this._trophyList[index].Size);
        Array.Reverse((Array) bytes3);
        memoryStream.Write(bytes3, 0, bytes3.Length);
        if (this._trophyList[index].Name.ToUpper().EndsWith(".SFM"))
          memoryStream.Write(buffer1, 0, buffer1.Length);
        else if (this._trophyList[index].Name.ToUpper().EndsWith(".ESFM"))
        {
          memoryStream.Write(buffer2, 0, buffer2.Length);
        }
        else
        {
          byte[] array = new byte[1];
          Array.Resize<byte>(ref array, 16);
          memoryStream.Write(array, 0, array.Length);
        }
        checked { ++index; }
      }
      return memoryStream.ToArray();
    }

    private ulong GetSize()
    {
      int num = checked (this._trophyList.Count - 1);
      int index = 0;
      ulong uint64 = 0;
      while (index <= num)
      {
        uint64 = Convert.ToUInt64(Decimal.Add(new Decimal(uint64), new Decimal(this._trophyList[index].Size)));
        checked { ++index; }
      }
      return uint64;
    }

    private byte[] GetBytes()
    {
      MemoryStream memoryStream = new MemoryStream();
      int num = checked (this._trophyList.Count - 1);
      int index = 0;
      while (index <= num)
      {
        byte[] bytes = this._trophyList[index].Bytes;
        memoryStream.Write(bytes, 0, bytes.Length);
        short pads = this.GetPads((long) bytes.Length, (short) 16);
        if (pads >= (short) 0)
        {
          byte[] buffer = new byte[checked ((int) pads - 1 + 1)];
          memoryStream.Write(buffer, 0, buffer.Length);
        }
        checked { ++index; }
      }
      return memoryStream.ToArray();
    }

    private ulong GetFilesSize(ArrayList contents)
    {
      int num = checked (contents.Count - 1);
      int index = 0;
      ulong uint64 = 0;
      while (index <= num)
      {
        uint64 = Convert.ToUInt64(Decimal.Add(new Decimal(uint64), new Decimal(new FileInfo(contents[index].ToString()).Length)));
        checked { ++index; }
      }
      return uint64;
    }

    private byte[] GetFilesBytes(ArrayList contents)
    {
      MemoryStream memoryStream = new MemoryStream();
      int num = checked (contents.Count - 1);
      int index = 0;
      while (index <= num)
      {
        byte[] buffer = File.ReadAllBytes(contents[index].ToString());
        memoryStream.Write(buffer, 0, buffer.Length);
        checked { ++index; }
      }
      return memoryStream.ToArray();
    }

    private byte[] GetFilesHeaderBytes(ArrayList contents)
    {
      MemoryStream memoryStream = new MemoryStream();
      int num = checked (contents.Count - 1);
      int index = 0;
      while (index <= num)
      {
        byte[] buffer = File.ReadAllBytes(contents[index].ToString());
        memoryStream.Write(buffer, 0, buffer.Length);
        checked { ++index; }
      }
      return memoryStream.ToArray();
    }

    private byte[] GetHeader(
      int version,
      long file_size,
      int files_count,
      int element_size,
      int dev_flag,
      string sha1)
    {
      TRPCreator.TRPHeader trpHeader = new TRPCreator.TRPHeader();
      MemoryStream memoryStream = new MemoryStream();
      trpHeader.magic = new byte[4];
      trpHeader.version = new byte[4];
      trpHeader.file_size = new byte[8];
      trpHeader.files_count = new byte[4];
      trpHeader.element_size = new byte[4];
      trpHeader.dev_flag = new byte[4];
      trpHeader.sha1 = new byte[20];
      trpHeader.padding = new byte[36];
      trpHeader.magic = this._hdrmagic;
      memoryStream.Write(trpHeader.magic, 0, trpHeader.magic.Length);
      trpHeader.version = BitConverter.GetBytes(version);
      Array.Reverse((Array) trpHeader.version);
      memoryStream.Write(trpHeader.version, 0, trpHeader.version.Length);
      trpHeader.file_size = BitConverter.GetBytes(file_size);
      Array.Reverse((Array) trpHeader.file_size);
      memoryStream.Write(trpHeader.file_size, 0, trpHeader.file_size.Length);
      trpHeader.files_count = BitConverter.GetBytes(files_count);
      Array.Reverse((Array) trpHeader.files_count);
      memoryStream.Write(trpHeader.files_count, 0, trpHeader.files_count.Length);
      trpHeader.element_size = BitConverter.GetBytes(element_size);
      Array.Reverse((Array) trpHeader.element_size);
      memoryStream.Write(trpHeader.element_size, 0, trpHeader.element_size.Length);
      trpHeader.dev_flag = BitConverter.GetBytes(dev_flag);
      Array.Reverse((Array) trpHeader.dev_flag);
      memoryStream.Write(trpHeader.dev_flag, 0, trpHeader.dev_flag.Length);
      switch (version)
      {
        case 1:
        case 2:
          memoryStream.Write(trpHeader.padding, 0, trpHeader.padding.Length);
          break;
        case 3:
          trpHeader.padding = new byte[20];
          memoryStream.Write(trpHeader.padding, 0, trpHeader.padding.Length);
          trpHeader.padding = new byte[48]
          {
            (byte) 48,
            (byte) 49,
            (byte) 48,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 0
          };
          memoryStream.Write(trpHeader.padding, 0, trpHeader.padding.Length);
          break;
      }
      this._hdr = trpHeader;
      return memoryStream.ToArray();
    }

    public List<Archiver> TrophyList
    {
      get
      {
        return this._trophyList;
      }
    }

    private string CalculateSHA1Hash(byte[] _byte)
    {
      byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(_byte);
      StringBuilder stringBuilder = new StringBuilder();
      byte[] numArray = hash;
      int index = 0;
      while (index < numArray.Length)
      {
        byte num = numArray[index];
        stringBuilder.Append(num.ToString("X2"));
        checked { ++index; }
      }
      return stringBuilder.ToString();
    }

    private byte[] HexStringToBytes(string strInput)
    {
      if (!this.HexStringIsValid(strInput))
        return (byte[]) null;
      int num1 = strInput.Length / 2;
      byte[] numArray = new byte[checked (num1 - 1 + 1)];
      int num2 = checked (num1 - 1);
      int index = 0;
      while (index <= num2)
      {
        numArray[index] = Convert.ToByte(strInput.Substring(checked (index * 2), 2), 16);
        checked { ++index; }
      }
      return numArray;
    }

    private string BytesToHexString(byte[] bytes_Input)
    {
      StringBuilder stringBuilder = new StringBuilder(checked (bytes_Input.Length * 2));
      byte[] numArray = bytes_Input;
      int index = 0;
      while (index < numArray.Length)
      {
        byte num = numArray[index];
        stringBuilder.Append(num.ToString("X02"));
        checked { ++index; }
      }
      return stringBuilder.ToString();
    }

    private bool HexStringIsValid(string Hex)
    {
      string str = Hex;
      int index = 0;
      int length = str.Length;
      while (index < length)
      {
        if ("0123456789ABCDEFabcdef".IndexOf(Conversions.ToString(str[index]), 0) == -1)
          return false;
        checked { ++index; }
      }
      return true;
    }

    private short GetPads(long fsize, short align = 16)
    {
      int num = 0;
      int result;
      while (!int.TryParse(Conversions.ToString((double) checked (fsize + (long) num) / (double) align), out result))
        checked { ++num; }
      return checked ((short) num);
    }

    private struct TRPHeader
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
