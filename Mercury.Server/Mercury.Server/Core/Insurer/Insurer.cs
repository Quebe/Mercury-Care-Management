using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Mercury.Server.Data;


namespace Mercury.Server.Core.Insurer {

    [Serializable]
    [DataContract (Name = "Insurer")]
    public class Insurer : CoreObject {

        #region Private Properties
        
        [DataMember (Name = "EntityId")]
        private Int64 entityId;

        [DataMember (Name = "NationalPlanId")]
        private String nationalPlanId;

        [DataMember (Name = "Entity")]
        private Entity.Entity entity = null;

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }


        public Int64 EntityId { get { return entityId; } }

        public String NationalPlanId {

            get { return nationalPlanId; }

            set { nationalPlanId = value.Substring (0, (value.Length > DataTypeConstants.UniqueId) ? DataTypeConstants.UniqueId : value.Length); }

        }


        public Entity.Entity Entity {

            get {

                if (entity != null) { return entity; }

                if (application == null) { return null; }

                if (entityId != 0) {

                    entity = application.EntityGet (entityId);

                }

                return entity;

            }

        }

        #endregion


        #region Constructors

        public Insurer (Application applicationReference) {

            BaseConstructor (applicationReference);

            entity = new Entity.Entity (base.application);
            
            return; 
        
        }

        public Insurer (Application applicationReference, Int64 forInsurerId) {

            BaseConstructor (applicationReference);

            entity = new Entity.Entity (base.application);

            if (!Load (forInsurerId)) {

                throw new ApplicationException ("Unable to load Insurer from the database for " + forInsurerId.ToString () + ".");

            }

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableInsurer;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.Insurer_Select " + forId.ToString ());

            tableInsurer = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableInsurer.Rows.Count == 1) {

                MapDataFields (tableInsurer.Rows[0]);

                entity = new Entity.Entity (base.application, entityId);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            entityId = (Int64) currentRow["EntityId"];

            nationalPlanId = (String) currentRow["NationalPlanId"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
