using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Reflection;
using System.IO.Compression;
using ByteSizeLib;
using System.Drawing.Imaging;
using System.Threading;
using System.Text.RegularExpressions;
using LibOrbisPkg.PKG;
using System.IO.MemoryMappedFiles;
using Microsoft.VisualBasic;
using Image = System.Drawing.Image;
using LibOrbisPkg.Util;
using GitHubUpdate;
using System.Runtime.InteropServices;
using DarkUI.Forms;
using DarkUI.Controls;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Dynamic;
using static PS4_Tools.PKG.SceneRelated;
using Microsoft.Win32;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using ClosedXML.Excel;
using TRPViewer;
using System.Management;
using static PS4_PKG_Tool.PS4PKGTOOL.Entry;
using PS4_PKG_Tool.PS4_PKG_Tool;
using System.Globalization;

namespace PS4_PKG_Tool
{
    public partial class Form1 : DarkForm
    {
        private MemoryMappedFile pkgFile;
        private string ExportPKGtoExcel;
        private dynamic send_pkg_json;
        private string TEMPFILENAMESENDPKG;
        private bool runworker = false;
        private bool renameBackFile;
        internal static string filenameDLC;

        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        DataTable DataTableExportExcel;
        public Form form_;
        private byte[] old_byte;

        //private PictureBox pb = new PictureBox();

        public Form1()
        {
            InitializeComponent();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; CheckForIllegalCrossThreadCalls = false;
            darkDataGridView1.ScrollBars = ScrollBars.Vertical;
            darkDataGridView2.ScrollBars = ScrollBars.Vertical;
            darkDataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            darkDataGridView3.ScrollBars = ScrollBars.Vertical;
            darkDataGridView3.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView3.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView3.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView3.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            darkDataGridView3.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView3.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView3.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.ActiveControl = null;  //this = form
            toolStripProgressBar1.MarqueeAnimationSpeed = 30;

            this.Text = "PS4 PKG Tool v1.3";
            ListViewDraw.colorListViewHeader(ref listView1, Color.FromArgb(57, 60, 62), Color.FromArgb(220, 220, 220));
            foreach (ColumnHeader column in listView1.Columns)
            {
                column.Width = listView1.Width / listView1.Columns.Count;
            }
        }

        private void ShowInformation(string text)
        {
            DarkMessageBox.ShowInformation(text, "PS4 PKG Tool");
        }

        private void ShowError(string text)
        {
            DarkMessageBox.ShowError(text, "PS4 PKG Tool");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            PS4PKGTOOL.Tool.killNodeJS();
            Properties.Settings.Default.Save();
            try
            {
                if (Directory.Exists(PS4PKGTOOL.WorkingDirectory))
                {
                    Directory.Delete(PS4PKGTOOL.WorkingDirectory, recursive: true);
                }
            }
            catch { }
           
            Application.Exit();
        }

        //exclude these folder while scanning ps4 pkg
        private static bool IsIgnorable(string dir)
        {
            if (dir.EndsWith("System Volume Information")) return true;
            if (dir.Contains("$RECYCLE.BIN")) return true;
            if (dir.Contains("$Recycle.Bin")) return true;
            return false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
         
          

        }

        private void scanPKG()
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (bgwOpenPKG.IsBusy != true)
                    bgwOpenPKG.RunWorkerAsync();
            });
        }


        private void darkDataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            //if bgm playing, stop it
            if (PS4PKGTOOL.BGM.isBGMPlaying = true)
            {
                PS4PKGTOOL.BGM.isBGMPlaying = false;
                PS4PKGTOOL.BGM.At9Player.Stop();
            }

            if (darkDataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                PS4PKGTOOL.PKG.SelectedPKGFilename = "";
                try
                {
                    foreach (DataGridViewCell cell in darkDataGridView1.SelectedCells)
                    {
                        int selectedrowindex = cell.RowIndex;
                        DataGridViewRow selectedRow = darkDataGridView1.Rows[selectedrowindex];
                        PS4PKGTOOL.PKG.SelectedPKGFilename = Convert.ToString(selectedRow.Cells[12].Value) + "\\" + Convert.ToString(selectedRow.Cells[0].Value);

                    }

                    runOrbis();

                }
                catch (System.Runtime.InteropServices.ExternalException)
                {

                    DarkMessageBox.ShowError("The Clipboard could not be accessed.", "PS4 PKG Tool");
                }
            }
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //if (PS4PKGTOOL.PKG.SelectedPKGFilename != "")
            //{
            //    PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            //    foreach (var item in PS4_PKG.Param.Tables) {
            //    if(item.Name == "PUBTOOLINFO")
            //        {
            //            MessageBox.Show(item.Value) ;
            //        }
            //    }
            //    //prevent image box showed for addon/patch pkg. they only have icon
            //    if (PS4_PKG.PKG_Type.ToString() != "Patch" && PS4_PKG.PKG_Type.ToString() != "Addon_Theme")
            //    {
            //        picture picture = new picture();
            //        picture.filenames = PS4PKGTOOL.PKG.SelectedPKGFilename;
            //        picture.ShowDialog();
            //    }
            //}
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Logger.log("Selected directory : ");

            foreach (var folder in Properties.Settings.Default.PKGLocationList)
            {
                Logger.log(folder);

            }

            if (!Directory.Exists(PS4PKGTOOL.WorkingDirectory))
            {
                Directory.CreateDirectory(PS4PKGTOOL.WorkingDirectory);
                Logger.log("Creating working directory..");
            }

            try
            {
                PS4PKGTOOL.ExtractResources();
             

            }
            catch
            {

            }
           
            Logger.log("Checking Node.js and server module..");
            #region nodejs&serve
            if (PS4PKGTOOL.NodeJsHttpServer.IsSoftwareInstalled("Node.js") == true)
            {
                Logger.log("Node.js installed.");
                Properties.Settings.Default.node_js = true;
            }
            else
            {
                Logger.log("Node.js not installed.");

                Properties.Settings.Default.node_js = false;

            }

            if (Directory.Exists(PS4PKGTOOL.NodeJsHttpServer.HttpServerModulePath))
            {
                Logger.log("Module installed.");

                Properties.Settings.Default.http_server = true;

            }
            else
            {
                Logger.log("Module not installed.");

                Properties.Settings.Default.http_server = false;

            }

            #endregion nodejs&serve


            this.Invoke((MethodInvoker)delegate
            {
                this.Enabled = false;
                //if bgm playing, stop it
                if (PS4PKGTOOL.BGM.isBGMPlaying = true)
                {
                    PS4PKGTOOL.BGM.isBGMPlaying = false;
                    PS4PKGTOOL.BGM.At9Player.Stop();
                }

                //disable selection changed event
                darkDataGridView1.SelectionChanged -= darkDataGridView1_SelectionChanged;

                //update UI
                darkDataGridView1.Enabled = false; //disable dgv during listing pkg
                darkDataGridView2.Enabled = false; //disable dgv during listing pkg

                //get selected path to scan on other method
                Logger.log("Scanning PKG..");

                scanPKG();

            });



        }


        private void runOrbis()
        {
            if (!File.Exists(PS4PKGTOOL.PKG.SelectedPKGFilename))
            {
                ShowError("File not found");
                foreach (DataGridViewRow row in darkDataGridView1.SelectedRows)
                {
                    darkDataGridView1.Rows.RemoveAt(row.Index);
                }
                return;
            }
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            this.Text = "PS4 PKG Tool v1.3 - Viewing \"" + PS4_PKG.PS4_Title + "\"";
            PS4PKGTOOL.PKG.CurrentPKGTitle = PS4_PKG.PS4_Title;
            PS4PKGTOOL.PKG.CurrentPKGType = PS4_PKG.PKG_Type.ToString();
            toolStripMenuItem35.Enabled = PS4PKGTOOL.PKG.CurrentPKGType == "Patch" ? true : false;
            viewUpdateChangelogToolStripMenuItem1.Enabled = PS4PKGTOOL.PKG.CurrentPKGType == "Patch" ? true : false;
            uninstallBasePKGToolStripMenuItem.Enabled = true;
            uninstallPatchPKGToolStripMenuItem.Enabled = true;
            uninstallAddonPKGToolStripMenuItem.Enabled = true;
            uninstallThemePKGToolStripMenuItem.Enabled = true;
            toolStripMenuItem24.Enabled = true;
            toolStripMenuItem25.Enabled = true;
            toolStripMenuItem26.Enabled = true;
            toolStripMenuItem27.Enabled = true;

            if (PS4_PKG.PKG_Type.ToString() == "Game")
            {
                uninstallAddonPKGToolStripMenuItem.Enabled = false;
                uninstallThemePKGToolStripMenuItem.Enabled = false;
                toolStripMenuItem26.Enabled = false;
                toolStripMenuItem27.Enabled = false;
            }
            else if (PS4_PKG.PKG_Type.ToString() == "Patch")
            {
                uninstallAddonPKGToolStripMenuItem.Enabled = false;
                uninstallThemePKGToolStripMenuItem.Enabled = false;
                toolStripMenuItem26.Enabled = false;
                toolStripMenuItem27.Enabled = false;
            }
            else if (PS4_PKG.PKG_Type.ToString() == "Addon")
            {
                uninstallBasePKGToolStripMenuItem.Enabled = false;
                uninstallPatchPKGToolStripMenuItem.Enabled = false;

                toolStripMenuItem24.Enabled = false;
                toolStripMenuItem25.Enabled = false;
            }

            treeView1.Nodes.Clear();
            listView1.Items.Clear();

            #region checkUpdate
            //check update
            labelTotalSize.Text = "";
            labelUpdateType.Text = "";
            labelRemaster.Text = "";
            labelPKGdigest.Text = "";
            labelMandatory.Text = "";
            labelTotalFile.Text = "";
            labelUpdateVersion.Text = "";
            labelSystemReq.Text = "";


            if (PS4_PKG.PKG_Type.ToString() == "Game" || PS4_PKG.PKG_Type.ToString() == "Patch")
            {
                if (bgwLoadPKGUpdate.IsBusy != true)
                    bgwLoadPKGUpdate.RunWorkerAsync();
            }
            #endregion checkUpdate

            #region pubtoolinfo

            try
            { 
                //load pubtoolinfo
                List<string> array = new List<string>();
                List<string> value = new List<string>();
                List<string> type = new List<string>();

                foreach (var item in PS4_PKG.Param.Tables)
                {
                    if (item.Name == "PUBTOOLINFO")
                    {
                        array = item.Value.Split(',').Reverse().ToList<string>();
                    }
                }

                foreach (var items in array)
                {
                    var result = items.Substring(items.LastIndexOf('=') + 1);
                    value.Add(result);

                    var results = items.Split('=')[0];
                    type.Add(results);
                }


                DataTable dtPubtool = new DataTable();
                foreach (var tv in type)
                {
                    dtPubtool.Columns.Add(tv.Replace("c_date", "Creation Date").Replace("sdk_ver", "PS4 SDK Version").Replace("st_type", "Storage Type").Replace("c_time", "Creation Time"));
                }


                var row = dtPubtool.NewRow();

                for (int i = 0; i < value.Count; i++)
                {
                    row[i] = value[i];
                }
                dtPubtool.Rows.Add(row);

                darkDataGridView4.DataSource = dtPubtool;
            } catch { }

            #endregion pubtoolinfo

            #region header

            try
            { 
                //header info
                List<string> Type = new List<string>();
                List<string> Value = new List<string>();

                foreach (var item in PS4_PKG.Header.DisplayType())
                {
                    Type.Add(item);
                }

                foreach (var item in PS4_PKG.Header.DisplayValue())
                {
                    Value.Add(item);
                }

                Type.ToArray();
                Value.ToArray();

                DataTable dtHeader = new DataTable();
                dtHeader.Columns.Add("Type");
                dtHeader.Columns.Add("Value");

                var TypeAndValue = Type.Zip(Value, (t, v) => new { Type = t, Value = v });
                foreach (var tv in TypeAndValue)
                {
                    dtHeader.Rows.Add(tv.Type, tv.Value);
                    dgvHeader.DataSource = dtHeader;
                }
            } catch { }

            #endregion header

            #region entryList

            //load entry list
            dgvEntryList.DataSource = null;
            dgvEntryList.Rows.Clear();
            dgvEntryList.Refresh();
            dgvEntryList.ScrollBars = ScrollBars.Vertical;
            try
            {
                using (var file = File.OpenRead(PS4PKGTOOL.PKG.SelectedPKGFilename))
                {

                    var pkg = new PkgReader(file).ReadPkg();
                    var i = 0;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Offset");
                    dt.Columns.Add("Size");
                    dt.Columns.Add("Flags 1");
                    dt.Columns.Add("Flags 2");
                    dt.Columns.Add("Encrypted?");

                    foreach (var meta in pkg.Metas.Metas)
                    {
                        PS4PKGTOOL.Entry.idEntryList.Add($"{i++,-6}");
                        PS4PKGTOOL.Entry.nameEntryList.Add($"{meta.id}");
                    }

                    foreach (var meta in pkg.Metas.Metas)
                    {
                        if (meta.Encrypted == true)
                        {
                            PS4PKGTOOL.Entry.offsetEntryList.Add($"{meta.DataOffset,-10:X8}");
                            PS4PKGTOOL.Entry.NameEntryList.Add($"{meta.id}");

                        }
                    }

                    PS4PKGTOOL.Entry.idEntryList.ToArray();
                    PS4PKGTOOL.Entry.nameEntryList.ToArray();


                    PS4PKGTOOL.Entry.NameEntryList.ToArray();
                    PS4PKGTOOL.Entry.offsetEntryList.ToArray();



                    i = 0;

                    int decValue;

                    foreach (var meta in pkg.Metas.Metas)
                    {
                        decValue = 0;
                        decValue = int.Parse($"{meta.DataSize,-10:X2}", System.Globalization.NumberStyles.HexNumber);
                        var finalSize = ByteSizeLib.ByteSize.FromBytes(decValue);


                        dt.Rows.Add($"{meta.id}", $"0x{meta.DataOffset,-10:X}", finalSize, $"0x{meta.Flags1,-8:X}", $"0x{meta.Flags2,-8:X}", $"{meta.Encrypted,-8:X}");
                        dgvEntryList.DataSource = dt;

                    }


                }


                dgvEntryList.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvEntryList.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEntryList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch { }

            #endregion entryList

            #region background

            try {
                pbPIC0.Image = null;
                pbPIC1.Image = null;
                pbPIC0.Refresh();
                pbPIC1.Refresh();
                if (PS4_PKG.PKG_Type.ToString() == "Game" || PS4_PKG.PKG_Type.ToString() == "Patch")
                {
                    if (PS4_PKG.Image != null)
                    {
                        pbPIC0.Click += new EventHandler(this.pbPIC0_click);
                        pbPIC0.Visible = true;
                        darkLabel3.Visible = false;
                        pbPIC0.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbPIC0.Image = PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Image);
                        PS4PKGTOOL.Bitmap.pic0.Image = pbPIC0.Image;
                    }
                    else
                    {
                        darkLabel3.Visible = true;
                        pbPIC0.Click -= pbPIC0_click;
                        pbPIC0.Visible = false;
                        pbPIC0.Image = null;
                    }

                    if (PS4_PKG.Image2 != null)
                    {
                        // i had to do this to fix bug that ps4_tool still showing PS4_PKG.Image2 is not null while its actually it is null
                        // save old byte and compare with new byte if both byte are same then the bug is there
                        if (old_byte == PS4_PKG.Image2)
                        {
                            darkLabel4.Visible = true;
                            pbPIC1.Click -= pbPIC1_click;
                            pbPIC1.Visible = false;
                            pbPIC1.Image = null;
                        }
                        else
                        {
                            old_byte = PS4_PKG.Image2;
                            pbPIC1.Click += new EventHandler(this.pbPIC1_click);
                            pbPIC1.Visible = true;
                            darkLabel4.Visible = false;
                            pbPIC1.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbPIC1.Image = PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Image2);
                            PS4PKGTOOL.Bitmap.pic1.Image = pbPIC1.Image;
                            PS4_PKG.Image2 = null;
                        }

                    }
                    else
                    {
                        darkLabel4.Visible = true;
                        pbPIC1.Click -= pbPIC1_click;
                        pbPIC1.Visible = false;
                        pbPIC1.Image = null;
                    }
                }
            } catch { }


            #endregion background

            #region trophies

            try
            {  //show trophies
                darkDataGridView3.Rows.Clear();
                darkDataGridView3.Refresh();

                if (PS4_PKG.Trophy_File != null)
                {
                    BackgroundWorker bgwTrophy = new BackgroundWorker();
                    bgwTrophy.WorkerSupportsCancellation = true;
                    bgwTrophy.DoWork += (s, e) =>
                    {
                        Logger.log("Loading trophies for " + PS4_PKG.PS4_Title + "..");

                        List<string> idEntryList = new List<string>();
                        List<string> nameEntryList = new List<string>();

                        //extract all entry
                        //add entry to array
                        using (var file = File.OpenRead(PS4PKGTOOL.PKG.SelectedPKGFilename))
                        {
                            var pkg = new PkgReader(file).ReadPkg();
                            var i = 0;

                            foreach (var meta in pkg.Metas.Metas)
                            {
                                idEntryList.Add($"{i++,-6}");
                                nameEntryList.Add($"{meta.id}");
                            }

                            idEntryList.ToArray();
                            nameEntryList.ToArray();
                        }

                        //extract each entry from array
                        string path = PS4PKGTOOL.Trophy.TrophyFolder;
                        try
                        {
                            Directory.CreateDirectory(path);

                            var numbersAndWords = idEntryList.Zip(nameEntryList, (n, w) => new { id = n, name = w });
                            foreach (var nw in numbersAndWords)
                            {
                                if (nw.name == "TROPHY__TROPHY00_TRP")
                                {
                                    var pkgPath = PS4PKGTOOL.PKG.SelectedPKGFilename;
                                    var idx = int.Parse(nw.id);
                                    var name = nw.name;
                                    PS4PKGTOOL.Trophy.outPath = path + "\\" + name.Replace("_SHA", ".SHA").Replace("_DAT", ".DAT").Replace("_SFO", ".SFO").Replace("_XML", ".XML").Replace("_SIG", ".SIG").Replace("_PNG", ".PNG").Replace("_JSON", ".JSON").Replace("_DDS", ".DDS").Replace("_TRP", ".TRP").Replace("_AT9", ".AT9"); ;

                                    using (var pkgFile = File.OpenRead(pkgPath))
                                    {
                                        var pkg = new PkgReader(pkgFile).ReadPkg();
                                        if (idx < 0 || idx >= pkg.Metas.Metas.Count)
                                        {
                                            //DarkMessageBox.ShowError("Error: entry number out of range", "PS4 PKG Tool");
                                            return;
                                        }
                                        using (var outFile = File.Create(PS4PKGTOOL.Trophy.outPath))
                                        {
                                            var meta = pkg.Metas.Metas[idx];
                                            outFile.SetLength(meta.DataSize);
                                            if (meta.Encrypted)
                                            {
                                                //if (passcode == null)
                                                //{
                                                //    //MessageBox.Show("Warning: Entry is encrypted but no passcode was provided! Saving encrypted bytes.");
                                                //}
                                                //else
                                                //{
                                                //    var entry = new SubStream(pkgFile, meta.DataOffset, (meta.DataSize + 15) & ~15);
                                                //    var tmp = new byte[entry.Length];
                                                //    entry.Read(tmp, 0, tmp.Length);
                                                //    tmp = LibOrbisPkg.PKG.Entry.Decrypt(tmp, pkg.Header.content_id, passcode, meta);
                                                //    outFile.Write(tmp, 0, (int)meta.DataSize);
                                                //    return;
                                                //}
                                            }
                                            new SubStream(pkgFile, meta.DataOffset, meta.DataSize).CopyTo(outFile);
                                        }
                                    }
                                }

                            }

                            //MessageBox.Show("All entry item extracted", "PS4 PKG Tool");
                        }
                        catch (Exception a)
                        {
                            Logger.log("a.Message");
                            //DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool");
                        }

                        //done exract, now open trp
                        if (File.Exists(PS4PKGTOOL.Trophy.outPath))
                        {
                            PS4PKGTOOL.Trophy.trophy = new TRPReader();
                            PS4PKGTOOL.Trophy.trophy.Load(PS4PKGTOOL.Trophy.outPath);

                            if (!PS4PKGTOOL.Trophy.trophy.IsError)
                            {
                                List<Archiver>.Enumerator enumerator = new List<Archiver>.Enumerator();
                                enumerator = PS4PKGTOOL.Trophy.trophy.TrophyList.GetEnumerator();



                                try
                                {

                                    while (enumerator.MoveNext())
                                    {
                                        Archiver current = enumerator.Current;
                                        if (current.Name.ToUpper().EndsWith(".PNG"))
                                        {


                                            PS4PKGTOOL.Trophy.imageToExtract.Add(Utilities.BytesToImage(PS4PKGTOOL.Trophy.trophy.ExtractFileToMemory(current.Name)));
                                            PS4PKGTOOL.Trophy.NameToExtract.Add(current.Name);

                                            var image = Utilities.BytesToImage(PS4PKGTOOL.Trophy.trophy.ExtractFileToMemory(current.Name));
                                            var resize = PS4PKGTOOL.Trophy.ResizeImage(image, image.Width / 2, image.Height / 2);
                                            darkDataGridView3.Rows.Add(resize, current.Name, Utilities.RoundBytes(current.Size), "0x" + current.Offset);


                                        }
                                        //dataGridView1.DataSource = dt;
                                        Application.DoEvents();
                                    }

                                }
                                finally
                                {
                                    enumerator.Dispose();
                                }


                            }
                            //ctrophy.SetVersion = trophy.Version;
                        }
                    };
                    bgwTrophy.RunWorkerCompleted += (s, e) =>
                    {

                    };
                    bgwTrophy.RunWorkerAsync();
                }
                else
                {
                    Logger.log(PS4_PKG.PS4_Title + " has no trophy.");

                    darkDataGridView3.Rows.Clear();
                    darkDataGridView3.Refresh();
                }
            } catch { }

            #endregion trophies


            if (Properties.Settings.Default.BGMEnable == true)
            {
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.WorkerSupportsCancellation = true;
                bgw.DoWork += (s, e) =>
                {
                    PS4PKGTOOL.BGM.PlayAt9(PS4PKGTOOL.PKG.SelectedPKGFilename);
                };
                bgw.RunWorkerCompleted += (s, e) =>
                {

                };
                bgw.RunWorkerAsync();
            }

            //reset value
            darkLabel1.Text = "";
            //pass fullpath to display trophy stuff
            //Trophy trophy = new Trophy();
            //trophy.filenames = filename;
            //richTextBox1.Text = filename + "\n\n";
            //display pkg icon on picturebox
            if (PS4_PKG.Icon != null)
            {
                pictureBox1.Visible = true;
                label3.Text = "";
                pictureBox1.Image = PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Icon);
            }
            else
            {
                pictureBox1.Visible = false;
                label3.Visible = true;
                label3.Text = "Image not available";
            }
         
            //pkg name in title box
            darkLabel1.Text = PS4_PKG.PS4_Title; ;
            //get param info
            DataTable dg2 = new DataTable();
            dg2.Columns.Add("PARAM");
            dg2.Columns.Add("VALUE");
            for (int i = 0; i < PS4_PKG.Param.Tables.Count; i++)
            {
                //item name n value
                dg2.Rows.Add(PS4_PKG.Param.Tables[i].Name, PS4_PKG.Param.Tables[i].Value);
            }
            darkDataGridView2.DataSource = dg2;
            darkDataGridView2.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            toolStripMenuItem2.Text = PS4_PKG.PS4_Title; ;
            toolStripMenuItem124.Text = PS4_PKG.PS4_Title; ;
        }

        private void pbPIC0_click(object sender, EventArgs e)
        {
            if (pbPIC0.Image == null)
                return;
            ////pbPIC0.Image = null;
            //if (flatTabControl2.SelectedTab == flatTabControl2.TabPages["pic0"])//your specific tabname
            //{
            //    pbPIC0.Image = PS4PKGTOOL.Bitmap.pic0.Image;
            //}
            //else if (flatTabControl2.SelectedTab == flatTabControl2.TabPages["pic1"])//your specific tabname
            //{
            //    pbPIC0.Image = PS4PKGTOOL.Bitmap.pic1.Image;
            //}
            darkContextMenuBitmapPIC0.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void pbPIC1_click(object sender, EventArgs e)
        {
            if (pbPIC1.Image == null)
                return;
            ////pbPIC1.Image = null;
            //if (flatTabControl2.SelectedTab == flatTabControl2.TabPages["pic0"])//your specific tabname
            //{
            //    pbPIC1.Image = PS4PKGTOOL.Bitmap.pic0.Image;
            //}
            //else if (flatTabControl2.SelectedTab == flatTabControl2.TabPages["pic1"])//your specific tabname
            //{
            //    pbPIC1.Image = PS4PKGTOOL.Bitmap.pic1.Image;
            //}
            darkContextMenuBitmapPIC1.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void bgwExtractAllPKGImage_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Enabled = false;
            string ButtonName = (string)e.Argument;
            PS4PKGTOOL.PKG.CountPKG = 0;
            PS4PKGTOOL.Bitmap.FailExtractImageList = "";
            toolStripProgressBar1.Maximum = PS4PKGTOOL.PKG.TotalPKG;
            List<string> listIndex0 = new List<string>();
            foreach (DataGridViewRow item in darkDataGridView1.Rows)
            {
                listIndex0.Add(item.Cells[0].Value.ToString());
            }

            List<string> listIndex8 = new List<string>();
            foreach (DataGridViewRow item in darkDataGridView1.Rows)
            {
                listIndex8.Add(item.Cells[12].Value.ToString());
            }

            var numbersAndWords = listIndex8.Zip(listIndex0, (n, w) => new { Index9 = n, Index0 = w });
            foreach (var nw in numbersAndWords)
            {
                try
                {
                    string currentPKG = nw.Index9 + "\\" + nw.Index0;
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(currentPKG);
                    string final_path = "";

                    if (PS4PKGTOOL.Bitmap.respectiveExtract == true)
                    {
                        final_path = PS4PKGTOOL.Bitmap.ExtractImageOutputDirectory + @"\" + PS4_PKG.PS4_Title + @"\";
                        Directory.CreateDirectory(final_path);
                    }
                    else { final_path = PS4PKGTOOL.Bitmap.ExtractImageOutputDirectory; }

                    switch (ButtonName)
                    {
                        case "all":
                            if (PS4_PKG.Image != null)
                            {
                                using (Bitmap tempImage = new Bitmap(PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Image)))
                                {
                                    if (PS4PKGTOOL.Bitmap.respectiveExtract == true)
                                    {
                                        tempImage.Save(final_path + "PIC0.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    else
                                    { tempImage.Save(final_path + "\\" + PS4_PKG.PS4_Title + "_PIC0.PNG", System.Drawing.Imaging.ImageFormat.Png); }

                                }
                            }

                            if (PS4_PKG.Image2 != null)
                            {
                                using (Bitmap tempImage = new Bitmap(PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Image2)))
                                {
                                    if (PS4PKGTOOL.Bitmap.respectiveExtract == true)
                                    {
                                        tempImage.Save(final_path + "PIC1.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    else { tempImage.Save(final_path + "\\" + PS4_PKG.PS4_Title + "_PIC1.PNG", System.Drawing.Imaging.ImageFormat.Png); }

                                }
                            }

                            if (PS4_PKG.Icon != null)
                            {
                                using (Bitmap tempImage = new Bitmap(PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Icon)))
                                {
                                    if (PS4PKGTOOL.Bitmap.respectiveExtract == true)
                                    {
                                        tempImage.Save(final_path + "ICON.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    else { tempImage.Save(final_path + "\\" + PS4_PKG.PS4_Title + "_ICON.PNG", System.Drawing.Imaging.ImageFormat.Png); }

                                }
                            }
                            break;

                        case "icon":
                            if (PS4_PKG.Icon != null)
                            {
                                using (Bitmap tempImage = new Bitmap(PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Icon)))
                                {
                                    if (PS4PKGTOOL.Bitmap.respectiveExtract == true)
                                    {
                                        tempImage.Save(final_path + "ICON.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    else { tempImage.Save(final_path + "\\" + PS4_PKG.PS4_Title + ".PNG", System.Drawing.Imaging.ImageFormat.Png); }
                                }
                            }
                            break;

                        case "image":
                            if (PS4_PKG.Image != null)
                            {
                                using (Bitmap tempImage = new Bitmap(PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Image)))
                                {
                                    if (PS4PKGTOOL.Bitmap.respectiveExtract == true)
                                    {
                                        tempImage.Save(final_path + "PIC0.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    else
                                        tempImage.Save(final_path + "\\" + PS4_PKG.PS4_Title + "_PIC0.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }

                            if (PS4_PKG.Image2 != null)
                            {
                                using (Bitmap tempImage = new Bitmap(PS4PKGTOOL.Bitmap.BytesToBitmap(PS4_PKG.Image2)))
                                {
                                    if (PS4PKGTOOL.Bitmap.respectiveExtract == true)
                                    {
                                        tempImage.Save(final_path + "PIC1.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    else
                                        tempImage.Save(final_path + "\\" + PS4_PKG.PS4_Title + "_PIC1.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }
                            break;
                    }

                    toolStripProgressBar1.Increment(1);
                    PS4PKGTOOL.PKG.CountPKG++;
                    toolStripStatusLabel2.Text = "Extracting images.. " + "(" + PS4PKGTOOL.PKG.CountPKG.ToString() + "/" + PS4PKGTOOL.PKG.TotalPKG.ToString() + ") ";
                }
                catch (Exception a)
                {
                    PS4PKGTOOL.Bitmap.FailExtractImageList += nw.Index9 + " : " + a.Message + "\n";
                }
            }
        }

        private void bgwExtractAllPKGImage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PS4PKGTOOL.Bitmap.FailExtractImageList))
            {
                DarkMessageBox.ShowWarning("Some PKG fail to extract : \n\n" + PS4PKGTOOL.Bitmap.FailExtractImageList, "PS4 PKG Tool");
                Logger.log("Some PKG fail to extract : \n\n" + PS4PKGTOOL.Bitmap.FailExtractImageList);
            }
            else
            {
                DarkMessageBox.ShowInformation("Images extracted.", "PS4 PKG Tool");
                Logger.log("Images extracted.");
            }

            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Value = 0;
            this.Enabled = true;
        }

        private void bgwRenamePKG_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Enabled = false;
            string ButtonName = (string)e.Argument;
            PS4PKGTOOL.PKG.CountPKG = 0;
            toolStripProgressBar1.Maximum = PS4PKGTOOL.PKG.TotalPKG;
            PS4PKGTOOL.PKG.CountFailRename = 0;
            PS4PKGTOOL.PKG.ListFailRename = "";

            List<string> listIndex0 = new List<string>();
            foreach (DataGridViewRow item in darkDataGridView1.Rows)
            {
                listIndex0.Add(item.Cells[0].Value.ToString());
            }

            List<string> listIndex9 = new List<string>();
            foreach (DataGridViewRow item in darkDataGridView1.Rows)
            {
                listIndex9.Add(item.Cells[12].Value.ToString());
            }

            var numbersAndWords = listIndex9.Zip(listIndex0, (n, w) => new { Index9 = n, Index0 = w });
            foreach (var nw in numbersAndWords)
            {
                string pkgfile = nw.Index9 + "\\" + nw.Index0;

                try
                {

                    string dir = Path.GetDirectoryName(pkgfile);
                    //DarkMessageBox.ShowInformation(fullpath);

                    //DarkMessageBox.ShowInformation(dir + @"\");
                    switch (ButtonName)
                    {
                        case "TITLE":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title(pkgfile, dir + @"\");
                            break;

                        case "TITLE_TITLE_ID":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Title_ID(pkgfile, dir + @"\");
                            break;

                        case "TITLE_TITLE_ID_VERSION":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Title_ID_Version(pkgfile, dir + @"\");
                            break;

                        case "TITLE_CATEGORY":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Category(pkgfile, dir + @"\");
                            break;

                        case "TITLE_ID":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_TitleID(pkgfile, dir + @"\");
                            break;

                        case "CONTENT_ID":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_ContentID(pkgfile, dir + @"\");
                            break;

                        case "TITLE_ID_TITLE":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_TitleID_Title(pkgfile, dir + @"\");
                            break;

                        case "TITLE_ID_CATEGORY_VERSION_TITLE":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_TitleID_Category_Version_Title(pkgfile, dir + @"\");
                            break;

                        case "TITLE_CATEGORY_VERSION":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Category_Version(pkgfile, dir + @"\");
                            break;

                        case "CONTENT_ID_FULL":
                            PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Content_ID_Full_(pkgfile, dir + @"\");
                            break;
                    }

                    PS4PKGTOOL.PKG.CountPKG++;
                    toolStripStatusLabel2.Text = "Renaming PKG.. " + "(" + PS4PKGTOOL.PKG.CountPKG.ToString() + "/" + PS4PKGTOOL.PKG.TotalPKG.ToString() + ") ";

                    toolStripProgressBar1.Increment(1);
                }
                catch (Exception a)
                {
                    PS4PKGTOOL.PKG.CountFailRename++;
                    PS4PKGTOOL.PKG.ListFailRename += Path.GetFileNameWithoutExtension(pkgfile) + " : " + a.Message + "\n";
                }
            }
        }

        private void bgwRenamePKG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Logger.log("Done.");
            if (PS4PKGTOOL.PKG.CountFailRename != 0)
            {
                DarkMessageBox.ShowWarning(PS4PKGTOOL.PKG.CountFailRename + " PKG fail to rename. See program log to view the errors.", "PS4 PKG Tool");
                Logger.log(PS4PKGTOOL.PKG.CountFailRename + " PKG fail to rename :");
                Logger.log(PS4PKGTOOL.PKG.ListFailRename);
            }
          
            toolStripStatusLabel2.Text = "Refreshing PKG list.. ";

            this.Invoke((MethodInvoker)delegate
            {
                if (bgwOpenPKG.IsBusy != true)
                    bgwOpenPKG.RunWorkerAsync();
            });
            
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Value = 0;
        }

        private void bgwMorePKGTool_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Enabled = false;
            string ButtonName = (string)e.Argument;
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            
            switch (ButtonName)
            {
                case "ENTRY":
                    
                    break;
                case "TROPHY":
                   
                    break;
                case "ADDON":
                    if (PS4PKGTOOL.Tool.CheckForInternetConnection() == true)
                    {
                        if (darkDataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                        {
                            try
                            {
                                string CUSA_DLC = "";
                                string CONTENTID_DLC = "";
                                foreach (DataGridViewCell cell in darkDataGridView1.SelectedCells)
                                {
                                    int selectedrowindex = cell.RowIndex;
                                    DataGridViewRow selectedRow = darkDataGridView1.Rows[selectedrowindex];
                                    CUSA_DLC = Convert.ToString(selectedRow.Cells[1].Value);
                                    CONTENTID_DLC = Convert.ToString(selectedRow.Cells[2].Value);
                                }

                                if (CUSA_DLC != null && CONTENTID_DLC != null)
                                {
                                    try
                                    {
                                        PS4PKGTOOL.PKG.StoreItems = PS4_Tools.PKG.Official.Get_All_Store_Items("CUSA07022");
                                    }
                                    catch
                                    {
                                        PS4PKGTOOL.PKG.StoreItems = null;
                                    }


                                    if (PS4PKGTOOL.PKG.StoreItems.Count > 0)
                                    {
                                        DLC grid = new DLC(PS4PKGTOOL.PKG.StoreItems);
                                        toolStripStatusLabel2.Text = "Viewing addon.. ";
                                        BeginInvoke((MethodInvoker)delegate
                                        {
                                            grid.ShowDialog();
                                        });
                                    }
                                    else
                                    {
                                        DarkMessageBox.ShowInformation("\"" + PS4_PKG.PS4_Title + "\" has no Addon", "PS4 PKG Tool");
                                    }

                                }
                                else
                                {
                                    DarkMessageBox.ShowError("An error occured", "PS4 PKG Tool");
                                }
                            }
                            catch (System.Runtime.InteropServices.ExternalException)
                            {
                                DarkMessageBox.ShowError("The Clipboard could not be accessed. Please try again.", "PS4 PKG Tool");
                                Logger.log("The Clipboard could not be accessed.");
                            }
                        }
                    }
                    else
                    {
                        DarkMessageBox.ShowError("Network is not Available", "PS4 PKG Tool");
                    }
                    break;

                case "UPDATE":
                    if (PS4PKGTOOL.Tool.CheckForInternetConnection() == true)
                    {
                        Logger.log("Checking update for " + PS4_PKG.PS4_Title + "..");
                        toolStripStatusLabel2.Text = "Checking update.. ";
                        var item = PS4_Tools.PKG.Official.CheckForUpdate(PS4_PKG.Param.TITLEID);
                        if (item != null)
                        {
                            if (item.Tag.Package.Manifest_url != null)
                            {
                                if (this.InvokeRequired)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        toolStripStatusLabel2.Text = "Viewing update detail.. ";
                                        DownloadUpdate du = new DownloadUpdate();
                                        du.filenames = PS4PKGTOOL.PKG.SelectedPKGFilename;
                                        du.ShowDialog();
                                    });
                                }
                            }
                        }
                        else
                        {
                            DarkMessageBox.ShowInformation("\"" + PS4_PKG.PS4_Title + "\" has no update", "PS4 PKG Tool");
                            Logger.log("\"" + PS4_PKG.PS4_Title + "\" has no update.");
                        }
                    }
                    else
                    {
                        DarkMessageBox.ShowError("Network is not Available.", "PS4 PKG Tool");
                        Logger.log("Network is not Available.");
                    }
                    break;

                //case "EXTRACT_UNENCRYPTED":
                //    //string path_extract = SelectedPKGDir;
                //    try
                //    {
                //        var proc = new Process
                //        {
                //            StartInfo = new ProcessStartInfo
                //            {
                //                WorkingDirectory = Settings.WorkingDirectory,
                //                FileName = Settings.WorkingDirectory + @"orbis_pub_cmd.exe",
                //                Arguments = " img_extract --no_passcode \"" + Settings.PKG.SelectedPKGFilename + "\" \"" + extractTemp + "\"",
                //                UseShellExecute = false,
                //                RedirectStandardOutput = true,
                //                CreateNoWindow = true
                //            }
                //        };
                //        proc.Start();
                //        proc.WaitForExit();

                //        if (proc.ExitCode == 0)
                //        {
                //            try
                //            {
                //                string[] array;

                //                array = Directory.GetFiles(extractTemp);
                //                Settings.PKG.extracted_item.AddRange(array);

                //                string[] folders = Directory.GetDirectories(extractTemp);
                //                foreach (string folder in folders)
                //                {
                //                    try
                //                    {
                //                        Settings.PKG.extracted_item.AddRange(Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories));

                //                    }
                //                    catch { } // Don't know what the problem is, don't care...
                //                }

                //                Settings.PKG.extracted_item.ToArray();

                //                foreach (var item in Settings.PKG.extracted_item)
                //                {
                //                    string filename = Path.GetFileName(item);
                //                    File.Copy(item, path_extract + "\\" + filename);
                //                }

                //                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(Settings.PKG.SelectedPKGFilename);
                //                DarkMessageBox.ShowInformation("Content for \"" + PS4_PKG.PS4_Title + "\" was extracted to : " + path_extract, "PS4 PKG Tool");
                //                Settings.PKG.extracted_item.Clear();
                //            }
                //            catch (Exception error)
                //            {
                //                DarkMessageBox.ShowError(error.Message.ToString(), "PS4 PKG Tool");
                //            }
                //        }
                //        else if (proc.ExitCode == 1)
                //        {
                //            DarkMessageBox.ShowError("An error occured", "PS4 PKG Tool");
                //        }
                //    }
                //    catch (Exception error)
                //    {
                //        DarkMessageBox.ShowError(error.Message.ToString(), "PS4 PKG Tool");
                //    }
                //    break;

                case "EXPORT":
                    try
                    {
                        Logger.log("Exporting PKG list..");
                        toolStripStatusLabel2.Text = "Exporting PKG list.. ";
                        var wb = new XLWorkbook();

                        // Add a DataTable as a worksheet
                        wb.Worksheets.Add(DataTableExportExcel, "PS4 PKG Tool");

                        wb.SaveAs(ExportPKGtoExcel);

                        DarkMessageBox.ShowInformation("PKG list exported.", "PS4 PKG Tool");
                        Logger.log("PKG list exported.");
                    }
                    catch (Exception s)
                    {
                        DarkMessageBox.ShowError(s.Message, "PS4 PKG Tool");
                        Logger.log(s.Message);
                    }
                    break;
            }
        }

        private void bgwMorePKGTool_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel2.Text = "... ";
            this.Enabled = true;
        }

     
      

        private void toolStripMenuItem96_Click(object sender, EventArgs e)
        {
            RefreshPkgList();
        }


        private void toolStripMenuItem120_Click(object sender, EventArgs e)
        {
            exportPkgListToExcel();
        }

        private void toolStripMenuItem125_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Checking addon.. ";
            darkMenuStrip1.Enabled = false;
            darkDataGridView1.Enabled = false;
            darkDataGridView2.Enabled = false;
            pictureBox1.Enabled = false;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            tbSearchGame.Enabled = false;


            if (!bgwMorePKGTool.IsBusy)
                bgwMorePKGTool.RunWorkerAsync("ADDON");
        }

        private void CheckPkgUpdate()
        {
            if (!bgwMorePKGTool.IsBusy)
                bgwMorePKGTool.RunWorkerAsync("UPDATE");
        }

        private void toolStripMenuItem126_Click(object sender, EventArgs e)
        {
            CheckPkgUpdate();
        }

        private void toolStripMenuItem128_Click(object sender, EventArgs e)
        {
            CopyTitleID();
        }

        private void CopyContentID()
        {
            if (PS4PKGTOOL.PKG.SelectedPKGFilename != "")
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
                string filter = PS4_PKG.Param.ContentID.Replace(":", " -");
                string title_filter_final = filter.Replace("  -", " -");
                Clipboard.SetText(title_filter_final);
                DarkMessageBox.ShowInformation("CONTENT_ID copied to clipboard", "PS4 PKG Tool");
                Logger.log("CONTENT_ID copied to clipboard");
            }
            else
            {
                DarkMessageBox.ShowError("PKG not selected.", "PS4 PKG Tool");
                Logger.log("PKG not selected. ");
            }
        }

        private void toolStripMenuItem129_Click(object sender, EventArgs e)
        {
            CopyContentID();
        }

        private void DecryptEncryptedItem()
        {
            toolStripStatusLabel2.Text = "Running decryptor.... ";
            darkMenuStrip1.Enabled = false;
            darkDataGridView1.Enabled = false;
            darkDataGridView2.Enabled = false;
            pictureBox1.Enabled = false;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            tbSearchGame.Enabled = false;

            pkgdec pkgdec = new pkgdec();
            pkgdec.filenames = PS4PKGTOOL.PKG.SelectedPKGFilename;
            pkgdec.ShowDialog();

            toolStripStatusLabel2.Text = "... ";
            darkMenuStrip1.Enabled = true;
            darkDataGridView1.Enabled = true;
            darkDataGridView2.Enabled = true;
            pictureBox1.Enabled = true;
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            tbSearchGame.Enabled = true;
        }

        private void toolStripMenuItem130_Click(object sender, EventArgs e)
        {
            DecryptEncryptedItem();
        }

        private void toolStripMenuItem131_Click(object sender, EventArgs e)
        {
            ExtractPkg();
        }

        private void toolStripMenuItem132_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "/select," + PS4PKGTOOL.PKG.SelectedPKGFilename);
        }

        private void ViewEntryList()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Logger.log("Checking entry list for " + PS4_PKG.PS4_Title + "..");
            toolStripStatusLabel2.Text = "Checking PKG entry.. ";
            this.Enabled = false; 
            CurrentEntry CurrentEntry = new CurrentEntry();
            CurrentEntry.filenames = PS4PKGTOOL.PKG.SelectedPKGFilename;
            CurrentEntry.ShowDialog();
            toolStripStatusLabel2.Text = "...";
            this.Enabled = true;
        }

        private void toolStripMenuItem141_Click(object sender, EventArgs e)
        {
            ViewEntryList();
        }

        private void ViewHashSigs()
        {
            toolStripStatusLabel2.Text = "Checking hashes and signatures.. ";
            darkMenuStrip1.Enabled = false;
            darkDataGridView1.Enabled = false;
            darkDataGridView2.Enabled = false;
            pictureBox1.Enabled = false;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            tbSearchGame.Enabled = false;
            //HashesAndSignatures h_s = new HashesAndSignatures();
            //h_s.filenames = Settings.PKG.SelectedPKGFilename;
            //h_s.ShowDialog();
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            darkMenuStrip1.Enabled = true;
            darkDataGridView1.Enabled = true;
            darkDataGridView2.Enabled = true;
            pictureBox1.Enabled = true;
            tbSearchGame.Enabled = true;
        }

        private void toolStripMenuItem142_Click(object sender, EventArgs e)
        {
            ViewHashSigs();
        }

        private void toolStripMenuItem143_Click(object sender, EventArgs e)
        {
            ViewTrophy();
        }

        private void toolStripMenuItem76_Click(object sender, EventArgs e)
        {
            flatTabControl1.SelectedIndex = 0;
            RefreshPkgList();
        }

        private void RefreshPkgList()
        {
            if (darkDataGridView1.DataSource != null)
            {
                Logger.log("Refreshing PKG list..");
                this.Invoke((MethodInvoker)delegate
                {
                    this.Enabled = false;
                    System.Threading.Thread.Sleep(1000);

                    while (darkDataGridView1.DataSource != null)
                    {
                        this.darkDataGridView1.DataSource = null;
                        this.darkDataGridView1.Rows.Clear();
                        darkDataGridView1.Rows.Clear();
                    }

                    while (darkDataGridView2.DataSource != null)
                    {
                        this.darkDataGridView2.DataSource = null;
                        this.darkDataGridView2.Rows.Clear();
                        darkDataGridView2.Rows.Clear();
                    }

                    PS4PKGTOOL.DoThis = true;
                    scanPKG();
                });
            }
        }


        private void toolStripMenuItem78_Click(object sender, EventArgs e)
        {
            DialogResult dialog = DarkMessageBox.DialogYesNo("Are you sure you wish to exit?", "PS4 PKG Tool");
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void toolStripMenuItem160_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ko-fi.com/pearlxcore");
        }

        private async void toolStripMenuItem158_Click(object sender, EventArgs e)
        {
            if (PS4PKGTOOL.Tool.CheckForInternetConnection() == true)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;

                var checker = new UpdateChecker("pearlxcore", "PS4-PKG-Tool", "v1.3"); // latest version on github release

                UpdateType update = await checker.CheckUpdate();

                if (update == UpdateType.None)
                {
                    DarkMessageBox.ShowInformation("The program is up to date.", "PS4 PKG Tool");
                }
                else
                {
                    // Ask the user if he wants to update
                    // You can use the prebuilt form for this if you want (it's really pretty!)
                    var result = new UpdateNotifyDialog(checker).ShowDialog();
                    if (result == DialogResult.Yes)
                    {
                        checker.DownloadAsset("PS4.PKG.Tool.zip"); // opens it in the user's browser
                    }
                }
            }
        }

        private void toolStripMenuItem159_Click(object sender, EventArgs e)
        {
            DarkMessageBox.ShowInformation("PS4 PKG Tool v1.3" + "\n\nCopyright © pearlxcore 2021\n\nCredit to xXxTheDarkprogramerxXx and Maxton.                 ", "About PS4 PKG Tool");
        }

        private void toolStripMenuItem162_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel2.Text = "Running PKG unlocker.. ";
            //AddonUnlocker unlocker = new AddonUnlocker();
            //unlocker.ShowDialog();
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void toolStripMenuItem164_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel2.Text = "Running extract tool.. ";
            //Extract_PKG extractPKG = new Extract_PKG();
            //extractPKG.filenames = Settings.PKG.SelectedPKGFilename;
            //extractPKG.ShowDialog();
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void toolStripMenuItem165_Click(object sender, EventArgs e)
        {
            //TODO

        }

        private void toolStripMenuItem166_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel2.Text = "Checking entry list.. ";
            //Entry entry = new Entry();
            //entry.ShowDialog();
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void toolStripMenuItem167_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel2.Text = "Running extract tool.. ";
            pkgdec pkgdec = new pkgdec();
            pkgdec.filenames = PS4PKGTOOL.PKG.SelectedPKGFilename;
            pkgdec.ShowDialog();
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void RenameAll_TITLE()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming all PKG to TITLE..");
          
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("TITLE");
        }

        private void toolStripMenuItem59_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE();
        }

        private void RenameAll_TITLE_TITLE_ID()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to TITLE_TITLE_ID..");
           
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("TITLE_TITLE_ID");
        }

        private void toolStripMenuItem60_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_TITLE_ID();
        }

        private void RenameAll_TITLE_TITLE_ID_VERSION()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to TITLE_TITLE_ID_VERSION..");
          
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("TITLE_TITLE_ID_VERSION");
        }

        private void toolStripMenuItem61_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_TITLE_ID_VERSION();
        }

        private void RenameAll_TITLE_CATEGORY()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to TITLE_CATEGORY..");
           
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("TITLE_CATEGORY");
        }

        private void toolStripMenuItem62_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_CATEGORY();
        }

        private void RenameAll_TITLE_ID()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to TITLE_ID..");
          
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("TITLE_ID");
        }

        private void toolStripMenuItem63_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_ID();
        }

        private void RenameAll_TITLE_ID_CATEGORY_VERSION_TITLE()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to TITLE_ID_CATEGORY_VERSION_TITLE..");
          
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("TITLE_ID_CATEGORY_VERSION_TITLE");
        }

        private void toolStripMenuItem64_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_ID_CATEGORY_VERSION_TITLE();
        }

        private void exportPKGListToExcelToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            exportPkgListToExcel();
        }

        private void exportPkgListToExcel()
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.Filter = "*.xlsx|*.xlsx";
            if (svd.ShowDialog() == DialogResult.OK)
            {
                ExportPKGtoExcel = svd.FileName;
                if (!bgwMorePKGTool.IsBusy)
                    bgwMorePKGTool.RunWorkerAsync("EXPORT");
            }
        }

        private void checkForDLCToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "Checking addon.. ";
            Logger.log("Checking addon..");

            if (!bgwMorePKGTool.IsBusy)
                bgwMorePKGTool.RunWorkerAsync("ADDON");
        }

        private void checkForUpdateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CheckPkgUpdate();
        }

        private void CopyTitleID()
        {
            if (PS4PKGTOOL.PKG.SelectedPKGFilename != "")
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
                string filter = PS4_PKG.Param.TITLEID.Replace(":", " -");
                string title_filter_final = filter.Replace("  -", " -");
                Clipboard.SetText(title_filter_final);
                DarkMessageBox.ShowInformation("TITLE_ID copied to clipboard", "PS4 PKG Tool");
                Logger.log("TITLE_ID copied to clipboard");
            }
            else
            {
                DarkMessageBox.ShowError("PKG not selected.", "PS4 PKG Tool");
                Logger.log("PKG not selected. ");
            }
        }

        private void toolStripMenuItem74_Click(object sender, EventArgs e)
        {
            CopyTitleID();
        }

        private void toolStripMenuItem79_Click(object sender, EventArgs e)
        {
            CopyContentID();
        }

        private void decryptPKGToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DecryptEncryptedItem();
        }

        private void ExtractPkg()
        {
            DialogResult dialog = DarkMessageBox.DialogYesNo("Extract PKG content?", "PS4 PKG Tool");
            if (dialog == DialogResult.Yes)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string outputt = fbd.SelectedPath;
                    toolStripStatusLabel2.Text = "Extract PKG.. ";
                    darkMenuStrip1.Enabled = false;
                    darkDataGridView1.Enabled = false;
                    darkDataGridView2.Enabled = false;
                    pictureBox1.Enabled = false;
                    toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                    tbSearchGame.Enabled = false;

                    //ExtractSelectedPKG newform = new ExtractSelectedPKG();
                    //newform.filenames = Settings.PKG.SelectedPKGFilename;
                    //newform.output = outputt;
                    //newform.ShowDialog();

                    toolStripStatusLabel2.Text = "... ";
                    darkMenuStrip1.Enabled = true;
                    darkDataGridView1.Enabled = true;
                    darkDataGridView2.Enabled = true;
                    pictureBox1.Enabled = true;
                    toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                    tbSearchGame.Enabled = true;
                }
            }
        }

        private void extractPKGToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ExtractPkg();
        }

        private void openFileLocStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.log("Viewing PKG in Explorer..");
            Process.Start("explorer.exe", "/select," + PS4PKGTOOL.PKG.SelectedPKGFilename);
        }

        private void UnencryptedContentToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ViewEntryList();
        }

        private void validateHashesAndToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ViewHashSigs();
        }

        private void ViewTrophy()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            if (PS4_PKG.Trophy_File != null)
            {
                this.Enabled = false;
                Logger.log("Checking trophy for " + PS4_PKG.PS4_Title + "..");
                toolStripStatusLabel2.Text = "Viewing trophy list.. ";
                Trophy trophy = new Trophy();
                trophy.filenames = PS4PKGTOOL.PKG.SelectedPKGFilename;
                trophy.ShowDialog();
                toolStripStatusLabel2.Text = "...";
                this.Enabled = true;
            }
            else
            {
                DarkMessageBox.ShowInformation("\"" + PS4_PKG.PS4_Title + "\" has no trophy.", "PS4 PKG Tool");
                Logger.log("\"" + PS4_PKG.PS4_Title + "\" has no trophy.");
            }
        }

        private void viewTrophyListToolStripMenuItemm_Click(object sender, EventArgs e)
        {
            ViewTrophy();
        }



        #region renamePKG

        private void updateSelectedPKGFilenameinDGV(Param_SFO.PARAM_SFO psfo, string rename_type, PS4_Tools.PKG.SceneRelated.Unprotected_PKG read)
        {
            foreach (DataGridViewCell cell in darkDataGridView1.SelectedCells)
            {
                int selectedrowindex = cell.RowIndex;
                DataGridViewRow selectedRow = darkDataGridView1.Rows[selectedrowindex];
                string filter = "";
                string title_filter_final = "";
                string version = "";
                switch (rename_type)
                {
                    case "TITLE":
                        filter = psfo.Title.Replace(":", " -").Replace("\"", "'");
                        title_filter_final = filter.Replace("  -", " -");
                        selectedRow.Cells[0].Value = title_filter_final + ".pkg";
                        break;
                    case "TITLE_TITLEID":
                        filter = psfo.Title.Replace(":", " -").Replace("\"", "'");
                        title_filter_final = filter.Replace("  -", " -");
                        selectedRow.Cells[0].Value = title_filter_final + " [" + psfo.TITLEID + "]" + ".pkg";
                        break;
                    case "TITLE_TITLEID_VERSION":
                        filter = psfo.Title.Replace(":", " -").Replace("\"", "'");
                        title_filter_final = filter.Replace("  -", " -");
                        if (read.PKG_Type.ToString() == "Addon")
                        {
                            selectedRow.Cells[0].Value = title_filter_final + " [" + psfo.TITLEID + "].pkg";
                        }
                        else
                        {
                            selectedRow.Cells[0].Value = title_filter_final + " [" + psfo.TITLEID + "] [v" + psfo.APP_VER + "].pkg";
                        }
                        break;
                    case "TITLE_CATEGORY":
                        filter = psfo.Title.Replace(":", " -").Replace("\"", "'");
                        title_filter_final = filter.Replace("  -", " -");
                        selectedRow.Cells[0].Value = title_filter_final + " [" + read.PKG_Type + "].pkg";
                        break;
                    case "TITLEID":
                        selectedRow.Cells[0].Value = psfo.TITLEID + ".pkg";
                        break;
                    case "TITLEID_TITLE":
                        filter = psfo.Title.Replace(":", " -").Replace("\"", "'");
                        title_filter_final = filter.Replace("  -", " -");
                        selectedRow.Cells[0].Value = "[" + psfo.TITLEID + "] " + title_filter_final + ".pkg";
                        break;
                    case "CONTENT_ID":
                        selectedRow.Cells[0].Value = psfo.ContentID + ".pkg";
                        break;
                    case "TITLE_ID_CATEGORY_VERSION_TITLE":
                        filter = psfo.Title.Replace(":", " -").Replace("\"", "'");
                        title_filter_final = filter.Replace("  -", " -");
                        if (read.PKG_Type.ToString() == "Addon")
                        {
                            selectedRow.Cells[0].Value = "[" + psfo.TitleID + "] [" + read.PKG_Type + "] " + title_filter_final + ".pkg";
                        }
                        else
                        {
                            selectedRow.Cells[0].Value = "[" + psfo.TitleID + "] [" + read.PKG_Type + "] [v" + psfo.APP_VER + "] " + title_filter_final + ".pkg";
                        }
                        break;
                    case "TITLE_CATEGORY_VERSION":
                        filter = psfo.Title.Replace(":", " -").Replace("\"", "'");
                        title_filter_final = filter.Replace("  -", " -");
                        if (read.PKG_Type.ToString() == "Addon")
                        {
                            selectedRow.Cells[0].Value = title_filter_final + " [" + read.PKG_Type + "].pkg";
                        }
                        else
                        {
                            selectedRow.Cells[0].Value = title_filter_final + " [" + read.PKG_Type + "] [v" + psfo.APP_VER + "].pkg";
                        }
                        break;
                    case "CONTENT_ID_FULL":
                        if (PS4PKGTOOL.PKG.isPkgGamePatchAppUnknown(read) == true)
                        {
                            foreach (Param_SFO.PARAM_SFO.Table t in read.Param.Tables.ToList())
                            {
                                if (t.Name == "VERSION")
                                {
                                    version = t.Value.Replace(".", ""); //convert value from string to int

                                }
                            }
                            selectedRow.Cells[0].Value = psfo.ContentID + "-A" + psfo.APP_VER.Replace(".", "") + "-V" + version.Replace(".", "") + ".pkg";
                        }
                        else
                        {
                            foreach (Param_SFO.PARAM_SFO.Table t in read.Param.Tables.ToList())
                            {
                                if (t.Name == "VERSION")
                                {
                                    version = t.Value.Replace(".", ""); //convert value from string to int

                                }
                            }
                            selectedRow.Cells[0].Value = psfo.ContentID + "-A0000-V" + version.Replace(".", "") + ".pkg";
                        }
                        break;
                }
                PS4PKGTOOL.PKG.SelectedPKGFilename = Convert.ToString(selectedRow.Cells[12].Value) + "\\" + Convert.ToString(selectedRow.Cells[0].Value);
            }
            Logger.log("Done.");

        }

        private void renamePKGtoTITLE()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);

            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to TITLE..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "TITLE", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool");
                Logger.log(a.Message);

            }

        }

        private void renamePKGtoTITLE_TITLEID()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to TITLE_TITLEID..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Title_ID(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "TITLE_TITLEID", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);

            }

        }

        private void renamePKGtoTITLE_TITLEID_VERSION()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to TITLE_TITLEID_VERSION..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Title_ID_Version(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "TITLE_TITLEID_VERSION", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);
            }

        }

        private void renamePKGtoTITLE_CATEGORY()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to TITLE_CATEGORY..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Category(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "TITLE_CATEGORY", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);
            }

        }

        private void renamePKGtoTITLEID()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to TITLEID..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_TitleID(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "TITLEID", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);
            }

        }

        private void renamePKGtoTITLEID_TITLE()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to TITLEID_TITLE..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_TitleID_Title(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "TITLEID_TITLE", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);
            }

        }

        private void renamePKGtoCONTENT_ID()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to CONTENT_ID..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_ContentID(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "CONTENT_ID", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);
            }

        }

        private void renamePKGtoCONTENT_ID_2()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to CONTENT_ID_2..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Content_ID_Full_(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "CONTENT_ID_FULL", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);
            }

        }


        private void renamePKGtoTITLEID_CATEGORY_VERSION_TITLE()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to TITLE_ID_CATEGORY_VERSION_TITLE..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_TitleID_Category_Version_Title(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "TITLE_ID_CATEGORY_VERSION_TITLE", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);
            }

        }

        private void renamePKGtoTITLE_CATEGORY_VERSION()
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;
            //DarkMessageBox.ShowInformation(psfo.Title);

            try
            {
                Logger.log("Renaming selected PKG to TITLE_CATEGORY_VERSION..");
                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Category_Version(PS4PKGTOOL.PKG.SelectedPKGFilename, Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename) + @"\");
                updateSelectedPKGFilenameinDGV(psfo, "TITLE_CATEGORY_VERSION", read);

            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool"); Logger.log(a.Message);
            }

        }

        #endregion renamePKG

        private void toolStripMenuItem84_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE();
        }

        private void toolStripMenuItem85_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE_TITLEID();
        }

        private void toolStripMenuItem86_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE_TITLEID_VERSION();
        }

        private void toolStripMenuItem87_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE_CATEGORY();
        }

        private void toolStripMenuItem88_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLEID();
        }

        private void toolStripMenuItem89_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLEID_TITLE();
        }

        private void toolStripMenuItem90_Click(object sender, EventArgs e)
        {
            renamePKGtoCONTENT_ID();
        }

        private void toolStripMenuItem112_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE();
        }

        private void toolStripMenuItem113_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_TITLE_ID();
        }

        private void toolStripMenuItem114_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_TITLE_ID_VERSION();
        }

        private void toolStripMenuItem115_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_CATEGORY();
        }

        private void toolStripMenuItem116_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_ID();
        }

        private void toolStripMenuItem118_Click(object sender, EventArgs e)
        {
            RenameAll_CONTENT_ID();
        }

        private void toolStripMenuItem117_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_ID_TITLE();
        }

        private void RenameAll_TITLE_ID_TITLE()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to TITLE_ID_TITLE..");
           
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("TITLE_ID_TITLE");
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_ID_TITLE();
        }

        #region contextMenuRenameSelectedPKG
        private void toolStripMenuItem134_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE();
        }

        private void toolStripMenuItem135_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE_TITLEID();
        }

        private void toolStripMenuItem136_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE_TITLEID_VERSION();
        }

        private void toolStripMenuItem137_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE_CATEGORY();
        }

        private void toolStripMenuItem138_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLEID();
        }

        private void toolStripMenuItem139_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLEID_TITLE();
        }

        private void toolStripMenuItem140_Click(object sender, EventArgs e)
        {
            renamePKGtoCONTENT_ID();
        }
        #endregion contextMenuRenameSelectedPKG

        private void darkTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panel5.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private async void toolStripMenuItem163_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel2.Text = "Running PKG merge.. ";
            //CombinePKG CombinePKG = new CombinePKG();
            //CombinePKG.ShowDialog();
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;

            /*
            using (var ofd = new OpenFileDialog() 
            { Title = "Select part 0", Filter = "PKG files (*_0.PKG)|*_0.PKG|All files (*.*)|*.*" })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                var filenames = new List<string>();
                filenames.Add(ofd.FileName);
                ulong pkgSize = 0;
                long remainingSize = 0;
                using (var s = File.OpenRead(filenames[0]))
                {
                    var hdr = new LibOrbisPkg.PKG.PkgReader(s).ReadHeader();
                    pkgSize = hdr.package_size;
                    remainingSize = (long)hdr.package_size - s.Length;
                }
                if (remainingSize <= 0)
                {
                    MessageBox.Show("Error: reported package size was less than part file size");
                    return;
                }

                // remove the _0.pkg (6 characters) from the filename
                var baseFilename = filenames[0].Substring(0, filenames[0].Length - 6);
                var targetFilename = baseFilename + ".pkg";
                using (var sfd = new SaveFileDialog()
                {
                    Title = "Select output file",
                    Filter = "PKG Files (*.pkg)|*.pkg",
                    FileName = targetFilename
                })
                {
                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;
                    targetFilename = sfd.FileName;
                }
                var i = 0;
                while (remainingSize > 0)
                {
                    var newFile = $"{baseFilename}_{++i}.pkg";
                    if (File.Exists(newFile))
                    {
                        filenames.Add(newFile);
                        remainingSize -= new FileInfo(newFile).Length;
                    }
                    else
                    {
                        MessageBox.Show($"Error: missing part {i}, should be {remainingSize} bytes.");
                        return;
                    }
                }

                using (var logWindow = new LogWindow())
                using (var fo = File.Create(targetFilename))
                {
                    fo.SetLength((long)pkgSize);
                    logWindow.StartPosition = this.StartPosition;
                    logWindow.Show(this);
                    logWindow.GetWriter().WriteLine($"Merging files to {targetFilename}...");
                    foreach (var fn in filenames)
                    {
                        using (var fi = File.OpenRead(fn))
                        {
                            logWindow.GetWriter().WriteLine($"Copying {fn}");
                            await fi.CopyToAsync(fo);
                        }
                    }
                    logWindow.GetWriter().WriteLine("Done. Saved to " + targetFilename);
                }
            }
            */
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripStatusLabel2.Text = "Running Patch PKG builder.. ";
            //PS4_Patch_Builder PS4_Patch_Builder = new PS4_Patch_Builder();
            //PS4_Patch_Builder.ShowDialog();
            toolStripStatusLabel2.Text = "... ";
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void extractImagesAndIcon()
        {
            DialogResult dialog = DarkMessageBox.DialogYesNoCancel("Extract all image and icon to its respective folder?", "PS4 PKG Tool");
            if (dialog == DialogResult.Cancel)
                return;

            else if (dialog == DialogResult.Yes)
            {
                PS4PKGTOOL.Bitmap.respectiveExtract = true;
            }
            else if (dialog == DialogResult.No)
            {
                PS4PKGTOOL.Bitmap.respectiveExtract = false;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Logger.log("Extracting image and icon.. ");
                toolStripStatusLabel2.Text = "Extracting image and icon.. ";
                PS4PKGTOOL.Bitmap.ExtractImageOutputDirectory = fbd.SelectedPath;

                if (!bgwExtractAllPKGImage.IsBusy)
                    bgwExtractAllPKGImage.RunWorkerAsync("all");
            }
            else
                return;
        }

        private void extractImagesAndIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractImagesAndIcon();
        }

        private void extractImageOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtractImageOnly();

        }

        private void extractIconOnly()
        {
            DialogResult dialog = DarkMessageBox.DialogYesNoCancel("Extract all PKG icon to its respective folder?", "PS4 PKG Tool");
            if (dialog == DialogResult.Cancel)
                return;

            else if (dialog == DialogResult.Yes)
            {
                PS4PKGTOOL.Bitmap.respectiveExtract = true;
            }
            else if (dialog == DialogResult.No)
            {
                PS4PKGTOOL.Bitmap.respectiveExtract = false;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Logger.log("Extracting icon.. ");
                toolStripStatusLabel2.Text = "Extracting icon.. ";
                PS4PKGTOOL.Bitmap.ExtractImageOutputDirectory = fbd.SelectedPath;

                if (!bgwExtractAllPKGImage.IsBusy)
                    bgwExtractAllPKGImage.RunWorkerAsync("icon");
            }
            else
                return;
        }

        private void extractIconOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractIconOnly();
        }

        private void toolStripMenuItem6_Click_1(object sender, EventArgs e)
        {
            extractIconOnly();
        }

        private void ExtractImageOnly()
        {
            DialogResult dialog = DarkMessageBox.DialogYesNoCancel("Extract all PKG image to its respective folder?", "PS4 PKG Tool");
            if (dialog == DialogResult.Cancel)
                return;

            else if (dialog == DialogResult.Yes)
            {
                PS4PKGTOOL.Bitmap.respectiveExtract = true;
            }
            else if (dialog == DialogResult.No)
            {
                PS4PKGTOOL.Bitmap.respectiveExtract = false;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Logger.log("Extracting image.. ");
                toolStripStatusLabel2.Text = "Extracting image.. ";
                PS4PKGTOOL.Bitmap.ExtractImageOutputDirectory = fbd.SelectedPath;

                if (!bgwExtractAllPKGImage.IsBusy)
                    bgwExtractAllPKGImage.RunWorkerAsync("image");
            }
            else
                return;
        }

        private void toolStripMenuItem5_Click_1(object sender, EventArgs e)
        {
            ExtractImageOnly();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            extractImagesAndIcon();
        }

        private void selectps4pkgfolder()
        {
            this.Hide();
            PKG_Location_InForm form = new PKG_Location_InForm();
            form.ShowDialog();


            if (form.isCancelling == true)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
                this.Enabled = false;

                //disable selection changed event
                darkDataGridView1.SelectionChanged -= darkDataGridView1_SelectionChanged;

                //update UI
                darkDataGridView1.Enabled = false; //disable dgv during listing pkg
                darkDataGridView2.Enabled = false; //disable dgv during listing pkg

                scanPKG();
            }


        }

        private void managePS4PKGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectps4pkgfolder();
        }

        private void RenameAll_TITLE_CATEGORY_VERSION()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to TITLE_CATEGORY_VERSION..");
           
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("TITLE_CATEGORY_VERSION");
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_CATEGORY_VERSION();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLEID_CATEGORY_VERSION_TITLE();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE_CATEGORY_VERSION();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLEID_CATEGORY_VERSION_TITLE();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            renamePKGtoTITLE_CATEGORY_VERSION();
        }

        private void RenameAll_CONTENT_ID()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to CONTENT_ID..");
         
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("CONTENT_ID");
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            RenameAll_CONTENT_ID();
        }

        private void bgwOpenPKG_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                PS4PKGTOOL.PKG.SelectedPKGFilename = null;
                pictureBox1.Image = null;
                darkLabel1.Text = "";
            });

            PS4PKGTOOL.PKG.scannedPKG.Clear();
            PS4PKGTOOL.PKG.validPS4PKG.Clear();
            PS4PKGTOOL.PKG.idEntryList.Clear();
            PS4PKGTOOL.PKG.nameEntryList.Clear();
            PS4PKGTOOL.PKG.TotalPKG = 0;
            PS4PKGTOOL.PKG.CountPKG = 0;
            PS4PKGTOOL.PKG.Game = 0;
            PS4PKGTOOL.PKG.Patch = 0;
            PS4PKGTOOL.PKG.App = 0;
            PS4PKGTOOL.PKG.Unknown = 0;
            PS4PKGTOOL.PKG.Addon = 0;
            toolStripProgressBar1.Value = 0;

            List<string> list = new List<string>();
            foreach (var folder in Properties.Settings.Default.PKGLocationList)
            {
                if (Properties.Settings.Default.ScanRecursive == true)
                {
                    foreach (var item in Directory.GetFiles(folder, "*.PKG", SearchOption.AllDirectories))
                    {
                        list.Add(item);
                    }
                }
                else
                {
                    foreach (var item in Directory.GetFiles(folder, "*.PKG", SearchOption.TopDirectoryOnly))
                    {
                        list.Add(item);
                    }
                }

            }

            //create datatable
            DataTableExportExcel = new DataTable(); //will be used for exporting datatable to excel
            DataTableExportExcel.Clear();
            DataTableExportExcel.Columns.Clear();

            //add column
            DataTableExportExcel.Columns.Add("Filename");
            DataTableExportExcel.Columns.Add("Title ID");
            DataTableExportExcel.Columns.Add("Content ID");
            DataTableExportExcel.Columns.Add("Region");
            DataTableExportExcel.Columns.Add("System Firmware");
            DataTableExportExcel.Columns.Add("Version");
            DataTableExportExcel.Columns.Add("PKG Type");
            DataTableExportExcel.Columns.Add("Category");
            DataTableExportExcel.Columns.Add("Size");
            DataTableExportExcel.Columns.Add("PSVR");
            DataTableExportExcel.Columns.Add("PS4 Pro Enhanced");
            DataTableExportExcel.Columns.Add("PS5 BC");
            DataTableExportExcel.Columns.Add("Location");

            DataTable dttemp = new DataTable();
            dttemp.Clear();
            dttemp.Columns.Clear();

            //add column
            dttemp.Columns.Add("Filename");
            dttemp.Columns.Add("Title ID");
            dttemp.Columns.Add("Content ID");
            dttemp.Columns.Add("Region", typeof(byte[]));
            dttemp.Columns.Add("System Firmware");
            dttemp.Columns.Add("Version");
            dttemp.Columns.Add("PKG Type");
            dttemp.Columns.Add("Category");
            dttemp.Columns.Add("Size");
            dttemp.Columns.Add("PSVR");
            dttemp.Columns.Add("PS4 Pro Enhanced");
            dttemp.Columns.Add("PS5 BC");
            dttemp.Columns.Add("Location");

            //verify ps4 pkg and count it
            foreach (var item in list)
            {
                //filter ps4 pkg by checking magic byte
                byte[] bufferA = new byte[16];

                bufferA = PS4PKGTOOL.PKG.checkPKGType(item);
                if (PS4PKGTOOL.PKG.CompareBytes(bufferA, PS4PKGTOOL.PKG.PKGHeader) == true || PS4PKGTOOL.PKG.CompareBytes(bufferA, PS4PKGTOOL.PKG.PKGHeader1) == true || 
                    PS4PKGTOOL.PKG.CompareBytes(bufferA, PS4PKGTOOL.PKG.PKGHeader2) == true || PS4PKGTOOL.PKG.CompareBytes(bufferA, PS4PKGTOOL.PKG.PKGHeader3) == true || 
                    PS4PKGTOOL.PKG.CompareBytes(bufferA, PS4PKGTOOL.PKG.PKGHeader4) == true)
                {
                    //imageGrid.Add(BytesToBitmap(PS4_PKG.Icon));

                    // add filtered ps4 pkg to new list
                    PS4PKGTOOL.PKG.validPS4PKG.Add(item);

                    //count
                    PS4PKGTOOL.PKG.TotalPKG++;
                }
            }

            PS4PKGTOOL.PKG.validPS4PKG.ToArray();

            this.Invoke((MethodInvoker)delegate
            {
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Maximum = PS4PKGTOOL.PKG.TotalPKG;
            });

            var str = File.ReadAllText(PS4PKGTOOL.WorkingDirectory + @"ps5bc.json");
            dynamic stuff = JsonConvert.DeserializeObject(str);




            //parse each pkg info into datatable
            foreach (var item in PS4PKGTOOL.PKG.validPS4PKG)
            {
                //dynamic stuff = JsonConvert.DeserializeObject(test);
                //foreach (var obj in stuff)
                //{
                //    ShowInformation(obj.npTitleIdshort);
                //}

                string final_ps4_version = "";

                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(item);
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream = new MemoryStream(byteArray);

                //begin using darkprogrammer's great ps4 tool
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(item);
                Param_SFO.PARAM_SFO psfo = PS4_PKG.Param;

                

                //get pkg's minimum system fw
                foreach (Param_SFO.PARAM_SFO.Table t in PS4_PKG.Param.Tables.ToList())
                {
                    if (t.Name == "SYSTEM_VER")
                    {
                        int value = Convert.ToInt32(t.Value); //convert value from string to int
                        string hexOutput = String.Format("{0:X}", value); //we want output as hex
                        string first_three = hexOutput.Substring(0, 3); //get only 1st 3 digit
                        final_ps4_version = first_three.Insert(1, "."); // final value and added dot
                                                                        //DarkMessageBox.ShowInformation(final_ps4_version + "\n" + tesdttt.ToString());
                    }
                }
                //pkg full size
                long filesize = new System.IO.FileInfo(item).Length;
                var size = Convert.ToInt64(filesize);
                var size_final = ByteSize.FromBytes(size).ToString(); //using 'bytesize' to display the value
                                                                      //file name
                string Filename_only = Path.GetFileName(item);
                //pkg location
                string path_only = Path.GetDirectoryName(item);
                string psVr = "";
                string neoEnable = "";
                string ps5bc = "";

                foreach (var obj in stuff)
                {
                    if (obj.npTitleIdshort == PS4_PKG.Param.TITLEID)
                    {
                        string titleID = obj.npTitleIdshort;
                        string psvr = obj.psVr;
                        string neo = obj.neoEnable;
                        string ps5bc_ = obj.ps5bc;
                        //MessageBox.Show(titleID + ", " + psvr + ", " + neo + ", " + ps5bc_);

                        //ps4vr
                        if (psvr == "1" || psvr == "2")
                            psVr = "Yes";
                        else if (psvr == "0")
                            psVr = "No";
                        else if (psvr != "null")
                            psVr = "N/A";
                        //ps4 pro enhanced
                        if (neo == "1")
                            neoEnable = "Yes";
                        else if (neo == "0")
                            neoEnable = "No";
                        else if (psvr != "null")
                            neoEnable = "N/A";
                        //ps5bc
                        ps5bc = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(ps5bc_.Replace("_", " ").ToLower());
                    }
                }

                //get region from content id
                var imageConverter = new ImageConverter();
                string regionString = "";
                byte[] regionIcon = null;
                var getLetter = PS4_PKG.Param.ContentID.Substring(0, 1); //get first letter from content id
                if (getLetter == "E") { regionString = "EU"; regionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.eu, typeof(byte[])); }
                else if (getLetter == "U") { regionString = "US"; regionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.us, typeof(byte[])); }
                else if (getLetter == "I") { regionString = "US"; regionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.us, typeof(byte[])); }
                else if (getLetter == "J") { regionString = "JAPAN"; regionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.jp, typeof(byte[])); }
                else if (getLetter == "H") { regionString = "HONG KONG"; regionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.hk, typeof(byte[])); }
                else if (getLetter == "A") { regionString = "ASIA"; regionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.asia, typeof(byte[])); }
                else if (getLetter == "K") { regionString = "KR"; regionIcon = (byte[])imageConverter.ConvertTo(Properties.Resources.kr, typeof(byte[])); }

                //add each item to datatable

                if (PS4_PKG.PKG_Type.ToString() == "Addon")
                {
                    DataTableExportExcel.Rows.Add(Filename_only, PS4_PKG.Param.TITLEID, PS4_PKG.Param.ContentID, regionString, "NA", "NA", PS4_PKG.PKGState, PS4_PKG.PKG_Type, size_final, psVr, neoEnable, ps5bc, path_only);
                    dttemp.Rows.Add(Filename_only, PS4_PKG.Param.TITLEID, PS4_PKG.Param.ContentID, regionIcon, "NA", "NA", PS4_PKG.PKGState, PS4_PKG.PKG_Type, size_final, psVr, neoEnable, ps5bc, path_only);

                }
                else
                {
                    DataTableExportExcel.Rows.Add(Filename_only, PS4_PKG.Param.TITLEID, PS4_PKG.Param.ContentID, regionString, final_ps4_version, psfo.APP_VER, PS4_PKG.PKGState, PS4_PKG.PKG_Type, size_final, psVr, neoEnable, ps5bc, path_only);
                    dttemp.Rows.Add(Filename_only, PS4_PKG.Param.TITLEID, PS4_PKG.Param.ContentID, regionIcon, final_ps4_version, psfo.APP_VER, PS4_PKG.PKGState, PS4_PKG.PKG_Type, size_final, psVr, neoEnable, ps5bc, path_only);
                } //increase value(1) each process

                if (PS4_PKG.PKG_Type.ToString() == "Game")
                    PS4PKGTOOL.PKG.Game++;

                else if (PS4_PKG.PKG_Type.ToString() == "Patch")
                    PS4PKGTOOL.PKG.Patch++;

                else if (PS4_PKG.PKG_Type.ToString() == "App")
                    PS4PKGTOOL.PKG.App++;

                else if (PS4_PKG.PKG_Type.ToString() == "Addon")
                    PS4PKGTOOL.PKG.Addon++;
                else
                    PS4PKGTOOL.PKG.Unknown++;

                if (PS4_PKG.PKGState.ToString() == "Official")
                {
                    PS4PKGTOOL.PKG.Official++;
                }
                else if (PS4_PKG.PKGState.ToString() == "Fake")
                {
                    PS4PKGTOOL.PKG.Fake++;
                }
                else if (PS4_PKG.PKGState.ToString() == "Addon_Unlocker")
                {
                    PS4PKGTOOL.PKG.Addon_Unlocker++;
                }

                PS4PKGTOOL.PKG.CountPKG++;

                darkStatusStrip1.Invoke((MethodInvoker)delegate
                {
                    toolStripStatusLabel2.Text = "Adding PS4 PKG.. " + "(" + PS4PKGTOOL.PKG.CountPKG.ToString() + "/" + PS4PKGTOOL.PKG.TotalPKG.ToString() + ") ";
                    toolStripProgressBar1.Increment(1);
                    darkDataGridView1.DataSource = dttemp;
                    darkDataGridView1.Refresh();
                });

            }


            darkDataGridView1.SelectionChanged += darkDataGridView1_SelectionChanged;

            //sort file name ascending
        }

        private void bgwOpenPKG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // no pkg found
            if (PS4PKGTOOL.PKG.TotalPKG == 0)
            {

                //clear dgv
                while (darkDataGridView1.DataSource != null)
                {
                    this.darkDataGridView1.DataSource = null;
                    this.darkDataGridView1.Rows.Clear();
                    darkDataGridView1.Rows.Clear();
                }
                while (darkDataGridView2.DataSource != null)
                {
                    this.darkDataGridView2.DataSource = null;
                    this.darkDataGridView2.Rows.Clear();
                    darkDataGridView2.Rows.Clear();

                }

                #region UpdateUI
                //update UI
                //pictureBox1.Image = null;
                //tbSearchGame.Visible = false;
                //label3.Visible = false;
                //darkLabel2.Visible = false;
                //label5.Visible = true;
                //toolStripStatusLabel1.Text = "Displaying " + darkDataGridView1.Rows.Count.ToString() + " PS4 PKG ";
                //toolStripStatusLabel3.Text = "";
                //pictureBox1.Enabled = false;

                //'File' strip menu
                //reloadContentToolStripMenuItem.Enabled = false;


                ////'Tool' strip menu
                //renameToolStripMenuItem.Enabled = false;
                //extractImageAndBackgroundToolStripMenuItem.Enabled = false;
                //exportPKGListToExcelToolStripMenuItem1.Enabled = false;
                //checkForDLCToolStripMenuItem.Enabled = false;
                //toolStripMenuItemDeletePKG.Enabled = false;
                //checkForUpdateToolStripMenuItem.Enabled = false;
                //copyStripMenuItem.Enabled = false;
                //decryptPKGToolStripMenuItem.Enabled = false;
                //extractPKGToolStripMenuItem.Enabled = false;
                //openFileLocStripMenuItem.Enabled = false;
                //renameCurrentPKGStripMenuItem.Enabled = false;
                //UnencryptedContentToolStripMenuItem.Enabled = false;
                //validateHashesAndToolStripMenuItem.Enabled = false;
                //viewTrophyListToolStripMenuItemm.Enabled = false;
                #endregion UpdateUI

                DarkMessageBox.ShowInformation("No PKG found", "PS4 PKG Tool");
                Logger.log("PKG not found.");

                selectps4pkgfolder();

            }
            else
            {

                #region UpdateUI
                //'File ' strip menu
                //openGameFolderToolStripMenuItem.Enabled = true;
                //reloadContentToolStripMenuItem.Enabled = true;

                //pictureBox1.Enabled = true;
                //darkDataGridView1.Enabled = true;
                //darkLabel1.Enabled = true;
                //darkDataGridView2.Enabled = true;
                //tbSearchGame.Visible = true;
                //darkLabel2.Visible = true;
                //toolStripStatusLabel1.Enabled = true;

                //'Tool' strip menu
                //renameToolStripMenuItem.Enabled = true;
                //extractImageAndBackgroundToolStripMenuItem.Enabled = true;
                //exportPKGListToExcelToolStripMenuItem1.Enabled = true;
                //checkForDLCToolStripMenuItem.Enabled = true;
                //toolStripMenuItemDeletePKG.Enabled = true;
                //checkForUpdateToolStripMenuItem.Enabled = true;
                //copyStripMenuItem.Enabled = true;
                //decryptPKGToolStripMenuItem.Enabled = true;
                //extractPKGToolStripMenuItem.Enabled = true;
                //openFileLocStripMenuItem.Enabled = true;
                //renameCurrentPKGStripMenuItem.Enabled = true;
                //UnencryptedContentToolStripMenuItem.Enabled = true;
                //validateHashesAndToolStripMenuItem.Enabled = true;
                //viewTrophyListToolStripMenuItemm.Enabled = true;

                
                #endregion UpdateUI

                if(PS4PKGTOOL.DoThis == true)
                {
                    PS4PKGTOOL.DoThis = false;

                    Logger.log("Extracting PKG background music..");

                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.WorkerSupportsCancellation = true;
                    bgw.DoWork += (s, a) =>
                    {
                        PS4PKGTOOL.BGM.ExtractBGM();
                    };
                    bgw.RunWorkerCompleted += (s, a) =>
                    {
                        PS4PKGTOOL.BGM.extractAT9Done = true;
                    };
                    bgw.RunWorkerAsync();

                    Logger.log("Checking hardisk free space..");

                    toolStripSplitButton1.DropDownItems.Clear();

                    try
                    {
                        foreach (var drive in DriveInfo.GetDrives())
                        {
                            long FreeSpace = drive.TotalFreeSpace;
                            long totalSpace = drive.TotalSize;
                            toolStripSplitButton1.DropDownItems.Add("[" + drive + "] Free Space : " + ByteSize.FromBytes(FreeSpace) + "/" + ByteSize.FromBytes(totalSpace));

                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.log("An error occured while getting hardisk freespace : " + ex.Message);

                    }

                    ToolStripSplitButtonTotalPKG.DropDownItems.Clear();

                    labelDisplayTotalPKG.Text = "Displaying " + darkDataGridView1.Rows.Count.ToString() + " PS4 PKG ";
                    if(PS4PKGTOOL.PKG.Game != 0)
                        ToolStripSplitButtonTotalPKG.DropDownItems.Add("Show only game PKG (" + PS4PKGTOOL.PKG.Game.ToString() + ")", null, new System.EventHandler(FilterPKGGameOnly));
                    if (PS4PKGTOOL.PKG.Patch != 0)
                        ToolStripSplitButtonTotalPKG.DropDownItems.Add("Show only patch PKG (" + PS4PKGTOOL.PKG.Patch.ToString() + ")", null, new System.EventHandler(FilterPKGPatchOnly));
                    if (PS4PKGTOOL.PKG.Addon != 0)
                        ToolStripSplitButtonTotalPKG.DropDownItems.Add("Show only addon PKG (" + PS4PKGTOOL.PKG.Addon.ToString() + ")", null, new System.EventHandler(FilterPKGAddonOnly));
                    if (PS4PKGTOOL.PKG.Unknown != 0)
                        ToolStripSplitButtonTotalPKG.DropDownItems.Add("Show only unknown PKG (" + PS4PKGTOOL.PKG.Unknown.ToString() + ")", null, new System.EventHandler(FilterPKGUnknownOnly));
                    ToolStripSplitButtonTotalPKG.DropDownItems.Add("Show all PKG", null, new System.EventHandler(FilterPKGShowAll));
                    //toolStripStatusLabel1.Text = "Displaying " + darkDataGridView1.Rows.Count.ToString() + " PS4 PKG (Game/Base PKG : " + PS4PKGTOOL.PKG.Game + ", Patch PKG : " + PS4PKGTOOL.PKG.Patch + ", Addon PKG : " + PS4PKGTOOL.PKG.Addon_Theme + ", Unknown PKG : " + PS4PKGTOOL.PKG.Unknown + ")";

                    Logger.log(darkDataGridView1.Rows.Count.ToString() + " PKG found.");

                    Logger.log("Loading PKG done.");

                }


           

                #region checkGridHideUnhide
                if (Properties.Settings.Default.title_id == true)
                {
                    darkDataGridView1.Columns[1].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[1].Visible = false;
                }

                if (Properties.Settings.Default.content_id == true)
                {
                    darkDataGridView1.Columns[2].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[2].Visible = false;
                }

                if (Properties.Settings.Default.Region == true)
                {
                    darkDataGridView1.Columns[3].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[3].Visible = false;
                }

                if (Properties.Settings.Default.system_firmware == true)
                {
                    darkDataGridView1.Columns[4].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[4].Visible = false;
                }

                if (Properties.Settings.Default.version == true)
                {
                    darkDataGridView1.Columns[5].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[5].Visible = false;
                }

                if (Properties.Settings.Default.pkg_type == true)
                {
                    darkDataGridView1.Columns[6].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[6].Visible = false;
                }

                if (Properties.Settings.Default.category == true)
                {
                    darkDataGridView1.Columns[7].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[7].Visible = false;
                }

                if (Properties.Settings.Default.size == true)
                {
                    darkDataGridView1.Columns[8].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[8].Visible = false;
                }

                if (Properties.Settings.Default.psvr == true)
                {
                    darkDataGridView1.Columns[9].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[9].Visible = false;
                }

                if (Properties.Settings.Default.neoEnable == true)
                {
                    darkDataGridView1.Columns[10].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[10].Visible = false;
                }

                if (Properties.Settings.Default.ps5bc == true)
                {
                    darkDataGridView1.Columns[11].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[11].Visible = false;
                }

                if (Properties.Settings.Default.location == true)
                {
                    darkDataGridView1.Columns[12].Visible = true;
                }
                else
                {
                    darkDataGridView1.Columns[12].Visible = false;
                }


                if (Properties.Settings.Default.BGMEnable == false)
                {
                    int NewVolume = 0; //set 0 to unmute
                    uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
                    waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);

                }
                else
                {
                    int NewVolume = 65535; //set 65535 to unmute
                    uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
                    waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);

                }

                #endregion checkGridHideUnhide


                this.Invoke((MethodInvoker)delegate
                {
                    //sort file name ascending
                    darkDataGridView1.Sort(darkDataGridView1.Columns[0], ListSortDirection.Ascending);

                    darkDataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[12].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    darkDataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    darkDataGridView1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    toolStripProgressBar1.Value = 0;
                });

                //DarkMessageBox.ShowInformation("Found " + s.ToString() + " PS4 PKG.\n\n(Game PKG : " + game.ToString() + ", Patch PKG : " + patch.ToString() + ", Application PKG : " + App.ToString() + ", Addon PKG : " + Addon_Theme.ToString() + ", Unknown PKG : " + Unknown.ToString() + ")", "PS4 PKG Tool");
            }

            //update UI
            toolStripStatusLabel2.Text = "... ";
            darkDataGridView1.Enabled = true;
            darkDataGridView2.Enabled = true;
            this.Enabled = true;

        }

        private void FilterPKGGameOnly(object sender, EventArgs e)
        {
            (darkDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", "Game");
        }

        private void FilterPKGPatchOnly(object sender, EventArgs e)
        {
            (darkDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", "Patch");
        }

        private void FilterPKGAddonOnly(object sender, EventArgs e)
        {
            (darkDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", "Addon");
        }

        private void FilterPKGUnknownOnly(object sender, EventArgs e)
        {
            (darkDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", "Unknown");
        }

        private void FilterPKGShowAll(object sender, EventArgs e)
        {
            (darkDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Category] LIKE '%{0}%'", "");
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            selectps4pkgfolder();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_ID_CATEGORY_VERSION_TITLE();
        }

        private void DeletePkg()
        {
            string filename = Path.GetFileName(PS4PKGTOOL.PKG.SelectedPKGFilename);
            DialogResult dialog = DarkMessageBox.DialogYesNo("This will permanently delete \"" + filename + "\". \n\nThis operation cannot be undone. Are you sure you want to continue?", "PS4 PKG Tool");
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    File.Delete(PS4PKGTOOL.PKG.SelectedPKGFilename);

                    foreach (DataGridViewRow row in darkDataGridView1.SelectedRows)
                    {
                        darkDataGridView1.Rows.RemoveAt(row.Index);
                    }
                    labelDisplayTotalPKG.Text = "Displaying " + darkDataGridView1.Rows.Count.ToString() + " PS4 PKG ";
                    DarkMessageBox.ShowInformation("File Deleted.", "PS4 PKG Tool");
                    Logger.log("File Deleted.");
                }
                catch (Exception a)
                { 
                    DarkMessageBox.ShowError("An error occured : " + a.Message, "PS4 PKG Tool"); Logger.log("An error occured : " + a.Message);
                    Logger.log("An error occured : " + a.Message);
                }
            }
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            DeletePkg();
        }

        private void deletePKGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeletePkg();
        }

        private void disableControlPkgSender()
        {
            //status bar - file
            managePS4PKGToolStripMenuItem.Enabled = false;
            reloadContentToolStripMenuItem.Enabled = false;
            exitToolStripMenuItem1.Enabled = false;

            //statusbar - tool
            renameToolStripMenuItem.Enabled = false;
            extractImageAndBackgroundToolStripMenuItem.Enabled = false;
            exportPKGListToExcelToolStripMenuItem1.Enabled = false;
            checkForUpdateToolStripMenuItem.Enabled = false;
            copyStripMenuItem.Enabled = false;
            toolStripMenuItemDeletePKG.Enabled = false;
            openFileLocStripMenuItem.Enabled = false;
            renameCurrentPKGStripMenuItem.Enabled = false;
            UnencryptedContentToolStripMenuItem.Enabled = false;
            viewTrophyListToolStripMenuItemm.Enabled = false;
            checkIfPKGInstalledOnPS4ToolStripMenuItem.Enabled = false;
            uninstallPKGFromPS4ToolStripMenuItem.Enabled = false;


            //contextmenu
            toolStripMenuItem15.Enabled = false;
            toolStripMenuItem96.Enabled = false;
            toolStripMenuItem111.Enabled = false;
            toolStripMenuItem3.Enabled = false;
            toolStripMenuItem120.Enabled = false;
            toolStripMenuItem126.Enabled = false;
            deletePKGToolStripMenuItem.Enabled = false;
            toolStripMenuItem133.Enabled = false;
            toolStripMenuItem127.Enabled = false;
            toolStripMenuItem132.Enabled = false;
            toolStripMenuItem141.Enabled = false;
            toolStripMenuItem143.Enabled = false;
            toolStripMenuItem20.Enabled = false;
            toolStripMenuItem21.Enabled = false;


        }

        private void enableControlPkgSender()
        {
            //status bar - file
            managePS4PKGToolStripMenuItem.Enabled = true;
            reloadContentToolStripMenuItem.Enabled = true;
            exitToolStripMenuItem1.Enabled = true;

            //statusbar - tool
            renameToolStripMenuItem.Enabled = true;
            extractImageAndBackgroundToolStripMenuItem.Enabled = true;
            exportPKGListToExcelToolStripMenuItem1.Enabled = true;
            checkForUpdateToolStripMenuItem.Enabled = true;
            copyStripMenuItem.Enabled = true;
            toolStripMenuItemDeletePKG.Enabled = true;
            openFileLocStripMenuItem.Enabled = true;
            renameCurrentPKGStripMenuItem.Enabled = true;
            UnencryptedContentToolStripMenuItem.Enabled = true;
            viewTrophyListToolStripMenuItemm.Enabled = true;
            checkIfPKGInstalledOnPS4ToolStripMenuItem.Enabled = true;
            uninstallPKGFromPS4ToolStripMenuItem.Enabled = true;

            //contextmenu
            toolStripMenuItem15.Enabled = true;
            toolStripMenuItem96.Enabled = true;
            toolStripMenuItem111.Enabled = true;
            toolStripMenuItem3.Enabled = true;
            toolStripMenuItem120.Enabled = true;
            toolStripMenuItem126.Enabled = true;
            deletePKGToolStripMenuItem.Enabled = true;
            toolStripMenuItem133.Enabled = true;
            toolStripMenuItem127.Enabled = true;
            toolStripMenuItem132.Enabled = true;
            toolStripMenuItem141.Enabled = true;
            toolStripMenuItem143.Enabled = true;
            toolStripMenuItem20.Enabled = true;
            toolStripMenuItem21.Enabled = true;

        }

        private void SendPKG()
        {
            if (toolStripMenuItem19.Text == "Send PKG to PS4")
            {
                DisableTabPages(flatTabControl1, "tabPage1");
                DisableControls(darkMenuStrip1);
                disableControlPkgSender();

                PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
                Param_SFO.PARAM_SFO psfo = read.Param;

                Logger.log("Sending " + read.PS4_Title + " to PS4..");


                var CheckRequirement = PS4PKGTOOL.PKGSENDER.CheckPrerequisite();
                if (CheckRequirement != "OK")
                {
                    ShowError(CheckRequirement);
                    Logger.log(CheckRequirement);
                    enableControlPkgSender();
                    EnableTabPages(flatTabControl1); 
                    EnableControls(darkMenuStrip1);
                    return;
                }

                //update 'Settings.PKG.SelectedPKGFilename'
                foreach (DataGridViewCell cell in darkDataGridView1.SelectedCells)
                {
                    int selectedrowindex = cell.RowIndex;

                    DataGridViewRow selectedRow = darkDataGridView1.Rows[selectedrowindex];

                    PS4PKGTOOL.PKG.SelectedPKGFilename = Convert.ToString(selectedRow.Cells[12].Value) + "\\" + Convert.ToString(selectedRow.Cells[0].Value);
                }


                PS4PKGTOOL.PKGSENDER.taskmonitoriscancelling = false;

                //check if pkg installed
                if (read.PKG_Type.ToString() == "Game" || read.PKG_Type.ToString() == "Patch")
                {
                    //check if pkg exists for game pkg
                    PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.baseAppExist = true;

                    dynamic app_exists_json = null;

                    app_exists_json = PS4PKGTOOL.PKGSENDER.CheckIfPkgInstalled(psfo);
                    if (app_exists_json == null)
                    {
                        DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                        Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                        enableControlPkgSender();
                        EnableTabPages(flatTabControl1);
                        EnableControls(darkMenuStrip1); return;
                    }

                    PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.status = app_exists_json.status.ToString();

                    if (PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.status == "success")
                    {
                        PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.exists = app_exists_json.exists.ToString();
                        if (PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.exists == "true")
                        {
                            if (read.PKG_Type.ToString() == "Game")
                            {
                                DarkMessageBox.ShowInformation("PKG already installed.", "PS4 PKG Tool");
                                Logger.log("PKG already installed.");
                                enableControlPkgSender();
                                EnableTabPages(flatTabControl1);
                                EnableControls(darkMenuStrip1); return;
                            }

                        }
                        else
                        {
                            if (read.PKG_Type.ToString() == "Patch")
                            {
                                PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.baseAppExist = false;
                            }


                        }
                    }
                }

                if (read.PKG_Type.ToString() == "Addon")
                {
                    PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.baseAppExist = true;
                }

                toolStripMenuItem18.Text = "Remote PKG Installer | Status : Running";
                toolStripMenuItem19.Text = "Stop Current Operation";
                toolStripMenuItem16.Text = "Remote PKG Installer | Status : Running";
                toolStripMenuItem17.Text = "Stop Current Operation";
                toolStripStatusLabel2.Text = "Sending " + read.PS4_Title + " to PS4..";

                bgwSendPKG.RunWorkerAsync();
            }
            else
            {
                //cancel current operation
                if (PS4PKGTOOL.PKGSENDER.isPreparing == true)
                {
                    DarkMessageBox.ShowError("Cannot cancel operation while preparing.", "PS4 PKG Tool");
                    Logger.log("Cannot cancel operation while preparing.");

                    return;
                }


                dynamic stop_task_json = null;
                stop_task_json = PS4PKGTOOL.PKGSENDER.StopTask();
                if (stop_task_json == null)
                {
                    DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                    Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");

                    enableControlPkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1); return;
                }

                PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
                Param_SFO.PARAM_SFO psfo = read.Param;


                PS4PKGTOOL.PKGSENDER.JSON.STOPTASK.status = stop_task_json.status.ToString();
                if (PS4PKGTOOL.PKGSENDER.JSON.STOPTASK.status == "success")
                {
                    //if stopping success, uninstall stopped game 

                    dynamic uninstall_app_json = null;
                    uninstall_app_json = PS4PKGTOOL.PKGSENDER.UninstallGame(psfo);
                    if (uninstall_app_json == null)
                    {
                        DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                        Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                        enableControlPkgSender();
                        EnableTabPages(flatTabControl1);
                        EnableControls(darkMenuStrip1); return;
                    }

                    PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLAPP.status = uninstall_app_json.status.ToString();

                    //cancel running bgw
                    bgwMonitorTask.CancelAsync();
                    bgwSendPKG.CancelAsync();


                    enableControlPkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    darkStatusStrip1.Invoke((MethodInvoker)delegate
                    {
                        toolStripStatusLabel2.Text = "...";
                        toolStripProgressBar1.Value = 0;
                    });

                    DarkMessageBox.ShowInformation("Operation stopped.", "PS4 PKG Tool");
                    Logger.log("Operation stopped.");

                   


                }
                else
                {
                    DarkMessageBox.ShowError("Failed to stop current operation.", "PS4 PKG Tool");
                    Logger.log("Failed to stop current operation.");

                }
            }

        }

        private void checkIfAppInstalledOnPS4()
        {
            //check if app exists
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            Logger.log("Checking if base PKG installed on PS4 (" + read.PS4_Title + ")..");


            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            disableControlPkgSender();

            var CheckRequirement = PS4PKGTOOL.PKGSENDER.CheckPrerequisite();
            if (CheckRequirement != "OK")
            {
                ShowError(CheckRequirement);
                Logger.log(CheckRequirement);
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }

            //check if app exists
            dynamic app_exists_json = null;


            //Process checkapp = new Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        FileName = Settings.WorkingDirectory + @"curl.exe",
            //        Arguments = "curl --data {\"\"\"title_id\"\"\":\"\"\"" + psfo.TITLEID + "\"\"\"} http://" + Properties.Settings.Default.PS4IP + ":12800/api/is_exists",
            //        UseShellExecute = false,
            //        RedirectStandardOutput = true,
            //        CreateNoWindow = true
            //    }
            //};

            //checkapp.Start();
            //bool exited = checkapp.WaitForExit(7000); // 2 seconds timeout
            //if (!exited)
            //{
            //    DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
            //    enableControlPkgSender();

            //    return;
            //}
            //while (!checkapp.StandardOutput.EndOfStream)
            //{
            //    string line = checkapp.StandardOutput.ReadLine();
            //    if (line != string.Empty)
            //    {
            //        app_exists_json = JsonConvert.DeserializeObject(line);

            //    }
            //}

            app_exists_json = PS4PKGTOOL.PKGSENDER.CheckIfPkgInstalled(psfo);
            if (app_exists_json == null)
            {
                DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }

            PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.status = app_exists_json.status.ToString();

            if (PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.status == "success")
            {
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.exists = app_exists_json.exists.ToString();
                if (PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.exists == "true")
                {
                    DarkMessageBox.ShowInformation("PKG already installed.", "PS4 PKG Tool");
                    Logger.log("PKG already installed.");
                }
                else
                {
                    DarkMessageBox.ShowInformation("PKG is not installed.", "PS4 PKG Tool");
                    Logger.log("PKG is not installed.");
                }
            }
        }

        private void OpenSettings()
        {
            ProgramSetting form = new ProgramSetting();
            form.ShowDialog();
            this.BringToFront();
            #region checkGridHideUnhide
            if (Properties.Settings.Default.title_id == true)
            {
                darkDataGridView1.Columns[1].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[1].Visible = false;
            }

            if (Properties.Settings.Default.content_id == true)
            {
                darkDataGridView1.Columns[2].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[2].Visible = false;
            }

            if (Properties.Settings.Default.Region == true)
            {
                darkDataGridView1.Columns[3].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[3].Visible = false;
            }

            if (Properties.Settings.Default.system_firmware == true)
            {
                darkDataGridView1.Columns[4].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[4].Visible = false;
            }

            if (Properties.Settings.Default.version == true)
            {
                darkDataGridView1.Columns[5].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[5].Visible = false;
            }

            if (Properties.Settings.Default.pkg_type == true)
            {
                darkDataGridView1.Columns[6].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[6].Visible = false;
            }

            if (Properties.Settings.Default.category == true)
            {
                darkDataGridView1.Columns[7].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[7].Visible = false;
            }

            if (Properties.Settings.Default.size == true)
            {
                darkDataGridView1.Columns[8].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[8].Visible = false;
            }

            if (Properties.Settings.Default.psvr == true)
            {
                darkDataGridView1.Columns[9].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[9].Visible = false;
            }

            if (Properties.Settings.Default.neoEnable == true)
            {
                darkDataGridView1.Columns[10].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[10].Visible = false;
            }

            if (Properties.Settings.Default.ps5bc == true)
            {
                darkDataGridView1.Columns[11].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[11].Visible = false;
            }

            if (Properties.Settings.Default.location == true)
            {
                darkDataGridView1.Columns[12].Visible = true;
            }
            else
            {
                darkDataGridView1.Columns[12].Visible = false;
            }

            if (Properties.Settings.Default.BGMEnable == false)
            {
                int NewVolume = 0; //set 0 to unmute
                uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
                waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
            }
            else
            {
                int NewVolume = 65535; //set 65535 to unmute
                uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
                waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
            }
            #endregion checkGridHideUnhide
        }

        private void toolStripMenuItem145_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void bgwSendPKG_DoWork(object sender, DoWorkEventArgs e)
        {
            renameBackFile = false;
            var currentPkgFile = PS4PKGTOOL.PKG.SelectedPKGFilename;
            send_pkg_json = null;
            PS4PKGTOOL.PKGSENDER.PKGSenderisDone = false;
            PS4PKGTOOL.PKGSENDER.PKGSenderisStopped = false;
            PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.status = "";
            PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.task_id = "";
            PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.title = "";
            PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.title_id = "";

            //kill if server running.
            PS4PKGTOOL.Tool.killNodeJS();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(currentPkgFile);
            Param_SFO.PARAM_SFO psfo = read.Param;
            var tempFilename = read.Content_ID + "_" + read.PKG_Type.ToString() + "_Send_To_PS4.pkg";


            //get directory
            var directory = Path.GetDirectoryName(currentPkgFile);

            //get original filename
            var ori_name = Path.GetFileName(currentPkgFile);

            //rename to temp filename
            if (currentPkgFile != directory + @"\" + tempFilename)
            {
                File.Move(currentPkgFile, directory + @"\" + tempFilename);

            }

            //update filename in gridview
            foreach (DataGridViewCell cell in darkDataGridView1.SelectedCells)
            {
                int selectedrowindex = cell.RowIndex;

                DataGridViewRow selectedRow = darkDataGridView1.Rows[selectedrowindex];

                selectedRow.Cells[0].Value = tempFilename;
            }

            TEMPFILENAMESENDPKG = directory + @"\" + tempFilename;
            PS4PKGTOOL.PKG.SelectedPKGFilename = TEMPFILENAMESENDPKG;

            //run server
            PS4PKGTOOL.PKGSENDER.RunServer(directory);

            ///send pkg
            send_pkg_json = PS4PKGTOOL.PKGSENDER.SendPKG(tempFilename);
            if(send_pkg_json == null)
            {
                DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                toolStripMenuItem18.Text = "Remote PKG Installer | Status : Idle";
                toolStripMenuItem19.Text = "Send PKG to PS4";
                toolStripMenuItem16.Text = "Remote PKG Installer | Status : Idle";
                toolStripMenuItem17.Text = "Send PKG to PS4";
                toolStripStatusLabel2.Text = "...";
                toolStripProgressBar1.Value = 0;
                return;
            }

            PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.status = send_pkg_json.status.ToString();

            if (PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.status == "success")
            {
                //if status success
                PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.status = send_pkg_json.status.ToString();
                PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.task_id = send_pkg_json.task_id.ToString();
                PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.title = send_pkg_json.title.ToString();
                PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.title_id = psfo.TitleID.ToUpper();

                //run monitor task bgw
                if (bgwMonitorTask.IsBusy == true)
                    bgwMonitorTask.CancelAsync();

                bgwMonitorTask.RunWorkerAsync();

            }
            else if (PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.status != "fail")
            {
                PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.error = send_pkg_json.error.ToString();
                DarkMessageBox.ShowError("Operation failed : \n\nStatus : " + PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.status + "\n" + PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.error, "PS4 PKG Tool");
                Logger.log("Operation failed : \n\nStatus : " + PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.status + "\n" + PS4PKGTOOL.PKGSENDER.JSON.SENDPKG.error);
                toolStripMenuItem18.Text = "Remote PKG Installer | Status : Idle";
                toolStripMenuItem19.Text = "Send PKG to PS4";
                toolStripMenuItem16.Text = "Remote PKG Installer | Status : Idle";
                toolStripMenuItem17.Text = "Send PKG to PS4";
                toolStripStatusLabel2.Text = "...";
                toolStripProgressBar1.Value = 0;
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                return;
            }


            while (true)
            {
                if (bgwSendPKG.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                if (PS4PKGTOOL.PKGSENDER.PKGSenderisStopped == true)
                {
                    break;
                }

                if (renameBackFile == true)
                {
                    break;
                }
            }

            //update original filename in gridview
            foreach (DataGridViewRow row in darkDataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(tempFilename))
                {
                    row.Cells[0].Value = ori_name;
                }
            }

            //rename original filename
            File.Move(TEMPFILENAMESENDPKG, currentPkgFile);
            PS4PKGTOOL.PKG.SelectedPKGFilename = currentPkgFile;

        }

        private void bgwSendPKG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void bgwMonitorTask_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                darkStatusStrip1.Invoke((MethodInvoker)delegate
                {
                    toolStripStatusLabel2.Text = e.UserState.ToString();
                    toolStripProgressBar1.Value = e.ProgressPercentage;
                    darkStatusStrip1.Refresh();
                });

            });

        }


        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            SendPKG();

        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            SendPKG();

        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            checkIfAppInstalledOnPS4();
        }

        private void checkIfPKGInstalledOnPS4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkIfAppInstalledOnPS4();
        }


        private void RenameAll_CONTENT_ID_FULL()
        {
            toolStripStatusLabel2.Text = "Renaming PKG.. ";
            Logger.log("Renaming PKG to CONTENT_ID_2..");
           
            if (!bgwRenamePKG.IsBusy)
                bgwRenamePKG.RunWorkerAsync("CONTENT_ID_FULL");
        }

        private void cONTENTIDFULLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameAll_CONTENT_ID_FULL();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            RenameAll_CONTENT_ID_FULL();
        }

        private void cONTENTID2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renamePKGtoCONTENT_ID_2();
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            renamePKGtoCONTENT_ID_2();
        }

        private void uninstallBasePKGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uninstallBasePkgFromPs4();
        }

        private void uninstallPatchPKGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uninstallPatchPkgFromPs4();

        }

        private void bgwMonitorTask_DoWork(object sender, DoWorkEventArgs e)
        {

            dynamic task_progress_json = null;
            PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal = "";
            PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageTransferredTotal = "";
            PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal = "";
            PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packagePreparingTotal = 0;



            darkStatusStrip1.Invoke((MethodInvoker)delegate
            {
                toolStripStatusLabel2.Text = "Preparing download..";
                Logger.log("Preparing download..");
                darkStatusStrip1.Refresh();
            });


            for (int i = 0; i < 100; i++)
            {
                try {
                    PS4PKGTOOL.PKGSENDER.isPreparing = true;

                    ///monitor task 
                    task_progress_json = PS4PKGTOOL.PKGSENDER.GetTaskProgress();
                    if (task_progress_json == null)
                    {

                    }

                    PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packagePreparingTotal = Convert.ToInt32(task_progress_json.preparing_percent.ToString());
                    //logger(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packagePreparingTotal.ToString());
                    //logger(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageTransferredTotal.ToString());


                    if (PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packagePreparingTotal == 100)
                    {
                        break;
                    }

                } catch { }
                
            }

            PS4PKGTOOL.PKGSENDER.isPreparing = false;

            task_progress_json = null;
            task_progress_json = PS4PKGTOOL.PKGSENDER.GetTaskProgress();
            if (task_progress_json == null)
            {
                //dont care
            }

            PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal = task_progress_json.length.ToString();
            PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageTransferredTotal = task_progress_json.transferred.ToString();
            PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal = task_progress_json.rest_sec_total.ToString();
            toolStripProgressBar1.Maximum = Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal); //100
            int TotalRemainTime = toolStripProgressBar1.Maximum;

            //int currentTimeRemaining = Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal);
            int increment = 0;
            int currentRemainingTime = 0;
            //logger("toolStripProgressBar1.Maximum : " + toolStripProgressBar1.Maximum);
            for (long i = (long)Convert.ToDouble(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageTransferredTotal); i < (long)Convert.ToDouble(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal); i++)
            {
                try
                {
                    PS4PKGTOOL.PKGSENDER.isPreparing = false;
                    task_progress_json = null;

                    if (bgwMonitorTask.CancellationPending == true)
                    {
                        e.Cancel = true;
                        PS4PKGTOOL.PKGSENDER.taskmonitoriscancelling = false;
                        break;
                    }

                    if (PS4PKGTOOL.PKGSENDER.taskmonitoriscancelling == true)
                    {
                        PS4PKGTOOL.PKGSENDER.taskmonitoriscancelling = false;

                        break;
                    }

                    PS4PKGTOOL.PKGSENDER.isPreparing = false;

                    task_progress_json = PS4PKGTOOL.PKGSENDER.GetTaskProgress();
                    if (task_progress_json == null)
                    {
                        //dont care
                    }

                    if (task_progress_json.status.ToString() == "fail")
                    {
                        PS4PKGTOOL.PKGSENDER.PKGSenderisStopped = true;
                        break;
                    }


                    PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal = task_progress_json.length.ToString();
                    PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageTransferredTotal = task_progress_json.transferred.ToString();
                    PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal = task_progress_json.rest_sec_total.ToString(); //80

                    long packageTransferredTotal_ = Convert.ToInt64(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageTransferredTotal);
                    long packageFilesizeTotal_ = Convert.ToInt64(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.packageFilesizeTotal);

                    var packageTransferredTotal__ = ByteSize.FromBytes(packageTransferredTotal_).ToString();
                    var packageFilesizeTotal__ = ByteSize.FromBytes(packageFilesizeTotal_).ToString();

                    //Thread.Sleep(1000);


                    if (Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal) == 0)
                    {

                        toolStripProgressBar1.Value = 0;
                        break;
                    }

                    //logger("TotalRemainTime : " + Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal));
                    //logger("currentRemainingTotal : " + TotalRemainTime);


                    increment = TotalRemainTime - Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal);
                    //logger("increment = toolStripProgressBar1.Maximum - Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal)");
                    //logger(increment.ToString() + " = " + toolStripProgressBar1.Maximum.ToString() + " - " + Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal).ToString());
                    //logger("increment : " + increment);

                    //bgwMonitorTask.ReportProgress(currentTimeRemaining, "Installing.. (" + packageTransferredTotal__ + "/" + packageFilesizeTotal__ + ")");
                    darkStatusStrip1.Invoke((MethodInvoker)delegate
                    {
                        toolStripProgressBar1.Increment(increment);
                        toolStripStatusLabel2.Text = "Installing.. (" + packageTransferredTotal__ + "/" + packageFilesizeTotal__ + ")";
                        Logger.log("Installing.. (" + packageTransferredTotal__ + "/" + packageFilesizeTotal__ + ")");
                        darkStatusStrip1.Refresh();
                    });
                    TotalRemainTime = Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal);
                    //logger("TotalRemainTime : " + TotalRemainTime);
                    //logger("PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal : " + Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal));
                    //logger("");




                }
                catch
                {
                    //dont care
                }


            }

            if (Convert.ToInt32(PS4PKGTOOL.PKGSENDER.JSON.MONITORTASK.TimeRemainingTotal) == 0)
            {
                PS4PKGTOOL.PKGSENDER.PKGSenderisDone = true;

            }



        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            uninstallBasePkgFromPs4();
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            uninstallPatchPkgFromPs4();

        }

        private void uninstallAddonPKGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uninstallAddonPkgFromPs4();
        }

        private void uninstallAddonPkgFromPs4()
        {
            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            disableControlPkgSender();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            Logger.log("Uninstalling addon PKG (" + read.PS4_Title + ")..");

            var CheckRequirement = PS4PKGTOOL.PKGSENDER.CheckPrerequisite();
            if (CheckRequirement != "OK")
            {
                ShowError(CheckRequirement);
                Logger.log(CheckRequirement);
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }

            //uninstall installed addon pkg

            dynamic uninstall_patch_json = null;

            uninstall_patch_json = PS4PKGTOOL.PKGSENDER.UninstallAddonTheme(psfo);
            if(uninstall_patch_json == null)
            {
                DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }

            enableControlPkgSender();
            EnableTabPages(flatTabControl1);
            EnableControls(darkMenuStrip1);
            PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLADDON.status = uninstall_patch_json.status.ToString();

            if (PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLADDON.status == "success")
            {
                DarkMessageBox.ShowInformation("PKG uninstalled.", "PS4 PKG Tool");
                Logger.log("PKG uninstalled.");
            }
            else
            {
                DarkMessageBox.ShowError("Uninstall failed.", "PS4 PKG Tool");
                Logger.log("Uninstall failed.");
            }
        }

        private void uninstallThemePKGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UninstallThemePkgFromPs4();
        }

        private void UninstallThemePkgFromPs4()
        {
            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            disableControlPkgSender();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            Logger.log("Uninstalling theme PKG (" + read.PS4_Title + ")..");

            var CheckRequirement = PS4PKGTOOL.PKGSENDER.CheckPrerequisite();
            if (CheckRequirement != "OK")
            {
                ShowError(CheckRequirement);
                Logger.log(CheckRequirement);
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }

            //uninstall installed theme pkg

            dynamic uninstall_theme_json = null;

            uninstall_theme_json = PS4PKGTOOL.PKGSENDER.UninstallAddonTheme(psfo);
            if(uninstall_theme_json == null)
            {
                DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }

            PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLTHEME.status = uninstall_theme_json.status.ToString();

            enableControlPkgSender();
            EnableTabPages(flatTabControl1);
            EnableControls(darkMenuStrip1);

            if (PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLTHEME.status == "success")
            {
                DarkMessageBox.ShowInformation("PKG uninstalled.", "PS4 PKG Tool");
                Logger.log("PKG uninstalled.");
            }
            else
            {
                DarkMessageBox.ShowError("Uninstall failed.", "PS4 PKG Tool");
                Logger.log("Uninstall failed.");
            }
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            uninstallAddonPkgFromPs4();
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            UninstallThemePkgFromPs4();
        }


        private void bgwMonitorTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                runworker = true;
                toolStripStatusLabel2.Text = "...";
                toolStripProgressBar1.Value = 0;

                if (PS4PKGTOOL.PKGSENDER.PKGSenderisStopped == true || PS4PKGTOOL.PKGSENDER.taskmonitoriscancelling == true)
                {
                    DarkMessageBox.ShowError("Operation stopped.", "PS4 PKG Tool");
                    Logger.log("Operation stopped.");
                }

                if (PS4PKGTOOL.PKGSENDER.PKGSenderisDone == true)
                {
                    if(PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.baseAppExist == false)
                    {
                        DarkMessageBox.ShowInformation("Patch PKG sent to PS4. Manually install it after base PKG is installed : Notifications -> Downloads", "PS4 PKG Tool");
                        Logger.log("Patch PKG sent to PS4. Manually install it after base PKG is installed : Notifications -> Downloads");
                    }
                    else
                    {
                        DarkMessageBox.ShowInformation("PKG successfully installed.", "PS4 PKG Tool");
                        Logger.log("PKG successfully installed.");
                    }
                }

                renameBackFile = true;

                //kill if server running.
                //killNodeJS();

                toolStripMenuItem18.Text = "Remote PKG Installer | Status : Idle";
                toolStripMenuItem19.Text = "Send PKG to PS4";
                toolStripMenuItem16.Text = "Remote PKG Installer | Status : Idle";
                toolStripMenuItem17.Text = "Send PKG to PS4";
            });


        }

        private void uninstallBasePkgFromPs4()
        {
            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            disableControlPkgSender();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            //if (read.PKG_Type.ToString() != "Game")
            //{
            //    DarkMessageBox.ShowError("Uninstall only for game PKG at this moment.", "PS4 PKG Tool");
            //    enableControlPkgSender();

            //    return;
            //}

            Logger.log("Uninstalling base PKG (" + read.PS4_Title + ")..");

            var CheckRequirement = PS4PKGTOOL.PKGSENDER.CheckPrerequisite();
            if (CheckRequirement != "OK")
            {
                ShowError(CheckRequirement);
                Logger.log(CheckRequirement);
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                return;
            }

            //check if pkg installed

            dynamic app_exists_json = null;

            app_exists_json = PS4PKGTOOL.PKGSENDER.CheckIfPkgInstalled(psfo);
            if (app_exists_json == null)
            {
                DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }

            PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.status = app_exists_json.status.ToString();

            if (PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.status == "success")
            {
                PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.exists = app_exists_json.exists.ToString();
                if (PS4PKGTOOL.PKGSENDER.JSON.CHECKAPPEXISTS.exists == "false")
                {
                    DarkMessageBox.ShowInformation("PKG is not installed.", "PS4 PKG Tool"); // pkg is not installed, return
                    Logger.log("PKG is not installed.");
                    enableControlPkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1); return;
                }
                else
                {
                    //uninstall installed pkg


                    dynamic uninstall_app_json = null;
                    uninstall_app_json = PS4PKGTOOL.PKGSENDER.UninstallGame(psfo);
                    if (uninstall_app_json == null)
                    {
                        DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                        Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                        enableControlPkgSender();
                        EnableTabPages(flatTabControl1);
                        EnableControls(darkMenuStrip1); return;
                    }

                    enableControlPkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLAPP.status = uninstall_app_json.status.ToString();

                    if (PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLAPP.status == "success")
                    {
                        DarkMessageBox.ShowInformation("PKG uninstalled.", "PS4 PKG Tool");
                        Logger.log("PKG uninstalled.");
                    }
                    else
                    {
                        DarkMessageBox.ShowError("Uninstall failed.", "PS4 PKG Tool");
                        Logger.log("Uninstall failed.");
                    }
                    enableControlPkgSender();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                }
            }

        }

        private void uninstallPatchPkgFromPs4()
        {
            DisableTabPages(flatTabControl1, "tabPage1");
            DisableControls(darkMenuStrip1);
            disableControlPkgSender();

            PS4_Tools.PKG.SceneRelated.Unprotected_PKG read = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);
            Param_SFO.PARAM_SFO psfo = read.Param;

            Logger.log("Uninstalling patch PKG (" + read.PS4_Title + ")..");

            var CheckRequirement = PS4PKGTOOL.PKGSENDER.CheckPrerequisite();
            if (CheckRequirement != "OK")
            {
                ShowError(CheckRequirement);
                Logger.log(CheckRequirement);
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }

            //uninstall installed patch pkg

            dynamic uninstall_app_json = null;

            uninstall_app_json = PS4PKGTOOL.PKGSENDER.UninstallPatch(psfo);
            if(uninstall_app_json == null)
            {
                DarkMessageBox.ShowError("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.", "PS4 PKG Tool");
                Logger.log("An error occur while trying to communicate with PS4. Launch/restart Remote Package Installer application on PS4 and don't minimize it.");
                enableControlPkgSender();
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1); return;
            }


            PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLPATCH.status = uninstall_app_json.status.ToString();
            enableControlPkgSender();
            EnableTabPages(flatTabControl1);
            EnableControls(darkMenuStrip1);
            if (PS4PKGTOOL.PKGSENDER.JSON.UNINTSALLPATCH.status == "success")
            {
                DarkMessageBox.ShowInformation("PKG uninstalled.", "PS4 PKG Tool");
                Logger.log("PKG uninstalled.");
            }
            else
            {
                DarkMessageBox.ShowError("Uninstall failed.", "PS4 PKG Tool");
                Logger.log("Uninstall failed.");
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            RenameAll_TITLE_CATEGORY_VERSION();
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Logger.log("Extracting trophy image..");
            string failExtract = "";
            var numbersAndWords = PS4PKGTOOL.Trophy.NameToExtract.Zip(PS4PKGTOOL.Trophy.imageToExtract, (n, w) => new { name = n, image = w });
            foreach (var nw in numbersAndWords)
            {
                try
                {
                    using (Bitmap tempImage = new Bitmap(Utilities.BytesToImage(PS4PKGTOOL.Trophy.trophy.ExtractFileToMemory(nw.name))))
                    {
                        tempImage.Save(fbd.SelectedPath + @"\" + nw.name, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (Exception a)
                {
                    failExtract += a.Message + "\n";
                }
            }

            if (failExtract == "")
            {
                DarkMessageBox.ShowInformation("Trophy icon extracted to " + fbd.SelectedPath, "PS4 PKG Tool");
                Logger.log("Trophy icon extracted.");
            }
            else
            {
                DarkMessageBox.ShowWarning("Some trophy icon fail to extract.", "PS4 PKG Tool");
                Logger.log("Some icon fail to extract");
                Logger.log(failExtract);

            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBackgroundImg(pbPIC0);
        }

        private void SaveBackgroundImg(PictureBox pb)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap tempImage = new Bitmap(pb.Image))
                    {
                        string pic = pb.Name == "pbPIC0" ? "PIC0" : "PIC1";
                        tempImage.Save(fbd.SelectedPath + @"\" + PS4PKGTOOL.PKG.CurrentPKGTitle + "_"+ pic + ".PNG", System.Drawing.Imaging.ImageFormat.Png);
                        DarkMessageBox.ShowInformation("Image saved.", "PS4 PKG Tool");
                        Logger.log("[" + PS4PKGTOOL.PKG.CurrentPKGTitle + "] Image saved to " + fbd.SelectedPath);
                    }

                    //pb.Image.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + PS4_PKG.Param.Title + @"\.jpeg", ImageFormat.Jpeg);
                }
            }
            catch { }
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            DesktopBackground(pbPIC0);
        }

        private void DesktopBackground(PictureBox pb)
        {
            using (Bitmap tempImage = new Bitmap(pb.Image))
            {
                var savedimagePath = Directory.Exists(Path.GetTempPath() + @"PS4 PKG Tool\Saved image\");
                if (!savedimagePath)
                {
                    Directory.CreateDirectory(Path.GetTempPath() + @"PS4 PKG Tool\Saved image\");
                }

                var path = Path.GetTempPath() + @"PS4 PKG Tool\Saved image\wallpaper.JPG";
                tempImage.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            Wallpaper.Set(Wallpaper.Style.Stretched);


            Logger.log("[" + PS4PKGTOOL.PKG.CurrentPKGTitle + "] Image set as background desktop image.");
        }

        private void darkDataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            this.darkDataGridView3.ClearSelection();
        }

        private void extractDecryptedEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);

                Logger.log("Extracting decrypted items..");
                //load pkg file
                string itemIndex = "";
                var IO = new EndianIO(PS4PKGTOOL.PKG.SelectedPKGFilename, EndianType.BigEndian, true);
                long file_length = IO.Length;
                if (file_length < 0x1C)
                {
                    IO.Close();
                    return;
                }

                var gamename = PS4_PKG.Param.Title;

                //set output path for extracted files
                string path2pkg = Path.GetDirectoryName(PS4PKGTOOL.PKG.SelectedPKGFilename);
                string fullpkgpath = Path.GetFullPath(PS4PKGTOOL.PKG.SelectedPKGFilename);
                string pkgbasename = Path.GetFileNameWithoutExtension(PS4PKGTOOL.PKG.SelectedPKGFilename);
                string pkgfilename = Path.GetFileName(PS4PKGTOOL.PKG.SelectedPKGFilename);
                string outputpath = fbd.SelectedPath; // Path.Combine(path2pkg, pkgbasename);
                // textBox1.AppendText("\r\n\r\npath2pkg:   " + path2pkg);     //  C:\Downloads\ps4packages\
                // textBox1.AppendText("\r\nfullpkgpath:   " + fullpkgpath);   //  C:\Downloads\ps4packages\Up1018...V0100.pkg
                // textBox1.AppendText("\r\npkgbasename:   " + pkgbasename);   //  Up1018...V0100
                // textBox1.AppendText("\r\npkgfilename:   " + pkgfilename);   //  Up1018...V0100.pkg
                //textBox1.AppendText("\r\n\r\noutput path:\r\n" + outputpath); //  C:\Downloads\ps4packages\Up1018...V0100 
                if (!Directory.Exists(outputpath))
                {
                    Directory.CreateDirectory(outputpath);
                }
                else
                {
                }

                //read and decrypt part 1 of key seed
                if (file_length < (0x2400 + 0x100))
                {
                    IO.Close();
                    return;
                }
                IO.SeekTo(0x2400);
                byte[] data = PS4PKGTOOL.Entry.Decrypt(IO.In.ReadBytes(256));
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




                        var nameAndOffset = NameEntryList.Zip(offsetEntryList, (n, w) => new { name = n, offset = w });
                        var entryName = "";

                        foreach (var no in nameAndOffset)
                        {
                            var offset = no.offset.Replace(" ", "");
                            var name = no.name;

                            if (entry[i].offset.ToString("X8") == offset.ToString())
                            {
                                entryName = name;
                            }


                        }


                        try
                        {
                            Logger.log("Extracting " + gamename + "_" + entryName + ".bin..");

                            //save decrypted data to file
                            savename = gamename + "_" + entryName + ".bin";
                            savepath = Path.Combine(outputpath, savename);

                            Array.Resize(ref file_data, Convert.ToInt32(entry[i].size));
                            File.WriteAllBytes(savepath, file_data);  //closes after write


                        }
                        catch (Exception a)
                        {
                            DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool");
                            Logger.log(a.Message);
                        }


                    } //if is encrypted
                }
                IO.Close();  //close pkg file
                DarkMessageBox.ShowInformation("All decrypted item extracted.", "PS4 PKG Tool");
                Logger.log("All decrypted item extracted.");
            }
        }

        private void extractAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog savepath = new FolderBrowserDialog();
            if (savepath.ShowDialog() != DialogResult.OK)
                return;

            
            Logger.log("Extracting all entry items..");
            try
            {
                var numbersAndWords = idEntryList.Zip(nameEntryList, (n, w) => new { id = n, name = w });
                foreach (var nw in numbersAndWords)
                {
                    var pkgPath = PS4PKGTOOL.PKG.SelectedPKGFilename;
                    var idx = int.Parse(nw.id);
                    var name = nw.name;
                    var outPath = savepath.SelectedPath + "\\" + name.Replace("_SHA", ".SHA").Replace("_DAT", ".DAT").Replace("_SFO", ".SFO").Replace("_XML", ".XML").Replace("_SIG", ".SIG").Replace("_PNG", ".PNG").Replace("_JSON", ".JSON").Replace("_DDS", ".DDS").Replace("_TRP", ".TRP").Replace("_AT9", ".AT9"); ;

                    using (var pkgFile = File.OpenRead(pkgPath))
                    {
                        var pkg = new PkgReader(pkgFile).ReadPkg();
                        if (idx < 0 || idx >= pkg.Metas.Metas.Count)
                        {
                            DarkMessageBox.ShowError("Error: entry number out of range.", "PS4 PKG Tool");
                            Logger.log("Error: entry number out of range.");
                            return;
                        }
                        using (var outFile = File.Create(outPath))
                        {
                            var meta = pkg.Metas.Metas[idx];
                            outFile.SetLength(meta.DataSize);
                            if (meta.Encrypted)
                            {
                               
                                    //Logger.log(name + " : Warning: Entry is encrypted but no passcode was provided! Saving encrypted bytes.");
                              
                            }
                            new SubStream(pkgFile, meta.DataOffset, meta.DataSize).CopyTo(outFile);
                        }
                    }
                }

                DarkMessageBox.ShowInformation("All entry item extracted.", "PS4 PKG Tool");
                Logger.log("All entry item extracted to " + savepath.SelectedPath);
            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool");
                Logger.log(a.Message);
            }
        }

        private void dgvEntryList_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvEntryList.ClearSelection();

        }

        private void dgvHeader_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvHeader.ClearSelection();

        }

        private void logger(string text)
        {
            //richTextBox1.Text += text + Environment.NewLine;
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode CurrentNode = e.Node;
            PS4PKGTOOL.PKG.NodeFullPath = CurrentNode.FullPath;

            listView1.Items.Clear();
            TreeNode node = treeView1.SelectedNode;
            List<TreeNode> list = node.GetAllNodes();
            List<string> child = new List<string>();
            List<string> childPath = new List<string>();

            List<string> folder = new List<string>();
            List<string> folderPath = new List<string>();

            foreach (var item in list)
            {
                if (item.Nodes.Count == 0)
                {
                    child.Add(item.Text);
                    childPath.Add(item.FullPath);
                }
                else
                {
                    folder.Add(item.Text);
                    folderPath.Add(item.FullPath);
                }
            }

            listView1.SmallImageList = this.imageList1;

            var numbersAndWords_ = folder.Zip(folderPath, (f, fp) => new { folder = f, folderPath = fp });

            foreach (var fp in numbersAndWords_)
            {
                if (fp.folder != PS4PKGTOOL.TreeView.Nodename)
                {
                    string fileName = Path.GetFileNameWithoutExtension(fp.folder);
                    ListViewItem item = new ListViewItem(fileName);
                    item.Text = fp.folder;
                    item.SubItems.Add("Folder");
                    item.SubItems.Add(fp.folderPath.Replace(fp.folder, "").Replace(@"/", ""));
                    item.Tag = fp.folderPath;
                    item.ImageIndex = 0;
                    listView1.Items.Add(item);
                }


            }

            var numbersAndWords = child.Zip(childPath, (c, p) => new { child = c, path = p });
            foreach (var cp in numbersAndWords)
            {
                var dir = Path.GetDirectoryName(cp.path);
                string fileName = Path.GetFileNameWithoutExtension(cp.child);
                ListViewItem item = new ListViewItem(fileName);
                item.Text = cp.child;
                item.SubItems.Add("File");
                item.SubItems.Add(dir);
                item.Tag = cp.child;
                item.ImageIndex = 1;
                listView1.Items.Add(item);
            }
        }

       
       


        private void bgwPopuplatePkgTreeView_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                ShowError("Passcode not correct.");
                Logger.log("Passcode not correct.");

                //EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                EnableControls(treeView1);
                return;
            }

            //EnableTabPages(flatTabControl1);
            EnableControls(darkMenuStrip1);
            EnableControls(treeView1);

        }

        private void bgwPopuplatePkgTreeView_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.log("Viewing PKG files..");
            //DisableTabPages(flatTabControl1, "tabPage5");
            DisableControls(darkMenuStrip1);
            DisableControls(treeView1);

            List<string> AllFileList = new List<string>();
            List<string> FileListWithExt = new List<string>();
            List<string> DirList = new List<string>();

            if (File.Exists(PS4PKGTOOL.WorkingDirectory + "orbis-pub-cmd.exe"))
            {

            }
            else
            {
            }
            Process checkapp = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = PS4PKGTOOL.WorkingDirectory + "orbis-pub-cmd.exe",
                    Arguments = "img_file_list --passcode " + PS4PKGTOOL.PKG.Passcode + " \"" + PS4PKGTOOL.PKG.SelectedPKGFilename + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            checkapp.Start();
            checkapp.WaitForExit(7000); // 2 seconds timeout
            while (!checkapp.StandardOutput.EndOfStream)
            {
                string line = checkapp.StandardOutput.ReadLine();
                if (line != null)
                {
                    if (line.Contains("Error"))
                    {
                        e.Cancel = true;
                        return;
                    }
                    AllFileList.Add(line);
                    //MessageBox.Show(line);
                }

            }

            var array = AllFileList.ToArray();
            //foreach (string item in AllFileList)
            //{

            //    var ext = System.IO.Path.GetExtension(item);
            //    if (ext == String.Empty)
            //    {
            //        DirList.Add(item.Replace("/", @"\"));
            //    }
            //    else
            //    {
            //        FileListWithExt.Add(item);

            //    }

            //}

           

            treeView1.PathSeparator = @"/";

            treeView1.ImageList = imageList1;
            TreeNode lastNode = null;
            string subPathAgg;
            foreach (string path in array)
            {
                subPathAgg = string.Empty;
                foreach (string subPath in path.Split('/'))
                {
                    subPathAgg += subPath + '/';
                    TreeNode[] nodes = treeView1.Nodes.Find(subPathAgg, true);
                    if (nodes.Length == 0) {
                        if (lastNode == null)
                            treeView1.Invoke((MethodInvoker)delegate
                            {
                                lastNode = treeView1.Nodes.Add(subPathAgg, subPath);
                            });
                        else
                            treeView1.Invoke((MethodInvoker)delegate
                            {
                                lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                            });
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
        }

        private void extractToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PS4PKGTOOL.PKG.NodeFullPath == string.Empty)
            {
                ShowError("Select file or folder to extract.");
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            PS4PKGTOOL.PKG.ExtractLocation = fbd.SelectedPath;

            if (bgwExtractPKG.IsBusy != true)
                bgwExtractPKG.RunWorkerAsync();
        }

        private void bgwExtractPKG_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.log("Extracting file/folder..");
            //MessageBox.Show("img_extract --passcode " + PS4PKGTOOL.PKG.Passcode + " \"" + PS4PKGTOOL.PKG.SelectedPKGFilename + "\":" + PS4PKGTOOL.PKG.NodeFullPath + " \"" + PS4PKGTOOL.PKG.ExtractLocation + "\"");
            Process extract = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = PS4PKGTOOL.WorkingDirectory + "orbis-pub-cmd.exe",
                    Arguments = "img_extract --passcode " + PS4PKGTOOL.PKG.Passcode + " \"" + PS4PKGTOOL.PKG.SelectedPKGFilename + "\":" + PS4PKGTOOL.PKG.NodeFullPath + " \"" + PS4PKGTOOL.PKG.ExtractLocation + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            extract.Start();
            extract.WaitForExit(); // 2 seconds timeout
            while (!extract.StandardOutput.EndOfStream)
            {
                string line = extract.StandardOutput.ReadLine();
                if (line != null)
                {
                    logger(line);
                }

            }

            ShowInformation("Extraction done.");
            Logger.log("Extraction done.");

        }

        private void bgwExtractPKG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                darkContextMenuExtractNode.Show(Cursor.Position.X, Cursor.Position.Y);
            }


        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PS4PKGTOOL.TreeView.Nodename = e.Node.Text;

        }


        private void darkDataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            this.darkDataGridView4.ClearSelection();

        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dgvUpdate.SelectedCells)
            {
                int selectedrowindex = cell.RowIndex;

                DataGridViewRow selectedRow = dgvUpdate.Rows[selectedrowindex];

                Clipboard.SetText(selectedRow.Cells[3].Value.ToString());
            }
            DarkMessageBox.ShowInformation("URL copied to clipboard.", "PS4 PKG Tool");
            Logger.log("URL copied to clipboard.");
        }

        private void downloadSelectedPKGUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DOWNLOADFOLDER == string.Empty)
            {
                DarkMessageBox.ShowError("Select PKG update download path in setting", "PS4 PKG Tool");
                return;
            }

            if (PS4PKGTOOL.Update.Downloading == "no")
            {

                //get selected item value
                if (dgvUpdate.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    try
                    {
                        //get each selected pkg full path
                        foreach (DataGridViewCell cell in dgvUpdate.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = dgvUpdate.Rows[selectedrowindex];
                            //path + filename.pkg
                            PS4PKGTOOL.Update.URL = Convert.ToString(selectedRow.Cells[3].Value);
                            PS4PKGTOOL.Update.PART = Convert.ToString(selectedRow.Cells[0].Value);
                            PS4PKGTOOL.Update.SIZE = Convert.ToString(selectedRow.Cells[1].Value);
                        }
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        DarkMessageBox.ShowError("The Clipboard could not be accessed. Please try again.", "PS4 PKG Tool");
                        return;
                    }

                }

                if (PS4PKGTOOL.Update.URL != null)
                {
                    Logger.log("Downloading update (" + PS4PKGTOOL.Update.SIZE + ")..");
                    // darkDataGridView1.Enabled = false;
                    if (bgwUpdate.IsBusy != true)
                        bgwUpdate.RunWorkerAsync();
                }
                else
                {
                    DarkMessageBox.ShowInformation("Select an update to download.", "PS4 PKG Tool");
                }
            }
            else
            {
                DialogResult dialog = DarkMessageBox.DialogYesNo("Cancel Download?", "PS4 PKG Tool");
                if (dialog == DialogResult.Yes)
                {
                    CancelDownloadingFile();
                    EnableTabPages(flatTabControl1);
                    EnableControls(darkMenuStrip1);
                    ToolStripSplitButtonTotalPKG.Enabled = true;

                    PS4PKGTOOL.Update.Downloading = "no";
                    downloadSelectedPKGUpdateToolStripMenuItem.Text = "Download Selected PKG";

                    DarkMessageBox.ShowInformation("The download has been cancelled.", "PS4 PKG Tool");
                    Logger.log("The download has been cancelled.");

                }
            }
        }

        public void CancelDownloadingFile()
        {
            PS4PKGTOOL.Update.client.CancelAsync();
            PS4PKGTOOL.Update.client.Dispose();
            PS4PKGTOOL.Update.Downloading = "2";
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
            //foreach (Control c in con.Controls)
            //{
            //    DisableControls(c);
            //}
            //con.Enabled = false;

            if (con != null)
            {
                con.Enabled = false;
                //DisableControls(con.Parent);
            }
        }

        private void EnableControls(Control con)
        {
            if (con != null)
            {
                con.Enabled = true;
                //EnableControls(con.Parent);
            }

        }


        private void bgwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
          
            foreach(Control tab in flatTabControl1.TabPages)
            {
                if (tab.Name == "tabPage5")
                    tab.Enabled = true;
                else
                    tab.Enabled = false;
            }
            ToolStripSplitButtonTotalPKG.Enabled = false;
            DisableControls(darkMenuStrip1);

            string sourceFile = PS4PKGTOOL.Update.URL;

            int pos = PS4PKGTOOL.Update.URL.LastIndexOf("/") + 1;
            string filename = PS4PKGTOOL.Update.URL.Substring(pos, PS4PKGTOOL.Update.URL.Length - pos);
            string destFile = Properties.Settings.Default.DOWNLOADFOLDER + "\\" + filename;

            PS4PKGTOOL.Update.client = new WebClient();

            PS4PKGTOOL.Update.client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            PS4PKGTOOL.Update.client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            toolStripProgressBar1.Maximum = 100;
            // Starts the download

            PS4PKGTOOL.Update.client.DownloadFileAsync(new Uri(sourceFile), destFile);

            downloadSelectedPKGUpdateToolStripMenuItem.Text = "Cancel download";
            PS4PKGTOOL.Update.Downloading = "yes";

        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel2.Text = "...";
            if (e.Cancelled)
            {
                PS4PKGTOOL.Update.client.Dispose();

                //MessageBox.Show("The download has been cancelled");
                
                downloadSelectedPKGUpdateToolStripMenuItem.Text = "Download Selected PKG";

                PS4PKGTOOL.Update.Downloading = "no";
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                ToolStripSplitButtonTotalPKG.Enabled = true;

                return;
            }

            if (e.Error != null) // We have an error! Retry a few times, then abort.
            {
                PS4PKGTOOL.Update.client.Dispose();

                DarkMessageBox.ShowError("An error ocurred while trying to download file.", "PS4 PKG Tool");
                Logger.log("An error ocurred while trying to download file.");
                PS4PKGTOOL.Update.Downloading = "no";
                EnableTabPages(flatTabControl1);
                EnableControls(darkMenuStrip1);
                ToolStripSplitButtonTotalPKG.Enabled = true;

                return;
            }
            Logger.log("File succesfully downloaded.");

            DialogResult dialog = DarkMessageBox.DialogYesNo("File succesfully downloaded. Open folder?", "PS4 PKG Tool");
            if (dialog == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(Properties.Settings.Default.DOWNLOADFOLDER);
            }
         
            PS4PKGTOOL.Update.Downloading = "no";
            PS4PKGTOOL.Update.client.Dispose();
            downloadSelectedPKGUpdateToolStripMenuItem.Text = "Download Selected PKG";
            EnableTabPages(flatTabControl1);
            EnableControls(darkMenuStrip1);
            ToolStripSplitButtonTotalPKG.Enabled = true;


        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            toolStripProgressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            toolStripStatusLabel2.Text = "Downloading update PKG.. (" + Math.Truncate(percentage).ToString() + "%)";
        }

        private void bgwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void bgwLoadPKGUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(PS4PKGTOOL.PKG.SelectedPKGFilename);

                DataTable DT = (DataTable)dgvUpdate.DataSource;
                if (DT != null)
                    DT.Clear();
                if (PS4PKGTOOL.Tool.CheckForInternetConnection() == true)
                {
                    Logger.log("Checking update for " + PS4_PKG.PS4_Title + "..");
                    var item = PS4_Tools.PKG.Official.CheckForUpdate(PS4_PKG.Param.TITLEID);
                    if (item != null)
                    {
                        if (item.Tag.Package.Manifest_url != null)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {


                                DataTable dt = new DataTable();
                                dt.Columns.Add("Part(s)");
                                dt.Columns.Add("File Size");
                                dt.Columns.Add("Hash Value");
                                dt.Columns.Add("URL");



                                int ver = Convert.ToInt32(item.Tag.Package.System_ver);

                                string hexOutput = String.Format("{0:X}", ver.ToString("X"));
                                string first_three = hexOutput.Substring(0, 3);
                                string version = first_three.Insert(1, ".");

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


                                var Part = 0;
                                foreach (var items in item.Tag.Package.Manifest_item.pieces)
                                {
                                    Part++;
                                    long fileSize = items.fileSize;
                                    var fileOffset = items.fileOffset;
                                    string hashValue = items.hashValue.ToString();
                                    string url = items.url.ToString();
                                    var size = ByteSizeLib.ByteSize.FromBytes(fileSize);

                                    dt.Rows.Add("Part " + Part, size, hashValue.ToUpper(), url);
                                    dgvUpdate.DataSource = dt;
                                }

                                dgvUpdate.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                dgvUpdate.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                dgvUpdate.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                dgvUpdate.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                dgvUpdate.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                dgvUpdate.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                dgvUpdate.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                dgvUpdate.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            });

                        }
                    }
                    else
                    {
                        Logger.log("\"" + PS4_PKG.PS4_Title + "\" has no update.");
                    }
                }
                else
                {
                    Logger.log("Network is not Available.");
                }
            }
            catch { }

           

          
        }


        private string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        private void bgwLoadPKGUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void darkButton1_Click_1(object sender, EventArgs e)
        {
            var passcode = tbPasscode.Text;
            if (passcode.Length != 32 && passcode.Length != 0)
            {
                return;
            }

            PS4PKGTOOL.PKG.Passcode = passcode;

            treeView1.Nodes.Clear();

            if (bgwPopuplatePkgTreeView.IsBusy != true)
                bgwPopuplatePkgTreeView.RunWorkerAsync();
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            ListViewDraw.colorListViewHeader(ref listView1, Color.FromArgb(57, 60, 62), Color.FromArgb(220, 220, 220));
            foreach (ColumnHeader column in listView1.Columns)
            {
                column.Width = listView1.Width / listView1.Columns.Count;
            }
        }

        private void listView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            //ListViewDraw.colorListViewHeader(ref listView1, Color.FromArgb(57, 60, 62), Color.FromArgb(220, 220, 220));
            //foreach (ColumnHeader column in listView1.Columns)
            //{
            //    column.Width = listView1.Width / listView1.Columns.Count;
            //}
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //if (e.IsSelected)
            //    e.Item.Selected = false;
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void tbSearchGame_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)System.Windows.Forms.Keys.Enter)
            //{
            //    (darkDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Filename] LIKE '%{0}%' OR [Title ID] LIKE '%{0}%'", tbSearchGame.Text);

            //}
        }

        private void darkTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void darkButton2_Click(object sender, EventArgs e)
        {
            (darkDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Filename] LIKE '%{0}%' OR [Title ID] LIKE '%{0}%' OR [Content ID] LIKE '%{0}%'", tbSearchGame.Text);

        }

        private void darkButton3_Click(object sender, EventArgs e)
        {
            (darkDataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Filename] LIKE '%{0}%' OR [Title ID] LIKE '%{0}%' OR [Content ID] LIKE '%{0}%'", "");
            tbSearchGame.Text = string.Empty;
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            SaveBackgroundImg(pbPIC1);
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            DesktopBackground(pbPIC1);
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            ListViewItem selItem = listView1.SelectedItems[0];
            PS4PKGTOOL.PKG.NodeFullPath = selItem.SubItems[2].Text.Replace(@"\",@"/") + @"/" + selItem.SubItems[0].Text;

            //MessageBox.Show(PS4PKGTOOL.PKG.NodeFullPath);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            PS4PKGTOOL.PKG.ExtractLocation = fbd.SelectedPath;

            if (bgwExtractPKG.IsBusy != true)
                bgwExtractPKG.RunWorkerAsync();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
                darkContextMenuExtractFromListView.Show(Cursor.Position.X, Cursor.Position.Y);


        }

        private void viewUpdateChangelogToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            ViewUpdateChangelog();
            var xmlExist = File.Exists(PS4PKGTOOL.WorkingDirectory + "changeinfo.xml");
            if (xmlExist)
            {
                string content = File.ReadAllText(PS4PKGTOOL.WorkingDirectory + "changeinfo.xml");
                File.Delete(PS4PKGTOOL.WorkingDirectory + "changeinfo.xml");
                UpdateChangelog UpdateChangelog = new UpdateChangelog(content);
                UpdateChangelog.ShowDialog();
            }
        }

        private void viewUpdateChangelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
          
        }

        private void ViewUpdateChangelog()
        {
            Logger.log("Viewing update changelog..");
            Process extract = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = PS4PKGTOOL.WorkingDirectory + "orbis-pub-cmd.exe",
                    Arguments = "img_extract --no_passcode \"" + PS4PKGTOOL.PKG.SelectedPKGFilename + "\":Sc0/changeinfo/changeinfo.xml" + " \"" + PS4PKGTOOL.WorkingDirectory.Remove(PS4PKGTOOL.WorkingDirectory.Length - 1) + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            extract.Start();
            extract.WaitForExit(); // 2 seconds timeout
            //while (!extract.StandardOutput.EndOfStream)
            //{
            //    string line = extract.StandardOutput.ReadLine();
            //    if (line != null)
            //    {
            //        logger(line);
            //    }

            //}

        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            ViewUpdateChangelog();
            var xmlExist = File.Exists(PS4PKGTOOL.WorkingDirectory + "changeinfo.xml");
            if (xmlExist)
            {
                string content = File.ReadAllText(PS4PKGTOOL.WorkingDirectory + "changeinfo.xml");
                File.Delete(PS4PKGTOOL.WorkingDirectory + "changeinfo.xml");
                UpdateChangelog UpdateChangelog = new UpdateChangelog(content);
                UpdateChangelog.ShowDialog();
            }
        }

        private TreeNode SearchTreeView(string p_sSearchTerm, TreeNodeCollection p_Nodes)
        {
            foreach (TreeNode node in p_Nodes)
            {
                if (node.Text == p_sSearchTerm)
                    return node;

                if (node.Nodes.Count > 0)
                {
                    TreeNode child = SearchTreeView(p_sSearchTerm, node.Nodes);
                    if (child != null) return child;
                }
            }

            return null;
        }


        private void tbSearchTreeView_TextChanged(object sender, EventArgs e)
        {
          

        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void darkButton4_Click(object sender, EventArgs e)
        {
            if (tbSearchTreeView.Text == string.Empty)
                return;
            treeView1.SelectedNode = SearchTreeView(tbSearchTreeView.Text, treeView1.Nodes);
            treeView1.Focus();
        }

        private void collapseAllNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();

        }
    }




}
