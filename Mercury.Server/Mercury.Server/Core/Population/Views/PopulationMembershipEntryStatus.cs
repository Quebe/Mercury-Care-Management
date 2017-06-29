﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.DataViews {

    [DataContract (Name = "PopulationMembershipEntryStatusDataView")]
    public class PopulationMembershipEntryStatus  {

        #region Private Properties

        [DataMember (Name = "PopulationMembershipId")]
        private Int64 populationMembershipId;

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "MemberName")]
        private String memberName;

        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate;

        [DataMember (Name = "Status")]
        private Enumerations.PopulationServiceEventStatus status = Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus.CompliantOrNoChange;

        #endregion


        #region Public Properties

        public Int64 PopulationMembershipId { get { return populationMembershipId; } set { populationMembershipId = value; } }

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public String MemberName { get { return memberName; } set { memberName = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public Enumerations.PopulationServiceEventStatus Status { get { return status; } set { status = value; } }

        #endregion


        #region Data Functions

        public void MapDataFields (System.Data.DataRow currentRow) {

            populationMembershipId = (Int64) currentRow["PopulationMembershipId"];

            populationId = (Int64) currentRow["PopulationId"];

            memberId = (Int64) currentRow["MemberId"];

            memberName = (String) currentRow["MemberName"];

            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            status = (Mercury.Server.Core.Population.Enumerations.PopulationServiceEventStatus) (Int32) currentRow["status"];

            return;

        }

        #endregion

    }
}