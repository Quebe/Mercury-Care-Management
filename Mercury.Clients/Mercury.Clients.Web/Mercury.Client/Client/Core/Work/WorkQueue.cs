using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Work {

    [Serializable]
    public class WorkQueue : CoreConfigurationObject {

        #region Private Properties

        private Int64 workflowId;


        private Int32 scheduleValue;

        private Server.Application.DateQualifier scheduleQualifier = Server.Application.DateQualifier.Days;

        private Int32 thresholdValue;

        private Server.Application.DateQualifier thresholdQualifier = Mercury.Server.Application.DateQualifier.Days;

        private Int32 initialConstraintValue;

        private Server.Application.DateQualifier initialConstraintQualifier = Mercury.Server.Application.DateQualifier.Days;

        private Int32 initialMilestoneValue;

        private Server.Application.DateQualifier initialMilestoneQualifier = Mercury.Server.Application.DateQualifier.Days;


        private Int64 getWorkViewId = 0;

        private Boolean getWorkUseGrouping = false;

        private List<Server.Application.WorkQueueGetWorkUserView> getWorkUserViews = new List<Server.Application.WorkQueueGetWorkUserView> ();


        private List<Server.Application.WorkQueueTeam> workTeams = new List<Server.Application.WorkQueueTeam> ();
        
        #endregion


        #region Public Properties

        public Int64 WorkflowId { get { return workflowId; } set { workflowId = value; } }


        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Server.Application.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public String ScheduleDescription { get { return scheduleValue.ToString () + " " + scheduleQualifier.ToString (); } }


        public Int32 ThresholdValue { get { return thresholdValue; } set { thresholdValue = value; } }

        public Server.Application.DateQualifier ThresholdQualifier { get { return thresholdQualifier; } set { thresholdQualifier = value; } }

        public String ThresholdDescription { get { return thresholdValue.ToString () + " " + thresholdQualifier.ToString (); } }

        
        public Int32 InitialConstraintValue { get { return initialConstraintValue; } set { initialConstraintValue = value; } }

        public Server.Application.DateQualifier InitialConstraintQualifier { get { return initialConstraintQualifier; } set { initialConstraintQualifier = value; } }

        public String InitialConstraintDescription { get { return initialConstraintValue.ToString () + " " + initialConstraintQualifier.ToString (); } }
        
        
        public Int32 InitialMilestoneValue { get { return initialMilestoneValue; } set { initialMilestoneValue = value; } }

        public Server.Application.DateQualifier InitialMilestoneQualifier { get { return initialMilestoneQualifier; } set { initialMilestoneQualifier = value; } }

        public String InitialMilestoneDescription { get { return initialMilestoneValue.ToString () + " " + initialMilestoneQualifier.ToString (); } }



        public Int64 GetWorkViewId { get { return getWorkViewId; } set { getWorkViewId = value; } }

        public WorkQueueView GetWorkView { get { return Application.WorkQueueViewGet (getWorkViewId, true); } }

        public Boolean GetWorkUseGrouping { get { return getWorkUseGrouping; } set { getWorkUseGrouping = value; } }

        public List<Server.Application.WorkQueueGetWorkUserView> GetWorkUserViews { get { return getWorkUserViews; } set { getWorkUserViews = value; } }


        public List<Server.Application.WorkQueueTeam> WorkTeams { get { return workTeams; } set { workTeams = value; } }



        public String WorkflowName {

            get {

                Workflow workflow = application.WorkflowGet (workflowId, true);

                String workflowName = (workflow != null) ? workflow.Name : String.Empty;

                return workflowName;

            }

        }

        public Workflow Workflow { get { return application.WorkflowGet (workflowId, true); } }


        //public WorkQueueView GetWorkView {

        //    get {

        //        if (getWorkView != null) { return getWorkView; }

        //        if (application == null) { return null; }

        //        getWorkView = application.WorkQueueViewGet (getWorkViewId);

        //        return getWorkView;

        //    }

        //}


        //public String GetWorkViewName { get { return (GetWorkView != null) ? GetWorkView.Name : String.Empty; } }

        //public override Application Application {

        //    set {

        //        base.Application = value;

        //        // PROPOGATE: SET ALL CHILD REFERENCES

        //        foreach (WorkQueueTeam currentWorkTeam in workTeams) { currentWorkTeam.Application = value; }

        //    }

        //}


        #endregion


        #region Constructors

        public WorkQueue (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public WorkQueue (Application applicationReference, Server.Application.WorkQueue serverWorkQueue) {

            BaseConstructor (applicationReference, serverWorkQueue);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.WorkQueue serverWorkQueue) {

            base.BaseConstructor (applicationReference, serverWorkQueue);


            workflowId = serverWorkQueue.WorkflowId;


            scheduleValue = serverWorkQueue.ScheduleValue;

            scheduleQualifier = serverWorkQueue.ScheduleQualifier;

            thresholdValue = serverWorkQueue.ThresholdValue;

            thresholdQualifier = serverWorkQueue.ThresholdQualifier;

            initialConstraintValue = serverWorkQueue.InitialConstraintValue;

            initialConstraintQualifier = serverWorkQueue.InitialConstraintQualifier;

            initialMilestoneValue = serverWorkQueue.InitialMilestoneValue;

            initialMilestoneQualifier = serverWorkQueue.InitialMilestoneQualifier;


            getWorkViewId = serverWorkQueue.GetWorkViewId;

            getWorkUseGrouping = serverWorkQueue.GetWorkUseGrouping;

            getWorkUserViews = new List<Server.Application.WorkQueueGetWorkUserView> ();

            if (serverWorkQueue.GetWorkUserViews != null) {

                foreach (Server.Application.WorkQueueGetWorkUserView currentUserView in serverWorkQueue.GetWorkUserViews) {

                    getWorkUserViews.Add (application.CopyWorkQueueGetWorkUserView (currentUserView));

                }

            }


            // COPY WORK QUEUE TEAMS (NOT REFERENCE)

            workTeams = new List<Server.Application.WorkQueueTeam> ();

            foreach (Server.Application.WorkQueueTeam currentWorkQueueTeam in serverWorkQueue.WorkTeams) {

                workTeams.Add (application.CopyWorkQueueTeam (currentWorkQueueTeam));

            }


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.WorkQueue serverWorkQueue) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverWorkQueue);


            serverWorkQueue.WorkflowId = workflowId;



            serverWorkQueue.ScheduleValue = scheduleValue;

            serverWorkQueue.ScheduleQualifier = scheduleQualifier;

            serverWorkQueue.ThresholdValue = thresholdValue;

            serverWorkQueue.ThresholdQualifier = thresholdQualifier;

            serverWorkQueue.InitialConstraintValue = initialConstraintValue;

            serverWorkQueue.InitialConstraintQualifier = initialConstraintQualifier;

            serverWorkQueue.InitialMilestoneValue = initialMilestoneValue;
            
            serverWorkQueue.InitialMilestoneQualifier = initialMilestoneQualifier;


            serverWorkQueue.GetWorkViewId = getWorkViewId;

            serverWorkQueue.GetWorkUseGrouping = getWorkUseGrouping;


            // COPY, DON'T REFERENCE

            serverWorkQueue.GetWorkUserViews = new Server.Application.WorkQueueGetWorkUserView[getWorkUserViews.Count];

            Int32 currentUserViewIndex = 0;

            foreach (Server.Application.WorkQueueGetWorkUserView currentUserView in getWorkUserViews) {

                serverWorkQueue.GetWorkUserViews[currentUserViewIndex] = application.CopyWorkQueueGetWorkUserView (currentUserView);

                currentUserViewIndex = currentUserViewIndex + 1;

            }


            // COPY, DON'T REFERENCE

            serverWorkQueue.WorkTeams = new Server.Application.WorkQueueTeam[workTeams.Count];

            Int32 currentWorkTeamIndex = 0;

            foreach (Server.Application.WorkQueueTeam currentWorkTeam in workTeams) {

                serverWorkQueue.WorkTeams[currentWorkTeamIndex] = application.CopyWorkQueueTeam (currentWorkTeam);

                currentWorkTeamIndex = currentWorkTeamIndex + 1;

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.WorkQueue serverWorkQueue = new Server.Application.WorkQueue ();

            MapToServerObject (serverWorkQueue);

            return serverWorkQueue;

        }

        public WorkQueue Copy () {

            Server.Application.WorkQueue serverWorkQueue = (Server.Application.WorkQueue)ToServerObject ();

            WorkQueue copiedWorkQueue = new WorkQueue (application, serverWorkQueue);

            return copiedWorkQueue;

        }

        public Boolean IsEqual (WorkQueue compareWorkQueue) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareWorkQueue);


            isEqual &= (workflowId == compareWorkQueue.WorkflowId);


            isEqual &= (scheduleValue == compareWorkQueue.ScheduleValue);

            isEqual &= (scheduleQualifier == compareWorkQueue.ScheduleQualifier);

            isEqual &= (thresholdValue == compareWorkQueue.ThresholdValue);

            isEqual &= (thresholdQualifier == compareWorkQueue.ThresholdQualifier);

            isEqual &= (initialConstraintValue == compareWorkQueue.InitialConstraintValue);

            isEqual &= (initialConstraintQualifier == compareWorkQueue.InitialConstraintQualifier);

            isEqual &= (initialMilestoneValue == compareWorkQueue.initialMilestoneValue);

            isEqual &= (initialMilestoneQualifier == compareWorkQueue.initialMilestoneQualifier);


            isEqual &= (getWorkViewId == compareWorkQueue.GetWorkViewId);

            isEqual &= (getWorkUseGrouping == compareWorkQueue.GetWorkUseGrouping);


            // COMPARE GET WORK USER VIEWS

            isEqual &= (getWorkUserViews.Count == compareWorkQueue.GetWorkUserViews.Count);

            if (isEqual) {

                foreach (Server.Application.WorkQueueGetWorkUserView currentUserView in getWorkUserViews) {

                    Server.Application.WorkQueueGetWorkUserView compareUserView = compareWorkQueue.GetWorkUserView (currentUserView.SecurityAuthorityId, currentUserView.UserAccountId);

                    if (compareUserView == null) { isEqual = false; break; }


                    isEqual &= (currentUserView.WorkQueueViewId == compareUserView.WorkQueueViewId);

                    if (!isEqual) { break; }

                }

            }

            // COMPARE WORK TEAMS 

            isEqual &= (workTeams.Count == compareWorkQueue.WorkTeams.Count);

            if (isEqual) {

                foreach (Server.Application.WorkQueueTeam currentWorkTeam in workTeams) {

                    Server.Application.WorkQueueTeam compareWorkTeam = compareWorkQueue.WorkTeam (currentWorkTeam.WorkTeamId);

                    if (compareWorkTeam == null) { isEqual = false; break; }


                    isEqual &= (currentWorkTeam.Permission == compareWorkTeam.Permission);

                    if (!isEqual) { break; }

                }

            }


            return isEqual;

        }


        public Boolean ContainsWorkTeam (Int64 workTeamId) {

            Boolean found = false;

            foreach (Server.Application.WorkQueueTeam currentWorkTeam in workTeams) {

                if (currentWorkTeam.WorkTeamId == workTeamId) {

                    found = true;

                    break;

                }

            }

            return found;

        }

        public Server.Application.WorkQueueTeam WorkTeam (Int64 workTeamId) {

            Server.Application.WorkQueueTeam workTeam = null;

            foreach (Server.Application.WorkQueueTeam currentWorkTeam in workTeams) {

                if (currentWorkTeam.WorkTeamId == workTeamId) {

                    workTeam = currentWorkTeam;

                    break;

                }

            }

            return workTeam;

        }

        public void AddWorkTeam (Int64 workTeamId, String workTeamName, Server.Application.WorkQueueTeamPermission permission) {

            if (!ContainsWorkTeam (workTeamId)) {

                Server.Application.WorkQueueTeam newTeam = new Server.Application.WorkQueueTeam ();

                newTeam.WorkQueueId = id;

                newTeam.WorkTeamId = workTeamId;

                newTeam.WorkTeamName = workTeamName;

                newTeam.Permission = permission;

                workTeams.Add (newTeam);

            }

            else {

                Server.Application.WorkQueueTeam workQueueTeam = WorkTeam (workTeamId);

                workQueueTeam.Permission = permission;

            }

            return;

        }


        public Boolean ContainsUserView (Int64 securityAuthorityId, String userAccountId) {

            Boolean containsUserView = false;

            foreach (Server.Application.WorkQueueGetWorkUserView currentUserView in getWorkUserViews) {

                if ((currentUserView.SecurityAuthorityId == securityAuthorityId)

                    && (currentUserView.UserAccountId == userAccountId)) {

                    containsUserView = true;

                    break;

                }

            }

            return containsUserView;

        }

        public Server.Application.WorkQueueGetWorkUserView GetWorkUserView (Int64 securityAuthorityId, String userAccountId) {

            Server.Application.WorkQueueGetWorkUserView userView = null;

            foreach (Server.Application.WorkQueueGetWorkUserView currentUserView in getWorkUserViews) {

                if ((currentUserView.SecurityAuthorityId == securityAuthorityId)

                    && (currentUserView.UserAccountId == userAccountId)) {

                    userView = currentUserView;

                    break;

                }

            }

            return userView;

        }

        public void AddUserView (Int64 securityAuthorityId, String securityAuthorityName, String userAccountId, String userAccountName, String userDisplayName, Int64 workQueueViewId) {

            if (!ContainsUserView (securityAuthorityId, userAccountId)) {

                Server.Application.WorkQueueGetWorkUserView userView = new Server.Application.WorkQueueGetWorkUserView ();

                userView.SecurityAuthorityId = securityAuthorityId;

                userView.SecurityAuthorityName = securityAuthorityName;

                userView.UserAccountId = userAccountId;

                userView.UserAccountName = userAccountName;

                userView.UserDisplayName = userDisplayName;

                userView.WorkQueueViewId = workQueueViewId;

                getWorkUserViews.Add (userView);

            }

        }

        #endregion 

    }

}

