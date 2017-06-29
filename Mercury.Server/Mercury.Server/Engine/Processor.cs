using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Engine {

    public class Processor {

        #region Private Properties

        private Application application;

        private String token;


        private Mercury.Server.Data.SqlConfiguration enterpriseConfiguration = new Mercury.Server.Data.SqlConfiguration ();

        private String environmentName;

        private Int32 maximumThreads = 1;


        private List<Int64> processedObjects = new List<Int64> ();

        private Dictionary<Int64, System.ComponentModel.BackgroundWorker> processingThreads = new Dictionary<long, System.ComponentModel.BackgroundWorker> ();

        private Object processingThreadsLock = new Object ();

        #endregion 


        #region Public Properties

        public Application Application { get { return application; } }
             

        public Mercury.Server.Data.SqlConfiguration EnterpriseConfiguration { get { return enterpriseConfiguration; } set { enterpriseConfiguration = value; } }

        public String EnvironmentName { get { return environmentName; } set { environmentName = value; } }

        public Int32 MaximumThreads { get { return maximumThreads; } set { maximumThreads = (maximumThreads < 1) ? 1 : value; } }

        #endregion 


        #region Constructors 

        protected void BaseConstructor (String enterpriseServerName, String enterpriseDatabaseName, String sqlUserName, String sqlPassword, String environmentName) {

            enterpriseConfiguration.ServerName = enterpriseServerName;

            enterpriseConfiguration.DatabaseName = enterpriseDatabaseName;

            enterpriseConfiguration.TrustedConnection = String.IsNullOrEmpty (sqlUserName);

            enterpriseConfiguration.UserName = sqlUserName;

            enterpriseConfiguration.Password = sqlPassword;


            EnvironmentName = environmentName;


            Mercury.Server.Security.Security security = new Mercury.Server.Security.Security ();

            Mercury.Server.Security.AuthenticationResponse authenticationResponse = security.Authenticate (enterpriseConfiguration, environmentName);

            if (!authenticationResponse.IsAuthenticated) {

                Exception authenticationException = new ApplicationException ("Unable to authenticate [" + enterpriseServerName + "." + enterpriseDatabaseName + " : " + environmentName + " / " + sqlUserName + "]. " + authenticationResponse.AuthenticationError.ToString (), 
                    
                    (authenticationResponse.AuthenticationException != null) ? new ApplicationException (authenticationResponse.AuthenticationException.Message) : null);

                throw authenticationException;

            }

            token = authenticationResponse.Token;

            application = new Application (enterpriseConfiguration, token);

            return;

        }

        public Processor (String enterpriseServerName, String enterpriseDatabaseName, String environmentName) {

            BaseConstructor (enterpriseServerName, enterpriseDatabaseName, String.Empty, String.Empty, environmentName);

            return;

        }

        public Processor (String enterpriseServerName, String enterpriseDatabaseName, String sqlUserName, String sqlPassword, String environmentName) {

            BaseConstructor (enterpriseServerName, enterpriseDatabaseName, sqlUserName, sqlPassword, environmentName);

            return;

        }

        #endregion 


        #region Public Methods

        public Boolean ProcessSingletons () {

            Boolean success = true;

            List<Mercury.Server.Core.Search.SearchResultMedicalServiceHeader> medicalServices = application.MedicalServiceHeadersGetByType (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Singleton);

            List<Int64> unprocessedServices = new List<Int64> ();


            
            processedObjects = new List<Int64> ();

            processingThreads = new Dictionary<long, System.ComponentModel.BackgroundWorker> ();


            
            foreach (Mercury.Server.Core.Search.SearchResultMedicalServiceHeader currentSingleton in medicalServices) {

                if (currentSingleton.Enabled) {

                    unprocessedServices.Add (currentSingleton.ServiceId);

                }

            }


            while (unprocessedServices.Count != processedObjects.Count) {

                System.Threading.Thread.Sleep (0);

                if (processingThreads.Count < maximumThreads) {

                    lock (processingThreadsLock) { 

                        Int64 singletonId = 0;

                        foreach (Int64 currentUnprocessedServiceId in unprocessedServices) {

                            if ((!processedObjects.Contains (currentUnprocessedServiceId)) && (!processingThreads.ContainsKey (currentUnprocessedServiceId))) {

                                singletonId = currentUnprocessedServiceId;

                                break;

                            }

                        }

                        if (singletonId != 0) {

                            System.ComponentModel.BackgroundWorker backgroundWorker = new System.ComponentModel.BackgroundWorker ();

                            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler (BackgroundWorker_Singleton_OnDoWork);

                            backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler (BackgroundWorker_Singleton_OnCompleted);

                            processingThreads.Add (singletonId, backgroundWorker);

                            backgroundWorker.RunWorkerAsync ();

                        }

                    }

                }

            }
            


            return success;

        }

        public Boolean ProcessSets () {

            Boolean success = true;


            List<Mercury.Server.Core.Search.SearchResultMedicalServiceHeader> medicalServices = application.MedicalServiceHeadersGetByType (Mercury.Server.Core.MedicalServices.Enumerations.MedicalServiceType.Set);

            List<Int64> allServices = new List<Int64> ();


            Dictionary<Int64, Int32> setDepthDictionary = new Dictionary<Int64, Int32> ();

            Int32 depthMin = 1;

            Int32 depthMax = 0;

            Int32 currentDepth = 1;


            processedObjects = new List<Int64> ();

            processingThreads = new Dictionary<long, System.ComponentModel.BackgroundWorker> ();


            foreach (Mercury.Server.Core.Search.SearchResultMedicalServiceHeader currentService in medicalServices) {

                if (currentService.Enabled) {

                    Core.MedicalServices.ServiceSet serviceSet = new Mercury.Server.Core.MedicalServices.ServiceSet (application, currentService.ServiceId);

                    Int32 setDepth = serviceSet.Depth (application);

                    setDepthDictionary.Add (serviceSet.Id, setDepth);

                    allServices.Add (serviceSet.Id);

                }

            }


            foreach (Int64 currentKey in setDepthDictionary.Keys) {

                if (setDepthDictionary[currentKey] < depthMin) { depthMin = setDepthDictionary[currentKey]; }

                if (setDepthDictionary[currentKey] > depthMax) { depthMax = setDepthDictionary[currentKey]; }

            }

            currentDepth = depthMin;


            while (allServices.Count != processedObjects.Count) {

                System.Threading.Thread.Sleep (0);

                if (processingThreads.Count < maximumThreads) {

                    lock (processingThreadsLock) {

                        Int64 serviceSetId = 0;

                        foreach (Int64 currentSetId in allServices) {

                            if (setDepthDictionary[currentSetId] == currentDepth) {

                                if ((!processedObjects.Contains (currentSetId)) && (!processingThreads.ContainsKey (currentSetId))) {

                                    serviceSetId = currentSetId;

                                    break;

                                }

                            }

                        }

                        if (serviceSetId != 0) {

                            System.ComponentModel.BackgroundWorker backgroundWorker = new System.ComponentModel.BackgroundWorker ();

                            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler (BackgroundWorker_Set_OnDoWork);

                            backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler (BackgroundWorker_Set_OnCompleted);

                            processingThreads.Add (serviceSetId, backgroundWorker);

                            backgroundWorker.RunWorkerAsync ();

                        }

                        Boolean depthProcessed = true;

                        foreach (Int64 currentSetId in setDepthDictionary.Keys) {

                            if (setDepthDictionary[currentSetId] == currentDepth) {

                                if (!processedObjects.Contains (currentSetId)) { depthProcessed = false; break; }

                            }

                        }

                        if (depthProcessed) { currentDepth = currentDepth + 1; }

                    }

                }

            }


            return success;

        }

        public Boolean ProcessAuthorizedServices () {

            Boolean success = true;

            List<Mercury.Server.Core.AuthorizedServices.AuthorizedService> authorizedServices = application.AuthorizedServicesAvailable ();

            List<Int64> unprocessedServices = new List<Int64> ();



            processedObjects = new List<Int64> ();

            processingThreads = new Dictionary<long, System.ComponentModel.BackgroundWorker> ();



            foreach (Mercury.Server.Core.AuthorizedServices.AuthorizedService currentAuthorizedService in authorizedServices) {

                if (currentAuthorizedService.Enabled) {

                    unprocessedServices.Add (currentAuthorizedService.Id);

                }

            }


            while (unprocessedServices.Count != processedObjects.Count) {

                System.Threading.Thread.Sleep (0);

                if (processingThreads.Count < maximumThreads) {

                    lock (processingThreadsLock) {

                        Int64 authorizedServiceId = 0;

                        foreach (Int64 currentUnprocessedServiceId in unprocessedServices) {

                            if ((!processedObjects.Contains (currentUnprocessedServiceId)) && (!processingThreads.ContainsKey (currentUnprocessedServiceId))) {

                                authorizedServiceId = currentUnprocessedServiceId;

                                break;

                            }

                        }

                        if (authorizedServiceId != 0) {

                            System.ComponentModel.BackgroundWorker backgroundWorker = new System.ComponentModel.BackgroundWorker ();

                            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler (BackgroundWorker_AuthorizedService_OnDoWork);

                            backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler (BackgroundWorker_AuthorizedService_OnCompleted);

                            processingThreads.Add (authorizedServiceId, backgroundWorker);

                            backgroundWorker.RunWorkerAsync ();

                        }

                    }

                }

            }



            return success;

        }

        public Boolean ProcessMetrics () {

            Boolean success = true;

            List<Mercury.Server.Core.Metrics.Metric> metrics = application.MetricsAvailable ();

            List<Int64> unprocessedMetrics = new List<Int64> ();



            processedObjects = new List<Int64> ();

            processingThreads = new Dictionary<long, System.ComponentModel.BackgroundWorker> ();



            foreach (Mercury.Server.Core.Metrics.Metric currentMetric in metrics) {

                if ((currentMetric.Enabled) && (currentMetric.MetricType == Core.Metrics.Enumerations.MetricType.Cost)) {

                    unprocessedMetrics.Add (currentMetric.Id);

                }

            }


            while (unprocessedMetrics.Count != processedObjects.Count) {

                System.Threading.Thread.Sleep (0);

                if (processingThreads.Count < maximumThreads) {

                    lock (processingThreadsLock) {

                        Int64 metricId = 0;

                        foreach (Int64 currentUnprocessedMetricId in unprocessedMetrics) {

                            if ((!processedObjects.Contains (currentUnprocessedMetricId)) && (!processingThreads.ContainsKey (currentUnprocessedMetricId))) {

                                metricId = currentUnprocessedMetricId;

                                break;

                            }

                        }

                        if (metricId != 0) {

                            System.ComponentModel.BackgroundWorker backgroundWorker = new System.ComponentModel.BackgroundWorker ();

                            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler (BackgroundWorker_Metric_OnDoWork);

                            backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler (BackgroundWorker_Metric_OnCompleted);

                            processingThreads.Add (metricId, backgroundWorker);

                            backgroundWorker.RunWorkerAsync ();

                        }

                    }

                }

            }

            return success;

        }

        public Boolean ProcessPopulations () {

            Boolean success = true;

            List<Mercury.Server.Core.Search.SearchResultPopulationHeader> populations = application.PopulationsAvailable ();

            List<Int64> populationIds = new List<Int64> ();



            processedObjects = new List<Int64> ();

            processingThreads = new Dictionary<long, System.ComponentModel.BackgroundWorker> ();


            foreach (Mercury.Server.Core.Search.SearchResultPopulationHeader currentPopulation in populations) {

                if (currentPopulation.Enabled) {

                    populationIds.Add (currentPopulation.PopulationId);

                }

            }


            maximumThreads = 1;

            while (processedObjects.Count != populationIds.Count) {

                System.Threading.Thread.Sleep (0);

                if (processingThreads.Count < maximumThreads) {

                    lock (processingThreadsLock) {

                        Int64 populationId = 0;

                        foreach (Int64 currentPopulationId in populationIds) {

                            if ((!processedObjects.Contains (currentPopulationId)) && (!processingThreads.ContainsKey (currentPopulationId))) {

                                populationId = currentPopulationId;

                                break;

                            }

                        }

                        if (populationId != 0) {

                            System.ComponentModel.BackgroundWorker backgroundWorker = new System.ComponentModel.BackgroundWorker ();

                            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler (BackgroundWorker_Population_OnDoWork);

                            backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler (BackgroundWorker_Population_OnCompleted);

                            processingThreads.Add (populationId, backgroundWorker);

                            backgroundWorker.RunWorkerAsync ();

                        }

                    }

                }

            }             

            return success;

        }

        public Boolean ProcessAll () {

            Boolean success = true;


            if (success) { success = ProcessSingletons (); }

            if (success) { success = ProcessSets (); }

            if (success) { success = ProcessPopulations (); }


            return success;

        }

        #endregion 


        #region Background Worker Thread Events

        protected void BackgroundWorker_Singleton_OnDoWork (Object sender, System.ComponentModel.DoWorkEventArgs eventArgs) {

            Int64 singletonId = 0;

            Boolean success = false;


            lock (processingThreadsLock) {

                foreach (Int64 currentSingletonId in processingThreads.Keys) {

                    if (processingThreads[currentSingletonId] == sender) {

                        singletonId = currentSingletonId;

                        System.Diagnostics.Debug.WriteLine ("Found Singleton to Process: " + currentSingletonId.ToString ());

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                    }

                }

            }

            if (singletonId != 0) {

                Application threadApplication = new Application (enterpriseConfiguration, token);

                Core.MedicalServices.ServiceSingleton singleton = threadApplication.MedicalServiceSingletonGet (singletonId);

                System.Diagnostics.Debug.WriteLine ("Processing Singleton: " + singleton.Name);

                System.Console.WriteLine ("Processing Singleton: " + singleton.Name);

                success = singleton.Process (threadApplication);

            }

            return; 

        }

        protected void BackgroundWorker_Singleton_OnCompleted (Object sender, System.ComponentModel.RunWorkerCompletedEventArgs eventArgs) {

            lock (processingThreadsLock) {

                foreach (Int64 currentSingletonId in processingThreads.Keys) {

                    if (processingThreads[currentSingletonId] == sender) {

                        processedObjects.Add (currentSingletonId);

                        processingThreads.Remove (currentSingletonId);

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                        break;

                    }

                }

            }

            return;

        }


        protected void BackgroundWorker_Set_OnDoWork (Object sender, System.ComponentModel.DoWorkEventArgs eventArgs) {

            Int64 setId = 0;

            Boolean success = false;


            lock (processingThreadsLock) {

                foreach (Int64 currentSetId in processingThreads.Keys) {

                    if (processingThreads[currentSetId] == sender) {

                        setId = currentSetId;

                        System.Diagnostics.Debug.WriteLine ("Found Set to Process: " + currentSetId.ToString ());

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                    }

                }

            }

            if (setId != 0) {

                Application threadApplication = new Application (enterpriseConfiguration, token);

                Core.MedicalServices.ServiceSet serviceSet = threadApplication.MedicalServiceSetGet (setId);

                System.Diagnostics.Debug.WriteLine ("Processing Set: " + serviceSet.Name);

                System.Console.WriteLine ("Processing Set: " + serviceSet.Name);

                success = serviceSet.Process ();

            }

            return;

        }

        protected void BackgroundWorker_Set_OnCompleted (Object sender, System.ComponentModel.RunWorkerCompletedEventArgs eventArgs) {

            lock (processingThreadsLock) {

                foreach (Int64 currentSetId in processingThreads.Keys) {

                    if (processingThreads[currentSetId] == sender) {

                        processedObjects.Add (currentSetId);

                        processingThreads.Remove (currentSetId);

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                        break;

                    }

                }

            }

            return;

        }


        protected void BackgroundWorker_AuthorizedService_OnDoWork (Object sender, System.ComponentModel.DoWorkEventArgs eventArgs) {

            Int64 authorizedServiceId = 0;

            Boolean success = false;


            lock (processingThreadsLock) {

                foreach (Int64 currentId in processingThreads.Keys) {

                    if (processingThreads[currentId] == sender) {

                        authorizedServiceId = currentId;

                        System.Diagnostics.Debug.WriteLine ("Found Authorized Service to Process: " + currentId.ToString ());

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                    }

                }

            }

            if (authorizedServiceId != 0) {

                Application threadApplication = new Application (enterpriseConfiguration, token);

                Core.AuthorizedServices.AuthorizedService authorizedService = threadApplication.AuthorizedServiceGet (authorizedServiceId);

                System.Diagnostics.Debug.WriteLine ("Processing Authorized Service: " + authorizedService.Name);

                System.Console.WriteLine ("Processing Authorized Service: " + authorizedService.Name);

                success = authorizedService.Process (threadApplication);

            }

            return;

        }

        protected void BackgroundWorker_AuthorizedService_OnCompleted (Object sender, System.ComponentModel.RunWorkerCompletedEventArgs eventArgs) {

            lock (processingThreadsLock) {

                foreach (Int64 currentId in processingThreads.Keys) {

                    if (processingThreads[currentId] == sender) {

                        processedObjects.Add (currentId);

                        processingThreads.Remove (currentId);

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                        break;

                    }

                }

            }

            return;

        }


        protected void BackgroundWorker_Metric_OnDoWork (Object sender, System.ComponentModel.DoWorkEventArgs eventArgs) {

            Int64 metricId = 0;

            Boolean success = false;


            lock (processingThreadsLock) {

                foreach (Int64 currentId in processingThreads.Keys) {

                    if (processingThreads[currentId] == sender) {

                        metricId = currentId;

                        System.Diagnostics.Debug.WriteLine ("Found Metric to Process: " + currentId.ToString ());

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                    }

                }

            }

            if (metricId != 0) {

                Application threadApplication = new Application (enterpriseConfiguration, token);

                Core.Metrics.Metric metric = threadApplication.MetricGet (metricId);

                System.Diagnostics.Debug.WriteLine ("Processing Metric: " + metric.Name);

                System.Console.WriteLine ("Processing Metric: " + metric.Name);

                success = metric.Process (threadApplication);

            }

            return;

        }

        protected void BackgroundWorker_Metric_OnCompleted (Object sender, System.ComponentModel.RunWorkerCompletedEventArgs eventArgs) {

            lock (processingThreadsLock) {

                foreach (Int64 currentId in processingThreads.Keys) {

                    if (processingThreads[currentId] == sender) {

                        processedObjects.Add (currentId);

                        processingThreads.Remove (currentId);

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                        break;

                    }

                }

            }

            return;

        }


        protected void BackgroundWorker_Population_OnDoWork (Object sender, System.ComponentModel.DoWorkEventArgs eventArgs) {

            Int64 populationId = 0;

            Boolean success = false;


            lock (processingThreadsLock) {

                foreach (Int64 currentPopulationId in processingThreads.Keys) {

                    if (processingThreads[currentPopulationId] == sender) {

                        populationId = currentPopulationId;

                        System.Diagnostics.Debug.WriteLine ("Found Population to Process: " + currentPopulationId.ToString ());

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                    }

                }

            }

            if (populationId != 0) {

                Application threadApplication = new Application (enterpriseConfiguration, token);

                Core.Population.Population population = threadApplication.PopulationGet (populationId);

                System.Diagnostics.Debug.WriteLine ("Processing Population: " + population.Name);

                System.Console.WriteLine ("Processing Population: " + population.Name);

                success = population.Process ();

            }

            return;

        }

        protected void BackgroundWorker_Population_OnCompleted (Object sender, System.ComponentModel.RunWorkerCompletedEventArgs eventArgs) {

            lock (processingThreadsLock) {

                foreach (Int64 currentPopulationId in processingThreads.Keys) {

                    if (processingThreads[currentPopulationId] == sender) {

                        processedObjects.Add (currentPopulationId);

                        processingThreads.Remove (currentPopulationId);

                        System.Diagnostics.Debug.WriteLine ("Current Thread Count: " + processingThreads.Count);

                        break;

                    }

                }

            }

            return;

        }

        #endregion 

    }

}
