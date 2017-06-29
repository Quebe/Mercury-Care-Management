using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.WebService {

    [DataContract (Name = "WebServiceHostBindingConfiguration")]
    public class BindingConfiguration {

        #region Private Properties

        [DataMember (Name = "BindingName")]
        private String bindingName;

        [DataMember (Name = "BindingType")]
        private Enumerations.WebServiceBindingType bindingType = Enumerations.WebServiceBindingType.BasicHttpBinding;

        [DataMember (Name = "Protocol")]
        private String protocol = "http";


        // BINDINGS

        [DataMember (Name = "SecurityMode")]
        private System.ServiceModel.BasicHttpSecurityMode securityMode = System.ServiceModel.BasicHttpSecurityMode.None;

        [DataMember (Name = "TransportCredentialType")]
        private System.ServiceModel.HttpClientCredentialType transportCredentialType = System.ServiceModel.HttpClientCredentialType.Basic;

        [DataMember (Name = "MessageCredentialType")]
        private System.ServiceModel.MessageCredentialType messageCredentialType = System.ServiceModel.MessageCredentialType.UserName;

        [DataMember (Name = "ProtectionLevel")]
        private System.Net.Security.ProtectionLevel protectionLevel = System.Net.Security.ProtectionLevel.None;


        // BINDING TIMEOUTS

        [DataMember (Name = "TimeoutOpen")]
        private TimeSpan timeoutOpen = new TimeSpan (0, 1, 0);

        [DataMember (Name = "TimeoutClose")]
        private TimeSpan timeoutClose = new TimeSpan (0, 1, 0);

        [DataMember (Name = "TimeoutSend")]
        private TimeSpan timeoutSend = new TimeSpan (0, 5, 0);

        [DataMember (Name = "TimeoutReceive")]
        private TimeSpan timeoutReceive = new TimeSpan (0, 5, 0);


        // BUFFER

        [DataMember (Name = "BufferSizeMaximum")]
        private Int32 bufferSizeMaximum = Int32.MaxValue;

        [DataMember (Name = "BufferPoolSizeMaximum")]
        private Int32 bufferPoolSizeMaximum = Int32.MaxValue;

        [DataMember (Name = "ReceivedMessageSizeMaximum")]
        private Int32 receivedMessageSizeMaximum = Int32.MaxValue;


        // READER QUOTAS 

        [DataMember (Name = "ReaderQuotasDepthMaximum")]
        private Int32 readerQuotasDepthMaximum = Int32.MaxValue;

        [DataMember (Name = "ReaderQuotasStringContentLengthMaximum")]
        private Int32 readerQuotasStringContentLengthMaximum = Int32.MaxValue;

        [DataMember (Name = "ReaderQuotasArrayLengthMaximum")]
        private Int32 readerQuotasArrayLengthMaximum = Int32.MaxValue;

        [DataMember (Name = "ReaderQuotasNameTableCharCountMaximum")]
        private Int32 readerQuotasNameTableCharCountMaximum = Int32.MaxValue;

        [DataMember (Name = "ReaderQuotasBytesPerReadMaximum")]
        private Int32 readerQuotasBytesPerReadMaximum = Int32.MaxValue;

        #endregion


        #region Public Properties

        public String BindingName { get { return (String.IsNullOrEmpty (bindingName)) ? bindingType.ToString () : bindingName; } set { bindingName = value; } }

        public Enumerations.WebServiceBindingType BindingType { get { return bindingType; } set { bindingType = value; } }

        public String Protocol { get { return protocol; } set { protocol = value; } }


        public System.ServiceModel.BasicHttpSecurityMode SecurityMode {

            get { return securityMode; }

            set { securityMode = value; }

        }

        public System.ServiceModel.SecurityMode SecurityModeBase {

            get {

                System.ServiceModel.SecurityMode mode = System.ServiceModel.SecurityMode.None;

                switch (securityMode) {

                    case System.ServiceModel.BasicHttpSecurityMode.Message: mode = System.ServiceModel.SecurityMode.Message; break;

                    case System.ServiceModel.BasicHttpSecurityMode.Transport: mode = System.ServiceModel.SecurityMode.Transport; break;

                    case System.ServiceModel.BasicHttpSecurityMode.TransportWithMessageCredential: mode = System.ServiceModel.SecurityMode.TransportWithMessageCredential; break;

                }

                return mode;

            }

        }

        public System.ServiceModel.HttpClientCredentialType TransportCredentialType {

            get { return transportCredentialType; }

            set { transportCredentialType = value; }

        }

        public System.ServiceModel.MessageCredentialType MessageCredentialType {

            get { return messageCredentialType; }

            set { messageCredentialType = value; }

        }

        public System.ServiceModel.BasicHttpMessageCredentialType BasicHttpMessageCredentialType {

            get {

                System.ServiceModel.BasicHttpMessageCredentialType credentialType = System.ServiceModel.BasicHttpMessageCredentialType.UserName;

                if (messageCredentialType == System.ServiceModel.MessageCredentialType.Certificate) {

                    credentialType = System.ServiceModel.BasicHttpMessageCredentialType.Certificate;

                }

                return credentialType;

            }

        }

        public System.Net.Security.ProtectionLevel ProtectionLevel {

            get { return protectionLevel; }

            set { protectionLevel = value; }

        }


        public TimeSpan TimeoutOpen { get { return timeoutOpen; } set { timeoutOpen = value; } }

        public TimeSpan TimeoutClose { get { return timeoutClose; } set { timeoutClose = value; } }

        public TimeSpan TimeoutSend { get { return timeoutSend; } set { timeoutSend = value; } }

        public TimeSpan TimeoutReceive { get { return timeoutReceive; } set { timeoutReceive = value; } }


        public Int32 BufferSizeMaximum { get { return bufferSizeMaximum; } set { bufferSizeMaximum = value; } }

        public Int32 BufferPoolSizeMaximum { get { return bufferPoolSizeMaximum; } set { bufferPoolSizeMaximum = value; } }

        public Int32 ReceivedMessageSizeMaximum { get { return receivedMessageSizeMaximum; } set { receivedMessageSizeMaximum = value; } }


        public Int32 ReaderQuotasDepthMaximum { get { return readerQuotasDepthMaximum; } set { readerQuotasDepthMaximum = value; } }

        public Int32 ReaderQuotasStringContentLengthMaximum { get { return readerQuotasStringContentLengthMaximum; } set { readerQuotasStringContentLengthMaximum = value; } }

        public Int32 ReaderQuotasArrayLengthMaximum { get { return readerQuotasArrayLengthMaximum; } set { readerQuotasArrayLengthMaximum = value; } }

        public Int32 ReaderQuotasNameTableCharCountMaximum { get { return readerQuotasNameTableCharCountMaximum; } set { readerQuotasNameTableCharCountMaximum = value; } }

        public Int32 ReaderQuotasBytesPerReadMaximum { get { return readerQuotasBytesPerReadMaximum; } set { readerQuotasBytesPerReadMaximum = value; } }


        public System.ServiceModel.Channels.Binding Binding {

            get {

                System.ServiceModel.Channels.Binding binding;


                switch (bindingType) {

                    case Enumerations.WebServiceBindingType.BasicHttpBinding:

                        #region BasicHttpBinding

                        System.ServiceModel.BasicHttpBinding basicHttpBinding = new System.ServiceModel.BasicHttpBinding ();

                        basicHttpBinding.Name = BindingName;

                        basicHttpBinding.Security.Mode = securityMode;


                        basicHttpBinding.Security.Transport.ClientCredentialType = transportCredentialType;

                        basicHttpBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;

                        basicHttpBinding.Security.Transport.Realm = String.Empty;

                        basicHttpBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType;

                        basicHttpBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;


                        SetBindingTimeouts (basicHttpBinding);


                        basicHttpBinding.MaxBufferSize = ((bufferSizeMaximum < Int16.MaxValue) ? Int16.MaxValue : bufferSizeMaximum);

                        basicHttpBinding.MaxBufferPoolSize = ((bufferPoolSizeMaximum < Int16.MaxValue) ? Int16.MaxValue : bufferPoolSizeMaximum);

                        basicHttpBinding.MaxReceivedMessageSize = ((receivedMessageSizeMaximum < Int16.MaxValue) ? Int16.MaxValue : receivedMessageSizeMaximum);


                        basicHttpBinding.ReaderQuotas.MaxDepth = ((readerQuotasDepthMaximum < Int16.MaxValue) ? Int16.MaxValue : readerQuotasDepthMaximum);

                        basicHttpBinding.ReaderQuotas.MaxStringContentLength = ((readerQuotasStringContentLengthMaximum < Int16.MaxValue) ? Int16.MaxValue : readerQuotasStringContentLengthMaximum);

                        basicHttpBinding.ReaderQuotas.MaxArrayLength = ((readerQuotasArrayLengthMaximum < Int16.MaxValue) ? Int16.MaxValue : readerQuotasArrayLengthMaximum);

                        basicHttpBinding.ReaderQuotas.MaxNameTableCharCount = ((readerQuotasNameTableCharCountMaximum < Int16.MaxValue) ? Int16.MaxValue : readerQuotasNameTableCharCountMaximum);

                        basicHttpBinding.ReaderQuotas.MaxBytesPerRead = ((readerQuotasBytesPerReadMaximum < Int16.MaxValue) ? Int16.MaxValue : readerQuotasBytesPerReadMaximum);


                        binding = basicHttpBinding;

                        #endregion

                        break;

                    case Enumerations.WebServiceBindingType.WsHttpBinding:

                        #region WSHttpBinding

                        System.ServiceModel.WSHttpBinding wsHttpBinding = new System.ServiceModel.WSHttpBinding ();

                        wsHttpBinding.Name = BindingName;

                        wsHttpBinding.Security.Mode = SecurityModeBase;


                        wsHttpBinding.Security.Transport.ClientCredentialType = transportCredentialType;

                        wsHttpBinding.Security.Transport.ProxyCredentialType = System.ServiceModel.HttpProxyCredentialType.None;

                        wsHttpBinding.Security.Transport.Realm = String.Empty;

                        wsHttpBinding.Security.Message.ClientCredentialType = messageCredentialType;

                        wsHttpBinding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;


                        SetBindingTimeouts (wsHttpBinding);


                        // MaxBufferSize DOES NOT EXIST FOR WS

                        wsHttpBinding.MaxBufferPoolSize = bufferPoolSizeMaximum;

                        wsHttpBinding.MaxReceivedMessageSize = receivedMessageSizeMaximum;


                        wsHttpBinding.ReaderQuotas.MaxDepth = readerQuotasDepthMaximum;

                        wsHttpBinding.ReaderQuotas.MaxStringContentLength = readerQuotasStringContentLengthMaximum;

                        wsHttpBinding.ReaderQuotas.MaxArrayLength = readerQuotasArrayLengthMaximum;

                        wsHttpBinding.ReaderQuotas.MaxNameTableCharCount = readerQuotasNameTableCharCountMaximum;

                        wsHttpBinding.ReaderQuotas.MaxBytesPerRead = readerQuotasBytesPerReadMaximum;


                        binding = wsHttpBinding;

                        #endregion

                        break;

                    default:

                        throw new ApplicationException ("Binding Type [" + bindingType.ToString () + "] not implemented.");

                }

                return binding;

            }

        }

        #endregion


        #region Constructors

        public BindingConfiguration () { /* DO NOTHING */ }

        public BindingConfiguration (String forBindingName, Enumerations.WebServiceBindingType forBindingType) {

            bindingName = forBindingName;

            bindingType = forBindingType;

            return;

        }

        #endregion 

        
        #region XML Serialization

        public System.Xml.XmlDocument XmlEmptyDocument () {

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            xmlDocument.InsertBefore (xmlDeclaration, xmlDocument.DocumentElement);


            return xmlDocument;

        }

        public System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument coreObjectDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = coreObjectDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            System.Xml.XmlElement coreObjectNode = coreObjectDocument.CreateElement ("BindingConfiguration");

            System.Xml.XmlElement propertiesNode = coreObjectDocument.CreateElement ("Properties");



            #region Initialize Document Structure

            coreObjectDocument.InsertBefore (xmlDeclaration, coreObjectDocument.DocumentElement);

            coreObjectDocument.AppendChild (coreObjectNode);

            coreObjectNode.AppendChild (propertiesNode);


            // ROOT (OBJECT TYPE) [NAME/ID]

            // +-- PROPERTIES 

            // +-- +-- PROPERTY [NAME/VALUE]

            #endregion


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "BindingName", bindingName);

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "BindingType", ((Int32)bindingType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "BindingTypeName", bindingType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "Protocol", protocol);


            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "SecurityMode", ((Int32)securityMode).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "SecurityModeName", securityMode.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "TransportCredentialType", ((Int32)transportCredentialType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "TransportCredentialTypeName", transportCredentialType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "MessageCredentialType", ((Int32)messageCredentialType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "MessageCredentialTypeName", messageCredentialType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ProtectionLevel", ((Int32)protectionLevel).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ProtectionLevelName", protectionLevel.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "TimeoutOpen", timeoutOpen.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "TimeoutClose", timeoutClose.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "TimeoutSend", timeoutSend.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "TimeoutReceive", timeoutReceive.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "BufferSizeMaximum", bufferSizeMaximum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "BufferPoolSizeMaximum", bufferPoolSizeMaximum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ReceivedMessageSizeMaximum", receivedMessageSizeMaximum.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ReaderQuotasDepthMaximum", ReaderQuotasDepthMaximum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ReaderQuotasStringContentLengthMaximum", ReaderQuotasStringContentLengthMaximum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ReaderQuotasArrayLengthMaximum", ReaderQuotasArrayLengthMaximum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ReaderQuotasNameTableCharCountMaximum", ReaderQuotasNameTableCharCountMaximum.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ReaderQuotasBytesPerReadMaximum", ReaderQuotasBytesPerReadMaximum.ToString ());

            #endregion


            return coreObjectDocument;

        }

        public void XmlImport (System.Xml.XmlNode objectNode) {

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            if ("BindingConfiguration" != objectNode.Name) {

                exceptionMessage = "Mismatch Object Types during import. Expected 'BindingConfiguration', but found '" + objectNode.Name + "'.";

                throw new ApplicationException (exceptionMessage);

            }


            propertiesNode = objectNode.SelectSingleNode ("Properties");

            foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "BindingName": BindingName = currentPropertyNode.InnerText; break;

                    case "BindingType": BindingType = (Enumerations.WebServiceBindingType) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "Protocol": Protocol = currentPropertyNode.InnerText; break;


                    case "SecurityMode": SecurityMode = (System.ServiceModel.BasicHttpSecurityMode)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "TransportCredentialType": TransportCredentialType = (System.ServiceModel.HttpClientCredentialType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "MessageCredentialType": MessageCredentialType = (System.ServiceModel.MessageCredentialType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "ProtectionLevel": ProtectionLevel = (System.Net.Security.ProtectionLevel)Convert.ToInt32 (currentPropertyNode.InnerText); break;


                    case "TimeoutOpen": TimeoutOpen = TimeSpan.Parse (currentPropertyNode.InnerText); break;

                    case "TimeoutClose": TimeoutClose = TimeSpan.Parse (currentPropertyNode.InnerText); break;

                    case "TimeoutSend": TimeoutSend = TimeSpan.Parse (currentPropertyNode.InnerText); break;

                    case "TimeoutReceive": TimeoutReceive = TimeSpan.Parse (currentPropertyNode.InnerText); break;



                    case "BufferSizeMaximum": BufferSizeMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "BufferPoolSizeMaximum": BufferPoolSizeMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "ReceivedMessageSizeMaximum": ReceivedMessageSizeMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;


                    case "ReaderQuotasDepthMaximum": ReaderQuotasDepthMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "ReaderQuotasStringContentLengthMaximum": ReaderQuotasStringContentLengthMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "ReaderQuotasArrayLengthMaximum": ReaderQuotasArrayLengthMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "ReaderQuotasNameTableCharCountMaximum": ReaderQuotasNameTableCharCountMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "ReaderQuotasBytesPerReadMaximum": ReaderQuotasBytesPerReadMaximum = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                }

            }

            return;

        }

        #endregion

        
        #region Private Methods

        private void SetBindingTimeouts (System.ServiceModel.Channels.Binding binding) {

            binding.OpenTimeout = (timeoutOpen == TimeSpan.Zero) ? new TimeSpan (0, 1, 0) : timeoutOpen;

            binding.CloseTimeout = (timeoutClose == TimeSpan.Zero) ? new TimeSpan (0, 1, 0) : timeoutClose;

            binding.SendTimeout = (timeoutSend == TimeSpan.Zero) ? new TimeSpan (0, 5, 0) : timeoutSend;

            binding.ReceiveTimeout = (timeoutReceive == TimeSpan.Zero) ? new TimeSpan (0, 5, 0) : timeoutReceive;

            return;

        }

        #endregion

    }

}
