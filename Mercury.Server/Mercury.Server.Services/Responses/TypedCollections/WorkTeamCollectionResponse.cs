using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses.TypedCollections {

    [DataContract (Name = "WorkTeamCollectionResponse")]
    public class WorkTeamCollectionResponse : ResponseBase {

        #region Private Properties

        [DataMember (Name = "Collection")]
        private List<Server.Core.Work.WorkTeam> collection = new List<Server.Core.Work.WorkTeam> ();

        #endregion


        #region Public Properties

        public List<Server.Core.Work.WorkTeam> Collection { get { return collection; } set { collection = value; } } // Property: ResultList

        #endregion

    }

}