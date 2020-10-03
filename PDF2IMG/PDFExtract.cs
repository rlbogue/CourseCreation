using NLog;
using Patagames.Pdf.Enums;
using Patagames.Pdf.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF2IMG
{
    public static class PDFExtract
    {
        public static Logger AppLogger = LogManager.GetCurrentClassLogger();
        public static Bitmap GetPageImage(PdfPage page, decimal dpiX, decimal dpiY)
        {
            Bitmap newBitmap = GetImageObject(page, dpiX, dpiY);
            if (newBitmap == null) { newBitmap = PDFRenderBitmap(page, dpiX, dpiY); }
            return newBitmap;
        }

        public static Bitmap GetImageObject(PdfPage page, decimal dpiX, decimal dpiY)
        {
            if (page.PageObjects.Count == 0) return null;

            List<PdfPageObject> imageObjects = page.PageObjects.Where(f => f.ObjectType == PageObjectTypes.PDFPAGE_IMAGE).ToList();
            bool otherVisibleObjects = false;

            foreach (PdfPageObject po in page.PageObjects)
            {
                switch (po.ObjectType)
                {
                    case PageObjectTypes.PDFPAGE_FORM:
                        otherVisibleObjects = true;
                        break;
                    case PageObjectTypes.PDFPAGE_IMAGE:
                        break;
                    case PageObjectTypes.PDFPAGE_PATH:
                        PdfPathObject path = po as PdfPathObject;
                        if (path.FillType != FillModes.None) otherVisibleObjects = true;
                        break;
                    case PageObjectTypes.PDFPAGE_SHADING:
                        // -- Seems like this is always visible.
                        // PdfShadingObject shading = po as PdfShadingObject;
                        otherVisibleObjects = true;
                        break;
                    case PageObjectTypes.PDFPAGE_TEXT:
                        PdfTextObject text = po as PdfTextObject;
                        if (text.RenderMode != TextRenderingModes.Nothing) otherVisibleObjects = true;
                        break;
                }
                if (otherVisibleObjects == true) break;
            }


            // one image and no other visible elements 
            if (imageObjects.Count == 1 && !otherVisibleObjects)
            {
                AppLogger.Trace("Using image extract method from PDF.");
                var imageObject = imageObjects.First() as PdfImageObject;

                if (imageObject == null || imageObject.Bitmap == null || imageObject.Bitmap.Image == null)
                {
                    AppLogger.Warn("Failed to get image bitmap from PDFium image object.");
                    return null;
                }
                Bitmap returnBitmap = new Bitmap(imageObject.Bitmap.Image);
                returnBitmap.SetResolution((float)dpiX, (float)dpiY);
                return returnBitmap;
            }

            return null;
        }

        public static Bitmap PDFRenderBitmap(PdfPage page, decimal dpiX, decimal dpiY)
        {
            try
            {
                int width = (int)(page.Width / 72 * Decimal.ToDouble(dpiX));
                int height = (int)(page.Height / 72 * Decimal.ToDouble(dpiY));

                using (PdfBitmap pdfBitmap = new PdfBitmap(width, height, true))
                {
                    pdfBitmap.FillRect(0, 0, width, height, Patagames.Pdf.FS_COLOR.White);
                    page.Render(pdfBitmap, 0, 0, width, height, PageRotate.Normal, RenderFlags.FPDF_PRINTING);

                    if (pdfBitmap.Image == null)
                    {
                        AppLogger.Warn("Failed to get image bitmap from PDFium image object.");
                        return null;
                    }

                    Bitmap newBitmap = new Bitmap(pdfBitmap.Image);
                    newBitmap.SetResolution((float)dpiX, (float)dpiY);
                    return newBitmap;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("p:pdfPage", page);
                throw;
            }
        }

    }
}
