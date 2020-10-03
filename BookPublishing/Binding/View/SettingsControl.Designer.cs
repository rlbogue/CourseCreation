namespace BookPublishing.Binding.View
{
    partial class SettingsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpAssembly = new System.Windows.Forms.GroupBox();
            this.chkResetPageNumber = new System.Windows.Forms.CheckBox();
            this.grpSeparator = new System.Windows.Forms.GroupBox();
            this.rdoNoBreak = new System.Windows.Forms.RadioButton();
            this.rdoPage = new System.Windows.Forms.RadioButton();
            this.rdoSection = new System.Windows.Forms.RadioButton();
            this.chkCopyToTemp = new System.Windows.Forms.CheckBox();
            this.grmMargins = new System.Windows.Forms.GroupBox();
            this.chkSetMargins = new System.Windows.Forms.CheckBox();
            this.lblTopMargin = new System.Windows.Forms.Label();
            this.txtTop = new System.Windows.Forms.TextBox();
            this.txtBottom = new System.Windows.Forms.TextBox();
            this.lblBottom = new System.Windows.Forms.Label();
            this.txtLeft = new System.Windows.Forms.TextBox();
            this.lblLeft = new System.Windows.Forms.Label();
            this.txtRight = new System.Windows.Forms.TextBox();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblGutter = new System.Windows.Forms.Label();
            this.txtGutter = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpText = new System.Windows.Forms.GroupBox();
            this.chkClearComments = new System.Windows.Forms.CheckBox();
            this.chkConvertFieldsToText = new System.Windows.Forms.CheckBox();
            this.chkAcceptRevisions = new System.Windows.Forms.CheckBox();
            this.chkConvertNumbersToText = new System.Windows.Forms.CheckBox();
            this.grpPage = new System.Windows.Forms.GroupBox();
            this.grpOrientation = new System.Windows.Forms.GroupBox();
            this.rdoPortrait = new System.Windows.Forms.RadioButton();
            this.rdoLandscape = new System.Windows.Forms.RadioButton();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.chkSetPage = new System.Windows.Forms.CheckBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.chkStartOnOdd = new System.Windows.Forms.CheckBox();
            this.grpImages = new System.Windows.Forms.GroupBox();
            this.lblMinHeight = new System.Windows.Forms.Label();
            this.txtMinHeight = new System.Windows.Forms.TextBox();
            this.lblMaxWidth = new System.Windows.Forms.Label();
            this.lblMaxHeight = new System.Windows.Forms.Label();
            this.txtMaxWidth = new System.Windows.Forms.TextBox();
            this.txtMaxHeight = new System.Windows.Forms.TextBox();
            this.txtContrast = new System.Windows.Forms.TextBox();
            this.chkSetImages = new System.Windows.Forms.CheckBox();
            this.txtBrightness = new System.Windows.Forms.TextBox();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.lblContrast = new System.Windows.Forms.Label();
            this.grpAssembly.SuspendLayout();
            this.grpSeparator.SuspendLayout();
            this.grmMargins.SuspendLayout();
            this.grpText.SuspendLayout();
            this.grpPage.SuspendLayout();
            this.grpOrientation.SuspendLayout();
            this.grpImages.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAssembly
            // 
            this.grpAssembly.Controls.Add(this.chkResetPageNumber);
            this.grpAssembly.Controls.Add(this.grpSeparator);
            this.grpAssembly.Controls.Add(this.chkCopyToTemp);
            this.grpAssembly.Location = new System.Drawing.Point(3, 187);
            this.grpAssembly.Name = "grpAssembly";
            this.grpAssembly.Size = new System.Drawing.Size(110, 146);
            this.grpAssembly.TabIndex = 30;
            this.grpAssembly.TabStop = false;
            this.grpAssembly.Text = "Assembly";
            // 
            // chkResetPageNumber
            // 
            this.chkResetPageNumber.AutoSize = true;
            this.chkResetPageNumber.Location = new System.Drawing.Point(12, 121);
            this.chkResetPageNumber.Name = "chkResetPageNumber";
            this.chkResetPageNumber.Size = new System.Drawing.Size(85, 17);
            this.chkResetPageNumber.TabIndex = 27;
            this.chkResetPageNumber.Text = "Page Num 1";
            this.chkResetPageNumber.UseVisualStyleBackColor = true;
            // 
            // grpSeparator
            // 
            this.grpSeparator.Controls.Add(this.rdoNoBreak);
            this.grpSeparator.Controls.Add(this.rdoPage);
            this.grpSeparator.Controls.Add(this.rdoSection);
            this.grpSeparator.Location = new System.Drawing.Point(6, 19);
            this.grpSeparator.Name = "grpSeparator";
            this.grpSeparator.Size = new System.Drawing.Size(80, 78);
            this.grpSeparator.TabIndex = 26;
            this.grpSeparator.TabStop = false;
            this.grpSeparator.Text = "Seperator";
            // 
            // rdoNoBreak
            // 
            this.rdoNoBreak.AutoSize = true;
            this.rdoNoBreak.Location = new System.Drawing.Point(6, 19);
            this.rdoNoBreak.Name = "rdoNoBreak";
            this.rdoNoBreak.Size = new System.Drawing.Size(51, 17);
            this.rdoNoBreak.TabIndex = 3;
            this.rdoNoBreak.TabStop = true;
            this.rdoNoBreak.Text = "None";
            this.rdoNoBreak.UseVisualStyleBackColor = true;
            this.rdoNoBreak.CheckedChanged += new System.EventHandler(this.rdoNoBreak_CheckedChanged);
            // 
            // rdoPage
            // 
            this.rdoPage.AutoSize = true;
            this.rdoPage.Location = new System.Drawing.Point(6, 36);
            this.rdoPage.Name = "rdoPage";
            this.rdoPage.Size = new System.Drawing.Size(50, 17);
            this.rdoPage.TabIndex = 4;
            this.rdoPage.TabStop = true;
            this.rdoPage.Text = "Page";
            this.rdoPage.UseVisualStyleBackColor = true;
            this.rdoPage.CheckedChanged += new System.EventHandler(this.rdoPage_CheckedChanged);
            // 
            // rdoSection
            // 
            this.rdoSection.AutoSize = true;
            this.rdoSection.Location = new System.Drawing.Point(6, 53);
            this.rdoSection.Name = "rdoSection";
            this.rdoSection.Size = new System.Drawing.Size(61, 17);
            this.rdoSection.TabIndex = 5;
            this.rdoSection.TabStop = true;
            this.rdoSection.Text = "Section";
            this.rdoSection.UseVisualStyleBackColor = true;
            this.rdoSection.CheckedChanged += new System.EventHandler(this.rdoSection_CheckedChanged);
            // 
            // chkCopyToTemp
            // 
            this.chkCopyToTemp.AutoSize = true;
            this.chkCopyToTemp.Location = new System.Drawing.Point(12, 97);
            this.chkCopyToTemp.Name = "chkCopyToTemp";
            this.chkCopyToTemp.Size = new System.Drawing.Size(92, 17);
            this.chkCopyToTemp.TabIndex = 6;
            this.chkCopyToTemp.Text = "Copy to Temp";
            this.chkCopyToTemp.UseVisualStyleBackColor = true;
            // 
            // grmMargins
            // 
            this.grmMargins.Controls.Add(this.chkSetMargins);
            this.grmMargins.Controls.Add(this.lblTopMargin);
            this.grmMargins.Controls.Add(this.txtTop);
            this.grmMargins.Controls.Add(this.txtBottom);
            this.grmMargins.Controls.Add(this.lblBottom);
            this.grmMargins.Controls.Add(this.txtLeft);
            this.grmMargins.Controls.Add(this.lblLeft);
            this.grmMargins.Controls.Add(this.txtRight);
            this.grmMargins.Controls.Add(this.lblRight);
            this.grmMargins.Controls.Add(this.lblGutter);
            this.grmMargins.Controls.Add(this.txtGutter);
            this.grmMargins.Controls.Add(this.txtHeader);
            this.grmMargins.Controls.Add(this.txtFooter);
            this.grmMargins.Controls.Add(this.lblFooter);
            this.grmMargins.Controls.Add(this.lblHeader);
            this.grmMargins.Location = new System.Drawing.Point(124, 3);
            this.grmMargins.Name = "grmMargins";
            this.grmMargins.Size = new System.Drawing.Size(102, 248);
            this.grmMargins.TabIndex = 29;
            this.grmMargins.TabStop = false;
            this.grmMargins.Text = "Margins";
            // 
            // chkSetMargins
            // 
            this.chkSetMargins.AutoSize = true;
            this.chkSetMargins.Location = new System.Drawing.Point(12, 19);
            this.chkSetMargins.Name = "chkSetMargins";
            this.chkSetMargins.Size = new System.Drawing.Size(52, 17);
            this.chkSetMargins.TabIndex = 23;
            this.chkSetMargins.Text = "Apply";
            this.chkSetMargins.UseVisualStyleBackColor = true;
            this.chkSetMargins.CheckedChanged += new System.EventHandler(this.chkSetMargins_CheckedChanged);
            // 
            // lblTopMargin
            // 
            this.lblTopMargin.AutoSize = true;
            this.lblTopMargin.Location = new System.Drawing.Point(20, 41);
            this.lblTopMargin.Name = "lblTopMargin";
            this.lblTopMargin.Size = new System.Drawing.Size(26, 13);
            this.lblTopMargin.TabIndex = 0;
            this.lblTopMargin.Text = "Top";
            this.lblTopMargin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTop
            // 
            this.txtTop.Location = new System.Drawing.Point(52, 41);
            this.txtTop.Name = "txtTop";
            this.txtTop.Size = new System.Drawing.Size(30, 20);
            this.txtTop.TabIndex = 6;
            this.txtTop.Text = "-1";
            // 
            // txtBottom
            // 
            this.txtBottom.Location = new System.Drawing.Point(52, 68);
            this.txtBottom.Name = "txtBottom";
            this.txtBottom.Size = new System.Drawing.Size(30, 20);
            this.txtBottom.TabIndex = 7;
            this.txtBottom.Text = "-1";
            // 
            // lblBottom
            // 
            this.lblBottom.AutoSize = true;
            this.lblBottom.Location = new System.Drawing.Point(6, 72);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(40, 13);
            this.lblBottom.TabIndex = 1;
            this.lblBottom.Text = "Bottom";
            this.lblBottom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLeft
            // 
            this.txtLeft.Location = new System.Drawing.Point(52, 94);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new System.Drawing.Size(30, 20);
            this.txtLeft.TabIndex = 8;
            this.txtLeft.Text = "-1";
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Location = new System.Drawing.Point(21, 97);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(25, 13);
            this.lblLeft.TabIndex = 2;
            this.lblLeft.Text = "Left";
            this.lblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRight
            // 
            this.txtRight.Location = new System.Drawing.Point(52, 120);
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new System.Drawing.Size(30, 20);
            this.txtRight.TabIndex = 9;
            this.txtRight.Text = "-1";
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Location = new System.Drawing.Point(14, 120);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(32, 13);
            this.lblRight.TabIndex = 3;
            this.lblRight.Text = "Right";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblGutter
            // 
            this.lblGutter.AutoSize = true;
            this.lblGutter.Location = new System.Drawing.Point(9, 145);
            this.lblGutter.Name = "lblGutter";
            this.lblGutter.Size = new System.Drawing.Size(36, 13);
            this.lblGutter.TabIndex = 10;
            this.lblGutter.Text = "Gutter";
            this.lblGutter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtGutter
            // 
            this.txtGutter.Location = new System.Drawing.Point(52, 145);
            this.txtGutter.Name = "txtGutter";
            this.txtGutter.Size = new System.Drawing.Size(30, 20);
            this.txtGutter.TabIndex = 13;
            this.txtGutter.Text = "-1";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(52, 171);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(30, 20);
            this.txtHeader.TabIndex = 11;
            this.txtHeader.Text = "-1";
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(52, 202);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(30, 20);
            this.txtFooter.TabIndex = 12;
            this.txtFooter.Text = "-1";
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new System.Drawing.Point(9, 202);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(37, 13);
            this.lblFooter.TabIndex = 5;
            this.lblFooter.Text = "Footer";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(6, 174);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(42, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Header";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpText
            // 
            this.grpText.Controls.Add(this.chkClearComments);
            this.grpText.Controls.Add(this.chkConvertFieldsToText);
            this.grpText.Controls.Add(this.chkAcceptRevisions);
            this.grpText.Controls.Add(this.chkConvertNumbersToText);
            this.grpText.Location = new System.Drawing.Point(232, 170);
            this.grpText.Name = "grpText";
            this.grpText.Size = new System.Drawing.Size(159, 113);
            this.grpText.TabIndex = 26;
            this.grpText.TabStop = false;
            this.grpText.Text = "Text";
            // 
            // chkClearComments
            // 
            this.chkClearComments.AutoSize = true;
            this.chkClearComments.Location = new System.Drawing.Point(6, 17);
            this.chkClearComments.Name = "chkClearComments";
            this.chkClearComments.Size = new System.Drawing.Size(102, 17);
            this.chkClearComments.TabIndex = 3;
            this.chkClearComments.Text = "Clear Comments";
            this.chkClearComments.UseVisualStyleBackColor = true;
            // 
            // chkConvertFieldsToText
            // 
            this.chkConvertFieldsToText.AutoSize = true;
            this.chkConvertFieldsToText.Location = new System.Drawing.Point(6, 83);
            this.chkConvertFieldsToText.Name = "chkConvertFieldsToText";
            this.chkConvertFieldsToText.Size = new System.Drawing.Size(129, 17);
            this.chkConvertFieldsToText.TabIndex = 6;
            this.chkConvertFieldsToText.Text = "Convert Fields to Text";
            this.chkConvertFieldsToText.UseVisualStyleBackColor = true;
            // 
            // chkAcceptRevisions
            // 
            this.chkAcceptRevisions.AutoSize = true;
            this.chkAcceptRevisions.Location = new System.Drawing.Point(6, 39);
            this.chkAcceptRevisions.Name = "chkAcceptRevisions";
            this.chkAcceptRevisions.Size = new System.Drawing.Size(109, 17);
            this.chkAcceptRevisions.TabIndex = 4;
            this.chkAcceptRevisions.Text = "Accept Revisions";
            this.chkAcceptRevisions.UseVisualStyleBackColor = true;
            // 
            // chkConvertNumbersToText
            // 
            this.chkConvertNumbersToText.AutoSize = true;
            this.chkConvertNumbersToText.Location = new System.Drawing.Point(6, 60);
            this.chkConvertNumbersToText.Name = "chkConvertNumbersToText";
            this.chkConvertNumbersToText.Size = new System.Drawing.Size(153, 17);
            this.chkConvertNumbersToText.TabIndex = 5;
            this.chkConvertNumbersToText.Text = "Convert Numbering to Text";
            this.chkConvertNumbersToText.UseVisualStyleBackColor = true;
            // 
            // grpPage
            // 
            this.grpPage.Controls.Add(this.grpOrientation);
            this.grpPage.Controls.Add(this.lblHeight);
            this.grpPage.Controls.Add(this.txtHeight);
            this.grpPage.Controls.Add(this.chkSetPage);
            this.grpPage.Controls.Add(this.txtWidth);
            this.grpPage.Controls.Add(this.lblWidth);
            this.grpPage.Location = new System.Drawing.Point(3, 3);
            this.grpPage.Name = "grpPage";
            this.grpPage.Size = new System.Drawing.Size(115, 165);
            this.grpPage.TabIndex = 27;
            this.grpPage.TabStop = false;
            this.grpPage.Text = "Page";
            // 
            // grpOrientation
            // 
            this.grpOrientation.Controls.Add(this.rdoPortrait);
            this.grpOrientation.Controls.Add(this.rdoLandscape);
            this.grpOrientation.Location = new System.Drawing.Point(6, 93);
            this.grpOrientation.Name = "grpOrientation";
            this.grpOrientation.Size = new System.Drawing.Size(95, 68);
            this.grpOrientation.TabIndex = 0;
            this.grpOrientation.TabStop = false;
            this.grpOrientation.Text = "Orientation";
            // 
            // rdoPortrait
            // 
            this.rdoPortrait.AutoSize = true;
            this.rdoPortrait.Checked = true;
            this.rdoPortrait.Location = new System.Drawing.Point(6, 19);
            this.rdoPortrait.Name = "rdoPortrait";
            this.rdoPortrait.Size = new System.Drawing.Size(58, 17);
            this.rdoPortrait.TabIndex = 14;
            this.rdoPortrait.TabStop = true;
            this.rdoPortrait.Text = "Portrait";
            this.rdoPortrait.UseVisualStyleBackColor = true;
            // 
            // rdoLandscape
            // 
            this.rdoLandscape.AutoSize = true;
            this.rdoLandscape.Location = new System.Drawing.Point(6, 42);
            this.rdoLandscape.Name = "rdoLandscape";
            this.rdoLandscape.Size = new System.Drawing.Size(78, 17);
            this.rdoLandscape.TabIndex = 15;
            this.rdoLandscape.Text = "Landscape";
            this.rdoLandscape.UseVisualStyleBackColor = true;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(6, 67);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(38, 13);
            this.lblHeight.TabIndex = 19;
            this.lblHeight.Text = "Height";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(48, 67);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(30, 20);
            this.txtHeight.TabIndex = 21;
            this.txtHeight.Text = "-1";
            // 
            // chkSetPage
            // 
            this.chkSetPage.AutoSize = true;
            this.chkSetPage.Location = new System.Drawing.Point(6, 19);
            this.chkSetPage.Name = "chkSetPage";
            this.chkSetPage.Size = new System.Drawing.Size(52, 17);
            this.chkSetPage.TabIndex = 23;
            this.chkSetPage.Text = "Apply";
            this.chkSetPage.UseVisualStyleBackColor = true;
            this.chkSetPage.CheckedChanged += new System.EventHandler(this.chkSetPage_CheckedChanged);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(48, 41);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(30, 20);
            this.txtWidth.TabIndex = 20;
            this.txtWidth.Text = "-1";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(9, 41);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(35, 13);
            this.lblWidth.TabIndex = 18;
            this.lblWidth.Text = "Width";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkStartOnOdd
            // 
            this.chkStartOnOdd.AutoSize = true;
            this.chkStartOnOdd.Location = new System.Drawing.Point(9, 170);
            this.chkStartOnOdd.Name = "chkStartOnOdd";
            this.chkStartOnOdd.Size = new System.Drawing.Size(86, 17);
            this.chkStartOnOdd.TabIndex = 17;
            this.chkStartOnOdd.Text = "Start on Odd";
            this.chkStartOnOdd.UseVisualStyleBackColor = true;
            // 
            // grpImages
            // 
            this.grpImages.Controls.Add(this.lblMinHeight);
            this.grpImages.Controls.Add(this.txtMinHeight);
            this.grpImages.Controls.Add(this.lblMaxWidth);
            this.grpImages.Controls.Add(this.lblMaxHeight);
            this.grpImages.Controls.Add(this.txtMaxWidth);
            this.grpImages.Controls.Add(this.txtMaxHeight);
            this.grpImages.Controls.Add(this.txtContrast);
            this.grpImages.Controls.Add(this.chkSetImages);
            this.grpImages.Controls.Add(this.txtBrightness);
            this.grpImages.Controls.Add(this.lblBrightness);
            this.grpImages.Controls.Add(this.lblContrast);
            this.grpImages.Location = new System.Drawing.Point(232, 3);
            this.grpImages.Name = "grpImages";
            this.grpImages.Size = new System.Drawing.Size(159, 165);
            this.grpImages.TabIndex = 28;
            this.grpImages.TabStop = false;
            this.grpImages.Text = "Images";
            // 
            // lblMinHeight
            // 
            this.lblMinHeight.AutoSize = true;
            this.lblMinHeight.Location = new System.Drawing.Point(11, 139);
            this.lblMinHeight.Name = "lblMinHeight";
            this.lblMinHeight.Size = new System.Drawing.Size(58, 13);
            this.lblMinHeight.TabIndex = 14;
            this.lblMinHeight.Text = "Min Height";
            this.lblMinHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMinHeight
            // 
            this.txtMinHeight.Location = new System.Drawing.Point(78, 139);
            this.txtMinHeight.Name = "txtMinHeight";
            this.txtMinHeight.Size = new System.Drawing.Size(30, 20);
            this.txtMinHeight.TabIndex = 13;
            this.txtMinHeight.Text = "-1";
            // 
            // lblMaxWidth
            // 
            this.lblMaxWidth.AutoSize = true;
            this.lblMaxWidth.Location = new System.Drawing.Point(11, 114);
            this.lblMaxWidth.Name = "lblMaxWidth";
            this.lblMaxWidth.Size = new System.Drawing.Size(58, 13);
            this.lblMaxWidth.TabIndex = 12;
            this.lblMaxWidth.Text = "Max Width";
            this.lblMaxWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMaxHeight
            // 
            this.lblMaxHeight.AutoSize = true;
            this.lblMaxHeight.Location = new System.Drawing.Point(12, 87);
            this.lblMaxHeight.Name = "lblMaxHeight";
            this.lblMaxHeight.Size = new System.Drawing.Size(61, 13);
            this.lblMaxHeight.TabIndex = 11;
            this.lblMaxHeight.Text = "Max Height";
            this.lblMaxHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaxWidth
            // 
            this.txtMaxWidth.Location = new System.Drawing.Point(78, 113);
            this.txtMaxWidth.Name = "txtMaxWidth";
            this.txtMaxWidth.Size = new System.Drawing.Size(30, 20);
            this.txtMaxWidth.TabIndex = 10;
            this.txtMaxWidth.Text = "-1";
            // 
            // txtMaxHeight
            // 
            this.txtMaxHeight.Location = new System.Drawing.Point(78, 87);
            this.txtMaxHeight.Name = "txtMaxHeight";
            this.txtMaxHeight.Size = new System.Drawing.Size(30, 20);
            this.txtMaxHeight.TabIndex = 9;
            this.txtMaxHeight.Text = "-1";
            // 
            // txtContrast
            // 
            this.txtContrast.Location = new System.Drawing.Point(78, 61);
            this.txtContrast.Name = "txtContrast";
            this.txtContrast.Size = new System.Drawing.Size(30, 20);
            this.txtContrast.TabIndex = 8;
            this.txtContrast.Text = "-1";
            // 
            // chkSetImages
            // 
            this.chkSetImages.AutoSize = true;
            this.chkSetImages.Location = new System.Drawing.Point(6, 17);
            this.chkSetImages.Name = "chkSetImages";
            this.chkSetImages.Size = new System.Drawing.Size(52, 17);
            this.chkSetImages.TabIndex = 2;
            this.chkSetImages.Text = "Apply";
            this.chkSetImages.UseVisualStyleBackColor = true;
            this.chkSetImages.CheckedChanged += new System.EventHandler(this.chkSetImages_CheckedChanged);
            // 
            // txtBrightness
            // 
            this.txtBrightness.Location = new System.Drawing.Point(78, 34);
            this.txtBrightness.Name = "txtBrightness";
            this.txtBrightness.Size = new System.Drawing.Size(30, 20);
            this.txtBrightness.TabIndex = 7;
            this.txtBrightness.Text = "-1";
            // 
            // lblBrightness
            // 
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Location = new System.Drawing.Point(17, 34);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(56, 13);
            this.lblBrightness.TabIndex = 0;
            this.lblBrightness.Text = "Brightness";
            this.lblBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContrast
            // 
            this.lblContrast.AutoSize = true;
            this.lblContrast.Location = new System.Drawing.Point(27, 61);
            this.lblContrast.Name = "lblContrast";
            this.lblContrast.Size = new System.Drawing.Size(46, 13);
            this.lblContrast.TabIndex = 1;
            this.lblContrast.Text = "Contrast";
            this.lblContrast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpAssembly);
            this.Controls.Add(this.grmMargins);
            this.Controls.Add(this.grpText);
            this.Controls.Add(this.grpPage);
            this.Controls.Add(this.grpImages);
            this.Controls.Add(this.chkStartOnOdd);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(395, 335);
            this.grpAssembly.ResumeLayout(false);
            this.grpAssembly.PerformLayout();
            this.grpSeparator.ResumeLayout(false);
            this.grpSeparator.PerformLayout();
            this.grmMargins.ResumeLayout(false);
            this.grmMargins.PerformLayout();
            this.grpText.ResumeLayout(false);
            this.grpText.PerformLayout();
            this.grpPage.ResumeLayout(false);
            this.grpPage.PerformLayout();
            this.grpOrientation.ResumeLayout(false);
            this.grpOrientation.PerformLayout();
            this.grpImages.ResumeLayout(false);
            this.grpImages.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAssembly;
        private System.Windows.Forms.GroupBox grpSeparator;
        private System.Windows.Forms.RadioButton rdoNoBreak;
        private System.Windows.Forms.RadioButton rdoPage;
        private System.Windows.Forms.RadioButton rdoSection;
        private System.Windows.Forms.CheckBox chkCopyToTemp;
        private System.Windows.Forms.GroupBox grmMargins;
        private System.Windows.Forms.CheckBox chkSetMargins;
        private System.Windows.Forms.Label lblTopMargin;
        private System.Windows.Forms.TextBox txtTop;
        private System.Windows.Forms.TextBox txtBottom;
        private System.Windows.Forms.Label lblBottom;
        private System.Windows.Forms.TextBox txtLeft;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.TextBox txtRight;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblGutter;
        private System.Windows.Forms.TextBox txtGutter;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grpText;
        private System.Windows.Forms.CheckBox chkClearComments;
        private System.Windows.Forms.CheckBox chkConvertFieldsToText;
        private System.Windows.Forms.CheckBox chkAcceptRevisions;
        private System.Windows.Forms.CheckBox chkConvertNumbersToText;
        private System.Windows.Forms.GroupBox grpPage;
        private System.Windows.Forms.GroupBox grpOrientation;
        private System.Windows.Forms.RadioButton rdoPortrait;
        private System.Windows.Forms.RadioButton rdoLandscape;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.CheckBox chkSetPage;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.CheckBox chkStartOnOdd;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.GroupBox grpImages;
        private System.Windows.Forms.TextBox txtContrast;
        private System.Windows.Forms.CheckBox chkSetImages;
        private System.Windows.Forms.TextBox txtBrightness;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.Label lblContrast;
        private System.Windows.Forms.Label lblMinHeight;
        private System.Windows.Forms.TextBox txtMinHeight;
        private System.Windows.Forms.Label lblMaxWidth;
        private System.Windows.Forms.Label lblMaxHeight;
        private System.Windows.Forms.TextBox txtMaxWidth;
        private System.Windows.Forms.TextBox txtMaxHeight;
        private System.Windows.Forms.CheckBox chkResetPageNumber;
    }
}
