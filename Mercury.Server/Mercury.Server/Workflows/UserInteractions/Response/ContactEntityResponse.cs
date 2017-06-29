using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Workflows.UserInteractions.Response {

    [Serializable]
    [DataContract (Name = "WorkflowUserInteractionResponseContactEntity")]
    public class ContactEntityResponse : ResponseBase {

        #region Private Properties
    
        //[DataMember (Name = "Entity")]
        //private Server.Core.Entity.Entity entity = null;

        //[DataMember (Name = "RelatedEntity")]
        //private Server.Core.Entity.Entity relatedEntity = null;

        //[DataMember (Name = "EntityContactInformationId")]
        //private Int64 entityContactInformationId = 0;

        //[DataMember (Name = "Direction")]
        //private Server.Core.Enumerations.ContactDirection direction = Mercury.Server.Core.Enumerations.ContactDirection.Outbound;

        //[DataMember (Name = "ContactType")]
        //private Server.Core.Enumerations.EntityContactType contactType = Mercury.Server.Core.Enumerations.EntityContactType.NotSpecified;

        //[DataMember (Name = "ContactDate")]
        //private DateTime contactDate = DateTime.Now;

        //[DataMember (Name = "Successful")]
        //private Boolean successful = false;

        //[DataMember (Name = "ContactOutcome")]
        //private Server.Core.Enumerations.ContactOutcome contactOutcome = Mercury.Server.Core.Enumerations.ContactOutcome.NotSpecified;

        //[DataMember (Name = "Regarding")]
        //private String regarding = String.Empty;

        //[DataMember (Name = "Remarks")]
        //private String remarks = String.Empty;


        [DataMember (Name = "EntityContact")]
        private Core.Entity.EntityContact entityContact = null;

        #endregion


        #region Public Properties

        public override Enumerations.UserInteractionType UserInteractionType { get { return Enumerations.UserInteractionType.ContactEntity; } }


        //public Server.Core.Entity.Entity Entity { get { return entity; } set { entity = value; } }

        //public Server.Core.Entity.Entity RelatedEntity { get { return relatedEntity; } set { relatedEntity = value; } }

        //public Int64 EntityContactInformationId { get { return entityContactInformationId; } set { entityContactInformationId = value; } }

        //public Core.Enumerations.ContactDirection Direction { get { return direction; } set { direction = value; } }

        //public Core.Enumerations.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }

        //public DateTime ContactDate { get { return contactDate; } set { contactDate = value; } }

        //public Boolean Successful { get { return successful; } set { successful = value; } }

        //public Server.Core.Enumerations.ContactOutcome ContactOutcome { get { return contactOutcome; } set { contactOutcome = value; } }

        //public String Regarding { get { return regarding; } set { regarding = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        //public String Remarks { get { return remarks; } set { remarks = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Description); } }

        public Core.Entity.EntityContact EntityContact { get { return entityContact; } set { entityContact = value; } }

        #endregion


        #region Constructor

        public ContactEntityResponse () {

            base.userInteractionType = Enumerations.UserInteractionType.ContactEntity;

            return;

        }

        #endregion

    }

}
