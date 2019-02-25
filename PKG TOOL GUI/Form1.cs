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
using Tools;
using ConsoleControl;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Reflection;
using System.IO.Compression;

namespace PKG_TOOL_GUI
{
    public partial class Form1 : Form
    {
        static byte[] bufferA = new byte[0];
        
        static byte[] PKGHeader = new byte[16];

        static string pip = @"get-pip.py";
        static string xlsx = @"xlsx.bat";
        static string rename1 = @"rename1.exe";
        static string rename2 = @"rename2.exe";

        static string list = @"pkg_list.exe";
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

        

        private void btnList_Click(object sender, EventArgs e)
        {
            path = textOpen.Text;
            cmd2 = " -d";
            cmd1 = " ";
            arg = (cmd1 + path + cmd2);
            console.ClearOutput();
            console.StartProcess(list, arg);
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

                    //Old method
                    //var process = System.Diagnostics.Process.Start(py, arg);

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

                    //console.StartProcess(xlsx, null);
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

        private void button1_Click(object sender, EventArgs e)
        {
            console.ClearOutput();
            DirectoryInfo d = new DirectoryInfo(textOpen.Text);
            foreach (var file in d.GetFiles("*.PKG"))
            {
                console.WriteOutput("> " + file.ToString() + "\n", Color.Silver);
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

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("- Load folder containing PS4 PKG.\n- Select name format to change the PKG name or export PKG list to xlsx sheet.", "How to use");
        }

        private void aboutPS4PKGToolGuiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GUI version of N1ghty's PKG Tool to handle PS4 PKG (rename and export PKG list)", "About PS4 PKG Tool GUI");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory;

            Extract("PKG_TOOL_GUI", path, "MyResources", "pkg_list.exe");
            Extract("PKG_TOOL_GUI", path, "MyResources", "rename1.exe");
            Extract("PKG_TOOL_GUI", path, "MyResources", "rename2.exe");

        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textOpen.Text = folderDialog.SelectedPath;

                    console.ClearOutput();

                    DirectoryInfo d = new DirectoryInfo(textOpen.Text);

                    foreach (var file in d.GetFiles("*.PKG"))
                    {
                                console.WriteOutput("> " + file.ToString() + "\n", Color.Silver);
                    }
                    string dirPath = textOpen.Text;
                    string[] fileNames = Directory.GetFiles(dirPath, "*.pkg", SearchOption.TopDirectoryOnly);
                    if (fileNames.Length != 0)
                    {
                        MessageBox.Show("PKG files detected in (" + textOpen.Text + ").\nChoose to Rename or Export the PKG.", "Info");
                        btnRename.Enabled = true;
                        btnList.Enabled = true;
                        btnRefreshList.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No PKG files detected in (" + textOpen.Text + ").","Error");

                        btnRename.Enabled = false;
                        btnList.Enabled = false;
                        btnRefreshList.Enabled = false;
                    }
                }
            }
        }

        private void btnCleartxt_Click(object sender, EventArgs e)
        {
            textOpen.Clear();
            console.ClearOutput();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Select Name Format")
            {

            }
            else if ( comboBox1.SelectedItem == "TITLE")
            {
                path = textOpen.Text;
                cmd3 = " -c %TITLE% -d";
                cmd1 = " ";
                arg = (cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(rename2, arg);
            }
            else if (comboBox1.SelectedItem == "CONTENT_ID")
            {
                path = textOpen.Text;
                cmd3 = " -c %CONTENT_ID% -d";
                cmd1 = " ";
                arg = (cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(rename1, arg);
            }
            else if (comboBox1.SelectedItem == "TITLE (TITLE_ID)")
            {
                path = textOpen.Text;
                cmd3 = " -1 -d";
                cmd1 = " ";
                arg = (cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(rename2, arg);
            }
            else if (comboBox1.SelectedItem == "TITLE (REGION)")
            {
                path = textOpen.Text;
                cmd3 = " -2 -d";
                cmd1 = " ";
                arg = (cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(rename2, arg);
            }
            else if (comboBox1.SelectedItem == "TITLE (TITLE_ID) [VERSION]")
            {
                path = textOpen.Text;
                cmd3 = " -n -d";
                cmd1 = " ";
                arg = (cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(rename1, arg);
            }
            


            //path = textOpen.Text;
            //cmd3 = " -d";
            //cmd1 = " ";
            //py = "python";
            //arg = (rename + cmd1 + path + cmd3);
            //console.ClearOutput();
            //console.StartProcess(py, arg);
        }
    }
}
