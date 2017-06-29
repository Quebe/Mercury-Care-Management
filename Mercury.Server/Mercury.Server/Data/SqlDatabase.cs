using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.Server.Data {

    public class SqlDatabase : IDatabase, IDisposable {

        #region Private Properties

        private System.Data.SqlClient.SqlConnection sqlConnection;

        private System.Collections.Stack sqlTransactions;

        private SqlConfiguration sqlConfiguration;


        private Exception lastException;

        private String lastSqlStatement;

        private Boolean disposed;


        private Object sqlLock = new Object ();

        #endregion


        #region Public Properties

        public SqlConfiguration Configuration { get { return sqlConfiguration;  } set { sqlConfiguration = value; } }

        public System.Data.SqlClient.SqlConnection SqlConnection { get { return sqlConnection; } }

        public System.Data.ConnectionState ConnectionState {  get { return (sqlConnection != null) ? sqlConnection.State : System.Data.ConnectionState.Closed; } }

        public Int32 ConnectionTimeout { get { return (sqlConnection != null) ? sqlConnection.ConnectionTimeout : 0; } }

        public Int32 OpenTransactions { get { return sqlTransactions.Count; } }

        public System.Exception LastException { get { return lastException; } }

        public String LastSqlStatement { get { return lastSqlStatement; } }

        protected void SetLastException (Exception exception) {

            lastException = exception;

            if (lastException != null) {

                Exception currentException = exception;

                while (currentException != null) {

                    System.Diagnostics.Trace.WriteLine ("[" + currentException.Source + "] " + currentException.Message);

                    currentException = currentException.InnerException;

                }

                System.Diagnostics.Trace.WriteLine ("[" + exception.Source + "] " + lastSqlStatement);

                System.Diagnostics.Trace.Flush ();

            } // if (lastException != null) 

        }

        #endregion


        #region Constructor and Destructor

        private void InitializeObjects () {
            sqlTransactions = new System.Collections.Stack ();
            sqlConfiguration = new SqlConfiguration ();

        }

        private void InitializePooling (Int32 minPoolSize, Int32 maxPoolSize) {
            sqlConfiguration.PoolingEnabled = true;

            sqlConfiguration.MinPoolSize = minPoolSize;
            sqlConfiguration.MaxPoolSize = maxPoolSize;

        }

        public SqlDatabase () {
            InitializeObjects ();

        }

        public SqlDatabase (Int32 minPoolSize, Int32 maxPoolSize) {
            InitializeObjects ();
            InitializePooling (minPoolSize, maxPoolSize);

        }

        public SqlDatabase (SqlConfiguration sqlConfiguration) {
            InitializeObjects ();

            Configuration = sqlConfiguration;

        }

        public void Dispose () {
            Dispose (true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.

            GC.SuppressFinalize (this);

        }

        protected virtual void Dispose (Boolean disposing) {

            if (!this.disposed) {

                if (disposing) {
                    // component.Dispose ();

                }

                // unmanaged objects
                try {
                    SetLastException (null);

                    // MyBase.Finalize ();

                    if (ConnectionState != System.Data.ConnectionState.Closed) {
                        sqlConnection.Close ();

                    } // end if

                } // end try

                catch (Exception FinalizeException) {

                    SetLastException (FinalizeException);

                    throw;

                } // end catch

                finally {
                    if (sqlConnection != null) {
                        sqlConnection.Dispose ();
                        sqlConnection = null;

                    } // end if

                } // end finally 

            }

            this.disposed = true;

        }

        #endregion


        #region Public Methods - General

        public void ClearLastException () { lastException = null; }

        #endregion 


        #region Sql Connection and On Demand Functions

        private Boolean ConnectToDatabase () {

            try {
                SetLastException (null);

                sqlConnection = new System.Data.SqlClient.SqlConnection (sqlConfiguration.ConnectionString);

                // force open/close to ensure proper connection
                OnDemandOpen ();
                OnDemandClose ();

            } // end try

            catch (System.Data.SqlClient.SqlException SqlException) {

                SetLastException (SqlException);

                return false;

            }

            catch (Exception ConnectException) {

                SetLastException (ConnectException);

                throw;

            } // end catch

            finally {
                // do nothing

            } // end finally

            return true;

        }

        public Boolean Connect () {

            return ConnectToDatabase ();

        }

        public void OnDemandOpen () {

            if (sqlConnection == null) {

                Connect ();

            }

            if (sqlConnection != null) {

                DateTime connectionStartTime = DateTime.Now;

                while ((sqlConnection.State == System.Data.ConnectionState.Closed) || (sqlConnection.State == System.Data.ConnectionState.Broken)) {

                    if ((sqlConnection.State != System.Data.ConnectionState.Open) || (sqlConnection.State != System.Data.ConnectionState.Connecting)) {

                        sqlConnection.Open ();

                    }

                    //if (DateTime.Now.Subtract (connectionStartTime).TotalSeconds > sqlConnection.ConnectionTimeout) {

                    //    break;

                    //}

                } // end if (ConnectionState)

            }

            return;

        }

        public void OnDemandClose () {
            
            // DO NOT CLOSE OUT NON-POOLED COLLECTION, THIS WILL BE CLOSED OUT AUTOMATICALLY AFTER POST BACK

            //   OTHERWISE, IT WOULD NEED TO BE MANUALLY RE-OPENED FOR EACH DATABASE REQUEST

            if (!sqlConfiguration.PoolingEnabled) { return; }

            // CLOSE OUT OPEN CONNECTION TO FREE THE CONNECTION POOL

            if (sqlConnection != null) { 

                if (sqlTransactions.Count == 0) {

                    if (sqlConnection.State == System.Data.ConnectionState.Open) {

                        sqlConnection.Close ();

                    }

                }

            }

            return;

        }

        #endregion


        #region Transaction Support Functions

        public void BeginTransaction (System.Data.IsolationLevel isolationLevel) {

            try {

                SetLastException (null);

                OnDemandOpen ();

                sqlTransactions.Push (sqlConnection.BeginTransaction (isolationLevel));

            } // end try

            catch (System.Data.SqlClient.SqlException SqlException) {
                SetLastException (SqlException);

                throw;

            } // end catch (SqlException)

            catch (Exception UnhandledException) {

                SetLastException (UnhandledException);

                throw;

            } // end catch (Exception)

            return;

        }

        public void BeginTransaction () {

            BeginTransaction (System.Data.IsolationLevel.ReadCommitted);

            return;

        }

        public void RollbackTransaction () {

            System.Data.SqlClient.SqlTransaction rollbackTransaction;

            try {

                SetLastException (null);

                if (sqlTransactions.Count > 0) {

                    rollbackTransaction = (System.Data.SqlClient.SqlTransaction)(sqlTransactions.Pop ());

                    rollbackTransaction.Rollback ();

                }

            } // end try

            catch (System.Data.SqlClient.SqlException SqlException) {

                SetLastException (SqlException);

                throw;

            } // end catch (SqlException)

            catch (Exception UnhandledException) {

                SetLastException (UnhandledException);

                throw;

            } // end catch (Exception)

            finally {

                OnDemandClose ();

            } // end finally 

            return;

        }

        public void CommitTransaction () {

            System.Data.SqlClient.SqlTransaction commitTransaction;

            try {

                SetLastException (null);

                if (sqlTransactions.Count > 0) {

                    commitTransaction = (System.Data.SqlClient.SqlTransaction)(sqlTransactions.Pop ());

                    commitTransaction.Commit ();

                } // end if (sqlTransactions.Count > 0) 

            } // end try

            catch (System.Data.SqlClient.SqlException SqlException) {

                SetLastException (SqlException);

                throw;

            } // end catch (SqlException)

            catch (Exception UnhandledException) {

                SetLastException (UnhandledException);

                throw;

            } // end catch (Exception)

            finally {

                OnDemandClose ();

            } // end finally 

            return;

        }

        #endregion


        #region Sql Client Objects

        public System.Data.SqlClient.SqlCommand CreateSqlCommand (String sqlStatement) {

            System.Data.SqlClient.SqlTransaction peekTransaction;

            if (sqlTransactions.Count > 0) {
                // use the open transaction to wrap the command

                peekTransaction = (System.Data.SqlClient.SqlTransaction)(sqlTransactions.Peek ());

                return new System.Data.SqlClient.SqlCommand (sqlStatement, sqlConnection, peekTransaction);

            } // end if (sqlTransactions.Count > 0) 

            else {
                // no open transactions, just return command object

                return new System.Data.SqlClient.SqlCommand (sqlStatement, sqlConnection);

            } // end if else

        } // CreateSqlCommand

        public void AppendSqlParameter (System.Data.SqlClient.SqlCommand sqlCommand, String parameterName, Int64 value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.BigInt);

            parameter.Value = value;

            sqlCommand.Parameters.Add (parameter);

            return;

        }

        public void AppendSqlParameter (System.Data.SqlClient.SqlCommand sqlCommand, String parameterName, Int64? value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.BigInt);

            if (value.HasValue) { parameter.Value = value.Value; }

            else { parameter.Value = DBNull.Value; }

            sqlCommand.Parameters.Add (parameter);

            return;

        }

        public void AppendSqlParameter (System.Data.SqlClient.SqlCommand sqlCommand, String parameterName, Int32 value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.Int);

            parameter.Value = value;

            sqlCommand.Parameters.Add (parameter);

            return;

        }

        public void AppendSqlParameter (System.Data.SqlClient.SqlCommand sqlCommand, String parameterName, Boolean value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.Bit);

            parameter.Value = value;

            sqlCommand.Parameters.Add (parameter);

            return;

        }

        public void AppendSqlParameter (System.Data.SqlClient.SqlCommand sqlCommand, String parameterName, DateTime value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.DateTime);

            parameter.Value = value;

            sqlCommand.Parameters.Add (parameter);

            return;

        }

        public void AppendSqlParameter (System.Data.SqlClient.SqlCommand sqlCommand, String parameterName, DateTime? value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.DateTime);

            if (value.HasValue) { parameter.Value = value.Value; }

            else { parameter.Value = DBNull.Value; }

            sqlCommand.Parameters.Add (parameter);

            return;

        }

        public void AppendSqlParameter (System.Data.SqlClient.SqlCommand sqlCommand, String parameterName, String value, Int32 length) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.VarChar);

            parameter.Value = CommonFunctions.SetValueMaxLength (value, length);

            sqlCommand.Parameters.Add (parameter);

            return;

        }

        public System.Data.SqlClient.SqlDataAdapter CreateSqlDataAdapter (String sqlStatement) {

            System.Data.SqlClient.SqlDataAdapter newDataAdapter;
            System.Data.SqlClient.SqlTransaction peekTransaction;

            newDataAdapter = new System.Data.SqlClient.SqlDataAdapter (sqlStatement, sqlConnection);

            if (sqlTransactions.Count > 0) {
                // use the open transaction to wrap the command

                peekTransaction = (System.Data.SqlClient.SqlTransaction)(sqlTransactions.Peek ());

                if (newDataAdapter.SelectCommand != null) { newDataAdapter.SelectCommand.Transaction = peekTransaction; }

                if (newDataAdapter.DeleteCommand != null) { newDataAdapter.DeleteCommand.Transaction = peekTransaction; }

                if (newDataAdapter.UpdateCommand != null) { newDataAdapter.UpdateCommand.Transaction = peekTransaction; }

            } // end if (sqlTransactions.Count > 0) 

            return newDataAdapter;

        } // CreateSqlDataAdapter

        #endregion


        #region Sql Client Objects - IDatabase

        public System.Data.IDbCommand CreateCommand (String sqlStatement) {

            System.Data.SqlClient.SqlCommand command = null;

            System.Data.SqlClient.SqlTransaction peekTransaction;

            if (sqlTransactions.Count > 0) {

                // use the open transaction to wrap the command

                peekTransaction = (System.Data.SqlClient.SqlTransaction)(sqlTransactions.Peek ());

                command = new System.Data.SqlClient.SqlCommand (sqlStatement, sqlConnection, peekTransaction);

            } // end if (sqlTransactions.Count > 0) 

            else {

                // no open transactions, just return command object

                command = new System.Data.SqlClient.SqlCommand (sqlStatement, sqlConnection);

            } // end if else

            return command;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Int64 value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.BigInt);

            parameter.Value = value;

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Int64? value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.BigInt);

            if (value.HasValue) { parameter.Value = value.Value; }

            else { parameter.Value = DBNull.Value; }

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Int32 value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.Int);

            parameter.Value = value;

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Boolean value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.Bit);

            parameter.Value = value;

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, DateTime value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.DateTime);

            parameter.Value = value;

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, DateTime? value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.DateTime);

            if (value.HasValue) { parameter.Value = value.Value; }

            else { parameter.Value = DBNull.Value; }

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Decimal value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.Decimal);

            parameter.Value = value;

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Decimal? value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.Decimal);

            if (value.HasValue) { parameter.Value = value.Value; }

            else { parameter.Value = DBNull.Value; }

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, Guid value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.UniqueIdentifier);

            parameter.Value = value;

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, String value, Int32 length) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.VarChar);

            parameter.Value = CommonFunctions.SetValueMaxLength (value, length);

            command.Parameters.Add (parameter);

            return;

        }

        public void AppendCommandParameter (System.Data.IDbCommand command, String parameterName, System.Xml.XmlDocument value) {

            System.Data.SqlClient.SqlParameter parameter = new System.Data.SqlClient.SqlParameter (parameterName, System.Data.SqlDbType.Xml);

            parameter.Value = value.InnerXml.Replace ("<?xml version=\"1.0\" encoding=\"utf-8\"?>", ""); // REMOVE ENCODING INSTRUCTIONS

            command.Parameters.Add (parameter);

            return;

        }

        public System.Data.IDataAdapter CreateDataAdapter (String sqlStatement) {

            System.Data.SqlClient.SqlDataAdapter newDataAdapter;

            System.Data.SqlClient.SqlTransaction peekTransaction;

            newDataAdapter = new System.Data.SqlClient.SqlDataAdapter (sqlStatement, sqlConnection);

            if (sqlTransactions.Count > 0) {
                // use the open transaction to wrap the command

                peekTransaction = (System.Data.SqlClient.SqlTransaction)(sqlTransactions.Peek ());

                if (newDataAdapter.SelectCommand != null) { newDataAdapter.SelectCommand.Transaction = peekTransaction; }

                if (newDataAdapter.DeleteCommand != null) { newDataAdapter.DeleteCommand.Transaction = peekTransaction; }

                if (newDataAdapter.UpdateCommand != null) { newDataAdapter.UpdateCommand.Transaction = peekTransaction; }

            } // end if (sqlTransactions.Count > 0) 

            return newDataAdapter;

        } // CreateSqlDataAdapter

        #endregion

        
        #region System Data Objects

        public System.Data.DataTable SelectDataTable (String selectStatement, Int32 commandTimeout) {

            System.Data.DataTable results;

            System.Data.SqlClient.SqlDataAdapter selectDataAdapter = null;

            results = new System.Data.DataTable ();

            results.Locale = System.Globalization.CultureInfo.InvariantCulture;

#if DEBUG

            DateTime startTime = DateTime.Now;

#endif
                
            try {

                SetLastException (null);

                lastSqlStatement = selectStatement;


                lock (sqlLock) {

                    OnDemandOpen ();

                    selectDataAdapter = (System.Data.SqlClient.SqlDataAdapter) CreateDataAdapter (selectStatement);

                    selectDataAdapter.SelectCommand.CommandTimeout = commandTimeout;

                    selectDataAdapter.Fill (results);

                }

            } // end try 

            catch (System.Data.SqlClient.SqlException SqlException) {

                SetLastException (SqlException);

            } // end catch (SqlException)

            catch (Exception UnhandledException) {

                SetLastException (UnhandledException);

                throw;

            } // end catch (UnhandledException)

            finally {

                if (selectDataAdapter != null) { selectDataAdapter.Dispose (); }

                OnDemandClose ();

            } // finally

#if DEBUG

            System.Diagnostics.Debug.Write ("----> Database Request [" + (Convert.ToInt32 (DateTime.Now.Subtract (startTime).TotalMilliseconds)) + "]: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

            System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

            System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

            System.Diagnostics.Debug.WriteLine (String.Empty);

            if ((DateTime.Now.Subtract (startTime).TotalMilliseconds) >= 1000) {

                System.Diagnostics.Debug.WriteLine (selectStatement);

            }

#endif
                

            return results;

        }

        public System.Data.DataTable SelectDataTable (String selectStatement) {

            return SelectDataTable (selectStatement, 0);

        }

        public System.Data.DataView SelectDataView (String selectStatement, Int32 commandTimeout) {

            return new System.Data.DataView (SelectDataTable (selectStatement, commandTimeout));

        }

        public System.Data.DataView SelectDataView (String selectStatement) {

            return SelectDataView (selectStatement, 0);

        }

        #endregion


        #region Sql Command Functions

        public Boolean ExecuteSqlStatement (String executeStatement, Int32 commandTimeout) {

            System.Data.IDbCommand executeCommand = null;

            Boolean executeSuccess;

            executeSuccess = true;


#if DEBUG

            DateTime startTime = DateTime.Now;

#endif 

            try {

                SetLastException (null);

                lastSqlStatement = executeStatement;

                lock (sqlLock) {

                    OnDemandOpen ();

                    executeCommand = CreateCommand (executeStatement);

                    executeCommand.CommandTimeout = commandTimeout;

                    executeCommand.ExecuteNonQuery ();

                }

            } // end try 

            catch (Exception ExecuteException) {
                
                executeSuccess = false;

                SetLastException (ExecuteException);

            } // end catch (Exception)

            finally {

                if (executeCommand == null) {

                    executeCommand.Dispose ();

                }

                OnDemandClose ();

#if DEBUG

                System.Diagnostics.Debug.Write ("----> Database Execute [" + ((DateTime.Now.Subtract (startTime).TotalMilliseconds).ToString ()) + "]: " + (new System.Diagnostics.StackTrace ().GetFrames ()[1].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[2].GetMethod ().Name));

                System.Diagnostics.Debug.Write ("   <-- FROM -->   " + (new System.Diagnostics.StackTrace ().GetFrames ()[3].GetMethod ().Name));

                System.Diagnostics.Debug.WriteLine (String.Empty);

#endif

            } // end finally 

            return executeSuccess;

        }

        public Boolean ExecuteSqlStatement (String executeStatement) {

            return ExecuteSqlStatement (executeStatement, 0);

        }

        public Int64 StoredProcedureReturnValue (String procedureStatement) {

            Int64 returnValue = 0;


            String sqlStatement = String.Empty;
            
            sqlStatement = sqlStatement + "DECLARE @returnValue AS BIGINT \r\n";

            sqlStatement = sqlStatement + "EXEC @returnValue = " + procedureStatement + " \r\n";

            sqlStatement = sqlStatement + "SELECT @returnValue";


            lock (sqlLock) {

                returnValue = (Int64) ExecuteScalar (sqlStatement);

            }

            return returnValue;

        }

        public Object LookupValue (String dataTable, String fieldExpression, String criteria, Object defaultValue) {

            System.Text.StringBuilder sqlStatement = new StringBuilder ();
            System.Data.IDbCommand lookupCommand = null;

            Object returnValue = null;

            try {

                SetLastException (null);

                OnDemandOpen ();

                sqlStatement.Append ("SELECT " + fieldExpression + " FROM " + dataTable);

                if (criteria.Length > 0) {
                    sqlStatement.Append (" WHERE " + criteria);

                } // end if 

                lastSqlStatement = sqlStatement.ToString ();

                lookupCommand = CreateCommand (sqlStatement.ToString ());

                lock (sqlLock) {

                    returnValue = lookupCommand.ExecuteScalar ();

                }

            } // end try

            catch (System.Data.SqlClient.SqlException SqlException) {

                SetLastException (SqlException);

            } // end catch (SqlException)

            catch (Exception LookupException) {

                SetLastException (LookupException);

                throw;

            } // end catch (Exception)

            finally {

                if (lookupCommand != null) {
                    lookupCommand.Dispose ();

                }

                OnDemandClose ();

            } // end finally

            if (returnValue == null) { // TODO: monitor for C# impacts with DBNull
                returnValue = defaultValue;

            } // end if 

            return returnValue;

        }

        public Object LookupValue (String dataTable, String fieldExpression, String criteria) {

            return LookupValue (dataTable, fieldExpression, criteria, null);

        } // LookupValue

        public Object ExecuteScalar (String sqlStatement) {

            Object scalarResult = null;

            System.Data.IDbCommand scalarCommand = null;

            try {

                SetLastException (null);

                lastSqlStatement = sqlStatement;

                OnDemandOpen ();

                scalarCommand = CreateCommand (sqlStatement);

                scalarCommand.CommandTimeout = 0;


                lock (sqlLock) {

                    scalarResult = scalarCommand.ExecuteScalar ();

                }

            }

            catch (Exception databaseException) {

                SetLastException (databaseException);

                throw databaseException;

            }

            finally {

                if (scalarCommand != null) { scalarCommand.Dispose (); }

                OnDemandClose ();

            }

            return scalarResult;

        }

        #endregion


        #region Binary Large Objects (BLOB)

        public Boolean BlobWrite (String tableName, String columnName, String criteria, System.IO.Stream dataStream) {

            Boolean success = false;

            String updateStatement = "UPDATE " + tableName + " SET " + columnName + " = @blobData WHERE " + criteria;

            System.Data.IDbCommand updateCommand = null;

            System.IO.MemoryStream blobStream = new System.IO.MemoryStream ();


            dataStream.Seek (0, System.IO.SeekOrigin.Begin);

            dataStream.CopyTo (blobStream);


            try {

                OnDemandOpen ();

                updateCommand = CreateCommand (updateStatement);

                System.Data.SqlClient.SqlParameter blobParameter = new System.Data.SqlClient.SqlParameter ("@blobData", System.Data.SqlDbType.VarBinary, Convert.ToInt32 (blobStream.Length),

                    System.Data.ParameterDirection.Input, true, 0, 0, null, System.Data.DataRowVersion.Current, blobStream.ToArray ());

                updateCommand.Parameters.Add (blobParameter);

                success = (updateCommand.ExecuteNonQuery () == 1);

            }

            catch (Exception sqlException) {

                SetLastException (sqlException);

            }

            finally {

                if (updateCommand != null) { updateCommand.Dispose (); } // end if

                updateCommand = null;

                OnDemandClose ();
            }

            return success;

        }

        public System.IO.MemoryStream BlobRead (String tableName, String columnName, String criteria) {

            System.IO.MemoryStream dataStream = new System.IO.MemoryStream ();

            String selectStatement = "SELECT " + columnName + " FROM " + tableName + " WHERE " + criteria;

            System.Data.SqlClient.SqlCommand selectCommand = null;

            System.Data.SqlClient.SqlDataReader dataReader = null;


            try {

                OnDemandOpen ();

                selectCommand = (System.Data.SqlClient.SqlCommand) CreateCommand (selectStatement);

                dataReader = selectCommand.ExecuteReader (System.Data.CommandBehavior.Default);

                if (dataReader.Read ()) {

                    System.Data.SqlTypes.SqlBytes sourceBytes = dataReader.GetSqlBytes (0);

                    if (sourceBytes.Buffer != null) {

                        dataStream = new System.IO.MemoryStream (sourceBytes.Buffer);

                    }

                    else { dataStream = new System.IO.MemoryStream (); }

                }

            }

            catch (Exception sqlException) {

                SetLastException (sqlException);

            }

            finally {

                if (dataReader != null) { dataReader.Close (); } // end if

                if (selectCommand != null) { selectCommand.Dispose (); } // end if

                dataReader = null;

                selectCommand = null;

                OnDemandClose ();
            }

            return dataStream;

        }

        #endregion

    }

}
