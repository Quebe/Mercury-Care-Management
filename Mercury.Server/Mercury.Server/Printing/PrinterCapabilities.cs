using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Printing {

    [DataContract (Name = "PrinterCapabilities")]
    public class PrinterCapabilities {

        #region Private Properties

        [DataMember (Name = "PageResolutions")]
        private Dictionary<String, String> pageResolutions = new Dictionary<String, String> ();

        [DataMember (Name = "ColorOptions")]
        private Dictionary<String, String> colorOptions = new Dictionary<String, String> ();

        [DataMember (Name = "Duplexing")]
        private List<System.Printing.Duplexing> duplexing = new List<System.Printing.Duplexing> ();

        [DataMember (Name = "InputBins")]
        private Dictionary<String, String> inputBins = new Dictionary<String, String> ();

        [DataMember (Name = "OutputBins")]
        private Dictionary<String, String> outputBins = new Dictionary<String, String> ();

        #endregion 


        #region Public Properties

        public List<System.Printing.Duplexing> Duplexing { get { return duplexing; } set { duplexing = value; } }

        #endregion 


        #region Constructors 

        public PrinterCapabilities () { /* DO NOTHING */ }

        public PrinterCapabilities (System.Printing.PrintQueue printQueue) {

            System.Xml.XmlNode propertyNode;

            String printCapabilities = System.Text.ASCIIEncoding.ASCII.GetString (printQueue.GetPrintCapabilitiesAsXml ().ToArray ());

            printCapabilities = printCapabilities.Replace ("<psf:", "<");

            printCapabilities = printCapabilities.Replace ("</psf:", "</");

            System.Xml.XmlDocument capabilitiesXml = new System.Xml.XmlDocument ();

            capabilitiesXml.LoadXml (printCapabilities);
            
            System.Diagnostics.Debug.WriteLine (capabilitiesXml.OuterXml);

            
            // SET DUPLEXING OPTIONS

            foreach (System.Printing.Duplexing currentDuplexing in printQueue.GetPrintCapabilities ().DuplexingCapability) {

                duplexing.Add (currentDuplexing);

            }

            #region SET INPUT BIN OPTIONS, HARD WAY THROUGH XML

            foreach (System.Xml.XmlNode currentNode in capabilitiesXml.SelectNodes ("/PrintCapabilities/Feature[@name=\"psk:JobInputBin\"]/Option")) {

                String inputBinName = currentNode.Attributes["name"].InnerText.Split (':')[currentNode.Attributes["name"].InnerText.Split (':').Length - 1];

                String inputBinDescription = inputBinName;

                propertyNode = currentNode.SelectSingleNode ("Property/Value");

                if (propertyNode != null) {

                    inputBinDescription = propertyNode.InnerText;

                }

                if (!inputBins.ContainsKey (inputBinName)) {

                    inputBins.Add (inputBinName, inputBinDescription);

                }

            }

            #endregion 


            #region SET OUTPUT BIN OPTIONS, HARD WAY THROUGH XML

            foreach (System.Xml.XmlNode currentNode in capabilitiesXml.SelectNodes ("/PrintCapabilities/Feature[@name=\"psk:JobOutputBin\"]/Option")) {

                String outputBinName = currentNode.Attributes["name"].InnerText.Split (':')[currentNode.Attributes["name"].InnerText.Split (':').Length - 1];

                String outputBinDescription = outputBinName;

                propertyNode = currentNode.SelectSingleNode ("Property/Value");

                if (propertyNode != null) {

                    outputBinDescription = propertyNode.InnerText;

                }

                if (!outputBins.ContainsKey (outputBinName)) {

                    outputBins.Add (outputBinName, outputBinDescription);

                }

            }

            #endregion 


            #region GET PAGE RESOLUTIONS, HARD WAY THROUGH XML

            foreach (System.Xml.XmlNode currentNode in capabilitiesXml.SelectNodes ("/PrintCapabilities/Feature[@name=\"psk:PageResolution\"]/Option")) {

                String pageResolutionName = currentNode.Attributes["name"].InnerText.Split (':')[currentNode.Attributes["name"].InnerText.Split (':').Length - 1];

                String pageResolutionDescription = pageResolutionName;

                propertyNode = currentNode.SelectSingleNode ("Property/Value");

                if (propertyNode != null) {

                    pageResolutionDescription = propertyNode.InnerText;

                }

                if (!pageResolutions.ContainsKey (pageResolutionName)) {

                    pageResolutions.Add (pageResolutionName, pageResolutionDescription);

                }

            }

            #endregion 


            #region GET OUTPUT COLOR SUPPORT, HARD WAY THROUGH XML

            foreach (System.Xml.XmlNode currentNode in capabilitiesXml.SelectNodes ("/PrintCapabilities/Feature[@name=\"psk:PageOutputColor\"]/Option")) {

                String colorName = currentNode.Attributes["name"].InnerText.Split (':')[currentNode.Attributes["name"].InnerText.Split (':').Length - 1];

                String colorDescription = colorName;

                propertyNode = currentNode.SelectSingleNode ("Property/Value");

                if (propertyNode != null) {

                    colorDescription = propertyNode.InnerText;

                }

                if (!colorOptions.ContainsKey (colorName)) {

                    colorOptions.Add (colorName, colorDescription);

                }

            }

            #endregion 

            return;


        }

        #endregion 

    }

}
