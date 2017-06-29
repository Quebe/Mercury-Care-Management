using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer {

    [DataContract (Name = "DataExplorer")]
    public class DataExplorer : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "IsPublic")]
        private Boolean isPublic = false;

        [DataMember (Name = "IsReadOnly")]
        private Boolean isReadOnly = false;

        [DataMember (Name = "OwnerSecurityAuthorityId")]
        private Int64 ownerSecurityAuthorityId;

        [DataMember (Name = "OwnerUserAccountId")]
        private String ownerUserAccountId;

        [DataMember (Name = "OwnerUserAccountName")]
        private String ownerUserAccountName;

        [DataMember (Name = "OwnerUserDisplayName")]
        private String ownerUserDisplayName;

        [DataMember (Name = "Variables")]
        private Dictionary<String, DataExplorerVariable> variables = new Dictionary<String, DataExplorerVariable> ();

        [DataMember (Name = "RootNode")]
        private DataExplorerNodeSet rootNode = null;

        #endregion 
        

        #region Public Properties

        public Boolean IsPublic { get { return isPublic; } set { isPublic = value; } }

        public Boolean IsReadOnly { get { return isReadOnly; } set { isReadOnly = value; } }

        public Int64 OwnerSecurityAuthorityId { get { return ownerSecurityAuthorityId; } set { ownerSecurityAuthorityId = value; } }

        public String OwnerUserAccountId { get { return ownerUserAccountId; } set { ownerUserAccountId = value; } }

        public String OwnerUserAccountName { get { return ownerUserAccountName; } set { ownerUserAccountName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String OwnerUserDisplayName { get { return ownerUserDisplayName; } set { ownerUserDisplayName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public Dictionary<String, DataExplorerVariable> Variables { get { return variables; } set { variables = value; } }

        public DataExplorerNodeSet RootNode { get { return rootNode; } set { rootNode = value; } }

        #endregion 


        #region Public Properties

        public override Application Application {

            set {

                base.Application = value;

                RootNode.Application = value;

                RootNode.DataExplorer = this;

            }

        }

        #endregion 


        #region Constructors

        public DataExplorer (Application applicationReference) {

            BaseConstructor (applicationReference);

            rootNode = new DataExplorerNodeSet (applicationReference);

            return; 
        
        }

        public DataExplorer (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            // ROOT NODE IS LOADED FROM ID

            return;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            success = base.Load (forId);

            System.Data.DataTable rootNodeTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM DataExplorerNode WHERE DataExplorerId = " + Id.ToString () + " AND ParentDataExplorerNodeId IS NULL");

            if (rootNodeTable.Rows.Count == 1) {

                rootNode = new DataExplorerNodeSet (application);

                rootNode.DataExplorer = this;

                success = rootNode.Load (Convert.ToInt64 (rootNodeTable.Rows[0]["DataExplorerNodeId"]));

            }

            return success;

        }

        public override Boolean Save () {

            Boolean success = false;

            String childIds = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.DataExplorerManage)) { throw new ApplicationException ("PermissionDenied"); }


                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                ModifiedAccountInfo = new Data.AuthorityAccountStamp (application);

                application.EnvironmentDatabase.BeginTransaction ();

                
                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("DataExplorer_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dataExplorerId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dataExplorerName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dataExplorerDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@isPublic", IsPublic);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@isReadOnly", IsReadOnly);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ownerSecurityAuthorityId", OwnerSecurityAuthorityId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ownerUserAccountId", OwnerUserAccountId, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ownerUserAccountName", OwnerUserAccountName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@ownerUserDisplayName", OwnerUserDisplayName, Server.Data.DataTypeConstants.Name);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", Enabled);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@visible", Visible);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@extendedProperties", ExtendedPropertiesXml);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", ModifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", ModifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", ModifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);


                success = (sqlCommand.ExecuteNonQuery () > 0);


                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM DataExplorerNode WHERE DataExplorerId = " + Id.ToString ()); 

                success = RootNode.Save ();

                if (!success) { throw new ApplicationException ("Unable to save Nodes."); }

                application.EnvironmentDatabase.CommitTransaction ();

                success = true;

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        public void MapFormDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            IsPublic = Convert.ToBoolean (currentRow["IsPublic"]);

            IsReadOnly = Convert.ToBoolean (currentRow["IsReadOnly"]);


            OwnerSecurityAuthorityId = base.IdFromSql (currentRow, "OwnerSecurityAuthorityId");

            OwnerUserAccountId = StringFromSql (currentRow, "OwnerUserAccountId");

            OwnerUserAccountName = StringFromSql (currentRow, "OwnerUserAccountName");

            OwnerUserDisplayName = StringFromSql (currentRow, "OwnerUserDisplayName"); 
            
            return;

        }

        #endregion 


        #region Public Methods

        public DataExplorerNode FindNode (Guid nodeInstanceId) { return rootNode.FindNode (nodeInstanceId); }

        #endregion 

    }

}


