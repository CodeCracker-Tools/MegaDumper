/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 19.04.2011
 * Time: 20:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Mega_Dumper
{
	partial class GenerateDmp
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.dmpoption = new System.Windows.Forms.ComboBox();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(44, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 14);
			this.label1.TabIndex = 16;
			this.label1.Text = "Name of dmp file:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(44, 38);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(544, 20);
			this.textBox1.TabIndex = 15;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(10, 37);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(28, 20);
			this.button1.TabIndex = 14;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// dmpoption
			// 
			this.dmpoption.FormattingEnabled = true;
			this.dmpoption.Location = new System.Drawing.Point(44, 116);
			this.dmpoption.Name = "dmpoption";
			this.dmpoption.Size = new System.Drawing.Size(274, 21);
			this.dmpoption.TabIndex = 17;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(365, 114);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(97, 22);
			this.button2.TabIndex = 18;
			this.button2.Text = "Generate dmp";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(44, 101);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 12);
			this.label2.TabIndex = 19;
			this.label2.Text = "DumpType:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(44, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(544, 15);
			this.label3.TabIndex = 20;
			this.label3.Text = "Statut";
			// 
			// GenerateDmp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(631, 149);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.dmpoption);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Name = "GenerateDmp";
			this.Text = "Generate dmp";
			this.Shown += new System.EventHandler(this.GenerateDmpShown);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox dmpoption;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
	}
}
