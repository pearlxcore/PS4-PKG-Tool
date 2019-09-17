using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PKG_TOOL_GUI
{
    public partial class AddonUnlocker : Form
    {
        private string path;

        public AddonUnlocker()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("**This only works with..\n\n\n", "PS4 PKG Tool");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(textBox1.Text);
                string ParamTemplate = File.ReadAllText(path + @"param_template.sfx");
                ParamTemplate = ParamTemplate.Replace("%1", PS4_PKG.Param.ContentID).Replace("%2", PS4_PKG.Param.TITLEID).Replace("theme", PS4_PKG.Param.Title);
                File.WriteAllText(path + @"fake_dlc_temp\param_template.sfx", ParamTemplate);
                var createSFO = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path + @"orbis_pub_cmd.exe",
                        Arguments = "sfo_create \"" + path + @"fake_dlc_temp\param_template.sfx" + "\" \"" + path + @"fake_dlc_temp\sce_sys\param.sfo" + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                createSFO.Start();
                createSFO.WaitForExit();
                while (!createSFO.StandardOutput.EndOfStream)
                {
                    string line = createSFO.StandardOutput.ReadLine();
                    //richTextBox1.Text += line;
                }
                if(PS4_PKG.Icon != null)
                {
                    using (Bitmap tempImage = new Bitmap(BytesToBitmap(PS4_PKG.Icon)))
                    {
                        string filter = PS4_PKG.Param.Title.Replace(":", " -");
                        string title_filter_final = filter.Replace("  -", " -");
                        tempImage.Save(path + @"fake_dlc_temp\sce_sys\icon0.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                    string GP4 = File.ReadAllText(path + @"fake_dlc_project.gp4");
                    GP4 = GP4.Replace("TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Replace("ID", PS4_PKG.Param.ContentID).Replace("DIR", path).Replace("<files img_no=\"0\">", "<file targ_path=\"sce_sys/icon0.png\" orig_path=\"" + path + "fake_dlc_temp\\sce_sys\\icon0.png\"/>").Replace("</volume>", "</volume>\n<files>");
                    File.WriteAllText(path + @"fake_dlc_temp\fake_dlc_project.gp4", GP4);
                }
                else
                {
                    string GP4 = File.ReadAllText(path + @"fake_dlc_project.gp4");
                    GP4 = GP4.Replace("TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Replace("ID", PS4_PKG.Param.ContentID).Replace("DIR", path);
                    File.WriteAllText(path + @"fake_dlc_temp\fake_dlc_project.gp4", GP4);
                }
                
                var createPKG = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path + @"orbis_pub_cmd.exe",
                        Arguments = "img_create \"" + path + @"fake_dlc_temp\fake_dlc_project.gp4" + "\" \"" + Environment.CurrentDirectory + @"\Addon Unlocker PKG\" + PS4_PKG.Param.ContentID + "_Unlocker.pkg" + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                createPKG.Start();
                createPKG.WaitForExit();
                while (!createPKG.StandardOutput.EndOfStream)
                {
                    string line = createPKG.StandardOutput.ReadLine();
                    //richTextBox1.Text += line;
                }
                if(createPKG.ExitCode == 0)
                {
                    DialogResult dialog = MessageBox.Show("FPKG created successfully. Open output folder?", "PS4 PKG Tool", MessageBoxButtons.YesNo);
                    if(dialog == DialogResult.Yes)
                    {
                        Process.Start(Environment.CurrentDirectory + @"\Addon Unlocker PKG\");
                    }
                }
            }
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


        private void AddonUnlocker_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.PackageIcon;
            path = Path.GetTempPath() + @"orbis\dlc_unlocker\";
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path + @"fake_dlc_temp\");
            Directory.CreateDirectory(path + @"fake_dlc_temp\sce_sys\");
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\Addon Unlocker PKG\");
            File.WriteAllBytes(path + @"param_template.sfx", Properties.Resources.param_template);
            File.WriteAllBytes(path + @"fake_dlc_project.gp4", Properties.Resources.fake_dlc_project);
            File.WriteAllBytes(path + @"orbis_pub_cmd.exe", Properties.Resources.orbis_pub_cmd);
            File.WriteAllBytes(path + @"ext.zip", Properties.Resources.ext);
            if(!Directory.Exists(path + @"ext\"))
            {
                System.IO.Compression.ZipFile.ExtractToDirectory(path + @"ext.zip", path);
            }

            MessageBox.Show("THIS TOOL WILL NOT WORK WITH MOST DLC. The DLC must be an unlockable type with no extra data required and may require a specific game update version. There is no way to know 100% if it'll work until you try it.\n\nSelect an official addon PKG and the tool will automatically generate the \"unlocker\" FPKG.", "PS4 PKG Tool");
        }
    }
}
