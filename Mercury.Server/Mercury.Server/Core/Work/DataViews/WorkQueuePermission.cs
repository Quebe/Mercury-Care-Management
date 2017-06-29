using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work.DataViews {

    [DataContract (Name = "WorkQueuePermissionDataView")]
    public class WorkQueuePermission {

        #region Private Properties

        [DataMember (Name = "WorkQueueId")]
        private Int64 workQueueId;

        [DataMember (Name = "Name")]
        private String workQueueName;

        [DataMember (Name = "Description")]
        private String description;

        [DataMember (Name = "WorkflowId")]
        private Int64 workflowId;

        [DataMember (Name = "Enabled")]
        private Boolean enabled = true;

        [DataMember (Name = "Visible")]
        private Boolean visible = true;

        [DataMember (Name = "Permission")]
        private Enumerations.WorkQueueTeamPermission permission = Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Denied;

        #endregion 


        #region Public Properties

        public Int64 WorkQueueId { get { return workQueueId; } set { workQueueId = value; } }

        public String Name { get { return workQueueName; } set { workQueueName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String Description { get { return description; } set { description = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description); } }

        public Int64 WorkflowId { get { return workflowId; } set { workflowId = value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        public Boolean Visible { get { return visible; } set { visible = value; } }

        public Enumerations.WorkQueueTeamPermission Permission { get { return permission; } set { permission = value; } }

        #endregion 


        #region Data Functions 

        public void MapDataFields (System.Data.DataRow currentRow) {

            workQueueId = (Int64) currentRow["WorkQueueId"];

            workQueueName = (String) currentRow["WorkQueueName"];

            description = (String) currentRow["WorkQueueDescription"];

            workflowId = (Int64) currentRow["WorkflowId"];

            enabled = (Boolean) currentRow["Enabled"];

            visible = (Boolean) currentRow["Visible"];

            if (currentRow.Table.Columns.Contains ("WorkQueuePermission")) {

                permission = (Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission) (Int32) currentRow["WorkQueuePermission"];

            }

            return;

        }

        #endregion 

    }

}
