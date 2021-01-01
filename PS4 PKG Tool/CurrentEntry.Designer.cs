namespace PS4_PKG_Tool
{
    partial class CurrentEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrentEntry));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.darkButton1 = new DarkUI.Controls.DarkButton();
            this.darkButton2 = new DarkUI.Controls.DarkButton();
            this.darkToolStrip1 = new DarkUI.Controls.DarkToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.extractAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractDecryptedEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkDataGridView1 = new DarkUI.Controls.DarkDataGridView();
            this.darkDockPanel1 = new DarkUI.Docking.DarkDockPanel();
            this.darkToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.darkDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(151, 56);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(511, 381);
            this.richTextBox1.TabIndex = 88;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // darkButton1
            // 
            this.darkButton1.Enabled = false;
            this.darkButton1.Location = new System.Drawing.Point(190, 406);
            this.darkButton1.Name = "darkButton1";
            this.darkButton1.Size = new System.Drawing.Size(154, 31);
            this.darkButton1.TabIndex = 89;
            this.darkButton1.TabStop = false;
            this.darkButton1.Text = "Extract all";
            this.darkButton1.Visible = false;
            this.darkButton1.Click += new System.EventHandler(this.darkButton1_Click);
            // 
            // darkButton2
            // 
            this.darkButton2.Enabled = false;
            this.darkButton2.Location = new System.Drawing.Point(350, 406);
            this.darkButton2.Name = "darkButton2";
            this.darkButton2.Size = new System.Drawing.Size(154, 31);
            this.darkButton2.TabIndex = 90;
            this.darkButton2.TabStop = false;
            this.darkButton2.Text = "Extract decrypted entry";
            this.darkButton2.Visible = false;
            this.darkButton2.Click += new System.EventHandler(this.darkButton2_Click);
            // 
            // darkToolStrip1
            // 
            this.darkToolStrip1.AutoSize = false;
            this.darkToolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkToolStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.darkToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.darkToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.darkToolStrip1.Name = "darkToolStrip1";
            this.darkToolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.darkToolStrip1.Size = new System.Drawing.Size(804, 28);
            this.darkToolStrip1.TabIndex = 92;
            this.darkToolStrip1.Text = "darkToolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractAllToolStripMenuItem,
            this.extractDecryptedEntryToolStripMenuItem});
            this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(56, 25);
            this.toolStripDropDownButton1.Text = "Extract";
            this.toolStripDropDownButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // extractAllToolStripMenuItem
            // 
            this.extractAllToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.extractAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("extractAllToolStripMenuItem.Image")));
            this.extractAllToolStripMenuItem.Name = "extractAllToolStripMenuItem";
            this.extractAllToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.extractAllToolStripMenuItem.Text = "Extract all item";
            this.extractAllToolStripMenuItem.Click += new System.EventHandler(this.extractAllToolStripMenuItem_Click);
            // 
            // extractDecryptedEntryToolStripMenuItem
            // 
            this.extractDecryptedEntryToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.extractDecryptedEntryToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("extractDecryptedEntryToolStripMenuItem.Image")));
            this.extractDecryptedEntryToolStripMenuItem.Name = "extractDecryptedEntryToolStripMenuItem";
            this.extractDecryptedEntryToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.extractDecryptedEntryToolStripMenuItem.Text = "Extract decrypted entry";
            this.extractDecryptedEntryToolStripMenuItem.Click += new System.EventHandler(this.extractDecryptedEntryToolStripMenuItem_Click);
            // 
            // darkDataGridView1
            // 
            this.darkDataGridView1.AllowUserToAddRows = false;
            this.darkDataGridView1.AllowUserToDeleteRows = false;
            this.darkDataGridView1.AllowUserToDragDropRows = false;
            this.darkDataGridView1.AllowUserToPasteCells = false;
            this.darkDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.darkDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.darkDataGridView1.ColumnHeadersHeight = 4;
            this.darkDataGridView1.Location = new System.Drawing.Point(12, 40);
            this.darkDataGridView1.MultiSelect = false;
            this.darkDataGridView1.Name = "darkDataGridView1";
            this.darkDataGridView1.ReadOnly = true;
            this.darkDataGridView1.RowHeadersWidth = 41;
            this.darkDataGridView1.RowTemplate.Height = 23;
            this.darkDataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.darkDataGridView1.Size = new System.Drawing.Size(780, 492);
            this.darkDataGridView1.TabIndex = 91;
            // 
            // darkDockPanel1
            // 
            this.darkDockPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkDockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkDockPanel1.Location = new System.Drawing.Point(0, 28);
            this.darkDockPanel1.Name = "darkDockPanel1";
            this.darkDockPanel1.Size = new System.Drawing.Size(804, 516);
            this.darkDockPanel1.TabIndex = 93;
            // 
            // CurrentEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 544);
            this.Controls.Add(this.darkDataGridView1);
            this.Controls.Add(this.darkDockPanel1);
            this.Controls.Add(this.darkToolStrip1);
            this.Controls.Add(this.darkButton2);
            this.Controls.Add(this.darkButton1);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CurrentEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entry item";
            this.Load += new System.EventHandler(this.CurrentEntry_Load);
            this.darkToolStrip1.ResumeLayout(false);
            this.darkToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.darkDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private DarkUI.Controls.DarkButton darkButton1;
        private DarkUI.Controls.DarkButton darkButton2;
        private DarkUI.Controls.DarkToolStrip darkToolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem extractAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractDecryptedEntryToolStripMenuItem;
        private DarkUI.Controls.DarkDataGridView darkDataGridView1;
        private DarkUI.Docking.DarkDockPanel darkDockPanel1;
    }
}