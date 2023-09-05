using Microsoft.Win32;
using PS4PKGTool.Util.Constants;
using PS4PKGTool.Util;
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
using PS4PKGTool.Utilities.Settings;
using DocumentFormat.OpenXml.Wordprocessing;
using Color = System.Drawing.Color;
using static PS4PKGTool.Utilities.PS4PKGToolHelper.Helper;
using PS4PKGTool.Utilities.PS4PKGToolHelper;
using System.Globalization;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.Text.RegularExpressions;

namespace PS4PKGTool
{
    public partial class ProgramSetting : DarkUI.Forms.DarkForm
    {
        const string TITLE = "Atomic Heart";
        const string TITLE_ID = "CUSA17266";
        const string VERSION = "1.00";
        const string APP_VERSION = "1.00";
        const string CATEGORY = "Game";
        const string CONTENT_ID = "EP4133-CUSA37321_00-ATOMICHEARTGAME0";
        const string CONTENT_ID2 = "EP4133-CUSA37321_00-ATOMICHEARTGAME0-A0100-V0116";
        const string REGION = "EU";
        const string SYSTEM_VERSION = "9.50";
        private string HttpServerModulePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\npm\node_modules\http-server";

        public bool Refresh = false;
        public ProgramSetting()
        {
            InitializeComponent();
        }

        private void btnOfficialUpdateDownloadFolder_Click(object sender, EventArgs e)
        {
            if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
            {
                tbOfficialUpdateDownloadFolder.Text = fbd.SelectedPath;
                Logger.LogInformation($"Official update PKG directory set to \"{fbd.SelectedPath}\"");
            }
        }

        private void ProgramSetting_Load(object sender, EventArgs e)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    darkComboBoxServerIP.Items.Add(ip.ToString());
                }
            }

            #region LoadSetting
            // directory settings
            darkCheckBoxRecursive.Checked = appSettings_.ScanRecursive;
            lbPkgDirectoryList.Items.AddRange(appSettings_.PkgDirectories?.Cast<string>().ToArray() ?? Array.Empty<string>());
            darkCheckBoxDontshowthisagain.Checked = appSettings_.ShowDirectorySettingsAtStartup;

            AutoSortRow.Checked = appSettings_.AutoSortRow;
            PKGColorLabeling.Checked = appSettings_.PkgColorLabel;
            darkLabelGamePkgColorLabel.ForeColor = (appSettings_.GamePkgForeColor == null) ? Color.FromArgb(220, 220, 220) : appSettings_.GamePkgForeColor;
            darkLabelPatchPkgColorLabel.ForeColor = (appSettings_.PatchPkgForeColor == null) ? Color.FromArgb(220, 220, 220) : appSettings_.PatchPkgForeColor;
            darkLabelAddonPkgColorLabel.ForeColor = (appSettings_.AddonPkgForeColor == null) ? Color.FromArgb(220, 220, 220) : appSettings_.AddonPkgForeColor;
            darkLabelAppPkgColorLabel.ForeColor = (appSettings_.AppPkgForeColor == null) ? Color.FromArgb(220, 220, 220) : appSettings_.AppPkgForeColor;
            darkLabelGamePkgColorLabel.BackColor = (appSettings_.GamePkgBackColor == null) ? Color.FromArgb(60, 63, 65) : appSettings_.GamePkgBackColor;
            darkLabelPatchPkgColorLabel.BackColor = (appSettings_.PatchPkgBackColor == null) ? Color.FromArgb(60, 63, 65) : appSettings_.PatchPkgBackColor;
            darkLabelAddonPkgColorLabel.BackColor = (appSettings_.AddonPkgBackColor == null) ? Color.FromArgb(60, 63, 65) : appSettings_.AddonPkgBackColor;
            darkLabelAppPkgColorLabel.BackColor = (appSettings_.AppPkgBackColor == null) ? Color.FromArgb(60, 63, 65) : appSettings_.AppPkgBackColor;
            tbCustomNamePattern.Text = appSettings_.RenameCustomName;

            ShowThisAtStartup.Checked = appSettings_.ShowDirectorySettingsAtStartup;
            tbOfficialUpdateDownloadFolder.Text = appSettings_.OfficialUpdateDownloadDirectory;
            tbPS4IP.Text = appSettings_.Ps4Ip;
            darkComboBoxServerIP.Text = appSettings_.LocalServerIp;
            labelPs5BcJsonDownloadDate.Text = (appSettings_.Ps5BcJsonLastDownloadDate == DateTime.MinValue || !File.Exists(Ps5BcJsonFile))
                ? ""
                : appSettings_.Ps5BcJsonLastDownloadDate.ToString("d MMMM yyyy", CultureInfo.InvariantCulture);

            cbPs5BcCheck.Checked = appSettings_.psvr_neo_ps5bc_check;
            Location.Checked = appSettings_.pkgDirectoryColumn;
            Size.Checked = appSettings_.pkgsizeColumn;
            Category.Checked = appSettings_.pkgcategoryColumn;
            PkgType.Checked = appSettings_.pkgTypeColumn;
            SystemFirmware.Checked = appSettings_.pkgminimumFirmwareColumn;
            Version.Checked = appSettings_.pkgversionColumn;
            Region.Checked = appSettings_.pkgregionColumn;
            ContentId.Checked = appSettings_.pkgcontentIdColumn;
            TitleId.Checked = appSettings_.pkgtitleIdColumn;
            cbBackported.Checked = appSettings_.pkgBackportColumn;
            BGM.Checked = appSettings_.PlayBgm;
            #endregion LoadSetting

            #region nodejs&serve
            if (Tool.IsAppInstalled("Node.js") == true)
            {
                darkLabelNodejsInstalled.Text = "✔";
                darkLabelNodejsInstalled.ForeColor = Color.Green;
                btnInstallNodejs.Enabled = false;
                appSettings_.NodeJsInstalled = true;
            }
            else
            {
                darkLabelNodejsInstalled.Text = "✘";
                darkLabelNodejsInstalled.ForeColor = Color.Red;
                btnInstallNodejs.Enabled = true;
                appSettings_.NodeJsInstalled = false;
            }

            if (Directory.Exists(HttpServerModulePath))
            {
                darkLabelserveModuleInstalled.Text = "✔";
                darkLabelserveModuleInstalled.ForeColor = Color.Green;
                btnInstalleServerModule.Enabled = false;
                appSettings_.HttpServerInstalled = true;

            }
            else
            {
                darkLabelserveModuleInstalled.Text = "✘";
                darkLabelserveModuleInstalled.ForeColor = Color.Red;
                btnInstalleServerModule.Enabled = true;
                appSettings_.HttpServerInstalled = false;
            }

            #endregion nodejs&serve

            cbPs5BcCheck.CheckedChanged += cbPs5BcCheck_CheckedChanged;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.Hide();
        }

        private void SaveSettings()
        {
            Logger.LogInformation("Saving program settings..");

            appSettings_.OfficialUpdateDownloadDirectory = tbOfficialUpdateDownloadFolder.Text;
            appSettings_.PlayBgm = BGM.Checked;
            appSettings_.ShowDirectorySettingsAtStartup = ShowThisAtStartup.Checked;
            appSettings_.AutoSortRow = AutoSortRow.Checked;
            appSettings_.PkgColorLabel =PKGColorLabeling.Checked;

            // fore color
            appSettings_.GamePkgForeColor = darkLabelGamePkgColorLabel.ForeColor;
            appSettings_.PatchPkgForeColor = darkLabelPatchPkgColorLabel.ForeColor;
            appSettings_.AddonPkgForeColor = darkLabelAddonPkgColorLabel.ForeColor;
            appSettings_.AppPkgForeColor = darkLabelAppPkgColorLabel.ForeColor;

            // back color
            appSettings_.GamePkgBackColor = darkLabelGamePkgColorLabel.BackColor;
            appSettings_.PatchPkgBackColor = darkLabelPatchPkgColorLabel.BackColor;
            appSettings_.AddonPkgBackColor = darkLabelAddonPkgColorLabel.BackColor;
            appSettings_.AppPkgBackColor = darkLabelAppPkgColorLabel.BackColor;

            appSettings_.RenameCustomName = tbCustomNamePattern.Text;
            appSettings_.pkgtitleIdColumn = TitleId.Checked;
            appSettings_.pkgcontentIdColumn = ContentId.Checked;
            appSettings_.pkgregionColumn = Region.Checked;
            appSettings_.pkgversionColumn = Version.Checked;
            appSettings_.pkgminimumFirmwareColumn = SystemFirmware.Checked;
            appSettings_.pkgTypeColumn = PkgType.Checked;
            appSettings_.pkgcategoryColumn = Category.Checked;
            appSettings_.pkgsizeColumn = Size.Checked;
            appSettings_.pkgDirectoryColumn = Location.Checked;
            appSettings_.pkgBackportColumn = cbBackported.Checked;
            appSettings_.psvr_neo_ps5bc_check = cbPs5BcCheck.Checked;

            appSettings_.LocalServerIp = darkComboBoxServerIP.Text;
            appSettings_.Ps4Ip = tbPS4IP.Text;
            appSettings_.NodeJsInstalled = Tool.IsAppInstalled("Node.js");
            appSettings_.HttpServerInstalled = (Directory.Exists(HttpServerModulePath)) ? true : false;

            var PkgDirectoryList = lbPkgDirectoryList.Items.Cast<string>().ToList();
            appSettings_.PkgDirectories = PkgDirectoryList;
            appSettings_.ScanRecursive = darkCheckBoxRecursive.Checked;
            appSettings_.ShowDirectorySettingsAtStartup = darkCheckBoxDontshowthisagain.Checked;

            if (labelPs5BcJsonDownloadDate.Text != "" || labelPs5BcJsonDownloadDate.Text.Length != 0)
                appSettings_.Ps5BcJsonLastDownloadDate = DateTime.Parse(labelPs5BcJsonDownloadDate.Text);

            SettingsManager.SaveSettings(appSettings_, SettingFilePath);
        }

        private void btnPingPs4_Click(object sender, EventArgs e)
        {
            if (tbPS4IP.Text == string.Empty)
                return;

            Logger.LogInformation("Checking PS4 connectivity..");
            bool isPS4Connected = Tool.CheckForPS4Connection(tbPS4IP.Text);

            if (isPS4Connected)
            {
                ShowInformation("PS4 detected.", true);
            }
            else
            {
                ShowError("PS4 not detected.", true);
            }
        }

        private void btnInstallServerModule_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("Installing http-server module..");
            bool nodejsInstalled = Tool.IsAppInstalled("Node.js");

            if (!nodejsInstalled)
            {
                ShowInformation("Please install Node.js before installing the serve module.", true);
                Tool.OpenWebLink("https://nodejs.org/en/download/");
                return;
            }

            this.Enabled = false;
            InstallServerModule();
            UpdateServerModuleStatus();
            this.Enabled = true;
        }

        private void InstallServerModule()
        {
            try
            {
                Process server = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    Arguments = "/C npm install http-server -g"
                };
                server.StartInfo = startInfo;

                if (server.Start())
                {
                    server.WaitForExit();
                    int exitCode = server.ExitCode;

                    if (exitCode == 0)
                    {
                        Logger.LogInformation("http-server package installed.");
                        appSettings_.HttpServerInstalled = (Directory.Exists(HttpServerModulePath)) ? true : false;
                    }
                    else
                    {
                        ShowError($"An error occurred while installing http-server. Exit code: {exitCode}. Try install http-server manually in command prompt.", true);
                    }
                }
                else
                {
                    Logger.LogError("Failed to start the process.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occurred: " + ex.Message);
            }
        }

        private void UpdateServerModuleStatus()
        {
            btnInstalleServerModule.Enabled = !appSettings_.HttpServerInstalled;
        }

        private void btnInstallNodejs_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("Installing Node.js..");
            Tool.OpenWebLink("https://nodejs.org/en/download/");
        }

        private void SetPKGLabelColor_Click(object sender, EventArgs args)
        {
            if (!(sender is Button clickedButton))
                return;


            if (clickedButton == btnResetPkgLabelColor)
            {
                darkLabelGamePkgColorLabel.ForeColor = Color.FromArgb(220, 220, 220);
                darkLabelGamePkgColorLabel.BackColor = Color.FromArgb(60, 63, 65);

                darkLabelPatchPkgColorLabel.ForeColor = Color.FromArgb(220, 220, 220);
                darkLabelPatchPkgColorLabel.BackColor = Color.FromArgb(60, 63, 65);

                darkLabelAddonPkgColorLabel.ForeColor = Color.FromArgb(220, 220, 220);
                darkLabelAddonPkgColorLabel.BackColor = Color.FromArgb(60, 63, 65);

                darkLabelAppPkgColorLabel.ForeColor = Color.FromArgb(220, 220, 220);
                darkLabelAppPkgColorLabel.BackColor = Color.FromArgb(60, 63, 65);
            }
            else
            {
                ColorDialog colorDialog = new ColorDialog();
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    string colorValue = ColorTranslator.ToHtml(selectedColor);

                    // fore
                    if (clickedButton == btnGamePkgForeColor)
                        darkLabelGamePkgColorLabel.ForeColor = selectedColor;
                    if (clickedButton == btnPatchPkgForeColor)
                        darkLabelPatchPkgColorLabel.ForeColor = selectedColor;
                    if (clickedButton == btnAddonPkgForeColor)
                        darkLabelAddonPkgColorLabel.ForeColor = selectedColor;
                    if (clickedButton == btnAppPkgForeColor)
                        darkLabelAppPkgColorLabel.ForeColor = selectedColor;

                    // back
                    if (clickedButton == btnGamePkgBackColor)
                        darkLabelGamePkgColorLabel.BackColor = selectedColor;
                    if (clickedButton == btnPatchPkgBackColor)
                        darkLabelPatchPkgColorLabel.BackColor = selectedColor;
                    if (clickedButton == btnAddonPkgBackColor)
                        darkLabelAddonPkgColorLabel.BackColor = selectedColor;
                    if (clickedButton == btnAppPkgBackColor)
                        darkLabelAppPkgColorLabel.BackColor = selectedColor;

                    Logger.LogInformation($"Selected {colorValue}.");
                }
            }
        }

        private async void btnDownloadPS5BCJson_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("Downloading PS5 Backward Compatibility json from github..");

            if (!Tool.CheckForInternetConnection("github.com"))
            {
                ShowError("Problem occured when try connecting to Github", true);
                return;
            }
            try
            {
                ShowTaskbarNotification("PS5 Backward Compatibility Status", "PS5 Backward Compatibility Status is being downloaded..");
                await Tool.DownloadFileFromUrlAsync("https://raw.githubusercontent.com/andshrew/supreme-enigma/master/docs/PS5-BC-Status.json", Ps5BcJsonFile);
                ShowInformation("PS5 Backward Compatibility Status file downloaded to PS4PKGToolTemp", true);
                appSettings_.Ps5BcJsonLastDownloadDate = DateTime.Now;
                labelPs5BcJsonDownloadDate.Text = appSettings_.Ps5BcJsonLastDownloadDate.ToString("d MMMM yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                ShowError("Failed to download PS5 Backward Compatibility Status file : " + ex.Message, true);
            }
        }

        private static void ShowTaskbarNotification(string title, string text)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information; // You can set your own icon here
            notifyIcon.Visible = true;
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.ShowBalloonTip(3000); // Display the balloon tip for 3 seconds

            // Handle click event if needed
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;

            // Clean up when done
            notifyIcon.Dispose();
        }

        private static void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            // Handle the balloon tip click event
        }

        private void cbPs5BcCheck_CheckedChanged(object sender, EventArgs e)
        {
            var ps5BcFile = File.Exists(Ps5BcJsonFile);
            if (cbPs5BcCheck.Checked && !ps5BcFile)
            {
                ShowWarning("Download PS5 Backward Compatibility Status json to use this feature", false);
                cbPs5BcCheck.Checked = false;
            }
            else if (cbPs5BcCheck.Checked)
            {
                Refresh = true;
            }
        }

        private void btnAddNamingPlaceholder_Click(object sender, EventArgs e)
        {
            if (darkComboBoxNamingPlaceholder.Text == string.Empty)
                return;

            // Get the current cursor position
            int cursorPosition = tbCustomNamePattern.SelectionStart;

            // Get the text before and after the cursor position
            string textBeforeCursor = tbCustomNamePattern.Text.Substring(0, cursorPosition);
            string textAfterCursor = tbCustomNamePattern.Text.Substring(cursorPosition);

            // Insert text
            string textToInsert = $"{darkComboBoxNamingPlaceholder.Text}";
            tbCustomNamePattern.Text = textBeforeCursor + textToInsert + textAfterCursor;
        }

        private void tbCustomNamePattern_TextChanged(object sender, EventArgs e)
        {
            darkLabelNamingPatternExample.Text = tbCustomNamePattern.Text
                    .Replace("{TITLE}", TITLE)
                    .Replace("{TITLE_ID}", TITLE_ID)
                    .Replace("{VERSION}", VERSION)
                    .Replace("{APP_VERSION}", APP_VERSION)
                    .Replace("{CATEGORY}", CATEGORY)
                    .Replace("{CONTENT_ID}", CONTENT_ID)
                    .Replace("{CONTENT_ID2}", CONTENT_ID2)
                    .Replace("{REGION}", REGION)
                    .Replace("{SYSTEM_VERSION}", SYSTEM_VERSION);
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            tbCustomNamePattern.Text = "";
        }

        private void btnClearAllPkgDirectory_Click(object sender, EventArgs e)
        {
            lbPkgDirectoryList.Items.Clear();
        }

        private void btnDeletePkgDirectory_Click(object sender, EventArgs e)
        {
            if (lbPkgDirectoryList.SelectedItems.Count > 0)
            {
                int selectedIndex = lbPkgDirectoryList.SelectedIndex;
                lbPkgDirectoryList.Items.RemoveAt(selectedIndex);
            }
        }

        private void btnAddPkgDirectory_Click(object sender, EventArgs e)
        {
            if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
            {
                string selectedFolder = fbd.SelectedPath;

                if (lbPkgDirectoryList.Items.Contains(selectedFolder))
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

                lbPkgDirectoryList.Items.Add(selectedFolder);
            }
        }
    }
}
