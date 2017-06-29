using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Services.Responses {

    [DataContract]
    public class ObjectSaveResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Success")]
        private Boolean success;

        [DataMember (Name = "Id")]
        private Int64 id;

        [DataMember (Name = "InstanceId")]
        private Guid instanceId;

        #endregion


        #region Public Properties

        public Boolean Success { get { return success; } set { success = value; } } // Property: Result

        public Int64 Id { get { return id; } set { id = value; } }

        public Guid InstanceId { get { return instanceId; } set { instanceId = value; } }

        #endregion


        #region Public Methods

        public override void SetException (Exception exception) {

            base.SetException (exception);

            success = false;

            return;

        }       

        #endregion

    }

}
