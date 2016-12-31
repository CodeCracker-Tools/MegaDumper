/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 01.01.2013
 * Time: 09:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace MegaDumper
{
	partial class ManagedInjector
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
			this.button2 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(60, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 14);
			this.label1.TabIndex = 16;
			this.label1.Text = "Name of assembly:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(60, 35);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(471, 20);
			this.textBox1.TabIndex = 15;
			this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox1DragDrop);
			this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox1DragEnter);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(26, 35);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(28, 20);
			this.button1.TabIndex = 14;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(74, 161);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(61, 23);
			this.button2.TabIndex = 17;
			this.button2.Text = "Inject";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(60, 82);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(471, 20);
			this.textBox2.TabIndex = 18;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(60, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(441, 11);
			this.label2.TabIndex = 19;
			this.label2.Text = "Class name in format Namespace.Class:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(60, 117);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(441, 11);
			this.label3.TabIndex = 21;
			this.label3.Text = "Method name (it must have a string argument, public attribute and must return int" +
			"):";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(60, 131);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(471, 20);
			this.textBox3.TabIndex = 20;
			// 
			// ManagedInjector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 202);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Name = "ManagedInjector";
			this.Text = "Managed Injector";
			this.Shown += new System.EventHandler(this.ManagedInjectorShown);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
	}
}
