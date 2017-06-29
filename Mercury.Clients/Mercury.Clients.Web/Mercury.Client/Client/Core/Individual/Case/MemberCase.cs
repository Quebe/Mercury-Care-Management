using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCase : CoreExtensibleObject {

        #region Private Properties

        private Int64 memberId;

        private String referenceNumber = String.Empty;

        private Server.Application.CaseItemStatus status = Server.Application.CaseItemStatus.UnderDevelopment;


        private Int64 assignedToWorkTeamId;

        private DateTime? assignedToWorkTeamDate;


        private Int64 assignedToSecurityAuthorityId;

        private String assignedToUserAccountId;

        private String assignedToUserAccountName;

        private String assignedToUserDisplayName;

        private DateTime? assignedToDate;


        private Int64 lockedBySecurityAuthorityId;

        private String lockedByUserAccountId;

        private String lockedByUserAccountName;

        private String lockedByUserDisplayName;

        private DateTime? lockedByDate;


        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);


        private List<MemberCaseProblemClass> problemClasses = new List<MemberCaseProblemClass> ();

        private List<MemberCaseCarePlan> carePlans = new List<MemberCaseCarePlan> ();

        private List<MemberCaseCareIntervention> careInterventions = new List<MemberCaseCareIntervention> ();

        #endregion


        #region Public Properties - Encapsulated

        public override string Description { get { return base.Description; } set { description = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description8000); } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public String ReferenceNumber { get { return referenceNumber; } }

        public Server.Application.CaseItemStatus Status { get { return status; } set { status = value; } }


        public Int64 AssignedToWorkTeamId { get { return assignedToWorkTeamId; } set { assignedToWorkTeamId = value; } }

        public DateTime? AssignedToWorkTeamDate { get { return assignedToWorkTeamDate; } set { assignedToWorkTeamDate = value; } }


        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }

        public Boolean HasWorkTeamAssignment { get { return (assignedToWorkTeamId != 0); } }

        public Boolean HasAssignment { get { return (assignedToSecurityAuthorityId != 0); } }

        public Boolean AssignedToThisSession {

            get {

                if (assignedToSecurityAuthorityId == 0) { return false; }

                if ((assignedToSecurityAuthorityId == application.Session.SecurityAuthorityId)

                    && (assignedToUserAccountId == application.Session.UserAccountId)) {

                    return true;

                }

                return false;

            }

        }

        public Boolean AssignedToThisSessionTeam {

            get {

                if (assignedToWorkTeamId == 0) { return false; }

                foreach (Client.Core.Work.WorkTeam currentTeam in application.WorkTeamsForSession (true)) {

                    if (currentTeam.Id == assignedToWorkTeamId) { return true; }

                }

                return false;

            }

        }

        public Boolean AssignedToThisSessionTeamManager {

            get {

                if (assignedToWorkTeamId == 0) { return false; }

                foreach (Client.Core.Work.WorkTeam currentTeam in application.WorkTeamsForSession (true)) {

                    if (currentTeam.Id == assignedToWorkTeamId) {

                        return (currentTeam.MembershipGet (application.Session.SecurityAuthorityId, application.Session.UserAccountId).WorkTeamRole == Server.Application.WorkTeamRole.Manager);

                    }

                }

                return false;

            }

        }

        public Int64 LockedBySecurityAuthorityId { get { return lockedBySecurityAuthorityId; } set { lockedBySecurityAuthorityId = value; } }

        public String LockedByUserAccountId { get { return lockedByUserAccountId; } set { lockedByUserAccountId = value; } }

        public String LockedByUserAccountName { get { return lockedByUserAccountName; } set { lockedByUserAccountName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String LockedByUserDisplayName { get { return lockedByUserDisplayName; } set { lockedByUserDisplayName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public DateTime? LockedByDate { get { return lockedByDate; } set { lockedByDate = value; } }

        public Boolean LockedByThisSession {

            get {

                if (lockedBySecurityAuthorityId == 0) { return false; }

                if ((lockedBySecurityAuthorityId == application.Session.SecurityAuthorityId)

                    && (lockedByUserAccountId == application.Session.UserAccountId)) {

                    return true;

                }

                return false;

            }

        }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public String EffectiveDateDescription { get { return ((status != Server.Application.CaseItemStatus.UnderDevelopment) ? effectiveDate.ToString ("MM/dd/yyyy") : String.Empty); } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String TerminationDateDescription { get { return ((status != Server.Application.CaseItemStatus.UnderDevelopment) ? ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")) : String.Empty); } }


        public List<MemberCaseProblemClass> ProblemClasses { get { return problemClasses; } set { problemClasses = value; } }

        public Dictionary<Int64, ProblemStatement> ProblemStatementsActive {

            get {

                Dictionary<Int64, ProblemStatement> problemStatements = new Dictionary<Int64, ProblemStatement> ();


                foreach (MemberCaseProblemClass currentProblemClass in problemClasses) {

                    foreach (MemberCaseProblemCarePlan currentProblemCarePlan in currentProblemClass.ProblemCarePlans) {

                        MemberCaseCarePlan currentCarePlan = CarePlan (currentProblemCarePlan.MemberCaseCarePlanId);

                        if (currentCarePlan != null) {

                            if ((currentCarePlan.Status == Server.Application.CaseItemStatus.Active) || (currentCarePlan.Status == Server.Application.CaseItemStatus.UnderDevelopment)) {

                                if (!problemStatements.ContainsKey (currentProblemCarePlan.ProblemStatementId)) {

                                    problemStatements.Add (currentProblemCarePlan.ProblemStatementId, currentProblemCarePlan.ProblemStatement);

                                }

                            }

                        }

                    }


                }


                return problemStatements;

            }

        }

        public List<MemberCaseCarePlan> CarePlans { get { return carePlans; } set { carePlans = value; } }

        public List<MemberCaseCarePlan> CarePlansUnderDevelopmentActive {

            get {

                List<MemberCaseCarePlan> filteredCarePlans =

                    (from currentCarePlan in carePlans

                    where ((currentCarePlan.Status == Server.Application.CaseItemStatus.UnderDevelopment)

                        || (currentCarePlan.Status == Server.Application.CaseItemStatus.Active))

                    orderby currentCarePlan.Name

                    select currentCarePlan).ToList ();


                return filteredCarePlans; 
            
            }

        }

        public List<MemberCaseCareIntervention> CareInterventions { get { return careInterventions; } set { careInterventions = value; } }

        #endregion


        #region Public Properties

        public Member.Member Member { get { return application.MemberGet (memberId, true); } }

        public Core.Work.WorkTeam AssignedToWorkTeam { get { return application.WorkTeamGet (assignedToWorkTeamId, true); } }

        public String AssignedToWorkTeamName { get { return ((AssignedToWorkTeam != null) ? AssignedToWorkTeam.Name : String.Empty); } }

        public Boolean IsReadOnly { get { return ((status != Server.Application.CaseItemStatus.Active) && (status != Server.Application.CaseItemStatus.UnderDevelopment)); } }

        public String StatusDescription { get { return Server.CommonFunctions.EnumerationToString (status); } }

        #endregion


        #region Constructors

        public MemberCase (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCase (Application applicationReference, Mercury.Server.Application.MemberCase serverObject) {

            BaseConstructor (applicationReference, serverObject);


            memberId = serverObject.MemberId;

            referenceNumber = serverObject.ReferenceNumber;

            status = serverObject.Status;


            assignedToWorkTeamId = serverObject.AssignedToWorkTeamId;

            assignedToWorkTeamDate = serverObject.AssignedToWorkTeamDate;


            assignedToSecurityAuthorityId = serverObject.AssignedToSecurityAuthorityId;

            assignedToUserAccountId = serverObject.AssignedToUserAccountId;

            assignedToUserAccountName = serverObject.AssignedToUserAccountName;

            assignedToUserDisplayName = serverObject.AssignedToUserDisplayName;

            assignedToDate = serverObject.AssignedToDate;


            lockedBySecurityAuthorityId = serverObject.LockedBySecurityAuthorityId;

            lockedByUserAccountId = serverObject.LockedByUserAccountId;

            lockedByUserAccountName = serverObject.LockedByUserAccountName;

            lockedByUserDisplayName = serverObject.LockedByUserDisplayName;

            lockedByDate = serverObject.LockedByDate;


            effectiveDate = serverObject.EffectiveDate;

            terminationDate = serverObject.TerminationDate;


            problemClasses.Clear ();

            foreach (Server.Application.MemberCaseProblemClass currentServerProblemClass in serverObject.ProblemClasses) {

                MemberCaseProblemClass problemClass = new MemberCaseProblemClass (Application, currentServerProblemClass);

                problemClass.MemberCase = this;

                problemClasses.Add (problemClass);

            }

            carePlans.Clear ();

            foreach (Server.Application.MemberCaseCarePlan currentServerCarePlan in serverObject.CarePlans) {

                MemberCaseCarePlan carePlan = new MemberCaseCarePlan (Application, currentServerCarePlan);

                carePlan.MemberCase = this;

                carePlans.Add (carePlan);

            }

            careInterventions.Clear ();

            foreach (Server.Application.MemberCaseCareIntervention currentServerCareIntervention in serverObject.CareInterventions) {

                MemberCaseCareIntervention careIntervention = new MemberCaseCareIntervention (Application, currentServerCareIntervention);

                careIntervention.MemberCase = this;

                careInterventions.Add (careIntervention);

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.MemberCase serverObject) {

            base.MapToServerObject ((Server.Application.CoreExtensibleObject)serverObject);


            serverObject.MemberId = memberId;

            serverObject.ReferenceNumber = referenceNumber;

            serverObject.Status = status;


            serverObject.AssignedToSecurityAuthorityId = AssignedToSecurityAuthorityId;

            serverObject.AssignedToUserAccountId = AssignedToUserAccountId;

            serverObject.AssignedToUserAccountName = AssignedToUserAccountName;

            serverObject.AssignedToUserDisplayName = AssignedToUserDisplayName;

            serverObject.AssignedToDate = AssignedToDate;


            serverObject.LockedBySecurityAuthorityId = LockedBySecurityAuthorityId;

            serverObject.LockedByUserAccountId = LockedByUserAccountId;

            serverObject.LockedByUserAccountName = LockedByUserAccountName;

            serverObject.LockedByUserDisplayName = LockedByUserDisplayName;

            serverObject.LockedByDate = LockedByDate;


            serverObject.EffectiveDate = effectiveDate;

            serverObject.TerminationDate = terminationDate;



            serverObject.CarePlans = new Server.Application.MemberCaseCarePlan[carePlans.Count];

            foreach (MemberCaseCarePlan currentCarePlan in carePlans) {

                Server.Application.MemberCaseCarePlan serverCarePlan = (Server.Application.MemberCaseCarePlan)currentCarePlan.ToServerObject ();

                serverObject.CarePlans[carePlans.IndexOf (currentCarePlan)] = serverCarePlan;

            }


            serverObject.CareInterventions = new Server.Application.MemberCaseCareIntervention[careInterventions.Count];

            foreach (MemberCaseCareIntervention currentCareIntervention in careInterventions) {

                Server.Application.MemberCaseCareIntervention serverCareIntervention = (Server.Application.MemberCaseCareIntervention)currentCareIntervention.ToServerObject ();

                serverObject.CareInterventions[careInterventions.IndexOf (currentCareIntervention)] = serverCareIntervention;

            }

            serverObject.ProblemClasses = new Server.Application.MemberCaseProblemClass[problemClasses.Count];

            foreach (MemberCaseProblemClass currentProblemClass in problemClasses) {

                Server.Application.MemberCaseProblemClass serverProblemClass = (Server.Application.MemberCaseProblemClass)currentProblemClass.ToServerObject ();

                serverObject.ProblemClasses[problemClasses.IndexOf (currentProblemClass)] = serverProblemClass;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCase serverObject = new Server.Application.MemberCase ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCase Copy () {

            Server.Application.MemberCase serverObject = (Server.Application.MemberCase)ToServerObject ();

            MemberCase copiedObject = new MemberCase (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCase compareObject) {

            Boolean isEqual = base.IsEqual ((CoreExtensibleObject)compareObject);


            isEqual &= (memberId == compareObject.MemberId);

            isEqual &= (referenceNumber == compareObject.ReferenceNumber);

            isEqual &= (status == compareObject.Status);


            // TODO: 



            isEqual &= (effectiveDate == compareObject.EffectiveDate);

            isEqual &= (terminationDate == compareObject.TerminationDate);


            return isEqual;

        }

        #endregion


        #region Public Methods

        public MemberCaseCarePlan CarePlan (Int64 forCarePlanId) {

            MemberCaseCarePlan carePlan = null;

            foreach (MemberCaseCarePlan currentCarePlan in carePlans) {

                if (currentCarePlan.Id == forCarePlanId) {

                    carePlan = currentCarePlan;

                    break;

                }

            }

            return carePlan;

        }

        public MemberCaseCareIntervention CareIntervention (Int64 forCareInterventionId) {

            MemberCaseCareIntervention careIntervention = null;

            foreach (MemberCaseCareIntervention currentCareIntervention in careInterventions) {

                if (currentCareIntervention.Id == forCareInterventionId) {

                    careIntervention = currentCareIntervention;

                    break;

                }

            }

            return careIntervention;

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
