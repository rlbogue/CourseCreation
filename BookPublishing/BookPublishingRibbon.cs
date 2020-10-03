using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Excel;
using System.Reflection;
using BookPublishing.FieldInfo;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace BookPublishing
{
    public partial class BookPublishingRibbon
    {
        private void BookPublishingRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }


        //private OpenFileDialog dlgOpenFile = new OpenFileDialog();

        private void btnMerge_Click(object sender, RibbonControlEventArgs e)
        {
            Document activeDoc = Globals.BookPublishingStartup.Application.ActiveDocument;
            if ((activeDoc == null)) return;
            //didSelect = false;
            //dlgOpenFile.DefaultExt = ".TXT";
            //dlgOpenFile.FileOk += new System.ComponentModel.CancelEventHandler(dlgOpenFile_FileOk);
            //dlgOpenFile.ShowDialog();
            //if ((didSelect))
            //{
                ManifestStatusDialog dlgManifestStatus = new ManifestStatusDialog();
                Utilities.Utility.UtilityStatus status = new Utilities.Utility.UtilityStatus();
                dlgManifestStatus.status = status;
                dlgManifestStatus.ShowDialog();
            //}
        }

        private void btnImageFixup_Click(object sender, RibbonControlEventArgs e)
        {
            Document activeDoc = Globals.BookPublishingStartup.Application.ActiveDocument;

            float maxWidth = activeDoc.PageSetup.PageWidth - activeDoc.PageSetup.LeftMargin - activeDoc.PageSetup.RightMargin - activeDoc.PageSetup.Gutter;
            float maxHeight = activeDoc.PageSetup.PageHeight - activeDoc.PageSetup.TopMargin - activeDoc.PageSetup.BottomMargin;

            int imageCount = activeDoc.InlineShapes.Count;
            int currentImage = 0;
            while (currentImage < imageCount)
            {
                ++currentImage;
                InlineShape curShape = activeDoc.InlineShapes[currentImage];

                if (curShape.Type == WdInlineShapeType.wdInlineShapePicture)
                {   // If it's a picture we can do fixup with it
                    if (curShape.Width > maxWidth) // Too Big, must horizontally
                    {
                        float scaling = maxWidth / curShape.Width;
                        curShape.Width = maxWidth;
                        curShape.Height = curShape.Height * scaling;
                        //curShape.ScaleWidth = scaling;
                        //curShape.ScaleHeight = scaling;
                    }

                    if (curShape.Height > maxHeight)
                    {
                        float scaling = maxHeight / curShape.Height;
                        curShape.Height = maxHeight;
                        curShape.Width = curShape.Width * scaling;
                        //curShape.ScaleWidth = scaling;
                        //curShape.ScaleHeight = scaling;
                    }

                    // Clear any/all indents
                    if (curShape.Height > 20)
                    {
                        curShape.Range.ParagraphFormat.LeftIndent = 0;
                        curShape.Range.ParagraphFormat.RightIndent = 0;
                        curShape.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                }
            }

        }

        private void btnBinder_Click(object sender, RibbonControlEventArgs e)
        {
            Document activeDoc = Globals.BookPublishingStartup.Application.ActiveDocument;
            if ((activeDoc == null)) return;

            Binder dlgBinder = new Binder();
            dlgBinder.ShowDialog();
        }

        private void btnFields_Click(object sender, RibbonControlEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            string fileName = Globals.BookPublishingStartup.Application.ActiveDocument.Name;
            fileName = Path.GetFileNameWithoutExtension(fileName) + "-Fields.XML";
            sfd.FileName = fileName;
            FieldsOKClick = false;
            sfd.FileOk += Fields_FileOk;
            sfd.ShowDialog();
            if (FieldsOKClick)
            {
                WordFieldsInfo fi = WordFieldsInfo.Get();
                XmlSerializer xSer = new XmlSerializer(typeof(WordFieldsInfo));
                using (Stream strm = File.Open(sfd.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    xSer.Serialize(strm, fi);
                }
            }
        }

        private bool FieldsOKClick;
        private void Fields_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FieldsOKClick = true;
        }

        private void btnSavePDFForm_Click(object sender, RibbonControlEventArgs e)
        {
            FieldStatus fs = new FieldStatus();

            Document doc = Globals.BookPublishingStartup.Application.ActiveDocument;
            string fileName = Globals.BookPublishingStartup.Application.ActiveDocument.FullName;
            if (fileName.IndexOf('/') != -1) fileName = URL2LocalPath(fileName);
            fileName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + ".PDF";
            
            fs.Show(new Win32Window(Globals.BookPublishingStartup.Application.ActiveWindow.Hwnd));
            fs.SetFileName(fileName);
            //FileInfo fi = new FileInfo(fileName);
            //if (fi.Exists)
            //{
            //    DialogResult btnPress = MessageBox.Show($"Overwrite {fi.FullName}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            //    if (btnPress != DialogResult.Yes) return;
            //}

            //PDFForm.CreatePDFForm(doc, fileName);
        }

        private const string syncProvidersRoot = "HKEY_CURRENT_USER\\Software\\SyncEngines\\Providers";
        private static Dictionary<string, string> url2PathTranslation = null;
        public static string URL2LocalPath(string inLocation)
        {
            // Get
            if (url2PathTranslation == null)
            {
                url2PathTranslation = new Dictionary<string, string>();
                RegistryKey providerRoot = Registry.CurrentUser;
                using (RegistryKey sw = providerRoot.OpenSubKey("Software"))
                using (RegistryKey se = sw.OpenSubKey("SyncEngines"))
                using (RegistryKey providers = se.OpenSubKey("Providers"))
                {
                    foreach(string providerName in providers.GetSubKeyNames())
                    {
                        using(RegistryKey provider = providers.OpenSubKey(providerName))
                        {
                            foreach(string instanceName in provider.GetSubKeyNames())
                            {
                                using(RegistryKey instance = provider.OpenSubKey(instanceName))
                                {
                                    url2PathTranslation.Add(instance.GetValue("UrlNamespace") as string,
                                                            instance.GetValue("MountPoint") as string);
                                }
                            }
                        }
                    }
                }
            }

            // Check
            int matchLength = 0;
            string targetPath = null;
            foreach(KeyValuePair<string, string> kvp in url2PathTranslation)
            {
                if (string.Compare(kvp.Key, 0, inLocation, 0, kvp.Key.Length, true) == 0 && kvp.Key.Length > matchLength)
                {
                    matchLength = kvp.Key.Length;
                    targetPath = kvp.Value;
                }
            }

            if (matchLength == 0) return null; // Not found
            string remainder = inLocation.Substring(matchLength).Replace('/','\\');
            string totalUrl = Path.Combine(targetPath, remainder);
            return totalUrl;
        }

        //private bool didSelect = false;
        //void dlgOpenFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    didSelect = true;
        //}
    }
}
