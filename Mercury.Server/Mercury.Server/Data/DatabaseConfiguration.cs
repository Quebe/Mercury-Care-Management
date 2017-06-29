using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Data {

    public class DatabaseConfiguration {

        #region Private Properties

        private String serverName = String.Empty;

        private String databaseName = String.Empty;

        private Boolean poolingEnabled;

        private Int32 minPoolSize = 1;

        private Int32 maxPoolSize = 10;

        private Boolean trustedConnection = true;

        private String userName = String.Empty;

        private String password = String.Empty;

        private String customAttributes = String.Empty;

        #endregion


        #region Public Properties

        public String ServerName { get { return serverName; } set { serverName = value; } }

        public String DatabaseName { get { return databaseName; } set { databaseName = value; } }

        public Boolean PoolingEnabled { get { return poolingEnabled; } set { poolingEnabled = value; } }

        public Int32 MinPoolSize {

            get { return minPoolSize; }

            set { minPoolSize = ((value < 0) ? 0 : value); }

        }

        public Int32 MaxPoolSize {

            get { return maxPoolSize; }

            set { maxPoolSize = ((value < minPoolSize) ? minPoolSize : value); }

        }

        public Boolean TrustedConnection { get { return trustedConnection; } set { trustedConnection = value; } }

        public String UserName { get { return userName; } set { userName = value; } }

        public String Password { get { return password; } set { password = value; } }

        public String CustomAttributes { get { return customAttributes; } set { customAttributes = value; } }

        public String ConnectionString {

            get {

                System.Text.StringBuilder connectionString;

                connectionString = new StringBuilder ();

                connectionString.Append ("Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";");

                connectionString.Append ("Persist Security Info=True;");


                if (trustedConnection) { connectionString.Append ("Trusted_Connection=SSPI"); }

                else { connectionString.Append ("User Id=" + userName + ";Password=" + password); }


                if (poolingEnabled) {

                    connectionString.Append (";Pooling=True;Min Pool Size=" + minPoolSize.ToString () + ";Max Pool Size=" + maxPoolSize.ToString ());

                }


                if (customAttributes.Length > 0) {

                    connectionString.Append (";" + customAttributes);

                }

                return connectionString.ToString ();

            }

        }

        #endregion


        #region Constructor and Destructor

        public DatabaseConfiguration () {

            serverName = "(local)";

            return;

        }

        public DatabaseConfiguration (String configurationPrefix) {

            ReadFromAppSettings (configurationPrefix);

            return;

        }

        public DatabaseConfiguration (SerializationInfo info, StreamingContext context) {

            ServerName = info.GetString ("serverName");

            DatabaseName = info.GetString ("databaseName");

            PoolingEnabled = info.GetBoolean ("poolingEnabled");

            MinPoolSize = info.GetInt32 ("minPoolSize");

            MaxPoolSize = info.GetInt32 ("maxPoolSize");


            TrustedConnection = info.GetBoolean ("trustedConnection");

            UserName = info.GetString ("userName");

            Password = info.GetString ("password");

            CustomAttributes = info.GetString ("customAttributes");

            return;

        }

        #endregion


        #region Set Configuration Properties

        public void ReadFromAppSettings (String configurationPrefix) {

            try {

                ServerName = ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".serverName")[0].ToString ();

                DatabaseName = ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".databaseName")[0].ToString ();

                try {

                    PoolingEnabled = Convert.ToBoolean (ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".poolingEnabled")[0].ToString ());

                }

                catch {

                    PoolingEnabled = false;

                }

                try {

                    TrustedConnection = Convert.ToBoolean (ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".trustedConnection")[0].ToString ());

                }

                catch {

                    TrustedConnection = true;

                }

                if (poolingEnabled) {

                    MinPoolSize = Convert.ToInt32 (ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".minPoolSize")[0].ToString ());

                    MaxPoolSize = Convert.ToInt32 (ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".maxPoolSize")[0].ToString ());

                } // end if (poolingEnabled)

                if (!trustedConnection) {

                    try {

                        UserName = ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".userName")[0].ToString ();

                        Password = ConfigurationManager.AppSettings.GetValues (configurationPrefix + ".password")[0].ToString ();
                    }

                    catch {
                        // DO NOTHING

                    }

                }

            } // end try

            catch (Exception ConfigException) {

                throw (new ApplicationException ("Unable to read Sql Server Configuration Settings.", ConfigException));

            } // end catch 

            return;

        }

        public void SetSqlConfiguration (String serverName, String databaseName, Boolean poolingEnabled, Int32 minPoolSize, Int32 maxPoolSize) {

            ServerName = serverName;

            DatabaseName = databaseName;

            PoolingEnabled = poolingEnabled;

            MinPoolSize = minPoolSize;

            MaxPoolSize = maxPoolSize;

            return;

        }

        public void SetSqlConfiguration (String serverName, String databaseName, Boolean poolingEnabled, Int32 minPoolSize, Int32 maxPoolSize, String customAttributes) {

            SetSqlConfiguration (serverName, databaseName, poolingEnabled, minPoolSize, maxPoolSize);

            CustomAttributes = customAttributes;

            return;

        }

        public void SetCredentials (String userName, String password) {

            trustedConnection = false;

            UserName = userName;

            Password = password;

            return;

        }

        #endregion

    }

}
