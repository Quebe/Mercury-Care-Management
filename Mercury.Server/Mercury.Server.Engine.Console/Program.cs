using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Engine.ConsoleEngine {

    class Program {

        static String enterpriseServerName = "(localhost)";                 // -s

        static String enterpriseDatabaseName = "MercuryEnterprise";         // -d

        static Boolean enterpriseUseTrustedConnection = true;               // (-u cancels)

        static String enterpriseUserName = String.Empty;                    // -u

        static String enterprisePassword = String.Empty;                    // -p

        static String environmentDatabaseName = String.Empty;               // -e

        static Int32 processThreadCount = 1;                                // -t


        static Boolean processServices = true;        // -r s

        static Boolean processMetrics = true;         // -r m

        static Boolean processPopulations = true;     // -r p

        static Boolean pauseAfterRun = false;         // -pause


        static Boolean ProcessCommandLine (string[] args) {


            foreach (String currentArgument in args) {

                if ((currentArgument.Equals ("/?")) || (currentArgument.Equals ("?"))) { 

                    System.Console.WriteLine ("");

                    System.Console.WriteLine ("Command Line Arguments: ");

                    System.Console.WriteLine ("  -s [String] : Enterprise Database Server Name");

                    System.Console.WriteLine ("  -d [String] : Enterprise Database Name");

                    System.Console.WriteLine ("  -u [String] : SQL User Name (cancels default trusted connection)");

                    System.Console.WriteLine ("  -p [String] : SQL User Password");

                    System.Console.WriteLine ("  -e [String] : Environment Database");

                    System.Console.WriteLine ("  -t [#] : number of processing threads ");

                    System.Console.WriteLine ("  -r [s/m/p]: Run Services/Metrics/Populations ");

                    System.Console.WriteLine ("  -pause: Pause After Run");

                    System.Console.WriteLine ("");

                    return false;

                }

            }


            for (Int32 currentArgumentIndex = 0; currentArgumentIndex < (args.Length - 1); currentArgumentIndex++) {

                switch (args[currentArgumentIndex]) {

                    case "-s": enterpriseServerName = args[currentArgumentIndex + 1]; break;

                    case "-d": enterpriseDatabaseName = args[currentArgumentIndex + 1]; break;

                    case "-u": enterpriseUserName = args[currentArgumentIndex + 1]; enterpriseUseTrustedConnection = false; break;

                    case "-p": enterprisePassword = args[currentArgumentIndex + 1]; break;

                    case "-e": environmentDatabaseName = args[currentArgumentIndex + 1]; break;

                    case "-t": Int32.TryParse (args[currentArgumentIndex + 1], out processThreadCount); break;

                    case "-r":

                        processServices = args[currentArgumentIndex + 1].ToLower ().Contains ("s");

                        processMetrics = args[currentArgumentIndex + 1].ToLower ().Contains ("m");

                        processPopulations = args[currentArgumentIndex + 1].ToLower ().Contains ("p");

                        break;

                    case "-pause": pauseAfterRun = true; break;

                }

            }

            return true;

        }

        static void Main (string[] args) {


            if (!ProcessCommandLine (args)) { 

                while (!System.Console.KeyAvailable) { /* do nothing */ }

                return;

            }


            System.Security.Principal.WindowsIdentity windowsIdentity = new System.Security.Principal.WindowsIdentity (System.Security.Principal.WindowsIdentity.GetCurrent ().Token);

            System.Security.Principal.WindowsImpersonationContext impersonationContext = windowsIdentity.Impersonate ();

            System.Threading.Thread.CurrentPrincipal = new System.Security.Principal.WindowsPrincipal (windowsIdentity);


            Mercury.Server.Data.SqlConfiguration enterpriseConfiguration = new Mercury.Server.Data.SqlConfiguration ();

            enterpriseConfiguration.ServerName = enterpriseServerName;

            enterpriseConfiguration.DatabaseName = enterpriseDatabaseName;

            enterpriseConfiguration.TrustedConnection = enterpriseUseTrustedConnection;

            enterpriseConfiguration.UserName = enterpriseUserName;

            enterpriseConfiguration.Password = enterprisePassword;


            try {

                Mercury.Server.Engine.Processor processor = new Processor (enterpriseServerName, enterpriseDatabaseName, enterpriseUserName, enterprisePassword, environmentDatabaseName);
                
                processor.MaximumThreads = processThreadCount;


                if (processServices) {

                    processor.ProcessSingletons ();

                    processor.ProcessSets ();

                    processor.ProcessAuthorizedServices ();

                }

                if (processMetrics) {

                    processor.ProcessMetrics ();

                }

                if (processPopulations) {

                    processor.ProcessPopulations ();

                }
                
            }

            catch (Exception engineException) {

                Exception currentException = engineException;

                do {

                    System.Console.WriteLine (currentException.Message);

                    System.Diagnostics.Debug.WriteLine (currentException.Message);

                    currentException = currentException.InnerException;

                } while (currentException != null);

            }

            finally {

                if (impersonationContext != null) {

                    impersonationContext.Undo ();

                }

            }


            System.Console.WriteLine ("Completed.");

            if (pauseAfterRun) {

                while (!System.Console.KeyAvailable) { /* do nothing */ }

            }

            return;

        }

    }

}
