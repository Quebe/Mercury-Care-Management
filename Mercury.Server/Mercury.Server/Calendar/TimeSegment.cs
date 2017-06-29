using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Calendar {

    [Serializable]
    [DataContract (Name = "CalendarTimeSegment")]
    public class TimeSegment {

        #region Private Properties

        [DataMember (Name = "StartTime")]
        private TimeSpan startTime = new TimeSpan (0, 0, 0);

        [DataMember (Name = "EndTime")]
        private TimeSpan endTime = new TimeSpan (23, 59, 59);

        #endregion


        #region Public Properties

        public TimeSpan StartTime { get { return startTime; } set { startTime = value; } }

        public TimeSpan EndTime { get { return endTime; } set { endTime = value; } }


        public String Description {

            get {

                String description = String.Empty;


                String startTimeString = String.Format ("{0:00}:{1:00}:{2:00}", startTime.Hours, startTime.Minutes, startTime.Seconds);

                String endTimeString = String.Format ("{0:00}:{1:00}:{2:00}", endTime.Hours, endTime.Minutes, endTime.Seconds);

                description = startTimeString + "-" + endTimeString;


                return description;

            }

        }

        #endregion 


        #region Constructors;

        public TimeSegment () { /* DO NOTHING */ }

        public TimeSegment (TimeSpan forStartTime, TimeSpan forEndTime) {

            Boolean success = true;


            // VALIDATE TIME COMPONENTS

            if ((forStartTime < new TimeSpan (0, 0, 0)) || (forStartTime > new TimeSpan (23, 59, 59))) { success = false; }

            if ((forEndTime < new TimeSpan (0, 0, 0)) || (forEndTime > new TimeSpan (23, 59, 59))) { success = false; }

            if (forEndTime < forStartTime) { success = false; }


            if (success) {

                startTime = forStartTime;

                endTime = forEndTime;

            }

            else { throw new ApplicationException ("Unable to initialize Calendar.TimeSegment based on provided parameters."); }


            return;

        }

        #endregion 

    }

}
