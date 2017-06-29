using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Runtime;

using Mercury.Server.Workflows.EventArguments;
using Mercury.Server.Workflows.UserInteractions;
using Mercury.Server.Workflows.UserInteractions.Enumerations;

namespace Mercury.Server.Workflows {

    public class WorkflowManager {

        #region Private Properties

        private System.Workflow.Runtime.WorkflowRuntime workflowRuntime = new System.Workflow.Runtime.WorkflowRuntime ();

        private System.Workflow.Activities.ExternalDataExchangeService dataExchageService = new System.Workflow.Activities.ExternalDataExchangeService ();

        private Mercury.Server.Workflows.WorkflowService workflowService = new WorkflowService ();

        private Boolean canUnload = true;

        private Int64 workflowId = 0;

        private System.Workflow.Runtime.WorkflowInstance workflowInstance;

        private WorkflowStatus workflowStatus = WorkflowStatus.Unloaded;

        private Mercury.Server.Workflows.WorkflowResponse workflowResponse = new Mercury.Server.Workflows.WorkflowResponse ();

        private System.Threading.AutoResetEvent waitHandle = null;

        private static String assemblyReferencePath;

        private static String assemblyReferenceName;

        private Application application = null;

        #endregion


        #region Public Properties

        public WorkflowStatus WorkflowStatus {

            get { return workflowStatus; }

        }

        private void SetWorkflowStatus (WorkflowStatus status) {

            Object lockObject = new Object ();

            lock (lockObject) {

                workflowStatus = status;

                if (workflowInstance != null) {

                    workflowResponse.WorkflowStatus = status;

                    System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceVerbose, "Workflow [" + workflowInstance.InstanceId + "]: " + status.ToString ());

                }

            }

        }

        public Mercury.Server.Workflows.WorkflowResponse WorkflowResponse {

            get { return workflowResponse; }

        }

        #endregion


        #region Domain Assembly Resolve Event

        public System.Reflection.Assembly CurrentDomain_AssemblyResolve (Object sender, ResolveEventArgs eventArgs) {

            System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceVerbose, "AppDomain AssemblyResolve Event Raised: " + eventArgs.Name);

            String requestedAssemblyName = eventArgs.Name.Split (',')[0] + ".dll";

            // LOOK IN SAME PATH AS ASSEMBLY REFERENCE FOR REQUEST ASSEMBLY

            System.Reflection.Assembly workflowAssembly = System.Reflection.Assembly.LoadFrom (assemblyReferencePath + requestedAssemblyName);

            return workflowAssembly;

        }

        #endregion


        #region Standard Workflow Events

        public void WorkflowCreated (Object sender, WorkflowEventArgs eventArgs) {

            // DON'T VALIDATE WORKFLOW INSTANCE ID FOR LOADED EVENT (WORKFLOWINSTANCE PRIVATE PROPERTY IS NULL)

            SetWorkflowStatus (WorkflowStatus.Created);

            return;
            
        }

        public void WorkflowStarted (Object sender, WorkflowEventArgs eventArgs) {

#if DEBUG

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) {

                System.Diagnostics.Debug.WriteLine ("!---> Workflow Manager, Other Workflow Instance Found: " + eventArgs.WorkflowInstance.InstanceId);

            }

#endif

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            SetWorkflowStatus (WorkflowStatus.Started);

            return;

        }

        public void WorkflowIdled (Object sender, WorkflowEventArgs eventArgs) {

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            SetWorkflowStatus (WorkflowStatus.Idled);

            if (workflowResponse.UserInteractionRequest.UserInteractionType != UserInteractionType.NotSpecified) {

                if (waitHandle != null) { waitHandle.Set (); }

            }

            return;

        }

        public void WorkflowCompleted (Object sender, WorkflowCompletedEventArgs eventArgs) {

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            SetWorkflowStatus (WorkflowStatus.Completed);
            
            workflowResponse.WorkflowSteps = new List<WorkflowStep> ();
            
            if (eventArgs.OutputParameters.ContainsKey ("WorkflowSteps")) {

                try {

                    workflowResponse.WorkflowSteps = (List<Server.Workflows.WorkflowStep>) eventArgs.OutputParameters ["WorkflowSteps"];

                    application.WorkQueueItemWorkflowStepsSave (workflowResponse.WorkQueueItemId, workflowResponse.WorkflowSteps);

                }

                catch { /* DO NOTHING */ }

            }


            if (eventArgs.OutputParameters.ContainsKey ("ChainWorkQueueItemId")) {

                try {

                    workflowResponse.ChainWorkQueueItemId = (Int64) eventArgs.OutputParameters["ChainWorkQueueItemId"];

                }

                catch { /* DO NOTHING */ }

            }

            workflowResponse.OutputParameters = eventArgs.OutputParameters;
            

            canUnload = false;

            if (waitHandle != null) { waitHandle.Set (); }

        }

        public void WorkflowTerminated (Object sender, WorkflowTerminatedEventArgs eventArgs) {

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            SetWorkflowStatus (WorkflowStatus.Terminated);

            workflowResponse = new Mercury.Server.Workflows.WorkflowResponse ();

            workflowResponse.SetException (eventArgs.Exception);

            if ((eventArgs.Exception != null) && (application != null)) {

                application.SetLastException (eventArgs.Exception);

            }

            System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceError, eventArgs.Exception.Message);

            canUnload = false;

            AppDomain.CurrentDomain.AssemblyResolve += null;

            if (waitHandle != null) { waitHandle.Set (); }

            return;

        }

        public void WorkflowSuspended (Object sender, WorkflowSuspendedEventArgs eventArgs) {

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            SetWorkflowStatus (WorkflowStatus.Suspended);

            workflowResponse.WorkflowMessage = eventArgs.Error;

            AppDomain.CurrentDomain.AssemblyResolve += null;

            if (waitHandle != null) { waitHandle.Set (); }

            return;

        }

        public void WorkflowResumed (Object sender, WorkflowEventArgs eventArgs) {

#if DEBUG

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) {

                System.Diagnostics.Debug.WriteLine ("!---> Workflow Manager, Other Workflow Instance Found: " + eventArgs.WorkflowInstance.InstanceId);

            }

#endif

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            SetWorkflowStatus (WorkflowStatus.Resumed);

            return;

        }

        public void WorkflowPersisted (Object sender, WorkflowEventArgs eventArgs) {

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            // LET THE SUSPENSION STATUS OVERRIDE THE PERSIST STATUS (THOUGH PERSIST HAPPENS AFTER A SUSPENSION)

            if (workflowResponse.WorkflowStatus != WorkflowStatus.Suspended) {

                SetWorkflowStatus (WorkflowStatus.Persisted);

            }

            return;

        }

        public void WorkflowLoaded (Object sender, WorkflowEventArgs eventArgs) {

            // DON'T VALIDATE WORKFLOW INSTANCE ID FOR LOADED EVENT (WORKFLOWINSTANCE PRIVATE PROPERTY IS NULL)

            SetWorkflowStatus (WorkflowStatus.Loaded);

            return;

        }

        public void WorkflowAborted (Object sender, WorkflowEventArgs eventArgs) {

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            SetWorkflowStatus (WorkflowStatus.Aborted);

            canUnload = false;

            AppDomain.CurrentDomain.AssemblyResolve += null;

            if (waitHandle != null) { waitHandle.Set (); }

            return;

        }

        public void WorkflowUnloaded (Object sender, WorkflowEventArgs eventArgs) {

            if (eventArgs.WorkflowInstance.InstanceId != workflowInstance.InstanceId) { return; }

            // LET THE SUSPENSION STATUS OVERRIDE THE UNLOAD STATUS (THOUGH UNLOAD HAPPENS AFTER A SUSPENSION)

            if (workflowResponse.WorkflowStatus != WorkflowStatus.Suspended) {

                SetWorkflowStatus (WorkflowStatus.Unloaded);

            }

            return;

        }

        #endregion


        #region Workflow Service Events

        public void WorkflowService_OnUserInteractionRequest (Object sender, UserInteractions.Request.RequestEventArgs eventArgs) {

            workflowResponse.UserInteractionRequest = eventArgs.Request;

            return;

        }

        #endregion


        #region Constructors

        private void InitializeWorkflowManager (Application applicationReference, String sqlPersistenceConnection) {

            application = applicationReference;


            System.Workflow.Runtime.Hosting.SqlWorkflowPersistenceService persistenceService;


            workflowRuntime.WorkflowCreated += new EventHandler<WorkflowEventArgs>(WorkflowCreated);

            workflowRuntime.WorkflowStarted += new EventHandler<WorkflowEventArgs>(WorkflowStarted);

            workflowRuntime.WorkflowIdled += new EventHandler<WorkflowEventArgs>(WorkflowIdled);

            workflowRuntime.WorkflowCompleted += new EventHandler<WorkflowCompletedEventArgs>(WorkflowCompleted);

            workflowRuntime.WorkflowTerminated +=new EventHandler<WorkflowTerminatedEventArgs>(WorkflowTerminated);

            workflowRuntime.WorkflowSuspended += new EventHandler<WorkflowSuspendedEventArgs>(WorkflowSuspended);

            workflowRuntime.WorkflowResumed += new EventHandler<WorkflowEventArgs>(WorkflowResumed);

            workflowRuntime.WorkflowPersisted += new EventHandler<WorkflowEventArgs>(WorkflowPersisted);

            workflowRuntime.WorkflowLoaded += new EventHandler<WorkflowEventArgs>(WorkflowLoaded);

            workflowRuntime.WorkflowAborted += new EventHandler<WorkflowEventArgs>(WorkflowAborted);

            workflowRuntime.WorkflowUnloaded += new EventHandler<WorkflowEventArgs>(WorkflowUnloaded);


            workflowService.OnUserInteractionRequest += new EventHandler<Mercury.Server.Workflows.UserInteractions.Request.RequestEventArgs> (WorkflowService_OnUserInteractionRequest);

            
            persistenceService = new System.Workflow.Runtime.Hosting.SqlWorkflowPersistenceService (sqlPersistenceConnection);

            // workflowRuntime.AddService ((new System.Workflow.Runtime.Hosting.SqlWorkflowPersistenceService (sqlPersistenceConnection, false, new TimeSpan (1, 0, 0), new TimeSpan (1, 0, 0))));

            workflowRuntime.AddService (persistenceService);

            workflowRuntime.AddService (dataExchageService);

            dataExchageService.AddService (workflowService);

        }

        public WorkflowManager (Application applicationReference, String sqlPersistenceConnection) {

            InitializeWorkflowManager (applicationReference, sqlPersistenceConnection);

        }

        public WorkflowManager (Application applicationReference, String sqlPersistenceConnection, System.Threading.AutoResetEvent threadWaitHandle) {

            InitializeWorkflowManager (applicationReference, sqlPersistenceConnection);

            waitHandle = threadWaitHandle;

        }

        #endregion


        #region Workflow Methods

        public void WorkflowStart (String assemblyUrl, String workflowClassName, WorkflowStartRequest startRequest) {

            workflowId = startRequest.WorkflowId;


            assemblyReferencePath = assemblyUrl.Substring (0, assemblyUrl.LastIndexOf ('\\') + 1);

            assemblyReferenceName = assemblyUrl.Replace (assemblyReferencePath, "");


            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler (CurrentDomain_AssemblyResolve);

            System.Reflection.Assembly workflowAssembly = System.Reflection.Assembly.LoadFrom (assemblyUrl);

            Type workflowClass = workflowAssembly.GetType (workflowClassName);

            if (workflowClass == null) {

                throw new ApplicationException ("Unable to find Class [" + workflowClassName + "] in referenced Assembly [" + assemblyUrl + "].");

            }


            Dictionary<String, Object> workflowArguments = new Dictionary<String, Object> ();

            foreach (System.Reflection.PropertyInfo currentProperty in workflowClass.GetProperties ()) {

                String propertyName = currentProperty.Name;

                Type propertyType = currentProperty.PropertyType;

                // System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceVerbose, "Property [" + propertyName + "]: " + propertyType.Name);

                if (startRequest.Arguments.Keys.Contains (propertyName)) {

                    try {

                        switch (propertyType.Name) {

                            case "Int16": workflowArguments.Add (propertyName, Convert.ToInt16 (startRequest.Arguments[propertyName])); break;

                            case "Int32": workflowArguments.Add (propertyName, Convert.ToInt32 (startRequest.Arguments[propertyName])); break;

                            case "Int64": workflowArguments.Add (propertyName, Convert.ToInt64 (startRequest.Arguments[propertyName])); break;

                            case "DateTime": workflowArguments.Add (propertyName, Convert.ToDateTime (startRequest.Arguments[propertyName])); break;

                            case "Decimal": workflowArguments.Add (propertyName, Convert.ToDecimal (startRequest.Arguments[propertyName])); break;

                            case "Single": workflowArguments.Add (propertyName, Convert.ToSingle (startRequest.Arguments[propertyName])); break;

                            case "Double": workflowArguments.Add (propertyName, Convert.ToDouble (startRequest.Arguments[propertyName])); break;

                            case "Boolean": workflowArguments.Add (propertyName, Convert.ToBoolean (startRequest.Arguments[propertyName])); break;

                            case "String": workflowArguments.Add (propertyName, Convert.ToString (startRequest.Arguments[propertyName])); break;

                            default: workflowArguments.Add (propertyName, startRequest.Arguments[propertyName]); break;

                        }

                    }

                    catch (Exception conversionException) {

                        System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceError, conversionException.Message);

                    }

                }

                else {

                    //System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceVerbose, "Ignored Property [" + propertyName + "]: " + propertyType.Name);

                    //System.Diagnostics.Trace.WriteLine ("Ignored Property [" + propertyName + "]: " + propertyType.Name); 
                
                }

            }

            

            workflowInstance = workflowRuntime.CreateWorkflow (workflowAssembly.GetType (workflowClassName), workflowArguments);

            workflowResponse.WorkQueueItemId = startRequest.WorkQueueItemId;

            workflowResponse.WorkflowInstanceId = workflowInstance.InstanceId;

            workflowInstance.Start ();

            return;

        }

        public void WorkflowStart (String assemblyUrl, String workflowClassName, WorkflowStartRequest startRequest, System.Threading.AutoResetEvent threadWaitHandle) {

            waitHandle = threadWaitHandle;

            WorkflowStart (assemblyUrl, workflowClassName, startRequest);

        }

        public void WorkflowContinue (Mercury.Server.Application application, Int64 forWorkflowId, String assemblyUrl, String workflowClassName, Guid workflowInstanceId, Server.Workflows.UserInteractions.Response.ResponseBase userInteractionResponse) {

            workflowId = forWorkflowId;


            assemblyReferencePath = assemblyUrl.Substring (0, assemblyUrl.LastIndexOf ('\\') + 1);

            assemblyReferenceName = assemblyUrl.Replace (assemblyReferencePath, "");


            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            if (!workflowRuntime.IsStarted) { workflowRuntime.StartRuntime (); }

            workflowInstance = workflowRuntime.GetWorkflow (workflowInstanceId);

            workflowResponse.WorkflowInstanceId = workflowInstance.InstanceId;

            workflowService.UserInteractionResponse (application, workflowInstanceId, userInteractionResponse);

            return;

        }

        public void WorkflowResume (Mercury.Server.Application application, Int64 forWorkflowId, String assemblyUrl, String workflowClassName, Guid workflowInstanceId) {

            workflowId = forWorkflowId;


            assemblyReferencePath = assemblyUrl.Substring (0, assemblyUrl.LastIndexOf ('\\') + 1);

            assemblyReferenceName = assemblyUrl.Replace (assemblyReferencePath, "");


            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler (CurrentDomain_AssemblyResolve);

            if (!workflowRuntime.IsStarted) { workflowRuntime.StartRuntime (); }

            workflowInstance = workflowRuntime.GetWorkflow (workflowInstanceId);

            workflowResponse.WorkflowInstanceId = workflowInstance.InstanceId;

            workflowInstance.Resume ();

            return;

        }

        public Boolean WorkflowUnload () {

            Boolean success = false;

            if ((workflowStatus == WorkflowStatus.Completed) || (workflowStatus == WorkflowStatus.Aborted) || (workflowStatus == WorkflowStatus.Terminated)) { return true; }

            if (!canUnload) { success = true; }

            success = workflowInstance.TryUnload ();

            if (!success) {

                try {

                    workflowInstance.Unload ();

                    success = true;

                }

                catch (Exception unloadException) {

                    System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceError, "Workflow Unload Exception: " + unloadException.Message);

                }

            }

            return success;

        }

        public void WorkflowStopManager () {

            workflowRuntime.StopRuntime ();

        }

        #endregion

    }

}
