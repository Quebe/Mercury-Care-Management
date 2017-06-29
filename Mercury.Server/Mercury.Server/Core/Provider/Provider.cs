using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Mercury.Server.Data;

namespace Mercury.Server.Core.Provider {

    [Serializable]
    [DataContract (Name = "Provider")]
    public class Provider : CoreObject {

        #region Private Properties

        [DataMember (Name = "EntityId")]
        private Int64 entityId;

        [DataMember (Name = "IsPerson")]
        private Boolean isPerson = false;

        [DataMember (Name = "BirthDate")]
        private DateTime? birthDate;

        [DataMember (Name = "DeathDate")]
        private DateTime? deathDate;

        [DataMember (Name = "Gender")]
        private String gender;

        [DataMember (Name = "EthnicityId")]
        private Int64 ethnicityId;

        [DataMember (Name = "CitizenshipId")]
        private Int64 citizenshipId;

        [DataMember (Name = "NationalProviderId")]
        private String nationalProviderId;


        [DataMember (Name = "Entity")]
        private Entity.Entity entity = null;

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }


        public Int64 EntityId { get { return entityId; } }

        public Boolean IsPerson { get { return isPerson; } set { isPerson = value; } }

        public DateTime? BirthDate { get { return birthDate; } set { birthDate = value; } }

        public DateTime? DeathDate { get { return deathDate; } set { deathDate = value; } }

        public String Gender { get { return gender; } set { gender = CommonFunctions.SetValueInRange (value.ToUpper (), "M;F;U", "U"); } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public Int64 CitizenshipId { get { return citizenshipId; } set { citizenshipId = value; } }

        public String NationalProviderId { get { return nationalProviderId; } set { nationalProviderId = CommonFunctions.SetValueMaxLength (value, DataTypeConstants.UniqueId); } }

        #endregion 


        #region Public Properties 
        
        public Int32 CurrentAge { get { return CommonFunctions.CurrentAge (birthDate); } }

        public Entity.Entity Entity {

            get {

                if (entity != null) { return entity; }

                if (application == null) { return null; }

                if (entityId != 0) {

                    entity = application.EntityGet (entityId);

                }

                return entity;

            }

        }

        public List<ProviderContract> Contracts { get { return application.ProviderContractsGet (id); } }

        #endregion

        
        #region Constructors 

        public Provider (Application applicationReference) {

            BaseConstructor (applicationReference);

            entity = new Entity.Entity (application);

            return; 
        
        }

        public Provider (Application applicationReference, Int64 forProviderId) {

            BaseConstructor (applicationReference, forProviderId);

            return;

        }

        #endregion


        #region Database Functions

        public override Boolean Load (Int64 forId) { return base.LoadFromDalSp (forId); }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            entityId = (Int64) currentRow["EntityId"];

            isPerson = Convert.ToBoolean (currentRow["IsPerson"]);


            birthDate = DateTimeFromSql (currentRow, "BirthDate");

            deathDate = DateTimeFromSql (currentRow, "DeathDate");

            
            Gender = (String) currentRow["Gender"];

            ethnicityId = IdFromSql (currentRow, "EthnicityId");

            citizenshipId = IdFromSql (currentRow, "CitizenshipId");


            NationalProviderId = (String) currentRow["NationalProviderId"];
            
            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Binding

        public override Dictionary <String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|Provider");

                bindingContexts.Add ("IsPerson", "Boolean");

                bindingContexts.Add ("BirthDate", "DateTime");

                bindingContexts.Add ("DeathDate", "DateTime");

                bindingContexts.Add ("Gender", "String");

                bindingContexts.Add ("EthnicityId", "Int64");

                bindingContexts.Add ("Ethnicity", "String");

                bindingContexts.Add ("CitizenshipId", "String");

                bindingContexts.Add ("Citizenship", "String");

                // bindingContexts.Add ("EnglishProficiency", "String");

                // bindingContexts.Add ("FederalTaxId", "String");

                //bindingContexts.Add ("MedicaidId", "String");

                //bindingContexts.Add ("MedicareId", "String");

                //bindingContexts.Add ("Upin", "String");

                //bindingContexts.Add ("Champus", "String");

                //bindingContexts.Add ("Nabp", "String");

                //bindingContexts.Add ("ExternalProviderId", "String");


                Dictionary<String, String> entityBindings = (new Entity.Entity (base.application)).DataBindingContexts;

                foreach (String currentContext in entityBindings.Keys) {

                    bindingContexts.Add ("Entity." + currentContext, entityBindings[currentContext]);

                }


                bindingContexts.Add ("ProviderContracts", "Collection|ProviderContract");


                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "Id": dataValue = Id.ToString (); break;

                case "BirthDate": if (!birthDate.HasValue) { dataValue = String.Empty; } else { dataValue = birthDate.Value.ToString ("MM/dd/yyyy"); } break;

                case "DeathDate": if (!deathDate.HasValue) { dataValue = String.Empty; } else { dataValue = deathDate.Value.ToString ("MM/dd/yyyy"); } break;

                case "Gender": dataValue = dataValue = gender; break;

                case "EthnicityId": dataValue = ethnicityId.ToString (); break;

                case "Ethnicity": dataValue = application.CoreObjectGetNameById ("Ethnicity", ethnicityId); break;

                case "CitizenshipId": dataValue = citizenshipId.ToString (); break;

                case "Citizenship": dataValue = application.CoreObjectGetNameById ("Citizenship", citizenshipId); break;

                // case "EnglishProficiency": dataValue = englishProficiency; break;

                case "NationalProviderId": dataValue = nationalProviderId; break;


                case "Entity": dataValue = Entity.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", "")); break;

                case "ProviderContracts":

                    dataValue = "ProviderContract";

                    foreach (ProviderContract currentProviderContract in base.application.ProviderContractsGet (Id)) {

                        dataValue = dataValue + "|" + currentProviderContract.Id;

                    }

                    break;

                default: dataValue = base.EvaluateDataBinding (bindingContext); break;

            }

            return dataValue;

        }

        #endregion

    }

}
