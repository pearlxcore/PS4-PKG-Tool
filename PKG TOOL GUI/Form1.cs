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

namespace PKG_TOOL_GUI
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        static byte[] bufferA = new byte[0];
        
        static byte[] PKGHeader = new byte[16];

        static string pip = @"get-pip.py";
        static string xlsx = @"xlsx.bat";
        static string rename1 = @"rename1.exe";
        static string rename2 = @"rename2.exe";
        static string paths = Environment.CurrentDirectory;
        static string list = @"pkg_list.exe";
        long length;
        static string cmd1, cmd2, cmd3, path, arg, py;
        public static bool Isconnected = true;

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
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = Environment.CurrentDirectory;
            File.Delete("pkg_list.exe");
            File.Delete("rename1.exe");
            File.Delete("rename2.exe");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pythonSetupToolStripMenuItem_Click(object sender, EventArgs e)
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
        
        private static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
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
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    metroTextBox1.Text = folderDialog.SelectedPath;


                    string[] allfiles = Directory.GetFiles(metroTextBox1.Text, "*.PKG", SearchOption.TopDirectoryOnly);
                    foreach (var file in allfiles)
                    {
                        length = new System.IO.FileInfo(file).Length;
                        var temp = ByteSize.FromBytes(length); //wtfackk!
                        var MB = temp.MegaBytes;
                        var result = Path.GetFileName(file); //wtfackk!
                        dataGridView1.Rows.Add(result, MB.ToString("#####"));
                    }
                    if (allfiles.Length != 0)
                    {
                        metroRename.Enabled = true;
                        metroList.Enabled = true;
                        metroRefresh.Enabled = true;
                        metroComboBox1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No PKG files detected in (" + metroTextBox1.Text + ").", "Error");

                        metroRename.Enabled = false;
                        metroList.Enabled = false;
                        metroRefresh.Enabled = false;
                    }
                }
            }
        }

        private void metroRename_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem == "Select Name Format")
            {

            }
            else if (metroComboBox1.SelectedItem == "TITLE")
            {
                path = metroTextBox1.Text;
                cmd3 = "\" -c %TITLE% -d";
                cmd1 = " \"";
                if (metroCheckBox1.Checked)
                {
                    arg = (cmd1 + path + cmd3 + " -r");
                }
                else
                {
                    arg = (cmd1 + path + cmd3);
                }
                Process p = new Process();
                p.StartInfo.FileName = "rename2.exe";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = arg;
                p.Start();
                p.WaitForExit();


            }
            else if (metroComboBox1.SelectedItem == "CONTENT_ID")
            {
                path = metroTextBox1.Text;
                cmd3 = "\" -c %CONTENT_ID% -d";
                cmd1 = " \"";
                if (metroCheckBox1.Checked)
                {
                    arg = (cmd1 + path + cmd3 + " -r");
                }
                else
                {
                    arg = (cmd1 + path + cmd3);
                }
                Process p = new Process();
                p.StartInfo.FileName = "rename1.exe";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = arg;
                p.Start();
                p.WaitForExit();


            }
            else if (metroComboBox1.SelectedItem == "TITLE (TITLE_ID)")
            {
                path = metroTextBox1.Text;
                cmd3 = "\" -1 -d";
                cmd1 = " \"";
                if (metroCheckBox1.Checked)
                {
                    arg = (cmd1 + path + cmd3 + " -r");
                }
                else
                {
                    arg = (cmd1 + path + cmd3);
                }
                Process p = new Process();
                p.StartInfo.FileName = "rename2.exe";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = arg;
                p.Start();
                p.WaitForExit();
            }
            else if (metroComboBox1.SelectedItem == "TITLE (REGION)")
            {
                path = metroTextBox1.Text;
                cmd3 = "\" -2 -d";
                cmd1 = " \"";
                if (metroCheckBox1.Checked)
                {
                    arg = (cmd1 + path + cmd3 + " -r");
                }
                else
                {
                    arg = (cmd1 + path + cmd3);
                }
                Process p = new Process();
                p.StartInfo.FileName = "rename2.exe";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = arg;
                p.Start();
                p.WaitForExit();
            }
            else if (metroComboBox1.SelectedItem == "TITLE (TITLE_ID) [VERSION]")
            {
                path = metroTextBox1.Text;
                cmd3 = "\" -n -d";
                cmd1 = " \"";
                if (metroCheckBox1.Checked)
                {
                    arg = (cmd1 + path + cmd3 + " -r");
                }
                else
                {
                    arg = (cmd1 + path + cmd3);
                }
                Process p = new Process();
                p.StartInfo.FileName = "rename1.exe";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = arg;
                p.Start();
                p.WaitForExit();
            }

            dataGridView1.Rows.Clear();
            DirectoryInfo d = new DirectoryInfo(metroTextBox1.Text);
            foreach (var file in d.GetFiles("*.PKG"))
            {
                length = new System.IO.FileInfo(file.FullName).Length;
                var temp = ByteSize.FromBytes(length);
                var MB = temp.MegaBytes;
                dataGridView1.Rows.Add(file, MB.ToString("#####.##"));

            }
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/pearlxcore");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            About About = new About();
            About.ShowDialog();
        }

        private void metroRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DirectoryInfo d = new DirectoryInfo(metroTextBox1.Text);
            foreach (var file in d.GetFiles("*.PKG"))
            {
                length = new System.IO.FileInfo(file.FullName).Length;
                var temp = ByteSize.FromBytes(length);
                var MB = temp.MegaBytes;
                dataGridView1.Rows.Add(file, MB.ToString("#####.##"));
            }
        }

        private void metroList_Click(object sender, EventArgs e)
        {
            path = metroTextBox1.Text;
            cmd2 = "\" -d";
            cmd1 = " \"";
            arg = (cmd1 + path + cmd2);
            Process p = new Process();
            p.StartInfo.FileName = "pkg_list.exe";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.Arguments = arg;
            p.Start();
            p.WaitForExit();
            System.Diagnostics.Process.Start("PKG_List.xlsx");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory;

            Extract("PKG_TOOL_GUI", path, "MyResources", "pkg_list.exe");
            Extract("PKG_TOOL_GUI", path, "MyResources", "rename1.exe");
            Extract("PKG_TOOL_GUI", path, "MyResources", "rename2.exe");

        }
        
        static string ConvertToGigabytes(ulong bytes)
        {
            return ((decimal)bytes / 1024M / 1024M / 1024M).ToString("F1") + "GB";
        }
    }
}
