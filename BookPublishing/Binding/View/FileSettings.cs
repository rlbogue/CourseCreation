using BookPublishing.Binding.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BookPublishing.Binding.View
{
    public partial class FileSettings : Form
    {
        public BindingAssembly.File File;

        public FileSettings()
        {
            InitializeComponent();
        }

        public void FiletoScreen(BindingAssembly.File file)
        {
            File = file;
            txtFileName.Text = file.FileName;
            ctlSettings.SettingsToScreen(file.Settings);
        }

        public BindingAssembly.File ScreenToFile()
        {
            ctlSettings.ScreenToSettings(File.Settings);
            File.FileName = txtFileName.Text;
            return File;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ScreenToFile();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpenFile = new OpenFileDialog();

            didSelectOpenFile = false;
            dlgOpenFile.Filter = "Word Document (*.DOCX)|*.docx";
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
    }
}
