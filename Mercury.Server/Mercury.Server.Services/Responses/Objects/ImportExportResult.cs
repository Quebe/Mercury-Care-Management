using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.Objects {

    public class ImportExportResult {

        #region Private Properties

        [DataMember (Name = "Id")]
        private Int64 id;

        [DataMember (Name = "InstanceId")]
        private Guid instanceId;


        [DataMember (Name = "ObjectType")]
        private String objectType;

        [DataMember (Name = "ObjectName")]
        private String objectName;


        [DataMember (Name = "Success")]
        private Boolean success;

        [DataMember (Name = "HasException")]
        private Boolean hasException = false;

        [DataMember (Name = "Exception")]
        private ServiceException exception;

        #endregion


        #region Public Properties

        public Boolean HasException { get { return hasException; } set { hasException = value; } } // Property: HasException

        public ServiceException Exception { get { return exception; } set { exception = value; } } // Property: Exception


        public Boolean Success { get { return success; } set { success = value; } } // Property: Result

        public Int64 Id { get { return id; } set { id = value; } }

        public Guid InstanceId { get { return instanceId; } set { instanceId = value; } }


        public String ObjectType { get { return objectType; } set { objectType = value; } }

        public String ObjectName { get { return objectName; } set { objectName = value; } }

        #endregion


        #region Constructors

        public ImportExportResult () { return; }

        public ImportExportResult (Server.ImportExport.Result forResult) {

            id = forResult.Id;

            instanceId = forResult.InstanceId;

            objectType = forResult.ObjectType;

            objectName = forResult.ObjectName;

            SetException (forResult.Exception);

            return;

        }

        #endregion 


        #region Public Methods

        virtual public void SetException (Exception forException) {

            success = (forException == null);

            hasException = (forException != null);

            exception = ((forException != null) ? new ServiceException (forException) : null);

            return;

        }

        #endregion

    }

}