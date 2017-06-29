using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;

namespace Mercury.Client.Core.Work {

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


        private ObservableCollection<Server.Application.WorkQueueTeam> workTeams = new ObservableCollection<Server.Application.WorkQueueTeam> ();


        private Workflow workflow = null;

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

        public Boolean GetWorkUseGrouping { get { return getWorkUseGrouping; } set { getWorkUseGrouping = value; } }


        public ObservableCollection<Server.Application.WorkQueueTeam> WorkTeams { get { return workTeams; } set { workTeams = value; } }



        public Workflow Workflow {

            get {

                if (workflow == null) {

                    GlobalProgressBarShow ("Workflow");

                    Application.WorkflowGet (workflowId, true, WorkflowGetCompleted);

                }

                return workflow;

            }

        }

        #endregion


        #region Property Data Binding Callbacks

        private void WorkflowGetCompleted (Object sender, Server.Application.WorkflowGetCompletedEventArgs e) {

            GlobalProgressBarHide ("Workflow");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                workflow = new Workflow (Application, e.Result);

                NotifyPropertyChanged ("Workflow");

            }

            return;

        }

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


            // COPY WORK QUEUE TEAMS (NOT REFERENCE)

            workTeams = new ObservableCollection<Server.Application.WorkQueueTeam> ();

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

            serverWorkQueue.WorkTeams = new ObservableCollection<Server.Application.WorkQueueTeam> ();

            foreach (Server.Application.WorkQueueTeam currentWorkTeam in workTeams) {

                serverWorkQueue.WorkTeams.Add (application.CopyWorkQueueTeam (currentWorkTeam));

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


            isEqual &= (workTeams.Count == compareWorkQueue.WorkTeams.Count);



            // COMPARE WORKFLOW PERMISSIONS

            // TODO: SILVERLIGHT UPDATE

            //if (isEqual) {

            //    foreach (Server.Application.WorkQueueTeam currentWorkTeam in workTeams) {

            //        Server.Application.WorkQueueTeam compareWorkTeam = compareWorkQueue.WorkTeam (currentWorkTeam.WorkTeamId);

            //        if (compareWorkTeam == null) { isEqual = false; break; }


            //        isEqual &= (currentWorkTeam.Permission == compareWorkTeam.Permission);

            //        if (!isEqual) { break; }

            //    }

            //}


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

        #endregion 

    }

}
