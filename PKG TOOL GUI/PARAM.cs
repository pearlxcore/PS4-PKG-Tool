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
    public partial class PARAM : Form
    {
      
        public string filenames { get; set; }
        public PARAM()
        {
            InitializeComponent();
            
        }

        

        private void PARAM_Load(object sender, EventArgs e)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(this.filenames);
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("PARAM");
            dttemp.Columns.Add("VALUE");
            for (int i = 0; i < PS4_PKG.Param.Tables.Count; i++)
            {
                dttemp.Rows.Add(PS4_PKG.Param.Tables[i].Name, PS4_PKG.Param.Tables[i].Value);
            }
            dataGridView1.DataSource = dttemp;
           
            filenames = "";
        }
    }
}
