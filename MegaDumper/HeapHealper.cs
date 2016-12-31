using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Mega_Dumper
{
	

	public static class HeapHealper
	{
		
        public const uint TH32CS_SNAPHEAPLIST = 0x00000001;
        public const uint TH32CS_SNAPPROCESS = 0x00000002;
        public const uint TH32CS_SNAPTHREAD = 0x00000004;
        public const uint TH32CS_SNAPMODULE = 0x00000008;
        public const uint TH32CS_SNAPMODULE32 = 0x00000010;
        public const uint TH32CS_SNAPALL = (TH32CS_SNAPHEAPLIST |
                                                 TH32CS_SNAPPROCESS |
                                                 TH32CS_SNAPTHREAD |
                                                 TH32CS_SNAPMODULE);
        public const uint TH32CS_INHERIT = 0x80000000;
        
		public const uint HF32_DEFAULT = 1;
        public const uint HF32_SHARED = 2; 

		public const uint LF32_FIXED = 0x00000001;
		public const uint LF32_FREE = 0x00000002;
		public const uint LF32_MOVEABLE = 0x00000004;

        public struct HEAPLIST32
        {
            public uint dwSize;
			public uint th32ProcessID;
			public uint th32HeapID;
			public uint dwFlags; 
        }

        public struct HEAPENTRY32
        {
			public uint dwSize;
			public IntPtr hHandle; 
			public uint dwAddress; 
			public uint dwBlockSize;
			public uint dwFlags;
			public uint dwLockCount;
			public uint dwResvd;
			public uint th32ProcessID;
			public uint th32HeapID; 
        }


        public struct PROCESSENTRY32W
        {
			public uint dwSize;
			public uint cntUsage;
			public uint th32ProcessID; 
			public UIntPtr th32DefaultHeapID;
			public uint th32ModuleID; 
			public uint cntThreads;
			public uint th32ParentProcessID; 
			public int pcPriClassBase; 
			public uint dwFlags;
			public string szExeFile; 
        }

        public struct PROCESSENTRY32
        {
			public uint dwSize;
			public uint cntUsage;
			public uint th32ProcessID;
			public uint th32DefaultHeapID;
			public uint th32ModuleID; 
			public uint cntThreads;
			public uint th32ParentProcessID; 
			public int pcPriClassBase; 
			public uint dwFlags;
			public string szExeFile; 
        }

        public struct THREADENTRY32
        {
			public uint dwSize;
			public uint cntUsage;
			public uint th32ThreadID;            
			public uint th32OwnerProcessID;      
			public int tpBasePri;
			public int tpDeltaPri;
			public uint dwFlags;
        }

        public struct MODULEENTRY32W
        {
			public uint dwSize;
			public uint th32ModuleID;  
			public uint th32ProcessID; 
			public uint GlblcntUsage;  
			public uint ProccntUsage;  
			public IntPtr modBaseAddr; 
			public uint modBaseSize;   
			public IntPtr hModule;     
			public string szModule;
			public string szExePath;
        }

        public struct MODULEENTRY32
        {
			public uint dwSize;
			public uint th32ModuleID; 
			public uint th32ProcessID;
			public uint GlblcntUsage; 
			public uint ProccntUsage; 
			public IntPtr modBaseAddr;
			public uint modBaseSize;  
			public IntPtr hModule;    
			public string szModule;
			public string szExePath;
        }

		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

		[DllImport("kernel32.dll")]
		public static extern bool CloseHandle(IntPtr hHeapSnapshot);

		[DllImport("kernel32.dll")]
		public static extern bool Heap32ListFirst(IntPtr hSnapshot, ref HEAPLIST32 lphl);

		[DllImport("kernel32.dll")]
		public static extern bool Heap32ListNext(IntPtr hSnapshot, ref HEAPLIST32 lphl);

		[DllImport("kernel32.dll")]
		public static extern bool Heap32First(ref HEAPENTRY32 lphe,
			uint th32ProcessID, uint th32HeapID);

		[DllImport("kernel32.dll")]
		public static extern bool Heap32Next(ref HEAPENTRY32 lphe);

		[DllImport("kernel32.dll")]
		public static extern bool Toolhelp32ReadProcessMemory(uint th32ProcessID,
			IntPtr lpBaseAddress, IntPtr lpBuffer, uint cbRead, IntPtr lpNumberOfBytesRead);

		[DllImport("kernel32.dll")]
		public static extern bool Process32FirstW(IntPtr hSnapshot, ref PROCESSENTRY32W lppe);

		[DllImport("kernel32.dll")]
		public static extern bool Process32NextW(IntPtr hSnapshot, ref PROCESSENTRY32W lppe);

		[DllImport("kernel32.dll")]
		public static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[DllImport("kernel32.dll")]
		public static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

		[DllImport("kernel32.dll")]
		public static extern bool Thread32First(IntPtr hSnapshot, ref THREADENTRY32 lpte);

		[DllImport("kernel32.dll")]
		public static extern bool Thread32Next(IntPtr hSnapshot, ref THREADENTRY32 lpte);

		[DllImport("kernel32.dll")]
		public static extern bool Module32FirstW(IntPtr hSnapshot, ref MODULEENTRY32W lpme);

		[DllImport("kernel32.dll")]
		public static extern bool Module32NextW(IntPtr hSnapshot, ref MODULEENTRY32W lpme);

		[DllImport("kernel32.dll")]
		public static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32W lpme);

		[DllImport("kernel32.dll")]
		public static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32W lpme);
		
		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentProcessId();
		
[DllImport("kernel32.dll")]
public static extern uint GetProcessHeaps(uint NumberOfHeaps,
IntPtr[] ProcessHeaps);

[DllImport("kernel32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
public static extern bool HeapWalk(IntPtr hHeap,
ref PROCESS_HEAP_ENTRY lpEntry);

[DllImport("kernel32.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
public static extern bool HeapLock(IntPtr heap);

[DllImport("kernel32.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
public static extern bool HeapUnlock(IntPtr heap);

// structs
[Flags]
public enum PROCESS_HEAP_ENTRY_WFLAGS : ushort
{
PROCESS_HEAP_ENTRY_BUSY = 0x0004,
PROCESS_HEAP_ENTRY_DDESHARE = 0x0020,
PROCESS_HEAP_ENTRY_MOVEABLE = 0x0010,
PROCESS_HEAP_REGION = 0x0001,
PROCESS_HEAP_UNCOMMITTED_RANGE = 0x0002,
}

[StructLayoutAttribute(LayoutKind.Explicit)]
public struct UNION_BLOCK
{
[FieldOffset(0)]
public STRUCT_BLOCK Block;

[FieldOffset(0)]
public STRUCT_REGION Region;
}

[StructLayoutAttribute(LayoutKind.Sequential)]
public struct STRUCT_BLOCK
{
public IntPtr hMem;
public uint dwReserved1_1;
public uint dwReserved1_2;
public uint dwReserved1_3;
}

[StructLayoutAttribute(LayoutKind.Sequential)]
public struct STRUCT_REGION
{
public uint dwCommittedSize;
public uint dwUnCommittedSize;
public IntPtr lpFirstBlock;
public IntPtr lpLastBlock;
}

[StructLayoutAttribute(LayoutKind.Sequential)]
public struct PROCESS_HEAP_ENTRY
{
public IntPtr lpData;
public uint cbData;
public byte cbOverhead;
public byte iRegionIndex;
public PROCESS_HEAP_ENTRY_WFLAGS wFlags;
public UNION_BLOCK UnionBlock;
}

	}
}
