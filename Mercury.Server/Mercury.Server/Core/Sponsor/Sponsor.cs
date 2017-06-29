using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Mercury.Server.Data;


namespace Mercury.Server.Core.Sponsor {

    [Serializable]
    [DataContract (Name = "Sponsor")]
    public class Sponsor : CoreObject {

        #region Private Properties

        [DataMember (Name = "EntityId")]
        private Int64 entityId;

        [DataMember (Name = "Entity")]
        private Core.Entity.Entity entity = new Entity.Entity (null);

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }

        
        public Int64 EntityId { get { return entityId; } }

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

        public Sponsor (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public Sponsor (Application applicationReference, Int64 forSponsorId) {

            BaseConstructor (applicationReference, forSponsorId);

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableSponsor;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.Sponsor_Select " + forId.ToString ());

            tableSponsor = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableSponsor.Rows.Count == 1) {

                MapDataFields (tableSponsor.Rows[0]);

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

            entity = application.EntityGet (entityId);

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
