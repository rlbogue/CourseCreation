using BookPublishing.Binding.Control;
using BookPublishing.Binding.Model;
using BookPublishing.Binding.View;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BookPublishing
{
    public partial class Binder : Form
    {
        public Binder()
        {
            InitializeComponent();
            assembly = new BindingAssembly();
            ctlSettings.SettingsToScreen(assembly.DefaultSettings);
            grdFiles.AutoGenerateColumns = true;

            grdFiles.DataSource = assembly.Files;
            foreach (DataGridViewColumn column in grdFiles.Columns)
            {
                column.ReadOnly = true;
                column.Visible = (column.Name == "FileName");   // Hide all but FileName
                if (column.Name == "FileName")
                {
                    column.HeaderText = "File Name";
                    column.Width = grdFiles.Width;
                }
            }
            grdFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        BindingAssembly assembly = null;

        public void UpdateFilesLoaded()
        {
            if (assembly == null) lblFilesLoaded.Text = "0 files to assemble";
            else
                lblFilesLoaded.Text = $"{assembly.Files.Count} files to assemble";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (assembly == null) assembly = new BindingAssembly();
            ctlSettings.ScreenToSettings(assembly.DefaultSettings);
            if (assembly.Files.Count > 0)
            {
                // Confirm, overwrite
                DialogResult result = MessageBox.Show("Clear existing entries and configuration?", "Overwrite", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes) assembly.Files.Clear();
                else if (result == DialogResult.Cancel) return;
                // else we'll append
            }

            try 
            {
                if (assembly == null) assembly = new BindingAssembly();
                assembly = FileReader.GetAssembly(txtFileName.Text, assembly);
            }
            catch (Exception ex)
            {
                SetStatusMessage(ex.Message);
            }

            ctlSettings.SettingsToScreen(assembly.DefaultSettings);
            UpdateFilesLoaded();
        }

        public void SetStatusMessage(string message)
        {
            // TODO: Create a place for status messages/approach
        }

        private void cmdFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpenFile = new OpenFileDialog();

            didSelectOpenFile = false;
            dlgOpenFile.Filter = "Text (*.TXT)|*.txt|XML (*.XML)|*.xml|Manifest (*.Manifest)|*.Manifest|All files (*.*)|*.*";
            dlgOpenFile.FilterIndex = 1;
            dlgOpenFile.FileOk += new System.ComponentModel.CancelEventHandler(dlgOpenFile_FileOk);
            dlgOpenFile.ShowDialog();

            if (!didSelectOpenFile) return;
            txtFileName.Text = dlgOpenFile.FileName;
        }

        private bool didSelectOpenFile = false;
        void dlgOpenFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            didSelectOpenFile = true;
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (assembly == null) assembly = new BindingAssembly();
            ctlSettings.ScreenToSettings(assembly.DefaultSettings);

            FileInfo fi = new FileInfo(txtFileName.Text);
            SaveFileDialog dlgSaveFile = new SaveFileDialog();

            if (fi.Exists)
            {
                dlgSaveFile.InitialDirectory = fi.DirectoryName;
            }
            dlgSaveFile.FileName = fi.Name;
            dlgSaveFile.Filter = "Text (*.TXT)|*.txt|XML (*.XML)|*.xml|Manifest (*.Manifest)|*.Manifest|All files (*.*)|*.*";
            dlgSaveFile.FilterIndex = 3;
            dlgSaveFile.FileOk += new System.ComponentModel.CancelEventHandler(DlgSaveFile_FileOk);

            dlgSaveFile.ShowDialog();

            if (!didSelectSaveFile) return;

            FileInfo newFi = new FileInfo(dlgSaveFile.FileName);
            
            switch (newFi.Extension.ToLower())
            {
                case ".txt":
                case ".text":
                    // For text we write a list of files -- only
                    using (FileStream fs = File.Open(newFi.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        using(StreamWriter sw = new StreamWriter(fs))
                        {
                            foreach (BindingAssembly.File file in assembly.Files)
                            {
                                sw.Write(GetRelativePath(dlgSaveFile.FileName, file.FileName)); sw.WriteLine();
                            }
                        }
                    }
                    break;
                default:
                    BindingAssembly ba = new BindingAssembly();
                    ba.CopyFrom(assembly);
                    foreach(BindingAssembly.File file in ba.Files)
                    {
                        file.FileName = GetRelativePath(dlgSaveFile.FileName, file.FileName);
                    }
                    using (FileStream fs = File.Open(newFi.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                    {
                        FileReader.Serialize(assembly, fs);
                    }
                    break;
            }
        }

        public static string GetRelativePath(string fromPath, string toPath)
        {
            int fromAttr = GetPathAttribute(fromPath);
            int toAttr = GetPathAttribute(toPath);
            if (fromAttr == 0 || toAttr == 0) return null;
            StringBuilder path = new StringBuilder(260); // MAX_PATH
            if (PathRelativePathTo(
                path,
                fromPath,
                fromAttr,
                toPath,
                toAttr) == 0)
            {
                return null;
            }
            return path.ToString();
        }

        private static int GetPathAttribute(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                return FILE_ATTRIBUTE_DIRECTORY;
            }

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                return FILE_ATTRIBUTE_NORMAL;
            }

            return 0;
        }

        private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;

        [DllImport("shlwapi.dll", SetLastError = true)]
        private static extern int PathRelativePathTo(StringBuilder pszPath,
            string pszFrom, int dwAttrFrom, string pszTo, int dwAttrTo);


        private bool didSelectSaveFile = false;

        private void DlgSaveFile_FileOk(object sender, CancelEventArgs e)
        {
            didSelectSaveFile = true;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (assembly == null) assembly = new BindingAssembly();
            SettingsForm sf = new SettingsForm(assembly.DefaultSettings);
            sf.ShowDialog();
            assembly.DefaultSettings.CopyFrom(sf.Settings);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = grdFiles.SelectedRows;
            if (rows != null && rows.Count == 1)
            {
                DialogResult dr = MessageBox.Show("Delete Row?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    BindingAssembly.File file = rows[0].DataBoundItem as BindingAssembly.File;
                    assembly.Files.Remove(file);
                }
            }
        }

        public void setFileControlEnabledState()
        {
            DataGridViewSelectedRowCollection rows = grdFiles.SelectedRows;
            bool isSelected = ((rows != null) && (rows.Count == 1));
            int rowIndex = 0;
            if (isSelected) rowIndex = rows[0].Index;
            bool isFirst = false;
            bool isLast = false;
            if (isSelected && rowIndex == 0) isFirst = true;
            if (isSelected && rowIndex == grdFiles.Rows.Count - 1) isLast = true;
            setFileControlEnabledState(isSelected, isFirst, isLast);
        }

        public void setFileControlEnabledState(bool isSelected, bool isFirst, bool isLast)
        {
            btnRemove.Enabled = isSelected;
            btnFileUp.Enabled = !isFirst && isSelected;
            btnFileDown.Enabled = !isLast && isSelected;
        }

        private void btnFileUp_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = grdFiles.SelectedRows;
            if ((rows == null ) || (rows.Count != 1)) return;
            int viewIndex = rows[0].Index;
            BindingAssembly.File selectedRow = rows[0].DataBoundItem as BindingAssembly.File;
            int rowIndex = assembly.Files.IndexOf(selectedRow);
            BindingAssembly.File previousRow = assembly.Files[rowIndex - 1];
            assembly.Files.Remove(selectedRow);
            assembly.Files.Insert(rowIndex-1, selectedRow);

            // Update the selected row
            grdFiles.Rows[viewIndex].Selected = false;
            grdFiles.Rows[viewIndex - 1].Selected = true;

            grdFiles.Refresh();
        }

        private void btnFileDown_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = grdFiles.SelectedRows;
            if ((rows == null) || (rows.Count != 1)) return;
            int viewIndex = rows[0].Index;
            BindingAssembly.File selectedRow = rows[0].DataBoundItem as BindingAssembly.File;
            int rowIndex = assembly.Files.IndexOf(selectedRow);
            BindingAssembly.File nextRow = assembly.Files[rowIndex + 1];
            assembly.Files.Remove(selectedRow);
            assembly.Files.Insert(rowIndex+1, selectedRow);

            // Update the selected row
            grdFiles.Rows[viewIndex].Selected = false;
            grdFiles.Rows[viewIndex + 1].Selected = true;
            grdFiles.Refresh();
        }

        private void grdFiles_SelectionChanged(object sender, EventArgs e)
        {
            setFileControlEnabledState();
        }

        private void tabMaster_Selected(object sender, TabControlEventArgs e)
        {
            switch(e.TabPage.Name)
            {
                case "tabManifest":
                    UpdateFilesLoaded();
                    break;
                case "tabFiles":

                    if (grdFiles.Rows.Count > 0) grdFiles.Rows[0].Selected = true;
                    setFileControlEnabledState();
                    break;
                default:
                    break;
            }
        }

        private void grdFiles_DoubleClick(object sender, EventArgs e)
        {
            // Process row
            DataGridViewSelectedRowCollection rows = grdFiles.SelectedRows;
            if ((rows == null) || (rows.Count != 1)) return; // Not sure what happened here
            BindingAssembly.File file = rows[0].DataBoundItem as BindingAssembly.File;

            if (file != null)
            {
                FileSettings fs = new FileSettings();
                fs.FiletoScreen(file);
                fs.ShowDialog();
            }
            grdFiles.Refresh();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            BinderStatus dlgBinderStatus = new BinderStatus();

            dlgBinderStatus.Assembly = assembly;
            dlgBinderStatus.StartWorkerThread();
            dlgBinderStatus.ShowDialog();
            this.Close();
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            FileSettings fs = new FileSettings();
            BindingAssembly.File file = new BindingAssembly.File();
            file.Settings.CopyFrom(assembly.DefaultSettings);
            fs.FiletoScreen(file);
            fs.ShowDialog();
            if (!string.IsNullOrWhiteSpace(file.FileName)) assembly.Files.Add(file);
        }
    }
}
