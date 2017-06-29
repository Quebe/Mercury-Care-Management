using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Search {

    [DataContract (Name = "SearchResultProvider")]
    public class SearchResultProvider {
        
        #region Private Properties

        [DataMember (Name = "ProviderId")]
        private Int64 providerId;

        [DataMember (Name = "EntityId")]
        private Int64 entityId;

        [DataMember (Name = "Name")]
        private String providerName;

        [DataMember (Name = "FederalTaxId")]
        private String federalTaxId;

        [DataMember (Name = "NationalProviderId")]
        private String nationalProviderId;

        [DataMember (Name = "PrimarySpecialtyName")]
        private String primarySpecialtyName;

        [DataMember (Name = "ExternalProviderId")]
        private String externalProviderId;

        #endregion


        #region Public Properties

        public Int64 ProviderId { get { return providerId; } }

        public Int64 EntityId { get { return entityId; } }

        public String Name { get { return providerName; } }

        public String FederalTaxId { get { return federalTaxId; } }

        public String NationalProviderId { get { return nationalProviderId; } }

        public String PrimarySpecialtyName { get { return primarySpecialtyName; } }

        public String ExternalProviderId { get { return externalProviderId; } }

        #endregion


        #region Public Methods

        public void MapDataFields (System.Data.DataRow currentRow) {

            providerId = (Int64) currentRow["ProviderId"];

            entityId = (Int64) currentRow["EntityId"];

            providerName = (String) currentRow["EntityName"];

            federalTaxId = (String) currentRow["FederalTaxId"];

            nationalProviderId = (String) currentRow["NationalProviderId"];

            primarySpecialtyName = (String) currentRow["PrimarySpecialtyName"];

            // externalProviderId = (String) currentRow["ExternalProviderId"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion 

    }

}


