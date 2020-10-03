using ScriptPortal.Vegas;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvailTekVegasRenderExtensions
{
    public class RenderStatusManager
    {
        public RenderStaleStatus Dialog;
        public DataTable Table = null;
        public DataView View = null;
        public bool Cancel = false;
        public static class Fields
        {
            public static class Names
            {
                public const string ProjectDirectory = "ProjectDirectory";
                public const string ProjectFileName = "ProjectFileName";
                public const string ProjectLength = "ProjectLength";
                public const string MediaFilesCopied = "MediaFilesCopied";
                public const string RenderStart = "RenderStart";
                public const string RenderEnd = "RenderEnd";
                public const string RenderTime = "RenderTime";
                public const string RenderStatus = "RenderStatus";
            }

            public static class Headers
            {
                public const string ProjectDirectory = "Directory";
                public const string ProjectFileName = "File Name";
                public const string ProjectLength = "Length";
                public const string MediaFilesCopied = "Files Copied";
                public const string RenderStart = "Started";
                public const string RenderEnd = "Ended";
                public const string RenderTime = "Time";
                public const string RenderStatus = "Status";
            }

            public static KeyValuePair<string, Type>[] FieldTypes =
                                   new KeyValuePair<string, Type>[]
            {
                new KeyValuePair<string, Type>(Names.RenderStatus, typeof(string)),
                new KeyValuePair<string, Type>(Names.ProjectDirectory, typeof(string)),
                new KeyValuePair<string, Type>(Names.ProjectFileName, typeof(string)),
                new KeyValuePair<string, Type>(Names.ProjectLength, typeof(Timecode)),
                new KeyValuePair<string, Type>(Names.MediaFilesCopied, typeof(int)),
                new KeyValuePair<string, Type>(Names.RenderStart, typeof(DateTime)),
                new KeyValuePair<string, Type>(Names.RenderEnd, typeof(DateTime)),
                new KeyValuePair<string, Type>(Names.RenderTime, typeof(TimeSpan))
            };

            public static KeyValuePair<string, string>[] FieldHeaders =
                                new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>(Names.RenderStatus, Headers.RenderStatus),
                new KeyValuePair<string, string>(Names.ProjectDirectory, Headers.ProjectDirectory),
                new KeyValuePair<string, string>(Names.ProjectFileName, Headers.ProjectFileName),
                new KeyValuePair<string, string>(Names.ProjectLength, Headers.ProjectLength),
                new KeyValuePair<string, string>(Names.MediaFilesCopied, Headers.MediaFilesCopied),
                new KeyValuePair<string, string>(Names.RenderStart, Headers.RenderStart),
                new KeyValuePair<string, string>(Names.RenderEnd, Headers.RenderEnd),
                new KeyValuePair<string, string>(Names.RenderTime, Headers.RenderTime)
            };
        }
        
        public RenderStatusManager()
        {
            Table = new DataTable();
            foreach (KeyValuePair<string, Type> kvp in Fields.FieldTypes) Table.Columns.Add(kvp.Key, kvp.Value);
        }

        public object StartProjectProcessing(FileInfo fiProject)
        {
            DataRow dr = Table.NewRow();
            UpdateField(dr, Fields.Names.ProjectDirectory, fiProject.DirectoryName);
            UpdateField(dr, Fields.Names.ProjectFileName, Path.GetFileNameWithoutExtension(fiProject.Name));
            Table.Rows.Add(dr);
            if (Dialog != null) Dialog.SelectLastRow();
            return dr;
        }

        public void UpdateField(object row, string fieldName, object value)
        {
            DataRow dr = row as DataRow;
            if (dr == null) throw new InvalidDataException("Bad row");
            dr[fieldName] = value;
        }
        public string GetTableAsCSV()
        {
            StringBuilder sb = new StringBuilder(Table.Rows.Count * 100);
            sb.Append($"{Fields.Names.ProjectDirectory},{Fields.Names.ProjectFileName},{Fields.Names.MediaFilesCopied},{Fields.Names.RenderStart},{Fields.Names.RenderEnd},{Fields.Names.RenderTime},{Fields.Names.RenderStatus}\n");

            foreach(DataRow dr in Table.Rows)
            {
                sb.Append($"{dr[Fields.Names.ProjectDirectory]},{dr[Fields.Names.ProjectFileName]},{dr[Fields.Names.MediaFilesCopied]},{dr[Fields.Names.RenderStart]},{dr[Fields.Names.RenderEnd]},{dr[Fields.Names.RenderTime]},{dr[Fields.Names.RenderStatus]}\n");
            }
            return sb.ToString();
        }

    }
}
