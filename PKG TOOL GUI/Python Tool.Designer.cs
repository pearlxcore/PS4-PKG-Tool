namespace PKG_TOOL_GUI
{
    partial class Python_Tool
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPIP = new System.Windows.Forms.Button();
            this.Ipy = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPIP);
            this.groupBox4.Controls.Add(this.Ipy);
            this.groupBox4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Silver;
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(357, 59);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Python Tools";
            // 
            // btnPIP
            // 
            this.btnPIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPIP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPIP.Location = new System.Drawing.Point(179, 17);
            this.btnPIP.Name = "btnPIP";
            this.btnPIP.Size = new System.Drawing.Size(157, 33);
            this.btnPIP.TabIndex = 32;
            this.btnPIP.Text = "Install Dependencies";
            this.btnPIP.UseVisualStyleBackColor = false;
            this.btnPIP.Click += new System.EventHandler(this.btnPIP_Click);
            // 
            // Ipy
            // 
            this.Ipy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ipy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Ipy.Location = new System.Drawing.Point(21, 18);
            this.Ipy.Name = "Ipy";
            this.Ipy.Size = new System.Drawing.Size(152, 32);
            this.Ipy.TabIndex = 31;
            this.Ipy.Text = "Install Python ";
            this.Ipy.UseVisualStyleBackColor = false;
            this.Ipy.Click += new System.EventHandler(this.Ipy_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // Python_Tool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(386, 89);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Python_Tool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Python Tool";
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPIP;
        private System.Windows.Forms.Button Ipy;
        private System.Windows.Forms.Timer timer1;
    }
}