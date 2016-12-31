/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 22.01.2011
 * Time: 14:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Mega_Dumper
{
	partial class FrmModules
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
			this.lvmodules = new System.Windows.Forms.ListView();
			this.modulename = new System.Windows.Forms.ColumnHeader();
			this.ba = new System.Windows.Forms.ColumnHeader();
			this.ms = new System.Windows.Forms.ColumnHeader();
			this.ep = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.dumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.freeModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.codeSectionChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.detectAntidumpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvmodules
			// 
			this.lvmodules.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.lvmodules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvmodules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.modulename,
									this.ba,
									this.ms,
									this.ep});
			this.lvmodules.ContextMenuStrip = this.contextMenuStrip1;
			this.lvmodules.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lvmodules.FullRowSelect = true;
			this.lvmodules.Location = new System.Drawing.Point(0, 0);
			this.lvmodules.MultiSelect = false;
			this.lvmodules.Name = "lvmodules";
			this.lvmodules.Size = new System.Drawing.Size(583, 320);
			this.lvmodules.TabIndex = 9;
			this.lvmodules.UseCompatibleStateImageBehavior = false;
			this.lvmodules.View = System.Windows.Forms.View.Details;
			// 
			// modulename
			// 
			this.modulename.Text = "Module Name";
			this.modulename.Width = 137;
			// 
			// ba
			// 
			this.ba.Text = "Base Address";
			this.ba.Width = 88;
			// 
			// ms
			// 
			this.ms.Text = "Module size";
			this.ms.Width = 83;
			// 
			// ep
			// 
			this.ep.Text = "EntryPointAddress";
			this.ep.Width = 109;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.dumpToolStripMenuItem,
									this.copyToolStripMenuItem,
									this.freeModuleToolStripMenuItem,
									this.codeSectionChangesToolStripMenuItem,
									this.detectAntidumpsToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(191, 114);
			// 
			// dumpToolStripMenuItem
			// 
			this.dumpToolStripMenuItem.Name = "dumpToolStripMenuItem";
			this.dumpToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.dumpToolStripMenuItem.Text = "Dump";
			this.dumpToolStripMenuItem.Click += new System.EventHandler(this.DumpToolStripMenuItemClick);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.copyToolStripMenuItem.Text = "Copy info";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
			// 
			// freeModuleToolStripMenuItem
			// 
			this.freeModuleToolStripMenuItem.Name = "freeModuleToolStripMenuItem";
			this.freeModuleToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.freeModuleToolStripMenuItem.Text = "Free module";
			this.freeModuleToolStripMenuItem.Click += new System.EventHandler(this.FreeModuleToolStripMenuItemClick);
			// 
			// codeSectionChangesToolStripMenuItem
			// 
			this.codeSectionChangesToolStripMenuItem.Name = "codeSectionChangesToolStripMenuItem";
			this.codeSectionChangesToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.codeSectionChangesToolStripMenuItem.Text = "Code section changes";
			this.codeSectionChangesToolStripMenuItem.Click += new System.EventHandler(this.CodeSectionChangesToolStripMenuItemClick);
			// 
			// detectAntidumpsToolStripMenuItem
			// 
			this.detectAntidumpsToolStripMenuItem.Name = "detectAntidumpsToolStripMenuItem";
			this.detectAntidumpsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.detectAntidumpsToolStripMenuItem.Text = "Detect anti-dumps";
			this.detectAntidumpsToolStripMenuItem.Click += new System.EventHandler(this.DetectAntidumpsToolStripMenuItemClick);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(12, 353);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(82, 22);
			this.button1.TabIndex = 10;
			this.button1.Text = "Refresh";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.Location = new System.Drawing.Point(0, 323);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(583, 23);
			this.label2.TabIndex = 13;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Location = new System.Drawing.Point(256, 352);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(73, 23);
			this.button2.TabIndex = 14;
			this.button2.Text = "Copy infos";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(383, 353);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(114, 23);
			this.button3.TabIndex = 15;
			this.button3.Text = "Copy module names";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button4.Location = new System.Drawing.Point(125, 353);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 16;
			this.button4.Text = "Inject dll";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// FrmModules
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(583, 387);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lvmodules);
			this.Name = "FrmModules";
			this.Text = "Modules";
			this.Shown += new System.EventHandler(this.FrmModulesShown);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ToolStripMenuItem codeSectionChangesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem detectAntidumpsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem freeModuleToolStripMenuItem;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ToolStripMenuItem dumpToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ColumnHeader ep;
		private System.Windows.Forms.ColumnHeader ba;
		private System.Windows.Forms.ColumnHeader ms;
		private System.Windows.Forms.ColumnHeader modulename;
		private System.Windows.Forms.ListView lvmodules;
		

		

		

	}
}
