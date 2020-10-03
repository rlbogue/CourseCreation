using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BookPublishing.Binding.Model
{
    public class BindingAssembly
    {
        /// <summary>
        /// Constructors
        /// </summary>
        public BindingAssembly()
        {
            DefaultSettings = new Settings();
        }

        public BindingAssembly(Settings defaultSettings)
        {
            DefaultSettings = new Settings();
            DefaultSettings.CopyFrom(defaultSettings);
        }

        public void CopyFrom(BindingAssembly fromAssembly)
        {
            // Copy information from the provided assembly
            // TODO: Make this a bit less crude.  Right now it's using a big hammer.
            DefaultSettings.CopyFrom(fromAssembly.DefaultSettings);

            foreach (BindingAssembly.File file in fromAssembly.Files) Files.Add(new BindingAssembly.File(file));
        }

        public Settings DefaultSettings;
        public BindingList<File> Files = new BindingList<File>();

        public enum DocumentBreak
        {
            None = 0,
            Page = 1,
            Section = 2
        }

        public enum PageOrientation
        {
            DontSet = 0,
            Portrait = 1,
            Landscape = 2
        }

        [Flags]
        public enum DocumentColorMode
        {
            DontSet = 0,
            FullColor = 1,
            TextBlackAndWhite = 2,
            ImagesBlackAndWhite = 4,
            ImagesAndTextBlackAndWhite = 6
        }



        public class File : INotifyPropertyChanged
        {
            public File() 
            {
                Settings = new Settings();
                Settings.PropertyChanged += SettingsChanged;
            } 

            public File(Settings settings)
            {
                Settings = new Settings();
                Settings.CopyFrom(settings);
                Settings.PropertyChanged += SettingsChanged;
            }
            public File(string fileName) 
            {
                FileName = fileName;
                Settings = new Settings();
                Settings.PropertyChanged += SettingsChanged;
            }
            public File(string fileName, Settings settings) 
            { 
                FileName = fileName;
                Settings = new Settings();
                Settings.PropertyChanged += SettingsChanged;
                Settings.CopyFrom(settings);
            }

            public File(File inFile)
            {
                CopyFrom(inFile);
            }
            public void CopyFrom(File inFile)
            {
                FileName = inFile.FileName;
                fields = inFile.fields;
                images = inFile.images;
                numbers = inFile.numbers;
                Settings = new Settings(inFile.Settings);
            }

            private PropertyChangedEventArgs propChangedFileName = new PropertyChangedEventArgs("FileName");
            protected string fileName;
            [XmlAttribute("FileName")]
            public string FileName { 
                get { return (fileName); } 
                set {
                    if (fileName != value && PropertyChanged != null) PropertyChanged(this, propChangedFileName);
                    fileName = value; 
                } 
            }

            [XmlIgnore]
            public string NameOnly
            {
                get { return (Path.GetFileNameWithoutExtension(fileName)); }
            }

            private PropertyChangedEventArgs propChangeSettings = new PropertyChangedEventArgs("Settings");

            protected void SettingsChanged(object settings, PropertyChangedEventArgs args)
            {
                PropertyChanged?.Invoke(this, propChangeSettings);
            }

            protected Settings settings;
            [XmlElement("Settings")]
            public Settings Settings 
            { 
                get { return (settings); } 
                set {
                    if (settings != value)
                    {
                        settings = value;
                        PropertyChanged?.Invoke(this, propChangeSettings);
                    }
                } 
            }
            private PropertyChangedEventArgs propChangeStatus = new PropertyChangedEventArgs("Status");
            protected string status;
            [XmlIgnore]
            public string Status 
            { 
                get { return (status); } 
                set {
                    if (status != value)
                    {
                        status = value;
                        try { PropertyChanged?.Invoke(this, propChangeStatus); } catch { }
                    }
                } 
            }

            private PropertyChangedEventArgs propChangeImages = new PropertyChangedEventArgs("Images");
            protected int images;
            [XmlIgnore]
            public int Images {
                get { return (images); } 
                set {
                    if (images != value)
                    {
                        images = value;
                        PropertyChanged?.Invoke(this, propChangeImages);
                    }

                } 
            }

            private PropertyChangedEventArgs propChangeFields = new PropertyChangedEventArgs("Fields");
            protected int fields;
            [XmlIgnore]
            public int Fields { 
                get { return (fields); } 
                set {
                    if (fields != value)
                    {
                        fields = value;
                        PropertyChanged?.Invoke(this, propChangeFields);
                    }
                } 
            }

            [XmlIgnore]
            public int Numbers { get { return (numbers); } set { numbers = value; } }
            protected int numbers;

            public event PropertyChangedEventHandler PropertyChanged;

            public void SetStatus(string status)
            {
                Status = status;
            }
        }

        public class Settings : INotifyPropertyChanged
        {
            public Settings()
            {
                Assembly = new Assembly();
                Page = new Page();
                Margins = new Margins();
                Images = new Images();
                Text = new Text();
            }

            public Settings(Settings inSettings)
            {
                CopyFrom(inSettings);
            }

            public void CopyFrom(Settings fromSettings)
            {
                Assembly = new Assembly(fromSettings.Assembly);
                Page = new Page(fromSettings.Page);
                Margins = new Margins(fromSettings.Margins);
                Images = new Images(fromSettings.Images);
                Text = new Text(fromSettings.Text);
            }
            [XmlElement("Assembly")]
            public Assembly Assembly { get { return (assembly); } set { assembly = value; } }
            protected Assembly assembly;

            [XmlElement("Page")]
            public Page Page { get { return (page); } set { page = value; } }
            protected Page page;
            [XmlElement("Margins")]
            public Margins Margins { get { return (margins); } set { margins = value; } }
            protected Margins margins;
            [XmlElement("Images")]
            public Images Images { get { return (images); } set { images = value; } }
            protected Images images;
            [XmlElement("Text")]
            public Text Text { get { return (text); } set { text = value; } }
            protected Text text;

            public event PropertyChangedEventHandler PropertyChanged;
        }

        public class Assembly
        {
            public Assembly() {}
            public Assembly(Assembly inAssembly)
            {
                CopyFrom(inAssembly);
            }
            public void CopyFrom(Assembly fromAssembly)
            {
                CopyToTempFirst = fromAssembly.CopyToTempFirst;
                DocBreak = fromAssembly.DocBreak;
                ResetPageNumber = fromAssembly.ResetPageNumber;
                StartOnOdd = fromAssembly.StartOnOdd;
            }
            [XmlAttribute("CopyFirst")]
            public bool CopyToTempFirst { get { return (copyToTempFirst); } set { copyToTempFirst = value; } }
            protected bool copyToTempFirst = false;
            [XmlAttribute("BreakType")]
            public DocumentBreak DocBreak { get { return (docBreak); } set { docBreak = value; } }
            protected DocumentBreak docBreak = DocumentBreak.Page;
            [XmlAttribute("ResetPageNumber")]
            public bool ResetPageNumber { get { return (resetPageNumber); } set { resetPageNumber = value; } }
            protected bool resetPageNumber;
            [XmlAttribute("StartOnOdd")]
            public bool StartOnOdd { get { return (startOnOdd); } set { startOnOdd = value; } }
            protected bool startOnOdd = false;
        }
        public class Page
        {
            public Page () { }
            public Page(Page inPage)
            {
                CopyFrom(inPage);
            }
            public void CopyFrom(Page fromPage)
            {
                Perform = fromPage.Perform;
                Height = fromPage.Height;
                Width = fromPage.Width;
                Orientation = fromPage.Orientation;
            }
            [XmlAttribute("Perform")]
            public bool Perform { get { return (perform); } set { perform = value; } } 
            protected bool perform = false;

            [XmlAttribute("Height")]
            public double Height { get { return (height); } set { height = value; } }
            protected double height = 9.68;
            [XmlAttribute("Width")]
            public double Width { get { return (width); } set { width = value; } }
            protected double width = 7.44;

            [XmlAttribute("Orientation")]
            public PageOrientation Orientation { get { return (orientation); } set { orientation = value; } }
            protected PageOrientation orientation = PageOrientation.Portrait;
        }

        public class Margins
        {
            public Margins() { }
            public Margins(Margins inMargins)
            {
                CopyFrom(inMargins);
            }

            public void CopyFrom(Margins fromMargins)
            {
                Perform = fromMargins.Perform;
                Left = fromMargins.Left;
                Right = fromMargins.Right;
                Top = fromMargins.Top;
                Bottom = fromMargins.Bottom;
                Gutter = fromMargins.Gutter;
                Header = fromMargins.Header;
                Footer = fromMargins.Footer;
            }

            [XmlAttribute("Perform")]
            public bool Perform { get { return (perform); } set { perform = value; } }
            protected bool perform = false;
            [XmlAttribute("Left")]
            public double Left { get { return (left); } set { left = value; } }
            protected double left = 0.25;
            [XmlAttribute("Right")]
            public double Right { get { return (right); } set { right = value; } }
            protected double right = 0.25;
            [XmlAttribute("Top")]
            public double Top { get { return (top); } set { top = value; } }
            protected double top = 0.5;
            [XmlAttribute("Bottom")]
            public double Bottom { get { return (bottom); } set { bottom = value; } }
            protected double bottom = 0.5;
            [XmlAttribute("Gutter")]
            public double Gutter { get { return (gutter); } set { gutter = value; } }
            protected double gutter = 0.5;
            [XmlAttribute("Header")]
            public double Header { get { return (header); } set { header = value; } } 
            protected double header = 0.25;
            [XmlAttribute("Footer")]
            public double Footer { get { return (footer); } set { footer = value; } }
            protected double footer = 0.25;
        }

        public class Images
        {
            public Images() { }
            public Images(Images inImages)
            {
                CopyFrom(inImages);
            }

            public void CopyFrom(Images fromImages)
            {
                Perform = fromImages.Perform;
                Brightness = fromImages.Brightness;
                Contrast = fromImages.Contrast;
                MaximumHeight = fromImages.MaximumHeight;
                MaximumWidth = fromImages.MaximumWidth;
                MinimumAdjustmentHeight = fromImages.MinimumAdjustmentHeight;
            }
            [XmlAttribute("Perform")]
            public bool Perform { get { return (perform); } set { perform = value; } }
            protected bool perform = false;
            [XmlAttribute("Brightness")]
            public double Brightness { get { return (brightness); } set { brightness = value; } }
            protected double brightness = -20;
            [XmlAttribute("Contrast")]
            public double Contrast { get { return (contrast); } set { contrast = value; } }
            protected double contrast = 30;
            [XmlAttribute("MaximumHeight")]
            public double MaximumHeight { get { return (maximumHeight); } set { maximumHeight = value; } }
            protected double maximumHeight = 0.0;
            [XmlAttribute("MaximumWidth")]
            public double MaximumWidth { get { return (maximumWidth); } set { maximumWidth = value; } }
            protected double maximumWidth = 0.0;
            [XmlAttribute("MinimumAdjustmentHeight")]
            public double MinimumAdjustmentHeight { get { return (minimumAdjustmentHeight); } set { minimumAdjustmentHeight = value; } }
            protected double minimumAdjustmentHeight = 0.5;
        }

        public class Text
        {
            public Text() { }
            public Text(Text inText)
            {
                CopyFrom(inText);
            }

            public void CopyFrom(Text fromText)
            {
                ClearComments = fromText.ClearComments;
                AcceptRevisions = fromText.AcceptRevisions;
                NumbersToText = fromText.NumbersToText;
                FieldsToText = fromText.FieldsToText;
            }
            [XmlAttribute("ClearComments")]
            public bool ClearComments { get { return (clearComments); } set { clearComments = value; } }
            protected bool clearComments = true;
            [XmlAttribute("AcceptRevisions")]
            public bool AcceptRevisions { get { return (acceptRevisions); } set { acceptRevisions = value; } }
            protected bool acceptRevisions = true; 
            [XmlAttribute("NumbersToText")]
            public bool NumbersToText { get { return (numbersToText); } set { numbersToText = value; } }
            protected bool numbersToText = true;
            [XmlAttribute("FieldsToText")]
            public bool FieldsToText { get { return (fieldsToText); } set { fieldsToText = value; } }
            protected bool fieldsToText = true;
        }
    }
}
