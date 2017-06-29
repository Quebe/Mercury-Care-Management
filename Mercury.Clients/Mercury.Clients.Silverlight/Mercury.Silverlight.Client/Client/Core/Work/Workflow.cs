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

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mercury.Client.Core.Work {

    public class Workflow : CoreConfigurationObject {

        #region Private Properties

        private Server.Application.EntityType entityType = Server.Application.EntityType.NotSpecified;

        private String actionVerb;


        private Server.Application.WorkflowFramework framework = Server.Application.WorkflowFramework.DotNet35;

        private String assemblyPath;

        private String assemblyName;

        private String assemblyClassName;


        private Dictionary<String, Server.Application.ActionParameter> workflowParameters = new Dictionary<String, Server.Application.ActionParameter> ();

        private ObservableCollection<Server.Application.WorkflowPermission> permissions = new ObservableCollection<Server.Application.WorkflowPermission> ();

        #endregion


        #region Public Properties

        public Server.Application.EntityType EntityType { get { return entityType; } set { entityType = value; } }

        public String ActionVerb { get { return actionVerb; } set { actionVerb = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Name); } }


        public Server.Application.WorkflowFramework Framework { get { return framework; } set { framework = value; } }

        public String AssemblyPath { get { return assemblyPath; } set { assemblyPath = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Path); } }

        public String AssemblyName { get { return assemblyName; } set { assemblyName = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Namespace); } }

        public String AssemblyClassName { get { return assemblyClassName; } set { assemblyClassName = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Namespace); } }


        public Dictionary<String, Server.Application.ActionParameter> WorkflowParameters { get { return workflowParameters; } set { workflowParameters = value; } }

        public ObservableCollection<Server.Application.WorkflowPermission> Permissions { get { return permissions; } set { permissions = value; } }

        #endregion


        #region Constructors

        public Workflow (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Workflow (Application applicationReference, Server.Application.Workflow serverWorkflow) {

            BaseConstructor (applicationReference, serverWorkflow);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Workflow serverWorkflow) {

            base.BaseConstructor (applicationReference, serverWorkflow);


            framework = serverWorkflow.Framework;


            entityType = serverWorkflow.EntityType;

            actionVerb = serverWorkflow.ActionVerb;

            assemblyPath = serverWorkflow.AssemblyPath;

            assemblyName = serverWorkflow.AssemblyName;

            assemblyClassName = serverWorkflow.AssemblyClassName;


            // MAKE COPY, NOT DIRECT ASSIGNMENT

            workflowParameters = new Dictionary<string, Server.Application.ActionParameter> ();

            foreach (String currentParameterName in serverWorkflow.WorkflowParameters.Keys) {

                workflowParameters.Add (currentParameterName, application.CopyActionParameter (serverWorkflow.WorkflowParameters[currentParameterName]));

            }

            // MAKE COPY, NOT DIRECT ASSIGNMENT

            permissions = new ObservableCollection<Server.Application.WorkflowPermission> ();

            foreach (Server.Application.WorkflowPermission currentPermission in serverWorkflow.Permissions) {

                permissions.Add (application.CopyWorkflowPermission (currentPermission));

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Workflow serverWorkflow) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverWorkflow);


            serverWorkflow.EntityType = entityType;

            serverWorkflow.ActionVerb = actionVerb;


            serverWorkflow.Framework = framework;

            serverWorkflow.AssemblyPath = assemblyPath;

            serverWorkflow.AssemblyName = assemblyName;

            serverWorkflow.AssemblyClassName = assemblyClassName;


            // COPY, DON'T REFERENCE

            serverWorkflow.WorkflowParameters = new Dictionary<String, Server.Application.ActionParameter> ();

            foreach (String currentParameterName in workflowParameters.Keys) {

                serverWorkflow.WorkflowParameters.Add (currentParameterName, application.CopyActionParameter (workflowParameters[currentParameterName]));

            }


            // COPY, DON'T REFERENCE

            ObservableCollection<Server.Application.WorkflowPermission> copiedPermissions = new ObservableCollection<Server.Application.WorkflowPermission> ();

            foreach (Server.Application.WorkflowPermission currentPermission in permissions) {

                copiedPermissions.Add (application.CopyWorkflowPermission (currentPermission));

            }

            serverWorkflow.Permissions = copiedPermissions;

            return;

        }

        public override Object ToServerObject () {

            Server.Application.Workflow serverWorkflow = new Server.Application.Workflow ();

            MapToServerObject (serverWorkflow);

            return serverWorkflow;

        }

        public Workflow Copy () {

            Server.Application.Workflow serverWorkflow = (Server.Application.Workflow)ToServerObject ();

            Workflow copiedWorkflow = new Workflow (application, serverWorkflow);

            return copiedWorkflow;

        }

        public Boolean IsEqual (Workflow compareWorkflow) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareWorkflow);


            isEqual &= (entityType == compareWorkflow.EntityType);

            isEqual &= (actionVerb == compareWorkflow.ActionVerb);


            isEqual &= (framework == compareWorkflow.Framework);

            isEqual &= (assemblyPath == compareWorkflow.AssemblyPath);

            isEqual &= (assemblyName == compareWorkflow.AssemblyName);

            isEqual &= (assemblyClassName == compareWorkflow.AssemblyClassName);


            isEqual &= (workflowParameters.Count == compareWorkflow.WorkflowParameters.Count);

            isEqual &= (permissions.Count == compareWorkflow.Permissions.Count);


            // COMPARE WORKFLOW PARAMETERS

            if (isEqual) {

                foreach (String currentParameterName in workflowParameters.Keys) {

                    if (compareWorkflow.WorkflowParameters.ContainsKey (currentParameterName)) {

                        isEqual &= (workflowParameters[currentParameterName] == compareWorkflow.WorkflowParameters[currentParameterName]);

                        if (!isEqual) { break; }

                    }

                    else { isEqual = false; break; }

                }

            }


            // COMPARE WORKFLOW PERMISSIONS

            if (isEqual) {

                foreach (Server.Application.WorkflowPermission currentPermission in permissions) {

                    Server.Application.WorkflowPermission comparePermission = Permission (currentPermission.WorkTeamId);

                    if (comparePermission == null) { isEqual = false; break; }


                    isEqual &= ((currentPermission.IsGranted == comparePermission.IsGranted) && (currentPermission.IsDenied == comparePermission.IsDenied));

                    if (!isEqual) { break; }

                }

            }


            return isEqual;

        }

        #endregion 
        
        #region Public Methods

        public Boolean ContainsPermissionWorkTeam (Int64 workTeamId) {

            Boolean permissionFound = false;

            foreach (Server.Application.WorkflowPermission currentPermission in permissions) {

                if (currentPermission.WorkTeamId == workTeamId) {

                    permissionFound = true;

                    break;

                }

            }

            return permissionFound;

        }

        public Server.Application.WorkflowPermission Permission (Int64 workTeamId) {

            Server.Application.WorkflowPermission permission = null;

            foreach (Server.Application.WorkflowPermission currentPermission in permissions) {

                if (currentPermission.WorkTeamId == workTeamId) {

                    permission = currentPermission;

                    break;

                }

            }

            return permission;

        }

        public Boolean SessionGrantedPermission {

            get {

                if (application == null) { return false; }


                Boolean isGrantedPermission = false;

                Boolean isDeniedPermission = false;


                // TODO: SILVERLIGHT UPDATE

                //foreach (Client.Core.Work.WorkTeam currentWorkTeam in application.WorkTeamsForSession (true)) {

                //    Server.Application.WorkflowPermission permission = Permission (currentWorkTeam.Id);

                //    if (permission != null) {

                //        isGrantedPermission |= permission.IsGranted;

                //        isDeniedPermission |= permission.IsDenied;

                //    }

                //}


                return ((isGrantedPermission) && (!isDeniedPermission));

            }

        }

        #endregion 

    }

}
