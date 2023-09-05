using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS4PKGTool.Utilities.Settings
{
    public static class SettingsManager
    {
        public static AppSettings appSettings_ = new AppSettings();
        public static string SettingFilePath = Path.Combine(PS4PKGToolHelper.Helper.PS4PKGToolTempDirectory, @"Settings.conf");
        public static void SaveSettings(AppSettings settings, string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    File.Create(filePath);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"saved_fbd_last_directory={settings.SavedFbdLastDirectory}");
                    string directories = string.Join(",", settings.PkgDirectories);
                    writer.WriteLine($"pkg_directories={directories}");
                    writer.WriteLine($"scan_recursive={settings.ScanRecursive}");
                    writer.WriteLine($"play_bgm={settings.PlayBgm}");
                    writer.WriteLine($"show_directory_settings_at_startup={settings.ShowDirectorySettingsAtStartup}");
                    writer.WriteLine($"auto_sort_row={settings.AutoSortRow}");
                    writer.WriteLine($"local_server_ip={settings.LocalServerIp}");
                    writer.WriteLine($"ps4_ip={settings.Ps4Ip}");
                    writer.WriteLine($"nodeJs_installed={settings.NodeJsInstalled}");
                    writer.WriteLine($"httpServer_installed={settings.HttpServerInstalled}");
                    writer.WriteLine($"official_update_download_directory={settings.OfficialUpdateDownloadDirectory}");
                    writer.WriteLine($"pkg_color_label={settings.PkgColorLabel}");
                    writer.WriteLine($"game_pkg_forecolor={settings.GamePkgForeColor.ToArgb()}");
                    writer.WriteLine($"patch_pkg_forecolor={settings.PatchPkgForeColor.ToArgb()}");
                    writer.WriteLine($"addon_pkg_forecolor={settings.AddonPkgForeColor.ToArgb()}");
                    writer.WriteLine($"app_pkg_forecolor={settings.AppPkgForeColor.ToArgb()}");
                    writer.WriteLine($"game_pkg_backcolor={settings.GamePkgBackColor.ToArgb()}");
                    writer.WriteLine($"patch_pkg_backcolor={settings.PatchPkgBackColor.ToArgb()}");
                    writer.WriteLine($"addon_pkg_backcolor={settings.AddonPkgBackColor.ToArgb()}");
                    writer.WriteLine($"app_pkg_backcolor={settings.AppPkgBackColor.ToArgb()}");
                    writer.WriteLine($"rename_custom_format={settings.RenameCustomName}");
                    string formattedDate = settings.Ps5BcJsonLastDownloadDate.ToString("d MMMM yyyy", CultureInfo.InvariantCulture);
                    writer.WriteLine($"ps5bc_json_download_date={formattedDate}");
                    writer.WriteLine($"psvr_neo_ps5bc_check={settings.psvr_neo_ps5bc_check}");

                    writer.WriteLine($"pkg_titleId_column={settings.pkgtitleIdColumn}");
                    writer.WriteLine($"pkg_contentId_column={settings.pkgcontentIdColumn}");
                    writer.WriteLine($"pkg_region_column={settings.pkgregionColumn}");
                    writer.WriteLine($"pkg_minimum_firmware_column={settings.pkgminimumFirmwareColumn}");
                    writer.WriteLine($"pkg_version_column={settings.pkgversionColumn}");
                    writer.WriteLine($"pkg_type_column={settings.pkgTypeColumn}");
                    writer.WriteLine($"pkg_category_column={settings.pkgcategoryColumn}");
                    writer.WriteLine($"pkg_size_column={settings.pkgsizeColumn}");
                    writer.WriteLine($"pkg_location_column={settings.pkgDirectoryColumn}");
                    writer.WriteLine($"pkg_backport_column={settings.pkgBackportColumn}");

                }
            }
            catch (Exception ex)
            {
                ShowError("Error saving settings: " + ex.Message, true);
            }
        }

        public static AppSettings LoadSettings(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("saved_fbd_last_directory="))
                            {
                                appSettings_.SavedFbdLastDirectory = line.Substring("saved_last_directory=".Length);
                            }
                            if (line.StartsWith("pkg_directories="))
                            {
                                string directories = line.Substring("pkg_directories=".Length);
                                appSettings_.PkgDirectories.AddRange(directories.Split(','));
                            }
                            else if (line.StartsWith("scan_recursive="))
                            {
                                bool.TryParse(line.Substring("scan_recursive=".Length), out bool scan_recursive);
                                appSettings_.ScanRecursive = scan_recursive;
                            }
                            else if (line.StartsWith("play_bgm="))
                            {
                                bool.TryParse(line.Substring("play_bgm=".Length), out bool PlayBgm);
                                appSettings_.PlayBgm = PlayBgm;
                            }
                            else if (line.StartsWith("show_directory_settings_at_startup="))
                            {
                                bool.TryParse(line.Substring("show_directory_settings_at_startup=".Length), out bool show_directory_settings_at_startup);
                                appSettings_.ShowDirectorySettingsAtStartup = show_directory_settings_at_startup;
                            }
                            else if (line.StartsWith("auto_sort_row"))
                            {
                                bool.TryParse(line.Substring("auto_sort_row=".Length), out bool auto_sort_row);
                                appSettings_.AutoSortRow = auto_sort_row;
                            }
                            else if (line.StartsWith("local_server_ip="))
                            {
                                appSettings_.LocalServerIp = line.Substring("local_server_ip=".Length);
                            }
                            else if (line.StartsWith("ps4_ip="))
                            {
                                appSettings_.Ps4Ip = line.Substring("ps4_ip=".Length);
                            }
                            else if (line.StartsWith("nodeJs_installed="))
                            {
                                bool.TryParse(line.Substring("nodeJs_installed=".Length), out bool nodeJs_installed);
                                appSettings_.NodeJsInstalled = nodeJs_installed;
                            }
                            else if (line.StartsWith("httpServer_installed="))
                            {
                                bool.TryParse(line.Substring("httpServer_installed=".Length), out bool httpServer_installed);
                                appSettings_.HttpServerInstalled = httpServer_installed;
                            }
                            else if (line.StartsWith("official_update_download_directory="))
                            {
                                appSettings_.OfficialUpdateDownloadDirectory = line.Substring("official_update_download_directory=".Length);
                            }
                            else if (line.StartsWith("pkg_color_label="))
                            {
                                bool.TryParse(line.Substring("pkg_color_label=".Length), out bool pkg_color_label);
                                appSettings_.PkgColorLabel = pkg_color_label;
                            }
                            else if (line.StartsWith("game_pkg_forecolor="))
                            {
                                ColorConverter converter = new ColorConverter();
                                appSettings_.GamePkgForeColor = (Color)converter.ConvertFromString(line.Substring("game_pkg_forecolor=".Length));
                            }
                            else if (line.StartsWith("patch_pkg_forecolor="))
                            {
                                ColorConverter converter = new ColorConverter();
                                appSettings_.PatchPkgForeColor = (Color)converter.ConvertFromString(line.Substring("patch_pkg_forecolor=".Length));
                            }
                            else if (line.StartsWith("addon_pkg_forecolor="))
                            {
                                ColorConverter converter = new ColorConverter();
                                appSettings_.AddonPkgForeColor = (Color)converter.ConvertFromString(line.Substring("addon_pkg_forecolor=".Length));
                            }
                            else if (line.StartsWith("app_pkg_forecolor="))
                            {
                                ColorConverter converter = new ColorConverter();
                                appSettings_.AppPkgForeColor = (Color)converter.ConvertFromString(line.Substring("app_pkg_forecolor=".Length));
                            }
                            else if (line.StartsWith("game_pkg_backcolor="))
                            {
                                ColorConverter converter = new ColorConverter();
                                appSettings_.GamePkgBackColor = (Color)converter.ConvertFromString(line.Substring("game_pkg_backcolor=".Length));
                            }
                            else if (line.StartsWith("patch_pkg_backcolor="))
                            {
                                ColorConverter converter = new ColorConverter();
                                appSettings_.PatchPkgBackColor = (Color)converter.ConvertFromString(line.Substring("patch_pkg_backcolor=".Length));
                            }
                            else if (line.StartsWith("addon_pkg_backcolor="))
                            {
                                ColorConverter converter = new ColorConverter();
                                appSettings_.AddonPkgBackColor = (Color)converter.ConvertFromString(line.Substring("addon_pkg_backcolor=".Length));
                            }
                            else if (line.StartsWith("app_pkg_backcolor="))
                            {
                                ColorConverter converter = new ColorConverter();
                                appSettings_.AppPkgBackColor = (Color)converter.ConvertFromString(line.Substring("app_pkg_backcolor=".Length));
                            }
                            else if (line.StartsWith("rename_custom_format="))
                            {
                                appSettings_.RenameCustomName = line.Substring("rename_custom_format=".Length);
                            }
                            else if (line.StartsWith("ps5bc_json_download_date="))
                            {
                                string[] formats = { "d MMMM yyyy" }; // Example: "3 June 2023", "03 June 2023"
                                string dateString = line.Substring("ps5bc_json_download_date=".Length);

                                if (!string.IsNullOrEmpty(dateString))
                                {
                                    DateTime convertedDate;
                                    if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out convertedDate))
                                    {
                                        appSettings_.Ps5BcJsonLastDownloadDate = convertedDate;
                                    }
                                }
                            }
                            else if (line.StartsWith("psvr_neo_ps5bc_check="))
                            {
                                bool.TryParse(line.Substring("psvr_neo_ps5bc_check=".Length), out bool psvr_neo_ps5bc_check);
                                appSettings_.psvr_neo_ps5bc_check = psvr_neo_ps5bc_check;
                            }
                            else if (line.StartsWith("pkg_titleId_column="))
                            {
                                bool.TryParse(line.Substring("pkg_titleId_column=".Length), out bool pkg_titleId_column);
                                appSettings_.pkgtitleIdColumn = pkg_titleId_column;
                            }
                            else if (line.StartsWith("pkg_contentId_column="))
                            {
                                bool.TryParse(line.Substring("pkg_contentId_column=".Length), out bool pkg_contentId_column);
                                appSettings_.pkgcontentIdColumn = pkg_contentId_column;
                            }
                            else if (line.StartsWith("pkg_region_column="))
                            {
                                bool.TryParse(line.Substring("pkg_region_column=".Length), out bool pkg_region_column);
                                appSettings_.pkgregionColumn = pkg_region_column;
                            }
                            else if (line.StartsWith("pkg_minimum_firmware_column="))
                            {
                                bool.TryParse(line.Substring("pkg_minimum_firmware_column=".Length), out bool pkg_minimum_firmware_column);
                                appSettings_.pkgminimumFirmwareColumn = pkg_minimum_firmware_column;
                            }
                            else if (line.StartsWith("pkg_version_column="))
                            {
                                bool.TryParse(line.Substring("pkg_version_column=".Length), out bool pkg_version_column);
                                appSettings_.pkgversionColumn = pkg_version_column;
                            }
                            else if (line.StartsWith("pkg_type_column="))
                            {
                                bool.TryParse(line.Substring("pkg_type_column=".Length), out bool pkg_type_column);
                                appSettings_.pkgTypeColumn = pkg_type_column;
                            }
                            else if (line.StartsWith("pkg_category_column="))
                            {
                                bool.TryParse(line.Substring("pkg_category_column=".Length), out bool pkg_category_column);
                                appSettings_.pkgcategoryColumn = pkg_category_column;
                            }
                            else if (line.StartsWith("pkg_size_column="))
                            {
                                bool.TryParse(line.Substring("pkg_size_column=".Length), out bool pkg_size_column);
                                appSettings_.pkgsizeColumn = pkg_size_column;
                            }
                            else if (line.StartsWith("pkg_location_column="))
                            {
                                bool.TryParse(line.Substring("pkg_location_column=".Length), out bool pkg_location_column);
                                appSettings_.pkgDirectoryColumn = pkg_location_column;
                            }
                            else if (line.StartsWith("pkg_backport_column="))
                            {
                                bool.TryParse(line.Substring("pkg_backport_column=".Length), out bool pkg_backport_column);
                                appSettings_.pkgBackportColumn = pkg_backport_column;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                ShowError("Error loading settings: " + ex.Message, true);
            }
            return appSettings_;
        }
    }
}
