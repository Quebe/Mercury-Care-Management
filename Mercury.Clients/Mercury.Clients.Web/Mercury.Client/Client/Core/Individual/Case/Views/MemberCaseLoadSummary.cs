using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case.Views {

    [Serializable]
    public class MemberCaseLoadSummary : CoreObject {

        #region Private Properties

        private Int64 assignedToWorkTeamId;

        private String assignedToWorkTeamName;

        private String assignedToUserDisplayName;

        private Int32 statusUnderDevelopmentCount = 0;

        private Int32 statusActiveCount = 0;

        private Int32 statusClosedCount = 0;

        #endregion


        #region Public Properties

        public Int64 AssignedToWorkTeamId { get { return assignedToWorkTeamId; } set { assignedToWorkTeamId = value; } }

        public String AssignedToWorkTeamName { get { return assignedToWorkTeamName; } set { assignedToWorkTeamName = value; } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = value; } }

        public Int32 StatusUnderDevelopmentCount { get { return statusUnderDevelopmentCount; } set { statusUnderDevelopmentCount = value; } }

        public Int32 StatusActiveCount { get { return statusActiveCount; } set { statusActiveCount = value; } }

        public Int32 StatusClosedCount { get { return statusClosedCount; } set { statusClosedCount = value; } }


        public Int32 StatusTotalOpenCount { get { return (StatusUnderDevelopmentCount + StatusActiveCount); } }

        #endregion

        
        #region Constructors

        public MemberCaseLoadSummary (Application applicationReference, Server.Application.MemberCaseLoadSummary serverObject) {

            AssignedToWorkTeamId = serverObject.AssignedToWorkTeamId;

            AssignedToWorkTeamName = serverObject.AssignedToWorkTeamName;

            AssignedToUserDisplayName = serverObject.AssignedToUserDisplayName;

            StatusUnderDevelopmentCount = serverObject.StatusUnderDevelopmentCount;

            StatusActiveCount = serverObject.StatusActiveCount;

            StatusClosedCount = serverObject.StatusClosedCount;

            return;

        }

        #endregion 


    }

}
