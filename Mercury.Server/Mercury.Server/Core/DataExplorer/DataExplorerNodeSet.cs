using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;

namespace Mercury.Server.Core.DataExplorer {
    
    [DataContract (Name = "DataExplorerNodeSet")]
    public class DataExplorerNodeSet : DataExplorerNode {

        #region Private Properties

        [DataMember (Name = "SetType")]
        private Enumerations.DataExplorerSetType setType = Enumerations.DataExplorerSetType.Intersection;

        #endregion 


        #region Public Properties 
        
        public override Enumerations.DataExplorerNodeResultDataType ResultDataType {

            get { return (Children.Count > 0) ? Children[0].ResultDataType : Enumerations.DataExplorerNodeResultDataType.NotSpecified; }

        }

        public Enumerations.DataExplorerSetType SetType { get { return setType; } set { setType = value; } }

        #endregion 
        

        #region Constructors

        protected DataExplorerNodeSet () { /* DO NOTHING, FOR INHERITANCE */ }
        
        public DataExplorerNodeSet (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return; 
        
        }

        public DataExplorerNodeSet (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);            
            
            return;

        }

        #endregion 
        

        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            success = base.LoadFromTable ("DataExplorerNode", "DataExplorerNodeId", forId);


            System.Data.DataTable childNodesTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM DataExplorerNode WHERE ParentDataExplorerNodeId = " + forId + " ORDER BY DataExplorerNodeId");

            foreach (System.Data.DataRow currentRow in childNodesTable.Rows) {

                // DETERMINE THE TYPE OF CHILD OBJECT TO CREATE 

                Enumerations.DataExplorerNodeType childNodeType = (Enumerations.DataExplorerNodeType)Convert.ToInt32 (currentRow["NodeType"]);

                DataExplorerNode childNode = null;

                switch (childNodeType) {

                    case Enumerations.DataExplorerNodeType.Set:

                        childNode = new DataExplorerNodeSet (application);

                        childNode.Parent = this;

                        success = childNode.Load (Convert.ToInt64 (currentRow ["DataExplorerNodeId"]));

                        break;

                    case Enumerations.DataExplorerNodeType.Evaluation:
                        
                        childNode = new DataExplorerNodeEvaluation (application);

                        childNode.MapDataFields (currentRow);

                        switch (((DataExplorerNodeEvaluation) childNode).EvaluationType) { 

                            case Enumerations.DataExplorerEvaluationType.MemberDemographic: childNode = new Evaluations.DataExplorerNodeEvaluationMemberDemographic (application); break;

                            case Enumerations.DataExplorerEvaluationType.MemberEnrollment: childNode = new Evaluations.DataExplorerNodeEvaluationMemberEnrollment (application); break;

                            case Enumerations.DataExplorerEvaluationType.MemberEnrollmentContinuousFromBirthDate: childNode = new Evaluations.DataExplorerNodeEvaluationMemberEnrollmentContinuousFromBirthDate (application); break;

                            case Enumerations.DataExplorerEvaluationType.MemberMetric: childNode = new Evaluations.DataExplorerNodeEvaluationMemberMetric (application); break;

                            case Enumerations.DataExplorerEvaluationType.MemberService: childNode = new Evaluations.DataExplorerNodeEvaluationMemberService (application); break;

                            case Enumerations.DataExplorerEvaluationType.PopulationMembership: childNode = new Evaluations.DataExplorerNodeEvaluationPopulationMembership (application); break;

                        }

                        childNode.Parent = this;

                        childNode.MapDataFields (currentRow);

                        break;

                }

                if (childNode != null) { Children.Add (childNode); }

            }


            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {
             
            base.MapDataFields (currentRow, "DataExplorerNode");


            if (ExtendedProperties.ContainsKey ("SetTypeInt32")) { SetType = (Enumerations.DataExplorerSetType)Convert.ToInt32 (ExtendedProperties["SetTypeInt32"]); }


            return;

        }

        #endregion 


        #region Public Methods

        public override void RecreateExtendedProperties () {

            base.RecreateExtendedProperties ();

            ExtendedProperties.Add ("SetTypeInt32", ((Int32)SetType).ToString ());

            return;

        }

        public override Int32 Execute () {

            Int32 rowCount = 0;

            String executeStatement = String.Empty;

            String childIdSet = String.Empty;


            if (Children.Count == 0) { return -1; }


            // EXECUTE ALL CHILD NODES TO REBUILD THE DATA SET

            foreach (DataExplorerNode currentChildNode in Children) {

                currentChildNode.Execute ();

                childIdSet += "{" + currentChildNode.NodeInstanceId.ToString () + "}";

            }


            switch (SetType) {

                case Enumerations.DataExplorerSetType.Union:

                    childIdSet = childIdSet.Replace ("}{", "', '");

                    childIdSet = childIdSet.Replace ("{", "'");

                    childIdSet = childIdSet.Replace ("}", "'");

                    executeStatement += "DELETE FROM DataExplorerNodeResult WHERE DataExplorerNodeResult.DataExplorerNodeInstanceId = '" + NodeInstanceId + "'\r\n"; // CLEAR SET RESULTS

                    executeStatement += "INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id)";

                    executeStatement += "  SELECT DISTINCT '" + NodeInstanceId + "', DataExplorerNodeResult.Id";

                    executeStatement += "    FROM DataExplorerNodeResult";

                    executeStatement += "    WHERE DataExplorerNodeInstanceId IN (" + childIdSet + ")";

                    application.EnvironmentDatabase.ExecuteSqlStatement (executeStatement, 0);

                    rowCount = Convert.ToInt32 (application.EnvironmentDatabase.ExecuteScalar ("SELECT COUNT (1) FROM DataExplorerNodeResult WHERE DataExplorerNodeInstanceId = '" + NodeInstanceId.ToString () + "'"));

                    break;

                case Enumerations.DataExplorerSetType.Intersection:

                    executeStatement += "DELETE FROM DataExplorerNodeResult WHERE DataExplorerNodeResult.DataExplorerNodeInstanceId = '" + NodeInstanceId + "'\r\n"; // CLEAR SET RESULTS

                    executeStatement += "INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id)\r\n";

                    executeStatement += "  SELECT DISTINCT '" + NodeInstanceId + "', DataExplorerNodeResult.Id\r\n";

                    executeStatement += "    FROM DataExplorerNodeResult\r\n";


                    // APPEND CHILDREN 2+ AS FULL JOINS

                    for (Int32 currentChildIndex = 1; currentChildIndex < Children.Count; currentChildIndex++) {

                        // SELF-JOIN WHERE RESULT SET IS DIFFERENT

                        executeStatement += "      JOIN DataExplorerNodeResult AS DataExplorerNodeResult" + currentChildIndex + "\r\n";

                        executeStatement += "        ON DataExplorerNodeResult.Id = DataExplorerNodeResult" + currentChildIndex + ".Id\r\n";

                        executeStatement += "        AND DataExplorerNodeResult" + currentChildIndex + ".DataExplorerNodeInstanceId = '" + Children[currentChildIndex].NodeInstanceId.ToString () + "'\r\n";

                    }


                    executeStatement += "    WHERE DataExplorerNodeResult.DataExplorerNodeInstanceId = '" + Children[0].NodeInstanceId + "'\r\n";

                    application.EnvironmentDatabase.ExecuteSqlStatement (executeStatement, 0);

                    rowCount = Convert.ToInt32 (application.EnvironmentDatabase.ExecuteScalar ("SELECT COUNT (1) FROM DataExplorerNodeResult WHERE DataExplorerNodeInstanceId = '" + NodeInstanceId.ToString () + "'"));

                    break;

                case Enumerations.DataExplorerSetType.SymmetricDifference: // ONLY VALID FOR TWO CHILDREN, IF LESS THAN OR MORE, RETURNS -1

                    if (Children.Count != 2) { return -1; }
                    
                    executeStatement += "DELETE FROM DataExplorerNodeResult WHERE DataExplorerNodeResult.DataExplorerNodeInstanceId = '" + NodeInstanceId + "'\r\n"; // CLEAR SET RESULTS

                    executeStatement += "INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id)\r\n";

                    executeStatement += "  SELECT DISTINCT '" + NodeInstanceId + "', ISNULL (Set1.Id, Set2.Id) FROM\r\n";

                    executeStatement += "    (SELECT Id FROM DataExplorerNodeResult WHERE (DataExplorerNodeResult.DataExplorerNodeInstanceId = '" + Children[0].NodeInstanceId.ToString () + "')) AS Set1 \r\n";
                    
			        executeStatement += "    FULL OUTER JOIN (SELECT Id FROM DataExplorerNodeResult WHERE (DataExplorerNodeResult.DataExplorerNodeInstanceId = '" + Children[1].NodeInstanceId.ToString () + "')) AS Set2 \r\n";

                    executeStatement += "    ON Set1.Id = Set2.Id \r\n";

                    executeStatement += "  WHERE ((Set1.Id IS NULL) OR (Set2.Id IS NULL))  \r\n";
			

                    application.EnvironmentDatabase.ExecuteSqlStatement (executeStatement, 0);

                    rowCount = Convert.ToInt32 (application.EnvironmentDatabase.ExecuteScalar ("SELECT COUNT (1) FROM DataExplorerNodeResult WHERE DataExplorerNodeInstanceId = '" + NodeInstanceId.ToString () + "'"));

                    break;

                case Enumerations.DataExplorerSetType.Complement:
                    
                    if (Children.Count != 2) { return -1; }
                    
                    executeStatement += "DELETE FROM DataExplorerNodeResult WHERE DataExplorerNodeResult.DataExplorerNodeInstanceId = '" + NodeInstanceId + "'\r\n"; // CLEAR SET RESULTS

                    executeStatement += "INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id)\r\n";

			
                    executeStatement += "  SELECT DISTINCT '" + NodeInstanceId + "', DataExplorerNodeResult.Id\r\n";

                    executeStatement += "    FROM DataExplorerNodeResult\r\n";
                    

                    // APPEND CHILDREN 2 AS LEFT FULL JOIN

                    executeStatement += "      LEFT JOIN DataExplorerNodeResult AS DataExplorerNodeResult1\r\n";

                    executeStatement += "        ON DataExplorerNodeResult.Id = DataExplorerNodeResult1.Id\r\n";

                    executeStatement += "        AND DataExplorerNodeResult1.DataExplorerNodeInstanceId = '" + Children[1].NodeInstanceId.ToString () + "'\r\n";


                    executeStatement += "    WHERE DataExplorerNodeResult.DataExplorerNodeInstanceId = '" + Children[0].NodeInstanceId + "'\r\n";

                    executeStatement += "      AND DataExplorerNodeResult1.DataExplorerNodeInstanceId IS NULL\r\n";

                    application.EnvironmentDatabase.ExecuteSqlStatement (executeStatement, 0);

                    rowCount = Convert.ToInt32 (application.EnvironmentDatabase.ExecuteScalar ("SELECT COUNT (1) FROM DataExplorerNodeResult WHERE DataExplorerNodeInstanceId = '" + NodeInstanceId.ToString () + "'"));

                    break;

            }

            return rowCount;

        }

        #endregion 

    }

}
