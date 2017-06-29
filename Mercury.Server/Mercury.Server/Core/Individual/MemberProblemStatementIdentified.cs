using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "MemberProblemStatementIdentified")]
    public class MemberProblemStatementIdentified : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId = 0;

        [DataMember (Name = "ProblemStatementId")]
        private Int64 problemStatementId = 0;

        [DataMember (Name = "IdentifiedDate")]
        private DateTime identifiedDate;

        [DataMember (Name = "IsRequired")]
        private Boolean isRequired = false;

        [DataMember (Name = "MemberCaseId")]
        private Int64 memberCaseId; // MEMBER CASE ID THAT THE PROBLEM STATEMENT WAS ADDED TO

        [DataMember (Name = "CompletionDate")]
        private DateTime? completionDate; // DATE THE PROBLEM STATEMENT WAS COMPLETED (CLOSED)

        // TODO: ADD COMPLETED/CLOSED INFORMATION 
        
        #endregion 


        #region Public Properties

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public DateTime IdentifiedDate { get { return identifiedDate; } set { identifiedDate = value; } }

        public Boolean IsRequired { get { return isRequired; } set { isRequired = value; } }

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public DateTime? CompletionDate { get { return completionDate; } set { completionDate = value; } }

        #endregion 


        #region Constructors

        public MemberProblemStatementIdentified (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;
            
        }

        #endregion 


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberId = base.IdFromSql (currentRow, "MemberId");

            ProblemStatementId = base.IdFromSql (currentRow, "ProblemStatementId");

            IdentifiedDate = (DateTime)currentRow["IdentifiedDate"];

            IsRequired = Convert.ToBoolean (currentRow["IsRequired"]);

            MemberCaseId = base.IdFromSql (currentRow, "MemberCaseId");

            CompletionDate = base.DateTimeFromSql (currentRow, "MemberCaseAssignedDate");


            return;

        }

        #endregion 

    }

}
