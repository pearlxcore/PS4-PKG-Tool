using DarkUI.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4_PKG_Tool
{
    public partial class picture : DarkForm
    {
        private List<Image> imageList;
        private Image imageOne;
        Size picSize = new Size(910, 570);
        Size iconSize = new Size(350, 395);
        private string currentPic;
        private bool isIcon;
        private string title;
        public picture()
        {
            InitializeComponent();
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public string filenames { get; internal set; }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //darkContextMenu1.Show(this, new Point(e.X, e.Y));
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
                isIcon = false;
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
                isIcon = false;
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
                isIcon = true;
            }
        }



        private void Picture_Load(object sender, EventArgs e)
        {
            this.Size = picSize;
            if (filenames != null)
            {
                PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);
                this.Text = PS4_PKG.PS4_Title;
                title = PS4_PKG.PS4_Title;
                if (PS4_PKG.Image != null && PS4_PKG.Image2 != null)
                {
                    var item = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "pic0",
                        Text = "PIC0",
                        Image = Properties.Resources.Image
                        
                    };
                    imageToolStripMenuItem.DropDownItems.Add(item);
                    item.Click += new EventHandler(this.item1_click);

                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Image);

                    var item2 = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "pic1",
                        Text = "PIC1",
                        Image = Properties.Resources.Image
                    };
                    imageToolStripMenuItem.DropDownItems.Add(item2);
                    item2.Click += new EventHandler(this.item2_click);
                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Image2);

                    var icon = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "icon",
                        Text = "ICON",
                        Image = Properties.Resources.Image
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
                        Text = "PIC0",
                        Image = Properties.Resources.Image
                    };
                    imageToolStripMenuItem.DropDownItems.Add(item);
                    item.Click += new EventHandler(this.item1_click);
                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Image);

                    var icon = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "icon",
                        Text = "ICON",
                        Image = Properties.Resources.Image
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
                        Text = "PIC1",
                        Image = Properties.Resources.Image
                    };
                    imageToolStripMenuItem.DropDownItems.Add(item2);
                    item2.Click += new EventHandler(this.item2_click);
                    pictureBox1.Image = BytesToBitmap(PS4_PKG.Image2);

                    var icon = new System.Windows.Forms.ToolStripMenuItem()
                    {
                        Name = "icon",
                        Text = "ICON",
                        Image = Properties.Resources.Image
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



        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap tempImage = new Bitmap(pictureBox1.Image))
                    {
                       
                        tempImage.Save(fbd.SelectedPath + @"\" + title + currentPic + ".PNG", System.Drawing.Imaging.ImageFormat.Png);
                        DarkMessageBox.ShowInformation("Image saved.", "PS4 PKG Tool");
                        Logger.log("[" + title + "] Image saved to : " + fbd.SelectedPath);
                    }

                    //pb.Image.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + PS4_PKG.Param.Title + @"\.jpeg", ImageFormat.Jpeg);
                }
            }
            catch { }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (Bitmap tempImage = new Bitmap(pictureBox1.Image))
            {
                var savedimagePath = Directory.Exists(Path.GetTempPath() + @"PS4 PKG Tool\Saved image\");
                if (!savedimagePath)
                {
                    Directory.CreateDirectory(Path.GetTempPath() + @"PS4 PKG Tool\Saved image\");
                }

                var path = Path.GetTempPath() + @"PS4 PKG Tool\Saved image\wallpaper.JPG";
                tempImage.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            if (isIcon == false)
                Wallpaper.Set(Wallpaper.Style.Stretched);
            else
                Wallpaper.Set(Wallpaper.Style.Centered);

            Logger.log("[" + title + "] Image set as background desktop image.");
        }
    }
}
