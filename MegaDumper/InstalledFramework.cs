/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 14.04.2011
 * Time: 18:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text;
using System.IO;

namespace Mega_Dumper
{
	/// <summary>
	/// Description of InstalledFramework.
	/// </summary>
	public partial class InstalledFramework : Form
	{
		public InstalledFramework()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
			
		public void GetInstalledFrameworks()
		{
			textBox1.Text = "Installed .Net Frameworks:"+"\r\n";
		    RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            string[] version_names = installed_versions.GetSubKeyNames();
            for (int i = 0; i < version_names.Length; i++)
            {
                RegistryKey spsubk = installed_versions.OpenSubKey(version_names[i]);
                if (spsubk.SubKeyCount>0&&version_names[i]!="CDF")
                {
                object spsub = spsubk.GetValue("SP", 0);
                int SP = Convert.ToInt32(spsub);
                object versionsub = spsubk.GetValue("Version", 0);
                string version = versionsub.ToString();
                version = version.Replace("v","");
                if (version_names[i]=="v4")
                {
                textBox1.Text += version_names[i];
                string[] fr4versions = spsubk.GetSubKeyNames();
                for (int j = 0; j < fr4versions.Length; j++)
            	{
                RegistryKey fr4 = spsubk.OpenSubKey(fr4versions[j]);
                object f4versionsub = fr4.GetValue("Version", 0);
                textBox1.Text += "      -"+fr4versions[j]+" version "+f4versionsub.ToString();
                if (j+1<fr4versions.Length) textBox1.Text += "\r\n";
                if (j==0) textBox1.Text += "    ";
                fr4.Close();
                }
                }
                else
                {
                textBox1.Text += version_names[i] + (SP > 0 ? " SP " + SP.ToString() : "");
                if (version!="0")
                textBox1.Text += "   version "+version;
                }
                textBox1.Text += "\r\n";
                }

                spsubk.Close();
                
            }
            
            installed_versions.Close();
            
            RegistryKey framework = Registry.LocalMachine.OpenSubKey(@"Software\\Microsoft\\.NetFramework");
            string frameworkdir = (string)framework.GetValue("InstallRoot", 0);
            framework.Close();
            textBox1.Text += "\r\n";
            textBox1.Text += ".NET Framework InstallRoot: "+frameworkdir+"\r\n";
            textBox1.Text += ".NET Framework directories:"+"\r\n";


         
            string [] dirs = Directory.GetDirectories(frameworkdir, "*.*");
            for (int i=0;i<dirs.Length;i++)
            {
            if (Directory.GetDirectories(dirs[i]).Length>0)
            textBox1.Text += dirs[i]+"\r\n";
            }
        
		}
		void InstalledFrameworkShown(object sender, EventArgs e)
		{
		GetInstalledFrameworks();
		}
		
		void Button1Click(object sender, EventArgs e)
		{
		GetInstalledFrameworks();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
		if (textBox1.Text!="") Clipboard.SetText(textBox1.Text);
		}
		
		void Button3Click(object sender, EventArgs e)
		{
		this.Close();
		}
	}
}
