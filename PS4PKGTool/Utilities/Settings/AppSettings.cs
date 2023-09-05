using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS4PKGTool.Utilities.Settings
{
    public class AppSettings
    {
        public string SavedFbdLastDirectory { get; set; }
        public List<string> PkgDirectories { get; set; }
        public bool ScanRecursive { get; set; }
        public bool PlayBgm { get; set; }
        public bool ShowDirectorySettingsAtStartup { get; set; }
        public bool AutoSortRow { get; set; }
        public string LocalServerIp { get; set; } = string.Empty;
        public string Ps4Ip { get; set; } = string.Empty;
        public string OfficialUpdateDownloadDirectory { get; set; } = string.Empty;
        public bool NodeJsInstalled { get; set; }
        public bool HttpServerInstalled { get; set; }
        public bool PkgColorLabel { get; set; }
        public Color AddonPkgForeColor { get; set; }
        public Color GamePkgForeColor { get; set; }
        public Color PatchPkgForeColor { get; set; }
        public Color AppPkgForeColor { get; set; }
        public Color AddonPkgBackColor { get; set; }
        public Color GamePkgBackColor { get; set; }
        public Color PatchPkgBackColor { get; set; }
        public Color AppPkgBackColor { get; set; }
        public string RenameCustomName { get; set; }
        public DateTime Ps5BcJsonLastDownloadDate { get; set; }
        public bool psvr_neo_ps5bc_check { get; set; }


        #region columnVisibility

        public bool pkgtitleIdColumn { get; set; }
        public bool pkgcontentIdColumn { get; set; }
        public bool pkgregionColumn { get; set; }
        public bool pkgminimumFirmwareColumn { get; set; }
        public bool pkgversionColumn { get; set; }
        public bool pkgTypeColumn { get; set; }
        public bool pkgcategoryColumn { get; set; }
        public bool pkgsizeColumn { get; set; }
        public bool pkgDirectoryColumn { get; set; }
        public bool pkgBackportColumn { get; set; }


        #endregion columnVisibility

        public AppSettings()
        {
            PkgDirectories = new List<string>();
        }
    }
}
