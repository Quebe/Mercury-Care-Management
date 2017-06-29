using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Security {

    [DataContract (Name = "Permission")]
    public class Permission : Core.CoreObject {


        #region Constructors

        public Permission () {

            return;
            
        }

        public Permission (Mercury.Server.Data.SqlDatabase sqlDatabase, String forPermissionName) {

            if (!LoadFromDatabase (sqlDatabase, forPermissionName)) {

                throw new Exception ("Unable to load Permission from the database for " + forPermissionName + ".");

            }

        }

        #endregion

        #region Database Functions

        public Boolean LoadFromDatabaseWithCriteria (Mercury.Server.Data.SqlDatabase sqlDatabase, String criteria) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEnvironment;

            selectStatement.Append ("SELECT * FROM Permission ");

            selectStatement.Append (criteria);

            tableEnvironment = sqlDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEnvironment.Rows.Count == 1) {

                MapDataFields (tableEnvironment.Rows[0]);
                
                return true;

            }

            else {

                return false;

            }

        }

        public Boolean LoadFromDatabase (Mercury.Server.Data.SqlDatabase sqlDatabase, String permission) {

            String criteria;

            criteria = "WHERE PermissionName = '" + permission + "'";

            return LoadFromDatabaseWithCriteria (sqlDatabase, criteria);

        }

        #endregion


    }

}
