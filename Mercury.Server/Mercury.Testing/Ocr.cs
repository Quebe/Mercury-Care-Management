using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Testing {

    public class Ocr {

        //#region Private Properties

        //private tessnet2.Tesseract ocrEngine = new tessnet2.Tesseract ();

        //#endregion 


        //public Ocr (String tessDataPath) {
                        
        //    ocrEngine.Init (tessDataPath, "eng", false);

        //    ocrEngine.SetVariable ("tessedit_char_whitelist", "0123456789");

        //    return;

        //}

        //public void ProcessDataRegion ( System.Drawing.Bitmap pageImage, System.Xml.XmlNode dataRegionNode) {

        //    Boolean success = false;

        //    String dataRegionName = dataRegionNode.Attributes["Name"].InnerText;

        //    String dataRegionType = dataRegionNode.Attributes["Type"].InnerText;

        //    String dataFormat = String.Empty; // DATA FORMAT OF OCR VALUE

        //    System.Drawing.Rectangle dataRegion = System.Drawing.Rectangle.Empty; // DATA REGION TO OCR

        //    System.Drawing.Bitmap dataRegionImage = null;

        //    System.Drawing.Graphics graphicsWriter = null;

        //    Int32 multipler = pageImage.Width / 850; // MULTIPLER FOR NON-100 DPI IMAGES (USED TO SCALE REGIONS)

        //    List<tessnet2.Word> ocrResults; // COLLECTION OF OCR WORDS

        //    String dataValue = String.Empty; // VALUE EXTRACTED FROM OCR


        //    switch (dataRegionType) {

        //        case "Text":

        //            dataRegion = new System.Drawing.Rectangle (

        //                Convert.ToInt32 (dataRegionNode.Attributes["Left"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Top"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Width"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Height"].InnerText) * multipler

        //                );


        //            dataRegionImage = new System.Drawing.Bitmap (dataRegion.Width, dataRegion.Height);

        //            graphicsWriter = System.Drawing.Graphics.FromImage (dataRegionImage);

        //            graphicsWriter.DrawImage (pageImage, 0, 0, dataRegion, System.Drawing.GraphicsUnit.Pixel);

        //            dataRegionImage.Save (@"C:\MERCURY\OCRDATAREGION" + dataRegionName + ".tif");


        //            ocrResults = ocrEngine.DoOCR (pageImage, dataRegion);

        //            foreach (tessnet2.Word currentWord in ocrResults) {

        //                if (currentWord.Text == "~") { success = false; break; }

        //                dataValue = dataValue + " " + currentWord.Text;

        //                success = true;

        //            }

        //            break;

        //        case "Number":

        //            dataRegion = new System.Drawing.Rectangle (

        //                Convert.ToInt32 (dataRegionNode.Attributes["Left"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Top"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Width"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Height"].InnerText) * multipler

        //                );


        //            dataRegionImage = new System.Drawing.Bitmap (dataRegion.Width, dataRegion.Height);

        //            graphicsWriter = System.Drawing.Graphics.FromImage (dataRegionImage);

        //            graphicsWriter.DrawImage (pageImage, 0, 0, dataRegion, System.Drawing.GraphicsUnit.Pixel);

        //            dataRegionImage.Save (@"C:\MERCURY\OCRDATAREGION" + dataRegionName + ".tif");


        //            ocrResults = ocrEngine.DoOCR (pageImage, dataRegion);

        //            foreach (tessnet2.Word currentWord in ocrResults) {

        //                if (currentWord.Text == "~") { success = false; break; }

        //                dataValue = dataValue + " " + currentWord.Text;

        //                success = true;

        //            }

        //            break;

        //        case "Date":

        //            dataFormat = dataRegionNode.Attributes["Format"].InnerText;

        //            dataRegion = new System.Drawing.Rectangle (

        //                Convert.ToInt32 (dataRegionNode.Attributes["Left"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Top"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Width"].InnerText) * multipler,

        //                Convert.ToInt32 (dataRegionNode.Attributes["Height"].InnerText) * multipler

        //                );
                    

        //            dataRegionImage = new System.Drawing.Bitmap (dataRegion.Width, dataRegion.Height);

        //            graphicsWriter = System.Drawing.Graphics.FromImage (dataRegionImage);

        //            graphicsWriter.DrawImage (pageImage, 0, 0, dataRegion, System.Drawing.GraphicsUnit.Pixel);

        //            dataRegionImage.Save (@"C:\MERCURY\OCRDATAREGION" + dataRegionName + ".tif");


        //            ocrResults = ocrEngine.DoOCR (pageImage, dataRegion);                    

        //            foreach (tessnet2.Word currentWord in ocrResults) {

        //                if (currentWord.Text == "~") { success = false; break;  }

        //                dataValue = dataValue + currentWord.Text;

        //                success = true;

        //            }

        //            if (success) {

        //                dataValue = dataValue.Replace (" ", "");

                        
        //            }

        //            break;

        //        case "Selection":

        //            dataFormat = dataRegionNode.Attributes["Format"].InnerText;

        //            foreach (System.Xml.XmlNode currentItemNode in dataRegionNode.SelectNodes ("Items/ItemRegion")) {

        //                String itemName = currentItemNode.Attributes["Name"].InnerText;

        //                Int32 thresholdPercent = Convert.ToInt32 ((((System.Xml.XmlElement) currentItemNode).HasAttribute ("Threshold")) ? currentItemNode.Attributes["Threshold"].InnerText : "20");

        //                thresholdPercent = (255 - ((255 * thresholdPercent) / 100));

        //                dataRegion = new System.Drawing.Rectangle (

        //                    Convert.ToInt32 (currentItemNode.Attributes["Left"].InnerText) * multipler,

        //                    Convert.ToInt32 (currentItemNode.Attributes["Top"].InnerText) * multipler,

        //                    Convert.ToInt32 (currentItemNode.Attributes["Width"].InnerText) * multipler,

        //                    Convert.ToInt32 (currentItemNode.Attributes["Height"].InnerText) * multipler

        //                    );


        //                dataRegionImage = new System.Drawing.Bitmap (dataRegion.Width, dataRegion.Height);

        //                graphicsWriter = System.Drawing.Graphics.FromImage (dataRegionImage);

        //                graphicsWriter.DrawImage (pageImage, 0, 0, dataRegion, System.Drawing.GraphicsUnit.Pixel);

        //                dataRegionImage.Save (@"C:\MERCURY\OCRDATAREGION" + dataRegionName + "_" + itemName + ".tif");


        //                System.Drawing.Color averageColor = Server.Public.Imaging.OcrImaging.AverageColor (dataRegionImage);

        //                Boolean isChecked = (averageColor.R < thresholdPercent);

                        
                        
        //                System.Diagnostics.Debug.WriteLine ("DATA REGION [" + dataRegionName + ":" + dataRegionType + ":" + itemName + "]: " + dataValue + " _ " + isChecked);
                            

        //            }

        //            break;

        //    }
            
        //    System.Diagnostics.Debug.WriteLine ("DATA REGION [" + dataRegionName + ":" + dataRegionType + "]: " + dataValue + " _ " + success);


        //    return;

        //}

        //public void ProcessDocument (System.Xml.XmlDocument jobControlFile, System.Drawing.Image[] imagePages) {

        //    System.Xml.XmlNode pagesNode = jobControlFile.SelectSingleNode ("/Ocr/DocumentDefinition/Pages");

        //    Int32 currentPageIndex = 0;

        //    foreach (System.Xml.XmlNode currentPageNode in pagesNode.ChildNodes) {

        //        System.Drawing.Bitmap currentPage = new System.Drawing.Bitmap (imagePages[currentPageIndex]);

        //        currentPage = Server.Public.Imaging.OcrImaging.DropoutColors (currentPage, System.Drawing.Color.Black, System.Drawing.Color.FromArgb (50, 50, 50));

        //        currentPage.Save (@"C:\MERCURY\OCRTESTPAGE" + currentPageIndex.ToString () + ".TIF", System.Drawing.Imaging.ImageFormat.Tiff);

        //        foreach (System.Xml.XmlNode currentDataRegion in currentPageNode.SelectNodes ("DataRegions/DataRegion")) {

        //            ProcessDataRegion (currentPage, currentDataRegion);

        //        }

        //        currentPageIndex = currentPageIndex + 1;

        //    }

        //}


        //public void Process (System.Xml.XmlDocument jobControlFile) {

        //    System.Xml.XmlNode sourceNode = jobControlFile.SelectSingleNode ("/Ocr/JobDefinition/Source");

        //    String[] availableFiles = System.IO.Directory.GetFiles (sourceNode.Attributes["FilePath"].InnerText, sourceNode.Attributes["FileCriteria"].InnerText);


        //    foreach (String currentFileName in availableFiles) {

        //        Server.Public.ImageStream imageStream = new Server.Public.ImageStream ();
                
        //        System.IO.FileStream imageFile = new System.IO.FileStream (currentFileName, System.IO.FileMode.Open);

        //        imageFile.CopyTo (imageStream.Image);

        //        imageFile.Close ();


        //        Int32 currentPage = 0;

        //        Int32 totalPages = imageStream.TiffPageCount ();

        //        Int32 documentSize = Convert.ToInt32 (sourceNode.Attributes["PageCount"].InnerText);

        //        System.Drawing.Image [] sourcePages = imageStream.TiffPages ();

        //        System.Diagnostics.Debug.WriteLine ("OCR Processing File: " + currentFileName + " Pages: " + totalPages);


        //        while (currentPage < totalPages) {

        //            System.Drawing.Image[] imagePages = new System.Drawing.Image [documentSize];

        //            for (Int32 currentDocumentPage = 0; currentDocumentPage < documentSize; currentDocumentPage++) {

        //                imagePages [currentDocumentPage] = sourcePages[currentPage];

        //                currentPage = currentPage + 1;

        //                if (currentPage >= totalPages) { break; }

        //            }

        //            if (imagePages.Length > 0) {

        //                ProcessDocument (jobControlFile, imagePages);

        //            }

        //        }

                
        //    }



        //    return;

        //}

        //public void Process (String jobControlFileName) {

        //    System.Xml.XmlDocument jobControlFile = new System.Xml.XmlDocument ();

        //    jobControlFile.Load (jobControlFileName);

        //    Process (jobControlFile);

        //    return;

        //}

    }

}
