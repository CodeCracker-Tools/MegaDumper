/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 27.10.2010
 * Time: 18:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;

namespace Mega_Dumper
{
	
	internal enum COR_PUB_ENUMPROCESS
    {
        /// Indicates that we need to get managed processes only
        COR_PUB_MANAGEDONLY = 0x00000001
    }
    
    
    /// This is ICorePublish default interface implementation
    [GuidAttribute("047a9a40-657e-11d3-8d5b-00104b35e7ef")]
    [ClassInterfaceAttribute(ClassInterfaceType.None)]
    [ComImportAttribute()]
    internal class CorpubPublish { }
    
    
     /// CLR core interface for working with managed processes
    [ComImport()]
    [Guid("9613A0E7-5A68-11D3-8F84-00A0C9B4D50C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICorPublish
    {
        /// Gets a set of managed processes
        void EnumProcesses([In]COR_PUB_ENUMPROCESS Type, [Out] out ICorPublishProcessEnum ppIenum);

        /// Gets a managed process by ID
        void GetProcess([In] uint pid, [Out] out ICorPublishProcess ppProcess);
    }
    
    
    [ComImport()]
    [Guid("D6315C8F-5A6A-11d3-8F84-00A0C9B4D50C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICorPublishAppDomain
    {
        /// Gets domain ID
        void GetID([Out] out uint puId);
        
        /// Gets domain name
        void GetName([In, MarshalAs(UnmanagedType.U4)] uint cchName, [Out, MarshalAs(UnmanagedType.U4)] out uint pcchName, [Out, MarshalAs(UnmanagedType.LPWStr)]  StringBuilder szName);
    }
    
    [ComImport()]
    [Guid("9F0C98F5-5A6A-11d3-8F84-00A0C9B4D50C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICorPublishAppDomainEnum
    {
        /// Skips a set of domains
        void Skip([In] uint celt);

        /// Resets the collection
        void Reset();

        /// Creates a deep copy of the collection
        void Clone([Out] out ICorPublishEnum ppEnum);

        /// Gets the collection size
        void GetCount([Out]out uint pcelt);

        /// Gets next set of managed domains
        int Next([In]  uint celt,[MarshalAs(UnmanagedType.Interface)] [Out] out ICorPublishAppDomain objects, [Out] out uint pceltFetched);
    }
    
    
    [ComImport()]
    [Guid("C0B22967-5A69-11D3-8F84-00A0C9B4D50C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICorPublishEnum
    {
        void Skip([In] uint celt);
        void Reset();
        void Clone([Out] out ICorPublishEnum ppEnum);
        void GetCount([Out]out uint pcelt);
    }

    
    [ComImport()]
    [Guid("18D87AF1-5A6A-11d3-8F84-00A0C9B4D50C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICorPublishProcess
    {
        /// Gets if the process is managed
        void IsManaged([Out, MarshalAs(UnmanagedType.Bool)] out bool pbManaged);

        /// Gets loaded domains set for process
        void EnumAppDomains([Out] out ICorPublishAppDomainEnum ppEnum);
        
        /// Gets process ID
        void GetProcessID([Out] out uint pid);

        /// Gets process name
        void GetDisplayName([In] uint cchName, [Out] out uint pcchName, [Out]  StringBuilder szName);
    }
    
    [ComImport()]
    [Guid("A37FBD41-5A69-11d3-8F84-00A0C9B4D50C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICorPublishProcessEnum
    {
        /// Skips a set of processes
        void Skip([In] uint celt);

        /// Resets the collection
        void Reset();

        /// Creates a deep copy of the collection
        void Clone([Out] out ICorPublishEnum ppEnum);

        /// Gets the collection size
        void GetCount([Out]out uint pcelt);

        /// Gets the set of managed processes
        int Next([In]  uint celt, [Out] out ICorPublishProcess objects, [Out] out uint pceltFetched);
    }
    
    public class ManagedProcessInfoCollection : IEnumerator, ICollection, IEnumerable, ICloneable
{
    // Fields
    private object _SyncRoot = new object();
    private ICorPublishProcess CurrentProcess;
    private ICorPublishProcessEnum Processes;

    // Methods
    internal ManagedProcessInfoCollection(ICorPublishProcessEnum AllProcesses)
    {
        this.Processes = AllProcesses;
        this.CurrentProcess = null;
    }

    public object Clone()
    {
        ICorPublishEnum ppEnum = null;
        ((ICorPublishEnum) this.Processes).Clone(out ppEnum);
        if (ppEnum != null)
        {
            return new ManagedProcessInfoCollection((ICorPublishProcessEnum) ppEnum);
        }
        return null;
    }

    public void CopyTo(Array Destination, int index)
    {
        if (Destination == null)
        {
            throw new ArgumentNullException("Destination array cannot be null.");
        }
        if (Destination.Rank != 1)
        {
            throw new ArgumentException("Invalid array rank!");
        }
        int num = index;
        ManagedProcessInfo[] infoArray = (ManagedProcessInfo[]) Destination;
        foreach (ManagedProcessInfo info in this)
        {
            infoArray[num++] = info;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        uint pceltFetched = 0;
        this.Processes.Next(1, out this.CurrentProcess, out pceltFetched);
        if (pceltFetched != 1)
        {
            this.CurrentProcess = null;
        }
        return ((this.CurrentProcess != null) && (pceltFetched == 1));
    }

    public void Reset()
    {
        this.Processes.Reset();
        this.CurrentProcess = null;
    }

    // Properties
    public int Count
    {
        get
        {
            uint pcelt = 0;
            this.Processes.GetCount(out pcelt);
            return (int) pcelt;
        }
    }

    public object Current
    {
        get
        {
            if (this.CurrentProcess != null)
            {
                return new ManagedProcessInfo(this.CurrentProcess);
            }
            return null;
        }
    }

    public bool IsSynchronized
    {
        get
        {
            return true;
        }
    }

    public object SyncRoot
    {
        get
        {
            return this._SyncRoot;
        }
    }
}

    public class ManagedDomainInfo
{
    // Fields
    private ICorPublishAppDomain Domain;

    // Methods
    internal ManagedDomainInfo(ICorPublishAppDomain SingleDomain)
    {
        this.Domain = SingleDomain;
    }

    // Properties
    public uint DomainID
    {
        get
        {
            uint puId = 0;
            this.Domain.GetID(out puId);
            return puId;
        }
    }

    public string DomainName
    {
        get
        {
            uint pcchName = 0;
            StringBuilder szName = new StringBuilder(0xff);
            this.Domain.GetName(0xff, out pcchName, szName);
            if ((pcchName > 0) && (pcchName > 0xff))
            {
                szName = new StringBuilder((int) pcchName);
                this.Domain.GetName(pcchName, out pcchName, szName);
            }
            return szName.ToString();
        }
    }
}

 

    
 public class ManagedDomainInfoCollection : IEnumerator, ICollection, IEnumerable, ICloneable
{
    // Fields
    private object _SyncRoot = new object();
    private ICorPublishAppDomain CurrentDomain;
    private ICorPublishAppDomainEnum Domains;

    // Methods
    internal ManagedDomainInfoCollection(ICorPublishAppDomainEnum AllDomains)
    {
        this.Domains = AllDomains;
        this.CurrentDomain = null;
    }

    public object Clone()
    {
        ICorPublishEnum ppEnum = null;
        ((ICorPublishEnum) this.Domains).Clone(out ppEnum);
        if (ppEnum != null)
        {
            return new ManagedDomainInfoCollection((ICorPublishAppDomainEnum) ppEnum);
        }
        return null;
    }

    public void CopyTo(Array Destination, int index)
    {
        if (Destination == null)
        {
            throw new ArgumentNullException("Destination array cannot be null.");
        }
        if (Destination.Rank != 1)
        {
            throw new ArgumentException("Invalid array rank!");
        }
        int num = index;
        ManagedDomainInfo[] infoArray = (ManagedDomainInfo[]) Destination;
        foreach (ManagedDomainInfo info in this)
        {
            infoArray[num++] = info;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        uint pceltFetched = 0;
        this.Domains.Next(1, out this.CurrentDomain, out pceltFetched);
        if (pceltFetched != 1)
        {
            this.CurrentDomain = null;
        }
        return ((this.CurrentDomain != null) && (pceltFetched == 1));
    }

    public void Reset()
    {
        this.Domains.Reset();
        this.CurrentDomain = null;
    }

    // Properties
    public int Count
    {
        get
        {
            uint pcelt = 0;
            this.Domains.GetCount(out pcelt);
            return (int) pcelt;
        }
    }

    public object Current
    {
        get
        {
            if (this.CurrentDomain != null)
            {
                return new ManagedDomainInfo(this.CurrentDomain);
            }
            return null;
        }
    }

    public bool IsSynchronized
    {
        get
        {
            return true;
        }
    }

    public object SyncRoot
    {
        get
        {
            return this._SyncRoot;
        }
    }
}

 


    public class ManagedProcessInfo
{
    // Fields
    private ICorPublishProcess Process;

    // Methods
    internal ManagedProcessInfo(ICorPublishProcess SingleProcess)
    {
        this.Process = SingleProcess;
    }

    public System.Diagnostics.Process ConvertToDiagnosticsProcess()
    {
        return System.Diagnostics.Process.GetProcessById((int) this.ProcessID);
    }

    public static ManagedProcessInfo GetProcessByID(uint ID)
    {
        ICorPublish publish = (ICorPublish) new CorpubPublish();
        if (publish != null)
        {
            ICorPublishProcess ppProcess = null;
            publish.GetProcess(ID, out ppProcess);
            if (ppProcess != null)
            {
                return new ManagedProcessInfo(ppProcess);
            }
        }
        return null;
    }

    public class ManagedProcessInfoCollection : IEnumerator, ICollection, IEnumerable, ICloneable
{
    // Fields
    private object _SyncRoot = new object();
    private ICorPublishProcess CurrentProcess;
    private ICorPublishProcessEnum Processes;

    // Methods
    internal ManagedProcessInfoCollection(ICorPublishProcessEnum AllProcesses)
    {
        this.Processes = AllProcesses;
        this.CurrentProcess = null;
    }

    public object Clone()
    {
        ICorPublishEnum ppEnum = null;
        ((ICorPublishEnum) this.Processes).Clone(out ppEnum);
        if (ppEnum != null)
        {
            return new ManagedProcessInfoCollection((ICorPublishProcessEnum) ppEnum);
        }
        return null;
    }

    public void CopyTo(Array Destination, int index)
    {
        if (Destination == null)
        {
            throw new ArgumentNullException("Destination array cannot be null.");
        }
        if (Destination.Rank != 1)
        {
            throw new ArgumentException("Invalid array rank!");
        }
        int num = index;
        ManagedProcessInfo[] infoArray = (ManagedProcessInfo[]) Destination;
        foreach (ManagedProcessInfo info in this)
        {
            infoArray[num++] = info;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        uint pceltFetched = 0;
        this.Processes.Next(1, out this.CurrentProcess, out pceltFetched);
        if (pceltFetched != 1)
        {
            this.CurrentProcess = null;
        }
        return ((this.CurrentProcess != null) && (pceltFetched == 1));
    }

    public void Reset()
    {
        this.Processes.Reset();
        this.CurrentProcess = null;
    }

    // Properties
    public int Count
    {
        get
        {
            uint pcelt = 0;
            this.Processes.GetCount(out pcelt);
            return (int) pcelt;
        }
    }

    public object Current
    {
        get
        {
            if (this.CurrentProcess != null)
            {
                return new ManagedProcessInfo(this.CurrentProcess);
            }
            return null;
        }
    }

    public bool IsSynchronized
    {
        get
        {
            return true;
        }
    }

    public object SyncRoot
    {
        get
        {
            return this._SyncRoot;
        }
    }
}


    public static ManagedProcessInfoCollection GetProcesses()
    {
        ICorPublish publish = (ICorPublish) new CorpubPublish();
        if (publish != null)
        {
            ICorPublishProcessEnum ppIenum = null;
            publish.EnumProcesses(COR_PUB_ENUMPROCESS.COR_PUB_MANAGEDONLY, out ppIenum);
            if (ppIenum != null)
            {
                return new ManagedProcessInfoCollection(ppIenum);
            }
        }
        return null;
    }

    // Properties
    public string DisplayName
    {
        get
        {
            uint pcchName = 0;
            StringBuilder szName = new StringBuilder(0xff);
            this.Process.GetDisplayName(0xff, out pcchName, szName);
            if ((pcchName > 0) && (pcchName > 0xff))
            {
                szName = new StringBuilder((int) pcchName);
                this.Process.GetDisplayName(pcchName, out pcchName, szName);
            }
            return szName.ToString();
        }
    }

    public ManagedDomainInfoCollection LoadedDomains
    {
        get
        {
            ICorPublishAppDomainEnum ppEnum = null;
            this.Process.EnumAppDomains(out ppEnum);
            if (ppEnum != null)
            {
                return new ManagedDomainInfoCollection(ppEnum);
            }
            return null;
        }
    }

    public uint ProcessID
    {
        get
        {
            uint pid = 0;
            this.Process.GetProcessID(out pid);
            return pid;
        }
    }
    }
    
	/// <summary>
	/// Description of Form2.
	/// </summary>
	public partial class NetPerformance : Form
	{
	public string ProcessName;
	public int procid;
		public NetPerformance(string procname,int prid)
		{
		ProcessName=procname;
		procid=prid;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Form2Load(object sender, EventArgs e)
		{
		this.Text=".NET Performance for "+ProcessName+" whit PID="+procid.ToString();
		comboBox1.Items.Add(".NET CLR Memory");
		comboBox1.Items.Add(".NET CLR Jit");
		comboBox1.Items.Add(".NET CLR Exceptions");
		comboBox1.Items.Add(".NET CLR LocksAndThreads");
		comboBox1.Items.Add(".NET CLR Data");
		comboBox1.Items.Add(".NET CLR Interop");
		comboBox1.Items.Add(".NET CLR Loading");
		comboBox1.Items.Add(".NET CLR Remoting");
		comboBox1.Items.Add(".NET CLR Security");
		comboBox1.SelectedIndex = 0;
	  
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
		perfobject.Items.Clear();
		string[] instanceNames;
        System.Collections.ArrayList counters = new System.Collections.ArrayList();
         if (comboBox1.SelectedIndex != -1)
         {
         bool IsFinded=false;
         System.Diagnostics.PerformanceCounterCategory mycat = new System.Diagnostics.PerformanceCounterCategory(comboBox1.SelectedItem.ToString());
         instanceNames = mycat.GetInstanceNames();
         int proccount=0;
         for (int i = 0; i < instanceNames.Length; i++)
         {
string fortest = instanceNames[i].ToLower();
int lastdiez = fortest.LastIndexOf("#");
if (lastdiez!=-1)
{
fortest = fortest.Remove(lastdiez,fortest.Length-lastdiez);
}
if (ProcessName.ToLower().Contains(fortest))
         {
         proccount++;
         if (proccount>=2) break;
         }
         }

         for (int i = 0; i < instanceNames.Length; i++)
         {
         IsFinded=false;
         System.Diagnostics.PerformanceCounterCategory mycattest = new System.Diagnostics.PerformanceCounterCategory(".NET CLR Memory");
         System.Collections.ArrayList testcounters = new System.Collections.ArrayList();
         testcounters.AddRange(mycattest.GetCounters(instanceNames[i]));
         
         foreach (System.Diagnostics.PerformanceCounter tcounter in testcounters)
         {
         if (tcounter.CounterName=="Process ID")
         {
         if ((int)tcounter.RawValue==procid)
         IsFinded=true;
         else
         IsFinded=false;
         }
         }
         

         if (!IsFinded||proccount>=2)
         {
string fortest = instanceNames[i];
int lastdiez = fortest.LastIndexOf("#");
if (lastdiez!=-1)
{
fortest = fortest.Remove(lastdiez,fortest.Length-lastdiez);
}
if (ProcessName.ToLower().Contains(fortest.ToLower()))
         {
IsFinded=true;
string[] prcdet = new string[]{""};
ListViewItem proctadd = new ListViewItem(prcdet);
perfobject.Items.Add(proctadd);
prcdet = new string[]{instanceNames[i]};
proctadd = new ListViewItem(prcdet);
perfobject.Items.Add(proctadd);
         }
         }
         
         if (IsFinded)
         {
         counters.AddRange(mycat.GetCounters(instanceNames[i]));
         // Add the retrieved counters to the list.
         foreach (System.Diagnostics.PerformanceCounter counter in counters)
         {
         string[] prcdetails = new string[]{counter.CounterName,counter.RawValue.ToString()};
         ListViewItem proc = new ListViewItem(prcdetails);
         perfobject.Items.Add(proc);


         }
         }
         if (proccount<2&&IsFinded) break;
         }
         
         }
        
		}
	}
}
