using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationEvents {

    [DataContract (Name = "PopulationServiceEventThreshold")]
    public class PopulationServiceEventThreshold : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationServiceEventId")]
        private Int64 populationServiceEventId;

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "RelativeDateValue")]
        private Int32 relativeDateValue;

        [DataMember (Name = "RelativeDateQualifier")]
        private Core.Enumerations.DateQualifier relativeDateQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "Status")]
        private Enumerations.PopulationServiceEventStatus status = Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus.CompliantOrNoChange;

        [DataMember (Name = "Action")]
        private Core.Action.Action action;

        #endregion


        #region Public Properties

        public Int64 PopulationServiceEventId { get { return populationServiceEventId; } set { populationServiceEventId = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int32 RelativeDateValue { get { return relativeDateValue; } set { relativeDateValue = value; } }

        public Core.Enumerations.DateQualifier RelativeDateQualifier { get { return relativeDateQualifier; } set { relativeDateQualifier = value; } }

        public Enumerations.PopulationServiceEventStatus Status { get { return status; } set { status = value; } }

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
        
        protected void ObjectConstructor (Application applicationReference) {

            BaseConstructor (applicationReference);

            action = new Mercury.Server.Core.Action.Action (applicationReference);

            return;

        }

        public PopulationServiceEventThreshold (Application applicationReference) {

            ObjectConstructor (applicationReference);

            return; 
        
        }

        public PopulationServiceEventThreshold (Application applicationReference, Int64 forServiceEventThresholdId) {

            ObjectConstructor (applicationReference);

            BaseConstructor (applicationReference, forServiceEventThresholdId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RelativeDateValue", RelativeDateValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RelativeDateQualifierInt32", ((Int32)RelativeDateQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RelativeDateQualifier", RelativeDateQualifier.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "StatusInt32", ((Int32)Status).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Status", Status.ToString ());

            #endregion


            #region Object Nodes

            if (Action != null) { document.LastChild.AppendChild (document.ImportNode (Action.XmlSerialize ().LastChild, true)); }

            #endregion


            return document;

        }

        #endregion

  
        //public override List<Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = "ServiceEventThreshold";

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "ServiceEventThreshold") {

        //        try {

        //            #region Service Event Threshold Properties

        //            foreach (System.Xml.XmlNode currentProperty in objectNode.ChildNodes[0]) {

        //                switch (currentProperty.Attributes["Name"].InnerText) {

        //                    case "RelativeDateValue": relativeDateValue = Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "RelativeDateQualifier": relativeDateQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "Status": status = (Mercury.Server.Core.Population.Enumerations.ServiceEventStatus) Convert.ToInt32 (currentProperty.InnerText); break;

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

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Service Event Threshold.")); }


        //    response.Add (importResponse);

        //    return response;

        //}

        //#endregion


        #region Database Functions
        
        override public Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;

            try {

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.PopulationServiceEventThreshold_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (populationServiceEventId.ToString () + ", ");

                sqlStatement.Append (populationId.ToString () + ", ");

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

                SetIdentity ();

            }

            catch (Exception applicationException) {

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            populationServiceEventId = (Int64) currentRow["PopulationServiceEventId"];

            populationId = (Int64) currentRow["PopulationId"];

            relativeDateValue = (Int32) currentRow["RelativeDateValue"];

            relativeDateQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow["RelativeDateQualifier"];

            status = (Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus) (Int32) currentRow["Status"];

            action = new Mercury.Server.Core.Action.Action (base.application);

            action.MapDataFields (String.Empty, currentRow);

            return;

        }

        #endregion

    }

}
