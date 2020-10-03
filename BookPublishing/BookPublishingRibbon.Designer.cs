namespace BookPublishing
{
    partial class BookPublishingRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public BookPublishingRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            try { base.Dispose(disposing); } catch { }
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.grpBookPublishing = this.Factory.CreateRibbonGroup();
            this.btnBinder = this.Factory.CreateRibbonButton();
            this.btnFields = this.Factory.CreateRibbonButton();
            this.btnSavePDFForm = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.grpBookPublishing.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.grpBookPublishing);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // grpBookPublishing
            // 
            this.grpBookPublishing.Items.Add(this.btnBinder);
            this.grpBookPublishing.Items.Add(this.btnFields);
            this.grpBookPublishing.Items.Add(this.btnSavePDFForm);
            this.grpBookPublishing.Label = "Book Publishing";
            this.grpBookPublishing.Name = "grpBookPublishing";
            // 
            // btnBinder
            // 
            this.btnBinder.Description = "Merge the contents of multiple word files into this file based on a manifest";
            this.btnBinder.Label = "Assemble Binder";
            this.btnBinder.Name = "btnBinder";
            this.btnBinder.ScreenTip = "Use a manifest file to create a master document";
            this.btnBinder.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnBinder_Click);
            // 
            // btnFields
            // 
            this.btnFields.Description = "Merge the contents of multiple word files into this file based on a manifest";
            this.btnFields.Label = "Fields List";
            this.btnFields.Name = "btnFields";
            this.btnFields.ScreenTip = "Use a manifest file to create a master document";
            this.btnFields.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnFields_Click);
            // 
            // btnSavePDFForm
            // 
            this.btnSavePDFForm.Description = "Merge the contents of multiple word files into this file based on a manifest";
            this.btnSavePDFForm.Label = "Save PDF Form";
            this.btnSavePDFForm.Name = "btnSavePDFForm";
            this.btnSavePDFForm.ScreenTip = "Use a manifest file to create a master document";
            this.btnSavePDFForm.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSavePDFForm_Click);
            // 
            // BookPublishingRibbon
            // 
            this.Name = "BookPublishingRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.BookPublishingRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.grpBookPublishing.ResumeLayout(false);
            this.grpBookPublishing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpBookPublishing;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnBinder;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnFields;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSavePDFForm;
    }

    partial class ThisRibbonCollection
    {
        internal BookPublishingRibbon BookPublishingRibbon
        {
            get { return this.GetRibbon<BookPublishingRibbon>(); }
        }
    }
}
