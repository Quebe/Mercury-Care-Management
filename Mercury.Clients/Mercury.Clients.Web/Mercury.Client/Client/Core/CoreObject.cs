using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core {

    [Serializable]
    public class CoreObject {

        #region Private Properties

        protected Application application = null;


        protected Int64 id = 0;

        protected String name = String.Empty;

        protected String description = String.Empty;


        protected Server.Application.AuthorityAccountStamp createAccountInfo = null;

        protected Server.Application.AuthorityAccountStamp modifiedAccountInfo = null;

        #endregion


        #region Public Properties

        public virtual Application Application { get { return application; } set { application = value; } }


        public virtual Int64 Id { get { return id; } set { id = value; } }

        public virtual String Name { get { return name; } set { name = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public virtual String Description { get { return description; } set { description = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }


        protected String NameSql { get { return Name.Replace ("'", "''").Trim (); } }

        protected String DescriptionSql { get { return Description.Replace ("'", "''").Trim (); } }


        public Server.Application.AuthorityAccountStamp CreateAccountInfo {

            get {

                if (createAccountInfo == null) { 
                    
                    createAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();

                    if (application != null) {

                        createAccountInfo.SecurityAuthorityName = application.Session.SecurityAuthorityName;

                        createAccountInfo.UserAccountId = application.Session.UserAccountId;

                        createAccountInfo.UserAccountName = application.Session.UserAccountName;

                        createAccountInfo.ActionDate = DateTime.Now;

                    }
                
                }

                return createAccountInfo;

            }

            set { createAccountInfo = value; }

        } // Property: CreateAccountInfo

        public Server.Application.AuthorityAccountStamp ModifiedAccountInfo {

            get {

                if (modifiedAccountInfo == null) { 
                    
                    modifiedAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();

                    if (application != null) {

                        modifiedAccountInfo.SecurityAuthorityName = application.Session.SecurityAuthorityName;

                        modifiedAccountInfo.UserAccountId = application.Session.UserAccountId;

                        modifiedAccountInfo.UserAccountName = application.Session.UserAccountName;

                        modifiedAccountInfo.ActionDate = DateTime.Now;

                    }
                }

                return modifiedAccountInfo;

            }

            set { modifiedAccountInfo = value; }

        } // Property: ModifiedAccountInfo


        public String ObjectType {

            get {

                String objectType = String.Empty;

                String[] objectNamespace = GetType ().ToString ().Split ('.');

                if (objectNamespace.Length > 0) {

                    objectType = objectNamespace[objectNamespace.Length - 1];

                }

                return objectType;

            }

        }

        #endregion
        

        #region Constructors

        protected CoreObject () { return; }

        public CoreObject (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CoreObject (Application applicationReference, Server.Application.CoreObject forCoreObject) {

            BaseConstructor (applicationReference, forCoreObject);

            return;

        }

        protected virtual void BaseConstructor (Application applicationReference) {

            application = applicationReference;

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.CoreObject forCoreObject) {

            BaseConstructor (applicationReference);

            MapFromServerObject (forCoreObject);

            return;

        }

        #endregion  


        #region Validation

        public virtual Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<String, String> ();

            if (application == null) { validationResponse.Add ("Exception", "Invalid Application Reference."); }

            else {

                Server.Application.CoreObject serverObject = (Server.Application.CoreObject)this.ToServerObject ();

                validationResponse = application.CoreObject_Validate (serverObject);

            }

            return validationResponse;

        }

        #endregion 


        #region Public Methods

        internal virtual void SetId (Int64 forId) { id = forId; }

        public virtual void MapFromServerObject (Server.Application.CoreObject forCoreObject) {
            
            id = forCoreObject.Id;

            name = forCoreObject.Name;

            description = forCoreObject.Description;


            createAccountInfo = forCoreObject.CreateAccountInfo;

            modifiedAccountInfo = forCoreObject.ModifiedAccountInfo;


            return;

        }

        public virtual void MapToServerObject (Server.Application.CoreObject coreObject) {

            coreObject.Id = Id;

            coreObject.Name = Name;

            coreObject.Description = Description;


            coreObject.CreateAccountInfo = new Server.Application.AuthorityAccountStamp ();

            coreObject.CreateAccountInfo.SecurityAuthorityName = CreateAccountInfo.SecurityAuthorityName;

            coreObject.CreateAccountInfo.UserAccountId = CreateAccountInfo.UserAccountId;

            coreObject.CreateAccountInfo.UserAccountName = CreateAccountInfo.UserAccountName;

            coreObject.CreateAccountInfo.ActionDate = CreateAccountInfo.ActionDate;


            coreObject.ModifiedAccountInfo = new Server.Application.AuthorityAccountStamp ();

            coreObject.ModifiedAccountInfo.SecurityAuthorityName = ModifiedAccountInfo.SecurityAuthorityName;

            coreObject.ModifiedAccountInfo.UserAccountId = ModifiedAccountInfo.UserAccountId;

            coreObject.ModifiedAccountInfo.UserAccountName = ModifiedAccountInfo.UserAccountName;

            coreObject.ModifiedAccountInfo.ActionDate = ModifiedAccountInfo.ActionDate;

            return;

        }

        public virtual Object ToServerObject () {

            Server.Application.CoreObject coreObject = new Server.Application.CoreObject ();

            MapToServerObject (coreObject); 

            return coreObject;

        }

        public Boolean IsEqual (CoreObject compareCoreObject) {

            Boolean isEqual = true;


            isEqual &= (id == compareCoreObject.Id);

            isEqual &= (name == compareCoreObject.Name);

            isEqual &= (description == compareCoreObject.Description);


            return isEqual;

        }

        #endregion 
        

        #region Virtual - Data Bindings

        public virtual Dictionary<String, String> DataBindingContexts { get { return application.CoreObject_DataBindingContexts ((Server.Application.CoreObject)this.ToServerObject (), true); } }

        virtual public String EvaluateDataBinding (String bindingContext) { return application.CoreObject_EvaluateDataBinding ((Server.Application.CoreObject)this.ToServerObject (), bindingContext); } 


        virtual public Dictionary<String, String> ParameterValueSelection (Mercury.Server.Application.ActionParameterDataType forDataType) {

            Dictionary<String, String> filteredContexts = new Dictionary<String, String> ();

            switch (forDataType) {

                case Mercury.Server.Application.ActionParameterDataType.Workflow:

                    foreach (Core.Work.Workflow currentWorkflow in application.WorkflowsAvailable (true)) {

                        if (currentWorkflow.Enabled) {

                            filteredContexts.Add (currentWorkflow.Name, currentWorkflow.Id.ToString ());

                        }

                    }

                    break;

                case Mercury.Server.Application.ActionParameterDataType.Correspondence:

                    foreach (Core.Reference.Correspondence currentCorrespondence in application.CorrespondencesAvailable (true)) {

                        if (currentCorrespondence.Enabled) {

                            filteredContexts.Add (currentCorrespondence.Name, currentCorrespondence.Id.ToString ());

                        }

                    }

                    break;

                case Mercury.Server.Application.ActionParameterDataType.WorkQueue:

                    foreach (Core.Work.WorkQueue currentWorkQueue in application.WorkQueuesAvailable (true)) {

                        if (currentWorkQueue.Enabled) {

                            filteredContexts.Add (currentWorkQueue.Name, currentWorkQueue.Id.ToString ());

                        }

                    }

                    break;

                case Mercury.Server.Application.ActionParameterDataType.WorkOutcome:

                    foreach (Core.Work.WorkOutcome currentWorkOutcome in application.WorkOutcomesAvailable (true)) {

                        if (currentWorkOutcome.Enabled) {

                            filteredContexts.Add (currentWorkOutcome.Name, currentWorkOutcome.Id.ToString ());

                        }

                    }

                    break;

                case Mercury.Server.Application.ActionParameterDataType.RoutingRule:

                    foreach (Core.Work.RoutingRule currentRoutingRule in application.RoutingRulesAvailable (true)) {

                        if (currentRoutingRule.Enabled) {

                            filteredContexts.Add (currentRoutingRule.Name, currentRoutingRule.Id.ToString ());

                        }

                    }

                    break;


                default:

                    Dictionary<String, String> bindingContexts = DataBindingContexts;

                    Boolean addContext = false;


                    foreach (String currentContextName in bindingContexts.Keys) {

                        addContext = false;

                        switch (forDataType) {

                            case Mercury.Server.Application.ActionParameterDataType.Id:

                            case Mercury.Server.Application.ActionParameterDataType.String:

                            case Mercury.Server.Application.ActionParameterDataType.DateTime:

                                if (bindingContexts[currentContextName].Split ('|')[0] == forDataType.ToString ()) { addContext = true; }

                                break;

                            case Mercury.Server.Application.ActionParameterDataType.MemberId:

                                if (bindingContexts[currentContextName] == "Id|Member") { addContext = true; }

                                break;

                            case Mercury.Server.Application.ActionParameterDataType.ProviderId:

                                if (bindingContexts[currentContextName] == "Id|Provider") { addContext = true; }

                                break;

                            case Mercury.Server.Application.ActionParameterDataType.EntityId:

                                if (bindingContexts[currentContextName] == "Id|Entity") { addContext = true; }

                                break;

                        }

                        if (addContext) { filteredContexts.Add (currentContextName, currentContextName); }

                    }

                    break;

            }

            return filteredContexts;

        }

        #endregion

    }

}
