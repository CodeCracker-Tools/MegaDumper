/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 22.01.2011
 * Time: 14:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections;
using System.Globalization;

using ProcessUtils;

namespace Mega_Dumper
{
	/// <summary>
	/// Description of Form4.
	/// </summary>
	public partial class FrmModules : Form
	{
    public string ProcessName;
    public string DirName;
	public int procid;



		public FrmModules(string procname,int prid,string Dir)
		{
		ProcessName=procname;
		procid=prid;
		DirName=Dir;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		private struct TOKEN_PRIVILEGES
		{
			public int PrivilegeCount;
			public long Luid;
			public int Attributes;
		}
		
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
		
     [DllImport("kernel32.dll")]
     static extern IntPtr OpenProcess(UInt32 dwDesiredAccess,  Int32 bInheritHandle, UInt32 dwProcessId);
     
     
     
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
     

		private ProcModule.ModuleInfo[] modules = null;
		void EnumModules()
		{
		this.Text="Modules from "+ProcessName+" whit PID="+procid.ToString();
		modules = ProcModule.GetModuleInfos(procid);
		
		if (modules!=null&&modules.Length>0)
		{
		for (int i=0;i<modules.Length;i++)
		{
         Graphics g = lvmodules.CreateGraphics();
         Font objFont = new Font("Microsoft Sans Serif", 8);
         SizeF stringSize = new SizeF();
         stringSize = g.MeasureString(modules[i].baseName, objFont);
         int processlenght = (int)(stringSize.Width+lvmodules.Margin.Horizontal*2);

         if (processlenght>modulename.Width)
         {
         modulename.Width=processlenght;
         }

         string[] prcdetails = new string[]{modules[i].baseName,modules[i].baseOfDll.ToString("X8"),
         modules[i].sizeOfImage.ToString("X8"),modules[i].entryPoint.ToString("X8")	};
         ListViewItem proc = new ListViewItem(prcdetails);
         lvmodules.Items.Add(proc);

		}
		}
		
		
		}
		
		void Button1Click(object sender, EventArgs e)
		{
		lvmodules.Items.Clear();
		EnumModules();
		}
		
		public static bool BytesEqual(byte[] Array1,byte[] Array2)
		{
		if (Array1.Length!=Array2.Length) return false;
		for (int i = 0; i < Array1.Length; i++)
		{
		if (Array1[i]!=Array2[i]) return false;
		}
		return true;
		}
		
		void DumpToolStripMenuItemClick(object sender, EventArgs e)
		{
		DumpModule();
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
     
     
		void Button2Click(object sender, EventArgs e)
		{
string strtoset = "";
int count = lvmodules.Items.Count;
for (int i=0;i<count;i++)
{
strtoset = strtoset+lvmodules.Items[i].SubItems[0].Text+"\t";
strtoset = strtoset+lvmodules.Items[i].SubItems[1].Text+"\t";
strtoset = strtoset+lvmodules.Items[i].SubItems[2].Text+"\t";
strtoset = strtoset+lvmodules.Items[i].SubItems[3].Text+"\t";
strtoset = strtoset+"\r\n";
}
if (strtoset!="") Clipboard.SetText(strtoset);


		}
		
		void DumpModule()
		{
		IntPtr hProcess = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ | PROCESS_TERMINATE, 0, (uint)procid);
		
		if( hProcess == IntPtr.Zero)
		{
		IntPtr pDACL, pSecDesc;

		GetSecurityInfo( (int) Process.GetCurrentProcess().Handle, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4, 0, 0, out pDACL, IntPtr.Zero, out pSecDesc);
		hProcess = OpenProcess( 0x40000, 0, (uint)procid);
		SetSecurityInfo( (int) hProcess, /*SE_KERNEL_OBJECT*/ 6, /*DACL_SECURITY_INFORMATION*/ 4 | /*UNPROTECTED_DACL_SECURITY_INFORMATION*/ 0x20000000, 0, 0, pDACL, IntPtr.Zero);
		ProcModule.CloseHandle( hProcess);
		hProcess = OpenProcess( PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ | PROCESS_TERMINATE, 0, (uint)procid);
		}

		if( hProcess != IntPtr.Zero)
		{
		string newdirname = DirName;
		if (DirName.Length<2||!Directory.Exists(DirName))
		newdirname="C:\\";
		
		newdirname=Path.Combine(DirName,"Dumps");
		System.IO.Directory.CreateDirectory(newdirname);
		int ImageBase = System.Convert.ToInt32(lvmodules.Items[lvmodules.SelectedIndices[0]].SubItems[1].Text, 16);
		string moduleName = lvmodules.Items[lvmodules.SelectedIndices[0]].SubItems[0].Text;
		
		bool isok;
		uint speed = 0x1000;
		
		try
		{
		SYSTEM_INFO pSI = new SYSTEM_INFO();
		GetSystemInfo(ref pSI);
		speed = pSI.dwPageSize;
		}
		catch
		{
		}
		
		byte[] bigMem = new byte[speed];
		byte[] InfoKeep = new byte[8];
		uint BytesRead=0;
		
		    int nrofsection=0;
		    byte[] Dump=null;
		    byte[] Partkeep=null;
		    int filealignment=0;
		    int rawaddress;
		    int address=0;
		    int offset=0;
		    bool ShouldFixrawsize=false;

		    isok = ReadProcessMemory(hProcess,(uint)(ImageBase+0x03C),InfoKeep,4, ref BytesRead);
		    int PEOffset=BitConverter.ToInt32(InfoKeep, 0);
		    
		    try
		    {
		    isok = ReadProcessMemory(hProcess,(uint)(ImageBase+PEOffset+0x0F8+20),InfoKeep,4, ref BytesRead);
		    byte[] PeHeader = new byte[speed];

		    rawaddress = BitConverter.ToInt32(InfoKeep, 0);
		    int sizetocopy = rawaddress;
		    if (sizetocopy>speed) sizetocopy=(int)speed;
		    isok = ReadProcessMemory(hProcess,(uint)(ImageBase),PeHeader,(uint)sizetocopy, ref BytesRead);
			offset=offset+rawaddress;
			
			nrofsection = (int)BitConverter.ToInt16(PeHeader, PEOffset+0x06);
		    int sectionalignment = BitConverter.ToInt32(PeHeader, PEOffset+0x038);
		 	filealignment = BitConverter.ToInt32(PeHeader, PEOffset+0x03C);
		    
			int sizeofimage = BitConverter.ToInt32(PeHeader, PEOffset+0x050);

			int calculatedimagesize = BitConverter.ToInt32(PeHeader, PEOffset+0x0F8+012);
			
			for (int i = 0; i < nrofsection; i++)
            {
			int virtualsize = BitConverter.ToInt32(PeHeader, PEOffset+0x0F8+0x28*i+08);
            int toadd = (virtualsize%sectionalignment);
			if (toadd!=0) toadd = sectionalignment-toadd;
			calculatedimagesize = calculatedimagesize+virtualsize+toadd;
			}
			
			if (calculatedimagesize>sizeofimage) sizeofimage=calculatedimagesize;
		    Dump = new byte[sizeofimage];
		    Array.Copy(PeHeader,Dump,sizetocopy);
		    Partkeep = new byte[sizeofimage];

		    }
		    catch
		    {

		    }
		    
		
			int calcrawsize=0;
            for (int i = 0; i < nrofsection; i++)
            {
            
			int rawsize,virtualsize,virtualAddress;
            for (int l = 0; l < nrofsection; l++)
            {
            rawsize = BitConverter.ToInt32(Dump, PEOffset+0x0F8+0x28*l+16);
            virtualsize = BitConverter.ToInt32(Dump, PEOffset+0x0F8+0x28*l+08);
            virtualAddress = BitConverter.ToInt32(Dump, PEOffset+0x0F8+0x28*l+012);
            
            // RawSize = Virtual Size rounded on FileAlligment
            calcrawsize=0;
            calcrawsize = virtualsize%filealignment;
            if (calcrawsize!=0) calcrawsize = filealignment-calcrawsize;
            calcrawsize = virtualsize+calcrawsize;

            if (calcrawsize!=0&&rawsize!=calcrawsize&&rawsize!=virtualsize)
            {
            ShouldFixrawsize=true;
            break;
            }
            }
            
            rawsize = BitConverter.ToInt32(Dump, PEOffset+0x0F8+0x28*i+16);
            virtualsize = BitConverter.ToInt32(Dump, PEOffset+0x0F8+0x28*i+08);
            // RawSize = Virtual Size rounded on FileAlligment
            virtualAddress = BitConverter.ToInt32(Dump, PEOffset+0x0F8+0x28*i+012);
            
            
            if (ShouldFixrawsize)
            {
            rawsize = virtualsize;
            BinaryWriter writer = new BinaryWriter(new MemoryStream(Dump));
            writer.BaseStream.Position=PEOffset+0x0F8+0x28*i+16;
            writer.Write(virtualsize);
            writer.BaseStream.Position=PEOffset+0x0F8+0x28*i+20;
            writer.Write(virtualAddress);
            writer.Close();

            }
            
    
                        
            address = BitConverter.ToInt32(Dump, PEOffset+0x0F8+0x28*i+12);
            
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+address),Partkeep,(uint)rawsize, ref BytesRead);
            if (!isok)
            {
            byte[] onepage = new byte[512];
            for (int c = 0; c < virtualsize; c=c+512)
            {
isok = ReadProcessMemory(hProcess,(uint)(ImageBase+virtualAddress+c),onepage,(uint)512, ref BytesRead);
Array.Copy(onepage, 0, Partkeep, c, 512);
            }
            }
         
            
            if (ShouldFixrawsize)
            {
            Array.Copy(Partkeep, 0, Dump, virtualAddress, rawsize);
            offset = virtualAddress+rawsize;
            }
            else
            {
            Array.Copy(Partkeep, 0, Dump, offset, rawsize);
            offset = offset+rawsize;
            }
            }
		    
		    
         
           
           
            if (Dump!=null&&Dump.Length>0&&Dump.Length>=offset)
            {
            int ImportDirectoryRva = BitConverter.ToInt32(Dump, PEOffset+0x080);
            if (ImportDirectoryRva>0&&ImportDirectoryRva<offset)
            {
            int current=0;
            int ThunkToFix=0;
            int ThunkData=0;
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ImportDirectoryRva+current+12),Partkeep,4, ref BytesRead);
            int NameOffset = BitConverter.ToInt32(Partkeep, 0);
            while (isok&&NameOffset!=0)
            {
            byte[] mscoreeAscii = {0x6D, 0x73, 0x63, 0x6F, 0x72, 0x65, 0x65, 0x2E, 0x64, 0x6C, 0x6C, 0x00};
            byte[] NameKeeper = new byte[mscoreeAscii.Length];
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+NameOffset),NameKeeper,(uint)mscoreeAscii.Length, ref BytesRead);
            if (isok&&BytesEqual(NameKeeper,mscoreeAscii))
            {
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ImportDirectoryRva+current),Partkeep,4, ref BytesRead);
            int OriginalFirstThunk = BitConverter.ToInt32(Partkeep, 0);  // OriginalFirstThunk;
            if (OriginalFirstThunk>0&&OriginalFirstThunk<offset)
            {
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+OriginalFirstThunk),Partkeep,4, ref BytesRead);
            ThunkData = BitConverter.ToInt32(Partkeep, 0);
            if (ThunkData>0&&ThunkData<offset)
            {
            byte[] CorExeMain = {0x5F, 0x43, 0x6F, 0x72, 0x45, 0x78, 0x65, 0x4D, 0x61, 0x69, 0x6E, 0x00};
            byte[] CorDllMain = {0x5F, 0x43, 0x6F, 0x72, 0x44, 0x6C, 0x6C, 0x4D, 0x61, 0x69, 0x6E, 0x00};
            NameKeeper = new byte[CorExeMain.Length];
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ThunkData+2),NameKeeper,
            (uint)CorExeMain.Length, ref BytesRead);
            if (isok&&(BytesEqual(NameKeeper,CorExeMain)||BytesEqual(NameKeeper,CorDllMain)))
            {
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ImportDirectoryRva+current+16),Partkeep,4, ref BytesRead);
            ThunkToFix = BitConverter.ToInt32(Partkeep, 0);  // FirstThunk;
            break;
            }
            }
            }
            }
            
            current = current+20; // 20 size of IMAGE_IMPORT_DESCRIPTOR
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ImportDirectoryRva+current+12),Partkeep,4, ref BytesRead);
            NameOffset = BitConverter.ToInt32(Partkeep, 0);
            }
            
            if (ThunkToFix>0&&ThunkToFix<offset)
            {
            BinaryWriter writer = new BinaryWriter(new MemoryStream(Dump));
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ThunkToFix),Partkeep,4, ref BytesRead);
            int ThunkValue = BitConverter.ToInt32(Partkeep, 0);
            if (isok&&(ThunkValue<0||ThunkValue>offset))
            {
            int fvirtualsize = BitConverter.ToInt32(Dump, PEOffset+0x0F8+08);
            int fvirtualAddress = BitConverter.ToInt32(Dump, PEOffset+0x0F8+012);
            int frawAddress = BitConverter.ToInt32(Dump, PEOffset+0x0F8+20);
            writer.BaseStream.Position=ThunkToFix-fvirtualAddress+frawAddress;
            writer.Write(ThunkData);
            }
            
            int EntryPoint = BitConverter.ToInt32(Dump, PEOffset+0x028);
            if (EntryPoint<=0||EntryPoint>offset)
            {
            int ca=0;
            do
            {
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ThunkData+ca),Partkeep,1, ref BytesRead);
            if (isok&&Partkeep[0]==0x0FF)
            {
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ThunkData+ca+1),Partkeep,1, ref BytesRead);
            if (isok&&Partkeep[0]==0x025)
            {
            isok = ReadProcessMemory(hProcess,(uint)(ImageBase+ThunkData+ca+2),Partkeep,4, ref BytesRead);
            if (isok)
            {
            int RealEntryPoint=ThunkData+ca;
            writer.BaseStream.Position=PEOffset+0x028;
            writer.Write(RealEntryPoint);
            }

            }
            }
            ca++;
            }
            while (isok);
            }
            writer.Close();
            }
            
            }
            }
                        
            if (Dump!=null&&Dump.Length>0&&Dump.Length>=offset)
            {
            FileStream fout;
            string filename = newdirname+"\\"+moduleName;
            fout = new FileStream(filename, FileMode.Create);
            fout.Write(Dump,0,offset);
            fout.Close();

            label2.ForeColor=Color.Blue;
			label2.Text = "Module saved in "+filename;
            }
            else
            {
            label2.ForeColor=Color.Red;	
			label2.Text = "Failed to dump module!";
            }

		    
		
		}
		else
		{
		label2.ForeColor=Color.Red;	
		label2.Text = "Failed to open process!";
		}
		}
		
		
		void Button3Click(object sender, EventArgs e)
		{
string strtoset = "";
int count = lvmodules.Items.Count;
for (int i=0;i<count;i++)
{
strtoset = strtoset+lvmodules.Items[i].SubItems[0].Text;
strtoset = strtoset+"\r\n";
}
if (strtoset!="") Clipboard.SetText(strtoset);

		}
		
string DirectoryName="";
	
		void Button4Click(object sender, EventArgs e)
		{
		OpenFileDialog fdlg = new OpenFileDialog();
		fdlg.Title = "Browse for target dll:";
		fdlg.InitialDirectory = @"c:\";
		if (DirectoryName!="") fdlg.InitialDirectory = DirectoryName;
		fdlg.Filter = "All files (*.dll)|*.dll";
		fdlg.FilterIndex = 2;
		fdlg.RestoreDirectory = true;
		fdlg.Multiselect = false;
		if(fdlg.ShowDialog() == DialogResult.OK)
		{
string FileName = fdlg.FileName;
int lastslash = FileName.LastIndexOf("\\");
if (lastslash!=-1) DirectoryName = FileName.Remove(lastslash,FileName.Length-lastslash);
if (DirectoryName.Length==2) DirectoryName=DirectoryName+"\\";

string error = "";
string libname = fdlg.FileName;
IntPtr retaddress = ProcModule.InjectLibraryInternal((uint)procid,libname,out error);
if (retaddress==IntPtr.Zero)
{
label2.ForeColor=Color.Red;
label2.Text = "Error: "+error;
}
else
{
label2.ForeColor=Color.Blue;
label2.Text = "Dll injected at address: "+retaddress.ToString("X8");
lvmodules.Items.Clear();
EnumModules();
}

}
		


		}
		
		void FreeModuleToolStripMenuItemClick(object sender, EventArgs e)
		{
if (lvmodules.SelectedIndices.Count>0)
{
string libaddress = lvmodules.Items[lvmodules.SelectedIndices[0]].SubItems[1].Text;
IntPtr libaddressptr = (IntPtr)System.Convert.ToInt32(libaddress, 16);
string error = "";
if (!ProcModule.FreeLibraryInternal((uint)procid,libaddressptr,out error))
{
label2.ForeColor=Color.Red;
label2.Text = "Error: "+error;
}
else
{
label2.ForeColor=Color.Blue;
label2.Text = "Dll free succesfully!";
lvmodules.Items.Clear();
EnumModules();
}

}
		}
		


		
		void FrmModulesShown(object sender, EventArgs e)
		{
		lvmodules.Items.Clear();
		EnumModules();
		}
		
		void DetectAntidumpsToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvmodules.SelectedIndices.Count>0)
		{
		string strmodulename = lvmodules.Items[lvmodules.SelectedIndices[0]].SubItems[0].Text;
		int baseaddress = int.Parse(lvmodules.Items[lvmodules.SelectedIndices[0]].SubItems[1].Text,NumberStyles.HexNumber);
		int modulesize = int.Parse(lvmodules.Items[lvmodules.SelectedIndices[0]].SubItems[2].Text,NumberStyles.HexNumber);
		MegaDumper.DetectAntidumps detectanti = new MegaDumper.DetectAntidumps(procid,strmodulename,baseaddress,modulesize);
		detectanti.Show();
		}
		}

		
		void CodeSectionChangesToolStripMenuItemClick(object sender, EventArgs e)
		{
		if (lvmodules.SelectedIndices.Count>0)
		{
		EmptyForm detectchanges = new EmptyForm(ProcessName,procid,4);
		detectchanges.modulename = modules[lvmodules.SelectedIndices[0]].fileName;
		detectchanges.baseaddress = modules[lvmodules.SelectedIndices[0]].baseOfDll;
		detectchanges.Show();
		}
		}
		
		void CopyToolStripMenuItemClick(object sender, System.EventArgs e)
		{
if (lvmodules.SelectedIndices.Count>0)
{
string strtoset = "";
for (int i=0;i<4;i++)
strtoset = strtoset+lvmodules.Items[lvmodules.SelectedIndices[0]].SubItems[i].Text+"\t";

if (strtoset!="") Clipboard.SetText(strtoset);
}
		}
		
		void CopyNameToolStripMenuItemClick(object sender, EventArgs e)
		{
string strtoset = "";
int count = lvmodules.Items.Count;
for (int i=0;i<count;i++)
strtoset = strtoset+lvmodules.Items[i].SubItems[0].Text+"\r\n";

if (strtoset!="") Clipboard.SetText(strtoset);

		}
	}
}
