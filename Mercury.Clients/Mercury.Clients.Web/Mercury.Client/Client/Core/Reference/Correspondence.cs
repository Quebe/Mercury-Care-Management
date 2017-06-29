using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Reference {

    [Serializable]
    public class Correspondence : CoreConfigurationObject {

        #region Private Properties

        private Double version = 1.0;

        private Int64 formId;

        private Boolean storeImage = false;

        private SortedList<Int32, CorrespondenceContent> content = new SortedList<Int32, CorrespondenceContent> ();

        #endregion


        #region Public Properties

        public Double Version { get { return version; } set { version = value; } }

        public Int64 FormId { get { return formId; } set { formId = value; } }

        public Boolean StoreImage { get { return storeImage; } set { storeImage = value; } }

        public SortedList<Int32, CorrespondenceContent> Content { get { return content; } set { content = value; } } 

        #endregion 
        

        #region Constructors

        public Correspondence (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Correspondence (Application applicationReference, Server.Application.Correspondence serverCorrespondence) {

            BaseConstructor (applicationReference, serverCorrespondence);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Correspondence serverObject) {

            base.BaseConstructor (applicationReference, serverObject);
            

            version = serverObject.Version;

            formId = serverObject.FormId;

            storeImage = serverObject.StoreImage;



            Int32 contentIndex = 0;

            foreach (Server.Application.CorrespondenceContent currentServerCorrespondenceContent in serverObject.Content.Values) {

                contentIndex = contentIndex + 1;

                CorrespondenceContent correspondenceContent = new CorrespondenceContent (application, currentServerCorrespondenceContent);

                correspondenceContent.CorrespondenceId = id;

                correspondenceContent.ContentSequence = contentIndex;

                content.Add (contentIndex, correspondenceContent);

            }


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Correspondence serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.Version = version;

            serverObject.FormId = formId;

            serverObject.StoreImage = storeImage;


            serverObject.Content = new Dictionary<Int32, Server.Application.CorrespondenceContent> ();

            Int32 contentIndex = 0;

            foreach (CorrespondenceContent currentContent in content.Values) {

                contentIndex = contentIndex + 1;

                Server.Application.CorrespondenceContent correspondenceContent = (Server.Application.CorrespondenceContent)currentContent.ToServerObject ();

                correspondenceContent.CorrespondenceId = id;

                correspondenceContent.ContentSequence = contentIndex;

                serverObject.Content.Add (contentIndex, correspondenceContent);

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.Correspondence serverCorrespondence = new Server.Application.Correspondence ();

            MapToServerObject (serverCorrespondence);

            return serverCorrespondence;

        }

        public Correspondence Copy () {

            Server.Application.Correspondence serverCorrespondence = (Server.Application.Correspondence)ToServerObject ();

            Correspondence copiedCorrespondence = new Correspondence (application, serverCorrespondence);

            return copiedCorrespondence;

        }

        public Boolean IsEqual (Correspondence compareCorrespondence) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareCorrespondence);


            isEqual &= (version == compareCorrespondence.Version);

            isEqual &= (formId == compareCorrespondence.FormId);

            isEqual &= (storeImage == compareCorrespondence.StoreImage);

            isEqual &= (content.Count == compareCorrespondence.Content.Count);

            if (isEqual) {

                foreach (Int32 currentSequence in content.Keys) {

                    isEqual &= (content[currentSequence].IsEqual (compareCorrespondence.content[currentSequence]));

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }

        #endregion


        #region Public Methods

        public void LoadContentAttachments () {

            foreach (CorrespondenceContent currentContent in content.Values) {

                CorrespondenceContent loadedContent = application.CorrespondenceContentGet (currentContent.Id, false);

                if (loadedContent != null) { currentContent.AttachmentBase64 = loadedContent.AttachmentBase64; }

            }

            return;

        }

        public Boolean ContentExists (CorrespondenceContent correspondenceContent) {

            Boolean exists = false;

            foreach (Int32 currentSequence in content.Keys) {

                exists = content[currentSequence].IsEqual (correspondenceContent);

                if (exists) { break; }

            }

            return exists;

        }

        public void AppendContent (CorrespondenceContent correspondenceContent) {

            if (!ContentExists (correspondenceContent)) {

                correspondenceContent.CorrespondenceId = Id;

                correspondenceContent.ContentSequence = content.Keys.Count + 1;

                content.Add (correspondenceContent.ContentSequence, correspondenceContent);

            }

            return;

        }

        #endregion
    }

}
