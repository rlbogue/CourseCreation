using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookPublishing.Binding.Model;
using System.IO;

namespace BookPublishing.Binding.View
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
            
        }

        private void chkSetPage_CheckedChanged(object sender, EventArgs e)
        {
            setPageControlsEnabled(chkSetPage.Checked);
        }

        public void setPageControlsEnabled()
        {
            setPageControlsEnabled(chkSetPage.Checked);
        }

        public void setPageControlsEnabled(bool isEnabled)
        {
            txtWidth.Enabled = isEnabled;
            txtHeight.Enabled = isEnabled;
            rdoPortrait.Enabled = isEnabled;
            rdoLandscape.Enabled = isEnabled;
        }

        private void chkSetMargins_CheckedChanged(object sender, EventArgs e)
        {
            setMarginControlsEnabled(chkSetMargins.Checked);
        }

        public void setMarginControlsEnabled()
        {
            setMarginControlsEnabled(chkSetMargins.Checked);
        }

        public void setMarginControlsEnabled(bool isEnabled)
        {
            txtTop.Enabled = isEnabled;
            txtBottom.Enabled = isEnabled;
            txtLeft.Enabled = isEnabled;
            txtRight.Enabled = isEnabled;
            txtHeader.Enabled = isEnabled;
            txtFooter.Enabled = isEnabled;
            txtGutter.Enabled = isEnabled;
        }

        private void chkSetImages_CheckedChanged(object sender, EventArgs e)
        {
            setImageControlsEnabled(chkSetImages.Checked);
        }

        public void setImageControlsEnabled()
        {
            setImageControlsEnabled(chkSetImages.Checked);
        }

        public void setImageControlsEnabled(bool isEnabled)
        {
            txtBrightness.Enabled = isEnabled;
            txtContrast.Enabled = isEnabled;
            txtMinHeight.Enabled = isEnabled;
            txtMaxHeight.Enabled = isEnabled;
            txtMaxWidth.Enabled = isEnabled;
        }


        public void SettingsToScreen(BindingAssembly.Settings settings)
        {
            PageToScreen(settings.Page);
            MarginsToScreen(settings.Margins);
            ImagesToScreen(settings.Images);
            TextToScreen(settings.Text);
            AssemblyToScreen(settings.Assembly);
        }

        public void ScreenToSettings(BindingAssembly.Settings settings)
        {
            ScreenToPage(settings.Page);
            ScreenToMargins(settings.Margins);
            ScreenToImages(settings.Images);
            ScreenToText(settings.Text);
            ScreenToAssembly(settings.Assembly);
        }

        public void AssemblyToScreen(BindingAssembly.Assembly assembly)
        {
            switch(assembly.DocBreak)
            {
                case BindingAssembly.DocumentBreak.None:
                    rdoNoBreak.Checked = true;
                    rdoPage.Checked = false;
                    rdoSection.Checked = false;
                    break;
                case BindingAssembly.DocumentBreak.Page:
                    rdoNoBreak.Checked = false;
                    rdoPage.Checked = true;
                    rdoSection.Checked = false;
                    break;
                case BindingAssembly.DocumentBreak.Section:
                    rdoNoBreak.Checked = false;
                    rdoPage.Checked = false;
                    rdoSection.Checked = true;
                    break;
                default:
                    throw new InvalidDataException("Unrecognized Assembly.Break");
            }
            chkCopyToTemp.Checked = assembly.CopyToTempFirst;
            chkResetPageNumber.Checked = assembly.ResetPageNumber;
            chkStartOnOdd.Checked = assembly.StartOnOdd;
            SetPageNumEnabled();
        }

        public void ScreenToAssembly(BindingAssembly.Assembly assembly)
        {
            if (rdoPage.Checked) assembly.DocBreak = BindingAssembly.DocumentBreak.Page;
            else if (rdoSection.Checked) assembly.DocBreak = BindingAssembly.DocumentBreak.Section;
            else assembly.DocBreak = BindingAssembly.DocumentBreak.None;
            assembly.CopyToTempFirst = chkCopyToTemp.Checked;
            assembly.ResetPageNumber = chkResetPageNumber.Checked;
            assembly.StartOnOdd = chkStartOnOdd.Checked;
        }

        public void ScreenToPage(BindingAssembly.Page page)
        {
            page.Perform = chkSetPage.Checked;
            double height = 0.0, width = 0.0;
            if (double.TryParse(txtHeight.Text, out height)) page.Height = height;
            if (double.TryParse(txtWidth.Text, out width)) page.Width = width;
            if (rdoLandscape.Checked) page.Orientation = BindingAssembly.PageOrientation.Landscape;
            else if (rdoPortrait.Checked) page.Orientation = BindingAssembly.PageOrientation.Portrait;
            else page.Orientation = BindingAssembly.PageOrientation.DontSet;
            
        }

        public void PageToScreen(BindingAssembly.Page page)
        {
            chkSetPage.Checked = page.Perform;
            txtHeight.Text = $"{page.Height:0.####}";
            txtWidth.Text = $"{page.Width:0.####}";
            switch (page.Orientation)
            {
                case BindingAssembly.PageOrientation.DontSet:
                    rdoPortrait.Checked = false;
                    rdoLandscape.Checked = false;
                    break;
                case BindingAssembly.PageOrientation.Landscape:
                    rdoPortrait.Checked = false;
                    rdoLandscape.Checked = true;
                    break;
                case BindingAssembly.PageOrientation.Portrait:
                    rdoPortrait.Checked = true;
                    rdoLandscape.Checked = false;
                    break;
                default:
                    throw new InvalidDataException("Unrecognized orientation");
            }

            setPageControlsEnabled();
        }

        public void ScreenToMargins(BindingAssembly.Margins margins)
        {
            margins.Perform = chkSetMargins.Checked;
            double top = 0.0, bottom = 0.0, left = 0.0, right = 0.0, gutter = 0.0, header = 0.0, footer = 0.0;
            if (double.TryParse(txtTop.Text, out top)) margins.Top = top;
            if (double.TryParse(txtBottom.Text, out bottom)) margins.Bottom = bottom;
            if (double.TryParse(txtLeft.Text, out left)) margins.Left = left;
            if (double.TryParse(txtRight.Text, out right)) margins.Right = right;
            if (double.TryParse(txtGutter.Text, out gutter)) margins.Gutter = gutter;
            if (double.TryParse(txtHeader.Text, out header)) margins.Header = header;
            if (double.TryParse(txtFooter.Text, out footer)) margins.Footer = footer;
        }

        public void MarginsToScreen(BindingAssembly.Margins margins)
        {
            chkSetMargins.Checked = margins.Perform;
            txtTop.Text = $"{margins.Top:0.####}";
            txtBottom.Text = $"{margins.Bottom:0.####}";
            txtLeft.Text = $"{margins.Left:0.####}";
            txtRight.Text = $"{margins.Right:0.####}";
            txtGutter.Text = $"{margins.Gutter:0.####}";
            txtHeader.Text = $"{margins.Header:0.####}";
            txtFooter.Text = $"{margins.Footer:0.####}";
            setMarginControlsEnabled();
        }

        public void ScreenToImages(BindingAssembly.Images images)
        {
            images.Perform = chkSetImages.Checked;
            double brightness = 0.0, contrast = 0.0, maxHeight = 0.0, maxWidth = 0.0, minHeight = 0.0;
            if (double.TryParse(txtBrightness.Text, out brightness)) images.Brightness = brightness;
            if (double.TryParse(txtContrast.Text, out contrast)) images.Contrast = contrast;
            if (double.TryParse(txtMinHeight.Text, out minHeight)) images.MinimumAdjustmentHeight = minHeight;
            if (double.TryParse(txtMaxHeight.Text, out maxHeight)) images.MaximumHeight = maxHeight;
            if (double.TryParse(txtMaxWidth.Text, out maxWidth)) images.MaximumWidth = maxWidth;
        }

        public void ImagesToScreen(BindingAssembly.Images images)
        {
            chkSetImages.Checked = images.Perform;
            txtBrightness.Text = $"{images.Brightness:0}";
            txtContrast.Text = $"{images.Contrast:0}";
            txtMinHeight.Text = $"{images.MinimumAdjustmentHeight:0.###}";
            txtMaxHeight.Text = $"{images.MaximumHeight:0.###}";
            txtMaxWidth.Text = $"{images.MaximumWidth:0.###}";
            setImageControlsEnabled();
        }

        public void ScreenToText(BindingAssembly.Text text)
        {
            text.ClearComments = chkClearComments.Checked;
            text.AcceptRevisions = chkAcceptRevisions.Checked;
            text.NumbersToText = chkConvertNumbersToText.Checked;
            text.FieldsToText = chkConvertFieldsToText.Checked;
        }

        public void TextToScreen(BindingAssembly.Text text)
        {
            chkClearComments.Checked = text.ClearComments;
            chkAcceptRevisions.Checked = text.AcceptRevisions;
            chkConvertNumbersToText.Checked = text.NumbersToText;
            chkConvertFieldsToText.Checked = text.FieldsToText;
        }

        private void rdoNoBreak_CheckedChanged(object sender, EventArgs e)
        {
            SetPageNumEnabled();
        }

        private void rdoPage_CheckedChanged(object sender, EventArgs e)
        {
            SetPageNumEnabled();
        }

        private void rdoSection_CheckedChanged(object sender, EventArgs e)
        {
            SetPageNumEnabled();
        }

        private void SetPageNumEnabled()
        {
            chkResetPageNumber.Enabled = rdoSection.Checked;
        }
    }
}
