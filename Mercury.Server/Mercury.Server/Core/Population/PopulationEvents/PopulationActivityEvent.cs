using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationEvents {

    [DataContract (Name = "PopulationActivityEvent")]
    public class PopulationActivityEvent : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "ScheduleType")]
        private Enumerations.PopulationActivityScheduleType scheduleType = Mercury.Server.Core.Population.Enumerations.PopulationActivityScheduleType.ByFrequency;

        [DataMember (Name = "ScheduleValue")]
        private Int32 scheduleValue;

        [DataMember (Name = "ScheduleQualifier")]
        private Core.Enumerations.DateQualifier scheduleQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "AnchorDate")]
        private Enumerations.PopulationActivityEventAnchorDate anchorDate = Mercury.Server.Core.Population.Enumerations.PopulationActivityEventAnchorDate.PopulationEffectiveDate;

        [DataMember (Name = "Reoccurring")]
        private Boolean reoccurring = false;

        [DataMember (Name = "PerformActionDateType")]
        private Enumerations.PopulationActivityPerformActionDateType performActionDateType = Mercury.Server.Core.Population.Enumerations.PopulationActivityPerformActionDateType.Immediately;

        [DataMember (Name = "Action")]
        private Core.Action.Action action;


        private Population population = null;

        #endregion


        #region Public Properties

        public override String Description {

            get {

                String description = String.Empty;

                switch (scheduleType) {

                    case Enumerations.PopulationActivityScheduleType.BirthMonth:

                    case Enumerations.PopulationActivityScheduleType.Monthly:

                    case Enumerations.PopulationActivityScheduleType.Quarterly:

                        description = "On " + scheduleType.ToString ();

                        break;

                    case Enumerations.PopulationActivityScheduleType.ByFrequency:

                        description = scheduleValue.ToString () + " " + scheduleQualifier.ToString ();

                        break;

                    case Enumerations.PopulationActivityScheduleType.CalendarMonth:

                        description = "Calendar Month of " + scheduleValue.ToString ();

                        break;

                }

                if (reoccurring) { description = description + " Reoccurring"; }

                description = description + " take action on " + performActionDateType.ToString ();

                description = description + " staring from " + Server.CommonFunctions.EnumerationToString (anchorDate);


                return description;

            }

        }


        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Enumerations.PopulationActivityScheduleType ScheduleType { get { return scheduleType; } set { scheduleType = value; } }

        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Core.Enumerations.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public Enumerations.PopulationActivityEventAnchorDate AnchorDate { get { return anchorDate; } set { anchorDate = value; } }

        public Boolean Reoccurring { get { return reoccurring; } set { reoccurring = value; } }

        public Enumerations.PopulationActivityPerformActionDateType PerformActionDateType { get { return performActionDateType; } set { performActionDateType = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }

        public Population Population { get { return population; } set { population = value; } }

        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                action.Application = value;

            }

        }

        #endregion
        

        #region Constructors

        protected void ObjectConstructor (Application applicationReference) {

            BaseConstructor (applicationReference);

            action = new Mercury.Server.Core.Action.Action (applicationReference);

            return;

        }

        public PopulationActivityEvent (Application applicationReference) {

            ObjectConstructor (applicationReference);
            
            return;  
        
        }

        public PopulationActivityEvent (Application applicationReference, Int64 forActivityEventId) {

            ObjectConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forActivityEventId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleTypeInt32", ((Int32)ScheduleType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleType", ScheduleType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleValue", ScheduleValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleQualifierInt32", ((Int32)ScheduleQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleQualifier", ScheduleQualifier.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AnchorDateInt32", ((Int32)AnchorDate).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AnchorDate", AnchorDate.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Reoccurring", Reoccurring.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PerformActionDateTypeInt32", ((Int32)PerformActionDateType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PerformActionDateType", PerformActionDateType.ToString ());

            #endregion


            #region Object Nodes

            if (Action != null) { document.LastChild.AppendChild (document.ImportNode (Action.XmlSerialize ().LastChild, true)); }

            #endregion

           
            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":

                            #region Properties

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].Value) {

                                    case "ScheduleTypeInt32": ScheduleType = (Enumerations.PopulationActivityScheduleType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ScheduleValue": ScheduleValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ScheduleQualifierInt32": ScheduleQualifier = (Core.Enumerations.DateQualifier) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "AnchorDateInt32": AnchorDate = (Enumerations.PopulationActivityEventAnchorDate) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "Reoccurring": Reoccurring = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                                    case "PerformActionDateTypeInt32": PerformActionDateType = (Enumerations.PopulationActivityPerformActionDateType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                }

                            }

                            #endregion

                            break;

                        case "Action":

                            action = new Action.Action (application);

                            response.AddRange (action.XmlImport (currentNode));
                            
                            break;

                    } // switch (currentNode.Name) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                // SAVE IS PERFORMED BY PARENT OBJECT

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        //#region XML Serialization

        //public override System.Xml.XmlDocument XmlSerialize () {

        //    System.Xml.XmlDocument eventDocument = new System.Xml.XmlDocument ();

        //    System.Xml.XmlDeclaration xmlDeclaration = eventDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

        //    eventDocument.InsertBefore (xmlDeclaration, eventDocument.DocumentElement);


        //    System.Xml.XmlElement eventNode = eventDocument.CreateElement ("ActivityEvent");

        //    System.Xml.XmlElement propertiesNode;


        //    eventDocument.AppendChild (eventNode);

        //    eventNode.SetAttribute ("ActivityEventId", activityEventId.ToString ());

        //    propertiesNode = eventDocument.CreateElement ("Properties");

        //    eventNode.AppendChild (propertiesNode);


        //    #region Action Properties

        //    CommonFunctions.XmlDocumentAppendPropertyNode (eventDocument, propertiesNode, "ActivityEventId", activityEventId.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (eventDocument, propertiesNode, "ScheduleType", ((Int32) scheduleType).ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (eventDocument, propertiesNode, "ScheduleValue", scheduleValue.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (eventDocument, propertiesNode, "ScheduleQualifier", ((Int32) scheduleQualifier).ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (eventDocument, propertiesNode, "AnchorDate", ((Int32) anchorDate).ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (eventDocument, propertiesNode, "Reoccurring", reoccurring.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (eventDocument, propertiesNode, "PerformActionDateType", ((Int32) performActionDateType).ToString ());

        //    #endregion


        //    System.Xml.XmlElement actionPropertyNode = eventDocument.CreateElement ("Property");

        //    actionPropertyNode.SetAttribute ("Name", "Action");

        //    if (action != null) {

        //        System.Xml.XmlDocument actionDocument = action.XmlSerialize ();

        //        actionPropertyNode.AppendChild (eventDocument.ImportNode (actionDocument.ChildNodes[1], true));

        //    }

        //    propertiesNode.AppendChild (actionPropertyNode);


        //    return eventDocument;

        //}

        //public override List<Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = "ActivityEvent";

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "ActivityEvent") {

        //        try {

        //            #region Activity Properties

        //            foreach (System.Xml.XmlNode currentProperty in objectNode.ChildNodes[0]) {

        //                String propertyName = currentProperty.Attributes["Name"].InnerText;

        //                switch (propertyName) {

        //                    case "ScheduleType": scheduleType = (Mercury.Server.Core.Population.Enumerations.ActivityScheduleType) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "ScheduleValue": scheduleValue = Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "ScheduleQualifier": scheduleQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "AnchorDate": anchorDate = (Mercury.Server.Core.Population.Enumerations.PopulationActivityEventAnchorDate) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "Reoccurring": reoccurring = Convert.ToBoolean (currentProperty.InnerText); break;

        //                    case "PerformActionDateType": performActionDateType = (Mercury.Server.Core.Population.Enumerations.ActivityPerformActionDateType) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "Action":

        //                        if (currentProperty.ChildNodes.Count > 0) {

        //                            System.Xml.XmlNode actionNode = currentProperty.ChildNodes[0];

        //                            Int32 actionId = Convert.ToInt32 (actionNode.Attributes["ActionId"].InnerText);

        //                            String actionName = actionNode.Attributes["Name"].InnerText;

        //                            if (actionId != 0) {

        //                                action = new Mercury.Server.Core.Action.Action (base.application, actionId, actionName);

        //                                response.AddRange (action.XmlImport (actionNode));

        //                            }

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

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Population.")); }


        //    response.Add (importResponse);

        //    return response;

        //}

        //#endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            if ((scheduleType == Mercury.Server.Core.Population.Enumerations.PopulationActivityScheduleType.ByFrequency) && (scheduleValue == 0)) { validationResponse.Add ("Schedule", "Invalid Schedule Value."); }

            if (action == null) { validationResponse.Add ("Action", "No Action Defined for Activity."); }

            else {

                Dictionary<String, String> actionValidation = action.Validate ();

                foreach (String currentKey in actionValidation.Keys) {

                    if (!validationResponse.ContainsKey (currentKey)) {

                        validationResponse.Add (currentKey, actionValidation[currentKey]);

                    }

                }

            }

            return validationResponse;

        }

        #endregion


        #region Data Functions

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            populationId = (Int64) currentRow["PopulationId"];

            scheduleType = (Mercury.Server.Core.Population.Enumerations.PopulationActivityScheduleType) (Int32) currentRow["ScheduleType"];

            scheduleValue = (Int32) currentRow["ScheduleValue"];

            scheduleQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow["ScheduleQualifier"];

            anchorDate = (Mercury.Server.Core.Population.Enumerations.PopulationActivityEventAnchorDate) (Int32) currentRow ["AnchorDate"];

            Reoccurring = (Boolean) currentRow["IsReoccurring"];

            performActionDateType = (Mercury.Server.Core.Population.Enumerations.PopulationActivityPerformActionDateType) (Int32) currentRow["PerformActionDateType"];

            action = new Mercury.Server.Core.Action.Action (base.application);

            action.MapDataFields (String.Empty, currentRow);


            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;

            try {

                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.PopulationActivityEvent_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (populationId.ToString () + ", ");

                sqlStatement.Append (((Int32) scheduleType).ToString () + ", ");

                sqlStatement.Append (scheduleValue.ToString () + ", ");

                sqlStatement.Append (((Int32) scheduleQualifier).ToString () + ", ");

                sqlStatement.Append (((Int32) anchorDate).ToString () + ", ");

                sqlStatement.Append (reoccurring.ToString () + ", ");

                sqlStatement.Append (((Int32) performActionDateType).ToString () + ", ");

                sqlStatement.Append (Action.Id.ToString () + ", ");

                sqlStatement.Append ("'" + action.ActionParametersXmlSqlParsedString + "', ");

                sqlStatement.Append ("'" + action.Description + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
