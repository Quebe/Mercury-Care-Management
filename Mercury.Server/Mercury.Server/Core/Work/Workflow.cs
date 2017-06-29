using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "Workflow")]
    public class Workflow : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "Framework")]
        private Enumerations.WorkflowFramework framework = Enumerations.WorkflowFramework.DotNet35;


        [DataMember (Name = "EntityType")]
        private Mercury.Server.Core.Enumerations.EntityType entityType = Mercury.Server.Core.Enumerations.EntityType.NotSpecified;

        [DataMember (Name = "ActionVerb")]
        private String actionVerb;

        [DataMember (Name = "AssemblyPath")]
        private String assemblyPath;

        [DataMember (Name = "AssemblyName")]
        private String assemblyName;

        [DataMember (Name = "AssemblyClassName")]
        private String assemblyClassName;


        [DataMember (Name = "WorkflowParameters")]
        private Dictionary<String, Core.Action.ActionParameter> workflowParameters = new Dictionary<String, Core.Action.ActionParameter> ();

        [DataMember (Name = "Permissions")]
        private List<WorkflowPermission> permissions = new List<WorkflowPermission> ();

        #endregion


        #region Public Properties

        public Enumerations.WorkflowFramework Framework { get { return framework; } set { framework = value; } }

        
        public Mercury.Server.Core.Enumerations.EntityType EntityType { get { return entityType; } set { entityType = value; } }

        public String ActionVerb { get { return actionVerb; } set { actionVerb = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }

        public String AssemblyPath { get { return assemblyPath; } set { assemblyPath = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Path); } }

        public String AssemblyName { get { return assemblyName; } set { assemblyName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Namespace); } }

        public String AssemblyClassName { get { return assemblyClassName; } set { assemblyClassName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Namespace); } }


        public Dictionary<String, Core.Action.ActionParameter> WorkflowParameters { get { return workflowParameters; } set { workflowParameters = value; } }

        public List<WorkflowPermission> Permissions { get { return permissions; } set { permissions = value; } }


        public String AssemblyUrl { get { return (assemblyPath + "\\" + assemblyName).Replace ("\\\\", "\\"); } }

        public override Application Application {

            set {

                base.Application = value;

                foreach (Core.Action.ActionParameter currentWorkflowParameter in workflowParameters.Values) {

                    // SET APPLICATION IF EVER CONVERTED TO CORE OBJECT

                }

                foreach (WorkflowPermission currentWorkflowPermission in permissions) {

                    currentWorkflowPermission.Application = value;

                }

            }

        }

        #endregion


        #region Constructors

        public Workflow (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public Workflow (Application applicationReference, Int64 forWorkflowId) {

            BaseConstructor (applicationReference, forWorkflowId);

            return;

        }
        
        #endregion


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "Framework", ((Int32)framework).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "FrameworkName", (framework.ToString ()));


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EntityType", ((Int32)entityType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "EntityTypeName", entityType.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ActionVerb", actionVerb);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AssemblyPath", assemblyPath);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AssemblyName", assemblyName);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AssemblyClassName", assemblyClassName);

            #endregion


            #region Workflow Parameters

            System.Xml.XmlElement parametersRoot = CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WorkflowParameters", String.Empty);

            foreach (Action.ActionParameter currentParameter in workflowParameters.Values) {

                System.Xml.XmlElement parameterNode;


                parameterNode = document.CreateElement ("WorkflowParameter");

                parameterNode.SetAttribute ("Name", currentParameter.Name);

                parameterNode.SetAttribute ("DataType", ((Int32)currentParameter.DataType).ToString ());

                parameterNode.SetAttribute ("AllowFixedValue", (currentParameter.AllowFixedValue.ToString ()));

                parameterNode.SetAttribute ("Required", (currentParameter.Required.ToString ()));

                parameterNode.SetAttribute ("ValueType", ((Int32)currentParameter.ValueType).ToString ());

                parameterNode.SetAttribute ("Description", currentParameter.ValueDescription);

                parameterNode.InnerText = currentParameter.Value;

                parametersRoot.AppendChild (parameterNode);

            }

            #endregion

            
            #region Workflow Permissions

            System.Xml.XmlElement workflowPermissionsNode = document.CreateElement ("WorkflowPermissions");

            document.ChildNodes[1].AppendChild (workflowPermissionsNode);

            foreach (WorkflowPermission currentWorkflowPermission in permissions) {

                workflowPermissionsNode.AppendChild (document.ImportNode (currentWorkflowPermission.XmlSerialize ().ChildNodes[1], true));

            }

            #endregion


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            try {

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        case "Framework": Framework = (Enumerations.WorkflowFramework) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "EntityType": EntityType = (Core.Enumerations.EntityType)Convert.ToInt32 (currentPropertyNode.InnerText); break;


                        case "ActionVerb": ActionVerb = currentPropertyNode.InnerText; break;

                        case "AssemblyPath": AssemblyPath = currentPropertyNode.InnerText; break;

                        case "AssemblyName": AssemblyName = currentPropertyNode.InnerText; break;

                        case "AssemblyClassName": AssemblyClassName = currentPropertyNode.InnerText; break;


                        case "WorkflowParameters":

                            #region Workflow Parameters

                            foreach (System.Xml.XmlNode currentParameter in currentPropertyNode.ChildNodes) {

                                Core.Action.ActionParameter workflowParameter = new Action.ActionParameter ();

                                workflowParameter.Name = currentParameter.Attributes["Name"].Value;

                                workflowParameter.DataType = (Action.Enumerations.ActionParameterDataType)Convert.ToInt32 (currentParameter.Attributes["DataType"].Value);

                                workflowParameter.AllowFixedValue = Convert.ToBoolean (currentParameter.Attributes["AllowFixedValue"].Value);

                                workflowParameter.Required = Convert.ToBoolean (currentParameter.Attributes["Required"].Value);

                                workflowParameter.ValueType = (Core.Action.Enumerations.ActionParameterValueType)Convert.ToInt32 (currentParameter.Attributes["ValueType"].Value);

                                workflowParameter.ValueDescription = currentParameter.Attributes["Description"].Value;

                                if (!workflowParameters.ContainsKey (workflowParameter.Name)) {

                                    workflowParameters.Add (workflowParameter.Name, workflowParameter);

                                }

                            }

                            #endregion

                            break;


                    }

                }



                #region Workflow Permissions

                System.Xml.XmlNode workflowPermissionsNode = objectNode.SelectSingleNode ("WorkflowPermissions");

                foreach (System.Xml.XmlNode currentPermissionNode in workflowPermissionsNode.ChildNodes) {

                    WorkflowPermission workflowPermission = new WorkflowPermission (application);

                    response.AddRange (workflowPermission.XmlImport (currentPermissionNode));


                    if (workflowPermission.WorkTeamId != 0) {

                        workflowPermission.WorkflowId = 0;
                        
                        permissions.Add (workflowPermission);

                        response.Add (new ImportExport.Result (workflowPermission.ObjectType, workflowPermission.Name, 0));
                    
                    }

                    else { response.Add (new ImportExport.Result (workflowPermission.ObjectType, currentPermissionNode.Attributes["Name"].InnerText, new ApplicationException ("Unable to map Workflow Permission to local Work Team."))); }
                    

                }

                #endregion


            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 
        

        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            // VALIDATE ACTION VERB
            ActionVerb = ActionVerb;

            if ((EntityType != Mercury.Server.Core.Enumerations.EntityType.NotSpecified) && (String.IsNullOrEmpty (ActionVerb))) { validationResponse.Add ("Action Verb", "Required when Entity Specified."); }


            // VALIDATE ASSEMBLY PATH IS NOT EMPTY
            AssemblyPath = AssemblyPath;

            if (String.IsNullOrEmpty (AssemblyPath)) { validationResponse.Add ("Assembly Path", "Empty or Null."); }

            // VALIDATE ASSEMBLY NAME IS NOT EMPTY
            AssemblyName = AssemblyName;

            if (String.IsNullOrEmpty (AssemblyName)) { validationResponse.Add ("Assembly Name", "Empty or Null."); }

            // VALIDATE CLASS NAME IS NOT EMPTY
            AssemblyClassName = AssemblyClassName;

            if (String.IsNullOrEmpty (AssemblyClassName)) { validationResponse.Add ("Class Name", "Empty or Null."); }


            return validationResponse;

        }

        #endregion


        #region Database Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            System.Xml.XmlDocument parametersXml = new System.Xml.XmlDocument ();


            framework = (Enumerations.WorkflowFramework) Convert.ToInt32 (currentRow["Framework"]);

            entityType = (Core.Enumerations.EntityType) Convert.ToInt32 (currentRow ["EntityType"]);

            actionVerb = (String) currentRow["ActionVerb"];

            assemblyPath = (String) currentRow["AssemblyPath"];

            assemblyName = (String) currentRow["AssemblyName"];

            assemblyClassName = (String) currentRow["AssemblyClassName"];


            #region Workflow Parameters

            if (!String.IsNullOrEmpty ((String) currentRow["WorkflowParameters"])) {

                parametersXml.LoadXml ((String) currentRow["WorkflowParameters"]);

                workflowParameters = new Dictionary<string, Mercury.Server.Core.Action.ActionParameter> ();


                foreach (System.Xml.XmlNode currentParameterNode in parametersXml.SelectNodes ("//Parameter")) {

                    try {

                        String parameterName = currentParameterNode.Attributes["Name"].Value;

                        if (!workflowParameters.ContainsKey (parameterName)) {

                            workflowParameters.Add (parameterName, new Mercury.Server.Core.Action.ActionParameter ());

                            workflowParameters[parameterName].Name = parameterName;

                            if (currentParameterNode.Attributes["DataType"] != null) {

                                workflowParameters[parameterName].DataType = (Mercury.Server.Core.Action.Enumerations.ActionParameterDataType) Convert.ToInt32 (currentParameterNode.Attributes["DataType"].Value);

                            }

                            if (currentParameterNode.Attributes["AllowFixedValue"] != null) {

                                workflowParameters[parameterName].AllowFixedValue = Convert.ToBoolean (currentParameterNode.Attributes["AllowFixedValue"].Value);

                            }

                            if (currentParameterNode.Attributes["Required"] != null) {

                                workflowParameters[parameterName].Required = Convert.ToBoolean (currentParameterNode.Attributes["Required"].Value);

                            }

                            if (currentParameterNode.Attributes["ValueType"] != null) {

                                Int32 xmlValueType = 0;

                                Int32.TryParse (currentParameterNode.Attributes["ValueType"].Value, out xmlValueType);

                                workflowParameters[parameterName].ValueType = (Mercury.Server.Core.Action.Enumerations.ActionParameterValueType) xmlValueType;

                            }

                            if (currentParameterNode.Attributes["Description"] != null) {

                                workflowParameters[parameterName].ValueDescription = (String) currentParameterNode.Attributes["Description"].Value;

                            }

                        }

                    }

                    catch (Exception parameterException) {

                        System.Diagnostics.Trace.WriteLineIf (application.TraceSwitchWorkflow.TraceError, parameterException.Message);

                    }

                }  // foreach

            } // if

            #endregion

            

            System.Data.DataTable permissionTable = application.EnvironmentDatabase.SelectDataTable ("SELECT * FROM WorkflowPermission WHERE WorkflowId = " + Id.ToString (), 0);

            foreach (System.Data.DataRow currentPermissionRow in permissionTable.Rows) {

                WorkflowPermission permission = new WorkflowPermission (application);

                permission.MapDataFields (currentPermissionRow);

                permissions.Add (permission);

            }

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkflowManage)) { throw new ApplicationException ("Permission Denied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                modifiedAccountInfo = new Data.AuthorityAccountStamp (application.Session);


                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.Workflow_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (((Int32) framework).ToString () + ", ");

                sqlStatement.Append (((Int32) entityType).ToString () + ", ");

                sqlStatement.Append ("'" + actionVerb.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + assemblyPath.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + assemblyName.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + assemblyClassName.Replace ("'", "''") + "', ");


                if (workflowParameters == null) { workflowParameters = new Dictionary<string, Mercury.Server.Core.Action.ActionParameter> (); }

                Core.Action.Action action = new Mercury.Server.Core.Action.Action (application);

                action.ActionParameters = workflowParameters;

                sqlStatement.Append ("'" + action.ActionParametersXmlSqlParsedString + "', ");

                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                if (success) {

                    success &= application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM WorkflowPermission WHERE WorkflowId = " + Id.ToString (), 0);

                    if (permissions == null) { permissions = new List<WorkflowPermission> (); }

                    foreach (WorkflowPermission currentPermission in permissions) {

                        currentPermission.WorkflowId = Id;

                        currentPermission.ModifiedAccountInfo = modifiedAccountInfo;

                        currentPermission.Application = application;                       

                        success &= currentPermission.Save ();

                        if (!success) { throw new ApplicationException ("Unable to save Workflow Permission"); }

                    }

                }

                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion 


        #region Public Methods

        public WorkflowPermission Permission (Int64 workTeamId) {

            WorkflowPermission permission = null;

            foreach (WorkflowPermission currentPermission in permissions) {

                if (currentPermission.WorkTeamId == workTeamId) {

                    permission = currentPermission;

                    break;

                }

            }

            return permission;

        }

        public Boolean HasPermissionForSession () {

            Boolean hasPermission = false;

           
            foreach (WorkTeam currentWorkTeam in application.WorkTeamsForSession ()) {

                WorkflowPermission permission = Permission (currentWorkTeam.Id);

                if (permission != null) {

                    if (permission.IsDenied) {

                        hasPermission = false;

                        break;

                    }

                    else if (permission.IsGranted) { hasPermission = true; }

                }

            }


            return hasPermission;

        }

        #endregion

    }

}
