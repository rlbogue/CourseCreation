using BookPublishing.Binding.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BookPublishing.Binding.View
{
    public partial class SettingsForm : Form
    {
        public BindingAssembly.Settings Settings;
        public SettingsForm(BindingAssembly.Settings settings)
        {
            InitializeComponent();
            Settings = settings;
            ctlSettings.SettingsToScreen(settings);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ctlSettings.ScreenToSettings(Settings);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
