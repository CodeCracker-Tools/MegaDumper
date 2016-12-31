/*
 * Created by SharpDevelop.
 * User: Bogdan
 * Date: 04.03.2011
 * Time: 16:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Mega_Dumper
{
	partial class AboutForm
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
			this.buttonClose = new System.Windows.Forms.Button();
			this.linkSourceforge = new System.Windows.Forms.LinkLabel();
			this.labelAppName = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.flowCredits = new System.Windows.Forms.FlowLayoutPanel();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.flowCredits.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonClose.Location = new System.Drawing.Point(418, 215);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(62, 23);
			this.buttonClose.TabIndex = 23;
			this.buttonClose.Text = "Cool";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
			// 
			// linkSourceforge
			// 
			this.linkSourceforge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.linkSourceforge.AutoSize = true;
			this.linkSourceforge.Location = new System.Drawing.Point(36, 215);
			this.linkSourceforge.Name = "linkSourceforge";
			this.linkSourceforge.Size = new System.Drawing.Size(135, 13);
			this.linkSourceforge.TabIndex = 20;
			this.linkSourceforge.TabStop = true;
			this.linkSourceforge.Text = "http://forum.tuts4you.com/";
			this.linkSourceforge.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkSourceforgeLinkClicked);
			// 
			// labelAppName
			// 
			this.labelAppName.AutoSize = true;
			this.labelAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelAppName.Location = new System.Drawing.Point(220, 19);
			this.labelAppName.Name = "labelAppName";
			this.labelAppName.Size = new System.Drawing.Size(101, 16);
			this.labelAppName.TabIndex = 16;
			this.labelAppName.Text = "MegaDumper";
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Location = new System.Drawing.Point(220, 40);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(60, 13);
			this.labelVersion.TabIndex = 17;
			this.labelVersion.Text = "Version 1.0";
			// 
			// label14
			// 
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(36, 202);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(82, 13);
			this.label14.TabIndex = 26;
			this.label14.Text = "Visit homepage:";
			// 
			// flowCredits
			// 
			this.flowCredits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.flowCredits.AutoScroll = true;
			this.flowCredits.Controls.Add(this.label8);
			this.flowCredits.Controls.Add(this.label5);
			this.flowCredits.Controls.Add(this.label11);
			this.flowCredits.Controls.Add(this.label13);
			this.flowCredits.Controls.Add(this.label1);
			this.flowCredits.Controls.Add(this.label3);
			this.flowCredits.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowCredits.Location = new System.Drawing.Point(220, 76);
			this.flowCredits.Name = "flowCredits";
			this.flowCredits.Padding = new System.Windows.Forms.Padding(3);
			this.flowCredits.Size = new System.Drawing.Size(291, 99);
			this.flowCredits.TabIndex = 29;
			this.flowCredits.WrapContents = false;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(6, 3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 13);
			this.label8.TabIndex = 17;
			this.label8.Text = "Author";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 13);
			this.label5.TabIndex = 18;
			this.label5.Text = "CodeCracker / SND";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(6, 29);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(68, 13);
			this.label11.TabIndex = 17;
			this.label11.Text = "Thanks to:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(6, 42);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(138, 13);
			this.label13.TabIndex = 30;
			this.label13.Text = "Kurapica - BlackStorm team";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(169, 13);
			this.label1.TabIndex = 31;
			this.label1.Text = "Whoknows - BlackStorm team";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(0, 13);
			this.label3.TabIndex = 15;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(521, 244);
			this.Controls.Add(this.flowCredits);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.linkSourceforge);
			this.Controls.Add(this.labelAppName);
			this.Controls.Add(this.labelVersion);
			this.Name = "AboutForm";
			this.Text = "AboutForm";
			this.flowCredits.ResumeLayout(false);
			this.flowCredits.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelAppName;
		private System.Windows.Forms.LinkLabel linkSourceforge;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.FlowLayoutPanel flowCredits;
		private System.Windows.Forms.Label label8;
	}
}
