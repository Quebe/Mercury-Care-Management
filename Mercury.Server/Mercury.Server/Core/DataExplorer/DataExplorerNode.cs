using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.DataExplorer {

    [KnownType (typeof (DataExplorerNodeEvaluation))]
    [KnownType (typeof (DataExplorerNodeSet))]
    [DataContract (Name = "DataExplorerNode")]
    public class DataExplorerNode : CoreExtensibleObject {

        #region Private Properties

        [DataMember (Name = "NodeInstanceId")]
        private Guid nodeInstanceId = Guid.NewGuid ();

        [DataMember (Name = "NodeType")]
        private Enumerations.DataExplorerNodeType nodeType = Enumerations.DataExplorerNodeType.Set;

        private DataExplorer dataExplorer = null;

        private DataExplorerNode parent = null;

        [DataMember (Name = "Children")]
        private List<DataExplorerNode> children = new List<DataExplorerNode> ();

        #endregion 


        #region Public Properites

        public Guid NodeInstanceId { get { return nodeInstanceId; } set { nodeInstanceId = value; } }

        public Enumerations.DataExplorerNodeType NodeType { get { return nodeType; } set { nodeType = value; } }

        public virtual Enumerations.DataExplorerNodeResultDataType ResultDataType { get { return Enumerations.DataExplorerNodeResultDataType.NotSpecified; } }

        public virtual Enumerations.DataExplorerNodeResultDataType ResultDetailDataType { get { return Enumerations.DataExplorerNodeResultDataType.NotSpecified; } }

        public DataExplorer DataExplorer { get { return ((parent == null) ? dataExplorer : parent.DataExplorer); } set { dataExplorer = (parent == null) ? value : null; } }

        public DataExplorerNode Parent { get { return parent; } set { parent = value; } }

        public List<DataExplorerNode> Children { get { return children; } set { children = value; } }

        public override Application Application {

            get { return base.Application; }

            set {

                base.Application = value;

                foreach (DataExplorerNode currentChild in children) {

                    currentChild.Application = value;

                    currentChild.Parent = this;

                }

            }

        }

        #endregion 


        #region Constructors

        protected DataExplorerNode () { /* DO NOTHING, FOR INHERITANCE */ }
        
        public DataExplorerNode (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return; 
        
        }

        public DataExplorerNode (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion 


        #region Data Functions

        public override Boolean Save () {

            Boolean success = false;

            String childIds = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.DataExplorerManage)) { throw new ApplicationException ("PermissionDenied"); }


                if (String.IsNullOrWhiteSpace (Name)) { Name = "Unnamed"; }

                Description = Name;


                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                RecreateExtendedProperties ();

                ModifiedAccountInfo = new Data.AuthorityAccountStamp (application);

                id = 0; // RESET ID FOR SAVE (ALWAYS INSERT, NO UPDATE)


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("DataExplorerNode_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dataExplorerNodeId", 0); // ALWAYS FORCE NEW WRITE OF OBJECT

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dataExplorerNodeName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dataExplorerNodeDescription", Description, Server.Data.DataTypeConstants.Description);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@dataExplorerId", DataExplorer.Id);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@nodeType", ((Int32) nodeType));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@parentDataExplorerNodeId", base.IdSqlAllowNullInt64 ((parent == null) ? 0 : parent.Id));


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", 1); // RESERVED FOR FUTURE

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@visible", 1); // RESERVED FOR FUTURE

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


                foreach (DataExplorerNode currentChildNode in Children) {

                    currentChildNode.Parent = this;

                    success = currentChildNode.Save ();

                    if (!success) { throw new ApplicationException ("Unable to save Child Nodes."); }                   

                }



                if (!success) { throw new ApplicationException ("Unable to save Nodes."); }

                success = true;

            }

            catch (Exception applicationException) {

                success = false;

                application.SetLastException (applicationException);

            }

            return success;

        }        

        #endregion 


        #region Public Methods

        public virtual void RecreateExtendedProperties () {

            ExtendedProperties = new Dictionary<String, String> ();

            // BASE HAS NO ADDITIONAL EXTENDED PROPERTIES, ONLY CHILD OBJECTS

            return;

        }

        public DataExplorerNode FindNode (Guid forNodeInstanceId) {

            if (nodeInstanceId == forNodeInstanceId) { return this; }


            DataExplorerNode foundNode = null;


            foreach (DataExplorerNode currentChild in children) {

                foundNode = currentChild.FindNode (forNodeInstanceId);

                if (foundNode != null) { break; }

            }


            return foundNode;

        }

        public virtual Int32 Execute () { return -1; }

        #endregion 

    }

}
