using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [DataContract (Name = "WorkflowPermission")]
    public class WorkflowPermission : CoreObject {

        #region Private Properties

        [DataMember (Name = "WorkflowId")]
        private Int64 workflowId;

        [DataMember (Name = "WorkTeamId")]
        private Int64 workTeamId;


        [DataMember (Name = "IsGranted")]
        private Boolean isGranted;

        [DataMember (Name = "IsDenied")]
        private Boolean isDenied;

        #endregion


        #region Public Properties

        public override String Name { get { return application.CoreObjectGetNameById ("WorkTeam", WorkTeamId); } }

        public override String Description { get { return application.CoreObjectGetNameById ("WorkTeam", WorkTeamId); } }


        public Int64 WorkflowId { get { return workflowId; } set { workflowId = value; } }

        public Int64 WorkTeamId { get { return workTeamId; } set { workTeamId = value; } }


        public Boolean IsGranted { get { return isGranted; } set { isGranted = value; } }

        public Boolean IsDenied { get { return isDenied; } set { isDenied = value; } }

        #endregion


        #region Constructors

        public WorkflowPermission (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public WorkflowPermission (Application applicationReference, Int64 forWorkflowId, Int64 forWorkTeamId) {

            base.BaseConstructor (applicationReference);

            return;

        }
        
        #endregion


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];



            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WorkflowId", workflowId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WorkflowName", application.CoreObjectGetNameById ("Workflow", workflowId));

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WorkTeamId", workTeamId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WorkTeamName", application.CoreObjectGetNameById ("WorkTeam", workTeamId));

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "IsGranted", isGranted.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "IsDenied", isDenied.ToString ());
            
            #endregion


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = new List<ImportExport.Result> ();

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            try {

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        case "WorkflowName": WorkflowId = application.CoreObjectGetIdByName ("Workflow", currentPropertyNode.InnerText); break;

                        case "WorkTeamName": WorkTeamId = application.CoreObjectGetIdByName ("WorkTeam", currentPropertyNode.InnerText); break;

                        case "IsGranted": IsGranted = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                        case "IsDenied": IsDenied = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    }

                }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }
        #endregion 


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            workflowId = (Int64) currentRow["WorkflowId"];

            workTeamId = (Int64) currentRow["WorkTeamId"];

            isGranted = (Boolean) currentRow["IsGranted"];

            isDenied = (Boolean) currentRow["IsDenied"];

            CreateAccountInfo.MapDataFields (currentRow, "Create");

            ModifiedAccountInfo.MapDataFields (currentRow, "Modified");

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            
            sqlStatement.Append ("EXEC WorkflowPermission_InsertUpdate ");


            sqlStatement.Append (workflowId.ToString () + ", ");

            sqlStatement.Append (workTeamId.ToString () + ", ");


            sqlStatement.Append (Convert.ToByte (isGranted) + ", ");

            sqlStatement.Append (Convert.ToByte (isDenied) + ", ");


            sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);

            

            success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;

        }

        #endregion


    }

}
