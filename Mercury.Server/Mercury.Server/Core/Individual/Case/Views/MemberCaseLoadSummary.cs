using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case.Views {

    [Serializable]
    [DataContract (Name = "MemberCaseLoadSummary")]
    public class MemberCaseLoadSummary : CoreObject {

        
        #region Private Properties

        [DataMember (Name = "AssignedToWorkTeamId")]
        private Int64 assignedToWorkTeamId;

        [DataMember (Name = "AssignedToWorkTeamName")]
        private String assignedToWorkTeamName;

        [DataMember (Name = "AssignedToUserDisplayName")]
        private String assignedToUserDisplayName;

        [DataMember (Name = "StatusUnderDevelopmentCount")]
        private Int32 statusUnderDevelopmentCount = 0;

        [DataMember (Name = "StatusActiveCount")]
        private Int32 statusActiveCount = 0;

        [DataMember (Name = "StatusClosedCount")]
        private Int32 statusClosedCount = 0;

        #endregion


        #region Public Properties

        public Int64 AssignedToWorkTeamId { get { return assignedToWorkTeamId; } set { assignedToWorkTeamId = value; } }

        public String AssignedToWorkTeamName { get { return assignedToWorkTeamName; } set { assignedToWorkTeamName = value; } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = value; } }

        public Int32 StatusUnderDevelopmentCount { get { return statusUnderDevelopmentCount; } set { statusUnderDevelopmentCount = value; } }

        public Int32 StatusActiveCount { get { return statusActiveCount; } set { statusActiveCount = value; } }

        public Int32 StatusClosedCount { get { return statusClosedCount; } set { statusClosedCount = value; } }

        #endregion


        #region Constructors

        public MemberCaseLoadSummary () {
            
            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            AssignedToWorkTeamId = base.IdFromSql (currentRow, "WorkTeamId");

            AssignedToWorkTeamName = base.StringFromSql (currentRow, "WorkTeamName");

            AssignedToUserDisplayName = base.StringFromSql (currentRow, "UserDisplayName");

            StatusUnderDevelopmentCount = (Int32)currentRow["StatusUnderDevelopmentCount"];

            StatusActiveCount = (Int32)currentRow["StatusActiveCount"];

            StatusClosedCount = (Int32)currentRow["StatusClosedCount"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
