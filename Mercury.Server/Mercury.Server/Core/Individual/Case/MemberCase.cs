using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {

    [Serializable]
    [DataContract (Name = "MemberCase")]
    public class MemberCase : CoreExtensibleObject {

        #region Private Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "ReferenceNumber")]
        private String referenceNumber = String.Empty;
        
        [DataMember (Name = "Status")]
        private Enumerations.CaseItemStatus status = Enumerations.CaseItemStatus.NotSpecified;


        [DataMember (Name = "AssignedToWorkTeamId")]
        private Int64 assignedToWorkTeamId;

        [DataMember (Name = "AssignedToWorkTeamDate")]
        private DateTime? assignedToWorkTeamDate;

        [DataMember (Name = "AssignedToSecurityAuthorityId")]
        private Int64 assignedToSecurityAuthorityId;

        [DataMember (Name = "AssignedToUserAccountId")]
        private String assignedToUserAccountId;

        [DataMember (Name = "AssignedToUserAccountName")]
        private String assignedToUserAccountName;

        [DataMember (Name = "AssignedToUserDisplayName")]
        private String assignedToUserDisplayName;

        [DataMember (Name = "AssignedToDate")]
        private DateTime? assignedToDate;



        [DataMember (Name = "LockedBySecurityAuthorityId")]
        private Int64 lockedBySecurityAuthorityId;

        [DataMember (Name = "LockedByUserAccountId")]
        private String lockedByUserAccountId;

        [DataMember (Name = "LockedByUserAccountName")]
        private String lockedByUserAccountName;

        [DataMember (Name = "LockedByUserDisplayName")]
        private String lockedByUserDisplayName;

        [DataMember (Name = "LockedByDate")]
        private DateTime? lockedByDate;
        


        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 12, 31);


        [DataMember (Name = "ProblemClasses")]
        private List<MemberCaseProblemClass> problemClasses = new List<MemberCaseProblemClass> ();

        [DataMember (Name = "CarePlans")]
        private List<MemberCaseCarePlan> carePlans = new List<MemberCaseCarePlan> ();

        [DataMember (Name = "CareInterventions")]
        private List<MemberCaseCareIntervention> careInterventions = new List<MemberCaseCareIntervention> ();

        #endregion 


        #region Public Properties

        public override string Description { get { return base.Description; } set { description = CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description8000); } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public String ReferenceNumber { get { return referenceNumber; } }
        
        public Enumerations.CaseItemStatus Status { get { return status; } }


        public Int64 AssignedToWorkTeamId { get { return assignedToWorkTeamId; } set { assignedToWorkTeamId = value; } }

        public DateTime? AssignedToWorkTeamDate { get { return assignedToWorkTeamDate; } set { assignedToWorkTeamDate = value; } }


        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }


        public Int64 LockedBySecurityAuthorityId { get { return lockedBySecurityAuthorityId; } set { lockedBySecurityAuthorityId = value; } }

        public String LockedByUserAccountId { get { return lockedByUserAccountId; } set { lockedByUserAccountId = value; } }

        public String LockedByUserAccountName { get { return lockedByUserAccountName; } set { lockedByUserAccountName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String LockedByUserDisplayName { get { return lockedByUserDisplayName; } set { lockedByUserDisplayName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public DateTime? LockedByDate { get { return lockedByDate; } set { lockedByDate = value; } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }


        public List<MemberCaseProblemClass> ProblemClasses { get { return problemClasses; } set { problemClasses = value; } }

        public List<MemberCaseCarePlan> CarePlans { get { return carePlans; } set { carePlans = value; } }

        public List<MemberCaseCareIntervention> CareInterventions { get { return careInterventions; } set { careInterventions = value; } }

        #endregion 


        #region Public Properties - Assignments

        public Enumerations.MemberCaseActionOutcome SetDescription (String forDescription) {

            // BUSINESS RULES FOR UPDATING THE REFERENCE NUMBER

            // 1. THE CASE MUST BE UNLOCKED OR LOCKED BY THE USER REQUESTING THE UPDATE (HANDLED BY THE STORED PROCEDURE)

            // 2. THE CASE MUST BE UNASSIGNED OR ASSIGNED TO THE USER REQUESTING THE UPDATE (HANDLED BY THE STORED PROCEDURE)

            // 3. IF UNASSIGNED TO A USER, BUT ASSIGNED TO A TEAM, THE USER MUST BE PART OF THE TEAM THAT CASE IS ASSIGNED TO (HANDLED BY THE STORED PROCEDURE)

            String originalDescription = Description; // STORE THE REFERENCE NUMBER IN CASE SAVE FAILS

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            Description = forDescription;

            outcome = SaveMemberCase (false, "Member Case Description Altered.");

            if (outcome != Enumerations.MemberCaseActionOutcome.Success) { Description = originalDescription; }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome SetReferenceNumber (String forReferenceNumber) {

            // BUSINESS RULES FOR UPDATING THE REFERENCE NUMBER

            // 1. THE CASE MUST BE UNLOCKED OR LOCKED BY THE USER REQUESTING THE UPDATE (HANDLED BY THE STORED PROCEDURE)

            // 2. THE CASE MUST BE UNASSIGNED OR ASSIGNED TO THE USER REQUESTING THE UPDATE (HANDLED BY THE STORED PROCEDURE)

            // 3. IF UNASSIGNED TO A USER, BUT ASSIGNED TO A TEAM, THE USER MUST BE PART OF THE TEAM THAT CASE IS ASSIGNED TO (HANDLED BY THE STORED PROCEDURE)

            String originalReferenceNumber = referenceNumber; // STORE THE REFERENCE NUMBER IN CASE SAVE FAILS

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            referenceNumber = CommonFunctions.SetValueMaxLength (forReferenceNumber, Server.Data.DataTypeConstants.UniqueId);

            outcome = SaveMemberCase (false, "Member Case Reference Number Altered.");

            if (outcome != Enumerations.MemberCaseActionOutcome.Success) { referenceNumber = originalReferenceNumber; }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome Lock () {

            // BUSINESS RULES FOR LOCKING

            // 1. THE CASE MUST BE UNLOCKED; IF THE CASE IS ALREADY LOCKED, THE LOCK CANNOT BE CHANGED, IT MUST BE UNLOCKED FIRST

            // 2. THE LOCK CAN ONLY BE ASSIGNED TO THE CALLING USER (APPLICATION.SESSION -> CURRENT USER)

            // 3. IF ASSIGNED TO A USER, THE LOCK REQUEST CAN ONLY COME FROM THAT USER

            // 4. IF UNASSIGNED TO A USER, BUT ASSIGNED TO A TEAM, THE USER MUST BE PART OF THE TEAM THAT CASE IS ASSIGNED TO (HANDLED BY THE STORED PROCEDURE)

            // 5. IF UNASSIGNED TO A TEAM AND USER, ANYONE CAN LOCK THE CASE

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;


            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                // NO VALIDATION REQUIRED

                // ATTEMPT TO SAVE NEW OR UPDATE EXISTING ENTITY ADDRESS

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCase_Lock");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lockedBySecurityAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lockedByUserAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lockedByUserAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lockedByUserDisplayName", application.Session.UserDisplayName, Server.Data.DataTypeConstants.Name);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", false);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Value));

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    // UPDATE MODIFIED DATE AND LOCK INFORMATION FROM STORED PROCEDURE RESULTS

                    modifiedAccountInfo = new Data.AuthorityAccountStamp (application);

                    modifiedAccountInfo.ActionDate = Convert.ToDateTime (((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Value);


                    lockedBySecurityAuthorityId = application.Session.SecurityAuthorityId;

                    lockedByUserAccountId = application.Session.UserAccountId;

                    lockedByUserAccountName = application.Session.UserAccountName;

                    lockedByUserDisplayName = application.Session.UserDisplayName;

                    lockedByDate = modifiedAccountInfo.ActionDate;

                }

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome Unlock () {

            // BUSINESS RULES FOR UNLOCKING

            // 1. THE CASE MUST BE LOCKED; IF THE CASE IS ALREADY UNLOCKED, USER WILL RECEIVE PERMISSION DENIED

            // 2. THE LOCK CAN ONLY BE UNASSIGNED (NOT ASSIGNED DIRECTLY TO ANOTHER USER)

            // PEOPLE THAT CAN LOCK IT ARE: (1) ORIGINAL PERSON THAT LOCKED IT
        
            // (2) CASE ASSIGNED TO TEAM -> MANAGER OF THE TEAM;

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;


            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                // NO VALIDATION REQUIRED

                // ATTEMPT TO SAVE NEW OR UPDATE EXISTING ENTITY ADDRESS

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCase_Unlock");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", false);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Value));

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    // UPDATE MODIFIED DATE AND LOCK INFORMATION FROM STORED PROCEDURE RESULTS

                    modifiedAccountInfo = new Data.AuthorityAccountStamp (application);

                    modifiedAccountInfo.ActionDate = Convert.ToDateTime (((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Value);


                    lockedBySecurityAuthorityId = 0;

                    lockedByUserAccountId = String.Empty;

                    lockedByUserAccountName = String.Empty;

                    lockedByUserDisplayName = String.Empty;

                    lockedByDate = null;

                }

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome AssignToWorkTeam (Int64 workTeamId) {

            // BUSINESS RULES FOR ASSIGNING A WORK TEAM (HANDLED IN STORE PROCEDURE)

			// CHECK TO SEE IF THE RECORD IS LOCKED, CAN ONLY MODIFY IF OWNER OF LOCK OR RECORD IS NOT LOCKED      
                    
            // IF WE REACH THIS POINT, THE RECORD IS UNLOCKED OR LOCKED BY REQUESTING USER, 
        
            // TO ASSIGN, ONE OF THE FOLLOWING MUST BE TRUE
        
            // 1. CASE IS NOT CURRENTLY ASSIGNED TO CASE AND REQUESTING USER IS MEMBER OF DESTINATION TEAM (ANY ROLE)
        
            // 2. CASE IS CURRENTLY ASSIGNED, REQUEST IS TO REMOVE ASSIGNMENT FROM TEAM, USER IS MANAGER OF TEAM
        
            // 3. CASE IS CURRENTLY ASSIGNED, REQUEST IS TO MOVE TO ANOTHER TEAM, USER IS MANAGER OF BOTH TEAMS
        
            // TODO: CHECK FOR CARE PLAN ASSIGNMENTS, AS THIS WILL REPLACE THEM

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;


            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                // NO VALIDATION REQUIRED (VALIDATION OCCURS IN STORED PROCEDURE)

                // ATTEMPT TO ASSIGN MEMBER CASE TO WORK TEAM

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCase_AssignToWorkTeam");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignedToWorkTeamId", base.IdSqlAllowNullInt64 (workTeamId));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", false);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Value));

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    this.Load (id); // RELOAD THIS MEMBER CASE 

                }

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome AssignToUser (Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName) {

            // BUSINESS RULES FOR ASSIGNING A USER (HANDLED IN STORE PROCEDURE)

            // CHECK TO SEE IF THE RECORD IS LOCKED, CAN ONLY MODIFY IF OWNER OF LOCK OR RECORD IS NOT LOCKED      

            // IF WE REACH THIS POINT, THE RECORD IS UNLOCKED OR LOCKED BY REQUESTING USER, 

            // TO ASSIGN, ONE OF THE FOLLOWING MUST BE TRUE
            
            // TO ASSIGN, ONE OF THE FOLLOWING MUST BE TRUE
        
            // 1. CASE IS NOT CURRENTLY ASSIGNED TO A USER, REQUESTING USER IS THE ASSIGN TO USER (SELF-ASSIGN)
        
            // 2. CASE IS NOT CURRENTLY ASSIGNED TO A USER, REQUESTING USER IS A MANAGER OF THE ASSIGNED TO TEAM
        
            // 3. CASE IS ASSIGNED TO A USER, REQUESTING USER IS THE CURRENT ASSIGNED TO USER AND THE NEW ASSIGNED TO IS UNASSIGN
        
            // 4. CASE IS ASSIGNED TO A USER, REQUESTING USER IS A MANAGER OF THE ASSIGNED TO TEAM
        
            // TODO: CHECK FOR CARE PLAN ASSIGNMENTS, AS THIS WILL REPLACE THEM

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;


            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                // NO VALIDATION REQUIRED (VALIDATION OCCURS IN STORED PROCEDURE)

                // ATTEMPT TO ASSIGN MEMBER CASE TO USER

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCase_AssignToUser");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignedToSecurityAuthorityId", securityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignedToUserAccountId", userAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignedToUserAccountName", userAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignedToUserDisplayName", userDisplayName, Server.Data.DataTypeConstants.Name);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", false);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Value));

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    this.Load (id); // RELOAD THIS MEMBER CASE 

                }

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome AssignMemberCaseProblemClassToUser (Int64 memberCaseProblemClassId, Int64 assignToSecurityAuthorityId, String assignToUserAccountId, String assignToUserAccountName, String assignToUserDisplayName) {

            // DEFAULT OUTCOME TO UNKNOWN ERROR

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // BUSINESS RULES FOR ASSIGNING MEMBER CASE PROBLEM CLASS TO USER

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;

            try {

                // NO VALIDATION REQUIRED (VALIDATION OCCURS IN STORED PROCEDURE)

                // ATTEMPT TO ASSIGN MEMBER CASE PROBLEM CLASS TO USER

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCaseProblemClass_AssignToUser");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseProblemClassId", memberCaseProblemClassId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignToSecurityAuthorityId", base.IdSqlAllowNullInt64 (assignToSecurityAuthorityId));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignToUserAccountId", assignToUserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignToUserAccountName", assignToUserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignToUserDisplayName", assignToUserDisplayName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDbDataParameter)sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter)sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                // EXECUTE SQL STORED PROCEDURE

                sqlCommand.ExecuteNonQuery ();

                // SET OUTCOME AS MEMBER CASE ACTION OUTCOME OF STORED PROCEDURE

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter)sqlCommand.Parameters["@RETURN"]).Value));

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    this.Load (Id); // RELOAD THIS MEMBER CASE

                }

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            // RETURN OUTCOME

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome AssignMemberCaseProblemClassToProvider (Int64 memberCaseProblemClassId, Int64 assignToProviderId) {

            // DEFAULT OUTCOME TO UNKNOWN ERROR

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // BUSINESS RULES FOR ASSIGNING MEMBER CASE PROBLEM CLASS TO PROVIDER

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;

            try {

                // NO VALIDATION REQUIRED (VALIDATION OCCURS IN STORED PROCEDURE)

                // ATTEMPT TO ASSIGN MEMBER CASE PROBLEM CLASS TO PROVIDER

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCaseProblemClass_AssignToProvider");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseProblemClassId", memberCaseProblemClassId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assignToProviderId", base.IdSqlAllowNullInt64 (assignToProviderId));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDbDataParameter)sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter)sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                // EXECUTE SQL STORED PROCEDURE

                sqlCommand.ExecuteNonQuery ();

                // SET OUTCOME AS MEMBER CASE ACTION OUTCOME OF STORED PROCEDURE

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter)sqlCommand.Parameters["@RETURN"]).Value));

                // SET APPLICATION EXCEPTION AS OUTCOME

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    this.Load (Id); // RELOAD THIS MEMBER CASE

                }

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            // RETURN OUTCOME

            return outcome;

        }

        #endregion 


        #region Public Properties

        public override Application Application {

            set {

                base.Application = value;


                // PROPOGATE: SET ALL CHILD REFERENCES

                if (problemClasses == null) { problemClasses = new List<MemberCaseProblemClass> (); }

                foreach (MemberCaseProblemClass currentProblemClass in problemClasses) { 
                
                    currentProblemClass.Application = value;

                    currentProblemClass.MemberCase = this;
                
                }


                if (carePlans == null) { carePlans = new List<MemberCaseCarePlan> (); }

                foreach (MemberCaseCarePlan currentCarePlan in carePlans) { 
                    
                    currentCarePlan.Application = value;

                    currentCarePlan.MemberCase = this;

                }

                if (careInterventions == null) { careInterventions = new List<MemberCaseCareIntervention> (); }

                foreach (MemberCaseCareIntervention currentCareIntervention in careInterventions) {

                    currentCareIntervention.Application = value;

                    currentCareIntervention.MemberCase = this;

                }

            }

        }

        public Member.Member Member { get { return application.MemberGet (memberId, true); } }

        #endregion 


        #region Constructors

        public MemberCase (Application applicationReference) {

            BaseConstructor (applicationReference);

            return; 
        
        }

        public MemberCase (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Support Functions

        public void SetApplicationException (Enumerations.MemberCaseActionOutcome outcome, String forObjectType = "Member Case") {

            switch (outcome) {

                case Enumerations.MemberCaseActionOutcome.Success:

                    base.application.ClearLastException ();

                    break;

                case Enumerations.MemberCaseActionOutcome.NotFoundError:

                    base.application.SetLastExceptionQuite (new ApplicationException (forObjectType + " not found (" + id.ToString () + ")."));

                    break;

                case Enumerations.MemberCaseActionOutcome.PermissionDenied:

                    base.application.SetLastExceptionQuite (new ApplicationException ("Permission Denied when saving " + forObjectType + "."));

                    break;

                case Enumerations.MemberCaseActionOutcome.ModifiedError:

                    base.application.SetLastExceptionQuite (new ApplicationException (forObjectType + " has been modified since last retreived from the database. Please review the changes and make your request again."));

                    break;

                case Enumerations.MemberCaseActionOutcome.LockedError:

                    base.application.SetLastExceptionQuite (new ApplicationException (forObjectType + " is locked by another user and cannot be updated."));

                    break;

                case Enumerations.MemberCaseActionOutcome.NotAssignedToError:

                    base.application.SetLastExceptionQuite (new ApplicationException (forObjectType + " is assigned to another user and cannot be updated."));

                    break;

                case Enumerations.MemberCaseActionOutcome.NoChangeDetectedError:

                    base.application.SetLastExceptionQuite (new ApplicationException ("No Change Detected when saving " + forObjectType + "."));

                    break;

                case Enumerations.MemberCaseActionOutcome.PermissionDeniedCaseStatus:

                    base.application.SetLastExceptionQuite (new ApplicationException ("Permission Denied when saving " + forObjectType + "for Status: " + Server.CommonFunctions.EnumerationToString (status) + "."));

                    break;

                case Enumerations.MemberCaseActionOutcome.ProblemStatementExists:

                    base.application.SetLastExceptionQuite (new ApplicationException ("Unable to add Problem Statement. Existing Problem Statement found."));

                    break;

                case Enumerations.MemberCaseActionOutcome.CannotRemoveLast:

                    base.application.SetLastExceptionQuite (new ApplicationException ("Cannot remove last " + forObjectType + "."));

                    break;

                case Enumerations.MemberCaseActionOutcome.MemberCaseCarePlanGoalInterventionExists:

                    base.application.SetLastExceptionQuite (new ApplicationException ("Unable to add Care Intervention to Goal. Existing Care Intervention found."));

                    break;

                case Enumerations.MemberCaseActionOutcome.DuplicateFound:

                    base.application.SetLastExceptionQuite (new ApplicationException ("Duplicate Found."));

                    break;

                default: 

                    base.application.SetLastException (new ApplicationException ("[" + CommonFunctions.EnumerationToString (outcome) + "] Unknown or unhandled exception occurred when trying to save " + forObjectType + ": " + id.ToString () + "."));

                    break;

            }

            return;

        }

        #endregion 


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<string, string> (); // DO NOT USE BASE VALIDATIO FOR ID/NAME


            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean LoadChildObjects() {

            Boolean success = base.LoadChildObjects ();


            if (success) { // LOAD CHILD OBJECTS

                careInterventions = new List<MemberCaseCareIntervention> ();

                System.Data.DataTable careInterventionsTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM MemberCaseCareIntervention WHERE MemberCaseId = " + Id);

                foreach (System.Data.DataRow currentRow in careInterventionsTable.Rows) {

                    MemberCaseCareIntervention careIntervention = new MemberCaseCareIntervention (application);

                    careIntervention.MapDataFields (currentRow);

                    careIntervention.LoadChildObjects ();

                    careIntervention.MemberCase = this;

                    careInterventions.Add (careIntervention);

                }
                
                carePlans = new List<MemberCaseCarePlan> ();

                System.Data.DataTable carePlansTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM MemberCaseCarePlan WHERE MemberCaseId = " + Id);

                foreach (System.Data.DataRow currentRow in carePlansTable.Rows) {

                    MemberCaseCarePlan carePlan = new MemberCaseCarePlan (application);

                    carePlan.MapDataFields (currentRow);

                    carePlan.LoadChildObjects ();

                    carePlan.MemberCase = this;

                    carePlans.Add (carePlan);

                }
                

                problemClasses = new List<MemberCaseProblemClass> ();

                System.Data.DataTable problemClassesTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM MemberCaseProblemClass WHERE MemberCaseId = " + Id.ToString ());

                foreach (System.Data.DataRow currentRow in problemClassesTable.Rows) {

                    MemberCaseProblemClass problemClass = new MemberCaseProblemClass (application);

                    problemClass.MapDataFields (currentRow);

                    problemClass.LoadChildObjects ();

                    problemClass.MemberCase = this;

                    problemClasses.Add (problemClass);

                }

            }


            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberId = base.IdFromSql (currentRow, "MemberId");

            referenceNumber = (String)currentRow["ReferenceNumber"];

            status = (Enumerations.CaseItemStatus)Convert.ToInt32 (currentRow["Status"]);


            assignedToWorkTeamId = base.IdFromSql (currentRow, "AssignedToWorkTeamId");

            assignedToWorkTeamDate = base.DateTimeFromSql (currentRow, "AssignedToWorkTeamDate");


            assignedToSecurityAuthorityId = base.IdFromSql (currentRow, "AssignedToSecurityAuthorityId");

            assignedToUserAccountId = (String)currentRow["AssignedToUserAccountId"];

            assignedToUserAccountName = (String)currentRow["AssignedToUserAccountName"];

            assignedToUserDisplayName = (String)currentRow["AssignedToUserDisplayName"];

            assignedToDate = base.DateTimeFromSql (currentRow, "AssignedToDate");


            lockedBySecurityAuthorityId = base.IdFromSql (currentRow, "LockedBySecurityAuthorityId");

            lockedByUserAccountId = (String)currentRow["LockedByUserAccountId"];

            lockedByUserAccountName = (String)currentRow["LockedByUserAccountName"];

            lockedByUserDisplayName = (String)currentRow["LockedByUserDisplayName"];

            lockedByDate = base.DateTimeFromSql (currentRow, "LockedByDate");


            effectiveDate = (DateTime)currentRow["EffectiveDate"];

            terminationDate = (DateTime)currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        private Enumerations.MemberCaseActionOutcome SaveMemberCase (Boolean ignoreAssignedTo, String memberCaseAuditDescription) {

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;


            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        application.SetLastExceptionQuite (new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]));

                        return Enumerations.MemberCaseActionOutcome.ValidationError;

                    }

                }

                modifiedAccountInfo = new Data.AuthorityAccountStamp (application.Session);


                // ATTEMPT TO SAVE NEW OR UPDATE EXISTING ENTITY ADDRESS
                
                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCase_Update");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberId", memberId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@referenceNumber", referenceNumber, Server.Data.DataTypeConstants.UniqueId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@status", (Int32)status);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseDescription", Description, Server.Data.DataTypeConstants.Description8000);

                
                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@effectiveDate", effectiveDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@terminationDate", terminationDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@extendedProperties", ExtendedPropertiesSql, ExtendedPropertiesSql.Length);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", ignoreAssignedTo);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", ModifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", ModifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", ModifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseAuditDescription", memberCaseAuditDescription, Server.Data.DataTypeConstants.Description8000);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?) null));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDbDataParameter) sqlCommand.Parameters["@RETURN"]).Value));

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) {

                    // UPDATE MODIFIED DATE FROM STORED PROCEDURE RESULTS

                    modifiedAccountInfo.ActionDate = Convert.ToDateTime (((System.Data.IDbDataParameter) sqlCommand.Parameters["@modifiedDate"]).Value);

                }


                // DO NOT SET ID, THIS IS AN UPDATE ONLY PROCEDURE 

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        #endregion


        #region Virtual - Data Bindings

        override public Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> dataBindings = base.DataBindingContexts;

                Dictionary<String, String> childBindings;

                
                childBindings = new Member.Member (base.application).DataBindingContexts;

                foreach (String bindingName in childBindings.Keys) {

                    dataBindings.Add ("MemberCase.Member." + bindingName, childBindings[bindingName]);

                }

                return dataBindings;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "MemberCase":

                    bindingContextPart = bindingContext.Split ('.')[1];

                    switch (bindingContextPart) {

                        case "Member":

                            if (bindingContext == "MemberCase.Member.Id") { dataValue = memberId.ToString (); }

                            else {

                                bindingContextPart = bindingContext.Replace ("MemberCase.Member.", "");

                                dataValue = Member.EvaluateDataBinding (bindingContextPart);

                            }

                            break;


                        default: dataValue = "!Error"; break;

                    }

                    break;

                default: base.EvaluateDataBinding (bindingContext); break;

            }

            return dataValue;

        }

        #endregion


        #region Public Methods

        public Enumerations.MemberCaseActionOutcome AddProblemStatement (Int64 problemStatementId, Boolean isSingleInstance) {

            // BUSINESS RULES FOR ADDING A PROBLEM STATEMENT TO A MEMBER CASE
            
			// 1. THE CASE MUST BE UNDER DEVELOPMENT OR ACTIVE
				
			// 2. THE CASE MUST NOT BE LOCKED
				
			// 3. THE CASE MUST BE UNASSIGNED OR THE USER A MEMBER OF THE CARE TEAM ASSIGNED TO THE CASE
				
			// 4. THE PROBLEM STATEMENT MUST NOT ALREADY EXIST IN ACTIVE     

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE
            
            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                // NO VALIDATION REQUIRED

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCase_AddProblemStatement");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@problemStatementId", problemStatementId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@isSingleInstance", isSingleInstance);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", false);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDataParameter) sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDataParameter) sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDataParameter) sqlCommand.Parameters["@RETURN"]).Value));

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) { Load (id); } // ON SUCCESS - RELOAD OBJECT

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }

        public Enumerations.MemberCaseActionOutcome DeleteProblemStatement (Int64 memberCaseProblemCarePlanId) {

            // BUSINESS RULES FOR ADDING A PROBLEM STATEMENT TO A MEMBER CASE

            // 1. THE CASE MUST BE UNDER DEVELOPMENT OR ACTIVE, CANNOT BE LAST PROBLEM FOR AN ACTIVE CARE PLAN

            // 2. THE CASE MUST NOT BE LOCKED

            // 3. THE CASE MUST BE UNASSIGNED OR THE USER A MEMBER OF THE CARE TEAM ASSIGNED TO THE CASE

            // 4. THE PROBLEM STATEMENT MUST NOT ALREADY EXIST IN ACTIVE     

            Enumerations.MemberCaseActionOutcome outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

            // DO NOT USE TRANSACTIONS, SERIALIZABLE TRANSACTION SUPPORTED THROUGH STORED PROCEDURE

            DateTime lastModifiedDate = modifiedAccountInfo.ActionDate;


            try {

                // NO VALIDATION REQUIRED

                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("dbo.MemberCase_DeleteProblemStatement");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseProblemCarePlanId", memberCaseProblemCarePlanId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ignoreAssignedTo", false);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityId", application.Session.SecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", application.Session.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", application.Session.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", application.Session.UserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@lastModifiedDate", lastModifiedDate);


                // RETURNED MODIFIED DATE 

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedDate", ((DateTime?)null));

                ((System.Data.IDataParameter)sqlCommand.Parameters["@modifiedDate"]).Direction = System.Data.ParameterDirection.Output;


                // RETURN VALUE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@RETURN", ((Int32)0));

                ((System.Data.IDataParameter)sqlCommand.Parameters["@RETURN"]).Direction = System.Data.ParameterDirection.ReturnValue;


                sqlCommand.ExecuteNonQuery ();

                outcome = (Enumerations.MemberCaseActionOutcome)(Convert.ToInt32 (((System.Data.IDataParameter)sqlCommand.Parameters["@RETURN"]).Value));

                SetApplicationException (outcome);

                if (outcome == Enumerations.MemberCaseActionOutcome.Success) { Load (id); } // ON SUCCESS - RELOAD OBJECT

            }

            catch (Exception applicationException) {

                outcome = Enumerations.MemberCaseActionOutcome.UnknownError;

                base.application.SetLastException (applicationException);

            }

            return outcome;

        }        

        public MemberCaseCarePlan FindMemberCaseCarePlan (Int64 memberCaseCarePlanId) {

            MemberCaseCarePlan memberCaseCarePlan = null;


            foreach (MemberCaseCarePlan currentCarePlan in CarePlans) {

                if (currentCarePlan.Id == memberCaseCarePlanId) {

                    memberCaseCarePlan = currentCarePlan;

                    break;

                }

            }


            return memberCaseCarePlan;

        }

        public MemberCaseCarePlan FindMemberCaseCarePlanByGoalId (Int64 memberCaseCarePlanGoalId) {

            MemberCaseCarePlan memberCaseCarePlan = null;


            foreach (MemberCaseCarePlan currentCarePlan in CarePlans) {

                if (currentCarePlan.ContainsGoal (memberCaseCarePlanGoalId)) {

                    memberCaseCarePlan = currentCarePlan;

                    break;

                }

            }


            return memberCaseCarePlan;

        }

        public MemberCaseCarePlanGoal FindMemberCaseCarePlanGoal (Int64 memberCaseCarePlanGoalId) {

            MemberCaseCarePlanGoal memberCaseCarePlanGoal = null;


            foreach (MemberCaseCarePlan currentCarePlan in CarePlans) {

                if (currentCarePlan.ContainsGoal (memberCaseCarePlanGoalId)) {

                    memberCaseCarePlanGoal = currentCarePlan.Goal (memberCaseCarePlanGoalId);

                    break;

                }

            }


            return memberCaseCarePlanGoal;

        }
        
        #endregion 

    }

}
