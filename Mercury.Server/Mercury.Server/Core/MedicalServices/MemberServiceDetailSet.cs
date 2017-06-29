using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.MedicalServices {

    [DataContract (Name = "MemberServiceDetailSet")]
    public class MemberServiceDetailSet {

        #region Private Properties

        [DataMember (Name = "MemberServiceId")]         
        private Int64 memberServiceId;                      // PARENT MEMBER SERVICE ENTRY (WHO OWNS THE DETAIL)

        [DataMember (Name = "SetDefinitionId")]
        private Int64 setDefinitionId;                      // PARENT MEMBER SERVICE DEFINITION ID

        [DataMember (Name = "DetailMemberServiceId")]
        private Int64 detailMemberServiceId;                // CHILD MEMBER SERVICE ENTRY (WHAT MEMBER SERVICE IS THE ACTUAL DETAIL)

        [DataMember (Name = "MemberId")]                    
        private Int64 memberId;                             // PARENT AND CHILD MEMBER ID

        [DataMember (Name = "EventDate")]
        private DateTime eventDate;                         // CHILD MEMBER SERVICE EVENT DATE

        [DataMember (Name = "ParentServiceId")]
        private Int64 parentServiceId;                      // PARENT MEMBER SERVICE SERVICE ID (OWNER SERVICE ID)

        [DataMember (Name = "ServiceId")]
        private Int64 serviceId;                            // CHILD MEMBER SERVICE SERVICE ID (DETAIL SERVICE ID)

        [DataMember (Name = "ServiceName")]
        private String serviceName;                         // CHILD MEMBER SERVICE NAME

        [DataMember (Name = "ServiceType")]
        private Core.MedicalServices.Enumerations.MedicalServiceType serviceType = Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.NotSpecified;

        #endregion


        #region Public Properties

        public Int64 MemberServiceId { get { return memberServiceId; } set { memberServiceId = value; } }

        public Int64 SetDefinitionId { get { return setDefinitionId; } set { setDefinitionId = value; } }

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 DetailMemberServiceId { get { return detailMemberServiceId; } set { detailMemberServiceId = value; } }

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }

        public Int64 ParentServiceId { get { return parentServiceId; } set { parentServiceId = value; } }

        public Int64 ServiceId { get { return serviceId; } set { serviceId = value; } }

        public String ServiceName { get { return serviceName; } set { serviceName = value; } }

        public Enumerations.MedicalServiceType ServiceType { get { return serviceType; } set { serviceType = value; } }

        #endregion


        #region Constructors

        public MemberServiceDetailSet () { return; }

        public MemberServiceDetailSet (Int64 forMemberServiceId, Int64 forSetDefinitionId) {

            memberServiceId = forMemberServiceId;

            setDefinitionId = forSetDefinitionId;

            return;

        }   

        #endregion


        #region Database Methods

        public void MapDataFields (System.Data.DataRow currentRow) {

            detailMemberServiceId = (Int64) currentRow["DetailMemberServiceId"];

            memberId = (Int64) currentRow["MemberId"];

            eventDate = (DateTime) currentRow["EventDate"];

            parentServiceId = (Int64) currentRow["ParentServiceId"]; 

            serviceId = (Int64) currentRow["ServiceId"]; 

            serviceName = (String) currentRow["ServiceName"]; 

            if ((Int32?) currentRow["ServiceType"] != null) { serviceType = ((Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType) (Int32) currentRow["ServiceType"]); }

            return;

        }

        public Boolean Save (Mercury.Server.Application application)  {

            Boolean success = false;

            StringBuilder sqlStatement;

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.MemberServiceDetailSet_InsertUpdate ");

                sqlStatement.Append (memberServiceId.ToString () + ", ");

                sqlStatement.Append (setDefinitionId.ToString () + ", ");

                sqlStatement.Append (detailMemberServiceId.ToString () + ", ");

                sqlStatement.Append (memberId.ToString () + ", ");

                sqlStatement.Append (parentServiceId.ToString () + ", ");

                sqlStatement.Append (serviceId.ToString () + ", ");

                sqlStatement.Append ("'" + serviceName.Replace ("'", "''") + "', ");

                sqlStatement.Append (((Int32) serviceType).ToString () + ", ");

                sqlStatement.Append ("'" + eventDate.ToString ("MM/dd/yyyy") + "'");


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

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
