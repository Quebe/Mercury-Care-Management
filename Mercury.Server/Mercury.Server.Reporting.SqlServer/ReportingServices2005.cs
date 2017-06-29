using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Reporting.SqlServer {

    public class ReportingServices2005 : Public.Reporting.IReportingServer {

        #region Private Properties

        Reporting.SqlServer.ReportExecution2005.ReportExecutionServiceSoapClient reportExecutionClient;

        #endregion 


        #region Public Methods

        public Public.ImageStream Render (Public.WebService.WebServiceHostConfiguration webServiceHostConfiguration, String reportName, Dictionary<String, String> forReportParameters, String format, Dictionary<String, String> extendedProperties) {

            Public.ImageStream imageStream = new Public.ImageStream ();

            Boolean deviceInfoHumanReadablePdf = ((extendedProperties.ContainsKey ("DeviceInfoHumanReadablePdf")) ? Convert.ToBoolean (extendedProperties["DeviceInfoHumanReadablePdf"]) : false);


            #region Initialization 

            // ENABLE IMPERSONATION FROM THREAD IDENTITY

            // AppDomain.CurrentDomain.SetPrincipalPolicy (System.Security.Principal.PrincipalPolicy.WindowsPrincipal);

            // System.Security.Principal.WindowsImpersonationContext impersonationContext = null;

            // impersonationContext = ((System.Security.Principal.WindowsIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Impersonate ();


            // CONFIGURE REPORTING SERVICE HOST


            reportExecutionClient = new ReportExecution2005.ReportExecutionServiceSoapClient (webServiceHostConfiguration.BindingConfiguration.Binding, webServiceHostConfiguration.EndpointAddress);

            reportExecutionClient.ClientCredentials.Windows.AllowedImpersonationLevel = webServiceHostConfiguration.ClientCredentials.WindowsImpersonationLevel;

            reportExecutionClient.ClientCredentials.Windows.ClientCredential = webServiceHostConfiguration.ClientCredentials.Credentials;


            #region Old Connection Information 

            //Server.Public.WebService.WebServiceHostConfiguration serviceHostReporting = new Server.Public.WebService.WebServiceHostConfiguration (

            //    "qstestmcm001", 80, "ReportServer", "ReportExecution2005.asmx"

            //    );

            //serviceHostReporting.ClientCredentials.WindowsImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

            //serviceHostReporting.BindingConfiguration = new Server.Public.WebService.BindingConfiguration ("BasicHttpBinding", Server.Public.WebService.Enumerations.WebServiceBindingType.BasicHttpBinding);

            //serviceHostReporting.BindingConfiguration.SecurityMode = System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly;

            //serviceHostReporting.BindingConfiguration.TransportCredentialType = System.ServiceModel.HttpClientCredentialType.Ntlm;

            //serviceHostReporting.BindingConfiguration.MessageCredentialType = System.ServiceModel.MessageCredentialType.UserName;


            //System.ServiceModel.BasicHttpBinding binding = (System.ServiceModel.BasicHttpBinding)serviceHostReporting.BindingConfiguration.Binding;

            ////binding.AllowCookies = true;


            //reportExecutionClient = new ReportExecution2005.ReportExecutionServiceSoapClient (binding, serviceHostReporting.EndpointAddress);

            //reportExecutionClient.ClientCredentials.Windows.AllowedImpersonationLevel = serviceHostReporting.ClientCredentials.WindowsImpersonationLevel;

            //reportExecutionClient.ClientCredentials.Windows.ClientCredential = serviceHostReporting.ClientCredentials.Credentials;

            #endregion


            // OPEN UP THE MAX OBJECTS IN GRAPH TO MAXIMUM

            foreach (System.ServiceModel.Description.OperationDescription currentOperation in reportExecutionClient.Endpoint.Contract.Operations) {

                System.ServiceModel.Description.DataContractSerializerOperationBehavior dataContractSerializer =

                    (System.ServiceModel.Description.DataContractSerializerOperationBehavior)

                    currentOperation.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior> ();

                if (dataContractSerializer != null) {

                    dataContractSerializer.MaxItemsInObjectGraph = Int32.MaxValue;

                }

            }

            #endregion 


            #region Render Report

            SqlServer.ReportExecution2005.ExecutionHeader executionHeader;

            SqlServer.ReportExecution2005.ServerInfoHeader serverInfoHeader;

            SqlServer.ReportExecution2005.ExecutionInfo executionInfo;
            
            SqlServer.ReportExecution2005.ParameterValue[] parameters = new ReportExecution2005.ParameterValue[forReportParameters.Count];

            Byte[]renderedReport;

            String reportExtension;

            String reportMimeType;

            String reportEncoding;

            SqlServer.ReportExecution2005.Warning[] reportWarnings;

            String[] reportStreamIds;


            executionHeader = reportExecutionClient.LoadReport (null, reportName, null, out serverInfoHeader, out executionInfo);

            
            // COPY PARAMETERS 

            Int32 currentParameterIndex = 0;

            foreach (String currentParameterName in forReportParameters.Keys) {

                parameters[currentParameterIndex] = new ReportExecution2005.ParameterValue ();

                parameters[currentParameterIndex].Label = currentParameterName;

                parameters[currentParameterIndex].Name = currentParameterName;

                parameters[currentParameterIndex].Value = forReportParameters[currentParameterName];

                currentParameterIndex = currentParameterIndex + 1;

            }

            reportExecutionClient.SetExecutionParameters (executionHeader, null, parameters, "en-us", out executionInfo);


            // SET UP DEVICE INFORMATION 

            String deviceInfo = String.Empty;

            switch (format.ToLower ()) {

                case "pdf":

                    deviceInfo = (deviceInfoHumanReadablePdf) ? @"<DeviceInfo><HumanReadablePDF>True</HumanReadablePDF></DeviceInfo>" : String.Empty;

                    break;

            }


            serverInfoHeader = reportExecutionClient.Render (executionHeader, null, format, ((!String.IsNullOrEmpty (deviceInfo)) ? deviceInfo : null), out renderedReport, out reportExtension, out reportMimeType, out reportEncoding, out reportWarnings, out reportStreamIds);

            imageStream.Image = new System.IO.MemoryStream (renderedReport);

            imageStream.Name = reportName;

            imageStream.Extension = reportExtension;

            imageStream.MimeType = reportMimeType;

            #endregion 


            return imageStream;

        }

        #endregion 

    }

}
