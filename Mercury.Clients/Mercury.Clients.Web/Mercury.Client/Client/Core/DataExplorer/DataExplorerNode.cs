using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer {

    [Serializable]
    public class DataExplorerNode : CoreExtensibleObject {

        #region Private Properties

        private Guid nodeInstanceId = Guid.NewGuid ();

        private Server.Application.DataExplorerNodeType nodeType = Server.Application.DataExplorerNodeType.Set;

        private DataExplorer dataExplorer = null;

        private DataExplorerNode parent = null;

        private List<DataExplorerNode> children = new List<DataExplorerNode> ();

        #endregion


        #region Public Properites

        public Guid NodeInstanceId { get { return nodeInstanceId; } set { nodeInstanceId = value; } }

        public Server.Application.DataExplorerNodeType NodeType { get { return nodeType; } set { nodeType = value; } }

        public virtual Server.Application.DataExplorerNodeResultDataType ResultDataType { get { return Server.Application.DataExplorerNodeResultDataType.NotSpecified; } }

        public virtual Server.Application.DataExplorerNodeResultDataType ResultDetailDataType { get { return Server.Application.DataExplorerNodeResultDataType.NotSpecified; } }

        public DataExplorer DataExplorer { get { return ((parent == null) ? dataExplorer : parent.DataExplorer); } set { dataExplorer = (parent == null) ? value : null; } }

        public DataExplorerNode Parent { get { return parent; } set { parent = value; } }

        public List<DataExplorerNode> Children { get { return children; } set { children = value; } }

        public override Application Application {

            get { return base.Application; }

            set {

                base.Application = value;

                foreach (DataExplorerNode currentChild in children) {

                    currentChild.Application = value;

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

        public DataExplorerNode (Application applicationReference, Mercury.Server.Application.DataExplorerNode serverObject) {

            BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);
            
            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNode serverObject) {

            NodeInstanceId = serverObject.NodeInstanceId;

            NodeType = serverObject.NodeType;

            // TODO: MAP IN CHILDREN

            return;

        }

        public virtual void MapToServerObject (Server.Application.DataExplorerNode serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.NodeInstanceId = nodeInstanceId;

            serverObject.NodeType = nodeType;


            serverObject.Children = new Server.Application.DataExplorerNode[children.Count];

            foreach (DataExplorerNode currentChildNode in children) {

                serverObject.Children[children.IndexOf (currentChildNode)] = (Server.Application.DataExplorerNode)currentChildNode.ToServerObject ();

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNode serverObject = new Server.Application.DataExplorerNode ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public DataExplorerNode Copy () {

            Server.Application.DataExplorerNode serverObject = (Server.Application.DataExplorerNode)ToServerObject ();

            DataExplorerNode copiedObject = new DataExplorerNode (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNode compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);


            // TODO: 


            return isEqual;

        }

        #endregion 


        #region Public Properties

        public DataExplorerNode FindNode (Guid forNodeInstanceId) {

            if (nodeInstanceId == forNodeInstanceId) { return this; }


            DataExplorerNode foundNode = null;


            foreach (DataExplorerNode currentChild in children) {

                foundNode = currentChild.FindNode (forNodeInstanceId);

                if (foundNode != null) { break; }

            }


            return foundNode;

        }

        #endregion 

    }

}
