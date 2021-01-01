using DarkUI.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : DarkUI.Forms.DarkForm
    {
        static string[] text = { @"Image0",
"Image0/archives",
"Image0/archives/update.pak",
"Image0/sce_sys",
"Image0/sce_sys/about",
"Image0/sce_sys/about/right.sprx",
"Image0/sce_sys/changeinfo",
"Image0/sce_sys/trophy",
"Image0/sce_sys/keystone",
"Image0/eboot.bin",
"Image0/memoryconfiguration.igz",
"Image0/sce_discmap.plt",
"Image0/sce_discmap_patch.plt",
"Image0/version.xml",
"Sc0",
"Sc0/app",
"Sc0/app/playgo-chunk.dat",
"Sc0/changeinfo",
"Sc0/changeinfo/changeinfo.xml",
"Sc0/trophy",
"Sc0/trophy/trophy00.trp",
"Sc0/icon0.dds",
"Sc0/icon0.png",
"Sc0/npbind.dat",
"Sc0/nptitle.dat",
"Sc0/origin-deltainfo.dat",
"Sc0/param.sfo",
"Sc0/playgo-chunk.dat",
"Sc0/playgo-chunk.sha",
"Sc0/playgo-manifest.xml",
"Sc0/psreserved.dat",
"Sc0/shareparam.json" };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var ps5bc = Properties.Resources.ps5bc;
            //var str = System.Text.Encoding.Default.GetString(ps5bc);
            //dynamic stuff = JsonConvert.DeserializeObject(str);

            //foreach (var obj in stuff)
            //{
            //    richTextBox1.Text += obj.npTitleIdshort + "\n";
            //}

            var str = File.ReadAllText(Path.GetTempPath() + @"PS4 PKG Tool\" + @"ps5bc.json");
            File.WriteAllText(Path.GetTempPath() + @"PS4 PKG Tool\" + @"hi.txt", str);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach(var item in text)
            {

            }

            //darkTreeView1.PathSeparator = @"/";

            //PopulateTreeView(treeView1, array, '/');
            TreeNode lastNode = null;
            darkTreeView1.Nodes.Clear();
            string subPathAgg;

            foreach (var item in text)
            {
                subPathAgg = string.Empty;
                foreach (string subPath in item.Split('/'))
                {
                    //if(subPath == "Sc0" || subPath == "Image0")

                    subPathAgg += subPath + '/';
                    richTextBox1.Text += subPath + "\n";
                    DarkTreeNode node = new DarkTreeNode(subPathAgg);

                    //if (nodes.Length == 0)
                    //{
                    //    if (lastNode == null)
                    //        treeView1.Invoke((MethodInvoker)delegate
                    //        {
                    //            lastNode = treeView1.Nodes.Add(subPathAgg, subPath);
                    //            //lastNode.ImageIndex = 1;
                    //        });
                    //    else
                    //        treeView1.Invoke((MethodInvoker)delegate
                    //        {
                    //            lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                    //        });
                    //    lastNode.ImageIndex = 1;
                    //    lastNode.SelectedImageIndex = 1;
                    //}
                    //else
                    //{
                    //    lastNode = nodes[0];
                    //    lastNode.ImageIndex = 0;
                    //    lastNode.SelectedImageIndex = 0;

                    //}
                    darkTreeView1.Nodes.Add(node);

                }

            }
        }

        private bool isfilevalid(string fileName)
        {
            System.IO.FileInfo fi = null;
            try
            {
                fi = new System.IO.FileInfo(fileName);
            }
            catch (ArgumentException) { }
            catch (System.IO.PathTooLongException) { }
            catch (NotSupportedException) { }
            if (ReferenceEquals(fi, null))
            {
                return false;
            }
            return true;
        }
    }
}
