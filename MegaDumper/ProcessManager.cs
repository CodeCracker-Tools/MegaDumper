/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 27.02.2011
 * Time: 15:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Collections;

using ProcessUtils;

namespace Mega_Dumper
{
	/// <summary>
	/// Description of ProcessManager.
	/// </summary>
	/// 
	    internal class OrdinalCaseInsensitiveComparer : System.Collections.IComparer {
        internal static readonly OrdinalCaseInsensitiveComparer Default = new OrdinalCaseInsensitiveComparer();
  
        public int Compare(Object a, Object b) {
            String sa = a as String;
            String sb = b as String;
            if (sa != null && sb != null) {
                return String.CompareOrdinal(sa.ToUpperInvariant(), sb.ToUpperInvariant());
            }
            return System.Collections.Comparer.Default.Compare(a,b);
        }
    } 
	public partial class ProcessManager : Form
	{
		public ProcessManager()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}



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

     
     [DllImport("kernel32.dll")]
     static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, UInt32 dwProcessId);
     
     
     [DllImport("kernel32.dll", SetLastError=true)]
     [return: MarshalAs(UnmanagedType.Bool)]
     static extern bool CloseHandle(IntPtr hObject);
     
     [DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
     public static extern bool CloseHandle(HandleRef handle);
 

		public string DirectoryName = "";
		void Button1Click(object sender, EventArgs e)
		{
		OpenFileDialog fdlg = new OpenFileDialog();
		fdlg.Title = "Browse for program:";
		fdlg.InitialDirectory = @"c:\";
		if (DirectoryName!="") fdlg.InitialDirectory = DirectoryName;
		fdlg.Filter = "All files (*.exe)|*.exe";
		fdlg.FilterIndex = 2;
		fdlg.RestoreDirectory = true;
		fdlg.Multiselect = false;
		if(fdlg.ShowDialog() == DialogResult.OK)
		{
		string FileName = fdlg.FileName;
		textBox1.Text = FileName;
		int lastslash = FileName.LastIndexOf("\\");
		if (lastslash!=-1) DirectoryName = FileName.Remove(lastslash,FileName.Length-lastslash);
        if (DirectoryName.Length==2) DirectoryName=DirectoryName+"\\";
		}
		}
		
[DllImport("kernel32", EntryPoint="CreateProcessW", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
public static extern int CreateProcess(IntPtr lpApplicationName, IntPtr lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, int bInheritHandles, int dwCreationFlags, IntPtr lpEnvironment, IntPtr lpCurrentDriectory, ref STARTUPINFO lpStartupInfo, ref PROCESS_INFORMATION lpProcessInformation);
 
[DllImport( "kernel32.dll")]
public static extern bool ResumeThread(IntPtr hThread);

[DllImport("kernel32.dll")]
public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,[In, Out] byte[] buffer, UInt32 size, out uint lpNumberOfBytesWritten);


[DllImport("kernel32.dll", SetLastError=true)]
public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

public struct PROCESS_INFORMATION
{
public IntPtr hProcess;
public IntPtr hThread;
public uint dwProcessId;
public uint dwThreadId;
}

public struct STARTUPINFO
{
public uint cb;
public string lpReserved;
public string lpDesktop;
public string lpTitle;
public uint dwX;
public uint dwY;
public uint dwXSize;
public uint dwYSize;
public uint dwXCountChars;
public uint dwYCountChars;
public uint dwFillAttribute;
public uint dwFlags;
public short wShowWindow;
public short cbReserved2;
public IntPtr lpReserved2;
public IntPtr hStdInput;
public IntPtr hStdOutput;
public IntPtr hStdError;
}

public enum ProcessCreationFlags : int
{
   DEBUG_PROCESS              = 0x1,
   DEBUG_ONLY_THIS_PROCESS    = 0x2,
   CREATE_SUSPENDED           = 0x4,
   DETACHED_PROCESS           = 0x8,
   CREATE_NEW_CONSOLE         = 0x10,
   NORMAL_PRIORITY_CLASS      = 0x20,
   IDLE_PRIORITY_CLASS        = 0x40,
   HIGH_PRIORITY_CLASS        = 0x80,
   REALTIME_PRIORITY_CLASS    = 0x100,
   CREATE_NEW_PROCESS_GROUP   = 0x200,
   CREATE_UNICODE_ENVIRONMENT = 0x400,
   CREATE_SEPARATE_WOW_VDM    = 0x800,
   CREATE_SHARED_WOW_VDM      = 0x1000,
   CREATE_FORCEDOS            = 0x2000,
   CREATE_DEFAULT_ERROR_MODE  = 0x4000000,
   CREATE_NO_WINDOW           = 0x8000000
}
	
const UInt32 INFINITE = 0xFFFFFFFF;

        public static byte[] ToByteArray(StringDictionary sd, bool unicode)
        {
            // get the keys
            string[] keys = new string[sd.Count];
            byte[] envBlock = null;
            sd.Keys.CopyTo(keys, 0);
            
            // get the values
            string[] values = new string[sd.Count];
            sd.Values.CopyTo(values, 0);
            
            // sort both by the keys
            // Windows 2000 requires the environment block to be sorted by the key
            // It will first converting the case the strings and do ordinal comparison.
            Array.Sort(keys, values, OrdinalCaseInsensitiveComparer.Default);

            // create a list of null terminated "key=val" strings
            StringBuilder stringBuff = new StringBuilder();
            for (int i = 0; i < sd.Count; ++ i) {
                stringBuff.Append(keys[i]);
                stringBuff.Append('=');
                stringBuff.Append(values[i]);
                stringBuff.Append('\0');
            }
            // an extra null at the end indicates end of list.
            stringBuff.Append('\0');
                        
            if( unicode) {
                envBlock = Encoding.Unicode.GetBytes(stringBuff.ToString());                        
            }
            else
                envBlock = Encoding.Default.GetBytes(stringBuff.ToString());

            //if (envBlock.Length > UInt16.MaxValue)
            //    throw new InvalidOperationException(SR.GetString(SR.EnvironmentBlockTooLong, envBlock.Length));

            return envBlock;
        }
        
public static IntPtr hprocess;
public static uint cprocessid;
public static IntPtr hcthread;
public static ProcModule.ModuleInfo targetkernel32 = null;
public static ProcModule.ModuleInfo targetMSVCR80 = null;

[DllImport("kernel32.dll")]
static extern uint SuspendThread(IntPtr hThread);

[DllImport("kernel32.dll")]
static extern bool Sleep(long Milliseconds);

[DllImport("psapi.dll", CharSet=CharSet.Auto)]
public static extern bool EnumProcessModules(IntPtr handle, IntPtr modules, int size, ref int needed);
 
[DllImport("kernel32.dll", ExactSpelling=true)]
static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
   uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

[DllImport("Kernel32.dll")]
public static extern bool ReadProcessMemory
(
 IntPtr hProcess,
 IntPtr lpBaseAddress,
 byte[] lpBuffer,
 UInt32 nSize,
 ref UInt32 lpNumberOfBytesRead
 );
 
 
[Flags]
public enum AllocationType
{
     Commit = 0x1000,
     Reserve = 0x2000,
     Decommit = 0x4000,
     Release = 0x8000,
     Reset = 0x80000,
     Physical = 0x400000,
     TopDown = 0x100000,
     WriteWatch = 0x200000,
     LargePages = 0x20000000
}

[Flags]
public enum MemoryProtection
{
     Execute = 0x10,
     ExecuteRead = 0x20,
     ExecuteReadWrite = 0x40,
     ExecuteWriteCopy = 0x80,
     NoAccess = 0x01,
     ReadOnly = 0x02,
     ReadWrite = 0x04,
     WriteCopy = 0x08,
     GuardModifierflag = 0x100,
     NoCacheModifierflag = 0x200,
     WriteCombineModifierflag = 0x400
}

static void LoopWhileModulesAreLoadedNt(uint procid,IntPtr htread)
{

IntPtr processHandle = IntPtr.Zero;
try
{
processHandle = OpenProcess(ProcessAccess.QueryInformation|ProcessAccess.VMRead, false, (uint)procid);
}
catch
{
}

if (processHandle==IntPtr.Zero)
return;

int oldneeded = 0;

while (true)
{
ResumeThread(htread);
Sleep(1);
SuspendThread(htread);

int newneeded = 0;
EnumProcessModules(processHandle,IntPtr.Zero,0,ref newneeded);
if (newneeded>oldneeded)  // new module is loaded!
{
oldneeded = newneeded;
if (newneeded>=12)  // if we have at last 3 module
return;
}



}
}

[DllImport("kernel32.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);

System.Windows.Forms.Timer checktimer;
public void CheckStatut(object source, EventArgs e)
{
uint exitcode = 0;
GetExitCodeProcess(hprocess,out exitcode);

if (exitcode!=259)  // STILL_ACTIVE = 259
{
button2.Enabled = true;
textBox3.AppendText("\r\n"+"Process terminated, exit code: "+exitcode.ToString()+"\r\n");

checktimer.Stop();
checktimer.Dispose();
checktimer = null;

hprocess = IntPtr.Zero;
cprocessid = 0;
hcthread = IntPtr.Zero;
targetkernel32 = null;
targetMSVCR80 = null;
VirtualAlloc1 = IntPtr.Zero;
VirtualAlloc2 = IntPtr.Zero;
VirtualAlloc3 = IntPtr.Zero;
}
else
{
byte[] keeper = new byte[4];
uint BytesRead=0;
if (VirtualAlloc1!=IntPtr.Zero&&ReadProcessMemory(hprocess,VirtualAlloc1,keeper,4, ref BytesRead))
{
if (keeper[0]!=0||keeper[1]!=0||keeper[2]!=0||keeper[2]!=0)
{
int ESPvalue = BitConverter.ToInt32(keeper,0);
byte[] eraser = new byte[]{0,0,0,0};
byte[] infiniteloop = new byte[]{0x0EB,0x0FE};
byte[] nops = new byte[]{0x090,0x090};

// write infinit loop to second:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc1+12),infiniteloop,2, out BytesRead);
// nop the first and write infinit loop back:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc1+10),nops,2, out BytesRead);
Sleep(2);
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc1+10),infiniteloop,2, out BytesRead);

textBox3.AppendText("\r\n");
textBox3.AppendText("CreateProcessW reached:"+"\r\n");
textBox3.AppendText("Value of ESP:"+ESPvalue.ToString("X8")+"\r\n");
byte[] parbytes = new byte[14*4];

if (ReadProcessMemory(hprocess,(IntPtr)ESPvalue,parbytes,(uint)parbytes.Length, ref BytesRead))
{
int[] parameters = new int[14];
for (int i=0;i<parameters.Length;i++)
{
parameters[i]=BitConverter.ToInt32(parbytes,i*4);
}

textBox3.AppendText("Return address: "+parameters[0].ToString("X8")+"\r\n");
textBox3.AppendText("hToken: "+parameters[1].ToString("X8")+"\r\n");
textBox3.AppendText("ModuleFileName: "+parameters[2].ToString("X8")+"  ");
string result = MemStringReader.ReadUnicodeString(hprocess,(IntPtr)parameters[2]);
if (result!=null)
{
textBox3.AppendText(result);
}
textBox3.AppendText("\r\n");

textBox3.AppendText("CommandLine: "+parameters[3].ToString("X8")+"  ");
result = MemStringReader.ReadUnicodeString(hprocess,(IntPtr)parameters[3]);
if (result!=null)
{
textBox3.AppendText(result);
}
textBox3.AppendText("\r\n");

textBox3.AppendText("pProcessSecurity: "+parameters[4].ToString("X8")+"\r\n");
textBox3.AppendText("pThreadSecurity: "+parameters[5].ToString("X8")+"\r\n");
textBox3.AppendText("InheritHandles: "+parameters[6].ToString("X8")+"\r\n");
textBox3.AppendText("CreationFlags: "+parameters[7].ToString("X8")+"\r\n");
textBox3.AppendText("pEnvironment: "+parameters[8].ToString("X8")+"\r\n");
textBox3.AppendText("CurrentDir: "+parameters[9].ToString("X8")+"  ");
result = MemStringReader.ReadUnicodeString(hprocess,(IntPtr)parameters[9]);
if (result!=null)
{
textBox3.AppendText(result);
}
textBox3.AppendText("\r\n");

textBox3.AppendText("pStartupInfo: "+parameters[10].ToString("X8")+"\r\n");
textBox3.AppendText("pProcessInfo: "+parameters[11].ToString("X8")+"\r\n");
textBox3.AppendText("hNewToken: "+parameters[12].ToString("X8")+"\r\n");

}

// clean old value:
WriteProcessMemory(hprocess,VirtualAlloc1,eraser,4, out BytesRead);


// suspend thread an let the user choose when to continue
SuspendThread(hcthread);
// finaly release second infinite loop:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc1+12),nops,2, out BytesRead);

}
}

if (VirtualAlloc2!=IntPtr.Zero&&ReadProcessMemory(hprocess,VirtualAlloc2,keeper,4, ref BytesRead))
{
if (keeper[0]!=0||keeper[1]!=0||keeper[2]!=0||keeper[2]!=0)
{
int ESPvalue = BitConverter.ToInt32(keeper,0);
byte[] eraser = new byte[]{0,0,0,0};
byte[] infiniteloop = new byte[]{0x0EB,0x0FE};
byte[] nops = new byte[]{0x090,0x090};

// write infinit loop to second:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc2+12),infiniteloop,2, out BytesRead);
// nop the first and write infinit loop back:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc2+10),nops,2, out BytesRead);
Sleep(2);
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc2+10),infiniteloop,2, out BytesRead);

textBox3.AppendText("\r\n");
textBox3.AppendText("CreateProcessW reached:"+"\r\n");
textBox3.AppendText("Value of ESP:"+ESPvalue.ToString("X8")+"\r\n");
byte[] parbytes = new byte[14*4];

if (ReadProcessMemory(hprocess,(IntPtr)ESPvalue,parbytes,(uint)parbytes.Length, ref BytesRead))
{
int[] parameters = new int[14];
for (int i=0;i<parameters.Length;i++)
{
parameters[i]=BitConverter.ToInt32(parbytes,i*4);
}

textBox3.AppendText("Return address: "+parameters[0].ToString("X8")+"\r\n");
textBox3.AppendText("hToken: "+parameters[1].ToString("X8")+"\r\n");
textBox3.AppendText("ModuleFileName: "+parameters[2].ToString("X8")+"  ");
string result = MemStringReader.ReadAsciiString(hprocess,(IntPtr)parameters[2]);
if (result!=null)
{
textBox3.AppendText(result);
}
textBox3.AppendText("\r\n");

textBox3.AppendText("CommandLine: "+parameters[3].ToString("X8")+"  ");
result = MemStringReader.ReadAsciiString(hprocess,(IntPtr)parameters[3]);
if (result!=null)
{
textBox3.AppendText(result);
}
textBox3.AppendText("\r\n");

textBox3.AppendText("pProcessSecurity: "+parameters[4].ToString("X8")+"\r\n");
textBox3.AppendText("pThreadSecurity: "+parameters[5].ToString("X8")+"\r\n");
textBox3.AppendText("InheritHandles: "+parameters[6].ToString("X8")+"\r\n");
textBox3.AppendText("CreationFlags: "+parameters[7].ToString("X8")+"\r\n");
textBox3.AppendText("pEnvironment: "+parameters[8].ToString("X8")+"\r\n");
textBox3.AppendText("CurrentDir: "+parameters[9].ToString("X8")+"  ");
result = MemStringReader.ReadAsciiString(hprocess,(IntPtr)parameters[9]);
if (result!=null)
{
textBox3.AppendText(result);
}
textBox3.AppendText("\r\n");

textBox3.AppendText("pStartupInfo: "+parameters[10].ToString("X8")+"\r\n");
textBox3.AppendText("pProcessInfo: "+parameters[11].ToString("X8")+"\r\n");
textBox3.AppendText("hNewToken: "+parameters[12].ToString("X8")+"\r\n");

}

// clean old value:
WriteProcessMemory(hprocess,VirtualAlloc2,eraser,4, out BytesRead);

// suspend thread an let the user choose when to continue
SuspendThread(hcthread);
// finaly release second infinite loop:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc2+12),nops,2, out BytesRead);

}
}

if (VirtualAlloc3!=IntPtr.Zero&&ReadProcessMemory(hprocess,VirtualAlloc3,keeper,4, ref BytesRead))
{
if (keeper[0]!=0||keeper[1]!=0||keeper[2]!=0||keeper[2]!=0)
{
int EBPvalue = BitConverter.ToInt32(keeper,0);

byte[] eraser = new byte[]{0,0,0,0};
byte[] infiniteloop = new byte[]{0x0EB,0x0FE};
byte[] nops = new byte[]{0x090,0x090};

// write infinit loop to second:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc3+0x1C),infiniteloop,2, out BytesRead);
// nop the first and write infinit loop back:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc3+0x1A),nops,2, out BytesRead);
Sleep(2);
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc3+0x1A),infiniteloop,2, out BytesRead);

textBox3.AppendText("\r\n");
textBox3.AppendText("memcpy reached:"+"\r\n");
textBox3.AppendText("Value of EBP:"+EBPvalue.ToString("X8")+"\r\n");

byte[] parbytes = new byte[5*4];

if (ReadProcessMemory(hprocess,(IntPtr)EBPvalue,parbytes,(uint)parbytes.Length, ref BytesRead))
{
int[] parameters = new int[5];
for (int i=0;i<parameters.Length;i++)
{
parameters[i]=BitConverter.ToInt32(parbytes,i*4);
}

textBox3.AppendText("Old ESP: "+parameters[0].ToString("X8")+"\r\n");
textBox3.AppendText("Return Address: "+parameters[1].ToString("X8")+"\r\n");
textBox3.AppendText("Source: "+parameters[3].ToString("X8"));
string result = MemStringReader.ReadAsciiString(hprocess,(IntPtr)parameters[3]);
if (result!=null)
{
textBox3.AppendText("  "+result);
}
textBox3.AppendText("\r\n");

textBox3.AppendText("len: (hex) "+parameters[4].ToString("X8")+"\r\n");
textBox3.AppendText("Destination: "+parameters[2].ToString("X8")+"\r\n");
}

// clean old value:
WriteProcessMemory(hprocess,VirtualAlloc3,eraser,4, out BytesRead);

// suspend thread an let the user choose when to continue
SuspendThread(hcthread);
// finaly release second infinite loop:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc3+0x1C),nops,2, out BytesRead);



}
}


}

}

void Button2Click(object sender, EventArgs e)
{
string filename = textBox1.Text;
string enviroments = textBox2.Text;

if (filename!=""&&File.Exists(filename))
{

int processflags = processflags = 0 | (int)ProcessCreationFlags.CREATE_SUSPENDED;
IntPtr memptr = IntPtr.Zero;
button2.Enabled = false;
textBox3.Text = "";
GCHandle gchenv = new GCHandle();

if (enviroments.Length!=0)
{
if (enviroments.Length>=3&&enviroments.Contains("="))
{
textBox3.AppendText("Setting Environment Variables...");

try
{

char[] separator = "\r\n".ToCharArray();
string[] realvalues = enviroments.Split(separator);
IntPtr newptr = memptr;
bool unicode = false;
if (Environment.OSVersion.Platform == PlatformID.Win32NT)
{
processflags = processflags | (int)ProcessCreationFlags.CREATE_UNICODE_ENVIRONMENT;
unicode = true;
}
StringDictionary environmentVariables = new StringDictionary();

// add Environments of the parent to the child!
foreach (System.Collections.DictionaryEntry entry in Environment.GetEnvironmentVariables())
{
environmentVariables.Add((string) entry.Key, (string) entry.Value);
}

for (int i=0;i<realvalues.Length;i++)
{
if (realvalues[i]!=""&&realvalues[i].Contains("="))
{
separator = "=".ToCharArray();
string[] currentvalue = realvalues[i].Split(separator);
if (currentvalue[0]!="")
{
if (environmentVariables.ContainsKey(currentvalue[0]))
{
textBox3.AppendText("\r\n"+"The key whit the name "+
	currentvalue[0]+" is already on dictinonary!");
}
else
{
environmentVariables.Add(currentvalue[0],currentvalue[1]);
}
}

}
}


gchenv = GCHandle.Alloc(ToByteArray(environmentVariables, unicode), GCHandleType.Pinned);
memptr = gchenv.AddrOfPinnedObject();

textBox3.AppendText("\r\n"+"Environment Variables setted!");
}
catch (Exception exc)
{
textBox3.AppendText("\r\n"+exc.Message+"\r\n");

}

}
else
{
textBox3.AppendText("Invalid Environment Variables!"+"\r\n");
}
}

STARTUPINFO structure = new STARTUPINFO();
PROCESS_INFORMATION process_information = new PROCESS_INFORMATION();
IntPtr lpApplicationName = Marshal.StringToHGlobalUni(filename);
try
{
CreateProcess(lpApplicationName, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0, processflags, memptr, IntPtr.Zero, ref structure, ref process_information);
hprocess = process_information.hProcess;
hcthread = process_information.hThread;
cprocessid = process_information.dwProcessId;
if (gchenv.IsAllocated)
gchenv.Free();

textBox3.AppendText("\r\n"+"Process created");
if (checkBox1.Checked)
{
textBox3.AppendText(" on suspended mode");
button3.Enabled = true;
}
textBox3.AppendText("!"+"\r\n");
}
catch (Exception ex3)
{
textBox3.AppendText("\r\n"+"Failed to start process whit the message"+
	                    ex3.Message+"\r\n");
return;
}

checktimer = new System.Windows.Forms.Timer();
checktimer.Interval = 30;
checktimer.Enabled = true;
checktimer.Tick += new System.EventHandler (CheckStatut);

if (checkBox2.Checked)
{
ProcModule.ModuleInfo[] modules = null;

for (int k=0;k<500;k++)  // try it 500 times!
{
LoopWhileModulesAreLoadedNt(process_information.dwProcessId,process_information.hThread);
modules = ProcModule.GetModuleInfos((int)process_information.dwProcessId);
if (modules!=null&&modules.Length>0)
{
for (int i=0;i<modules.Length;i++)
{
if (modules[i].baseName.ToLower().Contains("kernel32"))
{
targetkernel32 = modules[i];
break;
}
}
}
if (targetkernel32!=null)
break;
}

if (targetkernel32!=null&&targetkernel32.baseOfDll!=IntPtr.Zero)
HookCreateProcess(targetkernel32.baseOfDll);

}


if (checkBox3.Checked)
{
ProcModule.ModuleInfo[] modules = null;

for (int k=0;k<50;k++)  // try it 50 times!
{
LoopWhileModulesAreLoadedNt(process_information.dwProcessId,process_information.hThread);
modules = ProcModule.GetModuleInfos((int)process_information.dwProcessId);
if (modules!=null&&modules.Length>0)
{
for (int i=0;i<modules.Length;i++)
{
if (modules[i].baseName.Length>10&&modules[i].baseName.ToLower().StartsWith("msvcr"))
{
targetMSVCR80 = modules[i];
break;
}
}
}
if (targetMSVCR80!=null)
break;
}

if (targetMSVCR80!=null&&targetMSVCR80.baseOfDll!=IntPtr.Zero)
LogmemcpyInit(targetMSVCR80.baseOfDll);

}


if (!checkBox1.Checked)
{
ResumeThread(process_information.hThread);
}



}
else
{
textBox3.Text = "Please select a valid file!"+"\r\n";
}


		}





/*
CreateProcessW:
7C802332 >  8BFF         MOV EDI,EDI
7C802334    55           PUSH EBP
7C802335    8BEC         MOV EBP,ESP
7C802337    6A 00        PUSH 0
7C802339    FF75 2C      PUSH DWORD PTR SS:[EBP+2C]
7C80233C    FF75 28      PUSH DWORD PTR SS:[EBP+28]
7C80233F    FF75 24      PUSH DWORD PTR SS:[EBP+24]
7C802342    FF75 20      PUSH DWORD PTR SS:[EBP+20]
7C802345    FF75 1C      PUSH DWORD PTR SS:[EBP+1C] 
7C802348    FF75 18      PUSH DWORD PTR SS:[EBP+18]
7C80234B    FF75 14      PUSH DWORD PTR SS:[EBP+14]
7C80234E    FF75 10      PUSH DWORD PTR SS:[EBP+10]
7C802351    FF75 0C      PUSH DWORD PTR SS:[EBP+C]
7C802354    FF75 08      PUSH DWORD PTR SS:[EBP+8]
7C802357    6A 00        PUSH 0
7C802359    E8 C6730100  CALL 7C819724 ; KERNEL32.CreateProcessInternalW
7C80235E    5D           POP EBP
7C80235F    C2 2800      RETN 28

7C802367    8BFF          MOV EDI,EDI
7C802369    55            PUSH EBP
7C80236A    8BEC          MOV EBP,ESP
7C80236C    6A 00         PUSH 0
7C80236E    FF75 2C       PUSH DWORD PTR SS:[EBP+2C]
7C802371    FF75 28       PUSH DWORD PTR SS:[EBP+28]
7C802374    FF75 24       PUSH DWORD PTR SS:[EBP+24]
7C802377    FF75 20       PUSH DWORD PTR SS:[EBP+20]
7C80237A    FF75 1C       PUSH DWORD PTR SS:[EBP+1C]
7C80237D    FF75 18       PUSH DWORD PTR SS:[EBP+18]
7C802380    FF75 14       PUSH DWORD PTR SS:[EBP+14]
7C802383    FF75 10       PUSH DWORD PTR SS:[EBP+10]
7C802386    FF75 0C       PUSH DWORD PTR SS:[EBP+C]
7C802389    FF75 08       PUSH DWORD PTR SS:[EBP+8]
7C80238C    6A 00         PUSH 0
7C80238E    E8 2BB10100   CALL 7C81D4BE ; KERNEL32.CreateProcessInternalA
7C802393    5D            POP EBP
7C802394    C2 2800       RETN 28
*/
     
public static IntPtr VirtualAlloc1;
public static IntPtr VirtualAlloc2;
public static IntPtr VirtualAlloc3;

public void HookCreateProcess(IntPtr kernelbase)
{
if (cprocessid==0||hprocess==IntPtr.Zero) return;

int CreateProcessWRva = ExportTable.ProcGetExpAddress(
hprocess,kernelbase,"CreateProcessW");

int CreateProcessARva = ExportTable.ProcGetExpAddress(
hprocess,kernelbase,"CreateProcessA");

if (CreateProcessWRva==0||CreateProcessARva==0) return;

IntPtr CreateProcessWPatch1 = IntPtr.Zero;
IntPtr CreateProcessAPatch2 = IntPtr.Zero;

int tobecalled1 = 0;
int tobecalled2 = 0;

byte[] Forread = new byte[0x500];
uint BytesRead=0;
if (!ReadProcessMemory(hprocess,(IntPtr)((long)kernelbase+(long)CreateProcessWRva),Forread,(uint)0x500, ref BytesRead))
return;

int count = 0;
while (count<Forread.Length)
{
if (Forread[count]==0x0E8)
{
break;
}
count++;
}

long CreateProcessWcall = (long)kernelbase+(long)CreateProcessWRva+count;
tobecalled1 = BitConverter.ToInt32(Forread,count+1);
tobecalled1 = tobecalled1 + (int)CreateProcessWcall+5;
CreateProcessWPatch1 = (IntPtr)(CreateProcessWcall+1);

if (!ReadProcessMemory(hprocess,(IntPtr)((long)kernelbase+(long)CreateProcessARva),Forread,(uint)0x500, ref BytesRead))
return;

count = 0;
while (count<Forread.Length)
{
if (Forread[count]==0x0E8)
{
break;
}
count++;
}

long CreateProcessAcall = (long)kernelbase+(long)CreateProcessARva+count;
tobecalled2 = BitConverter.ToInt32(Forread,count+1);
tobecalled2 = tobecalled2 + (int)CreateProcessAcall+5;
CreateProcessAPatch2 = (IntPtr)(CreateProcessAcall+1);

// alocate memory for hooks:
VirtualAlloc1 = VirtualAllocEx(hprocess, IntPtr.Zero, 512, AllocationType.Commit, MemoryProtection.ReadWrite);
VirtualAlloc2 = VirtualAllocEx(hprocess, IntPtr.Zero, 512, AllocationType.Commit, MemoryProtection.ReadWrite);

if (VirtualAlloc1==IntPtr.Zero||VirtualAlloc2==IntPtr.Zero) return;

IntPtr bodyPlace1 = (IntPtr)((long)VirtualAlloc1+4);
IntPtr bodyPlace2 = (IntPtr)((long)VirtualAlloc2+4);

// body of first hook:
byte[] hoockbody = new byte[]{
0x89, 0x25, 0x34, 0x32, 0x24, 0x00,
0xEB, 0xFE,
0x90, 0x90,
0xBE, 0x35, 0x45, 0x53, 0x43,
0xFF, 0xE6};

byte[] fixer = BitConverter.GetBytes((int)VirtualAlloc1);

for (int k=0;k<fixer.Length;k++)
{
hoockbody[k+2]=fixer[k];
}

fixer = BitConverter.GetBytes(tobecalled1);

for (int k=0;k<fixer.Length;k++)
{
hoockbody[k+11]=fixer[k];
}

WriteProcessMemory(hprocess,bodyPlace1,hoockbody,(uint)hoockbody.Length,out BytesRead);

// body of second hook:
fixer = BitConverter.GetBytes((int)VirtualAlloc2);

for (int k=0;k<fixer.Length;k++)
{
hoockbody[k+2]=fixer[k];
}

fixer = BitConverter.GetBytes(tobecalled2);

for (int k=0;k<fixer.Length;k++)
{
hoockbody[k+11]=fixer[k];
}

WriteProcessMemory(hprocess,bodyPlace2,hoockbody,(uint)hoockbody.Length,out BytesRead);


// jump to my hoock1:
int jumptomyhook1 = (int)bodyPlace1-(int)CreateProcessWcall-5;
byte[] jumpbytes = BitConverter.GetBytes(jumptomyhook1);
WriteProcessMemory(hprocess,CreateProcessWPatch1,jumpbytes,(uint)jumpbytes.Length,out BytesRead);

// jump to my hoock2:
int jumptomyhook2 = (int)bodyPlace2-(int)CreateProcessAcall-5;
jumpbytes = BitConverter.GetBytes(jumptomyhook2);
WriteProcessMemory(hprocess,CreateProcessAPatch2,jumpbytes,(uint)jumpbytes.Length,out BytesRead);

textBox3.AppendText("CreateProcess hoocked!"+"\r\n");

}

/*
MSVCR80.memcpy:
78144FB0    55              PUSH EBP
78144FB1    8BEC            MOV EBP,ESP
78144FB3    57              PUSH EDI
78144FB4    56              PUSH ESI
78144FB5    8B75 0C         MOV ESI,DWORD PTR SS:[EBP+C]
78144FB8    8B4D 10         MOV ECX,DWORD PTR SS:[EBP+10]
78144FBB    8B7D 08         MOV EDI,DWORD PTR SS:[EBP+8]
78144FBE    8BC1            MOV EAX,ECX 

for finding right one: n should be >= size of one page:
00D40004    8B75 0C             MOV ESI,DWORD PTR SS:[EBP+C]
00D40007    8B4D 10             MOV ECX,DWORD PTR SS:[EBP+10]
00D4000A    817D 10 00100000    CMP DWORD PTR SS:[EBP+10],1000
00D40011    7D 01               JGE SHORT 00D40014
00D40013    C3                  RETN
; do my code!


 */ 
 
public void LogmemcpyInit(IntPtr MSVCR80base)
{
if (cprocessid==0||hprocess==IntPtr.Zero) return;

int memcpyRva = ExportTable.ProcGetExpAddress(
hprocess,MSVCR80base,"memcpy");

if (memcpyRva==0) return;

byte[] Forread = new byte[0x50];
uint BytesRead=0;
if (!ReadProcessMemory(hprocess,(IntPtr)((long)MSVCR80base+(long)memcpyRva),Forread,0x50, ref BytesRead))
return;


int count = 0;
while (count<Forread.Length)
{
if (Forread[count]==0x8B&&Forread[count+1]<0x80)
{
break;
}
count++;
}

byte[] oldbytes = new byte[6];
for (int i=0;i<oldbytes.Length;i++)
{
oldbytes[i]=Forread[count+i];
}
// alocate memory for hook:
VirtualAlloc3 = VirtualAllocEx(hprocess, IntPtr.Zero, 512, AllocationType.Commit, MemoryProtection.ReadWrite);
if (VirtualAlloc3==IntPtr.Zero) return;

// write old bytes under my code cave!
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc3+4),oldbytes,(uint)oldbytes.Length,out BytesRead);

byte[] mycode = new byte[]
{ 0x81, 0x7D, 0x10, 0x00, 0x20, 0x00, 0x00, 0x7D, 0x01, 0xC3 };

// write my code:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc3+oldbytes.Length+4),mycode,(uint)mycode.Length,out BytesRead);


// body of first hook:
byte[] hoockbody = new byte[]
{ 0x89, 0x2D, 0x34, 0x32, 0x24, 0x00,
0xEB, 0xFE,
0x90, 0x90,
0x0C3
};

byte[] fixer = BitConverter.GetBytes((int)VirtualAlloc3);

for (int k=0;k<fixer.Length;k++)
{
hoockbody[k+2]=fixer[k];
}

// write hoock body:
WriteProcessMemory(hprocess,(IntPtr)((long)VirtualAlloc3+mycode.Length+oldbytes.Length+4),hoockbody,(uint)hoockbody.Length,out BytesRead);


// jump to my hoock3:
int jumptomyhook3 = (int)VirtualAlloc3+4-(int)MSVCR80base-memcpyRva-count-5;
byte[] jumpbytes = BitConverter.GetBytes(jumptomyhook3);

oldbytes[0]=0xE8;  // E8 = call
oldbytes[5]=0x90;  // last byte nop
for (int i=0;i<4;i++)
{
oldbytes[i+1]=jumpbytes[i];
}

WriteProcessMemory(hprocess,(IntPtr)((long)MSVCR80base+(long)memcpyRva+count),oldbytes,(uint)oldbytes.Length,out BytesRead);


textBox3.AppendText("Logging memcpy enabled!"+"\r\n");

}
		
void Button3Click(object sender, EventArgs e)
{
if (hcthread!=IntPtr.Zero)
{
ResumeThread(hcthread);
}
 //textBox3.SelectionStart = textBox3.Text.Length;
 //textBox3.ScrollToCaret();
 //textBox3.Refresh();


}
		
void ProcessManagerFormClosing(object sender, FormClosingEventArgs e)
{
uint exitcode = 0;
GetExitCodeProcess(hprocess,out exitcode);

if (exitcode==259)  // STILL_ACTIVE = 259
{


}

}
		
}
}
