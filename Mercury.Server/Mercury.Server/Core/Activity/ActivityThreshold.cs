using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Activity {

    [DataContract (Name = "ActivityThreshold")]
    public class ActivityThreshold : CoreObject {

        #region Private Properties

        [DataMember (Name = "ActivityId")]
        private Int64 activityId;

        [DataMember (Name = "RelativeDateValue")]
        private Int32 relativeDateValue;

        [DataMember (Name = "RelativeDateQualifier")]
        private Core.Enumerations.DateQualifier relativeDateQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "Status")]
        private Enumerations.ActivityStatus status = Enumerations.ActivityStatus.NotSpecified;

        [DataMember (Name = "Action")]
        private Core.Action.Action action = null;
        
        #endregion


        #region Public Properties

        public override String Name { get { return ((action != null) ? Server.CommonFunctions.SetValueMaxLength (action.Description, 60) : String.Empty); } }

        public override String Description { get { return ((action != null) ? action.Description : String.Empty); } }

        public Int64 ActivityId { get { return activityId; } set { activityId = value; } }

        public Int32 RelativeDateValue { get { return relativeDateValue; } set { relativeDateValue = value; } }

        public Core.Enumerations.DateQualifier RelativeDateQualifier { get { return relativeDateQualifier; } set { relativeDateQualifier = value; } }

        public Enumerations.ActivityStatus Status { get { return status; } set { status = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }


        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                if (action != null) { action.Application = value; }

            }

        }

        #endregion


        #region Constructors
        
        protected void ObjectConstructor (Application applicationReference) {

            BaseConstructor (applicationReference);

            action = new Mercury.Server.Core.Action.Action (applicationReference);

            return;

        }

        public ActivityThreshold (Application applicationReference) {

            ObjectConstructor (applicationReference);

            return; 
        
        }

        public ActivityThreshold (Application applicationReference, Int64 forId) {

            ObjectConstructor (applicationReference);

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RelativeDateValue", RelativeDateValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RelativeDateQualifierInt32", ((Int32)RelativeDateQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "RelativeDateQualifier", RelativeDateQualifier.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "StatusInt32", ((Int32)Status).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Status", Status.ToString ());

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

                                    case "RelativeDateValue": RelativeDateValue = Convert.ToInt32 (currentNode.InnerText); break;

                                    case "RelativeDateQualifierInt32": RelativeDateQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentNode.InnerText); break;

                                    case "StatusInt32": Status = (Enumerations.ActivityStatus)Convert.ToInt32 (currentNode.InnerText); break;

                                    default: break;

                                }

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // DO NOT SAVE THRESHOLD, SAVED BY PARENT

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Data Functions

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            relativeDateValue = (Int32)currentRow["RelativeDateValue"];

            relativeDateQualifier = (Mercury.Server.Core.Enumerations.DateQualifier)(Int32)currentRow["RelativeDateQualifier"];

            status = (Enumerations.ActivityStatus)(Int32)currentRow["Status"];


            if (!(currentRow["ActionId"] is DBNull)) {

                action = new Mercury.Server.Core.Action.Action (base.application);

                action.MapDataFields (String.Empty, currentRow);

            }

            return;

        }

        #endregion

    }

}
