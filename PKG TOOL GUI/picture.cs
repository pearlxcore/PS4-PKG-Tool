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

namespace PKG_TOOL_GUI
{
    public partial class picture : Form
    {
        private List<Image> imageList;
        private Image imageOne;
        Size picSize = new Size(910, 520);
        Size iconSize = new Size(350, 370);
        private string currentPic;

        public picture()
        {
            InitializeComponent();
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public string filenames { get; internal set; }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);
            DialogResult dialog = MessageBox.Show("Save image?", "PS4 PKG Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {

                using (Bitmap tempImage = new Bitmap(pictureBox1.Image))
                {
                    string filter = PS4_PKG.Param.Title.Replace(":", " -");
                    string title_filter_final = filter.Replace("  -", " -");
                    tempImage.Save(Environment.CurrentDirectory + @"\" + title_filter_final + currentPic + ".PNG", System.Drawing.Imaging.ImageFormat.Png);
                }

                //pb.Image.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + PS4_PKG.Param.Title + @"\.jpeg", ImageFormat.Jpeg);
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

        void item1_click(object sender, EventArgs e)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);

            if (PS4_PKG.Image2 != null)
            {
                this.Size = picSize;

                pictureBox1.Image = BytesToBitmap(PS4_PKG.Image2);
                currentPic = " (PIC0)";
                pictureBox1.Refresh();

            }
        }

        void item2_click(object sender, EventArgs e)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);

            if (PS4_PKG.Image != null)
            {
                this.Size = picSize;

                pictureBox1.Image = BytesToBitmap(PS4_PKG.Image);
                currentPic = " (PIC1)";
                pictureBox1.Refresh();

            }
        }

        void icon_click(object sender, EventArgs e)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);

            if (PS4_PKG.Icon != null)
            {
                this.Size = iconSize;
                pictureBox1.Image = BytesToBitmap(PS4_PKG.Icon);
                currentPic = " (ICON)";
                pictureBox1.Refresh();
            }
        }



        private void Picture_Load(object sender, EventArgs e)
        {
            this.Size = picSize;
            if (filenames != null)
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);
                this.Text = PS4_PKG.PS4_Title;
                if (PS4_PKG.Image != null && PS4_PKG.Image2 != null)
                {
                    var item = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "pic0",
                        Text = "PIC0"
                    };
                    imageToolStripMenuItem.DropDownItems.Add(item);
                    item.Click += new EventHandler(this.item1_click);

                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Image);

                    var item2 = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "pic1",
                        Text = "PIC1"
                    };
                    imageToolStripMenuItem.DropDownItems.Add(item2);
                    item2.Click += new EventHandler(this.item2_click);
                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Image2);

                    var icon = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "icon",
                        Text = "ICON"
                    };
                    imageToolStripMenuItem.DropDownItems.Add(icon);
                    icon.Click += new EventHandler(this.icon_click);

                    currentPic = " (PIC0)";


                }
                else if (PS4_PKG.Image != null && PS4_PKG.Image2 == null)
                {
                    var item = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "pic0",
                        Text = "PIC0"
                    };
                    imageToolStripMenuItem.DropDownItems.Add(item);
                    item.Click += new EventHandler(this.item1_click);
                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Image);

                    var icon = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "icon",
                        Text = "ICON"
                    };
                    imageToolStripMenuItem.DropDownItems.Add(icon);
                    icon.Click += new EventHandler(this.icon_click);

                    currentPic = " (PIC0)";


                }
                else if (PS4_PKG.Image == null && PS4_PKG.Image2 != null)
                {
                    var item2 = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "pic1",
                        Text = "PIC1"
                    };
                    imageToolStripMenuItem.DropDownItems.Add(item2);
                    item2.Click += new EventHandler(this.item2_click);
                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Image2);

                    var icon = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "icon",
                        Text = "ICON"
                    };
                    imageToolStripMenuItem.DropDownItems.Add(icon);
                    icon.Click += new EventHandler(this.icon_click);

                    currentPic = " (PIC1)";

                }
                else if (PS4_PKG.Image == null && PS4_PKG.Image2 == null && PS4_PKG.Icon != null)
                {
                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Icon);
                    this.Size = iconSize;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Picture_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
