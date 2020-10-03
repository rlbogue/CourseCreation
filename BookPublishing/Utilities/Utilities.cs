using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace BookPublishing.Utilities
{
    public static class Utility
    {
        public class UtilityStatus
        {
            public List<string> files;
            public int fileIndex = 0;
            public string phase = "Initializing...";
            public List<string> messages = new List<string>();
            public int currentImage = 0;
            public int imageCount = 0;
        }

        public static UtilityStatus InsertFromManifest(Document activeDoc, string manifestFileName, float brightness, float contrast, bool forceRenumbering, bool fieldsToValues, float maxWidth, float maxHeight, ref UtilityStatus status)
        {
            try
            {
                status.files = new List<string>();
                using (StreamReader inFile = File.OpenText(manifestFileName))
                {
                    string fileLine = inFile.ReadLine();
                    while (fileLine != null && !inFile.EndOfStream)
                    {
                        if ((fileLine.Length > 0))
                        {
                            // Add non-blank lines
                            status.files.Add(fileLine);
                        }

                        fileLine = inFile.ReadLine();
                    }
                    if (!string.IsNullOrEmpty(fileLine)) status.files.Add(fileLine);
                }

                if ((status.files.Count > 0))
                {
                    AppendFiles(activeDoc, brightness, contrast, forceRenumbering, fieldsToValues, maxWidth, maxHeight, ref status);
                }
            }
            catch (Exception e)
            {
                status.messages.Add(string.Format("Exception in InsertFromManifest({0}, {1}, -) - {2}", activeDoc.FullName, manifestFileName, e.ToString()));
            }
            return status;
        }

        private static void AppendFiles(Document activeDoc, float brightness, float contrast, bool forceRenumbering, bool fieldsToValues, float maxWidth, float maxHeight, ref UtilityStatus status)
        {
            bool wasAdded = false;
            int fileCount = status.files.Count;
            while (status.fileIndex < status.files.Count)
            {
                int retries = 0; // per file
                do
                {
                    if (retries > 0) status.messages.Add(String.Format("Retrying file {0}", status.files[status.fileIndex]));
                    wasAdded = AppendFile(activeDoc, ref status, brightness, contrast, forceRenumbering, fieldsToValues, maxWidth, maxHeight, status.files[status.fileIndex]);
                }
                while (wasAdded == false && retries++ < 5);
                if (retries == 5) status.messages.Add(String.Format("Unable to add file {0}", status.files[status.fileIndex]));
                status.fileIndex++;
                System.Threading.Thread.Yield();
            }

        }

        private static bool AppendFile(Document activeDoc, ref UtilityStatus status, float brightness, float contrast, bool forceRenumbering, bool fieldsToValues, float maxWidth, float maxHeight, string file)
        {
            // Error Checking
            if (string.IsNullOrEmpty(file)) return false;
            if (activeDoc == null) return false;

            bool skipFixup = false;
            try
            {
                // Skip Fixup?
                if (file[0] == '*')
                {
                    skipFixup = true;
                    file = file.Substring(1).Trim();
                }

                // Declaration
                Application app = activeDoc.Application;
                object fileObj = file;
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
                status.phase = "Opening...";
                Document newDoc = app.Documents.Open(ref fileObj, ref confirmConversions, ref readOnly, ref addToRecentFiles,
                                                     ref passwordDocument, ref passwordTemplate, ref revert, ref writePasswordDocument,
                                                     ref writePasswordTemplate, ref format, ref encoding, ref visible, ref openAndRepair,
                                                     ref documentDirection, ref noEncodingDialog, ref xMLTransform);
                status.phase = "Fixup...";
                if (!skipFixup) DocumentFixup(newDoc, brightness, contrast, forceRenumbering, fieldsToValues, maxWidth, maxHeight, status);

                status.phase = "References Copy";
                CopyReferences(newDoc, activeDoc);
                status.phase = "Copying...";
                newDoc.Select();
                app.Selection.Copy();
                activeDoc.Select();
                object collapseEnd = WdCollapseDirection.wdCollapseEnd;
                app.Selection.Collapse(ref collapseEnd);
                app.Selection.Paste();

                status.phase = "Closing...";
                object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
                object originalFormat = WdOpenFormat.wdOpenFormatAuto;
                object routeDocument = Missing.Value;
                // Syntax to suppress compiler warning CS0467
                ((Microsoft.Office.Interop.Word._Document)(newDoc)).Close(ref saveChanges, ref originalFormat, ref routeDocument);
                object breakType = WdBreakType.wdPageBreak;
                app.Selection.InsertBreak(ref breakType);
            }
            catch (Exception e)
            {
                status.messages.Add(string.Format("Exception in AppendFile({0} - {1}) - {2}", activeDoc.FullName, file, e.ToString()));
                return false;
            }
            return true;
        }

        private const string TagXPath = "//*[local-name()='Guid']";
        private static void CopyReferences(Document newDoc, Document activeDoc)
        {
            Sources newSources = newDoc.Bibliography.Sources;
            Sources activeSources = activeDoc.Bibliography.Sources;

            // Optimization only build XML docs once for the active document
            SortedDictionary<string, Source> activeSourcesByTag = new SortedDictionary<string, Source>();
            foreach(Source activeSource in activeSources)
            {
                XmlDocument activeSourceXml = new XmlDocument();
                activeSourceXml.LoadXml(activeSource.XML);
                XmlNode tagNode = activeSourceXml.SelectSingleNode(TagXPath);
                activeSourcesByTag.Add(tagNode.InnerText.ToLower(), activeSource);
            }

            foreach(Source newSource in newSources)
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

        private static void DocumentFixup(Document doc, float brightness, float contrast, bool forceRenumbering, bool fieldsToValues, float maxWidth, float maxHeight, UtilityStatus status)
        {
            try
            {

                // Note: Process in reverse order to make sure the sequence numbers process correctly
                // Convert all fields into values
                int fieldIndex = doc.Fields.Count;
                while (fieldIndex > 0)
                {
                    Field thisField = doc.Fields[fieldIndex];
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

                // Force Fix item renumbering
                doc.ConvertNumbersToText(Missing.Value);

                // Images
                ImagesFixup(doc, brightness, contrast, maxWidth, maxHeight, ref status);
            }
            catch (Exception e)
            {
                status.messages.Add(string.Format("Exception in DocumentFixup({0}, -) - {1}", doc.FullName, e.ToString()));
            }
        }

        public static void ImagesFixup(Document activeDoc, float brightness, float contrast, float maxWidth, float maxHeight, ref UtilityStatus status)
        {
            status.imageCount = activeDoc.InlineShapes.Count;

            status.currentImage = 0;
            while (status.currentImage < status.imageCount)
            {
                ++status.currentImage;
                InlineShape curShape = activeDoc.InlineShapes[status.currentImage];

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


                    if (curShape.Height > 20)
                    {
                        // Adjust/Set Brightness/Contrast
                        curShape.PictureFormat.Brightness = .5F + (brightness / 200F);
                        curShape.PictureFormat.Contrast = .5F + (contrast / 200F);

                        // Clear Indent/Set Centered
                        curShape.Range.ParagraphFormat.LeftIndent = 0;
                        curShape.Range.ParagraphFormat.RightIndent = 0;
                        curShape.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                }
                System.Threading.Thread.Yield();
            }
        }
    }
}
