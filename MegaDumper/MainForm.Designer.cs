/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 11.10.2010
 * Time: 15:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Mega_Dumper
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.lvprocesslist = new System.Windows.Forms.ListView();
			this.procname = new System.Windows.Forms.ColumnHeader();
			this.PID = new System.Windows.Forms.ColumnHeader();
			this.status = new System.Windows.Forms.ColumnHeader();
			this.isnet = new System.Windows.Forms.ColumnHeader();
			this.location = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.dumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dumpModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gotoLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.advancedInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.virtualMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewHeapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enumAppdomainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nETPerformanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hookDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.environmentVariablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileDirectoriesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateDmpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.injectManagedDllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.bringToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.maximizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.priorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.anToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.nToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.suspendProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resumeProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.killProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.mainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.processManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.windowsHoocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.installedFrameworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dumpingOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dumpNativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.autoRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dontRestoreFilenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvprocesslist
			// 
			this.lvprocesslist.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.lvprocesslist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvprocesslist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.procname,
									this.PID,
									this.status,
									this.isnet,
									this.location});
			this.lvprocesslist.ContextMenuStrip = this.contextMenuStrip1;
			this.lvprocesslist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lvprocesslist.FullRowSelect = true;
			this.lvprocesslist.Location = new System.Drawing.Point(0, 24);
			this.lvprocesslist.MultiSelect = false;
			this.lvprocesslist.Name = "lvprocesslist";
			this.lvprocesslist.Size = new System.Drawing.Size(790, 417);
			this.lvprocesslist.TabIndex = 8;
			this.lvprocesslist.UseCompatibleStateImageBehavior = false;
			this.lvprocesslist.View = System.Windows.Forms.View.Details;
			// 
			// procname
			// 
			this.procname.Text = "Process Name";
			this.procname.Width = 87;
			// 
			// PID
			// 
			this.PID.Text = "PID";
			this.PID.Width = 41;
			// 
			// status
			// 
			this.status.Text = "Status";
			// 
			// isnet
			// 
			this.isnet.Text = ".NET";
			// 
			// location
			// 
			this.location.Text = "Location";
			this.location.Width = 104;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.dumpToolStripMenuItem,
									this.dumpModuleToolStripMenuItem,
									this.gotoLocationToolStripMenuItem,
									this.copyToolStripMenuItem,
									this.advancedInfoToolStripMenuItem,
									this.injectManagedDllToolStripMenuItem,
									this.toolStripSeparator1,
									this.toolStripMenuItem1,
									this.priorityToolStripMenuItem,
									this.suspendProcessToolStripMenuItem,
									this.resumeProcessToolStripMenuItem,
									this.killProcessToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(208, 274);
			// 
			// dumpToolStripMenuItem
			// 
			this.dumpToolStripMenuItem.Name = "dumpToolStripMenuItem";
			this.dumpToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.dumpToolStripMenuItem.Text = ".NET dump";
			this.dumpToolStripMenuItem.Click += new System.EventHandler(this.DumpToolStripMenuItemClick);
			// 
			// dumpModuleToolStripMenuItem
			// 
			this.dumpModuleToolStripMenuItem.Name = "dumpModuleToolStripMenuItem";
			this.dumpModuleToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.dumpModuleToolStripMenuItem.Text = "Modules";
			this.dumpModuleToolStripMenuItem.Click += new System.EventHandler(this.DumpModuleToolStripMenuItemClick);
			// 
			// gotoLocationToolStripMenuItem
			// 
			this.gotoLocationToolStripMenuItem.Name = "gotoLocationToolStripMenuItem";
			this.gotoLocationToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.gotoLocationToolStripMenuItem.Text = "Goto Location";
			this.gotoLocationToolStripMenuItem.Click += new System.EventHandler(this.GotoLocationToolStripMenuItemClick);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.copyToolStripMenuItem.Text = "Copy Location";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
			// 
			// advancedInfoToolStripMenuItem
			// 
			this.advancedInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.virtualMemoryToolStripMenuItem,
									this.viewHeapToolStripMenuItem,
									this.enumAppdomainsToolStripMenuItem,
									this.nETPerformanceToolStripMenuItem,
									this.hookDetectionToolStripMenuItem,
									this.environmentVariablesToolStripMenuItem,
									this.fileDirectoriesListToolStripMenuItem,
									this.generateDmpToolStripMenuItem});
			this.advancedInfoToolStripMenuItem.Name = "advancedInfoToolStripMenuItem";
			this.advancedInfoToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.advancedInfoToolStripMenuItem.Text = "Advanced Info";
			// 
			// virtualMemoryToolStripMenuItem
			// 
			this.virtualMemoryToolStripMenuItem.Name = "virtualMemoryToolStripMenuItem";
			this.virtualMemoryToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.virtualMemoryToolStripMenuItem.Text = "Virtual Memory";
			this.virtualMemoryToolStripMenuItem.Click += new System.EventHandler(this.VirtualMemoryToolStripMenuItemClick);
			// 
			// viewHeapToolStripMenuItem
			// 
			this.viewHeapToolStripMenuItem.Name = "viewHeapToolStripMenuItem";
			this.viewHeapToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.viewHeapToolStripMenuItem.Text = "View Heap";
			this.viewHeapToolStripMenuItem.Click += new System.EventHandler(this.ViewHeapToolStripMenuItemClick);
			// 
			// enumAppdomainsToolStripMenuItem
			// 
			this.enumAppdomainsToolStripMenuItem.Name = "enumAppdomainsToolStripMenuItem";
			this.enumAppdomainsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.enumAppdomainsToolStripMenuItem.Text = "Enum Appdomains";
			this.enumAppdomainsToolStripMenuItem.Click += new System.EventHandler(this.EnumAppdomainsToolStripMenuItemClick);
			// 
			// nETPerformanceToolStripMenuItem
			// 
			this.nETPerformanceToolStripMenuItem.Name = "nETPerformanceToolStripMenuItem";
			this.nETPerformanceToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.nETPerformanceToolStripMenuItem.Text = ".NET Performance";
			this.nETPerformanceToolStripMenuItem.Click += new System.EventHandler(this.NETPerformanceToolStripMenuItemClick);
			// 
			// hookDetectionToolStripMenuItem
			// 
			this.hookDetectionToolStripMenuItem.Name = "hookDetectionToolStripMenuItem";
			this.hookDetectionToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.hookDetectionToolStripMenuItem.Text = "JIT hook detection";
			this.hookDetectionToolStripMenuItem.Click += new System.EventHandler(this.HookDetectionToolStripMenuItemClick);
			// 
			// environmentVariablesToolStripMenuItem
			// 
			this.environmentVariablesToolStripMenuItem.Name = "environmentVariablesToolStripMenuItem";
			this.environmentVariablesToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.environmentVariablesToolStripMenuItem.Text = "Environment Variables";
			this.environmentVariablesToolStripMenuItem.Click += new System.EventHandler(this.EnvironmentVariablesToolStripMenuItemClick);
			// 
			// fileDirectoriesListToolStripMenuItem
			// 
			this.fileDirectoriesListToolStripMenuItem.Name = "fileDirectoriesListToolStripMenuItem";
			this.fileDirectoriesListToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.fileDirectoriesListToolStripMenuItem.Text = "Files/Directories list";
			this.fileDirectoriesListToolStripMenuItem.Click += new System.EventHandler(this.FileDirectoriesListToolStripMenuItemClick);
			// 
			// generateDmpToolStripMenuItem
			// 
			this.generateDmpToolStripMenuItem.Name = "generateDmpToolStripMenuItem";
			this.generateDmpToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.generateDmpToolStripMenuItem.Text = "Generate dmp";
			this.generateDmpToolStripMenuItem.Click += new System.EventHandler(this.GenerateDmpToolStripMenuItemClick);
			// 
			// injectManagedDllToolStripMenuItem
			// 
			this.injectManagedDllToolStripMenuItem.Name = "injectManagedDllToolStripMenuItem";
			this.injectManagedDllToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.injectManagedDllToolStripMenuItem.Text = "Inject Managed assembly";
			this.injectManagedDllToolStripMenuItem.Click += new System.EventHandler(this.InjectManagedDllToolStripMenuItemClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.bringToFrontToolStripMenuItem,
									this.restoreToolStripMenuItem,
									this.minimizeToolStripMenuItem,
									this.maximizeToolStripMenuItem,
									this.closeToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
			this.toolStripMenuItem1.Text = "Main Window";
			// 
			// bringToFrontToolStripMenuItem
			// 
			this.bringToFrontToolStripMenuItem.Name = "bringToFrontToolStripMenuItem";
			this.bringToFrontToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.bringToFrontToolStripMenuItem.Text = "Bring to Front";
			this.bringToFrontToolStripMenuItem.Click += new System.EventHandler(this.BringToFrontToolStripMenuItemClick);
			// 
			// restoreToolStripMenuItem
			// 
			this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
			this.restoreToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.restoreToolStripMenuItem.Text = "Restore";
			this.restoreToolStripMenuItem.Click += new System.EventHandler(this.RestoreToolStripMenuItemClick);
			// 
			// minimizeToolStripMenuItem
			// 
			this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
			this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.minimizeToolStripMenuItem.Text = "Minimize";
			this.minimizeToolStripMenuItem.Click += new System.EventHandler(this.MinimizeToolStripMenuItemClick);
			// 
			// maximizeToolStripMenuItem
			// 
			this.maximizeToolStripMenuItem.Name = "maximizeToolStripMenuItem";
			this.maximizeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.maximizeToolStripMenuItem.Text = "Maximize";
			this.maximizeToolStripMenuItem.Click += new System.EventHandler(this.MaximizeToolStripMenuItemClick);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
			// 
			// priorityToolStripMenuItem
			// 
			this.priorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.rttoolStripMenuItem,
									this.hToolStripMenuItem,
									this.anToolStripMenuItem,
									this.nToolStripMenuItem,
									this.bnToolStripMenuItem,
									this.iToolStripMenuItem});
			this.priorityToolStripMenuItem.Name = "priorityToolStripMenuItem";
			this.priorityToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.priorityToolStripMenuItem.Text = "Priority";
			this.priorityToolStripMenuItem.Click += new System.EventHandler(this.PriorityToolStripMenuItemClick);
			// 
			// rttoolStripMenuItem
			// 
			this.rttoolStripMenuItem.Name = "rttoolStripMenuItem";
			this.rttoolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.rttoolStripMenuItem.Text = "Real Time";
			this.rttoolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem3Click);
			// 
			// hToolStripMenuItem
			// 
			this.hToolStripMenuItem.Name = "hToolStripMenuItem";
			this.hToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.hToolStripMenuItem.Text = "High";
			this.hToolStripMenuItem.Click += new System.EventHandler(this.HToolStripMenuItemClick);
			// 
			// anToolStripMenuItem
			// 
			this.anToolStripMenuItem.Name = "anToolStripMenuItem";
			this.anToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.anToolStripMenuItem.Text = "Above Normal";
			this.anToolStripMenuItem.Click += new System.EventHandler(this.AnToolStripMenuItemClick);
			// 
			// nToolStripMenuItem
			// 
			this.nToolStripMenuItem.Name = "nToolStripMenuItem";
			this.nToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.nToolStripMenuItem.Text = "Normal";
			this.nToolStripMenuItem.Click += new System.EventHandler(this.NToolStripMenuItemClick);
			// 
			// bnToolStripMenuItem
			// 
			this.bnToolStripMenuItem.Name = "bnToolStripMenuItem";
			this.bnToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.bnToolStripMenuItem.Text = "Below Normal";
			this.bnToolStripMenuItem.Click += new System.EventHandler(this.BnToolStripMenuItemClick);
			// 
			// iToolStripMenuItem
			// 
			this.iToolStripMenuItem.Name = "iToolStripMenuItem";
			this.iToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.iToolStripMenuItem.Text = "Idle";
			this.iToolStripMenuItem.Click += new System.EventHandler(this.IToolStripMenuItemClick);
			// 
			// suspendProcessToolStripMenuItem
			// 
			this.suspendProcessToolStripMenuItem.Name = "suspendProcessToolStripMenuItem";
			this.suspendProcessToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.suspendProcessToolStripMenuItem.Text = "Suspend process";
			this.suspendProcessToolStripMenuItem.Click += new System.EventHandler(this.SuspendProcessToolStripMenuItemClick);
			// 
			// resumeProcessToolStripMenuItem
			// 
			this.resumeProcessToolStripMenuItem.Name = "resumeProcessToolStripMenuItem";
			this.resumeProcessToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.resumeProcessToolStripMenuItem.Text = "Resume process";
			this.resumeProcessToolStripMenuItem.Click += new System.EventHandler(this.ResumeProcessToolStripMenuItemClick);
			// 
			// killProcessToolStripMenuItem
			// 
			this.killProcessToolStripMenuItem.Name = "killProcessToolStripMenuItem";
			this.killProcessToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.killProcessToolStripMenuItem.Text = "Kill process";
			this.killProcessToolStripMenuItem.Click += new System.EventHandler(this.KillProcessToolStripMenuItemClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.mainToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(790, 24);
			this.menuStrip1.TabIndex = 14;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// mainToolStripMenuItem
			// 
			this.mainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.testToolStripMenuItem,
									this.processManagerToolStripMenuItem,
									this.windowsHoocksToolStripMenuItem,
									this.installedFrameworkToolStripMenuItem,
									this.dumpingOptionToolStripMenuItem,
									this.exitToolStripMenuItem});
			this.mainToolStripMenuItem.Name = "mainToolStripMenuItem";
			this.mainToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.mainToolStripMenuItem.Text = "Main";
			// 
			// testToolStripMenuItem
			// 
			this.testToolStripMenuItem.Name = "testToolStripMenuItem";
			this.testToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.testToolStripMenuItem.Text = "Refresh list";
			this.testToolStripMenuItem.Click += new System.EventHandler(this.TestToolStripMenuItemClick);
			// 
			// processManagerToolStripMenuItem
			// 
			this.processManagerToolStripMenuItem.Name = "processManagerToolStripMenuItem";
			this.processManagerToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.processManagerToolStripMenuItem.Text = "Process manager";
			this.processManagerToolStripMenuItem.Click += new System.EventHandler(this.ProcessManagerToolStripMenuItemClick);
			// 
			// windowsHoocksToolStripMenuItem
			// 
			this.windowsHoocksToolStripMenuItem.Name = "windowsHoocksToolStripMenuItem";
			this.windowsHoocksToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.windowsHoocksToolStripMenuItem.Text = "WindowsHoocks";
			this.windowsHoocksToolStripMenuItem.Click += new System.EventHandler(this.WindowsHoocksToolStripMenuItemClick);
			// 
			// installedFrameworkToolStripMenuItem
			// 
			this.installedFrameworkToolStripMenuItem.Name = "installedFrameworkToolStripMenuItem";
			this.installedFrameworkToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.installedFrameworkToolStripMenuItem.Text = "Installed Framework";
			this.installedFrameworkToolStripMenuItem.Click += new System.EventHandler(this.InstalledFrameworkToolStripMenuItemClick);
			// 
			// dumpingOptionToolStripMenuItem
			// 
			this.dumpingOptionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.dumpNativeToolStripMenuItem,
									this.autoRefreshToolStripMenuItem,
									this.dontRestoreFilenameToolStripMenuItem});
			this.dumpingOptionToolStripMenuItem.Name = "dumpingOptionToolStripMenuItem";
			this.dumpingOptionToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.dumpingOptionToolStripMenuItem.Text = "DumpingOption";
			// 
			// dumpNativeToolStripMenuItem
			// 
			this.dumpNativeToolStripMenuItem.CheckOnClick = true;
			this.dumpNativeToolStripMenuItem.Name = "dumpNativeToolStripMenuItem";
			this.dumpNativeToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.dumpNativeToolStripMenuItem.Text = "Dump native";
			// 
			// autoRefreshToolStripMenuItem
			// 
			this.autoRefreshToolStripMenuItem.Name = "autoRefreshToolStripMenuItem";
			this.autoRefreshToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.autoRefreshToolStripMenuItem.Text = "AutoRefresh";
			// 
			// dontRestoreFilenameToolStripMenuItem
			// 
			this.dontRestoreFilenameToolStripMenuItem.CheckOnClick = true;
			this.dontRestoreFilenameToolStripMenuItem.Name = "dontRestoreFilenameToolStripMenuItem";
			this.dontRestoreFilenameToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.dontRestoreFilenameToolStripMenuItem.Text = "Don\'t restore filename";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(790, 444);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.lvprocesslist);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MegaDumper 1.0 by CodeCracker / SnD";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.contextMenuStrip1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem injectManagedDllToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader status;
		private System.Windows.Forms.ToolStripMenuItem fileDirectoriesListToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader isnet;
		private System.Windows.Forms.ToolStripMenuItem generateDmpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem environmentVariablesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hookDetectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enumAppdomainsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem virtualMemoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem advancedInfoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dontRestoreFilenameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autoRefreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dumpNativeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dumpingOptionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem installedFrameworkToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowsHoocksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem processManagerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mainToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewHeapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rttoolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem anToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem priorityToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem maximizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bringToFrontToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resumeProcessToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem suspendProcessToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem gotoLocationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dumpModuleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem nETPerformanceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dumpToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ColumnHeader location;
		private System.Windows.Forms.ColumnHeader PID;
		private System.Windows.Forms.ColumnHeader procname;
		private System.Windows.Forms.ListView lvprocesslist;
	}
}
