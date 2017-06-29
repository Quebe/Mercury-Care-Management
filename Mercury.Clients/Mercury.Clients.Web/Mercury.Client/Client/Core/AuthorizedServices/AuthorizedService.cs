using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.AuthorizedServices {

    [Serializable]
    public class AuthorizedService : CoreConfigurationObject {

        #region Private Properties

        private List<AuthorizedServiceDefinition> definitions = new List<AuthorizedServiceDefinition> ();

        #endregion


        #region Public Properties

        public List<AuthorizedServiceDefinition> Definitions { get { return definitions; } set { definitions = value; } }

        #endregion


        #region Constructors

        public AuthorizedService (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public AuthorizedService (Application applicationReference, Server.Application.AuthorizedService serverAuthorizedService) {

            base.BaseConstructor (applicationReference, serverAuthorizedService);


            // COPY, DO NOT REFERENCE

            foreach (Server.Application.AuthorizedServiceDefinition currentDefinition in serverAuthorizedService.Definitions) {

                definitions.Add (new AuthorizedServiceDefinition (Application, currentDefinition));

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.AuthorizedService serverAuthorizedService) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverAuthorizedService);


            // COPY, DO NOT REFERENCE

            serverAuthorizedService.Definitions = new Server.Application.AuthorizedServiceDefinition[definitions.Count];

            Int32 definitionIndex = 0;

            foreach (AuthorizedServiceDefinition currentDefinition in definitions) {

                serverAuthorizedService.Definitions[definitionIndex] = (Server.Application.AuthorizedServiceDefinition)currentDefinition.ToServerObject ();

                definitionIndex = definitionIndex + 1;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.AuthorizedService serverAuthorizedService = new Server.Application.AuthorizedService ();

            MapToServerObject (serverAuthorizedService);

            return serverAuthorizedService;

        }

        public AuthorizedService Copy () {

            Server.Application.AuthorizedService serverAuthorizedService = (Server.Application.AuthorizedService)ToServerObject ();

            AuthorizedService copiedAuthorizedService = new AuthorizedService (application, serverAuthorizedService);

            return copiedAuthorizedService;

        }

        public Boolean IsEqual (AuthorizedService compareAuthorizedService) {

            Boolean isEqual = base.IsEqual (compareAuthorizedService);


            isEqual = isEqual && (definitions.Count == compareAuthorizedService.Definitions.Count);

            if (isEqual) {

                for (Int32 currentDefinition = 0; currentDefinition < definitions.Count; currentDefinition++) {

                    isEqual = isEqual && definitions[currentDefinition].IsEqual (compareAuthorizedService.Definitions[currentDefinition]);

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }

        #endregion 


        #region Public Methods

        public void AddDefinition (AuthorizedServiceDefinition authorizedServiceDefinition) {

            authorizedServiceDefinition.AuthorizedServiceId = Id;

            definitions.Add (authorizedServiceDefinition);

            return;

        }

        public AuthorizedServiceDefinition Definition (Int32 index) {

            AuthorizedServiceDefinition definition = null;

            try {

                definition = definitions[index];

            }

            catch {

                // DO NOTHING

            }

            return definition;

        }

        public List<Server.Application.MemberAuthorizedServiceDetail> Preview (Application application) {

            return application.AuthorizedServicePreview ((Mercury.Server.Application.AuthorizedService)ToServerObject ());

        }
        
        #endregion

    }

}
