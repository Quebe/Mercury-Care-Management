using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Search {

    [DataContract (Name = "SearchResultMember")]
    public class SearchResultMember {

        #region Private Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "EntityId")]
        private Int64 entityId;

        [DataMember (Name = "Name")]
        private String memberName;

        [DataMember (Name = "BirthDate")]
        private DateTime birthDate;

        [DataMember (Name = "CurrentAge")]
        private Int32 currentAge = 0;

        [DataMember (Name = "Gender")]
        private String gender;

        [DataMember (Name = "CurrentlyEnrolled")]
        private Boolean currentlyEnrolled;

        #endregion


        #region Public Properties

        public Int64 MemberId { get { return memberId; } }

        public Int64 EntityId { get { return entityId; } }

        public String Name { get { return memberName; } }

        public DateTime BirthDate { get { return birthDate; } set { birthDate = value; } }

        public Int32 CurrentAge { get { return currentAge; } }
        
        public String Gender { get { return gender; } set { gender = CommonFunctions.SetValueInRange (value.ToUpper (), "M;F;U", "U"); } }

        public Boolean CurrentlyEnrolled { get { return currentlyEnrolled; } }

        #endregion 


        #region Public Methods

        public void MapDataFields (System.Data.DataRow currentRow) {

            memberId = (Int64) currentRow["MemberId"];

            entityId = (Int64) currentRow["EntityId"];

            memberName = (String) currentRow["EntityName"];

            birthDate = (DateTime) currentRow["BirthDate"];

            gender = (String) currentRow["Gender"];

            currentlyEnrolled = Convert.ToBoolean ((Int32) currentRow["CurrentlyEnrolled"]);


            currentAge = 0;

            DateTime birthDay;


            currentAge = DateTime.Today.Year - birthDate.Year;

            birthDay = new DateTime (DateTime.Today.Year, birthDate.Month, (((birthDate.Month == 2) && (birthDate.Day == 29)) ? 28 : birthDate.Day));

            if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
