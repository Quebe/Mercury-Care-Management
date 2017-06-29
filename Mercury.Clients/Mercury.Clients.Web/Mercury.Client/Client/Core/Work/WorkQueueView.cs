using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Work {

    [Serializable]
    public class WorkQueueView : CoreConfigurationObject {

        #region Private Properties

        private List<Server.Application.WorkQueueViewFieldDefinition> fieldDefinitions = new List<Server.Application.WorkQueueViewFieldDefinition> ();

        private SortedList<Int32, Server.Application.WorkQueueViewFilterDefinition> filterDefinitions = new SortedList<Int32, Server.Application.WorkQueueViewFilterDefinition> ();

        private SortedList<Int32, Server.Application.WorkQueueViewSortDefinition> sortDefinitions = new SortedList<Int32, Server.Application.WorkQueueViewSortDefinition> ();

        #endregion


        #region Public Properties

        public List<Server.Application.WorkQueueViewFieldDefinition> FieldDefinitions { get { return fieldDefinitions; } set { fieldDefinitions = value; } }

        public SortedList<Int32, Server.Application.WorkQueueViewFilterDefinition> FilterDefinitions { get { return filterDefinitions; } set { filterDefinitions = value; } }

        public SortedList<Int32, Server.Application.WorkQueueViewSortDefinition> SortDefinitions { get { return sortDefinitions; } set { sortDefinitions = value; } }

        public Dictionary<String, Boolean> WellKnownFields {

            get {

                Dictionary<String, Boolean> wellKnownFields = new Dictionary<String, Boolean> ();

                wellKnownFields.Add ("WorkQueueItemId", true);

                wellKnownFields.Add ("WorkQueueId", false);

                wellKnownFields.Add ("ObjectType", true);

                wellKnownFields.Add ("ObjectId", false);

                wellKnownFields.Add ("ItemDescription", true);

                wellKnownFields.Add ("ItemGroupKey", true);

                wellKnownFields.Add ("WorkflowInstanceId", false);

                wellKnownFields.Add ("WorkflowStatus", false);

                wellKnownFields.Add ("WorkflowLastStep", true);

                wellKnownFields.Add ("WorkflowNextStep", true);

                wellKnownFields.Add ("AddedDate", true);

                wellKnownFields.Add ("LastWorkedDate", true);

                wellKnownFields.Add ("ConstraintDate", true);

                wellKnownFields.Add ("MilestoneDate", true);

                wellKnownFields.Add ("ThresholdDate", true);

                wellKnownFields.Add ("DueDate", true);

                wellKnownFields.Add ("CompletionDate", true);

                wellKnownFields.Add ("WorkOutcomeId", false);

                wellKnownFields.Add ("Priority", true);

                wellKnownFields.Add ("AssignedToSecurityAuthorityId", false);

                wellKnownFields.Add ("AssignedToUserAccountId", false);

                wellKnownFields.Add ("AssignedToUserAccountName", false);

                wellKnownFields.Add ("AssignedToUserDisplayName", true);

                wellKnownFields.Add ("AssignedToDate", true);

                wellKnownFields.Add ("ExtendedProperties", false);


                wellKnownFields.Add ("CreateAuthorityName", false);

                wellKnownFields.Add ("CreateAccountId", false);

                wellKnownFields.Add ("CreateAccountName", false);

                wellKnownFields.Add ("CreateDate", false);


                wellKnownFields.Add ("ModifiedAuthorityName", false);

                wellKnownFields.Add ("ModifiedAccountId", false);

                wellKnownFields.Add ("ModifiedAccountName", false);

                wellKnownFields.Add ("ModifiedDate", false);


                wellKnownFields.Add ("IsAssigned", true);

                wellKnownFields.Add ("IsCompleted", true);

                wellKnownFields.Add ("HasConstraintDatePassed", true);

                wellKnownFields.Add ("HasMilestoneDatePassed", true);

                wellKnownFields.Add ("HasThresholdDatePassed", true);

                wellKnownFields.Add ("HasDueDatePassed", true);

                wellKnownFields.Add ("WithinWorkTimeRestrictions", true);


                return wellKnownFields;

            }

        }

        #endregion


        #region Constructors

        public WorkQueueView (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public WorkQueueView (Application applicationReference, Server.Application.WorkQueueView serverWorkQueueView) {

            BaseConstructor (applicationReference, serverWorkQueueView);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.WorkQueueView serverWorkQueueView) {

            base.BaseConstructor (applicationReference, serverWorkQueueView);


            // COPY FIELD DEFINITIONS (NOT REFERENCE)

            fieldDefinitions = new List<Server.Application.WorkQueueViewFieldDefinition> ();

            foreach (Server.Application.WorkQueueViewFieldDefinition currentFieldDefinition in serverWorkQueueView.FieldDefinitions) {

                fieldDefinitions.Add (application.CopyWorkQueueViewFieldDefinition (currentFieldDefinition));

            }

            // COPY FILTER DEFINITIONS (NOT REFERENCE)

            filterDefinitions = new SortedList<Int32, Server.Application.WorkQueueViewFilterDefinition> ();

            foreach (Int32 currentSequence in serverWorkQueueView.FilterDefinitions.Keys) {

                filterDefinitions.Add (currentSequence, application.CopyWorkQueueViewFilterDefinition (serverWorkQueueView.FilterDefinitions[currentSequence]));

            }
            

            // COPY SORT DEFINITIONS (NOT REFERENCE)

            sortDefinitions = new SortedList<Int32, Server.Application.WorkQueueViewSortDefinition> ();

            foreach (Int32 currentSequence in serverWorkQueueView.SortDefinitions.Keys) { 

                sortDefinitions.Add (currentSequence, application.CopyWorkQueueViewSortDefinition (serverWorkQueueView.SortDefinitions [currentSequence]));

            }
            

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.WorkQueueView serverWorkQueueView) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverWorkQueueView);


            // COPY, DON'T REFERENCE

            serverWorkQueueView.FieldDefinitions = new Server.Application.WorkQueueViewFieldDefinition[fieldDefinitions.Count];

            Int32 currentDefinitionIndex = 0;

            foreach (Server.Application.WorkQueueViewFieldDefinition currentDefinition in fieldDefinitions) {

                serverWorkQueueView.FieldDefinitions[currentDefinitionIndex] = application.CopyWorkQueueViewFieldDefinition (currentDefinition);

                currentDefinitionIndex = currentDefinitionIndex + 1;

            }


            // COPY, DON'T REFERENCE

            serverWorkQueueView.FilterDefinitions = new Dictionary<int, Server.Application.WorkQueueViewFilterDefinition> ();

            foreach (Int32 currentSequence in filterDefinitions.Keys) {

                serverWorkQueueView.FilterDefinitions.Add (currentSequence, application.CopyWorkQueueViewFilterDefinition (filterDefinitions[currentSequence]));

            }


            // COPY, DON'T REFERENCE

            serverWorkQueueView.SortDefinitions = new Dictionary<int, Server.Application.WorkQueueViewSortDefinition> ();

            foreach (Int32 currentSequence in sortDefinitions.Keys) {

                serverWorkQueueView.SortDefinitions.Add (currentSequence, application.CopyWorkQueueViewSortDefinition (sortDefinitions[currentSequence]));

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.WorkQueueView serverWorkQueueView = new Server.Application.WorkQueueView ();

            MapToServerObject (serverWorkQueueView);

            return serverWorkQueueView;

        }

        public WorkQueueView Copy () {

            Server.Application.WorkQueueView serverWorkQueueView = (Server.Application.WorkQueueView)ToServerObject ();

            WorkQueueView copiedWorkQueueView = new WorkQueueView (application, serverWorkQueueView);

            return copiedWorkQueueView;

        }

        public Boolean IsEqual (WorkQueueView compareWorkQueueView) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareWorkQueueView);

            
            if (fieldDefinitions.Count != compareWorkQueueView.FieldDefinitions.Count) { isEqual = false; }

            else {

                for (Int32 currentIndex = 0; currentIndex < fieldDefinitions.Count; currentIndex++) {

                    isEqual = isEqual && (fieldDefinitions[currentIndex].DisplayName == compareWorkQueueView.FieldDefinitions[currentIndex].DisplayName);

                    isEqual = isEqual && (fieldDefinitions[currentIndex].PropertyName == compareWorkQueueView.FieldDefinitions[currentIndex].PropertyName);

                    isEqual = isEqual && (fieldDefinitions[currentIndex].DataType == compareWorkQueueView.FieldDefinitions[currentIndex].DataType);

                    isEqual = isEqual && (fieldDefinitions[currentIndex].DefaultValue == compareWorkQueueView.FieldDefinitions[currentIndex].DefaultValue);

                    if (!isEqual) { break; }

                }

            }


            if (filterDefinitions.Count != compareWorkQueueView.FilterDefinitions.Count) { isEqual = false; }

            else {

                for (Int32 currentIndex = 0; currentIndex < filterDefinitions.Count; currentIndex++) {

                    isEqual = isEqual && (filterDefinitions.Values[currentIndex].PropertyPath == compareWorkQueueView.FilterDefinitions.Values[currentIndex].PropertyPath);

                    isEqual = isEqual && (filterDefinitions.Values[currentIndex].Operator  == compareWorkQueueView.FilterDefinitions.Values[currentIndex].Operator);

                    isEqual = isEqual && (filterDefinitions.Values[currentIndex].Parameter.Value.ToString () == compareWorkQueueView.FilterDefinitions.Values[currentIndex].Parameter.Value.ToString ());

                    if (!isEqual) { break; }

                }

            }

            if (sortDefinitions.Count != compareWorkQueueView.SortDefinitions.Count) { isEqual = false; }

            else {

                for (Int32 currentIndex = 0; currentIndex < sortDefinitions.Count; currentIndex++) {

                    isEqual = isEqual && (sortDefinitions.Values[currentIndex].FieldName == compareWorkQueueView.SortDefinitions.Values[currentIndex].FieldName);

                    isEqual = isEqual && (sortDefinitions.Values[currentIndex].SortDirection == compareWorkQueueView.SortDefinitions.Values[currentIndex].SortDirection);

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }

        #endregion

    }

}

