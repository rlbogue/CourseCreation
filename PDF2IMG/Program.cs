using ImageMagick;
using NLog;
using Patagames.Pdf.Enums;
using Patagames.Pdf.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace PDF2IMG
{
    class Program
    {
        public static Logger AppLogger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("PDF2IMG <config>");
                return;
            }

            string paramConfigFile = args[0];

            Configuration cfg = null;
            try
            {
                cfg= Configuration.Initialize(paramConfigFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to load configuration file {paramConfigFile} - Exception {ex.ToString()}");
                return;
            }
            

            DirectoryInfo di = new DirectoryInfo(cfg.Directory);
            if (!di.Exists) { Console.WriteLine($"{di.FullName} does not exist"); return; }

            FileInfo[] files = null;
            if (string.IsNullOrWhiteSpace(cfg.Pattern)) files = di.GetFiles();
            else files = di.GetFiles(cfg.Pattern);

            if (files == null || files.Length == 0) { Console.WriteLine($"No files found in {di.FullName} with pattern of {cfg.Pattern}"); return; }

            PdfCommon.Initialize(cfg.LicenseKey);
            foreach(FileInfo file in files)
            {
                ProcessFile(di, file, cfg);
            }
        }

        public static void ProcessFile(DirectoryInfo di, FileInfo file, Configuration cfg)
        {
            Dictionary<string, string> replacements = GetReplacementsFromFileInfo(file);
            // Get Bitmaps
            List<Bitmap> bitmaps = null;
            try
            {
                using (Stream fStream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (PdfDocument doc = PdfDocument.Load(fStream))
                    {
                        int pages = doc.Pages.Count;
                        bitmaps = new List<Bitmap>(pages);
                        for(int i=0;i<pages;i++)
                        {
                            PdfPage page = doc.Pages[i];
                            Bitmap pageBitmap = PDFExtract.GetPageImage(page, cfg.DpiX, cfg.DpiY);
                            bitmaps.Add(pageBitmap);
                        }
                    }
                }
                // Write TIF
                if (!string.IsNullOrWhiteSpace(cfg.MTIFFile))
                {
                    // TODO: Format with string
                    string fileTIFOut = StringUtility.FormatString(cfg.MTIFFile, replacements);
                    WriteMTIF(fileTIFOut, bitmaps, 24L);
                }

                // Individual Files
                if (!string.IsNullOrWhiteSpace(cfg.PNGFile))
                {
                    WritePNG(bitmaps, di, cfg.PNGFile, replacements);
                }

                // SinglePNG
                if (!string.IsNullOrWhiteSpace(cfg.OPNGFile))
                {
                    WriteOPNG(bitmaps, di, cfg.OPNGFile, replacements, cfg.LeftPadding, cfg.RightPadding, cfg.TopPadding, cfg.RightPadding, 
                              cfg.FileTopPadding, cfg.FileBottomPadding);
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unable to process {file.FullName}", ex);
            }
            finally
            {
                if (bitmaps != null)
                { 
                    foreach(Bitmap bitmap in bitmaps)
                    {
                        bitmap.Dispose();
                    }
                }
            }

        }

        private static void WriteOPNG(List<Bitmap> bitmaps, DirectoryInfo di, string oPNGFile, 
                                      Dictionary<string, string> replacements, 
                                      int leftPadding, int rightPadding, int topPadding, int bottomPadding,
                                      int fileTopPadding, int fileBottomPadding)
        {
            string fileName = StringUtility.FormatString(oPNGFile, replacements);
            List<ImageContentsInfo> pageInfo = new List<ImageContentsInfo>();
            for(int i=0;i<bitmaps.Count;i++)
            {
                int page = i + 1;
                Console.WriteLine($"Edge finding for {page}...");
                Bitmap bitmap = bitmaps[i];
                ImageContentsInfo ic = new ImageContentsInfo();
                ic.width = bitmap.Width;
                ic.height = bitmap.Height;
                // find Edges
                // TODO: Optimize this
                int left = bitmap.Width - 1; 
                int right = 0;
                int top = bitmap.Height - 1; 
                int bottom = 0;
                // Get the data that we can process
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                int bitmapBytes = bmData.Stride * bmData.Height;
                byte[] imageBytes = new byte[bitmapBytes];
                Marshal.Copy(bmData.Scan0, imageBytes, 0, bitmapBytes);
                int bitsPerPixel = (bmData.Stride * 8) / bmData.Width;
                if (bitsPerPixel % 8 != 0) throw new ApplicationException($"{bitsPerPixel} not supported, multiples of 8 only");
                int bytesPerPixel = bitsPerPixel / 8;
                EdgeFinding(imageBytes, bmData.Width, bmData.Stride, bmData.Height, bmData.PixelFormat, out top, out bottom, out left, out right);
                bitmap.UnlockBits(bmData);

                ic.topEdge = top;
                ic.bottomEdge = bottom;
                ic.leftEdge = left;
                ic.rightEdge = right;
                pageInfo.Add(ic);
            }

            // We have edges figure out our width height
            int tWidth = 0;
            int tHeight = 0;
            foreach(ImageContentsInfo ic in pageInfo)
            {
                if (ic.rightEdge - ic.leftEdge > tWidth) tWidth = ic.rightEdge - ic.leftEdge;
                tHeight += ic.bottomEdge - ic.topEdge;
            }

            tWidth += leftPadding + rightPadding;
            tHeight += ((topPadding + bottomPadding) * pageInfo.Count) + fileTopPadding + fileBottomPadding;
            using(Bitmap tBitmap = new Bitmap(tWidth, tHeight))
            {
                using (Graphics tGraphics = Graphics.FromImage(tBitmap))
                {
                    // Paint background all white
                    using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                    {
                        tGraphics.FillRectangle(whiteBrush, new Rectangle(0, 0, tWidth, tHeight));
                    }

                    // Drop in the images
                    int yLoc = fileTopPadding;
                    for (int i=0;i<pageInfo.Count;i++)
                    {
                        yLoc += topPadding;

                        ImageContentsInfo ic = pageInfo[i];
                        Bitmap pgBm = bitmaps[i];

                        int width = ic.rightEdge - ic.leftEdge;
                        int height = ic.bottomEdge - ic.topEdge;
                        int xLoc = ((tWidth - (width + leftPadding + rightPadding)) / 2) + leftPadding;
                        tGraphics.DrawImage(pgBm, new Rectangle(xLoc, yLoc, width, height), new Rectangle(ic.leftEdge, ic.topEdge, width, height), GraphicsUnit.Pixel);
                       
                        yLoc += bottomPadding + height;
                    }
                }
                tBitmap.Save(fileName, ImageFormat.Png);
            }
            
        }

        private static void EdgeFinding(Byte[] imageBytes, int width, int stride, int height, PixelFormat pixelFormat, out int top, out int bottom, out int left, out int right)
        {
            // Set positions to opposites
            top = height;
            bottom = 0;
            left = width;
            right = 0;

            int bytesPerPixel = 0;
            switch(pixelFormat)
            {
                case PixelFormat.Format32bppArgb: 
                    bytesPerPixel = 4; break;
                case PixelFormat.Format8bppIndexed:
                    bytesPerPixel = 1; break;
                case PixelFormat.Format24bppRgb:
                    bytesPerPixel = 3; break;
                default:
                    throw new InvalidDataException($"Unsupported pixel depth {pixelFormat}");
            }

            //int pixelTotal = width * height;
            //int xPos = 0, yPos = 0;
            int linePadding = stride - (bytesPerPixel * width);
            for (int imgIdx = 0; imgIdx < imageBytes.Length; imgIdx += bytesPerPixel)
            {
                // Adjust for line padding
                if ((stride - (imgIdx % stride)) == linePadding && linePadding != 0)
                {
                    imgIdx += linePadding;
                    if (imgIdx >= imageBytes.Length) break;
                }

                Color clr = Color.Black;                
                switch (bytesPerPixel)
                {
                    case 1:
                        clr = Color.FromArgb(0xff, imageBytes[imgIdx], imageBytes[imgIdx], imageBytes[imgIdx]);
                        break;
                    case 3:
                        clr = Color.FromArgb(0xff, imageBytes[imgIdx], imageBytes[imgIdx + 1], imageBytes[imgIdx + 2]);
                        break;
                    case 4:
                        clr = Color.FromArgb(imageBytes[imgIdx], imageBytes[imgIdx+1], imageBytes[imgIdx+2], imageBytes[imgIdx+3]);
                        break;
                }
                
                if (clr.R != 0xff ||
                    clr.G != 0xff ||
                    clr.B != 0xff) // Is not White, get coordinates
                {
                    int yCoord = Convert.ToInt32(imgIdx / stride);
                    int xCoord = (imgIdx - (yCoord * stride)) / bytesPerPixel;
                    if (left > xCoord) 
                    { 
                        //Console.WriteLine($"    Left edge {left} => {xCoord} ({clr.A}, {clr.R}, {clr.G}, {clr.B})"); 
                        left = xCoord; 
                    }
                    if (right < xCoord) 
                    { 
                        //Console.WriteLine($"     Right edge {right} => {xCoord} ({clr.A}, {clr.R}, {clr.G}, {clr.B})"); 
                        right = xCoord; 
                    }
                    if (top > yCoord) 
                    { 
                        //Console.WriteLine($"     Top edge {top} => {yCoord} ({clr.A}, {clr.R}, {clr.G}, {clr.B})"); 
                        top = yCoord; 
                    }
                    if (bottom < yCoord) 
                    { 
                        //Console.WriteLine($"     Bottom edge {bottom} => {yCoord} ({clr.A}, {clr.R}, {clr.G}, {clr.B})"); 
                        bottom = yCoord; 
                    }
                }
            }

            //for (int i = 0;i < pixelTotal; i++)
            //{
            //    if (Racetrack(i, width, height, top, bottom, left, right, ref xPos, ref yPos))
            //    {
            //        // Locate Pixel
            //        // Is Pixel Content
            //        // If Pixel is Content update boundaries
            //    }
            //}
        }

        private static bool Racetrack(int pos, int width, int height, int top, int bottom, int left, int right, ref int xPos, ref int yPos)
        {
            int totalPixels = width * height;
            if (pos >= totalPixels) throw new InvalidDataException($"Position not in image {pos} - {width}x{height}");

            return false;
        }
        private static Dictionary<string, string> GetReplacementsFromFileInfo(FileInfo file)
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>();

            replacements.Add("fullname", file.FullName);
            replacements.Add("directory", file.DirectoryName);
            replacements.Add("file", file.Name.Substring(0, file.Name.Length - file.Extension.Length));
            replacements.Add("filename", file.Name);
            replacements.Add("extension", file.Extension);
            replacements.Add("created", file.CreationTimeUtc.ToString(DateTimeFormatInfo.InvariantInfo));
            replacements.Add("modified", file.LastWriteTimeUtc.ToString(DateTimeFormatInfo.InvariantInfo));

            // TODO: Add More, particularly date parts
            return replacements;
         }

        private static void WritePNG(List<Bitmap> bitmaps, DirectoryInfo di, string pattern, Dictionary<string,string> replacements)
        {
            Dictionary<string, string> lclReplacements = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> kvp in replacements) lclReplacements.Add(kvp.Key, kvp.Value);
            lclReplacements.Add("page", "000");
            for (int i = 0; i < bitmaps.Count; i++)
            {
                lclReplacements["page"] = string.Format("{0:000}", i+1);
                Bitmap bitmap = bitmaps[i];
                string fileName = StringUtility.FormatString(pattern, lclReplacements);
                if (fileName.IndexOf('\\') == -1) fileName = di.FullName + '\\' + fileName;
                bitmap.Save(fileName, ImageFormat.Png);
            }
        }

        private static void WriteMTIF(string fileTIFOut, List<Bitmap> bitmaps, long bitDepth)
        {
            Encoder encoder = Encoder.SaveFlag;
            ImageCodecInfo encoderInfo = ImageCodecInfo.GetImageEncoders().First(i => i.MimeType == "image/tiff");
            EncoderValue compression = EncoderValue.CompressionLZW; // TODO: Verify

            using (MemoryStream outStream = new MemoryStream())
            {
                using (EncoderParameters encoderParameters = new EncoderParameters(3))
                {
                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)compression);
                    encoderParameters.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, bitDepth);
                    encoderParameters.Param[2] = new EncoderParameter(encoder, (long)EncoderValue.MultiFrame);
                    Bitmap firstBitmap = null;
                    for (int i = 0; i < bitmaps.Count; i++)
                    {
                        Bitmap bitmap = bitmaps[i];
                        if (i == 0)
                        {
                            firstBitmap = bitmap;
                            firstBitmap.Save(outStream, encoderInfo, encoderParameters);
                        }
                        else
                        {
                            encoderParameters.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.FrameDimensionPage);
                            firstBitmap.SaveAdd(bitmap, encoderParameters);
                        }
                    }
                    encoderParameters.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.Flush);
                    firstBitmap.SaveAdd(encoderParameters);
                }
                outStream.Seek(0, SeekOrigin.Begin);
                using (Stream fileOutStream = File.Open(fileTIFOut, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    outStream.CopyTo(fileOutStream);
                }
            }
        }

    }
}
