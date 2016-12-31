/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 02.03.2011
 * Time: 00:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text;

namespace Mega_Dumper
{

	/// <summary>
	/// Description of VirtualMemoryView.
	/// </summary>
	public partial class VirtualMemoryView : Form
	{
		public int procid;
		public string processname;
		public VirtualMemoryView(int iprocid,string prname)
		{
		procid = iprocid;
		processname = prname;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}


[StructLayout(LayoutKind.Sequential)]
private struct SYSTEM_INFO
{
	public uint dwOemId;
	public uint dwPageSize;
	public uint lpMinimumApplicationAddress;
	public uint lpMaximumApplicationAddress;
	public uint dwActiveProcessorMask;
	public uint dwNumberOfProcessors;
	public uint dwProcessorType;
	public uint dwAllocationGranularity;
	public uint dwProcessorLevel;
	public uint dwProcessorRevision;
}

    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryBasicInformation
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public MemoryProtection AllocationProtect;
        public IntPtr RegionSize;
        public MemoryState State;
        public MemoryProtection Protect;
        public MemoryType Type;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 16)]
    public struct MemoryBasicInformation64
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public MemoryProtection AllocationProtect;
        private int _alignment1;
        public ulong RegionSize;
        public MemoryState State;
        public MemoryProtection Protect;
        public MemoryType Type;
        private int _alignment2;
    }
    
    [Flags]
    public enum MemoryProtection : uint
    {
        AccessDenied = 0x0,
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        Guard = 0x100,
        NoCache = 0x200,
        WriteCombine = 0x400,
        NoAccess = 0x01,
        ReadOnly = 0x02,
        ReadWrite = 0x04,
        WriteCopy = 0x08
    }

[DllImport("kernel32")]
private static extern void GetSystemInfo(ref SYSTEM_INFO pSI); 

[DllImport("kernel32.dll")]
public static extern bool VirtualQueryEx(IntPtr hProcess,
uint lpAddress, out MemoryBasicInformation lpBuffer,
uint dwLength);

[DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
public static extern int GetMappedFileName(
[In] int ProcessHandle,
[In] IntPtr Address,
[Out] StringBuilder Buffer,
[In] int Size);

[Flags]
public enum MemoryState : uint
{
    Commit = 0x1000,
    Decommit = 0x4000,
    Free = 0x10000,
    LargePages = 0x20000000,
    Physical = 0x400000,
    Release = 0x8000,
    Reserve = 0x2000,
    Reset = 0x80000
}

public enum MemoryType
{
    Image = 0x1000000,
    Mapped = 0x40000,
    Private = 0x20000
}

 
public static MemoryBasicInformation mbi;

[DllImport("psapi.dll", SetLastError = true)]
private static extern uint GetMappedFileName(IntPtr m_hProcess, IntPtr lpv,
out string lpFilename, uint nSize);

[DllImport("kernel32.dll")]
static extern IntPtr OpenProcess(UInt32 dwDesiredAccess,  Int32 bInheritHandle, UInt32 dwProcessId);
     	
private const uint PROCESS_TERMINATE = 0x0001;
private const uint PROCESS_CREATE_THREAD = 0x0002;
private const uint PROCESS_SET_SESSIONID = 0x0004;
private const uint PROCESS_VM_OPERATION = 0x0008;
private const uint PROCESS_VM_READ = 0x0010;
private const uint PROCESS_VM_WRITE = 0x0020;
private const uint PROCESS_DUP_HANDLE = 0x0040;
private const uint PROCESS_CREATE_PROCESS = 0x0080;
private const uint PROCESS_SET_QUOTA = 0x0100;
private const uint PROCESS_SET_INFORMATION = 0x0200;
private const uint PROCESS_QUERY_INFORMATION = 0x0400;
			
public void GetVirtualmemoryBlocks()
{

SYSTEM_INFO pSI = new SYSTEM_INFO();
GetSystemInfo(ref pSI);
uint i = 0;

IntPtr hProcess = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ | PROCESS_TERMINATE, 0, (uint)procid);

while (i < pSI.lpMaximumApplicationAddress)
{
if (VirtualQueryEx(hProcess,i,
out mbi,(uint)System.Runtime.InteropServices.Marshal.SizeOf(mbi))
)
{
uint alocbase = (uint)mbi.AllocationBase;



string mappedname = "";
if (mbi.Type == MemoryType.Mapped)
{
StringBuilder sb = new StringBuilder(1024);
GetMappedFileName(-1,mbi.BaseAddress,sb,1024);
if (sb.Length!=0)
{
mappedname = sb.ToString();
}
}

string[] prcdetails = new string[]{
mbi.BaseAddress.ToString("X8"),mbi.AllocationProtect.ToString(),
alocbase.ToString("X8"),mbi.Protect.ToString(),mbi.RegionSize.ToString("X8"),
mbi.State.ToString(),mbi.Type.ToString(),mappedname};
ListViewItem proc = new ListViewItem(prcdetails);
lvvirtualmem.Items.Add(proc);


i = (uint)mbi.BaseAddress + (uint)mbi.RegionSize;


}
}


}

		void VirtualMemoryViewShown(object sender, EventArgs e)
		{
this.Text="Virtual memory for "+processname+" whit PID="+procid.ToString();
GetVirtualmemoryBlocks();

		}
		

	}
}
