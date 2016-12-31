/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 27.10.2010
 * Time: 18:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Mega_Dumper
{
	partial class NetPerformance
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
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.value = new System.Windows.Forms.ColumnHeader();
			this.counter = new System.Windows.Forms.ColumnHeader();
			this.perfobject = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(36, 25);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(171, 21);
			this.comboBox1.TabIndex = 7;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
			// 
			// value
			// 
			this.value.Text = "Value";
			this.value.Width = 114;
			// 
			// counter
			// 
			this.counter.Text = "Counter";
			this.counter.Width = 338;
			// 
			// perfobject
			// 
			this.perfobject.Alignment = System.Windows.Forms.ListViewAlignment.Default;
			this.perfobject.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.perfobject.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.counter,
									this.value});
			this.perfobject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.perfobject.FullRowSelect = true;
			this.perfobject.Location = new System.Drawing.Point(35, 52);
			this.perfobject.MultiSelect = false;
			this.perfobject.Name = "perfobject";
			this.perfobject.Size = new System.Drawing.Size(524, 490);
			this.perfobject.TabIndex = 8;
			this.perfobject.UseCompatibleStateImageBehavior = false;
			this.perfobject.View = System.Windows.Forms.View.Details;
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(587, 566);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.perfobject);
			this.Name = "Form2";
			this.Text = ".NET Performance";
			this.Load += new System.EventHandler(this.Form2Load);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ListView perfobject;
		private System.Windows.Forms.ColumnHeader counter;
		private System.Windows.Forms.ColumnHeader value;
		private System.Windows.Forms.ComboBox comboBox1;
	}
}
