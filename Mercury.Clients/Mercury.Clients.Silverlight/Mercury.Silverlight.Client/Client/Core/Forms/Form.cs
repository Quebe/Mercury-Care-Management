using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Mercury.Client.Core.Forms {

    public class Form : Control {

        #region Private Members

        private Int64 formId = 0;

        private Int64 entityFormId = 0;

        private Server.Application.FormType formType = Server.Application.FormType.Template;

        private Server.Application.EntityType entityType = Server.Application.EntityType.Member;

        private Int64 entityObjectId = 0;


        private Server.Application.FormControlOrientation orientation = Server.Application.FormControlOrientation.Portrait;

        private Server.Application.FormControlPaperSize paperSize = Server.Application.FormControlPaperSize.Letter;


        protected Dictionary<String, System.Reflection.Assembly> compiledEvents = new Dictionary<String, System.Reflection.Assembly> ();

        protected List<EventResult> eventResults = new List<EventResult> ();


        private Boolean isServerProcessing = false;

        private Queue<ServerProcessEventArgs> serverProcessQueue = new Queue<ServerProcessEventArgs> ();

        private Object serverProcessQueueSyncRoot = new Object (); 

        #endregion


        #region Public Members;

        public override Int64 Id { get { return ((formType == Server.Application.FormType.Template) ? formId : entityFormId); } }


        public Int64 FormId { get { return formId; } set { formId = value; } }

        public Int64 EntityFormId { get { return entityFormId; } set { entityFormId = value; } }

        public Server.Application.FormType FormType { get { return formType; } set { formType = value; } }

        

        public Server.Application.EntityType EntityType { get { return entityType; } set { entityType = value; DataSourceChanged (); } }

        public Int64 EntityObjectId {

            get { return entityObjectId; }

            set {

                if (entityObjectId != value) {

                    if (value != 0) { formType = Server.Application.FormType.Instance; }

                    entityObjectId = value;

                    DataSourceChanged ();

                }

            }

        }

        public Server.Application.FormControlOrientation Orientation { get { return orientation; } set { orientation = value; } }

        public Server.Application.FormControlPaperSize PaperSize { get { return paperSize; } set { paperSize = value; } }


        public Dictionary<String, System.Reflection.Assembly> CompiledEvents { get { return compiledEvents; } set { compiledEvents = value; } }

        public List<EventResult> EventResults {

            get {

                if (eventResults == null) { eventResults = new List<EventResult> (); }

                return eventResults;

            }

            set { eventResults = value; }

        }

        public List<EventResult> EventResultsCopy {

            get {

                List<EventResult> copiedResults = new List<EventResult> ();

                foreach (EventResult currentEventResult in EventResults) {

                    EventResult copiedEventResult = new EventResult (currentEventResult.ControlId, currentEventResult.EventName, currentEventResult.LastException);

                    copiedEventResult.Success = currentEventResult.Success;

                    copiedEventResult.ListenerOutput = currentEventResult.ListenerOutput;

                    copiedResults.Add (copiedEventResult);

                }

                return copiedResults;

            }

        }

        #endregion


        #region Constructors

        public Form (Mercury.Client.Application applicationReference) {

            BaseConstructor (applicationReference);

            Application = applicationReference;

            ControlType = Server.Application.FormControlType.Form;

            capabilities.IsDataSource = true;

            return;

        }

        private void FormConstructor (Mercury.Client.Application applicationReference, Server.Application.Form serverForm) {

            Application = applicationReference;

            BaseConstructor (null, serverForm);

            ControlType = Server.Application.FormControlType.Form;

            formId = serverForm.FormId;

            entityFormId = serverForm.EntityFormId;

            formType = serverForm.FormType;

            description = serverForm.Description;

            entityType = serverForm.EntityType;

            entityObjectId = serverForm.EntityObjectId;

            orientation = serverForm.Orientation;

            paperSize = serverForm.PaperSize;

            eventResults = new List<EventResult> ();

            if (serverForm.EventResults != null) {

                foreach (Server.Application.FormControlEventResult currentServerEventResult in serverForm.EventResults) {

                    EventResult clientEventResult = new EventResult (Application, currentServerEventResult);

                    eventResults.Add (clientEventResult);

                }

            }

            CreateAccountInfo = serverForm.CreateAccountInfo;

            ModifiedAccountInfo = serverForm.ModifiedAccountInfo;

            Controls.Clear ();

            foreach (Server.Application.FormControl currentServerControl in serverForm.Controls) {

                if (currentServerControl.ControlType == Server.Application.FormControlType.Section) {

                    Controls.Add (new Controls.Section (Application, this, (Server.Application.FormControlSection) currentServerControl));

                }

            }

            return;

        }

        public Form (Mercury.Client.Application applicationReference, Server.Application.Form serverForm) {

            FormConstructor (applicationReference, serverForm);

            return;

        }

        #endregion


        #region Public Methods

        public Form Copy () {

            return new Form (Application, (Server.Application.Form) ToServerObject ());

        }

        public override Boolean AllowChildControl (Server.Application.FormControlType childControlType) {

            if (childControlType != Server.Application.FormControlType.Section) { return false; }

            return true;

        }

        public Int32 SectionIndex (Guid id) {

            Int32 sectionIndex = -1;

            for (Int32 currentSectionIndex = 0; currentSectionIndex < Controls.Count; currentSectionIndex++) {

                Controls.Section currentSection = (Controls.Section) Controls[currentSectionIndex];

                if (currentSection.ControlId == controlId) {

                    sectionIndex = currentSectionIndex;

                    break;

                }

            }

            return sectionIndex;

        }

        public Int32 SectionIndexByName (String sectionName) {

            Int32 sectionIndex = -1;

            for (Int32 currentSectionIndex = 0; currentSectionIndex < Controls.Count; currentSectionIndex++) {

                Controls.Section currentSection = (Controls.Section) Controls[currentSectionIndex];

                if (currentSection.Name == sectionName) {

                    sectionIndex = currentSectionIndex;

                    break;

                }

            }

            return sectionIndex;

        }

        public Boolean SectionExists (String sectionName) {

            Boolean sectionFound = false;

            for (Int32 currentSectionIndex = 0; currentSectionIndex < Controls.Count; currentSectionIndex++) {

                Controls.Section currentSection = (Controls.Section) Controls[currentSectionIndex];

                if (currentSection.Name == sectionName) {

                    sectionFound = true;

                    break;

                }

            }

            return sectionFound;

        }

        public Mercury.Client.Core.Forms.Controls.Section Section (Guid id) {

            Int32 sectionIndex = SectionIndex (id);

            if (sectionIndex != -1) {

                return (Mercury.Client.Core.Forms.Controls.Section) Controls[sectionIndex];

            }

            return null;

        }

        public Mercury.Client.Core.Forms.Controls.Section SectionByName (String sectionName) {

            Int32 sectionIndex = SectionIndexByName (sectionName);

            if (sectionIndex != -1) {

                return (Mercury.Client.Core.Forms.Controls.Section) Controls[sectionIndex];

            }

            return null;

        }

        public Mercury.Client.Core.Forms.Controls.Section Section (Int32 sectionIndex) {

            return (Mercury.Client.Core.Forms.Controls.Section) Controls[sectionIndex];

        }

        public void InsertSection (Int32 index) {

            Controls.Section newSection = new Controls.Section (Application, this);

            Int32 sectionSuffix = 1;


            if (index == -1) { index = Controls.Count; }

            while (SectionExists ("NewSection" + sectionSuffix.ToString ())) {

                sectionSuffix = sectionSuffix + 1;

            }


            newSection.Name = "New Section " + sectionSuffix.ToString ();

            newSection.Parent = this;

            Controls.Insert (index, newSection);

        }

        public override Object ToServerObject () {

            Server.Application.Form serverForm = new Server.Application.Form ();

            LocalControlToServer (null, serverForm);

            serverForm.FormId = formId;

            serverForm.EntityFormId = entityFormId;

            serverForm.FormType = formType;

            serverForm.Description = description;

            serverForm.EntityType = entityType;

            serverForm.EntityObjectId = entityObjectId;

            serverForm.Orientation = orientation;

            serverForm.PaperSize = paperSize;

            serverForm.CreateAccountInfo = CreateAccountInfo;

            serverForm.ModifiedAccountInfo = ModifiedAccountInfo;

            return serverForm;

        }

        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            //if (controls.Count == 0) { return; } // NO UPDATE WHEN BACKLOADING DOCUMENT


            //List<EventResult> localEventResults = new List<EventResult> ();

            //localEventResults.AddRange (EventResults);


            //Core.Forms.Form updatedForm = Application.Form_OnDataSourceChanged (this, dataSourceControl.ControlId, null);

            //FormConstructor (Application, (Server.Application.Form) updatedForm.ToServerObject ());


            //List<EventResult> serverEventResults = new List<EventResult> ();

            //serverEventResults.AddRange (updatedForm.EventResults);


            //eventResults = new List<EventResult> ();

            //eventResults.AddRange (localEventResults);

            //eventResults.AddRange (serverEventResults);

            //return;

        }

        #endregion


        #region Server Processing

        public void RaiseEvent (Control eventControl, String eventName, ServerProcessCompleted eventHandler) {

            if (eventControl == null) { return; }


            ServerProcessEventArgs request = new ServerProcessEventArgs ();

            request.RequestType = ServerProcessRequestType.RaiseEvent;

            request.SourceControl = eventControl;

            request.EventName = eventName;

            request.Completed += new ServerProcessCompleted(eventHandler);


            lock (serverProcessQueueSyncRoot) {

                serverProcessQueue.Enqueue (request);

            }

            ProcessServerQueue ();

            return;

        }

        public void ValueChanged (Control sourceControl, ServerProcessCompleted eventHandler) {

            if (sourceControl == null) { return; }


            ServerProcessEventArgs request = new ServerProcessEventArgs ();

            request.RequestType = ServerProcessRequestType.ValueChanged;

            request.SourceControl = sourceControl;

            request.Completed += new ServerProcessCompleted (eventHandler);


            lock (serverProcessQueueSyncRoot) {

                serverProcessQueue.Enqueue (request);

            }

            ProcessServerQueue ();

            return;

        }

        private void ProcessServerQueue () {

            if (isServerProcessing) { return; }

            if (serverProcessQueue.Count == 0) { return; }


            isServerProcessing = true; // MARK PROCESSING FOR SINGLE THREADING

            ServerProcessEventArgs request = null;

            Boolean cancelProcessing = false;

            lock (serverProcessQueueSyncRoot) {

                request = serverProcessQueue.Peek ();  // PEEK TO RETREIVE NEXT QUEUE ITEM FOR PROCESS (COMPLETION DEQUEUES THE ITEM)

            }

            if (request != null) {

                switch (request.RequestType) {

                    case ServerProcessRequestType.RaiseEvent:

                        if (request.SourceControl.HasEventHandler (request.EventName)) {

                            request.ProcessStartTime = DateTime.Now;

                            Application.FormControl_RaiseEvent ((Server.Application.Form) this.ToServerObject (), request.SourceControl.ControlId, request.EventName, RaiseEventCompleted);

                        }

                        else { cancelProcessing = true; }

                        break;

                    case ServerProcessRequestType.ValueChanged:

                        if ((eventHandlers.Count > 0) || (HasDependencyDataBinding)) {

                            request.ProcessStartTime = DateTime.Now;

                            Application.FormControl_ValueChanged ((Server.Application.Form) this.ToServerObject (), request.SourceControl.ControlId, ValueChangedCompleted);

                        }

                        else { cancelProcessing = true; }

                        break;

                    default: cancelProcessing = true; break;

                }

            }

            else { cancelProcessing = true; }



            if (cancelProcessing) {

                lock (serverProcessQueueSyncRoot) {

                    request = serverProcessQueue.Dequeue ();

                    isServerProcessing = false;

                }

            }


            if (!isServerProcessing) { ProcessServerQueue (); }

            return;

        }

        private void RaiseEventCompleted (Object sender, Server.Application.FormControl_RaiseEventCompletedEventArgs e) {

            ServerProcessEventArgs request = null;

            lock (serverProcessQueueSyncRoot) {

                request = serverProcessQueue.Dequeue ();

            }

            if (request != null) {

                request.ProcessEndTime = DateTime.Now;

                System.Diagnostics.Debug.WriteLine ("Form Process: " + DateTime.Now.Subtract (request.ProcessStartTime).Milliseconds.ToString ());

            }

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (!e.Result.HasException) {

                    DateTime updateStartTime = DateTime.Now;

                    UpdateControl (e.Result.Form);

                    System.Diagnostics.Debug.WriteLine ("Form Update Controls: " + DateTime.Now.Subtract (updateStartTime).Milliseconds.ToString ());

                }

            }


            if (request != null) {

                if (request.Completed != null) {

                    request.Completed (this, request);

                }

            }

            isServerProcessing = false;

            ProcessServerQueue ();
                
            return;

        }

        private void ValueChangedCompleted (Object sender, Server.Application.FormControl_ValueChangedCompletedEventArgs e) {

            ServerProcessEventArgs request = null;

            lock (serverProcessQueueSyncRoot) {

                request = serverProcessQueue.Dequeue ();

            }

            if (request != null) {

                request.ProcessEndTime = DateTime.Now;

                System.Diagnostics.Debug.WriteLine ("Form Process: " + DateTime.Now.Subtract (request.ProcessStartTime).Milliseconds.ToString ());

            }

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                if (!e.Result.HasException) {

                    DateTime updateStartTime = DateTime.Now;

                    UpdateControl (e.Result.Form);

                    System.Diagnostics.Debug.WriteLine ("Form Update Controls: " + DateTime.Now.Subtract (updateStartTime).Milliseconds.ToString ());

                }

            }

            if (request != null) {

                if (request.Completed != null) {

                    request.Completed (this, request);

                }

            }

            isServerProcessing = false;

            ProcessServerQueue ();

            return;

        }

        public delegate void ServerProcessCompleted (Object sender, ServerProcessEventArgs e);

        #endregion 

    }

}