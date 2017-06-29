using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Mercury.Server.Data {

    [Serializable]
    [DataContract (Name = "SqlConfiguration")]    
    public class SqlConfiguration {


        #region Private Properties

        [DataMember (Name = "ServerName")]
        private String serverName = String.Empty;

        [DataMember (Name = "DatabaseName")]
        private String databaseName = String.Empty;

        [DataMember (Name = "PoolingEnabled")]
        private Boolean poolingEnabled;

        [DataMember (Name = "PoolSizeMinimum")]
        private Int32 minPoolSize = 1;

        [DataMember (Name = "PoolSizeMaximum")]
        private Int32 maxPoolSize = 10;

        [DataMember (Name = "UseTrustedConnection")]
        private Boolean trustedConnection = true;

        [DataMember (Name = "SqlUserName")]
        private String userName = String.Empty;

        [DataMember (Name = "SqlUserPassword")]
        private String password = String.Empty;

        [DataMember (Name = "CustomAttributes")]
        private String customAttributes = String.Empty;

        #endregion


        #region Public Properties

        public String ServerName { get { return serverName; }  set { serverName = value; } }

        public String DatabaseName { get { return databaseName; }  set { databaseName = value; } }

        public Boolean PoolingEnabled { get { return poolingEnabled; } set { poolingEnabled = value; } }

        public Int32 MinPoolSize { get { return minPoolSize; } // end get

            set {

                if (value < 0) {
                    minPoolSize = 0;
                }

                else {
                    minPoolSize = value;

                }

            } // end set
        }

        public Int32 MaxPoolSize {

            get { return maxPoolSize; } // end get

            set {

                if (value < minPoolSize) {
                    maxPoolSize = minPoolSize;

                }

                else {
                    maxPoolSize = value;

                }
            } // end set
        }

        public Boolean TrustedConnection { get { return trustedConnection; } set { trustedConnection = value; } }

        public String UserName { get { return userName; } set { userName = value; } }

        public String Password { get { return password; } set { password = value; } }

        public String CustomAttributes { get { return customAttributes; }  set { customAttributes = value; } }

        public String ConnectionString {
            get {

                System.Text.StringBuilder connectionString;

                connectionString = new StringBuilder ();

                connectionString.Append ("Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";");
                connectionString.Append ("Persist Security Info=True;");


                if (trustedConnection) {

                    connectionString.Append ("Trusted_Connection=SSPI");

                    // connectionString.Append ("trusted_connection=true");

                } // end if (trustedConnection)

                else {
                    connectionString.Append ("User Id=" + userName + ";Password=" + password);

                } // end if else


                if (poolingEnabled) {
                    connectionString.Append (";Pooling=True;Min Pool Size=" + minPoolSize.ToString () + ";Max Pool Size=" + maxPoolSize.ToString ());

                } // end if (poolingEnabled)


                if (customAttributes.Length > 0) {
                    connectionString.Append (";" + customAttributes);

                } // end if (customAttributes)

                return connectionString.ToString ();

            }

        }

        #endregion


        #region Constructor and Destructor

        public SqlConfiguration () {
            serverName = "(local)";

            return;

        }

        public SqlConfiguration (String configurationPrefix) {

            try {

                ReadFromAppSettings (configurationPrefix);

            }

            catch {

                // do nothing

            }

        }

        public SqlConfiguration (SerializationInfo info, StreamingContext context) {
            ServerName = info.GetString ("serverName");
            DatabaseName = info.GetString ("databaseName");

            PoolingEnabled = info.GetBoolean ("poolingEnabled");
            MinPoolSize = info.GetInt32 ("minPoolSize");
            MaxPoolSize = info.GetInt32 ("maxPoolSize");

            TrustedConnection = info.GetBoolean ("trustedConnection");
            UserName = info.GetString ("userName");
            Password = info.GetString ("password");

            CustomAttributes = info.GetString ("customAttributes");

        }

        #endregion


        #region Serialization

        public virtual void GetObjectData (SerializationInfo info, StreamingContext context) {
            info.AddValue ("serverName", ServerName);
            info.AddValue ("databaseName", DatabaseName);

            info.AddValue ("poolingEnabled", PoolingEnabled);
            info.AddValue ("minPoolSize", MinPoolSize);
            info.AddValue ("maxPoolSize", MaxPoolSize);

            info.AddValue ("trustedConnection", TrustedConnection);
            info.AddValue ("userName", UserName);
            info.AddValue ("password", Password);

            info.AddValue ("customAttributes", CustomAttributes);

        }

        #endregion


        #region Set Configuration Properties

        public void ReadFromAppSettings (String configurationPrefix) {

            // read configuration from app.config file
            try {
                ServerName = System.Configuration.ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".serverName")[0].ToString ();
                DatabaseName = System.Configuration.ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".databaseName")[0].ToString ();

                try {
                    PoolingEnabled = Convert.ToBoolean (System.Configuration.ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".poolingEnabled")[0].ToString ());

                }

                catch {
                    PoolingEnabled = false;

                }

                try {
                    TrustedConnection = Convert.ToBoolean (System.Configuration.ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".trustedConnection")[0].ToString ());

                }

                catch {
                    TrustedConnection = true;

                }

                if (poolingEnabled) {
                    MinPoolSize = Convert.ToInt32 (System.Configuration.ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".minPoolSize")[0].ToString ());
                    MaxPoolSize = Convert.ToInt32 (System.Configuration.ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".maxPoolSize")[0].ToString ());

                } // end if (poolingEnabled)

                if (!trustedConnection) {

                    try {
                        UserName = System.Configuration.ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".userName")[0].ToString ();
                        Password = System.Configuration.ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".password")[0].ToString ();
                    }

                    catch {
                        // DO NOTHING

                    }

                }

            } // end try

            catch (Exception ConfigException) {

                throw (new ApplicationException ("Unable to read Sql Server Configuration Settings.", ConfigException));

            } // end catch 

        }

        public void SetSqlConfiguration (String serverName, String databaseName, Boolean poolingEnabled, Int32 minPoolSize, Int32 maxPoolSize) {
            ServerName = serverName;
            DatabaseName = databaseName;

            PoolingEnabled = poolingEnabled;
            MinPoolSize = minPoolSize;
            MaxPoolSize = maxPoolSize;

        }

        public void SetSqlConfiguration (String serverName, String databaseName, Boolean poolingEnabled, Int32 minPoolSize, Int32 maxPoolSize, String customAttributes) {
            SetSqlConfiguration (serverName, databaseName, poolingEnabled, minPoolSize, maxPoolSize);

            CustomAttributes = customAttributes;

        }

        public void SetCredentials (String userName, String password) {
            trustedConnection = false;
            UserName = userName;
            Password = password;

        }

        #endregion

    }

}
