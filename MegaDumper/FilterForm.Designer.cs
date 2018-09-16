namespace MegaDumper
{
    partial class FilterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxProcessName = new System.Windows.Forms.TextBox();
            this.numericUpDownPID = new System.Windows.Forms.NumericUpDown();
            this.checkBoxFilterByProcessName = new System.Windows.Forms.CheckBox();
            this.checkBoxFilterByPID = new System.Windows.Forms.CheckBox();
            this.checkBoxFilterByIsDotNet = new System.Windows.Forms.CheckBox();
            this.comboBoxIsDotNet = new System.Windows.Forms.ComboBox();
            this.checkBoxFilterByLocation = new System.Windows.Forms.CheckBox();
            this.textBoxProcessLocation = new System.Windows.Forms.TextBox();
            this.buttonBrowsePocessLocation = new System.Windows.Forms.Button();
            this.buttonApplyFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPID)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxProcessName
            // 
            this.textBoxProcessName.Location = new System.Drawing.Point(138, 23);
            this.textBoxProcessName.Name = "textBoxProcessName";
            this.textBoxProcessName.Size = new System.Drawing.Size(187, 22);
            this.textBoxProcessName.TabIndex = 1;
            this.textBoxProcessName.TextChanged += new System.EventHandler(this.textBoxProcessName_TextChanged);
            // 
            // numericUpDownPID
            // 
            this.numericUpDownPID.Location = new System.Drawing.Point(138, 62);
            this.numericUpDownPID.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.numericUpDownPID.Name = "numericUpDownPID";
            this.numericUpDownPID.Size = new System.Drawing.Size(102, 22);
            this.numericUpDownPID.TabIndex = 3;
            this.numericUpDownPID.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // checkBoxFilterByProcessName
            // 
            this.checkBoxFilterByProcessName.AutoSize = true;
            this.checkBoxFilterByProcessName.Location = new System.Drawing.Point(13, 23);
            this.checkBoxFilterByProcessName.Name = "checkBoxFilterByProcessName";
            this.checkBoxFilterByProcessName.Size = new System.Drawing.Size(119, 21);
            this.checkBoxFilterByProcessName.TabIndex = 4;
            this.checkBoxFilterByProcessName.Text = "Pocess name:";
            this.checkBoxFilterByProcessName.UseVisualStyleBackColor = true;
            this.checkBoxFilterByProcessName.CheckedChanged += new System.EventHandler(this.checkBoxFilterByProcessName_CheckedChanged);
            // 
            // checkBoxFilterByPID
            // 
            this.checkBoxFilterByPID.AutoSize = true;
            this.checkBoxFilterByPID.Location = new System.Drawing.Point(13, 62);
            this.checkBoxFilterByPID.Name = "checkBoxFilterByPID";
            this.checkBoxFilterByPID.Size = new System.Drawing.Size(56, 21);
            this.checkBoxFilterByPID.TabIndex = 5;
            this.checkBoxFilterByPID.Text = "PID:";
            this.checkBoxFilterByPID.UseVisualStyleBackColor = true;
            // 
            // checkBoxFilterByIsDotNet
            // 
            this.checkBoxFilterByIsDotNet.AutoSize = true;
            this.checkBoxFilterByIsDotNet.Location = new System.Drawing.Point(13, 99);
            this.checkBoxFilterByIsDotNet.Name = "checkBoxFilterByIsDotNet";
            this.checkBoxFilterByIsDotNet.Size = new System.Drawing.Size(66, 21);
            this.checkBoxFilterByIsDotNet.TabIndex = 6;
            this.checkBoxFilterByIsDotNet.Text = ".NET:";
            this.checkBoxFilterByIsDotNet.UseVisualStyleBackColor = true;
            // 
            // comboBoxIsDotNet
            // 
            this.comboBoxIsDotNet.FormattingEnabled = true;
            this.comboBoxIsDotNet.Items.AddRange(new object[] {
            "true",
            "false"});
            this.comboBoxIsDotNet.Location = new System.Drawing.Point(138, 99);
            this.comboBoxIsDotNet.Name = "comboBoxIsDotNet";
            this.comboBoxIsDotNet.Size = new System.Drawing.Size(102, 24);
            this.comboBoxIsDotNet.TabIndex = 7;
            this.comboBoxIsDotNet.Text = "true";
            // 
            // checkBoxFilterByLocation
            // 
            this.checkBoxFilterByLocation.AutoSize = true;
            this.checkBoxFilterByLocation.Location = new System.Drawing.Point(13, 135);
            this.checkBoxFilterByLocation.Name = "checkBoxFilterByLocation";
            this.checkBoxFilterByLocation.Size = new System.Drawing.Size(88, 21);
            this.checkBoxFilterByLocation.TabIndex = 9;
            this.checkBoxFilterByLocation.Text = "Location:";
            this.checkBoxFilterByLocation.UseVisualStyleBackColor = true;
            // 
            // textBoxProcessLocation
            // 
            this.textBoxProcessLocation.Location = new System.Drawing.Point(138, 135);
            this.textBoxProcessLocation.Name = "textBoxProcessLocation";
            this.textBoxProcessLocation.Size = new System.Drawing.Size(136, 22);
            this.textBoxProcessLocation.TabIndex = 8;
            // 
            // buttonBrowsePocessLocation
            // 
            this.buttonBrowsePocessLocation.Location = new System.Drawing.Point(280, 134);
            this.buttonBrowsePocessLocation.Name = "buttonBrowsePocessLocation";
            this.buttonBrowsePocessLocation.Size = new System.Drawing.Size(45, 23);
            this.buttonBrowsePocessLocation.TabIndex = 10;
            this.buttonBrowsePocessLocation.Text = "...";
            this.buttonBrowsePocessLocation.UseVisualStyleBackColor = true;
            this.buttonBrowsePocessLocation.Click += new System.EventHandler(this.buttonBrowsePocessLocation_Click);
            // 
            // buttonApplyFilter
            // 
            this.buttonApplyFilter.Location = new System.Drawing.Point(138, 175);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new System.Drawing.Size(91, 31);
            this.buttonApplyFilter.TabIndex = 11;
            this.buttonApplyFilter.Text = "Apply";
            this.buttonApplyFilter.UseVisualStyleBackColor = true;
            this.buttonApplyFilter.Click += new System.EventHandler(this.buttonApplyFilter_Click);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 218);
            this.Controls.Add(this.buttonApplyFilter);
            this.Controls.Add(this.buttonBrowsePocessLocation);
            this.Controls.Add(this.checkBoxFilterByLocation);
            this.Controls.Add(this.textBoxProcessLocation);
            this.Controls.Add(this.comboBoxIsDotNet);
            this.Controls.Add(this.checkBoxFilterByIsDotNet);
            this.Controls.Add(this.checkBoxFilterByPID);
            this.Controls.Add(this.checkBoxFilterByProcessName);
            this.Controls.Add(this.numericUpDownPID);
            this.Controls.Add(this.textBoxProcessName);
            this.MaximizeBox = false;
            this.Name = "FilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter processes";
            this.Load += new System.EventHandler(this.FilterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxProcessName;
        private System.Windows.Forms.NumericUpDown numericUpDownPID;
        private System.Windows.Forms.CheckBox checkBoxFilterByProcessName;
        private System.Windows.Forms.CheckBox checkBoxFilterByPID;
        private System.Windows.Forms.CheckBox checkBoxFilterByIsDotNet;
        private System.Windows.Forms.ComboBox comboBoxIsDotNet;
        private System.Windows.Forms.CheckBox checkBoxFilterByLocation;
        private System.Windows.Forms.TextBox textBoxProcessLocation;
        private System.Windows.Forms.Button buttonBrowsePocessLocation;
        private System.Windows.Forms.Button buttonApplyFilter;
    }
}