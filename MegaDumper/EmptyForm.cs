/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 27.10.2010
 * Time: 18:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Text;

using ProcessUtils;

namespace Mega_Dumper
{
	/// <summary>
	/// Description of Form3.
	/// </summary>
	/// 
	 

	public partial class EmptyForm : Form
	{
	public string ProcessName;
	public int procid;
	public int whattodo;

		public EmptyForm(string procname,int prid,int todo)
		{
		ProcessName=procname;
		procid=prid;
		whattodo=todo;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
	
		
		void Form3Load(object sender, System.EventArgs e)
		{
		textBox1.Text="";
		if (whattodo==1)
		this.Text="Hook detection for "+ProcessName+" whit PID="+procid.ToString();
		else if (whattodo==2)
		this.Text="Environment Variables for "+ProcessName+" whit PID="+procid.ToString();
		else if (whattodo==3)
		this.Text="Files/directories from "+ProcessName+" whit PID="+procid.ToString();
		else if (whattodo==4)
		this.Text="Code section differences: process name "+ProcessName+"; PID="+procid.ToString();
		}

		[DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory
        (
            IntPtr hProcess,
            IntPtr lpBaseAddress,
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

     [DllImport("kernel32.dll")]
     static extern IntPtr OpenProcess(UInt32 dwDesiredAccess,  Int32 bInheritHandle, UInt32 dwProcessId);
     
     [DllImport("kernel32.dll", SetLastError=true)]
     [return: MarshalAs(UnmanagedType.Bool)]
     static extern bool CloseHandle(IntPtr hObject);
     
[DllImport("ntdll.dll", SetLastError=true)]
static extern int NtQueryInformationProcess(IntPtr processHandle,
   int processInformationClass, ref PROCESS_BASIC_INFORMATION processInformation, uint processInformationLength,
   out int returnLength);
   
   /*
	[DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory
        (
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            UInt32 nSize,
            ref UInt32 lpNumberOfBytesRead
        );
   */     
        
[DllImport("kernel32.dll")]
static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, UInt32 dwProcessId);
     
void HoockDetect()
{
textBox1.Text= "Detecting hooks for process whit the name "+ProcessName+" and PID="+procid.ToString()+"\r\n";

byte[] Forread = new byte[0x500];
uint BytesRead=0;
int CompileAddress = 0;
IntPtr processHandle = IntPtr.Zero;

try
{
processHandle = OpenProcess(ProcessAccess.QueryInformation|ProcessAccess.VMRead, false, (uint)procid);
}
catch
{
}
if (processHandle!=IntPtr.Zero)
{
ProcModule.ModuleInfo targetmscorjit = null;
ProcModule.ModuleInfo[] modules = ProcModule.GetModuleInfos(procid);

if (modules!=null&&modules.Length>0)
{
for (int i=0;i<modules.Length;i++)
{
if (modules[i].baseName.ToLower().Contains("mscorjit"))
{
targetmscorjit = modules[i];
break;
}
}
}

if (targetmscorjit==null)
{
textBox1.Text= textBox1.Text + "Seems that the target process is not a .NET process!"+"\r\n";
}
else
{

int getJitrva = ExportTable.ProcGetExpAddress(processHandle,targetmscorjit.baseOfDll,"getJit");
bool isok=false;
isok = ReadProcessMemory(processHandle,
(IntPtr)((long)targetmscorjit.baseOfDll+(long)getJitrva),Forread,(uint)Forread.Length, ref BytesRead);
if (isok)
{
int count = 0;
while (Forread[count]!=0x0C3)
{
count++;
}

long cmpointer = (long)targetmscorjit.baseOfDll+getJitrva+count+1;
textBox1.Text= textBox1.Text + "Pointer of compile method : "+cmpointer.ToString("X8")+"\r\n";

CompileAddress =  BitConverter.ToInt32(Forread,count+1);
textBox1.Text= textBox1.Text + "Address of compile method is : "+CompileAddress.ToString("X8")+"\r\n";

if ((CompileAddress<(int)targetmscorjit.baseOfDll)||(CompileAddress>(int)targetmscorjit.baseOfDll+targetmscorjit.sizeOfImage))
{
textBox1.Text= textBox1.Text + "Address of compile method changed!!!"+"\r\n";
}
else
{
textBox1.Text= textBox1.Text +
"Address of compile method seems to be the original one!"+"\r\n";

}

}
else
{
textBox1.Text= textBox1.Text + "Failed to read from selected process!"+"\r\n";
}
ProcModule.CloseHandle(processHandle);
}  // end if is not .NET

}
else
{
textBox1.Text= textBox1.Text + "Failed to open selected process!"+"\r\n";
}


}

void EnumEnvironmentVars()
{
this.Text="Environment Variables for "+ProcessName+" whit PID="+procid.ToString();


 if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {

        try
        {
		IntPtr hProcess =
		OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, (uint)procid);
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
         // RTL_USER_PROCESS_PARAMETERS structure
         byte[] datas = new byte[0x64];
         isok = ReadProcessMemory(hProcess,AProcessParameters,datas,(uint)(datas.Length), ref BytesRead);  
         if (isok)
         {
         int EnvirAddress = BitConverter.ToInt32(datas,072);
         int blocksize = 0;
         IntPtr raddress = (IntPtr)(EnvirAddress+blocksize);
         int toskip = 0;

         while (ReadProcessMemory(hProcess,raddress,datas,4, ref BytesRead))
         {
         blocksize++;
         raddress = (IntPtr)(EnvirAddress+blocksize);
         if (toskip==0&&datas[0]==0&&datas[1]==0)
         {
         toskip = blocksize;
         }
         
         if (datas[0]==0&&datas[1]==0
         &&datas[2]==0&&datas[3]==0
         )
         {
         break;
         }
         }
// read datas:
toskip = toskip+2;
raddress = (IntPtr)(EnvirAddress+toskip);
datas = new byte[blocksize-toskip];
ReadProcessMemory(hProcess,raddress,datas,(uint)datas.Length, ref BytesRead);
System.Text.Encoding encoding = System.Text.Encoding.Unicode;
string envirstring = encoding.GetString(datas);
envirstring = envirstring.Replace("\x0000","\r\n");
envirstring = envirstring+"\r\n";
textBox1.Text = envirstring;
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

void DirectoriesFilesList()
{

this.Text="Directories/Files from "+ProcessName+" whit PID="+procid.ToString();

string filelist = "";
string directorylist = "";

        //try
        //{
		IntPtr hProcess =
		OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, 0, (uint)procid);
		if (hProcess!=IntPtr.Zero)
		{
		uint minaddress = 0;
		uint maxaddress = 0xF0000000;
		uint pagesize = 0x1000;
		
		try
		{
		MainForm.SYSTEM_INFO pSI = new MainForm.SYSTEM_INFO();
		MainForm.GetSystemInfo(ref pSI);
		minaddress = pSI.lpMinimumApplicationAddress;
		maxaddress = pSI.lpMaximumApplicationAddress;
		pagesize = pSI.dwPageSize;
		}
		catch
		{
		}
		
		bool isok;
		byte[] onepage = new byte[pagesize];
		uint BytesRead=0;
		
		for (uint j = minaddress; j < maxaddress; j+= pagesize)
    	{
		isok = MainForm.ReadProcessMemory(hProcess,j,onepage,pagesize, ref BytesRead);

		if (isok)
		{
		
		for (int k = 0; k < onepage.Length; k++)
        {
		if (onepage[k]==0x3A)
		{
		byte[] testbyte = new byte[1];
		isok = MainForm.ReadProcessMemory(hProcess,(uint)(j+k+1),testbyte,1, ref BytesRead);
		bool IsFinded = false;
		bool unicode = false;
		
		if (isok)
		{
		if (testbyte[0]==0x5C)
		{
		IsFinded = true;
		unicode = false;
		}
		else if (testbyte[0]==0)
		{
		testbyte = new byte[2];
		isok = MainForm.ReadProcessMemory(hProcess,(uint)(j+k+2),testbyte,2, ref BytesRead);
		if (isok)
		{
		if (testbyte[0]==0x5C&&testbyte[1]==00)
		{
		IsFinded = true;
		unicode = true;
		}
		}
		}
		}
		
		if (IsFinded)
		{
		string thepath = "";
		testbyte = new byte[2];
		testbyte[0] = 11;
		uint l=0;
		int onecharbefore = 1;
		if (unicode)  // if is unicode
		onecharbefore++;
		
		if (unicode)
		{
		uint hoha = (uint)(j+k);
		}
		
		while (true)
		{
		isok = MainForm.ReadProcessMemory(hProcess,(uint)(j+k-onecharbefore+l),testbyte,1, ref BytesRead);
		if (isok&&testbyte[0]!=0)
		{
		thepath = thepath+(char)testbyte[0];
		}
		else
		{
		break;
		}
		l++;
		if (unicode)  // if is unicode
		l++;
		}
		
		if (File.Exists(thepath))
		if (!filelist.Contains(thepath))
		filelist=filelist+thepath+"\r\n";
		
		if (Directory.Exists(thepath))
		if (!directorylist.Contains(thepath))
		directorylist=directorylist+thepath+"\r\n";
		
		}  // end of IsFinded if
		
		}
		}
		}
		}
		
		textBox1.Text = "Directories:\r\n"+directorylist+"\r\n"+
			"Files:\r\n"+filelist+"\r\n";
		}  // end of if the process can be opened!
        //}
        //catch
        //{
        //}
}
public string modulename = "";
public IntPtr baseaddress = IntPtr.Zero;

		public int RVA2Section(MainForm.image_section_header[] sections,int rva)
		{
		
		for (int i = 0; i < sections.Length; i++)
        {
		if ((sections[i].virtual_address<=rva)&&(sections[i].virtual_address + sections[i].virtual_size >= rva))
        return i;
		}
		
		return -1;
		}
		
unsafe void CodeSectionDifferences()
{
this.Text="Process name: "+ProcessName+"; PID="+procid.ToString();
textBox1.Text="Code section diferences in module "+modulename+" base address:"+
	baseaddress.ToString("X4")+"\r\n";
	
if (!File.Exists(modulename))
{
textBox1.Text=textBox1.Text+"The file: "+modulename+"don't exist!" +
	"Finding diferences aborted!"+"\r\n";
}
else
{
byte[] filebytes = File.ReadAllBytes(modulename);
if (filebytes.Length<0x200||filebytes[0]!=0x4D||filebytes[1]!=0x5A)
{
textBox1.Text = textBox1.Text+"Invalid PE file: "+modulename+"\r\n";
}
else
{
int PEOffset=BitConverter.ToInt32(filebytes, 0x03C);
if (PEOffset<=0||PEOffset>=filebytes.Length||
    filebytes[PEOffset]!=0x50||filebytes[PEOffset+1]!=0x45)
{
textBox1.Text = textBox1.Text+"Invalid PE file: "+modulename+"\r\n";
}
else
{
short nrofsection=BitConverter.ToInt16(filebytes, PEOffset+0x6);
short sizeofoptionalheader=BitConverter.ToInt16(filebytes, PEOffset+0x14);
int BaseOfCode=BitConverter.ToInt32(filebytes, PEOffset+0x1C);

MainForm.image_section_header[] sections = new MainForm.image_section_header[nrofsection];

long ptr = (long)(PEOffset)+(long)sizeofoptionalheader+4+
	(long)Marshal.SizeOf(typeof(MainForm.IMAGE_FILE_HEADER));
byte[] datakeeper = new byte[Marshal.SizeOf(typeof(MainForm.image_section_header))];
IntPtr pointer = IntPtr.Zero;

for (int i = 0; i < nrofsection; i++)
{
Array.Copy(filebytes,ptr,datakeeper,0,Marshal.SizeOf(typeof(MainForm.image_section_header)));
fixed (byte* p = datakeeper)
{
pointer = (IntPtr)p;
}

sections[i] = (MainForm.image_section_header)Marshal.PtrToStructure(pointer, typeof(MainForm.image_section_header));
ptr = ptr+(long)Marshal.SizeOf(typeof(MainForm.image_section_header));
}

int codesectionindex = RVA2Section(sections,BaseOfCode);
if (codesectionindex==-1)
{
textBox1.Text = textBox1.Text+"Failed to get code section for the file: "+modulename+"\r\n";
}
else
{

}

}

}
}


}

void EmptyFormShown(object sender, EventArgs e)
{
if (whattodo==1)
HoockDetect();
else if (whattodo==2)
EnumEnvironmentVars();
else if (whattodo==3)
DirectoriesFilesList();
else if (whattodo==4)
CodeSectionDifferences();
}
}

}
