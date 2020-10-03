using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BookPublishing.Utilities;

namespace BookPublishing
{
    public partial class ManifestStatusDialog : Form
    {
        public ManifestStatusDialog()
        {
            InitializeComponent();
        }

        public Utilities.Utility.UtilityStatus status = null;
        public List<string> files = null;
        public string manifestFile = null;

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private int lastLines = 0;

        public void UpdateUI()
        {
            if (status.fileIndex < status.files.Count) lblFileName.Text = status.files[status.fileIndex];
            if (status.fileIndex < status.files.Count)
            {
                lblXofY.Text = string.Format("File {0} of {1} - Image {2} of {3}", status.fileIndex, status.files.Count, status.currentImage, status.imageCount);
            }

            lblPhase.Text = status.phase;
            if (lastLines != status.messages.Count)
            {
                txtMessages.Lines = status.messages.ToArray();
                lastLines = status.messages.Count;
            }

            Validate(false);
        }

        private void ManifestStatusDialog_Load(object sender, EventArgs e)
        {

        }


        private Thread workerThread = null;

        private void WorkerThreadStart()
        {
            DoMergeWork();
            //BackgroundWorker worker = new BackgroundWorker
            //{
            //    WorkerReportsProgress = true,
            //    WorkerSupportsCancellation = false
            //};
            //worker.DoWork += DoMergeWork;
            //worker.ProgressChanged += worker_ProgressChanged;
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            //worker.RunWorkerAsync(_context);
        }

        private void DoMergeWork()
        {
            tmrRefresh.Interval = 100;
            tmrRefresh.Enabled = true;
            tmrRefresh.Start();

            Document activeDoc = Globals.BookPublishingStartup.Application.ActiveDocument;
            if ((activeDoc == null)) return;

            status.phase = "Margins";
            activeDoc.PageSetup.TopMargin = InchesToPoints(Convert.ToSingle(txtTopMargin.Text));
            activeDoc.PageSetup.BottomMargin = InchesToPoints(Convert.ToSingle(txtBottomMargin.Text));
            activeDoc.PageSetup.LeftMargin = InchesToPoints(Convert.ToSingle(txtLeftMargin.Text));
            activeDoc.PageSetup.RightMargin = InchesToPoints(Convert.ToSingle(txtRightMargin.Text));
            activeDoc.PageSetup.PageHeight = InchesToPoints(Convert.ToSingle(txtPageHeight.Text));
            activeDoc.PageSetup.PageWidth = InchesToPoints(Convert.ToSingle(txtPageWidth.Text));
            activeDoc.PageSetup.Gutter = InchesToPoints(Convert.ToSingle(txtGutterMargin.Text));
            activeDoc.PageSetup.GutterPos = WdGutterStyle.wdGutterPosLeft;

            float maxWidth = InchesToPoints(Convert.ToSingle(txtPageWidth.Text) - Convert.ToSingle(txtLeftMargin.Text) - Convert.ToSingle(txtRightMargin.Text) - Convert.ToSingle(txtGutterMargin.Text));
            float maxHeight = InchesToPoints(Convert.ToSingle(txtPageHeight.Text) - Convert.ToSingle(txtTopMargin.Text) - Convert.ToSingle(txtBottomMargin.Text)); 
            Utilities.Utility.InsertFromManifest(activeDoc, manifestFile,
                                         Convert.ToSingle(txtImageBrightness.Text), Convert.ToSingle(txtImageContrast.Text), chkNumbersToText.Checked, chkFieldsToText.Checked, maxWidth, maxHeight,
                                         ref status);

            // Final Fixup
            lblFileName.Text = "Master File Cleanup";

            if (chkAcceptRevisions.Checked)
            {
                status.phase = "Accept all Revisions";
                activeDoc.Revisions.AcceptAll();
                activeDoc.TrackRevisions = false;
            }

            if (chkClearComments.Checked)
            {
                status.phase = "Clear all comments";
                while (activeDoc.Comments.Count > 0)
                {
                    activeDoc.Comments[1].DeleteRecursively();
                }
            }

            status.phase = "Updating remaining fields...";
            int remainingFieldsCount = activeDoc.Fields.Count;
            int curRemainingField = 0;
            while (curRemainingField < remainingFieldsCount)
            {
                curRemainingField++;
                Field thisField = activeDoc.Fields[curRemainingField];
                if (thisField.Kind == WdFieldKind.wdFieldKindWarm) thisField.Update();
            }

            status.phase = "Finding blank pages...";
            // TODO: Blank Pages
            //int pageIndex = 1;
            //try
            //{
            //    activeDoc.Repaginate();
            //    while (true)
            //    {
            //        Range thisRange = activeDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, pageIndex);
            //        thisRange.Expand(WdUnits.wdColumn);
            //        if (thisRange.Text == null || string.IsNullOrEmpty(thisRange.Text.Trim()))
            //        {
            //            thisRange.Delete();
            //        }
            //        else
            //        {
            //            pageIndex++;
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    // Expect we'll run off the end of the document
            //}

            //status.phase = "Resetting styles to black and white...";
            //if (chkBlackWhite.Checked)
            //{
            //    foreach (Style thisStyle in activeDoc.Styles)
            //    {
            //        if (thisStyle.Font.TextColor.RGB != 0xffffff &&
            //            thisStyle.Font.TextColor.RGB != 0x0 && 
            //            thisStyle.Font.Fill.BackColor.RGB == 0xffffff)
            //            thisStyle.Font.TextColor.RGB = 0;
            //    }
            //}

            tmrRefresh.Stop();
            workerThread = null;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpenFile = new OpenFileDialog();

            didSelect = false;
            dlgOpenFile.DefaultExt = ".TXT";
            dlgOpenFile.FileOk += new System.ComponentModel.CancelEventHandler(dlgOpenFile_FileOk);
            dlgOpenFile.ShowDialog();

            if (!didSelect) return;

            this.manifestFile = dlgOpenFile.FileName;
            WorkerThreadStart();
        }

        private bool didSelect = false;
        void dlgOpenFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            didSelect = true;
        }

        private static float InchesToPoints(float inches)
        {
            return inches * 72;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         

    }
}
