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

        static string xlsx = @"xlsx.bat";
        static string rename = @"pkg_rename.py";
        static string list = @"pkg_list.py";
        static string pip = @"get-pip.py";
        static string cmd1, cmd2, cmd3, path, arg, py;

        public static bool Isconnected = false;

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

        private void btnRename2_Click(object sender, EventArgs e)
        {
            path = textOpen.Text;
            cmd3 = " -n -d";
            cmd1 = " ";
            py = "python";
            arg = (rename + cmd1 + path + cmd3);
            console.ClearOutput();

            console.StartProcess(py, arg);
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



                    bool searchpkg;
                    string dirPath = textOpen.Text;
                    string[] fileNames = Directory.GetFiles(dirPath, "*.pkg", SearchOption.TopDirectoryOnly);
                    if (fileNames.Length != 0)
                    {
                        searchpkg = true;


                        MessageBox.Show("PKG files detected in (" + textOpen.Text + ").\nChoose to Rename or Export the PKG.");

                        btnRename.Enabled = true;
                        btnRename2.Enabled = true;
                        btnList.Enabled = true;


                        //console.WriteOutput("PKG list : \n", System.Drawing.Color.Gray);

                        /*foreach (string fileName in fileNames)
                        {

                            //console.WriteOutput("{0}\n", fileNames System.Drawing.Color.Gray);
                              do you process for each file here 
                        }*/
                    }
                    else
                    {
                        searchpkg = false;
                        MessageBox.Show("No PKG files detected in (" + textOpen.Text + ").");

                        btnRename.Enabled = false;
                        btnRename2.Enabled = false;
                        btnList.Enabled = false;

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
            path = textOpen.Text;
            cmd3 = " -d";
            cmd1 = " ";
            py = "python";
            arg = (rename + cmd1 + path + cmd3);

            console.ClearOutput();

            console.StartProcess(py, arg);
        }
    }
}
