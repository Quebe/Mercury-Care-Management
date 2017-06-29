using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Mercury.Server.Data;

namespace Mercury.Server.Core.Entity {

    [Serializable]
    [DataContract (Name = "Entity")]
    public class Entity : CoreObject {

        #region Private Properties
        
        [DataMember (Name = "EntityType")]
        private Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;


        [DataMember (Name = "NameLast")]
        private String nameLast;

        [DataMember (Name = "NameFirst")]
        private String nameFirst;

        [DataMember (Name = "NameMiddle")]
        private String nameMiddle;

        [DataMember (Name = "NamePrefix")]
        private String namePrefix;

        [DataMember (Name = "NameSuffix")]
        private String nameSuffix;


        [DataMember (Name = "FederalTaxId")]
        private String federalTaxId;

        [DataMember (Name = "IdCodeQualifier")]
        private String idCodeQualifier;

        [DataMember (Name = "UniqueId")]
        private String uniqueId;


        private List<EntityAddress> entityAddresses = null;

        private EntityAddress currentPhysicalAddress = null;

        private EntityAddress currentMailingAddress = null;


        private List<EntityContactInformation> entityContactInformations = null;
        
        #endregion


        #region Public Properties

        public Enumerations.EntityType EntityType { get { return entityType; } set { entityType = value; } }


        public String NameLastNameFirst { get { return Name; } }

        public String NameLast { get { return nameLast; } set { nameLast = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.NameLast); } }

        public String NameFirst {  get { return nameFirst; } set { nameFirst = value.Substring (0, (value.Length > DataTypeConstants.NameFirst) ? DataTypeConstants.NameFirst: value.Length); } }

        public String NameMiddle { get { return nameMiddle; } set { nameMiddle = value.Substring (0, (value.Length > DataTypeConstants.NameMiddle) ? DataTypeConstants.NameMiddle : value.Length); } }

        public String NamePrefix { get { return namePrefix; } set { namePrefix = value.Substring (0, (value.Length > DataTypeConstants.NameSuffix) ? DataTypeConstants.NameSuffix : value.Length); } }

        public String NameSuffix {  get { return nameSuffix; } set { nameSuffix = value.Substring (0, (value.Length > DataTypeConstants.NamePrefix) ? DataTypeConstants.NamePrefix : value.Length); } }

        public String FederalTaxId {  get { return federalTaxId; } set { federalTaxId = value.Substring (0, (value.Length > DataTypeConstants.FederalTaxId) ? DataTypeConstants.FederalTaxId : value.Length); } }

        public String IdCodeQualifier { get { return idCodeQualifier; } set { idCodeQualifier = value; } }

        public String UniqueId {  get { return uniqueId; } set { uniqueId = value.Substring (0, (value.Length > DataTypeConstants.UniqueId) ? DataTypeConstants.UniqueId : value.Length); } }

        #endregion 


        #region Extended Public Properties
        
        public List<EntityAddress> Addresses {

            get {

                if (entityAddresses != null) { return entityAddresses; }

                if (application == null) { return new List<EntityAddress> (); }

                entityAddresses = application.EntityAddressesGet (Id);

                return entityAddresses;

            }

        }

        public EntityAddress CurrentPhysicalAddress {

            get {

                if (currentPhysicalAddress != null) { return currentPhysicalAddress; }

                if (base.application == null) { return null; }

                currentPhysicalAddress = base.application.EntityAddressGetByEntityTypeDate (Id, Mercury.Server.Core.Enumerations.EntityAddressType.PhysicalAddress, DateTime.Today);

                return currentPhysicalAddress;

            }

        }

        public EntityAddress CurrentMailingAddress {

            get {

                if (currentMailingAddress != null) { return currentMailingAddress; }

                if (base.application == null) { return null; }

                currentMailingAddress = base.application.EntityAddressGetByEntityTypeDate (Id, Mercury.Server.Core.Enumerations.EntityAddressType.MailingAddress, DateTime.Today);

                return currentMailingAddress; 

            }

        }

        public EntityAddress CurrentResidentialAddress { get { return CurrentPhysicalAddress; } } // 2010-03-11: BACKWARDS COMPATIBILITY, REMOVED RESIDENTIAL

        public EntityAddress CurrentAddress (Enumerations.EntityAddressType forAddressType) {

            EntityAddress entityAddress = null;

            foreach (EntityAddress currentAddress in Addresses) {

                if (currentAddress.AddressType == forAddressType) {

                    if ((DateTime.Today >= currentAddress.EffectiveDate) && (DateTime.Today <= currentAddress.TerminationDate)) {

                        entityAddress = currentAddress;

                        break;

                    }

                }

            }

            return entityAddress;

        }


        public List<EntityContactInformation> ContactInformations {

            get {

                if (entityContactInformations != null) { return entityContactInformations; }

                if (application == null) { return new List<EntityContactInformation> (); }

                entityContactInformations = application.EntityContactInformationsGet (Id);

                return entityContactInformations;

            }

        }

        public EntityContactInformation CurrentContactInformation (Enumerations.EntityContactType forContactType) {

            EntityContactInformation contactInformation = null;

            foreach (EntityContactInformation currentContactInformation in ContactInformations) {

                if (currentContactInformation.ContactType == forContactType) {

                    if ((DateTime.Today >= currentContactInformation.EffectiveDate) && (DateTime.Today <= currentContactInformation.TerminationDate)) {

                        contactInformation = currentContactInformation;

                        break;

                    }

                }

            }

            return contactInformation;

        }
        
        #endregion


        #region Constructors

        public Entity (Application applicationReference) {

            BaseConstructor (applicationReference);

            return; 
        
        }

        public Entity (Application applicationReference, Int64 entityId) {

            BaseConstructor (applicationReference, entityId);

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEntity;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.Entity_Select " + forId.ToString ());

            tableEntity = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEntity.Rows.Count == 1) {

                MapDataFields (tableEntity.Rows[0]);

                return true;

            }

            else { return false; }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            entityType = (Mercury.Server.Core.Enumerations.EntityType) Convert.ToInt32 (currentRow["EntityType"]);
            

            nameLast   = (String) currentRow["NameLast"];

            nameFirst  = (String) currentRow["NameFirst"];

            nameMiddle = (String) currentRow["NameMiddle"];

            namePrefix = (String) currentRow["NamePrefix"];

            nameSuffix = (String) currentRow["NameSuffix"];


            federalTaxId = (String) currentRow["FederalTaxId"];

            idCodeQualifier = (String) currentRow["IdCodeQualifier"];

            uniqueId = (String) currentRow["UniqueId"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Binding

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = base.DataBindingContexts;
                
                bindingContexts.Add ("EntityType", "Int32");

                bindingContexts.Add ("EntityTypeDescription", "String");

                bindingContexts.Add ("NameLast", "String");

                bindingContexts.Add ("NameFirst", "String");

                bindingContexts.Add ("NameMiddle", "String");

                bindingContexts.Add ("NamePrefix", "String");

                bindingContexts.Add ("NameSuffix", "String");

                bindingContexts.Add ("FederalTaxId", "String");

                bindingContexts.Add ("IdCodeQualifier", "String");

                bindingContexts.Add ("UniqueId", "String");


                Dictionary<String, String> addressBindings = (new EntityAddress (base.application)).DataBindingContexts;

                foreach (String currentContext in addressBindings.Keys) {

                    bindingContexts.Add ("CurrentMailingAddress." + currentContext, addressBindings[currentContext]);

                }
                
                foreach (String currentContext in addressBindings.Keys) {

                    bindingContexts.Add ("CurrentPhysicalAddress." + currentContext, addressBindings[currentContext]);

                }


                Dictionary<String, String> contactBindings = (new EntityContactInformation (base.application)).DataBindingContexts;

                foreach (String currentContext in contactBindings.Keys) {

                    bindingContexts.Add ("CurrentContactTelephone." + currentContext, contactBindings[currentContext]);

                }

                foreach (String currentContext in contactBindings.Keys) {

                    bindingContexts.Add ("CurrentContactFacsimile." + currentContext, contactBindings[currentContext]);

                }

                //foreach (String currentContext in contactBindings.Keys) {

                //    bindingContexts.Add ("CurrentContactEmail." + currentContext, contactBindings[currentContext]);

                //}

                //foreach (String currentContext in contactBindings.Keys) {

                //    bindingContexts.Add ("CurrentContactEmergencyPhone." + currentContext, contactBindings[currentContext]);

                //}

                //foreach (String currentContext in contactBindings.Keys) {

                //    bindingContexts.Add ("CurrentContactMobilePhone." + currentContext, contactBindings[currentContext]);

                //}

                //foreach (String currentContext in contactBindings.Keys) {

                //    bindingContexts.Add ("CurrentContactPager." + currentContext, contactBindings[currentContext]);

                //}

                bindingContexts.Add ("EntityAddresses", "Collection|EntityAddress");

                bindingContexts.Add ("EntityContactInformation", "Collection|EntityContactInformation");


                return bindingContexts;

            }

        }
       
        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];


            EntityAddress address = null;
            

            switch (bindingContextPart) {

                case "Id": dataValue = Id.ToString (); break;

                case "EntityType": dataValue = ((Int32) entityType).ToString (); break;

                case "EntityTypeDescription": dataValue = entityType.ToString (); break;

                case "Name": dataValue = name; break;

                case "NameLast": dataValue = nameLast; break;

                case "NameFirst": dataValue = nameFirst; break;

                case "NameMiddle": dataValue = nameMiddle; break;

                case "NamePrefix": dataValue = namePrefix; break;

                case "NameSuffix": dataValue = nameSuffix; break;

                case "FederalTaxId": dataValue = federalTaxId; break;

                case "IdCodeQualifier": dataValue = idCodeQualifier; break;

                case "UniqueId": dataValue = uniqueId; break;


                case "CurrentResidentialAddress":

                case "CurrentPhysicalAddress":

                case "CurrentMailingAddress":

                    #region Current Address

                    switch (bindingContextPart) {

                        case "CurrentResidentialAddress": address = CurrentPhysicalAddress; break; // BACKWARDS COMPATIBILITY

                        case "CurrentPhysicalAddress": address = CurrentPhysicalAddress; break;

                        case "CurrentMailingAddress": address = CurrentMailingAddress; break;

                        default: address = null; break;

                    }

                    if (address != null) {

                        dataValue = address.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    }

                    else { dataValue = String.Empty; }

                    #endregion

                    break;
                    

                case "CurrentContactTelephone":

                case "CurrentContactFacsimile":

                case "CurrentContactEmail":

                case "CurrentContactEmergencyPhone":

                case "CurrentContactCellPhone": // BACKWARDS COMPATIBILITY

                case "CurrentContactMobilePhone":

                case "CurrentContactPager":

                    #region Current Contact

                    EntityContactInformation contact = null;

                    switch (bindingContextPart) {

                        case "CurrentContactTelephone": contact = CurrentContactInformation (Enumerations.EntityContactType.Telephone); break;

                        case "CurrentContactFacsimile": contact = CurrentContactInformation (Enumerations.EntityContactType.Facsimile); break;

                        //case "CurrentContactEmail": contact = CurrentContactEmail; break;

                        //case "CurrentContactEmergencyPhone": contact = CurrentContactEmergencyPhone; break;

                        //case "CurrentContactCellPhone": contact = CurrentContactCellPhone; break; // BACKWARDS COMPATIBILITY

                        //case "CurrentContactMobilePhone": contact = CurrentContactCellPhone; break;

                        //case "CurrentContactPager": contact = CurrentContactPager; break;

                        default: contact = null; break;

                    }

                    if (contact != null) {

                        dataValue = contact.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    }

                    else { dataValue = String.Empty; }

                    #endregion

                    break;

                case "EntityAddresses":

                    #region All Entity Addresses

                    dataValue = "EntityAddress";

                    foreach (EntityAddress currentAddress in base.application.EntityAddressesGet (Id)) {

                        dataValue = dataValue + "|" + currentAddress.Id.ToString ();

                    }

                    #endregion 

                    break;

                case "EntityContactInformation":

                    #region All Entity Contacts

                    dataValue = "EntityContactInformation";

                    foreach (EntityContactInformation currentContactInformation in base.application.EntityContactInformationsGet (Id)) {

                        dataValue = dataValue + "|" + currentContactInformation.Id.ToString ();

                    }

                    #endregion

                    break;

                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        #endregion

    }

}
