using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [DataContract (Name = "RoutingRule")]
    public class RoutingRule : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "DefaultWorkQueueId")]
        private Int64 defaultWorkQueueId;

        [DataMember (Name = "Rules")]
        private SortedList<Int32,RoutingRuleDefinition> rules = new SortedList<Int32,RoutingRuleDefinition> ();


        [NonSerialized]
        WorkQueue defaultWorkQueue = null;

        #endregion


        #region Public Properties

        public Int64 DefaultWorkQueueId { get { return defaultWorkQueueId; } set { defaultWorkQueueId = value; defaultWorkQueue = null; } }

        public SortedList<Int32, RoutingRuleDefinition> Rules { get { return rules; } set { rules = value; } }


        public WorkQueue DefaultWorkQueue {

            get {

                if (defaultWorkQueue != null) { return defaultWorkQueue; }

                if (application == null) { return null; }

                defaultWorkQueue = application.WorkQueueGet (defaultWorkQueueId);

                return defaultWorkQueue;

            }

        }

        #endregion


        #region Constructors

        public RoutingRule (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public RoutingRule (Application applicationReference, Int64 forRoutingRuleId) {

            base.BaseConstructor (applicationReference, forRoutingRuleId);

            return;

        }

        #endregion


        #region XML Serialization

        //public override System.Xml.XmlDocument XmlSerialize () {

        //    System.Xml.XmlDocument routingRuleDocument = base.XmlSerialize ();

        //    System.Xml.XmlElement routingRuleNode = routingRuleDocument.CreateElement ("RoutingRule");

        //    System.Xml.XmlElement propertiesNode;

        //    System.Xml.XmlNode importedNode;


        //    routingRuleDocument.AppendChild (routingRuleNode);

        //    routingRuleNode.SetAttribute ("RoutingRuleId", routingRuleId.ToString ());

        //    routingRuleNode.SetAttribute ("Name", routingRuleName);

        //    propertiesNode = routingRuleDocument.CreateElement ("Properties");

        //    routingRuleNode.AppendChild (propertiesNode);


        //    #region Routing Rule Properties

        //    CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDocument, propertiesNode, "RoutinRuleId", routingRuleId.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDocument, propertiesNode, "RoutingRuleName", routingRuleName);

        //    CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDocument, propertiesNode, "Description", description);

        //    CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDocument, propertiesNode, "DefaultWorkQueueId", defaultWorkQueueId.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDocument, propertiesNode, "Enabled", enabled.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDocument, propertiesNode, "Visible", visible.ToString ());

        //    if (DefaultWorkQueue != null) {

        //        System.Xml.XmlElement defaultWorkQueueNode = CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDocument, propertiesNode, "DefaultWorkQueue", String.Empty);

        //        importedNode = routingRuleDocument.ImportNode (DefaultWorkQueue.XmlSerialize ().LastChild, true);

        //        defaultWorkQueueNode.AppendChild (importedNode);


        //    }

        //    #endregion



        //    #region Rule Definitions

        //    System.Xml.XmlDocument definitionDocument;

        //    System.Xml.XmlElement definitionNode;


        //    definitionNode = routingRuleDocument.CreateElement ("Definitions");

        //    routingRuleNode.AppendChild (definitionNode);


        //    foreach (Int32 currentRuleId in Rules.Keys) {

        //        RoutingRuleDefinition currentDefinition = Rules[currentRuleId];

        //        definitionDocument = currentDefinition.XmlSerialize ();

        //        if (definitionDocument.ChildNodes[1] != null) {

        //            definitionNode.AppendChild (routingRuleDocument.ImportNode (definitionDocument.ChildNodes[1], true));

        //        }

        //    }

        //    #endregion

        //    return routingRuleDocument;

        //}

        //public override List<Mercury.Server.Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Mercury.Server.Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = objectNode.Attributes["Name"].InnerText;

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "RoutingRule") {

        //        foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

        //            switch (currentNode.Name) {

        //                case "Properties":

        //                    foreach (System.Xml.XmlNode currentProperty in currentNode.ChildNodes) {

        //                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                            case "RoutingRuleName": RoutingRuleName = currentProperty.InnerText; break;

        //                            case "Description": Description = currentProperty.InnerText; break;

        //                            case "Enabled": Enabled = Convert.ToBoolean (currentProperty.InnerText); break;

        //                            case "Visible": Visible = Convert.ToBoolean (currentProperty.InnerText); break;

        //                            case "DefaultWorkQueue":

        //                                String defaultWorkQueueName = currentProperty.FirstChild.Attributes["Name"].InnerText;

        //                                DefaultWorkQueueId = application.WorkQueueGetIdByName (defaultWorkQueueName);

        //                                if (defaultWorkQueueId == 0) {

        //                                    defaultWorkQueue = new WorkQueue (application);

        //                                    response.AddRange (defaultWorkQueue.XmlImport (currentProperty.FirstChild));

        //                                    DefaultWorkQueueId = application.WorkQueueGetIdByName (defaultWorkQueue.Name);

        //                                }
                                        
        //                                break;

        //                        }

        //                    }

        //                    break;

        //                case "Definitions":

        //                    foreach (System.Xml.XmlNode currentDefinition in currentNode.ChildNodes) {

        //                        RoutingRuleDefinition routingRuleDefinition = new RoutingRuleDefinition (application);

        //                        response.AddRange (routingRuleDefinition.XmlImport (currentDefinition));

        //                        routingRuleDefinition.Sequence = rules.Count + 1;

        //                        rules.Add (routingRuleDefinition.Sequence, routingRuleDefinition);

        //                    }

        //                    break;

        //            }

        //        }

        //        importResponse.Success = Save ();

        //        importResponse.Id = Id;

        //        if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }

        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as WorkQueue.")); }

        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion

        
        #region Data Functions
        
        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            defaultWorkQueueId = (Int64) currentRow["DefaultWorkQueueId"];



            System.Data.DataTable definitionTable;

            definitionTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM RoutingRuleDefinition WHERE RoutingRuleId = " + Id.ToString () + " ORDER BY [Sequence]", 0);

            foreach (System.Data.DataRow currentDefinitionRow in definitionTable.Rows) {

                RoutingRuleDefinition definition = new RoutingRuleDefinition (application);

                definition.MapDataFields (currentDefinitionRow);

                rules.Add (definition.Sequence, definition);

            }

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.RoutingRuleManage)) { throw new ApplicationException ("PermissionDenied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.RoutingRule_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (defaultWorkQueueId.ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                if (success) {

                    success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM RoutingRuleDefinition WHERE RoutingRuleId = " + Id.ToString ());

                    if (success) {

                        foreach (Int32 currentSequence in rules.Keys) {

                            rules[currentSequence].RoutingRuleId = Id;

                            rules[currentSequence].Sequence = currentSequence;

                            success = rules[currentSequence].Save (application);

                            if (!success) { throw new ApplicationException ("Unable to save Routing Rule Definitions."); }

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

        public Int64 Evaluate (Member.Member member) {

            Int64 workQueueId = -1;  // (-1) NO MATCH FOUND
            
            // INITIALIZE TO A INVALID WORK QUEUE ID SO THAT A MATCH WITH AN UNASSIGNED WORK QUEUE (0) 
            // DOESN'T GET REASSIGNED TO THE DEFAULT WORK QUEUE FOR THE ROUTING RULE


            foreach (Int32 currentSequence in rules.Keys) { // CYCLE THROUGH ALL RULES

                if (rules[currentSequence].MatchMember (member)) { // MATCH FOUND

                    workQueueId = rules[currentSequence].WorkQueueId; // ASSIGNED WORK QUEUE ID FROM RULE

                    break; // STOP THE CYCLE (FIRST MATCH WINS)

                }

            }

            // ONLY DEFAULT WHEN NO MATCH HAS BEEN FOUND (-1) 
            if (workQueueId == -1) { workQueueId = defaultWorkQueueId; }


            return workQueueId;

        }

        #endregion 

    }
}
