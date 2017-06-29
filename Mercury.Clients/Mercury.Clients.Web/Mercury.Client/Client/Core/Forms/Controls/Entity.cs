using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Entity : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        protected Mercury.Server.Application.EntityType entityType = Mercury.Server.Application.EntityType.Member;

        protected Int64 entityObjectId = 0;

        protected Mercury.Server.Application.FormControlEntityDisplayAgeFormat displayAgeFormat = Mercury.Server.Application.FormControlEntityDisplayAgeFormat.InYears;

        protected Mercury.Server.Application.FormControlEntityDisplayStyle displayStyle = Mercury.Server.Application.FormControlEntityDisplayStyle.NormalExpanded;

        private Boolean allowCustomEntityName = false;

        private String entityName = String.Empty;


        protected Mercury.Client.Core.Member.Member member = null;

        protected Mercury.Client.Core.Provider.Provider provider = null;

        #endregion


        #region Public Properties

        public Mercury.Server.Application.EntityType EntityType { 
            
            get { return entityType; } 
            
            set {

                if (entityType != value) {

                    entityType = value;

                    dataBindingContexts = null;

                    bindableProperties = null;

                }
            
            } 
        
        }

        public Int64 EntityObjectId {

            get { return entityObjectId; }

            set {

                if (entityObjectId == value) { return; }


                entityObjectId = value;


                member = null;

                provider = null;

                entityName = String.Empty;

                if (entityObjectId != 0) { entityName = EntityName; }


                if (GetEventHandler ("EntityChanged") != null) {

                    RaiseEvent ("EntityChanged");

                }

                DataSourceChanged (); 

            }

        }


        public Mercury.Server.Application.FormControlEntityDisplayAgeFormat DisplayAgeFormat { get { return displayAgeFormat; } set { displayAgeFormat = value; } }

        public Mercury.Server.Application.FormControlEntityDisplayStyle DisplayStyle { get { return displayStyle; } set { displayStyle = value; } }


        public Boolean AllowCustomEntityName { get { return allowCustomEntityName; } set { allowCustomEntityName = value; } }

        public String EntityName {

            get {

                if (!String.IsNullOrEmpty (entityName)) { return entityName; }

                switch (entityType) {

                    case Mercury.Server.Application.EntityType.Member: if (Member != null) { entityName = Member.Entity.Name; } break;

                    case Mercury.Server.Application.EntityType.Provider: if (Provider != null) { entityName = Provider.Entity.Name; } break;

                    default: entityName = String.Empty; break;

                }

                return entityName;

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


        public Member.Member Member { 

            get {

                if (entityType != Mercury.Server.Application.EntityType.Member) { return null; }

                if (member != null) { return member; }

                if (entityObjectId == 0) { return null; }

                if (application == null) { return null; }

                member = application.MemberGet (entityObjectId, true);

                return member;

            }

        }

        public Provider.Provider Provider {

            get {

                if (entityType != Mercury.Server.Application.EntityType.Provider) { return null; }

                if (provider != null) { return provider; }

                if (entityObjectId == 0) { return null; }

                if (application == null) { return null; }

                provider = Application.ProviderGet (entityObjectId, true);

                return provider;

            }

        }

        #endregion


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Entity;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = true;

            capabilities.IsDataSource = true;

            label = new Label (Application, this);

            label.Visible = true;

            return;

        }

        override public void BaseConstructor (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.BaseConstructor (applicationReference, parentControl, serverControl);


            Mercury.Server.Application.FormControlEntity serverEntity = (Mercury.Server.Application.FormControlEntity) serverControl;

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

        public Entity (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlEntity serverControl) {

            InitializeControl (applicationReference);

            BaseConstructor (applicationReference, parentControl, serverControl);

            ChildServerControlsToLocal (this, serverControl);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);


            ((Mercury.Server.Application.FormControlEntity) serverControl).EntityType = entityType;

            ((Mercury.Server.Application.FormControlEntity) serverControl).EntityObjectId = entityObjectId;

            ((Mercury.Server.Application.FormControlEntity) serverControl).DisplayAgeFormat = displayAgeFormat;

            ((Mercury.Server.Application.FormControlEntity) serverControl).DisplayStyle = displayStyle;

            ((Mercury.Server.Application.FormControlEntity) serverControl).AllowCustomEntityName = allowCustomEntityName;

            ((Mercury.Server.Application.FormControlEntity) serverControl).EntityName = entityName;



            ((Mercury.Server.Application.FormControlEntity) serverControl).Label = new Mercury.Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Mercury.Server.Application.FormControlEntity) serverControl).Label);

            return;

        }

        #endregion


        #region Public Methods

        public Entity Clone () {

            Mercury.Server.Application.FormControlEntity serverEntity = new Mercury.Server.Application.FormControlEntity ();

            LocalControlToServer (null, serverEntity);

            Entity clone = new Entity (Application, null, serverEntity);

            clone.controlId = Guid.NewGuid ();

            return clone;

        }

        #endregion


        #region Data Bindings

        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            String dataValue = String.Empty;

            base.OnDataSourceChanged (dataSourceControl, propogate);

            foreach (Mercury.Server.Application.FormControlDataBinding currentBinding in GetDataBindings (dataSourceControl.ControlId)) {

                switch (currentBinding.BoundProperty) {

                    case "EntityId": // BACKWARDS COMPATIBILITY

                    case "EntityObjectId":

                        dataValue = EvaluateDataBinding (currentBinding);

                        Int64.TryParse (dataValue, out entityObjectId);

                        EntityObjectId = entityObjectId; // FORCE UPDATE THROUGH SETTING PUBLIC PROPERTY

                        break;

                }

            }

        }

        #endregion

    }

}
