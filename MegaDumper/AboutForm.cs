/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 04.03.2011
 * Time: 16:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mega_Dumper
{
	/// <summary>
	/// Description of AboutForm.
	/// </summary>
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ButtonCloseClick(object sender, EventArgs e)
		{
		this.Close();
		}
		
	    public static void TryStart(string command)
        {
            try
            {
                System.Diagnostics.Process.Start(command);
            }
            catch (Exception ex)
            {
                if (command.StartsWith("http://"))
                {
                    if (ex is System.ComponentModel.Win32Exception)
                    {
                        // Ignore file not found errors when opening web pages.
                        if ((ex as System.ComponentModel.Win32Exception).NativeErrorCode == 2)
                            return;
                    }
                }

                MessageBox.Show(ex.Message,ex.Source);
            }
        }
		void LinkSourceforgeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
TryStart("http://forum.tuts4you.com/");

		}

	}
}
