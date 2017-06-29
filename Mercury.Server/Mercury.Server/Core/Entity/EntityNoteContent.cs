using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Entity {

    [Serializable]
    [DataContract (Name = "EntityNoteContent")]
    public class EntityNoteContent : CoreObject {

        #region Private Properties

        [DataMember (Name = "EntityNoteId")]
        private Int64 entityNoteId;

        [DataMember (Name = "Content")]
        private String content = String.Empty;

        #endregion


        #region Public Properties

        public Int64 EntityNoteId { get { return entityNoteId; } set { entityNoteId = value; } }

        public String Content { get { return content; } set { content = value; } }

        #endregion


        #region Constructors

        public EntityNoteContent (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public EntityNoteContent (Application applicationReference, Int64 forEntityNoteContentId) {

            BaseConstructor (applicationReference);

            if (!Load (forEntityNoteContentId)) {

                throw new ApplicationException ("Unable to load Entity Note Content from database for " + forEntityNoteContentId.ToString () + ".");

            }

        }

        #endregion


        #region Data Functions

        public override Boolean Load (long forId) { return LoadFromDal (forId); }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            
            entityNoteId = (Int64) currentRow["EntityNoteId"];

            content = (String) currentRow["Content"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            Boolean usingTransaction = true;


            if (Id != 0) { return true; } // CANNOT MODIFIY EXISTING NOTE CONTENTS


            ModifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application);

            try {

                if (application.EnvironmentDatabase.OpenTransactions == 0) {

                    usingTransaction = true;

                    base.application.EnvironmentDatabase.BeginTransaction ();

                }

                System.Data.SqlClient.SqlCommand insertCommand = application.EnvironmentDatabase.CreateSqlCommand ("dal.EntityNoteContent_Insert");

                insertCommand.CommandType = System.Data.CommandType.StoredProcedure;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@entityNoteId", System.Data.SqlDbType.BigInt));

                insertCommand.Parameters["@entityNoteId"].Value = entityNoteId;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@content", System.Data.SqlDbType.Text));

                insertCommand.Parameters["@content"].Value = content;


                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAuthorityName", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAuthorityName"].Value = modifiedAccountInfo.SecurityAuthorityName;

                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAccountId", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAccountId"].Value = modifiedAccountInfo.UserAccountId;

                insertCommand.Parameters.Add (new System.Data.SqlClient.SqlParameter ("@modifiedAccountName", System.Data.SqlDbType.VarChar, 60));

                insertCommand.Parameters["@modifiedAccountName"].Value = modifiedAccountInfo.UserAccountName;


                if (insertCommand.ExecuteNonQuery () != 1) {

                    base.application.SetLastException (base.application.EnvironmentDatabase.LastException);

                    throw base.application.EnvironmentDatabase.LastException;

                }


                if (id == 0) { // RESET DOCUMENT ID CRITERIA

                    Object identity = base.application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (!Int64.TryParse ((String) identity, out id)) {

                        throw new ApplicationException ("Unable to retreive unique id.");

                    }

                }

                success = true;


                if (usingTransaction) { base.application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (usingTransaction) { base.application.EnvironmentDatabase.RollbackTransaction (); }

                base.application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion


    }

}
