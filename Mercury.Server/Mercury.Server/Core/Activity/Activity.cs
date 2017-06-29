using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Activity {

    [DataContract (Name = "Activity")]

    [KnownType (typeof (Core.Individual.CareLevelActivity))]
    [KnownType (typeof (Core.Individual.CareInterventionActivity))]
    public class Activity : CoreObject {
        
        #region Private Properties

        [DataMember (Name = "ActivityType")]
        private Enumerations.ActivityType activityType = Enumerations.ActivityType.Manual;


        [DataMember (Name = "InitialAnchorDate")]
        private Enumerations.ActivityAnchorDate initialAnchorDate = Enumerations.ActivityAnchorDate.OwnerObjectEffectiveDate;

        [DataMember (Name = "AnchorDate")]
        private Enumerations.ActivityAnchorDate anchorDate = Enumerations.ActivityAnchorDate.ActivityCreateDate;


        [DataMember (Name = "ScheduleType")]
        private Enumerations.ActivityScheduleType scheduleType = Enumerations.ActivityScheduleType.ByFrequency;

        [DataMember (Name = "ScheduleValue")]
        private Int32 scheduleValue;

        [DataMember (Name = "ScheduleQualifier")]
        private Core.Enumerations.DateQualifier scheduleQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "ConstraintValue")]
        private Int32 constraintValue;

        [DataMember (Name = "ConstraintQualifier")]
        private Core.Enumerations.DateQualifier constraintQualifier = Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "Reoccurring")]
        private Boolean reoccurring = false;


        [DataMember (Name = "PerformActionDate")]
        private Enumerations.ActivityPerformActionDate performActionDate = Enumerations.ActivityPerformActionDate.Immediately;

        [DataMember (Name = "Action")]
        private Core.Action.Action action = null;


        [DataMember (Name = "Thresholds")]
        private List<ActivityThreshold> thresholds = new List<ActivityThreshold> ();
        
        #endregion


        #region Public Properties

        public Enumerations.ActivityType ActivityType { get { return activityType; } set { activityType = value; } }


        public Enumerations.ActivityAnchorDate InitialAnchorDate { get { return initialAnchorDate; } set { initialAnchorDate = value; } }

        public Enumerations.ActivityAnchorDate AnchorDate { get { return anchorDate; } set { anchorDate = value; } }


        public Enumerations.ActivityScheduleType ScheduleType { get { return scheduleType; } set { scheduleType = value; } }

        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Core.Enumerations.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public Int32 ConstraintValue { get { return constraintValue; } set { constraintValue = value; } }

        public Core.Enumerations.DateQualifier ConstraintQualifier { get { return constraintQualifier; } set { constraintQualifier = value; } }

        public Boolean Reoccurring { get { return reoccurring; } set { reoccurring = value; } }


        public Enumerations.ActivityPerformActionDate PerformActionDate { get { return performActionDate; } set { performActionDate = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }

        public List<ActivityThreshold> Thresholds { get { return thresholds; } set { thresholds = value; } }

        #endregion


        #region Public Properties - Calculated and Extended

        public SortedList<Int64, ActivityThreshold> SortedThresholds {

            get {

                SortedList<Int64, ActivityThreshold> sortedThresholds = new SortedList<Int64, ActivityThreshold> ();

                Int64 thresholdKey = 0;

                foreach (ActivityThreshold currentThreshold in thresholds) {

                    switch (currentThreshold.RelativeDateQualifier) {

                        case Mercury.Server.Core.Enumerations.DateQualifier.Months: thresholdKey = currentThreshold.RelativeDateValue * 30; break;

                        case Mercury.Server.Core.Enumerations.DateQualifier.Years: thresholdKey = currentThreshold.RelativeDateValue * 365; break;

                        case Mercury.Server.Core.Enumerations.DateQualifier.Days:

                        default: thresholdKey = currentThreshold.RelativeDateValue; break;

                    }

                    while (sortedThresholds.Keys.Contains (thresholdKey)) {

                        thresholdKey = thresholdKey + 1;

                    }

                    sortedThresholds.Add (thresholdKey, currentThreshold);

                }

                return sortedThresholds;

            }

        }

        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                if (thresholds == null) { thresholds = new List<ActivityThreshold> (); }

                foreach (ActivityThreshold currentThreshold in thresholds) {

                    currentThreshold.Application = value;

                }

                if (action != null) { action.Application = value; } // ACTIVITY COULD BE MANUAL

            }

        }

        #endregion 


        #region Constructors

        protected void ObjectConstructor (Application applicationReference) {

            BaseConstructor (applicationReference);

            action = new Mercury.Server.Core.Action.Action (applicationReference);

            return;

        }


        protected Activity () { /* DO NOTHING */ } // FOR CHILD INHERITANCE REASONS

        public Activity (Application applicationReference) {

            ObjectConstructor (applicationReference);
            
            return;  
        
        }

        public Activity (Application applicationReference, Int64 forId) {

            ObjectConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ActivityTypeInt32", ((Int32)ActivityType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ActivityType", ActivityType.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialAnchorDateInt32", ((Int32)InitialAnchorDate).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialAnchorDate", InitialAnchorDate.ToString ());
            
            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AnchorDateInt32", ((Int32)AnchorDate).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AnchorDate", AnchorDate.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleTypeInt32", ((Int32)ScheduleType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleType", ScheduleType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleValue", ScheduleValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleQualifierInt32", ((Int32)ScheduleQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleQualifier", ScheduleQualifier.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ConstraintValue", ConstraintValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ConstraintQualifierInt32", ((Int32)ConstraintQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ConstraintQualifier", ConstraintQualifier.ToString ());

            
            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Reoccurring", Reoccurring.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PerformActionDateInt32", ((Int32)PerformActionDate).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "PerformActionDate", PerformActionDate.ToString ());
            
            #endregion


            #region Thresholds

            System.Xml.XmlNode thresholdsNode = document.CreateElement ("Thresholds");

            document.LastChild.AppendChild (thresholdsNode);

            foreach (ActivityThreshold currentThreshold in thresholds) {

                thresholdsNode.AppendChild (document.ImportNode (currentThreshold.XmlSerialize ().LastChild, true));

            }

            #endregion


            #region Object Nodes

            if (Action != null) {

                document.LastChild.AppendChild (document.ImportNode (Action.XmlSerialize ().LastChild, true));

            }

            #endregion 


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);


            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                                    case "ActivityTypeInt32": ActivityType = (Enumerations.ActivityType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "InitialAnchorDateInt32": InitialAnchorDate = (Enumerations.ActivityAnchorDate)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "AnchorDateInt32": AnchorDate = (Enumerations.ActivityAnchorDate)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ScheduleTypeInt32": ScheduleType = (Enumerations.ActivityScheduleType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ScheduleValue": ScheduleValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ScheduleQualifierInt32": ScheduleQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ConstraintValue": ConstraintValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ConstraintQualifierInt32": ConstraintQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "Reoccurring": Reoccurring = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                                    case "PerformActionDateInt32": PerformActionDate = (Enumerations.ActivityPerformActionDate)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    default: break;

                                }

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // DO NOT SAVE ACTIVITY, SAVED BY PARENT
                
            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            switch (activityType) {

                case Enumerations.ActivityType.Manual:

                    validationResponse = base.Validate (); // VALIDATE NAME AND DESCRIPTION 

                    break;

                case Enumerations.ActivityType.Automated:

                case Enumerations.ActivityType.Workflow:

                    if (action == null) { validationResponse.Add ("Action", "No Action Defined for Activity."); }

                    else {

                        Dictionary<String, String> actionValidation = action.Validate ();

                        foreach (String currentKey in actionValidation.Keys) {

                            if (!validationResponse.ContainsKey (currentKey)) {

                                validationResponse.Add (currentKey, actionValidation[currentKey]);

                            }

                        }

                    }

                    break;

            }


            if ((scheduleType == Enumerations.ActivityScheduleType.ByFrequency) && (scheduleValue == 0)) {

                validationResponse.Add ("Schedule", "Invalid Schedule Value.");

            }


            return validationResponse;

        }

        #endregion


        #region Data Functions

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            activityType = (Enumerations.ActivityType)(Int32)currentRow["ActivityType"];


            scheduleType = (Enumerations.ActivityScheduleType)(Int32)currentRow["ScheduleType"];

            scheduleValue = (Int32)currentRow["ScheduleValue"];

            scheduleQualifier = (Mercury.Server.Core.Enumerations.DateQualifier)(Int32)currentRow["ScheduleQualifier"];


            initialAnchorDate = (Enumerations.ActivityAnchorDate)(Int32)currentRow["InitialAnchorDate"];

            anchorDate = (Enumerations.ActivityAnchorDate)(Int32)currentRow["AnchorDate"];

            constraintValue = (Int32)currentRow["ConstraintValue"];

            constraintQualifier = (Mercury.Server.Core.Enumerations.DateQualifier)(Int32)currentRow["ConstraintQualifier"];


            Reoccurring = (Boolean)currentRow["IsReoccurring"];

            performActionDate = (Enumerations.ActivityPerformActionDate)(Int32)currentRow["PerformActionDate"];


            if (!(currentRow["ActionId"] is DBNull)) {

                action = new Mercury.Server.Core.Action.Action (base.application);

                action.MapDataFields (String.Empty, currentRow);

            }


            return;

        }

        #endregion

    }

}
