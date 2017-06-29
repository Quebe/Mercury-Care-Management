using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.MedicalServices {

    [DataContract (Name="MemberService")]
    public class MemberService : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;

        [DataMember (Name = "EventDate")]
        private DateTime eventDate = new DateTime (1900, 01, 01);

        [DataMember (Name = "AddedManually")]
        private Boolean addedManually = false;

        [DataMember (Name = "Service")]
        private Service service = null;

        #endregion


        #region Public Properties

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }

        public Boolean AddedManually { get { return addedManually; } set { addedManually = value; } }
 

        #endregion


        #region Public Properties

        public Service Service { 
            
            get {

                if (service == null) { 
                    
                    service = application.MedicalServiceGet (serviceId);

                    switch (service.ServiceType) {

                        case Enumerations.MedicalServiceType.Singleton: service = application.MedicalServiceSingletonGet (serviceId); break;

                        case Enumerations.MedicalServiceType.Set: service = application.MedicalServiceSetGet (serviceId); break;

                    }
                
                }
                                
                return service; 
            
            } 
            
            set { service = value; } 
        
        }

        public Enumerations.MedicalServiceType ServiceType { get { return ((Service == null) ? Enumerations.MedicalServiceType.NotSpecified : Service.ServiceType); } }

        public List<MemberServiceDetailSet> DetailsSet { get { return application.MemberServiceDetailSetGet (id); } }

        public List<MemberServiceDetailSingleton> DetailsSingleton { get { return application.MemberServiceDetailSingletonGet (id); } }

        #endregion 


        #region Constructors

        public MemberService (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public MemberService (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Public Methods

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            
            memberId = (Int64) currentRow["MemberId"];
            
            serviceId = (Int64) currentRow["ServiceId"];

            eventDate = (DateTime) currentRow["EventDate"];

            addedManually = (Boolean) currentRow["AddedManually"];


            createAccountInfo.MapDataFields (currentRow, "Create");

            modifiedAccountInfo.MapDataFields (currentRow, "Modified");


            if (currentRow.Table.Columns.Contains ("ServiceId1")) {

                System.Data.DataRow serviceRow = currentRow.Table.Copy ().Rows[currentRow.Table.Rows.IndexOf (currentRow)];

                while (serviceRow.Table.Columns[0].ColumnName != "ServiceId1") { serviceRow.Table.Columns.RemoveAt (0); }

                foreach (System.Data.DataColumn currentColumn in serviceRow.Table.Columns) {

                    if (currentColumn.ColumnName.EndsWith ("1")) {

                        currentColumn.ColumnName = currentColumn.ColumnName.Substring (0, currentColumn.ColumnName.Length - 1);

                    }

                }

                service = new Service (application);

                service.MapDataFields (serviceRow);

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

                sqlStatement.Append ("EXEC dbo.MemberService_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (memberId.ToString () + ", ");

                sqlStatement.Append (serviceId.ToString () + ", ");

                sqlStatement.Append ("'" + eventDate.ToString ("MM/dd/yyyy") + "', ");

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
