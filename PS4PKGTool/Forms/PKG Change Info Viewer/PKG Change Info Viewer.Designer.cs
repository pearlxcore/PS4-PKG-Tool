namespace PS4PKGTool
{
    partial class PKGChangeInfoViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PKGChangeInfoViewer));
            darkDataGridView1=new DarkUI.Controls.DarkDataGridView();
            ((System.ComponentModel.ISupportInitialize)darkDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // darkDataGridView1
            // 
            darkDataGridView1.AllowUserToAddRows=false;
            darkDataGridView1.AllowUserToDeleteRows=false;
            darkDataGridView1.AllowUserToDragDropRows=false;
            darkDataGridView1.AllowUserToPasteCells=false;
            darkDataGridView1.AutoScroll=true;
            darkDataGridView1.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            darkDataGridView1.ColumnHeadersHeight=4;
            darkDataGridView1.Dock=System.Windows.Forms.DockStyle.Fill;
            darkDataGridView1.Location=new System.Drawing.Point(0, 0);
            darkDataGridView1.MultiSelect=false;
            darkDataGridView1.Name="darkDataGridView1";
            darkDataGridView1.ReadOnly=true;
            darkDataGridView1.RowHeadersWidth=41;
            darkDataGridView1.RowTemplate.Height=23;
            darkDataGridView1.ScrollBars=System.Windows.Forms.ScrollBars.Horizontal;
            darkDataGridView1.Size=new System.Drawing.Size(699, 417);
            darkDataGridView1.TabIndex=76;
            // 
            // PKGChangeInfoViewer
            // 
            AutoScaleDimensions=new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
            ClientSize=new System.Drawing.Size(699, 417);
            Controls.Add(darkDataGridView1);
            Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Icon=(System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name="PKGChangeInfoViewer";
            StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;
            Text="PKG Change Info Viewer";
            ((System.ComponentModel.ISupportInitialize)darkDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DarkUI.Controls.DarkDataGridView darkDataGridView1;
    }
}