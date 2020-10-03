using BookPublishing.Binding.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Serialization;

namespace BookPublishing.Binding.Control
{
    public static class FileReader
    {
        const long MaxManifestFileLength = 10 * 1024 * 1024; // 10MB

        // Overloads
        public static BindingAssembly GetAssembly(string fileName)
        {
            BindingAssembly assembly = new BindingAssembly();
            return (GetAssembly(fileName, assembly));
        }
        public static BindingAssembly GetAssembly(string fileName, BindingAssembly.Settings defaultSettings)
        {
            BindingAssembly assembly = new BindingAssembly(defaultSettings);
            return (GetAssembly(fileName, assembly));
        }

        private static XmlSerializer xmlSer = null;

        public static BindingAssembly GetAssembly(string fileName, BindingAssembly assembly)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new InvalidDataException("Filename may not be null/empty");
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists) throw new InvalidDataException("Input manifest file must exist");
            if (fi.Length > MaxManifestFileLength) throw new InvalidDataException($"The manifest file may not be longer than {MaxManifestFileLength} bytes");
            // File exists and we've got a good name, read it
            string manifestFile = File.ReadAllText(fileName);

            bool isValidXml = false;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(manifestFile);
                isValidXml = true;
            }
            catch { }

            if (isValidXml)
            {
                // Try to Deserialize
                try 
                { 
                    if (xmlSer == null) xmlSer = new XmlSerializer(typeof(BindingAssembly));
                    BindingAssembly xmlAssembly = null;
                    using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        xmlAssembly = xmlSer.Deserialize(fs) as BindingAssembly;
                    }
                    FixupPaths(fileName, xmlAssembly);
                    assembly.CopyFrom(xmlAssembly);
                    return (assembly);
                }
                catch 
                {
                    throw new InvalidDataException("Unable to deserialize XML");
                }
            }
            else
            {
                // Process as list of files
                string[] lines = manifestFile.Split(new char[] { '\r', '\n' });
                foreach(string line in lines)
                {
                    string cleanLine = line.Trim();
                    if (cleanLine.Length > 0) assembly.Files.Add(new BindingAssembly.File(line, assembly.DefaultSettings));
                }
            }

            FixupPaths(fileName, assembly);
            return assembly;
        }

        public static void FixupPaths(string fileName, BindingAssembly assembly)
        {
            FileInfo manifest = new FileInfo(fileName);
            foreach(BindingAssembly.File file in assembly.Files)
            {
                FileInfo fi = new FileInfo(file.FileName);
                if (fi.Exists)
                {
                    if (fi.FullName != file.FileName)
                    {  // Really this shouldn't happen but it might figure something out that's useful, so use it.
                        file.FileName = fi.FullName;
                    }
                }
                else
                {
                    // File doesn't exist, see if we can find it
                    if (file.FileName.IndexOf(':') == -1)
                    {
                        // Relative path, so try relative
                        string mergedFileName = Path.Combine(manifest.DirectoryName, file.FileName);
                        FileInfo mergedFi = new FileInfo(mergedFileName);
                        if (mergedFi.Exists) file.FileName = mergedFi.FullName;
                        else
                        {
                            // We don't know where to find this file.
                            // TODO: Enhancements
                        }
                    }
                }
            }
        }

        public static void Serialize(BindingAssembly assembly, Stream stream)
        {
            if (xmlSer == null) xmlSer = new XmlSerializer(typeof(BindingAssembly));
            xmlSer.Serialize(stream, assembly);
        }
    }
}
