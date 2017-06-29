using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.DataExplorer {

    [Serializable]
    public class DataExplorerNodeSet : DataExplorerNode {

        #region Private Properties

        private Server.Application.DataExplorerSetType setType = Server.Application.DataExplorerSetType.Intersection;

        #endregion


        #region Public Properties

        public override Server.Application.DataExplorerNodeResultDataType ResultDataType {

            get { return (Children.Count > 0) ? Children[0].ResultDataType : Server.Application.DataExplorerNodeResultDataType.NotSpecified; }

        }

        public Server.Application.DataExplorerSetType SetType { get { return setType; } set { setType = value; } }

        #endregion 

        
        #region Constructors
        
        public DataExplorerNodeSet (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public DataExplorerNodeSet (Application applicationReference, Mercury.Server.Application.DataExplorerNodeSet serverObject) {

            BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);
            
            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Server.Application.DataExplorerNodeSet serverObject) {

            setType = serverObject.SetType;


            foreach (Server.Application.DataExplorerNode currentChildNode in serverObject.Children) {

                DataExplorerNode childNode = null;

                switch (currentChildNode.NodeType) {

                    case Server.Application.DataExplorerNodeType.Set:

                        childNode = new DataExplorerNodeSet (application, (Server.Application.DataExplorerNodeSet)currentChildNode);

                        childNode.Parent = this;

                        break;

                    case Server.Application.DataExplorerNodeType.Evaluation:

                        childNode = new DataExplorerNodeEvaluation (application, (Server.Application.DataExplorerNodeEvaluation)currentChildNode);

                        switch (((DataExplorerNodeEvaluation)childNode).EvaluationType) {

                            case Server.Application.DataExplorerEvaluationType.MemberDemographic: childNode = new Evaluations.DataExplorerNodeEvaluationMemberDemographic (application, ((Server.Application.DataExplorerNodeEvaluationMemberDemographic)currentChildNode)); break;

                            case Server.Application.DataExplorerEvaluationType.MemberEnrollment: childNode = new Evaluations.DataExplorerNodeEvaluationMemberEnrollment (application, ((Server.Application.DataExplorerNodeEvaluationMemberEnrollment)currentChildNode)); break;

                            case Server.Application.DataExplorerEvaluationType.MemberEnrollmentContinuousFromBirthDate: childNode = new Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate (application, ((Server.Application.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate)currentChildNode)); break;

                            case Server.Application.DataExplorerEvaluationType.MemberMetric: childNode = new Evaluations.DataExplorerNodeEvaluationMemberMetric (application, ((Server.Application.DataExplorerNodeEvaluationMemberMetric)currentChildNode)); break;

                            case Server.Application.DataExplorerEvaluationType.MemberService: childNode = new Evaluations.DataExplorerNodeEvaluationMemberService (application, ((Server.Application.DataExplorerNodeEvaluationMemberService)currentChildNode)); break;

                            case Server.Application.DataExplorerEvaluationType.PopulationMembership: childNode = new Evaluations.DataExplorerNodeEvaluationPopulationMembership (application, ((Server.Application.DataExplorerNodeEvaluationPopulationMembership)currentChildNode)); break;

                        }

                        childNode.Parent = this;

                        break;

                }

                if (childNode != null) { Children.Add (childNode); }

            }

            return;

        }

        public virtual void MapToServerObject (Server.Application.DataExplorerNodeSet serverObject) {

            base.MapToServerObject ((Server.Application.DataExplorerNode)serverObject);


            serverObject.SetType = setType;

            // TODO: CHILDREN

            return;

        }

        public override Object ToServerObject () {

            Server.Application.DataExplorerNodeSet serverObject = new Server.Application.DataExplorerNodeSet ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new DataExplorerNodeSet Copy () {

            Server.Application.DataExplorerNodeSet serverObject = (Server.Application.DataExplorerNodeSet)ToServerObject ();

            DataExplorerNodeSet copiedObject = new DataExplorerNodeSet (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (DataExplorerNodeSet compareObject) {

            Boolean isEqual = base.IsEqual ((DataExplorerNode)compareObject);


            // TODO: 


            return isEqual;

        }

        #endregion 


        #region Public Properties

        public DataExplorerNodeSet AddNodeSet (Server.Application.DataExplorerSetType forSetType) {

            DataExplorerNodeSet nodeSet = new DataExplorerNodeSet (application);

            nodeSet.SetType = forSetType;

            nodeSet.Parent = this;

            Children.Add (nodeSet);

            return nodeSet;

        }

        public DataExplorerNodeEvaluation AddNodeEvaluation (Server.Application.DataExplorerEvaluationType forEvaluationType) {

            DataExplorerNodeEvaluation evaluationNode = null;

            switch (forEvaluationType) {

                case Server.Application.DataExplorerEvaluationType.MemberDemographic:

                    evaluationNode = new Evaluations.DataExplorerNodeEvaluationMemberDemographic (application);

                    break;

                case Server.Application.DataExplorerEvaluationType.MemberEnrollment:

                    evaluationNode = new Evaluations.DataExplorerNodeEvaluationMemberEnrollment (application);

                    break;

                case Server.Application.DataExplorerEvaluationType.MemberEnrollmentContinuousFromBirthDate:

                    evaluationNode = new Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate (application);

                    break;

                case Server.Application.DataExplorerEvaluationType.MemberMetric:

                    evaluationNode = new Evaluations.DataExplorerNodeEvaluationMemberMetric (application);

                    break;

                case Server.Application.DataExplorerEvaluationType.MemberService:

                    evaluationNode = new Evaluations.DataExplorerNodeEvaluationMemberService (application);

                    break;

                case Server.Application.DataExplorerEvaluationType.PopulationMembership:

                    evaluationNode = new Evaluations.DataExplorerNodeEvaluationPopulationMembership (application);

                    break;

            }


            if (evaluationNode != null) {

                evaluationNode.Parent = this;

                Children.Add (evaluationNode);

            }

            return evaluationNode;

        }

        #endregion 

    }

}
