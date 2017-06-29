using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Reference {

    [Serializable]
    public class NoteType : CoreConfigurationObject {
        
        #region Constructors

        public NoteType (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public NoteType (Application applicationReference, Server.Application.NoteType serverNoteType) {

            BaseConstructor (applicationReference, serverNoteType);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.NoteType serverNoteType) {

            base.BaseConstructor (applicationReference, serverNoteType);
            
            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.NoteType serverNoteType) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverNoteType);


            return;

        }

        public override Object ToServerObject () {

            Server.Application.NoteType serverNoteType = new Server.Application.NoteType ();

            MapToServerObject (serverNoteType);

            return serverNoteType;

        }

        public NoteType Copy () {

            Server.Application.NoteType serverNoteType = (Server.Application.NoteType)ToServerObject ();

            NoteType copiedNoteType = new NoteType (application, serverNoteType);

            return copiedNoteType;
            
        }

        public Boolean IsEqual (NoteType compareNoteType) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareNoteType);


            return isEqual;

        }

        #endregion 

    }

}
