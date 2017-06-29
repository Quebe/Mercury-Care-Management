using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkQueueTeam")]
    public class WorkQueueTeam : CoreObject {

        #region Private Properties

        [DataMember (Name = "WorkQueueId")]
        private Int64 workQueueId;

        [DataMember (Name = "WorkTeamId")]
        private Int64 workTeamId;

        [NonSerialized]
        private WorkTeam workTeam = null;

        [DataMember (Name = "WorkTeamName")]
        private String workTeamName;

        [DataMember (Name = "Permission")]
        private Enumerations.WorkQueueTeamPermission permission = Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Denied;

        #endregion 


        #region Public Properties

        public Int64 WorkQueueId { get { return workQueueId; } set { workQueueId = value; } }

        public Int64 WorkTeamId { get { return workTeamId; } set { workTeamId = value; } }

        public WorkTeam WorkTeam {

            get {

                if (workTeam != null) { return workTeam; }

                if (application == null) { return null; }

                workTeam = application.WorkTeamGet (workTeamId);

                return workTeam;

            }

        }

        public String WorkTeamName { get { return workTeamName; } set { workTeamName = value; } }

        public Enumerations.WorkQueueTeamPermission Permission { get { return permission; } set { permission = value; } }

        #endregion


        #region Constructors

        public WorkQueueTeam (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return; 
        
        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument workQueueTeamDocument = base.XmlSerialize ();

            System.Xml.XmlElement workQueueTeamNode = workQueueTeamDocument.CreateElement ("WorkQueueTeam");

            System.Xml.XmlElement propertiesNode;



            workQueueTeamDocument.AppendChild (workQueueTeamNode);

            workQueueTeamNode.SetAttribute ("WorkQueueId", workTeamId.ToString ());

            workQueueTeamNode.SetAttribute ("WorkTeamId", workTeamId.ToString ());

            workQueueTeamNode.SetAttribute ("Name", workTeamName);

            propertiesNode = workQueueTeamDocument.CreateElement ("Properties");

            workQueueTeamNode.AppendChild (propertiesNode);


            #region Population Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueTeamDocument, propertiesNode, "WorkQueueId", workQueueId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueTeamDocument, propertiesNode, "WorkTeamId", workTeamId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueTeamDocument, propertiesNode, "WorkTeamName", workTeamName);

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueTeamDocument, propertiesNode, "Permission", ((Int32) permission).ToString ());

            #endregion


            if (WorkTeam != null) {

                workQueueTeamNode.AppendChild (workQueueTeamDocument.ImportNode (WorkTeam.XmlSerialize ().ChildNodes[1], true));

            }



            return workQueueTeamDocument;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            workQueueId = (Int64) currentRow["WorkQueueId"];

            workTeamId = (Int64) currentRow["WorkTeamId"];

            if (currentRow.Table.Columns.Contains ("WorkTeamName")) { workTeamName = (String) currentRow["WorkTeamName"]; }

            permission = (Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission) (Int32) currentRow["WorkQueuePermission"];

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            String insertStatement;

            try {

                insertStatement = "INSERT INTO WorkQueueTeam (WorkQueueId, WorkTeamId, WorkQueuePermission) VALUES (";

                insertStatement = insertStatement + workQueueId.ToString () + ", ";

                insertStatement = insertStatement + workTeamId.ToString () + ", ";

                insertStatement = insertStatement + ((Int32) permission).ToString () + ")";

                success = application.EnvironmentDatabase.ExecuteSqlStatement (insertStatement);

            }

            catch (Exception saveException) {

                success = false;

                application.SetLastException (saveException);

            }

            return success;

        }

        #endregion

    }

}
