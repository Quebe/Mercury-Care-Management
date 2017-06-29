using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Sponsor {

    public class Sponsor : CoreObject {

        #region Private Properties

        private Int64 entityId;


        private Entity.Entity entity = null;

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }

        public Int64 EntityId { get { return entityId; } }


        public Entity.Entity Entity {

            get {

                if (entity == null) {

                    GlobalProgressBarShow ("Entity");

                    Application.EntityGet (entityId, true, EntityGetCompleted);

                }

                return entity;

            }

        }

        #endregion


        #region Property Data Binding Callbacks

        private void EntityGetCompleted (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            GlobalProgressBarHide ("Entity");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                entity = new Entity.Entity (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("Entity");

            }

            return;

        }

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
