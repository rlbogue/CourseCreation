using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvailTekVegasRenderExtensions
{
    public partial class RenderStaleStatus : Form
    {
        RenderStatusManager manager = null;
        public RenderStaleStatus()
        {
            InitializeComponent();
        }

        public void SetRenderStatusManager(RenderStatusManager rsm)
        {
            manager = rsm;
          
            grdResults.DataSource = rsm.Table;
            foreach(KeyValuePair<string,string> kvp in RenderStatusManager.Fields.FieldHeaders)
            {
                grdResults.Columns[kvp.Key].HeaderText = kvp.Value;
            }
        }
        public void SelectLastRow()
        {
            grdResults.Rows[grdResults.Rows.Count - 1].Selected = true;
        }

        private void RenderStaleStatus_SizeChanged(object sender, EventArgs e)
        {
            SetGridSize();
        }

        private void SetGridSize()
        {
            Size sz = this.Size;
            sz.Width -= 20;
            sz.Height -= 40;
            grdResults.Size = sz;
        }

        private void RenderStaleStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (manager != null) manager.Cancel = true;
        }
    }
}
