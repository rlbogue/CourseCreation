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
    public partial class RenderStaleConfiguration : Form
    {
        public RenderStaleConfiguration()
        {
            InitializeComponent();
        }

        public bool Valid = false;

        public void LoadScreen(string startDir, string mediaDir, string mediaSubDir, string renderSubDir, string renderTemplate)
        {
            txtStartDirectory.Text = startDir;
            txtMediaDirectory.Text = mediaDir;
            txtMediaSubdirectory.Text = mediaSubDir;
            txtRenderSubdirectory.Text = renderSubDir;
            txtRenderTemplate.Text = renderTemplate;
        }

        public void GetValues(out string startDir, out string mediaDir, out string mediaSubDir, out string renderSubDir, out string renderTemplate)
        {
            startDir = txtStartDirectory.Text;
            mediaDir = txtMediaDirectory.Text;
            mediaSubDir = txtMediaSubdirectory.Text;
            renderSubDir = txtRenderSubdirectory.Text;
            renderTemplate = txtRenderTemplate.Text;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Valid = true;
            this.Close();
        }

    }
}
