using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.MedicalServices {

    [Serializable]
    public class ServiceSet : Service {

        #region Private Properties

        private List<Definitions.ServiceSetDefinition> definitions = new List<Mercury.Client.Core.MedicalServices.Definitions.ServiceSetDefinition> ();

        #endregion 


        #region Public Properties

        public List<Definitions.ServiceSetDefinition> Definitions { get { return definitions; } set { definitions = value; } }

        #endregion


        #region Constructors
        
        public ServiceSet (Application applicationReference) {

            base.ServiceType = Mercury.Server.Application.MedicalServiceType.Set;

            base.BaseConstructor (applicationReference);
            
            return;

        }

        public ServiceSet (Application applicationReference, Mercury.Server.Application.ServiceSet serverSet) {

            base.ServiceType = Mercury.Server.Application.MedicalServiceType.Set;

            base.ServiceConstructor (applicationReference, serverSet);

            foreach (Mercury.Server.Application.ServiceSetDefinition definition in serverSet.Definitions) {

                definitions.Add (new Definitions.ServiceSetDefinition (applicationReference, definition));

            }

            return;

        }

        #endregion


        #region Public Methods

        public void AddDefinition (Definitions.ServiceSetDefinition setDefinition) {

            setDefinition.ServiceId = Id;

            definitions.Add (setDefinition);

            return;

        }

        public List<Mercury.Server.Application.MemberServiceDetailSet> Preview (Application application) {

            return new List<Server.Application.MemberServiceDetailSet> ();

            // return application.MedicalServiceSetPreview ((Mercury.Server.Application.ServiceSet) ToServerObject ());

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ServiceSet serverServiceSet) {

            base.MapToServerObject ((Server.Application.Service)serverServiceSet);

            // COPY DO NOT REFERENCE

            serverServiceSet.Definitions = new Server.Application.ServiceSetDefinition[definitions.Count];

            Int32 currentDefinitionIndex = 0;

            foreach (MedicalServices.Definitions.ServiceSetDefinition currentSetDefinition in definitions) {

                serverServiceSet.Definitions[currentDefinitionIndex] = (Server.Application.ServiceSetDefinition) currentSetDefinition.ToServerObject ();

                currentDefinitionIndex = currentDefinitionIndex + 1;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.ServiceSet serverServiceSet = new Server.Application.ServiceSet ();

            MapToServerObject (serverServiceSet);

            return serverServiceSet;

        }

        public new ServiceSet Copy () {

            Server.Application.ServiceSet serverServiceSet = (Server.Application.ServiceSet)ToServerObject ();

            ServiceSet copiedServiceSet = new ServiceSet (application, serverServiceSet);

            return copiedServiceSet;

        }

        public Boolean IsEqual (ServiceSet compareService) {

            Boolean isEqual = base.IsEqual (compareService);

            isEqual = isEqual && (this.definitions.Count == compareService.Definitions.Count);

            if (isEqual) {

                for (Int32 currentDefinition = 0; currentDefinition < definitions.Count; currentDefinition++) {

                    isEqual = isEqual && (this.definitions[currentDefinition].IsEqual (compareService.Definitions[currentDefinition]));

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }

        #endregion 

    }

}
