using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4PKGTool.Utilities.PS4PKGToolHelper
{
    public class MessageBoxHelper
    {
        public static DialogResult ShowInformation(string message, bool logging)
        {
            if (logging)
                Logger.LogInformation(message);
            return DarkMessageBox.ShowInformation(message, "PS4 PKG Tool");
        }

        public static DialogResult ShowError(string message, bool logging)
        {
            if (logging)
                Logger.LogError(message);
            return DarkMessageBox.ShowError(message, "PS4 PKG Tool");
        }

        public static DialogResult ShowWarning(string message, bool logging)
        {
            if (logging)
                Logger.LogWarning(message);
            return DarkMessageBox.ShowWarning(message, "PS4 PKG Tool");
        }

        public static DialogResult DialogResultYesNo(string message)
        {
            return DarkMessageBox.DialogYesNo(message, "PS4 PKG Tool");
        }

        public static DialogResult DialogResultYesNoCancel(string message)
        {
            return DarkMessageBox.DialogYesNoCancel(message, "PS4 PKG Tool");
        }
    }
}
