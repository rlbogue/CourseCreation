using BookPublishing.Binding.View;

namespace BookPublishing
{
    partial class Binder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Binder));
            this.tabMaster = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblFilesLoaded = new System.Windows.Forms.Label();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cmdFileSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.ctlSettings = new BookPublishing.Binding.View.SettingsControl();
            this.tabFiles = new System.Windows.Forms.TabPage();
            this.btnCompile = new System.Windows.Forms.Button();
            this.grdFiles = new System.Windows.Forms.DataGridView();
            this.btnFileDown = new System.Windows.Forms.Button();
            this.btnFileUp = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.tabMaster.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMaster
            // 
            this.tabMaster.Controls.Add(this.tabPage1);
            this.tabMaster.Controls.Add(this.tabFiles);
            this.tabMaster.Location = new System.Drawing.Point(12, 13);
            this.tabMaster.Name = "tabMaster";
            this.tabMaster.SelectedIndex = 0;
            this.tabMaster.Size = new System.Drawing.Size(475, 445);
            this.tabMaster.TabIndex = 0;
            this.tabMaster.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabMaster_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblFilesLoaded);
            this.tabPage1.Controls.Add(this.btnSaveAs);
            this.tabPage1.Controls.Add(this.btnLoad);
            this.tabPage1.Controls.Add(this.cmdFileSelect);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtFileName);
            this.tabPage1.Controls.Add(this.ctlSettings);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(467, 419);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Manifest";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblFilesLoaded
            // 
            this.lblFilesLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilesLoaded.Location = new System.Drawing.Point(261, 369);
            this.lblFilesLoaded.Name = "lblFilesLoaded";
            this.lblFilesLoaded.Size = new System.Drawing.Size(200, 16);
            this.lblFilesLoaded.TabIndex = 10;
            this.lblFilesLoaded.Text = "0 files to assemble";
            this.lblFilesLoaded.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(386, 390);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAs.TabIndex = 8;
            this.btnSaveAs.Text = "Save As...";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(305, 390);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cmdFileSelect
            // 
            this.cmdFileSelect.Location = new System.Drawing.Point(334, 19);
            this.cmdFileSelect.Name = "cmdFileSelect";
            this.cmdFileSelect.Size = new System.Drawing.Size(31, 23);
            this.cmdFileSelect.TabIndex = 2;
            this.cmdFileSelect.Text = "...";
            this.cmdFileSelect.UseVisualStyleBackColor = true;
            this.cmdFileSelect.Click += new System.EventHandler(this.cmdFileSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manifest File";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(79, 21);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(249, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // ctlSettings
            // 
            this.ctlSettings.Location = new System.Drawing.Point(24, 58);
            this.ctlSettings.Name = "ctlSettings";
            this.ctlSettings.Size = new System.Drawing.Size(395, 355);
            this.ctlSettings.TabIndex = 11;
            // 
            // tabFiles
            // 
            this.tabFiles.Controls.Add(this.btnCompile);
            this.tabFiles.Controls.Add(this.grdFiles);
            this.tabFiles.Controls.Add(this.btnFileDown);
            this.tabFiles.Controls.Add(this.btnFileUp);
            this.tabFiles.Controls.Add(this.btnRemove);
            this.tabFiles.Controls.Add(this.btnAddFile);
            this.tabFiles.Location = new System.Drawing.Point(4, 22);
            this.tabFiles.Name = "tabFiles";
            this.tabFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabFiles.Size = new System.Drawing.Size(467, 419);
            this.tabFiles.TabIndex = 3;
            this.tabFiles.Text = "Files";
            this.tabFiles.UseVisualStyleBackColor = true;
            // 
            // btnCompile
            // 
            this.btnCompile.Location = new System.Drawing.Point(385, 390);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(75, 23);
            this.btnCompile.TabIndex = 6;
            this.btnCompile.Text = "Compile";
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // grdFiles
            // 
            this.grdFiles.AllowUserToAddRows = false;
            this.grdFiles.AllowUserToDeleteRows = false;
            this.grdFiles.AllowUserToResizeRows = false;
            this.grdFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFiles.Location = new System.Drawing.Point(6, 6);
            this.grdFiles.Name = "grdFiles";
            this.grdFiles.RowHeadersVisible = false;
            this.grdFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdFiles.Size = new System.Drawing.Size(398, 378);
            this.grdFiles.TabIndex = 5;
            this.grdFiles.SelectionChanged += new System.EventHandler(this.grdFiles_SelectionChanged);
            this.grdFiles.DoubleClick += new System.EventHandler(this.grdFiles_DoubleClick);
            // 
            // btnFileDown
            // 
            this.btnFileDown.Location = new System.Drawing.Point(410, 361);
            this.btnFileDown.Name = "btnFileDown";
            this.btnFileDown.Size = new System.Drawing.Size(50, 23);
            this.btnFileDown.TabIndex = 4;
            this.btnFileDown.Text = "Down";
            this.btnFileDown.UseVisualStyleBackColor = true;
            this.btnFileDown.Click += new System.EventHandler(this.btnFileDown_Click);
            // 
            // btnFileUp
            // 
            this.btnFileUp.Location = new System.Drawing.Point(410, 6);
            this.btnFileUp.Name = "btnFileUp";
            this.btnFileUp.Size = new System.Drawing.Size(50, 23);
            this.btnFileUp.TabIndex = 3;
            this.btnFileUp.Text = "Up";
            this.btnFileUp.UseVisualStyleBackColor = true;
            this.btnFileUp.Click += new System.EventHandler(this.btnFileUp_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(84, 390);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(3, 390);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 1;
            this.btnAddFile.Text = "Add";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // Binder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 470);
            this.Controls.Add(this.tabMaster);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Binder";
            this.Text = "Book Binder";
            this.tabMaster.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabMaster;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button cmdFileSelect;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabPage tabFiles;
        private System.Windows.Forms.Button btnFileDown;
        private System.Windows.Forms.Button btnFileUp;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.DataGridView grdFiles;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.Label lblFilesLoaded;
        private SettingsControl ctlSettings;
    }
}