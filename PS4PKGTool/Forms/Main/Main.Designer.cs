using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PS4PKGTool
{
    partial class Main
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
            components=new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            label1=new DarkUI.Controls.DarkLabel();
            label2=new DarkUI.Controls.DarkLabel();
            fileToolStripMenuItem=new ToolStripMenuItem();
            openGameFolderToolStripMenuItem=new ToolStripMenuItem();
            refreshContentToolStripMenuItem=new ToolStripMenuItem();
            exitToolStripMenuItem=new ToolStripMenuItem();
            toolToolStripMenuItem=new ToolStripMenuItem();
            gameToolStripMenuItem=new ToolStripMenuItem();
            renamePKGToTITLEToolStripMenuItem=new ToolStripMenuItem();
            renameToTITLEIDToolStripMenuItem=new ToolStripMenuItem();
            renameToCONTENTIDToolStripMenuItem=new ToolStripMenuItem();
            renamePKGToTITLETITLEIDToolStripMenuItem=new ToolStripMenuItem();
            renamePKGToTITLEToolStripMenuItem1=new ToolStripMenuItem();
            renamePKGToTITLECATEGORYToolStripMenuItem=new ToolStripMenuItem();
            viewTrophyListToolStripMenuItem=new ToolStripMenuItem();
            exportPKGListToExcelToolStripMenuItem=new ToolStripMenuItem();
            helpToolStripMenuItem=new ToolStripMenuItem();
            donateToolStripMenuItem=new ToolStripMenuItem();
            aboutToolStripMenuItem=new ToolStripMenuItem();
            contextMenuPKGGridView=new DarkUI.Controls.DarkContextMenu();
            openPS4PKGToolTempDirectoryToolStripMenuItem2=new ToolStripMenuItem();
            toolStripMenuItem96=new ToolStripMenuItem();
            toolStripMenuItem34=new ToolStripMenuItem();
            toolStripSeparator1=new ToolStripSeparator();
            toolStripMenuItem94=new ToolStripMenuItem();
            checkForDuplicatePKGToolStripMenuItem2=new ToolStripMenuItem();
            globalExportPKGListToExcelToolStripMenuItem2=new ToolStripMenuItem();
            toolStripMenuItem3=new ToolStripMenuItem();
            globalExtractImagesAndIconToolStripMenuItem2=new ToolStripMenuItem();
            globalExtractImageOnlyToolStripMenuItem2=new ToolStripMenuItem();
            globalExtractIconOnlyToolStripMenuItem2=new ToolStripMenuItem();
            toolStripMenuItem111=new ToolStripMenuItem();
            renameAllPkg1ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg2ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg3ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg4ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg5ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg6ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg7ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg8ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg9ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg10ToolStripMenuItem2=new ToolStripMenuItem();
            renameAllPkg11ToolStripMenuItem2=new ToolStripMenuItem();
            toolStripMenuItem38=new ToolStripMenuItem();
            movePkgTitleToolStripMenuItem2=new ToolStripMenuItem();
            movePkgTypeToolStripMenuItem2=new ToolStripMenuItem();
            movePkgCategoryToolStripMenuItem2=new ToolStripMenuItem();
            movePkgRegionToolStripMenuItem2=new ToolStripMenuItem();
            toolStripSeparator2=new ToolStripSeparator();
            GroupActionTitleStripMenuItem=new ToolStripMenuItem();
            toolStripMenuItem127=new ToolStripMenuItem();
            copyTitleIdtoolStripMenuItem2=new ToolStripMenuItem();
            copyContentIdtoolStripMenuItem2=new ToolStripMenuItem();
            backportToolStripMenuItem=new ToolStripMenuItem();
            setBackportedToolStripMenuItem2=new ToolStripMenuItem();
            setBackportRemarksToolStripMenuItem=new ToolStripMenuItem();
            backportRemarkTextboxtoolStripTextBox2=new ToolStripTextBox();
            setRemarktoolStripMenuItem2=new ToolStripMenuItem();
            toolStripSeparator9=new ToolStripSeparator();
            removeBackportedToolStripMenuItem2=new ToolStripMenuItem();
            deletePkgtoolStripMenuItem2=new ToolStripMenuItem();
            selectedExportPKGListToExcelToolStripMenuItem2=new ToolStripMenuItem();
            GroupActionExtacrtImageToolStripMenuItem=new ToolStripMenuItem();
            selectedExtractImagesAndIconToolStripMenuItem2=new ToolStripMenuItem();
            selectedExtractImageOnlyToolStripMenuItem2=new ToolStripMenuItem();
            selectedExtractIconOnlyToolStripMenuItem2=new ToolStripMenuItem();
            toolStripMenuItem133=new ToolStripMenuItem();
            renameSelectedPkg1ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg2ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg3ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg4ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg5ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg6ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg7ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg8ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg9ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg10ToolStripMenuItem2=new ToolStripMenuItem();
            renameSelectedPkg11ToolStripMenuItem2=new ToolStripMenuItem();
            viewPkgChangeInfotoolStripMenuItem2=new ToolStripMenuItem();
            viewPkgExplorerStripMenuItem2=new ToolStripMenuItem();
            toolStripSeparator7=new ToolStripSeparator();
            toolStripMenuItem18=new ToolStripMenuItem();
            RpiCheckPkgInstalledtoolStripMenuItem2=new ToolStripMenuItem();
            RpiSendPkgtoolStripMenuItem2=new ToolStripMenuItem();
            toolStripMenuItem21=new ToolStripMenuItem();
            RpiUninstallBasePKGToolStripMenuItem2=new ToolStripMenuItem();
            RpiUninstallPatchPKGToolStripMenuItem2=new ToolStripMenuItem();
            RpiUninstallDlcPKGToolStripMenuItem2=new ToolStripMenuItem();
            RpiUninstallThemePKGToolStripMenuItem2=new ToolStripMenuItem();
            toolStripSeparator5=new ToolStripSeparator();
            toolStripSeparator6=new ToolStripSeparator();
            toolStripSeparator3=new ToolStripSeparator();
            label8=new DarkUI.Controls.DarkLabel();
            darkMenuStrip1=new DarkUI.Controls.DarkMenuStrip();
            fileToolStripMenuItem2=new ToolStripMenuItem();
            managePS4PKGToolStripMenuItem=new ToolStripMenuItem();
            toolStripSeparator11=new ToolStripSeparator();
            exitToolStripMenuItem1=new ToolStripMenuItem();
            toolToolStripMenuItem1=new ToolStripMenuItem();
            openPS4PKGToolTempDirectoryToolStripMenuItem1=new ToolStripMenuItem();
            reloadContentToolStripMenuItem=new ToolStripMenuItem();
            settingstoolStripMenuItem=new ToolStripMenuItem();
            toolStripSeparator12=new ToolStripSeparator();
            globalActionToolStripMenuItem=new ToolStripMenuItem();
            checkForDuplicatePKGToolStripMenuItem1=new ToolStripMenuItem();
            globalExportPKGListToExcelToolStripMenuItem1=new ToolStripMenuItem();
            extractImageAndBackgroundToolStripMenuItem=new ToolStripMenuItem();
            globalExtractImagesAndIconToolStripMenuItem1=new ToolStripMenuItem();
            globalExtractImageOnlyToolStripMenuItem1=new ToolStripMenuItem();
            globalExtractIconOnlyToolStripMenuItem1=new ToolStripMenuItem();
            renameToolStripMenuItem=new ToolStripMenuItem();
            renameAllPkg1ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg2ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg3ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg4ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg5ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg6ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg7ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg8ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg9ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg10ToolStripMenuItem1=new ToolStripMenuItem();
            renameAllPkg11ToolStripMenuItem1=new ToolStripMenuItem();
            seperateAndMovePKGByTypeIntoFolderToolStripMenuItem=new ToolStripMenuItem();
            movePkgTitleToolStripMenuItem1=new ToolStripMenuItem();
            movePkgCategoryToolStripMenuItem1=new ToolStripMenuItem();
            movePkgTypeToolStripMenuItem1=new ToolStripMenuItem();
            movePkgRegionToolStripMenuItem1=new ToolStripMenuItem();
            toolStripSeparator4=new ToolStripSeparator();
            toolStripMenuItem2=new ToolStripMenuItem();
            globalCopyStripMenuItem=new ToolStripMenuItem();
            copyTitleIdtoolStripMenuItem1=new ToolStripMenuItem();
            copyContentIdtoolStripMenuItem1=new ToolStripMenuItem();
            toolStripMenuItem1=new ToolStripMenuItem();
            setBackportedtoolStripMenuItem1=new ToolStripMenuItem();
            toolStripMenuItem4=new ToolStripMenuItem();
            backportRemarkTextboxtoolStripTextBox1=new ToolStripTextBox();
            setRemarktoolStripMenuItem1=new ToolStripMenuItem();
            toolStripSeparator10=new ToolStripSeparator();
            removeBackportedtoolStripMenuItem1=new ToolStripMenuItem();
            deletePKGtoolStripMenuItem1=new ToolStripMenuItem();
            selectedExportPKGListToExcelToolStripMenuItem1=new ToolStripMenuItem();
            toolStripMenuItem28=new ToolStripMenuItem();
            selectedExtractImagesAndIconToolStripMenuItem1=new ToolStripMenuItem();
            selectedExtractImageOnlyToolStripMenuItem1=new ToolStripMenuItem();
            selectedExtractIconOnlyToolStripMenuItem1=new ToolStripMenuItem();
            renameCurrentPKGStripMenuItem=new ToolStripMenuItem();
            renameSelectedPkg1ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg2ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg3ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg4ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg5ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg6ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg7ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg8ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg9ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg10ToolStripMenuItem1=new ToolStripMenuItem();
            renameSelectedPkg11ToolStripMenuItem1=new ToolStripMenuItem();
            viewPkgChangeInfotoolStripMenuItem1=new ToolStripMenuItem();
            viewPkgExplorerStripMenuItem1=new ToolStripMenuItem();
            toolStripSeparator8=new ToolStripSeparator();
            toolStripMenuItem16=new ToolStripMenuItem();
            RpiCheckPkgInstalledtoolStripMenuItem1=new ToolStripMenuItem();
            RpiSendPkgtoolStripMenuItem1=new ToolStripMenuItem();
            uninstallPKGFromPS4ToolStripMenuItem=new ToolStripMenuItem();
            RpiUninstallBasePKGToolStripMenuItem1=new ToolStripMenuItem();
            RpiUninstallPatchPKGToolStripMenuItem1=new ToolStripMenuItem();
            RpiUninstallDlcPKGToolStripMenuItem1=new ToolStripMenuItem();
            RpiUninstallThemePKGToolStripMenuItem1=new ToolStripMenuItem();
            toolStripMenuItem144=new ToolStripMenuItem();
            toolStripMenuItem159=new ToolStripMenuItem();
            toolStripMenuItem160=new ToolStripMenuItem();
            toolStripMenuItem158=new ToolStripMenuItem();
            testToolStripMenuItem=new ToolStripMenuItem();
            colorTestToolStripMenuItem=new ToolStripMenuItem();
            darkStatusStrip1=new DarkUI.Controls.DarkStatusStrip();
            toolStripStatusLabel1=new ToolStripStatusLabel();
            toolStripStatusLabel3=new ToolStripStatusLabel();
            toolStripStatusLabel5=new ToolStripStatusLabel();
            toolStripProgressBar1=new ToolStripProgressBar();
            toolStripStatusLabel2=new ToolStripStatusLabel();
            labelDisplayTotalPKG=new ToolStripStatusLabel();
            ToolStripSplitButtonTotalPKG=new ToolStripSplitButton();
            toolStripSplitButton1=new ToolStripSplitButton();
            toolStripStatusLabel4=new ToolStripStatusLabel();
            contextMenuTrophy=new DarkUI.Controls.DarkContextMenu();
            ExtractTrophyImageToolStripMenuItem=new ToolStripMenuItem();
            contextMenuEntry=new DarkUI.Controls.DarkContextMenu();
            ExtractAllEntryToolStripMenuItem=new ToolStripMenuItem();
            ExtractDecryptedEntryToolStripMenuItem=new ToolStripMenuItem();
            imageList1=new ImageList(components);
            contextMenuOfficialUpdate=new DarkUI.Controls.DarkContextMenu();
            copyURLToolStripMenuItem=new ToolStripMenuItem();
            downloadSelectedPKGUpdateToolStripMenuItem=new ToolStripMenuItem();
            contextMenuBackgroundImage=new DarkUI.Controls.DarkContextMenu();
            saveImageToolStripMenuItem=new ToolStripMenuItem();
            SetImageAsDesktopBackgroundToolStripMenuItem=new ToolStripMenuItem();
            contextMenuExtractNode=new DarkUI.Controls.DarkContextMenu();
            extractNodeToolStripMenuItem=new ToolStripMenuItem();
            expandAllToolStripMenuItem=new ToolStripMenuItem();
            collapseAllNodeToolStripMenuItem=new ToolStripMenuItem();
            columnHeader1=new ColumnHeader();
            columnHeader5=new ColumnHeader();
            columnHeader6=new ColumnHeader();
            darkSectionPanel7=new DarkUI.Controls.DarkSectionPanel();
            contextMenuExtractListView=new DarkUI.Controls.DarkContextMenu();
            toolStripMenuItem32=new ToolStripMenuItem();
            flatTabControl1=new VisualStudioTabControl.VisualStudioTabControl();
            tabPage1=new TabPage();
            darkButton3=new DarkUI.Controls.DarkButton();
            darkSectionPanel10=new DarkUI.Controls.DarkSectionPanel();
            darkDataGridView2=new DarkUI.Controls.DarkDataGridView();
            darkSectionPanel9=new DarkUI.Controls.DarkSectionPanel();
            panel1=new Panel();
            darkLabel1=new DarkUI.Controls.DarkLabel();
            PKGGridView=new DarkUI.Controls.DarkDataGridView();
            darkSectionPanel8=new DarkUI.Controls.DarkSectionPanel();
            panel5=new Panel();
            pictureBox1=new PictureBox();
            label3=new DarkUI.Controls.DarkLabel();
            darkLabel2=new DarkUI.Controls.DarkLabel();
            tbSearchGame=new DarkUI.Controls.DarkTextBox();
            tabPage6=new TabPage();
            treeView1=new System.Windows.Forms.TreeView();
            listView2=new DarkUI.Controls.DarkTreeView();
            PKGListView=new System.Windows.Forms.ListView();
            tabPage2=new TabPage();
            TrophyGridView=new DarkUI.Controls.DarkDataGridView();
            tabPage3=new TabPage();
            flatTabControlBgi=new VisualStudioTabControl.VisualStudioTabControl();
            tabPagePic0=new TabPage();
            darkLabel3=new DarkUI.Controls.DarkLabel();
            pbPIC0=new PictureBox();
            tabPagePic1=new TabPage();
            darkLabel4=new DarkUI.Controls.DarkLabel();
            pbPIC1=new PictureBox();
            tabPage4=new TabPage();
            panel7=new Panel();
            darkSectionPanel2=new DarkUI.Controls.DarkSectionPanel();
            dgvEntryList=new DarkUI.Controls.DarkDataGridView();
            darkSectionPanel3=new DarkUI.Controls.DarkSectionPanel();
            darkDataGridView4=new DarkUI.Controls.DarkDataGridView();
            darkSectionPanel1=new DarkUI.Controls.DarkSectionPanel();
            dgvHeader=new DarkUI.Controls.DarkDataGridView();
            tabPage7=new TabPage();
            panel6=new Panel();
            btnExtractFullPKG=new DarkUI.Controls.DarkButton();
            darkLabel5=new DarkUI.Controls.DarkLabel();
            tbPasscode=new DarkUI.Controls.DarkTextBox();
            btnViewPKGData=new DarkUI.Controls.DarkButton();
            darkSectionPanel4=new DarkUI.Controls.DarkSectionPanel();
            splitContainer1=new SplitContainer();
            PKGTreeView=new System.Windows.Forms.TreeView();
            listView1=new System.Windows.Forms.ListView();
            columnHeader7=new ColumnHeader();
            columnHeader8=new ColumnHeader();
            columnHeader9=new ColumnHeader();
            btnSearchFileInTreeView=new DarkUI.Controls.DarkButton();
            tbSearchTreeView=new DarkUI.Controls.DarkTextBox();
            darkLabel6=new DarkUI.Controls.DarkLabel();
            tabPage5=new TabPage();
            panel8=new Panel();
            darkSectionPanel11=new DarkUI.Controls.DarkSectionPanel();
            dgvUpdate=new DarkUI.Controls.DarkDataGridView();
            darkSectionPanel12=new DarkUI.Controls.DarkSectionPanel();
            label13=new DarkUI.Controls.DarkLabel();
            label14=new DarkUI.Controls.DarkLabel();
            label15=new DarkUI.Controls.DarkLabel();
            label16=new DarkUI.Controls.DarkLabel();
            label17=new DarkUI.Controls.DarkLabel();
            label18=new DarkUI.Controls.DarkLabel();
            label19=new DarkUI.Controls.DarkLabel();
            labelRemaster=new DarkUI.Controls.DarkLabel();
            label21=new DarkUI.Controls.DarkLabel();
            labelUpdateType=new DarkUI.Controls.DarkLabel();
            labelMandatory=new DarkUI.Controls.DarkLabel();
            labelPKGdigest=new DarkUI.Controls.DarkLabel();
            label5=new DarkUI.Controls.DarkLabel();
            label12=new DarkUI.Controls.DarkLabel();
            label9=new DarkUI.Controls.DarkLabel();
            label11=new DarkUI.Controls.DarkLabel();
            label4=new DarkUI.Controls.DarkLabel();
            label10=new DarkUI.Controls.DarkLabel();
            label7=new DarkUI.Controls.DarkLabel();
            labelSystemReq=new DarkUI.Controls.DarkLabel();
            label6=new DarkUI.Controls.DarkLabel();
            labelUpdateVersion=new DarkUI.Controls.DarkLabel();
            labelTotalSize=new DarkUI.Controls.DarkLabel();
            labelTotalFile=new DarkUI.Controls.DarkLabel();
            contextMenuPKGGridView.SuspendLayout();
            darkMenuStrip1.SuspendLayout();
            darkStatusStrip1.SuspendLayout();
            contextMenuTrophy.SuspendLayout();
            contextMenuEntry.SuspendLayout();
            contextMenuOfficialUpdate.SuspendLayout();
            contextMenuBackgroundImage.SuspendLayout();
            contextMenuExtractNode.SuspendLayout();
            contextMenuExtractListView.SuspendLayout();
            flatTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            darkSectionPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)darkDataGridView2).BeginInit();
            darkSectionPanel9.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PKGGridView).BeginInit();
            darkSectionPanel8.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage6.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrophyGridView).BeginInit();
            tabPage3.SuspendLayout();
            flatTabControlBgi.SuspendLayout();
            tabPagePic0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPIC0).BeginInit();
            tabPagePic1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPIC1).BeginInit();
            tabPage4.SuspendLayout();
            panel7.SuspendLayout();
            darkSectionPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEntryList).BeginInit();
            darkSectionPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)darkDataGridView4).BeginInit();
            darkSectionPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).BeginInit();
            tabPage7.SuspendLayout();
            panel6.SuspendLayout();
            darkSectionPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabPage5.SuspendLayout();
            panel8.SuspendLayout();
            darkSectionPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUpdate).BeginInit();
            darkSectionPanel12.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            label1.Location=new System.Drawing.Point(0, 0);
            label1.Name="label1";
            label1.Size=new System.Drawing.Size(100, 23);
            label1.TabIndex=0;
            // 
            // label2
            // 
            label2.AutoSize=true;
            label2.Font=new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold|System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor=System.Drawing.Color.Silver;
            label2.Location=new System.Drawing.Point(154, 471);
            label2.Name="label2";
            label2.Size=new System.Drawing.Size(0, 14);
            label2.TabIndex=39;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openGameFolderToolStripMenuItem, refreshContentToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name="fileToolStripMenuItem";
            fileToolStripMenuItem.Size=new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text="File";
            // 
            // openGameFolderToolStripMenuItem
            // 
            openGameFolderToolStripMenuItem.Name="openGameFolderToolStripMenuItem";
            openGameFolderToolStripMenuItem.Size=new System.Drawing.Size(67, 22);
            // 
            // refreshContentToolStripMenuItem
            // 
            refreshContentToolStripMenuItem.Name="refreshContentToolStripMenuItem";
            refreshContentToolStripMenuItem.Size=new System.Drawing.Size(67, 22);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name="exitToolStripMenuItem";
            exitToolStripMenuItem.Size=new System.Drawing.Size(67, 22);
            // 
            // toolToolStripMenuItem
            // 
            toolToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gameToolStripMenuItem, viewTrophyListToolStripMenuItem, exportPKGListToExcelToolStripMenuItem });
            toolToolStripMenuItem.Name="toolToolStripMenuItem";
            toolToolStripMenuItem.Size=new System.Drawing.Size(41, 20);
            toolToolStripMenuItem.Text="Tool";
            // 
            // gameToolStripMenuItem
            // 
            gameToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { renamePKGToTITLEToolStripMenuItem, renameToTITLEIDToolStripMenuItem, renameToCONTENTIDToolStripMenuItem, renamePKGToTITLETITLEIDToolStripMenuItem, renamePKGToTITLEToolStripMenuItem1, renamePKGToTITLECATEGORYToolStripMenuItem });
            gameToolStripMenuItem.Enabled=false;
            gameToolStripMenuItem.Name="gameToolStripMenuItem";
            gameToolStripMenuItem.Size=new System.Drawing.Size(141, 22);
            gameToolStripMenuItem.Text="Rename PKG";
            // 
            // renamePKGToTITLEToolStripMenuItem
            // 
            renamePKGToTITLEToolStripMenuItem.Name="renamePKGToTITLEToolStripMenuItem";
            renamePKGToTITLEToolStripMenuItem.Size=new System.Drawing.Size(67, 22);
            // 
            // renameToTITLEIDToolStripMenuItem
            // 
            renameToTITLEIDToolStripMenuItem.Name="renameToTITLEIDToolStripMenuItem";
            renameToTITLEIDToolStripMenuItem.Size=new System.Drawing.Size(67, 22);
            // 
            // renameToCONTENTIDToolStripMenuItem
            // 
            renameToCONTENTIDToolStripMenuItem.Name="renameToCONTENTIDToolStripMenuItem";
            renameToCONTENTIDToolStripMenuItem.Size=new System.Drawing.Size(67, 22);
            // 
            // renamePKGToTITLETITLEIDToolStripMenuItem
            // 
            renamePKGToTITLETITLEIDToolStripMenuItem.Name="renamePKGToTITLETITLEIDToolStripMenuItem";
            renamePKGToTITLETITLEIDToolStripMenuItem.Size=new System.Drawing.Size(67, 22);
            // 
            // renamePKGToTITLEToolStripMenuItem1
            // 
            renamePKGToTITLEToolStripMenuItem1.Name="renamePKGToTITLEToolStripMenuItem1";
            renamePKGToTITLEToolStripMenuItem1.Size=new System.Drawing.Size(67, 22);
            // 
            // renamePKGToTITLECATEGORYToolStripMenuItem
            // 
            renamePKGToTITLECATEGORYToolStripMenuItem.Name="renamePKGToTITLECATEGORYToolStripMenuItem";
            renamePKGToTITLECATEGORYToolStripMenuItem.Size=new System.Drawing.Size(67, 22);
            // 
            // viewTrophyListToolStripMenuItem
            // 
            viewTrophyListToolStripMenuItem.Name="viewTrophyListToolStripMenuItem";
            viewTrophyListToolStripMenuItem.Size=new System.Drawing.Size(141, 22);
            // 
            // exportPKGListToExcelToolStripMenuItem
            // 
            exportPKGListToExcelToolStripMenuItem.Name="exportPKGListToExcelToolStripMenuItem";
            exportPKGListToExcelToolStripMenuItem.Size=new System.Drawing.Size(141, 22);
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name="helpToolStripMenuItem";
            helpToolStripMenuItem.Size=new System.Drawing.Size(32, 19);
            // 
            // donateToolStripMenuItem
            // 
            donateToolStripMenuItem.Name="donateToolStripMenuItem";
            donateToolStripMenuItem.Size=new System.Drawing.Size(32, 19);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name="aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size=new System.Drawing.Size(32, 19);
            // 
            // contextMenuPKGGridView
            // 
            contextMenuPKGGridView.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            contextMenuPKGGridView.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            contextMenuPKGGridView.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            contextMenuPKGGridView.Items.AddRange(new ToolStripItem[] { openPS4PKGToolTempDirectoryToolStripMenuItem2, toolStripMenuItem96, toolStripMenuItem34, toolStripSeparator1, toolStripMenuItem94, checkForDuplicatePKGToolStripMenuItem2, globalExportPKGListToExcelToolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem111, toolStripMenuItem38, toolStripSeparator2, GroupActionTitleStripMenuItem, toolStripMenuItem127, backportToolStripMenuItem, deletePkgtoolStripMenuItem2, selectedExportPKGListToExcelToolStripMenuItem2, GroupActionExtacrtImageToolStripMenuItem, toolStripMenuItem133, viewPkgChangeInfotoolStripMenuItem2, viewPkgExplorerStripMenuItem2, toolStripSeparator7, toolStripMenuItem18, RpiCheckPkgInstalledtoolStripMenuItem2, RpiSendPkgtoolStripMenuItem2, toolStripMenuItem21 });
            contextMenuPKGGridView.Name="DarkContextMenuStrip1";
            contextMenuPKGGridView.Size=new System.Drawing.Size(248, 509);
            // 
            // openPS4PKGToolTempDirectoryToolStripMenuItem2
            // 
            openPS4PKGToolTempDirectoryToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            openPS4PKGToolTempDirectoryToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            openPS4PKGToolTempDirectoryToolStripMenuItem2.Name="openPS4PKGToolTempDirectoryToolStripMenuItem2";
            openPS4PKGToolTempDirectoryToolStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            openPS4PKGToolTempDirectoryToolStripMenuItem2.Text="Open PS4PKGToolTemp directory";
            openPS4PKGToolTempDirectoryToolStripMenuItem2.Click+=OpenPS4PKGToolTempDirectory_Click;
            // 
            // toolStripMenuItem96
            // 
            toolStripMenuItem96.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem96.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem96.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem96.Name="toolStripMenuItem96";
            toolStripMenuItem96.ShowShortcutKeys=false;
            toolStripMenuItem96.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem96.Text="Refresh PKG list";
            toolStripMenuItem96.Click+=toolStripMenuItem96_Click;
            // 
            // toolStripMenuItem34
            // 
            toolStripMenuItem34.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem34.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem34.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem34.Name="toolStripMenuItem34";
            toolStripMenuItem34.ShowShortcutKeys=false;
            toolStripMenuItem34.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem34.Text="Settings";
            toolStripMenuItem34.Click+=toolStripMenuItem34_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator1.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator1.Name="toolStripSeparator1";
            toolStripSeparator1.Size=new System.Drawing.Size(244, 6);
            // 
            // toolStripMenuItem94
            // 
            toolStripMenuItem94.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem94.Enabled=false;
            toolStripMenuItem94.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripMenuItem94.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem94.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem94.Name="toolStripMenuItem94";
            toolStripMenuItem94.ShowShortcutKeys=false;
            toolStripMenuItem94.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem94.Text="Global Action";
            // 
            // checkForDuplicatePKGToolStripMenuItem2
            // 
            checkForDuplicatePKGToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            checkForDuplicatePKGToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            checkForDuplicatePKGToolStripMenuItem2.Name="checkForDuplicatePKGToolStripMenuItem2";
            checkForDuplicatePKGToolStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            checkForDuplicatePKGToolStripMenuItem2.Text="Check for duplicate PKG";
            checkForDuplicatePKGToolStripMenuItem2.Click+=CheckForDuplicatePKG_Click;
            // 
            // globalExportPKGListToExcelToolStripMenuItem2
            // 
            globalExportPKGListToExcelToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalExportPKGListToExcelToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalExportPKGListToExcelToolStripMenuItem2.ImageScaling=ToolStripItemImageScaling.None;
            globalExportPKGListToExcelToolStripMenuItem2.Name="globalExportPKGListToExcelToolStripMenuItem2";
            globalExportPKGListToExcelToolStripMenuItem2.ShowShortcutKeys=false;
            globalExportPKGListToExcelToolStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            globalExportPKGListToExcelToolStripMenuItem2.Text="Export PKG list as excel file";
            globalExportPKGListToExcelToolStripMenuItem2.Click+=ExportPKGToExcel_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem3.DropDownItems.AddRange(new ToolStripItem[] { globalExtractImagesAndIconToolStripMenuItem2, globalExtractImageOnlyToolStripMenuItem2, globalExtractIconOnlyToolStripMenuItem2 });
            toolStripMenuItem3.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem3.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem3.Name="toolStripMenuItem3";
            toolStripMenuItem3.ShowShortcutKeys=false;
            toolStripMenuItem3.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem3.Text="Extract images/icons";
            // 
            // globalExtractImagesAndIconToolStripMenuItem2
            // 
            globalExtractImagesAndIconToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalExtractImagesAndIconToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalExtractImagesAndIconToolStripMenuItem2.Name="globalExtractImagesAndIconToolStripMenuItem2";
            globalExtractImagesAndIconToolStripMenuItem2.Size=new System.Drawing.Size(200, 22);
            globalExtractImagesAndIconToolStripMenuItem2.Text="Extract images and icon";
            globalExtractImagesAndIconToolStripMenuItem2.Click+=ExtractImageIcon_Click;
            // 
            // globalExtractImageOnlyToolStripMenuItem2
            // 
            globalExtractImageOnlyToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalExtractImageOnlyToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalExtractImageOnlyToolStripMenuItem2.Name="globalExtractImageOnlyToolStripMenuItem2";
            globalExtractImageOnlyToolStripMenuItem2.Size=new System.Drawing.Size(200, 22);
            globalExtractImageOnlyToolStripMenuItem2.Text="Extract image only";
            globalExtractImageOnlyToolStripMenuItem2.Click+=ExtractImageIcon_Click;
            // 
            // globalExtractIconOnlyToolStripMenuItem2
            // 
            globalExtractIconOnlyToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalExtractIconOnlyToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalExtractIconOnlyToolStripMenuItem2.Name="globalExtractIconOnlyToolStripMenuItem2";
            globalExtractIconOnlyToolStripMenuItem2.Size=new System.Drawing.Size(200, 22);
            globalExtractIconOnlyToolStripMenuItem2.Text="Extract icon only";
            globalExtractIconOnlyToolStripMenuItem2.Click+=ExtractImageIcon_Click;
            // 
            // toolStripMenuItem111
            // 
            toolStripMenuItem111.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem111.DropDownItems.AddRange(new ToolStripItem[] { renameAllPkg1ToolStripMenuItem2, renameAllPkg2ToolStripMenuItem2, renameAllPkg3ToolStripMenuItem2, renameAllPkg4ToolStripMenuItem2, renameAllPkg5ToolStripMenuItem2, renameAllPkg6ToolStripMenuItem2, renameAllPkg7ToolStripMenuItem2, renameAllPkg8ToolStripMenuItem2, renameAllPkg9ToolStripMenuItem2, renameAllPkg10ToolStripMenuItem2, renameAllPkg11ToolStripMenuItem2 });
            toolStripMenuItem111.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem111.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem111.Name="toolStripMenuItem111";
            toolStripMenuItem111.ShowShortcutKeys=false;
            toolStripMenuItem111.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem111.Text="Rename all PKG";
            // 
            // renameAllPkg1ToolStripMenuItem2
            // 
            renameAllPkg1ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg1ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg1ToolStripMenuItem2.Name="renameAllPkg1ToolStripMenuItem2";
            renameAllPkg1ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg1ToolStripMenuItem2.Text="TITILE";
            renameAllPkg1ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg2ToolStripMenuItem2
            // 
            renameAllPkg2ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg2ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg2ToolStripMenuItem2.Name="renameAllPkg2ToolStripMenuItem2";
            renameAllPkg2ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg2ToolStripMenuItem2.Text="TITLE [TITLE_ID]";
            renameAllPkg2ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg3ToolStripMenuItem2
            // 
            renameAllPkg3ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg3ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg3ToolStripMenuItem2.Name="renameAllPkg3ToolStripMenuItem2";
            renameAllPkg3ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg3ToolStripMenuItem2.Text="TITLE [TITLE_ID] [APP_VERSION]";
            renameAllPkg3ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg4ToolStripMenuItem2
            // 
            renameAllPkg4ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg4ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg4ToolStripMenuItem2.Name="renameAllPkg4ToolStripMenuItem2";
            renameAllPkg4ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg4ToolStripMenuItem2.Text="TITLE [CATEGORY]";
            renameAllPkg4ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg5ToolStripMenuItem2
            // 
            renameAllPkg5ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg5ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg5ToolStripMenuItem2.Name="renameAllPkg5ToolStripMenuItem2";
            renameAllPkg5ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg5ToolStripMenuItem2.Text="TITLE_ID";
            renameAllPkg5ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg6ToolStripMenuItem2
            // 
            renameAllPkg6ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg6ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg6ToolStripMenuItem2.Name="renameAllPkg6ToolStripMenuItem2";
            renameAllPkg6ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg6ToolStripMenuItem2.Text="TITLE_ID [TITLE]";
            renameAllPkg6ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg7ToolStripMenuItem2
            // 
            renameAllPkg7ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg7ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg7ToolStripMenuItem2.Name="renameAllPkg7ToolStripMenuItem2";
            renameAllPkg7ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg7ToolStripMenuItem2.Text="[TITLE_ID] [CATEGORY] [APP_VERSION] TITLE";
            renameAllPkg7ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg8ToolStripMenuItem2
            // 
            renameAllPkg8ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg8ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg8ToolStripMenuItem2.Name="renameAllPkg8ToolStripMenuItem2";
            renameAllPkg8ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg8ToolStripMenuItem2.Text="TITLE [CATEGORY] [APP_VERSION]";
            renameAllPkg8ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg9ToolStripMenuItem2
            // 
            renameAllPkg9ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg9ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg9ToolStripMenuItem2.Name="renameAllPkg9ToolStripMenuItem2";
            renameAllPkg9ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg9ToolStripMenuItem2.Text="CONTENT_ID";
            renameAllPkg9ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg10ToolStripMenuItem2
            // 
            renameAllPkg10ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg10ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg10ToolStripMenuItem2.Name="renameAllPkg10ToolStripMenuItem2";
            renameAllPkg10ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg10ToolStripMenuItem2.Text="CONTENT_ID 2";
            renameAllPkg10ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameAllPkg11ToolStripMenuItem2
            // 
            renameAllPkg11ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg11ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg11ToolStripMenuItem2.Name="renameAllPkg11ToolStripMenuItem2";
            renameAllPkg11ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameAllPkg11ToolStripMenuItem2.Text="CUSTOM NAME";
            renameAllPkg11ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // toolStripMenuItem38
            // 
            toolStripMenuItem38.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem38.DropDownItems.AddRange(new ToolStripItem[] { movePkgTitleToolStripMenuItem2, movePkgTypeToolStripMenuItem2, movePkgCategoryToolStripMenuItem2, movePkgRegionToolStripMenuItem2 });
            toolStripMenuItem38.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem38.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem38.Name="toolStripMenuItem38";
            toolStripMenuItem38.ShowShortcutKeys=false;
            toolStripMenuItem38.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem38.Text="Move PKG into seperate folder";
            // 
            // movePkgTitleToolStripMenuItem2
            // 
            movePkgTitleToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            movePkgTitleToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            movePkgTitleToolStripMenuItem2.Name="movePkgTitleToolStripMenuItem2";
            movePkgTitleToolStripMenuItem2.Size=new System.Drawing.Size(312, 22);
            movePkgTitleToolStripMenuItem2.Text="Move by PKG title";
            movePkgTitleToolStripMenuItem2.Click+=MovePkg_Click;
            // 
            // movePkgTypeToolStripMenuItem2
            // 
            movePkgTypeToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            movePkgTypeToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            movePkgTypeToolStripMenuItem2.Name="movePkgTypeToolStripMenuItem2";
            movePkgTypeToolStripMenuItem2.Size=new System.Drawing.Size(312, 22);
            movePkgTypeToolStripMenuItem2.Text="Move by PKG category (Base/Update/Addon)";
            movePkgTypeToolStripMenuItem2.Click+=MovePkg_Click;
            // 
            // movePkgCategoryToolStripMenuItem2
            // 
            movePkgCategoryToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            movePkgCategoryToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            movePkgCategoryToolStripMenuItem2.Name="movePkgCategoryToolStripMenuItem2";
            movePkgCategoryToolStripMenuItem2.Size=new System.Drawing.Size(312, 22);
            movePkgCategoryToolStripMenuItem2.Text="Move by PKG type (Fake/Official)";
            movePkgCategoryToolStripMenuItem2.Click+=MovePkg_Click;
            // 
            // movePkgRegionToolStripMenuItem2
            // 
            movePkgRegionToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            movePkgRegionToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            movePkgRegionToolStripMenuItem2.Name="movePkgRegionToolStripMenuItem2";
            movePkgRegionToolStripMenuItem2.Size=new System.Drawing.Size(312, 22);
            movePkgRegionToolStripMenuItem2.Text="Move by PKG region";
            movePkgRegionToolStripMenuItem2.Click+=MovePkg_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator2.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator2.Name="toolStripSeparator2";
            toolStripSeparator2.Size=new System.Drawing.Size(244, 6);
            // 
            // GroupActionTitleStripMenuItem
            // 
            GroupActionTitleStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            GroupActionTitleStripMenuItem.Enabled=false;
            GroupActionTitleStripMenuItem.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            GroupActionTitleStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            GroupActionTitleStripMenuItem.ImageScaling=ToolStripItemImageScaling.None;
            GroupActionTitleStripMenuItem.Name="GroupActionTitleStripMenuItem";
            GroupActionTitleStripMenuItem.ShowShortcutKeys=false;
            GroupActionTitleStripMenuItem.Size=new System.Drawing.Size(247, 22);
            GroupActionTitleStripMenuItem.Text="...";
            // 
            // toolStripMenuItem127
            // 
            toolStripMenuItem127.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem127.DropDownItems.AddRange(new ToolStripItem[] { copyTitleIdtoolStripMenuItem2, copyContentIdtoolStripMenuItem2 });
            toolStripMenuItem127.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem127.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem127.Name="toolStripMenuItem127";
            toolStripMenuItem127.ShowShortcutKeys=false;
            toolStripMenuItem127.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem127.Text="Copy";
            // 
            // copyTitleIdtoolStripMenuItem2
            // 
            copyTitleIdtoolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            copyTitleIdtoolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            copyTitleIdtoolStripMenuItem2.Name="copyTitleIdtoolStripMenuItem2";
            copyTitleIdtoolStripMenuItem2.Size=new System.Drawing.Size(143, 22);
            copyTitleIdtoolStripMenuItem2.Text="TITLE_ID";
            copyTitleIdtoolStripMenuItem2.Click+=CopyID_Click;
            // 
            // copyContentIdtoolStripMenuItem2
            // 
            copyContentIdtoolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            copyContentIdtoolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            copyContentIdtoolStripMenuItem2.Name="copyContentIdtoolStripMenuItem2";
            copyContentIdtoolStripMenuItem2.Size=new System.Drawing.Size(143, 22);
            copyContentIdtoolStripMenuItem2.Text="CONTENT_ID";
            copyContentIdtoolStripMenuItem2.Click+=CopyID_Click;
            // 
            // backportToolStripMenuItem
            // 
            backportToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            backportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setBackportedToolStripMenuItem2, setBackportRemarksToolStripMenuItem, toolStripSeparator9, removeBackportedToolStripMenuItem2 });
            backportToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            backportToolStripMenuItem.Name="backportToolStripMenuItem";
            backportToolStripMenuItem.Size=new System.Drawing.Size(247, 22);
            backportToolStripMenuItem.Text="Backport";
            // 
            // setBackportedToolStripMenuItem2
            // 
            setBackportedToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            setBackportedToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            setBackportedToolStripMenuItem2.Name="setBackportedToolStripMenuItem2";
            setBackportedToolStripMenuItem2.Size=new System.Drawing.Size(208, 22);
            setBackportedToolStripMenuItem2.Text="Set as backported";
            setBackportedToolStripMenuItem2.Click+=Backport_Click;
            // 
            // setBackportRemarksToolStripMenuItem
            // 
            setBackportRemarksToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            setBackportRemarksToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backportRemarkTextboxtoolStripTextBox2, setRemarktoolStripMenuItem2 });
            setBackportRemarksToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            setBackportRemarksToolStripMenuItem.Name="setBackportRemarksToolStripMenuItem";
            setBackportRemarksToolStripMenuItem.Size=new System.Drawing.Size(208, 22);
            setBackportRemarksToolStripMenuItem.Text="Set backport remarks";
            // 
            // backportRemarkTextboxtoolStripTextBox2
            // 
            backportRemarkTextboxtoolStripTextBox2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            backportRemarkTextboxtoolStripTextBox2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            backportRemarkTextboxtoolStripTextBox2.Name="backportRemarkTextboxtoolStripTextBox2";
            backportRemarkTextboxtoolStripTextBox2.Size=new System.Drawing.Size(180, 23);
            // 
            // setRemarktoolStripMenuItem2
            // 
            setRemarktoolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            setRemarktoolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            setRemarktoolStripMenuItem2.Name="setRemarktoolStripMenuItem2";
            setRemarktoolStripMenuItem2.Size=new System.Drawing.Size(240, 22);
            setRemarktoolStripMenuItem2.Text="Set remarks";
            setRemarktoolStripMenuItem2.Click+=Backport_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator9.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator9.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator9.Name="toolStripSeparator9";
            toolStripSeparator9.Size=new System.Drawing.Size(205, 6);
            // 
            // removeBackportedToolStripMenuItem2
            // 
            removeBackportedToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            removeBackportedToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            removeBackportedToolStripMenuItem2.Name="removeBackportedToolStripMenuItem2";
            removeBackportedToolStripMenuItem2.Size=new System.Drawing.Size(208, 22);
            removeBackportedToolStripMenuItem2.Text="Remove backported label";
            removeBackportedToolStripMenuItem2.Click+=Backport_Click;
            // 
            // deletePkgtoolStripMenuItem2
            // 
            deletePkgtoolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            deletePkgtoolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            deletePkgtoolStripMenuItem2.ImageScaling=ToolStripItemImageScaling.None;
            deletePkgtoolStripMenuItem2.Name="deletePkgtoolStripMenuItem2";
            deletePkgtoolStripMenuItem2.ShowShortcutKeys=false;
            deletePkgtoolStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            deletePkgtoolStripMenuItem2.Text="Delete PKG";
            deletePkgtoolStripMenuItem2.Click+=DeletePKG_Click;
            // 
            // selectedExportPKGListToExcelToolStripMenuItem2
            // 
            selectedExportPKGListToExcelToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            selectedExportPKGListToExcelToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            selectedExportPKGListToExcelToolStripMenuItem2.ImageScaling=ToolStripItemImageScaling.None;
            selectedExportPKGListToExcelToolStripMenuItem2.Name="selectedExportPKGListToExcelToolStripMenuItem2";
            selectedExportPKGListToExcelToolStripMenuItem2.ShowShortcutKeys=false;
            selectedExportPKGListToExcelToolStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            selectedExportPKGListToExcelToolStripMenuItem2.Text="Export as excel file";
            selectedExportPKGListToExcelToolStripMenuItem2.Click+=ExportPKGToExcel_Click;
            // 
            // GroupActionExtacrtImageToolStripMenuItem
            // 
            GroupActionExtacrtImageToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            GroupActionExtacrtImageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { selectedExtractImagesAndIconToolStripMenuItem2, selectedExtractImageOnlyToolStripMenuItem2, selectedExtractIconOnlyToolStripMenuItem2 });
            GroupActionExtacrtImageToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            GroupActionExtacrtImageToolStripMenuItem.ImageScaling=ToolStripItemImageScaling.None;
            GroupActionExtacrtImageToolStripMenuItem.Name="GroupActionExtacrtImageToolStripMenuItem";
            GroupActionExtacrtImageToolStripMenuItem.ShowShortcutKeys=false;
            GroupActionExtacrtImageToolStripMenuItem.Size=new System.Drawing.Size(247, 22);
            GroupActionExtacrtImageToolStripMenuItem.Text="Extract images/icons";
            // 
            // selectedExtractImagesAndIconToolStripMenuItem2
            // 
            selectedExtractImagesAndIconToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            selectedExtractImagesAndIconToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            selectedExtractImagesAndIconToolStripMenuItem2.Name="selectedExtractImagesAndIconToolStripMenuItem2";
            selectedExtractImagesAndIconToolStripMenuItem2.Size=new System.Drawing.Size(200, 22);
            selectedExtractImagesAndIconToolStripMenuItem2.Text="Extract images and icon";
            selectedExtractImagesAndIconToolStripMenuItem2.Click+=ExtractImageIcon_Click;
            // 
            // selectedExtractImageOnlyToolStripMenuItem2
            // 
            selectedExtractImageOnlyToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            selectedExtractImageOnlyToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            selectedExtractImageOnlyToolStripMenuItem2.Name="selectedExtractImageOnlyToolStripMenuItem2";
            selectedExtractImageOnlyToolStripMenuItem2.Size=new System.Drawing.Size(200, 22);
            selectedExtractImageOnlyToolStripMenuItem2.Text="Extract image only";
            selectedExtractImageOnlyToolStripMenuItem2.Click+=ExtractImageIcon_Click;
            // 
            // selectedExtractIconOnlyToolStripMenuItem2
            // 
            selectedExtractIconOnlyToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            selectedExtractIconOnlyToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            selectedExtractIconOnlyToolStripMenuItem2.Name="selectedExtractIconOnlyToolStripMenuItem2";
            selectedExtractIconOnlyToolStripMenuItem2.Size=new System.Drawing.Size(200, 22);
            selectedExtractIconOnlyToolStripMenuItem2.Text="Extract icon only";
            selectedExtractIconOnlyToolStripMenuItem2.Click+=ExtractImageIcon_Click;
            // 
            // toolStripMenuItem133
            // 
            toolStripMenuItem133.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem133.DropDownItems.AddRange(new ToolStripItem[] { renameSelectedPkg1ToolStripMenuItem2, renameSelectedPkg2ToolStripMenuItem2, renameSelectedPkg3ToolStripMenuItem2, renameSelectedPkg4ToolStripMenuItem2, renameSelectedPkg5ToolStripMenuItem2, renameSelectedPkg6ToolStripMenuItem2, renameSelectedPkg7ToolStripMenuItem2, renameSelectedPkg8ToolStripMenuItem2, renameSelectedPkg9ToolStripMenuItem2, renameSelectedPkg10ToolStripMenuItem2, renameSelectedPkg11ToolStripMenuItem2 });
            toolStripMenuItem133.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem133.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem133.Name="toolStripMenuItem133";
            toolStripMenuItem133.ShowShortcutKeys=false;
            toolStripMenuItem133.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem133.Text="Rename PKG";
            // 
            // renameSelectedPkg1ToolStripMenuItem2
            // 
            renameSelectedPkg1ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg1ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg1ToolStripMenuItem2.Name="renameSelectedPkg1ToolStripMenuItem2";
            renameSelectedPkg1ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg1ToolStripMenuItem2.Text="TITILE";
            renameSelectedPkg1ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg2ToolStripMenuItem2
            // 
            renameSelectedPkg2ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg2ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg2ToolStripMenuItem2.Name="renameSelectedPkg2ToolStripMenuItem2";
            renameSelectedPkg2ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg2ToolStripMenuItem2.Text="TITLE [TITLE_ID]";
            renameSelectedPkg2ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg3ToolStripMenuItem2
            // 
            renameSelectedPkg3ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg3ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg3ToolStripMenuItem2.Name="renameSelectedPkg3ToolStripMenuItem2";
            renameSelectedPkg3ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg3ToolStripMenuItem2.Text="TITLE [TITLE_ID] [APP_VERSION]";
            renameSelectedPkg3ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg4ToolStripMenuItem2
            // 
            renameSelectedPkg4ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg4ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg4ToolStripMenuItem2.Name="renameSelectedPkg4ToolStripMenuItem2";
            renameSelectedPkg4ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg4ToolStripMenuItem2.Text="TITLE [CATEGORY]";
            renameSelectedPkg4ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg5ToolStripMenuItem2
            // 
            renameSelectedPkg5ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg5ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg5ToolStripMenuItem2.Name="renameSelectedPkg5ToolStripMenuItem2";
            renameSelectedPkg5ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg5ToolStripMenuItem2.Text="TITLE_ID";
            renameSelectedPkg5ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg6ToolStripMenuItem2
            // 
            renameSelectedPkg6ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg6ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg6ToolStripMenuItem2.Name="renameSelectedPkg6ToolStripMenuItem2";
            renameSelectedPkg6ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg6ToolStripMenuItem2.Text="TITLE_ID [TITLE]";
            renameSelectedPkg6ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg7ToolStripMenuItem2
            // 
            renameSelectedPkg7ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg7ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg7ToolStripMenuItem2.Name="renameSelectedPkg7ToolStripMenuItem2";
            renameSelectedPkg7ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg7ToolStripMenuItem2.Text="[TITLE_ID] [CATEGORY] [APP_VERSION] TITLE";
            renameSelectedPkg7ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg8ToolStripMenuItem2
            // 
            renameSelectedPkg8ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg8ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg8ToolStripMenuItem2.Name="renameSelectedPkg8ToolStripMenuItem2";
            renameSelectedPkg8ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg8ToolStripMenuItem2.Text="TITLE [CATEGORY] [APP_VERSION]";
            renameSelectedPkg8ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg9ToolStripMenuItem2
            // 
            renameSelectedPkg9ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg9ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg9ToolStripMenuItem2.Name="renameSelectedPkg9ToolStripMenuItem2";
            renameSelectedPkg9ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg9ToolStripMenuItem2.Text="CONTENT_ID";
            renameSelectedPkg9ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg10ToolStripMenuItem2
            // 
            renameSelectedPkg10ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg10ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg10ToolStripMenuItem2.Name="renameSelectedPkg10ToolStripMenuItem2";
            renameSelectedPkg10ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg10ToolStripMenuItem2.Text="CONTENT_ID 2";
            renameSelectedPkg10ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg11ToolStripMenuItem2
            // 
            renameSelectedPkg11ToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg11ToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg11ToolStripMenuItem2.Name="renameSelectedPkg11ToolStripMenuItem2";
            renameSelectedPkg11ToolStripMenuItem2.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg11ToolStripMenuItem2.Text="CUSTOM NAME";
            renameSelectedPkg11ToolStripMenuItem2.Click+=RenamePkg_Click;
            // 
            // viewPkgChangeInfotoolStripMenuItem2
            // 
            viewPkgChangeInfotoolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            viewPkgChangeInfotoolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            viewPkgChangeInfotoolStripMenuItem2.ImageScaling=ToolStripItemImageScaling.None;
            viewPkgChangeInfotoolStripMenuItem2.Name="viewPkgChangeInfotoolStripMenuItem2";
            viewPkgChangeInfotoolStripMenuItem2.ShowShortcutKeys=false;
            viewPkgChangeInfotoolStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            viewPkgChangeInfotoolStripMenuItem2.Text="View PKG Change Info";
            viewPkgChangeInfotoolStripMenuItem2.Click+=ViewPatchChangelog_Click;
            // 
            // viewPkgExplorerStripMenuItem2
            // 
            viewPkgExplorerStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            viewPkgExplorerStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            viewPkgExplorerStripMenuItem2.ImageScaling=ToolStripItemImageScaling.None;
            viewPkgExplorerStripMenuItem2.Name="viewPkgExplorerStripMenuItem2";
            viewPkgExplorerStripMenuItem2.ShowShortcutKeys=false;
            viewPkgExplorerStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            viewPkgExplorerStripMenuItem2.Text="View PKG in Explorer";
            viewPkgExplorerStripMenuItem2.Click+=ViewPKGExplorer_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator7.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator7.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator7.Name="toolStripSeparator7";
            toolStripSeparator7.Size=new System.Drawing.Size(244, 6);
            // 
            // toolStripMenuItem18
            // 
            toolStripMenuItem18.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem18.Enabled=false;
            toolStripMenuItem18.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripMenuItem18.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem18.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem18.Name="toolStripMenuItem18";
            toolStripMenuItem18.ShowShortcutKeys=false;
            toolStripMenuItem18.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem18.Text="Remote PKG Installer | Status : Idle";
            // 
            // RpiCheckPkgInstalledtoolStripMenuItem2
            // 
            RpiCheckPkgInstalledtoolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiCheckPkgInstalledtoolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiCheckPkgInstalledtoolStripMenuItem2.ImageScaling=ToolStripItemImageScaling.None;
            RpiCheckPkgInstalledtoolStripMenuItem2.Name="RpiCheckPkgInstalledtoolStripMenuItem2";
            RpiCheckPkgInstalledtoolStripMenuItem2.ShowShortcutKeys=false;
            RpiCheckPkgInstalledtoolStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            RpiCheckPkgInstalledtoolStripMenuItem2.Text="Check if base PKG installed on PS4";
            RpiCheckPkgInstalledtoolStripMenuItem2.Click+=Rpi_Click;
            // 
            // RpiSendPkgtoolStripMenuItem2
            // 
            RpiSendPkgtoolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiSendPkgtoolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiSendPkgtoolStripMenuItem2.ImageScaling=ToolStripItemImageScaling.None;
            RpiSendPkgtoolStripMenuItem2.Name="RpiSendPkgtoolStripMenuItem2";
            RpiSendPkgtoolStripMenuItem2.ShowShortcutKeys=false;
            RpiSendPkgtoolStripMenuItem2.Size=new System.Drawing.Size(247, 22);
            RpiSendPkgtoolStripMenuItem2.Text="Send PKG to PS4";
            RpiSendPkgtoolStripMenuItem2.Click+=Rpi_Click;
            // 
            // toolStripMenuItem21
            // 
            toolStripMenuItem21.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem21.DropDownItems.AddRange(new ToolStripItem[] { RpiUninstallBasePKGToolStripMenuItem2, RpiUninstallPatchPKGToolStripMenuItem2, RpiUninstallDlcPKGToolStripMenuItem2, RpiUninstallThemePKGToolStripMenuItem2 });
            toolStripMenuItem21.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem21.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem21.Name="toolStripMenuItem21";
            toolStripMenuItem21.ShowShortcutKeys=false;
            toolStripMenuItem21.Size=new System.Drawing.Size(247, 22);
            toolStripMenuItem21.Text="Uninstall PKG from PS4";
            // 
            // RpiUninstallBasePKGToolStripMenuItem2
            // 
            RpiUninstallBasePKGToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiUninstallBasePKGToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiUninstallBasePKGToolStripMenuItem2.Name="RpiUninstallBasePKGToolStripMenuItem2";
            RpiUninstallBasePKGToolStripMenuItem2.Size=new System.Drawing.Size(228, 22);
            RpiUninstallBasePKGToolStripMenuItem2.Text="Uninstall base PKG";
            RpiUninstallBasePKGToolStripMenuItem2.Click+=Rpi_Click;
            // 
            // RpiUninstallPatchPKGToolStripMenuItem2
            // 
            RpiUninstallPatchPKGToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiUninstallPatchPKGToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiUninstallPatchPKGToolStripMenuItem2.Name="RpiUninstallPatchPKGToolStripMenuItem2";
            RpiUninstallPatchPKGToolStripMenuItem2.Size=new System.Drawing.Size(228, 22);
            RpiUninstallPatchPKGToolStripMenuItem2.Text="Uninstall patch PKG";
            RpiUninstallPatchPKGToolStripMenuItem2.Click+=Rpi_Click;
            // 
            // RpiUninstallDlcPKGToolStripMenuItem2
            // 
            RpiUninstallDlcPKGToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiUninstallDlcPKGToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiUninstallDlcPKGToolStripMenuItem2.Name="RpiUninstallDlcPKGToolStripMenuItem2";
            RpiUninstallDlcPKGToolStripMenuItem2.Size=new System.Drawing.Size(228, 22);
            RpiUninstallDlcPKGToolStripMenuItem2.Text="Uninstall addon PKG (DLC)";
            RpiUninstallDlcPKGToolStripMenuItem2.Click+=Rpi_Click;
            // 
            // RpiUninstallThemePKGToolStripMenuItem2
            // 
            RpiUninstallThemePKGToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiUninstallThemePKGToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiUninstallThemePKGToolStripMenuItem2.Name="RpiUninstallThemePKGToolStripMenuItem2";
            RpiUninstallThemePKGToolStripMenuItem2.Size=new System.Drawing.Size(228, 22);
            RpiUninstallThemePKGToolStripMenuItem2.Text="Uninstall addon PKG (Theme)";
            RpiUninstallThemePKGToolStripMenuItem2.Click+=Rpi_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator5.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator5.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator5.Name="toolStripSeparator5";
            toolStripSeparator5.Size=new System.Drawing.Size(267, 6);
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator6.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator6.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator6.Name="toolStripSeparator6";
            toolStripSeparator6.Size=new System.Drawing.Size(267, 6);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator3.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator3.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator3.Name="toolStripSeparator3";
            toolStripSeparator3.Size=new System.Drawing.Size(267, 6);
            // 
            // label8
            // 
            label8.Anchor=AnchorStyles.Top|AnchorStyles.Right;
            label8.AutoSize=true;
            label8.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label8.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            label8.Location=new System.Drawing.Point(959, 649);
            label8.Name="label8";
            label8.Size=new System.Drawing.Size(0, 15);
            label8.TabIndex=82;
            label8.Visible=false;
            // 
            // darkMenuStrip1
            // 
            darkMenuStrip1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            darkMenuStrip1.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkMenuStrip1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            darkMenuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem2, toolToolStripMenuItem1, toolStripMenuItem144, testToolStripMenuItem });
            darkMenuStrip1.Location=new System.Drawing.Point(0, 0);
            darkMenuStrip1.Name="darkMenuStrip1";
            darkMenuStrip1.Padding=new Padding(3, 2, 0, 2);
            darkMenuStrip1.Size=new System.Drawing.Size(984, 24);
            darkMenuStrip1.TabIndex=84;
            darkMenuStrip1.Text="darkMenuStrip1";
            // 
            // fileToolStripMenuItem2
            // 
            fileToolStripMenuItem2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            fileToolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { managePS4PKGToolStripMenuItem, toolStripSeparator11, exitToolStripMenuItem1 });
            fileToolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            fileToolStripMenuItem2.Name="fileToolStripMenuItem2";
            fileToolStripMenuItem2.Size=new System.Drawing.Size(37, 20);
            fileToolStripMenuItem2.Text="File";
            // 
            // managePS4PKGToolStripMenuItem
            // 
            managePS4PKGToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            managePS4PKGToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            managePS4PKGToolStripMenuItem.Name="managePS4PKGToolStripMenuItem";
            managePS4PKGToolStripMenuItem.ShowShortcutKeys=false;
            managePS4PKGToolStripMenuItem.Size=new System.Drawing.Size(216, 22);
            managePS4PKGToolStripMenuItem.Text="Open PKG Directory Settings";
            managePS4PKGToolStripMenuItem.Visible=false;
            managePS4PKGToolStripMenuItem.Click+=managePS4PKGToolStripMenuItem_Click;
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator11.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator11.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator11.Name="toolStripSeparator11";
            toolStripSeparator11.Size=new System.Drawing.Size(213, 6);
            toolStripSeparator11.Visible=false;
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            exitToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            exitToolStripMenuItem1.Name="exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size=new System.Drawing.Size(216, 22);
            exitToolStripMenuItem1.Text="Exit";
            exitToolStripMenuItem1.Click+=toolStripMenuItem78_Click;
            // 
            // toolToolStripMenuItem1
            // 
            toolToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { openPS4PKGToolTempDirectoryToolStripMenuItem1, reloadContentToolStripMenuItem, settingstoolStripMenuItem, toolStripSeparator12, globalActionToolStripMenuItem, checkForDuplicatePKGToolStripMenuItem1, globalExportPKGListToExcelToolStripMenuItem1, extractImageAndBackgroundToolStripMenuItem, renameToolStripMenuItem, seperateAndMovePKGByTypeIntoFolderToolStripMenuItem, toolStripSeparator4, toolStripMenuItem2, globalCopyStripMenuItem, toolStripMenuItem1, deletePKGtoolStripMenuItem1, selectedExportPKGListToExcelToolStripMenuItem1, toolStripMenuItem28, renameCurrentPKGStripMenuItem, viewPkgChangeInfotoolStripMenuItem1, viewPkgExplorerStripMenuItem1, toolStripSeparator8, toolStripMenuItem16, RpiCheckPkgInstalledtoolStripMenuItem1, RpiSendPkgtoolStripMenuItem1, uninstallPKGFromPS4ToolStripMenuItem });
            toolToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolToolStripMenuItem1.Name="toolToolStripMenuItem1";
            toolToolStripMenuItem1.Size=new System.Drawing.Size(41, 20);
            toolToolStripMenuItem1.Text="Tool";
            // 
            // openPS4PKGToolTempDirectoryToolStripMenuItem1
            // 
            openPS4PKGToolTempDirectoryToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            openPS4PKGToolTempDirectoryToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            openPS4PKGToolTempDirectoryToolStripMenuItem1.Name="openPS4PKGToolTempDirectoryToolStripMenuItem1";
            openPS4PKGToolTempDirectoryToolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            openPS4PKGToolTempDirectoryToolStripMenuItem1.Text="Open PS4PKGToolTemp directory";
            openPS4PKGToolTempDirectoryToolStripMenuItem1.Click+=OpenPS4PKGToolTempDirectory_Click;
            // 
            // reloadContentToolStripMenuItem
            // 
            reloadContentToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            reloadContentToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            reloadContentToolStripMenuItem.Name="reloadContentToolStripMenuItem";
            reloadContentToolStripMenuItem.Size=new System.Drawing.Size(254, 22);
            reloadContentToolStripMenuItem.Text="Refresh PKG list";
            reloadContentToolStripMenuItem.Click+=toolStripMenuItem76_Click;
            // 
            // settingstoolStripMenuItem
            // 
            settingstoolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            settingstoolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            settingstoolStripMenuItem.Name="settingstoolStripMenuItem";
            settingstoolStripMenuItem.Size=new System.Drawing.Size(254, 22);
            settingstoolStripMenuItem.Text="Settings";
            settingstoolStripMenuItem.Click+=settingstoolStripMenuItem_Click;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator12.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator12.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator12.Name="toolStripSeparator12";
            toolStripSeparator12.Size=new System.Drawing.Size(251, 6);
            // 
            // globalActionToolStripMenuItem
            // 
            globalActionToolStripMenuItem.Enabled=false;
            globalActionToolStripMenuItem.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            globalActionToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(153, 153, 153);
            globalActionToolStripMenuItem.Name="globalActionToolStripMenuItem";
            globalActionToolStripMenuItem.Size=new System.Drawing.Size(254, 22);
            globalActionToolStripMenuItem.Text="Global Action";
            // 
            // checkForDuplicatePKGToolStripMenuItem1
            // 
            checkForDuplicatePKGToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            checkForDuplicatePKGToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            checkForDuplicatePKGToolStripMenuItem1.Name="checkForDuplicatePKGToolStripMenuItem1";
            checkForDuplicatePKGToolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            checkForDuplicatePKGToolStripMenuItem1.Text="Check for duplicate PKG";
            checkForDuplicatePKGToolStripMenuItem1.Click+=CheckForDuplicatePKG_Click;
            // 
            // globalExportPKGListToExcelToolStripMenuItem1
            // 
            globalExportPKGListToExcelToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalExportPKGListToExcelToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalExportPKGListToExcelToolStripMenuItem1.Name="globalExportPKGListToExcelToolStripMenuItem1";
            globalExportPKGListToExcelToolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            globalExportPKGListToExcelToolStripMenuItem1.Text="Export PKG list as excel file";
            globalExportPKGListToExcelToolStripMenuItem1.Click+=ExportPKGToExcel_Click;
            // 
            // extractImageAndBackgroundToolStripMenuItem
            // 
            extractImageAndBackgroundToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            extractImageAndBackgroundToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { globalExtractImagesAndIconToolStripMenuItem1, globalExtractImageOnlyToolStripMenuItem1, globalExtractIconOnlyToolStripMenuItem1 });
            extractImageAndBackgroundToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            extractImageAndBackgroundToolStripMenuItem.Name="extractImageAndBackgroundToolStripMenuItem";
            extractImageAndBackgroundToolStripMenuItem.Size=new System.Drawing.Size(254, 22);
            extractImageAndBackgroundToolStripMenuItem.Text="Extract images/icons";
            // 
            // globalExtractImagesAndIconToolStripMenuItem1
            // 
            globalExtractImagesAndIconToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalExtractImagesAndIconToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalExtractImagesAndIconToolStripMenuItem1.Name="globalExtractImagesAndIconToolStripMenuItem1";
            globalExtractImagesAndIconToolStripMenuItem1.Size=new System.Drawing.Size(200, 22);
            globalExtractImagesAndIconToolStripMenuItem1.Text="Extract images and icon";
            globalExtractImagesAndIconToolStripMenuItem1.Click+=ExtractImageIcon_Click;
            // 
            // globalExtractImageOnlyToolStripMenuItem1
            // 
            globalExtractImageOnlyToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalExtractImageOnlyToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalExtractImageOnlyToolStripMenuItem1.Name="globalExtractImageOnlyToolStripMenuItem1";
            globalExtractImageOnlyToolStripMenuItem1.Size=new System.Drawing.Size(200, 22);
            globalExtractImageOnlyToolStripMenuItem1.Text="Extract image only";
            globalExtractImageOnlyToolStripMenuItem1.Click+=ExtractImageIcon_Click;
            // 
            // globalExtractIconOnlyToolStripMenuItem1
            // 
            globalExtractIconOnlyToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalExtractIconOnlyToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalExtractIconOnlyToolStripMenuItem1.Name="globalExtractIconOnlyToolStripMenuItem1";
            globalExtractIconOnlyToolStripMenuItem1.Size=new System.Drawing.Size(200, 22);
            globalExtractIconOnlyToolStripMenuItem1.Text="Extract icon only";
            globalExtractIconOnlyToolStripMenuItem1.Click+=ExtractImageIcon_Click;
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { renameAllPkg1ToolStripMenuItem1, renameAllPkg2ToolStripMenuItem1, renameAllPkg3ToolStripMenuItem1, renameAllPkg4ToolStripMenuItem1, renameAllPkg5ToolStripMenuItem1, renameAllPkg6ToolStripMenuItem1, renameAllPkg7ToolStripMenuItem1, renameAllPkg8ToolStripMenuItem1, renameAllPkg9ToolStripMenuItem1, renameAllPkg10ToolStripMenuItem1, renameAllPkg11ToolStripMenuItem1 });
            renameToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameToolStripMenuItem.Name="renameToolStripMenuItem";
            renameToolStripMenuItem.Size=new System.Drawing.Size(254, 22);
            renameToolStripMenuItem.Text="Rename all PKG";
            // 
            // renameAllPkg1ToolStripMenuItem1
            // 
            renameAllPkg1ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg1ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg1ToolStripMenuItem1.Name="renameAllPkg1ToolStripMenuItem1";
            renameAllPkg1ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg1ToolStripMenuItem1.Text="TITILE";
            renameAllPkg1ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg2ToolStripMenuItem1
            // 
            renameAllPkg2ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg2ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg2ToolStripMenuItem1.Name="renameAllPkg2ToolStripMenuItem1";
            renameAllPkg2ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg2ToolStripMenuItem1.Text="TITLE [TITLE_ID]";
            renameAllPkg2ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg3ToolStripMenuItem1
            // 
            renameAllPkg3ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg3ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg3ToolStripMenuItem1.Name="renameAllPkg3ToolStripMenuItem1";
            renameAllPkg3ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg3ToolStripMenuItem1.Text="TITLE [TITLE_ID] [APP_VERSION]";
            renameAllPkg3ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg4ToolStripMenuItem1
            // 
            renameAllPkg4ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg4ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg4ToolStripMenuItem1.Name="renameAllPkg4ToolStripMenuItem1";
            renameAllPkg4ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg4ToolStripMenuItem1.Text="TITLE [CATEGORY]";
            renameAllPkg4ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg5ToolStripMenuItem1
            // 
            renameAllPkg5ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg5ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg5ToolStripMenuItem1.Name="renameAllPkg5ToolStripMenuItem1";
            renameAllPkg5ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg5ToolStripMenuItem1.Text="TITLE_ID";
            renameAllPkg5ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg6ToolStripMenuItem1
            // 
            renameAllPkg6ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg6ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg6ToolStripMenuItem1.Name="renameAllPkg6ToolStripMenuItem1";
            renameAllPkg6ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg6ToolStripMenuItem1.Text="[TITLE_ID] TITLE";
            renameAllPkg6ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg7ToolStripMenuItem1
            // 
            renameAllPkg7ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg7ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg7ToolStripMenuItem1.Name="renameAllPkg7ToolStripMenuItem1";
            renameAllPkg7ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg7ToolStripMenuItem1.Text="[TITLE_ID] [CATEGORY] [APP_VERSION] TITLE";
            renameAllPkg7ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg8ToolStripMenuItem1
            // 
            renameAllPkg8ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg8ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg8ToolStripMenuItem1.Name="renameAllPkg8ToolStripMenuItem1";
            renameAllPkg8ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg8ToolStripMenuItem1.Text="TITLE [CATEGORY] [APP_VERSION]";
            renameAllPkg8ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg9ToolStripMenuItem1
            // 
            renameAllPkg9ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg9ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg9ToolStripMenuItem1.Name="renameAllPkg9ToolStripMenuItem1";
            renameAllPkg9ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg9ToolStripMenuItem1.Text="CONTENT_ID";
            renameAllPkg9ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg10ToolStripMenuItem1
            // 
            renameAllPkg10ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg10ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg10ToolStripMenuItem1.Name="renameAllPkg10ToolStripMenuItem1";
            renameAllPkg10ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg10ToolStripMenuItem1.Text="CONTENT_ID 2";
            renameAllPkg10ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameAllPkg11ToolStripMenuItem1
            // 
            renameAllPkg11ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameAllPkg11ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameAllPkg11ToolStripMenuItem1.Name="renameAllPkg11ToolStripMenuItem1";
            renameAllPkg11ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameAllPkg11ToolStripMenuItem1.Text="CUSTOM NAME";
            renameAllPkg11ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // seperateAndMovePKGByTypeIntoFolderToolStripMenuItem
            // 
            seperateAndMovePKGByTypeIntoFolderToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            seperateAndMovePKGByTypeIntoFolderToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { movePkgTitleToolStripMenuItem1, movePkgCategoryToolStripMenuItem1, movePkgTypeToolStripMenuItem1, movePkgRegionToolStripMenuItem1 });
            seperateAndMovePKGByTypeIntoFolderToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            seperateAndMovePKGByTypeIntoFolderToolStripMenuItem.Name="seperateAndMovePKGByTypeIntoFolderToolStripMenuItem";
            seperateAndMovePKGByTypeIntoFolderToolStripMenuItem.Size=new System.Drawing.Size(254, 22);
            seperateAndMovePKGByTypeIntoFolderToolStripMenuItem.Text="Move PKG into seperate folder";
            // 
            // movePkgTitleToolStripMenuItem1
            // 
            movePkgTitleToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            movePkgTitleToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            movePkgTitleToolStripMenuItem1.Name="movePkgTitleToolStripMenuItem1";
            movePkgTitleToolStripMenuItem1.Size=new System.Drawing.Size(312, 22);
            movePkgTitleToolStripMenuItem1.Text="Move by PKG title";
            movePkgTitleToolStripMenuItem1.Click+=MovePkg_Click;
            // 
            // movePkgCategoryToolStripMenuItem1
            // 
            movePkgCategoryToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            movePkgCategoryToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            movePkgCategoryToolStripMenuItem1.Name="movePkgCategoryToolStripMenuItem1";
            movePkgCategoryToolStripMenuItem1.Size=new System.Drawing.Size(312, 22);
            movePkgCategoryToolStripMenuItem1.Text="Move by PKG category (Base/Update/Addon)";
            movePkgCategoryToolStripMenuItem1.Click+=MovePkg_Click;
            // 
            // movePkgTypeToolStripMenuItem1
            // 
            movePkgTypeToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            movePkgTypeToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            movePkgTypeToolStripMenuItem1.Name="movePkgTypeToolStripMenuItem1";
            movePkgTypeToolStripMenuItem1.Size=new System.Drawing.Size(312, 22);
            movePkgTypeToolStripMenuItem1.Text="Move by PKG type (Fake/Official)";
            movePkgTypeToolStripMenuItem1.Click+=MovePkg_Click;
            // 
            // movePkgRegionToolStripMenuItem1
            // 
            movePkgRegionToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            movePkgRegionToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            movePkgRegionToolStripMenuItem1.Name="movePkgRegionToolStripMenuItem1";
            movePkgRegionToolStripMenuItem1.Size=new System.Drawing.Size(312, 22);
            movePkgRegionToolStripMenuItem1.Text="Move by PKG region";
            movePkgRegionToolStripMenuItem1.Click+=MovePkg_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator4.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator4.Name="toolStripSeparator4";
            toolStripSeparator4.Size=new System.Drawing.Size(251, 6);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Enabled=false;
            toolStripMenuItem2.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripMenuItem2.ForeColor=System.Drawing.Color.FromArgb(153, 153, 153);
            toolStripMenuItem2.Name="toolStripMenuItem2";
            toolStripMenuItem2.Size=new System.Drawing.Size(254, 22);
            toolStripMenuItem2.Text="...";
            // 
            // globalCopyStripMenuItem
            // 
            globalCopyStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            globalCopyStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { copyTitleIdtoolStripMenuItem1, copyContentIdtoolStripMenuItem1 });
            globalCopyStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            globalCopyStripMenuItem.Name="globalCopyStripMenuItem";
            globalCopyStripMenuItem.Size=new System.Drawing.Size(254, 22);
            globalCopyStripMenuItem.Text="Copy";
            // 
            // copyTitleIdtoolStripMenuItem1
            // 
            copyTitleIdtoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            copyTitleIdtoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            copyTitleIdtoolStripMenuItem1.Name="copyTitleIdtoolStripMenuItem1";
            copyTitleIdtoolStripMenuItem1.Size=new System.Drawing.Size(143, 22);
            copyTitleIdtoolStripMenuItem1.Text="TITLE_ID";
            copyTitleIdtoolStripMenuItem1.Click+=CopyID_Click;
            // 
            // copyContentIdtoolStripMenuItem1
            // 
            copyContentIdtoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            copyContentIdtoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            copyContentIdtoolStripMenuItem1.Name="copyContentIdtoolStripMenuItem1";
            copyContentIdtoolStripMenuItem1.Size=new System.Drawing.Size(143, 22);
            copyContentIdtoolStripMenuItem1.Text="CONTENT_ID";
            copyContentIdtoolStripMenuItem1.Click+=CopyID_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { setBackportedtoolStripMenuItem1, toolStripMenuItem4, toolStripSeparator10, removeBackportedtoolStripMenuItem1 });
            toolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem1.Name="toolStripMenuItem1";
            toolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            toolStripMenuItem1.Text="Backport";
            // 
            // setBackportedtoolStripMenuItem1
            // 
            setBackportedtoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            setBackportedtoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            setBackportedtoolStripMenuItem1.Name="setBackportedtoolStripMenuItem1";
            setBackportedtoolStripMenuItem1.Size=new System.Drawing.Size(208, 22);
            setBackportedtoolStripMenuItem1.Text="Set as backported";
            setBackportedtoolStripMenuItem1.Click+=Backport_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem4.DropDownItems.AddRange(new ToolStripItem[] { backportRemarkTextboxtoolStripTextBox1, setRemarktoolStripMenuItem1 });
            toolStripMenuItem4.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem4.Name="toolStripMenuItem4";
            toolStripMenuItem4.Size=new System.Drawing.Size(208, 22);
            toolStripMenuItem4.Text="Set backport remarks";
            // 
            // backportRemarkTextboxtoolStripTextBox1
            // 
            backportRemarkTextboxtoolStripTextBox1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            backportRemarkTextboxtoolStripTextBox1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            backportRemarkTextboxtoolStripTextBox1.Name="backportRemarkTextboxtoolStripTextBox1";
            backportRemarkTextboxtoolStripTextBox1.Size=new System.Drawing.Size(180, 23);
            // 
            // setRemarktoolStripMenuItem1
            // 
            setRemarktoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            setRemarktoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            setRemarktoolStripMenuItem1.Name="setRemarktoolStripMenuItem1";
            setRemarktoolStripMenuItem1.Size=new System.Drawing.Size(240, 22);
            setRemarktoolStripMenuItem1.Text="Set remarks";
            setRemarktoolStripMenuItem1.Click+=Backport_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator10.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator10.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator10.Name="toolStripSeparator10";
            toolStripSeparator10.Size=new System.Drawing.Size(205, 6);
            // 
            // removeBackportedtoolStripMenuItem1
            // 
            removeBackportedtoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            removeBackportedtoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            removeBackportedtoolStripMenuItem1.Name="removeBackportedtoolStripMenuItem1";
            removeBackportedtoolStripMenuItem1.Size=new System.Drawing.Size(208, 22);
            removeBackportedtoolStripMenuItem1.Text="Remove backported label";
            removeBackportedtoolStripMenuItem1.Click+=Backport_Click;
            // 
            // deletePKGtoolStripMenuItem1
            // 
            deletePKGtoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            deletePKGtoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            deletePKGtoolStripMenuItem1.Name="deletePKGtoolStripMenuItem1";
            deletePKGtoolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            deletePKGtoolStripMenuItem1.Text="Delete PKG";
            deletePKGtoolStripMenuItem1.Click+=DeletePKG_Click;
            // 
            // selectedExportPKGListToExcelToolStripMenuItem1
            // 
            selectedExportPKGListToExcelToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            selectedExportPKGListToExcelToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            selectedExportPKGListToExcelToolStripMenuItem1.ImageScaling=ToolStripItemImageScaling.None;
            selectedExportPKGListToExcelToolStripMenuItem1.Name="selectedExportPKGListToExcelToolStripMenuItem1";
            selectedExportPKGListToExcelToolStripMenuItem1.ShowShortcutKeys=false;
            selectedExportPKGListToExcelToolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            selectedExportPKGListToExcelToolStripMenuItem1.Text="Export as excel file";
            selectedExportPKGListToExcelToolStripMenuItem1.Click+=ExportPKGToExcel_Click;
            // 
            // toolStripMenuItem28
            // 
            toolStripMenuItem28.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem28.DropDownItems.AddRange(new ToolStripItem[] { selectedExtractImagesAndIconToolStripMenuItem1, selectedExtractImageOnlyToolStripMenuItem1, selectedExtractIconOnlyToolStripMenuItem1 });
            toolStripMenuItem28.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem28.ImageScaling=ToolStripItemImageScaling.None;
            toolStripMenuItem28.Name="toolStripMenuItem28";
            toolStripMenuItem28.ShowShortcutKeys=false;
            toolStripMenuItem28.Size=new System.Drawing.Size(254, 22);
            toolStripMenuItem28.Text="Extract images/icons";
            // 
            // selectedExtractImagesAndIconToolStripMenuItem1
            // 
            selectedExtractImagesAndIconToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            selectedExtractImagesAndIconToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            selectedExtractImagesAndIconToolStripMenuItem1.Name="selectedExtractImagesAndIconToolStripMenuItem1";
            selectedExtractImagesAndIconToolStripMenuItem1.Size=new System.Drawing.Size(200, 22);
            selectedExtractImagesAndIconToolStripMenuItem1.Text="Extract images and icon";
            selectedExtractImagesAndIconToolStripMenuItem1.Click+=ExtractImageIcon_Click;
            // 
            // selectedExtractImageOnlyToolStripMenuItem1
            // 
            selectedExtractImageOnlyToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            selectedExtractImageOnlyToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            selectedExtractImageOnlyToolStripMenuItem1.Name="selectedExtractImageOnlyToolStripMenuItem1";
            selectedExtractImageOnlyToolStripMenuItem1.Size=new System.Drawing.Size(200, 22);
            selectedExtractImageOnlyToolStripMenuItem1.Text="Extract image only";
            selectedExtractImageOnlyToolStripMenuItem1.Click+=ExtractImageIcon_Click;
            // 
            // selectedExtractIconOnlyToolStripMenuItem1
            // 
            selectedExtractIconOnlyToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            selectedExtractIconOnlyToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            selectedExtractIconOnlyToolStripMenuItem1.Name="selectedExtractIconOnlyToolStripMenuItem1";
            selectedExtractIconOnlyToolStripMenuItem1.Size=new System.Drawing.Size(200, 22);
            selectedExtractIconOnlyToolStripMenuItem1.Text="Extract icon only";
            selectedExtractIconOnlyToolStripMenuItem1.Click+=ExtractImageIcon_Click;
            // 
            // renameCurrentPKGStripMenuItem
            // 
            renameCurrentPKGStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameCurrentPKGStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { renameSelectedPkg1ToolStripMenuItem1, renameSelectedPkg2ToolStripMenuItem1, renameSelectedPkg3ToolStripMenuItem1, renameSelectedPkg4ToolStripMenuItem1, renameSelectedPkg5ToolStripMenuItem1, renameSelectedPkg6ToolStripMenuItem1, renameSelectedPkg7ToolStripMenuItem1, renameSelectedPkg8ToolStripMenuItem1, renameSelectedPkg9ToolStripMenuItem1, renameSelectedPkg10ToolStripMenuItem1, renameSelectedPkg11ToolStripMenuItem1 });
            renameCurrentPKGStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameCurrentPKGStripMenuItem.Name="renameCurrentPKGStripMenuItem";
            renameCurrentPKGStripMenuItem.Size=new System.Drawing.Size(254, 22);
            renameCurrentPKGStripMenuItem.Text="Rename PKG";
            // 
            // renameSelectedPkg1ToolStripMenuItem1
            // 
            renameSelectedPkg1ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg1ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg1ToolStripMenuItem1.Name="renameSelectedPkg1ToolStripMenuItem1";
            renameSelectedPkg1ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg1ToolStripMenuItem1.Text="TITILE";
            renameSelectedPkg1ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg2ToolStripMenuItem1
            // 
            renameSelectedPkg2ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg2ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg2ToolStripMenuItem1.Name="renameSelectedPkg2ToolStripMenuItem1";
            renameSelectedPkg2ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg2ToolStripMenuItem1.Text="TITLE [TITLE_ID]";
            renameSelectedPkg2ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg3ToolStripMenuItem1
            // 
            renameSelectedPkg3ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg3ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg3ToolStripMenuItem1.Name="renameSelectedPkg3ToolStripMenuItem1";
            renameSelectedPkg3ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg3ToolStripMenuItem1.Text="TITLE [TITLE_ID] [APP_VERSION]";
            renameSelectedPkg3ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg4ToolStripMenuItem1
            // 
            renameSelectedPkg4ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg4ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg4ToolStripMenuItem1.Name="renameSelectedPkg4ToolStripMenuItem1";
            renameSelectedPkg4ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg4ToolStripMenuItem1.Text="TITLE [CATEGORY]";
            renameSelectedPkg4ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg5ToolStripMenuItem1
            // 
            renameSelectedPkg5ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg5ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg5ToolStripMenuItem1.Name="renameSelectedPkg5ToolStripMenuItem1";
            renameSelectedPkg5ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg5ToolStripMenuItem1.Text="TITLE_ID";
            renameSelectedPkg5ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg6ToolStripMenuItem1
            // 
            renameSelectedPkg6ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg6ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg6ToolStripMenuItem1.Name="renameSelectedPkg6ToolStripMenuItem1";
            renameSelectedPkg6ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg6ToolStripMenuItem1.Text="TITLE_ID [TITLE]";
            renameSelectedPkg6ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg7ToolStripMenuItem1
            // 
            renameSelectedPkg7ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg7ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg7ToolStripMenuItem1.Name="renameSelectedPkg7ToolStripMenuItem1";
            renameSelectedPkg7ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg7ToolStripMenuItem1.Text="[TITLE_ID] [CATEGORY] [APP_VERSION] TITLE";
            renameSelectedPkg7ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg8ToolStripMenuItem1
            // 
            renameSelectedPkg8ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg8ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg8ToolStripMenuItem1.Name="renameSelectedPkg8ToolStripMenuItem1";
            renameSelectedPkg8ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg8ToolStripMenuItem1.Text="TITLE [CATEGORY] [APP_VERSION]";
            renameSelectedPkg8ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg9ToolStripMenuItem1
            // 
            renameSelectedPkg9ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg9ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg9ToolStripMenuItem1.Name="renameSelectedPkg9ToolStripMenuItem1";
            renameSelectedPkg9ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg9ToolStripMenuItem1.Text="CONTENT_ID";
            renameSelectedPkg9ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg10ToolStripMenuItem1
            // 
            renameSelectedPkg10ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg10ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg10ToolStripMenuItem1.Name="renameSelectedPkg10ToolStripMenuItem1";
            renameSelectedPkg10ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg10ToolStripMenuItem1.Text="CONTENT_ID 2";
            renameSelectedPkg10ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // renameSelectedPkg11ToolStripMenuItem1
            // 
            renameSelectedPkg11ToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            renameSelectedPkg11ToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            renameSelectedPkg11ToolStripMenuItem1.Name="renameSelectedPkg11ToolStripMenuItem1";
            renameSelectedPkg11ToolStripMenuItem1.Size=new System.Drawing.Size(309, 22);
            renameSelectedPkg11ToolStripMenuItem1.Text="CUSTOM NAME";
            renameSelectedPkg11ToolStripMenuItem1.Click+=RenamePkg_Click;
            // 
            // viewPkgChangeInfotoolStripMenuItem1
            // 
            viewPkgChangeInfotoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            viewPkgChangeInfotoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            viewPkgChangeInfotoolStripMenuItem1.Name="viewPkgChangeInfotoolStripMenuItem1";
            viewPkgChangeInfotoolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            viewPkgChangeInfotoolStripMenuItem1.Text="View PKG Change Info";
            viewPkgChangeInfotoolStripMenuItem1.Click+=ViewPatchChangelog_Click;
            // 
            // viewPkgExplorerStripMenuItem1
            // 
            viewPkgExplorerStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            viewPkgExplorerStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            viewPkgExplorerStripMenuItem1.Name="viewPkgExplorerStripMenuItem1";
            viewPkgExplorerStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            viewPkgExplorerStripMenuItem1.Text="View PKG in Explorer";
            viewPkgExplorerStripMenuItem1.Click+=ViewPKGExplorer_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripSeparator8.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripSeparator8.Margin=new Padding(0, 0, 0, 1);
            toolStripSeparator8.Name="toolStripSeparator8";
            toolStripSeparator8.Size=new System.Drawing.Size(251, 6);
            // 
            // toolStripMenuItem16
            // 
            toolStripMenuItem16.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem16.Enabled=false;
            toolStripMenuItem16.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripMenuItem16.ForeColor=System.Drawing.Color.FromArgb(153, 153, 153);
            toolStripMenuItem16.Name="toolStripMenuItem16";
            toolStripMenuItem16.Size=new System.Drawing.Size(254, 22);
            toolStripMenuItem16.Text="Remote PKG Installer | Status : Idle";
            // 
            // RpiCheckPkgInstalledtoolStripMenuItem1
            // 
            RpiCheckPkgInstalledtoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiCheckPkgInstalledtoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiCheckPkgInstalledtoolStripMenuItem1.Name="RpiCheckPkgInstalledtoolStripMenuItem1";
            RpiCheckPkgInstalledtoolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            RpiCheckPkgInstalledtoolStripMenuItem1.Text="Check if base PKG installed on PS4";
            RpiCheckPkgInstalledtoolStripMenuItem1.Click+=Rpi_Click;
            // 
            // RpiSendPkgtoolStripMenuItem1
            // 
            RpiSendPkgtoolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiSendPkgtoolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiSendPkgtoolStripMenuItem1.Name="RpiSendPkgtoolStripMenuItem1";
            RpiSendPkgtoolStripMenuItem1.Size=new System.Drawing.Size(254, 22);
            RpiSendPkgtoolStripMenuItem1.Text="Send PKG to PS4";
            RpiSendPkgtoolStripMenuItem1.Click+=Rpi_Click;
            // 
            // uninstallPKGFromPS4ToolStripMenuItem
            // 
            uninstallPKGFromPS4ToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            uninstallPKGFromPS4ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { RpiUninstallBasePKGToolStripMenuItem1, RpiUninstallPatchPKGToolStripMenuItem1, RpiUninstallDlcPKGToolStripMenuItem1, RpiUninstallThemePKGToolStripMenuItem1 });
            uninstallPKGFromPS4ToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            uninstallPKGFromPS4ToolStripMenuItem.Name="uninstallPKGFromPS4ToolStripMenuItem";
            uninstallPKGFromPS4ToolStripMenuItem.Size=new System.Drawing.Size(254, 22);
            uninstallPKGFromPS4ToolStripMenuItem.Text="Uninstall PKG from PS4";
            // 
            // RpiUninstallBasePKGToolStripMenuItem1
            // 
            RpiUninstallBasePKGToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiUninstallBasePKGToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiUninstallBasePKGToolStripMenuItem1.Name="RpiUninstallBasePKGToolStripMenuItem1";
            RpiUninstallBasePKGToolStripMenuItem1.Size=new System.Drawing.Size(228, 22);
            RpiUninstallBasePKGToolStripMenuItem1.Text="Uninstall base PKG";
            RpiUninstallBasePKGToolStripMenuItem1.Click+=Rpi_Click;
            // 
            // RpiUninstallPatchPKGToolStripMenuItem1
            // 
            RpiUninstallPatchPKGToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiUninstallPatchPKGToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiUninstallPatchPKGToolStripMenuItem1.Name="RpiUninstallPatchPKGToolStripMenuItem1";
            RpiUninstallPatchPKGToolStripMenuItem1.Size=new System.Drawing.Size(228, 22);
            RpiUninstallPatchPKGToolStripMenuItem1.Text="Uninstall patch PKG";
            RpiUninstallPatchPKGToolStripMenuItem1.Click+=Rpi_Click;
            // 
            // RpiUninstallDlcPKGToolStripMenuItem1
            // 
            RpiUninstallDlcPKGToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiUninstallDlcPKGToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiUninstallDlcPKGToolStripMenuItem1.Name="RpiUninstallDlcPKGToolStripMenuItem1";
            RpiUninstallDlcPKGToolStripMenuItem1.Size=new System.Drawing.Size(228, 22);
            RpiUninstallDlcPKGToolStripMenuItem1.Text="Uninstall addon PKG (DLC)";
            RpiUninstallDlcPKGToolStripMenuItem1.Click+=Rpi_Click;
            // 
            // RpiUninstallThemePKGToolStripMenuItem1
            // 
            RpiUninstallThemePKGToolStripMenuItem1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            RpiUninstallThemePKGToolStripMenuItem1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            RpiUninstallThemePKGToolStripMenuItem1.Name="RpiUninstallThemePKGToolStripMenuItem1";
            RpiUninstallThemePKGToolStripMenuItem1.Size=new System.Drawing.Size(228, 22);
            RpiUninstallThemePKGToolStripMenuItem1.Text="Uninstall addon PKG (Theme)";
            RpiUninstallThemePKGToolStripMenuItem1.Click+=Rpi_Click;
            // 
            // toolStripMenuItem144
            // 
            toolStripMenuItem144.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem144.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem159, toolStripMenuItem160, toolStripMenuItem158 });
            toolStripMenuItem144.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem144.Name="toolStripMenuItem144";
            toolStripMenuItem144.Size=new System.Drawing.Size(44, 20);
            toolStripMenuItem144.Text="Help";
            // 
            // toolStripMenuItem159
            // 
            toolStripMenuItem159.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem159.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem159.Name="toolStripMenuItem159";
            toolStripMenuItem159.Size=new System.Drawing.Size(165, 22);
            toolStripMenuItem159.Text="About";
            toolStripMenuItem159.Click+=toolStripMenuItem159_Click;
            // 
            // toolStripMenuItem160
            // 
            toolStripMenuItem160.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem160.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem160.Name="toolStripMenuItem160";
            toolStripMenuItem160.Size=new System.Drawing.Size(165, 22);
            toolStripMenuItem160.Text="Buy me a coffee";
            toolStripMenuItem160.Click+=toolStripMenuItem160_Click;
            // 
            // toolStripMenuItem158
            // 
            toolStripMenuItem158.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem158.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem158.Name="toolStripMenuItem158";
            toolStripMenuItem158.Size=new System.Drawing.Size(165, 22);
            toolStripMenuItem158.Text="Check for update";
            toolStripMenuItem158.Click+=toolStripMenuItem158_Click;
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { colorTestToolStripMenuItem });
            testToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            testToolStripMenuItem.Name="testToolStripMenuItem";
            testToolStripMenuItem.Size=new System.Drawing.Size(38, 20);
            testToolStripMenuItem.Text="test";
            testToolStripMenuItem.Visible=false;
            // 
            // colorTestToolStripMenuItem
            // 
            colorTestToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            colorTestToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            colorTestToolStripMenuItem.Name="colorTestToolStripMenuItem";
            colorTestToolStripMenuItem.Size=new System.Drawing.Size(123, 22);
            colorTestToolStripMenuItem.Text="color test";
            colorTestToolStripMenuItem.Click+=colorTestToolStripMenuItem_Click;
            // 
            // darkStatusStrip1
            // 
            darkStatusStrip1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            darkStatusStrip1.Font=new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkStatusStrip1.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            darkStatusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel3, toolStripStatusLabel5, toolStripProgressBar1, toolStripStatusLabel2, labelDisplayTotalPKG, ToolStripSplitButtonTotalPKG, toolStripSplitButton1, toolStripStatusLabel4 });
            darkStatusStrip1.Location=new System.Drawing.Point(0, 633);
            darkStatusStrip1.Name="darkStatusStrip1";
            darkStatusStrip1.Padding=new Padding(0, 5, 0, 2);
            darkStatusStrip1.Size=new System.Drawing.Size(984, 28);
            darkStatusStrip1.TabIndex=85;
            darkStatusStrip1.Text="darkStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name="toolStripStatusLabel1";
            toolStripStatusLabel1.Size=new System.Drawing.Size(13, 16);
            toolStripStatusLabel1.Text="  ";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.ForeColor=System.Drawing.Color.Silver;
            toolStripStatusLabel3.Name="toolStripStatusLabel3";
            toolStripStatusLabel3.Size=new System.Drawing.Size(0, 16);
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.Name="toolStripStatusLabel5";
            toolStripStatusLabel5.Size=new System.Drawing.Size(0, 16);
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name="toolStripProgressBar1";
            toolStripProgressBar1.Size=new System.Drawing.Size(298, 15);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.DisplayStyle=ToolStripItemDisplayStyle.Text;
            toolStripStatusLabel2.Font=new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripStatusLabel2.ForeColor=System.Drawing.Color.Silver;
            toolStripStatusLabel2.Name="toolStripStatusLabel2";
            toolStripStatusLabel2.Size=new System.Drawing.Size(474, 16);
            toolStripStatusLabel2.Spring=true;
            toolStripStatusLabel2.Text="... ";
            toolStripStatusLabel2.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDisplayTotalPKG
            // 
            labelDisplayTotalPKG.Name="labelDisplayTotalPKG";
            labelDisplayTotalPKG.Size=new System.Drawing.Size(16, 16);
            labelDisplayTotalPKG.Text="...";
            // 
            // ToolStripSplitButtonTotalPKG
            // 
            ToolStripSplitButtonTotalPKG.ForeColor=System.Drawing.Color.Silver;
            ToolStripSplitButtonTotalPKG.Name="ToolStripSplitButtonTotalPKG";
            ToolStripSplitButtonTotalPKG.Size=new System.Drawing.Size(72, 19);
            ToolStripSplitButtonTotalPKG.Text="Filter PKG";
            // 
            // toolStripSplitButton1
            // 
            toolStripSplitButton1.DisplayStyle=ToolStripItemDisplayStyle.Text;
            toolStripSplitButton1.ImageTransparentColor=System.Drawing.Color.Magenta;
            toolStripSplitButton1.Name="toolStripSplitButton1";
            toolStripSplitButton1.Size=new System.Drawing.Size(96, 19);
            toolStripSplitButton1.Text="Hard disk info";
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.Name="toolStripStatusLabel4";
            toolStripStatusLabel4.Size=new System.Drawing.Size(13, 16);
            toolStripStatusLabel4.Text="  ";
            // 
            // contextMenuTrophy
            // 
            contextMenuTrophy.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            contextMenuTrophy.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            contextMenuTrophy.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            contextMenuTrophy.Items.AddRange(new ToolStripItem[] { ExtractTrophyImageToolStripMenuItem });
            contextMenuTrophy.Name="DarkContextMenu1";
            contextMenuTrophy.Size=new System.Drawing.Size(175, 26);
            // 
            // ExtractTrophyImageToolStripMenuItem
            // 
            ExtractTrophyImageToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            ExtractTrophyImageToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            ExtractTrophyImageToolStripMenuItem.Name="ExtractTrophyImageToolStripMenuItem";
            ExtractTrophyImageToolStripMenuItem.Size=new System.Drawing.Size(174, 22);
            ExtractTrophyImageToolStripMenuItem.Text="Extract trophy icon";
            ExtractTrophyImageToolStripMenuItem.Click+=ExtractTrophyImageToolStripMenuItem_Click;
            // 
            // contextMenuEntry
            // 
            contextMenuEntry.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            contextMenuEntry.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            contextMenuEntry.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            contextMenuEntry.Items.AddRange(new ToolStripItem[] { ExtractAllEntryToolStripMenuItem, ExtractDecryptedEntryToolStripMenuItem });
            contextMenuEntry.Name="DarkContextMenuBitmap";
            contextMenuEntry.Size=new System.Drawing.Size(212, 48);
            // 
            // ExtractAllEntryToolStripMenuItem
            // 
            ExtractAllEntryToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            ExtractAllEntryToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            ExtractAllEntryToolStripMenuItem.Name="ExtractAllEntryToolStripMenuItem";
            ExtractAllEntryToolStripMenuItem.Size=new System.Drawing.Size(211, 22);
            ExtractAllEntryToolStripMenuItem.Text="Extract all item";
            ExtractAllEntryToolStripMenuItem.Click+=ExtractAllEntryToolStripMenuItem_Click;
            // 
            // ExtractDecryptedEntryToolStripMenuItem
            // 
            ExtractDecryptedEntryToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            ExtractDecryptedEntryToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            ExtractDecryptedEntryToolStripMenuItem.Name="ExtractDecryptedEntryToolStripMenuItem";
            ExtractDecryptedEntryToolStripMenuItem.Size=new System.Drawing.Size(211, 22);
            ExtractDecryptedEntryToolStripMenuItem.Text="Extract all decrypted entry";
            ExtractDecryptedEntryToolStripMenuItem.Click+=ExtractDecryptedEntryToolStripMenuItem_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth=ColorDepth.Depth8Bit;
            imageList1.ImageStream=(ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor=System.Drawing.Color.Transparent;
            imageList1.Images.SetKeyName(0, "Folder_16x.png");
            imageList1.Images.SetKeyName(1, "Document_16x.png");
            // 
            // contextMenuOfficialUpdate
            // 
            contextMenuOfficialUpdate.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            contextMenuOfficialUpdate.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            contextMenuOfficialUpdate.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            contextMenuOfficialUpdate.Items.AddRange(new ToolStripItem[] { copyURLToolStripMenuItem, downloadSelectedPKGUpdateToolStripMenuItem });
            contextMenuOfficialUpdate.Name="DarkContextMenuPkgUpdate";
            contextMenuOfficialUpdate.Size=new System.Drawing.Size(215, 48);
            // 
            // copyURLToolStripMenuItem
            // 
            copyURLToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            copyURLToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            copyURLToolStripMenuItem.Name="copyURLToolStripMenuItem";
            copyURLToolStripMenuItem.Size=new System.Drawing.Size(214, 22);
            copyURLToolStripMenuItem.Text="Copy all url";
            copyURLToolStripMenuItem.Click+=copyURLToolStripMenuItem_Click;
            // 
            // downloadSelectedPKGUpdateToolStripMenuItem
            // 
            downloadSelectedPKGUpdateToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            downloadSelectedPKGUpdateToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            downloadSelectedPKGUpdateToolStripMenuItem.Name="downloadSelectedPKGUpdateToolStripMenuItem";
            downloadSelectedPKGUpdateToolStripMenuItem.Size=new System.Drawing.Size(214, 22);
            downloadSelectedPKGUpdateToolStripMenuItem.Text="Download selected update";
            downloadSelectedPKGUpdateToolStripMenuItem.Click+=downloadSelectedPKGUpdateToolStripMenuItem_Click;
            // 
            // contextMenuBackgroundImage
            // 
            contextMenuBackgroundImage.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            contextMenuBackgroundImage.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            contextMenuBackgroundImage.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            contextMenuBackgroundImage.Items.AddRange(new ToolStripItem[] { saveImageToolStripMenuItem, SetImageAsDesktopBackgroundToolStripMenuItem });
            contextMenuBackgroundImage.Name="BitmapPIC0DarkContextMenu";
            contextMenuBackgroundImage.Size=new System.Drawing.Size(204, 48);
            // 
            // saveImageToolStripMenuItem
            // 
            saveImageToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            saveImageToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            saveImageToolStripMenuItem.Name="saveImageToolStripMenuItem";
            saveImageToolStripMenuItem.Size=new System.Drawing.Size(203, 22);
            saveImageToolStripMenuItem.Text="Save image";
            saveImageToolStripMenuItem.Click+=ContextMenuBackgroundImage_Click;
            // 
            // SetImageAsDesktopBackgroundToolStripMenuItem
            // 
            SetImageAsDesktopBackgroundToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            SetImageAsDesktopBackgroundToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            SetImageAsDesktopBackgroundToolStripMenuItem.Name="SetImageAsDesktopBackgroundToolStripMenuItem";
            SetImageAsDesktopBackgroundToolStripMenuItem.Size=new System.Drawing.Size(203, 22);
            SetImageAsDesktopBackgroundToolStripMenuItem.Text="Set as desktop wallpaper";
            SetImageAsDesktopBackgroundToolStripMenuItem.Click+=ContextMenuBackgroundImage_Click;
            // 
            // contextMenuExtractNode
            // 
            contextMenuExtractNode.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            contextMenuExtractNode.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            contextMenuExtractNode.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            contextMenuExtractNode.Items.AddRange(new ToolStripItem[] { extractNodeToolStripMenuItem, expandAllToolStripMenuItem, collapseAllNodeToolStripMenuItem });
            contextMenuExtractNode.Name="DarkContextMenuExtractNode";
            contextMenuExtractNode.Size=new System.Drawing.Size(184, 70);
            // 
            // extractNodeToolStripMenuItem
            // 
            extractNodeToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            extractNodeToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            extractNodeToolStripMenuItem.Name="extractNodeToolStripMenuItem";
            extractNodeToolStripMenuItem.Size=new System.Drawing.Size(183, 22);
            extractNodeToolStripMenuItem.Text="Extract selected item";
            extractNodeToolStripMenuItem.Visible=false;
            extractNodeToolStripMenuItem.Click+=extractToToolStripMenuItem_Click;
            // 
            // expandAllToolStripMenuItem
            // 
            expandAllToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            expandAllToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            expandAllToolStripMenuItem.Name="expandAllToolStripMenuItem";
            expandAllToolStripMenuItem.Size=new System.Drawing.Size(183, 22);
            expandAllToolStripMenuItem.Text="Expand all node";
            expandAllToolStripMenuItem.Click+=expandAllToolStripMenuItem_Click;
            // 
            // collapseAllNodeToolStripMenuItem
            // 
            collapseAllNodeToolStripMenuItem.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            collapseAllNodeToolStripMenuItem.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            collapseAllNodeToolStripMenuItem.Name="collapseAllNodeToolStripMenuItem";
            collapseAllNodeToolStripMenuItem.Size=new System.Drawing.Size(183, 22);
            collapseAllNodeToolStripMenuItem.Text="Collapse all node";
            collapseAllNodeToolStripMenuItem.Click+=collapseAllNodeToolStripMenuItem_Click;
            // 
            // columnHeader1
            // 
            columnHeader1.Text="Name";
            // 
            // columnHeader5
            // 
            columnHeader5.Text="Type";
            columnHeader5.TextAlign=HorizontalAlignment.Center;
            // 
            // columnHeader6
            // 
            columnHeader6.Text="Path";
            columnHeader6.TextAlign=HorizontalAlignment.Center;
            // 
            // darkSectionPanel7
            // 
            darkSectionPanel7.Dock=DockStyle.Fill;
            darkSectionPanel7.Location=new System.Drawing.Point(6, 6);
            darkSectionPanel7.Name="darkSectionPanel7";
            darkSectionPanel7.SectionHeader=null;
            darkSectionPanel7.Size=new System.Drawing.Size(288, 225);
            darkSectionPanel7.TabIndex=95;
            // 
            // contextMenuExtractListView
            // 
            contextMenuExtractListView.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            contextMenuExtractListView.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            contextMenuExtractListView.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            contextMenuExtractListView.Items.AddRange(new ToolStripItem[] { toolStripMenuItem32 });
            contextMenuExtractListView.Name="DarkContextMenuExtractFromListView";
            contextMenuExtractListView.Size=new System.Drawing.Size(184, 26);
            // 
            // toolStripMenuItem32
            // 
            toolStripMenuItem32.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            toolStripMenuItem32.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            toolStripMenuItem32.Name="toolStripMenuItem32";
            toolStripMenuItem32.Size=new System.Drawing.Size(183, 22);
            toolStripMenuItem32.Text="Extract selected item";
            toolStripMenuItem32.Click+=toolStripMenuItem32_Click;
            // 
            // flatTabControl1
            // 
            flatTabControl1.ActiveColor=System.Drawing.Color.Gainsboro;
            flatTabControl1.AllowDrop=true;
            flatTabControl1.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            flatTabControl1.Appearance=TabAppearance.FlatButtons;
            flatTabControl1.BackTabColor=System.Drawing.Color.FromArgb(60, 63, 65);
            flatTabControl1.BorderColor=System.Drawing.Color.Gainsboro;
            flatTabControl1.ClosingButtonColor=System.Drawing.Color.WhiteSmoke;
            flatTabControl1.ClosingMessage=null;
            flatTabControl1.Controls.Add(tabPage1);
            flatTabControl1.Controls.Add(tabPage6);
            flatTabControl1.Controls.Add(tabPage2);
            flatTabControl1.Controls.Add(tabPage3);
            flatTabControl1.Controls.Add(tabPage4);
            flatTabControl1.Controls.Add(tabPage7);
            flatTabControl1.Controls.Add(tabPage5);
            flatTabControl1.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            flatTabControl1.HeaderColor=System.Drawing.Color.FromArgb(60, 63, 65);
            flatTabControl1.HorizontalLineColor=System.Drawing.Color.Gainsboro;
            flatTabControl1.ItemSize=new System.Drawing.Size(240, 16);
            flatTabControl1.Location=new System.Drawing.Point(12, 38);
            flatTabControl1.Margin=new Padding(3, 2, 3, 2);
            flatTabControl1.Name="flatTabControl1";
            flatTabControl1.SelectedIndex=0;
            flatTabControl1.SelectedTextColor=System.Drawing.Color.FromArgb(60, 63, 65);
            flatTabControl1.ShowClosingButton=false;
            flatTabControl1.ShowClosingMessage=false;
            flatTabControl1.Size=new System.Drawing.Size(959, 592);
            flatTabControl1.TabIndex=91;
            flatTabControl1.TextColor=System.Drawing.Color.FromArgb(255, 255, 255);
            // 
            // tabPage1
            // 
            tabPage1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPage1.Controls.Add(darkButton3);
            tabPage1.Controls.Add(darkSectionPanel10);
            tabPage1.Controls.Add(darkSectionPanel9);
            tabPage1.Controls.Add(PKGGridView);
            tabPage1.Controls.Add(darkSectionPanel8);
            tabPage1.Controls.Add(darkLabel2);
            tabPage1.Controls.Add(tbSearchGame);
            tabPage1.ForeColor=System.Drawing.Color.Gainsboro;
            tabPage1.Location=new System.Drawing.Point(4, 20);
            tabPage1.Margin=new Padding(3, 2, 3, 2);
            tabPage1.Name="tabPage1";
            tabPage1.Padding=new Padding(3, 2, 3, 2);
            tabPage1.Size=new System.Drawing.Size(951, 568);
            tabPage1.TabIndex=0;
            tabPage1.Text="PKG List";
            // 
            // darkButton3
            // 
            darkButton3.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkButton3.Location=new System.Drawing.Point(432, 11);
            darkButton3.Margin=new Padding(3, 2, 3, 2);
            darkButton3.Name="darkButton3";
            darkButton3.Size=new System.Drawing.Size(75, 21);
            darkButton3.TabIndex=93;
            darkButton3.Text="Clear";
            darkButton3.Click+=darkButton3_Click;
            // 
            // darkSectionPanel10
            // 
            darkSectionPanel10.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Right;
            darkSectionPanel10.Controls.Add(darkDataGridView2);
            darkSectionPanel10.Location=new System.Drawing.Point(687, 364);
            darkSectionPanel10.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel10.Name="darkSectionPanel10";
            darkSectionPanel10.SectionHeader="Param.sfo";
            darkSectionPanel10.Size=new System.Drawing.Size(259, 196);
            darkSectionPanel10.TabIndex=90;
            // 
            // darkDataGridView2
            // 
            darkDataGridView2.AllowDrop=true;
            darkDataGridView2.AllowUserToAddRows=false;
            darkDataGridView2.AllowUserToDeleteRows=false;
            darkDataGridView2.AllowUserToDragDropRows=false;
            darkDataGridView2.AllowUserToPasteCells=false;
            darkDataGridView2.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            darkDataGridView2.ColumnHeadersHeight=4;
            darkDataGridView2.Dock=DockStyle.Fill;
            darkDataGridView2.Location=new System.Drawing.Point(1, 25);
            darkDataGridView2.Margin=new Padding(3, 4, 3, 4);
            darkDataGridView2.MultiSelect=false;
            darkDataGridView2.Name="darkDataGridView2";
            darkDataGridView2.ReadOnly=true;
            darkDataGridView2.RowHeadersWidth=41;
            darkDataGridView2.RowTemplate.Height=23;
            darkDataGridView2.ScrollBars=ScrollBars.Horizontal;
            darkDataGridView2.Size=new System.Drawing.Size(257, 170);
            darkDataGridView2.TabIndex=76;
            // 
            // darkSectionPanel9
            // 
            darkSectionPanel9.Anchor=AnchorStyles.Top|AnchorStyles.Right;
            darkSectionPanel9.Controls.Add(panel1);
            darkSectionPanel9.Location=new System.Drawing.Point(687, 285);
            darkSectionPanel9.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel9.Name="darkSectionPanel9";
            darkSectionPanel9.SectionHeader="Title";
            darkSectionPanel9.Size=new System.Drawing.Size(259, 75);
            darkSectionPanel9.TabIndex=89;
            // 
            // panel1
            // 
            panel1.BorderStyle=BorderStyle.FixedSingle;
            panel1.Controls.Add(darkLabel1);
            panel1.Dock=DockStyle.Fill;
            panel1.Location=new System.Drawing.Point(1, 25);
            panel1.Margin=new Padding(3, 2, 3, 2);
            panel1.Name="panel1";
            panel1.Size=new System.Drawing.Size(257, 49);
            panel1.TabIndex=70;
            // 
            // darkLabel1
            // 
            darkLabel1.BackColor=System.Drawing.Color.Transparent;
            darkLabel1.Dock=DockStyle.Fill;
            darkLabel1.Enabled=false;
            darkLabel1.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkLabel1.ForeColor=System.Drawing.Color.Silver;
            darkLabel1.Location=new System.Drawing.Point(0, 0);
            darkLabel1.Name="darkLabel1";
            darkLabel1.Size=new System.Drawing.Size(255, 47);
            darkLabel1.TabIndex=69;
            darkLabel1.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PKGGridView
            // 
            PKGGridView.AllowUserToAddRows=false;
            PKGGridView.AllowUserToDeleteRows=false;
            PKGGridView.AllowUserToDragDropRows=false;
            PKGGridView.AllowUserToPasteCells=false;
            PKGGridView.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            PKGGridView.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            PKGGridView.ColumnHeadersHeight=4;
            PKGGridView.ContextMenuStrip=contextMenuPKGGridView;
            PKGGridView.Location=new System.Drawing.Point(5, 40);
            PKGGridView.Name="PKGGridView";
            PKGGridView.ReadOnly=true;
            PKGGridView.RowHeadersWidth=41;
            PKGGridView.RowTemplate.Height=23;
            PKGGridView.ScrollBars=ScrollBars.Horizontal;
            PKGGridView.Size=new System.Drawing.Size(678, 520);
            PKGGridView.TabIndex=75;
            PKGGridView.CellFormatting+=PKGListGridView_CellFormatting;
            PKGGridView.ColumnHeaderMouseClick+=PKGListGridView_ColumnHeaderMouseClick;
            PKGGridView.SelectionChanged+=PKGListGridView_SelectionChanged;
            // 
            // darkSectionPanel8
            // 
            darkSectionPanel8.Anchor=AnchorStyles.Top|AnchorStyles.Right;
            darkSectionPanel8.BorderStyle=BorderStyle.FixedSingle;
            darkSectionPanel8.Controls.Add(panel5);
            darkSectionPanel8.Location=new System.Drawing.Point(687, 9);
            darkSectionPanel8.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel8.Name="darkSectionPanel8";
            darkSectionPanel8.SectionHeader="Avatar";
            darkSectionPanel8.Size=new System.Drawing.Size(260, 272);
            darkSectionPanel8.TabIndex=88;
            darkSectionPanel8.Tag="";
            // 
            // panel5
            // 
            panel5.BorderStyle=BorderStyle.FixedSingle;
            panel5.Controls.Add(pictureBox1);
            panel5.Controls.Add(label3);
            panel5.Dock=DockStyle.Fill;
            panel5.Location=new System.Drawing.Point(1, 25);
            panel5.Margin=new Padding(3, 2, 3, 2);
            panel5.Name="panel5";
            panel5.Size=new System.Drawing.Size(256, 244);
            panel5.TabIndex=79;
            panel5.Paint+=panel5_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor=System.Drawing.Color.Transparent;
            pictureBox1.BorderStyle=BorderStyle.FixedSingle;
            pictureBox1.Dock=DockStyle.Fill;
            pictureBox1.Location=new System.Drawing.Point(0, 0);
            pictureBox1.Margin=new Padding(3, 2, 3, 2);
            pictureBox1.Name="pictureBox1";
            pictureBox1.Size=new System.Drawing.Size(254, 242);
            pictureBox1.SizeMode=PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex=59;
            pictureBox1.TabStop=false;
            // 
            // label3
            // 
            label3.Anchor=AnchorStyles.None;
            label3.Font=new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label3.ForeColor=System.Drawing.Color.Silver;
            label3.Location=new System.Drawing.Point(3, 98);
            label3.Name="label3";
            label3.Size=new System.Drawing.Size(249, 46);
            label3.TabIndex=70;
            label3.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize=true;
            darkLabel2.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkLabel2.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel2.Location=new System.Drawing.Point(6, 14);
            darkLabel2.Name="darkLabel2";
            darkLabel2.Size=new System.Drawing.Size(214, 15);
            darkLabel2.TabIndex=88;
            darkLabel2.Text="Filter [Filename]/[Title ID]/[Content ID]:";
            // 
            // tbSearchGame
            // 
            tbSearchGame.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tbSearchGame.Location=new System.Drawing.Point(223, 11);
            tbSearchGame.Margin=new Padding(3, 2, 3, 2);
            tbSearchGame.Name="tbSearchGame";
            tbSearchGame.Size=new System.Drawing.Size(203, 23);
            tbSearchGame.TabIndex=86;
            tbSearchGame.TextAlign=HorizontalAlignment.Center;
            tbSearchGame.TextChanged+=TbSearchGame_TextChanged;
            // 
            // tabPage6
            // 
            tabPage6.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPage6.Controls.Add(treeView1);
            tabPage6.Controls.Add(listView2);
            tabPage6.Controls.Add(PKGListView);
            tabPage6.Location=new System.Drawing.Point(4, 20);
            tabPage6.Name="tabPage6";
            tabPage6.Size=new System.Drawing.Size(951, 568);
            tabPage6.TabIndex=7;
            tabPage6.Text="PKG List View";
            // 
            // treeView1
            // 
            treeView1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            treeView1.BorderStyle=BorderStyle.None;
            treeView1.ForeColor=System.Drawing.Color.Gainsboro;
            treeView1.Location=new System.Drawing.Point(235, 54);
            treeView1.Margin=new Padding(3, 4, 3, 4);
            treeView1.Name="treeView1";
            treeView1.Size=new System.Drawing.Size(402, 457);
            treeView1.TabIndex=94;
            // 
            // listView2
            // 
            listView2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            listView2.ForeColor=System.Drawing.Color.Gainsboro;
            listView2.Location=new System.Drawing.Point(613, 13);
            listView2.MaxDragChange=20;
            listView2.Name="listView2";
            listView2.Size=new System.Drawing.Size(284, 568);
            listView2.TabIndex=2;
            // 
            // PKGListView
            // 
            PKGListView.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            PKGListView.ForeColor=System.Drawing.Color.Gainsboro;
            PKGListView.FullRowSelect=true;
            PKGListView.Location=new System.Drawing.Point(0, 0);
            PKGListView.Name="PKGListView";
            PKGListView.Size=new System.Drawing.Size(245, 568);
            PKGListView.TabIndex=1;
            PKGListView.UseCompatibleStateImageBehavior=false;
            PKGListView.View=View.Details;
            // 
            // tabPage2
            // 
            tabPage2.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPage2.Controls.Add(TrophyGridView);
            tabPage2.ForeColor=System.Drawing.Color.Gainsboro;
            tabPage2.Location=new System.Drawing.Point(4, 20);
            tabPage2.Margin=new Padding(3, 2, 3, 2);
            tabPage2.Name="tabPage2";
            tabPage2.Padding=new Padding(3, 2, 3, 2);
            tabPage2.Size=new System.Drawing.Size(951, 568);
            tabPage2.TabIndex=1;
            tabPage2.Text="Trophy";
            // 
            // TrophyGridView
            // 
            TrophyGridView.AllowUserToAddRows=false;
            TrophyGridView.AllowUserToDeleteRows=false;
            TrophyGridView.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            TrophyGridView.ColumnHeadersHeight=4;
            TrophyGridView.ContextMenuStrip=contextMenuTrophy;
            TrophyGridView.Dock=DockStyle.Fill;
            TrophyGridView.Location=new System.Drawing.Point(3, 2);
            TrophyGridView.Margin=new Padding(3, 4, 3, 4);
            TrophyGridView.MultiSelect=false;
            TrophyGridView.Name="TrophyGridView";
            TrophyGridView.ReadOnly=true;
            TrophyGridView.RowHeadersWidth=41;
            TrophyGridView.RowTemplate.Height=23;
            TrophyGridView.ScrollBars=ScrollBars.Horizontal;
            TrophyGridView.Size=new System.Drawing.Size(945, 564);
            TrophyGridView.TabIndex=76;
            TrophyGridView.SelectionChanged+=darkDataGridView3_SelectionChanged;
            // 
            // tabPage3
            // 
            tabPage3.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPage3.Controls.Add(flatTabControlBgi);
            tabPage3.ForeColor=System.Drawing.Color.Gainsboro;
            tabPage3.Location=new System.Drawing.Point(4, 20);
            tabPage3.Margin=new Padding(3, 2, 3, 2);
            tabPage3.Name="tabPage3";
            tabPage3.Size=new System.Drawing.Size(951, 568);
            tabPage3.TabIndex=2;
            tabPage3.Text="Background Image";
            // 
            // flatTabControlBgi
            // 
            flatTabControlBgi.ActiveColor=System.Drawing.Color.Gainsboro;
            flatTabControlBgi.AllowDrop=true;
            flatTabControlBgi.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            flatTabControlBgi.Appearance=TabAppearance.FlatButtons;
            flatTabControlBgi.BackTabColor=System.Drawing.Color.FromArgb(60, 63, 65);
            flatTabControlBgi.BorderColor=System.Drawing.Color.Gainsboro;
            flatTabControlBgi.ClosingButtonColor=System.Drawing.Color.WhiteSmoke;
            flatTabControlBgi.ClosingMessage=null;
            flatTabControlBgi.Controls.Add(tabPagePic0);
            flatTabControlBgi.Controls.Add(tabPagePic1);
            flatTabControlBgi.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            flatTabControlBgi.HeaderColor=System.Drawing.Color.FromArgb(60, 63, 65);
            flatTabControlBgi.HorizontalLineColor=System.Drawing.Color.Gainsboro;
            flatTabControlBgi.ItemSize=new System.Drawing.Size(240, 16);
            flatTabControlBgi.Location=new System.Drawing.Point(16, 18);
            flatTabControlBgi.Margin=new Padding(3, 2, 3, 2);
            flatTabControlBgi.Name="flatTabControlBgi";
            flatTabControlBgi.SelectedIndex=0;
            flatTabControlBgi.SelectedTextColor=System.Drawing.Color.FromArgb(60, 63, 65);
            flatTabControlBgi.ShowClosingButton=false;
            flatTabControlBgi.ShowClosingMessage=false;
            flatTabControlBgi.Size=new System.Drawing.Size(919, 532);
            flatTabControlBgi.TabIndex=91;
            flatTabControlBgi.TextColor=System.Drawing.Color.FromArgb(255, 255, 255);
            // 
            // tabPagePic0
            // 
            tabPagePic0.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPagePic0.Controls.Add(darkLabel3);
            tabPagePic0.Controls.Add(pbPIC0);
            tabPagePic0.Location=new System.Drawing.Point(4, 20);
            tabPagePic0.Margin=new Padding(3, 2, 3, 2);
            tabPagePic0.Name="tabPagePic0";
            tabPagePic0.Padding=new Padding(3, 2, 3, 2);
            tabPagePic0.Size=new System.Drawing.Size(911, 508);
            tabPagePic0.TabIndex=0;
            tabPagePic0.Text="PIC0.PNG";
            // 
            // darkLabel3
            // 
            darkLabel3.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            darkLabel3.Font=new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            darkLabel3.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel3.Location=new System.Drawing.Point(92, 21);
            darkLabel3.Name="darkLabel3";
            darkLabel3.Size=new System.Drawing.Size(727, 466);
            darkLabel3.TabIndex=1;
            darkLabel3.Text="Image not available";
            darkLabel3.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
            darkLabel3.Visible=false;
            // 
            // pbPIC0
            // 
            pbPIC0.BackColor=System.Drawing.Color.Transparent;
            pbPIC0.Dock=DockStyle.Fill;
            pbPIC0.Location=new System.Drawing.Point(3, 2);
            pbPIC0.Margin=new Padding(3, 2, 3, 2);
            pbPIC0.Name="pbPIC0";
            pbPIC0.Size=new System.Drawing.Size(905, 504);
            pbPIC0.SizeMode=PictureBoxSizeMode.Zoom;
            pbPIC0.TabIndex=0;
            pbPIC0.TabStop=false;
            // 
            // tabPagePic1
            // 
            tabPagePic1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPagePic1.Controls.Add(darkLabel4);
            tabPagePic1.Controls.Add(pbPIC1);
            tabPagePic1.Location=new System.Drawing.Point(4, 20);
            tabPagePic1.Margin=new Padding(3, 2, 3, 2);
            tabPagePic1.Name="tabPagePic1";
            tabPagePic1.Padding=new Padding(3, 2, 3, 2);
            tabPagePic1.Size=new System.Drawing.Size(911, 508);
            tabPagePic1.TabIndex=1;
            tabPagePic1.Text="PIC1.PNG";
            // 
            // darkLabel4
            // 
            darkLabel4.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            darkLabel4.Font=new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            darkLabel4.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel4.Location=new System.Drawing.Point(92, 21);
            darkLabel4.Name="darkLabel4";
            darkLabel4.Size=new System.Drawing.Size(0, 4);
            darkLabel4.TabIndex=2;
            darkLabel4.Text="Image not available";
            darkLabel4.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
            darkLabel4.Visible=false;
            // 
            // pbPIC1
            // 
            pbPIC1.BackColor=System.Drawing.Color.Transparent;
            pbPIC1.Dock=DockStyle.Fill;
            pbPIC1.Location=new System.Drawing.Point(3, 2);
            pbPIC1.Margin=new Padding(3, 2, 3, 2);
            pbPIC1.Name="pbPIC1";
            pbPIC1.Size=new System.Drawing.Size(905, 504);
            pbPIC1.TabIndex=0;
            pbPIC1.TabStop=false;
            // 
            // tabPage4
            // 
            tabPage4.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPage4.Controls.Add(panel7);
            tabPage4.ForeColor=System.Drawing.Color.Gainsboro;
            tabPage4.Location=new System.Drawing.Point(4, 20);
            tabPage4.Margin=new Padding(3, 2, 3, 2);
            tabPage4.Name="tabPage4";
            tabPage4.Size=new System.Drawing.Size(951, 568);
            tabPage4.TabIndex=3;
            tabPage4.Text="Additional Info";
            // 
            // panel7
            // 
            panel7.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            panel7.Controls.Add(darkSectionPanel2);
            panel7.Controls.Add(darkSectionPanel3);
            panel7.Controls.Add(darkSectionPanel1);
            panel7.Location=new System.Drawing.Point(25, 29);
            panel7.Margin=new Padding(3, 2, 3, 2);
            panel7.Name="panel7";
            panel7.Size=new System.Drawing.Size(901, 526);
            panel7.TabIndex=98;
            // 
            // darkSectionPanel2
            // 
            darkSectionPanel2.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            darkSectionPanel2.Controls.Add(dgvEntryList);
            darkSectionPanel2.Location=new System.Drawing.Point(259, 98);
            darkSectionPanel2.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel2.Name="darkSectionPanel2";
            darkSectionPanel2.SectionHeader="Entry List";
            darkSectionPanel2.Size=new System.Drawing.Size(642, 429);
            darkSectionPanel2.TabIndex=95;
            // 
            // dgvEntryList
            // 
            dgvEntryList.AllowUserToAddRows=false;
            dgvEntryList.AllowUserToDeleteRows=false;
            dgvEntryList.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            dgvEntryList.ColumnHeadersHeight=4;
            dgvEntryList.ContextMenuStrip=contextMenuEntry;
            dgvEntryList.Dock=DockStyle.Fill;
            dgvEntryList.Location=new System.Drawing.Point(1, 25);
            dgvEntryList.Margin=new Padding(3, 4, 3, 4);
            dgvEntryList.MultiSelect=false;
            dgvEntryList.Name="dgvEntryList";
            dgvEntryList.ReadOnly=true;
            dgvEntryList.RowHeadersWidth=41;
            dgvEntryList.RowTemplate.Height=23;
            dgvEntryList.Size=new System.Drawing.Size(640, 403);
            dgvEntryList.TabIndex=77;
            dgvEntryList.SelectionChanged+=dgvEntryList_SelectionChanged;
            // 
            // darkSectionPanel3
            // 
            darkSectionPanel3.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            darkSectionPanel3.Controls.Add(darkDataGridView4);
            darkSectionPanel3.Location=new System.Drawing.Point(259, 0);
            darkSectionPanel3.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel3.Name="darkSectionPanel3";
            darkSectionPanel3.SectionHeader="Pubtoolinfo";
            darkSectionPanel3.Size=new System.Drawing.Size(642, 76);
            darkSectionPanel3.TabIndex=95;
            // 
            // darkDataGridView4
            // 
            darkDataGridView4.AllowUserToAddRows=false;
            darkDataGridView4.AllowUserToDeleteRows=false;
            darkDataGridView4.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            darkDataGridView4.ColumnHeadersHeight=4;
            darkDataGridView4.Dock=DockStyle.Fill;
            darkDataGridView4.Location=new System.Drawing.Point(1, 25);
            darkDataGridView4.Margin=new Padding(3, 4, 3, 4);
            darkDataGridView4.MultiSelect=false;
            darkDataGridView4.Name="darkDataGridView4";
            darkDataGridView4.ReadOnly=true;
            darkDataGridView4.RowHeadersWidth=41;
            darkDataGridView4.RowTemplate.Height=23;
            darkDataGridView4.ScrollBars=ScrollBars.Horizontal;
            darkDataGridView4.Size=new System.Drawing.Size(640, 50);
            darkDataGridView4.TabIndex=78;
            darkDataGridView4.SelectionChanged+=darkDataGridView4_SelectionChanged;
            // 
            // darkSectionPanel1
            // 
            darkSectionPanel1.Controls.Add(dgvHeader);
            darkSectionPanel1.Dock=DockStyle.Left;
            darkSectionPanel1.Location=new System.Drawing.Point(0, 0);
            darkSectionPanel1.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel1.Name="darkSectionPanel1";
            darkSectionPanel1.SectionHeader="Header Info";
            darkSectionPanel1.Size=new System.Drawing.Size(240, 526);
            darkSectionPanel1.TabIndex=94;
            // 
            // dgvHeader
            // 
            dgvHeader.AllowUserToAddRows=false;
            dgvHeader.AllowUserToDeleteRows=false;
            dgvHeader.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            dgvHeader.ColumnHeadersHeight=4;
            dgvHeader.Dock=DockStyle.Fill;
            dgvHeader.Location=new System.Drawing.Point(1, 25);
            dgvHeader.Margin=new Padding(3, 4, 3, 4);
            dgvHeader.MultiSelect=false;
            dgvHeader.Name="dgvHeader";
            dgvHeader.ReadOnly=true;
            dgvHeader.RowHeadersWidth=41;
            dgvHeader.RowTemplate.Height=23;
            dgvHeader.Size=new System.Drawing.Size(238, 500);
            dgvHeader.TabIndex=76;
            dgvHeader.SelectionChanged+=dgvHeader_SelectionChanged;
            // 
            // tabPage7
            // 
            tabPage7.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPage7.Controls.Add(panel6);
            tabPage7.ForeColor=System.Drawing.Color.Gainsboro;
            tabPage7.Location=new System.Drawing.Point(4, 20);
            tabPage7.Margin=new Padding(3, 2, 3, 2);
            tabPage7.Name="tabPage7";
            tabPage7.Padding=new Padding(3, 2, 3, 2);
            tabPage7.Size=new System.Drawing.Size(951, 568);
            tabPage7.TabIndex=5;
            tabPage7.Text="View and Extract";
            // 
            // panel6
            // 
            panel6.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            panel6.Controls.Add(btnExtractFullPKG);
            panel6.Controls.Add(darkLabel5);
            panel6.Controls.Add(tbPasscode);
            panel6.Controls.Add(btnViewPKGData);
            panel6.Controls.Add(darkSectionPanel4);
            panel6.Location=new System.Drawing.Point(21, 18);
            panel6.Margin=new Padding(3, 2, 3, 2);
            panel6.Name="panel6";
            panel6.Size=new System.Drawing.Size(909, 532);
            panel6.TabIndex=97;
            // 
            // btnExtractFullPKG
            // 
            btnExtractFullPKG.Anchor=AnchorStyles.Top;
            btnExtractFullPKG.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnExtractFullPKG.Location=new System.Drawing.Point(600, 15);
            btnExtractFullPKG.Margin=new Padding(3, 2, 3, 2);
            btnExtractFullPKG.Name="btnExtractFullPKG";
            btnExtractFullPKG.Size=new System.Drawing.Size(108, 23);
            btnExtractFullPKG.TabIndex=100;
            btnExtractFullPKG.Text="Extract full PKG";
            btnExtractFullPKG.Click+=btnExtractFullPKG_Click;
            // 
            // darkLabel5
            // 
            darkLabel5.Anchor=AnchorStyles.Top;
            darkLabel5.AutoSize=true;
            darkLabel5.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            darkLabel5.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel5.Location=new System.Drawing.Point(201, 19);
            darkLabel5.Name="darkLabel5";
            darkLabel5.Size=new System.Drawing.Size(59, 15);
            darkLabel5.TabIndex=96;
            darkLabel5.Text="Passcode:";
            // 
            // tbPasscode
            // 
            tbPasscode.Anchor=AnchorStyles.Top;
            tbPasscode.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tbPasscode.Location=new System.Drawing.Point(267, 15);
            tbPasscode.Margin=new Padding(3, 2, 3, 2);
            tbPasscode.MaxLength=32;
            tbPasscode.Multiline=true;
            tbPasscode.Name="tbPasscode";
            tbPasscode.Size=new System.Drawing.Size(202, 23);
            tbPasscode.TabIndex=0;
            tbPasscode.Text="00000000000000000000000000000000";
            // 
            // btnViewPKGData
            // 
            btnViewPKGData.Anchor=AnchorStyles.Top;
            btnViewPKGData.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnViewPKGData.Location=new System.Drawing.Point(475, 15);
            btnViewPKGData.Margin=new Padding(3, 2, 3, 2);
            btnViewPKGData.Name="btnViewPKGData";
            btnViewPKGData.Size=new System.Drawing.Size(119, 23);
            btnViewPKGData.TabIndex=94;
            btnViewPKGData.Text="View PKG files";
            btnViewPKGData.Click+=btnViewPKGData_Click;
            // 
            // darkSectionPanel4
            // 
            darkSectionPanel4.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            darkSectionPanel4.Controls.Add(splitContainer1);
            darkSectionPanel4.Location=new System.Drawing.Point(0, 48);
            darkSectionPanel4.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel4.Name="darkSectionPanel4";
            darkSectionPanel4.SectionHeader="PKG Files";
            darkSectionPanel4.Size=new System.Drawing.Size(909, 485);
            darkSectionPanel4.TabIndex=95;
            darkSectionPanel4.Tag="";
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle=BorderStyle.FixedSingle;
            splitContainer1.Dock=DockStyle.Fill;
            splitContainer1.Location=new System.Drawing.Point(1, 25);
            splitContainer1.Margin=new Padding(3, 4, 3, 4);
            splitContainer1.Name="splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(PKGTreeView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(listView1);
            splitContainer1.Panel2.Controls.Add(btnSearchFileInTreeView);
            splitContainer1.Panel2.Controls.Add(tbSearchTreeView);
            splitContainer1.Panel2.Controls.Add(darkLabel6);
            splitContainer1.Size=new System.Drawing.Size(907, 459);
            splitContainer1.SplitterDistance=404;
            splitContainer1.TabIndex=0;
            // 
            // PKGTreeView
            // 
            PKGTreeView.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            PKGTreeView.BorderStyle=BorderStyle.None;
            PKGTreeView.Dock=DockStyle.Fill;
            PKGTreeView.ForeColor=System.Drawing.Color.Gainsboro;
            PKGTreeView.Location=new System.Drawing.Point(0, 0);
            PKGTreeView.Margin=new Padding(3, 4, 3, 4);
            PKGTreeView.Name="PKGTreeView";
            PKGTreeView.Size=new System.Drawing.Size(402, 457);
            PKGTreeView.TabIndex=0;
            PKGTreeView.AfterSelect+=PKGTreeView_AfterSelect;
            PKGTreeView.NodeMouseClick+=PKGTreeView_NodeMouseClick;
            PKGTreeView.MouseClick+=PKGTreeView_MouseClick;
            // 
            // listView1
            // 
            listView1.AllowDrop=true;
            listView1.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            listView1.BorderStyle=BorderStyle.None;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader7, columnHeader8, columnHeader9 });
            listView1.Dock=DockStyle.Fill;
            listView1.ForeColor=System.Drawing.Color.Gainsboro;
            listView1.FullRowSelect=true;
            listView1.LargeImageList=imageList1;
            listView1.Location=new System.Drawing.Point(0, 0);
            listView1.Margin=new Padding(3, 4, 3, 4);
            listView1.Name="listView1";
            listView1.Size=new System.Drawing.Size(497, 457);
            listView1.TabIndex=0;
            listView1.UseCompatibleStateImageBehavior=false;
            listView1.View=View.Details;
            listView1.ColumnWidthChanging+=listView1_ColumnWidthChanging;
            listView1.ItemActivate+=listView1_ItemActivate;
            listView1.SizeChanged+=listView1_SizeChanged;
            listView1.MouseClick+=listView1_MouseClick;
            listView1.MouseDoubleClick+=listView1_MouseDoubleClick;
            // 
            // columnHeader7
            // 
            columnHeader7.Text="Name";
            // 
            // columnHeader8
            // 
            columnHeader8.Text="Type";
            // 
            // columnHeader9
            // 
            columnHeader9.Text="Path";
            // 
            // btnSearchFileInTreeView
            // 
            btnSearchFileInTreeView.Anchor=AnchorStyles.Top;
            btnSearchFileInTreeView.Image=(System.Drawing.Image)resources.GetObject("btnSearchFileInTreeView.Image");
            btnSearchFileInTreeView.Location=new System.Drawing.Point(366, 302);
            btnSearchFileInTreeView.Margin=new Padding(3, 2, 3, 2);
            btnSearchFileInTreeView.Name="btnSearchFileInTreeView";
            btnSearchFileInTreeView.Size=new System.Drawing.Size(34, 22);
            btnSearchFileInTreeView.TabIndex=99;
            btnSearchFileInTreeView.Visible=false;
            btnSearchFileInTreeView.Click+=SearchFileInTreeView_Click;
            // 
            // tbSearchTreeView
            // 
            tbSearchTreeView.Anchor=AnchorStyles.Top;
            tbSearchTreeView.Location=new System.Drawing.Point(158, 302);
            tbSearchTreeView.Margin=new Padding(3, 2, 3, 2);
            tbSearchTreeView.Name="tbSearchTreeView";
            tbSearchTreeView.Size=new System.Drawing.Size(199, 23);
            tbSearchTreeView.TabIndex=97;
            tbSearchTreeView.Visible=false;
            // 
            // darkLabel6
            // 
            darkLabel6.Anchor=AnchorStyles.Top;
            darkLabel6.AutoSize=true;
            darkLabel6.ForeColor=System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel6.Location=new System.Drawing.Point(88, 306);
            darkLabel6.Name="darkLabel6";
            darkLabel6.Size=new System.Drawing.Size(64, 15);
            darkLabel6.TabIndex=98;
            darkLabel6.Text="Search file:";
            darkLabel6.Visible=false;
            // 
            // tabPage5
            // 
            tabPage5.BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            tabPage5.Controls.Add(panel8);
            tabPage5.ForeColor=System.Drawing.Color.Gainsboro;
            tabPage5.Location=new System.Drawing.Point(4, 20);
            tabPage5.Margin=new Padding(3, 2, 3, 2);
            tabPage5.Name="tabPage5";
            tabPage5.Padding=new Padding(3, 2, 3, 2);
            tabPage5.Size=new System.Drawing.Size(951, 568);
            tabPage5.TabIndex=6;
            tabPage5.Text="Official Update";
            // 
            // panel8
            // 
            panel8.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            panel8.Controls.Add(darkSectionPanel11);
            panel8.Controls.Add(darkSectionPanel12);
            panel8.Location=new System.Drawing.Point(21, 20);
            panel8.Margin=new Padding(3, 2, 3, 2);
            panel8.Name="panel8";
            panel8.Size=new System.Drawing.Size(909, 529);
            panel8.TabIndex=112;
            // 
            // darkSectionPanel11
            // 
            darkSectionPanel11.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            darkSectionPanel11.Controls.Add(dgvUpdate);
            darkSectionPanel11.Location=new System.Drawing.Point(0, 0);
            darkSectionPanel11.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel11.Name="darkSectionPanel11";
            darkSectionPanel11.SectionHeader="Official Update Part List";
            darkSectionPanel11.Size=new System.Drawing.Size(909, 388);
            darkSectionPanel11.TabIndex=109;
            // 
            // dgvUpdate
            // 
            dgvUpdate.AllowUserToAddRows=false;
            dgvUpdate.AllowUserToDeleteRows=false;
            dgvUpdate.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            dgvUpdate.ColumnHeadersHeight=4;
            dgvUpdate.ContextMenuStrip=contextMenuOfficialUpdate;
            dgvUpdate.Dock=DockStyle.Fill;
            dgvUpdate.Location=new System.Drawing.Point(1, 25);
            dgvUpdate.Margin=new Padding(3, 4, 3, 4);
            dgvUpdate.MultiSelect=false;
            dgvUpdate.Name="dgvUpdate";
            dgvUpdate.ReadOnly=true;
            dgvUpdate.RowHeadersWidth=41;
            dgvUpdate.RowTemplate.Height=23;
            dgvUpdate.ScrollBars=ScrollBars.Horizontal;
            dgvUpdate.Size=new System.Drawing.Size(907, 362);
            dgvUpdate.TabIndex=108;
            // 
            // darkSectionPanel12
            // 
            darkSectionPanel12.Controls.Add(label13);
            darkSectionPanel12.Controls.Add(label14);
            darkSectionPanel12.Controls.Add(label15);
            darkSectionPanel12.Controls.Add(label16);
            darkSectionPanel12.Controls.Add(label17);
            darkSectionPanel12.Controls.Add(label18);
            darkSectionPanel12.Controls.Add(label19);
            darkSectionPanel12.Controls.Add(labelRemaster);
            darkSectionPanel12.Controls.Add(label21);
            darkSectionPanel12.Controls.Add(labelUpdateType);
            darkSectionPanel12.Controls.Add(labelMandatory);
            darkSectionPanel12.Controls.Add(labelPKGdigest);
            darkSectionPanel12.Controls.Add(label5);
            darkSectionPanel12.Controls.Add(label12);
            darkSectionPanel12.Controls.Add(label9);
            darkSectionPanel12.Controls.Add(label11);
            darkSectionPanel12.Controls.Add(label4);
            darkSectionPanel12.Controls.Add(label10);
            darkSectionPanel12.Controls.Add(label7);
            darkSectionPanel12.Controls.Add(labelSystemReq);
            darkSectionPanel12.Controls.Add(label6);
            darkSectionPanel12.Controls.Add(labelUpdateVersion);
            darkSectionPanel12.Controls.Add(labelTotalSize);
            darkSectionPanel12.Controls.Add(labelTotalFile);
            darkSectionPanel12.Dock=DockStyle.Bottom;
            darkSectionPanel12.Location=new System.Drawing.Point(0, 411);
            darkSectionPanel12.Margin=new Padding(3, 2, 3, 2);
            darkSectionPanel12.Name="darkSectionPanel12";
            darkSectionPanel12.SectionHeader="Update Summary";
            darkSectionPanel12.Size=new System.Drawing.Size(909, 118);
            darkSectionPanel12.TabIndex=113;
            // 
            // label13
            // 
            label13.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label13.AutoSize=true;
            label13.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label13.ForeColor=System.Drawing.Color.Silver;
            label13.Location=new System.Drawing.Point(269, 36);
            label13.Name="label13";
            label13.Size=new System.Drawing.Size(61, 15);
            label13.TabIndex=110;
            label13.Text="Remaster";
            // 
            // label14
            // 
            label14.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label14.AutoSize=true;
            label14.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label14.ForeColor=System.Drawing.Color.Silver;
            label14.Location=new System.Drawing.Point(269, 51);
            label14.Name="label14";
            label14.Size=new System.Drawing.Size(76, 15);
            label14.TabIndex=107;
            label14.Text="Update type";
            // 
            // label15
            // 
            label15.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label15.AutoSize=true;
            label15.ForeColor=System.Drawing.Color.Silver;
            label15.Location=new System.Drawing.Point(413, 81);
            label15.Name="label15";
            label15.Size=new System.Drawing.Size(10, 15);
            label15.TabIndex=118;
            label15.Text=":";
            // 
            // label16
            // 
            label16.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label16.AutoSize=true;
            label16.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label16.ForeColor=System.Drawing.Color.Silver;
            label16.Location=new System.Drawing.Point(269, 66);
            label16.Name="label16";
            label16.Size=new System.Drawing.Size(90, 15);
            label16.TabIndex=108;
            label16.Text="Package digest";
            // 
            // label17
            // 
            label17.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label17.AutoSize=true;
            label17.ForeColor=System.Drawing.Color.Silver;
            label17.Location=new System.Drawing.Point(413, 66);
            label17.Name="label17";
            label17.Size=new System.Drawing.Size(10, 15);
            label17.TabIndex=117;
            label17.Text=":";
            // 
            // label18
            // 
            label18.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label18.AutoSize=true;
            label18.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label18.ForeColor=System.Drawing.Color.Silver;
            label18.Location=new System.Drawing.Point(269, 81);
            label18.Name="label18";
            label18.Size=new System.Drawing.Size(109, 15);
            label18.TabIndex=109;
            label18.Text="Mandatory update";
            // 
            // label19
            // 
            label19.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label19.AutoSize=true;
            label19.ForeColor=System.Drawing.Color.Silver;
            label19.Location=new System.Drawing.Point(413, 51);
            label19.Name="label19";
            label19.Size=new System.Drawing.Size(10, 15);
            label19.TabIndex=116;
            label19.Text=":";
            // 
            // labelRemaster
            // 
            labelRemaster.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            labelRemaster.AutoSize=true;
            labelRemaster.ForeColor=System.Drawing.Color.Silver;
            labelRemaster.Location=new System.Drawing.Point(424, 36);
            labelRemaster.Name="labelRemaster";
            labelRemaster.Size=new System.Drawing.Size(16, 15);
            labelRemaster.TabIndex=111;
            labelRemaster.Text="...";
            // 
            // label21
            // 
            label21.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label21.AutoSize=true;
            label21.ForeColor=System.Drawing.Color.Silver;
            label21.Location=new System.Drawing.Point(413, 36);
            label21.Name="label21";
            label21.Size=new System.Drawing.Size(10, 15);
            label21.TabIndex=115;
            label21.Text=":";
            // 
            // labelUpdateType
            // 
            labelUpdateType.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            labelUpdateType.AutoSize=true;
            labelUpdateType.ForeColor=System.Drawing.Color.Silver;
            labelUpdateType.Location=new System.Drawing.Point(424, 51);
            labelUpdateType.Name="labelUpdateType";
            labelUpdateType.Size=new System.Drawing.Size(16, 15);
            labelUpdateType.TabIndex=112;
            labelUpdateType.Text="...";
            // 
            // labelMandatory
            // 
            labelMandatory.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            labelMandatory.AutoSize=true;
            labelMandatory.ForeColor=System.Drawing.Color.Silver;
            labelMandatory.Location=new System.Drawing.Point(424, 81);
            labelMandatory.Name="labelMandatory";
            labelMandatory.Size=new System.Drawing.Size(16, 15);
            labelMandatory.TabIndex=114;
            labelMandatory.Text="...";
            // 
            // labelPKGdigest
            // 
            labelPKGdigest.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            labelPKGdigest.AutoSize=true;
            labelPKGdigest.ForeColor=System.Drawing.Color.Silver;
            labelPKGdigest.Location=new System.Drawing.Point(424, 66);
            labelPKGdigest.Name="labelPKGdigest";
            labelPKGdigest.Size=new System.Drawing.Size(16, 15);
            labelPKGdigest.TabIndex=113;
            labelPKGdigest.Text="...";
            // 
            // label5
            // 
            label5.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label5.AutoSize=true;
            label5.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label5.ForeColor=System.Drawing.Color.Silver;
            label5.Location=new System.Drawing.Point(20, 36);
            label5.Name="label5";
            label5.Size=new System.Drawing.Size(122, 15);
            label5.TabIndex=98;
            label5.Text="System requirement";
            // 
            // label12
            // 
            label12.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label12.AutoSize=true;
            label12.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label12.ForeColor=System.Drawing.Color.Silver;
            label12.Location=new System.Drawing.Point(20, 51);
            label12.Name="label12";
            label12.Size=new System.Drawing.Size(92, 15);
            label12.TabIndex=95;
            label12.Text="Update version";
            // 
            // label9
            // 
            label9.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label9.AutoSize=true;
            label9.ForeColor=System.Drawing.Color.Silver;
            label9.Location=new System.Drawing.Point(164, 81);
            label9.Name="label9";
            label9.Size=new System.Drawing.Size(10, 15);
            label9.TabIndex=106;
            label9.Text=":";
            // 
            // label11
            // 
            label11.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label11.AutoSize=true;
            label11.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label11.ForeColor=System.Drawing.Color.Silver;
            label11.Location=new System.Drawing.Point(20, 66);
            label11.Name="label11";
            label11.Size=new System.Drawing.Size(55, 15);
            label11.TabIndex=96;
            label11.Text="Total file";
            // 
            // label4
            // 
            label4.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label4.AutoSize=true;
            label4.ForeColor=System.Drawing.Color.Silver;
            label4.Location=new System.Drawing.Point(164, 66);
            label4.Name="label4";
            label4.Size=new System.Drawing.Size(10, 15);
            label4.TabIndex=105;
            label4.Text=":";
            // 
            // label10
            // 
            label10.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label10.AutoSize=true;
            label10.Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label10.ForeColor=System.Drawing.Color.Silver;
            label10.Location=new System.Drawing.Point(20, 81);
            label10.Name="label10";
            label10.Size=new System.Drawing.Size(58, 15);
            label10.TabIndex=97;
            label10.Text="Total size";
            // 
            // label7
            // 
            label7.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label7.AutoSize=true;
            label7.ForeColor=System.Drawing.Color.Silver;
            label7.Location=new System.Drawing.Point(164, 51);
            label7.Name="label7";
            label7.Size=new System.Drawing.Size(10, 15);
            label7.TabIndex=104;
            label7.Text=":";
            // 
            // labelSystemReq
            // 
            labelSystemReq.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            labelSystemReq.AutoSize=true;
            labelSystemReq.ForeColor=System.Drawing.Color.Silver;
            labelSystemReq.Location=new System.Drawing.Point(175, 36);
            labelSystemReq.Name="labelSystemReq";
            labelSystemReq.Size=new System.Drawing.Size(16, 15);
            labelSystemReq.TabIndex=99;
            labelSystemReq.Text="...";
            // 
            // label6
            // 
            label6.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            label6.AutoSize=true;
            label6.ForeColor=System.Drawing.Color.Silver;
            label6.Location=new System.Drawing.Point(164, 36);
            label6.Name="label6";
            label6.Size=new System.Drawing.Size(10, 15);
            label6.TabIndex=103;
            label6.Text=":";
            // 
            // labelUpdateVersion
            // 
            labelUpdateVersion.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            labelUpdateVersion.AutoSize=true;
            labelUpdateVersion.ForeColor=System.Drawing.Color.Silver;
            labelUpdateVersion.Location=new System.Drawing.Point(175, 51);
            labelUpdateVersion.Name="labelUpdateVersion";
            labelUpdateVersion.Size=new System.Drawing.Size(16, 15);
            labelUpdateVersion.TabIndex=100;
            labelUpdateVersion.Text="...";
            // 
            // labelTotalSize
            // 
            labelTotalSize.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            labelTotalSize.AutoSize=true;
            labelTotalSize.ForeColor=System.Drawing.Color.Silver;
            labelTotalSize.Location=new System.Drawing.Point(175, 81);
            labelTotalSize.Name="labelTotalSize";
            labelTotalSize.Size=new System.Drawing.Size(16, 15);
            labelTotalSize.TabIndex=102;
            labelTotalSize.Text="...";
            // 
            // labelTotalFile
            // 
            labelTotalFile.Anchor=AnchorStyles.Bottom|AnchorStyles.Left;
            labelTotalFile.AutoSize=true;
            labelTotalFile.ForeColor=System.Drawing.Color.Silver;
            labelTotalFile.Location=new System.Drawing.Point(175, 66);
            labelTotalFile.Name="labelTotalFile";
            labelTotalFile.Size=new System.Drawing.Size(16, 15);
            labelTotalFile.TabIndex=101;
            labelTotalFile.Text="...";
            // 
            // Main
            // 
            AllowDrop=true;
            AutoScaleDimensions=new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            BackColor=System.Drawing.Color.FromArgb(60, 63, 65);
            ClientSize=new System.Drawing.Size(984, 661);
            Controls.Add(darkMenuStrip1);
            Controls.Add(flatTabControl1);
            Controls.Add(darkStatusStrip1);
            Controls.Add(label8);
            Controls.Add(label2);
            Font=new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Icon=(System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin=new Padding(3, 2, 3, 2);
            MinimumSize=new System.Drawing.Size(1000, 700);
            Name="Main";
            SizeGripStyle=SizeGripStyle.Show;
            StartPosition=FormStartPosition.CenterScreen;
            Text="PS4 PKG Tool";
            TransparencyKey=System.Drawing.Color.Fuchsia;
            FormClosed+=Form1_FormClosed;
            Load+=Form1_Load;
            contextMenuPKGGridView.ResumeLayout(false);
            darkMenuStrip1.ResumeLayout(false);
            darkMenuStrip1.PerformLayout();
            darkStatusStrip1.ResumeLayout(false);
            darkStatusStrip1.PerformLayout();
            contextMenuTrophy.ResumeLayout(false);
            contextMenuEntry.ResumeLayout(false);
            contextMenuOfficialUpdate.ResumeLayout(false);
            contextMenuBackgroundImage.ResumeLayout(false);
            contextMenuExtractNode.ResumeLayout(false);
            contextMenuExtractListView.ResumeLayout(false);
            flatTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            darkSectionPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)darkDataGridView2).EndInit();
            darkSectionPanel9.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PKGGridView).EndInit();
            darkSectionPanel8.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPage6.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TrophyGridView).EndInit();
            tabPage3.ResumeLayout(false);
            flatTabControlBgi.ResumeLayout(false);
            tabPagePic0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbPIC0).EndInit();
            tabPagePic1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbPIC1).EndInit();
            tabPage4.ResumeLayout(false);
            panel7.ResumeLayout(false);
            darkSectionPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEntryList).EndInit();
            darkSectionPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)darkDataGridView4).EndInit();
            darkSectionPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHeader).EndInit();
            tabPage7.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            darkSectionPanel4.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            panel8.ResumeLayout(false);
            darkSectionPanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUpdate).EndInit();
            darkSectionPanel12.ResumeLayout(false);
            darkSectionPanel12.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DarkUI.Controls.DarkLabel label1;
        private DarkUI.Controls.DarkLabel label2;
        private PictureBox pictureBox1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openGameFolderToolStripMenuItem;
        private ToolStripMenuItem toolToolStripMenuItem;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem viewTrophyListToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem renameToTITLEIDToolStripMenuItem;
        private ToolStripMenuItem renameToCONTENTIDToolStripMenuItem;
        private ToolStripMenuItem exportPKGListToExcelToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem refreshContentToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem donateToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem renamePKGToTITLEToolStripMenuItem;
        private ToolStripMenuItem renamePKGToTITLETITLEIDToolStripMenuItem;
        private ToolStripMenuItem renamePKGToTITLEToolStripMenuItem1;
        private ToolStripMenuItem renamePKGToTITLECATEGORYToolStripMenuItem;
        private Panel panel5;
        private DarkUI.Controls.DarkLabel label3;
        private DarkUI.Controls.DarkLabel label8;
        private DarkUI.Controls.DarkMenuStrip darkMenuStrip1;
        private DarkUI.Controls.DarkContextMenu PkgGridViewDarkContextMenu;
        private ToolStripMenuItem toolStripMenuItem94;
        private ToolStripMenuItem toolStripMenuItem96;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem toolStripMenuItem111;
        private ToolStripMenuItem renameAllPkg1ToolStripMenuItem2;
        private ToolStripMenuItem renameAllPkg2ToolStripMenuItem2;
        private ToolStripMenuItem renameAllPkg3ToolStripMenuItem2;
        private ToolStripMenuItem renameAllPkg4ToolStripMenuItem2;
        private ToolStripMenuItem renameAllPkg5ToolStripMenuItem2;
        private ToolStripMenuItem renameAllPkg6ToolStripMenuItem2;
        private ToolStripMenuItem renameAllPkg9ToolStripMenuItem2;
        private ToolStripMenuItem globalExportPKGListToExcelToolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem toolStripMenuItem127;
        private ToolStripMenuItem copyTitleIdtoolStripMenuItem2;
        private ToolStripMenuItem copyContentIdtoolStripMenuItem2;
        private ToolStripMenuItem viewPkgExplorerStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem133;
        private ToolStripMenuItem renameSelectedPkg1ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg2ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg3ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg4ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg5ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg6ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg9ToolStripMenuItem2;
        private ToolStripMenuItem fileToolStripMenuItem2;
        private ToolStripMenuItem reloadContentToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem toolToolStripMenuItem1;
        private ToolStripMenuItem globalActionToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem renameAllPkg1ToolStripMenuItem1;
        private ToolStripMenuItem renameAllPkg2ToolStripMenuItem1;
        private ToolStripMenuItem renameAllPkg3ToolStripMenuItem1;
        private ToolStripMenuItem renameAllPkg4ToolStripMenuItem1;
        private ToolStripMenuItem renameAllPkg5ToolStripMenuItem1;
        private ToolStripMenuItem renameAllPkg7ToolStripMenuItem1;
        private ToolStripMenuItem extractImageAndBackgroundToolStripMenuItem;
        private ToolStripMenuItem globalExportPKGListToExcelToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem globalCopyStripMenuItem;
        private ToolStripMenuItem copyTitleIdtoolStripMenuItem1;
        private ToolStripMenuItem copyContentIdtoolStripMenuItem1;
        private ToolStripMenuItem viewPkgExplorerStripMenuItem1;
        private ToolStripMenuItem renameCurrentPKGStripMenuItem;
        private ToolStripMenuItem renameSelectedPkg1ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg2ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg3ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg4ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg5ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg6ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg9ToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem144;
        private ToolStripMenuItem settingstoolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem158;
        private ToolStripMenuItem toolStripMenuItem159;
        private ToolStripMenuItem toolStripMenuItem160;
        private ToolStripMenuItem renameAllPkg6ToolStripMenuItem1;
        private ToolStripSplitButton ToolStripSplitButtonTotalPKG;
        public ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private DarkUI.Controls.DarkTextBox tbSearchGame;
        private ToolStripMenuItem globalExtractImagesAndIconToolStripMenuItem1;
        private ToolStripMenuItem globalExtractImageOnlyToolStripMenuItem1;
        private ToolStripMenuItem globalExtractIconOnlyToolStripMenuItem1;
        private DarkUI.Controls.DarkLabel darkLabel1;
        private ToolStripProgressBar toolStripProgressBar1;
        private DarkUI.Controls.DarkLabel darkLabel2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem globalExtractImagesAndIconToolStripMenuItem2;
        private ToolStripMenuItem globalExtractImageOnlyToolStripMenuItem2;
        private ToolStripMenuItem globalExtractIconOnlyToolStripMenuItem2;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem managePS4PKGToolStripMenuItem;
        private ToolStripMenuItem renameAllPkg7ToolStripMenuItem2;
        private ToolStripMenuItem renameAllPkg8ToolStripMenuItem2;
        private ToolStripMenuItem renameAllPkg8ToolStripMenuItem1;
        private ToolStripMenuItem renameAllPkg9ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg7ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg8ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg7ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg8ToolStripMenuItem1;
        private ToolStripMenuItem deletePkgtoolStripMenuItem2;
        private ToolStripMenuItem deletePKGtoolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem18;
        private ToolStripMenuItem RpiSendPkgtoolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem16;
        private ToolStripMenuItem RpiSendPkgtoolStripMenuItem1;
        private ToolStripMenuItem RpiCheckPkgInstalledtoolStripMenuItem2;
        private ToolStripMenuItem RpiCheckPkgInstalledtoolStripMenuItem1;
        private ToolStripMenuItem uninstallPKGFromPS4ToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem21;
        private ToolStripMenuItem renameAllPkg10ToolStripMenuItem1;
        private ToolStripMenuItem renameAllPkg10ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg10ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg10ToolStripMenuItem1;
        private DarkUI.Controls.DarkDataGridView darkDataGridView2;
        private ToolStripMenuItem RpiUninstallThemePKGToolStripMenuItem2;
        private VisualStudioTabControl.VisualStudioTabControl flatTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DarkUI.Controls.DarkDataGridView TrophyGridView;
        private TabPage tabPage3;
        private VisualStudioTabControl.VisualStudioTabControl flatTabControlBgi;
        private DarkUI.Controls.DarkContextMenu TrophyDarkContextMenu;
        private ToolStripMenuItem ExtractTrophyImageToolStripMenuItem;
        private DarkUI.Controls.DarkContextMenu contextMenuBackgroundImage;
        private ToolStripMenuItem saveImageToolStripMenuItem;
        private ToolStripMenuItem SetImageAsDesktopBackgroundToolStripMenuItem;
        private TabPage tabPage4;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel2;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel1;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel3;
        private DarkUI.Controls.DarkDataGridView darkDataGridView4;
        private DarkUI.Controls.DarkDataGridView dgvEntryList;
        private DarkUI.Controls.DarkDataGridView dgvHeader;
        private DarkUI.Controls.DarkContextMenu PkgEntryDarkContextMenu;
        private ToolStripMenuItem ExtractDecryptedEntryToolStripMenuItem;
        private ToolStripMenuItem ExtractAllEntryToolStripMenuItem;
        private ImageList imageList1;
        private DarkUI.Controls.DarkContextMenu PkgExtractDarkContextMenu;
        private ToolStripMenuItem extractNodeToolStripMenuItem;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader6;
        private Panel panel7;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel10;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel9;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel8;
        private DarkUI.Controls.DarkLabel label9;
        private DarkUI.Controls.DarkLabel label4;
        private DarkUI.Controls.DarkLabel label7;
        private DarkUI.Controls.DarkLabel label6;
        private DarkUI.Controls.DarkLabel labelTotalSize;
        private DarkUI.Controls.DarkLabel labelTotalFile;
        private DarkUI.Controls.DarkLabel labelUpdateVersion;
        private DarkUI.Controls.DarkLabel labelSystemReq;
        private DarkUI.Controls.DarkLabel label5;
        private DarkUI.Controls.DarkLabel label10;
        private DarkUI.Controls.DarkLabel label11;
        private DarkUI.Controls.DarkLabel label12;
        private DarkUI.Controls.DarkContextMenu OfficialUpdateDarkContextMenu;
        private ToolStripMenuItem copyURLToolStripMenuItem;
        private ToolStripMenuItem downloadSelectedPKGUpdateToolStripMenuItem;
        private DarkUI.Controls.DarkLabel label13;
        private DarkUI.Controls.DarkLabel label14;
        private DarkUI.Controls.DarkLabel label15;
        private DarkUI.Controls.DarkLabel label16;
        private DarkUI.Controls.DarkLabel label17;
        private DarkUI.Controls.DarkLabel label18;
        private DarkUI.Controls.DarkLabel label19;
        private DarkUI.Controls.DarkLabel labelRemaster;
        private DarkUI.Controls.DarkLabel label21;
        private DarkUI.Controls.DarkLabel labelUpdateType;
        private DarkUI.Controls.DarkLabel labelMandatory;
        private DarkUI.Controls.DarkLabel labelPKGdigest;
        private DarkUI.Controls.DarkTextBox tbPasscode;
        private DarkUI.Controls.DarkButton btnViewPKGData;
        private TabPage tabPage7;
        private SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView PKGTreeView;
        private System.Windows.Forms.ListView listView1;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private TabPage tabPage5;
        private Panel panel8;
        private DarkUI.Controls.DarkDataGridView dgvUpdate;
        private Panel panel6;
        private DarkUI.Controls.DarkButton darkButton3;
        private DarkUI.Controls.DarkDataGridView PKGGridView;
        private TabPage tabPagePic0;
        private TabPage tabPagePic1;
        private PictureBox pbPIC0;
        private PictureBox pbPIC1;
        private DarkUI.Controls.DarkLabel darkLabel3;
        private DarkUI.Controls.DarkLabel darkLabel4;
        private DarkUI.Controls.DarkContextMenu contextMenuExtractListView;
        private ToolStripMenuItem toolStripMenuItem32;
        private ToolStripMenuItem viewPkgChangeInfotoolStripMenuItem2;
        private ToolStripStatusLabel labelDisplayTotalPKG;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem toolStripMenuItem34;
        private ToolStripMenuItem viewPkgChangeInfotoolStripMenuItem1;
        private DarkUI.Controls.DarkLabel darkLabel5;
        private DarkUI.Controls.DarkTextBox tbSearchTreeView;
        private DarkUI.Controls.DarkLabel darkLabel6;
        private ToolStripMenuItem expandAllToolStripMenuItem;
        private DarkUI.Controls.DarkButton btnSearchFileInTreeView;
        private ToolStripMenuItem collapseAllNodeToolStripMenuItem;
        private ToolStripMenuItem seperateAndMovePKGByTypeIntoFolderToolStripMenuItem;
        private ToolStripMenuItem renameAllPkg11ToolStripMenuItem1;
        private ToolStripMenuItem renameSelectedPkg11ToolStripMenuItem1;
        private ToolStripMenuItem renameAllPkg11ToolStripMenuItem2;
        private ToolStripMenuItem renameSelectedPkg11ToolStripMenuItem2;
        private ToolStripMenuItem movePkgCategoryToolStripMenuItem1;
        private ToolStripMenuItem RpiUninstallDlcPKGToolStripMenuItem2;
        private ToolStripMenuItem RpiUninstallPatchPKGToolStripMenuItem2;
        private ToolStripMenuItem RpiUninstallBasePKGToolStripMenuItem2;
        private ToolStripMenuItem movePkgTypeToolStripMenuItem1;
        private ToolStripMenuItem movePkgRegionToolStripMenuItem1;
        private ToolStripMenuItem RpiUninstallBasePKGToolStripMenuItem1;
        private ToolStripMenuItem RpiUninstallPatchPKGToolStripMenuItem1;
        private ToolStripMenuItem RpiUninstallDlcPKGToolStripMenuItem1;
        private ToolStripMenuItem RpiUninstallThemePKGToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem38;
        private ToolStripMenuItem movePkgTypeToolStripMenuItem2;
        private ToolStripMenuItem movePkgCategoryToolStripMenuItem2;
        private ToolStripMenuItem movePkgRegionToolStripMenuItem2;
        private DarkUI.Controls.DarkButton btnExtractFullPKG;
        private ToolStripMenuItem GroupActionTitleStripMenuItem;
        private ToolStripMenuItem selectedExportPKGListToExcelToolStripMenuItem2;
        private ToolStripMenuItem GroupActionExtacrtImageToolStripMenuItem;
        private ToolStripMenuItem selectedExtractImagesAndIconToolStripMenuItem2;
        private ToolStripMenuItem selectedExtractImageOnlyToolStripMenuItem2;
        private ToolStripMenuItem selectedExtractIconOnlyToolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator3;
        private DarkUI.Controls.DarkContextMenu contextMenuPKGGridView;
        private DarkUI.Controls.DarkContextMenu contextMenuTrophy;
        private DarkUI.Controls.DarkContextMenu contextMenuEntry;
        private DarkUI.Controls.DarkContextMenu contextMenuOfficialUpdate;
        private DarkUI.Controls.DarkContextMenu contextMenuExtractNode;
        private DarkUI.Controls.DarkStatusStrip darkStatusStrip1;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel7;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel12;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel4;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel11;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem selectedExportPKGListToExcelToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem28;
        private ToolStripMenuItem selectedExtractImagesAndIconToolStripMenuItem1;
        private ToolStripMenuItem selectedExtractImageOnlyToolStripMenuItem1;
        private ToolStripMenuItem selectedExtractIconOnlyToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem backportToolStripMenuItem;
        private ToolStripMenuItem setBackportedToolStripMenuItem2;
        private ToolStripMenuItem removeBackportedToolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem setBackportedtoolStripMenuItem1;
        private ToolStripMenuItem removeBackportedtoolStripMenuItem1;
        private ToolStripTextBox backportRemarkTextboxtoolStripTextBox1;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem setBackportRemarksToolStripMenuItem;
        private ToolStripMenuItem setRemarktoolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem setRemarktoolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripTextBox backportRemarkTextboxtoolStripTextBox2;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem colorTestToolStripMenuItem;
        private ToolStripMenuItem movePkgTitleToolStripMenuItem2;
        private ToolStripMenuItem movePkgTitleToolStripMenuItem1;
        private ToolStripMenuItem checkForDuplicatePKGToolStripMenuItem2;
        private ToolStripMenuItem checkForDuplicatePKGToolStripMenuItem1;
        private TabPage tabPage6;
        private System.Windows.Forms.ListView PKGListView;
        private System.Windows.Forms.TreeView treeView1;
        private DarkUI.Controls.DarkTreeView listView2;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripMenuItem openPS4PKGToolTempDirectoryToolStripMenuItem2;
        private ToolStripMenuItem openPS4PKGToolTempDirectoryToolStripMenuItem1;
    }
}

