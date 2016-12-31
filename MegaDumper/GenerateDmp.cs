/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 19.04.2011
 * Time: 20:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Mega_Dumper
{
	
	/// <summary>
	/// Description of GenerateDmp.
	/// </summary>
	public partial class GenerateDmp : Form
	{
	public string ProcessName;
    public string DirName;
	public int procid;
	
		public GenerateDmp(string procname,int prid,string Dir)
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
		
		string DirectoryName ="";
		void Button1Click(object sender, EventArgs e)
		{
		SaveFileDialog fdlg = new SaveFileDialog();
		fdlg.Title = "Browse for program:";
		fdlg.InitialDirectory = @"c:\";
		if (DirectoryName!="") fdlg.InitialDirectory = DirectoryName;
		fdlg.Filter = "dmp file (*.dmp)|*.dmp";
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
		
		void GenerateDmpShown(object sender, EventArgs e)
		{
		if (DirName!=""&&Directory.Exists(DirName)&&ProcessName!="")
		textBox1.Text = Path.Combine(DirName,Path.GetFileNameWithoutExtension(ProcessName)+".dmp");
		
		if (DirName!=""&&Directory.Exists(DirName))
		DirectoryName = DirName;
					
		string[] dtypenames = MiniDmp.DType.GetNames(typeof(MiniDmp.DType));
		
		for (int i=0;i<dtypenames.Length;i++)
		{
		dmpoption.Items.Add(dtypenames[i]);
		}
		
		dmpoption.SelectedIndex = 0;
		
		}
		
		bool MsgOverwrite(string dumpfilename)
		{
		if (File.Exists(dumpfilename))
		{

DialogResult result = MessageBox.Show("A file whit same name already exist!\r\n" +
"Do you want to ovewrite the file ?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
if (result == DialogResult.Yes)
{
return true;
}
else if (result == DialogResult.No)
{
return false;
}

		}
		return true;
		
		}
		
		void Button2Click(object sender, EventArgs e)
		{
		string dumpfilename = textBox1.Text;
		
		if (dumpfilename=="")
		{
		label3.Text = "Please select a name for dump first!";
		}
		else
		{
		if (!MsgOverwrite(dumpfilename))
		{
		label3.Text = "Aborted by user!";
		}
		else
		{
		string selectedDType = dmpoption.SelectedItem.ToString();
		MiniDmp.DType selDType = (MiniDmp.DType)Enum.Parse(typeof(MiniDmp.DType), selectedDType);
		
		    try
            {
            string a = null;
            a.PadLeft(10);
            }
            catch
            {
            bool opok = MiniDmp.WriteDump((uint)procid,dumpfilename,selDType);
            if (opok)
			label3.Text = "Dump file saved on "+dumpfilename+"!";
            else
			label3.Text = "Error while generating the dump file!";
            
            }
            
		}
		}
			
		}
	}
}
