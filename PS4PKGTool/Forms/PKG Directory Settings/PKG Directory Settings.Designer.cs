using System;

namespace PS4PKGTool
{
    partial class PKG_Directory_Settings
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
            components=new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PKG_Directory_Settings));
            darkListBox1=new DarkUI.Controls.DarkListBox(components);
            btnDeletePkgDirectory=new DarkUI.Controls.DarkButton();
            btnAddPkgDirectory=new DarkUI.Controls.DarkButton();
            darkCheckBoxRecursive=new DarkUI.Controls.DarkCheckBox();
            darkSectionPanel1=new DarkUI.Controls.DarkSectionPanel();
            btnClearAllPkgDirectory=new DarkUI.Controls.DarkButton();
            btnLoadPkg=new DarkUI.Controls.DarkButton();
            darkCheckBoxDontshowthisagain=new DarkUI.Controls.DarkCheckBox();
            darkSectionPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // darkListBox1
            // 
            darkListBox1.BackColor=System.Drawing.Color.FromArgb(69, 73, 74);
            darkListBox1.BorderStyle=System.Windows.Forms.BorderStyle.None;
            darkListBox1.Dock=System.Windows.Forms.DockStyle.Fill;
            darkListBox1.DrawMode=System.Windows.Forms.DrawMode.OwnerDrawFixed;
            darkListBox1.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkListBox1.ForeColor=System.Drawing.Color.Gainsboro;
            darkListBox1.FormattingEnabled=true;
            darkListBox1.ItemHeight=15;
            darkListBox1.Location=new System.Drawing.Point(1, 25);
            darkListBox1.Name="darkListBox1";
            darkListBox1.Size=new System.Drawing.Size(446, 91);
            darkListBox1.TabIndex=0;
            darkListBox1.TabStop=false;
            darkListBox1.UseTabStops=false;
            // 
            // btnDeletePkgDirectory
            // 
            btnDeletePkgDirectory.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnDeletePkgDirectory.Location=new System.Drawing.Point(133, 164);
            btnDeletePkgDirectory.Name="btnDeletePkgDirectory";
            btnDeletePkgDirectory.Size=new System.Drawing.Size(103, 27);
            btnDeletePkgDirectory.TabIndex=1;
            btnDeletePkgDirectory.TabStop=false;
            btnDeletePkgDirectory.Text="Delete directory";
            btnDeletePkgDirectory.Click+=btnDeleteFolder_Click;
            // 
            // btnAddPkgDirectory
            // 
            btnAddPkgDirectory.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnAddPkgDirectory.Location=new System.Drawing.Point(18, 164);
            btnAddPkgDirectory.Name="btnAddPkgDirectory";
            btnAddPkgDirectory.Size=new System.Drawing.Size(103, 27);
            btnAddPkgDirectory.TabIndex=2;
            btnAddPkgDirectory.TabStop=false;
            btnAddPkgDirectory.Text="Add directory";
            btnAddPkgDirectory.Click+=btnAddFolder_Click;
            // 
            // darkCheckBoxRecursive
            // 
            darkCheckBoxRecursive.AutoSize=true;
            darkCheckBoxRecursive.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkCheckBoxRecursive.Location=new System.Drawing.Point(18, 18);
            darkCheckBoxRecursive.Name="darkCheckBoxRecursive";
            darkCheckBoxRecursive.Size=new System.Drawing.Size(80, 19);
            darkCheckBoxRecursive.TabIndex=5;
            darkCheckBoxRecursive.Text="Recursive";
            // 
            // darkSectionPanel1
            // 
            darkSectionPanel1.Controls.Add(darkListBox1);
            darkSectionPanel1.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkSectionPanel1.ForeColor=System.Drawing.Color.Gainsboro;
            darkSectionPanel1.Location=new System.Drawing.Point(18, 42);
            darkSectionPanel1.Name="darkSectionPanel1";
            darkSectionPanel1.SectionHeader="PKG Directory List";
            darkSectionPanel1.Size=new System.Drawing.Size(448, 117);
            darkSectionPanel1.TabIndex=6;
            // 
            // btnClearAllPkgDirectory
            // 
            btnClearAllPkgDirectory.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnClearAllPkgDirectory.Location=new System.Drawing.Point(248, 164);
            btnClearAllPkgDirectory.Name="btnClearAllPkgDirectory";
            btnClearAllPkgDirectory.Size=new System.Drawing.Size(103, 27);
            btnClearAllPkgDirectory.TabIndex=8;
            btnClearAllPkgDirectory.TabStop=false;
            btnClearAllPkgDirectory.Text="Clear list";
            btnClearAllPkgDirectory.Click+=darkButton2_Click;
            // 
            // btnLoadPkg
            // 
            btnLoadPkg.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnLoadPkg.Location=new System.Drawing.Point(363, 164);
            btnLoadPkg.Name="btnLoadPkg";
            btnLoadPkg.Size=new System.Drawing.Size(103, 27);
            btnLoadPkg.TabIndex=3;
            btnLoadPkg.TabStop=false;
            btnLoadPkg.Text="Load PKG";
            btnLoadPkg.Click+=btnLoadPkg_Click;
            // 
            // darkCheckBoxDontshowthisagain
            // 
            darkCheckBoxDontshowthisagain.AutoSize=true;
            darkCheckBoxDontshowthisagain.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkCheckBoxDontshowthisagain.Location=new System.Drawing.Point(234, 18);
            darkCheckBoxDontshowthisagain.Name="darkCheckBoxDontshowthisagain";
            darkCheckBoxDontshowthisagain.Size=new System.Drawing.Size(232, 19);
            darkCheckBoxDontshowthisagain.TabIndex=7;
            darkCheckBoxDontshowthisagain.Text="Show PKG directory settings at startup";
            darkCheckBoxDontshowthisagain.MouseClick+=darkCheckBoxDontshowthisagain_MouseClick;
            // 
            // PKG_Directory_Settings
            // 
            AutoScaleDimensions=new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
            ClientSize=new System.Drawing.Size(485, 208);
            Controls.Add(btnClearAllPkgDirectory);
            Controls.Add(darkCheckBoxDontshowthisagain);
            Controls.Add(darkSectionPanel1);
            Controls.Add(darkCheckBoxRecursive);
            Controls.Add(btnLoadPkg);
            Controls.Add(btnAddPkgDirectory);
            Controls.Add(btnDeletePkgDirectory);
            Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            FormBorderStyle=System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon=(System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox=false;
            Name="PKG_Directory_Settings";
            SizeGripStyle=System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
            Text="PKG Directory Settings";
            Load+=PKG_Locations_Load;
            darkSectionPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DarkUI.Controls.DarkListBox darkListBox1;
        private DarkUI.Controls.DarkButton btnDeletePkgDirectory;
        private DarkUI.Controls.DarkButton btnAddPkgDirectory;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxRecursive;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel1;
        private DarkUI.Controls.DarkButton btnLoadPkg;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxDontshowthisagain;
        private DarkUI.Controls.DarkButton btnClearAllPkgDirectory;
    }
}