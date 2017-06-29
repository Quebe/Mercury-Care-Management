using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Action {

    [Serializable]
    public class Action : CoreObject {

        #region Private Properties

        private String describingParameterName = String.Empty;

        private Dictionary<String, Server.Application.ActionParameter> actionParameters = new Dictionary<String, Server.Application.ActionParameter> ();

        #endregion


        #region Public Properties

        public String DescribingParameterName { get { return describingParameterName; } set { describingParameterName = value; } }

        public Dictionary<String, Server.Application.ActionParameter> ActionParameters { get { return actionParameters; } set { actionParameters = value; } }

        

        public override String Description {

            get {

                String description = String.Empty;

                if (Id == 0) { description = "** No Action Assigned"; }

                else {

                    description = Name;

                    if (actionParameters.ContainsKey (describingParameterName)) {

                        if (!String.IsNullOrEmpty (actionParameters[describingParameterName].ValueDescription)) {

                            description = description + " (" + actionParameters[describingParameterName].ValueDescription + ")";

                        }

                        else { description = description + " (Not Assigned)"; }

                    }

                }

                return description;

            }

        }

        #endregion
        

        #region Constructors

        public Action (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Action (Application applicationReference, Server.Application.Action serverAction) {

            BaseConstructor (applicationReference, serverAction);


            describingParameterName = serverAction.DescribingParameterName;

            
            // COPY, DO NOT REFERENCE

            actionParameters = new Dictionary<String, Server.Application.ActionParameter> ();

            foreach (String currentParameterName in serverAction.ActionParameters.Keys) {

                actionParameters.Add (currentParameterName, application.CopyActionParameter (serverAction.ActionParameters[currentParameterName]));

            }
            
            return;
            
        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Action serverAction) {

            base.MapToServerObject ((Server.Application.CoreObject)serverAction);


            serverAction.DescribingParameterName = describingParameterName;


            // COPY DO NOT REFERENCE

            serverAction.ActionParameters = new Dictionary<String, Server.Application.ActionParameter> ();

            foreach (String currentParameterName in actionParameters.Keys) {

                serverAction.ActionParameters.Add (currentParameterName, application.CopyActionParameter (actionParameters[currentParameterName]));

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.Action serverAction = new Server.Application.Action ();

            MapToServerObject (serverAction);

            return serverAction;

        }

        public Action Copy () {

            Server.Application.Action serverAction = (Server.Application.Action)ToServerObject ();

            Action copiedAction = new Action (application, serverAction);

            return copiedAction;

        }

        public Boolean IsEqual (Action compareAction) {

            Boolean isEqual = base.IsEqual (compareAction);


            if (describingParameterName != compareAction.DescribingParameterName) { isEqual = false; }

            if (isEqual) {

                foreach (String parameterName in compareAction.ActionParameters.Keys) {

                    if (!actionParameters.ContainsKey (parameterName)) { isEqual = false; }

                    else {

                        if (actionParameters[parameterName].Value != compareAction.ActionParameters[parameterName].Value) { isEqual = false; }

                    }

                    if (!isEqual) { break; }

                }

                foreach (String parameterName in actionParameters.Keys) {

                    if (!compareAction.ActionParameters.ContainsKey (parameterName)) { isEqual = false; }

                    else {

                        if (actionParameters[parameterName].Value != compareAction.ActionParameters[parameterName].Value) { isEqual = false; }

                    }

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }

        #endregion 


        #region Public Methods

        public void RebindActionParameters (Application application) {

            Action baseAction = application.ActionById (Id);


            // copy over workflow

            if ((baseAction.Name == "Workflow") && (baseAction.ActionParameters.ContainsKey ("Workflow"))) {

                if ((Name == "Workflow") && (actionParameters.ContainsKey ("Workflow"))) {

                    baseAction.ActionParameters["Workflow"] = actionParameters["Workflow"];

                }

                if (!String.IsNullOrEmpty (baseAction.ActionParameters["Workflow"].Value)) {

                    Work.Workflow workflow = application.WorkflowGet (Convert.ToInt64 (baseAction.ActionParameters["Workflow"].Value), true);

                    foreach (String workflowParameterName in workflow.WorkflowParameters.Keys) {

                        baseAction.ActionParameters.Add (workflowParameterName, workflow.WorkflowParameters[workflowParameterName]);

                    }

                }

            }


            actionParameters.Clear ();

            actionParameters = baseAction.Copy ().ActionParameters;

            return;

        }

        #endregion 


    }

}
