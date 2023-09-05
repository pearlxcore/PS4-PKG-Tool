using ByteSizeLib;
using ClosedXML.Excel;
using DarkUI.Config;
using DarkUI.Controls;
using DarkUI.Forms;
using GitHubUpdate;
using Irony;
using Newtonsoft.Json;
using PS4_Tools.LibOrbis.PKG;
using PS4_Tools.LibOrbis.Util;
using PS4_Trophy_xdpx;
using PS4PKGTool.Util;
using PS4PKGTool.Util.Constants;
using PS4PKGTool.Utilities.PS4PKGToolHelper;
using PS4PKGTool.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRPViewer;
using static PS4_Tools.PKG.SceneRelated;
using static PS4PKGTool.Utilities.PS4PKGToolHelper.Helper;
using static PS4PKGTool.Utilities.PS4PKGToolHelper.Helper.Backport;
using static PS4PKGTool.Utilities.PS4PKGToolHelper.Helper.Entry;
using static PS4PKGTool.Utilities.PS4PKGToolHelper.Helper.TreeView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Bitmap = System.Drawing.Bitmap;
using Entry = PS4PKGTool.Utilities.PS4PKGToolHelper.Helper.Entry;
using ListView = System.Windows.Forms.ListView;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using TreeView = PS4PKGTool.Utilities.PS4PKGToolHelper.Helper.TreeView;

namespace PS4PKGTool
{
    public partial class Main : DarkUI.Forms.DarkForm
    {
        private MemoryMappedFile pkgFile;
        private dynamic send_pkg_json;
        private string TEMPFILENAMESENDPKG;
        private bool runworker = false;
        private bool renameBackFile;
        internal static string filenameDLC;
        private static string ApplicationVersion { get; set; }
        private readonly List<string> ExcludedDirectoryList = new List<string>() { "System Volume Information", "$RECYCLE.BIN", "$Recycle.Bin" };

        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public Form form_;
        private byte[] old_byte;

        public static string GetApplicationVersion()
        {
            // Get the entry assembly (usually represents the current application)
            Assembly entryAssembly = Assembly.GetEntryAssembly();

            // Get the custom attribute for the AssemblyInformationalVersionAttribute
            // This attribute should be set in the project's Properties/AssemblyInfo.cs file
            AssemblyInformationalVersionAttribute versionAttribute =
                entryAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            // Retrieve and return the version string
            return versionAttribute?.InformationalVersion ?? "Version information not available";
        }

        public Main()
        {
            InitializeComponent();
            this.tabPage6.Hide();
            flatTabControl1.TabPages.Remove(tabPage6);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; CheckForIllegalCrossThreadCalls = false;
            PKGGridView.ScrollBars = ScrollBars.Vertical;
            darkDataGridView2.ScrollBars = ScrollBars.Vertical;
            TrophyGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            TrophyGridView.ScrollBars = ScrollBars.Vertical;

            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.ActiveControl = null;  //this = form
            toolStripProgressBar1.MarqueeAnimationSpeed = 30;

            ApplicationVersion = GetApplicationVersion();

            ListViewDraw.colorListViewHeader(ref listView1, Color.FromArgb(57, 60, 62), Color.FromArgb(220, 220, 220));
            foreach (ColumnHeader column in listView1.Columns)
            {
                column.Width = listView1.Width / listView1.Columns.Count;
            }
        }

        private void LogToTextBox(string logMessage)
        {
            //if (tbLog.InvokeRequired)
            //{
            //    // If the call is not on the UI thread, invoke it on the UI thread
            //    Invoke(new Action<string>(LogToTextBox), logMessage);
            //}
            //else
            //{
            //    // Append the log message to the TextBox
            //    tbLog.AppendText(logMessage + Environment.NewLine);
            //}
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Tool.KillNodeJS();
            //try
            //{
            //    if (Directory.Exists(WorkingDirectory))
            //    {
            //        Directory.Delete(WorkingDirectory, recursive: true);
            //    }
            //}
            //catch { }
            SettingsManager.SaveSettings(appSettings_, SettingFilePath);
            Application.Exit();
        }

        private static bool IsIgnorable(string dir)
        {
            string[] ignorableFolders = { "System Volume Information", "$RECYCLE.BIN", "$Recycle.Bin" };
            return ignorableFolders.Any(folder => dir.EndsWith(folder));
        }

        private void PKGListGridView_SelectionChanged(object sender, EventArgs e)
        {
            BGM.isBGMPlaying = false;
            BGM.At9Player.Stop();

            PKG.SelectedPKGFilename = "";

            if (PKGGridView.SelectedCells.Count > 0)
            {
                GetSelectedPKGPath();

                if (PKG.isDeletingPkg)
                    SelectFirstRowPkg();

                LoadPKGDetails();
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                WindowState = FormWindowState.Maximized;
                this.Text = "PS4 PKG Tool " + ApplicationVersion;
                Logger.LogInformation("Selected directory: ");

                foreach (var folder in appSettings_.PkgDirectories)
                {
                    Logger.LogInformation(folder);
                }

                Logger.LogInformation("Checking Node.js and server module...");

                bool isNodeJsInstalled = NodeJsHttpServer.IsSoftwareInstalled("Node.js");
                appSettings_.NodeJsInstalled = isNodeJsInstalled;
                Logger.LogInformation(isNodeJsInstalled ? "Node.js installed." : "Node.js not installed.");

                bool isHttpServerModuleInstalled = Directory.Exists(NodeJsHttpServer.HttpServerModulePath);
                appSettings_.HttpServerInstalled = isHttpServerModuleInstalled;
                Logger.LogInformation(isHttpServerModuleInstalled ? "Module installed." : "Module not installed.");

                this.Invoke((MethodInvoker)delegate
                {
                    this.Enabled = false;

                    // If BGM playing, stop it
                    while (BGM.isBGMPlaying)
                    {
                        BGM.isBGMPlaying = false;
                        BGM.At9Player.Stop();
                    }

                    // Disable selection changed event
                    //PKGListGridView.SelectionChanged -= PKGListGridView_SelectionChanged;

                    // Update UI
                    PKGGridView.Enabled = false; // Disable DataGridView during listing pkg
                    darkDataGridView2.Enabled = false; // Disable DataGridView during listing pkg

                    Logger.LogInformation("Scanning PKG...");

                    LoadPKGGridView();
                    //LoadPKGListView();
                });
            });
        }

        private void LoadPKGDetails()
        {
            if (!File.Exists(PKG.SelectedPKGFilename))
            {
                SelectFirstRowPkg();
            }

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG ps4Pkg = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
            UpdateFormTitle(ps4Pkg.PS4_Title);
            PKG.CurrentPKGTitle = ps4Pkg.PS4_Title;
            PKG.CurrentPKGType = ps4Pkg.PKG_Type.ToString();

            // update group action/title toolstripmenuitem
            GroupActionTitleStripMenuItem.Text = (PKGGridView.SelectedRows.Count > 1) ? "Group Action" : ps4Pkg.PS4_Title;
            toolStripMenuItem2.Text = (PKGGridView.SelectedRows.Count > 1) ? "Group Action" : ps4Pkg.PS4_Title;

            //viewPatchChangelogtoolStripMenuItem1.Enabled = PKG.CurrentPKGType == PKGCategory.PATCH;
            //viewPatchChangelogtoolStripMenuItem2.Enabled = PKG.CurrentPKGType == PKGCategory.GAME;

            RpiUninstallBasePKGToolStripMenuItem1.Enabled = true;
            RpiUninstallBasePKGToolStripMenuItem2.Enabled = true;

            RpiUninstallPatchPKGToolStripMenuItem1.Enabled = true;
            RpiUninstallPatchPKGToolStripMenuItem2.Enabled = true;

            RpiUninstallDlcPKGToolStripMenuItem1.Enabled = true;
            RpiUninstallDlcPKGToolStripMenuItem2.Enabled = true;

            RpiUninstallThemePKGToolStripMenuItem1.Enabled = true;
            RpiUninstallThemePKGToolStripMenuItem2.Enabled = true;

            if (ps4Pkg.PKG_Type.ToString() == PKGCategory.GAME)
            {
                RpiUninstallDlcPKGToolStripMenuItem1.Enabled = false;
                RpiUninstallDlcPKGToolStripMenuItem2.Enabled = false;

                RpiUninstallThemePKGToolStripMenuItem1.Enabled = false;
                RpiUninstallThemePKGToolStripMenuItem2.Enabled = false;

                RpiUninstallBasePKGToolStripMenuItem1.Enabled = true;
                RpiUninstallBasePKGToolStripMenuItem2.Enabled = true;

                RpiUninstallPatchPKGToolStripMenuItem1.Enabled = true;
                RpiUninstallPatchPKGToolStripMenuItem2.Enabled = true;
            }
            else if (ps4Pkg.PKG_Type.ToString() == PKGCategory.PATCH)
            {
                RpiUninstallDlcPKGToolStripMenuItem1.Enabled = false;
                RpiUninstallDlcPKGToolStripMenuItem2.Enabled = false;

                RpiUninstallThemePKGToolStripMenuItem1.Enabled = false;
                RpiUninstallThemePKGToolStripMenuItem2.Enabled = false;
            }
            else if (ps4Pkg.PKG_Type.ToString() == PKGCategory.ADDON)
            {
                RpiUninstallBasePKGToolStripMenuItem1.Enabled = false;
                RpiUninstallBasePKGToolStripMenuItem2.Enabled = false;

                RpiUninstallPatchPKGToolStripMenuItem1.Enabled = false;
                RpiUninstallPatchPKGToolStripMenuItem2.Enabled = false;

                RpiUninstallThemePKGToolStripMenuItem1.Enabled = true;
                RpiUninstallThemePKGToolStripMenuItem2.Enabled = true;

                RpiUninstallDlcPKGToolStripMenuItem1.Enabled = true;
                RpiUninstallDlcPKGToolStripMenuItem2.Enabled = true;
            }

            ShowPackageIcon(ps4Pkg);

            if (appSettings_.PlayBgm)
            {
                PlayBGM(PKG.SelectedPKGFilename);
            }

            UpdateParamInfoGrid(ps4Pkg);


            LoadBackgroundImages(ps4Pkg);

            LoadTrophyInfo(ps4Pkg);
            //LoadTrophies(ps4Pkg);

            LoadHeaderInfo(ps4Pkg);

            LoadPKGEntries(ps4Pkg);

            LoadPubToolInfo(ps4Pkg);

            listView1.Items.Clear();
            PKGTreeView.Nodes.Clear();

            GetOfficialUpdate(ps4Pkg);
        }

        private void UpdateFormTitle(string pkgTitle)
        {
            this.Text = "PS4 PKG Tool " + ApplicationVersion + " - Viewing \"" + pkgTitle + "\"";
        }

        private void GetOfficialUpdate(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        {
            if (pkg.PKG_Type.ToString() == PKGCategory.GAME || pkg.PKG_Type.ToString() == PKGCategory.PATCH)
            {
                var bg = new BackgroundWorker();
                bg.DoWork += delegate
                {
                    try
                    {
                        PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);

                        DataTable DT = (DataTable)dgvUpdate.DataSource;
                        DT?.Clear();

                        if (Tool.CheckForInternetConnection())
                        {
                            Logger.LogInformation("Checking official update for " + PS4_PKG.PS4_Title + "..");
                            var item = PS4_Tools.PKG.Official.CheckForUpdate(PS4_PKG.Param.TITLEID);

                            if (item != null && item.Tag.Package.Manifest_url != null)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    DataTable dt = new DataTable();
                                    dt.Columns.Add("Part");
                                    dt.Columns.Add("File Size");
                                    dt.Columns.Add("Hash");
                                    dt.Columns.Add("URL");

                                    int ver = Convert.ToInt32(item.Tag.Package.System_ver);
                                    string hexOutput = $"{ver:X}";
                                    string firstThree = hexOutput.Substring(0, 3);
                                    string version = firstThree.Insert(1, ".");

                                    long sizes = Convert.ToInt64(item.Tag.Package.Size);
                                    var size_final = ByteSize.FromBytes(sizes).ToString();

                                    labelSystemReq.Text = version;
                                    labelUpdateVersion.Text = item.Tag.Package.Version;
                                    labelTotalFile.Text = item.Tag.Package.Manifest_item.pieces.Count.ToString();
                                    labelTotalSize.Text = size_final;
                                    labelUpdateType.Text = ToTitleCase(item.Tag.Package.Type);
                                    labelRemaster.Text = ToTitleCase(item.Tag.Package.Remaster);
                                    labelPKGdigest.Text = item.Tag.Package.Digest;
                                    labelMandatory.Text = ToTitleCase(item.Tag.Mandatory);

                                    int part = 0;
                                    foreach (var manifestItem in item.Tag.Package.Manifest_item.pieces)
                                    {
                                        part++;
                                        long fileSize = manifestItem.fileSize;
                                        string hashValue = manifestItem.hashValue.ToString();
                                        string url = manifestItem.url.ToString();
                                        var size = ByteSize.FromBytes(fileSize);

                                        dt.Rows.Add("Part " + part, size, hashValue.ToUpper(), url);
                                    }

                                    dgvUpdate.DataSource = dt;

                                    foreach (DataGridViewColumn column in dgvUpdate.Columns)
                                    {
                                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    }
                                });
                            }
                            else
                            {
                                Logger.LogInformation("\"" + PS4_PKG.PS4_Title + "\" has no update.");
                            }
                        }
                        else
                        {
                            Logger.LogError("Network is not available.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("An error occurred while checking for updates: " + ex.Message);
                    }
                };
                bg.RunWorkerAsync();
            }
        }

        private void LoadPubToolInfo(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        {
            try
            {
                List<string> array = pkg.Param.Tables
                    .Where(item => item.Name == "PUBTOOLINFO")
                    .Select(item => item.Value)
                    .FirstOrDefault()
                    ?.Split(',')
                    .Reverse()
                    .ToList();

                List<string> value = array?.Select(item => item.Substring(item.LastIndexOf('=') + 1)).ToList();
                List<string> type = array?.Select(items => items.Split('=')[0]).ToList();

                DataTable dtPubtool = new DataTable();
                foreach (var tv in type)
                {
                    dtPubtool.Columns.Add(tv.Replace("c_date", "Creation Date")
                        .Replace("sdk_ver", "PS4 SDK Version")
                        .Replace("st_type", "Storage Type")
                        .Replace("c_time", "Creation Time"));
                }

                var row = dtPubtool.NewRow();

                for (int i = 0; i < value?.Count; i++)
                {
                    row[i] = value[i];
                }
                dtPubtool.Rows.Add(row);

                darkDataGridView4.DataSource = dtPubtool;
            }
            catch { }
        }

        private void LoadHeaderInfo(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        {
            try
            {
                List<string> type = pkg.Header.DisplayType().ToList();
                List<string> value = pkg.Header.DisplayValue().ToList();

                DataTable dtHeader = new DataTable();
                dtHeader.Columns.Add("Type");
                dtHeader.Columns.Add("Value");

                var typeAndValue = type.Zip(value, (t, v) => new { Type = t, Value = v });
                foreach (var tv in typeAndValue)
                {
                    dtHeader.Rows.Add(tv.Type, tv.Value);
                }

                dgvHeader.DataSource = dtHeader;
            }
            catch { }
        }

        private void LoadPKGEntries(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        {
            dgvEntryList.DataSource = null;
            dgvEntryList.Rows.Clear();
            dgvEntryList.Refresh();
            dgvEntryList.ScrollBars = ScrollBars.Vertical;

            try
            {
                using (var file = File.OpenRead(PKG.SelectedPKGFilename))
                {
                    var pkgReader = new PkgReader(file);
                    var pkgData = pkgReader.ReadPkg();
                    var dt = new DataTable();
                    var i = 0;
                    Entry.EntryIdNameDictionary.Clear();
                    Entry.EncryptedEntryOffsetNameDictionary.Clear();
                    var entryId = "";
                    var entryName = "";

                    dt.Columns.Add("Name");
                    dt.Columns.Add("Offset");
                    dt.Columns.Add("Size");
                    dt.Columns.Add("Flags 1");
                    dt.Columns.Add("Flags 2");
                    dt.Columns.Add("Encrypted?");

                    foreach (var meta in pkgData.Metas.Metas)
                    {
                        entryId = $"{i++,-6}";
                        entryName = meta.id.ToString();
                        EntryIdNameDictionary.Add(entryId, entryName);
                        if (meta.Encrypted)
                        {
                            EncryptedEntryOffsetNameDictionary.Add($"0x{meta.DataOffset:X8}", $"{meta.id}");
                        }
                    }

                    int decValue;
                    i = 0;

                    foreach (var meta in pkgData.Metas.Metas)
                    {
                        decValue = int.Parse($"{meta.DataSize:X2}", System.Globalization.NumberStyles.HexNumber);
                        var finalSize = ByteSizeLib.ByteSize.FromBytes(decValue);
                        dt.Rows.Add($"{meta.id}", $"0x{meta.DataOffset:X}", finalSize, $"0x{meta.Flags1:X}", $"0x{meta.Flags2:X}", $"{meta.Encrypted:X}");
                    }

                    dgvEntryList.DataSource = dt;
                }

                foreach (DataGridViewColumn column in dgvEntryList.Columns)
                {
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to load PKG entries : {ex.Message}.");
            }
        }

        private void LoadBackgroundImages(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        {
            try
            {
                pbPIC0.Image = null;
                pbPIC1.Image = null;
                pbPIC0.Refresh();
                pbPIC1.Refresh();

                if (pkg.PKG_Type.ToString() == PKGCategory.GAME || pkg.PKG_Type.ToString() == PKGCategory.PATCH)
                {
                    if (pkg.Image != null)
                    {
                        pbPIC0.Click += pictureBox_click;
                        pbPIC0.Visible = true;
                        darkLabel3.Visible = false;
                        pbPIC0.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbPIC0.Image = Helper.Bitmap.BytesToBitmap(pkg.Image);
                        Helper.Bitmap.pic0.Image = pbPIC0.Image;
                    }
                    else
                    {
                        darkLabel3.Visible = true;
                        pbPIC0.Click -= pictureBox_click;
                        pbPIC0.Visible = false;
                        pbPIC0.Image = null;
                    }

                    if (pkg.Image2 != null)
                    {
                        if (old_byte == pkg.Image2)
                        {
                            darkLabel4.Visible = true;
                            pbPIC1.Click -= pictureBox_click;
                            pbPIC1.Visible = false;
                            pbPIC1.Image = null;
                        }
                        else
                        {
                            old_byte = pkg.Image2;
                            pbPIC1.Click += pictureBox_click;
                            pbPIC1.Visible = true;
                            darkLabel4.Visible = false;
                            pbPIC1.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbPIC1.Image = Helper.Bitmap.BytesToBitmap(pkg.Image2);
                            Helper.Bitmap.pic1.Image = pbPIC1.Image;
                            pkg.Image2 = null;
                        }
                    }
                    else
                    {
                        darkLabel4.Visible = true;
                        pbPIC1.Click -= pictureBox_click;
                        pbPIC1.Visible = false;
                        pbPIC1.Image = null;
                    }
                }
            }
            catch { }
        }

        private void ExtractTrophyFile(List<string> pkgList)
        {
            if (pkgList.Count > 0)
            {
                Logger.LogInformation("Extracting trophy file..");

                foreach (var pkg in pkgList)
                {
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG ps4Pkg = PS4_Tools.PKG.SceneRelated.Read_PKG(pkg);
                    var checkTrophyFile = Path.Combine(Trophy.TrophyTempFolder, ps4Pkg.Content_ID + "_" + ps4Pkg.PKG_Type.ToString() + ".TRP");
                    if (File.Exists(checkTrophyFile))
                        break;
                    BackgroundWorker bgwTrophy = new BackgroundWorker();
                    bgwTrophy.WorkerSupportsCancellation = true;
                    bgwTrophy.DoWork += (s, e) =>
                    {
                        try
                        {

                            if (ps4Pkg.Trophy_File != null)
                            {

                                List<string> idEntryList = new List<string>();
                                List<string> nameEntryList = new List<string>();

                                using (var file = File.OpenRead(pkg))
                                {
                                    var pkgReader = new PkgReader(file);
                                    var pkgData = pkgReader.ReadPkg();
                                    var i = 0;

                                    foreach (var meta in pkgData.Metas.Metas)
                                    {
                                        idEntryList.Add($"{i++,-6}");
                                        nameEntryList.Add($"{meta.id}");
                                    }

                                    idEntryList.ToArray();
                                    nameEntryList.ToArray();
                                }

                                string pkgTitleId = ps4Pkg.PS4_Title;
                                string pkgContentId = ps4Pkg.Content_ID;
                                string pkgType = ps4Pkg.PKG_Type.ToString();
                                string trophyOutputPath = "";

                                Tool.CreateDirectoryIfNotExists(Trophy.TrophyTempFolder);

                                var numbersAndWords = idEntryList.Zip(nameEntryList, (n, w) => new { id = n, name = w });
                                foreach (var nw in numbersAndWords)
                                {
                                    if (nw.name == "TROPHY__TROPHY00_TRP")
                                    {
                                        var idx = int.Parse(nw.id);
                                        var name = nw.name;
                                        trophyOutputPath = Path.Combine(Trophy.TrophyTempFolder, pkgContentId + "_" + pkgType + name.Replace("TROPHY__TROPHY00", "").Replace("_SHA", ".SHA").Replace("_DAT", ".DAT").Replace("_SFO", ".SFO").Replace("_XML", ".XML").Replace("_SIG", ".SIG").Replace("_PNG", ".PNG").Replace("_JSON", ".JSON").Replace("_DDS", ".DDS").Replace("_TRP", ".TRP").Replace("_AT9", ".AT9"));

                                        using (var pkgFile = File.OpenRead(pkg))
                                        {
                                            var pkgReader = new PkgReader(pkgFile);
                                            var pkgData = pkgReader.ReadPkg();
                                            if (idx < 0 || idx >= pkgData.Metas.Metas.Count)
                                            {
                                                return;
                                            }
                                            using (var outFile = File.Create(trophyOutputPath))
                                            {
                                                var meta = pkgData.Metas.Metas[idx];
                                                outFile.SetLength(meta.DataSize);
                                                if (meta.Encrypted)
                                                {
                                                    // Decrypt encrypted bytes if needed
                                                }
                                                new SubStream(pkgFile, meta.DataOffset, meta.DataSize).CopyTo(outFile);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        catch { }
                    };
                    bgwTrophy.RunWorkerCompleted += (s, e) =>
                    {

                    };
                    bgwTrophy.RunWorkerAsync();

                }

            }

        }

        private void LoadTrophyInfo(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        {
            try
            {
                TrophyGridView.DataSource = null;
                TrophyGridView.Rows.Clear();

                DataTable trophyDataTable = new DataTable();

                // Add columns to the DataTable if needed
                trophyDataTable.Columns.Add("Image", typeof(Image));  // Example column, adjust as needed
                trophyDataTable.Columns.Add("Name", typeof(string));
                trophyDataTable.Columns.Add("Size", typeof(string));
                trophyDataTable.Columns.Add("Offset", typeof(string));

                if (pkg.Trophy_File != null)
                {

                    try
                    {
                        Logger.LogInformation("Loading trophies for " + pkg.PS4_Title + "..");

                        string trophyFile = Trophy.TrophyTempFolder + pkg.Content_ID + "_" + pkg.PKG_Type + ".TRP";
                        if (File.Exists(trophyFile))
                        {
                            Trophy.trophy = new TRPReader();
                            Trophy.trophy.Load(trophyFile);

                            if (!Trophy.trophy.IsError)
                            {
                                foreach (var current in Trophy.trophy.TrophyList)
                                {
                                    if (current.Name.ToUpper().EndsWith(".PNG"))
                                    {
                                        Trophy.ImageToExtractList.Add(Helper.Bitmap.BytesToImage(Trophy.trophy.ExtractFileToMemory(current.Name)));
                                        Trophy.TrophyFilenameToExtractList.Add(current.Name);

                                        var imageBytes = Trophy.trophy.ExtractFileToMemory(current.Name);
                                        Image image = Helper.Bitmap.BytesToImage(imageBytes);
                                        Image resize = Trophy.ResizeImage(image, image.Width / 2, image.Height / 2);

                                        trophyDataTable.Rows.Add(resize, current.Name, RoundBytes(current.Size), "0x" + current.Offset);
                                    }
                                    Application.DoEvents();
                                }

                                TrophyGridView.DataSource = trophyDataTable;
                                TrophyGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                TrophyGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                TrophyGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                TrophyGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                TrophyGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                TrophyGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                TrophyGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                        }
                    }
                    catch { }

                }
                else
                {
                    Logger.LogError(pkg.PS4_Title + " has no trophy.");
                }
            }
            catch { }

        }

        //private void LoadTrophies(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        //{
        //    try
        //    {
        //        TrophyGridView.DataSource = null;
        //        TrophyGridView.Rows.Clear();

        //        DataTable trophyDataTable = new DataTable();

        //        // Add columns to the DataTable if needed
        //        trophyDataTable.Columns.Add("Image", typeof(Image));  // Example column, adjust as needed
        //        trophyDataTable.Columns.Add("Name", typeof(string));
        //        trophyDataTable.Columns.Add("Size", typeof(string));
        //        trophyDataTable.Columns.Add("Offset", typeof(string));

        //        if (pkg.Trophy_File != null)
        //        {
        //            BackgroundWorker bgwTrophy = new BackgroundWorker();
        //            bgwTrophy.WorkerSupportsCancellation = true;
        //            bgwTrophy.DoWork += (s, e) =>
        //            {
        //                try
        //                {
        //                    Logger.LogError("Loading trophies for " + pkg.PS4_Title + "..");

        //                    List<string> idEntryList = new List<string>();
        //                    List<string> nameEntryList = new List<string>();

        //                    using (var file = File.OpenRead(PKG.SelectedPKGFilename))
        //                    {
        //                        var pkgReader = new PkgReader(file);
        //                        var pkgData = pkgReader.ReadPkg();
        //                        var i = 0;

        //                        foreach (var meta in pkgData.Metas.Metas)
        //                        {
        //                            idEntryList.Add($"{i++,-6}");
        //                            nameEntryList.Add($"{meta.id}");
        //                        }

        //                        idEntryList.ToArray();
        //                        nameEntryList.ToArray();
        //                    }

        //                    string path = Trophy.TrophyTempFolder;
        //                    Directory.CreateDirectory(path);

        //                    var numbersAndWords = idEntryList.Zip(nameEntryList, (n, w) => new { id = n, name = w });
        //                    foreach (var nw in numbersAndWords)
        //                    {
        //                        if (nw.name == "TROPHY__TROPHY00_TRP")
        //                        {
        //                            var pkgPath = PKG.SelectedPKGFilename;
        //                            var idx = int.Parse(nw.id);
        //                            var name = nw.name;
        //                            Trophy.outPath = Path.Combine(path, name.Replace("_SHA", ".SHA").Replace("_DAT", ".DAT").Replace("_SFO", ".SFO").Replace("_XML", ".XML").Replace("_SIG", ".SIG").Replace("_PNG", ".PNG").Replace("_JSON", ".JSON").Replace("_DDS", ".DDS").Replace("_TRP", ".TRP").Replace("_AT9", ".AT9"));

        //                            using (var pkgFile = File.OpenRead(pkgPath))
        //                            {
        //                                var pkgReader = new PkgReader(pkgFile);
        //                                var pkgData = pkgReader.ReadPkg();
        //                                if (idx < 0 || idx >= pkgData.Metas.Metas.Count)
        //                                {
        //                                    return;
        //                                }
        //                                using (var outFile = File.Create(Trophy.outPath))
        //                                {
        //                                    var meta = pkgData.Metas.Metas[idx];
        //                                    outFile.SetLength(meta.DataSize);
        //                                    if (meta.Encrypted)
        //                                    {
        //                                        // Decrypt encrypted bytes if needed
        //                                    }
        //                                    new SubStream(pkgFile, meta.DataOffset, meta.DataSize).CopyTo(outFile);
        //                                }
        //                            }
        //                        }
        //                    }



        //                    if (File.Exists(Trophy.outPath))
        //                    {
        //                        Trophy.trophy = new TRPReader();
        //                        Trophy.trophy.Load(Trophy.outPath);

        //                        if (!Trophy.trophy.IsError)
        //                        {


        //                            foreach (var current in Trophy.trophy.TrophyList)
        //                            {
        //                                if (current.Name.ToUpper().EndsWith(".PNG"))
        //                                {
        //                                    var imageBytes = Trophy.trophy.ExtractFileToMemory(current.Name);
        //                                    Image image = Helper.Bitmap.BytesToImage(imageBytes);
        //                                    Image resize = Trophy.ResizeImage(image, image.Width / 2, image.Height / 2);

        //                                    trophyDataTable.Rows.Add(resize, current.Name, RoundBytes(current.Size), "0x" + current.Offset);
        //                                }
        //                                Application.DoEvents();
        //                            }

        //                            TrophyGridView.DataSource = trophyDataTable;
        //                            TrophyGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                            TrophyGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                            TrophyGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                            TrophyGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        //                            TrophyGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                            TrophyGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                            TrophyGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                        }
        //                    }
        //                }
        //                catch { }
        //            };
        //            bgwTrophy.RunWorkerCompleted += (s, e) =>
        //            {

        //            };
        //            bgwTrophy.RunWorkerAsync();
        //        }
        //        else
        //        {
        //            Logger.LogError(pkg.PS4_Title + " has no trophy.");
        //        }
        //    }
        //    catch { }
        //}


        private void PlayBGM(string selectedPkgFilename)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += (s, e) =>
            {
                try
                {
                    BGM.PlayAt9(selectedPkgFilename);
                }
                catch { }
            };
            bgw.RunWorkerCompleted += (s, e) =>
            {

            };
            bgw.RunWorkerAsync();
        }

        private void ShowPackageIcon(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        {
            darkLabel1.Text = "";

            if (pkg.Icon != null)
            {
                pictureBox1.Visible = true;
                label3.Text = "";
                pictureBox1.Image = Helper.Bitmap.BytesToBitmap(pkg.Icon);
            }
            else
            {
                pictureBox1.Visible = false;
                label3.Visible = true;
                label3.Text = "Image not available";
            }

            darkLabel1.Text = pkg.PS4_Title;
        }

        private void UpdateParamInfoGrid(PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkg)
        {
            DataTable dg2 = new DataTable();
            dg2.Columns.Add("PARAM");
            dg2.Columns.Add("VALUE");

            for (int i = 0; i < pkg.Param.Tables.Count; i++)
            {
                dg2.Rows.Add(pkg.Param.Tables[i].Name, pkg.Param.Tables[i].Value);
            }

            darkDataGridView2.DataSource = dg2;
            darkDataGridView2.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void pictureBox_click(object sender, EventArgs e)
        {
            if (!(sender is PictureBox pictureBox)) return;

            if (pictureBox == null)
                return;

            contextMenuBackgroundImage.Show(pictureBox, pictureBox.PointToClient(Cursor.Position));
        }

        /// <summary>
        /// Return PKG directory of selected/all PKG from gridview
        /// </summary>
        /// <param name="selectionType"></param>
        /// <returns></returns>
        private List<string> GetSelectedPKGDirectoryList(string selectionType, bool sortAddon = false)
        {
            List<string> list = new List<string>();

            IEnumerable<DataGridViewRow> rowsToProcess = null;

            if (selectionType == PKGSelectionType.SELECTED)
            {
                rowsToProcess = PKGGridView.SelectedRows.Cast<DataGridViewRow>();
            }
            else if (selectionType == PKGSelectionType.ALL)
            {
                rowsToProcess = PKGGridView.Rows.Cast<DataGridViewRow>();
            }

            if (sortAddon)
            {
                rowsToProcess = rowsToProcess.OrderByDescending(row => row.Cells[7].Value); // Sort by column index 7
            }

            foreach (DataGridViewRow row in rowsToProcess)
            {
                string filename = row.Cells[0].Value.ToString();
                string path = row.Cells[12].Value.ToString();
                string pkgPath = Path.Combine(path, filename);
                list.Add(pkgPath);
            }

            return list;
        }

        private void ImageIconExtractor(string imageType, List<string> pkgFilesList, string outputDirectory, bool respectiveExtract)
        {
            int countPkg = 0;
            foreach (string pkgPath in pkgFilesList)
            {
                try
                {
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(pkgPath);
                    string finalPath = respectiveExtract
    ? $"{outputDirectory}\\{PS4_PKG.PS4_Title} ({PS4_PKG.PKG_Type})\\"
    : outputDirectory;

                    Directory.CreateDirectory(finalPath);

                    switch (imageType)
                    {
                        case ImageIconExtractionType.ALL:
                            ExtractImage(PS4_PKG.Image, finalPath, "PIC0", PS4_PKG, respectiveExtract);
                            ExtractImage(PS4_PKG.Image2, finalPath, "PIC1", PS4_PKG, respectiveExtract);
                            ExtractImage(PS4_PKG.Icon, finalPath, "ICON", PS4_PKG, respectiveExtract);
                            break;

                        case ImageIconExtractionType.ICON:
                            ExtractImage(PS4_PKG.Icon, finalPath, "ICON", PS4_PKG, respectiveExtract);
                            break;

                        case ImageIconExtractionType.IMAGE:
                            ExtractImage(PS4_PKG.Image, finalPath, "PIC0", PS4_PKG, respectiveExtract);
                            ExtractImage(PS4_PKG.Image2, finalPath, "PIC1", PS4_PKG, respectiveExtract);
                            break;
                    }
                    toolStripProgressBar1.Increment(1);
                    countPkg++;
                    toolStripStatusLabel2.Text = "Extracting images/icons.. (" + countPkg + "/" + pkgFilesList.Count + ")";
                }
                catch (Exception a)
                {
                    Helper.Bitmap.FailExtractImageList += Path.GetFileNameWithoutExtension(pkgPath) + " : " + a.Message + "\n";
                }
            }
        }

        void ExtractImage(byte[] imageBytes, string path, string fileNamePrefix, PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG, bool respectiveExtract)
        {
            if (imageBytes != null)
            {
                using (Bitmap tempImage = new Bitmap(Helper.Bitmap.BytesToBitmap(imageBytes)))
                {
                    string filePath = respectiveExtract
                        ? Path.Combine(path, $"{fileNamePrefix}.PNG")
                        : Path.Combine(path, $"{PS4_PKG.PS4_Title}_{fileNamePrefix}.PNG");

                    tempImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void ImageIconPostExtraction()
        {
            if (!string.IsNullOrEmpty(Helper.Bitmap.FailExtractImageList))
            {
                ShowWarning("Some PKG fail to extract : \n\n" + Helper.Bitmap.FailExtractImageList, false);
                Logger.LogWarning("Some PKG fail to extract : \n\n" + Helper.Bitmap.FailExtractImageList);
            }
            else
            {
                ShowInformation($"Images/icons extracted.", true);
            }

            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Value = 0;
            this.Enabled = true;
        }

        private void toolStripMenuItem96_Click(object sender, EventArgs e)
        {
            RefreshPkgList();
        }

        private void MorePKGTool(string type, DataTable dataTable = null, string excelFilename = null)
        {
            this.Enabled = false;
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);

            switch (type)
            {
                case "ENTRY":
                    // Code for the "ENTRY" button
                    break;

                case "TROPHY":
                    // Code for the "TROPHY" button
                    break;

                case PKGCategory.ADDON:
                    if (Tool.CheckForInternetConnection())
                    {
                        if (PKGGridView.GetCellCount(DataGridViewElementStates.Selected) > 0)
                        {
                            try
                            {
                                string CUSA_DLC = "";
                                string CONTENTID_DLC = "";
                                foreach (DataGridViewCell cell in PKGGridView.SelectedCells)
                                {
                                    int selectedrowindex = cell.RowIndex;
                                    DataGridViewRow selectedRow = PKGGridView.Rows[selectedrowindex];
                                    CUSA_DLC = Convert.ToString(selectedRow.Cells[1].Value);
                                    CONTENTID_DLC = Convert.ToString(selectedRow.Cells[2].Value);
                                }

                                if (CUSA_DLC != null && CONTENTID_DLC != null)
                                {
                                    try
                                    {
                                        PKG.StoreItems = PS4_Tools.PKG.Official.Get_All_Store_Items("CUSA07022");
                                    }
                                    catch
                                    {
                                        PKG.StoreItems = null;
                                    }

                                    if (PKG.StoreItems.Count > 0)
                                    {
                                        DLC grid = new DLC(PKG.StoreItems);
                                        toolStripStatusLabel2.Text = "Viewing addon.. ";
                                        BeginInvoke((MethodInvoker)delegate
                                        {
                                            grid.ShowDialog();
                                        });
                                    }
                                    else
                                    {
                                        ShowInformation("\"" + PS4_PKG.PS4_Title + "\" has no Addon", true);
                                    }
                                }
                                else
                                {
                                    ShowError("An error occurred", true);

                                }
                            }
                            catch (System.Runtime.InteropServices.ExternalException)
                            {
                                ShowError("The Clipboard could not be accessed. Please try again.", true);
                            }
                        }
                    }
                    else
                    {
                        ShowError("Network is not Available", true);
                    }
                    break;

                case "EXPORT":
                    try
                    {
                        Logger.LogInformation("Exporting PKG list..");
                        toolStripStatusLabel2.Text = "Exporting PKG list.. ";
                        var wb = new XLWorkbook();
                        wb.Worksheets.Add(dataTable, "PS4 PKG");
                        wb.SaveAs(excelFilename);

                        ShowInformation($"PKG list exported.", false);
                        Logger.LogInformation($"PKG list exported to \"{excelFilename}\".");
                    }
                    catch (Exception s)
                    {
                        ShowError(s.Message, true);
                    }
                    break;
            }
        }

        private void CopyContentID()
        {
            var pkgList = GetSelectedPKGDirectoryList(PKGSelectionType.SELECTED);

            if (pkgList.Count < 1)
            {
                ShowError("No PKG file selected.", false);
            }

            string formattedContentId = "";
            foreach (var pkg in pkgList)
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG selectedPackage = PS4_Tools.PKG.SceneRelated.Read_PKG(pkg);
                string titleIdFilter = selectedPackage.Param.ContentID.Replace(":", " -");
                formattedContentId += titleIdFilter.Replace("  -", " -") + "\n";
            }
            Clipboard.SetText(formattedContentId);
            ShowInformation("CONTENT_ID copied to clipboard", true);
        }

        private void toolStripMenuItem76_Click(object sender, EventArgs e)
        {
            RefreshPkgList();
        }

        private void RefreshPkgList()
        {
            if (PKGGridView.DataSource != null)
            {
                flatTabControl1.SelectedIndex = 0;
                Logger.LogInformation("Refreshing PKG list..");
                this.Invoke((MethodInvoker)delegate
                {
                    this.Enabled = false;
                    System.Threading.Thread.Sleep(1000);

                    while (PKGGridView.DataSource != null)
                    {
                        this.PKGGridView.DataSource = null;
                        this.PKGGridView.Rows.Clear();
                        PKGGridView.Rows.Clear();
                    }

                    while (darkDataGridView2.DataSource != null)
                    {
                        this.darkDataGridView2.DataSource = null;
                        this.darkDataGridView2.Rows.Clear();
                        darkDataGridView2.Rows.Clear();
                    }

                    FinalizePkgProcess = true;
                    LoadPKGGridView();
                });
            }
        }

        private void toolStripMenuItem78_Click(object sender, EventArgs e)
        {
            DialogResult dialog = DialogResultYesNo("Are you sure you wish to exit?");
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void toolStripMenuItem160_Click(object sender, EventArgs e)
        {
            Tool.OpenWebLink("https://ko-fi.com/pearlxcore");
        }

        private async void toolStripMenuItem158_Click(object sender, EventArgs e)
        {
            if (Tool.CheckForInternetConnection())
            {
                Logger.LogInformation("Checking for latest PS4 PKG Tool..");
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var checker = new UpdateChecker("pearlxcore", "PS4-PKG-Tool", "v" + ApplicationVersion);

                UpdateType update = await checker.CheckUpdate();

                if (update == UpdateType.None)
                {
                    ShowInformation("The program is up to date.", true);
                }
                else
                {
                    var result = new UpdateNotifyDialog(checker).ShowDialog();
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://github.com/pearlxcore/PS4-PKG-Tool/releases");
                    }
                }
            }
        }

        private void toolStripMenuItem159_Click(object sender, EventArgs e)
        {
            ShowInformation("PS4 PKG Tool " + ApplicationVersion + $"\n\nCopyright © pearlxcore {DateTime.Now.Year}\n\nCredit to xXxTheDarkprogramerxXx, Maxton, LMAN, Andshrew", false);
        }

        private void RenamePkg_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            string format = "";
            string selectionType = "";
            // global
            if (clickedMenuItem == renameAllPkg1ToolStripMenuItem1 || clickedMenuItem == renameAllPkg1ToolStripMenuItem2)
            {
                format = $"{NamingFormat.TITLE}";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem == renameAllPkg2ToolStripMenuItem1 || clickedMenuItem == renameAllPkg2ToolStripMenuItem2)
            {
                format = $"{NamingFormat.TITLE} [{NamingFormat.TITLE_ID}]";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem == renameAllPkg3ToolStripMenuItem1 || clickedMenuItem == renameAllPkg3ToolStripMenuItem2)
            {
                format= $"{NamingFormat.TITLE} [{NamingFormat.TITLE_ID}] [{NamingFormat.APP_VERSION}]";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem ==  renameAllPkg4ToolStripMenuItem1 || clickedMenuItem == renameAllPkg4ToolStripMenuItem2)
            {
                format= $"{NamingFormat.TITLE} [{NamingFormat.CATEGORY}]";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem ==  renameAllPkg5ToolStripMenuItem1 || clickedMenuItem == renameAllPkg5ToolStripMenuItem2)
            {
                format= $"{NamingFormat.TITLE_ID}";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem ==  renameAllPkg6ToolStripMenuItem1 || clickedMenuItem == renameAllPkg6ToolStripMenuItem2)
            {
                format= $"{NamingFormat.TITLE_ID} [{NamingFormat.TITLE}]";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem ==  renameAllPkg7ToolStripMenuItem1 || clickedMenuItem == renameAllPkg7ToolStripMenuItem2)
            {
                format = $"[{NamingFormat.TITLE_ID}] [{NamingFormat.CATEGORY}] [{NamingFormat.APP_VERSION}] {NamingFormat.TITLE}";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem ==  renameAllPkg8ToolStripMenuItem1 || clickedMenuItem == renameAllPkg8ToolStripMenuItem2)
            {
                format = $"{NamingFormat.TITLE} [{NamingFormat.CATEGORY}] [{NamingFormat.VERSION}]";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem ==  renameAllPkg9ToolStripMenuItem1 || clickedMenuItem == renameAllPkg9ToolStripMenuItem2)
            {
                format = $"{NamingFormat.CONTENT_ID}";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem ==  renameAllPkg10ToolStripMenuItem1 || clickedMenuItem == renameAllPkg10ToolStripMenuItem2)
            {
                format = $"{NamingFormat.CONTENT_ID2}";
                selectionType = PKGSelectionType.ALL;
            }
            else if (clickedMenuItem ==  renameAllPkg11ToolStripMenuItem1 || clickedMenuItem == renameAllPkg11ToolStripMenuItem2)
            {
                if (appSettings_.RenameCustomName == string.Empty)
                { ShowError("Set custom name format in settings.", true); return; }
                format= appSettings_.RenameCustomName;
                selectionType = PKGSelectionType.ALL;
            }

            // selected
            if (clickedMenuItem == renameSelectedPkg1ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg1ToolStripMenuItem2)
            {
                format = $"{NamingFormat.TITLE}";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg2ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg2ToolStripMenuItem2)
            {
                format = $"{NamingFormat.TITLE} [{NamingFormat.TITLE_ID}]";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg3ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg3ToolStripMenuItem2)
            {
                format= $"{NamingFormat.TITLE} [{NamingFormat.TITLE_ID}] [{NamingFormat.APP_VERSION}]";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg4ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg4ToolStripMenuItem2)
            {
                format= $"{NamingFormat.TITLE} [{NamingFormat.CATEGORY}]";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg5ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg5ToolStripMenuItem2)
            {
                format= $"{NamingFormat.TITLE_ID}";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg6ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg6ToolStripMenuItem2)
            {
                format= $"{NamingFormat.TITLE_ID} [{NamingFormat.TITLE}]";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg7ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg7ToolStripMenuItem2)
            {
                format = $"[{NamingFormat.TITLE_ID}] [{NamingFormat.CATEGORY}] [{NamingFormat.APP_VERSION}] {NamingFormat.TITLE}";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg8ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg8ToolStripMenuItem2)
            {
                format = $"{NamingFormat.TITLE} [{NamingFormat.CATEGORY}] [{NamingFormat.VERSION}]";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg9ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg9ToolStripMenuItem2)
            {
                format = $"{NamingFormat.CONTENT_ID}";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg10ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg10ToolStripMenuItem2)
            {
                format = $"{NamingFormat.CONTENT_ID2}";
                selectionType = PKGSelectionType.SELECTED;
            }
            else if (clickedMenuItem == renameSelectedPkg11ToolStripMenuItem1 || clickedMenuItem == renameSelectedPkg11ToolStripMenuItem2)
            {
                if (appSettings_.RenameCustomName == string.Empty)
                { ShowError("Set custom name format in settings.", true); return; }
                format= appSettings_.RenameCustomName;
                selectionType = PKGSelectionType.SELECTED;
            }
            RenamePKG(format, GetSelectedPKGDirectoryList(selectionType));
        }

        private void ExportPKGToExcel_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            // global
            if (clickedMenuItem == globalExportPKGListToExcelToolStripMenuItem1 || clickedMenuItem == globalExportPKGListToExcelToolStripMenuItem2)
            {
                InitializedExportPKGToExcel(GenerateDatatableFromSelectedPKG(PKGSelectionType.ALL));
            }

            // selected
            if (clickedMenuItem == selectedExportPKGListToExcelToolStripMenuItem1 || clickedMenuItem == selectedExportPKGListToExcelToolStripMenuItem2)
            {
                InitializedExportPKGToExcel(GenerateDatatableFromSelectedPKG(PKGSelectionType.SELECTED));
            }
        }

        private void InitializedExportPKGToExcel(DataTable dataTable = null)
        {
            if (ShowSaveFileDialog("Export PKG List to Excel", "*.xlsx|*.xlsx", out SaveFileDialog sfd))
            {
                string excelFilename = sfd.FileName;
                var bg = new BackgroundWorker();
                bg.DoWork += delegate
                {
                    MorePKGTool("EXPORT", dataTable, excelFilename);
                };
                bg.RunWorkerCompleted += delegate
                {
                    toolStripStatusLabel2.Text = "... ";
                    this.Enabled = true;
                };
                bg.RunWorkerAsync();
            }
        }

        private void CopyTitleID()
        {
            var pkgList = GetSelectedPKGDirectoryList(PKGSelectionType.SELECTED);

            if (pkgList.Count < 1)
            {
                ShowError("No PKG file selected.", false);
            }
            else
            {
                string formattedTitleId = string.Join("\n", pkgList.Select(pkg =>
                {
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG selectedPackage = PS4_Tools.PKG.SceneRelated.Read_PKG(pkg);
                    string titleIdFilter = selectedPackage.Param.TITLEID.Replace(":", " -");
                    return titleIdFilter.Replace("  -", " -");
                }));

                Clipboard.SetText(formattedTitleId);
                ShowInformation("PKG TITLE_ID copied to clipboard", true);
            }
        }

        private void CopyID_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            if (clickedMenuItem == copyTitleIdtoolStripMenuItem1 || clickedMenuItem == copyTitleIdtoolStripMenuItem2)
            {
                CopyTitleID();
            }
            if (clickedMenuItem == copyContentIdtoolStripMenuItem1 || clickedMenuItem == copyContentIdtoolStripMenuItem2)
            {
                CopyContentID();
            }
        }

        private void ViewPKGExplorer_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            if (clickedMenuItem == viewPkgExplorerStripMenuItem1 || clickedMenuItem == viewPkgExplorerStripMenuItem2)
            {
                ViewPKGInExplorer();
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel5.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        #region ImageIconExtractor
        private void InitializedImageIconExtractor(string imageType, string selectionType)
        {
            DialogResult extractionDialog = DialogResultYesNoCancel("Extract to their respective folders?");

            if (extractionDialog == DialogResult.Cancel)
                return;

            var respectiveExtract = (extractionDialog == DialogResult.Yes);

            if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
            {
                Logger.LogInformation("Extracting images/icons..");
                toolStripStatusLabel2.Text = "Extracting images/icons..";
                var outputDirectory = fbd.SelectedPath;
                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (s, e) =>
                {
                    ImageIconExtractor(imageType, GetSelectedPKGDirectoryList(selectionType), outputDirectory, respectiveExtract);
                };
                backgroundWorker.RunWorkerCompleted += (s, e) =>
                {
                    ImageIconPostExtraction();
                };
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void ExtractImageIcon_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem clickedMenuItem)
            {
                // global
                if (clickedMenuItem == globalExtractImagesAndIconToolStripMenuItem1 || clickedMenuItem == globalExtractImagesAndIconToolStripMenuItem2)
                {
                    InitializedImageIconExtractor(ImageIconExtractionType.ALL, PKGSelectionType.ALL);
                }

                if (clickedMenuItem == globalExtractImageOnlyToolStripMenuItem1 || clickedMenuItem == globalExtractImageOnlyToolStripMenuItem2)
                {
                    InitializedImageIconExtractor(ImageIconExtractionType.IMAGE, PKGSelectionType.ALL);
                }

                if (clickedMenuItem == globalExtractIconOnlyToolStripMenuItem1 || clickedMenuItem == globalExtractIconOnlyToolStripMenuItem2)
                {
                    InitializedImageIconExtractor(ImageIconExtractionType.ICON, PKGSelectionType.ALL);
                }

                // selected
                if (clickedMenuItem == selectedExtractImagesAndIconToolStripMenuItem1 || clickedMenuItem == selectedExtractImagesAndIconToolStripMenuItem2)
                {
                    InitializedImageIconExtractor(ImageIconExtractionType.ALL, PKGSelectionType.SELECTED);
                }

                if (clickedMenuItem == selectedExtractImageOnlyToolStripMenuItem1 || clickedMenuItem == selectedExtractImageOnlyToolStripMenuItem2)
                {
                    InitializedImageIconExtractor(ImageIconExtractionType.IMAGE, PKGSelectionType.SELECTED);
                }

                if (clickedMenuItem == selectedExtractIconOnlyToolStripMenuItem1 || clickedMenuItem == selectedExtractIconOnlyToolStripMenuItem2)
                {
                    InitializedImageIconExtractor(ImageIconExtractionType.ICON, PKGSelectionType.SELECTED);
                }
            }
        }
        #endregion ImageIconExtractor

        #region PKGScanning
        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            OpenPKGDirectorySettings();
        }

        private void OpenPKGDirectorySettings()
        {
            Logger.LogInformation("Opening PKG Directory Settings..");
            FirstLaunch = false;
            this.Hide();

            using (var form = new PKG_Directory_Settings())
            {
                form.ShowDialog();

                if (form.closingProgram)
                {
                    Application.Exit();
                    return;
                }
            }

            this.Show();
            this.Enabled = false;

            //PKGListGridView.SelectionChanged -= PKGListGridView_SelectionChanged;

            PKGGridView.Enabled = false; // Disable PKGListGridView during listing PKG
            darkDataGridView2.Enabled = false; // Disable darkDataGridView2 during listing PKG

            LoadPKGGridView();
        }

        private void managePS4PKGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPKGDirectorySettings();
        }

        static bool IsExcluded(List<string> exludedDirList, string target)
        {
            return exludedDirList.Any(d => new DirectoryInfo(target).Name.Equals(d));
        }

        private bool IsRootDirectory(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.Parent == null;
        }

        public static void autoResizeColumns(ListView lv)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView.ColumnHeaderCollection cc = lv.Columns;
            for (int i = 0; i < cc.Count; i++)
            {
                int colWidth = TextRenderer.MeasureText(cc[i].Text, lv.Font).Width + 10;
                if (colWidth > cc[i].Width)
                {
                    cc[i].Width = colWidth;
                }
            }
        }

        private void LoadPKGListView()
        {
            PKGListView.Columns.Add("Filename");
            PKGListView.Columns.Add("Title ID");
            PKGListView.Columns.Add("Content ID");
            PKGListView.Columns.Add("Region");
            PKGListView.Columns.Add("System Version");
            PKGListView.Columns.Add("Version [App Version]");
            PKGListView.Columns.Add("PKG Type");
            PKGListView.Columns.Add("Category");
            PKGListView.Columns.Add("Size");
            PKGListView.Columns.Add("PSVR");
            PKGListView.Columns.Add("PS4 Pro Enhanced");
            PKGListView.Columns.Add("PS5 BC");
            PKGListView.Columns.Add("Directory");
            PKGListView.Columns.Add("Backported");

            autoResizeColumns(PKGListView);

            this.Invoke((MethodInvoker)delegate
            {
                PKG.SelectedPKGFilename = null;
                pictureBox1.Image = null;
                darkLabel1.Text = "";
            });

            PKG.VerifiedPs4PkgList.Clear();
            PKG.EntryIdList.Clear();
            PKG.EntryNameList.Clear();
            //PKG.totalPkg = 0;
            PKG.pkgCount = 0;
            PKG.game = 0;
            PKG.patch = 0;
            PKG.app = 0;
            PKG.unknown = 0;
            PKG.addon = 0;
            toolStripProgressBar1.Value = 0;

            List<string> PkgFileList = new List<string>();
            var PkgDirectoryList = appSettings_.PkgDirectories;
            foreach (var directory in PkgDirectoryList)
            {
                var searchOption = appSettings_.ScanRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                try
                {
                    var allDirectories = Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly);

                    var directoriesToScan = new List<string> { directory };
                    directoriesToScan.AddRange(allDirectories.Where(dir => !ExcludedDirectoryList.Contains(Path.GetFileName(dir))));

                    foreach (var directory_ in directoriesToScan)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            toolStripStatusLabel2.Text = "Scanning directory.. " + "(" + directory_ + ") ";
                        });

                        try
                        {
                            var pkgFiles = Directory.GetFiles(directory_, "*.PKG", searchOption);
                            if (pkgFiles.Length > 0)
                                PkgFileList.AddRange(pkgFiles);
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            Logger.LogError(e.Message);
                        }
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    Logger.LogError(e.Message);
                }
            }

            PkgFileList = PkgFileList.Distinct().ToList();

            // verify scanned ps4 pkg and count it
            foreach (var item in PkgFileList)
            {
                //filter ps4 pkg by checking magic byte
                byte[] bufferA = new byte[16];

                bufferA = PKG.GetPkgHeaderBuffer(item);
                if (PKG.CompareBytes(bufferA, PKG.PkgHeader) || PKG.CompareBytes(bufferA, PKG.PkgHeader1) ||
                    PKG.CompareBytes(bufferA, PKG.PkgHeader2) || PKG.CompareBytes(bufferA, PKG.PkgHeader3) ||
                    PKG.CompareBytes(bufferA, PKG.PkgHeader4))
                {
                    PKG.VerifiedPs4PkgList.Add(item);
                    //PKG.totalPkg++;
                }
            }

            this.Invoke((MethodInvoker)delegate
            {
                toolStripProgressBar1.Visible = true;
                //toolStripProgressBar1.Maximum = PKG.totalPkg;
                toolStripProgressBar1.Maximum = PKG.VerifiedPs4PkgList.Count;
            });

            Dictionary<string, ListViewGroup> groupDictionary = new Dictionary<string, ListViewGroup>();

            foreach (var pkg in PKG.VerifiedPs4PkgList)
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG ps4Pkg = PS4_Tools.PKG.SceneRelated.Read_PKG(pkg);
                Param_SFO.PARAM_SFO psfo = ps4Pkg.Param;

                string pkgVersion = "";
                string pkgAppVersion = psfo.APP_VER;
                string pkgTitleId = ps4Pkg.Param.TITLEID;
                string pkgFileName = Path.GetFileName(pkg);
                string pkgDirectoryName = Path.GetDirectoryName(pkg);
                string psVr = "";
                string neoEnable = "";
                string ps5bc = "";
                string pkgSystemVersion = "";
                byte[] pkgRegionIcon = null;
                string pkgState = ps4Pkg.PKGState.ToString();
                string pkgType = ps4Pkg.PKG_Type.ToString();
                string pkgSize;
                string pkgIsBackported = "";

                // remove leading zero on app_ver
                string pattern = @"^0+(?=\d+\.)";
                pkgAppVersion = Regex.Replace(pkgAppVersion, pattern, "");

                // get pkg's minimum system fw
                foreach (Param_SFO.PARAM_SFO.Table t in ps4Pkg.Param.Tables.ToList())
                {
                    if (t.Name == "SYSTEM_VER")
                    {
                        int value = Convert.ToInt32(t.Value);
                        string hexOutput = String.Format("{0:X}", value);
                        if (t.Value != "0")
                        {
                            string first_three = hexOutput.Substring(0, 3);
                            pkgSystemVersion = first_three.Insert(1, ".");
                        }
                        else // SYSTEM_VER = 0 (HB Store.pkg)
                            pkgSystemVersion = t.Value;
                    }
                    if (t.Name == "VERSION")
                    {
                        // remove leading zero on app_ver
                        pkgVersion = t.Value;
                        pkgVersion = Regex.Replace(pkgVersion, pattern, "");
                    }
                }

                // get latest official update version and minimum firmware
                //string updateVersion;
                //string updateMinFirmware;
                //(updateVersion, updateMinFirmware)= PS4_Tools.PKG.Official.CheckLatestUpdateVersionAndMinFirmware(ps4Pkg.Param.TITLEID);


                // get pkg full size
                long fileSizeBytes = new System.IO.FileInfo(pkg).Length;
                pkgSize = ByteSize.FromBytes(fileSizeBytes).ToString();

                // backward compatible info
                if (appSettings_.psvr_neo_ps5bc_check && File.Exists(Ps5BcJsonFile))
                {
                    dynamic ps5BcJsonData = JsonConvert.DeserializeObject(File.ReadAllText(Ps5BcJsonFile));

                    if (pkgType == PKGCategory.GAME)
                    {
                        foreach (var item_ in ps5BcJsonData)
                        {
                            if (item_.npTitleIdshort == ps4Pkg.Param.TITLEID)
                            {
                                string titleID = item_.npTitleIdshort;
                                string psvr = item_.psVr;
                                string neo = item_.neoEnable;
                                string ps5bc_ = item_.ps5bc;

                                // ps4vr
                                psVr = (psvr == "1" || psvr == "2") ? "Yes" : (psvr == "0") ? "No" : (psvr != "null") ? "NA" : "";

                                // ps4 pro enhanced
                                neoEnable = (neo == "1") ? "Yes" : (neo == "0") ? "No" : (psvr != "null") ? "NA" : "";

                                // ps5bc
                                ps5bc = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(ps5bc_.Replace("_", " ").ToLower());
                            }
                        }
                    }
                    else
                    {
                        psVr = neoEnable = ps5bc = "-";
                    }
                }
                else
                {
                    psVr = neoEnable = ps5bc = "";
                }

                // get region from content id
                var imageConverter = new ImageConverter();
                var region = ps4Pkg.Region;
                if (region == PKGRegion.EU) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.eu, typeof(byte[])); }
                else if (region == PKGRegion.US) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.us, typeof(byte[])); }
                else if (region == PKGRegion.UK) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.us, typeof(byte[])); }
                else if (region == PKGRegion.JAPAN) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.jp, typeof(byte[])); }
                else if (region == PKGRegion.HONG_KONG) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.hk, typeof(byte[])); }
                else if (region == PKGRegion.ASIA) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.asia, typeof(byte[])); }
                else if (region == PKGRegion.KOREA) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.kr, typeof(byte[])); }

                // check if pkg is backported
                pkgIsBackported = File.Exists(Backport.BackportInfoFile) ? Backport.CheckPKGBackported(pkg) ?? "No" : "No";

                // Create a ListViewItem to hold the package information.
                ListViewItem item = new ListViewItem(new string[]
                {
            pkgFileName, pkgTitleId, ps4Pkg.Param.ContentID, pkgRegionIcon.ToString(), pkgSystemVersion,
            pkgVersion + $" [{pkgAppVersion}]", pkgState, pkgType, pkgSize, psVr, neoEnable, ps5bc, pkgDirectoryName, pkgIsBackported
                });

                // Create or retrieve a group based on the package title.
                string groupName = pkgTitleId;
                ListViewGroup group;

                if (!groupDictionary.TryGetValue(groupName, out group))
                {
                    group = new ListViewGroup(groupName);
                    groupDictionary[groupName] = group;
                    PKGListView.Groups.Add(group);
                }

                item.Group = group;

                // Add the ListViewItem to the ListView.
                PKGListView.Items.Add(item);
            }

            this.Enabled = true;
        }


        private void LoadPKGGridView()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += delegate
            {
                #region loadPkgProcess
                this.Invoke((MethodInvoker)delegate
                {
                    PKG.SelectedPKGFilename = null;
                    pictureBox1.Image = null;
                    darkLabel1.Text = "";
                });

                PKG.VerifiedPs4PkgList.Clear();
                PKG.EntryIdList.Clear();
                PKG.EntryNameList.Clear();
                //PKG.totalPkg = 0;
                PKG.pkgCount = 0;
                PKG.game = 0;
                PKG.patch = 0;
                PKG.app = 0;
                PKG.unknown = 0;
                PKG.addon = 0;
                toolStripProgressBar1.Value = 0;

                List<string> PkgFileList = new List<string>();
                var PkgDirectoryList = appSettings_.PkgDirectories;
                foreach (var directory in PkgDirectoryList)
                {
                    var searchOption = appSettings_.ScanRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                    try
                    {
                        var allDirectories = Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly);

                        var directoriesToScan = new List<string> { directory };
                        directoriesToScan.AddRange(allDirectories.Where(dir => !ExcludedDirectoryList.Contains(Path.GetFileName(dir))));

                        foreach (var directory_ in directoriesToScan)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                toolStripStatusLabel2.Text = "Scanning directory.. " + "(" + directory_ + ") ";
                            });

                            try
                            {
                                var pkgFiles = Directory.GetFiles(directory_, "*.PKG", searchOption);
                                if (pkgFiles.Length > 0)
                                    PkgFileList.AddRange(pkgFiles);
                            }
                            catch (UnauthorizedAccessException e)
                            {
                                Logger.LogError(e.Message);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Logger.LogError(e.Message);
                    }
                }

                PkgFileList = PkgFileList.Distinct().ToList();

                // datatable for gridview control
                DataTable dttemp = new DataTable();
                dttemp.Columns.Add("Filename");
                dttemp.Columns.Add("Title ID");
                dttemp.Columns.Add("Content ID");
                dttemp.Columns.Add("Region", typeof(byte[]));
                dttemp.Columns.Add("System Version");
                dttemp.Columns.Add("Version [App Version]");
                dttemp.Columns.Add("PKG Type");
                dttemp.Columns.Add("Category");
                dttemp.Columns.Add("Size");
                dttemp.Columns.Add("PSVR");
                dttemp.Columns.Add("PS4 Pro Enhanced");
                dttemp.Columns.Add("PS5 BC");
                dttemp.Columns.Add("Directory");
                dttemp.Columns.Add("Backported");

                // verify scanned ps4 pkg and count it
                foreach (var item in PkgFileList)
                {
                    //filter ps4 pkg by checking magic byte
                    byte[] bufferA = new byte[16];

                    bufferA = PKG.GetPkgHeaderBuffer(item);
                    if (PKG.CompareBytes(bufferA, PKG.PkgHeader) || PKG.CompareBytes(bufferA, PKG.PkgHeader1) ||
                        PKG.CompareBytes(bufferA, PKG.PkgHeader2) || PKG.CompareBytes(bufferA, PKG.PkgHeader3) ||
                        PKG.CompareBytes(bufferA, PKG.PkgHeader4))
                    {
                        PKG.VerifiedPs4PkgList.Add(item);
                        //PKG.totalPkg++;
                    }
                }

                this.Invoke((MethodInvoker)delegate
                {
                    toolStripProgressBar1.Visible = true;
                    //toolStripProgressBar1.Maximum = PKG.totalPkg;
                    toolStripProgressBar1.Maximum = PKG.VerifiedPs4PkgList.Count;
                });

                // process every verified pkg and display into gridview control
                foreach (var pkg in PKG.VerifiedPs4PkgList)
                {
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG ps4Pkg = PS4_Tools.PKG.SceneRelated.Read_PKG(pkg);
                    Param_SFO.PARAM_SFO psfo = ps4Pkg.Param;

                    string pkgVersion = "";
                    string pkgAppVersion = psfo.APP_VER;
                    string pkgTitleId = ps4Pkg.Param.TITLEID;
                    string pkgFileName = Path.GetFileName(pkg);
                    string pkgDirectoryName = Path.GetDirectoryName(pkg);
                    string psVr = "";
                    string neoEnable = "";
                    string ps5bc = "";
                    string pkgSystemVersion = "";
                    byte[] pkgRegionIcon = null;
                    string pkgState = ps4Pkg.PKGState.ToString();
                    string pkgType = ps4Pkg.PKG_Type.ToString();
                    string pkgSize;
                    string pkgIsBackported = "";

                    // remove leading zero on app_ver
                    string pattern = @"^0+(?=\d+\.)";
                    pkgAppVersion = Regex.Replace(pkgAppVersion, pattern, "");

                    // get pkg's minimum system fw
                    foreach (Param_SFO.PARAM_SFO.Table t in ps4Pkg.Param.Tables.ToList())
                    {
                        if (t.Name == "SYSTEM_VER")
                        {
                            int value = Convert.ToInt32(t.Value);
                            string hexOutput = String.Format("{0:X}", value);
                            if (t.Value != "0")
                            {
                                string first_three = hexOutput.Substring(0, 3);
                                pkgSystemVersion = first_three.Insert(1, ".");
                            }
                            else // SYSTEM_VER = 0 (HB Store.pkg)
                                pkgSystemVersion = t.Value;
                        }
                        if (t.Name == "VERSION")
                        {
                            // remove leading zero on app_ver
                            pkgVersion = t.Value;
                            pkgVersion = Regex.Replace(pkgVersion, pattern, "");
                        }
                    }

                    // get latest official update version and minimum firmware
                    //string updateVersion;
                    //string updateMinFirmware;
                    //(updateVersion, updateMinFirmware)= PS4_Tools.PKG.Official.CheckLatestUpdateVersionAndMinFirmware(ps4Pkg.Param.TITLEID);


                    // get pkg full size
                    long fileSizeBytes = new System.IO.FileInfo(pkg).Length;
                    pkgSize = ByteSize.FromBytes(fileSizeBytes).ToString();

                    // backward compatible info
                    if (appSettings_.psvr_neo_ps5bc_check && File.Exists(Ps5BcJsonFile))
                    {
                        dynamic ps5BcJsonData = JsonConvert.DeserializeObject(File.ReadAllText(Ps5BcJsonFile));

                        if (pkgType == PKGCategory.GAME)
                        {
                            foreach (var item in ps5BcJsonData)
                            {
                                if (item.npTitleIdshort == ps4Pkg.Param.TITLEID)
                                {
                                    string titleID = item.npTitleIdshort;
                                    string psvr = item.psVr;
                                    string neo = item.neoEnable;
                                    string ps5bc_ = item.ps5bc;

                                    // ps4vr
                                    psVr = (psvr == "1" || psvr == "2") ? "Yes" : (psvr == "0") ? "No" : (psvr != "null") ? "NA" : "";

                                    // ps4 pro enhanced
                                    neoEnable = (neo == "1") ? "Yes" : (neo == "0") ? "No" : (psvr != "null") ? "NA" : "";

                                    // ps5bc
                                    ps5bc = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(ps5bc_.Replace("_", " ").ToLower());
                                }
                            }
                        }
                        else
                        {
                            psVr = neoEnable = ps5bc = "-";
                        }
                    }
                    else
                    {
                        psVr = neoEnable = ps5bc = "";
                    }

                    // get region from content id
                    var imageConverter = new ImageConverter();
                    var region = ps4Pkg.Region;
                    if (region == PKGRegion.EU) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.eu, typeof(byte[])); }
                    else if (region == PKGRegion.US) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.us, typeof(byte[])); }
                    else if (region == PKGRegion.UK) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.us, typeof(byte[])); }
                    else if (region == PKGRegion.JAPAN) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.jp, typeof(byte[])); }
                    else if (region == PKGRegion.HONG_KONG) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.hk, typeof(byte[])); }
                    else if (region == PKGRegion.ASIA) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.asia, typeof(byte[])); }
                    else if (region == PKGRegion.KOREA) { pkgRegionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.kr, typeof(byte[])); }

                    // check if pkg is backported
                    pkgIsBackported = File.Exists(Backport.BackportInfoFile) ? Backport.CheckPKGBackported(pkg) ?? "No" : "No";


                    // add items to datatable
                    string pkgMinFirmware = ps4Pkg.PKG_Type.ToString() == PKGCategory.ADDON ? "NA" : $"{pkgSystemVersion}";
                    pkgAppVersion = (pkgAppVersion == string.Empty) ? "NA" : pkgAppVersion;
                    dttemp.Rows.Add(pkgFileName, pkgTitleId, ps4Pkg.Param.ContentID, pkgRegionIcon, pkgMinFirmware, pkgVersion + $" [{pkgAppVersion}]", pkgState, pkgType, pkgSize, psVr, neoEnable, ps5bc, pkgDirectoryName, pkgIsBackported);

                    switch (ps4Pkg.PKG_Type.ToString())
                    {
                        case PKGCategory.GAME: PKG.game++; break;
                        case PKGCategory.PATCH: PKG.patch++; break;
                        case PKGCategory.APP: PKG.app++; break;
                        case PKGCategory.ADDON: PKG.addon++; break;
                        default: PKG.unknown++; break;
                    }

                    switch (ps4Pkg.PKGState.ToString())
                    {
                        case PKGState.OFFICIAL: PKG.official++; break;
                        case PKGState.FAKE: PKG.fake++; break;
                        case PKGState.ADDON_UNLOCKER: PKG.unlockerAddon++; break;
                    }

                    PKG.pkgCount++;
                    darkStatusStrip1.Invoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel2.Text = "Loading PS4 PKG.. " + "(" + PKG.pkgCount.ToString() + "/" + PKG.VerifiedPs4PkgList.Count.ToString() + ") ";
                        toolStripProgressBar1.Increment(1);
                        PKGGridView.DataSource = dttemp;
                        for (int i = 9; i <= 11; i++)
                        {
                            PKGGridView.Columns[i].Visible = appSettings_.psvr_neo_ps5bc_check;
                        }
                        foreach (DataGridViewColumn column in PKGGridView.Columns)
                        {
                            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        PKGGridView.Refresh();
                    });

                }
                #endregion loadPkgProcess1
            };
            bw.RunWorkerCompleted += delegate
            {
                PostPkgLoad();
            };
            bw.RunWorkerAsync();
        }

        private void PostPkgLoad()
        {
            if (PKG.VerifiedPs4PkgList.Count == 0)
            {
                // clear gridview control
                while (PKGGridView.DataSource != null)
                {
                    PKGGridView.DataSource = null;
                    PKGGridView.Rows.Clear();
                    PKGGridView.Rows.Clear();
                }
                while (darkDataGridView2.DataSource != null)
                {
                    darkDataGridView2.DataSource = null;
                    darkDataGridView2.Rows.Clear();
                    darkDataGridView2.Rows.Clear();
                }
                ShowInformation("No PKG found", true);
                OpenPKGDirectorySettings();
            }
            else
            {
                ExtractTrophyFile(PKG.VerifiedPs4PkgList);
                FinalizePkgLoadingProcess();
                UpdateDataGridViewColumnVisibility();
                SetBackgroundMusicVolume();
                SetDataGridViewCellStyle();
            }
            //PKGListGridView.SelectionChanged += PKGListGridView_SelectionChanged;
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Value = 0;
            PKGGridView.Enabled = true;
            darkDataGridView2.Enabled = true;
            this.Enabled = true;
        }

        private void SetDataGridViewCellStyle()
        {
            this.Invoke((MethodInvoker)delegate
            {
                try
                {
                    // Sort file name ascending
                    if (appSettings_.AutoSortRow)
                        PKGGridView.Sort(PKGGridView.Columns[0], ListSortDirection.Ascending);

                    // Set header cell alignment
                    for (int columnIndex = 0; columnIndex < PKGGridView.Columns.Count; columnIndex++)
                    {
                        PKGGridView.Columns[columnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Set cell alignment
                    for (int columnIndex = 1; columnIndex <= 13; columnIndex++)
                    {
                        PKGGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                catch
                {
                }
            });
        }

        private static void SetBackgroundMusicVolume()
        {
            if (!appSettings_.PlayBgm)
            {
                int newVolume = 0; // Set 0 to unmute
                uint newVolumeAllChannels = (((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16));
                waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
            }
            else
            {
                int newVolume = 65535; // Set 65535 to unmute
                uint newVolumeAllChannels = (((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16));
                waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
            }
        }

        private void UpdateDataGridViewColumnVisibility()
        {
            try
            {
                PKGGridView.Columns[1].Visible = appSettings_.pkgtitleIdColumn;
                PKGGridView.Columns[2].Visible = appSettings_.pkgcontentIdColumn;
                PKGGridView.Columns[3].Visible = appSettings_.pkgregionColumn;
                PKGGridView.Columns[4].Visible = appSettings_.pkgminimumFirmwareColumn;
                PKGGridView.Columns[5].Visible = appSettings_.pkgversionColumn;
                PKGGridView.Columns[6].Visible = appSettings_.pkgTypeColumn;
                PKGGridView.Columns[7].Visible = appSettings_.pkgcategoryColumn;
                PKGGridView.Columns[8].Visible = appSettings_.pkgsizeColumn;
                PKGGridView.Columns[9].Visible = appSettings_.psvr_neo_ps5bc_check;
                PKGGridView.Columns[10].Visible = appSettings_.psvr_neo_ps5bc_check;
                PKGGridView.Columns[11].Visible = appSettings_.psvr_neo_ps5bc_check;
                PKGGridView.Columns[12].Visible = appSettings_.pkgDirectoryColumn;
                PKGGridView.Columns[13].Visible = appSettings_.pkgBackportColumn;
            }
            catch { }
        }

        private void FinalizePkgLoadingProcess()
        {
            if (FinalizePkgProcess)
            {
                FinalizePkgProcess = false;
                BackgroundWorker bgw = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };
                bgw.DoWork += (s, a) =>
                {
                    Logger.LogInformation("Extracting PKG background music..");
                    BGM.ExtractBgm();
                };
                bgw.RunWorkerCompleted += (s, a) =>
                {
                    BGM.extractAt9Done = true;
                };
                bgw.RunWorkerAsync();
                toolStripSplitButton1.DropDownItems.Clear();
                GetDrivesFreeSpace();
                ToolStripSplitButtonTotalPKG.DropDownItems.Clear();
                labelDisplayTotalPKG.Text = $"Displaying {PKGGridView.Rows.Count} PS4 PKG";
                if (PKG.game != 0)
                    ToolStripSplitButtonTotalPKG.DropDownItems.Add($"Show only Game PKG ({PKG.game})", null, new System.EventHandler(GridViewFilterPKG_Click));
                if (PKG.patch != 0)
                    ToolStripSplitButtonTotalPKG.DropDownItems.Add($"Show only Patch PKG ({PKG.patch})", null, new System.EventHandler(GridViewFilterPKG_Click));
                if (PKG.addon != 0)
                    ToolStripSplitButtonTotalPKG.DropDownItems.Add($"Show only Addon PKG ({PKG.addon})", null, new System.EventHandler(GridViewFilterPKG_Click));
                if (PKG.app != 0)
                    ToolStripSplitButtonTotalPKG.DropDownItems.Add($"Show only App PKG ({PKG.app})", null, new System.EventHandler(GridViewFilterPKG_Click));
                if (PKG.unknown != 0)
                    ToolStripSplitButtonTotalPKG.DropDownItems.Add($"Show only Unknown PKG ({PKG.unknown})", null, new System.EventHandler(GridViewFilterPKG_Click));

                ToolStripSplitButtonTotalPKG.DropDownItems.Add("Show all PKG", null, new System.EventHandler(GridViewFilterPKG_Click));
                Logger.LogInformation($"Loading PKG done. {PKGGridView.Rows.Count} PKG found.");
            }
        }

        private void GetDrivesFreeSpace()
        {
            Logger.LogInformation("Checking hard disk free space..");
            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    long freeSpace = drive.TotalFreeSpace;
                    long totalSpace = drive.TotalSize;
                    double freeSpaceGB = ByteSize.FromBytes(freeSpace).GigaBytes;
                    double totalSpaceGB = ByteSize.FromBytes(totalSpace).GigaBytes;

                    string formattedFreeSpace = $"{freeSpaceGB:F2} GB";
                    string formattedTotalSpace = $"{totalSpaceGB:F2} GB";

                    toolStripSplitButton1.DropDownItems.Add($"[{drive}] Free Space: {formattedFreeSpace}/{formattedTotalSpace}");
                    Logger.LogInformation($"[{drive}] Free Space: {formattedFreeSpace}/{formattedTotalSpace}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occurred while getting hard disk free space: " + ex.Message);
            }
        }
        #endregion PKGScanning

        #region PKGGridViewFiltering

        private void GridViewFilterPKG_Click(object sender, EventArgs e)
        {
            string text = sender.ToString();
            if (text.Contains(PKGCategory.GAME))
            {
                (PKGGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", PKGCategory.GAME);
            }
            else if (text.Contains(PKGCategory.PATCH))
            {
                (PKGGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", PKGCategory.PATCH);
            }
            else if (text.Contains(PKGCategory.ADDON))
            {
                (PKGGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", PKGCategory.ADDON);
            }
            else if (text.Contains(PKGCategory.APP))
            {
                (PKGGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", PKGCategory.APP);
            }
            else if (text.Contains(PKGCategory.UNKNOWN))
            {
                (PKGGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", "Unknown");
            }
            else if (text.Contains("all"))
            {
                (PKGGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", "");
            }
        }

        #endregion PKGGridViewFiltering

        #region PKGBasicOperation_Copy_Rename_Delete_ViewExplorer
        #region PKG_Copy_delete_View
        private void ViewPKGInExplorer()
        {
            var pkgList = GetSelectedPKGDirectoryList(PKGSelectionType.SELECTED);
            pkgList.ForEach(pkg => Logger.LogInformation($"Opening {pkg} PKG file in Explorer.."));
            pkgList.ForEach(pkg => Process.Start("explorer.exe", "/select," + pkg));
        }

        private void OpenTempDirectory()
        {
            Process.Start("explorer.exe", Helper.PS4PKGToolTempDirectory);
        }

        private void DeletePkg()
        {
            var pkgList = GetSelectedPKGDirectoryList(PKGSelectionType.SELECTED);
            DialogResult dialog = DialogResultYesNo("PKG file will be permanently deleted. This operation cannot be undone. Are you sure you want to continue?");

            if (dialog == DialogResult.Yes)
            {
                PKG.isDeletingPkg = true;
                try
                {
                    foreach (var pkg in pkgList)
                    {
                        var pkgFileName = Path.GetFileName(pkg);
                        var directoryName = Path.GetDirectoryName(pkg);
                        var fullPkgPath = Path.Combine(directoryName, pkgFileName);
                        var matchingRow = PKGGridView.Rows
                            .Cast<DataGridViewRow>()
                            .FirstOrDefault(row => row.Cells[0].Value.ToString() == pkgFileName && row.Cells[12].Value.ToString() == directoryName);

                        // remove pkg from gridview
                        if (matchingRow != null)
                        {
                            PKGGridView.Rows.Remove(matchingRow);
                        }

                        // remove pkg from VerifiedPs4PkgList
                        PKG.VerifiedPs4PkgList.Remove(fullPkgPath);

                        File.Delete(pkg);
                        Logger.LogInformation($"\"{pkg}\" deleted.");
                        //PKG.totalPkg--;
                    }

                    PKG.SelectedPKGFilename = ""; // Reset
                    labelDisplayTotalPKG.Text = "Displaying " + PKG.VerifiedPs4PkgList.Count.ToString() + " PS4 PKG";
                    ShowInformation("PKG file deleted.", true);
                    PKG.isDeletingPkg = false;
                }
                catch (Exception a)
                {
                    ShowError("An error occurred: " + a.Message, true);
                }
            }
        }

        private void DeletePKG_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            if (clickedMenuItem == deletePKGtoolStripMenuItem1 || clickedMenuItem == deletePkgtoolStripMenuItem2)
            {
                DeletePkg();
            }
        }

        #endregion PKG_Copy_delete_View

        #region renamePKG

        private void CheckForDuplicatePKG_Click(object sender, EventArgs args)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            Logger.LogInformation("Checking for duplicate PKG..");
            if (clickedMenuItem == checkForDuplicatePKGToolStripMenuItem1 ||  clickedMenuItem == checkForDuplicatePKGToolStripMenuItem2)
            {
                var result = FindDuplicatePKG();
                if (result.Count == 0)
                {
                    ShowInformation("No duplicate PKG detected", true);
                }
                else
                {
                    ShowWarning($"Duplicate PKG detected:\n\n{string.Join("\n", result)}", true);
                }
            }
        }

        private HashSet<string> FindDuplicatePKG()
        {
            Dictionary<string, List<string>> valuesWithFilenames = new Dictionary<string, List<string>>();
            HashSet<string> duplicatedFilenames = new HashSet<string>();

            foreach (DataGridViewRow row in PKGGridView.Rows)
            {
                string combinedValues = "";
                for (int columnIndex = 1; columnIndex <= 8; columnIndex++) // Check columns 1 to 8
                {
                    if (row.Cells[columnIndex].Value != null)
                    {
                        combinedValues += row.Cells[columnIndex].Value.ToString() + "|";
                    }
                }

                if (!string.IsNullOrEmpty(combinedValues))
                {
                    string filename = row.Cells[0].Value?.ToString(); // Assuming column index 0 has filenames

                    if (valuesWithFilenames.ContainsKey(combinedValues))
                    {
                        valuesWithFilenames[combinedValues].Add(filename);
                        duplicatedFilenames.Add(filename);
                    }
                    else
                    {
                        valuesWithFilenames[combinedValues] = new List<string> { filename };
                    }
                }
            }

            return duplicatedFilenames;
        }

        private void UpdatePKGFilename(string newPkgName, string sourcePkg, string targetPkg)
        {
            foreach (DataGridViewRow row in PKGGridView.Rows)
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG readPkg = PS4_Tools.PKG.SceneRelated.Read_PKG(sourcePkg);
                Param_SFO.PARAM_SFO psfo = readPkg.Param;
                string pkgFileName_ = Path.GetFileName(sourcePkg);
                string directoryName = Path.GetDirectoryName(sourcePkg);
                string cell0 = row.Cells[0].Value.ToString();
                string cell12 = row.Cells[12].Value.ToString();

                if (cell0 == pkgFileName_ && cell12 == directoryName)
                {
                    File.Move(sourcePkg, targetPkg);
                    PKGGridView.Invoke((Action)(() =>
                    {
                        int currentRowIndex = row.Index;
                        DataGridViewRow currentRow = PKGGridView.Rows[currentRowIndex];
                        currentRow.Cells[0].Value = newPkgName + ".pkg";
                    }));
                    break;
                }
            }
        }

        #region RenameAllPkg

        #endregion RenameAllPkg

        #region RenameSelectedPKG

        #endregion RenameSelectedPKG
        #endregion renamePKG
        #endregion PKGBasicOperation_Copy_Rename_Delete_ViewExplorer

        #region PKGSender
        private void DisableControls_PkgSender()
        {
            //status bar - file
            managePS4PKGToolStripMenuItem.Enabled = false;
            reloadContentToolStripMenuItem.Enabled = false;
            exitToolStripMenuItem1.Enabled = false;

            //statusbar - tool
            renameToolStripMenuItem.Enabled = false;
            extractImageAndBackgroundToolStripMenuItem.Enabled = false;
            globalExportPKGListToExcelToolStripMenuItem1.Enabled = false;
            globalCopyStripMenuItem.Enabled = false;
            deletePKGtoolStripMenuItem1.Enabled = false;
            viewPkgExplorerStripMenuItem1.Enabled = false;
            renameCurrentPKGStripMenuItem.Enabled = false;
            RpiCheckPkgInstalledtoolStripMenuItem1.Enabled = false;
            uninstallPKGFromPS4ToolStripMenuItem.Enabled = false;

            //contextmenu
            toolStripMenuItem96.Enabled = false;
            toolStripMenuItem111.Enabled = false;
            toolStripMenuItem3.Enabled = false;
            globalExportPKGListToExcelToolStripMenuItem2.Enabled = false;
            deletePkgtoolStripMenuItem2.Enabled = false;
            toolStripMenuItem133.Enabled = false;
            toolStripMenuItem127.Enabled = false;
            viewPkgExplorerStripMenuItem2.Enabled = false;
            RpiCheckPkgInstalledtoolStripMenuItem2.Enabled = false;
            toolStripMenuItem21.Enabled = false;
        }

        private void EnableControls_PkgSender()
        {
            //status bar - file
            managePS4PKGToolStripMenuItem.Enabled = true;
            reloadContentToolStripMenuItem.Enabled = true;
            exitToolStripMenuItem1.Enabled = true;

            //statusbar - tool
            renameToolStripMenuItem.Enabled = true;
            extractImageAndBackgroundToolStripMenuItem.Enabled = true;
            globalExportPKGListToExcelToolStripMenuItem1.Enabled = true;
            globalCopyStripMenuItem.Enabled = true;
            deletePKGtoolStripMenuItem1.Enabled = true;
            viewPkgExplorerStripMenuItem1.Enabled = true;
            renameCurrentPKGStripMenuItem.Enabled = true;
            RpiCheckPkgInstalledtoolStripMenuItem1.Enabled = true;
            uninstallPKGFromPS4ToolStripMenuItem.Enabled = true;

            //contextmenu
            toolStripMenuItem96.Enabled = true;
            toolStripMenuItem111.Enabled = true;
            toolStripMenuItem3.Enabled = true;
            globalExportPKGListToExcelToolStripMenuItem2.Enabled = true;
            deletePkgtoolStripMenuItem2.Enabled = true;
            toolStripMenuItem133.Enabled = true;
            toolStripMenuItem127.Enabled = true;
            viewPkgExplorerStripMenuItem2.Enabled = true;
            RpiCheckPkgInstalledtoolStripMenuItem2.Enabled = true;
            toolStripMenuItem21.Enabled = true;
        }

        private void InitializePKGSender()
        {
            if (RpiSendPkgtoolStripMenuItem2.Text == "Send PKG to PS4")
            {
                DisableTabPages(flatTabControl1, "tabPage1");
                DisableControls(darkMenuStrip1);
                DisableControls_PkgSender();

                PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
                Param_SFO.PARAM_SFO psfo = read.Param;

                Logger.LogInformation("Sending " + read.PS4_Title + " to PS4..");

                // Update 'Settings.PKG.SelectedPKGFilename'
                GetSelectedPKGPath();

                PKGSENDER.taskMonitorIsCancelling = false;

                // Check if pkg installed
                if (read.PKG_Type.ToString() == PKGCategory.GAME || read.PKG_Type.ToString() == PKGCategory.PATCH)
                {
                    // Check if pkg exists for game pkg
                    PKGSENDER.JSON.CHECKAPPEXISTS.baseAppExist = true;

                    dynamic appExistsJson = null;
                    appExistsJson = PKGSENDER.CheckIfPkgInstalled(psfo);
                    if (appExistsJson == null)
                    {
                        ShowError("An error occurred while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", true);
                        EnableControls_PkgSender();
                        EnableTabPages(flatTabControl1);
                        EnableControls(darkMenuStrip1);
                        return;
                    }

                    PKGSENDER.JSON.CHECKAPPEXISTS.status = appExistsJson.status.ToString();

                    if (PKGSENDER.JSON.CHECKAPPEXISTS.status == "success")
                    {
                        PKGSENDER.JSON.CHECKAPPEXISTS.exists = appExistsJson.exists.ToString();
                        if (PKGSENDER.JSON.CHECKAPPEXISTS.exists == "true")
                        {
                            if (read.PKG_Type.ToString() == PKGCategory.GAME)
                            {
                                ShowInformation("PKG already installed.", true);
                                EnableControls_PkgSender();
                                EnableTabPages(flatTabControl1);
                                EnableControls(darkMenuStrip1);
                                return;
                            }
                        }
                        else
                        {
                            if (read.PKG_Type.ToString() == PKGCategory.PATCH)
                            {
                                PKGSENDER.JSON.CHECKAPPEXISTS.baseAppExist = false;
                            }
                        }
                    }
                }

                if (read.PKG_Type.ToString() == PKGCategory.ADDON)
                {
                    PKGSENDER.JSON.CHECKAPPEXISTS.baseAppExist = true;
                }

                toolStripMenuItem18.Text = "Remote PKG Installer | Status : Running";
                RpiSendPkgtoolStripMenuItem2.Text = "Stop Current Operation";
                toolStripMenuItem16.Text = "Remote PKG Installer | Status : Running";
                RpiSendPkgtoolStripMenuItem1.Text = "Stop Current Operation";
                toolStripStatusLabel2.Text = "Sending " + read.PS4_Title + " to PS4..";
                SendPKG();
            }
            else
            {
                Logger.LogInformation("Cancelling operation..");
                // Cancel current operation
                if (PKGSENDER.isPreparing)
                {
                    ShowWarning("Cannot cancel operation while preparing.", true);
                    return;
                }

                dynamic stopTaskJson = null;
                stopTaskJson = PKGSENDER.StopTask();
                if (stopTaskJson == null)
                {
                    ShowError("An error occurred while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", true);
                    EnableControls_PkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    return;
                }

                PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
                Param_SFO.PARAM_SFO psfo = read.Param;

                PKGSENDER.JSON.STOPTASK.status = stopTaskJson.status.ToString();
                if (PKGSENDER.JSON.STOPTASK.status == "success")
                {
                    // If stopping success, uninstall stopped game 
                    dynamic uninstallAppJson = null;
                    uninstallAppJson = PKGSENDER.UninstallGame(psfo);
                    if (uninstallAppJson == null)
                    {
                        ShowError("An error occurred while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", true);
                        EnableControls_PkgSender();
                        EnableTabPages(flatTabControl1);
                        EnableControls(darkMenuStrip1);
                        return;
                    }

                    PKGSENDER.JSON.UNINTSALLAPP.status = uninstallAppJson.status.ToString();

                    // Cancel running background workers
                    PKGSENDER.MonitorPkgSenderTaskBackgroundWorker.CancelAsync();
                    SendPKG();
                    EnableControls_PkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    darkStatusStrip1.Invoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel2.Text = "...";
                        toolStripProgressBar1.Value = 0;
                    });

                    ShowInformation("Operation stopped.", true);
                }
                else
                {
                    ShowError("Failed to stop current operation.", true);
                }
            }
        }

        private async Task CheckIfAppInstalledOnPS4()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            Logger.LogInformation("Checking if base PKG installed on PS4 (" + read.PS4_Title + ")..");

            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            DisableControls_PkgSender();

            dynamic app_exists_json = null;

            try
            {
                app_exists_json = await PKGSENDER.CheckIfPkgInstalled(psfo);
                if (app_exists_json == null)
                {
                    ShowError("An error occurred while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", true);
                    EnableControls_PkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    return;
                }

                PKGSENDER.JSON.CHECKAPPEXISTS.status = app_exists_json.status.ToString();

                if (PKGSENDER.JSON.CHECKAPPEXISTS.status == "success")
                {
                    EnableControls_PkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    PKGSENDER.JSON.CHECKAPPEXISTS.exists = app_exists_json.exists.ToString();
                    if (PKGSENDER.JSON.CHECKAPPEXISTS.exists == "true")
                    {
                        ShowInformation("PKG already installed.", true);
                    }
                    else
                    {
                        ShowInformation("PKG is not installed.", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred: " + ex.Message, true);
            }
        }

        private void SendPKG()
        {
            var bg = new BackgroundWorker();
            bg.DoWork += delegate (object sender, DoWorkEventArgs e)
            {
                renameBackFile = false;
                var currentPkgFile = PKG.SelectedPKGFilename;
                send_pkg_json = null;
                PKGSENDER.pkgSendDone = false;
                PKGSENDER.pkgSendStopped = false;
                PKGSENDER.JSON.SENDPKG.status = "";
                PKGSENDER.JSON.SENDPKG.task_id = "";
                PKGSENDER.JSON.SENDPKG.title = "";
                PKGSENDER.JSON.SENDPKG.title_id = "";

                // Kill server if running
                Tool.KillNodeJS();

                PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(currentPkgFile);
                Param_SFO.PARAM_SFO psfo = read.Param;
                var tempFilename = read.Content_ID + "_" + read.PKG_Type.ToString() + "_Send_To_PS4.pkg";

                // Get directory
                var directory = Path.GetDirectoryName(currentPkgFile);

                // Get original filename
                var originalName = Path.GetFileName(currentPkgFile);

                // Rename to temp filename
                if (currentPkgFile != directory + @"\" + tempFilename)
                {
                    File.Move(currentPkgFile, directory + @"\" + tempFilename);
                }

                // Update filename in gridview
                foreach (DataGridViewCell cell in PKGGridView.SelectedCells)
                {
                    int selectedRowIndex = cell.RowIndex;
                    DataGridViewRow selectedRow = PKGGridView.Rows[selectedRowIndex];
                    selectedRow.Cells[0].Value = tempFilename;
                }

                TEMPFILENAMESENDPKG = directory + @"\" + tempFilename;
                PKG.SelectedPKGFilename = TEMPFILENAMESENDPKG;

                // Run server
                PKGSENDER.RunServer(directory);

                // Send pkg
                send_pkg_json = PKGSENDER.SendPKG(tempFilename);
                if (send_pkg_json == null)
                {
                    ShowError("An error occurred while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", true);
                    EnableControls_PkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    toolStripMenuItem18.Text = "Remote PKG Installer | Status : Idle";
                    RpiSendPkgtoolStripMenuItem2.Text = "Send PKG to PS4";
                    toolStripMenuItem16.Text = "Remote PKG Installer | Status : Idle";
                    RpiSendPkgtoolStripMenuItem1.Text = "Send PKG to PS4";
                    toolStripStatusLabel2.Text = "...";
                    toolStripProgressBar1.Value = 0;
                    return;
                }

                PKGSENDER.JSON.SENDPKG.status = send_pkg_json.status.ToString();

                if (PKGSENDER.JSON.SENDPKG.status == "success")
                {
                    PKGSENDER.JSON.SENDPKG.status = send_pkg_json.status.ToString();
                    PKGSENDER.JSON.SENDPKG.task_id = send_pkg_json.task_id.ToString();
                    PKGSENDER.JSON.SENDPKG.title = send_pkg_json.title.ToString();
                    PKGSENDER.JSON.SENDPKG.title_id = psfo.TitleID.ToUpper();
                    PKGSENDER.MonitorPkgSenderTaskBackgroundWorker = new BackgroundWorker();
                    PKGSENDER.MonitorPkgSenderTaskBackgroundWorker.WorkerSupportsCancellation = true;
                    MonitorPKGSenderTask(PKGSENDER.MonitorPkgSenderTaskBackgroundWorker);
                }
                else if (PKGSENDER.JSON.SENDPKG.status != "fail")
                {
                    PKGSENDER.JSON.SENDPKG.error = send_pkg_json.error.ToString();
                    ShowError("Operation failed : \n\nStatus : " + PKGSENDER.JSON.SENDPKG.status + "\n" + PKGSENDER.JSON.SENDPKG.error, true);
                    toolStripMenuItem18.Text = "Remote PKG Installer | Status : Idle";
                    RpiSendPkgtoolStripMenuItem2.Text = "Send PKG to PS4";
                    toolStripMenuItem16.Text = "Remote PKG Installer | Status : Idle";
                    RpiSendPkgtoolStripMenuItem1.Text = "Send PKG to PS4";
                    toolStripStatusLabel2.Text = "...";
                    toolStripProgressBar1.Value = 0;
                    EnableControls_PkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    return;
                }

                while (true)
                {
                    if (bg.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    if (PKGSENDER.pkgSendStopped)
                    {
                        break;
                    }

                    if (renameBackFile)
                    {
                        break;
                    }
                }

                // Update original filename in gridview
                foreach (DataGridViewRow row in PKGGridView.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(tempFilename))
                    {
                        row.Cells[0].Value = originalName;
                    }
                }

                // Rename original filename
                File.Move(TEMPFILENAMESENDPKG, currentPkgFile);
                PKG.SelectedPKGFilename = currentPkgFile;
            };
            bg.RunWorkerCompleted += delegate
            {
            };
            bg.RunWorkerAsync();
        }

        private async void Rpi_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            Logger.LogInformation("Checking RPI requirement..");
            var CheckRequirement = PKGSENDER.CheckRequirement();
            if (CheckRequirement != "OK")
            {
                ShowError(CheckRequirement, true);
                EnableControls_PkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                return;
            }

            if (clickedMenuItem == RpiCheckPkgInstalledtoolStripMenuItem1 || clickedMenuItem == RpiCheckPkgInstalledtoolStripMenuItem2)
            {
                await CheckIfAppInstalledOnPS4();
            }
            if (clickedMenuItem == RpiSendPkgtoolStripMenuItem1 || clickedMenuItem == RpiSendPkgtoolStripMenuItem2)
            {
                InitializePKGSender();
            }
            if (clickedMenuItem == RpiUninstallBasePKGToolStripMenuItem1 || clickedMenuItem == RpiUninstallBasePKGToolStripMenuItem2)
            {
                UninstallBasePkgFromPs4();
            }
            if (clickedMenuItem == RpiUninstallPatchPKGToolStripMenuItem1 || clickedMenuItem == RpiUninstallPatchPKGToolStripMenuItem2)
            {
                UninstallPatchPkgFromPs4();
            }
            if (clickedMenuItem == RpiUninstallDlcPKGToolStripMenuItem1 || clickedMenuItem == RpiUninstallDlcPKGToolStripMenuItem2)
            {
                UninstallDlcPkgFromPs4();
            }
            if (clickedMenuItem == RpiUninstallThemePKGToolStripMenuItem1 || clickedMenuItem == RpiUninstallThemePKGToolStripMenuItem2)
            {
                UninstallThemePkgFromPs4();
            }
        }

        private void MonitorPKGSenderTask(BackgroundWorker bg)
        {
            bg.DoWork += delegate (object sender, DoWorkEventArgs e)
            {
                dynamic taskProgressJson = null;

                darkStatusStrip1.Invoke((MethodInvoker)delegate
                {
                    toolStripStatusLabel2.Text = "Preparing download..";
                    Logger.LogInformation("Preparing download..");
                    darkStatusStrip1.Refresh();
                });

                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        PKGSENDER.isPreparing = true;

                        // Monitor task progress
                        taskProgressJson = PKGSENDER.GetTaskProgress();
                        if (taskProgressJson == null)
                        {
                            // Handle the null case
                        }

                        PKGSENDER.JSON.MONITORTASK.packagePreparingTotal = Convert.ToInt32(taskProgressJson.preparing_percent.ToString());

                        if (PKGSENDER.JSON.MONITORTASK.packagePreparingTotal == 100)
                        {
                            break;
                        }
                    }
                    catch
                    {
                        // Handle exceptions
                    }
                }

                PKGSENDER.isPreparing = false;

                taskProgressJson = PKGSENDER.GetTaskProgress();
                if (taskProgressJson == null)
                {
                    // Handle the null case
                }

                PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal = taskProgressJson.length.ToString();
                PKGSENDER.JSON.MONITORTASK.packageTransferredTotal = taskProgressJson.transferred.ToString();
                PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal = taskProgressJson.rest_sec_total.ToString();
                toolStripProgressBar1.Maximum = Convert.ToInt32(PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal);
                int totalRemainTime = toolStripProgressBar1.Maximum;
                int increment = 0;

                for (long i = Convert.ToInt64(PKGSENDER.JSON.MONITORTASK.packageTransferredTotal); i < Convert.ToInt64(PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal); i++)
                {
                    try
                    {
                        PKGSENDER.isPreparing = false;

                        if (bg.CancellationPending)
                        {
                            e.Cancel = true;
                            PKGSENDER.taskMonitorIsCancelling = false;
                            break;
                        }

                        if (PKGSENDER.taskMonitorIsCancelling)
                        {
                            PKGSENDER.taskMonitorIsCancelling = false;
                            break;
                        }

                        taskProgressJson = PKGSENDER.GetTaskProgress();
                        if (taskProgressJson == null)
                        {
                            // Handle the null case
                        }

                        if (taskProgressJson.status.ToString() == "fail")
                        {
                            PKGSENDER.pkgSendStopped = true;
                            break;
                        }

                        PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal = taskProgressJson.length.ToString();
                        PKGSENDER.JSON.MONITORTASK.packageTransferredTotal = taskProgressJson.transferred.ToString();
                        PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal = taskProgressJson.rest_sec_total.ToString();

                        long transferredTotal = Convert.ToInt64(PKGSENDER.JSON.MONITORTASK.packageTransferredTotal);
                        long filesizeTotal = Convert.ToInt64(PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal);
                        var packageTransferredTotalFormatted = ByteSize.FromBytes(transferredTotal).ToString();
                        var packageFilesizeTotalFormatted = ByteSize.FromBytes(filesizeTotal).ToString();

                        if (Convert.ToInt32(PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal) == 0)
                        {
                            toolStripProgressBar1.Value = 0;
                            break;
                        }

                        increment = totalRemainTime - Convert.ToInt32(PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal);

                        darkStatusStrip1.Invoke((MethodInvoker)delegate
                        {
                            toolStripProgressBar1.Increment(increment);
                            toolStripStatusLabel2.Text = $"Installing.. ({packageTransferredTotalFormatted}/{packageFilesizeTotalFormatted})";
                            Logger.LogInformation($"Installing.. ({packageTransferredTotalFormatted}/{packageFilesizeTotalFormatted})");
                            darkStatusStrip1.Refresh();
                        });

                        totalRemainTime = Convert.ToInt32(PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal);
                    }
                    catch
                    {
                        // Handle exceptions
                    }
                }

                if (Convert.ToInt32(PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal) == 0)
                {
                    PKGSENDER.pkgSendDone = true;
                }

            };
            bg.ProgressChanged += delegate (object sender, ProgressChangedEventArgs progressChangedEventArgs)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    darkStatusStrip1.Invoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel2.Text = progressChangedEventArgs.UserState.ToString();
                        toolStripProgressBar1.Value = progressChangedEventArgs.ProgressPercentage;
                        darkStatusStrip1.Refresh();
                    });
                });
            };
            bg.RunWorkerCompleted += delegate
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EnableControls_PkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    runworker = true;
                    toolStripStatusLabel2.Text = "...";
                    toolStripProgressBar1.Value = 0;

                    if (PKGSENDER.pkgSendStopped || PKGSENDER.taskMonitorIsCancelling)
                    {
                        ShowError("Operation stopped.", true);
                    }

                    if (PKGSENDER.pkgSendDone)
                    {
                        if (!PKGSENDER.JSON.CHECKAPPEXISTS.baseAppExist)
                        {
                            ShowInformation("Patch PKG sent to PS4. Manually install it after base PKG is installed : Notifications -> Downloads", true);
                        }
                        else
                        {
                            ShowInformation("PKG installed.", true);
                        }
                    }

                    renameBackFile = true;

                    // Kill the server if it is running.
                    // killNodeJS();

                    toolStripMenuItem18.Text = "Remote PKG Installer | Status : Idle";
                    RpiSendPkgtoolStripMenuItem2.Text = "Send PKG to PS4";
                    toolStripMenuItem16.Text = "Remote PKG Installer | Status : Idle";
                    RpiSendPkgtoolStripMenuItem1.Text = "Send PKG to PS4";
                });
            };
            bg.RunWorkerAsync();
        }

        private void UninstallDlcPkgFromPs4()
        {
            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            DisableControls_PkgSender();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            Logger.LogInformation("Uninstalling addon PKG (" + read.PS4_Title + ")..");

            // Uninstall installed addon pkg

            dynamic uninstall_patch_json = null;

            uninstall_patch_json = PKGSENDER.UninstallAddonTheme(psfo);
            if (uninstall_patch_json == null)
            {
                ShowError("An error occurred while trying to communicate with the PS4. Launch/restart the Remote Package Installer application on the PS4 and do not minimize it.", true);
                EnableControls_PkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                return;
            }

            EnableControls_PkgSender();
            EnableTabPages(flatTabControl1);
            EnableControls(darkMenuStrip1);
            PKGSENDER.JSON.UNINTSALLADDON.status = uninstall_patch_json.status.ToString();

            if (PKGSENDER.JSON.UNINTSALLADDON.status == "success")
            {
                ShowInformation("PKG uninstalled.", true);
            }
            else
            {
                ShowError("Uninstall failed.", true);
            }
        }

        private void UninstallThemePkgFromPs4()
        {
            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            DisableControls_PkgSender();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            Logger.LogInformation("Uninstalling theme PKG (" + read.PS4_Title + ")..");

            // Uninstall installed theme pkg

            dynamic uninstall_theme_json = null;

            uninstall_theme_json = PKGSENDER.UninstallAddonTheme(psfo);
            if (uninstall_theme_json == null)
            {
                ShowError("An error occurred while trying to communicate with the PS4. Launch/restart the Remote Package Installer application on the PS4 and do not minimize it.", true);
                EnableControls_PkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                return;
            }

            PKGSENDER.JSON.UNINTSALLTHEME.status = uninstall_theme_json.status.ToString();

            EnableControls_PkgSender();
            EnableTabPages(flatTabControl1);
            EnableControls(darkMenuStrip1);

            if (PKGSENDER.JSON.UNINTSALLTHEME.status == "success")
            {
                ShowInformation("PKG uninstalled.", true);
            }
            else
            {
                ShowError("Uninstall failed.", true);
            }
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            UninstallDlcPkgFromPs4();
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            UninstallThemePkgFromPs4();
        }

        private void UninstallBasePkgFromPs4()
        {
            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            DisableControls_PkgSender();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            Logger.LogInformation("Uninstalling base PKG (" + read.PS4_Title + ")..");

            // Check if pkg is installed

            dynamic app_exists_json = null;

            app_exists_json = PKGSENDER.CheckIfPkgInstalled(psfo);
            if (app_exists_json == null)
            {
                ShowError("An error occurred while trying to communicate with the PS4. Launch/restart the Remote Package Installer application on the PS4 and do not minimize it.", true);
                EnableControls_PkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                return;
            }

            PKGSENDER.JSON.CHECKAPPEXISTS.status = app_exists_json.status.ToString();

            if (PKGSENDER.JSON.CHECKAPPEXISTS.status == "success")
            {
                PKGSENDER.JSON.CHECKAPPEXISTS.exists = app_exists_json.exists.ToString();
                if (PKGSENDER.JSON.CHECKAPPEXISTS.exists == "false")
                {
                    ShowInformation("PKG is not installed.", true);
                }
                else
                {
                    // Uninstall installed pkg

                    dynamic uninstall_app_json = null;
                    uninstall_app_json = PKGSENDER.UninstallGame(psfo);
                    if (uninstall_app_json == null)
                    {
                        ShowError("An error occurred while trying to communicate with the PS4. Launch/restart the Remote Package Installer application on the PS4 and do not minimize it.", true);
                        EnableControls_PkgSender();
                        EnableTabPages(flatTabControl1);
                        EnableControls(darkMenuStrip1);
                        return;
                    }

                    EnableControls_PkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    PKGSENDER.JSON.UNINTSALLAPP.status = uninstall_app_json.status.ToString();

                    if (PKGSENDER.JSON.UNINTSALLAPP.status == "success")
                    {
                        ShowInformation("PKG uninstalled.", true);
                    }
                    else
                    {
                        ShowError("Uninstall failed.", true);
                    }
                }
            }
        }

        private void UninstallPatchPkgFromPs4()
        {
            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            DisableControls_PkgSender();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            Logger.LogInformation("Uninstalling patch PKG (" + read.PS4_Title + ")..");

            // Uninstall installed patch pkg

            dynamic uninstall_patch_json = null;

            uninstall_patch_json = PKGSENDER.UninstallPatch(psfo);
            if (uninstall_patch_json == null)
            {
                ShowError("An error occurred while trying to communicate with the PS4. Launch/restart the Remote Package Installer application on the PS4 and do not minimize it.", true);
                EnableControls_PkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                return;
            }

            PKGSENDER.JSON.UNINTSALLPATCH.status = uninstall_patch_json.status.ToString();

            EnableControls_PkgSender();
            EnableTabPages(flatTabControl1);
            EnableControls(darkMenuStrip1);

            if (PKGSENDER.JSON.UNINTSALLPATCH.status == "success")
            {
                ShowInformation("PKG uninstalled.", true);
            }
            else
            {
                ShowError("Uninstall failed.", true);
            }
        }
        #endregion PKGSender


        private void OpenProgramSettings()
        {
            Logger.LogInformation("Opening Program Settings..");

            ProgramSetting form = new ProgramSetting();
            form.ShowDialog();
            this.BringToFront();

            UpdatePKGColorLabel();

            if (form.Refresh)
            {
                RefreshPkgList();
            }
            else
            {
                #region checkGridHideUnhide
                UpdateDataGridViewColumnVisibility();
                SetBackgroundMusicVolume();
                #endregion checkGridHideUnhide
            }
        }

        private void ExtractTrophyIcon()
        {
            if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
            {
                Logger.LogInformation("Extracting trophy icon..");
                string failExtract = "";

                if (Trophy.TrophyFilenameToExtractList.Count > 1)
                {
                    var trophyInfo = Trophy.TrophyFilenameToExtractList.Zip(Trophy.ImageToExtractList, (name, image) => new { Name = name, Image = image });

                    foreach (var info in trophyInfo)
                    {
                        try
                        {
                            using (Bitmap tempImage = new Bitmap(Helper.Bitmap.BytesToImage(Trophy.trophy.ExtractFileToMemory(info.Name))))
                            {
                                tempImage.Save(Path.Combine(fbd.SelectedPath, info.Name), ImageFormat.Png);
                            }
                        }
                        catch (Exception ex)
                        {
                            failExtract += ex.Message + "\n";
                        }
                    }

                    if (string.IsNullOrEmpty(failExtract))
                    {
                        ShowInformation("Trophy icons extracted.", true);
                    }
                    else
                    {
                        ShowError("Some trophy icons failed to extract.", true);
                        //Logger.LogError(failExtract);
                    }
                }
                else
                {
                    ShowError("Error occured when trying to extract trophy icons.", true);
                }
            }
        }

        private void ExtractTrophyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractTrophyIcon();
        }

        private void ContextMenuBackgroundImage_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            // save image
            if (clickedMenuItem == saveImageToolStripMenuItem && flatTabControlBgi.SelectedTab == tabPagePic0)
            {
                SaveBackgroundImage(pbPIC0);
            }
            if (clickedMenuItem == saveImageToolStripMenuItem && flatTabControlBgi.SelectedTab == tabPagePic1)
            {
                SaveBackgroundImage(pbPIC1);
            }

            // set image as background 
            if (clickedMenuItem == SetImageAsDesktopBackgroundToolStripMenuItem && flatTabControlBgi.SelectedTab == tabPagePic0)
            {
                SetImageAsDesktopBackground(pbPIC0);
            }
            if (clickedMenuItem == SetImageAsDesktopBackgroundToolStripMenuItem && flatTabControlBgi.SelectedTab == tabPagePic1)
            {
                SetImageAsDesktopBackground(pbPIC1);
            }
        }

        private void SaveBackgroundImage(PictureBox pb)
        {
            try
            {
                if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
                {
                    using (Bitmap tempImage = new Bitmap(pb.Image))
                    {
                        string pic = pb.Name == "pbPIC0" ? "PIC0" : "PIC1";
                        string filePath = Path.Combine(fbd.SelectedPath, $"{PKG.CurrentPKGTitle}_{pic}.PNG");
                        tempImage.Save(filePath, ImageFormat.Png);
                        ShowInformation("Background image saved.", false);
                        Logger.LogInformation($"Background image saved to \"{fbd.SelectedPath}\".");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Failed to save background image: {ex.Message}", true);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

        private void SetImageAsDesktopBackground(PictureBox pb)
        {
            try
            {
                if (pb.Image == null)
                    return;

                using (Bitmap tempImage = new Bitmap(pb.Image))
                {
                    string savedImagePath = Path.Combine(PS4PKGToolTempDirectory, "Wallpaper");
                    Directory.CreateDirectory(savedImagePath);
                    string imagePath = Path.Combine(savedImagePath, "Wallpaper.JPG");

                    tempImage.Save(imagePath, ImageFormat.Jpeg);

                    SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, imagePath, SPIF_UPDATEINIFILE);
                    Logger.LogInformation("Image set as desktop background.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occurred: " + ex.Message);
            }
        }


        private void darkDataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            this.TrophyGridView.ClearSelection();
        }

        private void ExtractDecryptedEntry()
        {
            if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);

                Logger.LogInformation("Extracting decrypted items..");
                //load pkg file
                string itemIndex = "";
                var IO = new EndianIO(PKG.SelectedPKGFilename, EndianType.BigEndian, true);
                long file_length = IO.Length;
                if (file_length < 0x1C)
                {
                    IO.Close();
                    return;
                }

                //set output path for extracted files
                string path2pkg = Path.GetDirectoryName(PKG.SelectedPKGFilename);
                string fullpkgpath = Path.GetFullPath(PKG.SelectedPKGFilename);
                string pkgbasename = Path.GetFileNameWithoutExtension(PKG.SelectedPKGFilename);
                string pkgfilename = Path.GetFileName(PKG.SelectedPKGFilename);
                string outputpath = fbd.SelectedPath; // Path.Combine(path2pkg, pkgbasename);
                // textBox1.AppendText("\r\n\r\npath2pkg:   " + path2pkg);     //  C:\Downloads\ps4packages\
                // textBox1.AppendText("\r\nfullpkgpath:   " + fullpkgpath);   //  C:\Downloads\ps4packages\Up1018...V0100.pkg
                // textBox1.AppendText("\r\npkgbasename:   " + pkgbasename);   //  Up1018...V0100
                // textBox1.AppendText("\r\npkgfilename:   " + pkgfilename);   //  Up1018...V0100.pkg
                //textBox1.AppendText("\r\n\r\noutput path:\r\n" + outputpath); //  C:\Downloads\ps4packages\Up1018...V0100 
                Tool.CreateDirectoryIfNotExists(outputpath);

                //read and decrypt part 1 of key seed
                if (file_length < (0x2400 + 0x100))
                {
                    IO.Close();
                    return;
                }
                IO.SeekTo(0x2400);
                byte[] data = Entry.Decrypt(IO.In.ReadBytes(256));
                for (int j = 0; j < data.Length; j++)
                {

                }

                //read file entry table
                uint entry_count = IO.In.SeekNReadUInt32(0x10);
                uint file_table_offset = IO.In.SeekNReadUInt32(0x18);
                uint padded_size;

                uint strtab_count = 0;
                uint strtab_offset = 0;
                uint strtab_size = 0;

                if (file_length < (file_table_offset + (0x20 * entry_count)))
                {
                    IO.Close();
                    return;
                }
                IO.SeekTo(file_table_offset);
                PackageEntry[] entry = new PackageEntry[entry_count];
                for (int i = 0; i < entry_count; i++)
                {
                    entry[i].type = IO.In.ReadUInt32();
                    entry[i].unk1 = IO.In.ReadUInt32();
                    entry[i].flags1 = IO.In.ReadUInt32();
                    entry[i].flags2 = IO.In.ReadUInt32();
                    entry[i].offset = IO.In.ReadUInt32();
                    entry[i].size = IO.In.ReadUInt32();
                    entry[i].padding = IO.In.ReadBytes(8);

                    //set key index, encryption flag, string table properties
                    entry[i].key_index = ((entry[i].flags2 & 0xF000) >> 12);
                    entry[i].is_encrypted = ((entry[i].flags1 & 0x80000000) != 0) ? true : false;
                    if (entry[i].unk1 != 0) strtab_count++;
                    if (entry[i].type == 0x200)
                    {
                        strtab_offset = entry[i].offset;
                        strtab_size = entry[i].size;
                    }
                }

                //read strtab
                if (file_length < (strtab_offset + strtab_size))
                {
                    IO.Close();
                    return;
                }
                string[] entry_name = new string[entry_count];
                if (strtab_count > 0)
                {
                    IO.SeekTo(strtab_offset);
                    byte[] string_table = IO.In.ReadBytes(strtab_size);
                    for (int i = 0; i < entry_count - 1; i++)
                    {
                        if (entry[i].unk1 != 0x00)
                        { //has strtab entry
                            entry_name[i] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[i].unk1), (Convert.ToInt32(entry[i + 1].unk1) - 1) - Convert.ToInt32(entry[i].unk1));
                        }
                        else
                        {
                            entry_name[i] = "";
                        }
                    }
                    if (entry[entry_count - 1].unk1 != 0x00)
                    {
                        entry_name[entry_count - 1] = System.Text.Encoding.UTF8.GetString(string_table, Convert.ToInt32(entry[entry_count - 1].unk1), (Convert.ToInt32(strtab_size) - 1) - Convert.ToInt32(entry[entry_count - 1].unk1));
                    }
                    else
                    {
                        entry_name[entry_count - 1] = "";
                    }
                }
                else
                {
                    for (int i = 0; i < entry_count; i++) entry_name[i] = "";
                }

                var errorExtract = new Dictionary<string, string>();

                for (int i = 0; i < entry_count; i++)
                {
                    string savepath;
                    string savename;
                    string extrasavepath;

                    if (file_length < (entry[i].offset + entry[i].size))
                    {
                        IO.Close();
                        return;
                    }

                    if (entry[i].is_encrypted != false)
                    {
                        //print file attributes


                        //combine file entry and rsa decrypted data to form key seed
                        byte[] entry_data = new byte[0x40];
                        Array.Copy(entry[i].ToArray(), entry_data, 0x20);
                        Array.Copy(data, 0, entry_data, 0x20, 0x20);

                        //use sha256 to transform seed into aes iv and key
                        byte[] iv = new byte[0x10], key = new byte[0x10];
                        byte[] hash = Sha256(entry_data, 0, entry_data.Length);
                        Array.Copy(hash, 0, iv, 0, 0x10);
                        Array.Copy(hash, 0x10, key, 0, 0x10);

                        //output aes key and iv for current file


                        //read and decrypt current file
                        IO.In.BaseStream.Position = entry[i].offset;
                        if ((entry[i].size % 16) != 0)
                            padded_size = entry[i].size + (16 - (entry[i].size % 16));
                        else padded_size = entry[i].size;

                        //decrypt file
                        byte[] file_data = DecryptAes(key, iv, IO.In.ReadBytes(padded_size));

                        var entryOffset = entry[i].offset.ToString("X8");




                        var entryName = "";

                        var test = EncryptedEntryOffsetNameDictionary;
                        foreach (var no in EncryptedEntryOffsetNameDictionary)
                        {
                            var offset = no.Key.Replace(" ", "");
                            entryName = no.Value;

                            if (entry[i].offset.ToString("X8") == offset.ToString())
                            {
                                entryOffset = offset;
                            }

                            try
                            {
                                //save decrypted data to file
                                savepath = Path.Combine(outputpath, entryName);

                                Array.Resize(ref file_data, Convert.ToInt32(entry[i].size));
                                File.WriteAllBytes(savepath, file_data);  //closes after write
                            }
                            catch (Exception a)
                            {
                                errorExtract.Add(entryName, a.Message);
                            }
                        }

                    }
                }
                IO.Close();
                if (errorExtract.Count > 0)
                {
                    ShowWarning("Failed to extract some entries. See logs.", false);
                    Logger.LogWarning($"Failed to extract some entries:\n{string.Join("\n", errorExtract)}");
                }
                else
                {
                    ShowInformation("All decrypted entries extracted.", true);
                }
            }
        }

        private void ExtractDecryptedEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractDecryptedEntry();
        }

        private void ExtractAllEntry()
        {
            if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
            {
                Logger.LogInformation("Extracting PKG entries..");

                try
                {
                    List<string> errorEncryptedEntries = new List<string>();
                    foreach (var entry in EntryIdNameDictionary)
                    {
                        var pkgPath = PKG.SelectedPKGFilename;
                        var idx = int.Parse(entry.Key);
                        var name = entry.Value;
                        var outPath = fbd.SelectedPath + "\\" + name.Replace("_SHA", ".SHA").Replace("_DAT", ".DAT").Replace("_SFO", ".SFO").Replace("_XML", ".XML").Replace("_SIG", ".SIG").Replace("_PNG", ".PNG").Replace("_JSON", ".JSON").Replace("_DDS", ".DDS").Replace("_TRP", ".TRP").Replace("_AT9", ".AT9");

                        using (var pkgFile = File.OpenRead(pkgPath))
                        {
                            var pkg = new PkgReader(pkgFile).ReadPkg();
                            if (idx < 0 || idx >= pkg.Metas.Metas.Count)
                            {
                                ShowError("Error: entry number out of range.", true);
                                return;
                            }
                            using (var outFile = File.Create(outPath))
                            {
                                try
                                {
                                    var meta = pkg.Metas.Metas[idx];
                                    outFile.SetLength(meta.DataSize);
                                    if (meta.Encrypted)
                                    {
                                        // Logger.log(name + " : Warning: Entry is encrypted but no passcode was provided! Saving encrypted bytes.");
                                        errorEncryptedEntries.Add(name);
                                    }
                                    new SubStream(pkgFile, meta.DataOffset, meta.DataSize).CopyTo(outFile);
                                }
                                catch (Exception ex)
                                {
                                    Logger.LogError($"Error extracting {name} : {ex.Message}");
                                }
                            }
                        }
                    }

                    if (errorEncryptedEntries.Count> 0)
                    {
                        ShowWarning("Failed to extract some entries due to encrypted. See logs.", false);
                        Logger.LogWarning($"Failed to extract some entries due to encryption:\n{string.Join("\n", errorEncryptedEntries)}");
                    }
                    else
                    {
                        ShowInformation($"All entries extracted.", true);
                    }
                }
                catch (Exception a)
                {
                    ShowError(a.Message, true);
                }
            }
        }

        private void ExtractAllEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractAllEntry();
        }

        private void dgvEntryList_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvEntryList.ClearSelection();
        }

        private void dgvHeader_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvHeader.ClearSelection();
        }

        private void PopulatePKGDataToTreeView()
        {
            string orbisPubCmdErrorMessage = "";
            var bg = new BackgroundWorker();
            bg.DoWork += delegate (object sender, DoWorkEventArgs e)
            {
                Logger.LogInformation("Viewing PKG file list..");
                DisableControls(darkMenuStrip1);
                DisableControls(PKGTreeView);

                List<string> allFilePaths = new List<string>();
                List<string> fileListWithExtensions = new List<string>();
                List<string> dirList = new List<string>();

                Process pkgListProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = PS4PKGToolTempDirectory + "orbis-pub-cmd.exe",
                        Arguments = "img_file_list --passcode " + PKG.Passcode + " \"" + PKG.SelectedPKGFilename + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                pkgListProcess.Start();
                pkgListProcess.WaitForExit(7000); // 7 seconds timeout
                while (!pkgListProcess.StandardOutput.EndOfStream)
                {
                    string line = pkgListProcess.StandardOutput.ReadLine();
                    if (line != null)
                    {
                        if (line.Contains("Error"))
                        {
                            e.Cancel = true;
                            orbisPubCmdErrorMessage = line;
                            return;
                        }
                        allFilePaths.Add(line);
                    }
                }

                var array = allFilePaths.ToArray();

                PKGTreeView.PathSeparator = @"/";

                PKGTreeView.ImageList = imageList1;
                TreeNode lastNode = null;
                string subPathAgg;
                foreach (string path in array)
                {
                    subPathAgg = string.Empty;
                    foreach (string subPath in path.Split('/'))
                    {
                        subPathAgg += subPath + '/';
                        TreeNode[] nodes = PKGTreeView.Nodes.Find(subPathAgg, true);
                        if (nodes.Length == 0)
                        {
                            if (lastNode == null)
                            {
                                PKGTreeView.Invoke((MethodInvoker)delegate
                                {
                                    lastNode = PKGTreeView.Nodes.Add(subPathAgg, subPath);
                                });
                            }
                            else
                            {
                                PKGTreeView.Invoke((MethodInvoker)delegate
                                {
                                    lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                                });
                            }
                            toolStripStatusLabel2.Text = $"Reading {subPath}";
                            lastNode.ImageIndex = 1;
                            lastNode.SelectedImageIndex = 1;
                        }
                        else
                        {
                            lastNode = nodes[0];
                            lastNode.ImageIndex = 0;
                            lastNode.SelectedImageIndex = 0;
                        }
                    }
                    lastNode = null;
                }
                toolStripStatusLabel2.Text = $"...";
            };
            bg.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
            {
                if (e.Cancelled)
                {
                    ShowError($"Operation cancelled : {orbisPubCmdErrorMessage}.", true);
                }
                Logger.LogInformation("PKG file list loaded.");
                EnableControls(darkMenuStrip1);
                EnableControls(PKGTreeView);
            };
            bg.RunWorkerAsync();
        }

        private void extractToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PKGTreeView.SelectedNode.Nodes.Count > 0)
            {
                if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
                {
                    List<string> nodeList = new List<string>();
                    foreach (TreeNode node in PKGTreeView.Nodes)
                    {
                        nodeList.Add(node.FullPath);
                    }

                    string extractLocation = fbd.SelectedPath;
                    ExtractSelectedPKGData(nodeList, extractLocation);
                }
            }
        }

        private void KillProcess(string processName)
        {
            try
            {
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit();
                        Logger.LogInformation($"{processName} killed.");
                    }
                    catch (Exception ex)
                    {
                        // Handle any exception that occurred during process termination
                        Logger.LogError($"Error while killing process {processName}: {ex.Message}");
                    }
                }
            }
            catch
            {

            }

        }

        private void ExtractFullPKG()
        {
            if (btnExtractFullPKG.Text == "Extract full PKG")
            {
                var bgw = new BackgroundWorker();
                if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
                {
                    string extractLocation = fbd.SelectedPath;
                    btnExtractFullPKG.Text = "Stop Extract";
                    string orbisPubCmdErrorMessage = "";
                    bgw.WorkerSupportsCancellation = true;
                    bgw.DoWork += (sender, e) =>
                    {
                        // Extraction code here
                        PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PKG.SelectedPKGFilename);
                        string selectedPKGFilename = PKG.SelectedPKGFilename;
                        Logger.LogInformation($"Extracting PKG ({selectedPKGFilename})..");
                        toolStripStatusLabel2.Text = $"Extracting PKG ({selectedPKGFilename})..";
                        extractLocation = $@"{extractLocation}\{PS4_PKG.PS4_Title}";
                        Tool.CreateDirectoryIfNotExists(extractLocation);
                        string arguments = $"img_extract --passcode {tbPasscode.Text} \"{selectedPKGFilename}\" \"{extractLocation}\"";
                        Process extract = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = PS4PKGToolTempDirectory + "orbis-pub-cmd.exe",
                                Arguments = arguments,
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            }
                        };

                        extract.Start();
                        extract.WaitForExit(2000); // 2 seconds timeout

                        while (!extract.StandardOutput.EndOfStream)
                        {
                            string line = extract.StandardOutput.ReadLine();
                            // Read output here
                            if (line.Contains("[Error]"))
                            {
                                orbisPubCmdErrorMessage = line;
                                ShowError($"Operation cancelled : {orbisPubCmdErrorMessage}.", true);
                                return;
                            }
                        }
                        ShowInformation($"PKG extracted.", false);
                        Logger.LogInformation($"PKG extracted to \"{extractLocation}\".");
                    };
                    bgw.RunWorkerCompleted += (sender, e) =>
                    {
                        toolStripStatusLabel2.Text = "...";
                        btnExtractFullPKG.Text = "Extract full PKG";
                    };
                    bgw.RunWorkerAsync();
                }
                else if (btnExtractFullPKG.Text == "Stop Extract")
                {
                    bgw.CancelAsync();
                    KillProcess("orbis-pub-cmd");
                    toolStripStatusLabel2.Text = "...";
                    btnExtractFullPKG.Text = "Extract full PKG";
                    ShowInformation("Operation cancelled.", true);
                }
            }
        }

        private void ExtractSelectedPKGData(List<string> nodeList, string extractLocation)
        {
            var bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += delegate
            {
                foreach (var targ_path in nodeList)
                {
                    string in_path = PKG.SelectedPKGFilename;
                    string out_path = "";
                    // Check if the path indicates a directory (ends with slash or backslash)
                    bool isDirectory = targ_path.EndsWith("/") || targ_path.EndsWith("\\");

                    // If it's a directory, use it directly for extraction
                    if (isDirectory)
                    {
                        // Code here for handling directories

                        int lastSlashIndex = targ_path.LastIndexOf('/');

                        // Use LastIndexOf again to find the position of the second-to-last slash
                        int secondToLastSlashIndex = targ_path.LastIndexOf('/', lastSlashIndex - 1);
                        string folderName = targ_path.Substring(secondToLastSlashIndex + 1, lastSlashIndex - secondToLastSlashIndex - 1);

                        out_path = $@"{extractLocation}\{folderName}";

                        Tool.CreateDirectoryIfNotExists(out_path);

                        //int lastBackslashIndex = itemFullPath.LastIndexOf("\\");

                        //// Remove everything after the last backslash (including the backslash)
                        //itemFullPath = itemFullPath.Substring(0, lastBackslashIndex);
                    }
                    else
                    {
                        // If it's not a directory, it's a file

                        // Check if the node is a file without an extension
                        bool isFileWithoutExtension = !Path.HasExtension(targ_path);

                        if (isFileWithoutExtension)
                        {
                            // Code here for handling files without extension
                            int lastSlashIndex = targ_path.LastIndexOf('/');
                            string itemName = targ_path.Substring(lastSlashIndex + 1);

                            // For files without extension, treat them as directories
                            out_path = $@"{extractLocation}\{itemName}";

                            //if (!Directory.Exists(Path.GetDirectoryName(itemFullPath)))
                            //    Directory.CreateDirectory(Path.GetDirectoryName(itemFullPath));

                            //// Find the last occurrence of backslash
                            //int lastBackslashIndex = itemFullPath.LastIndexOf("\\");

                            //// Remove everything after the last backslash (including the backslash)
                            //itemFullPath = itemFullPath.Substring(0, lastBackslashIndex);
                        }
                        else
                        {
                            // Code here for handling files with extension

                            //string itemParentDirectory = Path.GetDirectoryName(node.Replace("/", @"\"));
                            string itemName = Path.GetFileName(targ_path.Replace("/", @"\"));

                            //directory saja
                            //itemFullPath = $@"{extractLocation}\{itemParentDirectory}\{itemName}";
                            out_path = $@"{extractLocation}\{itemName}";

                            //if (!Directory.Exists(Path.GetDirectoryName(itemFullPath)))
                            //    Directory.CreateDirectory(Path.GetDirectoryName(itemFullPath));

                            //itemFullPath = Path.GetDirectoryName(itemFullPath);
                            //// Rest of the code for handling files with extension
                            //// ...
                            //// Find the last occurrence of backslash
                            //int lastBackslashIndex = itemFullPath.LastIndexOf("\\");

                            //// Remove everything after the last backslash (including the backslash)
                            //itemFullPath = itemFullPath.Substring(0, lastBackslashIndex);
                        }
                    }

                    // Rest of the code (extraction code) goes here
                    // ...


                    Logger.LogInformation($"Extracting {targ_path} ({in_path})..");
                    toolStripStatusLabel2.Text = $"Extracting {targ_path} ({in_path})..";

                    Process extract = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = PS4PKGToolTempDirectory + "orbis-pub-cmd.exe",
                            Arguments = "img_extract --passcode " + PKG.Passcode + " \"" + in_path + "\":" + targ_path + " \"" + out_path.Replace(@"/", @"\") + "\"",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };

                    extract.Start();
                    extract.WaitForExit(2000); // 2 seconds timeout

                    while (!extract.StandardOutput.EndOfStream)
                    {
                        string line = extract.StandardOutput.ReadLine();
                        // read output
                    }

                    int exitCode = extract.ExitCode;
                    if (exitCode == 0)
                    {
                        Logger.LogInformation($"File extracted to \"{out_path}\"");
                        ShowInformation("Extraction done.", false);
                    }
                    else
                    {
                        Logger.LogError("Process finished with exit code: " + exitCode);
                    }
                }
            };
            bgw.RunWorkerCompleted += delegate
            {
                toolStripStatusLabel2.Text = $"...";
            };
            bgw.RunWorkerAsync();
        }

        private void PKGTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuExtractNode.Show(PKGTreeView, e.Location);
            }
        }

        private void PKGTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Helper.TreeView.Nodename = e.Node.Text;
        }

        private void darkDataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            this.darkDataGridView4.ClearSelection();
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder urlBuilder = new StringBuilder();

            foreach (DataGridViewRow row in dgvUpdate.Rows)
            {
                //int selectedRowIndex = row.Index;
                //DataGridViewRow selectedRow = dgvUpdate.Rows[selectedRowIndex];

                string url = row.Cells[3].Value?.ToString();
                if (!string.IsNullOrEmpty(url))
                {
                    urlBuilder.AppendLine(url);
                }
            }

            string combinedUrls = urlBuilder.ToString().Trim();
            if (!string.IsNullOrEmpty(combinedUrls))
            {
                Clipboard.SetText(combinedUrls);

                ShowInformation("URL copied to clipboard.", true);
            }
            else
            {
                ShowInformation("No URL found.", true);
            }
        }

        private void InitializedDownloadSelectedOfficialUupdate()
        {
            if (string.IsNullOrEmpty(appSettings_.OfficialUpdateDownloadDirectory))
            {
                ShowError("Select PKG update download path in settings", true);
                return;
            }

            if (Helper.Update.Downloading == "no")
            {
                if (dgvUpdate.SelectedCells.Count > 0)
                {
                    try
                    {
                        // Get information for the first selected cell
                        DataGridViewCell cell = dgvUpdate.SelectedCells[0];
                        int selectedRowIndex = cell.RowIndex;
                        DataGridViewRow selectedRow = dgvUpdate.Rows[selectedRowIndex];

                        // Retrieve the selected update's information
                        Helper.Update.URL = Convert.ToString(selectedRow.Cells[3].Value);
                        Helper.Update.PART = Convert.ToString(selectedRow.Cells[0].Value);
                        Helper.Update.SIZE = Convert.ToString(selectedRow.Cells[1].Value);

                        Logger.LogInformation("Downloading official update (" + Helper.Update.SIZE + ")..");
                        DownloadSelectedOfficialUpdate();
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        ShowError("The Clipboard could not be accessed. Please try again.", true);
                    }
                }
                else
                {
                    ShowError("Select an update to download.", false);
                }
            }
            else
            {
                DialogResult dialogResult = DialogResultYesNo("Cancel Download?");
                if (dialogResult == DialogResult.Yes)
                {
                    CancelDownloadingFile();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    ToolStripSplitButtonTotalPKG.Enabled = true;

                    Helper.Update.Downloading = "no";
                    downloadSelectedPKGUpdateToolStripMenuItem.Text = "Download PKG file";

                    ShowInformation("The download has been cancelled.", true);
                }
            }
        }

        private void downloadSelectedPKGUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializedDownloadSelectedOfficialUupdate();
        }

        public void CancelDownloadingFile()
        {
            Helper.Update.client.CancelAsync();
            Helper.Update.client.Dispose();
            Helper.Update.Downloading = "2";
        }

        private void DisableTabPages(Control con, string name)
        {
            foreach (Control tab in flatTabControl1.TabPages)
            {
                if (tab.Name != name)
                {
                    tab.Enabled = false;
                }
            }
        }

        private void EnableTabPages(Control con)
        {
            foreach (Control tab in flatTabControl1.TabPages)
            {
                tab.Enabled = true;
            }
        }

        private void DisableControls(Control con)
        {
            if (con != null)
            {
                con.Enabled = false;
            }
        }

        private void EnableControls(Control con)
        {
            if (con != null)
            {
                con.Enabled = true;
            }
        }

        private void DownloadSelectedOfficialUpdate()
        {
            var bg = new BackgroundWorker();
            bg.DoWork += delegate
            {
                foreach (Control tab in flatTabControl1.TabPages)
                {
                    tab.Enabled = tab.Name == "tabPage5";
                }

                ToolStripSplitButtonTotalPKG.Enabled = false;
                DisableControls(darkMenuStrip1);

                string sourceFile = Helper.Update.URL;

                int pos = Helper.Update.URL.LastIndexOf("/") + 1;
                string filename = Helper.Update.URL.Substring(pos);
                string destFile = Path.Combine(appSettings_.OfficialUpdateDownloadDirectory, filename);

                Helper.Update.client = new WebClient();

                Helper.Update.client.DownloadProgressChanged += (sender, e) =>
                {
                    double bytesIn = e.BytesReceived;
                    double totalBytes = e.TotalBytesToReceive;
                    double percentage = bytesIn / totalBytes * 100;

                    toolStripProgressBar1.Value = (int)Math.Truncate(percentage);
                    toolStripStatusLabel2.Text = "Downloading update PKG.. (" + percentage.ToString("F0") + "%)";
                };
                Helper.Update.client.DownloadFileCompleted += (sender, e) =>
                {
                    toolStripProgressBar1.Value = 0;
                    toolStripStatusLabel2.Text = "...";

                    try
                    {
                        if (e.Cancelled)
                        {
                            // Handle cancellation
                            ShowInformation("The download has been cancelled.", true);
                        }
                        else if (e.Error != null)
                        {
                            // Handle error
                            ShowError("An error occurred while trying to download the file.", true);
                        }
                        else
                        {
                            // Handle successful download
                            Logger.LogInformation("File downloaded.");

                            DialogResult dialogResult = MessageBoxHelper.DialogResultYesNo("File downloaded. Open folder?");
                            if (dialogResult == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(appSettings_.OfficialUpdateDownloadDirectory);
                            }
                        }
                    }
                    finally
                    {
                        // Cleanup
                        Helper.Update.client.Dispose();

                        // Reset download status
                        downloadSelectedPKGUpdateToolStripMenuItem.Text = "Download PKG file";
                        Helper.Update.Downloading = "no";

                        // Enable controls
                        EnableTabPages(flatTabControl1);
                        EnableControls(darkMenuStrip1);
                        ToolStripSplitButtonTotalPKG.Enabled = true;
                    }
                };

                toolStripProgressBar1.Maximum = 100;

                try
                {
                    // Start the download asynchronously
                    Helper.Update.client.DownloadFileAsync(new Uri(sourceFile), destFile);

                    downloadSelectedPKGUpdateToolStripMenuItem.Text = "Cancel download";
                    Helper.Update.Downloading = "yes";
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the download initiation
                    ShowError("An error occurred while initiating the download: " + ex.Message, true);
                }
            };
            bg.RunWorkerCompleted += delegate
            {
            };
            bg.RunWorkerAsync();
        }

        private string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        private void btnViewPKGData_Click(object sender, EventArgs e)
        {
            if (!CheckOrbisPubCmdExists())
                return;
            string passcode = tbPasscode.Text;
            if (passcode.Length != 32 && passcode.Length != 0)
            {
                // Invalid passcode length, return or display an error message
                return;
            }

            PKG.Passcode = passcode;

            // Clear the nodes of the PKGTreeView control
            PKGTreeView.Nodes.Clear();
            listView1.Items.Clear();
            // Populate PKG data to the tree view
            PopulatePKGDataToTreeView();
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            ListViewDraw.colorListViewHeader(ref listView1, Color.FromArgb(57, 60, 62), Color.FromArgb(220, 220, 220));

            foreach (ColumnHeader column in listView1.Columns)
            {
                column.Width = listView1.Width / listView1.Columns.Count;
            }
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void darkButton3_Click(object sender, EventArgs e)
        {
            if (tbSearchGame.Text == string.Empty)
                return;
            (PKGGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Filename] LIKE '%{0}%' OR [Title ID] LIKE '%{0}%' OR [Content ID] LIKE '%{0}%'", "");
            tbSearchGame.Text = string.Empty;
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (!CheckOrbisPubCmdExists())
                    return;

                if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
                {
                    List<string> nodeList = new List<string>();
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        if (item.SubItems[1].Text == "Directory")
                            nodeList.Add(item.SubItems[2].Text.Replace(@"\", @"/") + @"/" + item.SubItems[0].Text + @"/");
                        else
                            nodeList.Add(item.SubItems[2].Text.Replace(@"\", @"/") + @"/" + item.SubItems[0].Text);
                    }

                    string extractLocation = fbd.SelectedPath;
                    ExtractSelectedPKGData(nodeList, extractLocation);
                }
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem clickedItem = listView1.GetItemAt(e.X, e.Y);

                // Check if the clicked item is not null and its text value is not "..."
                if (clickedItem != null && clickedItem.Text != "...")
                {
                    contextMenuExtractListView.Show(listView1, e.Location);
                }
            }
        }

        private void ViewUpdateChangelog()
        {
            try
            {
                string orbisPubCmdErrorMessage = "";
                Process extract = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = PS4PKGToolTempDirectory + "orbis-pub-cmd.exe",
                        Arguments = "img_extract --no_passcode \"" + PKG.SelectedPKGFilename + "\":Sc0/changeinfo/changeinfo.xml" + " \"" + PS4PKGToolTempDirectory.Remove(PS4PKGToolTempDirectory.Length - 1) + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                extract.Start();
                extract.WaitForExit();

                while (!extract.StandardOutput.EndOfStream)
                {
                    string line = extract.StandardOutput.ReadLine();
                    if (line != null)
                    {
                        if (line.Contains("Error"))
                        {
                            orbisPubCmdErrorMessage = line;
                            break;
                        }
                    }
                }

                if (orbisPubCmdErrorMessage == "[Error]\tCould not find file or directory. (Sc0/changeinfo/changeinfo.xml)")
                {
                    ShowInformation("Change info not available.", true);
                    return;
                }
                else if (orbisPubCmdErrorMessage != "")
                {
                    ShowError($"Operation cancelled : {orbisPubCmdErrorMessage}.", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while viewing the update changelog: " + ex.Message, true);
            }
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            OpenProgramSettings();
        }

        private void ViewPatchChangelog_Click(object sender, EventArgs e)
        {
            Logger.LogInformation("Viewing patch PKG changelog..");

            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            if (clickedMenuItem == viewPkgChangeInfotoolStripMenuItem1 || clickedMenuItem == viewPkgChangeInfotoolStripMenuItem2)
            {
                if (!CheckOrbisPubCmdExists())
                    return;

                ViewUpdateChangelog();

                string changeInfoFile = PS4PKGToolTempDirectory + "changeinfo.xml";
                if (File.Exists(changeInfoFile))
                {
                    try
                    {
                        string changeInfoData = File.ReadAllText(changeInfoFile);
                        File.Delete(changeInfoFile);
                        using (PKGChangeInfoViewer updateChangelog = new PKGChangeInfoViewer(changeInfoData))
                        {
                            updateChangelog.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowError("An error occurred while viewing the update changelog: " + ex.Message, true);
                    }
                }
            }
        }

        private bool CheckOrbisPubCmdExists()
        {
            if (!File.Exists(OrbisPubCmd))
            {
                ShowError($"Missing {Path.GetFileName(OrbisPubCmd)} in PS4PKGToolTemp.", true);
                return false;
            }

            return true;
        }

        private TreeNode SearchFileInTreeView(string p_sSearchTerm, TreeNodeCollection p_Nodes)
        {
            foreach (TreeNode node in p_Nodes)
            {
                if (node.Name == p_sSearchTerm) // Use the 'Name' property for comparison
                    return node;

                if (node.Nodes.Count > 0)
                {
                    TreeNode child = SearchFileInTreeView(p_sSearchTerm, node.Nodes);
                    if (child != null)
                        return child;
                }
            }

            return null;
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PKGTreeView.ExpandAll();
        }

        private void SearchFileInTreeView_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearchTreeView.Text))
                return;

            TreeNode selectedNode = SearchFileInTreeView(tbSearchTreeView.Text, PKGTreeView.Nodes);
            if (selectedNode != null)
            {
                PKGTreeView.SelectedNode = selectedNode;
                PKGTreeView.Focus();
            }
            else
            {
                // Handle case when no matching node is found
                // For example, display a message to the user
            }
        }

        private void collapseAllNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PKGTreeView.CollapseAll();
        }

        private void RenamePKG(string namingFormat, List<string> pkgList)
        {
            var bg = new BackgroundWorker();
            bg.DoWork += delegate
            {
                int countPkg = 0;
                this.Enabled = false;
                PKG.pkgCount = 0;
                toolStripProgressBar1.Maximum = pkgList.Count;
                PKG.CountFailRename = 0;
                PKG.ListFailRename = "";
                Logger.LogInformation($"Renaming PKG file to {namingFormat} format..");
                foreach (var pkg in pkgList)
                {
                    try
                    {
                        PS4_Tools.PKG.SceneRelated.Unprotected_PKG readPkg = PS4_Tools.PKG.SceneRelated.Read_PKG(pkg);
                        Param_SFO.PARAM_SFO psfo = readPkg.Param;
                        string destinationFolder = Path.GetDirectoryName(pkg) + @"\";
                        string newPkgName = "";
                        string sourcePkg = "";
                        string targetPkg = "";
                        (newPkgName, sourcePkg, targetPkg) = PS4_Tools.PKG.SceneRelated.GetNewPKGName(pkg, destinationFolder, namingFormat);
                        UpdatePKGFilename(newPkgName, sourcePkg, targetPkg);
                        countPkg++;

                        PKGGridView.Invoke((Action)(() =>
                        {
                            toolStripStatusLabel2.Text = $"Renaming PKG.. ({countPkg}/{pkgList.Count})";
                            toolStripProgressBar1.Increment(1);
                        }));
                    }
                    catch (Exception a)
                    {
                        PKG.CountFailRename++;
                        PKG.ListFailRename += Path.GetFileNameWithoutExtension(pkg) + " : " + a.Message + "\n";
                    }
                }
            };
            bg.RunWorkerCompleted += delegate
            {
                if (PKG.CountFailRename > 0)
                {
                    ShowWarning(PKG.CountFailRename + " PKG failed to rename. See program log to view the errors.", false);
                    Logger.LogWarning(PKG.CountFailRename + " PKG failed to rename:");
                    Logger.LogWarning(PKG.ListFailRename);
                }
                else
                {
                    ShowInformation("PKG rename done.", true);
                }

                PKGGridView.Invoke((Action)(() =>
                {
                    toolStripStatusLabel2.Text = "...";
                    toolStripProgressBar1.Value = 0;
                    this.Enabled = true;
                }));
            };
            bg.RunWorkerAsync();
        }

        private void GetSelectedPKGPath()
        {
            foreach (DataGridViewCell cell in PKGGridView.SelectedCells)
            {
                int selectedRowIndex = cell.RowIndex;
                DataGridViewRow selectedRow = PKGGridView.Rows[selectedRowIndex];
                PKG.SelectedPKGFilename = $"{selectedRow.Cells[12].Value}\\{selectedRow.Cells[0].Value}";
            }
        }

        private void SelectFirstRowPkg()
        {
            if (PKGGridView.Rows.Count > 0)
            {
                DataGridViewRow firstRow = PKGGridView.Rows[0];
                string valueColumn0 = firstRow.Cells[0].Value?.ToString();
                string valueColumn12 = firstRow.Cells[12].Value?.ToString();
                PKG.SelectedPKGFilename = Path.Combine(valueColumn12, valueColumn0);
            }
        }

        private void MovePkg_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            if (ShowFolderBrowserDialog(out FolderBrowserDialog fbd))
            {
                if (clickedMenuItem == movePkgTypeToolStripMenuItem1 || clickedMenuItem == movePkgTypeToolStripMenuItem2)
                {
                    MovePKG("type", fbd.SelectedPath);
                }
                if (clickedMenuItem == movePkgCategoryToolStripMenuItem1 || clickedMenuItem == movePkgCategoryToolStripMenuItem2)
                {
                    MovePKG("category", fbd.SelectedPath);
                }
                if (clickedMenuItem == movePkgRegionToolStripMenuItem1 || clickedMenuItem == movePkgRegionToolStripMenuItem2)
                {
                    MovePKG("region", fbd.SelectedPath);
                }
                if (clickedMenuItem == movePkgTitleToolStripMenuItem1 || clickedMenuItem == movePkgTitleToolStripMenuItem2)
                {
                    MovePKG("title", fbd.SelectedPath);
                }
            }
        }

        private void MovePKG(string moveBy, string outputFolder)
        {
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += delegate
            {
                this.Enabled = false;
                PKG.pkgCount = 0;
                toolStripProgressBar1.Maximum = PKG.VerifiedPs4PkgList.Count;
                var pkgList = GetSelectedPKGDirectoryList(PKGSelectionType.ALL, true);
                Logger.LogInformation($"Moving PKG to new directory..");

                foreach (var pkgFile in pkgList)
                {
                    try
                    {
                        PKG.pkgCount++;
                        toolStripStatusLabel2.Text = "Moving PKG.. " + "(" + PKG.pkgCount.ToString() + "/" + PKG.VerifiedPs4PkgList.Count.ToString() + ") ";
                        toolStripProgressBar1.Increment(1);

                        switch (moveBy)
                        {
                            case "category":
                                MovePKGByCategory(pkgFile, outputFolder);
                                break;
                            case "type":
                                MovePKGByType(pkgFile, outputFolder);
                                break;
                            case "region":
                                MovePKGByRegion(pkgFile, outputFolder);
                                break;
                            case "title":
                                MovePKGByTitle(pkgFile, outputFolder);
                                break;
                        }
                    }
                    catch (Exception a)
                    {
                        PKG.CountFailMove++;
                        PKG.ListFailMove += Path.GetFileNameWithoutExtension(pkgFile) + " : " + a.Message + "\n";
                    }
                }
            };

            backgroundWorker.RunWorkerCompleted += delegate
            {
                if (PKG.CountFailMove > 0)
                {
                    ShowWarning(PKG.CountFailMove + " PKG failed to move. See program log to view the errors.", false);
                    Logger.LogWarning(PKG.CountFailMove + " PKG failed to move:");
                    Logger.LogWarning(PKG.ListFailMove);
                }
                else
                {
                    ShowInformation($"PKG moved to new directories.", true);
                }

                toolStripStatusLabel2.Text = "Refreshing PKG list.. ";
                LoadPKGGridView();
                //toolStripStatusLabel2.Text = "... ";
                //toolStripProgressBar1.Value = 0;
            };

            backgroundWorker.RunWorkerAsync();
        }

        private void MovePKGByCategory(string pkgFilePath, string outputFolder)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkgData = PS4_Tools.PKG.SceneRelated.Read_PKG(pkgFilePath);
            string pkgCategory = pkgData.PKG_Type.ToString();
            string baseFolder = outputFolder + "\\GAME";
            string patchFolder = outputFolder + "\\PATCH";
            string addonFolder = outputFolder + "\\ADDON";

            switch (pkgCategory)
            {
                case PKGCategory.GAME:
                    Tool.CreateDirectoryIfNotExists(baseFolder);
                    File.Move(pkgFilePath, Path.Combine(baseFolder, Path.GetFileName(pkgFilePath)));
                    CheckAndAddPathToList(baseFolder);
                    break;
                case PKGCategory.PATCH:
                    Tool.CreateDirectoryIfNotExists(patchFolder);
                    File.Move(pkgFilePath, Path.Combine(patchFolder, Path.GetFileName(pkgFilePath)));
                    CheckAndAddPathToList(patchFolder);
                    break;
                case PKGCategory.ADDON:
                    Tool.CreateDirectoryIfNotExists(addonFolder);
                    File.Move(pkgFilePath, Path.Combine(addonFolder, Path.GetFileName(pkgFilePath)));
                    CheckAndAddPathToList(addonFolder);
                    break;
            }
        }

        private void MovePKGByType(string pkgFilePath, string outputFolder)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkgData = PS4_Tools.PKG.SceneRelated.Read_PKG(pkgFilePath);
            string pkgType = pkgData.PKGState.ToString();
            string fakeFolder = outputFolder + "\\FAKE";
            string officialFolder = outputFolder + "\\OFFICIAL";
            string addonUnlockerFolder = outputFolder + "\\ADDON UNLOCKER";

            switch (pkgType)
            {
                case "Official":
                    Tool.CreateDirectoryIfNotExists(officialFolder);
                    File.Move(pkgFilePath, Path.Combine(officialFolder, Path.GetFileName(pkgFilePath)));
                    CheckAndAddPathToList(officialFolder);
                    break;
                case "Fake":
                    Tool.CreateDirectoryIfNotExists(fakeFolder);
                    File.Move(pkgFilePath, Path.Combine(fakeFolder, Path.GetFileName(pkgFilePath)));
                    CheckAndAddPathToList(fakeFolder);
                    break;
                case "Addon_Unlocker":
                    Tool.CreateDirectoryIfNotExists(addonUnlockerFolder);
                    File.Move(pkgFilePath, Path.Combine(addonUnlockerFolder, Path.GetFileName(pkgFilePath)));
                    CheckAndAddPathToList(addonUnlockerFolder);
                    break;
            }
        }

        private void MovePKGByTitle(string pkgFilePath, string outputFolder)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkgData = PS4_Tools.PKG.SceneRelated.Read_PKG(pkgFilePath);
            if (pkgData.PKG_Type.ToString() == PKGCategory.ADDON) // addon part
            {
                string[] folderPaths = Directory.GetDirectories(outputFolder);

                string matchingFolderName = null;

                foreach (string folderPath in folderPaths)
                {
                    string folderName_ = Path.GetFileName(folderPath);

                    if (folderName_.Contains(pkgData.Param.TITLEID, StringComparison.OrdinalIgnoreCase))
                    {
                        matchingFolderName = folderName_;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(matchingFolderName))
                {
                    var dir = Path.Combine(outputFolder, matchingFolderName, "Addon");
                    var newFilePath = Path.Combine(dir, Path.GetFileName(pkgFilePath));
                    Tool.CreateDirectoryIfNotExists(dir);
                    File.Move(pkgFilePath, newFilePath);
                }
            }
            else
            {
                var folderName = outputFolder + "\\" + pkgData.PS4_Title + $" ({pkgData.Param.TitleID})";
                string fullPath = "";
                switch (pkgData.PKG_Type.ToString())
                {
                    case PKGCategory.GAME:
                        fullPath =  folderName + "\\Game";
                        break;
                    case PKGCategory.PATCH:
                        fullPath =  folderName + "\\Patch";
                        break;
                    case PKGCategory.APP:
                        fullPath =  folderName + "\\App";
                        break;
                    case PKGCategory.ADDON: // this code should be moved to 'addon part' 
                        fullPath =  folderName + "\\Addon";
                        break;
                    default:
                        break;
                }

                Tool.CreateDirectoryIfNotExists(fullPath);
                File.Move(pkgFilePath, Path.Combine(fullPath, Path.GetFileName(pkgFilePath)));
                CheckAndAddPathToList(outputFolder);
            }
        }

        private void MovePKGByRegion(string pkgFilePath, string outputFolder)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG pkgData = PS4_Tools.PKG.SceneRelated.Read_PKG(pkgFilePath);
            string region = pkgData.Region;

            // Define a dictionary to map region names to folder paths
            Dictionary<string, string> regionFolders = new Dictionary<string, string>
    {
        { PKGRegion.EU, "EU" },
        { PKGRegion.US, "US" },
        { PKGRegion.JAPAN, "JAPAN" },
        { PKGRegion.HONG_KONG, "HONG KONG" },
        { PKGRegion.ASIA, "ASIA" },
        { PKGRegion.KOREA, "KOREA" }
    };

            if (regionFolders.TryGetValue(region, out string regionFolder))
            {
                string targetFolder = Path.Combine(outputFolder, regionFolder);

                Tool.CreateDirectoryIfNotExists(targetFolder);
                File.Move(pkgFilePath, Path.Combine(targetFolder, Path.GetFileName(pkgFilePath)));
                CheckAndAddPathToList(targetFolder);
            }
            else
            {
                // Handle the case where the region is not recognized or supported
            }
        }

        private void CheckAndAddPathToList(string path)
        {
            var match = appSettings_.PkgDirectories.FirstOrDefault(stringToCheck => stringToCheck.Contains(path));
            if (match == null)
            {
                appSettings_.PkgDirectories.Add(path);
            }
        }

        private void PKGListGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the cell is in the first column (index 0)
            if (e.ColumnIndex != 0)
            {
                // Set the alignment of the cell's content to center
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            UpdatePKGColorLabel();
        }

        private void UpdatePKGColorLabel()
        {
            if (appSettings_.PkgColorLabel)
            {
                foreach (DataGridViewRow row in PKGGridView.Rows)
                {
                    string category = row.Cells[7].Value.ToString();

                    switch (category)
                    {
                        case PKGCategory.PATCH:
                            row.DefaultCellStyle.ForeColor = appSettings_.PatchPkgForeColor;
                            row.DefaultCellStyle.BackColor = appSettings_.PatchPkgBackColor;
                            break;
                        case PKGCategory.GAME:
                            row.DefaultCellStyle.ForeColor = appSettings_.GamePkgForeColor;
                            row.DefaultCellStyle.BackColor = appSettings_.GamePkgBackColor;
                            break;
                        case PKGCategory.ADDON:
                            row.DefaultCellStyle.ForeColor = appSettings_.AddonPkgForeColor;
                            row.DefaultCellStyle.BackColor = appSettings_.AddonPkgBackColor;
                            break;
                        case PKGCategory.APP:
                            row.DefaultCellStyle.ForeColor = appSettings_.AppPkgForeColor;
                            row.DefaultCellStyle.BackColor = appSettings_.AppPkgBackColor;
                            break;
                        default:
                            // Add additional cases for other categories if needed
                            break;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in PKGGridView.Rows)
                {
                    var isOdd = (row.Index % 2 != 0);
                    row.DefaultCellStyle = GetCellStyle(isFocused: false, isOdd, isHeader: false);
                }
            }
        }

        private static DataGridViewCellStyle GetCellStyle(bool isFocused, bool isOdd, bool isHeader)
        {
            return new DataGridViewCellStyle
            {
                BackColor = (isHeader ? Colors.DarkBackground : (isOdd ? Colors.GreyBackground : Colors.HeaderBackground)),
                ForeColor = Colors.LightText,
                SelectionBackColor = ((isFocused && isHeader) ? Colors.DarkBackground : Colors.BlueSelection),
                SelectionForeColor = Colors.LightText
            };
        }

        private void PKGListGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int column = e.ColumnIndex;

            if (column == 3)
            {
                PKGGridView.Columns[3].SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void btnExtractFullPKG_Click(object sender, EventArgs e)
        {
            if (!CheckOrbisPubCmdExists())
                return;
            ExtractFullPKG();
        }

        /// <summary>
        /// Generate and return datatable of selected/all PKG from gridview
        /// </summary>
        /// <param name="pKGSelectionType"></param>
        /// <returns></returns>
        private DataTable GenerateDatatableFromSelectedPKG(string pKGSelectionType)
        {
            // Create a new DataTable
            DataTable selectedPKGDatatable = new DataTable();

            // Add columns to the DataTable
            foreach (DataGridViewColumn column in PKGGridView.Columns)
            {
                selectedPKGDatatable.Columns.Add(column.HeaderText);
            }

            // Define the bitmap column index
            int bitmapColumnIndex = 3;

            if (pKGSelectionType == PKGSelectionType.ALL)
            {
                // Add all rows to the DataTable
                foreach (DataGridViewRow row in PKGGridView.Rows)
                {
                    DataRow dataRow = selectedPKGDatatable.NewRow();

                    // Populate the DataRow with cell values from the DataGridView row
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ColumnIndex == bitmapColumnIndex && cell.Value is byte[] regionIcon)
                        {
                            // Convert byte array to region string
                            string region = ConvertImageToRegion(regionIcon);

                            // Use the region value as needed
                            dataRow[cell.ColumnIndex] = region;
                        }
                        else
                        {
                            dataRow[cell.ColumnIndex] = cell.Value;
                        }
                    }

                    selectedPKGDatatable.Rows.Add(dataRow);
                }
            }
            else
            {
                // Add selected rows to the DataTable
                foreach (DataGridViewRow row in PKGGridView.SelectedRows)
                {
                    DataRow dataRow = selectedPKGDatatable.NewRow();

                    // Populate the DataRow with cell values from the selected row
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ColumnIndex == bitmapColumnIndex && cell.Value is byte[] regionIcon)
                        {
                            // Convert byte array to region string
                            string region = ConvertImageToRegion(regionIcon);

                            // Use the region value as needed
                            dataRow[cell.ColumnIndex] = region;
                        }
                        else
                        {
                            dataRow[cell.ColumnIndex] = cell.Value;
                        }
                    }

                    selectedPKGDatatable.Rows.Add(dataRow);
                }
            }

            return selectedPKGDatatable;
        }

        public static string ConvertImageToRegion(byte[] regionIcon)
        {
            var imageConverter = new ImageConverter();

            Dictionary<byte[], string> regionMapping = new Dictionary<byte[], string>
    {
        { (byte[])imageConverter.ConvertTo(Properties.Resources.eu, typeof(byte[])), PKGRegion.EU },
        { (byte[])imageConverter.ConvertTo(Properties.Resources.us, typeof(byte[])), PKGRegion.US },
        { (byte[])imageConverter.ConvertTo(Properties.Resources.jp, typeof(byte[])), PKGRegion.JAPAN },
        { (byte[])imageConverter.ConvertTo(Properties.Resources.hk, typeof(byte[])), "HONG KONG" },
        { (byte[])imageConverter.ConvertTo(Properties.Resources.asia, typeof(byte[])), PKGRegion.ASIA },
        { (byte[])imageConverter.ConvertTo(Properties.Resources.kr, typeof(byte[])), PKGRegion.KOREA }
    };

            foreach (var kvp in regionMapping)
            {
                if (Utils.ByteArraysEqual(regionIcon, kvp.Key))
                {
                    return kvp.Value;
                }
            }

            return string.Empty;
        }

        private void TbSearchGame_TextChanged(object sender, EventArgs e)
        {
            (PKGGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Filename] LIKE '%{0}%' OR [Title ID] LIKE '%{0}%' OR [Content ID] LIKE '%{0}%'", tbSearchGame.Text);
        }

        private void PKGTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView.currentNode = e.Node;
            PKG.NodeFullPath = currentNode.FullPath;

            // Find all root nodes
            rootNodes = new List<TreeNode>();
            foreach (TreeNode rootNode in PKGTreeView.Nodes)
            {
                rootNodes.Add(rootNode);
            }

            PopulateListView();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            var selectedItem = (TreeNodeInfo)listView1.SelectedItems[0].Tag;

            if (selectedItem.Path == "...")
            {
                // Handle navigating to the parent directory or showing both root nodes
                if (currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                    PKG.NodeFullPath = currentNode.FullPath;
                    PopulateListView();
                }
                else if (currentNode != null && !rootNodes.Contains(currentNode))
                {
                    currentNode = null;
                    PKG.NodeFullPath = ""; // You might want to set this to the appropriate default value
                    PopulateListView();
                }
                else
                {
                    currentNode = null;
                    PKG.NodeFullPath = ""; // You might want to set this to the appropriate default value
                    PopulateListView(true);
                }
            }
            else if (selectedItem.Node.Nodes.Count > 0) // Check if the clicked item is a directory
            {
                // Handle clicking on a directory in the ListView
                currentNode = selectedItem.Node;
                PKG.NodeFullPath = currentNode.FullPath;
                PopulateListView();
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HandleListViewActivation();
        }

        private void HandleListViewActivation()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedItem = (TreeNodeInfo)listView1.SelectedItems[0].Tag;

                if (selectedItem.Path == "...")
                {
                    // Handle navigating to the parent directory or showing both root nodes
                    if (currentNode.Parent != null)
                    {
                        currentNode = currentNode.Parent;
                        PKG.NodeFullPath = currentNode.FullPath;
                        PopulateListView();
                    }
                    else if (currentNode != null && !rootNodes.Contains(currentNode))
                    {
                        currentNode = null;
                        PKG.NodeFullPath = ""; // You might want to set this to the appropriate default value
                        PopulateListView();
                    }
                    else
                    {
                        currentNode = null;
                        PKG.NodeFullPath = ""; // You might want to set this to the appropriate default value
                        PopulateListView(true);
                    }
                }
                else if (selectedItem.Node.Nodes.Count > 0) // Check if the clicked item is a directory
                {
                    // Handle clicking on a directory in the ListView
                    currentNode = selectedItem.Node;
                    PKG.NodeFullPath = currentNode.FullPath;
                    PopulateListView();
                }
            }
        }

        private void PopulateListView(bool showRootNodes = false)
        {
            listView1.Items.Clear();

            // Add the "..." row to navigate to the parent directory
            if (currentNode != null && !showRootNodes)
            {
                TreeNodeInfo parentItem = new TreeNodeInfo
                {
                    Node = currentNode.Parent != null ? currentNode.Parent : null,
                    Path = "..."
                };
                ListViewItem parentListViewItem = new ListViewItem("...");
                parentListViewItem.Tag = parentItem;
                parentListViewItem.ImageIndex = 2; // Set the appropriate image index for the "parent folder" icon
                listView1.Items.Add(parentListViewItem);
            }

            listView1.SmallImageList = this.imageList1;

            List<TreeNode> list;
            if (currentNode != null)
            {
                list = currentNode.Nodes.Cast<TreeNode>().ToList();
            }
            else if (showRootNodes)
            {
                list = rootNodes;
            }
            else
            {
                return; // If currentNode is null and showRootNodes is false, do not populate the ListView.
            }

            foreach (var item in list)
            {
                string fileName = Path.GetFileNameWithoutExtension(item.Text);
                string dir = Path.GetDirectoryName(item.FullPath);
                bool isDirectory = item.Nodes.Count > 0;

                TreeNodeInfo treeNodeInfo = new TreeNodeInfo
                {
                    Node = item,
                    Path = item.FullPath
                };

                ListViewItem listViewItem = new ListViewItem(isDirectory ? "Directory" : "File");
                listViewItem.Text = item.Text;
                listViewItem.SubItems.Add(isDirectory ? "Directory" : Path.GetExtension(item.Text).Replace(".", ""));
                listViewItem.SubItems.Add(dir);
                listViewItem.Tag = treeNodeInfo;
                listViewItem.ImageIndex = isDirectory ? 0 : 1;

                listView1.Items.Add(listViewItem);
            }
        }

        private void settingstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProgramSettings();
        }

        private void Backport_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            if (clickedMenuItem == setBackportedtoolStripMenuItem1 || clickedMenuItem == setBackportedToolStripMenuItem2)
            {
                if (PKGGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in PKGGridView.SelectedRows)
                    {
                        row.Cells["Backported"].Value = "Yes";
                        Logger.LogInformation($"\"{row.Cells["Filename"].Value}\" set as backported.");
                    }
                    ShowInformation("PKG set as backported.", false);
                }
            }

            if (clickedMenuItem == setRemarktoolStripMenuItem1)
            {
                if (PKGGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in PKGGridView.SelectedRows)
                    {
                        row.Cells["Backported"].Value = backportRemarkTextboxtoolStripTextBox1.Text;
                        Logger.LogInformation($"Added backport remark to \"{row.Cells["Filename"].Value}\" ({backportRemarkTextboxtoolStripTextBox1.Text}).");
                    }
                    ShowInformation("Backport remark added.", false);
                }
            }

            if (clickedMenuItem == setRemarktoolStripMenuItem2)
            {
                if (PKGGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in PKGGridView.SelectedRows)
                    {
                        row.Cells["Backported"].Value = backportRemarkTextboxtoolStripTextBox2.Text;
                        Logger.LogInformation($"Added backport remark to \"{row.Cells["Filename"].Value}\" ({backportRemarkTextboxtoolStripTextBox2.Text}).");
                    }
                    ShowInformation("Backport remark added.", false);
                }
            }

            if (clickedMenuItem == removeBackportedtoolStripMenuItem1 || clickedMenuItem == removeBackportedToolStripMenuItem2)
            {
                if (PKGGridView.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in PKGGridView.SelectedRows)
                    {
                        row.Cells["Backported"].Value = "No";
                        Logger.LogInformation($"Removed backport label from \"{row.Cells["Filename"].Value}\".");
                    }
                    ShowInformation("Backport remark removed.", false);
                }
            }

            Backport.SaveData(PKGGridView);
        }

        private void tbLog_TextChanged(object sender, EventArgs e)
        {
            //tbLog.SelectionStart = tbLog.Text.Length;
            //tbLog.ScrollToCaret();
        }

        private void colorTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Iterate through the DataGridView rows
            foreach (DataGridViewRow row in PKGGridView.Rows)
            {
                // Check if the row is not a header and has valid data
                if (!row.IsNewRow && row.Cells["Title ID"].Value != null && row.Cells["Category"].Value != null)
                {
                    string titleId = row.Cells["Title ID"].Value.ToString();
                    string category = row.Cells["Category"].Value.ToString();
                    string fileName = row.Cells["Filename"].Value.ToString();

                    // Find or create the parent node based on the Title ID
                    TreeNode parentNode = FindOrCreateNode(treeView1.Nodes, titleId);

                    // Create the subnode for the Category
                    TreeNode subNode = new TreeNode(fileName);

                    // Add the subnode to the appropriate parent node
                    if (category == "Game")
                    {
                        FindOrCreateCategoryNode(parentNode, "Game").Nodes.Add(subNode);
                    }
                    else if (category == "Patch")
                    {
                        FindOrCreateCategoryNode(parentNode, "Patch").Nodes.Add(subNode);
                    }
                    else if (category == "Addon")
                    {
                        FindOrCreateCategoryNode(parentNode, "Addon").Nodes.Add(subNode);
                    }
                    else if (category == "App")
                    {
                        FindOrCreateCategoryNode(parentNode, "App").Nodes.Add(subNode);
                    }
                }
            }
        }

        // Helper method to find or create a node with a specific text
        private TreeNode FindOrCreateNode(TreeNodeCollection nodes, string text)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == text)
                {
                    return node;
                }
            }

            // If the node doesn't exist, create it
            TreeNode newNode = new TreeNode(text);
            nodes.Add(newNode);
            return newNode;
        }

        // Helper method to find or create a category node (Game, Patch, Addon, App)
        private TreeNode FindOrCreateCategoryNode(TreeNode parentNode, string categoryName)
        {
            foreach (TreeNode categoryNode in parentNode.Nodes)
            {
                if (categoryNode.Text == categoryName)
                {
                    return categoryNode;
                }
            }

            // If the category node doesn't exist, create it
            TreeNode newCategoryNode = new TreeNode(categoryName);
            parentNode.Nodes.Add(newCategoryNode);
            return newCategoryNode;
        }

        private void OpenPS4PKGToolTempDirectory_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem clickedMenuItem))
                return;

            if (clickedMenuItem == openPS4PKGToolTempDirectoryToolStripMenuItem1 || clickedMenuItem == openPS4PKGToolTempDirectoryToolStripMenuItem2)
            {
                OpenTempDirectory();
            }
        }
    }
}
