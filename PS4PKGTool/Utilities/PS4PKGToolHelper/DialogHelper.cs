using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4PKGTool.Utilities.PS4PKGToolHelper
{
    public class DialogHelper
    {
        public static bool ShowFolderBrowserDialog(out FolderBrowserDialog fbd)
        {
            fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.ShowPinnedPlaces = true;
            return fbd.ShowDialog() == DialogResult.OK;
        }

        public static bool ShowSaveFileDialog(string title, string filter, out SaveFileDialog sfd)
        {
            sfd = new SaveFileDialog();
            sfd.Title = title;
            sfd.Filter = filter;
            return sfd.ShowDialog() == DialogResult.OK;
        }
    }
}
