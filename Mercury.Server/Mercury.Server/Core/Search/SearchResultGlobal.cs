using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Search {

    [DataContract]
    public class SearchResultGlobal {

        #region Private Properties

        [DataMember (Name = "ObjectId")]
        private Int64 objectId;

        [DataMember (Name = "ObjectType")]
        private String objectType;

        [DataMember (Name = "Name")]
        private String objectName;

        [DataMember (Name = "Detail1")]
        private String detail1;

        [DataMember (Name = "Detail2")]
        private String detail2;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = new DateTime (0001, 01, 01);

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties

        public Int64 ObjectId { get { return objectId; } }

        public String ObjectType { get { return objectType; } }

        public String Name { get { return objectName; } }

        public String Detail1 { get { return Detail1; } }

        public String Detail2 { get { return Detail2; } }


        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }

        #endregion 


        #region Constructors 

        public SearchResultGlobal () { /* DO NOTHING */ }

        public SearchResultGlobal (SearchResultMember memberResult) {

            objectId = memberResult.MemberId;

            objectType = "Member";

            objectName = memberResult.Name;

            detail1 = memberResult.BirthDate.ToString ("MM/dd/yyyy") + " (" + memberResult.CurrentAge.ToString ().PadLeft (2, ' ') + ") | " + memberResult.Gender;
            
            detail2 = (memberResult.CurrentlyEnrolled) ? "Actively Enrolled" : "Terminated";

            return;

        }

        public SearchResultGlobal (SearchResultProvider providerResult) {

            objectId = providerResult.ProviderId;

            objectType = "Provider";

            objectName = providerResult.Name;

            detail1 = providerResult.FederalTaxId + " | " + providerResult.NationalProviderId;

            detail2 = providerResult.PrimarySpecialtyName; 

            return;

        }

        #endregion 

    }

}
