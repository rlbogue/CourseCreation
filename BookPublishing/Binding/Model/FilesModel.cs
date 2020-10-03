using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BookPublishing.Binding.Model
{
    public class FilesModel
    {
        public static class FieldNames
        {
            public const string Index = "Index";
            public const string FullName = "FullFileName";
            public const string FileName = "FileName";
            public const string Status = "Status";
            public const string Images = "Images";
            public const string Fields = "Fields";
            public const string FileObj = "FileObject";
        }
        public FilesModel()
        {
            // Define Columns / Setup table
            Table.Columns.Add(FieldNames.Index, typeof(int));
            Table.Columns.Add(FieldNames.FullName, typeof(string));
            Table.Columns.Add(FieldNames.FileName, typeof(string));
            Table.Columns.Add(FieldNames.Status, typeof(string));
            Table.Columns.Add(FieldNames.Images, typeof(int));
            Table.Columns.Add(FieldNames.Fields, typeof(int));
            Table.Columns.Add(FieldNames.FileObj, typeof(BindingAssembly.File));

        }
        public DataTable Table = new DataTable();
        public void FillFromList(List<BindingAssembly.File> files)
        {
            Table.Rows.Clear();
            int index = 0;
            foreach(BindingAssembly.File file in files)
            {
                DataRow dr = Table.NewRow();
                LoadRow(dr, file, index);
                Table.Rows.Add(dr);
                index++;
            }
        }

        public List<BindingAssembly.File> GetFilesFromTable()
        {
            List<BindingAssembly.File> files = new List<BindingAssembly.File>(Table.Rows.Count);

            foreach(DataRow row in Table.Rows)
            {
                files.Add(row[FieldNames.FileObj] as BindingAssembly.File);
            }
            return files;
        }

        private void LoadRow(DataRow dr, BindingAssembly.File file, int index)
        {
            dr[FieldNames.Index] = index;
            dr[FieldNames.FullName] = file.FileName;
            dr[FieldNames.FileName] = System.IO.Path.GetFileName(file.FileName);
            dr[FieldNames.FileObj] = file;
            dr[FieldNames.Images] = file.Images;
            dr[FieldNames.Fields] = file.Fields;
        }

        public static BindingAssembly.File GetFileFromRow(DataRow dr)
        {
            return (dr[FieldNames.FileObj] as BindingAssembly.File);
        }
    }
}
