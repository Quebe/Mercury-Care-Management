using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Security {

    [DataContract (Name = "SecurityAuthority")]
    public class SecurityAuthority : Core.CoreObject {


        #region Private Properties

        [DataMember (Name = "SecurityAuthorityType")]
        private Enumerations.SecurityAuthorityType securityAuthorityType = Enumerations.SecurityAuthorityType.WindowsIntegrated;


        [DataMember (Name = "Protocol")]
        private String protocol = String.Empty;

        [DataMember (Name = "ServerName")]
        private String serverName = String.Empty;

        [DataMember (Name = "Domain")]
        private String domain = String.Empty;


        [DataMember (Name = "MemberContext")]
        private String memberContext = String.Empty;

        [DataMember (Name = "ProviderContext")]
        private String providerContext = String.Empty;

        [DataMember (Name = "AssociateContext")]
        private String associateContext = String.Empty;


        [DataMember (Name = "AgentName")]
        private String agentName = String.Empty;

        [DataMember (Name = "AgentPassword")]
        private String agentPassword = String.Empty;


        [DataMember (Name = "ProviderAssemblyPath")]
        private String providerAssemblyPath = String.Empty;

        [DataMember (Name = "ProviderAssemblyName")]
        private String providerAssemblyName = String.Empty;

        [DataMember (Name = "ProviderNamespace")]
        private String providerNamespace = String.Empty;

        [DataMember (Name = "ProviderClassName")]
        private String providerClassName = String.Empty;


        [DataMember (Name = "ConfigurationSection")]
        private String configurationSection = String.Empty;

        #endregion 

        
        #region Public Properties

        public Enumerations.SecurityAuthorityType SecurityAuthorityType { get { return securityAuthorityType; } set { securityAuthorityType = value; } }

        public String Protocol { get { return protocol; } set { protocol = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Protocol); } }


        public String ServerName { get { return serverName; } set { serverName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String Domain { get { return domain; } set { domain = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }


        public String MemberContext { get { return memberContext; } set { memberContext = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Context); } }

        public String ProviderContext { get { return providerContext; } set { providerContext = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Context); } }

        public String AssociateContext { get { return associateContext; } set { associateContext = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Context); } }


        public String AgentName { get { return agentName; } set { agentName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String AgentPassword { get { return agentPassword; } set { agentPassword = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Password); } }


        public String ProviderAssemblyPath { get { return providerAssemblyPath; } set { providerAssemblyPath = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Path); } }

        public String ProviderAssemblyName { get { return providerAssemblyName; } set { providerAssemblyName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String ProviderNamespace { get { return providerNamespace; } set { providerNamespace = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Namespace); } }

        public String ProviderClassName { get { return providerClassName; } set { providerClassName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String ConfigurationSection { get { return configurationSection; } set { configurationSection = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        #endregion


        #region Constructors

        public SecurityAuthority (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public SecurityAuthority (Application applicationReference, Int64 securityAuthorityId) {

            base.BaseConstructor (applicationReference, securityAuthorityId);

            return;

        }

        #endregion


        #region Data Operations

        public override Boolean Load (long forId) {

            Boolean success = true;

            String selectStatement = "SELECT * FROM dbo.SecurityAuthority WHERE SecurityAuthorityId = " + forId.ToString ();


            try {

                System.Data.DataTable securityAuthorityTable = application.EnterpriseDatabase.SelectDataTable (selectStatement);

                if (securityAuthorityTable.Rows.Count == 1) {

                    MapDataFields (securityAuthorityTable.Rows[0]);

                }

                else { success = false; }

            }

            catch (Exception loadException) {

                success = false;

                application.TraceWriteLineWarning (application.TraceSwitchGeneral, "Unable to load " + GetType ().ToString () + " from database for Id: " + forId.ToString ());

                application.TraceWriteLineWarning (application.TraceSwitchGeneral, loadException.Message);

            }

             return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            SecurityAuthorityType = (Enumerations.SecurityAuthorityType) (Int32) currentRow["SecurityAuthorityType"];

            Protocol = (String) currentRow["Protocol"];

            ServerName = (String) currentRow["ServerName"];

            Domain = (String) currentRow["Domain"];


            MemberContext = (String) currentRow["MemberContext"];

            ProviderContext = (String) currentRow["ProviderContext"];

            AssociateContext = (String) currentRow["AssociateContext"];



            AgentName = (String) currentRow["AgentName"];

            AgentPassword = (String) currentRow["AgentPassword"];


            ProviderAssemblyPath = (String) currentRow["ProviderAssemblyPath"];

            ProviderAssemblyName = (String) currentRow["ProviderAssemblyName"];

            ProviderNamespace = (String) currentRow["ProviderNamespace"];

            ProviderClassName = (String) currentRow["ProviderClassName"];

            ConfigurationSection = (String) currentRow["ConfigurationSection"];            

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        public Boolean Save (Mercury.Server.Data.SqlDatabase sqlDatabase) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC SecurityAuthority_InsertUpdate ");

            sqlStatement.Append (Id.ToString () + ", '" + NameSql + "', " + ((Int32) SecurityAuthorityType).ToString () + ", ");

            sqlStatement.Append ("'" + Protocol + "', '" + ServerName + "', '" + Domain + "', ");

            sqlStatement.Append ("'" + MemberContext + "', '" + ProviderContext + "', '" + AssociateContext + "', ");

            sqlStatement.Append ("'" + AgentName + "', '" + agentPassword + "', ");

            sqlStatement.Append ("'" + ProviderAssemblyPath + "', '" + ProviderAssemblyName + "', ");

            sqlStatement.Append ("'" + ProviderNamespace + "', '" + ProviderClassName + "', '" + ConfigurationSection + "'");

            sqlStatement.Append (", '" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

            success = sqlDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;

        }

        public Boolean Delete (Mercury.Server.Data.SqlDatabase sqlDatabase) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC SecurityAuthority_Delete " + Id);

            success = sqlDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;

        }

        #endregion

    }

}
