using DarkUI.Forms;
using LibOrbisPkg.PKG;
using LibOrbisPkg.Util;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRPViewer;

namespace PS4_PKG_Tool
{
    public partial class Trophy : DarkForm
    {
        private TRPReader trophy;
        private List<Image> imgList = new List<Image>();
        private List<Image> imageToExtract = new List<Image>();
        private ImageList _ImageListIcons = new ImageList();
        private bool issaved;
        private string path;
        private TRPCreator ctrophy = new TRPCreator();
        List<string> idEntryList = new List<string>();
        List<string> nameEntryList = new List<string>();
        List<string> NameToExtract = new List<string>();

        private string passcode;
        private string tempPath = Path.GetTempPath() + @"orbis\";

        public string filenames { get; set; }

        public Trophy()
        {
            InitializeComponent();
            darkDataGridView1.ScrollBars = ScrollBars.Both;
            darkDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            darkDataGridView1.RowTemplate.Height = 40;
            darkDataGridView1.Columns[0].Width = 110;
            //darkDataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //listView1.FullRowSelect = true;
            darkDataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            darkDataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < darkDataGridView1.Columns.Count; i++)
                if (darkDataGridView1.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)darkDataGridView1.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Stretch;
                    break;
                }
        }

        internal virtual ImageList ImageListIcons
        {
            get
            {
                return _ImageListIcons;
            }
            set
            {
                _ImageListIcons = value;
            }
        }

        private void Trophy_Load(object sender, EventArgs e)
        {
            //extract all entry
            //add entry to array
            using (var file = File.OpenRead(filenames))
            {
                var pkg = new PkgReader(file).ReadPkg();
                var i = 0;



                foreach (var meta in pkg.Metas.Metas)
                {
                    idEntryList.Add($"{i++,-6}");
                    nameEntryList.Add($"{meta.id}");
                }

                idEntryList.ToArray();
                nameEntryList.ToArray();




            }

            //extract each entry from array
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);
            string game_title = PS4_PKG.Param.Title;
            string filter = game_title.Replace(":", " -");
            string title_filter_final = filter.Replace("  -", " -");

            string path = tempPath + @"Trophy\";
            try
            {


                Directory.CreateDirectory(path + title_filter_final);

                var numbersAndWords = idEntryList.Zip(nameEntryList, (n, w) => new { id = n, name = w });
                foreach (var nw in numbersAndWords)
                {
                    var pkgPath = filenames;
                    var idx = int.Parse(nw.id);
                    var name = nw.name;
                    var outPath = path + title_filter_final + "\\" + name.Replace("_SHA", ".SHA").Replace("_DAT", ".DAT").Replace("_SFO", ".SFO").Replace("_XML", ".XML").Replace("_SIG", ".SIG").Replace("_PNG", ".PNG").Replace("_JSON", ".JSON").Replace("_DDS", ".DDS").Replace("_TRP", ".TRP").Replace("_AT9", ".AT9"); ;

                    using (var pkgFile = File.OpenRead(pkgPath))
                    {
                        var pkg = new PkgReader(pkgFile).ReadPkg();
                        if (idx < 0 || idx >= pkg.Metas.Metas.Count)
                        {
                            DarkMessageBox.ShowError("Error: entry number out of range", "PS4 PKG Tool");
                            return;
                        }
                        using (var outFile = File.Create(outPath))
                        {
                            var meta = pkg.Metas.Metas[idx];
                            outFile.SetLength(meta.DataSize);
                            if (meta.Encrypted)
                            {
                                if (passcode == null)
                                {
                                    //MessageBox.Show("Warning: Entry is encrypted but no passcode was provided! Saving encrypted bytes.");
                                }
                                else
                                {
                                    var entry = new SubStream(pkgFile, meta.DataOffset, (meta.DataSize + 15) & ~15);
                                    var tmp = new byte[entry.Length];
                                    entry.Read(tmp, 0, tmp.Length);
                                    tmp = LibOrbisPkg.PKG.Entry.Decrypt(tmp, pkg.Header.content_id, passcode, meta);
                                    outFile.Write(tmp, 0, (int)meta.DataSize);
                                    return;
                                }
                            }
                            new SubStream(pkgFile, meta.DataOffset, meta.DataSize).CopyTo(outFile);
                        }
                    }
                }

                //MessageBox.Show("All entry item extracted", "PS4 PKG Tool");
            }
            catch (Exception a)
            {
                DarkMessageBox.ShowError(a.Message, "PS4 PKG Tool");
            }





            //done exract, now open trp
            if (File.Exists(path + title_filter_final + "\\TROPHY__TROPHY00.TRP"))
            {
                trophy = new TRPReader();
                trophy.Load(path + title_filter_final + "\\TROPHY__TROPHY00.TRP");

                if (!trophy.IsError)
                {
                    List<Archiver>.Enumerator enumerator = new List<Archiver>.Enumerator();
                    enumerator = trophy.TrophyList.GetEnumerator();



                    try
                    {
                        //listView1.Columns.Add("Icon");
                        //listView1.Columns.Add("Name");
                        //listView1.Columns.Add("Size");
                        //listView1.Columns.Add("Offset");

                        //listView1.View = View.Details;

                        //_ImageListIcons.ImageSize = new Size(96, 96);
                        //listView1.SmallImageList = _ImageListIcons;

                        while (enumerator.MoveNext())
                        {
                            Archiver current = enumerator.Current;
                            if (current.Name.ToUpper().EndsWith(".PNG"))
                            {


                                imgList.Add(Utilities.BytesToImage(trophy.ExtractFileToMemory(current.Name)));
                                ImageListIcons.Images.Add(imgList[checked(imgList.Count - 1)]);

                                imageToExtract.Add(Utilities.BytesToImage(trophy.ExtractFileToMemory(current.Name)));
                                NameToExtract.Add(current.Name);

                                var image = Utilities.BytesToImage(trophy.ExtractFileToMemory(current.Name));
                                var resize = ResizeImage(image, image.Width / 2, image.Height / 2);
                                darkDataGridView1.Rows.Add(resize, current.Name, Utilities.RoundBytes(current.Size), "0x" + current.Offset);

                  //              listView1.Items.Add(new ListViewItem()
                  //              {
                  //                  SubItems = {
                  //  current.Name, Utilities.RoundBytes(current.Size)
                  //},
                  //                  ImageIndex = checked(ImageListIcons.Images.Count - 1),
                  //                  Tag = (object)imgList.Count
                  //              });


                            }
                            //dataGridView1.DataSource = dt;
                            Application.DoEvents();
                        }

                        NameToExtract.ToArray();

                        //listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        //listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.None);
                        //listView1.Columns[1].Width = 347;
                        //listView1.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.None);
                        //listView1.Columns[2].Width = 103;



                    }
                    finally
                    {
                        enumerator.Dispose();
                    }
                    if (!issaved)
                    {
                        if (Operators.CompareString(trophy.SHA1, trophy.CalculateSHA1Hash(), false) != 0)
                            DarkMessageBox.ShowError("This file is corrupted, mismatched SHA1 hash!", "PS4 PKG Tool");
                        //else
                        //MessageBox.Show("Loaded successfully.");
                    }
                    else
                        label1.Text = "Saved successfully.";
                    label1.Enabled = trophy.Version > 1;
                    //this.Text = "[ Trophy v" + Conversions.ToString(trophy.Version > 1) + " - Files count: " + listView1.Items.Count + " ]";
                    try
                    {
                        string input = Encoding.UTF8.GetString(trophy.ExtractFileToMemory("TROP.SFM"));
                        Match match1 = new Regex("(?<start>[<]title[-]name[>])(?<titlename>.+)(?<end>[<][/]title[-]name[>])").Match(input);
                        if (match1.Success)
                        {
                            trophy.TitleName = match1.Groups["titlename"].Value;
                            Text = "TRPViewer - " + trophy.TitleName;
                        }
                        Match match2 = new Regex("(?<start>[<]npcommid[>])(?<npcommid>.+)(?<end>[<][/]npcommid[>])").Match(input);
                        if (match2.Success)
                            trophy.NPCommId = match2.Groups["npcommid"].Value;
                    }
                    catch (Exception ex)
                    {
                        ProjectData.SetProjectError(ex);
                        ProjectData.ClearProjectError();
                    }
                }
                ctrophy.SetVersion = trophy.Version;
            }

            this.Text += PS4_PKG.PS4_Title;





            /*
        PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(this.filenames);
        this.Text = "Trophy List : " + PS4_PKG.PS4_Title;

        DataTable dttemp = new DataTable();
        dttemp.Columns.Add("Index");
        dttemp.Columns.Add("Trophy File");
        dttemp.Columns.Add("Offset");
        dttemp.Columns.Add("Size");

        try
        {
            for (int i = 0; i < PS4_PKG.Trophy_File.trophyItemList.Count; i++)
            {
                dttemp.Rows.Add(PS4_PKG.Trophy_File.trophyItemList[i].Index, PS4_PKG.Trophy_File.trophyItemList[i].Name, PS4_PKG.Trophy_File.trophyItemList[i].Offset, PS4_PKG.Trophy_File.trophyItemList[i].Size);
                //dttemp.Rows.Add(PS4_PKG.Param.Tables[i].Name, PS4_PKG.Param.Tables[i].Value);

            }


        }
        catch (Exception ex)
        {

        }
        dataGridView1.DataSource = dttemp;
        filenames = "";
        */
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // if (listView1.SelectedItems.Count > 1) e.Item.Selected = false;
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;

                using (var headerFont = new Font("Segoe UI", 9, FontStyle.Regular))
                {
                    e.Graphics.FillRectangle(Brushes.Pink, e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                }
            }
        }

        private void ddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Logger.log("Extracting trophy image..");
                string failExtract = "";
                var numbersAndWords = NameToExtract.Zip(imageToExtract, (n, w) => new { name = n, image = w });
                foreach (var nw in numbersAndWords)
                {
                    try
                    {
                        using (Bitmap tempImage = new Bitmap(Utilities.BytesToImage(trophy.ExtractFileToMemory(nw.name))))
                        {
                            string filter = PS4_PKG.Param.Title.Replace(":", " -");
                            string title_filter_final = filter.Replace("  -", " -");
                            tempImage.Save(fbd.SelectedPath + @"\" + nw.name, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    catch (Exception a)
                    {
                        failExtract += a.Message + "\n";
                    }


                }

                if (failExtract == "") {
                    DarkMessageBox.ShowInformation("Trophy icon extracted.", "PS4 PKG Tool");
                    Logger.log("Trophy icon extracted.");
                }
                else {
                    DarkMessageBox.ShowWarning("Some icon fail to extract\n\n" + failExtract, "PS4 PKG Tool");
                    Logger.log("Some icon fail to extract\n\n" + failExtract);
                }
                    
            }
        }
    }

}
