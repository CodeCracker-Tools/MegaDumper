/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 11.10.2010
 * Time: 15:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Collections;

using ProcessUtils;
using WinEnumerator;
using Mega_Dumper;
	
namespace Mega_Dumper
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		[DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory
        (
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            UInt32 nSize,
            ref UInt32 lpNumberOfBytesRead
        );
        
        
        [DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory
        (
            IntPtr hProcess,
            uint lpBaseAddress,
            byte[] lpBuffer,
            UInt32 nSize,
            ref UInt32 lpNumberOfBytesRead
        );

public enum ProcessAccess : int
{
    /// <summary>Specifies all possible access flags for the process object.</summary>
    AllAccess = CreateThread | DuplicateHandle | QueryInformation | SetInformation | Terminate | VMOperation | VMRead | VMWrite | Synchronize,
    /// <summary>Enables usage of the process handle in the CreateRemoteThread function to create a thread in the process.</summary>
    CreateThread = 0x2,
    /// <summary>Enables usage of the process handle as either the source or target process in the DuplicateHandle function to duplicate a handle.</summary>
    DuplicateHandle = 0x40,
    /// <summary>Enables usage of the process handle in the GetExitCodeProcess and GetPriorityClass functions to read information from the process object.</summary>
    QueryInformation = 0x400,
    /// <summary>Enables usage of the process handle in the SetPriorityClass function to set the priority class of the process.</summary>
    SetInformation = 0x200,
    /// <summary>Enables usage of the process handle in the TerminateProcess function to terminate the process.</summary>
    Terminate = 0x1,
    /// <summary>Enables usage of the process handle in the VirtualProtectEx and WriteProcessMemory functions to modify the virtual memory of the process.</summary>
    VMOperation = 0x8,
    /// <summary>Enables usage of the process handle in the ReadProcessMemory function to' read from the virtual memory of the process.</summary>
    VMRead = 0x10,
    /// <summary>Enables usage of the process handle in the WriteProcessMemory function to write to the virtual memory of the process.</summary>
    VMWrite = 0x20,
    /// <summary>Enables usage of the process handle in any of the wait functions to wait for the process to terminate.</summary>
    Synchronize = 0x100000
}



		
[StructLayout(LayoutKind.Sequential)]
public struct SYSTEM_INFO
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

[DllImport("kernel32")]
public static extern void GetSystemInfo(ref SYSTEM_INFO pSI); 
     
     [DllImport("kernel32.dll")]
     static extern IntPtr OpenProcess(UInt32 dwDesiredAccess,  Int32 bInheritHandle, UInt32 dwProcessId);
     
     [DllImport("kernel32.dll", SetLastError=true)]
     [return: MarshalAs(UnmanagedType.Bool)]
     static extern bool CloseHandle(IntPtr hObject);
     
     
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
     
     //inner enum used only internally
    [Flags]
    private enum SnapshotFlags : uint
    {
    HeapList = 0x00000001,
    Process = 0x00000002,
    Thread = 0x00000004,
    Module = 0x00000008,
    Module32 = 0x00000010,
    Inherit = 0x80000000,
    All = 0x0000001F
    }
    //inner struct used only internally
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct PROCESSENTRY32
    {
    const int MAX_PATH = 260;
    internal UInt32 dwSize;
    internal UInt32 cntUsage;
    internal UInt32 th32ProcessID;
    internal IntPtr th32DefaultHeapID;
    internal UInt32 th32ModuleID;
    internal UInt32 cntThreads;
    internal UInt32 th32ParentProcessID;
    internal Int32 pcPriClassBase;
    internal UInt32 dwFlags;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
    internal string szExeFile;
    }

    [DllImport("kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern IntPtr CreateToolhelp32Snapshot([In]UInt32 dwFlags, [In]UInt32 th32ProcessID);

    [DllImport("kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern bool Process32First([In]IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

    [DllImport("kernel32", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern bool Process32Next([In]IntPtr hSnapshot, ref PROCESSENTRY32 lppe);


[DllImport("ntdll.dll", SetLastError=true)]
static extern int NtQueryInformationProcess(IntPtr processHandle,
   int processInformationClass, ref PROCESS_BASIC_INFORMATION processInformation, uint processInformationLength,
   out int returnLength);
   
[StructLayout(LayoutKind.Sequential, Pack = 1)]
private struct PROCESS_BASIC_INFORMATION
{
  public int ExitStatus;
  public int PebBaseAddress;
  public int AffinityMask;
  public int BasePriority;
  public int UniqueProcessId;
  public int InheritedFromUniqueProcessId;

  public int Size
  {
    get { return (6*4); }
  }
}


 
		public MainForm()
		{
		
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		



		void Button1Click(object sender, EventArgs e)
		{
		Application.Exit();
		}

[DllImport("kernel32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);

public void OnTimerEvent(object source, EventArgs e)
{
UInt32[] oldproc = new UInt32[lvprocesslist.Items.Count];

// get old list of process: 
for (int i=0;i<oldproc.Length;i++)
{
oldproc[i]=Convert.ToUInt32(lvprocesslist.Items[i].SubItems[1].Text);
}

uint[] processIds = new uint[0x200];
int proccount = 0;

        try
        {
        IntPtr handleToSnapshot = IntPtr.Zero;
        PROCESSENTRY32 procEntry = new PROCESSENTRY32();
        procEntry.dwSize = (UInt32)Marshal.SizeOf(typeof(PROCESSENTRY32));
        handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.Process, 0);
        if (Process32First(handleToSnapshot, ref procEntry))
        {
        do
        {
bool isThere = false;

for (int i=0;i<oldproc.Length;i++)
{
if (procEntry.th32ProcessID == oldproc[i])
{
isThere = true;
break;
}
}

// new process created ?
if (!isThere)
{
		Process theProc = null;
        string directoryName ="";
        string processname = procEntry.szExeFile;
        string isnet = "false";
        
        try
		{
        theProc = Process.GetProcessById((int)procEntry.th32ProcessID);
 		if (IsNetProcess((int)procEntry.th32ProcessID))
        {
        isnet = "true";
        }
        else
        {
        isnet = "false";
        }
        
        }
        catch
        {
        }
        string rname = "";
        try
		{
        rname = theProc.MainModule.FileName.Replace("\\??\\","");
        if (File.Exists(rname))
        {
        directoryName=Path.GetDirectoryName(rname);
        }
		}
		catch
		{
		}
        theProc.Close();
        
        if (!File.Exists(rname))
        {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {

        string newname="";
        try
        {
		IntPtr hProcess =
		OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, procEntry.th32ProcessID);
		if (hProcess!=IntPtr.Zero)
		{
      
        PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
         int bytesWritten;
         int result = NtQueryInformationProcess(hProcess,0, ref pbi, (uint)Marshal.SizeOf(pbi),out bytesWritten);
         if (result>=0)  // == 0 is OK
         {
         byte[] peb = new byte[472];
         uint BytesRead=0;
         bool isok = ReadProcessMemory(hProcess,(IntPtr)(pbi.PebBaseAddress),peb,(uint)(peb.Length), ref BytesRead);
         if (isok)
         {
         // this is on all Windows NT version - including Windows 7/Vista
       	 IntPtr AProcessParameters = (IntPtr)BitConverter.ToInt32(peb,016);
         
         byte[] ProcessParameters = new byte[72];
         isok = ReadProcessMemory(hProcess,AProcessParameters,ProcessParameters,(uint)(ProcessParameters.Length), ref BytesRead);  
         if (isok)
         {
         int aCurrentDirectory = BitConverter.ToInt32(ProcessParameters,040);
         byte[] Forread = new byte[2];
         int size=0;
         
         do
         {
         isok = ReadProcessMemory(hProcess,(IntPtr)(aCurrentDirectory+size),Forread,2, ref BytesRead);
         size=size+2;
         }
         while (isok&&Forread[0]!=0);
         size=size-2;
         byte[] CurrentDirectory = new byte[size];
         isok = ReadProcessMemory(hProcess,(IntPtr)(aCurrentDirectory),CurrentDirectory,(uint)size, ref BytesRead);
         newname = System.Text.Encoding.Unicode.GetString(CurrentDirectory);
		 if (newname.Length>=3)
		 {
		 newname = newname.Replace("\\??\\","");
		 directoryName = newname;
		 }
         }
         }

         }
        CloseHandle(hProcess);
        }
        }
        catch
        {
        
        }
        }
        }
        

        
 		 // compute size:
         Graphics g = lvprocesslist.CreateGraphics();
         Font objFont = new Font("Microsoft Sans Serif", 8);
         SizeF stringSize = new SizeF();
         stringSize = g.MeasureString(processname, objFont);
         int processlenght = (int)(stringSize.Width+lvprocesslist.Margin.Horizontal*2)+5;
         stringSize = g.MeasureString(directoryName, objFont);
         int directorylenght = (int)(stringSize.Width+lvprocesslist.Margin.Horizontal*2)+40;
         
         if (processlenght>procname.Width)
         {
         procname.Width=processlenght;
         }
         
         if (directorylenght>location.Width)
         {
         location.Width=directorylenght;
         }
         
         string[] prcdetails = new string[]{processname,procEntry.th32ProcessID.ToString(),"",isnet,directoryName};
         ListViewItem proc = new ListViewItem(prcdetails);
         lvprocesslist.Items.Add(proc);
        
		}
		else
		{
		proccount++;
		processIds[proccount]=procEntry.th32ProcessID;
		}
		
        } while (Process32Next(handleToSnapshot, ref procEntry));
        }
        CloseHandle(handleToSnapshot);
        }
        catch
        {
        }

// check statut of old processes: 
for (int i=0;i<oldproc.Length;i++)
{
bool isThere = false;
for (int j=0;j<processIds.Length;j++)
{
if (oldproc[i]== processIds[j])
isThere = true;
}

if (!isThere)
{
if (lvprocesslist.Items[i].SubItems[2].Text!="Killed")
lvprocesslist.Items[i].SubItems[2].Text ="Killed";
}

}

}


public bool IsNetProcess(int processid)
{
        ProcModule.ModuleInfo[] modules = ProcModule.GetModuleInfos(processid);
        string lowerfn = "";
        for (int i=0;i<modules.Length;i++)
        {
        lowerfn = modules[i].baseName.ToLower();
        if (lowerfn=="mscorjit.dll"||lowerfn=="mscorlib.dll"||
            lowerfn=="mscoree.dll"||lowerfn=="mscorwks.dll")
        return true;
        
        if (lowerfn=="clr.dll"||lowerfn=="clrjit.dll")  // Fr 4.0
        return true;
        
        }
        
        return false;
}
        
		public Timer timer1;
		private void EnumProcesses()
		{
if (timer1==null)
{
timer1 = new Timer();
timer1.Interval = 100;
timer1.Enabled = true;
timer1.Tick += new System.EventHandler (OnTimerEvent);
}

	    lvprocesslist.Items.Clear();
        Process theProc=null;

        string directoryName="";
        string processname="";
        string isnet = "false";
        
/*
IMO the key difference is in priviledges requirements.
I've seen cases in which EnumProcesses() would fail,
but CreateToolhelp32Snapshot() ran perfectly well.
*/ 
        try
        {
        IntPtr handleToSnapshot = IntPtr.Zero;
        PROCESSENTRY32 procEntry = new PROCESSENTRY32();
        procEntry.dwSize = (UInt32)Marshal.SizeOf(typeof(PROCESSENTRY32));
        handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.Process, 0);
        if (Process32First(handleToSnapshot, ref procEntry))
        {
        do
        {
        directoryName ="";
        processname = procEntry.szExeFile;
        string statut = "";//exited
        try
		{
        theProc = Process.GetProcessById((int)procEntry.th32ProcessID);
        
        if (IsNetProcess((int)procEntry.th32ProcessID))
        {
        isnet = "true";
        }
        else
        {
        isnet = "false";
        }
        
        }
        catch
        {
        }
        
     	string rname = "";
        try
		{
        rname = theProc.MainModule.FileName.Replace("\\??\\","");
        if (File.Exists(rname))
        {
        directoryName=Path.GetDirectoryName(rname);
        }
		}
		catch
		{
		}
		
        theProc.Close();
        
        if (!File.Exists(rname))
        {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {

        string newname="";
        try
        {
		IntPtr hProcess =
		OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, procEntry.th32ProcessID);
		if (hProcess!=IntPtr.Zero)
		{
      
        PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
         int bytesWritten;
         int result = NtQueryInformationProcess(hProcess,0, ref pbi, (uint)Marshal.SizeOf(pbi),out bytesWritten);
         if (result>=0)  // == 0 is OK
         {
         byte[] peb = new byte[472];
         uint BytesRead=0;
         bool isok = ReadProcessMemory(hProcess,(IntPtr)(pbi.PebBaseAddress),peb,(uint)(peb.Length), ref BytesRead);
         if (isok)
         {
         // this is on all Windows NT version - including Windows 7/Vista
       	 IntPtr AProcessParameters = (IntPtr)BitConverter.ToInt32(peb,016);
         
         byte[] ProcessParameters = new byte[72];
         isok = ReadProcessMemory(hProcess,AProcessParameters,ProcessParameters,(uint)(ProcessParameters.Length), ref BytesRead);  
         if (isok)
         {
         int aCurrentDirectory = BitConverter.ToInt32(ProcessParameters,040);
         byte[] Forread = new byte[2];
         int size=0;
         
         do
         {
         isok = ReadProcessMemory(hProcess,(IntPtr)(aCurrentDirectory+size),Forread,2, ref BytesRead);
         size=size+2;
         }
         while (isok&&Forread[0]!=0);
         size=size-2;
         byte[] CurrentDirectory = new byte[size];
         isok = ReadProcessMemory(hProcess,(IntPtr)(aCurrentDirectory),CurrentDirectory,(uint)size, ref BytesRead);
         newname = System.Text.Encoding.Unicode.GetString(CurrentDirectory);
		 if (newname.Length>=3)
		 {
		 newname = newname.Replace("\\??\\","");
		 directoryName = newname;
		 }
         }
         }

         }
        CloseHandle(hProcess);
        }
        }
        catch
        {
        
        }
        
        }
        }
        

        
 		 // compute size:
         Graphics g = lvprocesslist.CreateGraphics();
         Font objFont = new Font("Microsoft Sans Serif", 8);
         SizeF stringSize = new SizeF();
         stringSize = g.MeasureString(processname, objFont);
         int processlenght = (int)(stringSize.Width+lvprocesslist.Margin.Horizontal*2)+5;
         stringSize = g.MeasureString(directoryName, objFont);
         int directorylenght = (int)(stringSize.Width+lvprocesslist.Margin.Horizontal*2)+40;
         
         if (processlenght>procname.Width)
         {
         procname.Width=processlenght;
         }
         
         if (directorylenght>location.Width)
         {
         location.Width=directorylenght;
         }
         
         string[] prcdetails = new string[]{processname,procEntry.th32ProcessID.ToString(),statut,isnet,directoryName};
         ListViewItem proc = new ListViewItem(prcdetails);
         lvprocesslist.Items.Add(proc);
         
        } while (Process32Next(handleToSnapshot, ref procEntry));
        }
        CloseHandle(handleToSnapshot);
        
        }
        catch
        {
        }
  	
 
         }
		
		void MainFormLoad(object sender, EventArgs e)
		{
		EnableDebuggerPrivileges();
		EnumProcesses();	
		}
		
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		private struct TOKEN_PRIVILEGES
		{
			public int PrivilegeCount;
			public long Luid;
			public int Attributes;
		}
		
		private const int SE_PRIVILEGE_ENABLED = 0x00000002;
		private const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
		private const int TOKEN_QUERY = 0x00000008;
		
		[DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern int OpenProcessToken(int ProcessHandle, int DesiredAccess, ref int tokenhandle);
		
		[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
		private static extern int GetCurrentProcess();

		[DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern int LookupPrivilegeValue(string lpsystemname, string lpname, ref long lpLuid);

		[DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern int AdjustTokenPrivileges(int tokenhandle, int disableprivs, ref TOKEN_PRIVILEGES Newstate, int bufferlength, int PreivousState, int Returnlength);

		[DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern int GetSecurityInfo( int HANDLE, int SE_OBJECT_TYPE, int SECURITY_INFORMATION, int psidOwner, int psidGroup, out IntPtr pDACL, IntPtr pSACL, out IntPtr pSecurityDescriptor);

		[DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern int SetSecurityInfo( int HANDLE, int SE_OBJECT_TYPE, int SECURITY_INFORMATION, int psidOwner, int psidGroup, IntPtr pDACL, IntPtr pSACL);

		
		internal void EnableDebuggerPrivileges()
		{
			try
			{
			int token = 0;
			TOKEN_PRIVILEGES tp = new TOKEN_PRIVILEGES();
			tp.PrivilegeCount = 1;
			tp.Luid = 0;
			tp.Attributes = SE_PRIVILEGE_ENABLED;

			// We just assume this works
			if (OpenProcessToken( GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref token) == 0)
			return;

			if( LookupPrivilegeValue( null, "SeDebugPrivilege", ref tp.Luid) == 0)
			return;

			if( AdjustTokenPrivileges( token, 0, ref tp, Marshal.SizeOf(tp), 0, 0) == 0)
			return;
			}
			catch
			{
			
			}
		}
		
		

		
		void DumpToolStripMenuItemClick(object sender, EventArgs e)
		{
		DumpProcess();
		}
		
		public int RVA2Offset(byte[] input,int rva)
		{
		int PEOffset=BitConverter.ToInt32(input, 0x3C);
		int nrofsection = (int)BitConverter.ToInt16(input, PEOffset+0x06);
		
		for (int i = 0; i < nrofsection; i++)
        {
        int virtualAddress = BitConverter.ToInt32(input, PEOffset+0x0F8+0x28*i+012);
        int fvirtualsize = BitConverter.ToInt32(input, PEOffset+0x0F8+0x28*i+08);
        int frawAddress = BitConverter.ToInt32(input, PEOffset+0x28*i+0x0F8+20);
        if ((virtualAddress<=rva)&&(virtualAddress + fvirtualsize >= rva))
        return (frawAddress+ (rva - virtualAddress));
		}
		
		return -1;
		}
		
		
		public int Offset2RVA(byte[] input,int offset)
		{
		int PEOffset=BitConverter.ToInt32(input, 0x3C);
		int nrofsection = (int)BitConverter.ToInt16(input, PEOffset+0x06);
		
		for (int i = 0; i < nrofsection; i++)
        {
        int virtualAddress = BitConverter.ToInt32(input, PEOffset+0x0F8+0x28*i+012);
        int virtualsize = BitConverter.ToInt32(input, PEOffset+0x0F8+0x28*i+08);
        int frawAddress = BitConverter.ToInt32(input, PEOffset+0x28*i+0x0F8+20);
        int frawsize = BitConverter.ToInt32(input, PEOffset+0x28*i+0x0F8+16);
        if ((frawAddress<=offset)&&(frawAddress + frawsize >= offset))
        return (virtualAddress+ (offset- frawAddress));
		}
		
		return -1;
		}
		
public unsafe struct image_section_header
{
  public fixed byte name[8];
  public int  virtual_size;
  public int  virtual_address;
  public int  size_of_raw_data;
  public int  pointer_to_raw_data;
  public int  pointer_to_relocations;
  public int  pointer_to_linenumbers;
  public short number_of_relocations;
  public short number_of_linenumbers;
  public int  characteristics;
};
		
public struct IMAGE_FILE_HEADER
{
  public short  Machine;
  public short  NumberOfSections;
  public int TimeDateStamp;
  public int PointerToSymbolTable;
  public int NumberOfSymbols;
  public short  SizeOfOptionalHeader;
  public short  Characteristics;
}



		public bool FixImportandEntryPoint(int dumpVA,byte[] Dump)
		{
		if (Dump==null||Dump.Length==0) return false;
		
		int PEOffset=BitConverter.ToInt32(Dump, 0x3C);
				
        int ImportDirectoryRva = BitConverter.ToInt32(Dump, PEOffset+0x080);
        int impdiroffset = RVA2Offset(Dump,ImportDirectoryRva);
        if (impdiroffset==-1) return false;
        
		byte[] mscoreeAscii = {0x6D, 0x73, 0x63, 0x6F, 0x72, 0x65, 0x65, 0x2E, 0x64, 0x6C, 0x6C, 0x00};
        byte[] CorExeMain = {0x5F, 0x43, 0x6F, 0x72, 0x45, 0x78, 0x65, 0x4D, 0x61, 0x69, 0x6E, 0x00};
        byte[] CorDllMain = {0x5F, 0x43, 0x6F, 0x72, 0x44, 0x6C, 0x6C, 0x4D, 0x61, 0x69, 0x6E, 0x00};
        int ThunkToFix = 0;
        int ThunkData = 0;
        
        byte[] NameKeeper = new byte[mscoreeAscii.Length];
        int current=0;
        int NameRVA = BitConverter.ToInt32(Dump, impdiroffset+current+12);
        while (NameRVA>0)
        {
        int NameOffset = RVA2Offset(Dump,NameRVA);
        if (NameOffset>0)
        {
        try
        {
        bool ismscoree = true;
        for (int i = 0; i < mscoreeAscii.Length; i++)
        {
        if (Dump[NameOffset+i]!=mscoreeAscii[i])
        {
        ismscoree=false;
        break;
        }
        }
        
        if (ismscoree)
        {
        int OriginalFirstThunk = BitConverter.ToInt32(Dump, impdiroffset+current);
        int OriginalFirstThunkfo = RVA2Offset(Dump,OriginalFirstThunk);
        if (OriginalFirstThunkfo>0)
        {
        ThunkData = BitConverter.ToInt32(Dump,OriginalFirstThunkfo);
        int ThunkDatafo = RVA2Offset(Dump,ThunkData);
        if (ThunkDatafo>0)
        {
		ismscoree = true;
        for (int i = 0; i < mscoreeAscii.Length; i++)
        {
        if (Dump[ThunkDatafo+2+i]!=CorExeMain[i]&&Dump[ThunkDatafo+2+i]!=CorDllMain[i])
        {
        ismscoree=false;
        break;
        }
        }
        
        if (ismscoree)
        {
		ThunkToFix = BitConverter.ToInt32(Dump, impdiroffset+current+16);  // FirstThunk;
		break;
        }
        
        }
        }
                
        }
        }
        catch
        {
        }
        
        }
        
        try
        {
		current = current+20; // 20 = size of IMAGE_IMPORT_DESCRIPTOR
		NameRVA = BitConverter.ToInt32(Dump, ImportDirectoryRva+current+12);
        }
        catch
        {
        break;
        }
        }
        
        if (ThunkToFix<=0||ThunkData==0) return false;
        
        int ThunkToFixfo = RVA2Offset(Dump,ThunkToFix);
        if (ThunkToFixfo<0) return false;
        
        BinaryWriter writer = new BinaryWriter(new MemoryStream(Dump));
		int ThunkValue = BitConverter.ToInt32(Dump, ThunkToFixfo);  // old thunk value
        if (ThunkValue<=0||RVA2Offset(Dump,ThunkValue)<0)
        {
        writer.BaseStream.Position=ThunkToFixfo;
        writer.Write(ThunkData);
        }
        
        int EntryPoint = BitConverter.ToInt32(Dump, PEOffset+0x028);
        if (EntryPoint<=0||RVA2Offset(Dump,EntryPoint)<0)
        {
		byte[] ThunkToFixbytes = BitConverter.GetBytes((int)(ThunkToFix+dumpVA));
        for (int i=0;i<Dump.Length-6;i++)
        {
        if (Dump[i+0]==0x0FF&&Dump[i+1]==0x025&&Dump[i+2]==ThunkToFixbytes[0]&&Dump[i+3]==ThunkToFixbytes[1]&&Dump[i+4]==ThunkToFixbytes[2]&&Dump[i+5]==ThunkToFixbytes[3])
        {
        int EntrPointRVA = Offset2RVA(Dump,i);
        writer.BaseStream.Position=PEOffset+0x028;
        writer.Write(EntrPointRVA);
        break;
        }
        }
        }
        
        writer.Close();
        return true;
		}
		
public struct DUMP_DIRECTORIES
{
  public string  root;
  public string  dumps;
  public string  nativedirname;
  public string  sysdirname;
  public string  unknowndirname;
}

public void SetDirectoriesPath(ref DUMP_DIRECTORIES dpmdirs)
{
dpmdirs.dumps = Path.Combine(dpmdirs.root,"Dumps");
dpmdirs.nativedirname = Path.Combine(dpmdirs.dumps,"Native");
dpmdirs.sysdirname = Path.Combine(dpmdirs.dumps,"System");
dpmdirs.unknowndirname = Path.Combine(dpmdirs.dumps,"UnknownName");
}

public bool CreateDirectories(ref DUMP_DIRECTORIES dpmdirs)
{
SetDirectoriesPath(ref dpmdirs);

		if (!Directory.Exists(dpmdirs.dumps))
		{
		try
		{
		System.IO.Directory.CreateDirectory(dpmdirs.dumps);
		}
		catch
		{
System.Windows.Forms.FolderBrowserDialog browse =
new System.Windows.Forms.FolderBrowserDialog();
browse.ShowNewFolderButton = false;
browse.Description="Failed to create the directory - select a new location:";
browse.SelectedPath=dpmdirs.root;

	if (browse.ShowDialog()==System.Windows.Forms.DialogResult.OK)
	{
dpmdirs.root = browse.SelectedPath;
CreateDirectories(ref dpmdirs);
    }
	else
	{
	return false;
	}
	
		}
		}

		
		if (!Directory.Exists(dpmdirs.nativedirname))
		{
		try
		{
		System.IO.Directory.CreateDirectory(dpmdirs.nativedirname);
		}
		catch
		{
System.Windows.Forms.FolderBrowserDialog browse =
new System.Windows.Forms.FolderBrowserDialog();
browse.ShowNewFolderButton = false;
browse.Description="Failed to create the directory - select a new location:";
browse.SelectedPath=dpmdirs.root;

	if (browse.ShowDialog()==System.Windows.Forms.DialogResult.OK)
	{
dpmdirs.root = browse.SelectedPath;
CreateDirectories(ref dpmdirs);
    }
	else
	{
	return false;
	}
	
		}
		}

		
		if (!Directory.Exists(dpmdirs.sysdirname))
		{
		try
		{
		System.IO.Directory.CreateDirectory(dpmdirs.sysdirname);
		}
		catch
		{
System.Windows.Forms.FolderBrowserDialog browse =
new System.Windows.Forms.FolderBrowserDialog();
browse.ShowNewFolderButton = false;
browse.Description="Failed to create the directory - select a new location:";
browse.SelectedPath=dpmdirs.root;

	if (browse.ShowDialog()==System.Windows.Forms.DialogResult.OK)
	{
dpmdirs.root = browse.SelectedPath;
CreateDirectories(ref dpmdirs);
    }
	else
	{
	return false;
	}
	
		}
		}
		
		
		if (!Directory.Exists(dpmdirs.unknowndirname))
		{
		try
		{
		System.IO.Directory.CreateDirectory(dpmdirs.unknowndirname);
		}
		catch
		{
System.Windows.Forms.FolderBrowserDialog browse =
new System.Windows.Forms.FolderBrowserDialog();
browse.ShowNewFolderButton = false;
browse.Description="Failed to create the directory - select a new location:";
browse.SelectedPath=dpmdirs.root;

	if (browse.ShowDialog()==System.Windows.Forms.DialogResult.OK)
	{
dpmdirs.root = browse.SelectedPath;
CreateDirectories(ref dpmdirs);
    }
	else
	{
	return false;
	}
	
		}
		}

		return true;
}

		unsafe void DumpProcess()
		{
if (lvprocesslist.SelectedIndices.Count==0)
return;

		int intselectedindex = lvprocesslist.SelectedIndices[0]; 
		if (intselectedindex!=-1)
		{
		UInt32 processId = Convert.ToUInt32(lvprocesslist.Items[intselectedindex].SubItems[1].Text);
		
		IntPtr hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, processId);
		
		if( hProcess == IntPtr.Zero)
		{
		IntPtr pDACL, pSecDesc;

		GetSecurityInfo( (int) Process.GetCurrentProcess().Handle, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4, 0, 0, out pDACL, IntPtr.Zero, out pSecDesc);
		hProcess = OpenProcess( 0x40000, 0, processId);
		SetSecurityInfo( (int) hProcess, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4 | /*UNPROTECTED_DACL_SECURITY_INFORMATION*/ 0x20000000, 0, 0, pDACL, IntPtr.Zero);
		CloseHandle( hProcess);
		hProcess = OpenProcess( PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, processId);
		}

		if( hProcess != IntPtr.Zero)
		{
		uint minaddress = 0;
		uint maxaddress = 0xF0000000;
		uint pagesize = 0x1000;
		
		try
		{
		SYSTEM_INFO pSI = new SYSTEM_INFO();
		GetSystemInfo(ref pSI);
		minaddress = pSI.lpMinimumApplicationAddress;
		maxaddress = pSI.lpMaximumApplicationAddress;
		pagesize = pSI.dwPageSize;
		}
		catch
		{
		}

		int CurrentCount=1;
		string dirname = lvprocesslist.Items[intselectedindex].SubItems[4].Text;
		if (dirname.Length<2||!Directory.Exists(dirname))
		dirname="C:\\";
		
		//StartOfDirCreation1:
		DUMP_DIRECTORIES ddirs = new MainForm.DUMP_DIRECTORIES();
		ddirs.root = dirname;
		CreateDirectories(ref ddirs);
		
		//dirname = Path.Combine(dirname,"Dumps");
		//string nativedirname = Path.Combine(dirname,"Native");	
		//string sysdirname = Path.Combine(dirname,"System");
		//string unknowndirname = Path.Combine(dirname,"UnknownName");
	
		
		bool isok;
		byte[] onepage = new byte[pagesize];
		uint BytesRead=0;
		byte[] infokeep = new byte[8];
	
		for (uint j = minaddress; j < maxaddress; j+= pagesize)
    	{
		
		isok = ReadProcessMemory(hProcess,j,onepage,pagesize, ref BytesRead);

		if (isok)
		{
		for (int k = 0; k < onepage.Length - 2; k++)
        {

		    if (onepage[k]==0x4D&&onepage[k+1]==0x5A)
            {
		    if (ReadProcessMemory(hProcess,(uint)(j+k+0x03C),infokeep,4, ref BytesRead))
		    {
		    int PEOffset=BitConverter.ToInt32(infokeep, 0);
		    if (PEOffset>0&&(PEOffset+0x0120)<pagesize)
		    {
		    if (ReadProcessMemory(hProcess,(uint)(j+k+PEOffset),infokeep,2, ref BytesRead))
		    {
		    if (infokeep[0]==0x050&&infokeep[1]==0x045)
		    {
long NetMetadata = 0;
if (ReadProcessMemory(hProcess,(uint)(j+k+PEOffset+0x0E8),infokeep,8, ref BytesRead))
NetMetadata = BitConverter.ToInt64(infokeep,0);

if (dumpNativeToolStripMenuItem.Checked||NetMetadata!=0)
		    {
byte[] PeHeader = new byte[pagesize];
if (ReadProcessMemory(hProcess,(uint)(j+k),PeHeader,pagesize, ref BytesRead))
{
int nrofsection = (int)BitConverter.ToInt16(PeHeader, PEOffset+0x06);
if (nrofsection>0)
{
bool isNetFile = true;
string dumpdir = "";
//string dumpdir = ddirs.dumps;
if (NetMetadata==0)
isNetFile = false;

int sectionalignment = BitConverter.ToInt32(PeHeader, PEOffset+0x038);
int filealignment = BitConverter.ToInt32(PeHeader, PEOffset+0x03C);
short sizeofoptionalheader = BitConverter.ToInt16(PeHeader, PEOffset+0x014);

bool IsDll = false;
if ((PeHeader[PEOffset+0x017]&32)!=0) IsDll = true;
IntPtr pointer = IntPtr.Zero;
image_section_header[] sections = new image_section_header[nrofsection];
uint ptr = (uint)(j+k+PEOffset)+(uint)sizeofoptionalheader+4+
	(uint)Marshal.SizeOf(typeof(IMAGE_FILE_HEADER));

for (int i = 0; i < nrofsection; i++)
{
byte[] datakeeper = new byte[Marshal.SizeOf(typeof(image_section_header))];
ReadProcessMemory(hProcess,ptr,datakeeper,(uint)datakeeper.Length, ref BytesRead);
fixed (byte* p = datakeeper)
{
pointer = (IntPtr)p;
}
		
sections[i] = (image_section_header)Marshal.PtrToStructure(pointer, typeof(image_section_header));
ptr = ptr+(uint)Marshal.SizeOf(typeof(image_section_header));
}



// get total raw size (of all sections):
int totalrawsize = 0;
int rawsizeoflast = sections[nrofsection-1].size_of_raw_data;
int rawaddressoflast = sections[nrofsection-1].pointer_to_raw_data;
if (rawsizeoflast>0&&rawaddressoflast>0)
totalrawsize = rawsizeoflast+rawaddressoflast;
string filename = "";

// calculate right size of image
int actualsizeofimage = BitConverter.ToInt32(PeHeader, PEOffset+0x050);
int sizeofimage = actualsizeofimage;
int calculatedimagesize = BitConverter.ToInt32(PeHeader, PEOffset+0x0F8+012);
int rawsize,rawAddress,virtualsize,virtualAddress=0;
int calcrawsize=0;

for (int i = 0; i < nrofsection; i++)
{
virtualsize = sections[i].virtual_size;
int toadd = (virtualsize%sectionalignment);
if (toadd!=0) toadd = sectionalignment-toadd;
calculatedimagesize = calculatedimagesize+virtualsize+toadd;
}

if (calculatedimagesize>sizeofimage) sizeofimage=calculatedimagesize;

try
{
byte[] crap = new byte[totalrawsize];
}
catch
{
totalrawsize = sizeofimage;
}

if (totalrawsize!=0)
{
try
{
byte[] rawdump = new byte[totalrawsize];
isok = ReadProcessMemory(hProcess,(uint)(j+k),rawdump,(uint)rawdump.Length, ref BytesRead);
if (isok)
{

CreateTheFile1:
dumpdir = ddirs.nativedirname;
if (isNetFile)
dumpdir = ddirs.dumps;

filename = dumpdir+"\\rawdump_"+(j+k).ToString("X8");
if (File.Exists(filename))
filename = dumpdir+"\\rawdump"+CurrentCount.ToString()+"_"+(j+k).ToString("X8");


if (IsDll)
filename=filename+".dll";
else
filename=filename+".exe";

try
{
File.WriteAllBytes(filename,rawdump);
}
catch
{
System.Windows.Forms.FolderBrowserDialog browse =
new System.Windows.Forms.FolderBrowserDialog();
browse.ShowNewFolderButton = false;
browse.Description="Failed to create the file - select a new location:";
browse.SelectedPath=ddirs.root;

	if (browse.ShowDialog()==System.Windows.Forms.DialogResult.OK)
	{
ddirs.root = browse.SelectedPath;
CreateDirectories(ref ddirs);
    }
	else
	{
	return;
	}
goto CreateTheFile1;
}

CurrentCount++;

}
}
catch
{
}
}



byte[] virtualdump = new byte[sizeofimage];
Array.Copy(PeHeader,virtualdump,pagesize);

int rightrawsize = 0;
	for (int l = 0; l < nrofsection; l++)
	{
	rawsize = sections[l].size_of_raw_data;
	rawAddress = sections[l].pointer_to_raw_data;
	virtualsize = sections[l].virtual_size;
	virtualAddress = sections[l].virtual_address;
	
	// RawSize = Virtual Size rounded on FileAlligment
	calcrawsize=0;
	calcrawsize = virtualsize%filealignment;
	if (calcrawsize!=0) calcrawsize = filealignment-calcrawsize;
	calcrawsize = virtualsize+calcrawsize;

	if (calcrawsize!=0&&rawsize!=calcrawsize&&rawsize!=virtualsize
	   ||rawAddress<0)
	{
	// if raw size is bad:
	rawsize = virtualsize;
	rawAddress = virtualAddress;
	BinaryWriter writer = new BinaryWriter(new MemoryStream(virtualdump));
	writer.BaseStream.Position=PEOffset+0x0F8+0x28*l+16;
	writer.Write(virtualsize);
	writer.BaseStream.Position=PEOffset+0x0F8+0x28*l+20;
	writer.Write(virtualAddress);
	writer.Close();
	}
	
	byte[] csection = new byte[0];
	try
	{
	csection = new byte[rawsize];
	}
	catch
	{
	csection = new byte[virtualsize];
	}
	int rightsize = csection.Length;
	isok = ReadProcessMemory(hProcess,(uint)(j+k+virtualAddress),csection,(uint)rawsize, ref BytesRead);
	if (!isok||BytesRead!=rawsize)
	{
	rightsize = 0;
	byte[] currentpage = new byte[pagesize];
	for (int c = 0; c < rawsize; c=c+(int)pagesize)
	{
// some section have a houge size so : try
try
{
isok = ReadProcessMemory(hProcess,(uint)(j+k+virtualAddress+c),currentpage,(uint)pagesize, ref BytesRead);
}
catch
{
break;
}

if (isok)
{
rightsize = rightsize+(int)pagesize;
for (int i=0;i<pagesize;i++)
{
if ((c+i)<csection.Length)
csection[c+i]=currentpage[i];
}
}


	}
	}
    
	
	try
	{
	Array.Copy(csection, 0, virtualdump, rawAddress, rightsize);
	}
	catch
	{
	}
	
	if (l==nrofsection-1)
	{
	rightrawsize = rawAddress+rawsize;
	}
	
    }

FixImportandEntryPoint((int)(j+k),virtualdump);

CreateTheFile2:
dumpdir = ddirs.nativedirname;
if (isNetFile)
dumpdir = ddirs.dumps;

filename = dumpdir+"\\vdump_"+(j+k).ToString("X8");
if (File.Exists(filename))
filename = dumpdir+"\\vdump"+CurrentCount.ToString()+"_"+(j+k).ToString("X8");

if (IsDll)
filename=filename+".dll";
else
filename=filename+".exe";

FileStream fout = null;


try
{
fout = new FileStream(filename, FileMode.Create);
}
catch
{
System.Windows.Forms.FolderBrowserDialog browse =
new System.Windows.Forms.FolderBrowserDialog();
browse.ShowNewFolderButton = false;
browse.Description="Failed to create the file - select a new location:";
browse.SelectedPath=ddirs.root;

	if (browse.ShowDialog()==System.Windows.Forms.DialogResult.OK)
	{
ddirs.root = browse.SelectedPath;
CreateDirectories(ref ddirs);
    }
	else
	{
	return;
	}
goto CreateTheFile2;
}

if (fout!=null)
{
if (rightrawsize>virtualdump.Length) rightrawsize=virtualdump.Length;

fout.Write(virtualdump,0,rightrawsize);
fout.Close();
}
CurrentCount++;




}
}


            // dumping end here
		    }
		    }
		    }
		    }
		    }
            }
	
		}
		}


		}
		
		if (!dontRestoreFilenameToolStripMenuItem.Checked)
		{

		
		// rename files:
		if (Directory.Exists(ddirs.dumps))
		{
		DirectoryInfo di = new DirectoryInfo(ddirs.dumps);
		FileInfo[] rgFiles = di.GetFiles();
 
		foreach(FileInfo fi in rgFiles)
		{
string placedir = ddirs.dumps;
FileVersionInfo info = FileVersionInfo.GetVersionInfo(fi.FullName);
if (info.CompanyName!=null&&info.CompanyName.ToLower().Contains("microsoft corporation")
   &&(info.ProductName.ToLower().Contains(".net framework")||
   info.FileDescription.ToLower().Contains("runtime library")
  ))
{
placedir = ddirs.sysdirname;
}
if (info.OriginalFilename!=null&&info.OriginalFilename!="")
{
string Newfilename=Path.Combine(placedir,info.OriginalFilename);
int count = 2;
if (File.Exists(Newfilename))
{
string extension = Path.GetExtension(Newfilename);
if (extension=="") extension = ".dll";
do
{
Newfilename = placedir+"\\"+Path.GetFileNameWithoutExtension(info.OriginalFilename)
	+"("+count.ToString()+")"+extension;
		
count++;
}
while (File.Exists(Newfilename));
}

System.IO.File.Move(fi.FullName,Newfilename);
}
else
{
string Newfilename = Path.Combine(ddirs.unknowndirname,fi.Name);
int count = 2;
if (File.Exists(Newfilename))
{
string extension = Path.GetExtension(fi.Name);

do
{
Newfilename = ddirs.unknowndirname+"\\"+Path.GetFileNameWithoutExtension(fi.Name)
	+"("+count.ToString()+")"+extension;
		
count++;
}
while (File.Exists(Newfilename));
}

System.IO.File.Move(fi.FullName,Newfilename);
}


		}
		}
		
		// rename files:
		if (Directory.Exists(ddirs.nativedirname))
		{
		DirectoryInfo di = new DirectoryInfo(ddirs.nativedirname);
		FileInfo[] rgFiles = di.GetFiles();
 
		foreach(FileInfo fi in rgFiles)
		{
FileVersionInfo info = FileVersionInfo.GetVersionInfo(fi.FullName);
if (info.OriginalFilename!=null&&info.OriginalFilename!="")
{
string Newfilename=Path.Combine(ddirs.nativedirname,info.OriginalFilename);
int count = 2;
if (File.Exists(Newfilename))
{
string extension = Path.GetExtension(Newfilename);
if (extension=="") extension = ".dll";
do
{
Newfilename = ddirs.nativedirname+"\\"+Path.GetFileNameWithoutExtension(info.OriginalFilename)
	+"("+count.ToString()+")"+extension;
		
count++;
}
while (File.Exists(Newfilename));
}

System.IO.File.Move(fi.FullName,Newfilename);
}
		}
		}
		}
		CurrentCount--;
		MessageBox.Show(CurrentCount.ToString()+" files dumped in directory "+ddirs.dumps,"Success!",0,MessageBoxIcon.Information);

		}
		else
		{
		MessageBox.Show("Failed to open selected process!","Error!",0,MessageBoxIcon.Error);
		}
		CloseHandle(hProcess);
		}
		}
		
		

		
		void CopyToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count>0)
{	
string strtoset = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[4].Text;
if (strtoset!="") Clipboard.SetText(strtoset);
}
}
		
		void DumpModuleToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count==0)
return;

		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		string dirname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[4].Text;
		if (strprname!="")
		{
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		FrmModules pmodfrm = new FrmModules(strprname,procid,dirname);
		pmodfrm.Show();
		}

		}
		
		void Button3Click(object sender, EventArgs e)
		{
		ProcessManager prman = new ProcessManager();
		prman.Show();
		}

		void GotoLocationToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count==0)
return;
	
string dirname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[4].Text;
string filename = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
string fullfilename = Path.Combine(dirname,filename);
if (Directory.Exists(dirname))
{
try
{
string argument = @"/select, " + fullfilename;
System.Diagnostics.Process.Start("explorer.exe", argument);
}
catch
{
}

}
		}
		
		void ToolStripMenuItem2Click(object sender, EventArgs e)
		{
			
		}
		
[DllImport("kernel32.dll", SetLastError=true)]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

		void KillProcessToolStripMenuItemClick(object sender, EventArgs e)
		{
		int intselectedindex = lvprocesslist.SelectedIndices[0]; 
		if (intselectedindex!=-1)
		{
		UInt32 processId = Convert.ToUInt32(lvprocesslist.Items[intselectedindex].SubItems[1].Text);
		IntPtr hProcess = OpenProcess(PROCESS_TERMINATE | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, processId);
		
		if( hProcess == IntPtr.Zero)
		{
		IntPtr pDACL, pSecDesc;

		GetSecurityInfo( (int) Process.GetCurrentProcess().Handle, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4, 0, 0, out pDACL, IntPtr.Zero, out pSecDesc);
		hProcess = OpenProcess( 0x40000, 0, processId);
		SetSecurityInfo( (int) hProcess, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4 | /*UNPROTECTED_DACL_SECURITY_INFORMATION*/ 0x20000000, 0, 0, pDACL, IntPtr.Zero);
		CloseHandle( hProcess);
		hProcess = OpenProcess( PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, processId);
		}
		
		try
		{
		TerminateProcess(hProcess,0);
		}
		catch
		{
		}
		CloseHandle( hProcess);
		
		}
		}
		
[DllImport("ntdll.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool ZwSuspendProcess(IntPtr hProcess);

[DllImport("ntdll.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool ZwResumeProcess(IntPtr hProcess);


		void SuspendProcessToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count==0)
return;

		int intselectedindex = lvprocesslist.SelectedIndices[0]; 
		if (intselectedindex!=-1)
		{
		UInt32 processId = Convert.ToUInt32(lvprocesslist.Items[intselectedindex].SubItems[1].Text);
		IntPtr hProcess = OpenProcess(0x800, 0, processId);
		
		if( hProcess == IntPtr.Zero)
		{
		IntPtr pDACL, pSecDesc;

		GetSecurityInfo( (int) Process.GetCurrentProcess().Handle, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4, 0, 0, out pDACL, IntPtr.Zero, out pSecDesc);
		hProcess = OpenProcess( 0x40000, 0, processId);
		SetSecurityInfo( (int) hProcess, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4 | /*UNPROTECTED_DACL_SECURITY_INFORMATION*/ 0x20000000, 0, 0, pDACL, IntPtr.Zero);
		CloseHandle( hProcess);
		hProcess = OpenProcess( PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, processId);
		}
		
		try
		{
		ZwSuspendProcess(hProcess);
		}
		catch
		{
		}
		CloseHandle( hProcess);
		
		}
		}
		
		void ResumeProcessToolStripMenuItemClick(object sender, EventArgs e)
		{
		int intselectedindex = lvprocesslist.SelectedIndices[0]; 
		if (intselectedindex!=-1)
		{
		UInt32 processId = Convert.ToUInt32(lvprocesslist.Items[intselectedindex].SubItems[1].Text);
		IntPtr hProcess = OpenProcess(0x800, 0, processId);
		
		if( hProcess == IntPtr.Zero)
		{
		IntPtr pDACL, pSecDesc;

		GetSecurityInfo( (int) Process.GetCurrentProcess().Handle, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4, 0, 0, out pDACL, IntPtr.Zero, out pSecDesc);
		hProcess = OpenProcess( 0x40000, 0, processId);
		SetSecurityInfo( (int) hProcess, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4 | /*UNPROTECTED_DACL_SECURITY_INFORMATION*/ 0x20000000, 0, 0, pDACL, IntPtr.Zero);
		CloseHandle( hProcess);
		hProcess = OpenProcess( PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, processId);
		}
		
		try
		{
		ZwResumeProcess(hProcess);
		}
		catch
		{
		}
		CloseHandle( hProcess);
		
		}
		}

		
		void CheckBox3CheckedChanged(object sender, EventArgs e)
		{
timer1.Stop();
timer1.Dispose();
timer1 = null;


if (timer1==null)
{
timer1 = new Timer();
timer1.Interval = 100;
timer1.Enabled = true;
timer1.Tick += new System.EventHandler (OnTimerEvent);
}



		}


[DllImport("user32.dll")]
public static extern Int32 SetForegroundWindow(IntPtr hWnd);

[DllImport("user32.dll", SetLastError=true)]
static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

[DllImport("user32.dll")]
static extern bool CloseWindow(IntPtr hWnd);

public enum ShowWindowCommand : int
{
    /// <summary>
    /// Hides the window and activates another window.
    /// </summary>
    Hide = 0,
    /// <summary>
    /// Activates and displays a window. If the window is minimized or
    /// maximized, the system restores it to its original size and position.
    /// An application should specify this flag when displaying the window
    /// for the first time.
    /// </summary>
    Normal = 1,
    /// <summary>
    /// Activates the window and displays it as a minimized window.
    /// </summary>
    ShowMinimized = 2,
    /// <summary>
    /// Maximizes the specified window.
    /// </summary>
    Maximize = 3, // is this the right value?
    /// <summary>
    /// Activates the window and displays it as a maximized window.
    /// </summary>      
    ShowMaximized = 3,
    /// <summary>
    /// Displays a window in its most recent size and position. This value
    /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except
    /// the window is not actived.
    /// </summary>
    ShowNoActivate = 4,
    /// <summary>
    /// Activates the window and displays it in its current size and position.
    /// </summary>
    Show = 5,
    /// <summary>
    /// Minimizes the specified window and activates the next top-level
    /// window in the Z order.
    /// </summary>
    Minimize = 6,
    /// <summary>
    /// Displays the window as a minimized window. This value is similar to
    /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the
    /// window is not activated.
    /// </summary>
    ShowMinNoActive = 7,
    /// <summary>
    /// Displays the window in its current size and position. This value is
    /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the
    /// window is not activated.
    /// </summary>
    ShowNA = 8,
    /// <summary>
    /// Activates and displays the window. If the window is minimized or
    /// maximized, the system restores it to its original size and position.
    /// An application should specify this flag when restoring a minimized window.
    /// </summary>
    Restore = 9,
    /// <summary>
    /// Sets the show state based on the SW_* value specified in the
    /// STARTUPINFO structure passed to the CreateProcess function by the
    /// program that started the application.
    /// </summary>
    ShowDefault = 10,
    /// <summary>
    ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
    /// that owns the window is not responding. This flag should only be
    /// used when minimizing windows from a different thread.
    /// </summary>
    ForceMinimize = 11
}

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommand nCmdShow);

[DllImport("user32.dll")]
private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow); 

[DllImport("user32.dll", EntryPoint="SystemParametersInfo")]
public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);


void BringToFrontToolStripMenuItemClick(object sender, EventArgs e)
{
if (lvprocesslist.SelectedIndices.Count>0)
{	
string strwhitpid = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text;
int processpid = System.Convert.ToInt32(strwhitpid, 10);

EnumWindows eW = new EnumWindows();
eW.GetWindows();
foreach (EnumWindowsItem item in eW.Items)
{
if (item.Visible)
{
int currentpid = 0;
uint threadid = GetWindowThreadProcessId(item.Handle,out currentpid);
if (currentpid==processpid)
{
  // SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001
SystemParametersInfo( (uint) 0x2001, 0, 0, 0x0002 | 0x0001);
ShowWindowAsync(item.Handle, 3);
SetForegroundWindow(item.Handle);
SystemParametersInfo( (uint) 0x2001, 200000, 200000, 0x0002 | 0x0001);

}
}
}
}


}

		
		void RestoreToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count==0)
return;

string strwhitpid = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text;
int processpid = System.Convert.ToInt32(strwhitpid, 10);

EnumWindows eW = new EnumWindows();
eW.GetWindows();
foreach (EnumWindowsItem item in eW.Items)
{
if (item.Visible)
{
int currentpid = 0;
uint threadid = GetWindowThreadProcessId(item.Handle,out currentpid);
if (currentpid==processpid)
{
ShowWindow(item.Handle, ShowWindowCommand.Restore);
}
}
}
}

	
void MinimizeToolStripMenuItemClick(object sender, EventArgs e)
{
if (lvprocesslist.SelectedIndices.Count==0)
return;

string strwhitpid = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text;
int processpid = System.Convert.ToInt32(strwhitpid, 10);

EnumWindows eW = new EnumWindows();
eW.GetWindows();
foreach (EnumWindowsItem item in eW.Items)
{
if (item.Visible)
{
int currentpid = 0;
uint threadid = GetWindowThreadProcessId(item.Handle,out currentpid);
if (currentpid==processpid)
{
ShowWindow(item.Handle, ShowWindowCommand.Minimize);
}
}
}
}

		
		void MaximizeToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count==0)
return;

string strwhitpid = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text;
int processpid = System.Convert.ToInt32(strwhitpid, 10);

EnumWindows eW = new EnumWindows();
eW.GetWindows();
foreach (EnumWindowsItem item in eW.Items)
{
if (item.Visible)
{
int currentpid = 0;
uint threadid = GetWindowThreadProcessId(item.Handle,out currentpid);
if (currentpid==processpid)
{
ShowWindow(item.Handle, ShowWindowCommand.Maximize);
}
}
}
		}
		
		void CloseToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count==0)
return;

string strwhitpid = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text;
int processpid = System.Convert.ToInt32(strwhitpid, 10);

EnumWindows eW = new EnumWindows();
eW.GetWindows();
foreach (EnumWindowsItem item in eW.Items)
{
if (item.Visible)
{
int currentpid = 0;
uint threadid = GetWindowThreadProcessId(item.Handle,out currentpid);
if (currentpid==processpid)
{
CloseWindow(item.Handle);
}
}
}
		}
		

		

		
public enum ProcessPriorities : uint
{
      Above_Normal = 0x00008000, //Process that has priority above NORMAL_PRIORITY_CLASS but below HIGH_PRIORITY_CLASS.
      Below_Normal = 0x00004000, //Process that has priority above IDLE_PRIORITY_CLASS but below NORMAL_PRIORITY_CLASS.
      High = 0x00000080,         //Process that performs time-critical tasks that must be executed immediately for it to run correctly. The threads of a high-priority class process preempt the threads of normal or idle priority class processes. An example is the Task List, which must respond quickly when called by the user, regardless of the load on the operating system. Use extreme care when using the high-priority class, because a high-priority class CPU-bound application can use nearly all available cycles.
      Idle = 0x00000040,         //Process whose threads run only when the system is idle and are preempted by the threads of any process running in a higher priority class. An example is a screen saver. The idle priority class is inherited by child processes.
      Normal = 0x00000020,       //Process with no special scheduling needs.
      Real_Time = 0x00000100      //Process that has the highest possible priority. The threads of a real-time priority class process preempt the threads of all other processes, including operating system processes performing important tasks. For example, a real-time process that executes for more than a very brief interval can cause disk caches not to flush or cause the mouse to be unresponsive.
}

[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
public static extern ProcessPriorities GetPriorityClass(IntPtr handle);

[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
public static extern bool SetPriorityClass(IntPtr handle, ProcessPriorities priority);

void PriorityToolStripMenuItemClick(object sender, EventArgs e)
{
if (lvprocesslist.SelectedIndices.Count>0)
{
int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
if (procid!=0)
{
IntPtr hProcess =
OpenProcess(PROCESS_QUERY_INFORMATION, 0, (uint)procid);
if (hProcess!=IntPtr.Zero)
{
ProcessPriorities cpriority = GetPriorityClass(hProcess);
switch (cpriority)
{
    case ProcessPriorities.Real_Time: 
        rttoolStripMenuItem.Checked = true;
        
        hToolStripMenuItem.Checked = false;
        anToolStripMenuItem.Checked = false;
        nToolStripMenuItem.Checked = false;
        bnToolStripMenuItem.Checked = false;
        iToolStripMenuItem.Checked = false;
        break;
    case ProcessPriorities.High:
        hToolStripMenuItem.Checked = true;
        
        rttoolStripMenuItem.Checked = false;
        anToolStripMenuItem.Checked = false;
        nToolStripMenuItem.Checked = false;
        bnToolStripMenuItem.Checked = false;
        iToolStripMenuItem.Checked = false;
        break;
    case ProcessPriorities.Above_Normal:
        anToolStripMenuItem.Checked = true;
        
        rttoolStripMenuItem.Checked = false;
        hToolStripMenuItem.Checked = false;
        nToolStripMenuItem.Checked = false;
        bnToolStripMenuItem.Checked = false;
        iToolStripMenuItem.Checked = false; 
    	break;
    case ProcessPriorities.Normal:
        nToolStripMenuItem.Checked = true;
        
        rttoolStripMenuItem.Checked = false;
        hToolStripMenuItem.Checked = false;
        anToolStripMenuItem.Checked = false;
        bnToolStripMenuItem.Checked = false;
        iToolStripMenuItem.Checked = false;
    	break;
    case ProcessPriorities.Below_Normal:
        bnToolStripMenuItem.Checked = true;
        
        rttoolStripMenuItem.Checked = false;
        hToolStripMenuItem.Checked = false;
        anToolStripMenuItem.Checked = false;
        nToolStripMenuItem.Checked = false;
        iToolStripMenuItem.Checked = false;
    	break;
    case ProcessPriorities.Idle:
        iToolStripMenuItem.Checked = true;
        
        rttoolStripMenuItem.Checked = false;
        hToolStripMenuItem.Checked = false;
        anToolStripMenuItem.Checked = false;
        nToolStripMenuItem.Checked = false;
        bnToolStripMenuItem.Checked = false;
    	break;
    default:
        break;
}

CloseHandle(hProcess);
}

}
	
}
//ProcessPriorities retuened = 
}


void ToolStripMenuItem3Click(object sender, EventArgs e)
{
if (lvprocesslist.SelectedIndices.Count>0)
{
int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
if (procid!=0)
{
IntPtr hProcess =
OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_SET_INFORMATION, 0, (uint)procid);
if (hProcess!=IntPtr.Zero)
{
if (SetPriorityClass(hProcess, ProcessPriorities.Real_Time))
{
rttoolStripMenuItem.Checked = true;
}
}
}
}
}
		
void HToolStripMenuItemClick(object sender, EventArgs e)
{
if (lvprocesslist.SelectedIndices.Count>0)
{
int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
if (procid!=0)
{
IntPtr hProcess =
OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_SET_INFORMATION, 0, (uint)procid);
if (hProcess!=IntPtr.Zero)
{
if (SetPriorityClass(hProcess, ProcessPriorities.High))
{
hToolStripMenuItem.Checked = true;
}
}
}
}
		}
		
		void AnToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count>0)
{
int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
if (procid!=0)
{
IntPtr hProcess =
OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_SET_INFORMATION, 0, (uint)procid);
if (hProcess!=IntPtr.Zero)
{
if (SetPriorityClass(hProcess, ProcessPriorities.Above_Normal))
{
anToolStripMenuItem.Checked = true;
}
}
}
}
		}
		
		void NToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count>0)
{
int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
if (procid!=0)
{
IntPtr hProcess =
OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_SET_INFORMATION, 0, (uint)procid);
if (hProcess!=IntPtr.Zero)
{
if (SetPriorityClass(hProcess, ProcessPriorities.Normal))
{
nToolStripMenuItem.Checked = true;
}
}
}
}
		}
		
		void BnToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count>0)
{
int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
if (procid!=0)
{
IntPtr hProcess =
OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_SET_INFORMATION, 0, (uint)procid);
if (hProcess!=IntPtr.Zero)
{
if (SetPriorityClass(hProcess, ProcessPriorities.Below_Normal))
{
bnToolStripMenuItem.Checked = true;
}
}
}
}
		}
		
		void IToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count>0)
{
int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
if (procid!=0)
{
IntPtr hProcess =
OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_SET_INFORMATION, 0, (uint)procid);
if (hProcess!=IntPtr.Zero)
{
if (SetPriorityClass(hProcess, ProcessPriorities.Idle))
{
iToolStripMenuItem.Checked = true;
}
}
}
}
		}
		

		

		void TestToolStripMenuItemClick(object sender, EventArgs e)
		{
		EnumProcesses();
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
		Application.Exit();
		}
		
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
		AboutForm abf = new AboutForm();
		abf.Show();
		}
		
		void ProcessManagerToolStripMenuItemClick(object sender, EventArgs e)
		{
		ProcessManager prman = new ProcessManager();
		prman.Show();
		}
		
		void WindowsHoocksToolStripMenuItemClick(object sender, EventArgs e)
		{
	ViewWindowsHoocks wwh = new ViewWindowsHoocks();
	wwh.Show();
		}
		
		
		void InstalledFrameworkToolStripMenuItemClick(object sender, EventArgs e)
		{
		InstalledFramework insfr = new InstalledFramework();
		insfr.Show();
		}
		


		
		void VirtualMemoryToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvprocesslist.SelectedIndices.Count>0)
		{
		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		VirtualMemoryView vmv = new VirtualMemoryView(procid,strprname);
		vmv.Show();

		}
		}
		
		void EnumAppdomainsToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvprocesslist.SelectedIndices.Count>0)
{
int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
if (procid!=0)
{
EnumAppDomains enumasm = new EnumAppDomains(procid);
enumasm.Show();
}
}

		}
		
		void HookDetectionToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvprocesslist.SelectedIndices.Count>0)
		{
		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		if (strprname!="")
		{
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		EmptyForm hdet = new EmptyForm(strprname,procid,1);
		hdet.Show();
		}
		}
		}
		
		void EnvironmentVariablesToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvprocesslist.SelectedIndices.Count>0)
		{
		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		EmptyForm envenum = new EmptyForm(strprname,procid,2);
		envenum.Show();

		}
		}
		

		
		void ViewHeapToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvprocesslist.SelectedIndices.Count>0)
		{
		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		if (strprname!="")
		{
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		if ((uint)(procid)==HeapHealper.GetCurrentProcessId())
		{
MessageBox.Show("Can't enumerate heap for MegaDumper itself!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

		}
		else
		{
		HeapView hw = new HeapView(strprname,procid);
		hw.Show();
		}
		}

		}
		}
		
		void NETPerformanceToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvprocesslist.SelectedIndices.Count>0)
		{
		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		if (strprname!="")
		{
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		NetPerformance np = new NetPerformance(strprname,procid);
		np.Show();

		}
		}
		}
		

		
		void GenerateDmpToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvprocesslist.SelectedIndices.Count>0)
		{
		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		string dirname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[4].Text;
		if (strprname!="")
		{
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		GenerateDmp pmodfrm = new GenerateDmp(strprname,procid,dirname);
		pmodfrm.Show();
		}
		}
		}
		

		
		void FileDirectoriesListToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvprocesslist.SelectedIndices.Count>0)
		{
		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		EmptyForm envenum = new EmptyForm(strprname,procid,3);
		envenum.Show();

		}
		}
		
		void InjectManagedDllToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvprocesslist.SelectedIndices.Count>0)
		{
		string strprname = lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[0].Text;
		int procid = int.Parse(lvprocesslist.Items[lvprocesslist.SelectedIndices[0]].SubItems[1].Text);
		MegaDumper.ManagedInjector maninject = new MegaDumper.ManagedInjector(strprname,procid);
		maninject.Show();

		}
		}
}
}
