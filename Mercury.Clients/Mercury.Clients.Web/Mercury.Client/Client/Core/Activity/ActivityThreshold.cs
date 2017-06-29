using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Activity {

    [Serializable]
    public class ActivityThreshold : CoreObject {

        #region Private Properties

        private Int32 relativeDateValue;

        private Server.Application.DateQualifier relativeDateQualifier = Server.Application.DateQualifier.Months;

        private Server.Application.ActivityStatus status = Server.Application.ActivityStatus.NotSpecified;

        private Core.Action.Action action = null;

        #endregion


        #region Public Properties

        public override String Name { get { return ((action != null) ? Server.CommonFunctions.SetValueMaxLength (action.Description, 60) : String.Empty); } }

        public override String Description { get { return ((action != null) ? action.Description : String.Empty); } }


        public Int32 RelativeDateValue { get { return relativeDateValue; } set { relativeDateValue = value; } }

        public Server.Application.DateQualifier RelativeDateQualifier { get { return relativeDateQualifier; } set { relativeDateQualifier = value; } }

        public Server.Application.ActivityStatus Status { get { return status; } set { status = value; } }

        public Core.Action.Action Action { get { return action; } set { action = value; } }

        #endregion


        #region Constructors
        
        public ActivityThreshold (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public ActivityThreshold (Application applicationReference, Mercury.Server.Application.ActivityThreshold serverObject) {

            BaseConstructor (applicationReference, serverObject);


            relativeDateValue = serverObject.RelativeDateValue;

            relativeDateQualifier = serverObject.RelativeDateQualifier;

            status = serverObject.Status;


            if (serverObject.Action != null) { action = new Action.Action (applicationReference, serverObject.Action); }


            return;

        }


        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ActivityThreshold serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.RelativeDateValue = relativeDateValue;

            serverObject.RelativeDateQualifier = relativeDateQualifier;

            serverObject.Status = status;


            if (action != null) { serverObject.Action = (Server.Application.Action)action.ToServerObject (); }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.ActivityThreshold serverObject = new Server.Application.ActivityThreshold ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public ActivityThreshold Copy () {

            Server.Application.ActivityThreshold serverObject = (Server.Application.ActivityThreshold)ToServerObject ();

            ActivityThreshold copiedActivityThreshold = new ActivityThreshold (application, serverObject);

            return copiedActivityThreshold;

        }

        public Boolean IsEqual (ActivityThreshold compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);


            isEqual &= (relativeDateValue == compareObject.RelativeDateValue);

            isEqual &= (relativeDateQualifier == compareObject.RelativeDateQualifier);

            isEqual &= (status == compareObject.Status);


            isEqual &= (((action != null) && (compareObject.Action != null)) || ((action == null) && (compareObject.Action == null)));

            if ((action != null) && (compareObject.Action != null)) { 

                isEqual &= action.IsEqual (compareObject.Action);

            }

            return isEqual;

        }

        #endregion 

    }

}
