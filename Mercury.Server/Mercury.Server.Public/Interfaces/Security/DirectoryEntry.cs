using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.Interfaces.Security {

    [DataContract (Name = "SecurityAuthorityDirectoryEntry")]
    public class DirectoryEntry {

        #region Private Properties

        [DataMember (Name = "ObjectType")]
        private String objectType;

        [DataMember (Name = "ObjectSid")]
        private String objectSid;

        [DataMember (Name = "Name")]
        private String name;

        [DataMember (Name = "DisplayName")]
        private String displayName;

        [DataMember (Name = "DistinguishedName")]
        private String distinguishedName;

        [DataMember (Name = "Path")]
        private String path;

        #endregion


        #region Public Properties

        public String ObjectType { get { return objectType;  } set { objectType = value; } }

        public String ObjectSid { get { return objectSid;  } set { objectSid = value; } }

        public String Name { get { return name;  } set { name = value; } }

        public String DisplayName { get { return displayName; } set { displayName = value; } }

        public String DistinguishedName { get { return distinguishedName;  } set { distinguishedName = value; } }

        public String Path { get { return path;  } set { path = value; } }

        #endregion 


        #region Constructors

        public DirectoryEntry () { return; }

        public DirectoryEntry (System.DirectoryServices.DirectoryEntry directoryEntry) {

            try {

                switch (directoryEntry.SchemaClassName.ToLowerInvariant ()) {

                    case "user": objectType = "User"; break;

                    case "group": objectType = "Group"; break;

                }

                objectSid = CommonFunctions.ObjectSidToString ((byte[]) directoryEntry.Properties["objectSid"].Value);


                name = GetPropertyValue (directoryEntry, "name", String.Empty);

                if (String.IsNullOrEmpty (name)) { name = GetPropertyValue (directoryEntry, "Name", String.Empty); }


                displayName = GetPropertyValue (directoryEntry, "DisplayName", String.Empty);

                if (String.IsNullOrEmpty (displayName)) { displayName = GetPropertyValue (directoryEntry, "givenName", String.Empty); }

                if (String.IsNullOrEmpty (displayName)) { displayName = GetPropertyValue (directoryEntry, "FullName", name); }

                if (String.IsNullOrEmpty (displayName)) { displayName = name; }


                distinguishedName = GetPropertyValue (directoryEntry, "distinguishedName", name);

            
            }

            catch (Exception directoryException) {

                System.Diagnostics.Debug.WriteLine (directoryException.Message);

            }

            return;

        }

        #endregion


        #region Public Methods

        public String GetPropertyValue (System.DirectoryServices.DirectoryEntry directoryEntry, String propertyName, String defaultValue) {

            String propertyValue = defaultValue;


            try {

                if (directoryEntry.Properties[propertyName] != null) {

                    if (directoryEntry.Properties[propertyName].Value != null) {

                        propertyValue = directoryEntry.Properties[propertyName].Value.ToString ();

                    }

                }

            }

            catch {

                propertyValue = defaultValue;

            }

            return propertyValue;

        }

        #endregion

    }

}
