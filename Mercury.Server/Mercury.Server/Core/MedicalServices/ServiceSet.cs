using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.MedicalServices {

    [DataContract (Name = "ServiceSet")]
    public class ServiceSet : Service {

        #region Private Properties 

        [DataMember (Name = "Definitions")]
        private List<Definitions.ServiceSetDefinition> definitions = new List<Mercury.Server.Core.MedicalServices.Definitions.ServiceSetDefinition> ();

        private List<Definitions.ServiceSetDefinition> activeDefinitions = new List<Mercury.Server.Core.MedicalServices.Definitions.ServiceSetDefinition> ();

        #endregion 


        #region Public Properties

        public List<Definitions.ServiceSetDefinition> Definitions { get { return definitions; } set { definitions = value; } }

        public List<Definitions.ServiceSetDefinition> ActiveDefinitions {

            get {

                if (activeDefinitions != null) {

                    if (activeDefinitions.Count > 0) { return activeDefinitions; }

                }

                activeDefinitions = new List<Mercury.Server.Core.MedicalServices.Definitions.ServiceSetDefinition> ();

                foreach (Definitions.ServiceSetDefinition currentDefinition in definitions) {

                    if (currentDefinition.Enabled) { activeDefinitions.Add (currentDefinition); }

                }

                return activeDefinitions;

            }

        }

        //public override System.Xml.XmlDocument ExtendedProperties {

        //    get {

        //        System.Xml.XmlDocument extendedProperties = base.ExtendedProperties;

        //        base.ExtendedProperties_AddProperty (extendedProperties, "SetType", ((Int32) setType).ToString ());

        //        base.ExtendedProperties_AddProperty (extendedProperties, "WithinDays", withinDays.ToString ());

        //        return extendedProperties;

        //    }

        //}


        public override Application Application {

            set {

                base.Application = value;

                foreach (MedicalServices.Definitions.ServiceSetDefinition currentDefinition in definitions) {

                    currentDefinition.Application = value;

                }

            }

        } 

        #endregion


        #region Constructors

        public ServiceSet (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public ServiceSet (Application applicationReference, Int64 forServiceId) {

            BaseConstructor (applicationReference, forServiceId);

            return;

        }

        #endregion


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode definitionsNode = document.CreateElement ("Definitions");

            document.ChildNodes[1].AppendChild (definitionsNode);


            foreach (MedicalServices.Definitions.ServiceSetDefinition currentDefinition in definitions) {

                try {

                    currentDefinition.Application = application;

                    System.Xml.XmlNode definitionNode = currentDefinition.XmlSerialize ().LastChild;

                    definitionsNode.AppendChild (document.ImportNode (definitionNode, true));

                }

                catch (Exception applicationException) {

                    System.Diagnostics.Debug.WriteLine (applicationException.Message);

                }

            }


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode definitionsNode;

            String exceptionMessage = String.Empty;


            try {

                definitionsNode = objectNode.SelectSingleNode ("Definitions");

                foreach (System.Xml.XmlNode currentDefinitionNode in definitionsNode.ChildNodes) {

                    MedicalServices.Definitions.ServiceSetDefinition definition = new Mercury.Server.Core.MedicalServices.Definitions.ServiceSetDefinition (application);

                    response.AddRange (definition.XmlImport (currentDefinitionNode));

                    definition.ServiceId = this.Id;

                    definitions.Add (definition);

                }

                if (!Save ()) { throw new ApplicationException ("Unable to import Service Set."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 


        #region Database Methods

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            System.Data.DataTable definitionTable;


            success = base.Load (forId);

            if (success) {

                sqlStatement.Append ("SELECT ServiceSetDefinition.*, Service.ServiceName");

                sqlStatement.Append ("  FROM ServiceSetDefinition ");

                sqlStatement.Append ("    JOIN Service ON ServiceSetDefinition.DefinitionServiceId = Service.ServiceId");

                sqlStatement.Append ("  WHERE ServiceSetDefinition.ServiceId = " + forId);

                sqlStatement.Append ("  ORDER BY Service.ServiceName");

                definitionTable = base.application.EnvironmentDatabase.SelectDataTable (sqlStatement.ToString ());

                foreach (System.Data.DataRow currentRow in definitionTable.Rows) {

                    MedicalServices.Definitions.ServiceSetDefinition setDefinition = new Mercury.Server.Core.MedicalServices.Definitions.ServiceSetDefinition (application);

                    setDefinition.ServiceId = Id;

                    setDefinition.MapDataFields (currentRow);

                    definitions.Add (setDefinition);

                }

            }


            return success;

        }
    
        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;


            try {

                application.EnvironmentDatabase.BeginTransaction ();

                success = base.Save ();

                if (!success) { throw new ApplicationException (application.LastException.Message); }



                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("DELETE FROM ServiceSetDefinition WHERE ServiceId = " + Id.ToString ());

                if (definitions.Count > 0) {

                    String definitionIdString = String.Empty;

                    foreach (MedicalServices.Definitions.ServiceSetDefinition currentDefinition in definitions) {

                        definitionIdString = definitionIdString + "{" + currentDefinition.Id.ToString () + "}";

                    }

                    if (definitionIdString.Length != 0) {

                        definitionIdString = definitionIdString.Replace ("}{", ", ");

                        definitionIdString = definitionIdString.Replace ("{", "(");

                        definitionIdString = definitionIdString.Replace ("}", ")");

                        definitionIdString = " AND ServiceSetDefinitionId NOT IN " + definitionIdString;

                    }

                    sqlStatement.Append (definitionIdString);

                }

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                foreach (MedicalServices.Definitions.ServiceSetDefinition setDefinition in definitions) {

                    setDefinition.ServiceId = Id;

                    success = setDefinition.Save (application);

                    if (!success) { throw base.application.EnvironmentDatabase.LastException; }

                }

                base.application.EnvironmentDatabase.CommitTransaction ();

                success = true;

            }

            catch (Exception applicationException) {

                base.application.EnvironmentDatabase.RollbackTransaction ();

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        public override Boolean Delete () {

            Boolean success = false;

            StringBuilder sqlStatement;


            try {

                base.application.EnvironmentDatabase.BeginTransaction ();


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("DELETE FROM ServiceSetDefinition WHERE ServiceId = " + Id.ToString ());

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("DELETE FROM Service WHERE ServiceId = " + Id);

                success = base.application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) { throw base.application.EnvironmentDatabase.LastException; }


                base.application.EnvironmentDatabase.CommitTransaction ();

                success = true;

            }

            catch (Exception applicationException) {

                base.application.EnvironmentDatabase.RollbackTransaction ();

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion


        #region Public Methods

        override public Int32 Depth (Application application) {

            Int32 maxdepth = 0;

            Int32 currentDepth = 0;

            foreach (MedicalServices.Definitions.ServiceSetDefinition currentDefinition in definitions) {

                MedicalServices.Service medicalService = new Service (application, currentDefinition.DefinitionServiceId);

                switch (medicalService.ServiceType) {

                    case Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set:

                        ServiceSet serviceSet = new ServiceSet (application, currentDefinition.DefinitionServiceId);

                        currentDepth = serviceSet.Depth (application);

                        if (maxdepth < currentDepth) { maxdepth = currentDepth; }

                        break;

                }

            }

            maxdepth = maxdepth + 1;

            return maxdepth;

        }

        protected String IntersectionSqlStatement (out String virtualJoinTables) {

            String sqlStatement = String.Empty;

            String selectClause = String.Empty;

            String virtualPrimaryTable = String.Empty;

            virtualJoinTables = String.Empty;

            foreach (MedicalServices.Definitions.ServiceSetDefinition currentDefinition in ActiveDefinitions) {

                if (currentDefinition.Enabled) {

                    String virtualTableName = "Table" + currentDefinition.Id.ToString ();

                    if (String.IsNullOrEmpty (virtualPrimaryTable)) {

                        virtualPrimaryTable = virtualTableName;

                        virtualJoinTables = virtualPrimaryTable;

                        sqlStatement = "(" + currentDefinition.SqlStatement + ") AS " + virtualPrimaryTable;

                    }

                    else {

                        sqlStatement = sqlStatement + "\r\n    JOIN ( \r\n      " + currentDefinition.SqlStatement + "\r\n    ) AS " + virtualTableName;

                        sqlStatement = sqlStatement + "    ON (" + virtualPrimaryTable + ".MemberId = " + virtualTableName + ".MemberId)";

                        foreach (String currentTable in virtualJoinTables.Split (';')) {

                            sqlStatement = sqlStatement + "\r\n      AND (" + currentTable + ".DetailMemberServiceId != " + virtualTableName + ".DetailMemberServiceId)";

                            sqlStatement = sqlStatement + "\r\n      AND (ABS (DATEDIFF (DD, " + currentTable + ".EventDate, " + virtualTableName + ".EventDate)) <= " + WithinDays.ToString () + ")";

                            // TODO AND NOT PREVIOUS USED IN ANOTHER DETAIL SET OF THE SAME TYPE

                        }

                        virtualJoinTables = virtualJoinTables + ";" + virtualTableName;

                    }

                }

            }

            selectClause = "SELECT " + virtualPrimaryTable + ".MemberId AS MemberServiceMemberId, ";

            foreach (String currentTable in virtualJoinTables.Split (';')) {

                selectClause = selectClause + "\r\n      " + currentTable + ".ServiceSetDefinitionId AS " + currentTable + "ServiceSetDefinitionId, ";

                selectClause = selectClause + "\r\n      " + currentTable + ".EventDate AS " + currentTable + "EventDate, ";

                selectClause = selectClause + "\r\n      " + currentTable + ".DetailMemberServiceId AS " + currentTable + "DetailMemberServiceId, ";

                selectClause = selectClause + "\r\n      " + currentTable + ".ServiceId AS " + currentTable + "ServiceId, ";

                selectClause = selectClause + "\r\n      " + currentTable + ".ServiceName AS " + currentTable + "ServiceName, ";

                selectClause = selectClause + "\r\n      " + currentTable + ".ServiceType AS " + currentTable + "ServiceType, ";
            }

            selectClause = selectClause + "\r\n      '' AS EmptyColumn \r\n    FROM \r\n";

            sqlStatement = selectClause + sqlStatement;

            sqlStatement = sqlStatement + "\r\n  ORDER BY MemberServiceMemberId, " + virtualPrimaryTable + "EventDate DESC, " + virtualPrimaryTable + "ServiceId";

            return sqlStatement;

        }

        public List<MemberServiceDetailSet> Preview (Application application) {

            List<MemberServiceDetailSet> previewResults = new List<MemberServiceDetailSet> ();

            MemberServiceDetailSet detailResult;

            System.Data.DataTable resultsTable;

            String sqlStatement = String.Empty;


            String identifiedMemberServices = String.Empty;


            switch (SetType) {

                case Mercury.Server.Core.MedicalServices.Enumerations.ServiceSetType.Union: 

                    #region Union Set

                    foreach (MedicalServices.Definitions.ServiceSetDefinition currentDefinition in ActiveDefinitions) {

                        if (currentDefinition.Enabled) {

                            if (sqlStatement != String.Empty) { sqlStatement = sqlStatement + "\r\n UNION "; }

                            sqlStatement = sqlStatement + currentDefinition.SqlStatement;

                        }

                    }

                    if (!String.IsNullOrEmpty (sqlStatement)) {

                        sqlStatement = sqlStatement.Replace ("SELECT", "SELECT TOP 10");

                        sqlStatement = sqlStatement.Replace ("LEFT JOIN MemberServiceDetailSet ON MemberService.MemberServiceId = MemberServiceDetailSet.MemberServiceId", "");

                        sqlStatement = sqlStatement.Replace ("AND MemberServiceDetailSet.ParentServiceId = " + Id.ToString (), "");

                        sqlStatement = sqlStatement.Replace ("AND MemberServiceDetailSet.MemberServiceId IS NULL", "");

                        resultsTable = base.application.EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                        foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                            detailResult = new MemberServiceDetailSet (0, (Int64) currentRow ["ServiceSetDefinitionId"]);

                            detailResult.MapDataFields (currentRow);

                            previewResults.Add (detailResult);

                        }

                    }

                    #endregion

                    break;

                case Mercury.Server.Core.MedicalServices.Enumerations.ServiceSetType.Intersection:

                    #region Intersection Set

                    String virtualJoinTables = String.Empty;


                    sqlStatement = IntersectionSqlStatement (out virtualJoinTables);

                    sqlStatement = "SELECT TOP 10 " + sqlStatement.Substring (6, sqlStatement.Length - 6);

                    resultsTable = base.application.EnvironmentDatabase.SelectDataTable (sqlStatement, 0);

                    foreach (System.Data.DataRow currentRow in resultsTable.Rows) {
                        
                        Int64 memberId = (Int64) currentRow ["MemberServiceMemberId"];

                        DateTime maxEventDate = new DateTime (1900, 1, 1);
                       
                        String uniqueServiceKey;

                        foreach (String currentTable in virtualJoinTables.Split (';')) {

                            if (maxEventDate < ((DateTime) currentRow [currentTable + "EventDate"])) {

                                maxEventDate = (DateTime) currentRow [currentTable + "EventDate"];

                            }

                        }

                        uniqueServiceKey = memberId.ToString () + ":" + maxEventDate.ToString ("MM/dd/yyyy");

                        if (!identifiedMemberServices.Contains (uniqueServiceKey)) {

                            identifiedMemberServices = identifiedMemberServices + uniqueServiceKey + "|";

                            foreach (String currentTable in virtualJoinTables.Split (';')) {

                                detailResult = new MemberServiceDetailSet (memberId, (Int64) currentRow[currentTable + "ServiceSetDefinitionId"]);

                                detailResult.MemberId = memberId;

                                detailResult.DetailMemberServiceId = (Int64) currentRow[currentTable + "DetailMemberServiceId"];

                                detailResult.EventDate = (DateTime) currentRow[currentTable + "EventDate"];

                                detailResult.ServiceId = (Int64) currentRow[currentTable + "ServiceId"];

                                detailResult.ServiceName = (String) currentRow[currentTable + "ServiceName"];

                                detailResult.ServiceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) (Int32) currentRow[currentTable + "ServiceType"];

                                previewResults.Add (detailResult);

                            }

                        }

                    }

                    #endregion

                    break;

            }

            return previewResults;

        }


        protected String IntersectionSetMemberServiceSql () {

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("\r\n\r\n\r\nDECLARE @SetMemberService AS TABLE (");
            sqlStatement.Append ("  SetDefinitionId BIGINT, ");
            sqlStatement.Append ("  MemberServiceId BIGINT, ");
            sqlStatement.Append ("  MemberId        BIGINT, ");
            sqlStatement.Append ("  ServiceId       BIGINT, ");
            sqlStatement.Append ("  EventDate     DATETIME, ");
            sqlStatement.Append ("  ServiceName    VARCHAR (060), ");
            sqlStatement.Append ("  ServiceType        INT) \r\n");

            sqlStatement.Append ("\r\n  INSERT INTO @SetMemberService ");
            sqlStatement.Append ("\r\n    SELECT ");
            sqlStatement.Append ("\r\n      Definition.ServiceSetDefinitionId AS SetDefinitionId,    -- DEFINITION ID");
            sqlStatement.Append ("\r\n      MemberService.MemberServiceId, -- DEFINITION IDENTIFIED MEMBER SERVICE");
            sqlStatement.Append ("\r\n      MemberService.MemberId,        -- DEFINITION IDENTIFIED MEMBER SERVICE");
            sqlStatement.Append ("\r\n      MemberService.ServiceId,       -- DEFINITION SERVICE ID (NOT THE SET SERVICE ID)");
            sqlStatement.Append ("\r\n      MemberService.EventDate,       -- DEFINITION IDENTIFIED MEMBER SERVICE");
            sqlStatement.Append ("\r\n      Service.ServiceName,");
            sqlStatement.Append ("\r\n      Service.ServiceType");
            
            sqlStatement.Append ("\r\n    FROM ");
            sqlStatement.Append ("\r\n      -- " + ServiceName);

            sqlStatement.Append ("\r\n        ServiceSetDefinition AS Definition");

            sqlStatement.Append ("\r\n        -- IDENTIFY MEMBER SERVICES BY DEFINITIONS FOR ANALYSIS");
            sqlStatement.Append ("\r\n        JOIN MemberService");
            sqlStatement.Append ("\r\n          ON Definition.DefinitionServiceId = MemberService.ServiceId");

            sqlStatement.Append ("\r\n        -- SERVICE DEFINITION FOR MEMBER SERVICE");
            sqlStatement.Append ("\r\n        JOIN Service AS Service");
            sqlStatement.Append ("\r\n          ON MemberService.ServiceId = Service.ServiceId");

            sqlStatement.Append ("\r\n        -- NOT PREVIOUSLY USED BY THE SAME SET FOR A DIFFERENT INSTANCE");
            sqlStatement.Append ("\r\n        LEFT JOIN MemberServiceDetailSet AS UtilizedMemberService");
            sqlStatement.Append ("\r\n          ON MemberService.MemberServiceId = UtilizedMemberService.DetailMemberServiceId");
            sqlStatement.Append ("\r\n          AND UtilizedMemberService.ParentServiceId = " + Id.ToString ());
  
            sqlStatement.Append ("\r\n    WHERE ");
            sqlStatement.Append ("\r\n      (Definition.ServiceId = " + Id.ToString () + ") AND (UtilizedMemberService.DetailMemberServiceId IS NULL)");
            sqlStatement.Append ("\r\n      AND (Definition.Enabled = 1)");

            return sqlStatement.ToString ();

        }

        protected String IntersectionSetMemberServiceSortedSql () {

            StringBuilder sqlStatement = new StringBuilder ();


            sqlStatement.Append ("\r\n");

            sqlStatement.Append ("DECLARE @SetMemberServiceSorted AS TABLE (Id BIGINT, MemberId BIGINT, EventDate DATETIME, ");

            for (Int32 currentDefinition = 1; currentDefinition <= ActiveDefinitions.Count; currentDefinition++) {

                sqlStatement.Append ("Service" + currentDefinition.ToString () + "_SetDefinitionId BIGINT, ");

                sqlStatement.Append ("Service" + currentDefinition.ToString () + "_MemberServiceId BIGINT, ");

                sqlStatement.Append ("Service" + currentDefinition.ToString () + "_ServiceId BIGINT, ");

                sqlStatement.Append ("Service" + currentDefinition.ToString () + "_EventDate DATETIME, ");

                sqlStatement.Append ("Service" + currentDefinition.ToString () + "_ServiceName VARCHAR (060), ");

                sqlStatement.Append ("Service" + currentDefinition.ToString () + "_ServiceType INT" + ((currentDefinition < ActiveDefinitions.Count) ? ", " : ""));

            }

            sqlStatement.Append (")\r\n");


            sqlStatement.Append ("\r\nINSERT INTO @SetMemberServiceSorted\r\n");

            sqlStatement.Append ("SELECT ROW_NUMBER () OVER (ORDER BY Service1.MemberId, Service" + ActiveDefinitions.Count.ToString () + ".EventDate, ");

            for (Int32 currentDefinition = 1; currentDefinition <= ActiveDefinitions.Count; currentDefinition++) {

                sqlStatement.Append ("Service" + currentDefinition.ToString () + ".EventDate" + ((currentDefinition < ActiveDefinitions.Count) ? ", " : ""));

            }

            sqlStatement.Append (") AS Id,");

            sqlStatement.Append ("\r\n    Service1.MemberId AS MemberId, ");

            sqlStatement.Append ("\r\n    Service" + ActiveDefinitions.Count.ToString () + ".EventDate AS EventDate, ");


            for (Int32 currentDefinition = 1; currentDefinition <= ActiveDefinitions.Count; currentDefinition++) {

                sqlStatement.Append ("\r\n    Service" + currentDefinition.ToString () + ".SetDefinitionId AS Service" + currentDefinition.ToString () + "_SetDefinitionId, ");

                sqlStatement.Append ("\r\n    Service" + currentDefinition.ToString () + ".MemberServiceId AS Service" + currentDefinition.ToString () + "_MemberServiceId, ");

                sqlStatement.Append ("\r\n    Service" + currentDefinition.ToString () + ".ServiceId       AS Service" + currentDefinition.ToString () + "_ServiceId, ");

                sqlStatement.Append ("\r\n    Service" + currentDefinition.ToString () + ".EventDate       AS Service" + currentDefinition.ToString () + "_EventDate, ");

                sqlStatement.Append ("\r\n    Service" + currentDefinition.ToString () + ".ServiceName     AS Service" + currentDefinition.ToString () + "_ServiceName, ");

                sqlStatement.Append ("\r\n    Service" + currentDefinition.ToString () + ".ServiceType     AS Service" + currentDefinition.ToString () + "_ServiceType" + ((currentDefinition < ActiveDefinitions.Count) ? ", " : ""));

            }

            sqlStatement.Append ("\r\n  FROM ");

            sqlStatement.Append ("\r\n      -- " + ServiceName);

            sqlStatement.Append ("\r\n    @SetMemberService AS Service1");


            for (Int32 currentDefinition = 2; currentDefinition <= ActiveDefinitions.Count; currentDefinition++) {
                
                sqlStatement.Append ("\r\n        JOIN @SetMemberService AS Service" + currentDefinition.ToString ());
                sqlStatement.Append ("\r\n          ON ");
                sqlStatement.Append ("\r\n              (Service" + (currentDefinition - 1).ToString () + ".MemberId = Service" + currentDefinition.ToString () + ".MemberId)                   -- SAME MEMBER ");
                sqlStatement.Append ("\r\n          AND (Service" + (currentDefinition - 1).ToString () + ".MemberServiceId <> Service" + currentDefinition.ToString () + ".MemberServiceId)    -- DIFFERENT SERVICES (SERVICE CAN ONLY BE USE ONCE)");
                sqlStatement.Append ("\r\n          AND (");
                sqlStatement.Append ("\r\n                (Service" + (currentDefinition - 1).ToString () + ".EventDate < Service" + currentDefinition.ToString () + ".EventDate) -- SEQUENCE BY EVENT DATE");
                sqlStatement.Append ("\r\n            OR ((Service" + (currentDefinition - 1).ToString () + ".EventDate = Service" + currentDefinition.ToString () + ".EventDate) AND (Service" + (currentDefinition - 1).ToString () + ".MemberServiceId < Service" + currentDefinition.ToString () + ".MemberServiceId))) -- SAME EVENT DATE, SEQUENCE BY MEMBER SERVICE ID");
                
                if ((currentDefinition) > 1) {

                    for (Int32 previousDefinition = 1; previousDefinition < currentDefinition; previousDefinition++) {

                        sqlStatement.Append ("\r\n          AND (Service" + previousDefinition.ToString () + ".SetDefinitionId <> Service" + currentDefinition.ToString () + ".SetDefinitionId)");

                    }

                    sqlStatement.Append ("\r\n          AND (DATEDIFF (DD, Service1.EventDate, Service" + currentDefinition.ToString () + ".EventDate) <= " + WithinDays.ToString () + ")");

                }

                if (currentDefinition == ActiveDefinitions.Count) {

//                    sqlStatement.Append ("\r\n          AND (DATEDIFF (DD, Service1.EventDate, Service" + currentDefinition.ToString () + ".EventDate) <= " + withinDays.ToString () + ")");

                }

            }


            return sqlStatement.ToString ();

        }

        protected String IntersectionSetMemberServiceFilteredSql () {

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("\r\nSELECT SetMemberServiceFiltered.* ");

            sqlStatement.Append ("\r\n  FROM ");
            sqlStatement.Append ("\r\n      -- " + ServiceName);
            sqlStatement.Append ("\r\n    @SetMemberServiceSorted AS SetMemberServiceFiltered");
            sqlStatement.Append ("\r\n          JOIN (");
            sqlStatement.Append ("\r\n            SELECT MinimumMemberService.MemberId, MinimumMemberService.EventDate, MIN (Id) AS Id ");
            sqlStatement.Append ("\r\n              FROM @SetMemberServiceSorted AS MinimumMemberService");
            sqlStatement.Append ("\r\n                JOIN (SELECT MemberId, MIN (EventDate) AS EventDate FROM @SetMemberServiceSorted GROUP BY MemberId) AS MinimumEventDate");
            sqlStatement.Append ("\r\n                  ON MinimumMemberService.MemberId = MinimumEventDate.MemberId AND MinimumMemberService.EventDate = MinimumEventDate.EventDate");
            sqlStatement.Append ("\r\n              GROUP BY MinimumMemberService.MemberId, MinimumMemberService.EventDate");
            sqlStatement.Append ("\r\n            ) AS MinimumMemberEvent ON SetMemberServiceFiltered.Id = MinimumMemberEvent.Id");

            sqlStatement.Append ("\r\n          LEFT JOIN MemberService");
            sqlStatement.Append ("\r\n            ON  SetMemberServiceFiltered.MemberId  = MemberService.MemberId");
            sqlStatement.Append ("\r\n            AND SetMemberServiceFiltered.EventDate = MemberService.EventDate");
            sqlStatement.Append ("\r\n            AND MemberService.ServiceId = " + Id.ToString ());

            sqlStatement.Append ("\r\n      WHERE (MemberService.MemberServiceId IS NULL)");

            sqlStatement.Append ("\r\n      ORDER BY SetMemberServiceFiltered.MemberId, SetMemberServiceFiltered.EventDate");

            return sqlStatement.ToString ();

        }


        protected Boolean ProcessIntersectionSetLarge () {

            Boolean success = true;

            String procedureStatement = String.Empty;

            System.Data.DataTable uniqueMemberTable;

            Int32 memberCount = 0;

            Int32 currentMember = 0;

            Int32 memberStep;

            const Int32 stepDivisor = 200;



            procedureStatement = IntersectionSetMemberServiceSql ();

            procedureStatement = procedureStatement + "  \r\n  SELECT DISTINCT MemberId FROM @SetMemberService";


            uniqueMemberTable = base.application.EnvironmentDatabase.SelectDataTable (procedureStatement);

            memberCount = uniqueMemberTable.Rows.Count;

            memberStep = memberCount / stepDivisor;

            if (memberStep == 0) { memberStep = stepDivisor; }

            else if (memberStep < 25) { memberStep = 25; }


            while (currentMember < memberCount) { 

                String memberCriteria = String.Empty;

                Int32 maxRowIndex = ((currentMember + memberStep) < memberCount) ? (currentMember + memberStep) : memberCount;

                for (Int32 currentMemberIndex = currentMember; currentMemberIndex < maxRowIndex; currentMemberIndex++) {

                    memberCriteria = memberCriteria + "{" + ((Int64) uniqueMemberTable.Rows[currentMemberIndex]["MemberId"]).ToString () + "}";

                }

                Int32 memberProcessCount = maxRowIndex - currentMember + 1;

                currentMember = maxRowIndex;


                memberCriteria = memberCriteria.Replace ("}{", ", ");

                memberCriteria = memberCriteria.Replace ("{", "(");

                memberCriteria = memberCriteria.Replace ("}", ")");

                memberCriteria = "AND (MemberService.MemberId IN " + memberCriteria + ")";


                procedureStatement = String.Empty;

                procedureStatement = procedureStatement + IntersectionSetMemberServiceSql () + memberCriteria;

                procedureStatement = procedureStatement + IntersectionSetMemberServiceSortedSql ();

                procedureStatement = procedureStatement + IntersectionSetMemberServiceFilteredSql ();



                base.ProcessStep_StartStep ("Service Set", Name + " (Large): " + currentMember.ToString () + " | " + memberCount.ToString (), procedureStatement);

                success = ProcessIntersectionSet (procedureStatement, false);

                base.ProcessStep_StopStep ((success) ? "Success: " + memberProcessCount.ToString () : "Failure", (success) ? String.Empty : base.application.LastException.Message);

                if (!success) { break; }

            }
                
            return true;

        }

        protected Boolean ProcessIntersectionSet (String forStatement, Boolean outputStep) {

            Boolean success = true;

            String procedureStatement = String.Empty;

            Int32 currentPass = 0;

            Int64 currentRowIndex = 0;

            System.Data.DataTable memberServiceTable = new System.Data.DataTable();

            System.Diagnostics.Debug.WriteLine ("\r\n\r\n ----- " + Name + " -----");

            try {

                do {

                    currentPass = currentPass + 1;


                    if (String.IsNullOrEmpty (forStatement)) {

                        procedureStatement = String.Empty;

                        procedureStatement = procedureStatement + IntersectionSetMemberServiceSql ();

                        procedureStatement = procedureStatement + IntersectionSetMemberServiceSortedSql ();

                        procedureStatement = procedureStatement + IntersectionSetMemberServiceFilteredSql ();

                    }

                    else { procedureStatement = forStatement; }


                    System.Diagnostics.Debug.WriteLine (procedureStatement);

                    if (outputStep) { base.ProcessStep_StartStep ("Service Set", Name + ": " + currentPass, procedureStatement); }

                    memberServiceTable = base.application.EnvironmentDatabase.SelectDataTable (procedureStatement, 86400);

                    if (base.application.EnvironmentDatabase.LastException != null) { throw base.application.EnvironmentDatabase.LastException; }

                    currentRowIndex = 0;

                    foreach (System.Data.DataRow currentRow in memberServiceTable.Rows) {

                        currentRowIndex = currentRowIndex + 1;

                        MemberService memberService = new MemberService (application);

                        memberService.MemberId = (Int64) currentRow["MemberId"];

                        memberService.ServiceId = Id;

                        memberService.EventDate = (DateTime) currentRow["EventDate"];

                        memberService.AddedManually = false;

                        success = memberService.Save (base.application);

                        if (success) {

                            for (Int32 currentDefinition = 1; currentDefinition <= ActiveDefinitions.Count; currentDefinition++) {

                                String fieldPrefix = "Service" + currentDefinition.ToString () + "_";


                                // THE BELOW SHOULD BE COLUMN "SetDefinitionId" VERSUS "ServiceSetDefinitionId" FROM DYNAMIC SQL

                                MemberServiceDetailSet memberServiceDetail = new MemberServiceDetailSet (memberService.Id, (Int64) currentRow[fieldPrefix + "SetDefinitionId"]);

                                memberServiceDetail.MemberId = memberService.MemberId;

                                memberServiceDetail.DetailMemberServiceId = (Int64) currentRow[fieldPrefix + "MemberServiceId"];

                                memberServiceDetail.EventDate = (DateTime) currentRow[fieldPrefix + "EventDate"];

                                memberServiceDetail.ParentServiceId = Id;

                                memberServiceDetail.ServiceId = (Int64) currentRow[fieldPrefix + "ServiceId"];

                                memberServiceDetail.ServiceName = (String) currentRow[fieldPrefix + "ServiceName"];

                                memberServiceDetail.ServiceType = (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) (Int32) currentRow[fieldPrefix + "ServiceType"];

                                success = memberServiceDetail.Save (base.application);

                                if (!success) { break; }

                            }

                        }

                    }

                    if (outputStep) { base.ProcessStep_StopStep ((success) ? "Success: " + memberServiceTable.Rows.Count.ToString () : "Failure", ""); }

                    if (!success) { break; }

                } while (memberServiceTable.Rows.Count != 0);

            }

            catch (Exception executionException) {

                success = false;

                base.application.SetLastException (executionException);

                if (outputStep) { base.ProcessStep_StopStep ((success) ? "Success: " + memberServiceTable.Rows.Count.ToString () : "Failure", executionException.Message); }

            }

            return success;

        }

        public Boolean Process () {

            Boolean success = true;

            String selectStatement = String.Empty;
            
            System.Data.DataTable resultsTable;

            
            Int64 memberServiceId;

            MemberService memberService;

            MemberServiceDetailSet memberServiceDetail;


            if (!Enabled) { return false; }


            base.ProcessLog_StartProcess ();

            switch (SetType) {

                case Mercury.Server.Core.MedicalServices.Enumerations.ServiceSetType.Union:

                    #region Union Set

                    foreach (MedicalServices.Definitions.ServiceSetDefinition currentDefinition in ActiveDefinitions) {

                        if (currentDefinition.Enabled) {

                            selectStatement = currentDefinition.SqlStatement;

                            if (!String.IsNullOrEmpty (selectStatement)) {

                                resultsTable = base.application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                                foreach (System.Data.DataRow currentRow in resultsTable.Rows) {

                                    memberServiceId = base.application.MemberServiceGetId ((Int64) currentRow["MemberId"], Id, (DateTime) currentRow["EventDate"]);

                                    if (memberServiceId == 0) {

                                        memberService = new MemberService (application);

                                        memberService.MemberId = (Int64) currentRow["MemberId"];

                                        memberService.ServiceId = Id;

                                        memberService.EventDate = (DateTime) currentRow["EventDate"];

                                        memberService.AddedManually = false;

                                        success = memberService.Save (base.application);

                                        memberServiceId = memberService.Id;

                                    }

                                    if ((success) && (memberServiceId != 0)) {

                                        memberServiceDetail = new MemberServiceDetailSet (memberServiceId, currentDefinition.Id);

                                        memberServiceDetail.MapDataFields (currentRow);

                                        success = memberServiceDetail.Save (base.application);

                                    }

                                    if (!success) { break; }

                                }

                            }

                        }

                        if (!success) { break; }

                    }


                    #endregion

                    break;

                case Mercury.Server.Core.MedicalServices.Enumerations.ServiceSetType.Intersection:

                    if (definitions.Count >= 4) { success = ProcessIntersectionSetLarge (); }

                    else { success = ProcessIntersectionSet (String.Empty, true); }

                    break;

            }

            base.ProcessLog_StopProcess ((success) ? "Success" : "Failure", "");
            
            return success;

        }

        #endregion

    }

}
