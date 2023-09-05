using PS4PKGTool.Util;
using PS4PKGTool.Utilities.PS4PKGToolHelper;
using PS4PKGTool.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PS4PKGTool.Utilities.PS4PKGToolHelper.Helper;
//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;

namespace PS4PKGTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            EnsureSettingsFileExists();

            appSettings_ = LoadSettings(SettingFilePath);

            ChooseStartupForm();
        }

        private static void EnsureSettingsFileExists()
        {
            if (!Directory.Exists(Helper.PS4PKGToolTempDirectory))
            {
                Directory.CreateDirectory(Helper.PS4PKGToolTempDirectory);
                Logger.LogInformation("Creating PS4PKGToolTemp directory...");
            }

            if (!File.Exists(SettingFilePath) || new FileInfo(SettingFilePath).Length == 0)
            {
                CreateDefaultSettings();
            }
        }

        private static void CreateDefaultSettings()
        {
            string defaultSettings =
    @"pkg_directories=
scan_recursive=False
play_bgm=False
show_directory_settings_at_startup=True
auto_sort_row=False
local_server_ip=
ps4_ip=
nodeJs_installed=
httpServer_installed=
official_update_download_directory=
pkg_color_label=False
game_pkg_forecolor=-2302756
patch_pkg_forecolor=-2302756
addon_pkg_forecolor=-2302756
app_pkg_forecolor=-2302756
game_pkg_backcolor=-12828863
patch_pkg_backcolor=-12828863
addon_pkg_backcolor=-12828863
app_pkg_backcolor=-12828863
rename_custom_format=
ps5bc_json_download_date=
psvr_neo_ps5bc_check=
pkg_titleId_column=True
pkg_contentId_column=True
pkg_region_column=True
pkg_minimum_firmware_column=True
pkg_version_column=True
pkg_type_column=True
pkg_category_column=True
pkg_size_column=True
pkg_location_column=True
pkg_backport_column=True";
            File.WriteAllText(SettingFilePath, defaultSettings);
        }

        private static void ChooseStartupForm()
        {
            Form startupForm = !appSettings_.ShowDirectorySettingsAtStartup ? new Main() : new PKG_Directory_Settings();
            Application.Run(startupForm);
        }
    }
}
