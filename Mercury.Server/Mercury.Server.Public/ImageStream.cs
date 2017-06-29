using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public {

    [Serializable]
    public class ImageStream {

        #region Private Properties

        private System.IO.MemoryStream image = new System.IO.MemoryStream ();

        private String name = String.Empty;

        private String extension = String.Empty;

        private String mimeType = String.Empty;

        private Boolean isCompressed = false;

        #endregion 


        #region Public Properties

        public System.IO.MemoryStream Image { get { return image; } set { image = value; } }

        public System.IO.MemoryStream ImageDecompressed {

            get {

                if (!isCompressed) { return image; }


                System.IO.MemoryStream decompressedStream = new System.IO.MemoryStream ();

                System.IO.Compression.GZipStream compressedStream = new System.IO.Compression.GZipStream (image, System.IO.Compression.CompressionMode.Decompress);

                compressedStream.CopyTo (decompressedStream);

                return decompressedStream;

            }

        }

        public String ImageBase64 { get { return Convert.ToBase64String (image.ToArray ()); } }

        public String Name { get { return name; } set { name = value; } }

        public String Extension { get { return extension; } set { extension = value; } }

        public String MimeType { get { return mimeType; } set { mimeType = value; } }

        public Boolean IsCompressed { get { return isCompressed; } set { isCompressed = value; } }

        #endregion 


        #region Database Functions

        public void MapDataFields (String columnPrefix, System.Data.DataRow currentRow) {

            if (currentRow.Table.Columns.Contains (columnPrefix + "ImageName")) {

                Name = (String) currentRow [columnPrefix + "ImageName"];

            }

            if (currentRow.Table.Columns.Contains (columnPrefix + "ImageExtension")) {

                Extension = (String) currentRow [columnPrefix + "ImageExtension"];

            }

            if (currentRow.Table.Columns.Contains (columnPrefix + "ImageMimeType")) {

                MimeType = (String)currentRow[columnPrefix + "ImageMimeType"];

            }


            if (currentRow.Table.Columns.Contains (columnPrefix + "ImageIsCompressed")) {

                isCompressed = Convert.ToBoolean (currentRow[columnPrefix + "ImageIsCompressed"]);

            }

            return;

        }

        #endregion 


        #region Public Methods

        public void LoadFromFile (String imageFile) {

            System.IO.FileStream fileStream = new System.IO.FileStream (imageFile, System.IO.FileMode.Open);

            image = new System.IO.MemoryStream ();

            image.SetLength (fileStream.Length);

            fileStream.Read (image.GetBuffer (), 0, Convert.ToInt32 (image.Length));

            image.Flush ();

            fileStream.Close ();

            image.Seek (0, System.IO.SeekOrigin.Begin);

            return;

        }

        #endregion 


        #region Public Methods - Compression

        public void Compress () {

            if (isCompressed) { return; }


            System.IO.Compression.GZipStream compressedStream = new System.IO.Compression.GZipStream (image, System.IO.Compression.CompressionMode.Compress);

            compressedStream.CopyTo (image);


            return;

        }

        public void Decompress () {

            if (!isCompressed) { return; }


            System.IO.Compression.GZipStream compressedStream = new System.IO.Compression.GZipStream (image, System.IO.Compression.CompressionMode.Decompress);

            compressedStream.CopyTo (image);


            return;


        }

        #endregion 


        #region Public Methods - TIFF

        public Int32 TiffPageCount () {

            return TiffPageCount (System.Drawing.Bitmap.FromStream (ImageDecompressed));

        }

        public Int32 TiffPageCount (System.Drawing.Image tiffImage) {

            Int32 pageCount = -1;

            try {
                
                pageCount = tiffImage.GetFrameCount (System.Drawing.Imaging.FrameDimension.Page);

            }

            catch (Exception imageException) {

                System.Diagnostics.Debug.WriteLine ("!---> Image Stream Exception [TiffPageCount]: " + imageException.Message);

            }

            return pageCount;

        }

        public System.Drawing.Image[] TiffPages () {

            System.Drawing.Image[] tiffPages = new System.Drawing.Image [0];            

            try {

                // INITIALIZATION - SOURCE

                System.Drawing.Image tiffImage = System.Drawing.Bitmap.FromStream (ImageDecompressed);

                Int32 tiffPageCount = TiffPageCount (tiffImage);

                tiffPages = new System.Drawing.Image[tiffPageCount];


                // INITIALIZATION - FRAME DIMENSIONS

                System.Drawing.Imaging.FrameDimension pageDimension = new System.Drawing.Imaging.FrameDimension (tiffImage.FrameDimensionsList[0]);


                for (Int32 currentPageIndex = 0; currentPageIndex < tiffPageCount; currentPageIndex++) {

                    System.IO.MemoryStream tiffPage = new System.IO.MemoryStream ();

                    tiffImage.SelectActiveFrame (pageDimension, currentPageIndex);

                    tiffImage.Save (tiffPage, System.Drawing.Imaging.ImageFormat.Tiff);

                    tiffPages[currentPageIndex] = System.Drawing.Image.FromStream (tiffPage);

                }

            }
                
            catch (Exception imageException) {

                System.Diagnostics.Debug.WriteLine ("!---> Image Stream Exception [TiffPages]: " + imageException.Message);

            }

            return tiffPages;

        }

        public System.Windows.Xps.Packaging.XpsDocument TiffToXps () {

            System.Drawing.Image[] tiffPages = TiffPages ();


            // INITIALIZE XPS 

            System.IO.MemoryStream xpsStream = new System.IO.MemoryStream ();

            System.IO.Packaging.Package xpsPackage = System.IO.Packaging.Package.Open (xpsStream, System.IO.FileMode.Create);

            

            System.Windows.Xps.Packaging.XpsDocument xpsDocument = new System.Windows.Xps.Packaging.XpsDocument (xpsPackage);

            xpsDocument.Uri = new Uri ("http://www.quebesystems.com", UriKind.Absolute);

            System.Windows.Xps.XpsDocumentWriter xpsWriter = System.Windows.Xps.Packaging.XpsDocument.CreateXpsDocumentWriter (xpsDocument);


            // SET UP XPS DOCUMENT AND FIRST FIXED DOCUMENT

            System.Windows.Xps.Packaging.IXpsFixedDocumentSequenceWriter xpsFixedDocumentSequenceWriter = xpsDocument.AddFixedDocumentSequence ();

            System.Windows.Xps.Packaging.IXpsFixedDocumentWriter xpsFixedDocumentWriter = xpsFixedDocumentSequenceWriter.AddFixedDocument ();

            
            // WRITE TIFF IMAGES AS PAGES IN XPS

            foreach (System.Drawing.Image currentTiffPage in tiffPages) {

                // ADD A NEW PAGE, THEN EMBED IMAGE TO THE PAGE

                System.Windows.Xps.Packaging.IXpsFixedPageWriter xpsFixedPageWriter = xpsFixedDocumentWriter.AddFixedPage ();

                System.Windows.Xps.Packaging.XpsImage xpsImage = xpsFixedPageWriter.AddImage (System.Windows.Xps.Packaging.XpsImageType.TiffImageType);

                System.IO.Stream xpsImageStream = xpsImage.GetStream (); // GET DESTINATION STREAM TO COPY TO


                // COPY TIFF IMAGE TO XPS IMAGE

                currentTiffPage.Save (xpsImageStream, System.Drawing.Imaging.ImageFormat.Tiff);

                xpsImage.Commit ();


                // IMAGE IS EMBED, BUT PAGE HAS NOT BEEN CREATED, CREATE PAGE

                System.Xml.XmlWriter xmlPageWriter = xpsFixedPageWriter.XmlWriter;

                xmlPageWriter.WriteStartElement ("FixedPage"); // XPS PAGE STARTS WITH A FIXED PAGE TAG

                xmlPageWriter.WriteAttributeString ("xmlns", "http://schemas.microsoft.com/xps/2005/06");

                xmlPageWriter.WriteAttributeString ("xml:lang", "en-US");

                xmlPageWriter.WriteAttributeString ("Width", currentTiffPage.Width.ToString ());

                xmlPageWriter.WriteAttributeString ("Height", currentTiffPage.Height.ToString ());



                xmlPageWriter.WriteStartElement ("Path"); 

                xmlPageWriter.WriteAttributeString ("Data", "M 0,0 H " + currentTiffPage.Width.ToString () + " V " + currentTiffPage.Height.ToString () + " H 0 z");

                xmlPageWriter.WriteStartElement ("Path.Fill"); 

                xmlPageWriter.WriteStartElement ("ImageBrush"); 


                xmlPageWriter.WriteAttributeString ("TileMode", "None");

                xmlPageWriter.WriteAttributeString ("ViewboxUnits", "Absolute");

                xmlPageWriter.WriteAttributeString ("ViewportUnits", "Absolute");

                xmlPageWriter.WriteAttributeString ("Viewbox", "0, 0, " + currentTiffPage.Width.ToString () + ", " + currentTiffPage.Height.ToString ());

                xmlPageWriter.WriteAttributeString ("Viewport", "0, 0, " + currentTiffPage.Width.ToString () + ", " + currentTiffPage.Height.ToString ());

                xmlPageWriter.WriteAttributeString ("ImageSource", xpsImage.Uri.ToString ());


                xmlPageWriter.WriteEndElement (); // IMAGE BRUSH

                xmlPageWriter.WriteEndElement (); // PATH.FILL

                xmlPageWriter.WriteEndElement (); // PATH


                // PAGE END ELEMENT

                xmlPageWriter.WriteEndElement (); // FIXED PAGE

                
                // PAGE COMMIT

                xpsFixedPageWriter.Commit ();

            }

            // COMMIT DOCUMENT WRITER 

            xpsFixedDocumentWriter.Commit ();

            xpsFixedDocumentSequenceWriter.Commit ();


            xpsDocument.Close ();

            xpsPackage.Close ();


            //System.IO.FileStream fileStream = new System.IO.FileStream (@"C:\MERCURY\TEST7.XPS", System.IO.FileMode.Create);

            //fileStream.Write (xpsStream.ToArray (), 0, Convert.ToInt32 (xpsStream.Length));

            //fileStream.Flush ();

            //fileStream.Close ();


            String contentUriName = ("memorystream://content" + Guid.NewGuid ().ToString ().Replace ("-", "") + ".xps");

            Uri contentUri = new Uri (contentUriName, UriKind.Absolute);

            System.IO.Packaging.Package attachmentPackage = System.IO.Packaging.Package.Open (xpsStream);

            System.IO.Packaging.PackageStore.AddPackage (contentUri, attachmentPackage);

            System.Windows.Xps.Packaging.XpsDocument xpsContent = new System.Windows.Xps.Packaging.XpsDocument (attachmentPackage, System.IO.Packaging.CompressionOption.Normal, contentUriName );



            return xpsContent;

        }

        #endregion 

    }

}
