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

namespace PKG_TOOL_GUI
{
    public partial class Form1 : Form
    {
        string a, b, filename;
        string[] allfiles;
        string[] allfolder;
        string[] folders;
        string[] filePath;
        int s = 0;
        int total = 0;
        static byte[] bufferA = new byte[16];
        string temppath = Path.GetTempPath() + "PKG_Tool\\";
        public static string resultPath;
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
        static string pip = @"get-pip.py";
        static string xlsx = @"xlsx.bat";
        static string paths = Environment.CurrentDirectory;
        long length;
        static string cmd1, cmd2, cmd3, path, arg, py;
        public static bool Isconnected = true;
        static public bool _pkgtype = false;
        string result;
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
           

        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(temppath))
            {
                Directory.Delete(temppath, true);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pythonSetupToolStripMenuItem_Click(object sender, EventArgs e) // not used
        {
            if (Isconnected == true)
            {

                if (Directory.Exists(@"E:\Python27\Lib\site-packages\pip") || Directory.Exists(@"C:\Python27\Lib\site-packages\pip"))
                {
                    MessageBox.Show("PIP already installed.");


                }
                else
                {
                    string path = Environment.CurrentDirectory;
                    Extract("PKG_TOOL_GUI", path, "MyResources", "get-pip.py");

                    //console.WriteOutput("Checking..\n", System.Drawing.Color.Gray);

                    cmd1 = " ";
                    py = "python";
                    arg = (cmd1 + pip);


                    ProcessStartInfo startInfo = new ProcessStartInfo(py, arg);
                    startInfo.WindowStyle = ProcessWindowStyle.Minimized; //make program run hidden
                    Process wait = Process.Start(startInfo); //set new var for waitforexit()
                    wait.WaitForExit();
                    File.Delete("get-pip.py");
                }

                // E: is my directory >.<
                if (Directory.Exists(@"E:\Python27\Lib\site-packages\xlsxwriter") || Directory.Exists(@"C:\Python27\Lib\site-packages\xlsxwriter"))
                {
                    MessageBox.Show("XLSX Writer already installed.", "Info");

                }
                else
                {
                    string path = Environment.CurrentDirectory;

                    Extract("PKG_TOOL_GUI", path, "MyResources", "xlsx.bat");
                    Extract("PKG_TOOL_GUI", path, "MyResources", "xlsxlist.py");


                    ProcessStartInfo startInfo = new ProcessStartInfo(xlsx, null);
                    startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    Process.Start(startInfo);

                    MessageBox.Show("XlSX Writer installed.");

                    File.Delete("xlsx.bat");
                    File.Delete("xlsxlist.py");

                }
            }
            else
            {
                MessageBox.Show("No internet connection detected. Please check your connection.", "Connection error");
            }
        }
        
        private static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName) //not used
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))

            using (BinaryReader r = new BinaryReader(s))

            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))

            using (BinaryWriter w = new BinaryWriter(fs))

                w.Write(r.ReadBytes((int)s.Length));

        }
        
        private void metroOpen_Click(object sender, EventArgs e)
        {
            
        }

        private void processPKG()
        {


            


            if (allfiles.Length != 0)
            {
                
                comboBox1.Enabled = true;

                foreach (var file in allfiles)
                {
                    bufferA = checkPKGType(file);
                    if (Tool.CompareBytes(bufferA, PKGHeader) == true || Tool.CompareBytes(bufferA, PKGHeader1) == true || Tool.CompareBytes(bufferA, PKGHeader2) == true || Tool.CompareBytes(bufferA, PKGHeader3) == true)
                    {

                        resultPath = Path.GetDirectoryName(file);
                        length = new System.IO.FileInfo(file).Length;
                        var temp = ByteSize.FromBytes(length); //wtfackk!
                        var MB = temp.MegaBytes;
                        result = Path.GetFileName(file); //wtfackk!
                        dataGridView1.Rows.Add(result, MB.ToString("#####"), resultPath);


                        s++;
                    }

                }
                dataGridView1.Rows[0].Selected = true;
            }
            else
            {
                MessageBox.Show("No PKG files detected in (" + textBox2.Text + ").", "Error");

                
            }


            metroLabel3.Text = s.ToString() + " PKG found in [" + textBox2.Text + "]";
            
        }

      

        private void metroLink1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/pearlxcore");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            richTextBox1.Text = "============================================\n\n";

            dataGridView1.Rows.Clear();
            this.Invoke((MethodInvoker)delegate ()
            {
                string text = comboBox1.Text;
                DirectoryInfo directory = new DirectoryInfo(textBox2.Text);
                if (checkBox1.Checked)
                {
                    folders = System.IO.Directory.GetDirectories(textBox2.Text, "*", System.IO.SearchOption.AllDirectories);

                }
                else
                {
                    folders = System.IO.Directory.GetDirectories(textBox2.Text, "*", System.IO.SearchOption.TopDirectoryOnly);



                }
                foreach (var folder in folders)
                {

                    if (comboBox1.SelectedItem == "")
                    {

                    }
                    else if (comboBox1.SelectedItem == "TITLE")
                    {



                        cmd3 = "\" -c %TITLE% -d -r";
                        cmd1 = " \"";
                        arg = (cmd1 + folder + cmd3);

                        Process p = new Process();
                        p.StartInfo.FileName = temppath + "rename2.exe";
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.Arguments = arg;
                        p.Start();
                        p.WaitForExit();

                        richTextBox1.Text += p.StartInfo.FileName + p.StartInfo.Arguments + "\n\n";


                    }
                    else if (comboBox1.SelectedItem == "CONTENT_ID")
                    {


                      
                        cmd3 = "\" -c %CONTENT_ID% -d -r";
                        cmd1 = " \"";
                        arg = (cmd1 + folder + cmd3);

                        Process p = new Process();
                        p.StartInfo.FileName = temppath + "rename1.exe";
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.Arguments = arg;
                        p.Start();
                        p.WaitForExit();

                        richTextBox1.Text += p.StartInfo.FileName + p.StartInfo.Arguments + "\n\n";


                    }
                    else if (comboBox1.SelectedItem == "TITLE (TITLE_ID)")
                    {

                       

                        cmd3 = "\" -1 -d -r";
                        cmd1 = " \"";
                        arg = (cmd1  + folder + cmd3);

                        Process p = new Process();
                        p.StartInfo.FileName = temppath + "rename2.exe";
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.Arguments = arg;
                        p.Start();
                        p.WaitForExit();

                        richTextBox1.Text += p.StartInfo.FileName + p.StartInfo.Arguments + "\n\n";

                    }
                    


                    








                }



                richTextBox1.Text += "============================================\n\n";





                refreshList();



            });
            metroLabel3.Text = "Processing done.";


        }

        
        private void metroRefresh_Click(object sender, EventArgs e)
        {
            metroLabel3.Text = "Processing PKG.. Please wait.";

            dataGridView1.Rows.Clear();
            refreshList();
            metroLabel3.Text = "Processing done.";

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            processPKG();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
        }

        private void metroRename_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            metroLabel3.Text = "Processing PKG.. Please wait.";

            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            backgroundWorker1.RunWorkerAsync(comboBox1.Text);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    richTextBox1.Clear();
                    try
                    {
                      

                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                            a = Convert.ToString(selectedRow.Cells[2].Value);
                            b = Convert.ToString(selectedRow.Cells[0].Value);
                            runOrbis();
                        }
                        richTextBox1.Text += "a is " + a + "\n\n";
                        richTextBox1.Text += "b is " + b + "\n\n";

                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {

                        MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                    }
                }
            }
        }

        private void refreshList()
        {
            allfiles = Directory.GetFiles(textBox2.Text, "*.PKG", SearchOption.AllDirectories);

            foreach (var file in allfiles)
            {
                bufferA = checkPKGType(file);
                if (Tool.CompareBytes(bufferA, PKGHeader) == true || Tool.CompareBytes(bufferA, PKGHeader1) == true || Tool.CompareBytes(bufferA, PKGHeader2) == true || Tool.CompareBytes(bufferA, PKGHeader3) == true)
                {
                    var resultPath = Path.GetDirectoryName(file);
                    length = new System.IO.FileInfo(file).Length;
                    var temp = ByteSize.FromBytes(length); //wtfackk!
                    var MB = temp.MegaBytes;
                    var result = Path.GetFileName(file); //wtfackk!
                    dataGridView1.Rows.Add(result, MB.ToString("#####"), resultPath);



                }
                
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Trophy trophy = new Trophy();
            trophy.filenames = filename;
            trophy.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            s = 0;
            //dataGridView1.Rows.Clear();
            //dataGridView1.Refresh();
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    metroLabel3.Text = "Processing PKG.. Please wait.";
                    textBox2.Text = folderDialog.SelectedPath;
                    if (checkBox1.Checked)
                    {
                        allfiles = Directory.GetFiles(textBox2.Text, "*.PKG", SearchOption.AllDirectories);

                    }
                    else
                    {
                        allfiles = Directory.GetFiles(textBox2.Text, "*.PKG", SearchOption.TopDirectoryOnly);

                    }


                    processPKG();


                }
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Trophy trophy = new Trophy();
            trophy.filenames = filename;
            trophy.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            PARAM param = new PARAM();
            param.filenames = filename;
            param.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            metroLabel3.Text = "Processing PKG.. Please wait.";

            dataGridView1.Rows.Clear();
            refreshList();
            metroLabel3.Text = "Processing done.";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            metroLabel3.Text = "Processing PKG.. Please wait.";

            if (backgroundWorker1.IsBusy)
                backgroundWorker1.CancelAsync();
            backgroundWorker1.RunWorkerAsync(comboBox1.Text);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            metroLabel3.Text = "Processing PKG.. Please wait.";



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
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\PKG_List.xlsx");
                metroLabel3.Text = "Processing done.";

            }
            else
            {
                MessageBox.Show("An error occur while exporting list", "PKG Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            PARAM param = new PARAM();
            param.filenames = filename;
            param.ShowDialog();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            foreach (var file in allfiles)
            {
                bufferA = checkPKGType(file);
                if (Tool.CompareBytes(bufferA, PKGHeader) == true || Tool.CompareBytes(bufferA, PKGHeader1) == true || Tool.CompareBytes(bufferA, PKGHeader2) == true || Tool.CompareBytes(bufferA, PKGHeader3) == true)
                {
                    PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filename);

                    resultPath = Path.GetDirectoryName(file);
                    PS4_Tools.PKG.SceneRelated.Rename_pkg_To_ContentID(resultPath, @"D:\", true);


                }

            }
            //PS4_Tools.PKG.SceneRelated.Rename_pkg_To_ContentID(this.filenames, @"D:\", true);

        }

        private void Button7_Click_1(object sender, EventArgs e)
        {

        }

        private void BackgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    richTextBox1.Clear();
                    try
                    {


                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                            a = Convert.ToString(selectedRow.Cells[2].Value);
                            b = Convert.ToString(selectedRow.Cells[0].Value);
                            runOrbis();
                        }
                        richTextBox1.Text += "a is " + a + "\n\n";
                        richTextBox1.Text += "b is " + b + "\n\n";

                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {

                        MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                    }
                }
            

        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
                if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    richTextBox1.Clear();
                    try
                    {


                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                            a = Convert.ToString(selectedRow.Cells[2].Value);
                            b = Convert.ToString(selectedRow.Cells[0].Value);
                            runOrbis();
                        }
                        richTextBox1.Text += "a is " + a + "\n\n";
                        richTextBox1.Text += "b is " + b + "\n\n";

                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {

                        MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                    }
                }
            

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            PARAM param = new PARAM();
            param.filenames = filename;
            param.ShowDialog();
        }

        private void metroList_Click(object sender, EventArgs e)
        {
            metroLabel3.Text = "Processing PKG.. Please wait.";

           

                cmd3 = "\" -r -d";
                cmd1 = " \"";
                arg = (cmd1 + textBox2.Text.Replace(":\\", "") + cmd3);

                Process list = new Process();
                list.StartInfo.FileName = temppath + "pkg_list.exe";
                list.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                list.StartInfo.Arguments = arg;
                list.Start();
                list.WaitForExit();

                richTextBox1.Text += list.StartInfo.FileName + list.StartInfo.Arguments + "\n\n";
                

            

           
          
          

            if (File.Exists(Environment.CurrentDirectory + "\\PKG_List.xlsx"))
            {
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\PKG_List.xlsx");
                metroLabel3.Text = "Processing done.";

            }
            else
            {
                MessageBox.Show("An error occur while exporting list", "PKG Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory;
            if (!Directory.Exists(temppath))
            {
                Directory.CreateDirectory(temppath);
            }

            File.WriteAllBytes(temppath + "\\pkg_list.exe", Properties.Resources.pkg_list);
            File.WriteAllBytes(temppath + "\\rename1.exe", Properties.Resources.rename1);
            File.WriteAllBytes(temppath + "\\rename2.exe", Properties.Resources.rename2);

        }

        static string ConvertToGigabytes(ulong bytes)
        {
            return ((decimal)bytes / 1024M / 1024M / 1024M).ToString("F1") + "GB";
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
            filename = a + "\\" + b;
  
            PARAM param = new PARAM();
            param.filenames = filename;
            Trophy trophy = new Trophy();
            trophy.filenames = filename;
            richTextBox1.Text = filename + "\n\n";
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filename);
            pictureBox1.Image = BytesToBitmap(PS4_PKG.Icon);

            label1.Text = PS4_PKG.PS4_Title;
            





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
    }
}
