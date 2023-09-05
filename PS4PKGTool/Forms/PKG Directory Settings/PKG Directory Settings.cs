using GameArchives.PKF;
using PS4PKGTool.Util;
using PS4PKGTool.Utilities.PS4PKGToolHelper;
using PS4PKGTool.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PS4PKGTool.Utilities.PS4PKGToolHelper.Helper;

namespace PS4PKGTool
{
    public partial class PKG_Directory_Settings : DarkUI.Forms.DarkForm
    {
        public bool closingProgram = true;
        private Main form;
        static Version currentVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());

        public PKG_Directory_Settings()
        {
            InitializeComponent();

            // remove empty listbox item
            if (appSettings_.PkgDirectories.Count == 1 && appSettings_.PkgDirectories[0] == string.Empty)
            {
                appSettings_.PkgDirectories.Clear();
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
            {
                string selectedFolder = fbd.SelectedPath;

                if (darkListBox1.Items.Contains(selectedFolder))
                {
                    ShowError("Path already added.", false);
                    return;
                }

                if (Tool.IsRootDrive(selectedFolder))
                {
                    DialogResult dialogResult = DialogResultYesNo("Scanning the whole drive may take some time. Are you sure you want to proceed?");
                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }

                darkListBox1.Items.Add(selectedFolder);
            }
        }

        private void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            if (darkListBox1.SelectedItems.Count > 0)
            {
                int selectedIndex = darkListBox1.SelectedIndex;
                darkListBox1.Items.RemoveAt(selectedIndex);
            }
        }

        private void btnLoadPkg_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("Loading PKG..");
            appSettings_.ShowDirectorySettingsAtStartup = darkCheckBoxDontshowthisagain.Checked;

            if (darkListBox1.Items.Count == 0)
            {
                ShowError("Specify PS4 PKG directory.", false);
                return;
            }

            bool dirNotExists = false;
            foreach (string folder in darkListBox1.Items)
            {
                if (!Directory.Exists(folder))
                {
                    ShowError("Directory does not exist: " + folder, false);
                    dirNotExists = true;
                }
            }
            if (dirNotExists)
                return;
            closingProgram = false;
            if (!Helper.Tool.CheckForInternetConnection())
                ShowWarning("No internet connection detected. Some features in the program may not working.", true);

            var PkgDirectoryList = darkListBox1.Items.Cast<string>().ToList();
            appSettings_.PkgDirectories = PkgDirectoryList;
            appSettings_.ScanRecursive = darkCheckBoxRecursive.Checked;
            appSettings_.ShowDirectorySettingsAtStartup = darkCheckBoxDontshowthisagain.Checked;

            SettingsManager.SaveSettings(appSettings_, SettingFilePath);

            Helper.FinalizePkgProcess = true;

            this.Hide();
            if (Helper.FirstLaunch)
            {
                form = new Main();

                if (!form.Visible)
                {
                    form.Show();
                }
                else
                {
                    form.BringToFront();
                }
            }
        }

        private void PKG_Locations_Load(object sender, EventArgs e)
        {
            string logFilePath = Path.Combine(Environment.CurrentDirectory, "PS4_PKG_Tool_LogFile.txt");
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            File.Create(logFilePath).Dispose();
            darkCheckBoxDontshowthisagain.Visible = (Helper.FirstLaunch) ? true : false;
            darkCheckBoxRecursive.Checked = appSettings_.ScanRecursive;
            darkListBox1.Items.AddRange(appSettings_.PkgDirectories?.Cast<string>().ToArray() ?? Array.Empty<string>());
            darkCheckBoxDontshowthisagain.Checked = appSettings_.ShowDirectorySettingsAtStartup;
        }

        private void darkCheckBoxDontshowthisagain_MouseClick(object sender, MouseEventArgs e)
        {
            if (darkCheckBoxDontshowthisagain.Checked)
                ShowInformation("You can enable this back in program settings.", false);
        }

        private void darkButton2_Click(object sender, EventArgs e)
        {
            darkListBox1.Items.Clear();
        }
    }
}
