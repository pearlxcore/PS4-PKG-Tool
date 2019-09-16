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
using Tools;
using System.Drawing.Imaging;
using System.Threading;
using PS4_Tools;

namespace PKG_TOOL_GUI
{
    public partial class Form1 : Form
    {
        SaveFileDialog saveFileDialog_Excel = new SaveFileDialog();

        string[] root;
        List<string> allFiles = new List<string>();
        List<string> extracted_item = new List<string>();
        List<string> allFiles_refresh = new List<string>();
        DataTable dttemp_ = new DataTable();
        string a, b, filename, path_only, folderBrowserDialog1_path;
        int s = 0;
        static byte[] bufferA = new byte[16];
        string temppath = Path.GetTempPath() + "PKG_Tool\\";
        public static string resultPath, version, filenameDLC;
        static byte[] PKGHeader = new byte[16] 
        {
            0x7F, 0x43, 0x4E, 0x54, 0x83, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F,
        };
        static byte[] PKGHeader1 = new byte[16]
        {
            0x7F, 0x43, 0x4E, 0x54, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F,
        };
        static byte[] PKGHeader2 = new byte[16]
        {
            0x7F, 0x43, 0x4E, 0x54, 0x81, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F,
        };
        static byte[] PKGHeader3 = new byte[16]
        {
            0x7F, 0x43, 0x4E, 0x54, 0x40, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F,
        };
        static string paths = Environment.CurrentDirectory;
        public static bool Isconnected = true;
        static public bool _pkgtype = false;
        private string failRename;
        private string final_ps4_version;
        private string CUSA_DLC;
        private List<PKG.Official.StoreItems> storeitems;
        private string tempPath;
        private string extractTemp;
        private PictureBox pb_;
        public Form form_;

        public static bool CheckForInternetConnection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
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
                    return Isconnected;
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

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            dataGridView1.Font = new Font("Segoe UI Symbol", 8);
            dataGridView1.DoubleBuffered(true);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Symbol", 8.75F, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Symbol", 8.75F, FontStyle.Bold);
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView2.DoubleBuffered(true);
            dataGridView2.Font = new Font("Segoe UI Symbol", 8);

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(temppath))
            {
                Directory.Delete(temppath, true);
            }

            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }

        //exclude this 2 folder while scanning ps4 pkg
        private static bool IsIgnorable(string dir)
        {
            if (dir.EndsWith("System Volume Information")) return true;
            if (dir.Contains("$RECYCLE.BIN")) return true;
            return false;
        }

        private void OpenPKG()
        {
            progressBar1.Value = 0;
            allFiles.Clear();
            s = 0;
            //search file with .pkg extension
            folderBrowserDialog1_path = folderBrowserDialog1.SelectedPath;
            root = Directory.GetFiles(folderBrowserDialog1_path, "*.PKG");
            allFiles.AddRange(root);
            string[] folders = Directory.GetDirectories(folderBrowserDialog1_path);
            foreach (string folder in folders)
            {
                try
                {
                    if (!IsIgnorable(folder))//method to exclude useless folder
                    {
                        //add scanned file to list
                        allFiles.AddRange(Directory.GetFiles(folder, "*.PKG", SearchOption.AllDirectories));
                    }
                }
                catch { } // don't know what the problem is, don't care...
            }
            //list to array
            allFiles.ToArray();
            //clear this
            filename = null;
            pictureBox1.Image = null;
            label4.Text = "";
            //create datatable
            DataTable dttemp = new DataTable();
            dttemp.Clear();
            dttemp.Columns.Clear();
            //add column
            dttemp.Columns.Add("PKG Name");
            dttemp.Columns.Add("Title ID");
            dttemp.Columns.Add("Content_ID");
            dttemp.Columns.Add("System Firmware");
            dttemp.Columns.Add("Version");
            dttemp.Columns.Add("PKG Type");
            dttemp.Columns.Add("Category");
            dttemp.Columns.Add("Size");
            dttemp.Columns.Add("Path");
            //count total scanned item
            foreach (var fullpath in allFiles)
            {
                //filter ps4 pkg by checking magic byte
                bufferA = checkPKGType(fullpath);
                if (Tool.CompareBytes(bufferA, PKGHeader) == true || Tool.CompareBytes(bufferA, PKGHeader1) == true || Tool.CompareBytes(bufferA, PKGHeader2) == true || Tool.CompareBytes(bufferA, PKGHeader3) == true)
                {
                    s++;
                }
            }
            //label1.Enabled = false;
            progressBar1.Visible = true;
            panel3.Visible = true;
            progressBar1.Maximum = s;
            //parse each pkg info into datatable
            foreach (var fullpath in allFiles)
            {
                //again, filter the pkg
                bufferA = checkPKGType(fullpath);
                if (Tool.CompareBytes(bufferA, PKGHeader) == true || Tool.CompareBytes(bufferA, PKGHeader1) == true || Tool.CompareBytes(bufferA, PKGHeader2) == true || Tool.CompareBytes(bufferA, PKGHeader3) == true)
                {
                    //begin using darkprogrammer's great ps4 tool
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(fullpath);
                    //get pkg version
                    for (int i = 0; i < PS4_PKG.Param.Tables.Count; i++)
                    {
                        if (PS4_PKG.Param.Tables[i].Name == "VERSION")
                        {
                            //get the value 
                            version = PS4_PKG.Param.Tables[i].Value;
                        }
                    }

                    using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(fullpath)))
                    {
                        binaryReader.BaseStream.Seek(119L, SeekOrigin.Begin);
                        //ushort num4 = Util.Utils.ReadUInt16(binaryReader);
                    }
                        //get pkg's minimum system fw
                        foreach (Param_SFO.PARAM_SFO.Table t in PS4_PKG.Param.Tables.ToList())
                    {
                        if (t.Name == "SYSTEM_VER")
                        {
                            int value = Convert.ToInt32(t.Value); //convert value from string to int
                            string hexOutput = String.Format("{0:X}", value); //we want output as hex
                            string first_three = hexOutput.Substring(0, 3); //get only 1st 3 digit
                            final_ps4_version = first_three.Insert(1, "."); // final value and added dot
                            //MessageBox.Show(final_ps4_version + "\n" + tesdttt.ToString());
                        }
                    }
                    //pkg full size
                    long filesize = new System.IO.FileInfo(fullpath).Length;
                    var size = Convert.ToInt64(filesize);
                    var size_final = ByteSize.FromBytes(size).ToString(); //using 'bytesize' to display the value
                    //file name
                    string Filename_only = Path.GetFileName(fullpath);
                    //pkg location
                    path_only = Path.GetDirectoryName(fullpath);
                    //richTextBox1.Text += Filename_only + " >> " + path_only + " >> " + size_final + " >> " + PS4_PKG.Param.TITLEID + " >> " + version + "\n";
                    //add each item to datatable
                    dttemp.Rows.Add(Filename_only, PS4_PKG.Param.TITLEID, PS4_PKG.Param.ContentID, final_ps4_version, version, PS4_PKG.PKGState, PS4_PKG.PKG_Type, size_final, path_only);
                    //increase value(1) each process
                    progressBar1.Increment(1);
                }
            }
            //add datatable to gridview
            dataGridView1.DataSource = dttemp;
            //dk if this is needed
            dttemp = null;
            //label1.Enabled = true;
            progressBar1.Visible = false;
            panel3.Visible = false;
            label5.Visible = false;
            //sort file name ascending
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void ExitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //what happen when left clicked on pkg item
            if (e.Button == MouseButtons.Left)
            {
                //get selected item value
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    filename = "";
                    //richTextBox1.Clear();
                    try
                    {
                        //get each selected pkg full path
                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                            //path + filename.pkg
                            filename = Convert.ToString(selectedRow.Cells[8].Value) + "\\" + Convert.ToString(selectedRow.Cells[0].Value);
                        }
                        //richTextBox1.Text += "a is " + a + "\n\n";
                        //richTextBox1.Text += "b is " + b + "\n\n";
                        runOrbis();
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                    }

                }
            }           
            //what happen when right clicked on pkg item
            else if (e.Button == MouseButtons.Right)
            {
                var hti = dataGridView1.HitTest(e.X, e.Y);

                if (hti.RowIndex != -1)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hti.RowIndex].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[hti.RowIndex].Cells[0];
                }
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    try
                    {
                        //show contextmenustrip
                        contextMenuStrip1.Show(this, new Point(e.X, e.Y));
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                    }
                }
            }
        }
        
        private void Form1_Resize(object sender, EventArgs e)
        {
            //show param info gridview when maximized
            if (WindowState == FormWindowState.Maximized)
            {
                dataGridView2.Visible = true;
            }
            else 
            {
                dataGridView2.Visible = false;
            }
        }

        private void OpenPS4PKGFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label5.Text = "Adding PS4 PKG..";
            folderBrowserDialog1.Description = "Select PS4 PKG folder";
            //label1.Text = "Opening Folder..";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenPKG();
                if (s == 0)
                {
                    //label1.Text = "No PKG found.";
                    MessageBox.Show("No PKG found", "PS4 PKG Tool");
                }
                else
                {
                    //label1.Text = s.ToString() + " PKG found.";
                    MessageBox.Show("PKG found : " + s.ToString(), "PS4 PKG Tool");
                    openGameFolderToolStripMenuItem.Enabled = true;
                    renameToolStripMenuItem.Enabled = true;
                    reloadContentToolStripMenuItem.Enabled = true;
                    exportPKGListToExcelToolStripMenuItem1.Enabled = true;
                    viewTrophyListToolStripMenuItem1.Enabled = true;
                    checkForUpdateToolStripMenuItem.Enabled = true;
                    checkForDLCToolStripMenuItem.Enabled = true;
                    UnencryptedContentToolStripMenuItem.Enabled = true;
                    extractImageAndBackgroundToolStripMenuItem.Enabled = true;
                    clearListToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                label5.Text = "Browse PS4 PKG Folder : File > Open Game Folder";
                if(s != 0)
                {
                    //label1.Text = s.ToString() + " PKG found.";
                }
                else
                {
                    //label1.Text = "...";

                }
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                filename = "";
                //richTextBox1.Clear();
                try
                {
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        int selectedrowindex = cell.RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                        a = Convert.ToString(selectedRow.Cells[8].Value);
                        b = Convert.ToString(selectedRow.Cells[0].Value);
                        filename = a + "\\" + b;
                    }
                    //richTextBox1.Text += "a is " + a + "\n\n";
                    //richTextBox1.Text += "b is " + b + "\n\n";
                    runOrbis();

                }
                catch (System.Runtime.InteropServices.ExternalException)
                {

                    MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                }
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                filename = "";
                //richTextBox1.Clear();
                try
                {


                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        int selectedrowindex = cell.RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                        a = Convert.ToString(selectedRow.Cells[8].Value);
                        b = Convert.ToString(selectedRow.Cells[0].Value);
                        filename = a + "\\" + b;

                    }
                    //richTextBox1.Text += "a is " + a + "\n\n";
                    //richTextBox1.Text += "b is " + b + "\n\n";
                    runOrbis();

                }
                catch (System.Runtime.InteropServices.ExternalException)
                {

                    MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                }
            }
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PS4 PKG Tool\n\nCopyright © pearlxcore 2018\n\nCredit to xDPx & Maxton!", "About PS4 PKG Tool");

        }

        private void DonateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/paypalme2/pearlxcore?locale.x=en_US");

        }

        private void export_pkg_list()
        {
            /*
             * label1.Text = "";
            label1.Text = "Exporting PKG list to excel sheet..";

            cmd3 = "-r -d";
            arg = " \"" + textBox2.Text + "\" " + cmd3;

            Process list = new Process();
            list.StartInfo.FileName = temppath + "pkg_list.exe";
            list.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            list.StartInfo.Arguments = arg.Replace(":\\\"", ":");
            list.Start();
            list.WaitForExit();

            richTextBox1.Text += list.StartInfo.FileName + list.StartInfo.Arguments + "\n\n";

            if (File.Exists(Environment.CurrentDirectory + "\\PKG_List.xlsx"))
            {
                label1.Text += " Done.";
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\PKG_List.xlsx");

            }
            else
            {
                label1.Text += " An error occur while exporting list.";
                MessageBox.Show("An error occur while exporting list", "PS4 PKG Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            */

            //saveFileDialog_Excel.Filter= "*.xls,*.xlsx|*.xls,*.xlsx";
            saveFileDialog_Excel.Filter = "*.xlsx|*.xlsx";

            if (saveFileDialog_Excel.ShowDialog() == DialogResult.OK)
            {
                panel4.Visible = true;
                label1.Text = "Exporting list..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker4.IsBusy)
                    backgroundWorker4.RunWorkerAsync("EXPORT");
                
            }
        }

       

        private void ViewTrophyListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trophy trophy = new Trophy();
            trophy.filenames = filename;
            trophy.ShowDialog();
        }

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you wish to exit?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialog == DialogResult.Yes)
            {
                Application.Exit();

            }
        }

        private void ReloadContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.DataSource != null)
            {

                System.Threading.Thread.Sleep(1000);


                while (dataGridView1.DataSource != null)
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Clear();

                }

                s = 0;


                OpenPKG();
            }
            
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            picture picture = new picture();
            picture.filenames = filename;
            picture.ShowDialog();
        }

        void pb_MouseClick(object sender, MouseEventArgs e)
        {
            
                
        }

        public static void CheckConnection()
        {
            if (CheckForInternetConnection())
            {
                Isconnected = true;
            }
            else
            {
                Isconnected = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory;
            if (!Directory.Exists(temppath))
            {
                Directory.CreateDirectory(temppath);
            }

            try
            {
                tempPath = Path.GetTempPath() + @"orbis\";
                extractTemp = tempPath + @"extract_temp";
                Directory.CreateDirectory(tempPath);
                Directory.CreateDirectory(extractTemp);
                File.WriteAllBytes(tempPath + @"orbis_pub_cmd.exe", Properties.Resources.orbis_pub_cmd);
                File.WriteAllBytes(tempPath + @"ext.zip", Properties.Resources.ext);
                System.IO.Compression.ZipFile.ExtractToDirectory(tempPath + @"ext.zip", tempPath);
            }
            catch (Exception z)
            {
                //MessageBox.Show(z.Message.ToString());
            }
            


        }

        public static byte[] checkPKGType(string dump)
        {
            using (BinaryReader b = new BinaryReader(new FileStream(dump, FileMode.Open, FileAccess.Read)))
            {
                bufferA = new byte[16];

                b.BaseStream.Seek(0x0, SeekOrigin.Begin);
                b.Read(bufferA, 0, 16);
                return bufferA;
            }
        }

        private void runOrbis()
        {
            //reset value
            label4.Text = "";
            //pass fullpath to display trophy stuff
            Trophy trophy = new Trophy();
            trophy.filenames = filename;
            //richTextBox1.Text = filename + "\n\n";
            //display pkg icon on picturebox
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filename);
            if(PS4_PKG.Icon != null)
            {
                pictureBox1.Visible = true;
                label3.Text = "";
                pictureBox1.Image = BytesToBitmap(PS4_PKG.Icon);
            }
            else
            {
                pictureBox1.Visible = false;
                label3.Text = "Image not available";
            }
            //variable for dlc form title
            filenameDLC = PS4_PKG.PS4_Title;
            //pkg name in title box
            label4.Text = PS4_PKG.PS4_Title;
            //get param info
            DataTable dg2 = new DataTable();
            dg2.Columns.Add("PARAM");
            dg2.Columns.Add("VALUE");
            for (int i = 0; i < PS4_PKG.Param.Tables.Count; i++)
            {
                //item name n value
                dg2.Rows.Add(PS4_PKG.Param.Tables[i].Name, PS4_PKG.Param.Tables[i].Value);
            }
            dataGridView2.DataSource = dg2;

            toolStripMenuItem2.Text = PS4_PKG.PS4_Title;
            toolStripMenuItem11.Text = PS4_PKG.PS4_Title;
        }

        public static System.Drawing.Bitmap BytesToBitmap(byte[] ImgBytes)
        {
            System.Drawing.Bitmap result = null;
            if (ImgBytes != null)
            {
                MemoryStream stream = new MemoryStream(ImgBytes);
                result = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(stream);
            }
            return result;
        }

        private void CONTENTIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'CONTENT_ID' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("CONTENT_ID");
            }

                
        }

        private void check_pkg_update()
        {
            CheckConnection();

            if (Isconnected == true)
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filename);

                var item = PS4_Tools.PKG.Official.CheckForUpdate(PS4_PKG.Param.TITLEID);
                /*TitleID Patch Data Is Avaiavle Here*/


                if (item != null)
                {
                    /*Build some string*/

                    string update = "";
                    update += "Version : " + item.Tag.Package.Version;
                    int ver = Convert.ToInt32(item.Tag.Package.System_ver);

                    string hexOutput = String.Format("{0:X}", ver.ToString("X"));
                    string first_three = hexOutput.Substring(0, 3);
                    string version = first_three.Insert(1, ".");
                    update += "\nSystem Version : " + version;

                    update += "\nRemaster : " + item.Tag.Package.Remaster;
                    update += "\nManifest File Number of Pieces : " + item.Tag.Package.Manifest_item.pieces.Count;
                    long size = Convert.ToInt64(item.Tag.Package.Size);
                    var size_final = ByteSize.FromBytes(size).ToString();
                    update += "\nTotal size : " + size_final;

                    //MessageBox.Show(update, "PS4 PKG Tool");


                    if (item.Tag.Package.Manifest_url != null && update != null)
                    {
                        DialogResult dialog = MessageBox.Show("Latest update for \"" + PS4_PKG.PS4_Title + "\" is available : \n\n" + update + "\n\nOpen JSON File?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(item.Tag.Package.Manifest_url);

                        }
                    }



                }
                else
                {
                    MessageBox.Show("\"" + PS4_PKG.PS4_Title + "\" has no update", "PS4 PKG Tool");

                }
            }
            else
            {
                MessageBox.Show("Network is not Available", "PS4 PKG Tool");
            }



        }

        private void CheckForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            label1.Text = "Checking update..";
            menuStrip1.Enabled = false;
            dataGridView1.Enabled = false;
            dataGridView2.Enabled = false;
            pictureBox1.Enabled = false;
            if (!backgroundWorker4.IsBusy)
                backgroundWorker4.RunWorkerAsync("UPDATE");
        }

        private void CheckForDLCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            label1.Text = "Checking addon..";
            menuStrip1.Enabled = false;
            dataGridView1.Enabled = false;
            dataGridView2.Enabled = false;
            pictureBox1.Enabled = false;
            if (!backgroundWorker4.IsBusy)
                backgroundWorker4.RunWorkerAsync("ADDON");
        }

        private void check_dlc()
        {
            CheckConnection();
            if (CheckForInternetConnection())
            {
                if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    //richTextBox1.Clear();
                    try
                    {
                        PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filename);


                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                            CUSA_DLC = Convert.ToString(selectedRow.Cells[1].Value);

                        }

                        if (CUSA_DLC != null)
                        {
                            try
                            {
                                storeitems = PS4_Tools.PKG.Official.Get_All_Store_Items(CUSA_DLC);
                                DLC grid = new DLC(storeitems);
                                grid.ShowDialog();
                            }
                            catch
                            {
                                MessageBox.Show("\"" + PS4_PKG.PS4_Title + "\" has no Addon", "PS4 PKG Tool");
                            }

                        }
                        else
                        {
                            MessageBox.Show("An error occured", "PS4 PKG Tool");

                        }

                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {

                        MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                    }


                }
            }
            else
            {
                MessageBox.Show("Network is not Available", "PS4 PKG Tool");

            }



        }

        private void UnencryptedContentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = tempPath,
                        FileName = tempPath + @"orbis_pub_cmd.exe",
                        Arguments = " img_file_list --no_passcode \"" + filename + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                proc.WaitForExit();


                string text = "";
                string filtered = "";
                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    if (line.Contains("/"))
                    {
                        if (line.Contains("."))
                        {
                            filtered = line.Substring(line.LastIndexOf('/') + 1);

                        }

                    }

                    if (line.Contains("."))
                    {
                        text += filtered + "\n";

                    }
                    /*if (line.Contains("20%"))
                    {
                        progressBar1.Increment(20);
                    }

                    if (line.Contains("40%"))
                    {
                        progressBar1.Increment(20);
                    }
                    */

                }

                if (proc.ExitCode == 0)
                {
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filename);

                    //MessageBox.Show("SUCCESS");
                    MessageBox.Show("Entry list for \"" + PS4_PKG.PS4_Title + "\" : \n\n" + text, "PS4 PKG Tool");

                }
                else if (proc.ExitCode == 1)
                {
                    MessageBox.Show("An error occured", "PS4 PKG Tool");
                }



                /*
                Process extract = new Process();
                extract.StartInfo.FileName = tempPath + @"orbis_pub_cmd.exe";
                extract.StartInfo.Arguments = " img_extract --no_passcode \"" + filename + "\" \"" + out_extract + "\"";
                extract.StartInfo.UseShellExecute = false;
                extract.StartInfo.RedirectStandardOutput = true;

                richTextBox1.Text = extract.StartInfo.FileName + extract.StartInfo.Arguments;

                extract.Start();
                extract.WaitForExit();
                string text = extract.StandardOutput.ReadToEnd();
                richTextBox1.Text += text;
                */
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }
        }

        private void ExtractUnencryptedContentToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            DialogResult dialog = MessageBox.Show("Extract all unencyrpted content from current selected PKG?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                folderBrowserDialog1.Description = "Choose Directory to extract content";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    panel4.Visible = true;
                    label1.Text = "Extracting content..";
                    menuStrip1.Enabled = false;
                    dataGridView1.Enabled = false;
                    dataGridView2.Enabled = false;
                    pictureBox1.Enabled = false;
                    if (!backgroundWorker4.IsBusy)
                        backgroundWorker4.RunWorkerAsync("EXTRACT_UNENCRYPTED");
                }
            }
                

        }

        private void BgwRefresh_DoWork(object sender, DoWorkEventArgs e)
        {
            OpenPKG();
        }

        private void BgwRefresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void RenamePKGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Extract all PKG image to its respective folder?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = folderBrowserDialog1.SelectedPath;

                    foreach (var item in allFiles)
                    {
                        try
                        {
                            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(item);
                            string title_filter = PS4_PKG.Param.Title.Replace(":", " -");
                            string title_filter_final = title_filter.Replace("  -", " -");
                            string final_path = path + @"\" + title_filter_final + @"\";
                            Directory.CreateDirectory(final_path);


                            if (PS4_PKG.Image != null)
                            {
                                using (Bitmap tempImage = new Bitmap(BytesToBitmap(PS4_PKG.Image)))
                                {
                                    tempImage.Save(final_path + "PIC0.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }

                            if (PS4_PKG.Image2 != null)
                            {
                                using (Bitmap tempImage = new Bitmap(BytesToBitmap(PS4_PKG.Image2)))
                                {
                                    tempImage.Save(final_path + "PIC1.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }

                            if (PS4_PKG.Icon != null)
                            {
                                using (Bitmap tempImage = new Bitmap(BytesToBitmap(PS4_PKG.Icon)))
                                {
                                    tempImage.Save(final_path + "ICON.PNG", System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }

                        }
                        catch (Exception a)
                        {
                            MessageBox.Show(a.Message.ToString(), "PS4 PKG Tool");
                        }


                    }
                    MessageBox.Show("All PKG image extracted to its respective folder", "PS4 PKG Tool");


                }
            }

        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE");


            }
        }

        private void ToolStripMenuItem9_Click(object sender, EventArgs e)
        {

            Extract_all_image();
        }

        private void Extract_all_image()
        {
            DialogResult dialog = MessageBox.Show("Extract all PKG image to its respective folder?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    label1.Text = "Extracting images..";

                    dataGridView1.Enabled = false;
                    pictureBox1.Enabled = false;
                    dataGridView2.Enabled = false;
                    menuStrip1.Enabled = false;
                    panel4.Visible = true;

                    if (!backgroundWorker1.IsBusy)
                        backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void ToolStripMenuItem10_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem15_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            label1.Text = "Checking update..";
            menuStrip1.Enabled = false;
            dataGridView1.Enabled = false;
            dataGridView2.Enabled = false;
            pictureBox1.Enabled = false;
            if (!backgroundWorker4.IsBusy)
                backgroundWorker4.RunWorkerAsync("UPDATE");

        }

        private void ToolStripMenuItem16_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            label1.Text = "Checking addon..";
            menuStrip1.Enabled = false;
            dataGridView1.Enabled = false;
            dataGridView2.Enabled = false;
            pictureBox1.Enabled = false;
            if (!backgroundWorker4.IsBusy)
                backgroundWorker4.RunWorkerAsync("ADDON");
        }

        private void ToolStripMenuItem17_Click(object sender, EventArgs e)
        {
            Trophy trophy = new Trophy();
            trophy.filenames = filename;
            trophy.ShowDialog();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string path = folderBrowserDialog1.SelectedPath;

            foreach (var item in allFiles)
            {
                try
                {
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(item);
                    string title_filter = PS4_PKG.Param.Title.Replace(":", " -");
                    string title_filter_final = title_filter.Replace("  -", " -");
                    string final_path = path + @"\" + title_filter_final + @"\";
                    Directory.CreateDirectory(final_path);


                    if (PS4_PKG.Image != null)
                    {
                        using (Bitmap tempImage = new Bitmap(BytesToBitmap(PS4_PKG.Image)))
                        {
                            tempImage.Save(final_path + "PIC0.PNG", System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }

                    if (PS4_PKG.Image2 != null)
                    {
                        using (Bitmap tempImage = new Bitmap(BytesToBitmap(PS4_PKG.Image2)))
                        {
                            tempImage.Save(final_path + "PIC1.PNG", System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }

                    if (PS4_PKG.Icon != null)
                    {
                        using (Bitmap tempImage = new Bitmap(BytesToBitmap(PS4_PKG.Icon)))
                        {
                            tempImage.Save(final_path + "ICON.PNG", System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message.ToString(), "PS4 PKG Tool");
                }


            }
            MessageBox.Show("All PKG image extracted to its respective folder", "PS4 PKG Tool");

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panel4.Visible = false;
            dataGridView1.Enabled = true;
            pictureBox1.Enabled = true;
            dataGridView2.Enabled = true;
            menuStrip1.Enabled = true;
            label1.Text = "";
        }

        private void BackgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            string ButtonName = (string)e.Argument;

            
            failRename = null;
            foreach (var fullpath in allFiles)
            {


                //PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(fullpath);

                bufferA = checkPKGType(fullpath);
                if (Tool.CompareBytes(bufferA, PKGHeader) == true || Tool.CompareBytes(bufferA, PKGHeader1) == true || Tool.CompareBytes(bufferA, PKGHeader2) == true || Tool.CompareBytes(bufferA, PKGHeader3) == true)
                {


                    try
                    {
                        string dir = Path.GetDirectoryName(fullpath);
                        //MessageBox.Show(fullpath);

                        //MessageBox.Show(dir + @"\");
                        switch (ButtonName)
                        {
                            case "TITLE":
                                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title(fullpath, dir + @"\");
                                break;

                            case "TITLE_TITLE_ID":
                                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Title_ID(fullpath, dir + @"\");
                                break;

                            case "TITLE_TITLE_ID_VERSION":
                                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Title_ID_Version(fullpath, dir + @"\");
                                break;

                            case "TITLE_CATEGORY":
                                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_Title_Category(fullpath, dir + @"\");
                                break;

                            case "TITLE_ID":
                                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_TitleID(fullpath, dir + @"\");
                                break;

                            case "CONTENT_ID":
                                PS4_Tools.PKG.SceneRelated.Rename_pkg_To_ContentID(fullpath, dir + @"\");
                                break;

                        }
                    }
                    catch (Exception a)
                    {
                        failRename += Path.GetFileNameWithoutExtension(fullpath) + " : " + a.Message + "\n";
                    }


                }
                else
                {

                }


            }
        }

        private void BackgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = "Refreshing content..";

            if (failRename != null)
            {
                MessageBox.Show("Some PKG fail to rename : \n\n" + failRename, "PS4 PKG Tool");
            }
            else
            {
                //MessageBox.Show("Done", "PS4 PKG Tool");

            }

            

            OpenPKG();

            menuStrip1.Enabled = true;
            dataGridView1.Enabled = true;
            dataGridView2.Enabled = true;
            pictureBox1.Enabled = true;
            panel4.Visible = false;
            label1.Text = "";

        }

        private void BackgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            string ButtonName = (string)e.Argument;

            switch (ButtonName)
            {
                case "ADDON":
                    check_dlc();
                    break;

                case "UPDATE":
                    check_pkg_update();
                    break;

                case "EXTRACT_UNENCRYPTED":
                    string path_extract = folderBrowserDialog1.SelectedPath;
                    try
                    {

                        var proc = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                WorkingDirectory = tempPath,
                                FileName = tempPath + @"orbis_pub_cmd.exe",
                                Arguments = " img_extract --no_passcode \"" + filename + "\" \"" + extractTemp + "\"",
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            }
                        };
                        richTextBox1.Text += proc.StartInfo.FileName + proc.StartInfo.Arguments + "\n";
                        proc.Start();
                        proc.WaitForExit();


                        if (proc.ExitCode == 0)
                        {
                            try
                            {
                                richTextBox1.Text += "success\n";
                                string[] array;

                                array = Directory.GetFiles(extractTemp);
                                extracted_item.AddRange(array);
                                string[] folders = Directory.GetDirectories(extractTemp);



                                foreach (string folder in folders)
                                {
                                    try
                                    {
                                        extracted_item.AddRange(Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories));

                                    }
                                    catch { } // Don't know what the problem is, don't care...
                                }

                                extracted_item.ToArray();

                                foreach (var item in extracted_item)
                                {

                                    string filename = Path.GetFileName(item);
                                    richTextBox1.Text += item.ToString() + "\n";
                                    File.Copy(item, path_extract + "\\" + filename);

                                }

                                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filename);

                                MessageBox.Show("Content for \"" + PS4_PKG.PS4_Title + "\" was extracted to : " + path_extract, "PS4 PKG Tool");
                                extracted_item.Clear();
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show(error.Message.ToString(), "PS4 PKG Tool");
                            }


                        }
                        else if (proc.ExitCode == 1)
                        {
                            MessageBox.Show("An error occured", "PS4 PKG Tool");
                        }



                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message.ToString(), "PS4 PKG Tool");
                    }
                    break;

                case "EXPORT":
                    panel4.Visible = true;
                    label1.Text = "Exporting list..";
                    menuStrip1.Enabled = false;
                    dataGridView1.Enabled = false;
                    dataGridView2.Enabled = false;
                    pictureBox1.Enabled = false;
                    ThreadPool.QueueUserWorkItem(
                                        (pp) =>
                                        {
                                            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                                            Microsoft.Office.Interop.Excel.Workbook MyWorkbook = excel.Workbooks.Add();
                                            Microsoft.Office.Interop.Excel.Worksheet MyWorkField = (Microsoft.Office.Interop.Excel.Worksheet)MyWorkbook.Worksheets.Add();

                                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                            {
                                                MyWorkField.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                                            }

                                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                            {
                                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                {
                                                    MyWorkField.Cells[i + 2, j + 1] = dataGridView1[j, i].FormattedValue.ToString();
                                                }
                                            }

                                            object MyWorkBook_Path = saveFileDialog_Excel.FileName;
                                            MyWorkbook.SaveAs(MyWorkBook_Path);

                                            this.Invoke((MethodInvoker)(() =>
                                            {
                                                MessageBox.Show("PKG list saved to " + MyWorkBook_Path, "PS4 PKG Tool");
                                            }));

                                            excel.Application.Quit();
                                        });
                    break;

            }


        }

        private void BackgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            menuStrip1.Enabled = true;
            dataGridView1.Enabled = true;
            dataGridView2.Enabled = true;
            pictureBox1.Enabled = true;
            panel4.Visible = false;
            label1.Text = "";
        }

        private void ExportAsExcelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            export_pkg_list();

        }

        private void ClearListToolStripMenuItem_Click(object sender, EventArgs e)
        {

            while (dataGridView1.DataSource != null)
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                dataGridView1.Rows.Clear();

            }

            while (dataGridView2.DataSource != null)
            {
                this.dataGridView2.DataSource = null;
                this.dataGridView2.Rows.Clear();
                dataGridView2.Rows.Clear();

            }

            pictureBox1.Image = null;
            label4.Text = "";
            openGameFolderToolStripMenuItem.Enabled = false;
            renameToolStripMenuItem.Enabled = false;
            reloadContentToolStripMenuItem.Enabled = false;
            exportPKGListToExcelToolStripMenuItem1.Enabled = false;
            viewTrophyListToolStripMenuItem1.Enabled = false;
            checkForUpdateToolStripMenuItem.Enabled = false;
            checkForDLCToolStripMenuItem.Enabled = false;
            UnencryptedContentToolStripMenuItem.Enabled = false;
            extractImageAndBackgroundToolStripMenuItem.Enabled = false;
            clearListToolStripMenuItem.Enabled = false;
            s = 0;

        }

        private void ExportAsExcelFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            export_pkg_list();

        }

        private void ToolStripMenuItem13_Click(object sender, EventArgs e)
        {
            try
            {

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = tempPath,
                        FileName = tempPath + @"orbis_pub_cmd.exe",
                        Arguments = " img_file_list --no_passcode \"" + filename + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                proc.WaitForExit();


                string text = "";
                string filtered = "";
                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    if (line.Contains("/"))
                    {
                        if (line.Contains("."))
                        {
                            filtered = line.Substring(line.LastIndexOf('/') + 1);

                        }

                    }

                    if (line.Contains("."))
                    {
                        text += filtered + "\n";

                    }
                    /*if (line.Contains("20%"))
                    {
                        progressBar1.Increment(20);
                    }

                    if (line.Contains("40%"))
                    {
                        progressBar1.Increment(20);
                    }
                    */

                }

                if (proc.ExitCode == 0)
                {
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filename);

                    //MessageBox.Show("SUCCESS");
                    MessageBox.Show("Entry list for \"" + PS4_PKG.PS4_Title + "\" : \n\n" + text, "PS4 PKG Tool");

                }
                else if (proc.ExitCode == 1)
                {
                    MessageBox.Show("An error occured", "PS4 PKG Tool");
                }



                /*
                Process extract = new Process();
                extract.StartInfo.FileName = tempPath + @"orbis_pub_cmd.exe";
                extract.StartInfo.Arguments = " img_extract --no_passcode \"" + filename + "\" \"" + out_extract + "\"";
                extract.StartInfo.UseShellExecute = false;
                extract.StartInfo.RedirectStandardOutput = true;

                richTextBox1.Text = extract.StartInfo.FileName + extract.StartInfo.Arguments;

                extract.Start();
                extract.WaitForExit();
                string text = extract.StandardOutput.ReadToEnd();
                richTextBox1.Text += text;
                */
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }

        }

        private void ToolStripMenuItem14_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Extract all unencyrpted content from current selected PKG?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                folderBrowserDialog1.Description = "Choose Directory to extract content";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    panel4.Visible = true;
                    label1.Text = "Extracting content..";
                    menuStrip1.Enabled = false;
                    dataGridView1.Enabled = false;
                    dataGridView2.Enabled = false;
                    pictureBox1.Enabled = false;
                    if (!backgroundWorker4.IsBusy)
                        backgroundWorker4.RunWorkerAsync("EXTRACT_UNENCRYPTED");
                }
            }
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE [TITLE_ID]' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE_TITLE_ID");
            }
        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE [TITLE_ID] [VERSION]' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE_TITLE_ID_VERSION");
            }
        }

        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE [CATEGORY]' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE_CATEGORY");
            }
        }

        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE_ID' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE_ID");
            }
        }

        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'CONTENT_ID' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("CONTENT_ID");
            }
        }

        private void OpenPS4PKGFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label5.Text = "Adding PS4 PKG..";
            folderBrowserDialog1.Description = "Select PS4 PKG folder";

            //label1.Text = "Opening Folder..";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                OpenPKG();

                if (s == 0)
                {
                    //label1.Text = "No PKG found.";
                    MessageBox.Show("No PKG found", "PS4 PKG Tool");

                }
                else
                {

                    //label1.Text = s.ToString() + " PKG found.";
                    MessageBox.Show("PKG found : " + s.ToString(), "PS4 PKG Tool");
                    openGameFolderToolStripMenuItem.Enabled = true;
                    renameToolStripMenuItem.Enabled = true;
                    reloadContentToolStripMenuItem.Enabled = true;
                    exportPKGListToExcelToolStripMenuItem1.Enabled = true;
                    viewTrophyListToolStripMenuItem1.Enabled = true;
                    checkForUpdateToolStripMenuItem.Enabled = true;
                    checkForDLCToolStripMenuItem.Enabled = true;
                    UnencryptedContentToolStripMenuItem.Enabled = true;
                    extractImageAndBackgroundToolStripMenuItem.Enabled = true;
                    clearListToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                label5.Text = "Browse PS4 PKG Folder : File > Open Game Folder";
                if (s != 0)
                {
                    //label1.Text = s.ToString() + " PKG found.";

                }
                else
                {
                    //label1.Text = "...";

                }

            }
        }

        private void ReloadListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {

                System.Threading.Thread.Sleep(1000);


                while (dataGridView1.DataSource != null)
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Clear();

                }

                s = 0;


                OpenPKG();
            }
        }

        private void ClearListToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            while (dataGridView1.DataSource != null)
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                dataGridView1.Rows.Clear();

            }

            while (dataGridView2.DataSource != null)
            {
                this.dataGridView2.DataSource = null;
                this.dataGridView2.Rows.Clear();
                dataGridView2.Rows.Clear();

            }

            pictureBox1.Image = null;
            label4.Text = "";
            openGameFolderToolStripMenuItem.Enabled = false;
            renameToolStripMenuItem.Enabled = false;
            reloadContentToolStripMenuItem.Enabled = false;
            exportPKGListToExcelToolStripMenuItem1.Enabled = false;
            viewTrophyListToolStripMenuItem1.Enabled = false;
            checkForUpdateToolStripMenuItem.Enabled = false;
            checkForDLCToolStripMenuItem.Enabled = false;
            UnencryptedContentToolStripMenuItem.Enabled = false;
            extractImageAndBackgroundToolStripMenuItem.Enabled = false;
            clearListToolStripMenuItem.Enabled = false;
            s = 0;

        }

        private void ExtractImageAndBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            Extract_all_image();
            
        }

       

        private void TITLEIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE_ID' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE_ID");
            }
                
        }

        private void TITILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE");

                
            }

            
        }


        private void TITLETITLEIDVERSIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE [TITLE_ID] [VERSION]' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE_TITLE_ID_VERSION");
            }

                
        }

        private void TITLETITLEIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE [TITLE_ID]' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE_TITLE_ID");
            }
                
        }

        private void TITLECATEGORYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Rename all PKG to 'TITLE [CATEGORY]' format?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                panel4.Visible = true;
                label1.Text = "Renaming PKG..";
                menuStrip1.Enabled = false;
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = false;
                pictureBox1.Enabled = false;
                if (!backgroundWorker3.IsBusy)
                    backgroundWorker3.RunWorkerAsync("TITLE_CATEGORY");
            }

                
        }

    }
}
