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

        static string rename = @"pkg_rename.py";
        static string list = @"pkg_list.py";
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
            py = "python";
            arg = (list + cmd1 + path + cmd2);
            console.ClearOutput();
            console.StartProcess(py, arg);
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = Environment.CurrentDirectory;
            File.Delete("lib.zip");
            File.Delete("xlsx.bat");
            File.Delete("common.py");
            File.Delete("get-pip.py");
            File.Delete("pkg_list.py");
            File.Delete("pkg_parser.py");
            File.Delete("pkg_rename.py");
            File.Delete("xlsxlist.py");
            File.Delete("python-2.7.8.msi");
            Directory.Delete(path + "\\lib", true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pythonSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Python_Tool Python_Tool = new Python_Tool();
            Python_Tool.ShowDialog();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            path = textOpen.Text;
            cmd3 = " -3 -d";
            cmd1 = " ";
            py = "python";
            arg = (rename + cmd1 + path + cmd3);
            console.ClearOutput();
            console.StartProcess(py, arg);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory;

            if (Directory.Exists(path + "\\lib"))
            {
                Directory.Delete(path + "\\lib", true);
            }

            Extract("PKG_TOOL_GUI", path, "MyResources", "lib.zip");
            Extract("PKG_TOOL_GUI", path, "MyResources", "xlsx.bat");
            Extract("PKG_TOOL_GUI", path, "MyResources", "common.py");
            Extract("PKG_TOOL_GUI", path, "MyResources", "get-pip.py");
            Extract("PKG_TOOL_GUI", path, "MyResources", "pkg_list.py");
            Extract("PKG_TOOL_GUI", path, "MyResources", "pkg_parser.py");
            Extract("PKG_TOOL_GUI", path, "MyResources", "pkg_rename.py");
            Extract("PKG_TOOL_GUI", path, "MyResources", "xlsxlist.py");
            Extract("PKG_TOOL_GUI", path, "MyResources", "python-2.7.8.msi");
            ZipFile.ExtractToDirectory(@"lib.zip", path);

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
                        MessageBox.Show("PKG files detected in (" + textOpen.Text + ").\nChoose to Rename or Export the PKG.");
                        btnRename.Enabled = true;
                        btnList.Enabled = true;
                        btnRefreshList.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No PKG files detected in (" + textOpen.Text + ").");

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
                cmd3 = " -1 -d";
                cmd1 = " ";
                py = "python";
                arg = (rename + cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(py, arg);
            }
            else if (comboBox1.SelectedItem == "TITLE_ID")
            {
                path = textOpen.Text;
                cmd3 = " -2 -d";
                cmd1 = " ";
                py = "python";
                arg = (rename + cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(py, arg);
            }
            else if (comboBox1.SelectedItem == "CONTENT_ID")
            {
                path = textOpen.Text;
                cmd3 = " -3 -d";
                cmd1 = " ";
                py = "python";
                arg = (rename + cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(py, arg);
            }
            else if (comboBox1.SelectedItem == "TITLE (TITLE_ID)")
            {
                path = textOpen.Text;
                cmd3 = " -4 -d";
                cmd1 = " ";
                py = "python";
                arg = (rename + cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(py, arg);
            }
            else if (comboBox1.SelectedItem == "TITLE (REGION)")
            {
                path = textOpen.Text;
                cmd3 = " -5 -d";
                cmd1 = " ";
                py = "python";
                arg = (rename + cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(py, arg);
            }
            else if (comboBox1.SelectedItem == "TITLE (TITLE_ID) [VERSION]")
            {
                path = textOpen.Text;
                cmd3 = " -n -d";
                cmd1 = " ";
                py = "python";
                arg = (rename + cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(py, arg);
            }
            else if (comboBox1.SelectedItem == "TITLE (TITLE_ID) [REGION]")
            {
                path = textOpen.Text;
                cmd3 = " -6 -d";
                cmd1 = " ";
                py = "python";
                arg = (rename + cmd1 + path + cmd3);
                console.ClearOutput();
                console.StartProcess(py, arg);
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
