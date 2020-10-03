using BookPublishing.Utilities;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace BookPublishing.FieldInfo
{
    public class WordFieldsInfo
    {
        public InvokeBindingList<WordFieldInfo> Fields;
        private ISynchronizeInvoke _sync;
        public WordFieldsInfo()
        {
            Fields = new InvokeBindingList<WordFieldInfo>(null);
        }

        public WordFieldsInfo(ISynchronizeInvoke sync)
        {
            _sync = sync;
            Fields = new InvokeBindingList<WordFieldInfo>(sync);
        }

        public static WordFieldsInfo Get()
        {
            return (Get(Globals.BookPublishingStartup.Application.ActiveDocument,null));
        }
        public static WordFieldsInfo Get(Document doc, ISynchronizeInvoke sync)
        {
            bool keepGoing = true;
            WordFieldsInfo wfi = new WordFieldsInfo(sync);
            bool finished = Get(doc, wfi, ref keepGoing);
            return (wfi);
        }
        public static bool GetFromShapes(Document doc, WordFieldsInfo wfi, ref bool keepGoing, Action<int> updateStatus )
        {
            Microsoft.Office.Interop.Word.Application app = doc.Application;
            // Process fields that may be embedded in shapes
            int currentShape = 0;
            foreach (Shape shp in doc.Shapes)
            {
                currentShape++;
                if (updateStatus != null) updateStatus(currentShape);
                if (shp.Type == Microsoft.Office.Core.MsoShapeType.msoTextBox)
                {
                    shp.Select();
                    app.Selection.ShapeRange.TextFrame.TextRange.Select();
                    if (app.Selection.ContentControls.Count == 1)
                    {
                        WordFieldInfo field = new WordFieldInfo();
                        wfi.Fields.Add(field);
                        field.GetFromDocumentShape(doc, app.Selection.ContentControls[1], shp);
                    }
                }
            }
            return (true);
        }
        public static bool Get(Document doc, WordFieldsInfo wfi, ref bool keepGoing)
        {
            Microsoft.Office.Interop.Word.Application app = doc.Application;
            doc.Repaginate();
            foreach(ContentControl cc in doc.ContentControls)
            {
                if (!keepGoing) return false;
                try 
                { 
                    WordFieldInfo field = new WordFieldInfo();
                    wfi.Fields.Add(field);
                    field.GetFromDocument(doc, cc);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine($"{ex}");
                }
            }
            return (true);
        }
    }
}
