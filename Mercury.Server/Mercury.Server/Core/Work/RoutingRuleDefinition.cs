using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [DataContract (Name="RoutingRuleDefinition")]
    public class RoutingRuleDefinition : CoreObject {

        #region Private Properties

        [DataMember (Name = "RoutingRuleId")]
        private Int64 routingRuleId;

        [DataMember (Name = "Sequence")]
        private Int32 sequence;

        [DataMember (Name = "InsurerId")]
        private Int64 insurerId;

        [DataMember (Name = "ProgramId")]
        private Int64 programId;

        [DataMember (Name = "BenefitPlanId")]
        private Int64 benefitPlanId;

        [DataMember (Name = "Gender")]
        private Core.Enumerations.Gender gender = Mercury.Server.Core.Enumerations.Gender.Both;

        [DataMember (Name = "UseAgeCriteria")]
        private Boolean useAgeCriteria = false;

        [DataMember (Name = "AgeMinimum")]
        private Int32 ageMinimum = 0;

        [DataMember (Name = "AgeMaximum")]
        private Int32 ageMaximum = 0;

        [DataMember (Name = "IsAgeInMonths")]
        private Boolean isAgeInMonths = false;

        [DataMember (Name = "EthnicityId")]
        private Int64 ethnicityId = 0;

        [DataMember (Name = "LanguageId")]
        private Int64 languageId = 0;

        [DataMember (Name = "State")]
        private String state;

        [DataMember (Name = "City")]
        private String city;

        [DataMember (Name = "County")]
        private String county;

        [DataMember (Name = "ZipCode")]
        private String zipCode;

        [DataMember (Name = "WorkQueueId")]
        private Int64 workQueueId;


        private WorkQueue workQueue = null;

        #endregion


        #region Public Properties

        public Int64 RoutingRuleId { get { return routingRuleId; } set { routingRuleId = value; } }

        public Int32 Sequence { get { return sequence; } set { sequence = value; } }

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 ProgramId { get { return programId; } set { programId = value; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } set { benefitPlanId = value; } }

        public Core.Enumerations.Gender Gender { get { return gender; } set { gender = value; } }

        public Boolean UseAgeCriteria { get { return useAgeCriteria; } set { useAgeCriteria = value; } }

        public Int32 AgeMinimum { get { return ageMinimum; } set { ageMinimum = value; } }

        public Int32 AgeMaximum { get { return ageMaximum; } set { ageMaximum = value; } }

        public Boolean IsAgeInMonths { get { return isAgeInMonths; } set { isAgeInMonths = value; } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public Int64 LanguageId { get { return languageId; } set { languageId = value; } }

        public String State { get { return state; } set { state = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressState); } }

        public String City { get { return city; } set { city = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressCity); } }

        public String County { get { return county; } set { county = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressCounty); } }

        public String ZipCode { get { return zipCode; } set { zipCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressZipCode); } }

        public Int64 WorkQueueId { get { return workQueueId; } set { workQueueId = value; workQueue = null; } }


        public WorkQueue WorkQueue {

            get {

                if (workQueue != null) { return workQueue; }

                if (application == null) { return null; }

                workQueue = application.WorkQueueGet (workQueueId);

                return workQueue;

            }

        }

        #endregion


        #region Constructors

        public RoutingRuleDefinition (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return; 
        
        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument routingRuleDefinitionDocument = base.XmlSerialize ();

            System.Xml.XmlElement routingRuleDefinitionNode = routingRuleDefinitionDocument.CreateElement ("RoutingRuleDefinition");

            System.Xml.XmlElement propertiesNode;

            System.Xml.XmlNode importedNode;


            routingRuleDefinitionDocument.AppendChild (routingRuleDefinitionNode);

            routingRuleDefinitionNode.SetAttribute ("RoutingRuleId", routingRuleId.ToString ());

            routingRuleDefinitionNode.SetAttribute ("Sequence", sequence.ToString ());

            propertiesNode = routingRuleDefinitionDocument.CreateElement ("Properties");

            routingRuleDefinitionNode.AppendChild (propertiesNode);


            #region Routing Rule Definition Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "InsurerName", application.CoreObjectGetNameById ("Insurer", insurerId));

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "ProgramName", application.CoreObjectGetNameById ("Program", programId));

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "BenefitPlanName", application.CoreObjectGetNameById ("BenefitPlan", benefitPlanId));


            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "Gender", ((Int32) gender).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "GenderDescription", gender.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "UseAgeCriteria", useAgeCriteria.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "AgeMinimum", ageMinimum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "AgeMaximum", ageMaximum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "IsAgeInMonths", IsAgeInMonths.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "EthnicityId", ethnicityId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "LanguageId", languageId.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "EthnicityName", application.CoreObjectGetNameById ("Ethnicity", ethnicityId));

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "LanguageName", application.CoreObjectGetNameById ("Language", languageId));

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "State", state);

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "City", city);

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "ZipCode", zipCode);

            CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "County", county);

            if (WorkQueue != null) {

                System.Xml.XmlElement WorkQueueNode = CommonFunctions.XmlDocumentAppendPropertyNode (routingRuleDefinitionDocument, propertiesNode, "WorkQueue", String.Empty);

                importedNode = routingRuleDefinitionDocument.ImportNode (WorkQueue.XmlSerialize ().LastChild, true);

                WorkQueueNode.AppendChild (importedNode);

            }

            #endregion

            return routingRuleDefinitionDocument;

        }

        //public override List<Mercury.Server.Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Mercury.Server.Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = "RoutingRuleDefinition";

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "RoutingRuleDefinition") {

        //        foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

        //            switch (currentNode.Name) {

        //                case "Properties":

        //                    foreach (System.Xml.XmlNode currentProperty in currentNode.ChildNodes) {

        //                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                            case "InsurerName": insurerId = application.InsurerReferenceIdByName (currentProperty.InnerText); break;

        //                            case "ProgramName": programId = application.ProgramReferenceIdByName (currentProperty.InnerText); break;

        //                            case "BenefitPlanName": benefitPlanId = application.BenefitPlanReferenceIdByName (currentProperty.InnerText); break;

        //                            case "Gender": gender = (Mercury.Server.Core.Enumerations.Gender) Convert.ToInt32 (currentProperty.InnerText); break;

        //                            case "UseAgeCriteria": useAgeCriteria = Convert.ToBoolean (currentProperty.InnerText); break;

        //                            case "AgeMinimum": ageMinimum = Convert.ToInt32 (currentProperty.InnerText); break;

        //                            case "AgeMaximum": ageMaximum = Convert.ToInt32 (currentProperty.InnerText); break;

        //                            case "AgeInMonths": ageInMonths = Convert.ToBoolean (currentProperty.InnerText); break;
    
        //                            case "EthnicityName": ethnicityId = application.EthnicityReferenceIdByName (currentProperty.InnerText); break;

        //                            case "LanguageName": languageId = application.LanguageReferenceIdByName (currentProperty.InnerText); break;

        //                            case "State": state = currentProperty.InnerText; break;

        //                            case "City": city = currentProperty.InnerText; break;

        //                            case "ZipCode": zipCode = currentProperty.InnerText; break;

        //                            case "County": county = currentProperty.InnerText; break;

        //                            case "WorkQueue":

        //                                String WorkQueueName = currentProperty.FirstChild.Attributes["Name"].InnerText;

        //                                WorkQueueId = application.WorkQueueGetIdByName (WorkQueueName);

        //                                if (WorkQueueId == 0) {

        //                                    workQueue = new WorkQueue (application);

        //                                    response.AddRange (workQueue.XmlImport (currentProperty.FirstChild));

        //                                    WorkQueueId = application.WorkQueueGetIdByName (workQueue.Name);

        //                                }

        //                                break;

        //                        }

        //                    }

        //                    break;

        //            }

        //        }

        //        importResponse.Success = Save ();

        //        importResponse.Id = Id;

        //        if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }

        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Routing Rule Definition.")); }

        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion


        #region Database Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            routingRuleId = Convert.ToInt64 (currentRow["RoutingRuleId"]);

            sequence = Convert.ToInt32 (currentRow["Sequence"]);


            Int64.TryParse (currentRow["InsurerId"].ToString (), out insurerId);

            Int64.TryParse (currentRow["ProgramId"].ToString (), out programId);

            Int64.TryParse (currentRow["BenefitPlanId"].ToString (), out benefitPlanId);


            gender = (Mercury.Server.Core.Enumerations.Gender)Convert.ToInt32 (currentRow["Gender"]);

            useAgeCriteria = (Boolean) currentRow["UseAgeCriteria"];

            ageMinimum = (Int32) currentRow["AgeMinimum"];

            ageMaximum = (Int32) currentRow["AgeMaximum"];

            IsAgeInMonths = (Boolean)currentRow["IsAgeInMonths"];

            Int64.TryParse (currentRow["EthnicityId"].ToString (), out ethnicityId);

            Int64.TryParse (currentRow["LanguageId"].ToString (), out languageId);


            state = ((String) currentRow["State"]).Trim ();

            city = (String) currentRow["City"];

            county = (String) currentRow["County"];

            zipCode = ((String) currentRow["ZipCode"]).Trim ();


            workQueueId = Convert.ToInt64 (currentRow["WorkQueueId"]);

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            try {

                sqlStatement.Append ("INSERT INTO RoutingRuleDefinition (");

                sqlStatement.Append ("RoutingRuleId, Sequence, InsurerId, ProgramId, BenefitPlanId, ");
                
                sqlStatement.Append ("Gender, UseAgeCriteria, AgeMinimum, AgeMaximum, IsAgeInMonths, EthnicityId, LanguageId, ");
                    
                sqlStatement.Append ("State, City, County, ZipCode, ");

                sqlStatement.Append ("WorkQueueId) VALUES (");

                sqlStatement.Append (routingRuleId.ToString () + ", ");

                sqlStatement.Append (sequence.ToString () + ", ");

                sqlStatement.Append (insurerId.ToString () + ", ");

                sqlStatement.Append (programId.ToString () + ", ");

                sqlStatement.Append (benefitPlanId.ToString () + ", ");

                
                sqlStatement.Append (((Int32) gender).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (useAgeCriteria).ToString () + ", ");

                sqlStatement.Append (ageMinimum.ToString () + ", ");

                sqlStatement.Append (ageMaximum.ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (IsAgeInMonths).ToString () + ", ");

                sqlStatement.Append (ethnicityId.ToString () + ", ");

                sqlStatement.Append (languageId.ToString () + ", ");
                

                sqlStatement.Append ("'" + state + "', ");

                sqlStatement.Append ("'" + city + "', ");

                sqlStatement.Append ("'" + county + "', ");

                sqlStatement.Append ("'" + zipCode + "', ");

                sqlStatement.Append (workQueueId.ToString ());

                sqlStatement.Append (")");


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                
                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();

            Int64 int64Parsed = 0;

            // AGE RANGE IS CORRECT IS SPECIFIED 
            
            if (useAgeCriteria) { 

                if (ageMinimum > ageMaximum) { 

                    validationResponse.Add ("Age Range", "Invalid Minimum and Maximum Ages");

                }

            }


            // VALIDATE ZIP CODE 

            if (!String.IsNullOrEmpty (zipCode)) {

                if (!Int64.TryParse (zipCode, out int64Parsed)) { validationResponse.Add ("Zip Code", "Invalid Zip Code Found"); }

                else if (zipCode.Trim ().Length != 5) { validationResponse.Add ("Zip Code", "Invalid Zip Code Found"); }

            }


            return validationResponse;

        }

        #endregion


        #region Public Methods

        public Boolean MatchMember (Member.Member member) {

            Boolean match = true;


            if (!member.HasCurrentEnrollment) { throw new Exception ("Member not actively enrolled. Unable to Route."); }


            Int64 currentEnrollmentInsurerId = 0;

            if (member.CurrentEnrollment.Program != null) { currentEnrollmentInsurerId = member.CurrentEnrollment.Program.InsurerId; }



            match = match && ((currentEnrollmentInsurerId == insurerId) || (insurerId == 0));

            match = match && ((member.CurrentEnrollment.ProgramId == programId) || (programId == 0));

            match = match && ((member.CurrentEnrollment.MostRecentCoverage.BenefitPlanId == benefitPlanId) || (benefitPlanId == 0));


            if (match) {

                if (!(gender == Mercury.Server.Core.Enumerations.Gender.Both)) {

                    match = match && (((member.Gender == "M") && (gender == Mercury.Server.Core.Enumerations.Gender.Male)) || ((member.Gender == "F") && (gender == Mercury.Server.Core.Enumerations.Gender.Female)));

                }

                if (useAgeCriteria) {

                    if (!isAgeInMonths) {

                        match = match && ((member.CurrentAge >= ageMinimum) && (member.CurrentAge <= ageMaximum));

                    }

                    else {

                        match = match && ((member.CurrentAgeInMonths >= ageMinimum) && (member.CurrentAgeInMonths <= ageMaximum));

                    }

                }

                match = match && ((member.EthnicityId == ethnicityId) || (ethnicityId == 0));

                match &= ((member.LanguageId == languageId) || (languageId == 0)); /* MCM-1066 */

            }

            if (match) {

                match = match && ((member.Entity.CurrentPhysicalAddress.State == state) || (String.IsNullOrEmpty (state.Trim ())));

                match = match && ((member.Entity.CurrentPhysicalAddress.City == city) || (String.IsNullOrEmpty (city.Trim ())));

                match = match && ((member.Entity.CurrentPhysicalAddress.County == county) || (String.IsNullOrEmpty (county.Trim ())));

                match = match && ((member.Entity.CurrentPhysicalAddress.ZipCode == zipCode) || (String.IsNullOrEmpty (zipCode.Trim ())));

            }

            return match; 

        }

        #endregion

    }

}
