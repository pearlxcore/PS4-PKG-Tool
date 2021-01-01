namespace PS4_PKG_Tool
{
    partial class DLC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DLC));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.darkDataGridView1 = new DarkUI.Controls.DarkDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.darkDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // darkDataGridView1
            // 
            this.darkDataGridView1.AllowDrop = true;
            this.darkDataGridView1.AllowUserToAddRows = false;
            this.darkDataGridView1.AllowUserToDeleteRows = false;
            this.darkDataGridView1.AllowUserToDragDropRows = false;
            this.darkDataGridView1.AllowUserToPasteCells = false;
            this.darkDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.darkDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.darkDataGridView1.ColumnHeadersHeight = 4;
            this.darkDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.darkDataGridView1.MultiSelect = false;
            this.darkDataGridView1.Name = "darkDataGridView1";
            this.darkDataGridView1.ReadOnly = true;
            this.darkDataGridView1.RowHeadersWidth = 41;
            this.darkDataGridView1.RowTemplate.Height = 23;
            this.darkDataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.darkDataGridView1.Size = new System.Drawing.Size(800, 450);
            this.darkDataGridView1.TabIndex = 92;
            // 
            // DLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.darkDataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DLC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DLC";
            this.Load += new System.EventHandler(this.DLC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.darkDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DarkUI.Controls.DarkDataGridView darkDataGridView1;
    }
}