using BookPublishing.Binding.Control;
using BookPublishing.Binding.Model;
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
using Point = System.Drawing.Point;

namespace BookPublishing.Binding.View
{
    public partial class BinderStatus : Form
    {
        protected BindingAssembly assembly;
        public BindingAssembly Assembly {
            get { return (assembly); }
            set
            {
                assembly = value;

                // Set binding
                grdStatus.AutoGenerateColumns = true;
                grdStatus.DataSource = assembly.Files;
                foreach (DataGridViewColumn column in grdStatus.Columns)
                {
                    switch (column.Name)
                    {
                        //                        case "FileName":
                        case "NameOnly":
                            column.Visible = true;
                            column.HeaderText = "File Name";
                            column.ReadOnly = true;
                            column.Width = 300;
                            break;
                        case "Images":
                            column.Visible = true;
                            column.HeaderText = "Images";
                            column.ReadOnly = true;
                            column.Width = 55;
                            break;
                        case "Fields":
                            column.Visible = true;
                            column.HeaderText = "Fields";
                            column.ReadOnly = true;
                            column.Width = 55;
                            break;
                        case "Numbers":
                            column.Visible = true;
                            column.HeaderText = "Numbers";
                            column.ReadOnly = true;
                            column.Width = 55;
                            break;
                        case "Status":
                            column.Visible = true;
                            column.HeaderText = "Status";
                            column.ReadOnly = true;
                            column.Width = 130;
                            break;
                        default:
                            column.Visible = false;
                            break;

                    }

                }
                grdStatus.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }


        public BinderStatus()
        {
            InitializeComponent();

        }

        public Thread workingThread = null;


        public void StartWorkerThread()
        {
            if (workingThread == null)
            {
                workingThread = new Thread(Compile);
                workingThread.Start();
            }
        }

        public BindingController Controller;

        public void Compile()
        {
            Document wordDoc = Globals.BookPublishingStartup.Application.ActiveDocument;
            Controller = new BindingController(wordDoc, Assembly, this);
            Controller.ProcessAssembly();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Controller.Continue = false;
        }

        public void NotifyDone()
        {
            Action notifyDone = delegate () { btnOK.Visible = true; };
            if (this.InvokeRequired) { Invoke(notifyDone); }
            else { btnOK.Visible = true; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BinderStatus_SizeChanged(object sender, EventArgs e)
        {
            SetPositions();
        }

        private void SetPositions()
        {
            Size sz = this.Size;
            // btnCancel Position
            Point cancelLcn = new Point(sz.Width - btnCancel.Width - 30,
                                  sz.Height - btnCancel.Height - 50);
            btnCancel.Location = cancelLcn;
            // btnOK Position
            Point okLcn = new Point(cancelLcn.X - btnOK.Width - 10,
                                    cancelLcn.Y);
            btnOK.Location = okLcn;
            // Gridsize
            sz.Width -= 20;
            sz.Height -= 60 + btnOK.Size.Height;
            grdStatus.Size = sz;
        }

        private void BinderStatus_Load(object sender, EventArgs e)
        {
            SetPositions();
        }
    }
}
