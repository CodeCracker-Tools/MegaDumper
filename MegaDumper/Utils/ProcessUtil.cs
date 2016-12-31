/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 11.10.2010
 * Time: 15:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text;
using System.IO;
using System.Threading;

namespace ProcessUtils
{

	public class ProcModule
	{

//  begin of enum modules

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
     public static extern bool CloseHandle(IntPtr hObject);
     
     [DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true, ExactSpelling=true)]
     public static extern bool CloseHandle(HandleRef handle);
 

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

 	[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
	public static extern bool Module32First(HandleRef handle, IntPtr entry);
 
	[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
	public static extern bool Module32Next(HandleRef handle, IntPtr entry);
 

		
	public static bool get_IsNt()
	{
    return (Environment.OSVersion.Platform == PlatformID.Win32NT);
	}

    /// This data structure contains information about a module
    /// in a process that is collected in bulk by querying the
    /// operating system. 
    /// The reason to make this a separate structure from the ProcessModule
    /// component is so that we can throw it away all at once when
    /// Refresh is called on the component. 

    public class ModuleInfo
    {
        public string baseName;
        public string fileName;
        public IntPtr baseOfDll;
        public IntPtr entryPoint; 
        public int sizeOfImage;
        public int Id; // used only on win9x - for matching up with ProcessInfo.mainModuleId 
    } 

[StructLayout(LayoutKind.Sequential)]
public class WinModuleEntry
{
    public const int sizeofModuleName = 0x100;
    public const int sizeofFileName = 260;
    public int dwSize;
    public int th32ModuleID;
    public int th32ProcessID;
    public int GlblcntUsage;
    public int ProccntUsage;
    public IntPtr modBaseAddr = IntPtr.Zero;
    public int modBaseSize;
    public IntPtr hModule = IntPtr.Zero;
}

 

     public static ModuleInfo[] WinGetModuleInfos(uint processId)
     {
            IntPtr handle = (IntPtr)(-1);
            GCHandle bufferHandle = new GCHandle(); 
            ArrayList moduleInfos = new ArrayList();
 
            try {
                handle = CreateToolhelp32Snapshot(8, processId);
                if (handle == (IntPtr)(-1))
                {
                return null;
                }
                int entrySize = Marshal.SizeOf(typeof(WinModuleEntry));
                int bufferSize = entrySize + WinModuleEntry.sizeofFileName + WinModuleEntry.sizeofModuleName;
                int[] buffer = new int[bufferSize / 4];
                bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned); 
                IntPtr bufferPtr = bufferHandle.AddrOfPinnedObject();
                Marshal.WriteInt32(bufferPtr, bufferSize); 
 
                HandleRef handleRef = new HandleRef(null, handle);
 
                if (Module32First(handleRef, bufferPtr)) {
                    do {
                        WinModuleEntry module = new WinModuleEntry();
                        Marshal.PtrToStructure(bufferPtr, module); 
                        ModuleInfo moduleInfo = new ModuleInfo();
                        moduleInfo.baseName = Marshal.PtrToStringAnsi((IntPtr)((long)bufferPtr + entrySize)); 
                        moduleInfo.fileName = Marshal.PtrToStringAnsi((IntPtr)((long)bufferPtr + entrySize + WinModuleEntry.sizeofModuleName)); 
                        moduleInfo.baseOfDll = module.modBaseAddr;
                        moduleInfo.sizeOfImage = module.modBaseSize; 
                        moduleInfo.Id = module.th32ModuleID;
                        moduleInfos.Add(moduleInfo);
                        Marshal.WriteInt32(bufferPtr, bufferSize);
                    } 
                    while (Module32Next(handleRef, bufferPtr));
                } 
            } 
            finally {
                if (bufferHandle.IsAllocated) bufferHandle.Free(); 
                if (handle != (IntPtr)(-1)) CloseHandle(new HandleRef(null, handle));
            }
 
            ModuleInfo[] temp = new ModuleInfo[moduleInfos.Count];
            moduleInfos.CopyTo(temp, 0); 
            return temp; 
        }
    
public static ModuleInfo[] NtGetModuleInfos(int processId)
{
return NtGetModuleInfos(processId, false);
}

		public static bool IsOSOlderThanXP()
       {
       return Environment.OSVersion.Version.Major < 5 ||
       (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0);

       }
		
[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
public static extern IntPtr OpenProcess(ProcessAccess access, bool inherit, int processId);
 
[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
internal static extern int GetCurrentProcessId();

[DllImport("psapi.dll", CharSet=CharSet.Auto)]
public static extern bool EnumProcessModules(IntPtr handle, IntPtr modules, int size, ref int needed);
 
// There is no need to call CloseProcess or to use a SafeHandle if you get the handle
// using GetCurrentProcess as it returns a pseudohandle 
[DllImport("kernel32.dll",CallingConvention = CallingConvention.Winapi)]
[return: MarshalAs(UnmanagedType.Bool)]
internal static extern bool IsWow64Process( 
                   [In] IntPtr hSourceProcessHandle, 
                   [Out, MarshalAs(UnmanagedType.Bool)] 
                   out bool isWow64);
        
[DllImport("psapi.dll", CharSet=CharSet.Auto)]
public static extern bool GetModuleInformation(IntPtr processHandle, HandleRef moduleHandle, NtModuleInfo ntModuleInfo, int size);
[StructLayout(LayoutKind.Sequential)]

public class NtModuleInfo
{
    public IntPtr BaseOfDll = IntPtr.Zero;
    public int SizeOfImage;
    public IntPtr EntryPoint = IntPtr.Zero;
}
 
[DllImport("psapi.dll", CharSet=CharSet.Auto)]
public static extern int GetModuleBaseName(IntPtr processHandle, HandleRef moduleHandle, StringBuilder baseName, int size);
 
[DllImport("psapi.dll", CharSet=CharSet.Auto)]
public static extern int GetModuleFileNameEx(IntPtr processHandle, HandleRef moduleHandle, StringBuilder baseName, int size);
 

        private static ModuleInfo[] NtGetModuleInfos(int processId, bool firstModuleOnly) { 
            // preserving Everett behavior. 
            //if( processId == SystemProcessID || processId == IdleProcessID)
            //{
            // system process and idle process doesn't have any modules 
            //    throw new Win32Exception(HResults.EFail,SR.GetString(SR.EnumProcessModuleFailed));
            //}

            IntPtr processHandle = IntPtr.Zero; 
            try {
             // PROCESS_QUERY_INFORMATION | PROCESS_VM_READ
                processHandle = OpenProcess(ProcessAccess.QueryInformation|ProcessAccess.VMRead, false, processId);

                IntPtr[] moduleHandles = new IntPtr[64];
                GCHandle moduleHandlesArrayHandle = new GCHandle(); 
                int moduleCount = 0;
                for (;;) {
                    bool enumResult = false;
                    try { 
                        moduleHandlesArrayHandle = GCHandle.Alloc(moduleHandles, GCHandleType.Pinned);
                        enumResult = EnumProcessModules(processHandle, moduleHandlesArrayHandle.AddrOfPinnedObject(), moduleHandles.Length * IntPtr.Size, ref moduleCount); 
 
                        // The API we need to use to enumerate process modules differs on two factors:
                        //   1) If our process is running in WOW64. 
                        //   2) The bitness of the process we wish to introspect.
                        //
                        // If we are not running in WOW64 or we ARE in WOW64 but want to inspect a 32 bit process
                        // we can call psapi!EnumProcessModules. 
                        //
                        // If we are running in WOW64 and we want to inspect the modules of a 64 bit process then 
                        // psapi!EnumProcessModules will return false with ERROR_PARTIAL_COPY (299).  In this case we can't 
                        // do the enumeration at all.  So we'll detect this case and bail out.
                        // 
                        // Also, EnumProcessModules is not a reliable method to get the modules for a process.
                        // If OS loader is touching module information, this method might fail and copy part of the data.
                        // This is no easy solution to this problem. The only reliable way to fix this is to
                        // suspend all the threads in target process. Of course we don't want to do this in Process class. 
                        // So we just to try avoid the ---- by calling the same method 50 (an arbitary number) times.
                        // 
                        if (!enumResult)
                        {
                            bool sourceProcessIsWow64 = false;
                            bool targetProcessIsWow64 = false; 
                            if (!IsOSOlderThanXP()) {
                                IntPtr hCurProcess = IntPtr.Zero;
                                try {
                                    hCurProcess = OpenProcess(ProcessAccess.QueryInformation,true,GetCurrentProcessId()); 
                                    bool wow64Ret;
 
                                    wow64Ret = IsWow64Process(hCurProcess, out sourceProcessIsWow64); 
                                    if (!wow64Ret) {
                                        return null; 
                                    }

                                    wow64Ret = IsWow64Process(processHandle, out targetProcessIsWow64);
                                    if (!wow64Ret) { 
                                        return null;
                                    } 
 
                                    if (sourceProcessIsWow64 && !targetProcessIsWow64) {
                                    // Wow64 isn't going to allow this to happen,
                                    // the best we can do is give a descriptive error to the user.
                                    // throw new Win32Exception(299 /* ERROR_PARTIAL_COPY */,
                                    // SR.GetString(SR.EnumProcessModuleFailedDueToWow));
                                    return null;
                                    }

                                } finally {
                                    if (hCurProcess != IntPtr.Zero) {
                                        CloseHandle(hCurProcess); 
                                    } 
                                }
                            } 

                            // If the failure wasn't due to Wow64, try again.
                            for (int i = 0; i < 50; i++) {
                                enumResult = EnumProcessModules(processHandle, moduleHandlesArrayHandle.AddrOfPinnedObject(), moduleHandles.Length * IntPtr.Size, ref moduleCount); 
                                if (enumResult) {
                                    break; 
                                } 
                                Thread.Sleep(1);
                            } 
                        }
                    }
                    finally {
                        moduleHandlesArrayHandle.Free(); 
                    }
 
                    if (!enumResult) { 
                        return null;
                    } 

                    moduleCount /= IntPtr.Size;
                    if (moduleCount <= moduleHandles.Length) break;
                    moduleHandles = new IntPtr[moduleHandles.Length * 2]; 
                }
                ArrayList moduleInfos = new ArrayList(); 
 
                int ret;
                for (int i = 0; i < moduleCount; i++) { 
                    ModuleInfo moduleInfo = new ModuleInfo();
                    IntPtr moduleHandle = moduleHandles[i];
                    NtModuleInfo ntModuleInfo = new NtModuleInfo();
                    if (!GetModuleInformation(processHandle, new HandleRef(null, moduleHandle), ntModuleInfo, Marshal.SizeOf(ntModuleInfo))) 
                        return null;
                    moduleInfo.sizeOfImage = ntModuleInfo.SizeOfImage; 
                    moduleInfo.entryPoint = ntModuleInfo.EntryPoint; 
                    moduleInfo.baseOfDll = ntModuleInfo.BaseOfDll;
 
                    StringBuilder baseName = new StringBuilder(1024);
                    ret = GetModuleBaseName(processHandle, new HandleRef(null, moduleHandle), baseName, baseName.Capacity * 2);
                    if (ret == 0) return null;;
                    moduleInfo.baseName = baseName.ToString();

                    StringBuilder fileName = new StringBuilder(1024); 
                    ret = GetModuleFileNameEx(processHandle, new HandleRef(null, moduleHandle), fileName, fileName.Capacity * 2); 
                    if (ret == 0) return null;;
                    moduleInfo.fileName = fileName.ToString(); 

                    // smss.exe is started before the win32 subsystem so it get this funny "\systemroot\.." path.
                    // We change this to the actual path by appending "smss.exe" to GetSystemDirectory()
                    if (string.Compare(moduleInfo.fileName, "\\SystemRoot\\System32\\smss.exe", StringComparison.OrdinalIgnoreCase) == 0) { 
                        moduleInfo.fileName = Path.Combine(Environment.SystemDirectory, "smss.exe");
                    } 
                    // Avoid returning Unicode-style long string paths.  IO methods cannot handle them. 
                    if (moduleInfo.fileName != null
                        && moduleInfo.fileName.Length >= 4 
                        && moduleInfo.fileName.StartsWith(@"\\?\", StringComparison.Ordinal)) {

                            moduleInfo.fileName = moduleInfo.fileName.Substring(4);
                    } 

                    moduleInfos.Add(moduleInfo); 
                    // 
                    // If the user is only interested in the main module, break now.
                    // This avoid some waste of time. In addition, if the application unloads a DLL 
                    // we will not get an exception.
                    //
                    if( firstModuleOnly) { break; }
                } 
                ModuleInfo[] temp = new ModuleInfo[moduleInfos.Count];
                moduleInfos.CopyTo(temp, 0); 
                return temp; 
            }
            finally { 
                //Debug.WriteLineIf(Process.processTracing.TraceVerbose, "Process - CloseHandle(process)");
                if (processHandle!=IntPtr.Zero)
                {
                CloseHandle(processHandle);
                } 
            }
        }
     
        public static ModuleInfo[] GetModuleInfos(int processId)
        {
        if (get_IsNt())
        return NtGetModuleInfos(processId); 
        else
        return WinGetModuleInfos((uint)processId);
        }
		
// end of get modules!

// begin of module Injection

		public const uint PROCESS_TERMINATE = 0x0001;
		public const uint PROCESS_CREATE_THREAD = 0x0002;
		public const uint PROCESS_SET_SESSIONID = 0x0004;
		public const uint PROCESS_VM_OPERATION = 0x0008;
		public const uint PROCESS_VM_READ = 0x0010;
		public const uint PROCESS_VM_WRITE = 0x0020;
		public const uint PROCESS_DUP_HANDLE = 0x0040;
		public const uint PROCESS_CREATE_PROCESS = 0x0080;
		public const uint PROCESS_SET_QUOTA = 0x0100;
		public const uint PROCESS_SET_INFORMATION = 0x0200;
		public const uint PROCESS_QUERY_INFORMATION = 0x0400;
		
		
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

[DllImport("kernel32.dll")]
public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess,  Int32 bInheritHandle, UInt32 dwProcessId);


[DllImport("kernel32.dll", ExactSpelling=true)]
public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
   uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

[DllImport("kernel32", ExactSpelling=true, CharSet=CharSet.Ansi)]
private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, ref int lpNumberOfBytesWritten);

[DllImport("kernel32.dll")]
public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,[In, Out] byte[] buffer, UInt32 size, out uint lpNumberOfBytesWritten);

// CreateRemoteThread, since ThreadProc is in remote process, we must use a raw function-pointer.
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(        
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          IntPtr lpStartAddress, // raw Pointer into remote process
          IntPtr lpParameter,
          uint dwCreationFlags,
          IntPtr lpThreadId
        );
        
[DllImport("kernel32.dll")]
public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

[DllImport("kernel32.dll", ExactSpelling=true)]
public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
   int dwSize, FreeType dwFreeType);
   
[Flags]
public enum FreeType
{
     Decommit = 0x4000,
     Release = 0x8000,
}

[DllImport("kernel32.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
public static extern bool GetExitCodeThread(IntPtr hThread, out IntPtr lpExitCode);

[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
public static extern IntPtr GetModuleHandleW(string lpModuleName);

[DllImport("kernel32.dll", CharSet=CharSet.Ansi)]
public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


public static string PostVerify(uint hprocess,string libFullPath)
{
if (hprocess==0)
{
return "Invalid process";
}

if (libFullPath==""||!File.Exists(libFullPath))
{
return "Invalid file!";
}

return "";
}

public static IntPtr InjectLibraryInternal(uint processid,string libFullPath,out string error)
{
error = PostVerify(processid,libFullPath);
if (error!="") return IntPtr.Zero;

ModuleInfo targetkernel = null;
ModuleInfo[] modules = GetModuleInfos((int)processid);

if (modules!=null&&modules.Length>0)
{
for (int i=0;i<modules.Length;i++)
{
if (modules[i].baseName.ToLower().Contains("kernel32"))
{
targetkernel = modules[i];
break;
}
}
}



// 
//	GetProcAddress(GetModuleHandleW("kernel32.dll"),"LoadLibraryA");
if (targetkernel==null||targetkernel.baseOfDll==IntPtr.Zero)
{
error = "Failed to get base of kernel32!";
return IntPtr.Zero;
}

IntPtr hprocess = IntPtr.Zero;
hprocess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ | PROCESS_CREATE_THREAD, 0, processid);
if (hprocess==IntPtr.Zero)
{
error = "Can't open selected process!";
return IntPtr.Zero;
}
else
{
int LoadLibraryArva = ExportTable.ProcGetExpAddress
	(hprocess, targetkernel.baseOfDll,"LoadLibraryA");
if (LoadLibraryArva==0)
{
CloseHandle(hprocess);
error = "Failed to get address of LoadLibraryA!";
return IntPtr.Zero;
}
IntPtr loadlibraryAddress = (IntPtr)((long)targetkernel.baseOfDll+(long)LoadLibraryArva);

uint sizeAscii = (uint)Encoding.ASCII.GetByteCount(libFullPath)+1;
// allocate memory to the local process for libFullPath
IntPtr pLibPath = VirtualAllocEx(hprocess, IntPtr.Zero, sizeAscii, AllocationType.Commit, MemoryProtection.ReadWrite);
if (pLibPath==IntPtr.Zero)
{
CloseHandle(hprocess);
error = "Can't alocate memory on process!";
return IntPtr.Zero;
}
else
{
int bytesWritten=0;
// write libFullPath to pLibPath
if (!WriteProcessMemory(hprocess,pLibPath,Marshal.StringToHGlobalAnsi(libFullPath),
	sizeAscii-1,ref bytesWritten)||bytesWritten != (int)sizeAscii-1)
{
VirtualFreeEx(hprocess, pLibPath, 0, FreeType.Release);
CloseHandle(hprocess);
error = "Can't write libname on process!";
return IntPtr.Zero;
}
else
{
// load dll via call to LoadLibrary using CreateRemoteThread
IntPtr hThread = CreateRemoteThread(hprocess,IntPtr.Zero, 0,
loadlibraryAddress, pLibPath, 0, IntPtr.Zero);

if (hThread==IntPtr.Zero)
{
VirtualFreeEx(hprocess, pLibPath, 0, FreeType.Release);
CloseHandle(hprocess);
error = "Can't create the remote thread!";
return IntPtr.Zero;
}
else
{
if (WaitForSingleObject(hThread,uint.MaxValue)!=0)
{
VirtualFreeEx(hprocess, pLibPath, 0, FreeType.Release);
CloseHandle(hprocess);
error = "Error on WaitForSingleObject!";
return IntPtr.Zero;
}
// get address of loaded module
IntPtr hLibModule = IntPtr.Zero;
if (!GetExitCodeThread(hThread, out hLibModule))
{
VirtualFreeEx(hprocess, pLibPath, 0, FreeType.Release);
CloseHandle(hprocess);
error = "Error on GetExitCodeThread!";
return IntPtr.Zero;
}

if (hLibModule == IntPtr.Zero)
{
VirtualFreeEx(hprocess, pLibPath, 0, FreeType.Release);
CloseHandle(hprocess);
error = "Code executed properly, but unable to get an appropriate module handle!";
return IntPtr.Zero;
}

error = "";
VirtualFreeEx(hprocess, pLibPath, 0, FreeType.Release);
CloseHandle(hprocess);
return hLibModule;
}

}


}

}

}
	

public static bool FreeLibraryInternal(uint processid,IntPtr libAddress,out string error)
{
ModuleInfo targetkernel = null;
ModuleInfo[] modules = GetModuleInfos((int)processid);

if (modules!=null&&modules.Length>0)
{
for (int i=0;i<modules.Length;i++)
{
if (modules[i].baseName.ToLower().Contains("kernel32"))
{
targetkernel = modules[i];
break;
}
}
}

if (targetkernel==null||targetkernel.baseOfDll==IntPtr.Zero)
{
error = "Failed to get base of kernel32!";
return false;
}

IntPtr hprocess = IntPtr.Zero;
hprocess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ | PROCESS_CREATE_THREAD, 0, processid);
if (hprocess==IntPtr.Zero)
{
error = "Can't open selected process!";
return false;
}
else
{
int FreeLibraryrva = ExportTable.ProcGetExpAddress
	(hprocess, targetkernel.baseOfDll,"FreeLibrary");
if (FreeLibraryrva==0)
{
CloseHandle(hprocess);
error = "Failed to get address of FreeLibrary!";
return false;
}
else
{
IntPtr FreeLibAddress = (IntPtr)((long)targetkernel.baseOfDll+(long)FreeLibraryrva);
// load dll via call to LoadLibrary using CreateRemoteThread
IntPtr hThread = CreateRemoteThread(hprocess,IntPtr.Zero, 0,
FreeLibAddress, libAddress, 0, IntPtr.Zero);

if (hThread==IntPtr.Zero)
{
CloseHandle(hprocess);
error = "Can't create the remote thread!";
return false;
}
else
{
if (WaitForSingleObject(hThread,uint.MaxValue)!=0)
{
CloseHandle(hprocess);
error = "Error on WaitForSingleObject!";
return false;
}
else
{
error = "";
return true;
}
}
}
}

}

	}
	
	public class ExportTable
	{
// begin of Export reader:
    
    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_EXPORT_DIRECTORY
    {
        public UInt32 Characteristics;
        public UInt32 TimeDateStamp;
        public UInt16 MajorVersion;
        public UInt16 MinorVersion;
        public UInt32 Name;
        public UInt32 Base;
        public UInt32 NumberOfFunctions;
        public UInt32 NumberOfNames;
        public UInt32 AddressOfFunctions;     // RVA from base of image
        public UInt32 AddressOfNames;     // RVA from base of image
        public UInt32 AddressOfNameOrdinals;  // RVA from base of image
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
        
public unsafe static int ProcGetExpAddress(
IntPtr hprocess,IntPtr hmodule,string exportnname)
{
byte[] Forread = new byte[8];
uint BytesRead = 0;
int PEOffset = 0;
int ExportDirectoryRVA = 0;
int ExportDirectorySize = 0;

if (!ReadProcessMemory(hprocess,(IntPtr)((long)hmodule+0x3C),Forread,4, ref BytesRead))
return -1;

PEOffset = BitConverter.ToInt32(Forread,0);

if (!ReadProcessMemory(hprocess,(IntPtr)((long)hmodule+PEOffset+0x78),Forread,8, ref BytesRead))
return -1;

try
{
ExportDirectoryRVA  = BitConverter.ToInt32(Forread,0);
ExportDirectorySize = BitConverter.ToInt32(Forread,4);
Forread = new byte[ExportDirectorySize];
if (!ReadProcessMemory(hprocess,(IntPtr)((long)hmodule+ExportDirectoryRVA),Forread,(uint)ExportDirectorySize, ref BytesRead))
return -1;

IntPtr ptPoit = Marshal.AllocHGlobal(ExportDirectorySize);
Marshal.Copy(Forread, 0, ptPoit, ExportDirectorySize);
IMAGE_EXPORT_DIRECTORY IED = (IMAGE_EXPORT_DIRECTORY)Marshal.PtrToStructure(ptPoit, typeof(IMAGE_EXPORT_DIRECTORY));
Marshal.FreeHGlobal(ptPoit);

int[] funcaddresses = new int[IED.NumberOfFunctions];
int[] funcnames = new int[IED.NumberOfNames];
ushort[] ordinals = new ushort[IED.NumberOfFunctions];

byte[] keeper = new byte[IED.NumberOfFunctions*4];
if (!ReadProcessMemory(hprocess,(IntPtr)((long)hmodule+IED.AddressOfFunctions),keeper,(uint)IED.NumberOfFunctions*4, ref BytesRead))
return -1;

for (int i=0;i<funcaddresses.Length;i++)
funcaddresses[i]=BitConverter.ToInt32(keeper,i*4);

if (!ReadProcessMemory(hprocess,(IntPtr)((long)hmodule+IED.AddressOfNames),keeper,(uint)IED.NumberOfNames*4, ref BytesRead))
return -1;

for (int i=0;i<funcnames.Length;i++)
funcnames[i]=BitConverter.ToInt32(keeper,i*4);

if (!ReadProcessMemory(hprocess,(IntPtr)((long)hmodule+IED.AddressOfNameOrdinals),keeper,(uint)IED.NumberOfFunctions*2, ref BytesRead))
return -1;

for (int i=0;i<funcnames.Length;i++)
ordinals[i]=BitConverter.ToUInt16(keeper,i*2);

// wrong: the first name is CLRCreateInstance
for (int i=0;i<funcnames.Length;i++)
{
if (funcnames[i]!=0)
{
byte[] currentchar = new byte[1];
int count = 0;
string funcname = "";
while (ReadProcessMemory(hprocess,(IntPtr)((long)hmodule+funcnames[i]+count),
      currentchar,1, ref BytesRead)&&currentchar[0]!=0)
{
funcname = funcname+(char)currentchar[0];
count++;
}

if (funcname==exportnname)
{
return funcaddresses[ordinals[i]];
}

}
}

}
catch
{
return -1;
}
return -1;
	}


// end of Export reader



	}
	
	public class MemStringReader
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
        
     
public unsafe static string ReadUnicodeString(
IntPtr hprocess,IntPtr address)
	{
if (hprocess==IntPtr.Zero||address==IntPtr.Zero)
return null;

string readedstring = "";
byte[] datas = new byte[2];
uint BytesRead = 0;

uint count=0;

while (ReadProcessMemory(hprocess,(IntPtr)((long)address+(long)count),datas,2, ref BytesRead))
{
if (datas[0]==0&&datas[1]==0)
{
break;
}
count=count+2;
}
if (count==0) return "";

datas = new byte[count];
if (!ReadProcessMemory(hprocess,(IntPtr)((long)address),datas,count, ref BytesRead))
return "";

System.Text.Encoding encoding = System.Text.Encoding.Unicode;
readedstring = encoding.GetString(datas);

return readedstring;
	}
	
           
public unsafe static string ReadAsciiString(
IntPtr hprocess,IntPtr address)
	{
if (hprocess==IntPtr.Zero||address==IntPtr.Zero)
return null;

string readedstring = "";
byte[] datas = new byte[1];
uint BytesRead = 0;

uint count=0;

while (ReadProcessMemory(hprocess,(IntPtr)((long)address+(long)count),datas,1, ref BytesRead))
{
if (datas[0]==0)
{
break;
}
count++;
}
if (count==0) return "";

datas = new byte[count];
if (!ReadProcessMemory(hprocess,(IntPtr)((long)address),datas,count, ref BytesRead))
return "";

System.Text.Encoding encoding = System.Text.Encoding.ASCII;
readedstring = encoding.GetString(datas);

return readedstring;
	}
	
	}
	
}

