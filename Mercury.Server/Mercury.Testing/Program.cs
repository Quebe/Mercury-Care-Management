using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Mercury.Testing {

    class Program {

        static PdfSharp.Pdf.PdfObject PdfSharpGetItem (PdfSharp.Pdf.PdfDocument document, Int32 forObjectId) {

            PdfSharp.Pdf.PdfObject item = null;

            foreach (PdfSharp.Pdf.PdfObject currentItem in document.Internals.GetAllObjects ()) {

                if (currentItem.Internals.ObjectID.ObjectNumber == forObjectId) {

                    item = currentItem;

                    break;

                }

            }


            return item;

        }

        static void PdfSharpTestParseDictionary (PdfSharp.Pdf.PdfDictionary dictionary) {

            PdfSharpTestParseElements (dictionary.Elements);
            

            if (dictionary.Stream != null) {

                System.Diagnostics.Debug.WriteLine ("[DICTIONARY] " + dictionary.Stream.ToString ());

            }

            return;

        }

        static void PdfSharpTestParseResources (PdfSharp.Pdf.PdfDictionary resources) {

            PdfSharpTestParseReference (resources.Reference);

            PdfSharpTestParseElements (resources.Elements);


            return;

        }

        static void PdfSharpTestParseElements (PdfSharp.Pdf.PdfDictionary.DictionaryElements elements) {

            foreach (String currentElementKey in elements.Keys) {

                System.Diagnostics.Debug.WriteLine ("[Element:" + currentElementKey + "] " + elements[currentElementKey].ToString ());

                System.Diagnostics.Debug.WriteLine ("[Element:" + currentElementKey + "] " + elements[currentElementKey].GetType ().ToString ());


                if (currentElementKey == "/Resources") {

                    PdfSharpTestParseResources (elements[currentElementKey] as PdfSharp.Pdf.PdfDictionary);

                }


                switch (elements[currentElementKey].GetType ().ToString ()) {


                    case "PdfSharp.Pdf.Advanced.PdfReference":


                        PdfSharp.Pdf.Advanced.PdfReference reference = elements[currentElementKey] as PdfSharp.Pdf.Advanced.PdfReference;

                        PdfSharpTestParseReference (reference);


                        break;


                }
                
                
            }

            return;

        }

        static void PdfSharpTestParseReference (PdfSharp.Pdf.Advanced.PdfReference reference) {

            if (reference == null) { return; }

            switch (reference.Value.GetType ().ToString ()) {

                case "PdfSharp.Pdf.Advanced.PdfContent":

                    PdfSharp.Pdf.Advanced.PdfContent pdfContent = reference.Value as PdfSharp.Pdf.Advanced.PdfContent;

                    String content = System.Text.ASCIIEncoding.ASCII.GetString (pdfContent.Stream.Value);

                    foreach (String currentCommand in content.Split ('\n')) {

                        System.Diagnostics.Debug.WriteLine ("Command: " + currentCommand);

                    }                   

                    break;

                case "PdfSharp.Pdf.PdfDictionary":

                    PdfSharp.Pdf.PdfDictionary dictionary = reference.Value as PdfSharp.Pdf.PdfDictionary;

                    PdfSharpTestParseDictionary (dictionary);

                    break;

                case "PdfSharp.Pdf.PdfPage":

                    PdfSharp.Pdf.PdfPage pdfPage = reference.Value as PdfSharp.Pdf.PdfPage;                    

                    break;

                    
                default:

                    System.Diagnostics.Debug.WriteLine ("[REFERENCE PARSE UNKNOWN TYPE] " + reference.Value.GetType ().ToString ());

                    break;

            }

            return;

        }

        static void PdfSharpTestParsePage (PdfSharp.Pdf.PdfPage page) {


            foreach (String currentPageElementKey in page.Elements.Keys) {

                System.Diagnostics.Debug.WriteLine ("[Page Element:" + currentPageElementKey + "] " + page.Elements [currentPageElementKey].ToString ());

            }



            PdfSharp.Pdf.PdfDictionary pageResources = page.Elements.GetDictionary ("/Resources");

            if (pageResources != null) {

                PdfSharp.Pdf.PdfDictionary pageObjects = pageResources.Elements.GetDictionary ("/XObject");

            }
                
            



            PdfSharp.Pdf.PdfDictionary pageFonts = pageResources.Elements.GetDictionary ("/Font");


            foreach (String currentElementKey in pageFonts.Elements.Keys) {

                System.Diagnostics.Debug.WriteLine ("[FONT:" + currentElementKey + "]");

                PdfSharp.Pdf.Advanced.PdfReference fontReference = pageFonts.Elements[currentElementKey] as PdfSharp.Pdf.Advanced.PdfReference;

                PdfSharp.Pdf.PdfDictionary fontDictionary = fontReference.Value as PdfSharp.Pdf.PdfDictionary;

                foreach (String fontElementKey in fontDictionary.Elements.Keys) { 

                    String propertyName = fontElementKey;

                    String propertyValue = fontDictionary.Elements [fontElementKey].ToString ();

                    System.Diagnostics.Debug.WriteLine ("[FONT:" + currentElementKey + "] | [" + propertyName + ":" + propertyValue + "]");

                    if (propertyName == "/FontDescriptor") {

                        Int32 fontDescriptorObjectId = Convert.ToInt32 (propertyValue.Split (' ')[0]);

                        PdfSharp.Pdf.PdfObject fontDescriptor = PdfSharpGetItem (page.Owner, fontDescriptorObjectId);

                        System.Diagnostics.Debug.WriteLine ("DESCRIPTOR (BEGIN)");

                        foreach (String descriptorElementKey in (fontDescriptor as PdfSharp.Pdf.PdfDictionary).Elements.Keys) {

                            propertyName = descriptorElementKey;

                            propertyValue = (fontDescriptor as PdfSharp.Pdf.PdfDictionary).Elements[descriptorElementKey].ToString ();

                            System.Diagnostics.Debug.WriteLine ("[FONT:" + currentElementKey + "] | [" + propertyName + ":" + propertyValue + "]");

                        }

                        System.Diagnostics.Debug.WriteLine ("DESCRIPTOR ( END )");

                    }




                }


            }



            return;

        }


        static void PdfSharpTest () {

            PdfSharp.Pdf.PdfDocument pdfDocument = PdfSharp.Pdf.IO.PdfReader.Open (@"C:\MERCURY\flyer.pdf", PdfSharp.Pdf.IO.PdfDocumentOpenMode.ReadOnly);


            



            foreach (PdfSharp.Pdf.PdfPage currentPage in pdfDocument.Pages) {

                System.Diagnostics.Debug.WriteLine ("[Page] " + currentPage.Reference.ToString ());

                PdfSharpTestParsePage (currentPage);


                // PdfSharpTextParseElements (currentPage.Elements);

                foreach (PdfSharp.Pdf.PdfItem currentItem in currentPage.Contents) {

                    if (currentItem is PdfSharp.Pdf.Advanced.PdfReference) {

                        PdfSharp.Pdf.Advanced.PdfReference currentReference = currentItem as PdfSharp.Pdf.Advanced.PdfReference;

                        PdfSharpTestParseReference (currentReference);

                    }

                    else {

                        System.Diagnostics.Debug.WriteLine ("[Page Item] " + currentItem.ToString ());

                    }



                }





            }



            return;

        }

        static void PrintingTest () {

            System.Printing.PrintServer printServer = new System.Printing.PrintServer (@"\\qsdc001");


            System.Printing.PrintQueueCollection printQueuesAvailable = printServer.GetPrintQueues ();


            foreach (System.Printing.PrintQueue currentPrintQueue in printQueuesAvailable) {

                System.Diagnostics.Debug.WriteLine ("Print Queue: " + currentPrintQueue.Name);

                

                System.Printing.PrintCapabilities capabilities = currentPrintQueue.GetPrintCapabilities ();

                for (Int32 currentIndex = 0; currentIndex < capabilities.DuplexingCapability.Count; currentIndex++) {

                    System.Diagnostics.Debug.WriteLine ("Duplexing: " + capabilities.DuplexingCapability[currentIndex].ToString ());   

                }


                for (Int32 currentIndex = 0; currentIndex < capabilities.OutputColorCapability.Count; currentIndex++) {

                    System.Diagnostics.Debug.WriteLine ("Color: " + capabilities.OutputColorCapability[currentIndex].ToString ());

                }


                for (Int32 currentIndex = 0; currentIndex < capabilities.OutputQualityCapability.Count; currentIndex++) {

                    System.Diagnostics.Debug.WriteLine ("Quality: " + capabilities.OutputQualityCapability[currentIndex].ToString ());

                }


                for (Int32 currentIndex = 0; currentIndex < capabilities.PageResolutionCapability.Count; currentIndex++) {

                    System.Diagnostics.Debug.WriteLine ("Resolution: " + capabilities.PageResolutionCapability[currentIndex].ToString ());

                }

                String printCapabilities = System.Text.ASCIIEncoding.ASCII.GetString (currentPrintQueue.GetPrintCapabilitiesAsXml ().ToArray ());

                printCapabilities = printCapabilities.Replace ("<psf:", "<");

                printCapabilities = printCapabilities.Replace ("</psf:", "</");

                System.Diagnostics.Debug.WriteLine (printCapabilities);

                System.Xml.XmlDocument capabilitiesXml = new System.Xml.XmlDocument ();

                capabilitiesXml.LoadXml (printCapabilities);

                System.Diagnostics.Debug.WriteLine (printCapabilities);

                foreach (System.Xml.XmlNode currentNode in capabilitiesXml.SelectNodes ("/PrintCapabilities/Feature[@name=\"psk:JobInputBin\"]/Option")) {

                    String printTrayName = currentNode.Attributes["name"].InnerText.Split (':')[currentNode.Attributes["name"].InnerText.Split (':').Length - 1];

                    Console.WriteLine (printTrayName);

                }

                foreach (System.Printing.InputBin currentInputBin in currentPrintQueue.GetPrintCapabilities ().InputBinCapability) {
                    
                    Console.WriteLine (currentInputBin);

                }

            }


            Console.WriteLine ("\nPress Return to continue.");

            Console.ReadLine ();




            return;


        }

        static void PdfPrintingTest () {


            PdfSharp.Pdf.PdfDocument pdfDocument = PdfSharp.Pdf.IO.PdfReader.Open (@"C:\MERCURY\BHPP_Flyer_5_10.pdf", PdfSharp.Pdf.IO.PdfDocumentOpenMode.ReadOnly);

                     

            System.Printing.LocalPrintServer printServer = new System.Printing.LocalPrintServer ();

            // System.Printing.PrintServer printServer = new System.Printing.PrintServer (@"\\qsdc001");

            System.Printing.PrintQueueCollection printQueues = printServer.GetPrintQueues ();

            foreach (System.Printing.PrintQueue currentPrintQueue in printQueues) {

                System.Diagnostics.Debug.WriteLine (currentPrintQueue.Name);

            }





            System.Diagnostics.Process acrobatProcess = new System.Diagnostics.Process ();

            acrobatProcess.StartInfo.FileName = @"C:\Program Files (x86)\Adobe\Reader 9.0\Reader\AcroRd32.exe";

            acrobatProcess.Start ();

            // System.Threading.Thread.Sleep (5000);


            PdfSharp.Pdf.Printing.PdfFilePrinter.AdobeReaderPath = @"C:\Program Files (x86)\Adobe\Reader 9.0\Reader\AcroRd32.exe";

            PdfSharp.Pdf.Printing.PdfFilePrinter pdfPrinter = new PdfSharp.Pdf.Printing.PdfFilePrinter ();

            pdfPrinter.PrinterName = @"Microsoft XPS Document Writer";

            pdfPrinter.PdfFileName = @"C:\MERCURY\BHPP_Flyer_5_10.pdf";

            pdfPrinter.WorkingDirectory = @"C:\MERCURY\";

            pdfPrinter.Print ();

            // acrobatProcess.CloseMainWindow ();

        }

        static void Main (string[] args) {

            // PdfSharpTest ();

            //PdfPrintingTest ();

            PrintingTest ();


            //Ocr ocrEngine = new Ocr (@"C:\Mercury\TessData");

            //ocrEngine.Process (@"C:\Mercury.Development\Mercury.Server\Mercury.Testing\OcrEpsdt8015.xml");





            // TIFF FILE, GET

            //Mercury.Server.Public.ImageStream imageStream = new Server.Public.ImageStream ();

            //imageStream.LoadFromFile (@"C:\Mercury\epsdt8015.tif");

            //Int32 pageCount = imageStream.TiffPageCount ();

            //System.Diagnostics.Debug.WriteLine (pageCount);


            //System.Drawing.Bitmap pageImage;


            //#region DropoutColorProcessing

            //pageImage = new System.Drawing.Bitmap (@"C:\MERCURY\EPSDT8015.TIF");

            //System.Drawing.Bitmap ocrDropoutImage = new System.Drawing.Bitmap (pageImage.Width, pageImage.Height);

            //ocrDropoutImage = Server.Public.Imaging.OcrImaging.DropoutColors (pageImage, System.Drawing.Color.Black, System.Drawing.Color.FromArgb (50, 50, 50));

            //ocrDropoutImage.Save (@"C:\MERCURY\OCRTESTOUT.TIF", System.Drawing.Imaging.ImageFormat.Tiff);

            //#endregion 


            //pageImage = new System.Drawing.Bitmap (@"C:\MERCURY\OCRTESTOUT.TIF");

            //System.Drawing.Rectangle[] regions = new System.Drawing.Rectangle[] {

            //    new  System.Drawing.Rectangle (36, 103, 150, 22),

            //    new  System.Drawing.Rectangle (191, 133, 8, 8),

            //    new  System.Drawing.Rectangle (218, 133, 8, 8),

            //    new  System.Drawing.Rectangle (246, 135, 5, 5),

            //    new  System.Drawing.Rectangle (271, 135, 5, 5),
                
            //    new  System.Drawing.Rectangle (764, 132, 9, 9)

            //    //new System.Drawing.Rectangle (36, 103, 33, 22),

            //    //new System.Drawing.Rectangle (75, 103, 15, 22),

            //    //new System.Drawing.Rectangle (98, 103, 15, 22),

            //    //new System.Drawing.Rectangle (122, 103, 15, 22),

            //    //new System.Drawing.Rectangle (145, 103, 15, 22),

            //    //new System.Drawing.Rectangle (165, 103, 15, 22),


            //};


            //tessnet2.Tesseract ocrEngine = new tessnet2.Tesseract ();

            //ocrEngine.Init (@"C:\MERCURY\TESSDATA", "eng", false);

            //ocrEngine.SetVariable ("tessedit_char_whitelist", "0123456789");

            //Int32 currentRegionIndex = 0;

            //Int32 scaleMultiplier = pageImage.Width / (850); // 100 DPI

            //foreach (System.Drawing.Rectangle currentRegion in regions) {

            //    System.Drawing.Rectangle scaledRegion = new System.Drawing.Rectangle (

            //        currentRegion.X * scaleMultiplier,

            //        currentRegion.Y * scaleMultiplier,

            //        currentRegion.Width * scaleMultiplier,

            //        currentRegion.Height * scaleMultiplier

            //        );



            //    currentRegionIndex = currentRegionIndex + 1;

            //    System.Drawing.Bitmap ocrImage = new System.Drawing.Bitmap (scaledRegion.Width, scaledRegion.Height);

            //    System.Drawing.Graphics graphicsPage = System.Drawing.Graphics.FromImage (ocrImage);

            //    graphicsPage.DrawImage (pageImage, 0, 0, scaledRegion, System.Drawing.GraphicsUnit.Pixel);

            //    ocrImage.Save (@"C:\MERCURY\OCRTESTOUT" + currentRegionIndex.ToString () + ".TIF", System.Drawing.Imaging.ImageFormat.Tiff);


            //    List<tessnet2.Word> results = ocrEngine.DoOCR (pageImage, currentRegion);

            //    foreach (tessnet2.Word currentWord in results) {

            //        if (currentWord.Text != "~") {

            //            System.Diagnostics.Debug.WriteLine ("Word: " + currentWord.Text + " [" + currentWord.Confidence + "]");

            //        }

            //        else {

            //            System.Drawing.Color averageColor = Server.Public.Imaging.OcrImaging.AverageColor (ocrImage);

            //            System.Diagnostics.Debug.Write ("AVERAGE: " + averageColor + " ");

            //            if (averageColor.R < 200) { System.Diagnostics.Debug.WriteLine ("CHECKED"); }

            //            else { System.Diagnostics.Debug.WriteLine ("NOT CHECKED"); }

            //        }

            //    }



            //}


             // OCR IMAGE 

            //MODI.Document ocrDocument = new MODI.Document ();

            //ocrDocument.Create (@"C:\MERCURY\OCRTESTOUT.TIF");

            //ocrDocument.OCR (MODI.MiLANGUAGES.miLANG_ENGLISH, false,false);


            //MODI.Image resultImage = (MODI.Image)ocrDocument.Images[0];

            //MODI.Layout resultLayout = resultImage.Layout;

            //for (Int32 currentIndex = 0; currentIndex < resultLayout.Words.Count; currentIndex++) {

            //    System.Diagnostics.Debug.WriteLine ("FOUND: " + ((MODI.Word)resultLayout.Words[currentIndex]).Text);


            //}


            //ocrDocument.Close ();

            // 

            
            




            


            //System.Printing.PrintServer printServer = new System.Printing.PrintServer (@"\\qsdc001");

            //System.Printing.PrintQueueCollection printQueues = printServer.GetPrintQueues ();

            //foreach (System.Printing.PrintQueue currentPrintQueue in printQueues) {

            //    System.Diagnostics.Debug.WriteLine (currentPrintQueue.Name);

            //}
            

            /*
            AppDomain.CurrentDomain.SetPrincipalPolicy (System.Security.Principal.PrincipalPolicy.WindowsPrincipal);

            System.Security.Principal.WindowsImpersonationContext impersonationContext = null;

            impersonationContext = ((System.Security.Principal.WindowsIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Impersonate ();


            #region Reporting Service Host

            Server.Public.WebService.WebServiceHostConfiguration serviceHostReporting = new Server.Public.WebService.WebServiceHostConfiguration (
                
                "qstestmcm001", 80, "ReportServer", "ReportService2005.asmx"
                
                );

            serviceHostReporting.ClientCredentials.WindowsImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

            serviceHostReporting.BindingConfiguration = new Server.Public.WebService.BindingConfiguration ("BasicHttpBinding", Server.Public.WebService.Enumerations.WebServiceBindingType.BasicHttpBinding);

            serviceHostReporting.BindingConfiguration.SecurityMode = System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly;

            serviceHostReporting.BindingConfiguration.TransportCredentialType = System.ServiceModel.HttpClientCredentialType.Ntlm;

            serviceHostReporting.BindingConfiguration.MessageCredentialType = System.ServiceModel.MessageCredentialType.UserName;


            System.ServiceModel.BasicHttpBinding binding = (System.ServiceModel.BasicHttpBinding) serviceHostReporting.BindingConfiguration.Binding;

            //binding.AllowCookies = true;


            ReportingService2005.ReportingService2005SoapClient reportingClient = new ReportingService2005.ReportingService2005SoapClient (

                binding, serviceHostReporting.EndpointAddress);


            // reportingClient = new ReportingService2005.ReportingService2005SoapClient ("ReportingService2005Soap");



            reportingClient.ClientCredentials.Windows.AllowedImpersonationLevel = serviceHostReporting.ClientCredentials.WindowsImpersonationLevel;

            reportingClient.ClientCredentials.Windows.ClientCredential = serviceHostReporting.ClientCredentials.Credentials;


            ReportingService2005.ReportParameter[] reportParameters = null;

            reportingClient.GetReportParameters ("/Report1", null, false,null, null, out reportParameters);
            
            #endregion 


            #region Execution Service Host

            Server.Public.WebService.WebServiceHostConfiguration serviceHostExecution = new Server.Public.WebService.WebServiceHostConfiguration ();

            #endregion 

            */

            
            // OPEN UP THE MAX OBJECTS IN GRAPH TO MAXIMUM

            //foreach (System.ServiceModel.Description.OperationDescription currentOperation in applicationClient.Endpoint.Contract.Operations) {

            //    System.ServiceModel.Description.DataContractSerializerOperationBehavior dataContractSerializer =

            //        (System.ServiceModel.Description.DataContractSerializerOperationBehavior)

            //        currentOperation.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior> ();

            //    if (dataContractSerializer != null) {

            //        dataContractSerializer.MaxItemsInObjectGraph = Int32.MaxValue;

            //    }


            //}


        }

    }

}
