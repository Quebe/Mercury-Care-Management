using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    [Serializable]
    class AppointmentInfo {
        private readonly string _id;
        private string _subject;
        private DateTime _start;
        private DateTime _end;
        private string _recurrenceRule;
        private string _recurrenceParentId;
        private int? _userID;

        public string ID {
            get { return _id; }
        }

        public string Subject {
            get { return _subject; }
            set { _subject = value; }
        }

        public DateTime Start {
            get { return _start; }
            set { _start = value; }
        }

        public DateTime End {
            get { return _end; }
            set { _end = value; }
        }

        public string RecurrenceRule {
            get { return _recurrenceRule; }
            set { _recurrenceRule = value; }
        }

        public string RecurrenceParentID {
            get { return _recurrenceParentId; }
            set { _recurrenceParentId = value; }
        }

        public int? UserID {
            get { return _userID; }
            set { _userID = value; }
        }

        private AppointmentInfo () {
            _id = Guid.NewGuid ().ToString ();
        }

        public AppointmentInfo (string subject, DateTime start, DateTime end,
            string recurrenceRule, string recurrenceParentID, int? userID)
            : this () {
            _subject = subject;
            _start = start;
            _end = end;
            _recurrenceRule = recurrenceRule;
            _recurrenceParentId = recurrenceParentID;
            _userID = userID;
        }

        public AppointmentInfo (Telerik.Web.UI.Appointment source)
            : this () {
            CopyInfo (source);
        }

        public void CopyInfo (Telerik.Web.UI.Appointment source) {
            Subject = source.Subject;
            Start = source.Start;
            End = source.End;
            RecurrenceRule = source.RecurrenceRule;
            if (source.RecurrenceParentID != null) {
                RecurrenceParentID = source.RecurrenceParentID.ToString ();
            }

            Telerik.Web.UI.Resource user = source.Resources.GetResourceByType ("User");
            if (user != null) {
                UserID = (int?) user.Key;
            }
            else {
                UserID = null;
            }
        }
    }

    public partial class ProviderScheduler : System.Web.UI.UserControl {

        #region Session Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private const string AppointmentsKey = "Telerik.Web.Examples.Scheduler.BindToList.CS.Apts";

        private List<AppointmentInfo> Appointments {
            get {
                List<AppointmentInfo> sessApts = Session[AppointmentsKey] as List<AppointmentInfo>;
                if (sessApts == null) {
                    sessApts = new List<AppointmentInfo> ();
                    Session[AppointmentsKey] = sessApts;
                }

                return sessApts;
            }
        }



        public Client.Core.Provider.Provider Provider {

            get { return (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"]; }

            set {

                Client.Core.Provider.Provider provider = (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"];

                if (provider != value) {

                    Session[SessionCachePrefix + "Provider"] = value;


                }

            }

        }

        public Boolean AllowUserInteraction {

            get {

                Boolean allowUserInteraction = false;

                if (Session[SessionCachePrefix + "AllowUserInteraction"] != null) {

                    allowUserInteraction = (Boolean) Session[SessionCachePrefix + "AllowUserInteraction"];

                }

                return allowUserInteraction;

            }

            set {

                // OVERRIDE TO DISABLE TOOLBAR

                Session[SessionCachePrefix + "AllowUserInteraction"] = false;

                // Session[SessionCachePrefix + "AllowUserInteraction"] = value; 

            }

        }

        #endregion 

        protected override void OnInit (EventArgs e) {
            base.OnInit (e);

            if (!IsPostBack) {
                Session.Remove (AppointmentsKey);

                InitializeResources ();
                InitializeAppointments ();
            }

            RadScheduler1.DataSource = Appointments;
        }

        protected void RadScheduler1_AppointmentInsert (object sender, Telerik.Web.UI.SchedulerCancelEventArgs e) {
            Appointments.Add (new AppointmentInfo (e.Appointment));
        }

        protected void RadScheduler1_AppointmentUpdate (object sender, Telerik.Web.UI.AppointmentUpdateEventArgs e) {
            AppointmentInfo ai = FindById (e.ModifiedAppointment.ID);
            ai.CopyInfo (e.ModifiedAppointment);
        }

        protected void RadScheduler1_AppointmentDelete (object sender, Telerik.Web.UI.SchedulerCancelEventArgs e) {
            Appointments.Remove (FindById (e.Appointment.ID));
        }

        private void InitializeResources () {
            Telerik.Web.UI.ResourceType resType = new Telerik.Web.UI.ResourceType ("User");
            resType.ForeignKeyField = "UserID";

            RadScheduler1.ResourceTypes.Add (resType);
            RadScheduler1.Resources.Add (new Telerik.Web.UI.Resource ("User", 1, "Alex"));
            RadScheduler1.Resources.Add (new Telerik.Web.UI.Resource ("User", 2, "Bob"));
            RadScheduler1.Resources.Add (new Telerik.Web.UI.Resource ("User", 3, "Charlie"));
        }

        private void InitializeAppointments () {
            DateTime start = DateTime.UtcNow.Date;
            start = start.AddHours (6);
            Appointments.Add (new AppointmentInfo ("Block Appointment", start, start.AddHours (1), string.Empty, null, 1));
            Appointments.Add (new AppointmentInfo ("Block Appointment", start.AddHours (2), start.AddHours (3), string.Empty, null, 2));

            start = start.AddDays (-1);
            DateTime dayStart = RadScheduler1.UtcDayStart (start);
            Appointments.Add (new AppointmentInfo ("Block Appointment", dayStart, dayStart.AddDays (1), string.Empty, null, 1));
            Appointments.Add (new AppointmentInfo ("Block Appointment", start.AddHours (2), start.AddHours (3), string.Empty, null, 2));

            start = start.AddDays (2);
            Appointments.Add (new AppointmentInfo ("Block Appointment", start.AddHours (2), start.AddHours (3), string.Empty, null, 1));
        }

        private AppointmentInfo FindById (object ID) {
            foreach (AppointmentInfo ai in Appointments) {
                if (ai.ID.Equals (ID)) {
                    return ai;
                }
            }

            return null;
        }


    }

}