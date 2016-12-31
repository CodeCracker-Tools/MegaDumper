/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 02.03.2011
 * Time: 19:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;


namespace Mega_Dumper
{
	/// <summary>
	/// Description of EnumAssemblies.
	/// </summary>
	public partial class EnumAppDomains : Form
	{
	int procid = 0;
		public EnumAppDomains(int processid)
		{
		procid = processid;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
	

		
		void EnumAppDomainsShown(object sender, EventArgs e)
		{
		ICorPublish publish = (ICorPublish) new CorpubPublish();

		if (publish != null)
		{
        ICorPublishProcess ppProcess = null;
        try
        {
        publish.GetProcess((uint)procid, out ppProcess);
        }
        catch
        {
        }
        
        if (ppProcess != null)
        {
        bool IsManaged = false;
        ppProcess.IsManaged(out IsManaged);
        if (IsManaged)
        {
// Enumerate the domains within the process.
        ICorPublishAppDomainEnum ppEnum = null;
        ppProcess.EnumAppDomains(out ppEnum);
        
        ICorPublishAppDomain pappDomain;  // ICorPublishAppDomain
        uint aFetched = 0;
        while (ppEnum.Next(1,out pappDomain,out aFetched)==0 && aFetched > 0)
        {
	
    StringBuilder szName= null;
    try
    {
    uint pcchName = 0;
    pappDomain.GetName(0, out pcchName, null);
    szName = new StringBuilder((int) pcchName);
    pappDomain.GetName((uint)szName.Capacity, out pcchName, szName);
    }
    catch
    {
    }
    
    string appdomainname = szName.ToString();
    uint appdomainid = 0;
    pappDomain.GetID(out appdomainid);
    
ListViewItem appdomaintoadd = new ListViewItem(new string[]{appdomainid.ToString(),appdomainname});
lvdomains.Items.Add(appdomaintoadd);

        }
        }
        else
        {
MessageBox.Show("Selected process is not a managed .NET process!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }
        }
        else
        {
MessageBox.Show("Failed to open slected process \r\n" +
        	                "maybe is not a .NET process!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }
		}
		}
	}
}
