using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Metrics {

    [DataContract (Name = "MemberMetric")]
    public class MemberMetric : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "MetricId")]
        private Int64 metricId;

        [DataMember (Name = "EventDate")]
        private DateTime eventDate = new DateTime (1900, 01, 01);

        [DataMember (Name = "MetricValue")]
        private Decimal metricValue = 0;

        [DataMember (Name = "AddedManually")]
        private Boolean addedManually = false;

        [DataMember (Name = "Metric")]
        private Metric metric = null;

        #endregion


        #region Public Properties
        
        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public Int64 MetricId { get { return metricId; } set { metricId = value; } }

        public DateTime EventDate { get { return eventDate; } set { eventDate = value; } }

        public Decimal MetricValue { get { return metricValue; } set { metricValue = value; } }

        public Boolean AddedManually { get { return addedManually; } set { addedManually = value; } }


        public Metric Metric { get { return metric; } set { metric = value; } }

        #endregion


        #region Constructors

        public MemberMetric (Application applicationReference) { base.BaseConstructor (applicationReference); }

        public MemberMetric (Application applicationReference, Int64 forMemberMetricId) { base.BaseConstructor (applicationReference, forMemberMetricId); }

        #endregion


        #region Public Methods

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            memberId = (Int64) currentRow["MemberId"];

            metricId = (Int64) currentRow["MetricId"];

            eventDate = (DateTime) currentRow["EventDate"];

            metricValue = Convert.ToDecimal (currentRow["MetricValue"]);

            addedManually = (Boolean) currentRow["AddedManually"];


            if (currentRow.Table.Columns.Contains ("MetricId1")) {

                System.Data.DataRow metricRow = currentRow.Table.Copy ().Rows[currentRow.Table.Rows.IndexOf (currentRow)];

                while (metricRow.Table.Columns[0].ColumnName != "MetricId1") { metricRow.Table.Columns.RemoveAt (0); }

                foreach (System.Data.DataColumn currentColumn in metricRow.Table.Columns) {

                    if (currentColumn.ColumnName.EndsWith ("1")) {

                        currentColumn.ColumnName = currentColumn.ColumnName.Substring (0, currentColumn.ColumnName.Length - 1);

                    }

                }

                metric = new Metric (null);

                metric.MapDataFields (metricRow);

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

                sqlStatement.Append ("EXEC dbo.MemberMetric_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append (memberId.ToString () + ", ");

                sqlStatement.Append (metricId.ToString () + ", ");

                sqlStatement.Append ("'" + eventDate.ToString ("MM/dd/yyyy") + "', ");

                sqlStatement.Append (metricValue.ToString () + ", ");

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
