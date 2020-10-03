using System.Windows.Forms;

namespace BookPublishing.FieldInfo
{
    public partial class FieldStatus : Form
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
            this.lblPhaseIntro = new System.Windows.Forms.Label();
            this.lblPhase = new System.Windows.Forms.Label();
            this.lblFields = new System.Windows.Forms.Label();
            this.grdFields = new System.Windows.Forms.DataGridView();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFileSave = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFieldCount = new System.Windows.Forms.Label();
            this.lblShapeCount = new System.Windows.Forms.Label();
            this.lblShapes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdFields)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPhaseIntro
            // 
            this.lblPhaseIntro.AutoSize = true;
            this.lblPhaseIntro.Location = new System.Drawing.Point(12, 59);
            this.lblPhaseIntro.Name = "lblPhaseIntro";
            this.lblPhaseIntro.Size = new System.Drawing.Size(37, 13);
            this.lblPhaseIntro.TabIndex = 0;
            this.lblPhaseIntro.Text = "Phase";
            // 
            // lblPhase
            // 
            this.lblPhase.Location = new System.Drawing.Point(55, 59);
            this.lblPhase.Name = "lblPhase";
            this.lblPhase.Size = new System.Drawing.Size(185, 13);
            this.lblPhase.TabIndex = 1;
            this.lblPhase.Text = "Initializing";
            // 
            // lblFields
            // 
            this.lblFields.AutoSize = true;
            this.lblFields.Location = new System.Drawing.Point(12, 84);
            this.lblFields.Name = "lblFields";
            this.lblFields.Size = new System.Drawing.Size(34, 13);
            this.lblFields.TabIndex = 2;
            this.lblFields.Text = "Fields";
            // 
            // grdFields
            // 
            this.grdFields.AllowUserToAddRows = false;
            this.grdFields.AllowUserToDeleteRows = false;
            this.grdFields.AllowUserToResizeRows = false;
            this.grdFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFields.Location = new System.Drawing.Point(1, 127);
            this.grdFields.MultiSelect = false;
            this.grdFields.Name = "grdFields";
            this.grdFields.RowHeadersVisible = false;
            this.grdFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdFields.Size = new System.Drawing.Size(489, 167);
            this.grdFields.TabIndex = 4;
            this.grdFields.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grdFields_RowsAdded);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(13, 13);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(54, 13);
            this.lblFileName.TabIndex = 5;
            this.lblFileName.Text = "File Name";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(73, 10);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(387, 20);
            this.txtFileName.TabIndex = 6;
            // 
            // btnFileSave
            // 
            this.btnFileSave.Location = new System.Drawing.Point(466, 7);
            this.btnFileSave.Name = "btnFileSave";
            this.btnFileSave.Size = new System.Drawing.Size(24, 23);
            this.btnFileSave.TabIndex = 7;
            this.btnFileSave.Text = "...";
            this.btnFileSave.UseVisualStyleBackColor = true;
            this.btnFileSave.Click += new System.EventHandler(this.btnFileSave_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(246, 49);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 8;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(246, 74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFieldCount
            // 
            this.lblFieldCount.Location = new System.Drawing.Point(58, 84);
            this.lblFieldCount.Name = "lblFieldCount";
            this.lblFieldCount.Size = new System.Drawing.Size(100, 16);
            this.lblFieldCount.TabIndex = 10;
            this.lblFieldCount.Text = "0 / 0";
            // 
            // lblShapeCount
            // 
            this.lblShapeCount.Location = new System.Drawing.Point(58, 100);
            this.lblShapeCount.Name = "lblShapeCount";
            this.lblShapeCount.Size = new System.Drawing.Size(100, 16);
            this.lblShapeCount.TabIndex = 11;
            this.lblShapeCount.Text = "0 / 0";
            // 
            // lblShapes
            // 
            this.lblShapes.AutoSize = true;
            this.lblShapes.Location = new System.Drawing.Point(3, 100);
            this.lblShapes.Name = "lblShapes";
            this.lblShapes.Size = new System.Drawing.Size(43, 13);
            this.lblShapes.TabIndex = 12;
            this.lblShapes.Text = "Shapes";
            // 
            // FieldStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 290);
            this.Controls.Add(this.lblShapes);
            this.Controls.Add(this.lblShapeCount);
            this.Controls.Add(this.lblFieldCount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnFileSave);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.grdFields);
            this.Controls.Add(this.lblFields);
            this.Controls.Add(this.lblPhase);
            this.Controls.Add(this.lblPhaseIntro);
            this.Name = "FieldStatus";
            this.Text = "FieldStatus";
            this.SizeChanged += new System.EventHandler(this.FieldStatus_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.grdFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPhaseIntro;
        private System.Windows.Forms.Label lblPhase;
        private System.Windows.Forms.Label lblFields;
        private System.Windows.Forms.DataGridView grdFields;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFileSave;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnCancel;
        private Label lblFieldCount;
        private Label lblShapeCount;
        private Label lblShapes;
    }
}