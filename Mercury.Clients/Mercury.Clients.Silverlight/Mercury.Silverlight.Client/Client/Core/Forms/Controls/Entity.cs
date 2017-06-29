using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Forms.Controls {

    public class Entity : Control, System.ComponentModel.INotifyPropertyChanged {

        #region Private Properties

        private Server.Application.EntityType entityType = Server.Application.EntityType.Member;

        private Int64 entityObjectId = 0;

        private Server.Application.FormControlEntityDisplayAgeFormat displayAgeFormat = Server.Application.FormControlEntityDisplayAgeFormat.InYears;

        private Server.Application.FormControlEntityDisplayStyle displayStyle = Server.Application.FormControlEntityDisplayStyle.NormalExpanded;

        private Boolean allowCustomEntityName = false;

        private String entityName = String.Empty;


        private Server.Application.Entity entity = null;

        private Server.Application.Member member = null;

        private Server.Application.Provider provider = null;


        private Boolean isLoadingEntity = false;

        private Boolean isLoadingMember = false;

        private Boolean isLoadingProvider = false;

        #endregion


        #region Public Properties

        public Server.Application.EntityType EntityType {

            get { return entityType; }

            set {

                if (entityType != value) {

                    entityType = value;

                    dataBindingContextsIsLoaded = false;

                    dataBindablePropertiesIsLoaded = false;

                }

            }

        }

        public Int64 EntityObjectId {

            get { return entityObjectId; }

            set {

                if (entityObjectId == value) { return; }

                entityObjectId = value;


                entity = null;

                member = Member;

                provider = null;

                entityName = String.Empty;

                
                // TODO: RAISE EVENT FOR CHANGE 
                
                //if (GetEventHandler ("EntityChanged") != null) {

                //    RaiseEvent ("EntityChanged");

                //}

                //DataSourceChanged ();

            }

        }


        public Server.Application.FormControlEntityDisplayAgeFormat DisplayAgeFormat { get { return displayAgeFormat; } set { displayAgeFormat = value; } }

        public Server.Application.FormControlEntityDisplayStyle DisplayStyle { get { return displayStyle; } set { displayStyle = value; } }


        public Boolean AllowCustomEntityName { get { return allowCustomEntityName; } set { allowCustomEntityName = value; } }

        public String EntityName {

            get {

                if ((allowCustomEntityName) && (entityObjectId == 0)) { return entityName; }

                if (entity != null) { return entity.Name; }

                return "** No Entity Selected";

            }

            set {

                if ((allowCustomEntityName) && (entityObjectId == 0)) { entityName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); }

            }

        }


        public override Boolean HasValue {

            get {

                Boolean hasValue = (entityObjectId != 0);

                if (!hasValue) { hasValue = ((allowCustomEntityName) && (!String.IsNullOrEmpty (entityName.Trim ()))); }

                return hasValue;

            }

        }

        public override String Value { get { return (HasValue) ? entityObjectId.ToString () : String.Empty; } }


        // public Server.Application.Entity Entity { get { return entity; } }

        public Server.Application.Member Member {

            get {

                if (entityType != Server.Application.EntityType.Member) { return null; }

                if (member != null) { return member; }

                if (entityObjectId == 0) { return null; }

                if (Application == null) { return null; }

                if (!isLoadingMember) {

                    isLoadingMember = true;

                    Application.MemberGet (entityObjectId, true, MemberGetCompleted);

                }

                return member;

            }

        }

        public Server.Application.Provider Provider {

            get {

                if (entityType != Server.Application.EntityType.Provider) { return null; }

                if (provider != null) { return provider; }

                if (entityObjectId == 0) { return null; }

                if (Application == null) { return null; }

                if (!isLoadingProvider) {

                    isLoadingProvider = true;

                    Application.ProviderGet (entityObjectId, true, ProviderGetCompleted);

                }

                return provider;

            }

        }

        #endregion


        #region Property Service Calls

        private void EntityGetCompleted (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null)) {

                entity = e.Result;

                NotifyPropertyChanged ("EntityName");

                isLoadingEntity = false;

            }

        }

        private void MemberGetCompleted (Object sender, Server.Application.MemberGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null)) {

                member = e.Result;

                NotifyPropertyChanged ("Member");

                isLoadingMember = false;

                if (member != null) {

                    if (!isLoadingEntity) {

                        isLoadingEntity = true;

                        Application.EntityGet (member.EntityId, true, EntityGetCompleted);

                    }
                
                }

            }

            return;

        }

        private void ProviderGetCompleted (Object sender, Server.Application.ProviderGetCompletedEventArgs e) {

            if ((!e.Cancelled) && (e.Error == null)) {

                provider = e.Result;

                NotifyPropertyChanged ("Provider");

                isLoadingProvider = false;

                if (provider != null) {

                    if (!isLoadingEntity) {

                        isLoadingEntity = true;

                        Application.EntityGet (provider.EntityId, true, EntityGetCompleted);

                    }

                }

            }

            return;

        }

        #endregion 


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Entity;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (Application, this);

            label.Visible = true;

            return;

        }

        override public void BaseConstructor (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (parentControl, serverControl);


            Server.Application.FormControlEntity serverEntity = (Server.Application.FormControlEntity) serverControl;

            entityType = serverEntity.EntityType;

            entityObjectId = serverEntity.EntityObjectId;

            displayAgeFormat = serverEntity.DisplayAgeFormat;

            displayStyle = serverEntity.DisplayStyle;

            allowCustomEntityName = serverEntity.AllowCustomEntityName;

            entityName = serverEntity.EntityName;


            label = new Label (Application, this, serverEntity.Label);

            return;

        }


        public Entity (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Entity (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlEntity serverControl) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverControl);

            ChildServerControlsToLocal (this, serverControl);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);


            ((Server.Application.FormControlEntity) serverControl).EntityType = entityType;

            ((Server.Application.FormControlEntity) serverControl).EntityObjectId = entityObjectId;

            ((Server.Application.FormControlEntity) serverControl).DisplayAgeFormat = displayAgeFormat;

            ((Server.Application.FormControlEntity) serverControl).DisplayStyle = displayStyle;

            ((Server.Application.FormControlEntity) serverControl).AllowCustomEntityName = allowCustomEntityName;

            ((Server.Application.FormControlEntity) serverControl).EntityName = entityName;



            ((Server.Application.FormControlEntity) serverControl).Label = new Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Server.Application.FormControlEntity) serverControl).Label);

            return;

        }

        #endregion

    }

}
