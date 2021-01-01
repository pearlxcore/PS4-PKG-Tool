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
    public partial class PKG_Locations : DarkUI.Forms.DarkForm
    {
        private Form1 form;

        public PKG_Locations()
        {
            InitializeComponent();
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
            Properties.Settings.Default.dsfg = fbd.SelectedPath;
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

        private bool Is_Form_Loaded_Already(string FormName)
        {
            foreach (Form form_loaded in Application.OpenForms)
            {
                if (form_loaded.Text.IndexOf(FormName) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void ErrorNewMessageBox(string title, string message)
        {
            DarkMessageBox msgResized = new DarkMessageBox(title, message);
            msgResized.StartPosition = FormStartPosition.CenterScreen;
            msgResized.Show();
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
            //declare first
            Properties.Settings.Default.PKGLocationList = new List<string>();

            foreach (string folder in darkListBox1.Items)
            {

                Properties.Settings.Default.PKGLocationList.Add(folder);
            }

            if(darkCheckBoxRecursive.CheckState == CheckState.Checked)
            {
                Properties.Settings.Default.ScanRecursive = true;

            }
            else
            {
                Properties.Settings.Default.ScanRecursive = false;
            }
            Properties.Settings.Default.Save();

            PS4PKGTOOL.DoThis = true;

            this.Hide();

            form = new Form1();

            if (!form.Visible)
            {
                // Add the message
                form.Show();
            }
            else
            {
                // Top
                form.BringToFront();
            }


            //ReopenChildForm();

            //if(Is_Form_Loaded_Already("Form1") == false)
            //{
            //    form = new Form1();
            //    form.Show();
            //}
            //else
            //{

            //}
        }

        private void ReopenChildForm()
        {
            if (!form.Visible)
            {
                form.Hide();
            }
            //Update form information
            form.Show();
        }

        private static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }


        private void PKG_Locations_Load(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + @"\PS4_PKG_Tool_LogFile.txt"))
            {
                File.Delete(Environment.CurrentDirectory + @"\PS4_PKG_Tool_LogFile.txt");
            }

            File.Create("PS4_PKG_Tool_LogFile.txt").Dispose();

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

        private void myCheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //checkedListBox1.ClearSelected();
        }

      
    }
}
