using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Calendar {

    [Serializable]
    [DataContract (Name = "CalendarDayOfWeekTimes")]
    public class DayOfWeekTimes {

        #region Private Properties

        [DataMember (Name = "SundayTimes")]
        private List<TimeSegment> sundayTimes = new List<TimeSegment> ();

        [DataMember (Name = "MondayTimes")]
        private List<TimeSegment> mondayTimes = new List<TimeSegment> ();

        [DataMember (Name = "TuesdayTimes")]
        private List<TimeSegment> tuesdayTimes = new List<TimeSegment> ();

        [DataMember (Name = "WednesdayTimes")]
        private List<TimeSegment> wednesdayTimes = new List<TimeSegment> ();

        [DataMember (Name = "ThursdayTimes")]
        private List<TimeSegment> thursdayTimes = new List<TimeSegment> ();

        [DataMember (Name = "FridayTimes")]
        private List<TimeSegment> fridayTimes = new List<TimeSegment> ();

        [DataMember (Name = "SaturdayTimes")]
        private List<TimeSegment> saturdayTimes = new List<TimeSegment> ();


        #endregion 


        #region Public Properties

        public List<TimeSegment> SundayTimes { get { return sundayTimes; } }

        public List<TimeSegment> MondayTimes { get { return mondayTimes; } }

        public List<TimeSegment> TuesdayTimes { get { return tuesdayTimes; } }

        public List<TimeSegment> WednesdayTimes { get { return wednesdayTimes; } }

        public List<TimeSegment> ThursdayTimes { get { return thursdayTimes; } }

        public List<TimeSegment> FridayTimes { get { return fridayTimes; } }

        public List<TimeSegment> SaturdayTimes { get { return saturdayTimes; } }


        public Boolean HasTimes {

            get {

                return ((sundayTimes.Count != 0) || (mondayTimes.Count != 0) || (tuesdayTimes.Count != 0)

                    || (wednesdayTimes.Count != 0) || (thursdayTimes.Count != 0) || (fridayTimes.Count != 0) || (saturdayTimes.Count != 0));

            }

        }

        public String Description {

            get {

                String description = String.Empty;

                String timesString = String.Empty;


                for (Int32 currentDayOfWeek = 0; currentDayOfWeek < 7; currentDayOfWeek++) {

                    List<TimeSegment> times = Times ((DayOfWeek) currentDayOfWeek);

                    if (times.Count > 0) {

                        if (!String.IsNullOrEmpty (description)) { description += "; "; }

                        description = description + ((DayOfWeek) currentDayOfWeek).ToString () + ": ";

                        timesString = String.Empty;

                        foreach (TimeSegment currentSegment in times) {

                            if (!String.IsNullOrEmpty (timesString)) { timesString += ", "; }

                            timesString += currentSegment.Description;

                        } /* END FOREACH */

                        description += timesString;

                    }

                }


                return description;

            }

        }

        #endregion


        #region Constructors

        public DayOfWeekTimes () { /* DO NOTHING */ }

        public DayOfWeekTimes (System.Xml.XmlDocument timesXml) {

            XmlDeserialize (timesXml.LastChild);

            return;

        }

        #endregion 


        #region XML Serialization

        private System.Xml.XmlElement XmlSerializeDay (DayOfWeek forDayOfWeek, List<TimeSegment> times, System.Xml.XmlDocument xmlDocument) {

            System.Xml.XmlElement dayNode = xmlDocument.CreateElement ("Day");

            dayNode.SetAttribute ("DayOfWeek", ((Int32) forDayOfWeek).ToString ());

            
            String startTimeString = String.Empty;

            String endTimeString = String.Empty;

            SortedList <String, TimeSegment> sortedTime = new SortedList<String,TimeSegment> ();

            foreach (TimeSegment currentTime in times) {

                startTimeString = String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds);

                sortedTime.Add (startTimeString, currentTime);

            }

            foreach (TimeSegment currentTime in sortedTime.Values) {

                System.Xml.XmlElement timeNode = xmlDocument.CreateElement ("Time");


                startTimeString = String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds);

                endTimeString = String.Format ("{0:00}:{1:00}:{2:00}", currentTime.EndTime.Hours, currentTime.EndTime.Minutes, currentTime.EndTime.Seconds);

                timeNode.SetAttribute ("StartTime", startTimeString);

                timeNode.SetAttribute ("EndTime", endTimeString);


                dayNode.AppendChild (timeNode);
                
            }


            return dayNode;

        }

        virtual public System.Xml.XmlDocument XmlSerialize {

            get {

                System.Xml.XmlDocument timesXml = new System.Xml.XmlDocument ();

                System.Xml.XmlDeclaration xmlDeclaration = timesXml.CreateXmlDeclaration ("1.0", "utf-8", null);

                System.Xml.XmlElement rootNode = timesXml.CreateElement ("DayOfWeekTimes");

                timesXml.InsertBefore (xmlDeclaration, timesXml.DocumentElement);

                timesXml.AppendChild (rootNode);


                rootNode.AppendChild (XmlSerializeDay (DayOfWeek.Sunday, SundayTimes, timesXml));

                rootNode.AppendChild (XmlSerializeDay (DayOfWeek.Monday, MondayTimes, timesXml));

                rootNode.AppendChild (XmlSerializeDay (DayOfWeek.Tuesday, TuesdayTimes, timesXml));

                rootNode.AppendChild (XmlSerializeDay (DayOfWeek.Wednesday, WednesdayTimes, timesXml));

                rootNode.AppendChild (XmlSerializeDay (DayOfWeek.Thursday, ThursdayTimes, timesXml));

                rootNode.AppendChild (XmlSerializeDay (DayOfWeek.Friday, FridayTimes, timesXml));

                rootNode.AppendChild (XmlSerializeDay (DayOfWeek.Saturday, SaturdayTimes, timesXml));


                return timesXml;

            }

        }

        virtual public void XmlDeserialize (System.Xml.XmlNode timesXml) {

            foreach (System.Xml.XmlNode currentDayNode in timesXml.SelectNodes ("./Day")) {

                try {

                    DayOfWeek forDayOfWeek = (DayOfWeek) (Convert.ToInt32 (currentDayNode.Attributes["DayOfWeek"].InnerText.Trim ()));

                    foreach (System.Xml.XmlNode currentTimeNode in currentDayNode.ChildNodes) {

                        try {

                            TimeSpan startTime = TimeSpan.Parse (currentTimeNode.Attributes["StartTime"].InnerText.Trim ());

                            TimeSpan endTime = TimeSpan.Parse (currentTimeNode.Attributes["EndTime"].InnerText.Trim ());

                            AddTimeSegment (forDayOfWeek, startTime, endTime);
                            
                        }

                        catch { /* DO NOTHING */ }

                    }

                }

                catch { /* DO NOTHING */ }

            }

            return;

        }

        #endregion 


        #region Public Methods

        public void ClearAll () {

            sundayTimes.Clear ();

            mondayTimes.Clear ();

            tuesdayTimes.Clear ();

            wednesdayTimes.Clear ();

            thursdayTimes.Clear ();

            fridayTimes.Clear ();

            saturdayTimes.Clear ();

            return;

        }

        public List<TimeSegment> Times (DayOfWeek forDayOfWeek) {

            List<TimeSegment> times = null;

            switch (forDayOfWeek) {

                case DayOfWeek.Sunday: times = sundayTimes; break;

                case DayOfWeek.Monday: times = mondayTimes; break;

                case DayOfWeek.Tuesday: times = tuesdayTimes; break;

                case DayOfWeek.Wednesday: times = wednesdayTimes; break;

                case DayOfWeek.Thursday: times = thursdayTimes; break;

                case DayOfWeek.Friday: times = fridayTimes; break;

                case DayOfWeek.Saturday: times = saturdayTimes; break;

            }

            return times;

        }

        public Boolean AddTimeSegment (DayOfWeek forDayOfWeek, TimeSpan startTime, TimeSpan endTime) {

            Boolean success = true;

            List<TimeSegment> times = Times (forDayOfWeek);

            TimeSegment timeSegment = null;


            try {

                timeSegment = new TimeSegment (startTime, endTime); // VALIDATION IN CONSTRUCTOR (THROWS EXCEPTION)


                // VALIDATE FOR OVERLAPPING SEGMENTS

                foreach (TimeSegment currentSegment in times) {

                    if ((currentSegment.StartTime <= endTime) && (currentSegment.EndTime >= startTime)) {

                        success = false;

                        break;

                    }

                }

            }

            catch { success = false; }


            if (success) {

                times.Add (new TimeSegment (startTime, endTime));

            }

            return success;

        }

        #endregion 

    }

}
