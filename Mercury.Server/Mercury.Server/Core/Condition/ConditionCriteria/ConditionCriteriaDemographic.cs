using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Condition.ConditionCriteria {

    [DataContract (Name = "ConditionCriteriaDemographic")]
    public class ConditionCriteriaDemographic : CoreObject {
        
        #region Private Properties
        
        [DataMember (Name = "ConditionId")]
        private Int64 conditionId;

        [DataMember (Name = "Gender")]
        private Core.Enumerations.Gender gender = Mercury.Server.Core.Enumerations.Gender.Both;

        [DataMember (Name = "UseAgeCriteria")]
        private Boolean useAgeCriteria = false;

        [DataMember (Name = "AgeMinimum")]
        private Int32 ageMinimum = 0;

        [DataMember (Name = "AgeMaximum")]
        private Int32 ageMaximum = 0;

        [DataMember (Name = "EthnicityId")]
        private Int64 ethnicityId;
        
        #endregion


        #region Public Properties

        public Int64 ConditionId { get { return conditionId; } set { conditionId = value; } }

        public Core.Enumerations.Gender Gender { get { return gender; } set { gender = value; } }

        public Boolean UseAgeCriteria { get { return useAgeCriteria; } set { useAgeCriteria = value; } }

        public Int32 AgeMinimum { get { return ageMinimum; } set { ageMinimum = value; } }

        public Int32 AgeMaximum { get { return ageMaximum; } set { ageMaximum = value; } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public String EthnicityName { get { return application.CoreObjectGetNameById ("Ethnicity", ethnicityId); } }

        public String CriteriaClause {

            get {

                String criteriaClause = String.Empty;

                switch (gender) {

                    case Mercury.Server.Core.Enumerations.Gender.Female:

                        criteriaClause = criteriaClause + "{ActiveMembership.Gender = 'F'}"; break;

                    case Mercury.Server.Core.Enumerations.Gender.Male:

                        criteriaClause = criteriaClause + "{ActiveMembership.Gender = 'M'}"; break;

                } // switch (gender)

                if (useAgeCriteria) {

                    criteriaClause = criteriaClause + "{ActiveMembership.BirthDate BETWEEN ";

                    criteriaClause = criteriaClause + "'" + (DateTime.Today.AddYears ((ageMaximum + 1) * -1).AddDays (1).ToString ("MM/dd/yyyy")) + "'";

                    criteriaClause = criteriaClause + " AND '" + (DateTime.Today.AddYears ((ageMinimum + 0) * -1).AddDays (0).ToString ("MM/dd/yyyy")) + "'}";

                }

                if (ethnicityId != 0) {

                    criteriaClause = criteriaClause + "{ActiveMembership.EthnicityId = " + ethnicityId.ToString () + "}";

                }


                criteriaClause = criteriaClause.Replace ("}{", ") AND (");

                criteriaClause = criteriaClause.Replace ('{', '(');

                criteriaClause = criteriaClause.Replace ('}', ')');

                return criteriaClause;

            }

        }

        #endregion


        #region Constructors

        public ConditionCriteriaDemographic (Application applicationReference) { base.BaseConstructor (applicationReference); }

        public ConditionCriteriaDemographic (Application applicationReference, Int64 forId) { base.BaseConstructor (applicationReference, forId); }

        #endregion


        #region XML Serialization
        
        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "GenderInt32", ((Int32) Gender).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Gender", Gender.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "UseAgeCriteria", UseAgeCriteria.ToString ());

            if (useAgeCriteria) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AgeMinimum", AgeMinimum.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AgeMaximum", AgeMaximum.ToString ());

            }

            if (ethnicityId != 0) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EthnicityId", EthnicityId.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Ethnicity", EthnicityName);

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

                                    case "GenderInt32": Gender = (Core.Enumerations.Gender)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "UseAgeCriteria": UseAgeCriteria = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                                    case "AgeMinimum": AgeMinimum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "AgeMaximum": AgeMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "Ethnicity": EthnicityId = application.CoreObjectGetIdByName ("Ethnicity", currentPropertyNode.InnerText); break;

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

                sqlStatement.Append ("EXEC dbo.ConditionCriteriaDemographic_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (conditionId.ToString () + ", ");

                sqlStatement.Append (((Int32) gender).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (useAgeCriteria).ToString () + ", ");

                sqlStatement.Append (ageMinimum.ToString () + ", ");

                sqlStatement.Append (ageMaximum.ToString () + ", ");

                sqlStatement.Append (ethnicityId.ToString () + ", ");

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


            conditionId = (Int64) currentRow["ConditionId"];

            gender = (Mercury.Server.Core.Enumerations.Gender) (Int32) currentRow["Gender"];

            useAgeCriteria = (Boolean) currentRow["UseAgeCriteria"];

            ageMinimum = (Int32) currentRow["AgeMinimum"];

            ageMaximum = (Int32) currentRow["AgeMaximum"];

            Int64.TryParse (currentRow["EthnicityId"].ToString (), out ethnicityId);


            return;

        }

        #endregion


        #region Public Methods

        #endregion

    }

}
