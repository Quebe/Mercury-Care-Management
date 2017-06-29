using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mercury.Server.Public.Faxing;

namespace Mercury.Server.Faxing.GfiFaxMaker {

    public class GfiFaxMaker14 : Public.Faxing.IFaxServer {

        #region Private Properties
        
        private System.IO.FileSystemWatcher statusWatcher = null;

        private DateTime statusWatchStartTime = DateTime.Now;


        private Public.Faxing.FaxServerConfiguration faxServerConfiguration;

        private Public.Faxing.FaxSender sender;

        private Public.Faxing.FaxRecipient recipient;

        private Public.Faxing.FaxDocument document;

        #endregion 


        #region Public Properties


        #endregion 


        #region Public Events

        public event EventHandler<FaxCompletedEventArgs> OnFaxCompleted;

        #endregion


        #region Methods

        private System.Xml.XmlDocument XmlEmptyDocument () {

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            xmlDocument.InsertBefore (xmlDeclaration, xmlDocument.DocumentElement);


            return xmlDocument;

        }

        public void Fax (Public.Faxing.FaxServerConfiguration forFaxServerConfiguration, FaxSender forSender, FaxRecipient forRecipient, FaxDocument forDocument, Dictionary<String, String> extendedProperties) {

            faxServerConfiguration = forFaxServerConfiguration;

            sender = forSender;

            recipient = forRecipient;

            document = forDocument;

                        
            String controlFileName = document.UniqueId + ".xml";

            String attachmentFileName = document.UniqueId + "." + document.Attachment.Extension;

            String statusFileName = controlFileName + ".status";


            System.Diagnostics.Debug.WriteLine ("Source ID:" + document.UniqueId);


            // CREATE FAX CONTROL FILE


            System.Xml.XmlDocument controlDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = controlDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            System.Xml.XmlElement controlElementNode = controlDocument.CreateElement ("faxmakerdata");

            System.Xml.XmlElement fieldsElementNode = controlDocument.CreateElement ("fields");

            System.Xml.XmlElement senderElementNode = controlDocument.CreateElement ("sender");

            System.Xml.XmlElement recipientsElementNode = controlDocument.CreateElement ("recipients");

            System.Xml.XmlElement recipientsFaxElementNode = controlDocument.CreateElement ("fax");

            System.Xml.XmlElement recipientElementNode = controlDocument.CreateElement ("recipient");


            #region Initialize Document Structure

            controlDocument.InsertBefore (xmlDeclaration, controlDocument.DocumentElement);

            controlDocument.AppendChild (controlElementNode);

            controlElementNode.AppendChild (fieldsElementNode);

            controlElementNode.AppendChild (senderElementNode);

            controlElementNode.AppendChild (recipientsElementNode);

            recipientsElementNode.AppendChild (recipientsFaxElementNode);

            recipientsFaxElementNode.AppendChild (recipientElementNode);


            // FAXMAKERDATA

            // +-- FIELDS

            // +-- SENDER 

            // +-- RECIPIENTS

            // +-- +-- FAX

            // +-- +-- +-- RECIPIENT

            #endregion


            #region Sender Node

            if (!String.IsNullOrWhiteSpace (sender.Name)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, senderElementNode, "lastname", sender.Name); }

            if (!String.IsNullOrWhiteSpace (sender.Company)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, senderElementNode, "company", sender.Company); }

            if (!String.IsNullOrWhiteSpace (sender.Department)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, senderElementNode, "department", sender.Department); }

            if (!String.IsNullOrWhiteSpace (sender.FaxNumber)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, senderElementNode, "faxnumber", sender.FaxNumber); }

            if (!String.IsNullOrWhiteSpace (sender.VoiceNumber)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, senderElementNode, "voicenumber", sender.VoiceNumber); }

            // if (!String.IsNullOrWhiteSpace (sender.Email)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, senderElementNode, "emailaddress", sender.Email); }

            Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, senderElementNode, "emailaddress", sender.Email);

            #endregion 
            

            #region Recipient Node

            String faxNumber = recipient.FaxNumber;

            faxNumber = (faxNumber.Length == 7) ? faxNumber.Substring (0, 3) + "-" + faxNumber.Substring (3, 4) : faxNumber;

            faxNumber = (faxNumber.Length == 10) ? "1-" + faxNumber.Substring (0, 3) + "-" + faxNumber.Substring (3, 3) + "-" + faxNumber.Substring (6, 4) : faxNumber;



            if (!String.IsNullOrWhiteSpace (recipient.RecipientName)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, recipientElementNode, "lastname", recipient.RecipientName); }

            if (!String.IsNullOrWhiteSpace (recipient.CompanyName)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, recipientElementNode, "company", recipient.CompanyName); }

            if (!String.IsNullOrWhiteSpace (recipient.DepartmentName)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, recipientElementNode, "department", recipient.DepartmentName); }

            Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, recipientElementNode, "faxnumber", faxNumber);

            if (!String.IsNullOrWhiteSpace (recipient.RecipientEmail)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, recipientElementNode, "emaladdress", recipient.RecipientEmail); }

            #endregion 
            

            #region Fields Node

            if (!String.IsNullOrWhiteSpace (document.Subject)) { Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, fieldsElementNode, "subject", document.Subject); }

            Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, fieldsElementNode, "resolution", "high");

            Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, fieldsElementNode, "attachment", attachmentFileName);

            Mercury.Server.Public.CommonFunctions.XmlDocumentAppendNode (controlDocument, fieldsElementNode, "uid", document.UniqueId);

            #endregion 

            
            attachmentFileName = faxServerConfiguration.FaxUrl + @"\" + attachmentFileName;

            attachmentFileName = attachmentFileName.Replace (@"\\" + attachmentFileName, @"\" + attachmentFileName);


            System.IO.FileStream attachmentFile = new System.IO.FileStream (attachmentFileName, System.IO.FileMode.Create);

            document.Attachment.Image.Seek (0, System.IO.SeekOrigin.Begin);

            document.Attachment.Image.WriteTo (attachmentFile);

            attachmentFile.Flush ();

            attachmentFile.Close ();


            controlFileName = faxServerConfiguration.FaxUrl + @"\" + controlFileName;

            controlFileName = controlFileName.Replace (@"\\" + controlFileName, @"\" + controlFileName);

            controlDocument.Save (controlFileName);


            statusWatchStartTime = DateTime.Now;

            statusWatcher = new System.IO.FileSystemWatcher ();

            statusWatcher.Filter = "*.status";

            statusWatcher.Created += new System.IO.FileSystemEventHandler (StatusWatcher_OnFileCreated);

            statusWatcher.Path = faxServerConfiguration.FaxUrl;

            statusWatcher.EnableRaisingEvents = true;
            

            return;

        }

        public void StatusWatcher_OnFileCreated (Object sender, System.IO.FileSystemEventArgs e) {

            FaxCompletedEventArgs returnArguments = new FaxCompletedEventArgs (document.UniqueId);


            if (e.Name == document.UniqueId + ".xml.status") {

                // FILE FOUND

                statusWatcher.EnableRaisingEvents = false; // DISABLE MONITORING


                // READ STATUS FILE AND SET EVENT ARGUMENTS

                System.Xml.XmlDocument statusDocument = new System.Xml.XmlDocument ();

                statusDocument.Load (e.FullPath);


                System.Xml.XmlNode errorCodeNode = statusDocument.SelectSingleNode ("//errorcode");

                System.Xml.XmlNode errorDescriptionNode = statusDocument.SelectSingleNode ("//description");

                System.Xml.XmlNode documentUniqueIdNode = statusDocument.SelectSingleNode ("//uid");


                if (Convert.ToInt32 (errorCodeNode.InnerText) != 0) {

                    String descriptionMessage = String.Empty;

                    if (errorDescriptionNode.InnerText.Contains ("Description:")) {

                        Int32 startPosition = errorDescriptionNode.InnerText.IndexOf ("Description:");

                        startPosition = startPosition + ("Description:").Length;

                        descriptionMessage = errorDescriptionNode.InnerText.Substring (startPosition, errorDescriptionNode.InnerText.Length - startPosition);

                        descriptionMessage = descriptionMessage.Trim ();

                    }

                    else { descriptionMessage = errorDescriptionNode.InnerText; }

                    returnArguments = new FaxCompletedEventArgs (document.UniqueId, descriptionMessage);

                }


                if (OnFaxCompleted != null) { OnFaxCompleted (this, returnArguments ); }

            }


            else if (DateTime.Now.Subtract (statusWatchStartTime).TotalSeconds > faxServerConfiguration.MonitorTimeout) {

                // TIMEOUT FOR MONITORING, NEVER RECEIVED STATUS FILE

                statusWatcher.EnableRaisingEvents = false; // DISABLE MONITORING

                // ASSUME FAILURE

                returnArguments = new FaxCompletedEventArgs (document.UniqueId, "Timeout waiting for response status.");

                if (OnFaxCompleted != null) { OnFaxCompleted (this, returnArguments); }

            }

            return;

        }

        #endregion 
    }

}


