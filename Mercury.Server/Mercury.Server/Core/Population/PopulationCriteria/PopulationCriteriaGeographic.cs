using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Population.PopulationCriteria {

    [DataContract (Name = "PopulationCriteriaGeographic")]
    public class PopulationCriteriaGeographic : CoreObject {

        #region Private Properties

        [DataMember (Name = "PopulationId")]
        private Int64 populationId;

        [DataMember (Name = "State")]
        private String state;

        [DataMember (Name = "City")]
        private String city;

        [DataMember (Name = "County")]
        private String county;

        [DataMember (Name = "ZipCode")]
        private String zipCode;

        #endregion


        #region Public Properties

        public Int64 PopulationId { get { return populationId; } set { populationId = value; } }

        public String State { get { return state; } set { state = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressState); } }

        public String City { get { return city; } set { city = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressCity); } }

        public String County { get { return county; } set { county = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressCounty); } }

        public String ZipCode { get { return zipCode; } set { zipCode = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.AddressZipCode); } }

        #endregion
        

        #region Constructors

        public PopulationCriteriaGeographic (Application applicationReference) { base.BaseConstructor (applicationReference); }

        public PopulationCriteriaGeographic (Application applicationReference, Int64 forId) { base.BaseConstructor (applicationReference, forId); }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "City", city);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "State", state);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "County", county);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ZipCode", zipCode);

            #endregion


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":

                            #region Properties

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].Value) {

                                    case "City": City = currentPropertyNode.InnerText; break;

                                    case "State": State = currentPropertyNode.InnerText; break;

                                    case "County": County = currentPropertyNode.InnerText; break;

                                    case "ZipCode": ZipCode = currentPropertyNode.InnerText; break;

                                }

                            }

                            #endregion

                            break;

                    } // switch (currentNode.Name) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                // SAVE IS PERFORMED BY PARENT OBJECT

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Database Functions

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;


            try {

                ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.PopulationCriteriaGeographic_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (populationId.ToString () + ", ");

                sqlStatement.Append ("'" + state + "', ");

                sqlStatement.Append ("'" + city + "', ");

                sqlStatement.Append ("'" + county + "', ");

                sqlStatement.Append ("'" + zipCode + "', ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                success = true;

            }

            catch (Exception applicationException) {

                application.SetLastException (applicationException);

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            populationId = (Int64) currentRow["PopulationId"];

            state = (String) currentRow["State"];

            city = (String) currentRow["City"];

            county = (String) currentRow["County"];

            zipCode = (String) currentRow["ZipCode"];


            return;

        }

        #endregion

    }

}
