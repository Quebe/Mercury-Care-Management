using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Security {

    [DataContract (Name = "EnvironmentAccess")]
    public class EnvironmentAccess : Core.CoreObject {

        #region Private Properties

        [DataMember (Name = "EnvironmentId")]
        private Int64 environmentId;

        [DataMember (Name = "SecurityAuthorityId")]
        private Int64 securityAuthorityId;

        [DataMember (Name = "SecurityGroupId")]
        private String securityGroupId;

        [DataMember (Name = "SecurityGroupName")]
        private String securityGroupName;


        [DataMember (Name = "IsGranted")]
        private Boolean isGranted;

        [DataMember (Name = "IsDenied")]
        private Boolean isDenied;

        #endregion

        
        #region Public Properties

        public Int64 EnvironmentId { get { return environmentId; } set { environmentId = value; } } // Property: EnvironmentId 

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } } // Property: SecurityAuthorityId 

        public String SecurityGroupId { get { return securityGroupId; } set { securityGroupId = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } } // Property: SecurityGroupId 

        public String SecurityGroupName { get { return securityGroupName; } set { securityGroupName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }


        public Boolean IsGranted { get { return isGranted; } set { isGranted = value; } }

        public Boolean IsDenied { get { return isDenied; } set { isDenied = value; } }
        
        #endregion


        #region Constructors

        public EnvironmentAccess (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public EnvironmentAccess (Application applicationReference, Int64 environmentId, Int64 authorityId, String securityGroupId) {

            base.BaseConstructor (applicationReference);

            if (!LoadFromDatabase (environmentId, authorityId, securityGroupId)) {

                throw new Exception ("Unable to load Environment Access from database.");

            }

        }

        #endregion


        #region Database Functions

        private Boolean LoadFromDatabase (String selectStatement) {

            System.Data.DataTable environmentAccessTable;

            environmentAccessTable = application.EnterpriseDatabase.SelectDataTable (selectStatement);

            if (environmentAccessTable.Rows.Count == 1) {

                MapDataFields (environmentAccessTable.Rows[0]);
                
            }

            else {

                return false;

            }

            return true;

        }

        private Boolean LoadFromDatabase (Int64 environmentId, Int64 authorityId, String securityGroupId) {

            StringBuilder selectStatement = new StringBuilder ();

            selectStatement.Append ("SELECT * FROM EnvironmentAccess");
            selectStatement.Append ("  WHERE EnvironmentId = " + environmentId);
            selectStatement.Append ("    AND SecurityAuthorityId = " + authorityId);
            selectStatement.Append ("    AND SecurityGroupId = '" + securityGroupId + "'");

            return LoadFromDatabase (selectStatement.ToString ());

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            environmentId  = (Int64)   currentRow["EnvironmentId"];

            securityAuthorityId = (Int64) currentRow["SecurityAuthorityId"];

            SecurityGroupId = (String) currentRow["SecurityGroupId"];


            if (currentRow.Table.Columns.Contains ("SecurityGroupName")) {

                SecurityGroupName = (String)currentRow["SecurityGroupName"];

            }


            isGranted     = (Boolean) currentRow["IsGranted"];

            isDenied      = (Boolean) currentRow["IsDenied"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC EnvironmentAccess_InsertUpdate " + environmentId + ", " + securityAuthorityId + ", '" + securityGroupId + "', ");
            sqlStatement.Append (Convert.ToByte (isGranted) + ", " + Convert.ToByte (isDenied));
            sqlStatement.Append (", '" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");

            success = application.EnterpriseDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;

        }

        #endregion 

    }

}
