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
using PS4PKGTool.Utilities.PS4PKGToolHelper;

namespace PS4PKGTool
{
    public partial class DLC : DarkUI.Forms.DarkForm
    {
        List<PS4_Tools.PKG.Official.StoreItems> Items = new List<PS4_Tools.PKG.Official.StoreItems>();
        public string filenames { get; set; }

        public DLC(List<PS4_Tools.PKG.Official.StoreItems> items)
        {

            InitializeComponent();
            darkDataGridView1.ScrollBars = ScrollBars.Both;

            Items = items;
        }

        private void DLC_Load(object sender, EventArgs e)
        {

            this.Text = "Addon : " + Helper.PKG.CurrentPKGTitle;

            darkDataGridView1.DataSource = Items;
            darkDataGridView1.Columns["Store_Content_Platform"].Visible = false;

            //if (backgroundWorker1.IsBusy)
            //    backgroundWorker1.CancelAsync();
            //backgroundWorker1.RunWorkerAsync();

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
