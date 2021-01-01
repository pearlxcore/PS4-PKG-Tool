

using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace PS4_PKG_Tool
{
    #region EndianType

    /// <summary>
    /// The two Endian Types that determine the way data is read and written.
    /// </summary>
    public enum EndianType
    {
        /// <summary>
        /// Reversed byte order.
        /// </summary>
        BigEndian,

        /// <summary>
        /// Normal byte order.
        /// </summary>
        LittleEndian
    }

    #endregion

    #region EndianIO

    /// <summary>
    /// A powerful multi-endian reading and writing class.
    /// </summary>
    public class EndianIO : IDisposable
    {
        internal FileMode FileMode;
        internal FileAccess FileAccess;
        internal FileShare FileShare;
        private EndianType _endianType;

        /// <summary>
        /// Gets or sets the Endian Type.
        /// </summary>
        ///
        public EndianType EndianType
        {
            get
            {
                return _endianType;
            }
            set
            {
                _endianType = value;
                if (In != null)
                    In.EndianType = value;
                if (Out != null)
                    Out.EndianType = value;
            }
        }

        /// <summary>
        /// Gets the Endian Reader instance.
        /// </summary>
        public EndianReader In { get; internal protected set; }

        /// <summary>
        /// Gets the Endian Writer instance.
        /// </summary>
        public EndianWriter Out { get; internal protected set; }

        /// <summary>
        /// Gets the file name of the currently open file.
        /// </summary>
        public virtual string FileName { get; internal protected set; }

        /// <summary>
        /// Gets the stream initialized on the file or bytes.
        /// </summary>
        public virtual Stream Stream { get; internal protected set; }

        /// <summary>
        /// Determines whether there are any open streams.
        /// </summary>
        public bool Opened { get; internal protected set; }

        public virtual long Length { get { return this.Stream.Length; } }
        /// <summary>
        /// Initializes a new instance of the EndianIO class.
        /// </summary>
        public EndianIO() { }

        /// <summary>
        /// Initializes a new instance of the EndianIO class on the specified file.
        /// </summary>
        /// <param name="FileName">The location of the file to load.</param>
        /// <param name="EndianType">The Endian Type used to read/write.</param>
        /// <param name="FileMode">System.IO.FileMode</param>
        /// <param name="FileAccess">System.IO.FileAccess</param>
        /// <param name="FileShare">System.IO.FileShare</param>
        public EndianIO(string FileName, EndianType EndianType, FileMode FileMode, FileAccess FileAccess, FileShare FileShare)
        {
            this.FileMode = FileMode;
            this.FileAccess = FileAccess;
            this.FileShare = FileShare;
            this.EndianType = EndianType;
            this.FileName = FileName;
        }
        public EndianIO(string FileName, EndianType EndianType)
            : this(FileName, EndianType, false)
        {
        }
        public EndianIO(string FileName, EndianType EndianType, bool Open)
        {
            this.FileMode = FileMode.OpenOrCreate;
            this.FileAccess = FileAccess.ReadWrite;
            this.FileShare = FileShare.Read;
            this.EndianType = EndianType;
            this.FileName = FileName;
            if (Open)
                this.Open();
        }
        /// <summary>
        /// Initializes a new instance of the EndianIO class on the specified stream.
        /// </summary>
        /// <param name="Stream">The Stream to load.</param>
        /// <param name="EndianType">The Endian Type used to read/write.</param>
        public EndianIO(Stream Stream, EndianType EndianType)
            : this(Stream, EndianType, false)
        {
        }
        public EndianIO(Stream Stream, EndianType EndianType, bool Open)
        {
            this.EndianType = EndianType;
            this.Stream = Stream;

            if (Open) this.Open();
        }
        /// <summary>
        /// Initializes a new instance of the EndianIO class on the specified byte array.
        /// </summary>
        /// <param name="ByteArray">The byte array to load.</param>
        /// <param name="EndianType">The Endian Type used to read/write.</param>
        public EndianIO(byte[] ByteArray, EndianType EndianType)
            : this(ByteArray, EndianType, false)
        {
        }

        public EndianIO(byte[] ByteArray, EndianType EndianType, bool Open)
        {
            this.EndianType = EndianType;
            Stream = new MemoryStream(ByteArray);
            if (Open) this.Open();
        }

        /// <summary>
        /// The destructor for the class.
        /// </summary>
        ~EndianIO()
        {
            try
            {
                // If the file doesn't exist anymore (disconnected device perhaps), this will throw an error on shutdown.
                this.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Opens the IO and initializes the necessary streams. If the IO is already open, this will re-open it.
        /// </summary>
        public virtual void Open()
        {
            if (Opened)
                Close();
            if (Stream != null)
            {
                if (Stream.CanRead)
                {
                    this.In = new EndianReader(Stream, EndianType);
                }
                if (Stream.CanWrite)
                {
                    this.Out = new EndianWriter(Stream, EndianType);
                }
                Opened = true;
            }
            if (FileName != null)
            {
                Stream = new FileStream(FileName, FileMode, FileAccess, FileShare);
                if (FileAccess == FileAccess.Read || FileAccess == FileAccess.ReadWrite)
                {
                    this.In = new EndianReader(Stream, EndianType);
                }
                if (FileAccess == FileAccess.Write || FileAccess == FileAccess.ReadWrite)
                {
                    this.Out = new EndianWriter(Stream, EndianType);
                }
                Opened = true;
            }
        }

        /// <summary>
        /// Closes the IO and any open streams.
        /// </summary>
        public virtual void Close()
        {
            if (Opened)
            {
                if (Stream != null) Stream.Dispose();
                if (In != null) In.Close();
                if (Out != null) Out.Close();
                Opened = false;
            }
        }

        /// <summary>
        /// Sets the positions of the reader and writer using the specified Seek Origin.
        /// </summary>
        /// <param name="Position">Position to set.</param>
        /// <param name="Origin">System.IO.SeekOrigin</param>
        public virtual void SeekTo(long Position, SeekOrigin Origin) { Stream.Seek(Position, Origin); }

        /// <summary>
        /// Sets the positions of the reader and writer.
        /// </summary>
        /// <param name="Position">Position value.</param>
        public virtual void SeekTo(object Position) { SeekTo(Convert.ToInt64(Position), SeekOrigin.Begin); }

        /// <summary>
        /// Returns the stream as an array.
        /// </summary>
        public byte[] ToArray()
        {
            if (Stream.GetType() == typeof(FileStream))
            {
                Stream.Position = 0;
                byte[] buffer = new byte[Stream.Length];
                Stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            return ((MemoryStream)Stream).ToArray();
        }

        /// <summary>
        /// Gets or sets the current position of the IO.
        /// </summary>
        public virtual long Position
        {
            get { return Stream.Position; }
            set { Stream.Position = value; }
        }

        /// <summary>
        /// Closes the streams and release any resources that may be in use.
        /// </summary>
        public void Dispose() { Close(); }

        public override string ToString() { return Path.GetFileName(FileName); }
    }

    #endregion

    #region EndianReader

    /// <summary>
    /// The Endian Reader class that can read data in Big Endian and Little Endian.
    /// </summary>
    public class EndianReader : BinaryReader
    {
        public EndianType EndianType;

        /// <summary>
        /// Initializes a new instance of the EndianReader class on the specified stream in the specified Endian Type.
        /// </summary>
        /// <param name="Input">The input stream to load for reading.</param>
        /// <param name="EndianType">The Endian Type used to read.</param>
        public EndianReader(Stream Input, EndianType EndianType) : base(Input) { this.EndianType = EndianType; }
        public EndianReader(byte[] Data, EndianType EndianType) : this(new MemoryStream(Data), EndianType) { }
        public void SeekTo(object Position)
        {
            SeekTo(Position, SeekOrigin.Begin);
        }
        public virtual void SeekTo(object offset, SeekOrigin SeekOrigin)
        {
            this.BaseStream.Seek(Convert.ToInt64(offset), SeekOrigin);
        }

        public byte[] ReadBytes(object count)
        {
            byte[] buffer = new byte[Convert.ToInt32(count)];
            Read(buffer, 0, buffer.Length);
            return buffer;
        }

        public override byte[] ReadBytes(int count)
        {
            byte[] buffer = new byte[count];
            Read(buffer, 0, count);
            return buffer;
        }

        public override int Read(byte[] buffer, int index, int count)
        {
            return this.BaseStream.Read(buffer, index, count);
        }

        /// <summary>
        /// Reads the specified number of bytes from the current stream into a byte array and advances the current position by that number of bytes.
        /// </summary>
        /// <param name="Count">The number of bytes to read.</param>
        /// <param name="EndianType">Endian Type to read with.</param>
        public byte[] ReadBytes(object Count, EndianType EndianType)
        {
            var array = base.ReadBytes(Convert.ToInt32(Count));
            if (EndianType == EndianType.BigEndian) Array.Reverse(array);
            return array;
        }

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes.
        /// </summary>
        public override short ReadInt16() { return ReadInt16(EndianType); }

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public short ReadInt16(EndianType EndianType) { return BitConverter.ToInt16(ReadBytes(2, EndianType), 0); }

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream and advances the position of the stream by two bytes.
        /// </summary>
        public override ushort ReadUInt16() { return ReadUInt16(EndianType); }

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream and advances the position of the stream by two bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public ushort ReadUInt16(EndianType EndianType) { return BitConverter.ToUInt16(ReadBytes(2, EndianType), 0); }

        /// <summary>
        /// Reads a 3-byte signed integer from the current stream and advances the current position of the stream by three bytes.
        /// </summary>
        public int ReadInt24() { return ReadInt24(EndianType); }

        /// <summary>
        /// Reads a 3-byte signed integer from the current stream and advances the current position of the stream by three bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public int ReadInt24(EndianType EndianType)
        {
            var buffer = base.ReadBytes(3);
            if (EndianType == EndianType.BigEndian) return (buffer[0] << 16) | (buffer[1] << 8) | buffer[2];
            return (buffer[2] << 16) | (buffer[1] << 8) | buffer[0];
        }

        /// <summary>
        /// Reads a 3-byte unsigned integer from the current stream and advances the position of the stream by two bytes.
        /// </summary>
        public uint ReadUInt24() { return ReadUInt24(EndianType); }

        /// <summary>
        /// Reads a 3-byte unsigned integer from the current stream and advances the position of the stream by two bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public uint ReadUInt24(EndianType EndianType) { return (uint)ReadInt24(EndianType); }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the current position of the stream by four bytes.
        /// </summary>
        public override int ReadInt32() { return ReadInt32(EndianType); }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public int ReadInt32(EndianType EndianType) { return BitConverter.ToInt32(ReadBytes(4, EndianType), 0); }

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream and advances the position of the stream by four bytes.
        /// </summary>
        public override uint ReadUInt32() { return ReadUInt32(EndianType); }

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream and advances the position of the stream by four bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public uint ReadUInt32(EndianType EndianType) { return BitConverter.ToUInt32(ReadBytes(4, EndianType), 0); }

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream and advances the current position of the stream by eight bytes.
        /// </summary>
        public override long ReadInt64() { return ReadInt64(EndianType); }

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public long ReadInt64(EndianType EndianType) { return BitConverter.ToInt64(ReadBytes(8, EndianType), 0); }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the position of the stream by eight bytes.
        /// </summary>
        public override ulong ReadUInt64() { return ReadUInt64(EndianType); }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the position of the stream by eight bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public ulong ReadUInt64(EndianType EndianType) { return BitConverter.ToUInt64(ReadBytes(8, EndianType), 0); }

        /// <summary>
        /// Reads an 8-byte floating point value from the current stream and advances the current position of the stream by eight bytes.
        /// </summary>
        public override double ReadDouble() { return ReadDouble(EndianType); }

        /// <summary>
        /// Reads an 8-byte floating point value from the current stream and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public double ReadDouble(EndianType EndianType) { return BitConverter.ToDouble(ReadBytes(8, EndianType), 0); }

        /// <summary>
        /// Reads a 4-byte floating point value from the current stream and advances the current position of the stream by four bytes.
        /// </summary>
        public override float ReadSingle() { return ReadSingle(EndianType); }

        /// <summary>
        /// Reads a 4-byte floating point value from the current stream and advances the current position of the stream by four bytes.
        /// </summary>
        /// <param name="EndianType">Endian Type to read with.</param>
        public float ReadSingle(EndianType EndianType) { return BitConverter.ToSingle(ReadBytes(4, EndianType), 0); }

        /// <summary>
        /// Reads a string from the current stream. The string is prefixed with the length, encoded as an integer seven bits at a time.
        /// </summary>
        /// <param name="Length">Length of the string.</param>
        public string ReadString(int Length) { return Encoding.ASCII.GetString(base.ReadBytes(Length)).Replace("\0", string.Empty); }

        /// <summary>
        /// Reads a unicode string from the current stream. The string is prefixed with the length, encoded as an integer seven bits at a time.
        /// </summary>
        /// <param name="Length">Length of the string.</param>
        public string ReadUnicodeString(int Length) { return Encoding.BigEndianUnicode.GetString(base.ReadBytes(Length * 2)).Replace("\0", string.Empty); }

        public string ReadAsciiString(int Length)
        {
            string str = String.Empty;
            int num = 0;
            for (int i = 0; i < Length; i++)
            {
                char ch = (char)this.ReadByte();
                num++;
                if (ch == '\0')
                    break;
                str += ch;
            }
            this.BaseStream.Seek((long)(Length - num), SeekOrigin.Current);
            return str;
        }

        public string ReadNullTerminatedString()
        {
            char ch;
            string str = String.Empty;
            while ((ch = base.ReadChar()) != '\0')
            {
                if (ch == '\0')
                    return str;
                str += ch;
            }
            return str;
        }

        public string ReadStringNullTerminated()
        {
            string str = String.Empty;
            while (true)
            {
                byte num = this.ReadByte();
                if (num == 0x0)
                    return str;
                str = str + ((char)num);
            }
        }

        public string ReadUnicodeNullTermString()
        {
            string str = String.Empty;
            while (true)
            {
                ushort num = this.ReadUInt16(EndianType.BigEndian);
                if (num == 0x0)
                    return str;
                str += (char)num;
            }
        }

        public ushort SeekNReadUInt16(long Address)
        {
            base.BaseStream.Position = Address;
            return ReadUInt16();
        }

        public uint SeekNReadUInt32(long Address)
        {
            base.BaseStream.Position = Address;
            return ReadUInt32();
        }

        /// <summary>
        /// Reads an image from the current stream and advances the current position of the stream by the size of the image.
        /// </summary>
        /// <param name="Size">Size of the image.</param>
        public Image ParseImage(int Size) { using (var stream = new MemoryStream(base.ReadBytes(Size))) return Image.FromStream(stream); }
    }

    #endregion

    #region EndianWriter

    /// <summary>
    /// The Endian Writer class that can write data in Big Endian and Little Endian.
    /// </summary>
    public class EndianWriter : BinaryWriter
    {
        public EndianType EndianType;

        /// <summary>
        /// Initializes a new instance of the EndianWriter class on the specified stream in the specified Endian Type.
        /// </summary>
        /// <param name="Input">The stream to load for reading.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public EndianWriter(Stream Input, EndianType EndianType) : base(Input) { this.EndianType = EndianType; }
        public EndianWriter(byte[] Buffer, EndianType EndianType) : this(new MemoryStream(Buffer), EndianType) { }

        public virtual void SeekTo(object Position)
        {
            base.BaseStream.Seek(Convert.ToInt64(Position), SeekOrigin.Begin);
        }

        /// <summary>
        /// Writes a byte array to the underlying stream.
        /// </summary>
        /// <param name="Data">Byte array to write</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public virtual void Write(byte[] Data, EndianType EndianType)
        {
            if (EndianType == EndianType.BigEndian)
                Array.Reverse(Data);
            base.Write(Data);
        }

        /// <summary>
        /// Writes a character array to the current stream and advances the current position of the stream in accordance with the Encoding used and the specific characters being written to the stream.
        /// </summary>
        /// <param name="Value">Chars to write.</param>
        public override void Write(char[] Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes a character array to the current stream and advances the current position of the stream in accordance with the Encoding used and the specific characters being written to the stream.
        /// </summary>
        /// <param name="Value">Chars to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(char[] Value, EndianType EndianType)
        {
            if (EndianType == EndianType.BigEndian) { Array.Reverse(Value); }
            base.Write(Value);
        }

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream position by two bytes.
        /// </summary>
        /// <param name="Value">Short to write.</param>
        public override void Write(short Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream position by two bytes.
        /// </summary>
        /// <param name="Value">Short to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(short Value, EndianType EndianType) { Write(BitConverter.GetBytes(Value), EndianType); }

        /// <summary>
        /// Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes.
        /// </summary>
        /// <param name="Value">Ushort to write.</param>
        public override void Write(ushort Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes.
        /// </summary>
        /// <param name="Value">Ushort to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(ushort Value, EndianType EndianType) { Write(BitConverter.GetBytes(Value), EndianType); }

        /// <summary>
        /// Writes a 3-byte signed integer from the current stream and advances the current position of the stream by three bytes.
        /// </summary>
        /// <param name="Value">Int24 to write.</param>
        public void WriteInt24(int Value) { WriteInt24(Value, EndianType); }

        /// <summary>
        /// Writes a 3-byte signed integer from the current stream and advances the current position of the stream by three bytes.
        /// </summary>
        /// <param name="Value">Int24 to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void WriteInt24(int Value, EndianType EndianType)
        {
            var buffer = BitConverter.GetBytes(Value);
            Array.Resize(ref buffer, 3);
            Write(buffer, EndianType);
        }

        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="Value">Int to write.</param>
        public override void Write(int Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="Value">Int to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(int Value, EndianType EndianType) { Write(BitConverter.GetBytes(Value), EndianType); }

        /// <summary>
        /// Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="Value">Uint to write.</param>
        public override void Write(uint Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="Value">Uint to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(uint Value, EndianType EndianType) { Write(BitConverter.GetBytes(Value), EndianType); }

        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="Value">Long to write.</param>
        public override void Write(long Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="Value">Long to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(long Value, EndianType EndianType) { Write(BitConverter.GetBytes(Value), EndianType); }

        /// <summary>
        /// Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="Value">Ulong to write.</param>
        public override void Write(ulong Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="Value">Ulong to write</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(ulong Value, EndianType EndianType) { Write(BitConverter.GetBytes(Value), EndianType); }

        /// <summary>
        /// Writes an eight-byte floating-point value to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="Value">Double to write.</param>
        public override void Write(double Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes an eight-byte floating-point value to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="Value">Double to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(double Value, EndianType EndianType) { Write(BitConverter.GetBytes(Value), EndianType); }

        /// <summary>
        /// Writes a four-byte floating-point value to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="Value">Float to write.</param>
        public override void Write(float Value) { Write(Value, EndianType); }

        /// <summary>
        /// Writes a four-byte floating-point value to the current stream and advances the stream position by four bytes.
        /// </summary>
        /// <param name="Value">Float to write.</param>
        /// <param name="EndianType">Endian Type to write with.</param>
        public void Write(float Value, EndianType EndianType) { Write(BitConverter.GetBytes(Value), EndianType); }

        /// <summary>
        /// Writes a length-prefixed string to this stream in the current encoding of the BinaryWriter, and advances the current position of the stream in accordance with the encoding used and the specific characters being written to the stream.
        /// </summary>
        /// <param name="Value">String to write.</param>
        public override void Write(string Value) { base.Write(Encoding.ASCII.GetBytes(Value)); }

        /// <summary>
        /// Writes a length-prefixed unicode string to this stream in the current encoding of the BinaryWriter, and advances the current position of the stream in accordance with the encoding used and the specific characters being written to the stream.
        /// </summary>
        /// <param name="Value">Unicode string to write.</param>
        public void WriteUnicodeString(string Value) { WriteUnicodeString(Value, Value.Length); }

        /// <summary>
        /// Writes a length-prefixed unicode string to this stream in the current encoding of the BinaryWriter, and advances the current position of the stream in accordance with the encoding used and the specific characters being written to the stream.
        /// </summary>
        /// <param name="Value">Unicode string to write.</param>
        /// <param name="Length">Length of the string. (If it exceed the actual length, null bytes will be written.)</param>
        public void WriteUnicodeString(string Value, int Length)
        {
            int length = Value.Length;
            for (int i = 0; i < length; i++)
            {
                if (i > Length)
                    break;
                ushort num3 = Value[i];
                this.Write(num3, this.EndianType);
            }
            int num4 = (Length - length) * 2;
            if (num4 > 0)
                this.Write(new byte[num4]);
        }
        /// <summary>
        /// Writes an image from the current stream and advances the current position of the stream by the size of the image.
        /// </summary>
        /// <param name="Image">Image to write.</param>
        public void Write(Image Image)
        {
            using (var ms = new MemoryStream())
            {
                Image.Save(ms, Image.RawFormat);
                base.Write(ms.ToArray());
            }
        }

        public void WriteUnicodeNullTermString(string String)
        {
            int length = String.Length;
            for (int i = 0; i < length; i++)
            {
                ushort num = String[i];
                this.Write(num, this.EndianType);
            }
            this.Write(new byte[2]);
        }

        public void WriteUInt24(uint value)
        {
            WriteUInt24(value, this.EndianType);
        }

        public void WriteUInt24(uint value, EndianType endianType)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            Array.Resize(ref buffer, 3);

            if (endianType == EndianType.BigEndian)
                Array.Reverse(buffer);

            base.Write(buffer);
        }

        public void WriteAsciiString(string String, int Length)
        {
            this.WriteAsciiString(String, Length, this.EndianType);
        }

        public void WriteAsciiString(string String, int Length, EndianType EndianType)
        {
            if (String.Length > Length)
                String = String.Substring(0, Length);
            this.Write(System.Text.Encoding.ASCII.GetBytes(String));
            if (String.Length < Length)
                this.Write(new byte[Length - String.Length]);
        }

        public virtual void Write(byte[] Buffer, object offset, object BufferLength)
        {
            base.Write(Buffer, Convert.ToInt32(offset), Convert.ToInt32(BufferLength));
        }

        public virtual void Write(byte[] Buffer, object BufferLength)
        {
            base.Write(Buffer, 0, Convert.ToInt32(BufferLength));
        }

        public virtual void SeekTo(object offset, SeekOrigin SeekOrigin)
        {
            Seek(Convert.ToInt32(offset), SeekOrigin);
        }

        public void SeekNWrite(long position, int Value)
        {
            base.BaseStream.Position = position;
            this.Write(Value);
        }

        public void SeekNWrite(long position, short Value)
        {
            base.BaseStream.Position = position;
            this.Write(Value);
        }

        public void WriteByte(object value)
        {
            long val = Convert.ToInt64(value);
            base.Write((byte)(val & 0xFF));
        }
    }

    #endregion
}