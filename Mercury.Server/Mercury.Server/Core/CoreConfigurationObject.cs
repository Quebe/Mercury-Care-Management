using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core {

    [Serializable]
    [DataContract (Name = "CoreConfigurationObject")]

    [KnownType (typeof (Server.Core.Authorizations.AuthorizationType))]

    [KnownType (typeof (Server.Core.AuthorizedServices.AuthorizedService))]

    [KnownType (typeof (Server.Core.Insurer.BenefitPlan))]
    [KnownType (typeof (Server.Core.Insurer.Contract))]
    [KnownType (typeof (Server.Core.Insurer.InsuranceType))]
    [KnownType (typeof (Server.Core.Insurer.Program))]

    [KnownType (typeof (Server.Core.Individual.CareMeasureScale))]
    [KnownType (typeof (Server.Core.Individual.CareMeasureDomain))]
    [KnownType (typeof (Server.Core.Individual.CareMeasureClass))]
    [KnownType (typeof (Server.Core.Individual.CareMeasure))]
    [KnownType (typeof (Server.Core.Individual.CareMeasureComponent))]

    [KnownType (typeof (Server.Core.Individual.CareIntervention))]

    [KnownType (typeof (Server.Core.Individual.CareLevel))]
    [KnownType (typeof (Server.Core.Individual.ProblemStatement))]
    [KnownType (typeof (Server.Core.Individual.CarePlan))]
    [KnownType (typeof (Server.Core.Individual.CarePlanGoal))]
    [KnownType (typeof (Server.Core.Individual.CarePlanIntervention))]
    [KnownType (typeof (Server.Core.Individual.CareOutcome))]

    [KnownType (typeof (Server.Core.Individual.Case.MemberCaseCarePlan))]
    [KnownType (typeof (Server.Core.Individual.Case.MemberCaseCarePlanGoal))]

    [KnownType (typeof (Server.Core.MedicalServices.Service))]

    [KnownType (typeof (Server.Core.Metrics.Metric))]

    [KnownType (typeof (Server.Core.Population.Population))]
    [KnownType (typeof (Server.Core.Population.PopulationType))]

    [KnownType (typeof (Server.Core.Reference.ContactRegarding))]
    [KnownType (typeof (Server.Core.Reference.Correspondence))]
    [KnownType (typeof (Server.Core.Reference.NoteType))]

    [KnownType (typeof (Server.Core.Work.RoutingRule))]
    [KnownType (typeof (Server.Core.Work.Workflow))]
    [KnownType (typeof (Server.Core.Work.WorkOutcome))]
    [KnownType (typeof (Server.Core.Work.WorkQueue))]
    [KnownType (typeof (Server.Core.Work.WorkQueueView))]
    [KnownType (typeof (Server.Core.Work.WorkTeam))]

    [KnownType (typeof (Server.Faxing.FaxServer))]
    [KnownType (typeof (Server.Reporting.ReportingServer))]
    [KnownType (typeof (Server.Core.DataExplorer.DataExplorer))]
    public class CoreConfigurationObject : CoreExtensibleObject {

        #region Private Properties

        [DataMember (Name = "Enabled")]
        private Boolean enabled;

        [DataMember (Name = "Visible")]
        private Boolean visible;

        #endregion 


        #region Public Properties

        virtual public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        virtual public Boolean Visible { get { return visible; } set { visible = value; } }

        #endregion 


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Enabled", Enabled.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Visible", Visible.ToString ());

            #endregion 


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode propertiesNode;


            try {

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        case "Enabled": Enabled = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                        case "Visible": Visible = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                    }

                }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 


        #region Validation

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            // VALIDATE UNIQUE INSTANCE
            Int64 duplicateId = application.CoreObjectGetIdByName (ObjectType, Name);

            if ((duplicateId != 0) && (duplicateId != Id)) { validationResponse.Add ("Duplicate", "Duplicate Found."); }


            return validationResponse;

        }

        #endregion 


        #region Database Functions

        public override void MapDataFields (System.Data.DataRow currentRow, String forColumnPrefix) {

            base.MapDataFields (currentRow, forColumnPrefix);


            if (currentRow.Table.Columns.Contains ("Enabled")) {

                enabled = (Boolean) currentRow["Enabled"];

            }

            if (currentRow.Table.Columns.Contains ("Visible")) {

                visible = (Boolean) currentRow["Visible"];

            }
            
            return;

        }

        #endregion


    }

}
