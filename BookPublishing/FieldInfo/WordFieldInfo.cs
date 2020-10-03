using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;

namespace BookPublishing.FieldInfo
{
    public enum FieldType
    {
        Unknown = 0,
        SingleLine = 1,
        MultiLine = 2,
        DropDownList = 3,
        Checkbox = 4
    }


    public class WordFieldInfo : INotifyPropertyChanged
    {

        private int _page;
        private static PropertyChangedEventArgs propChangedPage = new PropertyChangedEventArgs("Page");
        public int Page 
        { 
            get { return (_page); } 
            set 
            { 
                if (_page != value) 
                { 
                    _page = value; 
                    PropertyChanged?.Invoke(this, propChangedPage); 
                } 
            } 
        }
        private double _x;
        private static PropertyChangedEventArgs propChangedX = new PropertyChangedEventArgs("X");
        public double X { get { return (_x); } set { if (_x != value) { _x = value; PropertyChanged?.Invoke(this, propChangedX); } } }

        private double _width;
        private static PropertyChangedEventArgs propChangedWidth = new PropertyChangedEventArgs("Width");
        public double Width { get { return (_width); } set { if (_width != value) { _width = value; PropertyChanged?.Invoke(this, propChangedWidth); } } }

        private double _y;
        private static PropertyChangedEventArgs propChangedY = new PropertyChangedEventArgs("Y");
        public double Y { get { return (_y); } set { if (_y != value) { _y = value; PropertyChanged?.Invoke(this, propChangedY); } } }

        private double _height;
        private static PropertyChangedEventArgs propChangedHeight = new PropertyChangedEventArgs("Height");
        public double Height { get { return (_height); } set { if (_height != value) { _height = value; PropertyChanged?.Invoke(this, propChangedHeight); } } }

        private FieldType _fieldType;
        private static PropertyChangedEventArgs propChangedFieldType = new PropertyChangedEventArgs("FieldType");
        public FieldType FieldType { get { return (_fieldType); } set { if (_fieldType != value) { _fieldType = value; PropertyChanged?.Invoke(this, propChangedFieldType); } } }

        private bool _spansPages;
        private static PropertyChangedEventArgs propChangedSpansPages = new PropertyChangedEventArgs("SpansPages");
        public bool SpansPages { get { return (_spansPages); } set { if (_spansPages != value) { _spansPages = value; PropertyChanged?.Invoke(this, propChangedSpansPages); } } }

        private TimeSpan _timetoGetLocation = TimeSpan.Zero;
        private static PropertyChangedEventArgs propChangedTimeToGetLocation = new PropertyChangedEventArgs("TimeToGetLocation");
        public TimeSpan TimeToGetLocation { get { return (_timetoGetLocation); } set { if (_timetoGetLocation != value) { _timetoGetLocation = value; PropertyChanged?.Invoke(this, propChangedTimeToGetLocation); } } }

        private string _status;
        private static PropertyChangedEventArgs propChangedStatus = new PropertyChangedEventArgs("Status");
        public string Status { get { return (_status); } set { if (_status != value) { _status = value; PropertyChanged?.Invoke(this, propChangedStatus); } } }

        public PartialDictionary<string,string> Options;
        public event PropertyChangedEventHandler PropertyChanged;

        public WordFieldInfo()
        {

        }
        public WordFieldInfo(Document doc, ContentControl cc)
        {
            GetFromDocument(doc, cc);
        }
        
        public void GetFromDocumentShape(Document doc, ContentControl cc, Microsoft.Office.Interop.Word.Shape shp)
        {
            DateTime timerStart = DateTime.Now;
            FieldType = WordFieldInfo.ConvertFieldType(cc);
            if (FieldType == FieldType.DropDownList)
            {
                Options = GetOptions(cc);
            }
            X = GetXFromShape(shp);
            Y = GetYFromShape(shp);
            Height = shp.Height;
            Width = shp.Width;
            Page = cc.Range.Information[WdInformation.wdActiveEndPageNumber];
            Status = "Done";
            DateTime timerStop = DateTime.Now;
            TimeToGetLocation = timerStop - timerStart;
        }

        public static double GetXFromShape(Microsoft.Office.Interop.Word.Shape shp)
        {
            double x = shp.Left;
            Range shapeAnchor = shp.Anchor.Duplicate;
            object collapse = WdCollapseDirection.wdCollapseStart;
            shapeAnchor.Collapse(collapse);
            double anchorX = shapeAnchor.Information[WdInformation.wdHorizontalPositionRelativeToPage];
            if (x != anchorX) x += anchorX;
            return x;
        }
        public static double GetYFromShape(Microsoft.Office.Interop.Word.Shape shp)
        {
            double y = shp.Top;
            Range shapeAnchor = shp.Anchor.Duplicate;
            object collapse = WdCollapseDirection.wdCollapseStart;
            shapeAnchor.Collapse(collapse);
            double anchorY = shapeAnchor.Information[WdInformation.wdVerticalPositionRelativeToPage];
            if (y != anchorY) y += anchorY;
            return y;
        }

        public void GetFromDocument(Document doc, ContentControl cc)
        {
            DateTime timerStart = DateTime.Now;
            Status = "Initializing";

            FieldType = WordFieldInfo.ConvertFieldType(cc);
            if (FieldType == FieldType.DropDownList)
            {
                Options = GetOptions(cc);
            }
            Range fieldRange = cc.Range;
            Status = "Selecting";
            object collapseDirectionStart = WdCollapseDirection.wdCollapseStart;
            object collapseDirectionEnd = WdCollapseDirection.wdCollapseEnd;
            object moveUnit = WdUnits.wdCharacter;
            fieldRange.Collapse(ref collapseDirectionStart);
            fieldRange.Move(ref moveUnit, -1);
            Status = "Getting Page";

            double pageWidth = 0.0;
            double pageWidthUsable = 0.0;
            double pageHeight = 0.0;
            double pageTopMargin = 0.0;
            double pageBottomMargin = 0.0;
            double pageHeightUsable = 0.0;
            try
            {
                pageWidth = fieldRange.PageSetup.PageWidth;
                pageWidthUsable = pageWidth - fieldRange.PageSetup.LeftMargin - fieldRange.PageSetup.RightMargin - fieldRange.PageSetup.Gutter;
                pageHeight = fieldRange.PageSetup.PageHeight;
                pageTopMargin = fieldRange.PageSetup.TopMargin;
                pageBottomMargin = fieldRange.PageSetup.BottomMargin;
                pageHeightUsable = pageHeight - pageTopMargin - pageBottomMargin;
            }
            catch (Exception ex)
            {
                // The most likely scenario is that we're in a textbox so there's no page context, ignore this exception and accept zeros for the page information
                System.Diagnostics.Trace.WriteLine($"{ex}");
            }
            Page = fieldRange.Information[WdInformation.wdActiveEndPageNumber];
            int colNum = fieldRange.Information[WdInformation.wdEndOfRangeColumnNumber];
            int rowNum = fieldRange.Information[WdInformation.wdEndOfRangeRowNumber];
            // Collapsed to START so we get the start values here.
            X = fieldRange.Information[WdInformation.wdHorizontalPositionRelativeToPage];
            Y = fieldRange.Information[WdInformation.wdVerticalPositionRelativeToPage]; Status = "Checking for Table";
            if (colNum > 0 && rowNum > 0)
            {
                // This is inside a table cell, the field bounds are the cell bounds
                Table tbl = fieldRange.Tables[1];
                Cell cell = tbl.Cell(rowNum, colNum);
                X -= cell.LeftPadding;
                Y -= cell.TopPadding;
                Width = cell.Width;
                Height = cell.Height;
                if (Height == 9999999 && FieldType == FieldType.MultiLine)
                {
                    // Work around height not being accurate
                    double nextY;
                    if (tbl.Rows.Count > rowNum)
                    {
                        nextY = tbl.Rows[rowNum + 1].Range.Information[WdInformation.wdVerticalPositionRelativeToPage];
                    }
                    else
                    {
                        Range next = tbl.Range; // tbl.Cell(tbl.Rows.Count, tbl.Columns.Count).Range;
                        next.Collapse(ref collapseDirectionEnd);
                        //object moveForward = WdConstants.wdForward;
                        //next.MoveUntil("^p",ref  moveForward);
                        nextY = next.Information[WdInformation.wdVerticalPositionRelativeToPage];
                    }
                    // If we're on the next page, add the usable page height and remove the top margin to get field size
                    if (nextY < Y) nextY += pageHeightUsable - pageTopMargin;
                    Height = nextY - Y;
                    if (Height < 0.0) Height = 0.0;
                }
            }
            if (Width == 0 || Width > 5000 || Height == 0 || Height > 5000) // Need to fixup
            {
                Status = "Finding field width/height";
                fieldRange = cc.Range;

                fieldRange.Collapse(ref collapseDirectionEnd);
                double endX = fieldRange.Information[WdInformation.wdHorizontalPositionRelativeToPage];
                if (Width == 0 || Width > 5000) { Width = Math.Abs(endX - X); }
                if (Width == 0)
                {
                    Width = pageWidthUsable; // Not in table, and no width, so set it to usable page width
                    // Adjust for left indent
                    float leftIndent = fieldRange.ParagraphFormat.LeftIndent + fieldRange.ParagraphFormat.FirstLineIndent;
                    if (leftIndent > 0 && leftIndent < pageWidthUsable) { Width -= leftIndent; X += leftIndent; }
                }
                double endY = fieldRange.Information[WdInformation.wdVerticalPositionRelativeToPage];
                if (Height == 0 || Height > 5000)
                {
                    // Try to get the difference between the start and end
                    Height = endY - Y;
                    if (Height < 0) // Page Transition, add one page height
                    {
                        Height += pageHeightUsable - pageTopMargin;
                        SpansPages = true;
                    }
                    else if (Height == 0) // Still don't have a height, set to font height
                    { 
                        Font rangeFont = fieldRange.Font;
                        Height += rangeFont.Size;
                    }
                }
            }
            Status = "Done";
            DateTime timerStop = DateTime.Now;
            TimeToGetLocation = timerStop - timerStart;

        }

        public static PartialDictionary<string, string> GetOptions(ContentControl cc)
        {
            PartialDictionary<string, string> options = new PartialDictionary<string, string>();

            foreach (ContentControlListEntry option in cc.DropdownListEntries)
            {
                options.Add(option.Text, option.Value);
            }
            return options;
        }
        public static FieldType ConvertFieldType(ContentControl cc)
        {
            switch (cc.Type)
            {
                case WdContentControlType.wdContentControlText:
                    if (cc.MultiLine) return FieldType.MultiLine;
                    else return FieldType.SingleLine;
                case WdContentControlType.wdContentControlRichText:
                    return FieldType.MultiLine;
                case WdContentControlType.wdContentControlComboBox:
                case WdContentControlType.wdContentControlDropdownList:
                    return FieldType.DropDownList;
                case WdContentControlType.wdContentControlCheckBox:
                    return FieldType.Checkbox;
                default:
                    return FieldType.Unknown;
            }
        }
    }
}
