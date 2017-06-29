using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Entity {

    [Serializable]
    [DataContract (Name = "EntityCorrespondence")]
    public class EntityCorrespondence : CoreExtensibleObject {

        #region Private Properties

        [DataMember (Name = "EntityId")]
        private Int64 entityId;


        [DataMember (Name = "CorrespondenceId")]
        private Int64 correspondenceId;

        [DataMember (Name = "CorrespondenceName")]
        private String correspondenceName;

        [DataMember (Name = "CorrespondenceVersion")]
        private Double correspondenceVersion = 1.0;


        [DataMember (Name = "EntityFormId")]
        private Int64 entityFormId = 0;

        [DataMember (Name = "RelatedEntityId")]
        private Int64 relatedEntityId = 0;

        [DataMember (Name = "RelatedObjectType")]
        private String relatedObjectType = String.Empty;

        [DataMember (Name = "RelatedObjectId")]
        private Int64 relatedObjectId = 0;


        [DataMember (Name = "ReadyToSendDate")]
        private DateTime readyToSendDate = DateTime.Today;

        [DataMember (Name = "SentDate")]
        private DateTime? sentDate;

        [DataMember (Name = "ReceivedDate")]
        private DateTime? receivedDate;

        [DataMember (Name = "ReturnedDate")]
        private DateTime? returnedDate;


        [DataMember (Name = "ContactType")]
        private Core.Enumerations.EntityContactType contactType = Mercury.Server.Core.Enumerations.EntityContactType.ByMail;

        [DataMember (Name = "EntityAddressId")]
        private Int64 entityAddressId = 0;

        [DataMember (Name = "EntityContactInformationId")]
        private Int64 entityContactInformationId = 0;


        [DataMember (Name = "Attention")]
        private String attention = String.Empty;

        [DataMember (Name = "AddressLine1")]
        private String addressLine1 = String.Empty;

        [DataMember (Name = "AddressLine2")]
        private String addressLine2 = String.Empty;

        [DataMember (Name = "AddressCity")]
        private String addressCity = String.Empty;

        [DataMember (Name = "AddressState")]
        private String addressState = String.Empty;

        [DataMember (Name = "AddressZipCode")]
        private String addressZipCode = String.Empty;

        [DataMember (Name = "AddressZipPlus4")]
        private String addressZipPlus4 = String.Empty;

        [DataMember (Name = "AddressPostalCode")]
        private String addressPostalCode = String.Empty;


        [DataMember (Name = "ContactFaxNumber")]
        private String contactFaxNumber = String.Empty;

        [DataMember (Name = "ContactEmail")]
        private String contactEmail = String.Empty;

        [DataMember (Name = "Remarks")]
        private String remarks = String.Empty;


        [DataMember (Name = "AutomationId")]
        private Guid automationId = Guid.Empty;

        [DataMember (Name = "AutomationStatus")]
        private Automation.Enumerations.AutomationStatus automationStatus  = Automation.Enumerations.AutomationStatus.NotSpecified;

        [DataMember (Name = "AutomationDate")]
        private DateTime? automationDate;

        [DataMember (Name = "AutomationException")]
        private String automationException;


        [DataMember (Name = "HasImage")]
        private Boolean hasImage = false;


        [NonSerialized]
        private Core.Reference.Correspondence correspondence = null;

        #endregion


        #region Public Properties

        public Core.Enumerations.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        
        public Int64 CorrespondenceId { get { return correspondenceId; } set { correspondenceId = value; } }

        public String CorrespondenceName { get { return correspondenceName; } set { correspondenceName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public Double CorrespondenceVersion { get { return correspondenceVersion; } set { correspondenceVersion = value; } }


        public Int64 EntityFormId { get { return entityFormId; } set { entityFormId = value; } }

        public Int64 RelatedEntityId { get { return relatedEntityId; } set { relatedEntityId = value; } }

        public String RelatedObjectType { get { return relatedObjectType; } set { relatedObjectType = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.ObjectType); } }

        public Int64 RelatedObjectId { get { return relatedObjectId; } set { relatedObjectId = value; } }


        public DateTime ReadyToSendDate { get { return readyToSendDate; } set { readyToSendDate = value; } }

        public DateTime? SentDate { get { return sentDate; } set { sentDate = value; } }

        public DateTime? ReceivedDate { get { return receivedDate; } set { receivedDate = value; } }

        public DateTime? ReturnedDate { get { return returnedDate; } set { returnedDate = value; } }


        public Int64 EntityAddressId { get { return entityAddressId; } set { entityAddressId = value; } }

        public Int64 EntityContactInformationId { get { return entityContactInformationId; } set { entityContactInformationId = value; } }

        public String Attention { get { return attention; } set { attention = value; } }

        public String AddressLine1 { get { return addressLine1; } set { addressLine1 = value; } }

        public String AddressLine2 { get { return addressLine2; } set { addressLine2 = value; } }

        public String AddressCity { get { return addressCity; } set { addressCity = value; } }

        public String AddressState { get { return addressState; } set { addressState = value; } }

        public String AddressZipCode { get { return addressZipCode; } set { addressZipCode = value; } }

        public String AddressZipPlus4 { get { return addressZipPlus4; } set { addressZipPlus4 = value; } }

        public String AddressPostalCode { get { return addressPostalCode; } set { addressPostalCode = value; } }


        public String ContactFaxNumber { get { return contactFaxNumber; } set { contactFaxNumber = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.ContactNumber); } }

        public String ContactFaxNumberFormatted {

            get {

                String numberFormatted = contactFaxNumber;

                numberFormatted = numberFormatted.Replace (" ", "");

                numberFormatted = numberFormatted.Replace ("(", "");

                numberFormatted = numberFormatted.Replace (")", "");

                numberFormatted = numberFormatted.Replace ("-", "");

                if (numberFormatted.Length < 10) { numberFormatted = "000" + numberFormatted; }

                String formatPattern = @"(\d{3})(\d{3})(\d{4})";

                numberFormatted = System.Text.RegularExpressions.Regex.Replace (numberFormatted, formatPattern, "($1) $2-$3");

                return numberFormatted;

            }

        }

        public String ContactEmail { get { return contactEmail; } set { contactEmail = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String Remarks { get { return remarks; } set { remarks = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }


        public Guid AutomationId { get { return automationId; } set { automationId = value; } }

        public Automation.Enumerations.AutomationStatus AutomationStatus { get { return automationStatus; } set { automationStatus = value; } }

        public DateTime? AutomationDate { get { return automationDate; } set { automationDate = value; } }

        public String AutomationException { get { return automationException; } set { automationException = value; } }


        public Boolean HasImage { get { return hasImage; } set { hasImage = value; } }

        #endregion


        #region Public Properties

        public Core.Reference.Correspondence Correspondence {

            get {

                if (correspondence == null) { correspondence = application.CorrespondenceGet (correspondenceId); }

                return correspondence;

            }

        }

        #endregion 


        #region Constructors

        protected void EntityCorrespondenceBaseConstructor (Application applicationReference, Int64 forEntityId, String forAttention, EntityAddress address, Reference.Correspondence correspondence) {

            BaseConstructor (applicationReference);


            entityId = forEntityId;

            attention = forAttention;

            if (address != null) {

                entityAddressId = address.Id;

                addressLine1 = address.Line1;

                addressLine2 = address.Line2;

                addressCity = address.City;

                addressState = address.State;

                addressZipCode = address.ZipCode;

                addressZipPlus4 = address.ZipPlus4;

                addressPostalCode = address.PostalCode;

            }

            if (correspondence != null) {

                correspondenceId = correspondence.Id;

                correspondenceName = correspondence.Name;

                correspondenceVersion = correspondence.Version;
                
            }

            readyToSendDate = DateTime.Today;

            sentDate = null;

            receivedDate = null;

            returnedDate = null;


            createAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp ();

            return;

        }

        public EntityCorrespondence (Application applicationReference, Entity entity, Reference.Correspondence correspondence) {

            EntityCorrespondenceBaseConstructor (applicationReference, entity.Id, entity.Name, entity.CurrentMailingAddress, correspondence);

            return;

        }

        public EntityCorrespondence (Application applicationReference, Entity entity, EntityAddress address, Reference.Correspondence correspondence) {

            EntityCorrespondenceBaseConstructor (applicationReference, entity.Id, String.Empty, address, correspondence);

            return;

        }

        public EntityCorrespondence (Application applicationReference, Int64 forEntityId, Int64 forCorrespondenceId) {

            Entity entity = new Entity (applicationReference, forEntityId);

            Reference.Correspondence correspondence = new Mercury.Server.Core.Reference.Correspondence (applicationReference, forCorrespondenceId);

            EntityCorrespondenceBaseConstructor (applicationReference, entity.Id, entity.Name, entity.CurrentMailingAddress, correspondence);

            return;

        }

        public EntityCorrespondence (Application applicationReference, Int64 forEntityId, String forAttention, EntityAddress address, Reference.Correspondence correspondence) {

            EntityCorrespondenceBaseConstructor (applicationReference, forEntityId, forAttention, address, correspondence);

            return;

        }

        public EntityCorrespondence (Application applicationReference, Int64 forEntityId, EntityAddress address, Reference.Correspondence correspondence) {

            EntityCorrespondenceBaseConstructor (applicationReference, forEntityId, String.Empty, address, correspondence);

            return;

        }

        public EntityCorrespondence (Application applicationReference, Int64 forEntityCorrespondenceId) {

            BaseConstructor (applicationReference);

            if (!Load (forEntityCorrespondenceId)) {

                throw new ApplicationException ("Unable to load Entity Correspondence from the database for " + forEntityCorrespondenceId.ToString () + ".");

            }

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            Boolean success = false;

            String selectStatement = String.Empty;

            System.Data.DataTable tableEntityCorrespondence;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement += "SELECT EntityCorrespondence.*, ";

            selectStatement += "    CAST (CASE WHEN (EntityCorrespondenceImage.EntityCorrespondenceId IS NULL) THEN 0 ELSE 1 END AS BIT) AS HasImage";
            
            selectStatement += "  FROM dbo.EntityCorrespondence ";

            selectStatement += "    LEFT JOIN dbo.EntityCorrespondenceImage ON EntityCorrespondence.EntityCorrespondenceId = EntityCorrespondenceImage.EntityCorrespondenceId";

            selectStatement += "  WHERE EntityCorrespondence.EntityCorrespondenceId = " + forId.ToString ();


            tableEntityCorrespondence = base.application.EnvironmentDatabase.SelectDataTable (selectStatement);

            if (tableEntityCorrespondence.Rows.Count == 1) {

                MapDataFields (tableEntityCorrespondence.Rows[0]);

                success = true;

            }

            return success;

        }

        override public Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                base.application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC EntityCorrespondence_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (entityId.ToString () + ", ");

                sqlStatement.Append (correspondenceId.ToString () + ", ");

                sqlStatement.Append ("'" + correspondenceName.Replace ("'", "''") + "', ");

                sqlStatement.Append (correspondenceVersion.ToString () + ", ");


                sqlStatement.Append (IdSqlAllowNull (entityFormId) + ", ");

                sqlStatement.Append (IdSqlAllowNull (relatedEntityId) + ", ");

                sqlStatement.Append ("'" + relatedObjectType + "', ");

                sqlStatement.Append (IdSqlAllowNull (relatedObjectId) + ", ");


                sqlStatement.Append ("'" + readyToSendDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ((sentDate.HasValue) ? "'" + sentDate.Value.ToString ("MM/dd/yyyy") + "', " : "NULL, ");

                sqlStatement.Append ((receivedDate.HasValue) ? "'" + receivedDate.Value.ToString ("MM/dd/yyyy") + "', " : "NULL, ");

                sqlStatement.Append ((returnedDate.HasValue) ? "'" + returnedDate.Value.ToString ("MM/dd/yyyy") + "', " : "NULL, ");


                sqlStatement.Append (((Int32) contactType).ToString () + ", ");

                sqlStatement.Append (IdSqlAllowNull (entityAddressId) + ", ");

                sqlStatement.Append (IdSqlAllowNull (entityContactInformationId) + ", ");

                sqlStatement.Append ("'" + attention.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + addressLine1.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + addressLine2.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + addressCity.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + addressState.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + addressZipCode.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + addressZipPlus4.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + addressPostalCode.Replace ("'", "''") + "', ");


                sqlStatement.Append ("'" + contactFaxNumber.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + contactEmail.Replace ("'", "''") + "', ");


                if (String.IsNullOrWhiteSpace (remarks)) { remarks = String.Empty; }

                sqlStatement.Append ("'" + remarks.Replace ("'", "''") + "', ");


                sqlStatement.Append (GuidSqlAllowNull (automationId) + ", ");

                sqlStatement.Append (((Int32)automationStatus).ToString () + ", ");

                sqlStatement.Append (DateTimeSqlAllowNull (automationDate) + ", ");

                sqlStatement.Append (StringSqlAllowNull (automationException) + ", ");


                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    base.application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw base.application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                success = true;

                base.application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                base.application.EnvironmentDatabase.RollbackTransaction ();

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            entityId = (Int64) currentRow["EntityId"];

            correspondenceId = (Int64) currentRow["CorrespondenceId"];

            correspondenceName = (String) currentRow["CorrespondenceName"];

            correspondenceVersion = Convert.ToDouble (currentRow["CorrespondenceVersion"]);


            entityFormId = IdFromSql (currentRow, "EntityFormId");

            relatedEntityId = IdFromSql (currentRow, "RelatedEntityId");

            relatedObjectType = (String) currentRow["RelatedObjectType"];

            relatedObjectId = IdFromSql (currentRow, "RelatedObjectId");


            readyToSendDate = (DateTime) currentRow["ReadyToSendDate"];

            sentDate = (currentRow["SentDate"] is System.DBNull) ? null : sentDate = Convert.ToDateTime (currentRow["SentDate"]);

            receivedDate = (currentRow["ReceivedDate"] is System.DBNull) ? null : returnedDate = Convert.ToDateTime (currentRow["ReceivedDate"]);

            returnedDate = (currentRow["ReturnedDate"] is System.DBNull) ? null : returnedDate = Convert.ToDateTime (currentRow["ReturnedDate"]);


            contactType = (Mercury.Server.Core.Enumerations.EntityContactType) (Int32) currentRow["ContactType"];

            entityAddressId = IdFromSql (currentRow, "EntityAddressId");

            entityContactInformationId = IdFromSql (currentRow, "EntityContactInformationId");

            attention = (String) currentRow["Attention"];

            addressLine1 = (String) currentRow["AddressLine1"];

            addressLine2 = (String) currentRow["AddressLine2"];

            addressCity = (String) currentRow["AddressCity"];

            addressState = (String) currentRow["AddressState"];

            addressZipCode = (String) currentRow["AddressZipCode"];

            addressZipPlus4 = (String) currentRow["AddressZipPlus4"];

            addressPostalCode = (String) currentRow["AddressPostalCode"];


            ContactFaxNumber = (String) currentRow["ContactFaxNumber"];

            ContactEmail = (String) currentRow["ContactEmail"];

            Remarks = StringFromSql (currentRow, "Remarks");


            AutomationId = GuidFromSql (currentRow, "AutomationId");

            AutomationStatus = (Automation.Enumerations.AutomationStatus)Convert.ToInt32 (currentRow["AutomationStatus"]);

            AutomationDate = DateTimeFromSql (currentRow, "AutomationDate");

            AutomationException = StringFromSql (currentRow, "AutomationException");


            if (currentRow.Table.Columns.Contains ("HasImage")) {

                hasImage = Convert.ToBoolean (currentRow["HasImage"]);

            }

            return;

        }

        #endregion


        #region Automation Functions

        private Public.ImageStream RenderContentReport (Reference.CorrespondenceContent correspondenceContent, String format) {

            Public.ImageStream reportImage = null;


            Reporting.ReportingServer reportingServer = application.ReportingServerGet (correspondenceContent.ReportingServerId);

            if (reportingServer == null) { throw new ApplicationException ("Unable to load Reporting Server to Render Content."); }


            System.Reflection.Assembly reportingServerAssembly = System.Reflection.Assembly.LoadFrom (reportingServer.AssemblyReference);

            Type reportingServerType = reportingServerAssembly.GetType (reportingServer.AssemblyClassName);

            if (reportingServerType == null) {

                throw new ApplicationException ("Unable to find Class [" + reportingServer.AssemblyClassName + "] in referenced Assembly [" + reportingServer.AssemblyReference + "].");

            }

            Public.Reporting.IReportingServer reportingServerObject = (Public.Reporting.IReportingServer)Activator.CreateInstance (reportingServerType);

            Dictionary<String, String> reportParameters = new Dictionary<String, String> ();

            reportParameters.Add ("entityCorrespondenceId", id.ToString ());


            reportImage = reportingServerObject.Render (reportingServer.WebServiceHostConfiguration, correspondenceContent.ReportName, reportParameters, format, reportingServer.ExtendedProperties);

            return reportImage;

        }

        private Public.ImageStream RenderXps () {

            Public.ImageStream renderedImage = new Public.ImageStream ();

            System.IO.MemoryStream renderedContent = new System.IO.MemoryStream ();


            String contentUriName = ("memorystream://rendered" + Guid.NewGuid ().ToString ().Replace ("-", "") + ".xps");

            Uri contentUri = new Uri (contentUriName, UriKind.Absolute);


            // FOR TESTING, OUTPUT TO DISK FILE BY CHANGING OPEN FROM MEMORY STREAM TO FILE LOCATION

            System.IO.Packaging.Package xpsPackage = System.IO.Packaging.Package.Open (renderedContent, System.IO.FileMode.Create);



            System.IO.Packaging.PackageStore.AddPackage (contentUri, xpsPackage);

            System.Windows.Xps.Packaging.XpsDocument renderedXpsDocument = new System.Windows.Xps.Packaging.XpsDocument (xpsPackage, System.IO.Packaging.CompressionOption.Normal, contentUriName);

            System.Windows.Xps.XpsDocumentWriter xpsWriter = System.Windows.Xps.Packaging.XpsDocument.CreateXpsDocumentWriter (renderedXpsDocument);

            System.Windows.Xps.Packaging.IXpsFixedDocumentSequenceWriter contentSequenceWriter;
            
            contentSequenceWriter = renderedXpsDocument.AddFixedDocumentSequence ();

            List <System.Windows.Xps.Packaging.XpsDocument> xpsContents = new List<System.Windows.Xps.Packaging.XpsDocument> ();


            // LOAD CORRESPONDENCE RECORD TO SEE IF THERE IS ANY CONTENT AVAILABLE

            Reference.Correspondence renderCorrespondence = application.CorrespondenceGet (correspondenceId);

            if (renderCorrespondence == null) { throw new ApplicationException ("Unable to load base Correspondence to Render Content."); }

            if (renderCorrespondence.Content.Count == 0) { throw new ApplicationException ("No Content to Render for Correspondence."); }

            renderCorrespondence.LoadContentAttachments ();


            foreach (Reference.CorrespondenceContent currentCorrespondenceContent in renderCorrespondence.Content.Values) {

                System.Windows.Xps.Packaging.XpsDocument xpsContent = null;


                switch (currentCorrespondenceContent.ContentType) {

                    case Reference.Enumerations.CorrespondenceContentType.Report:

                        #region Generate Report Content

                        Reporting.ReportingServer reportingServer = application.ReportingServerGet (currentCorrespondenceContent.ReportingServerId);

                        if (reportingServer == null) { throw new ApplicationException ("Unable to load Reporting Server to Render Content."); }


                        System.Reflection.Assembly reportingServerAssembly = System.Reflection.Assembly.LoadFrom (reportingServer.AssemblyReference);

                        Type reportingServerType = reportingServerAssembly.GetType (reportingServer.AssemblyClassName);

                        if (reportingServerType == null) {

                            throw new ApplicationException ("Unable to find Class [" + reportingServer.AssemblyClassName + "] in referenced Assembly [" + reportingServer.AssemblyReference + "].");

                        }

                        Public.Reporting.IReportingServer reportingServerObject = (Public.Reporting.IReportingServer)Activator.CreateInstance (reportingServerType);

                        Dictionary<String, String> reportParameters = new Dictionary<String, String> ();

                        reportParameters.Add ("entityCorrespondenceId", id.ToString ());

                        
                        // SSRS RENDER TIFF, CONVERT TO XPS

                        Public.ImageStream imageStream = reportingServerObject.Render (reportingServer.WebServiceHostConfiguration, currentCorrespondenceContent.ReportName, reportParameters, "image", reportingServer.ExtendedProperties);

                        xpsContent = imageStream.TiffToXps ();

                        xpsContents.Add (xpsContent);

                        #endregion 

                        break;

                    case Reference.Enumerations.CorrespondenceContentType.Attachment:

                        #region Load Attachment

                        contentUriName = ("memorystream://attachment" + Guid.NewGuid ().ToString ().Replace ("-", "") + ".xps");

                        contentUri = new Uri (contentUriName, UriKind.Absolute);


                        System.IO.MemoryStream attachmentStream = new System.IO.MemoryStream ();

                        System.IO.Packaging.Package attachmentPackage = System.IO.Packaging.Package.Open (currentCorrespondenceContent.Attachment);

                        System.IO.Packaging.PackageStore.AddPackage (contentUri, attachmentPackage);

                        xpsContent = new System.Windows.Xps.Packaging.XpsDocument (attachmentPackage, System.IO.Packaging.CompressionOption.Normal, contentUriName);

                        
                        xpsContents.Add (xpsContent);

                        #endregion 

                        break;
                
                }

            }

            #region Merge XPS Contents

            foreach (System.Windows.Xps.Packaging.XpsDocument currentContentDocument in xpsContents) {

                foreach (System.Windows.Xps.Packaging.IXpsFixedDocumentReader currentContentDocumentReader in currentContentDocument.FixedDocumentSequenceReader.FixedDocuments) {

                    System.Windows.Xps.Packaging.IXpsFixedDocumentWriter contentDocumentWriter = contentSequenceWriter.AddFixedDocument ();

                    foreach (System.Windows.Xps.Packaging.IXpsFixedPageReader currentContentPageReader in currentContentDocumentReader.FixedPages) {

                        System.Windows.Xps.Packaging.IXpsFixedPageWriter contentPageWriter = contentDocumentWriter.AddFixedPage ();

                        System.Xml.XmlWriter xmlPageWriter = contentPageWriter.XmlWriter;


                        String pageContent = CommonFunctions.XmlReaderToString (currentContentPageReader.XmlReader);


                        #region Resource Dictionaries

                        foreach (System.Windows.Xps.Packaging.XpsResourceDictionary currentXpsDictionary in currentContentPageReader.ResourceDictionaries) {

                            System.Windows.Xps.Packaging.XpsResourceDictionary xpsDictionary = contentPageWriter.AddResourceDictionary ();

                            System.IO.Stream xpsDictionaryStream = xpsDictionary.GetStream (); // GET DESTINATION STREAM TO COPY TO

                            currentXpsDictionary.GetStream ().CopyTo (xpsDictionaryStream);

                            
                            // REMAP SOURCE URI

                            pageContent = pageContent.Replace (currentXpsDictionary.Uri.ToString (), xpsDictionary.Uri.ToString ());

                            xpsDictionary.Commit ();

                        }

                        #endregion 
                        

                        #region Color Contexts

                        foreach (System.Windows.Xps.Packaging.XpsColorContext currentXpsColorContext in currentContentPageReader.ColorContexts) {

                            System.Windows.Xps.Packaging.XpsColorContext xpsColorContext = contentPageWriter.AddColorContext ();

                            System.IO.Stream xpsColorContextStream = xpsColorContext.GetStream (); // GET DESTINATION STREAM TO COPY TO

                            currentXpsColorContext.GetStream ().CopyTo (xpsColorContextStream);


                            // REMAP SOURCE URI

                            pageContent = pageContent.Replace (currentXpsColorContext.Uri.ToString (), xpsColorContext.Uri.ToString ());

                            xpsColorContext.Commit ();

                        }

                        #endregion 
                       

                        #region Fonts

                        foreach (System.Windows.Xps.Packaging.XpsFont currentXpsFont in currentContentPageReader.Fonts) {

                            System.Windows.Xps.Packaging.XpsFont xpsFont = contentPageWriter.AddFont (false);

                            xpsFont.IsRestricted = false; 

                            System.IO.MemoryStream deobfuscatedStream = new System.IO.MemoryStream ();

                            currentXpsFont.GetStream ().CopyTo (deobfuscatedStream);


                            String fontResourceName = currentXpsFont.Uri.ToString ();

                            fontResourceName = fontResourceName.Split ('/')[fontResourceName.Split ('/').Length - 1];

                            if (fontResourceName.Contains (".odttf")) {

                                fontResourceName = fontResourceName.Replace (".odttf", String.Empty);

                                Guid fontGuid = new Guid (fontResourceName);

                                deobfuscatedStream = CommonFunctions.XmlFontDeobfuscate (currentXpsFont.GetStream (), fontGuid);

                            }


                            System.IO.Stream xpsFontStream = xpsFont.GetStream (); // GET DESTINATION STREAM TO COPY TO

                            deobfuscatedStream.CopyTo (xpsFontStream);


                            // REMAP SOURCE URI

                            pageContent = pageContent.Replace (currentXpsFont.Uri.ToString (), xpsFont.Uri.ToString ());

                            xpsFont.Commit ();

                        }

                        #endregion 


                        #region Images

                        foreach (System.Windows.Xps.Packaging.XpsImage currentXpsImage in currentContentPageReader.Images) {

                            // FILE EXTENSION TO DETERMINE IMAGE TYPE

                            System.Windows.Xps.Packaging.XpsImageType imageType = System.Windows.Xps.Packaging.XpsImageType.TiffImageType;

                            String fileExtension = currentXpsImage.Uri.ToString ().Split ('.')[1];

                            switch (fileExtension.ToLower ()) { 

                                case "jpeg": case "jpg": imageType = System.Windows.Xps.Packaging.XpsImageType.JpegImageType; break;

                                case "png": imageType = System.Windows.Xps.Packaging.XpsImageType.PngImageType; break;

                                case "wdp": imageType = System.Windows.Xps.Packaging.XpsImageType.WdpImageType; break;

                                case "tif": case "tiff": default: imageType = System.Windows.Xps.Packaging.XpsImageType.TiffImageType; break;

                            }

                            System.Windows.Xps.Packaging.XpsImage xpsImage = contentPageWriter.AddImage (imageType);
                                                        
                            System.IO.Stream xpsImageStream = xpsImage.GetStream (); // GET DESTINATION STREAM TO COPY TO

                            currentXpsImage.GetStream ().CopyTo (xpsImageStream);

                            
                            // REMAP SOURCE URI

                            pageContent = pageContent.Replace (currentXpsImage.Uri.ToString (), xpsImage.Uri.ToString ());

                            // xpsImage.Uri = currentXpsImage.Uri;


                            xpsImage.Commit ();

                        }

                        #endregion 
           

                        // COPY XAML CONTENT 

                        xmlPageWriter.WriteRaw (pageContent);


                        contentPageWriter.Commit ();

                    }

                    contentDocumentWriter.Commit ();

                }

            }

            #endregion 

            
            contentSequenceWriter.Commit ();

            renderedXpsDocument.Close ();

            xpsPackage.Close ();


            renderedImage.Image = renderedContent;

            renderedImage.Name = "EntityCorrespondence" + id.ToString () + ".xps";

            renderedImage.Extension = "xps";

            renderedImage.MimeType = "application/vnd.ms-xpsdocument";

            renderedImage.IsCompressed = false;

            return renderedImage;

        }

        private Public.ImageStream RenderPdf () {

            Public.ImageStream renderedImage = new Public.ImageStream ();

            System.IO.MemoryStream renderedContent = new System.IO.MemoryStream ();


            PdfSharp.Pdf.PdfDocument renderedPdf = new PdfSharp.Pdf.PdfDocument ();


            // LOAD CORRESPONDENCE RECORD TO SEE IF THERE IS ANY CONTENT AVAILABLE

            Reference.Correspondence renderCorrespondence = application.CorrespondenceGet (correspondenceId);

            if (renderCorrespondence == null) { throw new ApplicationException ("Unable to load base Correspondence to Render Content."); }

            if (renderCorrespondence.Content.Count == 0) { throw new ApplicationException ("No Content to Render for Correspondence."); }

            renderCorrespondence.LoadContentAttachments ();


            foreach (Reference.CorrespondenceContent currentCorrespondenceContent in renderCorrespondence.Content.Values) {

                PdfSharp.Pdf.PdfDocument pdfContent = null;


                switch (currentCorrespondenceContent.ContentType) {

                    case Reference.Enumerations.CorrespondenceContentType.Report:

                        #region Generate Report Content

                        Public.ImageStream reportStream = RenderContentReport (currentCorrespondenceContent, "pdf");

                        pdfContent = PdfSharp.Pdf.IO.PdfReader.Open (reportStream.Image, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);

                        #endregion

                        break;

                    case Reference.Enumerations.CorrespondenceContentType.Attachment:

                        #region Load Attachment

                        pdfContent = PdfSharp.Pdf.IO.PdfReader.Open (currentCorrespondenceContent.Attachment, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);
                        
                        #endregion

                        break;

                }

                if (pdfContent != null) {

                    foreach (PdfSharp.Pdf.PdfPage currentPage in pdfContent.Pages) {

                        renderedPdf.Pages.Add (currentPage);

                    }

                }
                
            }            

            
            
            renderedPdf.Save (renderedContent, false);



            renderedImage.Image = renderedContent;

            renderedImage.Name = "EntityCorrespondence" + id.ToString () + ".pdf";

            renderedImage.Extension = "pdf";

            renderedImage.MimeType = "application/pdf";

            renderedImage.IsCompressed = false;

            return renderedImage;

        }

        public Public.ImageStream Render () {

            Public.ImageStream renderedImage = RenderPdf ();


            if (Correspondence.StoreImage) {

                // SAVE IMAGE TO DATABASE

                application.EnvironmentDatabase.BeginTransaction ();

                String insertStatement = "IF NOT EXISTS (SELECT EntityCorrespondenceId FROM EntityCorrespondenceImage WHERE EntityCorrespondenceId = " + id.ToString () + ")";

                insertStatement += "  INSERT INTO dbo.EntityCorrespondenceImage (EntityCorrespondenceId, EntityCorrespondenceImageData, ";

                insertStatement += "    EntityCorrespondenceImageName, EntityCorrespondenceImageExtension, EntityCorrespondenceImageMimeType, EntityCorrespondenceImageIsCompressed)";

                insertStatement += " VALUES (" + id.ToString () + ", NULL, ";

                insertStatement += "'" + renderedImage.Name.Replace ("'", "''") + "', ";

                insertStatement += "'" + renderedImage.Extension.Replace ("'", "''") + "', ";

                insertStatement += "'" + renderedImage.MimeType.Replace ("'", "''") + "', ";

                insertStatement += Convert.ToInt32 (renderedImage.IsCompressed).ToString () + ")";

                application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement);

                application.EnvironmentDatabase.BlobWrite ("dbo.EntityCorrespondenceImage", "EntityCorrespondenceImageData", "EntityCorrespondenceId = " + id.ToString (), renderedImage.Image);

                application.EnvironmentDatabase.CommitTransaction ();

            }


            return renderedImage;

        }


        public Boolean Fax (Faxing.FaxServer faxServer) {

            if (faxServer == null) { return false; }

            Boolean sentSuccess = true;


            // SET AUTOMATION INFORMATION

            automationId = Guid.NewGuid ();

            automationStatus = Automation.Enumerations.AutomationStatus.Open;

            automationDate = DateTime.Now;

            automationException = String.Empty;



            try {

                // SET UP FAX DOCUMENT

                Public.Faxing.IFaxServer faxServerObject;

                String faxAttention = ((!String.IsNullOrWhiteSpace (attention)) ? attention : application.EntityGet (entityId).Name);

                Public.Faxing.FaxSender faxSender = new Public.Faxing.FaxSender (application.Session.UserDisplayName, String.Empty, faxServer.SenderEmailAddress);

                faxSender.Email = faxSender.Email.Replace ("%useraccountname%", application.Session.UserAccountName);

                Public.Faxing.FaxRecipient faxRecipient = new Public.Faxing.FaxRecipient (faxAttention, this.ContactFaxNumber);

                Public.Faxing.FaxDocument faxDocument;


                Public.ImageStream attachment = application.EntityCorrespondenceImageGet (id);

                if ((attachment.Image != null) ? (attachment.Image.Length == 0) : true) {

                    attachment = Render ();

                }

                faxDocument = new Public.Faxing.FaxDocument (automationId.ToString (), correspondenceName, attachment);


                System.Reflection.Assembly faxServerAssembly = System.Reflection.Assembly.LoadFrom (faxServer.AssemblyReference);

                Type faxServerType = faxServerAssembly.GetType (faxServer.AssemblyClassName);

                if (faxServerType == null) {

                    throw new ApplicationException ("Unable to find Class [" + faxServer.AssemblyClassName + "] in referenced Assembly [" + faxServer.AssemblyReference + "].");

                }

                faxServerObject = (Public.Faxing.IFaxServer)Activator.CreateInstance (faxServerType);

                faxServerObject.OnFaxCompleted += new EventHandler<Public.Faxing.FaxCompletedEventArgs> (Fax_OnFaxCompleted);

                faxServerObject.Fax (faxServer.FaxServerConfiguration, faxSender, faxRecipient, faxDocument, faxServer.ExtendedProperties);

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

                AutomationStatus = Automation.Enumerations.AutomationStatus.Critical;

                AutomationException = applicationException.Message;

                sentSuccess = false;

            }

            if (sentSuccess) { sentSuccess = Save (); }

            return sentSuccess;

        }

        public Boolean Fax (Int64 faxServerId) { return Fax (application.FaxServerGet (faxServerId)); }

        public Boolean Fax (String faxServerName) { return Fax (application.FaxServerGet (faxServerName)); }

        public void Fax_OnFaxCompleted (Object sender, Public.Faxing.FaxCompletedEventArgs e) {

            // RELOAD ENTITY CORRESPONDENCE TO ENSURE THAT WE DON'T MISS ANY CHANGES

            EntityCorrespondence entityCorrespondence = application.EntityCorrespondenceGet (id);

            entityCorrespondence.SentDate = DateTime.Now;

            entityCorrespondence.AutomationStatus = ((e.Successful) ? Automation.Enumerations.AutomationStatus.Completed : Automation.Enumerations.AutomationStatus.Critical);

            entityCorrespondence.AutomationException = (e.ExceptionMessage);

            entityCorrespondence.Save ();

            return;

        }

        #endregion 

    }

}
