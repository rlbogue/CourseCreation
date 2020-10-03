namespace BookPublishing
{
    partial class ManifestStatusDialog
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
            this.components = new System.ComponentModel.Container();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblXofY = new System.Windows.Forms.Label();
            this.lblPhase = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.txtTopMargin = new System.Windows.Forms.TextBox();
            this.lblMargins = new System.Windows.Forms.Label();
            this.txtLeftMargin = new System.Windows.Forms.TextBox();
            this.txtBottomMargin = new System.Windows.Forms.TextBox();
            this.txtRightMargin = new System.Windows.Forms.TextBox();
            this.lblGutter = new System.Windows.Forms.Label();
            this.txtGutterMargin = new System.Windows.Forms.TextBox();
            this.lblPageHeight = new System.Windows.Forms.Label();
            this.lblPageWidth = new System.Windows.Forms.Label();
            this.txtPageHeight = new System.Windows.Forms.TextBox();
            this.txtPageWidth = new System.Windows.Forms.TextBox();
            this.lblImageBrightness = new System.Windows.Forms.Label();
            this.lblImageContrast = new System.Windows.Forms.Label();
            this.txtImageBrightness = new System.Windows.Forms.TextBox();
            this.txtImageContrast = new System.Windows.Forms.TextBox();
            this.chkBlackWhite = new System.Windows.Forms.CheckBox();
            this.chkClearComments = new System.Windows.Forms.CheckBox();
            this.chkAcceptRevisions = new System.Windows.Forms.CheckBox();
            this.chkNumbersToText = new System.Windows.Forms.CheckBox();
            this.chkFieldsToText = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(527, 295);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 500;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(13, 142);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(51, 13);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "FileName";
            // 
            // lblXofY
            // 
            this.lblXofY.AutoSize = true;
            this.lblXofY.Location = new System.Drawing.Point(13, 155);
            this.lblXofY.Name = "lblXofY";
            this.lblXofY.Size = new System.Drawing.Size(55, 13);
            this.lblXofY.TabIndex = 2;
            this.lblXofY.Text = "File X of Y";
            // 
            // lblPhase
            // 
            this.lblPhase.AutoSize = true;
            this.lblPhase.Location = new System.Drawing.Point(13, 168);
            this.lblPhase.Name = "lblPhase";
            this.lblPhase.Size = new System.Drawing.Size(85, 13);
            this.lblPhase.TabIndex = 3;
            this.lblPhase.Text = "Preinitialization...";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(527, 266);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 5;
            this.btnGo.Text = "Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(12, 184);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.Size = new System.Drawing.Size(505, 134);
            this.txtMessages.TabIndex = 6;
            // 
            // txtTopMargin
            // 
            this.txtTopMargin.Location = new System.Drawing.Point(59, 11);
            this.txtTopMargin.Name = "txtTopMargin";
            this.txtTopMargin.Size = new System.Drawing.Size(41, 20);
            this.txtTopMargin.TabIndex = 7;
            this.txtTopMargin.Text = "0.4";
            // 
            // lblMargins
            // 
            this.lblMargins.AutoSize = true;
            this.lblMargins.Location = new System.Drawing.Point(56, 44);
            this.lblMargins.Name = "lblMargins";
            this.lblMargins.Size = new System.Drawing.Size(44, 13);
            this.lblMargins.TabIndex = 10;
            this.lblMargins.Text = "Margins";
            // 
            // txtLeftMargin
            // 
            this.txtLeftMargin.Location = new System.Drawing.Point(9, 41);
            this.txtLeftMargin.Name = "txtLeftMargin";
            this.txtLeftMargin.Size = new System.Drawing.Size(41, 20);
            this.txtLeftMargin.TabIndex = 11;
            this.txtLeftMargin.Text = "0.4";
            // 
            // txtBottomMargin
            // 
            this.txtBottomMargin.Location = new System.Drawing.Point(59, 69);
            this.txtBottomMargin.Name = "txtBottomMargin";
            this.txtBottomMargin.Size = new System.Drawing.Size(41, 20);
            this.txtBottomMargin.TabIndex = 12;
            this.txtBottomMargin.Text = "0.4";
            // 
            // txtRightMargin
            // 
            this.txtRightMargin.Location = new System.Drawing.Point(106, 41);
            this.txtRightMargin.Name = "txtRightMargin";
            this.txtRightMargin.Size = new System.Drawing.Size(41, 20);
            this.txtRightMargin.TabIndex = 13;
            this.txtRightMargin.Text = "0.4";
            // 
            // lblGutter
            // 
            this.lblGutter.AutoSize = true;
            this.lblGutter.Location = new System.Drawing.Point(107, 12);
            this.lblGutter.Name = "lblGutter";
            this.lblGutter.Size = new System.Drawing.Size(36, 13);
            this.lblGutter.TabIndex = 14;
            this.lblGutter.Text = "Gutter";
            // 
            // txtGutterMargin
            // 
            this.txtGutterMargin.Location = new System.Drawing.Point(149, 9);
            this.txtGutterMargin.Name = "txtGutterMargin";
            this.txtGutterMargin.Size = new System.Drawing.Size(41, 20);
            this.txtGutterMargin.TabIndex = 15;
            this.txtGutterMargin.Text = "0.35";
            // 
            // lblPageHeight
            // 
            this.lblPageHeight.AutoSize = true;
            this.lblPageHeight.Location = new System.Drawing.Point(211, 12);
            this.lblPageHeight.Name = "lblPageHeight";
            this.lblPageHeight.Size = new System.Drawing.Size(66, 13);
            this.lblPageHeight.TabIndex = 16;
            this.lblPageHeight.Text = "Page Height";
            // 
            // lblPageWidth
            // 
            this.lblPageWidth.AutoSize = true;
            this.lblPageWidth.Location = new System.Drawing.Point(214, 40);
            this.lblPageWidth.Name = "lblPageWidth";
            this.lblPageWidth.Size = new System.Drawing.Size(63, 13);
            this.lblPageWidth.TabIndex = 17;
            this.lblPageWidth.Text = "Page Width";
            // 
            // txtPageHeight
            // 
            this.txtPageHeight.Location = new System.Drawing.Point(284, 11);
            this.txtPageHeight.Name = "txtPageHeight";
            this.txtPageHeight.Size = new System.Drawing.Size(45, 20);
            this.txtPageHeight.TabIndex = 18;
            this.txtPageHeight.Text = "9.68";
            // 
            // txtPageWidth
            // 
            this.txtPageWidth.Location = new System.Drawing.Point(283, 37);
            this.txtPageWidth.Name = "txtPageWidth";
            this.txtPageWidth.Size = new System.Drawing.Size(45, 20);
            this.txtPageWidth.TabIndex = 19;
            this.txtPageWidth.Text = "7.44";
            // 
            // lblImageBrightness
            // 
            this.lblImageBrightness.AutoSize = true;
            this.lblImageBrightness.Location = new System.Drawing.Point(191, 68);
            this.lblImageBrightness.Name = "lblImageBrightness";
            this.lblImageBrightness.Size = new System.Drawing.Size(88, 13);
            this.lblImageBrightness.TabIndex = 20;
            this.lblImageBrightness.Text = "Image Brightness";
            // 
            // lblImageContrast
            // 
            this.lblImageContrast.AutoSize = true;
            this.lblImageContrast.Location = new System.Drawing.Point(201, 98);
            this.lblImageContrast.Name = "lblImageContrast";
            this.lblImageContrast.Size = new System.Drawing.Size(78, 13);
            this.lblImageContrast.TabIndex = 21;
            this.lblImageContrast.Text = "Image Contrast";
            // 
            // txtImageBrightness
            // 
            this.txtImageBrightness.Location = new System.Drawing.Point(285, 65);
            this.txtImageBrightness.Name = "txtImageBrightness";
            this.txtImageBrightness.Size = new System.Drawing.Size(45, 20);
            this.txtImageBrightness.TabIndex = 22;
            this.txtImageBrightness.Text = "-20";
            // 
            // txtImageContrast
            // 
            this.txtImageContrast.Location = new System.Drawing.Point(285, 91);
            this.txtImageContrast.Name = "txtImageContrast";
            this.txtImageContrast.Size = new System.Drawing.Size(45, 20);
            this.txtImageContrast.TabIndex = 23;
            this.txtImageContrast.Text = "30";
            // 
            // chkBlackWhite
            // 
            this.chkBlackWhite.AutoSize = true;
            this.chkBlackWhite.Checked = true;
            this.chkBlackWhite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBlackWhite.Location = new System.Drawing.Point(352, 13);
            this.chkBlackWhite.Name = "chkBlackWhite";
            this.chkBlackWhite.Size = new System.Drawing.Size(105, 17);
            this.chkBlackWhite.TabIndex = 24;
            this.chkBlackWhite.Text = "Black and White";
            this.chkBlackWhite.UseVisualStyleBackColor = true;
            // 
            // chkClearComments
            // 
            this.chkClearComments.AutoSize = true;
            this.chkClearComments.Checked = true;
            this.chkClearComments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClearComments.Location = new System.Drawing.Point(352, 37);
            this.chkClearComments.Name = "chkClearComments";
            this.chkClearComments.Size = new System.Drawing.Size(102, 17);
            this.chkClearComments.TabIndex = 25;
            this.chkClearComments.Text = "Clear Comments";
            this.chkClearComments.UseVisualStyleBackColor = true;
            // 
            // chkAcceptRevisions
            // 
            this.chkAcceptRevisions.AutoSize = true;
            this.chkAcceptRevisions.Checked = true;
            this.chkAcceptRevisions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAcceptRevisions.Location = new System.Drawing.Point(352, 61);
            this.chkAcceptRevisions.Name = "chkAcceptRevisions";
            this.chkAcceptRevisions.Size = new System.Drawing.Size(109, 17);
            this.chkAcceptRevisions.TabIndex = 26;
            this.chkAcceptRevisions.Text = "Accept Revisions";
            this.chkAcceptRevisions.UseVisualStyleBackColor = true;
            // 
            // chkNumbersToText
            // 
            this.chkNumbersToText.AutoSize = true;
            this.chkNumbersToText.Checked = true;
            this.chkNumbersToText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNumbersToText.Location = new System.Drawing.Point(352, 85);
            this.chkNumbersToText.Name = "chkNumbersToText";
            this.chkNumbersToText.Size = new System.Drawing.Size(104, 17);
            this.chkNumbersToText.TabIndex = 27;
            this.chkNumbersToText.Text = "Numbers to Text";
            this.chkNumbersToText.UseVisualStyleBackColor = true;
            // 
            // chkFieldsToText
            // 
            this.chkFieldsToText.AutoSize = true;
            this.chkFieldsToText.Checked = true;
            this.chkFieldsToText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFieldsToText.Location = new System.Drawing.Point(352, 109);
            this.chkFieldsToText.Name = "chkFieldsToText";
            this.chkFieldsToText.Size = new System.Drawing.Size(89, 17);
            this.chkFieldsToText.TabIndex = 28;
            this.chkFieldsToText.Text = "Fields to Text";
            this.chkFieldsToText.UseVisualStyleBackColor = true;
            // 
            // ManifestStatusDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 330);
            this.Controls.Add(this.chkFieldsToText);
            this.Controls.Add(this.chkNumbersToText);
            this.Controls.Add(this.chkAcceptRevisions);
            this.Controls.Add(this.chkClearComments);
            this.Controls.Add(this.chkBlackWhite);
            this.Controls.Add(this.txtImageContrast);
            this.Controls.Add(this.txtImageBrightness);
            this.Controls.Add(this.lblImageContrast);
            this.Controls.Add(this.lblImageBrightness);
            this.Controls.Add(this.txtPageWidth);
            this.Controls.Add(this.txtPageHeight);
            this.Controls.Add(this.lblPageWidth);
            this.Controls.Add(this.lblPageHeight);
            this.Controls.Add(this.txtGutterMargin);
            this.Controls.Add(this.lblGutter);
            this.Controls.Add(this.txtRightMargin);
            this.Controls.Add(this.txtBottomMargin);
            this.Controls.Add(this.txtLeftMargin);
            this.Controls.Add(this.lblMargins);
            this.Controls.Add(this.txtTopMargin);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lblPhase);
            this.Controls.Add(this.lblXofY);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.cmdCancel);
            this.Name = "ManifestStatusDialog";
            this.Text = "Status of Compilation";
            this.Load += new System.EventHandler(this.ManifestStatusDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblXofY;
        private System.Windows.Forms.Label lblPhase;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.TextBox txtTopMargin;
        private System.Windows.Forms.Label lblMargins;
        private System.Windows.Forms.TextBox txtLeftMargin;
        private System.Windows.Forms.TextBox txtBottomMargin;
        private System.Windows.Forms.TextBox txtRightMargin;
        private System.Windows.Forms.Label lblGutter;
        private System.Windows.Forms.TextBox txtGutterMargin;
        private System.Windows.Forms.Label lblPageHeight;
        private System.Windows.Forms.Label lblPageWidth;
        private System.Windows.Forms.TextBox txtPageHeight;
        private System.Windows.Forms.TextBox txtPageWidth;
        private System.Windows.Forms.Label lblImageBrightness;
        private System.Windows.Forms.Label lblImageContrast;
        private System.Windows.Forms.TextBox txtImageBrightness;
        private System.Windows.Forms.TextBox txtImageContrast;
        private System.Windows.Forms.CheckBox chkBlackWhite;
        private System.Windows.Forms.CheckBox chkClearComments;
        private System.Windows.Forms.CheckBox chkAcceptRevisions;
        private System.Windows.Forms.CheckBox chkNumbersToText;
        private System.Windows.Forms.CheckBox chkFieldsToText;
    }
}