using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PS4_PKG_Tool
{
    public partial class UpdateChangelog : DarkUI.Forms.DarkForm
    {
        static string xmlContent;
        List<string> app_ver = new List<string>();
        public UpdateChangelog(string txt)
        {
            InitializeComponent();
            xmlContent = txt;
            parseXml();
        }

        private void parseXml()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("app_ver");
            dt.Columns.Add("Changelog");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            XmlElement root = doc.DocumentElement;
            var nodes = doc.SelectSingleNode("//changeinfo"); // You can also use XPath here
           
            foreach (XmlNode node in nodes)
            {
                var cdata = node.FirstChild.InnerText;

                //MessageBox.Show(cdata);
                dt.Rows.Add(node.Attributes["app_ver"].Value, cdata);
                //darkTextBox1.Text += nodes.InnerText.Replace("![CDATA[", "").Replace("]]", "");
            }

            darkDataGridView1.DataSource = dt;
            darkDataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            darkDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            darkDataGridView1.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            darkDataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            darkDataGridView1.ScrollBars = ScrollBars.Both;

            foreach (DataGridViewColumn col in darkDataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //    for (int i = 0; i < nodes.Count; i++)
            //{
            //    darkTextBox1.Text += nodes[i].Attributes["app_ver"].Value;
            //    darkTextBox1.Text += nodes[i].InnerText;

            //    dt.Rows.Add(nodes[i].Attributes["app_ver"].Value, nodes[i].InnerText);
            //}
            //darkDataGridView1.DataSource = dt;
            //darkDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //darkDataGridView1.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
    }
}
