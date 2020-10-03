using BookPublishing.Binding.Model;
using BookPublishing.Binding.View;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;

namespace BookPublishing.Binding.Control
{
    public class BindingController
    {
        public Document WordDoc;
        public BindingAssembly Assembly;
        public BinderStatus Status;
        public bool Continue = true;
        protected bool done = false;
        public bool Done { 
            get { return (done); } 
            set {
                done = value;
                if (value == true) { Status.NotifyDone(); }
            } 
        }

        public BindingController(Document wordDoc, BindingAssembly assembly, BinderStatus status)
        {
            WordDoc = wordDoc;
            Assembly = assembly;
            Status = status;
        }

        public void ProcessAssembly()
        {
            Done = false;
            ProcessFileImages(Assembly.DefaultSettings, null, WordDoc);
            ProcessFileText(Assembly.DefaultSettings, null, WordDoc);
            ProcessFileMargins(Assembly.DefaultSettings, null, WordDoc);
            ProcessFilePage(Assembly.DefaultSettings, null, WordDoc);
            WordDoc.TrackRevisions = false;

            for(int i=0;i<Assembly.Files.Count;i++)
            {
                if (!Continue) { Done = true;return; }
                BindingAssembly.File file = Assembly.Files[i];
                bool nextStartOnOdd = false;
                if (i + 1 < Assembly.Files.Count &&
                    Assembly.Files[i + 1].Settings.Assembly.StartOnOdd) nextStartOnOdd = true;
                ProcessFile(file, nextStartOnOdd);
            }

            WordDoc.Repaginate();
            Done = true;
        }

        public void ProcessFile(BindingAssembly.File file, bool nextStartOnOdd)
        {
            object collapseEnd = WdCollapseDirection.wdCollapseEnd; // used to collapse the selection
            bool resetPageNumbering = (file.Settings.Assembly.DocBreak == BindingAssembly.DocumentBreak.Section &&
                                       file.Settings.Assembly.ResetPageNumber);
            try
            {
                // Assembly - Copy to Temp
                string fileName = file.FileName;
                if (file.Settings.Assembly.CopyToTempFirst)
                {
                    try {
                        file.SetStatus("Copying File...");
                        string destName = Path.GetTempFileName();
                        System.IO.File.Copy(file.FileName, destName, true);
                        fileName = destName;
                    }
                    catch (Exception ex)
                    {
                        file.SetStatus($"Error copying file. Exception {ex.ToString()}");
                        return;
                    }
                }

                // Open Document
                file.SetStatus("Opening Document...");
                Application app = WordDoc.Application;
                object fileObj = fileName;
                object confirmConversions = false;
                object readOnly = true;
                object addToRecentFiles = false;
                object passwordDocument = Missing.Value;
                object passwordTemplate = Missing.Value;
                object revert = true;
                object writePasswordDocument = Missing.Value;
                object writePasswordTemplate = Missing.Value;
                object format = Missing.Value;
                object encoding = Missing.Value;
                object visible = false;
                object openAndRepair = false;
                object documentDirection = Missing.Value;
                object noEncodingDialog = Missing.Value;
                object xMLTransform = Missing.Value;
                Document newDoc = app.Documents.Open(ref fileObj, ref confirmConversions, ref readOnly, ref addToRecentFiles,
                                                     ref passwordDocument, ref passwordTemplate, ref revert, ref writePasswordDocument,
                                                     ref writePasswordTemplate, ref format, ref encoding, ref visible, ref openAndRepair,
                                                     ref documentDirection, ref noEncodingDialog, ref xMLTransform);
                // Get Statistics for File
                file.Images = newDoc.InlineShapes.Count;
                file.Fields = newDoc.Fields.Count;
                file.Numbers = newDoc.CountNumberedItems(WdNumberType.wdNumberListNum, 1);

                // - Images
                ProcessFileImages(file.Settings, file, newDoc);

                // - Text
                ProcessFileText(file.Settings, file, newDoc);

                // - Margins
                ProcessFileMargins(file.Settings, file, newDoc);

                // - Page Layout
                ProcessFilePage(file.Settings, file, newDoc);

                System.Diagnostics.Trace.WriteLine($"Master: {WordDoc.Characters.Count} New: {file.FileName} / {newDoc.Characters.Count}");
                // Add to master
                file.SetStatus("Copying references...");
                CopyReferences(newDoc, WordDoc);
                file.SetStatus("Copying document...");
                newDoc.Select();
                int newDocSections = newDoc.Sections.Count;
                app.Selection.Copy();

                // For performance, try to not grab the whole document as a selection
                Section lastSectionBeforePaste = WordDoc.Sections.Last;
                int wordDocSections = WordDoc.Sections.Count;
                lastSectionBeforePaste.Range.Select();
                app.Selection.Collapse(ref collapseEnd);
                app.Selection.Paste();
                if (resetPageNumbering)
                {
                    // Find the right section
                    Section sec = WordDoc.Sections[wordDocSections];
                    ResetNumberingForSection(sec);
                    WordDoc.Sections.Last.Range.Select();
                    app.Selection.Collapse(ref collapseEnd);
                }

                // Assembly - Break
                object breakType = null;
                switch (file.Settings.Assembly.DocBreak)
                {
                    case BindingAssembly.DocumentBreak.Page:
                        breakType = WdBreakType.wdPageBreak;
                        break;
                    case BindingAssembly.DocumentBreak.Section:
                        if (nextStartOnOdd) breakType = WdBreakType.wdSectionBreakOddPage;
                        else breakType = WdBreakType.wdSectionBreakNextPage;
                        break;

                }
                if (breakType != null)
                {
                    //Trace.WriteLine($"Adding break {file.Settings.Assembly.DocBreak} to {app.Selection.Document.Name}");
                    app.Selection.InsertBreak(ref breakType);
                }

                // Close Document
                object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
                object originalFormat = WdOpenFormat.wdOpenFormatAuto;
                object routeDocument = Missing.Value;
                // Syntax to suppress compiler warning CS0467
                ((Microsoft.Office.Interop.Word._Document)(newDoc)).Close(ref saveChanges, ref originalFormat, ref routeDocument);

                // if copied, delete temp file.
                if (file.Settings.Assembly.CopyToTempFirst)
                {
                    file.SetStatus("Deleting temp file...");
                    File.Delete(fileName);
                }
                file.SetStatus("Processing completed...");
            }
            catch(Exception ex)
            {
                file.SetStatus($"Error processing file. Exception:{ex.ToString()}");
            }
        }

        public void ResetNumberingForSection(Section sec)
        {
            object align = WdPageNumberAlignment.wdAlignPageNumberOutside;
            object firstPage = true;
            PageNumbers pageNums = sec.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers;
            PageNumber pgNum = pageNums.Add(ref align, ref firstPage);
            pageNums.RestartNumberingAtSection = true;
            pageNums.StartingNumber = 1;
            pgNum.Delete();
        }
        public void ProcessFileText(BindingAssembly.Settings settings, BindingAssembly.File file, Document wordDoc)
        {
            if (file != null) file.SetStatus("Adjusting text...");

            // -- Fields to Text
            if (settings.Text.FieldsToText)
            {
                if (file != null) file.SetStatus("Converting fields to text");
                try
                {
                    int fieldIndex = wordDoc.Fields.Count;
                    while (fieldIndex > 0)
                    {
                        Field thisField = wordDoc.Fields[fieldIndex];
                        if (thisField.Kind != WdFieldKind.wdFieldKindCold)
                        {
                            try
                            {
                                thisField.Update();
                                thisField.Unlink();
                            }
                            catch (COMException cEx)
                            {
                                if (string.Compare(cEx.Message, "Object has been deleted.") != 0) throw cEx;
                            }
                            fieldIndex--;
                        }
                        else fieldIndex--;
                    }
                }
                catch (Exception ex)
                {
                    if (file != null) file.SetStatus($"Failed to convert fields to text.  Exception {ex.ToString()}");
                }
            }

            // -- Numbers to Text
            if (settings.Text.NumbersToText)
            {
                if (file != null) file.SetStatus("Converting numbers to text...");
                try
                {
                    wordDoc.ConvertNumbersToText(Missing.Value);
                }
                catch (Exception ex)
                {
                    file.SetStatus($"Filed to convert numbers to text.  Exception {ex.ToString()}");
                }
            }

            // -- Clear Comments
            if (settings.Text.ClearComments)
            {
                if (file != null) file.SetStatus($"Clearing comments...");
                try
                {
                    while(wordDoc.Comments.Count > 0)
                    {
                        wordDoc.Comments[1].DeleteRecursively();
                    }
                }
                catch (Exception ex)
                {
                    if (file != null) file.SetStatus($"Failed to clear comments. Exception {ex.ToString()}");
                }
            }
            // -- Accept Revisions
            if (settings.Text.AcceptRevisions)
            {
                if (file != null) file.SetStatus("Accepting revisions...");
                try
                {
                    wordDoc.Revisions.AcceptAll();
                    wordDoc.TrackRevisions = false;
                }
                catch (Exception ex)
                {
                    if (file != null) file.SetStatus($"Failed to accept revisions.  Exception {ex.ToString()}");
                }
            }
            if (file != null) file.SetStatus("Finished adjusting text...");
        }

        public void ProcessFileMargins(BindingAssembly.Settings settings, BindingAssembly.File file, Document wordDoc)
        {
            if (!settings.Margins.Perform) return;

            if (file != null) file.SetStatus("Adjusting margins...");
            try
            {
                wordDoc.PageSetup.TopMargin = (float) InchesToPoints(settings.Margins.Top);
                wordDoc.PageSetup.BottomMargin = (float) InchesToPoints(settings.Margins.Bottom);
                wordDoc.PageSetup.LeftMargin = (float) InchesToPoints(settings.Margins.Left);
                wordDoc.PageSetup.RightMargin = (float) InchesToPoints(settings.Margins.Right);
                wordDoc.PageSetup.Gutter = (float) InchesToPoints(settings.Margins.Gutter);
                wordDoc.PageSetup.GutterPos = WdGutterStyle.wdGutterPosLeft; // TODO make configurable
                if (file != null) file.SetStatus("Finished adjusting margins...");
            }
            catch (Exception ex)
            {
                if (file != null) file.SetStatus($"Unable to adjust margins.  Exception {ex.ToString()}");
            }

        }

        public static double InchesToPoints(double inches)
        {
            return (inches * 72);
        }

        public void ProcessFileImages(BindingAssembly.Settings settings, BindingAssembly.File file, Document wordDoc)
        {
            if (!settings.Images.Perform) return;

            if (file != null) file.SetStatus("Adjusting Images...");

            // Get Maximum Height / Width
            double maxHeight = 0.0, maxWidth = 0.0;
            if (settings.Images.MaximumHeight != 0.0) maxHeight = InchesToPoints(settings.Images.MaximumHeight);
            else // No taller than page
            {
                maxHeight = wordDoc.PageSetup.PageHeight - wordDoc.PageSetup.TopMargin - wordDoc.PageSetup.BottomMargin;
                if (wordDoc.PageSetup.GutterPos == WdGutterStyle.wdGutterPosTop) maxHeight -= wordDoc.PageSetup.Gutter;

            }

            if (settings.Images.MaximumWidth != 0.0) maxWidth = InchesToPoints(settings.Images.MaximumWidth);
            else // No wider than page
            {
                maxWidth = wordDoc.PageSetup.PageWidth - wordDoc.PageSetup.LeftMargin - wordDoc.PageSetup.RightMargin;
                if (wordDoc.PageSetup.GutterPos != WdGutterStyle.wdGutterPosTop) maxWidth -= wordDoc.PageSetup.Gutter;
            }

            int currentImage = 0;

            while (currentImage < file.Images)
            {
                ++currentImage;
                if (file != null) file.SetStatus($"Processing image {currentImage}...");
                InlineShape curShape = wordDoc.InlineShapes[currentImage];

                if (curShape.Type == WdInlineShapeType.wdInlineShapePicture)
                {   // If it's a picture we can do fixup with it
                    if (curShape.Width > maxWidth) // Too Big, must horizontally
                    {
                        float scaling = (float) maxWidth / curShape.Width;
                        curShape.Width = (float) maxWidth;
                        curShape.Height = curShape.Height * scaling;
                    }

                    if (curShape.Height > maxHeight)
                    {
                        float scaling = (float) maxHeight / curShape.Height;
                        curShape.Height = (float) maxHeight;
                        curShape.Width = curShape.Width * scaling;
                    }

                    if (curShape.Height > settings.Images.MinimumAdjustmentHeight)
                    {
                        // Adjust/Set Brightness/Contrast
                        curShape.PictureFormat.Brightness = .5F + ((float)settings.Images.Brightness / 200F);
                        curShape.PictureFormat.Contrast = .5F + ((float)settings.Images.Contrast / 200F);

                        // Clear Indent/Set Centered
                        // TODO: Evaluate for later
                        //curShape.Range.ParagraphFormat.LeftIndent = 0;
                        //curShape.Range.ParagraphFormat.RightIndent = 0;
                        //curShape.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                }
            }
            if (file != null) file.SetStatus("Finished adjusting images...");
        }

        public void ProcessFilePage(BindingAssembly.Settings settings, BindingAssembly.File file, Document wordDoc)
        {
            if (!settings.Page.Perform) return;

            if (file != null) file.SetStatus("Adjusting page size...");
            try
            {
                wordDoc.PageSetup.PageHeight = (float)InchesToPoints(settings.Page.Height);
                wordDoc.PageSetup.PageWidth = (float)InchesToPoints(settings.Page.Width);
                switch(settings.Page.Orientation)
                {
                    case BindingAssembly.PageOrientation.Landscape:
                        wordDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
                        break;
                    case BindingAssembly.PageOrientation.Portrait:
                        wordDoc.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
                        break;
                }
                if (file != null) file.SetStatus("Finshed adjusting page size...");
            }
            catch (Exception ex)
            {
                if (file != null) file.SetStatus($"Unable to set page size. Exception {ex.ToString()}");
            }
        }

        private const string TagXPath = "//*[local-name()='Guid']";
        private static void CopyReferences(Document newDoc, Document wordDoc)
        {
            Sources newSources = newDoc.Bibliography.Sources;
            Sources activeSources = wordDoc.Bibliography.Sources;

            // Optimization only build XML docs once for the active document
            SortedDictionary<string, Source> activeSourcesByTag = new SortedDictionary<string, Source>();
            foreach (Source activeSource in activeSources)
            {
                XmlDocument activeSourceXml = new XmlDocument();
                activeSourceXml.LoadXml(activeSource.XML);
                XmlNode tagNode = activeSourceXml.SelectSingleNode(TagXPath);
                activeSourcesByTag.Add(tagNode.InnerText.ToLower(), activeSource);
            }

            foreach (Source newSource in newSources)
            {
                // TODO: Only process cited sources // Cited doesn't seem to work

                try
                {
                    XmlDocument newSourceXml = new XmlDocument();
                    newSourceXml.LoadXml(newSource.XML);
                    XmlNode tagNode = newSourceXml.SelectSingleNode(TagXPath);
                    if (!activeSourcesByTag.ContainsKey(tagNode.InnerText.ToLower()))
                    {
                        // Source doesn't exist in target document
                        activeSources.Add(newSource.XML);
                    }
                    else
                    {
                        // Tag Exists translate in new doc

                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine($"File: {newDoc.FullName} Source:{newSource.XML} Exception: {e.ToString()}");
                }
            }
        }


    }
}
