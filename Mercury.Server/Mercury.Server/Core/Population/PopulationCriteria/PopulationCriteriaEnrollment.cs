using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationCriteria {

    [DataContract (Name = "PopulationCriteriaEnrollment")]
    public class PopulationCriteriaEnrollment : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "InsurerId")]
        private Int64 insurerId;

        [DataMember (Name = "ProgramId")]
        private Int64 programId;

        [DataMember (Name = "BenefitPlanId")]
        private Int64 benefitPlanId;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 ProgramId { get { return programId; } set { programId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        #endregion 


        #region Public Properties

        public String InsurerName { get { return application.CoreObjectGetNameById ("Insurer", insurerId); } }

        public String ProgramName { get { return application.CoreObjectGetNameById ("Program", programId); } }

        public String BenefitPlanName { get { return application.CoreObjectGetNameById ("BenefitPlan", benefitPlanId); } }

        public String CriteriaClause {

            get {

                String criteriaClause = String.Empty;

                
                if (insurerId != 0) {

                    criteriaClause = criteriaClause + "{ActiveMembership.InsurerId = " + insurerId.ToString () + "}";

                    //criteriaClause = criteriaClause + "{ActiveMembership.ExternalInsurerId = dal.ConvertInsurerIdToSource (" + insurerId.ToString () + ")}";

                }

                if (programId != 0) {

                    criteriaClause = criteriaClause + "{ActiveMembership.ProgramId = " + programId.ToString () + "}";

                    //criteriaClause = criteriaClause + "{ActiveMembership.ExternalProgramId = dal.ConvertProgramIdToSource (" + programId.ToString () + ")}";

                }

                if (benefitPlanId != 0) {

                    criteriaClause = criteriaClause + "{ActiveMembership.BenefitPlanId = " + benefitPlanId.ToString () + "}";

                    //criteriaClause = criteriaClause + "{ActiveMembership.ExternalBenefitPlanId = dal.ConvertBenefitPlanIdToSource (" + benefitPlanId.ToString () + ")}";

                }

                criteriaClause = criteriaClause.Replace ("}{", ") AND (");

                criteriaClause = criteriaClause.Replace ('{', '(');

                criteriaClause = criteriaClause.Replace ('}', ')');

                return criteriaClause;

            }

        }

        #endregion

        
        #region Constructors

        public PopulationCriteriaEnrollment (Application applicationReference) { base.BaseConstructor (applicationReference); }

        public PopulationCriteriaEnrollment (Application applicationReference, Int64 forId) { base.BaseConstructor (applicationReference, forId); }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            if (insurerId != 0) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InsurerId", InsurerId.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Insurer", InsurerName);

            }

            if (programId != 0) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ProgramId", ProgramId.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Program", ProgramName);

            }

            if (benefitPlanId != 0) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "BenefitPlanId", BenefitPlanId.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "BenefitPlan", BenefitPlanName);

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

                            #region Properties

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].Value) {

                                    case "Insurer": InsurerId = application.CoreObjectGetIdByName ("Insurer", currentPropertyNode.InnerText); break;

                                    case "Program": ProgramId = application.CoreObjectGetIdByName ("Program", currentPropertyNode.InnerText); break;

                                    case "BenefitPlan": BenefitPlanId = application.CoreObjectGetIdByName ("BenefitPlan", currentPropertyNode.InnerText); break;
                                
                                }

                            }

                            #endregion

                            break;

                    } // switch (currentNode.Name) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                // SAVE IS PERFORMED BY PARENT OBJECT

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion
               

        #region Database Functions

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;


            try {

                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.PopulationCriteriaEnrollment_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (populationId.ToString () + ", ");

                sqlStatement.Append (insurerId.ToString () + ", ");

                sqlStatement.Append (programId.ToString () + ", ");

                sqlStatement.Append (benefitPlanId.ToString () + ", ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                SetIdentity ();

                success = true;

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            populationId = (Int64) currentRow["PopulationId"];


            InsurerId = base.IdFromSql (currentRow, "InsurerId");

            ProgramId = base.IdFromSql (currentRow, "ProgramId");

            BenefitPlanId = base.IdFromSql (currentRow, "BenefitPlanId");


            return;

        }

        #endregion


        #region Public Methods

        #endregion

    }

}
