using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core {

    [Serializable]
    [DataContract (Name = "CoreExtensibleObject ")]

    [KnownType (typeof (CoreConfigurationObject))]

    [KnownType (typeof (Server.Core.Entity.EntityCorrespondence))]
    [KnownType (typeof (Server.Core.Work.WorkQueueItem))]

    [KnownType (typeof (Server.Core.Individual.CarePlanGoal))]
    [KnownType (typeof (Server.Core.Individual.Case.MemberCase))]
    [KnownType (typeof (Server.Core.DataExplorer.DataExplorerNode))]
    [KnownType (typeof (Server.Core.Individual.Case.Views.MemberCaseCarePlanSummary))]
    public class CoreExtensibleObject : CoreObject {

        #region Private Properties
        
        [DataMember (Name = "ExtendedProperties")]
        private Dictionary<String, String> extendedProperties = new Dictionary<String, String> ();

        #endregion


        #region Public Properties

        virtual public Dictionary<String, String> ExtendedProperties { 
            
            get {

                if (extendedProperties == null) { extendedProperties = new Dictionary<String, String> (); }
    
                return extendedProperties; 
            
            } 
            
            set { 
                
                extendedProperties = value;

                if (extendedProperties == null) { extendedProperties = new Dictionary<String, String> (); }

            } 
        
        }

        #endregion


        #region Extended Properties

        public virtual System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                extendedProperties = ExtendedProperties; // REASSIGN FROM PUBLIC PROPERTY IN CASE THE PUBLIC PROPERTY HAS BEEN OVERRIDDEN, THIS BECOMES A LOCAL REFERENCE IN A SENSE


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

                // USE PUBLIC PROPERTY FOR ASSIGNMENT IN CASE OF OVERRIDE

                String propertyName = currentPropertyNode.Attributes["Name"].InnerText.Trim ();

                if (!ExtendedProperties.ContainsKey (propertyName)) {

                    ExtendedProperties.Add (propertyName, currentPropertyNode.InnerText.Trim ());

                }

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


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode coreObjectNode = document.ChildNodes[1];

            coreObjectNode.AppendChild (document.ImportNode (ExtendedPropertiesXml.ChildNodes[1], true));

            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode extendedPropertiesNode;

            try {

                extendedPropertiesNode = objectNode.SelectSingleNode ("ExtendedProperties");

                ExtendedPropertiesDeserialize (extendedPropertiesNode);

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 


        #region Database Functions

        public override void MapDataFields (System.Data.DataRow currentRow, String forColumnPrefix) {

            base.MapDataFields (currentRow, forColumnPrefix);


            if (currentRow.Table.Columns.Contains ("ExtendedProperties")) {

                extendedProperties = new Dictionary<String, String> ();

                if (!(currentRow["ExtendedProperties"] is DBNull)) {

                    System.Xml.XmlDocument extendedPropertiesXml = new System.Xml.XmlDocument ();

                    if (!(currentRow["ExtendedProperties"] is System.DBNull)) {

                        if (!String.IsNullOrEmpty ((String)currentRow["ExtendedProperties"])) {

                            extendedPropertiesXml.LoadXml ((String)currentRow["ExtendedProperties"]);

                            ExtendedPropertiesDeserialize (extendedPropertiesXml.LastChild);

                        }

                    }

                }

            }

            return;

        }
        
        #endregion

    }

}
