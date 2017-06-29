using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntityAddress : System.Web.UI.UserControl {


        #region Private Properties

        private Client.Core.Entity.EntityAddress entityAddress = null;

        #endregion


        #region State Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + "_";

            }

        }

        public String ReferrerUrl {

            get {

                String referringUrl = "/Application/MemberProfile/MemberProfile.aspx?MemberId=" + MercuryApplication.MemberGetByEntityId (EntityId, true).Id.ToString (); ;

                if (Session[SessionCachePrefix + "ReferrerUrl"] != null) {

                    referringUrl = (String) Session[SessionCachePrefix + "ReferrerUrl"];

                }

                return referringUrl;

            }

            set { Session[SessionCachePrefix + "ReferrerUrl"] = value; }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public Client.Core.Entity.Entity Entity {

            get { return (Client.Core.Entity.Entity) Session[SessionCachePrefix + "Entity"]; }

            set {

                Client.Core.Entity.Entity entity = (Client.Core.Entity.Entity) Session[SessionCachePrefix + "Entity"];

                if (entity != value) {

                    Session[SessionCachePrefix + "Entity"] = value;

                    entity = value;

                }

            }

        }

        public Int64 EntityId {

            get {

                Int64 entityId = 0;

                if (Session[SessionCachePrefix + "EntityId"] != null) {

                    entityId = (Int64) Session[SessionCachePrefix + "EntityId"];

                }

                else {

                    Int64.TryParse (Request.QueryString["EntityId"], out entityId);

                    Session[SessionCachePrefix + "EntityId"] = entityId;

                }

                return entityId;

            }

        }

        public Int64 EntityAddressId {

            get {

                Int64 entityAddressId = 0;

                if (Session[SessionCachePrefix + "EntityAddressId"] != null) {

                    entityAddressId = (Int64) Session[SessionCachePrefix + "EntityAddressId"];

                }

                else {

                    Int64.TryParse (Request.QueryString["EntityAddressId"], out entityAddressId);

                    Session[SessionCachePrefix + "EntityAddressId"] = entityAddressId;

                }

                return entityAddressId;

            }

        }

        public Boolean AllowCancel { get { return ButtonCancel.Enabled; } set { ButtonCancel.Enabled = value; } }

        //public Boolean AllowUserInteraction {

        //    get {

        //        return ((Telerik.Web.UI.GridBoundColumn) EntityNoteHistoryGrid.Columns.FindByUniqueName ("Action")).Visible;

        //    }

        //    set {

        //        ((Telerik.Web.UI.GridBoundColumn) EntityNoteHistoryGrid.Columns.FindByUniqueName ("Action")).Visible = value;

        //    }

        //}


        public Client.Core.Entity.EntityAddress EntityAddressInstance { get { return entityAddress; } }

        public event EventHandler<EntityAddressCompletedEventArgs> EntityAddressCompleted;

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            ActionResponseLabel.Text = String.Empty;

            InitializeContentGrid ();

            InitializeState ();

            InitializeSecurity ();
            
            EntityAddressCounty.AllowCustomText = true;

            if (Entity.EntityType == Mercury.Server.Application.EntityType.Provider) {

                Telerik.Web.UI.RadComboBoxItem serviceLocation = EntityAddressType.Items.FindItemByValue ("77");

                serviceLocation.Visible = true;

            }

            // DEFAULT EFFECTIVE DATE TO CURRENT DATE

            if (EntityAddressEffectiveDatePicker.SelectedDate == null) {

                EntityAddressEffectiveDatePicker.SelectedDate = System.DateTime.Today.Date;

            }

            return;

        }
        
        #endregion


        #region Initializations

        private Telerik.Web.UI.RadComboBoxItem CreateRadComboBoxItem (String text, String value, Boolean isSelected) {

            Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem ();

            item = new Telerik.Web.UI.RadComboBoxItem ();

            item.Text = text;

            item.Value = value;

            item.Selected = isSelected;

            return item;

        }

        private void InitializeContentGrid () {

            System.Data.DataTable dataTable;

            dataTable = new System.Data.DataTable ();

            dataTable.Columns.Add ("EntityAddressId");

            dataTable.Columns.Add ("AddressType");

            dataTable.Columns.Add ("Line1");

            dataTable.Columns.Add ("Line2");

            dataTable.Columns.Add ("City");

            dataTable.Columns.Add ("State");

            dataTable.Columns.Add ("ZipCode");

            dataTable.Columns.Add ("ZipPlus4");

            dataTable.Columns.Add ("PostalCode");

            dataTable.Columns.Add ("County");

            dataTable.Columns.Add ("Longitude");

            dataTable.Columns.Add ("Latitude");

            dataTable.Columns.Add ("EffectiveDate");

            dataTable.Columns.Add ("TerminationDate");

            dataTable.Columns.Add ("CreateAccountName");

            dataTable.Columns.Add ("CreateDate");

            dataTable.Columns.Add ("ModifiedAccountName");

            dataTable.Columns.Add ("ModifiedDate");

            List<Mercury.Client.Core.Entity.EntityAddress> addresses = new List<Mercury.Client.Core.Entity.EntityAddress> ();

            addresses = MercuryApplication.EntityAddressesGet (EntityId, false);

            foreach (Mercury.Client.Core.Entity.EntityAddress currentAddress in addresses) {

                dataTable.Rows.Add (

                    currentAddress.Id,

                    Mercury.Server.CommonFunctions.EnumerationToString (currentAddress.AddressType),

                    currentAddress.Line1,

                    currentAddress.Line2,

                    currentAddress.City,

                    currentAddress.State,

                    currentAddress.ZipCode,

                    currentAddress.ZipPlus4,

                    currentAddress.PostalCode,

                    currentAddress.County,

                    currentAddress.Longitude,

                    currentAddress.Latitude,

                    currentAddress.EffectiveDate.ToString ("MM/dd/yyyy"),

                    currentAddress.TerminationDate.ToString ("MM/dd/yyyy"),

                    currentAddress.CreateAccountInfo.UserAccountName,

                    currentAddress.CreateAccountInfo.ActionDate.ToString ("MM/dd/yyyy"),

                    currentAddress.ModifiedAccountInfo.UserAccountName,

                    currentAddress.ModifiedAccountInfo.ActionDate.ToString ("MM/dd/yyyy")

                    );

            } /* END FOREACH */

            EntityAddressContentGrid.MasterTableView.DataSource = dataTable;

            EntityAddressContentGrid.MasterTableView.DataBind ();

            return;

        }

        private void InitializeSecurity () {

            if (Entity.EntityType == Mercury.Server.Application.EntityType.Member) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberAddressManage)) {

                    ButtonOk.Enabled = false;

                }

            }

            if (Entity.EntityType == Mercury.Server.Application.EntityType.Provider) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProviderAddressManage)) {

                    ButtonOk.Enabled = false;

                }

            }

            return;

        }

        private void InitializeState () {

            if (EntityAddressState.Items.Count == 0) {

                foreach (String currentState in MercuryApplication.StateReference (true)) {

                    EntityAddressState.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentState, currentState));

                } /* END FOREACH */

            }

            return;

        }

        #endregion


        #region Control Events

        protected virtual void EntityAddressZipCode_OnTextChanged (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (EntityAddressZipCode.Text == null) { return; }

            if (EntityAddressZipCode.Text == String.Empty) { return; }

            if (EntityAddressZipCode.Text.Length < 5) { return; }

            String fiveDigitZipCode = EntityAddressZipCode.Text.Substring (0, 5);

            String stateFromZipCode = MercuryApplication.StateReferenceByZipCode (fiveDigitZipCode);

            if (!EntityAddressState.Items.Contains (new Telerik.Web.UI.RadComboBoxItem (stateFromZipCode, stateFromZipCode))) {

                InitializeState ();

            }

            EntityAddressState.SelectedValue = stateFromZipCode;

            String countyFromZipCode = MercuryApplication.CountyReferenceByZipCode (fiveDigitZipCode);

            if (!EntityAddressCounty.Items.Contains (new Telerik.Web.UI.RadComboBoxItem (countyFromZipCode, countyFromZipCode))) {

                EntityAddressCounty.Items.Add (new Telerik.Web.UI.RadComboBoxItem (countyFromZipCode, countyFromZipCode));

                EntityAddressCounty.SelectedValue = countyFromZipCode; ;

            }

            String cityFromZipCode = MercuryApplication.CityReferenceByZipCode (fiveDigitZipCode);

            if (!EntityAddressCity.Items.Contains (new Telerik.Web.UI.RadComboBoxItem (cityFromZipCode, cityFromZipCode))) {

                EntityAddressCity.Items.Clear ();

                foreach (Mercury.Server.Application.CityStateZipCodeView currentCity  in MercuryApplication.CityReferenceByState (EntityAddressState.SelectedValue, true)) {

                    EntityAddressCity.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCity.City, currentCity.City));

                } /* END FOREACH */

            }

            EntityAddressCity.SelectedValue = cityFromZipCode;

            return;

        }

        protected void EntityAddressCity_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (EntityAddressCity.SelectedItem == null) { return; }

            if (EntityAddressCity.SelectedValue == String.Empty) { return; }

            /* TODO */

            return;

        }

        protected void EntityAddressState_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (EntityAddressState.SelectedItem == null) { return; }

            if (EntityAddressState.SelectedValue == String.Empty) {

                InitializeState ();

            }

            else {

                EntityAddressCity.Items.Clear ();

                foreach (Mercury.Server.Application.CityStateZipCodeView currentCity in MercuryApplication.CityReferenceByState (EntityAddressState.SelectedValue, true)) {

                    EntityAddressCity.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCity.City, currentCity.City));

                } /* END FOREACH */

                EntityAddressCounty.Items.Clear ();

                foreach (String currentCounty in MercuryApplication.CountyReferenceByState (EntityAddressState.SelectedValue)) {

                    EntityAddressCounty.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCounty, currentCounty));

                } /* END FOREACH */

            }

            return;

        }

        protected void EntityAddressCounty_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (EntityAddressCounty.SelectedItem == null) { return; }

            if (EntityAddressCounty.SelectedValue == String.Empty) { return; }

            foreach (String currentCounty in MercuryApplication.CountyReferenceByState (EntityAddressState.SelectedValue)) {

                EntityAddressCounty.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCounty, currentCounty));

            } /* END FOREACH */

            //EntityAddressCity.Items.Clear ();

            //foreach (String currentCity in MercuryApplication.CityReferenceByCounty (EntityAddressCounty.SelectedValue)) {

            //    EntityAddressCity.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCity, currentCity));

            //} /* END FOREACH */

            return;

        }

        public void ButtonOk_OnClick (Object sender, EventArgs e) {


            #region Evaluate Required Fields

            // REQUIRE ADDRESS TYPE

            if (EntityAddressType.SelectedValue == "0") {

                ActionResponseLabel.Text = "** Address cannot be Not Specified.";

                ActionResponseLabel.Visible = true;

                return;

            }

            // REQUIRE AN EFFECTIVE DATE

            if (!EntityAddressEffectiveDatePicker.SelectedDate.HasValue) {

                ActionResponseLabel.Text = "** Effective Date is Required.";

                ActionResponseLabel.Visible = true;

                return;

            }

            // REQUIRE THAT TERMINATION DATE BE NOT PRIOR TO EFFECTIVE DATE

            if (EntityAddressTerminationDatePicker.SelectedDate.HasValue && EntityAddressEffectiveDatePicker.SelectedDate.HasValue) {

                if (EntityAddressTerminationDatePicker.SelectedDate < EntityAddressEffectiveDatePicker.SelectedDate) {

                    ActionResponseLabel.Text = "** Termination Date cannot be prior to the Effective Date.";

                    ActionResponseLabel.Visible = true;

                    return;

                }

            }

            // REQUIRE ADDRESS LINE 1

            if (String.IsNullOrEmpty (EntityAddressLine1.Text.Trim ())) {

                ActionResponseLabel.Text = "** Address Line1 is Required.";

                ActionResponseLabel.Visible = true;

                return;

            }

            // REQUIRE ADDRESS STATE

            if ((EntityAddressState.SelectedItem == null) || (EntityAddressState.SelectedValue == String.Empty)) {

                ActionResponseLabel.Text = "** Address State is Required.";

                ActionResponseLabel.Visible = true;

                return;

            }

            // REQUIRE ADDRESS CITY

            if ((EntityAddressCity.SelectedItem == null) || (EntityAddressCity.SelectedValue == String.Empty)) {

                ActionResponseLabel.Text = "** Address City is Required.";

                ActionResponseLabel.Visible = true;

                return;

            }

            // REQUIRE ADDRESS ZIP CODE

            if ((String.IsNullOrEmpty (EntityAddressZipCode.Text.Trim ())) || (EntityAddressZipCode.Text.Length < 5)) {

                ActionResponseLabel.Text = "** Valid Address Zip Code is Required.";

                ActionResponseLabel.Visible = true;

                return;

            }

            ActionResponseLabel.Text = String.Empty;

            ActionResponseLabel.Visible = false;

            #endregion


            #region Set the Entity Address Properties to be Saved

            entityAddress = new Mercury.Client.Core.Entity.EntityAddress (MercuryApplication);

            entityAddress.EntityId = Entity.Id;

            entityAddress.AddressType = (Mercury.Server.Application.EntityAddressType) Convert.ToInt64 (EntityAddressType.SelectedValue);

            entityAddress.EffectiveDate = (EntityAddressEffectiveDatePicker.SelectedDate.HasValue) ? EntityAddressEffectiveDatePicker.SelectedDate.Value : DateTime.Now;

            entityAddress.TerminationDate = (EntityAddressTerminationDatePicker.SelectedDate.HasValue) ? EntityAddressTerminationDatePicker.SelectedDate.Value : Convert.ToDateTime ("12/31/9999");

            entityAddress.Line1 = EntityAddressLine1.Text;

            entityAddress.Line2 = EntityAddressLine2.Text;

            entityAddress.ZipCode = EntityAddressZipCode.Text.Substring (0, 5);

            entityAddress.City = EntityAddressCity.SelectedValue;

            entityAddress.State = EntityAddressState.SelectedValue;

            entityAddress.PostalCode = String.Empty;

            if (EntityAddressZipCode.Text.Length == 9) {

                entityAddress.ZipPlus4 = EntityAddressZipCode.Text.Substring(5, 4);

            }

            else {

                entityAddress.ZipPlus4 = String.Empty;

            }

            if (EntityAddressCounty.SelectedValue != null) {

                entityAddress.County = EntityAddressCounty.SelectedValue;

            }

            else {

                entityAddress.County = String.Empty;

            }

            #endregion


            if (entityAddress != null) {

                EntityAddressCompleted (this, new EntityAddressCompletedEventArgs (entityAddress));

            }

            return;

        }

        public void ButtonCancel_OnClick (Object sender, EventArgs e) {

            if (EntityAddressCompleted != null) {

                EntityAddressCompleted (this, new EntityAddressCompletedEventArgs ());

            }

            return;

        }

        #endregion

    }

    public class EntityAddressCompletedEventArgs : EventArgs {

        #region Private Properties

        private Client.Core.Entity.EntityAddress entityAddress = null;

        private Boolean cancel = false;

        #endregion


        #region Public Properties

        public Client.Core.Entity.EntityAddress EntityAddress { get { return entityAddress; } }

        public Boolean Cancel { get { return cancel; } }

        #endregion


        #region Constructors

        public EntityAddressCompletedEventArgs (Client.Core.Entity.EntityAddress forEntityAddress) { entityAddress = forEntityAddress; return; }

        public EntityAddressCompletedEventArgs () { cancel = true; return; }

        #endregion

    }

}