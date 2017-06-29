using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core {

    [Serializable]
    public class CoreExtensibleObject : CoreObject {
        
        #region Private Properties

        private Dictionary<String, String> extendedProperties = new Dictionary<String, String> ();

        #endregion


        #region Public Properties

        virtual public Dictionary<String, String> ExtendedProperties { get { return extendedProperties; } set { extendedProperties = value; } }

        #endregion


        #region Extended Properties

        virtual public System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedPropertiesXml = new System.Xml.XmlDocument ();

                System.Xml.XmlDeclaration xmlDeclaration = extendedPropertiesXml.CreateXmlDeclaration ("1.0", "utf-8", null);

                System.Xml.XmlElement rootNode = extendedPropertiesXml.CreateElement ("ExtendedProperties");

                extendedPropertiesXml.InsertBefore (xmlDeclaration, extendedPropertiesXml.DocumentElement);

                extendedPropertiesXml.AppendChild (rootNode);


                foreach (String currentPropertyName in extendedProperties.Keys) {

                    System.Xml.XmlElement propertyNode;


                    propertyNode = extendedPropertiesXml.CreateElement ("Property");

                    propertyNode.SetAttribute ("Name", currentPropertyName.Trim ());

                    propertyNode.InnerText = extendedProperties[currentPropertyName].Trim ();

                    rootNode.AppendChild (propertyNode);

                }

                return extendedPropertiesXml;

            }

        }

        virtual public String ExtendedPropertiesSql {

            get {

                String extendedPropertiesSql = ExtendedPropertiesXml.InnerXml;

                extendedPropertiesSql = extendedPropertiesSql.Replace ("'", "''");

                extendedPropertiesSql = extendedPropertiesSql.Replace ((char) 0xA0, (char) 0x20);

                extendedPropertiesSql = extendedPropertiesSql.Replace ((char) 0xB7, (char) 0x20);

                return extendedPropertiesSql;

            }

        }

        virtual public void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {

                extendedProperties.Add (currentPropertyNode.Attributes["Name"].InnerText.Trim (), currentPropertyNode.InnerText.Trim ());

            }

            return;

        }

        virtual public void ExtendedProperties_SetValue (String propertyName, String propertyValue) {

            if (ExtendedProperties.ContainsKey (propertyName)) { ExtendedProperties.Remove (propertyName); }

            ExtendedProperties.Add (propertyName, propertyValue);

            return;

        }

        virtual public String ExtendedProperties_GetValue (String propertyName, String defaultValue) {

            String value = String.Empty;

            if (ExtendedProperties.ContainsKey (propertyName)) { value = ExtendedProperties[propertyName]; }

            else { value = defaultValue; }

            return value;

        }

        #endregion 
        

        #region Constructors

        protected CoreExtensibleObject () { return; }

        public CoreExtensibleObject (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CoreExtensibleObject (Application applicationReference, Server.Application.CoreExtensibleObject forCoreExtensibleObject) {

            BaseConstructor (applicationReference, forCoreExtensibleObject);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.CoreExtensibleObject forCoreExtensibleObject) {

            base.BaseConstructor (applicationReference, forCoreExtensibleObject);

            MapFromServerObject (forCoreExtensibleObject);

            return;

        }

        #endregion  
        
        
        #region Public Methods

        public void MapFromServerObject (Server.Application.CoreExtensibleObject forCoreExtensibleObject) {

            // COPY EXTENDED PROPERTIES, DO NOT SET BY REFERENCE

            extendedProperties = new Dictionary<String, String> ();

            if (forCoreExtensibleObject.ExtendedProperties == null) { forCoreExtensibleObject.ExtendedProperties = new Dictionary<String, String> (); }

            foreach (String currentPropertyName in forCoreExtensibleObject.ExtendedProperties.Keys) {

                extendedProperties.Add (currentPropertyName, forCoreExtensibleObject.ExtendedProperties[currentPropertyName]);

            }

            return;

        }

        public void MapToServerObject (Server.Application.CoreExtensibleObject coreExtensibleObject) {

            base.MapToServerObject ((Server.Application.CoreObject) coreExtensibleObject);


            // COPY, DON'T MOVE REFERENCE

            coreExtensibleObject.ExtendedProperties = new Dictionary<String, String> ();

            foreach (String currentKey in extendedProperties.Keys) {

                coreExtensibleObject.ExtendedProperties.Add (currentKey, extendedProperties[currentKey]);

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CoreExtensibleObject coreExtensibleObject = new Server.Application.CoreExtensibleObject ();

            MapToServerObject (coreExtensibleObject);

            return coreExtensibleObject;

        }

        public Boolean IsEqual (CoreExtensibleObject compareCoreExtensibleObject) {

            Boolean isEqual = base.IsEqual ((CoreObject) compareCoreExtensibleObject);


            isEqual &= (extendedProperties.Count == compareCoreExtensibleObject.ExtendedProperties.Count);

            if (isEqual) {

                foreach (String currentPropertyName in compareCoreExtensibleObject.ExtendedProperties.Keys) {

                    isEqual &= (extendedProperties.ContainsKey (currentPropertyName));

                    if (isEqual) {

                        isEqual &= (extendedProperties[currentPropertyName] == compareCoreExtensibleObject.ExtendedProperties[currentPropertyName]);

                    }

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }

        #endregion 

    }

}
