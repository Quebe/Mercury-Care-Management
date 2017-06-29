using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Faxing {

    [Serializable]
    public class FaxServer : Core.CoreConfigurationObject {

        #region Private Properties

        private String assemblyPath;

        private String assemblyName;

        private String assemblyClassName;


        private Server.Application.FaxServerConfiguration faxServerConfiguration;

        private Server.Application.WebServiceHostConfiguration webServiceHostConfiguration;

        #endregion


        #region Public Properties

        public String AssemblyPath { get { return assemblyPath; } set { assemblyPath = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Path); } }

        public String AssemblyName { get { return assemblyName; } set { assemblyName = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Namespace); } }

        public String AssemblyClassName { get { return assemblyClassName; } set { assemblyClassName = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Namespace); } }


        public Server.Application.FaxServerConfiguration FaxServerConfiguration {

            get {

                if (faxServerConfiguration == null) {

                    faxServerConfiguration = new Server.Application.FaxServerConfiguration ();

                    faxServerConfiguration.FaxUrl = String.Empty;

                    faxServerConfiguration.FaxQueueName = String.Empty;

                    faxServerConfiguration.MonitorInterval = 60;

                    faxServerConfiguration.MonitorTimeout = 600;

                    faxServerConfiguration.SenderEmailAddress = String.Empty;

                }

                return faxServerConfiguration;

            }

            set { faxServerConfiguration = value; }

        }

        public Server.Application.WebServiceHostConfiguration WebServiceHostConfiguration {

            get {

                if (webServiceHostConfiguration == null) {

                    webServiceHostConfiguration = new Server.Application.WebServiceHostConfiguration ();

                    webServiceHostConfiguration.BindingConfiguration = new Server.Application.WebServiceHostBindingConfiguration ();

                    webServiceHostConfiguration.ClientCredentials = new Server.Application.WebServiceHostClientCredentials ();

                }

                return webServiceHostConfiguration;

            }

            set { webServiceHostConfiguration = value; }

        }

        #endregion


        #region Constructors

        public FaxServer (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public FaxServer (Application applicationReference, Server.Application.FaxServer serverObject) {

            BaseConstructor (applicationReference, serverObject);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.FaxServer serverObject) {

            base.BaseConstructor (applicationReference, serverObject);


            assemblyPath = serverObject.AssemblyPath;

            assemblyName = serverObject.AssemblyName;

            assemblyClassName = serverObject.AssemblyClassName;


            faxServerConfiguration = applicationReference.CopyFaxServerConfiguration (serverObject.FaxServerConfiguration);

            webServiceHostConfiguration = applicationReference.CopyWebServiceHostConfiguration (serverObject.WebServiceHostConfiguration);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.FaxServer serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.AssemblyPath = assemblyPath;

            serverObject.AssemblyName = assemblyName;

            serverObject.AssemblyClassName = assemblyClassName;


            serverObject.FaxServerConfiguration = application.CopyFaxServerConfiguration (FaxServerConfiguration);

            serverObject.WebServiceHostConfiguration = application.CopyWebServiceHostConfiguration (WebServiceHostConfiguration);

            return;

        }

        public override Object ToServerObject () {

            Server.Application.FaxServer serverObject = new Server.Application.FaxServer ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public FaxServer Copy () {

            Server.Application.FaxServer serverObject = (Server.Application.FaxServer)ToServerObject ();

            FaxServer copiedObject = new FaxServer (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (FaxServer compareObject) {

            Boolean isEqual = base.IsEqual ((Core.CoreConfigurationObject)compareObject);


            isEqual &= (AssemblyPath == compareObject.AssemblyPath);

            isEqual &= (AssemblyName == compareObject.AssemblyName);

            isEqual &= (AssemblyClassName == compareObject.AssemblyClassName);


            if (isEqual) {

                isEqual &= (faxServerConfiguration.FaxUrl == compareObject.FaxServerConfiguration.FaxUrl);

                isEqual &= (faxServerConfiguration.FaxQueueName == compareObject.FaxServerConfiguration.FaxQueueName);

                isEqual &= (faxServerConfiguration.MonitorInterval == compareObject.FaxServerConfiguration.MonitorInterval);

                isEqual &= (faxServerConfiguration.MonitorTimeout == compareObject.FaxServerConfiguration.MonitorTimeout);

                isEqual &= (faxServerConfiguration.SenderEmailAddress == compareObject.FaxServerConfiguration.SenderEmailAddress);

            }


            if (isEqual) {

                isEqual &= (WebServiceHostConfiguration.Server == compareObject.WebServiceHostConfiguration.Server);

                isEqual &= (WebServiceHostConfiguration.Port == compareObject.WebServiceHostConfiguration.Port);

                isEqual &= (WebServiceHostConfiguration.ServicePath == compareObject.WebServiceHostConfiguration.ServicePath);

                isEqual &= (WebServiceHostConfiguration.ServiceName == compareObject.WebServiceHostConfiguration.ServiceName);

                if (isEqual) {

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.BindingName == compareObject.WebServiceHostConfiguration.BindingConfiguration.BindingName);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.BindingType == compareObject.WebServiceHostConfiguration.BindingConfiguration.BindingType);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.BufferPoolSizeMaximum == compareObject.WebServiceHostConfiguration.BindingConfiguration.BufferPoolSizeMaximum);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.BufferSizeMaximum == compareObject.WebServiceHostConfiguration.BindingConfiguration.BufferSizeMaximum);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.MessageCredentialType == compareObject.WebServiceHostConfiguration.BindingConfiguration.MessageCredentialType);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.ProtectionLevel == compareObject.WebServiceHostConfiguration.BindingConfiguration.ProtectionLevel);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.Protocol == compareObject.WebServiceHostConfiguration.BindingConfiguration.Protocol);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasArrayLengthMaximum == compareObject.WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasArrayLengthMaximum);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasBytesPerReadMaximum == compareObject.WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasBytesPerReadMaximum);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasDepthMaximum == compareObject.WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasDepthMaximum);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasNameTableCharCountMaximum == compareObject.WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasNameTableCharCountMaximum);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasStringContentLengthMaximum == compareObject.WebServiceHostConfiguration.BindingConfiguration.ReaderQuotasStringContentLengthMaximum);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.ReceivedMessageSizeMaximum == compareObject.WebServiceHostConfiguration.BindingConfiguration.ReceivedMessageSizeMaximum);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.SecurityMode == compareObject.WebServiceHostConfiguration.BindingConfiguration.SecurityMode);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.TimeoutClose == compareObject.WebServiceHostConfiguration.BindingConfiguration.TimeoutClose);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.TimeoutOpen == compareObject.WebServiceHostConfiguration.BindingConfiguration.TimeoutOpen);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.TimeoutReceive == compareObject.WebServiceHostConfiguration.BindingConfiguration.TimeoutReceive);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.TimeoutSend == compareObject.WebServiceHostConfiguration.BindingConfiguration.TimeoutSend);

                    isEqual &= (WebServiceHostConfiguration.BindingConfiguration.TransportCredentialType == compareObject.WebServiceHostConfiguration.BindingConfiguration.TransportCredentialType);

                }

                if (isEqual) {

                    isEqual &= (WebServiceHostConfiguration.ClientCredentials.UserName == compareObject.WebServiceHostConfiguration.ClientCredentials.UserName);

                    isEqual &= (WebServiceHostConfiguration.ClientCredentials.Password == compareObject.WebServiceHostConfiguration.ClientCredentials.Password);

                    isEqual &= (WebServiceHostConfiguration.ClientCredentials.Domain == compareObject.WebServiceHostConfiguration.ClientCredentials.Domain);

                    isEqual &= (WebServiceHostConfiguration.ClientCredentials.WindowsImpersonationLevel == compareObject.WebServiceHostConfiguration.ClientCredentials.WindowsImpersonationLevel);

                }

            }


            return isEqual;

        }

        #endregion

    }

}
