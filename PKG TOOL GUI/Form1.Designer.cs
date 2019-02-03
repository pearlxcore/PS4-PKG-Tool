namespace PKG_TOOL_GUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnRename2 = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.btnCleartxt = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textOpen = new System.Windows.Forms.TextBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.console = new ConsoleControl.ConsoleControl();
            this.btnPIP = new System.Windows.Forms.Button();
            this.Ipy = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(127, 452);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 39;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPIP);
            this.groupBox4.Controls.Add(this.Ipy);
            this.groupBox4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Silver;
            this.groupBox4.Location = new System.Drawing.Point(395, 82);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(331, 59);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Python Tools";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRename);
            this.groupBox3.Controls.Add(this.btnRename2);
            this.groupBox3.Controls.Add(this.btnList);
            this.groupBox3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Silver;
            this.groupBox3.Location = new System.Drawing.Point(27, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(362, 59);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rename/Export";
            // 
            // btnRename
            // 
            this.btnRename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRename.Enabled = false;
            this.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRename.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRename.Location = new System.Drawing.Point(7, 19);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(111, 33);
            this.btnRename.TabIndex = 2;
            this.btnRename.Text = "Rename (Readable)";
            this.btnRename.UseVisualStyleBackColor = false;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnRename2
            // 
            this.btnRename2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRename2.Enabled = false;
            this.btnRename2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRename2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRename2.Location = new System.Drawing.Point(124, 19);
            this.btnRename2.Name = "btnRename2";
            this.btnRename2.Size = new System.Drawing.Size(111, 33);
            this.btnRename2.TabIndex = 26;
            this.btnRename2.Text = "Rename (Default)";
            this.btnRename2.UseVisualStyleBackColor = false;
            this.btnRename2.Click += new System.EventHandler(this.btnRename2_Click);
            // 
            // btnList
            // 
            this.btnList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnList.Enabled = false;
            this.btnList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnList.Location = new System.Drawing.Point(241, 19);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(111, 33);
            this.btnList.TabIndex = 3;
            this.btnList.Text = "Export list";
            this.btnList.UseVisualStyleBackColor = false;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnCleartxt
            // 
            this.btnCleartxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCleartxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCleartxt.Location = new System.Drawing.Point(614, 20);
            this.btnCleartxt.Name = "btnCleartxt";
            this.btnCleartxt.Size = new System.Drawing.Size(75, 22);
            this.btnCleartxt.TabIndex = 2;
            this.btnCleartxt.Text = "Clear";
            this.btnCleartxt.UseVisualStyleBackColor = false;
            this.btnCleartxt.Click += new System.EventHandler(this.btnCleartxt_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCleartxt);
            this.groupBox2.Controls.Add(this.textOpen);
            this.groupBox2.Controls.Add(this.buttonOpen);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Silver;
            this.groupBox2.Location = new System.Drawing.Point(27, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(699, 52);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PKG Directory";
            // 
            // textOpen
            // 
            this.textOpen.BackColor = System.Drawing.SystemColors.InfoText;
            this.textOpen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textOpen.ForeColor = System.Drawing.Color.Silver;
            this.textOpen.Location = new System.Drawing.Point(6, 21);
            this.textOpen.Name = "textOpen";
            this.textOpen.ReadOnly = true;
            this.textOpen.Size = new System.Drawing.Size(520, 22);
            this.textOpen.TabIndex = 1;
            // 
            // buttonOpen
            // 
            this.buttonOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpen.ForeColor = System.Drawing.Color.Silver;
            this.buttonOpen.Location = new System.Drawing.Point(532, 20);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 22);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open Folder";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(31, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "By pearlxcore | Credit To cfwprophet And nighty";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.console);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Silver;
            this.groupBox1.Location = new System.Drawing.Point(27, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(699, 295);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // console
            // 
            this.console.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console.ForeColor = System.Drawing.Color.Gray;
            this.console.IsInputEnabled = false;
            this.console.Location = new System.Drawing.Point(7, 14);
            this.console.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.console.Name = "console";
            this.console.SendKeyboardCommandsToProcess = false;
            this.console.ShowDiagnostics = false;
            this.console.Size = new System.Drawing.Size(685, 266);
            this.console.TabIndex = 26;
            // 
            // btnPIP
            // 
            this.btnPIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPIP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPIP.Location = new System.Drawing.Point(164, 18);
            this.btnPIP.Name = "btnPIP";
            this.btnPIP.Size = new System.Drawing.Size(157, 33);
            this.btnPIP.TabIndex = 32;
            this.btnPIP.Text = "Install Dependencies";
            this.btnPIP.UseVisualStyleBackColor = false;
            this.btnPIP.Click += new System.EventHandler(this.btnPIP_Click_1);
            // 
            // Ipy
            // 
            this.Ipy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ipy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Ipy.Location = new System.Drawing.Point(6, 18);
            this.Ipy.Name = "Ipy";
            this.Ipy.Size = new System.Drawing.Size(152, 32);
            this.Ipy.TabIndex = 31;
            this.Ipy.Text = "Install Python ";
            this.Ipy.UseVisualStyleBackColor = false;
            this.Ipy.Click += new System.EventHandler(this.Ipy_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(752, 483);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PKG TOOL GUI";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnRename2;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Button btnCleartxt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textOpen;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private ConsoleControl.ConsoleControl console;
        private System.Windows.Forms.Button btnPIP;
        private System.Windows.Forms.Button Ipy;
    }
}

