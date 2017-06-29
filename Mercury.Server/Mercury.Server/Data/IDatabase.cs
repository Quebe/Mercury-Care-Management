using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Data {

    public interface IDatabase {

        #region Public Properties

        System.Data.ConnectionState ConnectionState { get; }

        Int32 ConnectionTimeout { get; }

        Int32 OpenTransactions { get; }

        System.Exception LastException { get; }

        #endregion


        #region Public Methods - General

        void ClearLastException ();

        #endregion


        #region Public Methods - Connection

        Boolean Connect ();

        void OnDemandOpen ();

        void OnDemandClose ();

        #endregion


        #region Transaction Support Functions

        void BeginTransaction (System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted);

        void RollbackTransaction ();

        void CommitTransaction ();

        #endregion


        #region Sql Client Objects

        System.Data.IDbCommand CreateCommand (String sqlStatement);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Int64 value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Int64? value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Int32 value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Boolean value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, DateTime value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, DateTime? value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Decimal value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Decimal? value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Guid value);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, String value, Int32 length);

        void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, System.Xml.XmlDocument value);

        System.Data.IDataAdapter CreateDataAdapter (String sqlStatement);

        #endregion


        #region System Data Objects

        System.Data.DataTable SelectDataTable (String selectStatement, Int32 commandTimeout = 0);

        System.Data.DataView SelectDataView (String selectStatement, Int32 commandTimeout = 0);

        #endregion


        #region Sql Command Functions

        Boolean ExecuteSqlStatement (String executeStatement, Int32 commandTimeout = 0);

        Int64 StoredProcedureReturnValue (String procedureStatement);

        Object LookupValue (String dataTable, String fieldExpression, String criteria, Object defaultValue = null);

        Object ExecuteScalar (String sqlStatement);

        #endregion


        #region Binary Large Objects (BLOB)

        Boolean BlobWrite (String tableName, String columnName, String criteria, System.IO.Stream dataStream);

        System.IO.MemoryStream BlobRead (String tableName, String columnName, String criteria);

        #endregion 

    }

}
