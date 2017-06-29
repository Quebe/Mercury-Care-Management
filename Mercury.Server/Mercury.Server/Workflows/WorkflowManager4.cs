using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows {

    [Serializable]
    public class WorkflowManager4 {

        #region Private Properties
        
        [NonSerialized]
        private System.Activities.WorkflowApplication workflowInstance = null;

        [NonSerialized]
        private System.Runtime.DurableInstancing.InstanceStore instanceStore = null;

        [NonSerialized]
        private System.Runtime.DurableInstancing.InstanceView instanceView = null;

        [NonSerialized]
        private System.Threading.AutoResetEvent waitHandle = null;

       
        private WorkflowStatus workflowStatus = WorkflowStatus.Created;

        private WorkflowResponse workflowResponse = new WorkflowResponse ();


        private static String assemblyReferencePath;

        private static String assemblyReferenceName;


        private Workflows.UserInteractions.Request.RequestBase userInteractionRequest;

        private Workflows.UserInteractions.Response.ResponseBase userInteractionResponse;

        #endregion 


        #region Public Properties

        public Server.Application Application { get; set; }

        public Workflows.UserInteractions.Request.RequestBase UserInteractionRequest {

            get { return userInteractionRequest; }

            set { userInteractionRequest = value; }

        }

        public Workflows.UserInteractions.Response.ResponseBase UserInteractionResponse {

            get { return userInteractionResponse; }

            set { userInteractionResponse = value; }

        }

        public WorkflowStatus WorkflowStatus {

            get { return workflowStatus; }

            set {

                Object lockObject = new Object ();

                lock (lockObject) {

                    workflowStatus = value;

                    // if (workflowInstance != null) { 

                    workflowResponse.WorkflowStatus = value;
                    
                    // System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceVerbose, "Workflow [" + workflowInstance.InstanceId + "]: " + status.ToString ());

                }

            }

        }

        public WorkflowResponse WorkflowResponse { get { return workflowResponse; } }

        #endregion 
        

        #region Domain Assembly Resolve Event

        public System.Reflection.Assembly CurrentDomain_AssemblyResolve (Object sender, ResolveEventArgs eventArgs) {

            System.Diagnostics.Trace.WriteLineIf (Application.TraceSwitchWorkflow.TraceVerbose, "AppDomain AssemblyResolve Event Raised: " + eventArgs.Name);

            String requestedAssemblyName = eventArgs.Name.Split (',')[0] + ".dll";

            // LOOK IN SAME PATH AS ASSEMBLY REFERENCE FOR REQUEST ASSEMBLY

            System.Reflection.Assembly workflowAssembly = System.Reflection.Assembly.LoadFrom (assemblyReferencePath + requestedAssemblyName);

            return workflowAssembly;

        }

        #endregion


        #region Standard Workflow Application Actions 

        public void WorkflowCompleted (System.Activities.WorkflowApplicationCompletedEventArgs e) {

            // RETURN IF FAULTED FROM EXCEPTION, ALREADY HANDLED IN OTHER EVENT HANDLER

            if (e.CompletionState == System.Activities.ActivityInstanceState.Faulted) { return; } 



            System.Diagnostics.Debug.WriteLine ("Workflow Completed: " + e.CompletionState.ToString ());

            System.Diagnostics.Debug.WriteLine (e.InstanceId.ToString ());

            System.Diagnostics.Debug.WriteLine ((e.TerminationException == null).ToString ());

            WorkflowStatus = Workflows.WorkflowStatus.Completed;


            if (e.Outputs.ContainsKey ("WorkflowSteps")) {

                try {

                    workflowResponse.WorkflowSteps = (List<Server.Workflows.WorkflowStep>)e.Outputs["WorkflowSteps"];

                    Application.WorkQueueItemWorkflowStepsSave (workflowResponse.WorkQueueItemId, workflowResponse.WorkflowSteps);

                }

                catch { /* DO NOTHING */ }

            }


            if (e.Outputs.ContainsKey ("ChainWorkQueueItemId")) {

                try {

                    workflowResponse.ChainWorkQueueItemId = (Int64)e.Outputs["ChainWorkQueueItemId"];

                }

                catch { /* DO NOTHING */ }

            }

            workflowResponse.OutputParameters = new Dictionary<String, Object> ();

            foreach (String outputKey in e.Outputs.Keys) {

                workflowResponse.OutputParameters.Add (outputKey, e.Outputs[outputKey]);

            }


            // DO NOT CLEAR WAIT STATE AS UNLOAD WILL CLEAR IT

            return;

        }

        public void WorkflowAborted (System.Activities.WorkflowApplicationAbortedEventArgs e) {

            System.Diagnostics.Debug.WriteLine ("Workflow Aborted: " + e.Reason);

            System.Diagnostics.Debug.WriteLine (e.InstanceId.ToString ());
            
            workflowResponse.SetException (e.Reason);

            if (Application != null) {

                Application.SetLastException (e.Reason);

                System.Diagnostics.Trace.WriteLineIf (Application.TraceSwitchWorkflow.TraceError, e.Reason.Message);

            }


            AppDomain.CurrentDomain.AssemblyResolve += null;

            WorkflowStatus = Workflows.WorkflowStatus.Aborted;

            if (waitHandle != null) { waitHandle.Set (); }

            return;

        }

        public System.Activities.UnhandledExceptionAction WorkflowOnUnhandledException (System.Activities.WorkflowApplicationUnhandledExceptionEventArgs e) {

            System.Diagnostics.Debug.WriteLine ("Workflow Unhandled Exception: " + e.ExceptionSource.DisplayName);

            System.Diagnostics.Debug.WriteLine ("Workflow Unhandled Exception: " + e.UnhandledException.ToString ());

            System.Diagnostics.Debug.WriteLine (e.InstanceId.ToString ());


            workflowResponse.SetException (e.UnhandledException);

            if (Application != null) {

                Application.SetLastException (e.UnhandledException);

                System.Diagnostics.Trace.WriteLineIf (Application.TraceSwitchWorkflow.TraceError, e.UnhandledException.Message);

            }


            AppDomain.CurrentDomain.AssemblyResolve += null;

            WorkflowStatus = Workflows.WorkflowStatus.Terminated;

            if (waitHandle != null) { waitHandle.Set (); }

            return System.Activities.UnhandledExceptionAction.Terminate;

        }


        public void WorkflowIdle (System.Activities.WorkflowApplicationIdleEventArgs e) {

            // DO NOTHING, WILL CALL PERSISTABLE IDLE NEXT 

            return;

        }

        public System.Activities.PersistableIdleAction WorkflowPersistableIdle (System.Activities.WorkflowApplicationIdleEventArgs e) {

            // SUSPENDED OVERRIDES IDLE/UNLOAD

            if (WorkflowStatus != Workflows.WorkflowStatus.Suspended) { 

                WorkflowStatus = Workflows.WorkflowStatus.Persisted;

            }

            workflowResponse.UserInteractionRequest = UserInteractionRequest;

            workflowResponse.WorkflowInstanceId = e.InstanceId;

            // UNLOAD EVENT RELEASES THREAD WAIT

            return System.Activities.PersistableIdleAction.Unload;

        }

        public void WorkflowUnloaded (System.Activities.WorkflowApplicationEventArgs e) {

            // ONLY PERSISTANCE MOVS TO UNLOAD

            if (WorkflowStatus == Workflows.WorkflowStatus.Persisted) {

                WorkflowStatus = Workflows.WorkflowStatus.Unloaded;

            }

            if (waitHandle != null) { waitHandle.Set (); }
            
            return;

        }

        #endregion 
        
               
        #region Constructors

        private void InitializeWorkflowManager (Application applicationReference, String sqlPersistenceConnection) {

            Application = applicationReference;


            // CREATE INSTANCE STORE AND VIEW FOR PERSISTING TO SQL DATABASE

            instanceStore = new System.Activities.DurableInstancing.SqlWorkflowInstanceStore (sqlPersistenceConnection);

            instanceView = instanceStore.Execute (instanceStore.CreateInstanceHandle (), new System.Activities.DurableInstancing.CreateWorkflowOwnerCommand (), TimeSpan.FromSeconds (30));

            instanceStore.DefaultInstanceOwner = instanceView.InstanceOwner;

            
            return;

        }

        public WorkflowManager4 (Application applicationReference, String sqlPersistenceConnection) {

            InitializeWorkflowManager (applicationReference, sqlPersistenceConnection);

            return;

        }

        public WorkflowManager4 (Application applicationReference, String sqlPersistenceConnection, System.Threading.AutoResetEvent threadWaitHandle) {

            InitializeWorkflowManager (applicationReference, sqlPersistenceConnection);

            waitHandle = threadWaitHandle;

            return;

        }

        #endregion


        #region Workflow Methods

        public void WorkflowStart (String assemblyUrl, String workflowClassName, WorkflowStartRequest startRequest) {

            workflowResponse.WorkQueueItemId = startRequest.WorkQueueItemId;


            #region Load Workflow Assembly

            assemblyReferencePath = assemblyUrl.Substring (0, assemblyUrl.LastIndexOf ('\\') + 1);

            assemblyReferenceName = assemblyUrl.Replace (assemblyReferencePath, "");


            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler (CurrentDomain_AssemblyResolve);

            System.Reflection.Assembly workflowAssembly = System.Reflection.Assembly.LoadFrom (assemblyUrl);

            Type workflowClass = workflowAssembly.GetType (workflowClassName);

            if (workflowClass == null) {

                throw new ApplicationException ("Unable to find Class [" + workflowClassName + "] in referenced Assembly [" + assemblyUrl + "].");

            }

            #endregion


            #region Build Workflow Arguments (Inputs)

            Dictionary<String, Object> workflowArguments = new Dictionary<String, Object> ();

            workflowArguments.Add ("WorkflowManager", this);

            foreach (System.Reflection.PropertyInfo currentProperty in workflowClass.GetProperties ()) {

                String propertyName = currentProperty.Name;

                Type propertyType = currentProperty.PropertyType;


                System.Diagnostics.Debug.WriteLine ("Workflow Property: " + propertyName + " [" + propertyType + "]");

                System.Diagnostics.Debug.WriteLine (currentProperty.ToString ());


                if (startRequest.Arguments.ContainsKey (propertyName)) {

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

                            default:

                                if (propertyName.EndsWith ("Id")) {

                                    workflowArguments.Add (propertyName, Convert.ToInt64 (startRequest.Arguments[propertyName]));

                                }

                                else {

                                    workflowArguments.Add (propertyName, startRequest.Arguments[propertyName]);

                                }
                                
                                break;

                        }

                    }

                    catch (Exception conversionException) {

                        System.Diagnostics.Trace.WriteLineIf (Application.TraceSwitchWorkflow.TraceError, conversionException.Message);

                    }

                }

            }

            #endregion 


            // START WORKFLOW

            workflowInstance = new System.Activities.WorkflowApplication ((System.Activities.Activity)Activator.CreateInstance (workflowAssembly.GetType (workflowClassName)), workflowArguments);


            // LINK EVENT HANDLERS

            workflowInstance.Completed = WorkflowCompleted;

            workflowInstance.Aborted = WorkflowAborted;

            // workflowInstance.Idle = WorkflowIdle; // DO NOTHING, REMOVED, FALL THROUGH TO PERSISTABLE IDLE TO UNLOAD

            workflowInstance.PersistableIdle = WorkflowPersistableIdle;

            workflowInstance.Unloaded = WorkflowUnloaded;

            workflowInstance.OnUnhandledException = WorkflowOnUnhandledException;


            // LINK PERSISTANCE INSTANCE STORE

            workflowInstance.InstanceStore = instanceStore;

            
            // EXECUTE WORKFLOW

            WorkflowStatus = Workflows.WorkflowStatus.Started;

            workflowInstance.Run ();

            return;

        }

        public void WorkflowStart (String assemblyUrl, String workflowClassName, WorkflowStartRequest startRequest, System.Threading.AutoResetEvent threadWaitHandle) {

            waitHandle = threadWaitHandle;

            WorkflowStart (assemblyUrl, workflowClassName, startRequest);

            return;

        }

        public void WorkflowContinue (Mercury.Server.Application application, Int64 workQueueItemId, String assemblyUrl, String workflowClassName, Guid workflowInstanceId, Server.Workflows.UserInteractions.Response.ResponseBase userInteractionResponse) {

            workflowResponse.WorkQueueItemId = workQueueItemId;


            #region Load Workflow Assembly

            assemblyReferencePath = assemblyUrl.Substring (0, assemblyUrl.LastIndexOf ('\\') + 1);

            assemblyReferenceName = assemblyUrl.Replace (assemblyReferencePath, "");


            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler (CurrentDomain_AssemblyResolve);

            System.Reflection.Assembly workflowAssembly = System.Reflection.Assembly.LoadFrom (assemblyUrl);

            Type workflowClass = workflowAssembly.GetType (workflowClassName);

            if (workflowClass == null) {

                throw new ApplicationException ("Unable to find Class [" + workflowClassName + "] in referenced Assembly [" + assemblyUrl + "].");

            }

            #endregion


            // SET RESPONSE VALUE

            UserInteractionResponse = userInteractionResponse;

            
            // CREATE WORKFLOW INSTANCE

            workflowInstance = new System.Activities.WorkflowApplication ((System.Activities.Activity)Activator.CreateInstance (workflowAssembly.GetType (workflowClassName)));


            // LINK EVENT HANDLERS

            workflowInstance.Completed = WorkflowCompleted;

            workflowInstance.Aborted = WorkflowAborted;

            // workflowInstance.Idle = WorkflowIdle; // DO NOTHING, REMOVED, FALL THROUGH TO PERSISTABLE IDLE TO UNLOAD

            workflowInstance.PersistableIdle = WorkflowPersistableIdle;

            workflowInstance.Unloaded = WorkflowUnloaded;

            workflowInstance.OnUnhandledException = WorkflowOnUnhandledException;


            // LINK PERSISTANCE INSTANCE STORE AND LOAD FROM STORE

            workflowInstance.InstanceStore = instanceStore;

            workflowInstance.Load (workflowInstanceId); 

            
            // RESUME FROM BOOKMARK

            workflowInstance.ResumeBookmark (workflowInstanceId.ToString (), this);
           

            return;

        }

        #endregion 
    
    }

}
