using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MegaDumper
{
    public partial class FilterForm : Form
    {
        private Dictionary<string, string> filterOptionsDictionary = new Dictionary<string, string>();

        public FilterForm()
        {
            InitializeComponent();
        }

        private void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            if (checkBoxFilterByProcessName.Checked && textBoxProcessName.Text != "")
            {
                filterOptionsDictionary.Add("processname", textBoxProcessName.Text);
            }
            if (checkBoxFilterByIsDotNet.Checked && comboBoxIsDotNet.SelectedItem.ToString() != "")
            {
                filterOptionsDictionary.Add("isdotnet", comboBoxIsDotNet.SelectedItem.ToString());
            }
            if (checkBoxFilterByLocation.Checked && textBoxProcessLocation.Text != "")
            {
                filterOptionsDictionary.Add("processlocation", textBoxProcessLocation.Text);
            }
            if (checkBoxFilterByPID.Checked && numericUpDownPID.Text != "")
            {
                filterOptionsDictionary.Add("PID", numericUpDownPID.Text);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {
            
        }

        private void textBoxProcessName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBoxFilterByProcessName_CheckedChanged(object sender, EventArgs e)
        {

        }

        public Dictionary<string, string> GetSelectedOptions()
        {
            return filterOptionsDictionary;
        }

        private void buttonBrowsePocessLocation_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBoxProcessLocation.Text = folderBrowserDialog1.SelectedPath;
                }
            }
        }
    }
}
