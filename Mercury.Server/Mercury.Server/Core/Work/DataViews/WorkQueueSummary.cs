using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work.DataViews {

    [DataContract (Name = "WorkQueueSummary")]
    public class WorkQueueSummary : CoreObject {

        #region Private Properties

        [DataMember (Name = "FirstWorkedTime")]
        private DateTime? firstWorkedTime = null;

        [DataMember (Name = "LastWorkedTime")]
        private DateTime? lastWorkedTime = null;

        [DataMember (Name = "WorkedItemsCount")]
        private Int32 workedItemsCount = 0;

        [DataMember (Name = "CompletedItemsCount")]
        private Int32 completedItemsCount = 0;
        
        [DataMember (Name = "AvailableItemsCount")]
        private Int32 availableItemsCount = 0;

        [DataMember (Name = "TotalItemsCount")]
        private Int32 totalItemsCount = 0;


        [DataMember (Name = "WarningItemsCount")]
        private Int32 warningItemsCount = 0;

        [DataMember (Name = "OverdueItemsCount")]
        private Int32 overdueItemsCount = 0;


        [DataMember (Name = "UsersInQueueCount")]
        private Int32 usersInQueueCount = 0;

        #endregion


        #region Public Properties

        public DateTime? FirstWorkedTime { get { return firstWorkedTime; } set { firstWorkedTime = value; } }

        public DateTime? LastWorkedTime { get { return lastWorkedTime; } set { lastWorkedTime = value; } }


        public Int32 WorkedItemsCount { get { return workedItemsCount; } set { workedItemsCount = value; } }

        public Int32 CompletedItemsCount { get { return completedItemsCount; } set { completedItemsCount = value; } }

        public Int32 AvailableItemsCount { get { return availableItemsCount; } set { availableItemsCount = value; } }

        public Int32 TotalItemsCount { get { return totalItemsCount; } set { totalItemsCount = value; } }


        public Int32 WarningItemsCount { get { return warningItemsCount; } set { warningItemsCount = value; } }

        public Int32 OverdueItemsCount { get { return overdueItemsCount; } set { overdueItemsCount = value; } }


        public Int32 UsersInQueueCount { get { return usersInQueueCount; } set { usersInQueueCount = value; } }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            id = (Int64)currentRow["WorkQueueId"];

            Name = (String)currentRow["WorkQueueName"];

            Description = Name;


            firstWorkedTime = base.DateTimeFromSql (currentRow, "FirstWorkedTime");

            lastWorkedTime = base.DateTimeFromSql (currentRow, "LastWorkedTime");


            workedItemsCount = (Int32)currentRow["WorkedItemsCount"];

            completedItemsCount = (Int32)currentRow["CompletedItemsCount"];

            availableItemsCount = (Int32)currentRow["AvailableItemsCount"];

            totalItemsCount = (Int32)currentRow["TotalItemsCount"];


            warningItemsCount = (Int32)currentRow["WarningItemsCount"];

            overdueItemsCount = (Int32)currentRow["OverdueItemsCount"];


            usersInQueueCount = (Int32)currentRow["UsersInQueueCount"];

            return;

        }

        #endregion 

    }

}
