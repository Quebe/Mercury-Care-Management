using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Reference {

    [Serializable]
    public class CorrespondenceContent : CoreObject {

        #region Private Properties

        private String correspondenceContentPath = String.Empty;

        private Int64 correspondenceId;

        private Int32 contentSequence;

        private Server.Application.CorrespondenceContentType contentType = Server.Application.CorrespondenceContentType.Report;

        private Int64 reportingServerId;

        private String attachmentBase64 = String.Empty;

        private String attachmentXpsBase64 = String.Empty;

        private Boolean isAttachmentCompressed = false;


        private Boolean isAttachmentLoaded = false; // RESERVED FOR FUTURE USE

        #endregion


        #region Public Properties

        public String CorrespondenceContentPath { get { return correspondenceContentPath; } set { correspondenceContentPath = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Path); } }

        public String ReportNameRaw {

            get {

                String reportName = CorrespondenceContentPath;

                reportName = reportName + @"/" + Name;

                reportName = reportName.Replace (@"//", @"/");

                return reportName;

            }

        }

        public String ReportName {

            get {

                String reportName = CorrespondenceContentPath.Replace ("%EnvironmentName%", application.Session.EnvironmentName);

                reportName = reportName + @"/" + Name.Replace ("%EnvironmentName%", application.Session.EnvironmentName);

                reportName = reportName.Replace (@"//", @"/");

                return reportName;

            }

            set {

                correspondenceContentPath = String.Empty;

                Int32 maxSegments = value.Split ('/').Length;

                Int32 currentSegmentIndex = 0;

                foreach (String currentSegment in value.Split ('/')) {

                    currentSegmentIndex = currentSegmentIndex + 1;

                    if (currentSegmentIndex < maxSegments) { 

                        correspondenceContentPath = correspondenceContentPath + currentSegment + "/";

                    }

                    else {  name = currentSegment; }

                }

            }

        }


        public Int64 CorrespondenceId { get { return correspondenceId; } set { correspondenceId = value; } }

        public Int32 ContentSequence { get { return contentSequence; } set { contentSequence = value; } }

        public Server.Application.CorrespondenceContentType ContentType { get { return contentType; } set { contentType = value; } }

        public Int64 ReportingServerId { get { return reportingServerId; } set { reportingServerId = value; } }

        public String AttachmentBase64 { get { return attachmentBase64; } set { attachmentBase64 = value; } }

        public System.IO.MemoryStream Attachment { 
            
            get {

                if (String.IsNullOrWhiteSpace (AttachmentBase64)) { return new System.IO.MemoryStream (); }
                
                return new System.IO.MemoryStream (Convert.FromBase64String (AttachmentBase64)); 
            
            }

            set { AttachmentBase64 = Convert.ToBase64String (value.ToArray ()); }
        
        }

        public String AttachmentXpsBase64 { get { return attachmentXpsBase64; } set { attachmentXpsBase64 = value; } }

        public System.IO.MemoryStream AttachmentXps {

            get {

                if (String.IsNullOrWhiteSpace (AttachmentXpsBase64)) { return new System.IO.MemoryStream (); }

                return new System.IO.MemoryStream (Convert.FromBase64String (AttachmentXpsBase64));

            }

            set { AttachmentXpsBase64 = Convert.ToBase64String (value.ToArray ()); }

        }

        public Boolean IsAttachmentCompressed { get { return isAttachmentCompressed; } set { isAttachmentCompressed = value; } }
        
        internal bool IsAttachmentLoaded { get { return isAttachmentLoaded; } set { isAttachmentLoaded = value; } }

        #endregion

        
        #region Constructors

        public CorrespondenceContent (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CorrespondenceContent (Application applicationReference, Server.Application.CorrespondenceContent serverObject) {

            BaseConstructor (applicationReference, serverObject);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.CorrespondenceContent serverObject) {

            base.BaseConstructor (applicationReference, serverObject);


            correspondenceContentPath = serverObject.CorrespondenceContentPath;

            correspondenceId = serverObject.CorrespondenceId;

            contentSequence = serverObject.ContentSequence;

            contentType = serverObject.ContentType;


            reportingServerId = serverObject.ReportingServerId;

            attachmentBase64 = serverObject.AttachmentBase64;

            attachmentXpsBase64 = serverObject.AttachmentXpsBase64;

            isAttachmentCompressed = serverObject.IsAttachmentCompressed;

            
            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.CorrespondenceContent serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.CorrespondenceContentPath = correspondenceContentPath;

            serverObject.CorrespondenceId = CorrespondenceId;

            serverObject.ContentSequence = ContentSequence;

            serverObject.ContentType = ContentType;


            serverObject.ReportingServerId = ReportingServerId;

            serverObject.AttachmentBase64 = AttachmentBase64;

            serverObject.AttachmentXpsBase64 = AttachmentXpsBase64;

            serverObject.IsAttachmentCompressed = IsAttachmentCompressed;
            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.CorrespondenceContent serverCorrespondenceContent = new Server.Application.CorrespondenceContent ();

            MapToServerObject (serverCorrespondenceContent);

            return serverCorrespondenceContent;

        }

        public CorrespondenceContent Copy () {

            Server.Application.CorrespondenceContent serverCorrespondenceContent = (Server.Application.CorrespondenceContent)ToServerObject ();

            CorrespondenceContent copiedCorrespondenceContent = new CorrespondenceContent (application, serverCorrespondenceContent);

            return copiedCorrespondenceContent;

        }

        public Boolean IsEqual (CorrespondenceContent compareCorrespondenceContent) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareCorrespondenceContent);


            isEqual &= (ReportName == compareCorrespondenceContent.ReportName);

            isEqual &= (ContentType == compareCorrespondenceContent.ContentType);

            isEqual &= (ReportingServerId == compareCorrespondenceContent.ReportingServerId);

            isEqual &= (IsAttachmentCompressed == compareCorrespondenceContent.IsAttachmentCompressed);

            isEqual &= (attachmentBase64 == compareCorrespondenceContent.attachmentBase64);

            isEqual &= (attachmentXpsBase64 == compareCorrespondenceContent.attachmentXpsBase64);

            return isEqual;

        }

        #endregion
        
    }

}
