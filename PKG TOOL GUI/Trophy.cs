using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PKG_TOOL_GUI
{
    public partial class Trophy : Form
    {
        public string filenames { get; set; }

        public Trophy()
        {
            InitializeComponent();
            dataGridView1.Font = new Font("Segoe UI Symbol", 8);
            dataGridView1.DoubleBuffered(true);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Symbol", 8.75F, FontStyle.Bold);

        }

        private void Trophy_Load(object sender, EventArgs e)
        {

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
        }
    }
}
