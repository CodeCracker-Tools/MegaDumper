/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 11.10.2010
 * Time: 15:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Data;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.IO.IsolatedStorage;
using System.Globalization;

namespace Mega_Dumper
{
	
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
        private static void Main(string[] args)
		{
		Application.EnableVisualStyles();
		Application.Run(new MainForm());
			
		}
		
	}
}
