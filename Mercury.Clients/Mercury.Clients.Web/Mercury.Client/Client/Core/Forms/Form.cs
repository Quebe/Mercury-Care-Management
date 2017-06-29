using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms {

    [Serializable]
    public class Form : Control {

        #region Private Members

        private Int64 formId = 0;

        private Int64 entityFormId = 0;

        private Mercury.Server.Application.FormType formType = Mercury.Server.Application.FormType.Template;

        private Mercury.Server.Application.EntityType entityType = Mercury.Server.Application.EntityType.Member;

        private Int64 entityObjectId = 0;


        private Mercury.Server.Application.FormControlOrientation orientation = Mercury.Server.Application.FormControlOrientation.Portrait;

        private Mercury.Server.Application.FormControlPaperSize paperSize = Mercury.Server.Application.FormControlPaperSize.Letter;


        private Boolean allowPrecompileEvents = false;

        protected Dictionary<String, System.Reflection.Assembly> compiledEvents = new Dictionary<String, System.Reflection.Assembly> ();

        protected List<EventResult> eventResults = new List<EventResult> ();

        #endregion


        #region Public Members;

        public override Int64 Id { get { return ((formType == Server.Application.FormType.Template) ? formId : entityFormId); } }

        public Int64 FormId { get { return formId; } set { formId = value; } }

        public Int64 EntityFormId { get { return entityFormId; } set { entityFormId = value; } }


        public Mercury.Server.Application.FormType FormType { get { return formType; } set { formType = value; } }

        public Mercury.Server.Application.EntityType EntityType { get { return entityType; } set { entityType = value; DataSourceChanged (); } }

        public Int64 EntityObjectId {

            get { return entityObjectId; }

            set {

                if (entityObjectId != value) {

                    if (value != 0) { formType = Mercury.Server.Application.FormType.Instance; }

                    entityObjectId = value;

                    DataSourceChanged ();

                }

            }

        }

        public Mercury.Server.Application.FormControlOrientation Orientation { get { return orientation; } set { orientation = value; } }

        public Mercury.Server.Application.FormControlPaperSize PaperSize { get { return paperSize; } set { paperSize = value; } }


        public Boolean AllowPrecompileEvents { get { return allowPrecompileEvents; } set { allowPrecompileEvents = value; } }

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


        public Dictionary<Int32, List<Controls.Section>> Pages {

            get {

                Dictionary<Int32, List<Controls.Section>> pages = new Dictionary<Int32, List<Controls.Section>> ();

                List<Controls.Section> currentPage = new List<Controls.Section> ();

                Int32 currentPageIndex = 1;


                foreach (Control currentControl in controls) {

                    Forms.Controls.Section currentSection = (Forms.Controls.Section)currentControl;

                    currentPage.Add (currentSection);

                    if (currentSection.PageBreakAfterSection) {

                        pages.Add (currentPageIndex, currentPage);

                        currentPageIndex = currentPageIndex + 1;

                        currentPage = new List<Controls.Section> ();

                    }

                }

                if (currentPage.Count > 0) { pages.Add (currentPageIndex, currentPage); } // ADD LAST PAGE

                return pages;

            }

        }

        public Dictionary<Int32, List<Controls.Section>> PagesVisible {

            get {

                Dictionary<Int32, List<Controls.Section>> pagesVisible = new Dictionary<Int32, List<Controls.Section>> ();

                Int32 currentPageIndex = 1;

                foreach (List<Controls.Section> currentPage in Pages.Values) {

                    if (IsPageVisible (currentPage)) {

                        pagesVisible.Add (currentPageIndex, currentPage);

                        currentPageIndex = currentPageIndex + 1;

                    }

                }

                return pagesVisible;

            }

        }

        public Boolean IsPageVisible (List<Controls.Section> currentPage) {

            Boolean isPageVisible = false;

            foreach (Controls.Section currentSection in currentPage) {

                if (currentSection.Visible) { isPageVisible = true; break; }

            }

            return isPageVisible;

        }

        public Int32 PageCountVisible {

            get {

                Int32 currentPageCount = 0;

                Dictionary<Int32, List<Controls.Section>> pages = Pages;

                foreach (List<Controls.Section> currentPage in pages.Values) {

                    if (IsPageVisible (currentPage)) {

                        currentPageCount = currentPageCount + 1;

                    }

                }

                return currentPageCount;

            }

        }

        public List<Controls.Section> PageControls (Int32 pageIndex) {

            List<Controls.Section> pageControls = new List<Controls.Section> ();


            if (pageIndex <= PagesVisible.Count) {

                pageControls = PagesVisible[pageIndex];

            }

            return pageControls;

        }

        public Mercury.Server.Application.AuthorityAccountStamp CurrentUser {

            get {

                Mercury.Server.Application.AuthorityAccountStamp currentUser = new Mercury.Server.Application.AuthorityAccountStamp ();

                currentUser.SecurityAuthorityName = Application.Session.SecurityAuthorityName;

                currentUser.UserAccountId = Application.Session.UserAccountId;

                currentUser.UserAccountName = Application.Session.UserAccountName;

                currentUser.ActionDate = DateTime.Now;

                return currentUser;

            }

        }

        #endregion


        #region Constructors

        protected Form () { /* DO NOTHING */ }


        public Form (Mercury.Client.Application applicationReference) {

            Application = applicationReference;

            ControlType = Mercury.Server.Application.FormControlType.Form;

            name = "Form";

            description = "New Form";

            capabilities.IsDataSource = true;

            return;

        }

        private void FormConstructor (Mercury.Client.Application applicationReference, Mercury.Server.Application.Form serverForm) {

            Application = applicationReference;

            BaseConstructor (applicationReference, null, serverForm);

            ControlType = Mercury.Server.Application.FormControlType.Form;

            formId = serverForm.FormId;

            entityFormId = serverForm.EntityFormId;

            formType = serverForm.FormType;

            description = serverForm.Description;

            entityType = serverForm.EntityType;

            entityObjectId = serverForm.EntityObjectId;

            orientation = serverForm.Orientation;

            paperSize = serverForm.PaperSize;

            allowPrecompileEvents = serverForm.AllowPrecompileEvents;

            eventResults = new List<EventResult> ();

            if (serverForm.EventResults != null) {

                foreach (Mercury.Server.Application.FormControlEventResult currentServerEventResult in serverForm.EventResults) {

                    EventResult clientEventResult = new EventResult (Application, currentServerEventResult);

                    eventResults.Add (clientEventResult);

                }

            }

            createAccountInfo = serverForm.CreateAccountInfo;

            modifiedAccountInfo = serverForm.ModifiedAccountInfo;

            Controls.Clear ();

            for (Int32 sectionIndex = 0; sectionIndex < serverForm.Controls.Length; sectionIndex++) {

                if (serverForm.Controls[sectionIndex].ControlType == Mercury.Server.Application.FormControlType.Section) {

                    Controls.Add (new Mercury.Client.Core.Forms.Controls.Section (Application, this, (Mercury.Server.Application.FormControlSection)serverForm.Controls[sectionIndex]));

                }

            }
            
            return;

        }

        public Form (Mercury.Client.Application applicationReference, Mercury.Server.Application.Form serverForm) {

            FormConstructor (applicationReference, serverForm);

            return;

        }

        #endregion


        #region Public Methods

        public Form Copy () {

            return new Form (Application, (Mercury.Server.Application.Form)ToServerObject ());

        }

        public override Boolean AllowChildControl (Mercury.Server.Application.FormControlType childControlType) {

            if (childControlType != Mercury.Server.Application.FormControlType.Section) { return false; }

            return true;

        }

        public Int32 SectionIndex (Guid id) {

            Int32 sectionIndex = -1;

            for (Int32 currentSectionIndex = 0; currentSectionIndex < Controls.Count; currentSectionIndex++) {

                Controls.Section currentSection = (Controls.Section)Controls[currentSectionIndex];

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

                Controls.Section currentSection = (Controls.Section)Controls[currentSectionIndex];

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

                Controls.Section currentSection = (Controls.Section)Controls[currentSectionIndex];

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

                return (Mercury.Client.Core.Forms.Controls.Section)Controls[sectionIndex];

            }

            return null;

        }

        public Mercury.Client.Core.Forms.Controls.Section SectionByName (String sectionName) {

            Int32 sectionIndex = SectionIndexByName (sectionName);

            if (sectionIndex != -1) {

                return (Mercury.Client.Core.Forms.Controls.Section)Controls[sectionIndex];

            }

            return null;

        }

        public Mercury.Client.Core.Forms.Controls.Section Section (Int32 sectionIndex) {

            return (Mercury.Client.Core.Forms.Controls.Section)Controls[sectionIndex];

        }

        public void InsertSection (Int32 index) {

            Controls.Section newSection = new Controls.Section (Application, this);

            Int32 sectionSuffix = 1;


            if (index == -1) { index = Controls.Count; }

            while (SectionExists ("NewSection" + sectionSuffix.ToString ())) {

                sectionSuffix = sectionSuffix + 1;

            }


            newSection.Name = "NewSection" + sectionSuffix.ToString ();

            newSection.Description = "New Section " + sectionSuffix.ToString ();

            newSection.Parent = this;

            Controls.Insert (index, newSection);

        }

        public override Object ToServerObject () {

            Mercury.Server.Application.Form serverForm = new Mercury.Server.Application.Form ();

            LocalControlToServer (null, serverForm);

            serverForm.FormId = formId;

            serverForm.EntityFormId = entityFormId;

            serverForm.FormType = formType;

            serverForm.Description = description;

            serverForm.EntityType = entityType;

            serverForm.EntityObjectId = entityObjectId;

            serverForm.Orientation = orientation;

            serverForm.PaperSize = paperSize;

            serverForm.AllowPrecompileEvents = allowPrecompileEvents;

            serverForm.CreateAccountInfo = createAccountInfo;

            serverForm.ModifiedAccountInfo = modifiedAccountInfo;

            return serverForm;

        }

        public void RaiseEvent (Control eventControl, String eventName) {

            if (controls.Count == 0) { return; } // NO UPDATE WHEN BACKLOADING DOCUMENT


            List<EventResult> localEventResults = new List<EventResult> ();

            localEventResults.AddRange (EventResults);


            Core.Forms.Form updatedForm = Application.FormControl_RaiseEvent (this, eventControl.ControlId, eventName);

            FormConstructor (Application, (Mercury.Server.Application.Form)updatedForm.ToServerObject ());


            List<EventResult> serverEventResults = new List<EventResult> ();

            serverEventResults.AddRange (updatedForm.EventResults);


            eventResults = new List<EventResult> ();

            eventResults.AddRange (localEventResults);

            eventResults.AddRange (serverEventResults);

            return;

        }

        public override void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            if (controls.Count == 0) { return; } // NO UPDATE WHEN BACKLOADING DOCUMENT


            List<EventResult> localEventResults = new List<EventResult> ();

            localEventResults.AddRange (EventResults);


            Core.Forms.Form updatedForm = Application.Form_OnDataSourceChanged (this, dataSourceControl.ControlId);

            FormConstructor (Application, (Mercury.Server.Application.Form)updatedForm.ToServerObject ());


            List<EventResult> serverEventResults = new List<EventResult> ();

            serverEventResults.AddRange (updatedForm.EventResults);


            eventResults = new List<EventResult> ();

            eventResults.AddRange (localEventResults);

            eventResults.AddRange (serverEventResults);

            return;

        }

        public List<Mercury.Server.Application.FormCompileMessage> Submit () {

            List<Mercury.Server.Application.FormCompileMessage> submitMessages = new List<Mercury.Server.Application.FormCompileMessage> ();

            if (controls.Count == 0) { return submitMessages; } // NO UPDATE WHEN BACKLOADING DOCUMENT


            Mercury.Server.Application.FormSubmitResponse submitResponse = Application.FormSubmit (this);

            FormConstructor (Application, submitResponse.Form);

            submitMessages.AddRange (submitResponse.Collection);

            return submitMessages;

        }

        #endregion

    }

}
