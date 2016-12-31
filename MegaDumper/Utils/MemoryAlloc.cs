namespace Native.Memory
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MemoryAlloc : IDisposable
    {
        private IntPtr _ptr;
        private int _size;

        public MemoryAlloc()
        {
        }

        public MemoryAlloc(int size)
        {
            this._ptr = Marshal.AllocHGlobal(size);
            this._size = size;
        }

        public MemoryAlloc(IntPtr ptr) : this(ptr, 0)
        {
        }

        public MemoryAlloc(IntPtr ptr, int size)
        {
            this._ptr = ptr;
            this._size = size;
        }

        public void Dispose()
        {
            this.Free();
        }

        public void Free()
        {
            if (this.Pointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.Pointer);
            }
        }

        public void IncrementSize(int newBytesCount)
        {
            IntPtr cb = new IntPtr(newBytesCount + this._size);
            this._ptr = Marshal.ReAllocHGlobal(this._ptr, cb);
            this._size = newBytesCount + this._size;
        }

        public static implicit operator int(MemoryAlloc memory)
        {
            return memory.Pointer.ToInt32();
        }

        public static implicit operator long(MemoryAlloc memory)
        {
            return memory.Pointer.ToInt64();
        }

        public static implicit operator IntPtr(MemoryAlloc memory)
        {
            return memory.Pointer;
        }

        public int ReadByte(int offset)
        {
            return this.ReadByte(offset, 0);
        }

        public int ReadByte(int offset, int index)
        {
            return Marshal.ReadByte(this._ptr, offset + (index * 4));
        }

        public byte[] ReadBytes(int length)
        {
            return this.ReadBytes(0, length);
        }

        public byte[] ReadBytes(int offset, int length)
        {
            byte[] buffer = new byte[(length - 1) + 1];
            this.ReadBytes(offset, buffer, 0, length);
            return buffer;
        }

        public void ReadBytes(byte[] buffer, int startIndex, int length)
        {
            this.ReadBytes(0, buffer, startIndex, length);
        }

        public void ReadBytes(int offset, byte[] buffer, int startIndex, int length)
        {
        	this._ptr = new IntPtr(this._ptr.ToInt32() + offset);
            Marshal.Copy(this._ptr, buffer, startIndex, length);
        }

        public int ReadInt32(int offset)
        {
            return this.ReadInt32(offset, 0);
        }

        public int ReadInt32(int offset, int index)
        {
            return Marshal.ReadInt32(this._ptr, offset + (index * 4));
        }

        public IntPtr ReadIntPtr(int offset)
        {
            return this.ReadIntPtr(offset, 0);
        }

        public IntPtr ReadIntPtr(int offset, int index)
        {
            return Marshal.ReadIntPtr(this._ptr, offset + (index * IntPtr.Size));
        }

        public T ReadStruct<T>()
        {
            return this.ReadStruct<T>(0);
        }

        public T ReadStruct<T>(int index)
        {
            return this.ReadStruct<T>(0, index);
        }

        public T ReadStruct<T>(int offset, int index)
        {
        	this._ptr = new IntPtr((int) (offset + (Marshal.SizeOf(typeof(T)) * index)) + this._ptr.ToInt32());
            return (T) Marshal.PtrToStructure(this._ptr, typeof(T));
        }

        public T ReadStructOffset<T>(int offset)
        {
        	this._ptr = new IntPtr(this._ptr.ToInt32()+offset);
            return (T) Marshal.PtrToStructure(this._ptr, typeof(T));
        }

        public uint ReadUInt32(int offset)
        {
            return this.ReadUInt32(offset, 0);
        }

        public uint ReadUInt32(int offset, int index)
        {
            return (uint) this.ReadInt32(offset, index);
        }

        public void Resize(int newSize)
        {
            IntPtr cb = new IntPtr(newSize);
            this._ptr = Marshal.ReAllocHGlobal(this._ptr, cb);
            this._size = newSize;
        }

        public void WriteByte(int offset, byte b)
        {
            Marshal.WriteByte((IntPtr) this, offset, b);
        }

        public void WriteBytes(int offset, byte[] b)
        {
        	this._ptr = new IntPtr(this._ptr.ToInt32()+offset);
            Marshal.Copy(b, 0, this._ptr, b.Length);
        }

        public void WriteInt16(int offset, short i)
        {
            Marshal.WriteInt16((IntPtr) this, offset, i);
        }

        public void WriteInt32(int offset, int i)
        {
            Marshal.WriteInt32((IntPtr) this, offset, i);
        }

        public void WriteIntPtr(int offset, IntPtr i)
        {
            Marshal.WriteIntPtr((IntPtr) this, offset, i);
        }

        public void WriteStruct<T>(T s)
        {
            this.WriteStruct<T>(0, s);
        }

        public void WriteStruct<T>(int index, T s)
        {
            this.WriteStruct<T>(0, index, s);
        }

        public void WriteStruct<T>(int offset, int index, T s)
        {
        	this._ptr = new IntPtr(this._ptr.ToInt32()+(int) (offset + (Marshal.SizeOf(typeof(T)) * index)));
            Marshal.StructureToPtr(s, this._ptr, false);
        }

        public void WriteUnicodeString(int offset, string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            int num = bytes.Length - 1;
            for (int i = 0; i <= num; i++)
            {
                Marshal.WriteByte(this.Pointer, offset + i, bytes[i]);
            }
        }

        public IntPtr Pointer
        {
            get
            {
                return this._ptr;
            }
            set
            {
                this._ptr = value;
            }
        }

        public int Size
        {
            get
            {
                return this._size;
            }
            set
            {
                this._size = value;
            }
        }
    }
}

