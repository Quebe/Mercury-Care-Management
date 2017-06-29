using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationEvents {

    [DataContract (Name = "PopulationServiceEvent")]
    public class PopulationServiceEvent : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;

        [DataMember (Name = "ExclusionServiceId")]
        private Int64 exclusionServiceId;

        [DataMember (Name = "AnchorDate")]
        private Enumerations.PopulationServiceEventAnchorDate anchorDate = Mercury.Server.Core.Population.Enumerations.PopulationServiceEventAnchorDate.PopulationAnchorDate;

        [DataMember (Name = "AnchorDateValue")]
        private Int32 anchorDateValue;

        [DataMember (Name = "ScheduleDateValue")]
        private Int32 scheduleDateValue; 

        [DataMember (Name = "ScheduleDateQualifier")]
        private Core.Enumerations.DateQualifier scheduleDateQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "Reoccurring")]
        private Boolean reoccurring = false;

        [DataMember (Name = "Thresholds")]
        private List<PopulationServiceEventThreshold> thresholds = new List<PopulationServiceEventThreshold> ();

        
        private Population population = null;

        private MedicalServices.Service service = null;

        #endregion


        #region Public Properties - Encapsulated
        
        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public Int64 ExclusionServiceId { get { return exclusionServiceId; } set { exclusionServiceId = value; } }

        public Enumerations.PopulationServiceEventAnchorDate AnchorDate { get { return anchorDate; } set { anchorDate = value; } }

        public Int32 AnchorDateValue { get { return anchorDateValue; } set { anchorDateValue = value; } }

        public Int32 ScheduleDateValue { get { return scheduleDateValue; } set { scheduleDateValue = value; } }

        public Core.Enumerations.DateQualifier ScheduleDateQualifier { get { return scheduleDateQualifier; } set { scheduleDateQualifier = value; } }

        public Boolean Reoccurring { get { return reoccurring; } set { reoccurring = value; } }

        public List<PopulationServiceEventThreshold> Thresholds { get { return thresholds; } set { thresholds = value; } }

        public SortedList<Int64, PopulationServiceEventThreshold> SortedThresholds {

            get {

                SortedList<Int64, PopulationServiceEventThreshold> sortedThresholds = new SortedList<Int64, PopulationServiceEventThreshold> ();

                Int64 thresholdKey = 0;

                foreach (PopulationServiceEventThreshold currentThreshold in thresholds) {

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

        #endregion 


        #region Public Properties

        public Population Population { get { return population; } set { population = value; } }

        public MedicalServices.Service Service {

            get {

                if (service != null) { return service; }

                if (base.application == null) { return null; }

                service = base.application.MedicalServiceGet (serviceId);

                return service;

            }

        }

        public MedicalServices.Service ExclusionService { get { return application.MedicalServiceGet (exclusionServiceId); } }

        public String ServiceName {

            get {

                String serviceName = String.Empty;

                if (Service != null) { serviceName = Service.Name; }

                return serviceName;

            }

        }

        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                foreach (PopulationServiceEventThreshold currentThreshold in thresholds) { currentThreshold.Application = value; }

            }

        }

        #endregion


        #region Constructors

        protected void ObjectConstructor (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public PopulationServiceEvent (Application applicationReference) { ObjectConstructor (applicationReference); return; }

        public PopulationServiceEvent (Application applicationReference, Int64 forServiceEventId) {

            ObjectConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forServiceEventId);

            return;

        }

        #endregion

        
        
        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            if (Service != null) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceId", Service.Id.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ServiceName", Service.Name);

            }

            if (ExclusionService != null) {

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ExclusionServiceId", ExclusionService.Id.ToString ());

                CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ExclusionServiceName", ExclusionService.Name);

            }

            

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AnchorDateInt32", ((Int32)AnchorDate).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AnchorDate", AnchorDate.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AnchorDateValue", AnchorDateValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleDateValue", ScheduleDateValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleDateQualifierInt32", ((Int32)ScheduleDateQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleDateQualifier", ScheduleDateQualifier.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Reoccuring", Reoccurring.ToString ());

            #endregion


            #region Thresholds

            System.Xml.XmlNode thresholdsNode = document.CreateElement ("Thresholds");

            document.LastChild.AppendChild (thresholdsNode);

            foreach (PopulationServiceEventThreshold currentThreshold in Thresholds) {

                thresholdsNode.AppendChild (document.ImportNode (currentThreshold.XmlSerialize ().LastChild, true)); 

            }

            #endregion 


            #region Object Nodes

            System.Xml.XmlNode dependenciesNode = document.CreateElement ("Dependencies");

            document.LastChild.InsertBefore (dependenciesNode, propertiesNode);

            if (Service != null) { dependenciesNode.AppendChild (document.ImportNode (Service.XmlSerialize ().LastChild, true)); }

            if (ExclusionService != null) { dependenciesNode.AppendChild (document.ImportNode (ExclusionService.XmlSerialize ().LastChild, true)); }

            #endregion 


            return document;

        }

        #endregion


        //#region XML Serialization

        //public override System.Xml.XmlDocument XmlSerialize () {


        //    #region Properties


        //    if (anchorDate == Mercury.Server.Core.Population.Enumerations.PopulationServiceEventAnchorDate.PreviousServiceEvent) {

        //        Core.MedicalServices.Service previousService = application.MedicalServiceGet (anchorDateValue);

        //        if (previousService != null) {

        //            System.Xml.XmlElement previousServiceNode = CommonFunctions.XmlDocumentAppendPropertyNode (eventDocument, propertiesNode, "AnchorPreviousService", String.Empty);

        //            importedNode = eventDocument.ImportNode (previousService.XmlSerialize ().LastChild, true);

        //            previousServiceNode.AppendChild (importedNode);

        //        }

        //    }
               


        //public override List<Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = "ServiceEvent";

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "ServiceEvent") {

        //        try {

        //            #region Service Event Properties

        //            foreach (System.Xml.XmlNode currentProperty in objectNode.ChildNodes[0]) {

        //                switch (currentProperty.Attributes["Name"].InnerText) {

        //                    case "AnchorDate": anchorDate = (Mercury.Server.Core.Population.Enumerations.PopulationServiceEventAnchorDate)Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "AnchorDateValue":

        //                        if (AnchorDate != Mercury.Server.Core.Population.Enumerations.PopulationServiceEventAnchorDate.PreviousServiceEvent) {

        //                            anchorDateValue = Convert.ToInt64 (currentProperty.InnerText); 

        //                        }

        //                        break;

        //                    case "AnchorPreviousService":

        //                        String previousServiceName = currentProperty.FirstChild.Attributes["Name"].InnerText;

        //                        anchorDateValue = application.ObjectGetIdByName ("Service", previousServiceName);

        //                        if (anchorDateValue == 0) {

        //                            Core.MedicalServices.Enumerations.MedicalServiceType serviceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) Convert.ToInt32 (currentProperty.FirstChild.Attributes["ServiceType"].InnerText);

        //                            switch (serviceType) {

        //                                case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton:

        //                                    Core.MedicalServices.ServiceSingleton singleton = new Core.MedicalServices.ServiceSingleton (base.application);

        //                                    response.AddRange (singleton.XmlImport (currentProperty.FirstChild));

        //                                    anchorDateValue = singleton.Id; 

        //                                    break;

        //                                case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set:

        //                                    Core.MedicalServices.ServiceSet serviceSet = new Core.MedicalServices.ServiceSet (base.application);

        //                                    response.AddRange (serviceSet.XmlImport (currentProperty.FirstChild));

        //                                    anchorDateValue = serviceSet.Id; 

        //                                    break;

        //                            }

        //                        }

        //                        break;

        //                    case "ScheduleValue": scheduleValue = Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "ScheduleQualifier": scheduleQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) Convert.ToInt32 (currentProperty.InnerText); break;

        //                    case "Reoccurring": reoccurring = Convert.ToBoolean (currentProperty.InnerText); break;

        //                    case "Service": 

        //                    case "ExclusionService":

        //                        if (currentProperty.ChildNodes.Count > 0) {

        //                            System.Xml.XmlNode serviceNode = currentProperty.ChildNodes[0];

        //                            String serviceName = serviceNode.Attributes["Name"].InnerText;

        //                            Core.MedicalServices.Service medicalService = base.application.MedicalServiceGet (serviceName);

        //                            if (medicalService != null) {

        //                                if (currentProperty.Attributes["Name"].InnerText == "Service") { serviceId = medicalService.ServiceId; }

        //                                else { exclusionServiceId = medicalService.ServiceId; }

        //                            }

        //                            else {

        //                                Core.MedicalServices.Enumerations.MedicalServiceType serviceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) Convert.ToInt32 (serviceNode.Attributes["ServiceType"].InnerText);

        //                                switch (serviceType) {

        //                                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton:

        //                                        Core.MedicalServices.ServiceSingleton singleton = new Core.MedicalServices.ServiceSingleton (base.application);

        //                                        response.AddRange (singleton.XmlImport (serviceNode));

        //                                        if (currentProperty.Attributes["Name"].InnerText == "Service") { serviceId = singleton.ServiceId; }

        //                                        else { exclusionServiceId = singleton.ServiceId; }

        //                                        break;

        //                                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set:

        //                                        Core.MedicalServices.ServiceSet serviceSet = new Core.MedicalServices.ServiceSet (base.application);

        //                                        response.AddRange (serviceSet.XmlImport (serviceNode));

        //                                        if (currentProperty.Attributes["Name"].InnerText == "Service") { serviceId = serviceSet.ServiceId; }

        //                                        else { exclusionServiceId = serviceSet.ServiceId; }

        //                                        break;

        //                                }

        //                            }

        //                            if ((currentProperty.Attributes["Name"].InnerText == "Service") && (serviceId == 0)) { throw new ApplicationException ("Unable to load Service Event."); }

        //                            if ((currentProperty.Attributes["Name"].InnerText == "ServiceExclusion") && (exclusionServiceId == 0)) { throw new ApplicationException ("Unable to load Service Event."); }

        //                        }

        //                        break;
                                            
        //                }

        //            }

        //            #endregion


        //            #region Thresholds

        //            System.Xml.XmlNode thresholdNodes = objectNode.ChildNodes[1];

        //            foreach (System.Xml.XmlNode currentThresholdNode in thresholdNodes.ChildNodes) {

        //                ServiceEventThreshold importThreshold = new ServiceEventThreshold (base.application);

        //                response.AddRange (importThreshold.XmlImport (currentThresholdNode));

        //                thresholds.Add (importThreshold);

        //            }

        //            #endregion


        //            if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }


        //        }

        //        catch (Exception importException) {

        //            importResponse.SetException (importException);

        //        }


        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as Population.")); }


        //    response.Add (importResponse);

        //    return response;

        //}

        //#endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();


            if (serviceId == 0) { validationResponse.Add ("Service", "No Service Selected."); }

            if (scheduleDateValue == 0) { validationResponse.Add ("Schedule", "No Schedule for Service."); }


            return validationResponse;

        }

        #endregion


        #region Database Functions
        
        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;


            try {

                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.PopulationServiceEvent_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (populationId.ToString () + ", ");

                sqlStatement.Append (serviceId.ToString () + ", ");

                sqlStatement.Append (IdSqlAllowNull (exclusionServiceId) + ", ");

                sqlStatement.Append (((Int32) anchorDate).ToString () + ", ");

                sqlStatement.Append (anchorDateValue.ToString () + ", ");

                sqlStatement.Append (scheduleDateValue.ToString () + ", ");

                sqlStatement.Append (((Int32) scheduleDateQualifier).ToString () + ", ");

                sqlStatement.Append (reoccurring.ToString () + ", ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();


                String thresholdIds = String.Empty;

                foreach (PopulationServiceEventThreshold currentThreshold in SortedThresholds.Values) {

                    currentThreshold.Application = base.application; // RESET APPLICATION REFERENCE FOR DATA TRANSFER OBJECT

                    currentThreshold.PopulationServiceEventId = Id;

                    currentThreshold.PopulationId = populationId;

                    success = currentThreshold.Save ();

                    thresholdIds = thresholdIds + currentThreshold.Id.ToString () + ",";

                    if (!success) { throw application.EnvironmentDatabase.LastException; }

                }

                if (success) {

                    sqlStatement = new StringBuilder ("DELETE FROM PopulationServiceEventThreshold WHERE PopulationServiceEventId = " + Id.ToString ());

                    if (thresholdIds.Length != 0) {

                        thresholdIds = thresholdIds.Substring (0, thresholdIds.Length - 1);

                        sqlStatement.Append (" AND PopulationServiceEventThresholdId NOT IN (" + thresholdIds + ")");

                    }

                    success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                }

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                success = true;

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            populationId = IdFromSql (currentRow, "PopulationId");

            serviceId = IdFromSql (currentRow, "ServiceId");

            exclusionServiceId = IdFromSql (currentRow, "ExclusionServiceId");

            anchorDate = (Mercury.Server.Core.Population.Enumerations.PopulationServiceEventAnchorDate)(Int32)currentRow["AnchorDate"];

            anchorDateValue = Convert.ToInt32 (currentRow["AnchorDateValue"]);

            scheduleDateValue = (Int32) currentRow["ScheduleDateValue"];

            scheduleDateQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow ["ScheduleDateQualifier"];

            Reoccurring = (Boolean) currentRow["IsReoccurring"];



            System.Data.DataTable thresholdTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM PopulationServiceEventThreshold WHERE PopulationServiceEventId = " + Id.ToString ());

            foreach (System.Data.DataRow thresholdRow in thresholdTable.Rows) {

                PopulationServiceEventThreshold threshold = new PopulationServiceEventThreshold (base.application);

                threshold.MapDataFields (thresholdRow);

                thresholds.Add (threshold);

            }
            

            return;

        }

        #endregion


        #region Virtual - Data Bindings

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = base.EvaluateDataBinding (bindingContext);


            return dataValue;

        }

        override public Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> dataBindings = new Dictionary<String, String> ();


                dataBindings.Add ("ServiceEvent.Id", "Id|ServiceEvent");

                dataBindings.Add ("ServiceEvent.ServiceId", "Id|Service");


                foreach (String bindingName in new Core.MedicalServices.Service (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add (bindingName, new Core.MedicalServices.Service (base.application).DataBindingContexts[bindingName]);

                }


                foreach (String bindingName in new Population (base.application).DataBindingContexts.Keys) {

                    dataBindings.Add (bindingName, new Population (base.application).DataBindingContexts[bindingName]);

                }


                return dataBindings;

            }

        }

        #endregion

    }

}
