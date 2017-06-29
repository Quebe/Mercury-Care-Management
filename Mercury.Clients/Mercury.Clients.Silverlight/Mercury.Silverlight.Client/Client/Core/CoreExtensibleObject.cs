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

    public class CoreExtensibleObject : CoreObject {

        #region Private Properties

        private Dictionary<String, String> extendedProperties = new Dictionary<String, String> ();

        #endregion


        #region Public Properties

        virtual public Dictionary<String, String> ExtendedProperties { get { return extendedProperties; } set { extendedProperties = value; } }

        #endregion


        #region Extended Properties

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


            // COPY EXTENDED PROPERTIES, DO NOT SET BY REFERENCE

            extendedProperties = new Dictionary<String, String> ();

            if (forCoreExtensibleObject.ExtendedProperties == null) { forCoreExtensibleObject.ExtendedProperties = new Dictionary<String, String> (); }

            foreach (String currentPropertyName in forCoreExtensibleObject.ExtendedProperties.Keys) {

                extendedProperties.Add (currentPropertyName, forCoreExtensibleObject.ExtendedProperties[currentPropertyName]);

            }

            return;

        }

        #endregion


        #region Public Methods

        public void MapToServerObject (Server.Application.CoreExtensibleObject coreExtensibleObject) {

            base.MapToServerObject ((Server.Application.CoreObject)coreExtensibleObject);


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

            Boolean isEqual = base.IsEqual ((CoreObject)compareCoreExtensibleObject);


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
