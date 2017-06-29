using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Request {


    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionRequestOpenImage")]
    public class OpenImageRequest : RequestBase {

        #region Private Properties

        [DataMember (Name = "ObjectType")]
        private String objectType = String.Empty;

        [DataMember (Name = "ObjectId")]
        private Int64 objectId = 0;

        [DataMember (Name = "Render")]
        private Boolean render = false;

        #endregion 
        

        #region Private Properties

        public String ObjectType { get { return objectType; } set { objectType = value; } }

        public Int64 ObjectId { get { return objectId; } set { objectId = value; } }

        public Boolean Render { get { return render; } set { render = value; } }

        #endregion 


        #region Constructors

        public OpenImageRequest (String forObjectType, Int64 forObjectId, Boolean forRender) {

            userInteractionType = Enumerations.UserInteractionType.OpenImage;

            objectType = forObjectType;

            objectId = forObjectId;

            render = forRender;

            return;

        }

        #endregion 


    }

}
