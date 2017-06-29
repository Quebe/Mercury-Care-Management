using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Mercury.Server.Core.Individual.Case {

    [DataContract (Name = "MemberCaseProblemClass")]
    public class MemberCaseProblemClass : CoreExtensibleObject {

        #region Private Properties

        [DataMember (Name = "MemberCaseId")]
        private Int64 memberCaseId;

        [DataMember (Name = "ProblemClassId")]
        private Int64 problemClassId;


        [DataMember (Name = "AssignedToSecurityAuthorityId")]
        private Int64 assignedToSecurityAuthorityId;

        [DataMember (Name = "AssignedToUserAccountId")]
        private String assignedToUserAccountId;

        [DataMember (Name = "AssignedToUserAccountName")]
        private String assignedToUserAccountName;

        [DataMember (Name = "AssignedToUserDisplayName")]
        private String assignedToUserDisplayName;

        [DataMember (Name = "AssignedToDate")]
        private DateTime? assignedToDate = null;

        [DataMember (Name = "AssignedToProviderId")]
        private Int64 assignedToProviderId;

        [DataMember (Name = "AssignedToProviderDate")]
        private DateTime? assignedToProviderDate = null;


        [DataMember (Name = "ProblemCarePlans")]
        private List<MemberCaseProblemCarePlan> problemCarePlans = new List<MemberCaseProblemCarePlan> ();


        private MemberCase memberCase = null;

        #endregion 

        
        #region Public Properties

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public Int64 ProblemClassId { get { return problemClassId; } set { problemClassId = value; } }


        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }

        
        public Int64 AssignedToProviderId { get { return assignedToProviderId; } set { assignedToProviderId = value; } }
        
        public DateTime? AssignedToProviderDate { get { return assignedToProviderDate; } set { assignedToProviderDate = value; } }


        public List<MemberCaseProblemCarePlan> ProblemCarePlans { get { return problemCarePlans; } set { problemCarePlans = value; } }

        public MemberCase MemberCase { get { return memberCase; } set { memberCase = value; } }

        public override Application Application { get { return base.Application; }

            set {

                base.Application = value;

                if (problemCarePlans == null) { problemCarePlans = new List<MemberCaseProblemCarePlan> (); }

                foreach (MemberCaseProblemCarePlan currentProblemCarePlan in problemCarePlans) {

                    currentProblemCarePlan.Application = value;

                    currentProblemCarePlan.MemberCaseProblemClass = this;

                }

            }

        }

        #endregion 

        
        #region Constructors

        public MemberCaseProblemClass (Application applicationReference) {

            BaseConstructor (applicationReference);

            return; 
        
        }

        public MemberCaseProblemClass (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions

        public override Boolean LoadChildObjects () {

            Boolean success = base.LoadChildObjects ();

            System.Data.DataTable dataTable;

            if (success) { // LOAD CHILD OBJECTS

                // LOAD PROBLEM CARE PLAN RELATIONSHIPS

                problemCarePlans = new List<MemberCaseProblemCarePlan> ();

                dataTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM MemberCaseProblemCarePlan WHERE MemberCaseProblemClassId = " + Id.ToString ());

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    MemberCaseProblemCarePlan problemCarePlan = new MemberCaseProblemCarePlan (application);

                    problemCarePlan.MapDataFields (currentRow);

                    problemCarePlan.LoadChildObjects ();

                    problemCarePlan.MemberCaseProblemClass = this;

                    problemCarePlans.Add (problemCarePlan);

                }

            }


            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseId = base.IdFromSql (currentRow, "MemberCaseId");

            problemClassId = (Int64)currentRow["ProblemClassId"];


            assignedToSecurityAuthorityId = base.IdFromSql (currentRow, "AssignedToSecurityAuthorityId");

            assignedToUserAccountId = (String)currentRow["AssignedToUserAccountId"];

            assignedToUserAccountName = (String)currentRow["AssignedToUserAccountName"];

            assignedToUserDisplayName = (String)currentRow["AssignedToUserDisplayName"];

            assignedToDate = base.DateTimeFromSql (currentRow, "AssignedToDate");


            assignedToProviderId = base.IdFromSql (currentRow, "AssignedToProviderId");
            
            assignedToProviderDate = base.DateTimeFromSql (currentRow, "AssignedToProviderDate");

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 
        
        #endregion 

    }

}
