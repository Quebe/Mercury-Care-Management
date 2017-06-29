using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Sponsor {

    [Serializable]
    public class Sponsor : CoreObject {
        
        #region Private Properties

        private Int64 entityId;

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }

        
        public Int64 EntityId { get { return entityId; } }

        public Entity.Entity Entity { get { return application.EntityGet (entityId, true); } }
       
        #endregion


        #region Constructors

        public Sponsor (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Sponsor (Application applicationReference, Server.Application.Sponsor serverSponsor) {

            BaseConstructor (applicationReference, serverSponsor);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Sponsor serverSponsor) {

            base.BaseConstructor (applicationReference, serverSponsor);


            entityId = serverSponsor.EntityId;


            return;

        }

        #endregion

    }

}
