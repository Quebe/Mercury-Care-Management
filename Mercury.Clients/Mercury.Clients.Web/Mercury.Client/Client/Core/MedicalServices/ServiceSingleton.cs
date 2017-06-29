using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.MedicalServices {

    [Serializable]
    public class ServiceSingleton : Service {

        #region Private Properties

        List<Definitions.ServiceSingletonDefinition> definitions = new List<Definitions.ServiceSingletonDefinition> ();

        #endregion


        #region Public Properties

        public List<Definitions.ServiceSingletonDefinition> Definitions { get { return definitions; } set { definitions = value; } }

        #endregion 


        #region Public Constuctor 

        public ServiceSingleton (Application applicationReference) {

            base.ServiceType = Mercury.Server.Application.MedicalServiceType.Singleton;

            base.BaseConstructor (applicationReference);
            
            return;

        }

        public ServiceSingleton (Application applicationReference, Mercury.Server.Application.ServiceSingleton serverSingleton) {

            base.ServiceType = Mercury.Server.Application.MedicalServiceType.Singleton;

            base.ServiceConstructor (applicationReference, serverSingleton);

            foreach (Mercury.Server.Application.ServiceSingletonDefinition definition in serverSingleton.Definitions) {

                definitions.Add (new Definitions.ServiceSingletonDefinition (applicationReference, definition));

            }

            return;

        }

        #endregion
        

        #region Public Methods

        public void AddDefinition (Definitions.ServiceSingletonDefinition singletonDefinition) {

            singletonDefinition.ServiceId = Id;

            definitions.Add (singletonDefinition);

            return;

        }

        public Definitions.ServiceSingletonDefinition Definition (Int32 index) {

            Core.MedicalServices.Definitions.ServiceSingletonDefinition definition = null;

            try {

                definition = definitions[index];

            }

            catch {

                // DO NOTHING

            }

            return definition;

        }

        public Boolean ContainsNdc (String forNdc) {

            Boolean containsNdc = false;


            foreach (MedicalServices.Definitions.ServiceSingletonDefinition currentDefinition in definitions) {

                if (currentDefinition.Enabled) {

                    containsNdc = currentDefinition.NdcCodeCriteria.Contains (forNdc);

                    if (containsNdc) { break; }

                }

            }

            return containsNdc;

        }

        public List<Mercury.Server.Application.MemberServiceDetailSingleton> Preview (Application application) {

            return application.MedicalServiceSingletonPreview ((Mercury.Server.Application.ServiceSingleton) ToServerObject ());

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ServiceSingleton serverServiceSingleton) {

            base.MapToServerObject ((Server.Application.Service)serverServiceSingleton);
            
            // COPY DO NOT REFERENCE

            serverServiceSingleton.Definitions = new Server.Application.ServiceSingletonDefinition[definitions.Count];

            Int32 currentDefinitionIndex = 0;

            foreach (MedicalServices.Definitions.ServiceSingletonDefinition currentSingletonDefinition in definitions) {

                serverServiceSingleton.Definitions[currentDefinitionIndex] = (Server.Application.ServiceSingletonDefinition) currentSingletonDefinition.ToServerObject ();

                currentDefinitionIndex = currentDefinitionIndex + 1;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.ServiceSingleton serverServiceSingleton = new Server.Application.ServiceSingleton ();

            MapToServerObject (serverServiceSingleton);

            return serverServiceSingleton;

        }

        public new ServiceSingleton Copy () {

            Server.Application.ServiceSingleton serverServiceSingleton = (Server.Application.ServiceSingleton)ToServerObject ();

            ServiceSingleton copiedServiceSingleton = new ServiceSingleton (application, serverServiceSingleton);

            return copiedServiceSingleton;

        }

        public Boolean IsEqual (ServiceSingleton compareService) {

            Boolean isEqual = base.IsEqual (compareService);


            isEqual = isEqual && (definitions.Count == compareService.Definitions.Count);

            if (isEqual) {

                for (Int32 currentDefinition = 0; currentDefinition < definitions.Count; currentDefinition++) {

                    isEqual = isEqual && definitions[currentDefinition].IsEqual (compareService.Definitions[currentDefinition]);

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }        

        #endregion 

    }

}
