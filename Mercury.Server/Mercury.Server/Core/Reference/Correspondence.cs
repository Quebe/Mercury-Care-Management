using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Reference {

    [DataContract (Name = "Correspondence")]
    public class Correspondence : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "Version")]
        private Double version = 1.0;

        [DataMember (Name = "FormId")]
        private Int64 formId;

        [DataMember (Name = "StoreImage")]
        private Boolean storeImage = false;

        [DataMember (Name = "Content")]
        private Dictionary<Int32, Reference.CorrespondenceContent> content = null;
        
        #endregion


        #region Public Properties

        public Double Version { get { return version; } set { version = value; } }

        public Int64 FormId { get { return formId; } set { formId = value; } }

        public Boolean StoreImage { get { return storeImage; } set { storeImage = value; } }

        public Dictionary<Int32, Reference.CorrespondenceContent> Content { get { return content; } set { content = value; } }


        public override Application Application {

            set {

                base.Application = value;

                foreach (CorrespondenceContent currentContent in content.Values) {

                    currentContent.Application = value;

                }

            }

        }

        #endregion 
        

        #region Constructors

        public Correspondence (Application applicationReference) { base.BaseConstructor (applicationReference);  return; }

        public Correspondence (Application applicationReference, Int64 forCorrespondenceId) {

            base.BaseConstructor (applicationReference, forCorrespondenceId);

            return;

        }

        #endregion


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Version", Version.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "FormId", FormId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "FormName", application.CoreObjectGetNameById ("Form", formId));

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "StoreImage", StoreImage.ToString ());

            #endregion


            #region Contents

            System.Xml.XmlNode componentsNode = document.CreateElement ("Contents");

            document.LastChild.AppendChild (componentsNode);

            foreach (Int32 currentContentIndex in content.Keys) { 

                componentsNode.AppendChild (document.ImportNode (content [currentContentIndex].XmlSerialize ().LastChild, true));

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


                                    case "Version": Version = Convert.ToDouble (currentPropertyNode.InnerText); break;

                                    // USE NAME TO PERFORM MATCHING AND NOT ID

                                    case "FormName": FormId = application.CoreObjectGetIdByName ("Form", currentPropertyNode.InnerText); break;

                                    case "StoreImage": StoreImage = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                                }

                            }

                            break;

                        case "Contents":

                            Int32 contentIndex = 0;

                            if (content == null) { content = new Dictionary<int, CorrespondenceContent> (); }

                            foreach (System.Xml.XmlNode currentContentNode in currentNode.ChildNodes) {

                                CorrespondenceContent loadedContent = new CorrespondenceContent (application);

                                response.AddRange (loadedContent.XmlImport (currentContentNode));

                                content.Add (contentIndex, loadedContent);

                                contentIndex = contentIndex + 1;

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVE IMPORTED CLASS

                if (!Save ()) { throw new ApplicationException ("Unable to save Correspondence: " + Name + "."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 


        #region Data Functions
        
        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            version = Convert.ToDouble (currentRow["Version"]);

            formId = (Int64) currentRow["FormId"];


            storeImage = Convert.ToBoolean (currentRow["StoreImage"]);


            #region Load Correspondence Content (without Attachment Images)

            String selectStatement = "SELECT ";
            
            selectStatement += "    CorrespondenceContentId, CorrespondenceId, ContentSequence, ContentType, ReportingServerId, CorrespondenceContentName, CorrespondenceContentPath, IsAttachmentCompressed,";
            
            selectStatement += "    CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, ";

            selectStatement += "    ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate";
            
            selectStatement += "  FROM dbo.CorrespondenceContent";

            selectStatement += "  WHERE CorrespondenceId = " + id.ToString ();

            selectStatement += "  ORDER BY ContentSequence, ContentType";


            content = new Dictionary<Int32,CorrespondenceContent> ();

            Int32 contentIndex = 0;

            System.Data.DataTable contentTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

            foreach (System.Data.DataRow currentContentRow in contentTable.Rows) {

                contentIndex ++;

                CorrespondenceContent correspondenceContent = new CorrespondenceContent (application);

                correspondenceContent.MapDataFields (currentContentRow);

                correspondenceContent.CorrespondenceId = id;

                correspondenceContent.ContentSequence = contentIndex;

                content.Add (contentIndex, correspondenceContent);

            }

            #endregion

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();



            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CorrespondenceManage)) { throw new ApplicationException ("PermissionDenied"); }



            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC Correspondence_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (version.ToString () + ", ");

                sqlStatement.Append (formId.ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (storeImage).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");
                

                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();


                if (success) {

                    success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM dbo.CorrespondenceContent WHERE CorrespondenceId = " + Id.ToString ());

                    if (success) {

                        if (content == null) { content = new Dictionary<Int32, CorrespondenceContent> (); }

                        Int32 contentIndex = 0;

                        foreach (CorrespondenceContent currentContent in content.Values) {

                            contentIndex = contentIndex + 1;

                            currentContent.Application = application;

                            currentContent.CorrespondenceId = Id;

                            currentContent.ContentSequence = contentIndex;

                            success = currentContent.Save ();

                            if (!success) { throw new ApplicationException ("Unable to save Correspondence Content."); }

                        }

                    }

                }


                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion 

        
        #region Public Methods

        public void LoadContentAttachments () {

            foreach (CorrespondenceContent currentContent in content.Values) {

                CorrespondenceContent loadedContent = application.CorrespondenceContentGet (currentContent.Id);

                if (loadedContent != null) { currentContent.AttachmentBase64 = loadedContent.AttachmentBase64; }

            }

            return;

        }

        #endregion 

    }
}
