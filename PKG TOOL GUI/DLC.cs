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
    public partial class DLC : Form
    {
        List<PS4_Tools.PKG.Official.StoreItems> Items = new List<PS4_Tools.PKG.Official.StoreItems>();
        public string filenames { get; set; }

        public DLC(List<PS4_Tools.PKG.Official.StoreItems> items)
        {

            InitializeComponent();
            dataGridView1.DoubleBuffered(true);

            dataGridView1.Font = new Font("Segoe UI Symbol", 8);
            dataGridView1.DoubleBuffered(true);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Symbol", 8.75F, FontStyle.Bold);
            Items = items;
        }

        private void DLC_Load(object sender, EventArgs e)
        {

            this.Text = "Addon : " + Form1.filenameDLC;

            dataGridView1.DataSource = Items;
            dataGridView1.Columns["Store_Content_Platform"].Visible = false;
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

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
