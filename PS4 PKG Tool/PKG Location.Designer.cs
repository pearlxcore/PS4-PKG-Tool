using System;

namespace PS4_PKG_Tool
{
    partial class PKG_Locations
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PKG_Locations));
            this.darkListBox1 = new DarkUI.Controls.DarkListBox(this.components);
            this.btnDeleteFolder = new DarkUI.Controls.DarkButton();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.darkCheckBoxRecursive = new DarkUI.Controls.DarkCheckBox();
            this.darkSectionPanel1 = new DarkUI.Controls.DarkSectionPanel();
            this.darkButton1 = new DarkUI.Controls.DarkButton();
            this.darkSectionPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // darkListBox1
            // 
            this.darkListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.darkListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.darkListBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.darkListBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.darkListBox1.FormattingEnabled = true;
            this.darkListBox1.ItemHeight = 15;
            this.darkListBox1.Location = new System.Drawing.Point(1, 29);
            this.darkListBox1.Name = "darkListBox1";
            this.darkListBox1.Size = new System.Drawing.Size(494, 87);
            this.darkListBox1.TabIndex = 0;
            this.darkListBox1.TabStop = false;
            this.darkListBox1.UseTabStops = false;
            // 
            // btnDeleteFolder
            // 
            this.btnDeleteFolder.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteFolder.Location = new System.Drawing.Point(75, 142);
            this.btnDeleteFolder.Name = "btnDeleteFolder";
            this.btnDeleteFolder.Size = new System.Drawing.Size(40, 27);
            this.btnDeleteFolder.TabIndex = 1;
            this.btnDeleteFolder.TabStop = false;
            this.btnDeleteFolder.Text = "-";
            this.btnDeleteFolder.Click += new System.EventHandler(this.btnDeleteFolder_Click);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFolder.Location = new System.Drawing.Point(20, 142);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(40, 27);
            this.btnAddFolder.TabIndex = 2;
            this.btnAddFolder.TabStop = false;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // darkCheckBoxRecursive
            // 
            this.darkCheckBoxRecursive.AutoSize = true;
            this.darkCheckBoxRecursive.Location = new System.Drawing.Point(372, 146);
            this.darkCheckBoxRecursive.Name = "darkCheckBoxRecursive";
            this.darkCheckBoxRecursive.Size = new System.Drawing.Size(144, 19);
            this.darkCheckBoxRecursive.TabIndex = 5;
            this.darkCheckBoxRecursive.Text = "Scan folder recursively";
            // 
            // darkSectionPanel1
            // 
            this.darkSectionPanel1.Controls.Add(this.darkListBox1);
            this.darkSectionPanel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.darkSectionPanel1.ForeColor = System.Drawing.Color.Gainsboro;
            this.darkSectionPanel1.Location = new System.Drawing.Point(20, 20);
            this.darkSectionPanel1.Name = "darkSectionPanel1";
            this.darkSectionPanel1.SectionHeader = "Add PS4 PKG Folder";
            this.darkSectionPanel1.Size = new System.Drawing.Size(496, 117);
            this.darkSectionPanel1.TabIndex = 6;
            // 
            // darkButton1
            // 
            this.darkButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.darkButton1.Location = new System.Drawing.Point(129, 142);
            this.darkButton1.Name = "darkButton1";
            this.darkButton1.Size = new System.Drawing.Size(40, 27);
            this.darkButton1.TabIndex = 3;
            this.darkButton1.TabStop = false;
            this.darkButton1.Text = "Ok";
            this.darkButton1.Click += new System.EventHandler(this.darkButton1_Click);
            // 
            // PKG_Locations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 187);
            this.Controls.Add(this.darkSectionPanel1);
            this.Controls.Add(this.darkCheckBoxRecursive);
            this.Controls.Add(this.darkButton1);
            this.Controls.Add(this.btnAddFolder);
            this.Controls.Add(this.btnDeleteFolder);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PKG_Locations";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PS4 PKG Tool";
            this.Load += new System.EventHandler(this.PKG_Locations_Load);
            this.darkSectionPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkUI.Controls.DarkListBox darkListBox1;
        private DarkUI.Controls.DarkButton btnDeleteFolder;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private DarkUI.Controls.DarkCheckBox darkCheckBoxRecursive;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel1;
        private DarkUI.Controls.DarkButton darkButton1;
    }
}