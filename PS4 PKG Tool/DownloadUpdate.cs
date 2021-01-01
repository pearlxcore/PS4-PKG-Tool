using ByteSizeLib;
using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4_PKG_Tool
{
    public partial class DownloadUpdate : DarkForm
    {
        CancellationTokenSource cts;
        WebClient client = new WebClient();

        private string URL;
        private string PART;
        private string SIZE;
        private int part;
        string downloading = "no";
        private string downloadedFilePath;

        public string filenames { get; set; }

        public DownloadUpdate()
        {
            InitializeComponent();
            darkDataGridView1.ScrollBars = ScrollBars.Both;

        }

        private void DownloadUpdate_Load(object sender, EventArgs e)
        {
            PS4_Tools.PKG.SceneRelated.Unprotected_PKG PS4_PKG = PS4_Tools.PKG.SceneRelated.Read_PKG(filenames);
            this.Text += PS4_PKG.PS4_Title;
            var item = PS4_Tools.PKG.Official.CheckForUpdate(PS4_PKG.Param.TITLEID);
            DataTable dt = new DataTable();
            dt.Columns.Add("Part(s)");
            dt.Columns.Add("File Size");
            dt.Columns.Add("Hash Value");
            dt.Columns.Add("URL");

            labelUpdateVersion.Text = item.Tag.Package.Version;
            int ver = Convert.ToInt32(item.Tag.Package.System_ver);

            string hexOutput = String.Format("{0:X}", ver.ToString("X"));
            string first_three = hexOutput.Substring(0, 3);
            string version = first_three.Insert(1, ".");
            labelSystemReq.Text = version;

            labelTotalFile.Text = item.Tag.Package.Manifest_item.pieces.Count.ToString();
            long sizes = Convert.ToInt64(item.Tag.Package.Size);
            var size_final = ByteSize.FromBytes(sizes).ToString();
            labelTotalSize.Text = size_final;

            part = 0;
            foreach (var items in item.Tag.Package.Manifest_item.pieces)
            {
                part++;
                long fileSize = items.fileSize;
                var fileOffset = items.fileOffset;
                string hashValue = items.hashValue.ToString();
                string url = items.url.ToString();
                var size = ByteSizeLib.ByteSize.FromBytes(fileSize);

                dt.Rows.Add("Part " + part, size, hashValue.ToUpper(), url);
                darkDataGridView1.DataSource = dt;
            }

            darkDataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            darkDataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            darkDataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }


        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\Game update");
            string sourceFile = URL;
            int pos = URL.LastIndexOf("/") + 1;
            string filename = URL.Substring(pos, URL.Length - pos);
            //MessageBox.Show(URL.Substring(pos, URL.Length - pos)); 
            string destFile = Properties.Settings.Default.DOWNLOADFOLDER + "\\" + filename;

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);

            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);


            // Starts the download

            client.DownloadFileAsync(new Uri(sourceFile), destFile);

            downloadToolStripMenuItem1.Text = "Cancel download";
            downloading = "yes";

        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            progressBar1.Value = 0;

            if (e.Cancelled)
            {
                client.Dispose();

                //MessageBox.Show("The download has been cancelled");
                progressBar1.Visible = false;
                label10.Visible = false;
                label10.Text = "";
                downloadToolStripMenuItem1.Text = "Download Selected PKG";

                downloading = "no";
                return;
            }

            if (e.Error != null) // We have an error! Retry a few times, then abort.
            {
                client.Dispose();

                DarkMessageBox.ShowError("An error ocurred while trying to download file", "PS4 PKG Tool");
                progressBar1.Visible = false;
                label10.Visible = false;
                label10.Text = "";
                downloading = "no";
                return;
            }
            client.Dispose();
            DialogResult dialog = DarkMessageBox.DialogYesNo("File succesfully downloaded. Open folder?", "PS4 PKG Tool");
            if (dialog == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(Properties.Settings.Default.DOWNLOADFOLDER);
            }
            progressBar1.Visible = false;
            label10.Visible = false;
            label10.Text = "";
            downloading = "no";
        }


        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }



        public void CancelDownloadingFile()
        {
            client.CancelAsync();
            client.Dispose();
            downloading = "2";
        }




        private void darkDataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void downloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DOWNLOADFOLDER == string.Empty)
            {
                DarkMessageBox.ShowError("Select PKG update download path in setting", "PS4 PKG Tool");
                return;
            }

            if (downloading == "no")
            {

                //get selected item value
                if (darkDataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    try
                    {
                        //get each selected pkg full path
                        foreach (DataGridViewCell cell in darkDataGridView1.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = darkDataGridView1.Rows[selectedrowindex];
                            //path + filename.pkg
                            URL = Convert.ToString(selectedRow.Cells[3].Value);
                            PART = Convert.ToString(selectedRow.Cells[0].Value);
                            SIZE = Convert.ToString(selectedRow.Cells[1].Value);
                        }
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        DarkMessageBox.ShowError("The Clipboard could not be accessed. Please try again.", "PS4 PKG Tool");
                        return;
                    }

                }

                if (URL != null)
                {
                    progressBar1.Visible = true;
                    label10.Visible = true;
                    label10.Text = "Downloading update (" + SIZE + ")..";
                    Logger.log("Downloading update (" + SIZE + ")..");
                    // darkDataGridView1.Enabled = false;
                    if (backgroundWorker1.IsBusy != true)
                        backgroundWorker1.RunWorkerAsync();
                }
                else
                {
                    DarkMessageBox.ShowInformation("Select an update to download.", "PS4 PKG Tool");
                }
            }
            else
            {
                DialogResult dialog = DarkMessageBox.DialogYesNo("Cancel Download?", "PS4 PKG Tool");
                if (dialog == DialogResult.Yes)
                {
                    CancelDownloadingFile();
                    DarkMessageBox.ShowInformation("The download has been cancelled.", "PS4 PKG Tool");
                    Logger.log("The download has been cancelled.");
                }
            }
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in darkDataGridView1.SelectedCells)
            {
                int selectedrowindex = cell.RowIndex;

                DataGridViewRow selectedRow = darkDataGridView1.Rows[selectedrowindex];

                Clipboard.SetText(selectedRow.Cells[3].Value.ToString());
            }
            DarkMessageBox.ShowInformation("URL copied to clipboard.", "PS4 PKG Tool");
            Logger.log("URL copied to clipboard.");
        }
    }
}
