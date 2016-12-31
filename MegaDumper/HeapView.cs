/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 03.03.2011
 * Time: 21:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

namespace Mega_Dumper
{
	/// <summary>
	/// Description of HeapView.
	/// </summary>
	public partial class HeapView : Form
	{
		
		string processname;
		int processid;
			
		public HeapView(string pname,int prid)
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
		
IntPtr INVALID_HANDLE_VALUE = (IntPtr)(-1);


		
		void HeapViewShown(object sender, EventArgs e)
		{

		IntPtr snapshot = HeapHealper.CreateToolhelp32Snapshot(HeapHealper.TH32CS_SNAPHEAPLIST, (uint)processid);
		if (snapshot != INVALID_HANDLE_VALUE)
		{

		HeapHealper.HEAPLIST32  hlist = new HeapHealper.HEAPLIST32();
		HeapHealper.HEAPENTRY32 heap  = new HeapHealper.HEAPENTRY32();
		
		hlist.dwSize = (uint)Marshal.SizeOf(hlist);
		heap.dwSize = (uint)Marshal.SizeOf(heap);
		
		
		    HeapHealper.Heap32ListFirst(snapshot, ref hlist);

			do
			{
			HeapHealper.Heap32First(ref heap, hlist.th32ProcessID, hlist.th32HeapID);
				do
				{
string flags = "";
if (heap.dwFlags==0x00000001)
flags = "LF32_FIXED";

if (heap.dwFlags==0x00000002)
flags = "LF32_FREE";

if (heap.dwFlags==0x00000004)
flags = "LF32_MOVEABLE";

ListViewItem heaptoadd = new ListViewItem(new string[]{heap.dwAddress.ToString("X8"),heap.dwBlockSize.ToString("X8"),flags});
lvheaps.Items.Add(heaptoadd);

				} while (HeapHealper.Heap32Next(ref heap));
                
            } while (HeapHealper.Heap32ListNext(snapshot, ref hlist));
			

		 	HeapHealper.CloseHandle(snapshot);
			}
			
		}
	}
}
