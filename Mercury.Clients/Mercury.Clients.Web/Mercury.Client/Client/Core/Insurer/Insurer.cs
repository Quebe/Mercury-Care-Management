using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Insurer {

    [Serializable]
    public class Insurer : CoreObject {

        #region Private Properties

        private Int64 entityId;

        private String nationalPlanId;
        
        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }


        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public String NationalPlanId { get { return nationalPlanId; } set { nationalPlanId = value; } }


        public Entity.Entity Entity { get { return application.EntityGet (entityId, true); } }

        #endregion 


        #region Constructors

        public Insurer (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Insurer (Application applicationReference, Server.Application.Insurer serverInsurer) {

            BaseConstructor (applicationReference, serverInsurer);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Insurer serverInsurer) {

            base.BaseConstructor (applicationReference, serverInsurer);


            entityId = serverInsurer.EntityId;

            nationalPlanId = serverInsurer.NationalPlanId;


            return;

        }

        #endregion

    }

}
