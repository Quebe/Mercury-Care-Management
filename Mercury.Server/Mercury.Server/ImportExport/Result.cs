using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.ImportExport {

    [DataContract (Name = "ImportExportResult")]
    public class Result {

        #region Private Properties

        [DataMember (Name = "HasException")]
        private Boolean hasException = false;

        private Exception exception;

        [DataMember (Name = "Success")]
        private Boolean success;

        [DataMember (Name = "Id")]
        private Int64 id;

        [DataMember (Name = "InstanceId")]
        private Guid instanceId;

        [DataMember (Name = "ObjectType")]
        private String objectType;

        [DataMember (Name = "ObjectName")]
        private String objectName;

        #endregion


        #region Public Properties

        public Boolean HasException { get { return hasException; } set { hasException = value; } } // Property: HasException

        public Exception Exception { get { return exception; } set { exception = value; } } // Property: Exception


        public Boolean Success { get { return success; } set { success = value; } } // Property: Result

        public Int64 Id { get { return id; } set { id = value; } }

        public Guid InstanceId { get { return instanceId; } set { instanceId = value; } }


        public String ObjectType { get { return objectType; } set { objectType = value; } }

        public String ObjectName { get { return objectName; } set { objectName = value; } }

        #endregion


        #region Constructors

        public Result () { return; }

        public Result (String forObjectType, String forObjectName, Exception forException) {

            objectType = forObjectType;

            objectName = forObjectName;

            SetException (forException);

            return;

        }

        public Result (String forObjectType, String forObjectName, Int64 forId) {

            objectType = forObjectType;

            objectName = forObjectName;

            id = forId;

            return;

        }

        #endregion 


        #region Public Methods

        virtual public void SetException (Exception forException) {

            success = (forException == null);

            hasException = (forException != null);

            exception = forException;

            return;

        }

        #endregion

    }

}
