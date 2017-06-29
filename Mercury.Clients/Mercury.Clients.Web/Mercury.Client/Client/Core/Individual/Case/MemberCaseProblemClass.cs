using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseProblemClass : CoreExtensibleObject {

        #region Private Properties

        private Int64 memberCaseId;

        private MemberCase memberCase = null;


        private Int64 problemClassId;


        private Int64 assignedToSecurityAuthorityId;

        private String assignedToUserAccountId;

        private String assignedToUserAccountName;

        private String assignedToUserDisplayName;

        private DateTime? assignedToDate = null;

        private Int64 assignedToProviderId;

        private DateTime? assignedToProviderDate = null;


        private List<MemberCaseProblemCarePlan> problemCarePlans = new List<MemberCaseProblemCarePlan> ();

        #endregion


        #region Public Properties

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public MemberCase MemberCase { get { return memberCase; } set { memberCase = value; } }

        public Int64 ProblemClassId { get { return problemClassId; } set { problemClassId = value; } }


        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }


        public Int64 AssignedToProviderId { get { return assignedToProviderId; } set { assignedToProviderId = value; } }

        public DateTime? AssignedToProviderDate { get { return assignedToProviderDate; } set { assignedToProviderDate = value; } }


        public List<MemberCaseProblemCarePlan> ProblemCarePlans { get { return problemCarePlans; } set { problemCarePlans = value; } }

        #endregion 

        
        #region Public Properties

        public Server.Application.ProblemDomain ProblemDomain { get { return ((ProblemClass != null) ? application.ProblemDomainGet (ProblemClass.ProblemDomainId, true) : null); } }

        public Server.Application.ProblemClass ProblemClass { get { return application.ProblemClassGet (problemClassId, true); } }

        public String ProblemClassName { get { return ((ProblemClass != null) ? ProblemClass.Name : String.Empty); } }

        public String Classification {

            get {

                String classification = (ProblemDomain != null) ? ProblemDomain.Name : String.Empty;

                if (!String.IsNullOrWhiteSpace (ProblemClassName)) { classification += " - " + ProblemClassName; }

                return classification;

            }

        }

        public List<MemberCaseCarePlan> CarePlans {

            get {

                List<MemberCaseCarePlan> carePlans = new List<MemberCaseCarePlan> ();


                foreach (MemberCaseProblemCarePlan currentProblemCarePlan in problemCarePlans) {

                    MemberCaseCarePlan carePlan = MemberCase.CarePlan (currentProblemCarePlan.MemberCaseCarePlanId);

                    if (carePlan != null) { carePlans.Add (carePlan); }

                }


                var carePlansSorted =

                    from currentCarePlan in carePlans

                    orderby currentCarePlan.Name, currentCarePlan.Id

                    select currentCarePlan;


                carePlans = carePlansSorted.ToList ();

                return carePlans;

            }

        }

        public Boolean HasActiveCarePlans {

            get {


                foreach (MemberCaseProblemCarePlan currentProblemCarePlan in problemCarePlans) {

                    if (currentProblemCarePlan.MemberCaseCarePlan.Status == Server.Application.CaseItemStatus.UnderDevelopment) { return true; }

                    if (currentProblemCarePlan.MemberCaseCarePlan.Status == Server.Application.CaseItemStatus.Active) { return true; }

                }

                return false; 

            }

        }

        public Client.Core.Provider.Provider AssignedToProvider { get { return application.ProviderGet (assignedToProviderId, true); } }

        #endregion 


        #region Constructors

        public MemberCaseProblemClass (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseProblemClass (Application applicationReference, Mercury.Server.Application.MemberCaseProblemClass serverObject) {

            BaseConstructor (applicationReference, serverObject);


            memberCaseId = serverObject.MemberCaseId;

            problemClassId = serverObject.ProblemClassId;

            
            assignedToSecurityAuthorityId = serverObject.AssignedToSecurityAuthorityId;

            assignedToUserAccountId = serverObject.AssignedToUserAccountId;

            assignedToUserAccountName = serverObject.AssignedToUserAccountName;

            assignedToUserDisplayName = serverObject.AssignedToUserDisplayName;

            assignedToDate = serverObject.AssignedToDate;


            assignedToProviderId = serverObject.AssignedToProviderId;

            assignedToProviderDate = serverObject.AssignedToProviderDate;


            problemCarePlans.Clear ();

            foreach (Server.Application.MemberCaseProblemCarePlan currentServerProblemCarePlan in serverObject.ProblemCarePlans) {

                MemberCaseProblemCarePlan problemCarePlan = new MemberCaseProblemCarePlan (Application, currentServerProblemCarePlan);

                problemCarePlan.MemberCaseProblemClass = this;

                problemCarePlans.Add (problemCarePlan);

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.MemberCaseProblemClass serverObject) {

            base.MapToServerObject ((Server.Application.CoreExtensibleObject)serverObject);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseProblemClass serverObject = new Server.Application.MemberCaseProblemClass ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCaseProblemClass Copy () {

            Server.Application.MemberCaseProblemClass serverObject = (Server.Application.MemberCaseProblemClass)ToServerObject ();

            MemberCaseProblemClass copiedObject = new MemberCaseProblemClass (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseProblemClass compareObject) {

            Boolean isEqual = base.IsEqual ((CoreExtensibleObject)compareObject);


            
            return isEqual;

        }

        #endregion 

    }

}
