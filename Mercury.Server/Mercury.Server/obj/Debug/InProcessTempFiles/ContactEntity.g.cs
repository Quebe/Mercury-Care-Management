//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Mercury.Server.Workflows.Activities.ContactEntity {
    
    
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class ContactEntity : System.Activities.Activity, System.ComponentModel.ISupportInitialize {
        
        private bool _contentLoaded;
        
        private System.Activities.InOutArgument<System.Collections.Generic.List<Mercury.Server.Workflows.WorkflowStep>> _WorkflowSteps;
        
        private System.Activities.InArgument<Mercury.Server.Core.Entity.Entity> _Entity;
        
        private System.Activities.InArgument<bool> _AllowEditContactDateTime;
        
        private System.Activities.InArgument<bool> _AllowEditRegarding;
        
        private System.Activities.InArgument<string> _RegardingMessage;
        
        private System.Activities.InArgument<string> _IntroductionScript;
        
        private System.Activities.InArgument<bool> _AllowCancel;
        
        private System.Activities.InOutArgument<Mercury.Server.Workflows.WorkflowManager4> _WorkflowManager;
        
        private System.Activities.InArgument<bool> _AutoSaveContact;
        
        private System.Activities.InOutArgument<int> _ContactAttempts;
        
        private System.Activities.OutArgument<bool> _ContactSuccessful;
        
        private System.Activities.OutArgument<Mercury.Server.Core.Enumerations.ContactOutcome> _ContactOutcome;
        
        private System.Activities.OutArgument<bool> _ActivityCanceled;
        
        private System.Activities.InArgument<long> _WorkQueueItemId;
        
        private System.Activities.OutArgument<Mercury.Server.Core.Entity.EntityContact> _EntityContact;
        
partial void BeforeInitializeComponent(ref bool isInitialized);

partial void AfterInitializeComponent();

        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("XamlBuildTask", "15.0.0.0")]
        public ContactEntity() {
            this.InitializeComponent();
        }
        
        public System.Activities.InOutArgument<System.Collections.Generic.List<Mercury.Server.Workflows.WorkflowStep>> WorkflowSteps {
            get {
                return this._WorkflowSteps;
            }
            set {
                this._WorkflowSteps = value;
            }
        }
        
        public System.Activities.InArgument<Mercury.Server.Core.Entity.Entity> Entity {
            get {
                return this._Entity;
            }
            set {
                this._Entity = value;
            }
        }
        
        public System.Activities.InArgument<bool> AllowEditContactDateTime {
            get {
                return this._AllowEditContactDateTime;
            }
            set {
                this._AllowEditContactDateTime = value;
            }
        }
        
        public System.Activities.InArgument<bool> AllowEditRegarding {
            get {
                return this._AllowEditRegarding;
            }
            set {
                this._AllowEditRegarding = value;
            }
        }
        
        public System.Activities.InArgument<string> RegardingMessage {
            get {
                return this._RegardingMessage;
            }
            set {
                this._RegardingMessage = value;
            }
        }
        
        public System.Activities.InArgument<string> IntroductionScript {
            get {
                return this._IntroductionScript;
            }
            set {
                this._IntroductionScript = value;
            }
        }
        
        public System.Activities.InArgument<bool> AllowCancel {
            get {
                return this._AllowCancel;
            }
            set {
                this._AllowCancel = value;
            }
        }
        
        public System.Activities.InOutArgument<Mercury.Server.Workflows.WorkflowManager4> WorkflowManager {
            get {
                return this._WorkflowManager;
            }
            set {
                this._WorkflowManager = value;
            }
        }
        
        public System.Activities.InArgument<bool> AutoSaveContact {
            get {
                return this._AutoSaveContact;
            }
            set {
                this._AutoSaveContact = value;
            }
        }
        
        public System.Activities.InOutArgument<int> ContactAttempts {
            get {
                return this._ContactAttempts;
            }
            set {
                this._ContactAttempts = value;
            }
        }
        
        public System.Activities.OutArgument<bool> ContactSuccessful {
            get {
                return this._ContactSuccessful;
            }
            set {
                this._ContactSuccessful = value;
            }
        }
        
        public System.Activities.OutArgument<Mercury.Server.Core.Enumerations.ContactOutcome> ContactOutcome {
            get {
                return this._ContactOutcome;
            }
            set {
                this._ContactOutcome = value;
            }
        }
        
        public System.Activities.OutArgument<bool> ActivityCanceled {
            get {
                return this._ActivityCanceled;
            }
            set {
                this._ActivityCanceled = value;
            }
        }
        
        public System.Activities.InArgument<long> WorkQueueItemId {
            get {
                return this._WorkQueueItemId;
            }
            set {
                this._WorkQueueItemId = value;
            }
        }
        
        public System.Activities.OutArgument<Mercury.Server.Core.Entity.EntityContact> EntityContact {
            get {
                return this._EntityContact;
            }
            set {
                this._EntityContact = value;
            }
        }
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("XamlBuildTask", "15.0.0.0")]
        public void InitializeComponent() {
            if ((this._contentLoaded == true)) {
                return;
            }
            this._contentLoaded = true;
            bool isInitialized = false;
            this.BeforeInitializeComponent(ref isInitialized);
            if ((isInitialized == true)) {
                this.AfterInitializeComponent();
                return;
            }
            string resourceName = this.FindResource();
            System.IO.Stream initializeXaml = typeof(ContactEntity).Assembly.GetManifestResourceStream(resourceName);
            System.Xml.XmlReader xmlReader = null;
            System.Xaml.XamlReader reader = null;
            System.Xaml.XamlObjectWriter objectWriter = null;
            try {
                System.Xaml.XamlSchemaContext schemaContext = XamlStaticHelperNamespace._XamlStaticHelper.SchemaContext;
                xmlReader = System.Xml.XmlReader.Create(initializeXaml);
                System.Xaml.XamlXmlReaderSettings readerSettings = new System.Xaml.XamlXmlReaderSettings();
                readerSettings.LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                readerSettings.AllowProtectedMembersOnRoot = true;
                reader = new System.Xaml.XamlXmlReader(xmlReader, schemaContext, readerSettings);
                System.Xaml.XamlObjectWriterSettings writerSettings = new System.Xaml.XamlObjectWriterSettings();
                writerSettings.RootObjectInstance = this;
                writerSettings.AccessLevel = System.Xaml.Permissions.XamlAccessLevel.PrivateAccessTo(typeof(ContactEntity));
                objectWriter = new System.Xaml.XamlObjectWriter(schemaContext, writerSettings);
                System.Xaml.XamlServices.Transform(reader, objectWriter);
            }
            finally {
                if ((xmlReader != null)) {
                    ((System.IDisposable)(xmlReader)).Dispose();
                }
                if ((reader != null)) {
                    ((System.IDisposable)(reader)).Dispose();
                }
                if ((objectWriter != null)) {
                    ((System.IDisposable)(objectWriter)).Dispose();
                }
            }
            this.AfterInitializeComponent();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("XamlBuildTask", "15.0.0.0")]
        private string FindResource() {
            string[] resources = typeof(ContactEntity).Assembly.GetManifestResourceNames();
            for (int i = 0; (i < resources.Length); i = (i + 1)) {
                string resource = resources[i];
                if ((resource.Contains(".ContactEntity.g.xaml") || resource.Equals("ContactEntity.g.xaml"))) {
                    return resource;
                }
            }
            throw new System.InvalidOperationException("Resource not found.");
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033")]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("XamlBuildTask", "15.0.0.0")]
        void System.ComponentModel.ISupportInitialize.BeginInit() {
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033")]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("XamlBuildTask", "15.0.0.0")]
        void System.ComponentModel.ISupportInitialize.EndInit() {
            this.InitializeComponent();
        }
    }
}