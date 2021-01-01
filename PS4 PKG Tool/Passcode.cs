using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4_PKG_Tool
{
    public partial class Passcode : DarkUI.Forms.DarkForm
    {
        public Passcode()
        {
            InitializeComponent();
        }

        private void Passcode_Load(object sender, EventArgs e)
        {
            PS4PKGTOOL.Passcode.passcode = null;
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            if( darkTextBox1.Text.Length != 32 && darkTextBox1.Text.Length != 0)
            {
                DarkMessageBox.ShowError("Passcode is not valid.", "PS4 PKG TOOL");
                return;
            }

            if (darkTextBox1.Text.Length == 0)
            {
                PS4PKGTOOL.Passcode.passcode = null;
            }

            if (darkTextBox1.Text.Length == 32)
            {
                PS4PKGTOOL.Passcode.passcode = darkTextBox1.Text;
            }

            this.Hide();
        }
    }
}
