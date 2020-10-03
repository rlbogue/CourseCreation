using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDF2IMG
{
    [Serializable]
    public class Configuration
    {
        public static Configuration Initialize(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists) throw new ApplicationException($"The configuration file {fileName} cannot be found");

            Configuration cfg = null;
            using (FileStream fs = File.OpenRead(fileName))
            {
                cfg = GetContentXmlSerializer().Deserialize(fs) as Configuration;
            }

            if (string.IsNullOrEmpty(cfg.LicenseKey)) cfg.LicenseKey = PDFComponentLicenseKey;

            return cfg;
        }

        public static object xSerLoc = new object();
        public static XmlSerializer xSer = null;
        public static XmlSerializer GetContentXmlSerializer()
        {
            if (xSer == null)
            {
                lock (xSerLoc)
                {
                    if (xSer == null)
                        xSer = new XmlSerializer(typeof(Configuration));
                }
            }
            return (xSer);
        }

        public static string PDFComponentLicenseKey = "EEF63308-0101E407-011E0A50-44464955-4D5F5045-521A0072-6F622E62-6F677565-4074686F-7270726F-6A656374-732E636F-6D40006B-0E416D03-E95C71F5-A93B94E7-9F6E5E87-1C7764A6-7D007E8F-4035CA78-A43C7F40-50607820-E20CD5E0-A3E38A62-7F71C9EE-262BE590-9ED1588E-0E0EA350-D50FAD";

        [XmlElement("Directory")]
        protected string directory;
        public string Directory { get { return (directory); } set { directory = value; } }
        [XmlElement("Pattern")]
        protected string pattern;
        public string Pattern { get { return (pattern); } set { pattern = value; } }
        [XmlElement("DpiX")]
        protected decimal dpiX;
        public decimal DpiX { get { return (dpiX); } set { dpiX = value; } }
        [XmlElement("DpiY")]
        protected decimal dpiY;
        public decimal DpiY { get { return (dpiY); } set { dpiY = value; } }
        [XmlElement("LicenseKey")]
        protected string licenseKey;
        public string LicenseKey { get { return (licenseKey); } set { licenseKey = value; } }

        [XmlElement("MTIFFile")]
        protected string mtifFile;
        public string MTIFFile { get { return (mtifFile); } set { mtifFile = value; } }

        [XmlElement("PNGFile")]
        protected string pngFile;
        public string PNGFile { get { return (pngFile); } set { pngFile = value; } }

        [XmlElement("OPNGFile")]
        protected string opngFile;
        public string OPNGFile { get { return (opngFile); }  set { opngFile = value; } }

        [XmlElement("TopPadding")]
        protected int topPadding;
        public int TopPadding { get { return (topPadding); } set { topPadding = value; } }

        [XmlElement("BottomPadding")]
        protected int bottomPadding;
        public int BottomPadding { get { return (bottomPadding); } set { bottomPadding = value; } }

        [XmlElement("LeftPadding")]
        protected int leftPadding;
        public int LeftPadding { get { return (leftPadding); } set { leftPadding = value; } }

        [XmlElement("RightPadding")]
        protected int rightPadding;
        public int RightPadding { get { return (rightPadding); } set { rightPadding = value; } }

        [XmlElement("FileTopPadding")]
        protected int fileTopPadding;
        public int FileTopPadding { get { return (fileTopPadding); } set { fileTopPadding = value; } }

        [XmlElement("FileBottomPadding")]
        protected int fileBottomPadding;
        public int FileBottomPadding { get { return (fileBottomPadding); } set { fileBottomPadding = value; } }
    }
}
