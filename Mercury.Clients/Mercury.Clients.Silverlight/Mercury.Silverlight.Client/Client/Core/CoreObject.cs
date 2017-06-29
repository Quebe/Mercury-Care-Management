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
using System.Collections.Generic;

namespace Mercury.Client.Core {

    public class CoreObject : System.ComponentModel.INotifyPropertyChanged {

        #region Private Properties

        protected Application application = null;


        protected Int64 id = 0;

        protected String name = String.Empty;

        protected String description = String.Empty;


        protected Server.Application.AuthorityAccountStamp createAccountInfo = null;

        protected Server.Application.AuthorityAccountStamp modifiedAccountInfo = null;


        protected List<String> serverRequests = new List<String> ();

        protected List<String> loadedData = new List<String> ();

        #endregion


        #region Public Properties

        public virtual Application Application { get { return application; } set { application = value; } }


        public virtual Int64 Id { get { return id; } }

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


            id = forCoreObject.Id;

            name = forCoreObject.Name;

            description = forCoreObject.Description;


            createAccountInfo = forCoreObject.CreateAccountInfo;

            modifiedAccountInfo = forCoreObject.ModifiedAccountInfo;

            return;

        }

        #endregion  
        

        #region Silverlight Support

        public void GlobalProgressBarShow (String reference) {

            if (Application != null) {

                Application.ProgressBarShow (GetType ().ToString () + "." + Id.ToString () + "." + reference);

            }

            return;

        }

        public void GlobalProgressBarHide (String reference) {

            if (Application != null) {

                Application.ProgressBarHide (GetType ().ToString () + "." + Id.ToString () + "." + reference);

            }

            return;

        }


        protected Boolean isLoaded = false;

        public Boolean IsLoaded { get { return isLoaded; } }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged (String propertyName) {

            Application.MainDispatcher.BeginInvoke (

                   delegate {

                       if (PropertyChanged != null) {

                           PropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (propertyName));

                       }

                   }

               );

            return;

        }

        #endregion 


        #region Validation

            // TODO: SILVERLIGHT UPDATE

        #endregion 


        #region Public Methods

        internal virtual void SetId (Int64 forId) { id = forId; }

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

        
        #region Data Bindings

        #region Private Properties

        protected Boolean dataBindingContextsIsLoaded = false;

        private Application.CoreObject_DataBindingContextsCompleted dataBindingContextsCallback = null;

        protected Dictionary<String, String> dataBindingContexts = new Dictionary<String, String> ();

        #endregion


        #region Public Properties

        public Boolean DataBindingContextsIsLoaded { get { return dataBindingContextsIsLoaded; } }

        #endregion


        public void DataBindingContexts (Application.CoreObject_DataBindingContextsCompleted eventHandler) {

            dataBindingContextsIsLoaded = false;

            if (dataBindingContextsIsLoaded) {

                Server.Application.CoreObject_DataBindingContextsCompletedEventArgs completedEventArgs;

                completedEventArgs = new Server.Application.CoreObject_DataBindingContextsCompletedEventArgs (

                    new Object[] { dataBindingContexts }, null, false, null);

                if (eventHandler != null) { eventHandler (this, completedEventArgs); }

            }

            else {

                dataBindingContextsCallback = eventHandler;

                application.CoreObject_DataBindingContexts (((Server.Application.CoreObject)ToServerObject ()), DataBindingContexts_Completed);

            }

            return;

        }

        public void DataBindingContexts_Completed (Object sender, Server.Application.CoreObject_DataBindingContextsCompletedEventArgs eventArgs) {

            dataBindingContexts = eventArgs.Result.Dictionary;

            dataBindingContextsIsLoaded = true;


            if (dataBindingContextsCallback != null) {

                dataBindingContextsCallback (sender, eventArgs);

            }

            return;

        }

        #endregion

    }

}
