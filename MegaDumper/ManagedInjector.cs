/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 01.01.2013
 * Time: 09:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using ProcessUtils;
using System.Text;

namespace MegaDumper
{
	
		public class InlineASM
		{
		public byte[] asm;
		int pos = 0;

		public InlineASM()
		{
		asm=new byte[500];
		pos=0;
		}
		
		public void PushOffset(IntPtr address)
		{
		asm[pos]=0x68;
		byte[] abytes = BitConverter.GetBytes((uint)address);
		asm[pos+1]=abytes[0];
		asm[pos+2]=abytes[1];
		asm[pos+3]=abytes[2];
		asm[pos+4]=abytes[3];
		pos=pos+5;
		}
		public void MovEaxValue(IntPtr address)
		{
		asm[pos]=0xB8;
		byte[] abytes = BitConverter.GetBytes((uint)address);
		asm[pos+1]=abytes[0];
		asm[pos+2]=abytes[1];
		asm[pos+3]=abytes[2];
		asm[pos+4]=abytes[3];
		pos=pos+5;
		}
		public void CallEax()
		{
		asm[pos]=0xFF;
		asm[pos+1]=0xD0;
		pos=pos+2;
		}
		public void CallEcx()
		{
		asm[pos]=0xFF;
		asm[pos+1]=0xD1;
		pos=pos+2;
		}
		public void CallEdx()
		{
		asm[pos]=0xFF;
		asm[pos+1]=0xD2;
		pos=pos+2;
		}
		public void PushEax()
		{
		asm[pos]=0x50;
		pos=pos+1;
		}
		public void PushByte(byte inputbyte)
		{
		asm[pos]=0x6A;
		asm[pos+1]=inputbyte;
		pos=pos+2;
		}
		public void MovEcxDwordPtr(IntPtr address)
		{
		asm[pos]=0x8B;
		asm[pos+1]=0x0D;
		byte[] abytes = BitConverter.GetBytes((uint)address);
		asm[pos+2]=abytes[0];
		asm[pos+3]=abytes[1];
		asm[pos+4]=abytes[2];
		asm[pos+5]=abytes[3];
		pos=pos+6;
		}
		public void MovEcxDwordPtrEdxOffset(byte offset)
		{
		asm[pos]=0x8B;
		asm[pos+1]=0x4A;
		asm[pos+2]=offset;
		pos=pos+3;
		}
		public void MovEaxDwordPtrEcxOffset(byte offset)
		{
		asm[pos]=0x8B;
		asm[pos+1]=0x41;
		asm[pos+2]=offset;
		pos=pos+3;
		}
		public void MovEdxDwordPtrEcxOffset(byte offset)
		{
		asm[pos]=0x8B;
		asm[pos+1]=0x51;
		asm[pos+2]=offset;
		pos=pos+3;
		}
		public void MovEcxDwordPtrEax()
		{
		asm[pos]=0x8B;
		asm[pos+1]=0x08;
		pos=pos+2;
		}
		public void MovEaxDwordPtr(IntPtr address)
		{
		asm[pos]=0xA1;
		byte[] abytes = BitConverter.GetBytes((uint)address);
		asm[pos+1]=abytes[0];
		asm[pos+2]=abytes[1];
		asm[pos+3]=abytes[2];
		asm[pos+4]=abytes[3];
		pos=pos+5;
		}
		public void MovEdxDwordPtrEcx()
		{
		asm[pos]=0x8B;
		asm[pos+1]=0x11;
		pos=pos+2;
		}
		public void Retn()
		{
		asm[pos]=0xC3;
		pos=pos+1;
		}
		}
		
	/// <summary>
	/// Description of ManagedInjector.
	/// </summary>
	public partial class ManagedInjector : Form
	{	
		static string processname;
		static int processid;
		static IntPtr hprocess = IntPtr.Zero;
		public ManagedInjector(string pname,int prid)
		{
			processname = pname;
			processid = prid;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
        static byte[] CLSID_CLRRuntimeHost = new byte[] { 0x6E, 0xA0, 0xF1, 0x90, 0x12, 0x77, 0x62, 0x47, 0x86, 0xB5, 0x7A, 0x5E, 0xBA, 0x6B, 0xDB, 0x02 };
        static byte[] IID_ICLRRuntimeHost = new byte[] { 0x6C, 0xA0, 0xF1, 0x90, 0x12, 0x77, 0x62, 0x47, 0x86, 0xB5, 0x7A, 0x5E, 0xBA, 0x6B, 0xDB, 0x02 };
		void ManagedInjectorShown(object sender, EventArgs e)
		{
this.Text="Managed injector in "+processname+" whit PID="+processid.ToString();

		}
		
		void Button2Click(object sender, EventArgs e)
		{
		HostCLR_RunMethod(textBox1.Text, textBox2.Text, textBox3.Text, "Testargs","v2.0.50727");
		}
		
		static IntPtr CorBindToRuntimeExAddress()
		{
ProcModule.ModuleInfo targetmscoree = null;
ProcModule.ModuleInfo[] modules = ProcModule.GetModuleInfos((int)processid);

if (modules!=null&&modules.Length>0)
{
for (int i=0;i<modules.Length;i++)
{
if (modules[i].baseName.ToLower().Contains("mscoree.dll"))
{
targetmscoree = modules[i];
break;
}
}
}


if (targetmscoree==null||targetmscoree.baseOfDll==IntPtr.Zero)
{
return IntPtr.Zero;
}

IntPtr CLRCreateInstanceAddress =IntPtr.Zero;

if (hprocess!=IntPtr.Zero)
{
int CLRCreateInstancerva = ExportTable.ProcGetExpAddress
	(hprocess, targetmscoree.baseOfDll,"CorBindToRuntimeEx");
if (CLRCreateInstancerva==0)
return IntPtr.Zero;

return (IntPtr)((long)targetmscoree.baseOfDll+(long)CLRCreateInstancerva);
}
return IntPtr.Zero;
		}
		
				
		static void WriteUnicodeString(IntPtr Address,string istring)
		{
		UnicodeEncoding Unicode = new UnicodeEncoding();
		byte[] ubytes = Unicode.GetBytes(istring);
		uint BytesRead=0;
		ProcModule.WriteProcessMemory(hprocess,Address,ubytes,(uint)ubytes.Length, out BytesRead);
		
		}
  public static void HostCLR_RunMethod(String AssemblyPath, String TypeName, String MethodName, String Args, String Version)
        {
hprocess = ProcModule.OpenProcess(ProcModule.PROCESS_QUERY_INFORMATION | ProcModule.PROCESS_VM_OPERATION | ProcModule.PROCESS_VM_WRITE | ProcModule.PROCESS_VM_READ | ProcModule.PROCESS_CREATE_THREAD, 0, (uint)processid);
IntPtr CorBindToRuntimeExPtr = CorBindToRuntimeExAddress();
uint BytesRead=0;

IntPtr codeCave_Code = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, 500, ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ExecuteReadWrite);
IntPtr CLSID_CLRRuntimeHostPtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, (uint)CLSID_CLRRuntimeHost.Length * 4, ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);
IntPtr IID_ICLRRuntimeHostPtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, (uint)IID_ICLRRuntimeHost.Length, ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);
IntPtr ClrHostPtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, 04, ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);
IntPtr dwRetPtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, 0x4, ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);

IntPtr AssemblyPathPtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, (uint)(AssemblyPath.Length*2 + 2), ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);
IntPtr TypeNamePtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, (uint)(TypeName.Length*2 + 2), ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);
IntPtr MethodNamePtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, (uint)(MethodName.Length*2 + 2), ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);
IntPtr ArgsPtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, (uint)(Args.Length*2 + 2), ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);
  
IntPtr BuildFlavorPtr = ProcModule.VirtualAllocEx(hprocess, IntPtr.Zero, 0x10, ProcModule.AllocationType.Commit, ProcModule.MemoryProtection.ReadWrite);

ProcModule.WriteProcessMemory(hprocess,CLSID_CLRRuntimeHostPtr,CLSID_CLRRuntimeHost,(uint)CLSID_CLRRuntimeHost.Length, out BytesRead);
ProcModule.WriteProcessMemory(hprocess,IID_ICLRRuntimeHostPtr,IID_ICLRRuntimeHost,(uint)IID_ICLRRuntimeHost.Length, out BytesRead);
WriteUnicodeString(BuildFlavorPtr, "wks");
WriteUnicodeString(AssemblyPathPtr, AssemblyPath);
WriteUnicodeString(TypeNamePtr, TypeName);
WriteUnicodeString(MethodNamePtr, MethodName);
WriteUnicodeString(ArgsPtr, Args);

InlineASM inline = new InlineASM();
inline.PushOffset(ClrHostPtr);
inline.PushOffset(IID_ICLRRuntimeHostPtr);
inline.PushOffset(CLSID_CLRRuntimeHostPtr);
inline.PushByte(0);
inline.PushOffset(BuildFlavorPtr);
inline.PushByte(0);
inline.MovEaxValue(CorBindToRuntimeExPtr);
inline.CallEax(); // call CorBindToRuntimeEx

inline.MovEaxDwordPtr(ClrHostPtr);
inline.MovEcxDwordPtrEax();
inline.MovEdxDwordPtrEcxOffset(0x0C);
inline.PushEax();
inline.CallEdx();  // pClrHost->Start();

inline.PushOffset(dwRetPtr);
inline.PushOffset(ArgsPtr);
inline.PushOffset(MethodNamePtr);
inline.PushOffset(TypeNamePtr);
inline.PushOffset(AssemblyPathPtr);
inline.MovEaxDwordPtr(ClrHostPtr);
inline.MovEcxDwordPtrEax();
inline.PushEax();
inline.MovEaxDwordPtrEcxOffset(0x2C);
inline.CallEax();  // pClrHost->ExecuteInDefaultAppDomain

inline.Retn();
ProcModule.WriteProcessMemory(hprocess,codeCave_Code,inline.asm,(uint)inline.asm.Length, out BytesRead);

IntPtr hThread = ProcModule.CreateRemoteThread(hprocess,IntPtr.Zero, 0,
codeCave_Code, IntPtr.Zero, 0, IntPtr.Zero);

/*
if (ProcModule.WaitForSingleObject(hThread,uint.MaxValue)!=0)
{
return;
}
*/

IntPtr retcode = IntPtr.Zero;
if (!ProcModule.GetExitCodeThread(hThread, out retcode))
{
return;
}

ProcModule.CloseHandle(hprocess);

  }
		public string DirectoryName = "";
		void Button1Click(object sender, EventArgs e)
		{
		OpenFileDialog fdlg = new OpenFileDialog();
		fdlg.Title = "Browse for assembly";
		fdlg.InitialDirectory = @"c:\";
		if (DirectoryName!="") fdlg.InitialDirectory = DirectoryName;
		fdlg.Filter = "Executable files (*.exe,*.dll)|*.exe;*.dll";
		fdlg.FilterIndex = 2;
		fdlg.RestoreDirectory = true;
		if(fdlg.ShowDialog() == DialogResult.OK)
		{
		string FileName = fdlg.FileName;
		textBox1.Text = FileName;
		int lastslash = FileName.LastIndexOf("\\");
		if (lastslash!=-1) DirectoryName = FileName.Remove(lastslash,FileName.Length-lastslash);
        if (DirectoryName.Length==2) DirectoryName=DirectoryName+"\\";
		}
		}
		
		void TextBox1DragEnter(object sender, DragEventArgs e)
		{
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
    	else
    	e.Effect = DragDropEffects.None;
		}
		
		void TextBox1DragDrop(object sender, DragEventArgs e)
		{
		try
        {
        Array a = (Array) e.Data.GetData(DataFormats.FileDrop);
        if(a != null)
        {
        string s = a.GetValue(0).ToString();
        int lastoffsetpoint = s.LastIndexOf(".");
        if (lastoffsetpoint != -1)
        {
        string Extension = s.Substring(lastoffsetpoint);
        Extension=Extension.ToLower();
        if (Extension == ".exe"||Extension == ".dll")
        {
        this.Activate();
        textBox1.Text = s;
        int lastslash = s.LastIndexOf("\\");
        if (lastslash!=-1) DirectoryName = s.Remove(lastslash,s.Length-lastslash);
        if (DirectoryName.Length==2) DirectoryName=DirectoryName+"\\";
        }
        }
        }
        }
        catch
        {

        }
		}
	}
}
