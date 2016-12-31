/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 02.03.2011
 * Time: 19:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Mega_Dumper
{
	partial class EnumAppDomains
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
			this.lvdomains = new System.Windows.Forms.ListView();
			this.id = new System.Windows.Forms.ColumnHeader();
			this.name = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// lvdomains
			// 
			this.lvdomains.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.lvdomains.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvdomains.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.id,
									this.name});
			this.lvdomains.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lvdomains.FullRowSelect = true;
			this.lvdomains.Location = new System.Drawing.Point(-1, -1);
			this.lvdomains.MultiSelect = false;
			this.lvdomains.Name = "lvdomains";
			this.lvdomains.Size = new System.Drawing.Size(518, 199);
			this.lvdomains.TabIndex = 10;
			this.lvdomains.UseCompatibleStateImageBehavior = false;
			this.lvdomains.View = System.Windows.Forms.View.Details;
			// 
			// id
			// 
			this.id.Text = "Id";
			this.id.Width = 56;
			// 
			// name
			// 
			this.name.Text = "Appdomain name";
			this.name.Width = 373;
			// 
			// EnumAssemblies
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(516, 196);
			this.Controls.Add(this.lvdomains);
			this.Name = "Enum AppDomains";
			this.Text = "Enum AppDomains";
			this.Shown += new System.EventHandler(this.EnumAppDomainsShown);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ListView lvdomains;
		private System.Windows.Forms.ColumnHeader id;
		private System.Windows.Forms.ColumnHeader name;
	}
}
