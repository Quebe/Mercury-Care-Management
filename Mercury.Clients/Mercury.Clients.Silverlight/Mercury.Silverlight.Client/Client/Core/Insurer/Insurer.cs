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

namespace Mercury.Client.Core.Insurer {

    public class Insurer : CoreObject {

        #region Private Properties

        private Int64 entityId;

        private String nationalPlanId;


        private Entity.Entity entity = null;

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }


        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public String NationalPlanId { get { return nationalPlanId; } set { nationalPlanId = value; } }


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
