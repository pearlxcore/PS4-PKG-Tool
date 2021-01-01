// Decompiled with JetBrains decompiler
// Type: TRPViewer.Archiver
// Assembly: TRPViewer, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC455631-E5E4-4FE8-BC3B-A8476C3FB343
// Assembly location: C:\Users\pearlxcore\Desktop\TRPViewer(1).exe

namespace TRPViewer
{
  public class Archiver
  {
    public int Index;
    public string Name;
    public long Offset;
    public long Size;
    public byte[] Bytes;

    public Archiver(int m_Index, string m_Name, uint m_Offset, ulong m_Size, byte[] m_Bytes)
    {
      this.Index = m_Index;
      this.Name = m_Name;
      this.Size = checked ((long) m_Size);
      this.Offset = (long) m_Offset;
      this.Bytes = m_Bytes;
    }
  }
}
