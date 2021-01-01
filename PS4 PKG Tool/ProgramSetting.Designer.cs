namespace PS4_PKG_Tool
{
    partial class ProgramSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramSetting));
            this.darkSectionPanel2 = new DarkUI.Controls.DarkSectionPanel();
            this.PKGType = new DarkUI.Controls.DarkCheckBox();
            this.Category = new DarkUI.Controls.DarkCheckBox();
            this.Size = new DarkUI.Controls.DarkCheckBox();
            this.Location = new DarkUI.Controls.DarkCheckBox();
            this.TitleID = new DarkUI.Controls.DarkCheckBox();
            this.ContentID = new DarkUI.Controls.DarkCheckBox();
            this.Region = new DarkUI.Controls.DarkCheckBox();
            this.Version = new DarkUI.Controls.DarkCheckBox();
            this.SystemFirmware = new DarkUI.Controls.DarkCheckBox();
            this.PKGname = new DarkUI.Controls.DarkCheckBox();
            this.darkSectionPanel3 = new DarkUI.Controls.DarkSectionPanel();
            this.BGM = new DarkUI.Controls.DarkCheckBox();
            this.darkSectionPanel4 = new DarkUI.Controls.DarkSectionPanel();
            this.darkLabelserveModuleInstalled = new DarkUI.Controls.DarkLabel();
            this.darkLabelNodejsInstalled = new DarkUI.Controls.DarkLabel();
            this.darkLabel4 = new DarkUI.Controls.DarkLabel();
            this.darkLabel3 = new DarkUI.Controls.DarkLabel();
            this.darkButtonInstaleServemodule = new DarkUI.Controls.DarkButton();
            this.darkButtonInstallNodejs = new DarkUI.Controls.DarkButton();
            this.darkButton2 = new DarkUI.Controls.DarkButton();
            this.darkComboBoxServerIP = new DarkUI.Controls.DarkComboBox();
            this.darkLabel2 = new DarkUI.Controls.DarkLabel();
            this.darkLabel1 = new DarkUI.Controls.DarkLabel();
            this.tbPS4IP = new DarkUI.Controls.DarkTextBox();
            this.darkButton1 = new DarkUI.Controls.DarkButton();
            this.darkSectionPanel1 = new DarkUI.Controls.DarkSectionPanel();
            this.btnDownloadFolder = new DarkUI.Controls.DarkButton();
            this.tbDownloadFolder = new DarkUI.Controls.DarkTextBox();
            this.neoEnable = new DarkUI.Controls.DarkCheckBox();
            this.PSVR = new DarkUI.Controls.DarkCheckBox();
            this.ps5bc = new DarkUI.Controls.DarkCheckBox();
            this.darkSectionPanel2.SuspendLayout();
            this.darkSectionPanel3.SuspendLayout();
            this.darkSectionPanel4.SuspendLayout();
            this.darkSectionPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // darkSectionPanel2
            // 
            this.darkSectionPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkSectionPanel2.Controls.Add(this.ps5bc);
            this.darkSectionPanel2.Controls.Add(this.neoEnable);
            this.darkSectionPanel2.Controls.Add(this.PSVR);
            this.darkSectionPanel2.Controls.Add(this.PKGType);
            this.darkSectionPanel2.Controls.Add(this.Category);
            this.darkSectionPanel2.Controls.Add(this.Size);
            this.darkSectionPanel2.Controls.Add(this.Location);
            this.darkSectionPanel2.Controls.Add(this.TitleID);
            this.darkSectionPanel2.Controls.Add(this.ContentID);
            this.darkSectionPanel2.Controls.Add(this.Region);
            this.darkSectionPanel2.Controls.Add(this.Version);
            this.darkSectionPanel2.Controls.Add(this.SystemFirmware);
            this.darkSectionPanel2.Controls.Add(this.PKGname);
            this.darkSectionPanel2.Location = new System.Drawing.Point(14, 12);
            this.darkSectionPanel2.Name = "darkSectionPanel2";
            this.darkSectionPanel2.SectionHeader = "Grid Settings";
            this.darkSectionPanel2.Size = new System.Drawing.Size(316, 248);
            this.darkSectionPanel2.TabIndex = 1;
            // 
            // PKGType
            // 
            this.PKGType.AutoSize = true;
            this.PKGType.Checked = true;
            this.PKGType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PKGType.Location = new System.Drawing.Point(169, 65);
            this.PKGType.Name = "PKGType";
            this.PKGType.Size = new System.Drawing.Size(74, 19);
            this.PKGType.TabIndex = 9;
            this.PKGType.Text = "PKG Type";
            this.PKGType.CheckedChanged += new System.EventHandler(this.PKGType_Click);
            // 
            // Category
            // 
            this.Category.AutoSize = true;
            this.Category.Checked = true;
            this.Category.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Category.Location = new System.Drawing.Point(169, 95);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(74, 19);
            this.Category.TabIndex = 8;
            this.Category.Text = "Category";
            this.Category.CheckedChanged += new System.EventHandler(this.Category_Click);
            // 
            // Size
            // 
            this.Size.AutoSize = true;
            this.Size.Checked = true;
            this.Size.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Size.Location = new System.Drawing.Point(169, 125);
            this.Size.Name = "Size";
            this.Size.Size = new System.Drawing.Size(46, 19);
            this.Size.TabIndex = 7;
            this.Size.Text = "Size";
            this.Size.CheckedChanged += new System.EventHandler(this.Size_Click);
            // 
            // Location
            // 
            this.Location.AutoSize = true;
            this.Location.Checked = true;
            this.Location.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Location.Location = new System.Drawing.Point(169, 155);
            this.Location.Name = "Location";
            this.Location.Size = new System.Drawing.Size(72, 19);
            this.Location.TabIndex = 6;
            this.Location.Text = "Location";
            this.Location.CheckedChanged += new System.EventHandler(this.Location_Click);
            // 
            // TitleID
            // 
            this.TitleID.AutoSize = true;
            this.TitleID.Checked = true;
            this.TitleID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TitleID.Location = new System.Drawing.Point(18, 65);
            this.TitleID.Name = "TitleID";
            this.TitleID.Size = new System.Drawing.Size(62, 19);
            this.TitleID.TabIndex = 5;
            this.TitleID.Text = "Title ID";
            this.TitleID.CheckedChanged += new System.EventHandler(this.TitleID_Click);
            // 
            // ContentID
            // 
            this.ContentID.AutoSize = true;
            this.ContentID.Checked = true;
            this.ContentID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ContentID.Location = new System.Drawing.Point(18, 95);
            this.ContentID.Name = "ContentID";
            this.ContentID.Size = new System.Drawing.Size(83, 19);
            this.ContentID.TabIndex = 4;
            this.ContentID.Text = "Content ID";
            this.ContentID.CheckedChanged += new System.EventHandler(this.ContentID_Click);
            // 
            // Region
            // 
            this.Region.AutoSize = true;
            this.Region.Checked = true;
            this.Region.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Region.Location = new System.Drawing.Point(18, 125);
            this.Region.Name = "Region";
            this.Region.Size = new System.Drawing.Size(63, 19);
            this.Region.TabIndex = 3;
            this.Region.Text = "Region";
            this.Region.CheckedChanged += new System.EventHandler(this.Region_Click);
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.Checked = true;
            this.Version.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Version.Location = new System.Drawing.Point(169, 35);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(64, 19);
            this.Version.TabIndex = 2;
            this.Version.Text = "Version";
            this.Version.CheckedChanged += new System.EventHandler(this.Version_Click);
            // 
            // SystemFirmware
            // 
            this.SystemFirmware.AutoSize = true;
            this.SystemFirmware.Checked = true;
            this.SystemFirmware.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SystemFirmware.Location = new System.Drawing.Point(18, 155);
            this.SystemFirmware.Name = "SystemFirmware";
            this.SystemFirmware.Size = new System.Drawing.Size(116, 19);
            this.SystemFirmware.TabIndex = 1;
            this.SystemFirmware.Text = "System Firmware";
            this.SystemFirmware.CheckedChanged += new System.EventHandler(this.SystemFirmware_Click);
            // 
            // PKGname
            // 
            this.PKGname.AutoSize = true;
            this.PKGname.Checked = true;
            this.PKGname.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PKGname.Enabled = false;
            this.PKGname.Location = new System.Drawing.Point(18, 35);
            this.PKGname.Name = "PKGname";
            this.PKGname.Size = new System.Drawing.Size(82, 19);
            this.PKGname.TabIndex = 0;
            this.PKGname.Text = "PKG Name";
            // 
            // darkSectionPanel3
            // 
            this.darkSectionPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkSectionPanel3.Controls.Add(this.BGM);
            this.darkSectionPanel3.Location = new System.Drawing.Point(334, 11);
            this.darkSectionPanel3.Name = "darkSectionPanel3";
            this.darkSectionPanel3.SectionHeader = "Sound Settings";
            this.darkSectionPanel3.Size = new System.Drawing.Size(198, 64);
            this.darkSectionPanel3.TabIndex = 1;
            // 
            // BGM
            // 
            this.BGM.AutoSize = true;
            this.BGM.Checked = true;
            this.BGM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BGM.Location = new System.Drawing.Point(32, 34);
            this.BGM.Name = "BGM";
            this.BGM.Size = new System.Drawing.Size(125, 19);
            this.BGM.TabIndex = 0;
            this.BGM.Text = "Background Music";
            this.BGM.Click += new System.EventHandler(this.BGM_Click);
            // 
            // darkSectionPanel4
            // 
            this.darkSectionPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkSectionPanel4.Controls.Add(this.darkLabelserveModuleInstalled);
            this.darkSectionPanel4.Controls.Add(this.darkLabelNodejsInstalled);
            this.darkSectionPanel4.Controls.Add(this.darkLabel4);
            this.darkSectionPanel4.Controls.Add(this.darkLabel3);
            this.darkSectionPanel4.Controls.Add(this.darkButtonInstaleServemodule);
            this.darkSectionPanel4.Controls.Add(this.darkButtonInstallNodejs);
            this.darkSectionPanel4.Controls.Add(this.darkButton2);
            this.darkSectionPanel4.Controls.Add(this.darkComboBoxServerIP);
            this.darkSectionPanel4.Controls.Add(this.darkLabel2);
            this.darkSectionPanel4.Controls.Add(this.darkLabel1);
            this.darkSectionPanel4.Controls.Add(this.tbPS4IP);
            this.darkSectionPanel4.Location = new System.Drawing.Point(336, 82);
            this.darkSectionPanel4.Name = "darkSectionPanel4";
            this.darkSectionPanel4.SectionHeader = "PS4 PKG Sender Settings";
            this.darkSectionPanel4.Size = new System.Drawing.Size(198, 234);
            this.darkSectionPanel4.TabIndex = 2;
            // 
            // darkLabelserveModuleInstalled
            // 
            this.darkLabelserveModuleInstalled.AutoSize = true;
            this.darkLabelserveModuleInstalled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabelserveModuleInstalled.Location = new System.Drawing.Point(98, 192);
            this.darkLabelserveModuleInstalled.Name = "darkLabelserveModuleInstalled";
            this.darkLabelserveModuleInstalled.Size = new System.Drawing.Size(0, 15);
            this.darkLabelserveModuleInstalled.TabIndex = 11;
            // 
            // darkLabelNodejsInstalled
            // 
            this.darkLabelNodejsInstalled.AutoSize = true;
            this.darkLabelNodejsInstalled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabelNodejsInstalled.Location = new System.Drawing.Point(68, 158);
            this.darkLabelNodejsInstalled.Name = "darkLabelNodejsInstalled";
            this.darkLabelNodejsInstalled.Size = new System.Drawing.Size(0, 15);
            this.darkLabelNodejsInstalled.TabIndex = 10;
            // 
            // darkLabel4
            // 
            this.darkLabel4.AutoSize = true;
            this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel4.Location = new System.Drawing.Point(19, 192);
            this.darkLabel4.Name = "darkLabel4";
            this.darkLabel4.Size = new System.Drawing.Size(83, 15);
            this.darkLabel4.TabIndex = 9;
            this.darkLabel4.Text = "Server Module";
            // 
            // darkLabel3
            // 
            this.darkLabel3.AutoSize = true;
            this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel3.Location = new System.Drawing.Point(19, 158);
            this.darkLabel3.Name = "darkLabel3";
            this.darkLabel3.Size = new System.Drawing.Size(47, 15);
            this.darkLabel3.TabIndex = 8;
            this.darkLabel3.Text = "Node.js";
            // 
            // darkButtonInstaleServemodule
            // 
            this.darkButtonInstaleServemodule.Location = new System.Drawing.Point(129, 188);
            this.darkButtonInstaleServemodule.Name = "darkButtonInstaleServemodule";
            this.darkButtonInstaleServemodule.Size = new System.Drawing.Size(51, 23);
            this.darkButtonInstaleServemodule.TabIndex = 7;
            this.darkButtonInstaleServemodule.Text = "Install";
            this.darkButtonInstaleServemodule.Click += new System.EventHandler(this.darkButtonInstaleServemodule_Click);
            // 
            // darkButtonInstallNodejs
            // 
            this.darkButtonInstallNodejs.Location = new System.Drawing.Point(129, 154);
            this.darkButtonInstallNodejs.Name = "darkButtonInstallNodejs";
            this.darkButtonInstallNodejs.Size = new System.Drawing.Size(51, 23);
            this.darkButtonInstallNodejs.TabIndex = 6;
            this.darkButtonInstallNodejs.Text = "Install";
            this.darkButtonInstallNodejs.Click += new System.EventHandler(this.darkButtonInstallNodejs_Click);
            // 
            // darkButton2
            // 
            this.darkButton2.Location = new System.Drawing.Point(129, 119);
            this.darkButton2.Name = "darkButton2";
            this.darkButton2.Size = new System.Drawing.Size(51, 24);
            this.darkButton2.TabIndex = 5;
            this.darkButton2.Text = "Ping";
            this.darkButton2.Click += new System.EventHandler(this.darkButton2_Click_1);
            // 
            // darkComboBoxServerIP
            // 
            this.darkComboBoxServerIP.FormattingEnabled = true;
            this.darkComboBoxServerIP.Location = new System.Drawing.Point(21, 65);
            this.darkComboBoxServerIP.Name = "darkComboBoxServerIP";
            this.darkComboBoxServerIP.Size = new System.Drawing.Size(158, 24);
            this.darkComboBoxServerIP.TabIndex = 4;
            // 
            // darkLabel2
            // 
            this.darkLabel2.AutoSize = true;
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Location = new System.Drawing.Point(18, 102);
            this.darkLabel2.Name = "darkLabel2";
            this.darkLabel2.Size = new System.Drawing.Size(88, 15);
            this.darkLabel2.TabIndex = 3;
            this.darkLabel2.Text = "PS4 IP address :";
            // 
            // darkLabel1
            // 
            this.darkLabel1.AutoSize = true;
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(18, 46);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(101, 15);
            this.darkLabel1.TabIndex = 2;
            this.darkLabel1.Text = "Server IP address :";
            // 
            // tbPS4IP
            // 
            this.tbPS4IP.Location = new System.Drawing.Point(22, 120);
            this.tbPS4IP.Name = "tbPS4IP";
            this.tbPS4IP.Size = new System.Drawing.Size(101, 23);
            this.tbPS4IP.TabIndex = 0;
            // 
            // darkButton1
            // 
            this.darkButton1.Location = new System.Drawing.Point(336, 322);
            this.darkButton1.Name = "darkButton1";
            this.darkButton1.Size = new System.Drawing.Size(198, 23);
            this.darkButton1.TabIndex = 3;
            this.darkButton1.Text = "Save & Close";
            this.darkButton1.Click += new System.EventHandler(this.darkButton1_Click);
            // 
            // darkSectionPanel1
            // 
            this.darkSectionPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkSectionPanel1.Controls.Add(this.btnDownloadFolder);
            this.darkSectionPanel1.Controls.Add(this.tbDownloadFolder);
            this.darkSectionPanel1.Location = new System.Drawing.Point(14, 266);
            this.darkSectionPanel1.Name = "darkSectionPanel1";
            this.darkSectionPanel1.SectionHeader = "Download Folder (Update PKG)";
            this.darkSectionPanel1.Size = new System.Drawing.Size(316, 79);
            this.darkSectionPanel1.TabIndex = 4;
            // 
            // btnDownloadFolder
            // 
            this.btnDownloadFolder.Location = new System.Drawing.Point(240, 38);
            this.btnDownloadFolder.Name = "btnDownloadFolder";
            this.btnDownloadFolder.Size = new System.Drawing.Size(33, 23);
            this.btnDownloadFolder.TabIndex = 5;
            this.btnDownloadFolder.Text = "..";
            this.btnDownloadFolder.Click += new System.EventHandler(this.darkButton2_Click);
            // 
            // tbDownloadFolder
            // 
            this.tbDownloadFolder.Location = new System.Drawing.Point(18, 38);
            this.tbDownloadFolder.Name = "tbDownloadFolder";
            this.tbDownloadFolder.ReadOnly = true;
            this.tbDownloadFolder.Size = new System.Drawing.Size(216, 23);
            this.tbDownloadFolder.TabIndex = 0;
            // 
            // neoEnable
            // 
            this.neoEnable.AutoSize = true;
            this.neoEnable.Checked = true;
            this.neoEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.neoEnable.Location = new System.Drawing.Point(169, 185);
            this.neoEnable.Name = "neoEnable";
            this.neoEnable.Size = new System.Drawing.Size(127, 19);
            this.neoEnable.TabIndex = 11;
            this.neoEnable.Text = "PS4 Pro Enchanced";
            this.neoEnable.Click += new System.EventHandler(this.neoEnable_Click);
            // 
            // PSVR
            // 
            this.PSVR.AutoSize = true;
            this.PSVR.Checked = true;
            this.PSVR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PSVR.Location = new System.Drawing.Point(18, 185);
            this.PSVR.Name = "PSVR";
            this.PSVR.Size = new System.Drawing.Size(56, 19);
            this.PSVR.TabIndex = 10;
            this.PSVR.Text = "PS VR";
            this.PSVR.Click += new System.EventHandler(this.PSVR_Click);
            // 
            // ps5bc
            // 
            this.ps5bc.AutoSize = true;
            this.ps5bc.Checked = true;
            this.ps5bc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ps5bc.Location = new System.Drawing.Point(18, 215);
            this.ps5bc.Name = "ps5bc";
            this.ps5bc.Size = new System.Drawing.Size(63, 19);
            this.ps5bc.TabIndex = 12;
            this.ps5bc.Text = "PS5 BC";
            this.ps5bc.Click += new System.EventHandler(this.ps5bc_Click);
            // 
            // ProgramSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 356);
            this.Controls.Add(this.darkSectionPanel1);
            this.Controls.Add(this.darkButton1);
            this.Controls.Add(this.darkSectionPanel3);
            this.Controls.Add(this.darkSectionPanel4);
            this.Controls.Add(this.darkSectionPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProgramSetting";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PS4 PKG Tool";
            this.Load += new System.EventHandler(this.ProgramSetting_Load);
            this.darkSectionPanel2.ResumeLayout(false);
            this.darkSectionPanel2.PerformLayout();
            this.darkSectionPanel3.ResumeLayout(false);
            this.darkSectionPanel3.PerformLayout();
            this.darkSectionPanel4.ResumeLayout(false);
            this.darkSectionPanel4.PerformLayout();
            this.darkSectionPanel1.ResumeLayout(false);
            this.darkSectionPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel2;
        private DarkUI.Controls.DarkCheckBox PKGname;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel3;
        private DarkUI.Controls.DarkCheckBox BGM;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel4;
        private DarkUI.Controls.DarkLabel darkLabel2;
        private DarkUI.Controls.DarkLabel darkLabel1;
        private DarkUI.Controls.DarkTextBox tbPS4IP;
        private DarkUI.Controls.DarkCheckBox TitleID;
        private DarkUI.Controls.DarkCheckBox ContentID;
        private DarkUI.Controls.DarkCheckBox Region;
        private DarkUI.Controls.DarkCheckBox Version;
        private DarkUI.Controls.DarkCheckBox SystemFirmware;
        private DarkUI.Controls.DarkCheckBox PKGType;
        private DarkUI.Controls.DarkCheckBox Category;
        private DarkUI.Controls.DarkCheckBox Size;
        private DarkUI.Controls.DarkCheckBox Location;
        private DarkUI.Controls.DarkButton darkButton1;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel1;
        private DarkUI.Controls.DarkButton btnDownloadFolder;
        private DarkUI.Controls.DarkTextBox tbDownloadFolder;
        private DarkUI.Controls.DarkComboBox darkComboBoxServerIP;
        private DarkUI.Controls.DarkButton darkButton2;
        private DarkUI.Controls.DarkLabel darkLabelserveModuleInstalled;
        private DarkUI.Controls.DarkLabel darkLabelNodejsInstalled;
        private DarkUI.Controls.DarkLabel darkLabel4;
        private DarkUI.Controls.DarkLabel darkLabel3;
        private DarkUI.Controls.DarkButton darkButtonInstaleServemodule;
        private DarkUI.Controls.DarkButton darkButtonInstallNodejs;
        private DarkUI.Controls.DarkCheckBox neoEnable;
        private DarkUI.Controls.DarkCheckBox PSVR;
        private DarkUI.Controls.DarkCheckBox ps5bc;
    }
}