using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public {

    public class CommonFunctions {

        public static String SetValueInRange (String value, String allowedValues, String defaultValue) {
        
            String returnValue = String.Empty;

            String[] valueList = allowedValues.Split (';');

            for (Int32 currentIndex = 0; currentIndex < valueList.Length; currentIndex++) {

                if (value == valueList [currentIndex]) { 

                    returnValue = value;

                    break;

                }

            }

            if (String.IsNullOrEmpty (returnValue)) { returnValue = defaultValue; }

            return returnValue;

        }

        public static String SetValueMaxLength (String value, Int32 maxLength) {

            return value.Substring (0, (value.Length > maxLength) ? maxLength : value.Length);

        }

        public static void XmlProperties_AddProperty (System.Xml.XmlDocument propertiesDocument, String propertiesTagName, String propertyName, String propertyValue) {

            System.Xml.XmlNode rootNode = propertiesDocument.GetElementsByTagName (propertiesTagName)[0];

            System.Xml.XmlElement propertyNode;


            propertyNode = propertiesDocument.CreateElement ("Property");

            propertyNode.SetAttribute ("Name", propertyName);

            propertyNode.InnerText = propertyValue;

            rootNode.AppendChild (propertyNode);

            return;

        }

        public static void XmlProperties_AddProperty (System.Xml.XmlDocument propertiesDocument, String propertiesTagName, String propertyName, String propertyText, String propertyValue) {

            System.Xml.XmlNode rootNode = propertiesDocument.GetElementsByTagName (propertiesTagName)[0];

            System.Xml.XmlElement propertyNode;


            propertyNode = propertiesDocument.CreateElement ("Property");

            propertyNode.SetAttribute ("Name", propertyName);

            propertyNode.SetAttribute ("Text", propertyText);

            propertyNode.InnerText = propertyValue;

            rootNode.AppendChild (propertyNode);

            return;

        }

        public static String XmlProperties_ReadProperty (System.Xml.XmlDocument propertiesDocument, String propertyName) {

            System.Xml.XmlNode property;

            property = propertiesDocument.SelectSingleNode ("//Property[@Name='" + propertyName + "']");

            if (property != null) {

                return property.InnerText;

            }

            return String.Empty;

        }

        public static String XmlProperties_ReadProperty (System.Xml.XmlDocument propertiesDocument, String propertyName, String defaultValue) {

            System.Xml.XmlNode property;

            property = propertiesDocument.SelectSingleNode ("//Property[@Name='" + propertyName + "']");

            if (property != null) {

                return property.InnerText;

            }

            return defaultValue;

        }

        public static System.Xml.XmlElement XmlDocumentAppendPropertyNode (System.Xml.XmlDocument document, System.Xml.XmlElement parentNode, String propertyName, String propertyValue) {

            System.Xml.XmlElement propertyNode;

            propertyNode = document.CreateElement ("Property");

            propertyNode.SetAttribute ("Name", propertyName);

            propertyNode.InnerText = propertyValue;

            parentNode.AppendChild (propertyNode);

            return propertyNode;

        }

        public static System.Xml.XmlElement XmlDocumentAppendNode (System.Xml.XmlDocument document, System.Xml.XmlElement parentNode, String nodeName, String innerText) {

            System.Xml.XmlElement childNode;

            childNode = document.CreateElement (nodeName);

            childNode.InnerText = innerText;

            parentNode.AppendChild (childNode);

            return childNode;

        }
        
        public static String ObjectSidToString (Byte[] sidByteArray) {

            if (sidByteArray == null) { return String.Empty; }

            System.Security.Principal.SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier (sidByteArray, 0);

            return sid.ToString ();

        }

    }

}
