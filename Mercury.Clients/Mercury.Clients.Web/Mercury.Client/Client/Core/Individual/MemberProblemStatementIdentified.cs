using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class MemberProblemStatementIdentified : CoreObject {

        #region Private Properties

        private Int64 memberId = 0;

        private Int64 problemStatementId = 0;

        private DateTime identifiedDate;

        private Boolean isRequired = false;

        private Int64 memberCaseId; // MEMBER CASE ID THAT THE PROBLEM STATEMENT WAS ADDED TO

        private DateTime? completionDate; // DATE THE PROBLEM STATEMENT WAS ADDED TO THE CASE

        // TODO: ADD COMPLETED/CLOSED INFORMATION 
        
        #endregion 


        #region Public Properties - Encapsulated

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public DateTime IdentifiedDate { get { return identifiedDate; } set { identifiedDate = value; } }

        public Boolean IsRequired { get { return isRequired; } set { isRequired = value; } }

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public DateTime? CompletionDate { get { return completionDate; } set { completionDate = value; } }

        #endregion 


        #region Public Properties

        public ProblemStatement ProblemStatement { get { return application.ProblemStatementGet (problemStatementId, true); } }

        #endregion 


        #region Constructors

        public MemberProblemStatementIdentified (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;
            
        }


        public MemberProblemStatementIdentified (Application applicationReference, Server.Application.MemberProblemStatementIdentified serverObject) {

            BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion 


        #region Public Methods


        public void MapFromServerObject (Server.Application.MemberProblemStatementIdentified serverObject) {

            MemberId = serverObject.MemberId;

            ProblemStatementId = serverObject.ProblemStatementId;

            IdentifiedDate = serverObject.IdentifiedDate;

            IsRequired = serverObject.IsRequired;

            MemberCaseId = serverObject.MemberCaseId;

            CompletionDate = serverObject.CompletionDate;

            return;

        }


        #endregion 

    }

}
