/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 02.03.2011
 * Time: 00:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace Mega_Dumper
{
	/// <summary>
	/// Description of ViewWindowsHoocks.
	/// </summary>
	public partial class ViewWindowsHoocks : Form
	{
		public ViewWindowsHoocks()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
public static extern uint GetModuleHandleW(string libname);
 
[DllImport("kernel32.dll", CharSet=CharSet.Ansi)]
unsafe static extern byte* GetProcAddress(uint hModule, string procName);

[DllImport("ntdll.dll")]
public static extern uint NtCurrentTeb();

public enum HookType : uint
{
     WH_JOURNALRECORD = 0,
     WH_JOURNALPLAYBACK = 1,
     WH_KEYBOARD = 2,
     WH_GETMESSAGE = 3,
     WH_CALLWNDPROC = 4,
     WH_CBT = 5,
     WH_SYSMSGFILTER = 6,
     WH_MOUSE = 7,
     WH_HARDWARE = 8,
     WH_DEBUG = 9,
     WH_SHELL = 10,
     WH_FOREGROUNDIDLE = 11,
     WH_CALLWNDPROCRET = 12,    
     WH_KEYBOARD_LL = 13,
     WH_MOUSE_LL = 14
}

/*
typedef struct _HOOK
{
    // 0x00  ULONG Handle;
    // 0x04  ULONG LockObj;
	// 0x08  PVOID ThreadInfo;
	// 0x0C  PVOID Desktop1;
	// 0x10  PVOID Self;
    // 0x14  PVOID NextHook;
    // 0x18  LONG HookType;
    // 0x1C  PVOID FunctionAddress;
    // 0x20  ULONG Flags;
    // 0x24  ULONG ModuleHandle;
    // 0x28  PVOID Hooked;
    // 0x2C  PVOID Desktop2;
    // 0x30 
}
or

typedef struct _HOOK {
ULONG hHook;
ULONG cLockObj;
PTHREADINFO pti;
ULONG rpdesk;
ULONG pSelf;
struct _HOOK *phkNext;
int iHook;
ULONG offPfn;
unsigned int flags;
int ihmod;
PTHREADINFO ptiHooked;
PDESKTOP rpdesk;
} HOOK, *PHOOK;

Fields:
- hHook
Handle to the hook procedure, it’s the value returned by SetWindowsHookEx and it comes from HMAllocObject
- clockObj
  unknown !!!
- pti
Pointer to THREADINFO structure of the process which sets the hook
- rpdesk
  unknown !!!
- pSelf
Pointer to this HOOK structure
- phkNext
Next structure in the hook chain
- iHook
Hook type (i.e. WH_MOUSE or WH_KEYBOARD). This is the first parameter
passed to SetWindowsHookEx
- offPfn
Offset of the filter procedure, it is obtained by a simple substration
between the address of the hook procedure and the initial address of the dll
- flags
HF_xxx flags (HF_GLOBALS, HF_LOCAL, HF_DESTROYED…)
- ihmod
Number of hooks set into the module
- ptiHooked
Pointer to THREADINFO structure of the hooked thread. If HF_GLOBAL
is setted the pointer is setted to NULL because the hook works for
every running thread


 */
 

unsafe void LogAllHooks()
{

/* at the end of USER32!UserRegisterWowHandlers:
7e4537f5 b8a010477e mov     eax,offset USER32!gSharedInfo (7e4710a0)
7e4537fa 5d         pop     ebp
7e4537fb c20800     ret     8
*/
string hoocktext = "";
hoocktext = hoocktext+"Init variables:"+"\r\n";
uint pgSharedInfo = 0x77D700A0;
uint* pointer = null;
byte* UserRegisterWowHandlers = GetProcAddress(GetModuleHandleW("user32.dll"), "UserRegisterWowHandlers");

hoocktext = hoocktext+"user32.UserRegisterWowHandlers: "+
 ((uint)UserRegisterWowHandlers).ToString("X8")+"\r\n";

uint i=0;
do
{
if (UserRegisterWowHandlers[i]==0x0B8)
{
pointer = (uint*)(UserRegisterWowHandlers+i+1);
pgSharedInfo = pointer[0]; // get SharedInfo
break;
}
i++;
} while (i<0x256);

hoocktext = hoocktext+"USER32!gSharedInfo: "+pgSharedInfo.ToString("X8")+"\r\n";
 
uint ptrSharedDelta = NtCurrentTeb()+0x6CC;  // 0x7FFDF000
hoocktext = hoocktext+"ptrSharedDelta: "+ptrSharedDelta.ToString("X8")+"\r\n";
uint SharedDelta = ((uint*)(ptrSharedDelta))[7];
hoocktext = hoocktext+"SharedDelta: "+SharedDelta.ToString("X8")+"\r\n";
pointer = (uint*)(((uint*)(pgSharedInfo))[0]);
uint HandleEntries = pointer[2];
hoocktext = hoocktext+"HandleEntries: "+HandleEntries.ToString("X8")+"\r\n";
uint UserHandleTable = ((uint*)(pgSharedInfo))[1];
hoocktext = hoocktext+"UserHandleTable: "+UserHandleTable.ToString("X8")+"\r\n";
hoocktext = hoocktext+"\r\n";
hoocktext = hoocktext+"Start login hooks..."+"\r\n";

for(i=0; i<HandleEntries; i++)
{

//i*12
byte* hook = (byte*)(i*12+UserHandleTable);
// _HANDLE_ENTRY
if (hook[8]==5) // if _HANDLE_ENTRY.Type is TYPE_HOOK
{
try
{

hoocktext = hoocktext+"\r\n";
hoocktext = hoocktext+"HOOK: "+((uint)hook).ToString("X8")+"\r\n";
uint HookInfo = ((uint*)(hook))[0] - SharedDelta;
hoocktext = hoocktext+"HookInfo: "+HookInfo.ToString("X8")+"\r\n";
uint hookhandle = ((uint*)(HookInfo))[0];
hoocktext = hoocktext+"HookHadle: "+hookhandle.ToString("X8")+"\r\n";
// ---------------------------------------------------
uint LockObj = ((uint*)(HookInfo))[1];
hoocktext = hoocktext+"LockObj: "+LockObj.ToString("X8")+"\r\n";;
uint threadinfov = ((uint*)(HookInfo))[2];
hoocktext = hoocktext+"ThreadInfo: "+threadinfov.ToString("X8")+"\r\n";
uint desktop1 = ((uint*)(HookInfo))[3];
hoocktext = hoocktext+"Desktop1: "+desktop1.ToString("X8")+"\r\n";
uint selfv = ((uint*)(HookInfo))[4];
hoocktext = hoocktext+"Self: "+selfv.ToString("X8")+"\r\n";
uint nexthookv = ((uint*)(HookInfo))[5];
hoocktext = hoocktext+"NextHook: "+nexthookv.ToString("X8")+"\r\n";
// Important values comes here:
// WH_CBT = 5 - are global hooks
uint hooktype = ((uint*)(HookInfo))[6];
if (hooktype<=14)
hoocktext = hoocktext+"HookType: "+((HookType)hooktype).ToString()+
             " - "+hooktype.ToString("X8")+"\r\n";
else
hoocktext = hoocktext+"HookType: "+((HookType)hooktype).ToString()+"\r\n";
	             
uint functionrva = ((uint*)(HookInfo))[7];
hoocktext = hoocktext+"FunctionRVA: "+functionrva.ToString("X8")+"\r\n";
uint flagsv = ((uint*)(HookInfo))[8];

string flagsstring = "";
if ((flagsv&1)!=0)  // G_HOOK_FLAG_ACTIVE = 1
{
flagsstring = "G_HOOK_FLAG_ACTIVE";
}

if ((flagsv&2)!=0)  // G_HOOK_FLAG_IN_CALL = 2
{
if (flagsstring!="") flagsstring=flagsstring+",";
flagsstring = flagsstring+"G_HOOK_FLAG_IN_CALL";
}
/*
#define HF_GLOBAL 0x0001
#define HF_ANSI 0x0002
#define HF_NEEDHC_SKIP 0x0004
#define HF_HUNG 0x0008
#define HF_HOOKFAULTED 0x0010
#define HF_NOPLAYBACKDELAY 0x0020
#define HF_WX86KNOWINDOWLL 0x0040
#define HF_DESTROYED 0x0080
 */

hoocktext = hoocktext+"Flags: "+flagsstring+" - "+flagsv.ToString("X8")+"\r\n";
uint ihmod = ((uint*)(HookInfo))[9];
hoocktext = hoocktext+"ModuleHooksCount: "+ihmod.ToString("X8")+"\r\n";
uint hookedp = ((uint*)(HookInfo))[10];
hoocktext = hoocktext+"ptiHooked: "+hookedp.ToString("X8")+"\r\n";
uint desktop2v = ((uint*)(HookInfo))[11];
hoocktext = hoocktext+"Desktop2: "+desktop2v.ToString("X8")+"\r\n";


}
catch
{

}

}
}
textBox1.Text = hoocktext;
}

void ViewWindowsHoocksShown(object sender, EventArgs e)
{
LogAllHooks();
}

		

}
}
