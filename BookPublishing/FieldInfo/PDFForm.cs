using Microsoft.Office.Interop.Word;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace BookPublishing.FieldInfo
{
    public class PDFForm
    {

        public static bool AddFieldsToPDF(string fileName, string targetFileName, WordFieldsInfo wfi)
        {
            bool keepGoing = true;
            return (AddFieldsToPDF(fileName, targetFileName, wfi, ref keepGoing));
        }
        public static bool AddFieldsToPDF(string fileName, string targetFileName, WordFieldsInfo wfi, ref bool keepGoing)
        {
            try { 
            using (Stream stream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            using (PdfLoadedDocument pdfDoc = new PdfLoadedDocument(stream))
            {
                if (pdfDoc.Form == null) pdfDoc.CreateForm();
                else
                {
                    // Clear the existing Form
                    while (pdfDoc.Form.Fields.Count > 0) pdfDoc.Form.Fields.RemoveAt(0);
                }
                foreach (WordFieldInfo fi in wfi.Fields)
                {
                    if (!keepGoing) return false;
                    fi.Status = "Adding to PDF";
                    if (fi.Page <= pdfDoc.Pages.Count)
                    {
                        PdfPageBase page = pdfDoc.Pages[fi.Page - 1];
                        switch (fi.FieldType)
                        {
                            case FieldType.DropDownList:
                                PdfComboBoxField cboField = new PdfComboBoxField(page, GetFieldName(fi));
                                cboField.Bounds = new System.Drawing.RectangleF((float)fi.X, (float)fi.Y,
                                                                                    (float)fi.Width, (float)fi.Height);
                                foreach (KeyValuePair<string, string> kvp in fi.Options)
                                {
                                    cboField.Items.Add(new PdfListFieldItem(kvp.Key, kvp.Value));
                                }
                                cboField.SelectedIndex = 0;
                                pdfDoc.Form.Fields.Add(cboField);
                                break;
                            case FieldType.SingleLine:
                            case FieldType.MultiLine:
                                PdfTextBoxField txtField = new PdfTextBoxField(page, GetFieldName(fi));
                                txtField.BackColor = new Syncfusion.Pdf.Graphics.PdfColor((float)0xdd);
                                txtField.Bounds = new System.Drawing.RectangleF((float)fi.X, (float)fi.Y,
                                                                                    (float)fi.Width, (float)fi.Height);
                                txtField.Multiline = true;
                                txtField.Scrollable = true;
                                txtField.SpellCheck = true;
                                pdfDoc.Form.Fields.Add(txtField);
                                break;
                            case FieldType.Checkbox:
                                    PdfCheckBoxField chkField = new PdfCheckBoxField(page, GetFieldName(fi));
                                    chkField.Bounds = new System.Drawing.RectangleF((float)fi.X, (float)fi.Y,
                                                                                    (float)fi.Width, (float)fi.Height);
                                    pdfDoc.Form.Fields.Add(chkField);
                                    break;
                            default:
                                System.Diagnostics.Trace.WriteLine($"{fi.FieldType}");
                                break;
                        }
                        fi.Status = "Added to PDF";
                    }
                    else throw new IndexOutOfRangeException($"Field {fi.FieldType} at {fi.Page} ({fi.X}, {fi.Y}) is not on a valid page in the PDF");
                }
                using (Stream outStream = File.Open(targetFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    pdfDoc.Save(outStream);
                }

                pdfDoc.Close(true);
            }
            return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static bool CreatePDFForm(Document wordDoc, string targetFileName)
        {
            bool keepGoing = true;
            return (CreatePDFForm(wordDoc, targetFileName, null, ref keepGoing));
        }

        public static bool CreatePDFForm(Document wordDoc, string targetFileName, ISynchronizeInvoke sync, ref bool keepGoing)
        {
            string tempName = Path.GetTempFileName();

            WordFieldsInfo wfi = WordFieldsInfo.Get(wordDoc, sync);
            if (SaveDocToPDF(wordDoc, tempName))
            {
                AddFieldsToPDF(tempName, targetFileName, wfi, ref keepGoing);
            }
            return false;
        }

        public static string GetFieldName(WordFieldInfo fi)
        {
            return $"{fi.Page:000}-{fi.X:0.00}-{fi.Y:0.00}";
        }

        public static bool SaveDocToPDF(Document doc, string fileName)
        {
            bool success = false;
            try
            { 
                object wdFileName = fileName;
                object format = WdSaveFormat.wdFormatPDF;
                object lockComments = false;
                object password = String.Empty;
                object addToRecentFiles = false;
                object writePassword = String.Empty;
                object readOnlyRecommended = false;
                object embedTTF = true;
                object nativePicture = true;
                object saveFormsData = false;
                object saveMailer = false;
                object encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
                object insertLineBreaks = false;
                object allowSubstitutions = false;
                object lineEndingType = WdLineEndingType.wdCRLF;
                object addBidirect = false;
                object compatibility = WdCompatibilityMode.wdCurrent;

                doc.SaveAs2(ref wdFileName, ref format, ref lockComments, ref password, ref addToRecentFiles,
                            ref writePassword, ref readOnlyRecommended, ref embedTTF, ref nativePicture,
                            ref saveFormsData, ref saveMailer, ref encoding, ref insertLineBreaks,
                            ref allowSubstitutions, ref lineEndingType, ref addBidirect, ref compatibility);
                success = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"{ex}");
            }
            return success;
        }
    }
}
