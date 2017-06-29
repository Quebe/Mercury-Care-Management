using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Reference {

    [DataContract (Name = "CorrespondenceContent")]
    public class CorrespondenceContent : CoreObject {

        #region Private Properties

        [DataMember (Name = "CorrespondenceContentPath")]
        private String correspondenceContentPath = String.Empty;

        [DataMember (Name = "CorrespondenceId")]
        private Int64 correspondenceId;

        [DataMember (Name = "ContentSequence")]
        private Int32 contentSequence;

        [DataMember (Name = "ContentType")]
        private Enumerations.CorrespondenceContentType contentType = Enumerations.CorrespondenceContentType.Report;

        [DataMember (Name = "ReportingServerId")]
        private Int64 reportingServerId;

        [DataMember (Name = "AttachmentBase64")]
        private String attachmentBase64;

        [DataMember (Name = "AttachmentXpsBase64")]
        private String attachmentXpsBase64;

        [DataMember (Name = "IsAttachmentCompressed")]
        private Boolean isAttachmentCompressed = false;

        #endregion


        #region Public Properties

        public String CorrespondenceContentPath { get { return correspondenceContentPath; } set { correspondenceContentPath = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Path); } }

        public Int64 CorrespondenceId { get { return correspondenceId; } set { correspondenceId = value; } }

        public Int32 ContentSequence { get { return contentSequence; } set { contentSequence = value; } }

        public Enumerations.CorrespondenceContentType ContentType { get { return contentType; } set { contentType = value; } }

        public Int64 ReportingServerId { get { return reportingServerId; } set { reportingServerId = value; } }

        public Reporting.ReportingServer ReportingServer { get { return application.ReportingServerGet (reportingServerId); } }  

        public String ReportName { 
            
            get {

                String reportName = CorrespondenceContentPath.Replace ("%EnvironmentName%", application.Session.EnvironmentName);

                reportName = reportName + @"/" + Name.Replace ("%EnvironmentName%", application.Session.EnvironmentName);

                reportName = reportName.Replace (@"//", @"/");

                return reportName;
            
            } 
        
        }

        public String AttachmentBase64 { get { return attachmentBase64; } set { attachmentBase64 = value; } }

        public String AttachmentXpsBase64 { get { return attachmentXpsBase64; } set { attachmentXpsBase64 = value; } }

        public System.IO.MemoryStream Attachment {

            get {

                if (String.IsNullOrWhiteSpace (attachmentBase64)) { return new System.IO.MemoryStream (); }

                return new System.IO.MemoryStream (Convert.FromBase64String (attachmentBase64)); 
            
            }

            set { attachmentBase64 = Convert.ToBase64String (value.ToArray ()); }

        }

        public System.IO.MemoryStream AttachmentXps {

            get {

                if (String.IsNullOrWhiteSpace (attachmentXpsBase64)) { return new System.IO.MemoryStream (); }

                return new System.IO.MemoryStream (Convert.FromBase64String (attachmentXpsBase64));

            }

            set { attachmentXpsBase64 = Convert.ToBase64String (value.ToArray ()); }

        }

        public Boolean IsAttachmentCompressed { get { return isAttachmentCompressed; } set { isAttachmentCompressed = value; } }

        #endregion 


        #region Constructors

        public CorrespondenceContent (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public CorrespondenceContent (Application applicationReference, Int64 forId) {

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion 

        
        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CorrespondenceContentPath", CorrespondenceContentPath);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CorrespondenceId", CorrespondenceId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ContentSequence", ContentSequence.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ContentTypeInt32", ((Int32) ContentType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ContentType", ContentType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ReportingServerId", ReportingServerId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AttachmentBase64", AttachmentBase64);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AttachmentXpsBase64", AttachmentXpsBase64);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "IsAttachmentCompressed", IsAttachmentCompressed.ToString ());

            #endregion


            #region Object Nodes

            if (ReportingServer != null) { // NOT ALL CONTENT HAS A REPORTING SERVER (I.E. ATTACHMENTS)

                document.LastChild.AppendChild (document.ImportNode (ReportingServer.XmlSerialize ().LastChild, true));

            }

            #endregion 


            return document;

        }
        
        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);


            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].InnerText) {


                                    case "CorrespondenceContentPath": CorrespondenceContentPath = currentPropertyNode.InnerText; break;

                                    case "ContentSequence": ContentSequence = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ContentTypeInt32": ContentType = (Enumerations.CorrespondenceContentType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "AttachmentBase64": AttachmentBase64 = currentPropertyNode.InnerText; break;

                                    case "AttachmentXpsBase64": AttachmentXpsBase64 = currentPropertyNode.InnerText; break;

                                    case "IsAttachmentCompressed": IsAttachmentCompressed = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                                }

                            }

                            break;

                        case "ReportingServer":
                            
                            reportingServerId = application.CoreObjectGetIdByName ("ReportingServer", currentNode.Attributes["Name"].InnerText);

                            if (reportingServerId == 0) {

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                Reporting.ReportingServer reportingServer = new Reporting.ReportingServer (application);

                                response.AddRange (reportingServer.XmlImport (currentNode));

                                reportingServerId = application.CoreObjectGetIdByName ("ReportingServer", currentNode.Attributes["Name"].InnerText);

                                if (reportingServerId == 0) {

                                    throw new ApplicationException ("Unable to import Reporting Server: " + currentNode.Attributes["Name"].InnerText + ".");

                                }

                            }

                            break;

                    }

                }

                // DO NOT SAVE, SAVE IS DONE BY PARENT OBJECT 

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validation

        public override Dictionary<string, string> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<string, string> ();


            switch (contentType) {

                case Enumerations.CorrespondenceContentType.Report:
                
                    if (String.IsNullOrWhiteSpace (Name)) { validationResponse.Add ("Report or Attachment Name", "Empty or NULL."); }

                    break;

                case Enumerations.CorrespondenceContentType.Attachment:

                    String fileExtension = String.Empty;

                    if ((String.IsNullOrWhiteSpace (attachmentBase64)) || (name.Split ('.').Length < 2)) { validationResponse.Add ("Attachment", "Not found."); }

                    else {

                        try {

                            fileExtension = name.Split ('.')[name.Split ('.').Length - 1];

                            switch (fileExtension.ToLower ()) { 

                                case "pdf":

                                    PdfSharp.Pdf.PdfDocument pdfDocument = PdfSharp.Pdf.IO.PdfReader.Open (Attachment);

                                    // validationResponse.Add ("Attachment", "PDF file format not supported at this time.");

                                    break;

                                case "xps":
                                    
                                    System.IO.Packaging.Package attachmentPackage = System.IO.Packaging.Package.Open (Attachment);

                                    System.Windows.Xps.Packaging.XpsDocument xpsContent = new System.Windows.Xps.Packaging.XpsDocument (attachmentPackage);

                                    validationResponse.Add ("Attachment", "XPS file format not supported at this time.");

                                    break;

                                default:

                                    validationResponse.Add ("Attachment", "Unknown or unsupported file format [" + fileExtension + "].");

                                    break;

                            }

                            if (!String.IsNullOrWhiteSpace (attachmentXpsBase64)) {

                                try {

                                    System.IO.Packaging.Package attachmentPackage = System.IO.Packaging.Package.Open (AttachmentXps);

                                    System.Windows.Xps.Packaging.XpsDocument xpsContent = new System.Windows.Xps.Packaging.XpsDocument (attachmentPackage);

                                }

                                catch (Exception attachmentException) {

#if DEBUG

                                    System.Diagnostics.Debug.WriteLine (attachmentException.Message);

#endif
                                    validationResponse.Add ("Attachment XPS", "Unable to successfully open and parse XPS Attachment.");

                                }

                            }

                        }

                        catch (Exception attachmentException) {

#if DEBUG

                            System.Diagnostics.Debug.WriteLine (attachmentException.Message);

#endif

                            validationResponse.Add ("Attachment", "Unknown or unsupported file format [" + fileExtension + "].");

                        }

                    }

                    break;

            }


            return validationResponse;
        
        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            correspondenceContentPath = (String)currentRow["CorrespondenceContentPath"];

            correspondenceId = IdFromSql (currentRow, "CorrespondenceId");

            contentSequence = Convert.ToInt32 (currentRow ["ContentSequence"]);

            contentType = (Enumerations.CorrespondenceContentType) Convert.ToInt32 (currentRow ["ContentType"]);


            reportingServerId = IdFromSql (currentRow, "ReportingServerId");

            // ATTACHMENTS ARE LOADED EXTERNALLY
            
            isAttachmentCompressed = Convert.ToBoolean (currentRow["IsAttachmentCompressed"]);

            return;

        }

        public void LoadAttachment () {

            if (contentType == Enumerations.CorrespondenceContentType.Attachment) {

                // PRIMARY ATTACHMENT

                System.IO.Stream attachmentStream = application.EnvironmentDatabase.BlobRead ("dbo.CorrespondenceContent", "Attachment", "CorrespondenceContentId = " + id.ToString ());

                if (attachmentStream != null) {

                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream ();

                    attachmentStream.CopyTo (memoryStream);

                    Attachment = memoryStream;

                }

                // XPS ATTACHMENT

                System.IO.Stream attachmentXpsStream = application.EnvironmentDatabase.BlobRead ("dbo.CorrespondenceContent", "AttachmentXps", "CorrespondenceContentId = " + id.ToString ());

                if (attachmentXpsStream != null) {

                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream ();

                    attachmentXpsStream.CopyTo (memoryStream);

                    AttachmentXps = memoryStream;

                }

            }

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransactions = (application.EnvironmentDatabase.OpenTransactions == 0);

            
            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                

                if (usingTransactions) { application.EnvironmentDatabase.BeginTransaction (); }

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC CorrespondenceContent_InsertUpdate ");


                id = 0; // ALWAYS FORCE NEW INSERT

                sqlStatement.Append (id.ToString () + ", "); 

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + correspondenceContentPath.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + correspondenceId.ToString () + "', ");

                sqlStatement.Append (contentSequence.ToString () + ", ");

                sqlStatement.Append (((Int32) contentType).ToString () + ", ");

                sqlStatement.Append (IdSqlAllowNull (reportingServerId) + ", ");

                // ATTACHMENTS ARE SAVED EXTERNALLY

                sqlStatement.Append (isAttachmentCompressed.ToString () + ", ");

                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                // SAVE ATTACHMENTS (PRIMARY AND THEN XPS)

                if ((success) && (id != 0) && (!String.IsNullOrWhiteSpace (attachmentBase64))) {

                    success = application.EnvironmentDatabase.BlobWrite ("dbo.CorrespondenceContent", "Attachment", "CorrespondenceContentId = " + id.ToString (), Attachment);

                    if ((success) && (!String.IsNullOrWhiteSpace (attachmentXpsBase64))) {

                        success = application.EnvironmentDatabase.BlobWrite ("dbo.CorrespondenceContent", "AttachmentXps", "CorrespondenceContentId = " + id.ToString (), AttachmentXps);

                    }

                    if (!success) { throw application.EnvironmentDatabase.LastException; }

                }


                success = true;

                if (usingTransactions) { application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (usingTransactions) { application.EnvironmentDatabase.RollbackTransaction (); }

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
