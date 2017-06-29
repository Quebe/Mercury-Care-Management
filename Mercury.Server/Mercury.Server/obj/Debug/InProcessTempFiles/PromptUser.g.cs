//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Mercury.Server.Workflows.Activities.PromptUser {
    
    
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class PromptUser : System.Activities.Activity, System.ComponentModel.ISupportInitialize {
        
        private bool _contentLoaded;
        
        private System.Activities.InOutArgument<Mercury.Server.Workflows.WorkflowManager4> _WorkflowManager;
        
        private System.Activities.InOutArgument<System.Collections.Generic.List<Mercury.Server.Workflows.WorkflowStep>> _WorkflowSteps;
        
        private System.Activities.OutArgument<bool> _ActivityCanceled;
        
        private System.Activities.InArgument<Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptType> _PromptType;
        
        private System.Activities.InArgument<Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptImage> _PromptImage;
        
        private System.Activities.InArgument<string> _PromptTitle;
        
        private System.Activities.InArgument<string> _PromptMessage;
        
        private System.Activities.InArgument<bool> _AllowCancel;
        
        private System.Activities.OutArgument<Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptButtonClicked> _ButtonClicked;
        
        private System.Activities.OutArgument<string> _InputText;
        
        private System.Activities.InOutArgument<System.Collections.Generic.List<Mercury.Server.Workflows.UserInteractions.Structures.PromptSelectionItem>> _PromptSelectionItems;
        
        private System.Activities.OutArgument<string> _SelectedValue;
        
        private System.Activities.OutArgument<string> _SelectedText;
        
        private System.Activities.InArgument<long> _WorkQueueItemId;
        
partial void BeforeInitializeComponent(ref bool isInitialized);

partial void AfterInitializeComponent();

        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("XamlBuildTask", "15.0.0.0")]
        public PromptUser() {
            this.InitializeComponent();
        }
        
        public System.Activities.InOutArgument<Mercury.Server.Workflows.WorkflowManager4> WorkflowManager {
            get {
                return this._WorkflowManager;
            }
            set {
                this._WorkflowManager = value;
            }
        }
        
        public System.Activities.InOutArgument<System.Collections.Generic.List<Mercury.Server.Workflows.WorkflowStep>> WorkflowSteps {
            get {
                return this._WorkflowSteps;
            }
            set {
                this._WorkflowSteps = value;
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
        
        public System.Activities.InArgument<Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptType> PromptType {
            get {
                return this._PromptType;
            }
            set {
                this._PromptType = value;
            }
        }
        
        public System.Activities.InArgument<Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptImage> PromptImage {
            get {
                return this._PromptImage;
            }
            set {
                this._PromptImage = value;
            }
        }
        
        public System.Activities.InArgument<string> PromptTitle {
            get {
                return this._PromptTitle;
            }
            set {
                this._PromptTitle = value;
            }
        }
        
        public System.Activities.InArgument<string> PromptMessage {
            get {
                return this._PromptMessage;
            }
            set {
                this._PromptMessage = value;
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
        
        public System.Activities.OutArgument<Mercury.Server.Workflows.UserInteractions.Enumerations.UserPromptButtonClicked> ButtonClicked {
            get {
                return this._ButtonClicked;
            }
            set {
                this._ButtonClicked = value;
            }
        }
        
        public System.Activities.OutArgument<string> InputText {
            get {
                return this._InputText;
            }
            set {
                this._InputText = value;
            }
        }
        
        public System.Activities.InOutArgument<System.Collections.Generic.List<Mercury.Server.Workflows.UserInteractions.Structures.PromptSelectionItem>> PromptSelectionItems {
            get {
                return this._PromptSelectionItems;
            }
            set {
                this._PromptSelectionItems = value;
            }
        }
        
        public System.Activities.OutArgument<string> SelectedValue {
            get {
                return this._SelectedValue;
            }
            set {
                this._SelectedValue = value;
            }
        }
        
        public System.Activities.OutArgument<string> SelectedText {
            get {
                return this._SelectedText;
            }
            set {
                this._SelectedText = value;
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
            System.IO.Stream initializeXaml = typeof(PromptUser).Assembly.GetManifestResourceStream(resourceName);
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
                writerSettings.AccessLevel = System.Xaml.Permissions.XamlAccessLevel.PrivateAccessTo(typeof(PromptUser));
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
            string[] resources = typeof(PromptUser).Assembly.GetManifestResourceNames();
            for (int i = 0; (i < resources.Length); i = (i + 1)) {
                string resource = resources[i];
                if ((resource.Contains(".PromptUser.g.xaml") || resource.Equals("PromptUser.g.xaml"))) {
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