using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer {
    
    [Serializable]
    public class DataExplorer : CoreConfigurationObject {

        #region Private Properties

        private Boolean isPublic = false;

        private Int64 ownerSecurityAuthorityId;

        private String ownerUserAccountId;

        private String ownerUserAccountName;

        private String ownerUserDisplayName;

        private SortedDictionary<String, Server.Application.DataExplorerVariable> variables = new SortedDictionary<String, Server.Application.DataExplorerVariable> ();

        private DataExplorerNodeSet rootNode = null;

        #endregion 


        #region Public Properties - Encapsulated

        public Boolean IsPublic { get { return isPublic; } set { isPublic = value; } }

        public Int64 OwnerSecurityAuthorityId { get { return ownerSecurityAuthorityId; } set { ownerSecurityAuthorityId = value; } }

        public String OwnerUserAccountId { get { return ownerUserAccountId; } set { ownerUserAccountId = value; } }

        public String OwnerUserAccountName { get { return ownerUserAccountName; } set { ownerUserAccountName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String OwnerUserDisplayName { get { return ownerUserDisplayName; } set { ownerUserDisplayName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public SortedDictionary<String, Server.Application.DataExplorerVariable> Variables { get { return variables; } set { variables = value; } }

        public DataExplorerNodeSet RootNode { get { return rootNode; } set { rootNode = value; } }

        #endregion 
        

        #region Constructors

        public DataExplorer (Application applicationReference) {

            BaseConstructor (applicationReference);

            rootNode = new DataExplorerNodeSet (applicationReference);

            rootNode.DataExplorer = this;

            return;

        }

        public DataExplorer (Application applicationReference, Mercury.Server.Application.DataExplorer serverObject) {

            BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorer serverObject) {

            isPublic = serverObject.IsPublic;

            ownerSecurityAuthorityId = serverObject.OwnerSecurityAuthorityId;

            ownerUserAccountId = serverObject.OwnerUserAccountId;

            ownerUserAccountName = serverObject.OwnerUserAccountName;

            ownerUserDisplayName = serverObject.OwnerUserDisplayName;


            rootNode = new DataExplorerNodeSet (application, serverObject.RootNode);

            rootNode.DataExplorer = this; 


            return;

        }

        public virtual void MapToServerObject (Server.Application.DataExplorer serverObject) {

            base.MapToServerObject ((Server.Application.CoreExtensibleObject)serverObject);


            serverObject.IsPublic = IsPublic;

            serverObject.OwnerSecurityAuthorityId = OwnerSecurityAuthorityId;

            serverObject.OwnerUserAccountId = OwnerUserAccountId;

            serverObject.OwnerUserAccountName = OwnerUserAccountName;

            serverObject.OwnerUserDisplayName = OwnerUserDisplayName;


            serverObject.RootNode = (Server.Application.DataExplorerNodeSet) rootNode.ToServerObject ();

            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorer serverObject = new Server.Application.DataExplorer ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public DataExplorer Copy () {

            Server.Application.DataExplorer serverObject = (Server.Application.DataExplorer)ToServerObject ();

            DataExplorer copiedObject = new DataExplorer (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorer compareObject) {

            Boolean isEqual = base.IsEqual ((CoreExtensibleObject)compareObject);



            isEqual &= (IsPublic == compareObject.IsPublic);



            return isEqual;

        }

        #endregion 


        #region Public Methods

        public void AddVariable () {

            Server.Application.DataExplorerVariable variable = new Server.Application.DataExplorerVariable ();

            variable.Name = Guid.NewGuid ().ToString ();

            variable.Description = variable.Name;

            variables.Add (variable.Name, variable);

            return;

        }

        public void RemoveVariable (String variableName) {

            if (variables.ContainsKey (variableName)) {

                variables.Remove (variableName);

                // TODO: PROPOGATE VARIABLE REMOVAL THROUGHOUT NODE CHANGE

            }

            return;

        }

        public DataExplorerNode FindNode (Guid nodeInstanceId) { return rootNode.FindNode (nodeInstanceId); }

        public Server.Application.DataExplorerNode FindServerNode (Server.Application.DataExplorerNode currentNode, Guid nodeInstanceId) {

            if (currentNode.NodeInstanceId == nodeInstanceId) { return currentNode; }

            Server.Application.DataExplorerNode foundNode = null;


            foreach (Server.Application.DataExplorerNode currentChildNode in currentNode.Children) {

                foundNode = FindServerNode (currentChildNode, nodeInstanceId);

                if (foundNode != null) { break; }

            }


            return foundNode;

        }

        #endregion 

    }

}
