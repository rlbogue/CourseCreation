using Microsoft.Office.Interop.Word;
using Syncfusion.Pdf;
using Syncfusion.XPS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookPublishing.FieldInfo
{
    public partial class FieldStatus : Form
    {

        public FieldStatus()
        {
            InitializeComponent();
            wordFields = new WordFieldsInfo(this);
            SetDataBinding(wordFields.Fields);
        }

        public bool keepGoing = true;
        WordFieldsInfo wordFields;
        public int fieldCount = 0;
        public int shapeCount = 0;
        public int currentShape = 0;

        public void SetDataBinding(BindingList<WordFieldInfo> fields)
        {
            grdFields.AutoGenerateColumns = false;
            grdFields.DataSource = fields;
            string[] columnNames = new string[] { "Page", "X", "Y", "Height", "Width", "FieldType", "SpansPages", "TimeToGetLocation", "Status" };
            foreach (string colName in columnNames)
            {
                DataGridViewColumn col = new DataGridViewTextBoxColumn();
                col.Name = colName;
                col.DataPropertyName = colName;
                grdFields.Columns.Add(col);
                col.Visible = true;
                col.ReadOnly = true;
                switch (col.Name)
                {
                    case "Page":
                        col.HeaderText = "Page";
                        col.Width = 35;
                        break;
                    case "X":
                        col.HeaderText = "X";
                        col.Width = 45;
                        break;
                    case "Y":
                        col.HeaderText = "Y";
                        col.Width = 45;
                        break;
                    case "Height":
                        col.HeaderText = "Height";
                        col.Width = 50;
                        break;
                    case "Width":
                        col.HeaderText = "Width";
                        col.Width = 50;
                        break;
                    case "FieldType":
                        col.HeaderText = "Type";
                        col.Width = 75;
                        break;
                    case "SpansPages":
                        col.HeaderText = "Multipage";
                        col.Width = 55;
                        break;
                    case "TimeToGetLocation":
                        col.HeaderText = "Time";
                        col.Width = 100;
                        break;
                    case "Status":
                        col.HeaderText = "Status";
                        col.Width = 150;
                        break;
                    default:
                        col.Visible = false;
                        break;
                }
            }
        }
       public void SetControlLocations()
        {
            btnFileSave.Location = new System.Drawing.Point(this.Width - btnFileSave.Width - 10, btnFileSave.Location.Y);
            txtFileName.Width = this.Width - 20 - btnFileSave.Width - txtFileName.Location.X;
            grdFields.Width = this.Width - 20;
            grdFields.Height = this.Height - grdFields.Location.Y - 42;
        }

        public void SetPhase(string phase)
        {
            Action setPhase = delegate () { lblPhase.Text = phase; };
            if (this.InvokeRequired) { Invoke(setPhase); }
            else setPhase.Invoke();
        }

        public void SetFields(int fields)
        {
            fieldCount = fields;
        }

        public void SetShapes(int shapes)
        {
            shapeCount = shapes;
        }

        bool saveOKButtonClicked;
        private void btnFileSave_Click(object sender, EventArgs e)
        {
            saveOKButtonClicked = false;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = txtFileName.Text;
            sfd.FileOk += btnFileSave_FileOk;
            if (saveOKButtonClicked) txtFileName.Text = sfd.FileName;
        }

        private void btnFileSave_FileOk(object sender, CancelEventArgs e)
        {
            saveOKButtonClicked = true;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            if (myThread == null)
            {
                myThread = new Thread(WorkerProcess);
                myThread.Start();
            }
            else this.Close();
        }
        public void SetFileName(string fileName)
        {
            Action setFileName = delegate () { if(txtFileName != null) txtFileName.Text = fileName; };
            if (this.InvokeRequired) { Invoke(setFileName); }
            else setFileName.Invoke();
        }

        protected Thread myThread;
        public void WorkerProcess()
        {
            SetPhase("Starting");
            Document activeDoc = Globals.BookPublishingStartup.Application.ActiveDocument;
            SetFields(activeDoc.ContentControls.Count);
            SetShapes(activeDoc.Shapes.Count);
            SetPhase("Getting Inline Word fields");
            WordFieldsInfo.Get(activeDoc, wordFields, ref keepGoing);
            SetPhase("Checking Shapes for fields");
            WordFieldsInfo.GetFromShapes(activeDoc, wordFields, ref keepGoing, SetCurrentShape);
            string tempFileName = System.IO.Path.GetTempFileName();
            if (keepGoing)
            {
                SetPhase("Writing PDF");
                PDFForm.SaveDocToPDF(activeDoc, tempFileName);
            }
            if (keepGoing)
            {
                SetPhase("Adding Fields to PDF");
                PDFForm.AddFieldsToPDF(tempFileName, txtFileName.Text, wordFields, ref keepGoing);
            }
            if (keepGoing) SetPhase("Done");
            else SetPhase("Stopped");
            SetGoEnabled();
        }

        public void SetCurrentShape(int shapeNumber)
        {
            currentShape = shapeNumber;
            SetStatusLabels();
        }

        public void SetGoEnabled()
        {
            Action setGoEnabled = delegate () { btnGo.Enabled = true; };
            if (this.InvokeRequired) Invoke(setGoEnabled);
            else btnGo.Enabled = true;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            keepGoing = false;
        }

        private void FieldStatus_SizeChanged(object sender, EventArgs e)
        {
            SetControlLocations();
        }

        private void grdFields_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            SetStatusLabels();
            grdFields.Rows[grdFields.Rows.Count - 1].Selected = true;
            grdFields.FirstDisplayedScrollingRowIndex = grdFields.Rows.Count - 1;
        }

        private void SetStatusLabels()
        {
            Action setFieldLabel = delegate () 
            { 
                lblFieldCount.Text = $"{wordFields.Fields.Count}/{this.fieldCount}";
                lblShapeCount.Text = $"{currentShape}/{shapeCount}";
            };
            if (this.InvokeRequired) { Invoke(setFieldLabel); }
            else setFieldLabel.Invoke();
        }
    }
}
