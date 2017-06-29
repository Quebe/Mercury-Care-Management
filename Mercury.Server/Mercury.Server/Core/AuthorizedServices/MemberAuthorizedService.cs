using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.AuthorizedServices {
    
    [DataContract (Name="MemberAuthorizedService")]
    public class MemberAuthorizedService : CoreObject {
        
        #region Private Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "AuthorizedServiceId")]
        private Int64 authorizedServiceId;

        [DataMember (Name = "EventDate")]
        private DateTime eventDate = new DateTime (1900, 01, 01);

        [DataMember (Name = "InitialIdentifiedDate")]
        private DateTime initialIdentifiedDate = new DateTime (1900, 01, 01);

        [DataMember (Name = "AddedManually")]
        private Boolean addedManually = false;

        [DataMember (Name = "AuthorizedService")]
        private AuthorizedService authorizedService = null;

        #endregion


        #region Public Properties
        
        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 AuthorizedServiceId { get { return authorizedServiceId; } set { authorizedServiceId = value; } }        

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }

        public DateTime InitialIdentifiedDate { get { return initialIdentifiedDate; } set { initialIdentifiedDate = value; } }

        public Boolean AddedManually { get { return addedManually; } set { addedManually = value; } }

        public AuthorizedService AuthorizedService { get { return authorizedService; } set { authorizedService = value; } } 

        #endregion


        #region Public Properties

        public List<MemberAuthorizedServiceDetail> Details { get { return application.MemberAuthorizedServiceDetailsGet (id); } }

        #endregion 


        #region Constructors

        public MemberAuthorizedService (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public MemberAuthorizedService (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Public Methods

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            
            memberId = (Int64) currentRow["MemberId"];

            authorizedServiceId = (Int64) currentRow["AuthorizedServiceId"];

            eventDate = (DateTime) currentRow["EventDate"];

            initialIdentifiedDate = (DateTime) currentRow["InitialIdentifiedDate"];

            addedManually = (Boolean) currentRow["AddedManually"];



            if (currentRow.Table.Columns.Contains ("AuthorizedServiceId1")) {

                System.Data.DataRow serviceRow = currentRow.Table.Copy ().Rows[currentRow.Table.Rows.IndexOf (currentRow)];

                while (serviceRow.Table.Columns[0].ColumnName != "AuthorizedServiceId1") { serviceRow.Table.Columns.RemoveAt (0); }

                foreach (System.Data.DataColumn currentColumn in serviceRow.Table.Columns) {

                    if (currentColumn.ColumnName.EndsWith ("1")) {

                        currentColumn.ColumnName = currentColumn.ColumnName.Substring (0, currentColumn.ColumnName.Length - 1);

                    }

                }

                authorizedService = new AuthorizedService (application);

                authorizedService.MapDataFields (serviceRow);

            }

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;

            ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.MemberAuthorizedService_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (memberId.ToString () + ", ");

                sqlStatement.Append (authorizedServiceId.ToString () + ", ");

                sqlStatement.Append ("'" + eventDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append ("'" + initialIdentifiedDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append (Convert.ToInt32 (addedManually).ToString () + ", ");

                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                SetIdentity ();


                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
