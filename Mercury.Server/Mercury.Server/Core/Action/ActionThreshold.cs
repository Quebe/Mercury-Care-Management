using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Action {

    [Serializable]
    [DataContract (Name = "ActionThreshold")]
    public class ActionThreshold : CoreObject {

        #region Private Properties
        
        [DataMember (Name = "OwnerObjectId")]
        private Int64 ownerObjectId;

        [DataMember (Name = "ThresholdType")]
        private Enumerations.ActionThresholdType thresholdType = Mercury.Server.Core.Action.Enumerations.ActionThresholdType.NotSpecified;
        
        [DataMember (Name = "RelativeDateValue")]
        private Int32 relativeDateValue;

        [DataMember (Name = "RelativeDateQualifier")]
        private Core.Enumerations.DateQualifier relativeDateQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "Status")]
        private Enumerations.ActionThresholdStatus status = Enumerations.ActionThresholdStatus.CompliantOrNoChange;

        [DataMember (Name = "Action")]
        private Core.Action.Action action;

        #endregion


        #region Public Properties

        public Int64 OwnerObjectId { get { return ownerObjectId; } set { ownerObjectId = value; } }


        public Enumerations.ActionThresholdType ThresholdType { get { return thresholdType; } set { thresholdType = value; } }

        public Int32 RelativeDateValue { get { return relativeDateValue; } set { relativeDateValue = value; } }

        public Core.Enumerations.DateQualifier RelativeDateQualifier { get { return relativeDateQualifier; } set { relativeDateQualifier = value; } }

        public Enumerations.ActionThresholdStatus Status { get { return status; } set { status = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }

        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                action.Application = value;

            }

        }

        #endregion

        
        #region Constuctors 
        
        protected void ObjectConstructor (Application application, Int64 forOwnerId, Enumerations.ActionThresholdType forThresholdType) {

            BaseConstructor (application);

            ownerObjectId = forOwnerId;

            thresholdType = forThresholdType;

            action = new Mercury.Server.Core.Action.Action (application);

            return;

        }

        public ActionThreshold (Application application, Int64 forOwnerId, Enumerations.ActionThresholdType forThresholdType) {

            ObjectConstructor (application, forOwnerId, forThresholdType);

            return; 
        
        }

        public ActionThreshold (Application application, Int64 forThresholdId, Int64 forOwnerId, Enumerations.ActionThresholdType forThresholdType) {

            ObjectConstructor (application, forOwnerId, forThresholdType);

            if (!Load (application, forThresholdId)) {

                throw new ApplicationException ("Unable to load Threshold from the database for " + forThresholdId.ToString () + ".");

            }

            return;

        }

        #endregion


        #region XML Serialization

        //public override System.Xml.XmlDocument XmlSerialize () {

        //    if (thresholdType == Mercury.Server.Core.Action.Enumerations.ActionThresholdType.NotSpecified) { throw new ApplicationException ("Invalid Threshold Type Specified."); }


        //    System.Xml.XmlDocument thresholdDocument = base.XmlSerialize ();
          

        //    System.Xml.XmlElement thresholdNode = thresholdDocument.CreateElement (thresholdType.ToString () + "Threshold");

        //    System.Xml.XmlElement propertiesNode;


        //    thresholdDocument.AppendChild (thresholdNode);

        //    thresholdNode.SetAttribute ("ThresholdId", Id.ToString ());

        //    propertiesNode = thresholdDocument.CreateElement ("Properties");

        //    thresholdNode.AppendChild (propertiesNode);


        //    #region Properties

        //    CommonFunctions.XmlDocumentAppendPropertyNode (thresholdDocument, propertiesNode, "ThresholdId", thresholdId.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (thresholdDocument, propertiesNode, "ThresholdType", thresholdType.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (thresholdDocument, propertiesNode, "ThresholdTypeInt32", ((Int32) thresholdType).ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (thresholdDocument, propertiesNode, "RelativeDateValue", relativeDateValue.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (thresholdDocument, propertiesNode, "RelativeDateQualifier", ((Int32) relativeDateQualifier).ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (thresholdDocument, propertiesNode, "Status", ((Int32) status).ToString ());

        //    #endregion


        //    System.Xml.XmlElement actionPropertyNode = thresholdDocument.CreateElement ("Property");

        //    actionPropertyNode.SetAttribute ("Name", "Action");

        //    if (action != null) {

        //        System.Xml.XmlDocument actionDocument = action.XmlSerialize ();

        //        actionPropertyNode.AppendChild (thresholdDocument.ImportNode (actionDocument.ChildNodes[1], true));

        //    }

        //    propertiesNode.AppendChild (actionPropertyNode);


        //    return thresholdDocument;

        //}

        //public override List<Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = "Threshold";

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType.EndsWith ("Threshold")) {

        //        try {

        //            #region Service Event Threshold Properties

        //            foreach (System.Xml.XmlNode currentProperty in objectNode.ChildNodes[0]) {

        //                switch (currentProperty.Attributes["Name"].InnerText) {

        //                    case "ThresholdTypeInt32": thresholdType = (Mercury.Server.Core.Action.Enumerations.ActionThresholdType) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "RelativeDateValue": relativeDateValue = Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "RelativeDateQualifier": relativeDateQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "Status": status = (Enumerations.ActionThresholdStatus) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "Action":

        //                        System.Xml.XmlNode actionNode = currentProperty.ChildNodes[0];

        //                        Int32 actionId = Convert.ToInt32 (actionNode.Attributes["ActionId"].InnerText);

        //                        String actionName = actionNode.Attributes["Name"].InnerText;

        //                        if (actionId != 0) {

        //                            action = new Mercury.Server.Core.Action.Action (base.application, actionId, actionName);

        //                            response.AddRange (action.XmlImport (actionNode));

        //                        }

        //                        break;

        //                }

        //            }

        //            #endregion


        //            if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }


        //        }

        //        catch (Exception importException) {

        //            importResponse.SetException (importException);

        //        }


        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Threshold.")); }


        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion


        #region Database Functions

        public override Boolean Load (Int64 forId) {

            if (thresholdType == Mercury.Server.Core.Action.Enumerations.ActionThresholdType.NotSpecified) { throw new ApplicationException ("Invalid Threshold Type Specified."); }


            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable thresholdTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT * FROM dbo." + thresholdType.ToString () + "Threshold WHERE " + thresholdType.ToString () + "ThresholdId = " + forId.ToString ());

            thresholdTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (thresholdTable.Rows.Count == 1) {

                MapDataFields (thresholdTable.Rows[0]);

                success = true;

            }

            else { success = false; }

            return success;

        }

        override public Boolean Save () {

            if (thresholdType == Mercury.Server.Core.Action.Enumerations.ActionThresholdType.NotSpecified) { throw new ApplicationException ("Invalid Threshold Type Specified."); }


            Boolean success = false;

            StringBuilder sqlStatement;

            try {

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo." + thresholdType.ToString () + "Threshold_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (ownerObjectId.ToString () + ", ");


                sqlStatement.Append (relativeDateValue.ToString () + ", ");

                sqlStatement.Append (((Int32) relativeDateQualifier).ToString () + ", ");

                sqlStatement.Append (((Int32) status).ToString () + ", ");


                sqlStatement.Append (action.Id.ToString () + ", ");

                sqlStatement.Append ("'" + action.ActionParametersXmlSqlParsedString + "', ");

                sqlStatement.Append ("'" + action.Description + "'");


                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    base.application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw base.application.EnvironmentDatabase.LastException;

                }

                if (id == 0) {

                    Object identity = base.application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (identity != null) {

                        id = Int64.Parse (identity.ToString ());

                    }

                }

            }

            catch (Exception applicationException) {

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            if (thresholdType == Mercury.Server.Core.Action.Enumerations.ActionThresholdType.NotSpecified) { throw new ApplicationException ("Invalid Threshold Type Specified."); }

            base.MapDataFields (currentRow);


            id = (Int64) currentRow[thresholdType.ToString () + "ThresholdId"];

            relativeDateValue = (Int32) currentRow["RelativeDateValue"];

            relativeDateQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow["RelativeDateQualifier"];

            status = (Enumerations.ActionThresholdStatus) (Int32) currentRow["Status"];

            action = new Mercury.Server.Core.Action.Action (base.application);

            action.MapDataFields (String.Empty, currentRow);

            return;

        }

        #endregion

    }

}
