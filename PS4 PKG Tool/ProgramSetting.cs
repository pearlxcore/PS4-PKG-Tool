using DarkUI.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4_PKG_Tool
{
    public partial class ProgramSetting : DarkUI.Forms.DarkForm
    {
        private string HttpServerModulePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\npm\node_modules\http-server";

        public ProgramSetting()
        {
            InitializeComponent();
        }


        private void darkButton2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select download folder for PKG Update";
            if (fbd.ShowDialog() == DialogResult.Cancel)
                return;

            tbDownloadFolder.Text = fbd.SelectedPath;
            Properties.Settings.Default.DOWNLOADFOLDER = fbd.SelectedPath;
        }

        private void ProgramSetting_Load(object sender, EventArgs e)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            List<string> listIP = new List<string>();
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    darkComboBoxServerIP.Items.Add(ip.ToString());
                }
            }

            #region LoadSetting

            tbDownloadFolder.Text = Properties.Settings.Default.DOWNLOADFOLDER;
            tbPS4IP.Text = Properties.Settings.Default.PS4IP;
            darkComboBoxServerIP.Text = Properties.Settings.Default.SERVERIP;

            if (Properties.Settings.Default.title_id == true)
            {
                TitleID.CheckState = CheckState.Checked;
            }
            else
            {
                TitleID.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.content_id == true)
            {
                ContentID.CheckState = CheckState.Checked;
            }
            else
            {
                ContentID.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.Region == true)
            {
                Region.CheckState = CheckState.Checked;
            }
            else
            {
                Region.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.system_firmware == true)
            {
                SystemFirmware.CheckState = CheckState.Checked;
            }
            else
            {
                SystemFirmware.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.version == true)
            {
                Version.CheckState = CheckState.Checked;
            }
            else
            {
                Version.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.pkg_type == true)
            {
                PKGType.CheckState = CheckState.Checked;
            }
            else
            {
                PKGType.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.category == true)
            {
                Category.CheckState = CheckState.Checked;
            }
            else
            {
                Category.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.size == true)
            {
                Size.CheckState = CheckState.Checked;
            }
            else
            {
                Size.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.psvr == true)
            {
                PSVR.CheckState = CheckState.Checked;
            }
            else
            {
                PSVR.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.neoEnable == true)
            {
                neoEnable.CheckState = CheckState.Checked;
            }
            else
            {
                neoEnable.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.ps5bc == true)
            {
                ps5bc.CheckState = CheckState.Checked;
            }
            else
            {
                ps5bc.CheckState = CheckState.Unchecked;
            }

            if (Properties.Settings.Default.location == true)
            {
                Location.CheckState = CheckState.Checked;
            }
            else
            {
                Location.CheckState = CheckState.Unchecked;
            }


            if (Properties.Settings.Default.BGMEnable == true)
            {
                BGM.CheckState = CheckState.Checked;
            }
            else
            {
                BGM.CheckState = CheckState.Unchecked;
            }

            #endregion LoadSetting

            #region nodejs&serve
            if (IsSoftwareInstalled("Node.js") == true)
            {
                darkLabelNodejsInstalled.Text = "✔";
                darkLabelNodejsInstalled.ForeColor = Color.Green;
                darkButtonInstallNodejs.Enabled = false;
                Properties.Settings.Default.node_js = true;
            }
            else
            {

                darkLabelNodejsInstalled.Text = "✘";
                darkLabelNodejsInstalled.ForeColor = Color.Red;
                darkButtonInstallNodejs.Enabled = true;
                Properties.Settings.Default.node_js = false;

            }

            if (Directory.Exists(HttpServerModulePath))
            {
                darkLabelserveModuleInstalled.Text = "✔";
                darkLabelserveModuleInstalled.ForeColor = Color.Green;
                darkButtonInstaleServemodule.Enabled = false;
                Properties.Settings.Default.http_server = true;

            }
            else
            {
                darkLabelserveModuleInstalled.Text = "✘";
                darkLabelserveModuleInstalled.ForeColor = Color.Red;
                darkButtonInstaleServemodule.Enabled = true;
                Properties.Settings.Default.http_server = false;

            }

            #endregion nodejs&serve

        }

        public static bool IsSoftwareInstalled(string softwareName)
        {
            var registryUninstallPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            var registryUninstallPathFor32BitOn64Bit = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

            if (Is32BitWindows())
                return IsSoftwareInstalled(softwareName, RegistryView.Registry32, registryUninstallPath);

            var is64BitSoftwareInstalled = IsSoftwareInstalled(softwareName, RegistryView.Registry64, registryUninstallPath);
            var is32BitSoftwareInstalled = IsSoftwareInstalled(softwareName, RegistryView.Registry64, registryUninstallPathFor32BitOn64Bit);
            return is64BitSoftwareInstalled || is32BitSoftwareInstalled;
        }

        private static bool Is32BitWindows() => Environment.Is64BitOperatingSystem == false;

        private static bool IsSoftwareInstalled(string softwareName, RegistryView registryView, string installedProgrammsPath)
        {
            var uninstallKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView)
                                                  .OpenSubKey(installedProgrammsPath);

            if (uninstallKey == null)
                return false;

            return uninstallKey.GetSubKeyNames()
                               .Select(installedSoftwareString => uninstallKey.OpenSubKey(installedSoftwareString))
                               .Select(installedSoftwareKey => installedSoftwareKey.GetValue("DisplayName") as string)
                               .Any(installedSoftwareName => installedSoftwareName != null && installedSoftwareName.Contains(softwareName));
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(HttpServerModulePath))
            {

                Properties.Settings.Default.http_server = true;
            }
            else
            {

                Properties.Settings.Default.http_server = false;

            }

            if (IsSoftwareInstalled("Node.js") == true)
            {

                Properties.Settings.Default.node_js = true;
            }
            else
            {


                Properties.Settings.Default.node_js = false;

            }

            Properties.Settings.Default.SERVERIP = darkComboBoxServerIP.Text;
            Properties.Settings.Default.PS4IP = tbPS4IP.Text;
            Properties.Settings.Default.Save();
            this.Hide();
        }

        #region GridSetting

        private void TitleID_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.title_id == true)
            {
                TitleID.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.title_id = false;
            }
            else
            {
                Properties.Settings.Default.title_id = true;
                TitleID.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void ContentID_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.content_id == true)
            {
                ContentID.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.content_id = false;
            }
            else
            {
                Properties.Settings.Default.content_id = true;
                ContentID.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void Region_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Region == true)
            {
                Region.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.Region = false;
            }
            else
            {
                Properties.Settings.Default.Region = true;
                Region.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void SystemFirmware_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.system_firmware == true)
            {
                SystemFirmware.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.system_firmware = false;
            }
            else
            {
                Properties.Settings.Default.system_firmware = true;
                SystemFirmware.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void Version_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.version == true)
            {
                Version.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.version = false;
            }
            else
            {
                Properties.Settings.Default.version = true;
                Version.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void PKGType_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.pkg_type == true)
            {
                PKGType.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.pkg_type = false;
            }
            else
            {
                Properties.Settings.Default.pkg_type = true;
                PKGType.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void Category_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.category == true)
            {
                Category.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.category = false;
            }
            else
            {
                Properties.Settings.Default.category = true;
                Category.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void Size_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.size == true)
            {
                Size.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.size = false;
            }
            else
            {
                Properties.Settings.Default.size = true;
                Size.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }


        private void PSVR_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.psvr == true)
            {
                PSVR.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.psvr = false;
            }
            else
            {
                Properties.Settings.Default.psvr = true;
                PSVR.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void neoEnable_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.neoEnable == true)
            {
                neoEnable.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.neoEnable = false;
            }
            else
            {
                Properties.Settings.Default.neoEnable = true;
                neoEnable.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void ps5bc_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ps5bc == true)
            {
                ps5bc.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.ps5bc = false;
            }
            else
            {
                Properties.Settings.Default.ps5bc = true;
                ps5bc.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        private void Location_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.location == true)
            {
                Location.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.location = false;
            }
            else
            {
                Properties.Settings.Default.location = true;
                Location.CheckState = CheckState.Checked;
            }

            Properties.Settings.Default.Save();
        }

        #endregion GridSetting



        private void BGM_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.BGMEnable == true)
            {
                BGM.CheckState = CheckState.Unchecked;
                Properties.Settings.Default.BGMEnable = false;
            }
            else
            {
                BGM.CheckState = CheckState.Checked;
                Properties.Settings.Default.BGMEnable = true;
            }

            Properties.Settings.Default.Save();
        }

        private void darkButton2_Click_1(object sender, EventArgs e)
        {
            if (CheckForPS4Connection() == true)
            {
                DarkMessageBox.ShowInformation("PS4 detected.", "PS4 PKG Tool");
            }
            else
            {
                DarkMessageBox.ShowError("PS4 not detected.", "PS4 PKG Tool");
            }
        }

        public bool CheckForPS4Connection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = tbPS4IP.Text;
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else if (reply.Status == IPStatus.TimedOut)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void darkButtonInstaleServemodule_Click(object sender, EventArgs e)
        {
            if (IsSoftwareInstalled("Node.js") == false)
            {
                DarkMessageBox.ShowWarning("Please install Node.js before installing serve module.", "PS4 PKG Tool");
                System.Diagnostics.Process.Start("https://nodejs.org/en/download/");
                return;
            }

            this.Enabled = false;

            Process server = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C npm install http-server -g";
            server.StartInfo = startInfo;
            server.Start();
            server.WaitForExit();

            if (Directory.Exists(HttpServerModulePath))
            {
                darkLabelserveModuleInstalled.Text = "✔";
                darkLabelserveModuleInstalled.ForeColor = Color.Green;
                darkButtonInstaleServemodule.Enabled = false;
                Properties.Settings.Default.http_server = true;
            }
            else
            {
                darkLabelserveModuleInstalled.Text = "✘";
                darkLabelserveModuleInstalled.ForeColor = Color.Red;
                darkButtonInstaleServemodule.Enabled = true;
                Properties.Settings.Default.http_server = false;

            }

            this.Enabled = true;

        }

        private void darkButtonInstallNodejs_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://nodejs.org/en/download/");
        }

    }
}
