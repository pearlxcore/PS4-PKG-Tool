using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4_PKG_Tool
{
    public partial class PKG_Location_InForm : DarkUI.Forms.DarkForm
    {
        public bool isCancelling = true;

        public PKG_Location_InForm()
        {
            InitializeComponent();
        }

        private void PKG_Location_InForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ScanRecursive == true)
            {
                darkCheckBoxRecursive.CheckState = CheckState.Checked;
            }
            else
            {
                darkCheckBoxRecursive.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.PKGLocationList != null)
            {
                foreach (var item in Properties.Settings.Default.PKGLocationList)
                {
                    darkListBox1.Items.Add(item);
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select folder containing PS4 PKG";
            fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
            fbd.SelectedPath = Properties.Settings.Default.RootFolder;
            if (fbd.ShowDialog() == DialogResult.Cancel)
                return;

            foreach (var item in darkListBox1.Items)
            {
                if (item.ToString() == fbd.SelectedPath)
                {
                    DarkMessageBox.ShowError("Path already added.", "PS4 PKG Tool");
                    return;
                }
            }

            string selectedFolder = fbd.SelectedPath;
            Properties.Settings.Default.RootFolder = fbd.SelectedPath;
            Properties.Settings.Default.Save();
            darkListBox1.Items.Add(selectedFolder);
        }

        private void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            if (darkListBox1.SelectedItems.Count != 0)
            {
                darkListBox1.Items.RemoveAt(darkListBox1.SelectedIndex);

            }
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            if (darkListBox1.Items.Count == 0)
            {
                DarkMessageBox.ShowError("Please add PS4 PKG folder.", "PS4 PKG Tool");
                return;
            }
            
            bool dirNotExists = false;
            foreach (string folder in darkListBox1.Items)
            {
                if (!Directory.Exists(folder))
                {
                    DarkMessageBox.ShowError("Directory not exists : " + folder, "PS4 PKG Tool");
                    dirNotExists = true;
                }
            }
            if (dirNotExists == true)
                return;
            isCancelling = false;
            //declare first
            Properties.Settings.Default.PKGLocationList = new List<string>();

            foreach (string folder in darkListBox1.Items)
            {
                Properties.Settings.Default.PKGLocationList.Add(folder);
            }

            PS4PKGTOOL.DoThis = true;

            Properties.Settings.Default.Save();

            this.Hide();
        }

        private void darkCheckBoxRecursive_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ScanRecursive == true)
            {
                darkCheckBoxRecursive.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.ScanRecursive = false;
            }
            else
            {
                darkCheckBoxRecursive.CheckState = CheckState.Checked;
                Properties.Settings.Default.ScanRecursive = true;
            }
            Properties.Settings.Default.Save();
        }
    }
}
