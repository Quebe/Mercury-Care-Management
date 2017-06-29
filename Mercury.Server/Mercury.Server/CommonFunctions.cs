using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server {

    public class CommonFunctions {

        #region Property Functions

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

            if (value != null) { value.Trim (); } else { value = String.Empty; }

            return value.Substring (0, (value.Length > maxLength) ? maxLength : value.Length);

        }


        /// <summary>
        /// Returns a Proptery Value from an Object through Reflection. Supports multi-level heirachy for reflection.
        /// </summary>
        /// <param name="forObject">Object that contains requested property.</param>
        /// <param name="propertyName">Name of the property requested.</param>
        /// <returns>Value of property (as Object).</returns>
        /// <example>
        /// For a multi-level property "Entity.CurrentMailingAddress.Line1", the function parses
        /// the number of hierarchy objects by splitting on the '.' so that it becomes a recursive
        /// call to "Entity", "CurrentMailingAddress.Line1" and then "CurrentMailingAddress", "Line1".
        /// </example>

        public static Object GetPropertyValue (Object forObject, String propertyName) {

            if (forObject == null) { return null; }

            Object propertyValue = null;

            if (propertyName.Split ('.').Length > 1) {

                String firstPropertyName = propertyName.Split ('.')[0];

                Object propertyObject = forObject.GetType ().GetProperty (firstPropertyName).GetValue (forObject, null);

                String childPropertyName = propertyName.Substring (firstPropertyName.Length + 1, propertyName.Length - (firstPropertyName.Length + 1));

                propertyValue = GetPropertyValue (propertyObject, childPropertyName);

            }

            else {

                propertyValue = forObject.GetType ().GetProperty (propertyName).GetValue (forObject, null);

            }

            return propertyValue;

        }

        #endregion 


        #region XML Property Functions

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

        public static System.Xml.XmlElement XmlDocumentAppendPropertyNode (System.Xml.XmlDocument document, System.Xml.XmlNode parentNode, String propertyName, String propertyValue) {

            System.Xml.XmlElement propertyNode;

            propertyNode = document.CreateElement ("Property");

            propertyNode.SetAttribute ("Name", propertyName);

            propertyNode.InnerText = propertyValue;

            parentNode.AppendChild (propertyNode);

            return propertyNode;

        }

        public static void XmlReaderClone (System.Xml.XmlReader reader, System.Xml.XmlWriter writer) {

            if (reader == null) { throw new ArgumentNullException ("System.Xml.XmlReader"); }

            if (writer == null) { throw new ArgumentNullException ("System.Xml.XmlWriter"); }

            switch (reader.NodeType) {

                case System.Xml.XmlNodeType.Element:

                    writer.WriteStartElement (reader.Prefix, reader.LocalName, reader.NamespaceURI);

                    writer.WriteAttributes (reader, true);

                    if (reader.IsEmptyElement) { writer.WriteEndElement (); }

                    break;

                case System.Xml.XmlNodeType.Text:

                    writer.WriteString (reader.Value);

                    break;

                case System.Xml.XmlNodeType.Whitespace:

                case System.Xml.XmlNodeType.SignificantWhitespace:

                    writer.WriteWhitespace (reader.Value);

                    break;

                case System.Xml.XmlNodeType.CDATA:

                    writer.WriteCData (reader.Value);

                    break;

                case System.Xml.XmlNodeType.EntityReference:

                    writer.WriteEntityRef (reader.Name);

                    break;

                case System.Xml.XmlNodeType.XmlDeclaration:

                case System.Xml.XmlNodeType.ProcessingInstruction:

                    writer.WriteProcessingInstruction (reader.Name, reader.Value);

                    break;

                case System.Xml.XmlNodeType.DocumentType:

                    writer.WriteDocType (reader.Name, reader.GetAttribute ("PUBLIC"), reader.GetAttribute ("SYSTEM"), reader.Value);

                    break;

                case System.Xml.XmlNodeType.Comment:
                    
                    writer.WriteComment (reader.Value);

                    break;

                case System.Xml.XmlNodeType.EndElement:

                    writer.WriteFullEndElement ();

                    break;
            }

            if (reader.Read ()) { XmlReaderClone (reader, writer); }

        }

        public static String XmlReaderToString (System.Xml.XmlReader reader) {

            if (reader == null) { throw new ArgumentNullException ("System.Xml.XmlReader"); }

            StringBuilder xmlString = new StringBuilder ();

            while (reader.Read ()) {

                xmlString.AppendLine (reader.ReadOuterXml ());

            }

            return xmlString.ToString ();

        }

        #endregion 


        public static Boolean IsGuid (String guid) {

            Boolean isGuid = false;

            if (!String.IsNullOrEmpty (guid)) {

                System.Text.RegularExpressions.Regex expression = new System.Text.RegularExpressions.Regex (@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

                isGuid = expression.IsMatch (guid);

            }

            return isGuid;

        }

        public static String GuidCollapse (Guid forGuid) { return forGuid.ToString ().Replace ("-", ""); }

        public static Guid GuidExpand (String forGuid) {

            // 3F2504E04F8911D39A0C0305E82C3301

            // 12345678901234567890123456789012

            // 3F2504E0-4F89-11D3-9A0C-0305E82C3301

            Guid guid = Guid.Empty;

            String guidExpanded = forGuid.Substring (0, 8) + "-" + forGuid.Substring (8, 4) + "-"
                
                + forGuid.Substring (12, 4) + "-" + forGuid.Substring (16, 4)

                + forGuid.Substring (20, 12);

            Guid.TryParse (guidExpanded, out guid);

            return guid;

        }

        public static String ObjectSidToString (Byte[] sidByteArray) {

            if (sidByteArray == null) { return String.Empty; }

            System.Security.Principal.SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier (sidByteArray, 0);

            return sid.ToString ();

        }

        public static String EnumerationToString (Object value) {

            String enumerationString = value.ToString ();

            if (!String.IsNullOrEmpty (enumerationString)) {

                enumerationString = System.Text.RegularExpressions.Regex.Replace (enumerationString, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

                enumerationString = enumerationString.Trim ();

            }

            return enumerationString;

        }

        public static String PascalString (String value) {

            String enumerationString = value;

            if (!String.IsNullOrEmpty (enumerationString)) {

                enumerationString = System.Text.RegularExpressions.Regex.Replace (enumerationString, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

                enumerationString = enumerationString.Trim ();

            }

            return enumerationString;

        }

        public static Int32 CurrentAge (DateTime? forBirthDate) {

            if (!forBirthDate.HasValue) { return -1; }

            Int32 currentAge = 0;

            DateTime birthDay;


            currentAge = DateTime.Today.Year - forBirthDate.Value.Year;

            birthDay = new DateTime (DateTime.Today.Year, forBirthDate.Value.Month, (((forBirthDate.Value.Month == 2) && (forBirthDate.Value.Day == 29)) ? 28 : forBirthDate.Value.Day));

            if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

            return currentAge;

        }


        #region Database Functions

        public static Int64 IdFromSql (System.Data.DataRow currentRow, String fieldName) {

            Int64 forId = 0;


            if (currentRow.Table.Columns.Contains (fieldName)) {

                if (!(currentRow[fieldName] is DBNull)) {

                    forId = Convert.ToInt64 (currentRow[fieldName]);

                }

            }


            return forId;

        }

        public static String StringFromSql (System.Data.DataRow currentRow, String fieldName) {

            String forString = String.Empty;


            if (currentRow.Table.Columns.Contains (fieldName)) {

                if (!(currentRow[fieldName] is DBNull)) {

                    forString = Convert.ToString (currentRow[fieldName]);

                }

            }


            return forString;

        }

        public static DateTime? DateTimeFromSql (System.Data.DataRow currentRow, String fieldName) {

            DateTime? forDateTime = null;


            if (currentRow.Table.Columns.Contains (fieldName)) {

                if (!(currentRow[fieldName] is DBNull)) {

                    forDateTime = Convert.ToDateTime (currentRow[fieldName]);

                }

            }


            return forDateTime;

        }

        #endregion 


        #region XPS Functions

        public static System.IO.MemoryStream XmlFontDeobfuscate (System.IO.Stream fontStream, Guid forGuid) {

            System.IO.MemoryStream font = new System.IO.MemoryStream ();


            fontStream.Seek (0, System.IO.SeekOrigin.Begin); // MOVE TO FRONT OF STREAM


            Byte[] guidDecoder = new Byte[16];

            for (Int32 currentIndex = 0; currentIndex < guidDecoder.Length; currentIndex++) {

                guidDecoder[currentIndex] = Convert.ToByte (forGuid.ToString ("N").Substring (currentIndex * 2, 2), 16);

            }


            // FILL BUFFER

            Byte[] buffer = new Byte[fontStream.Length];

            fontStream.Read (buffer, 0, Convert.ToInt32 (fontStream.Length));


            // DECODE FIRST 32 BYTES

            for (Int32 currentIndex = 0; currentIndex < 32; currentIndex++) {

                Int32 decoderPosition = guidDecoder.Length - (currentIndex % guidDecoder.Length) - 1;

                buffer[currentIndex] ^= guidDecoder[decoderPosition];

            }


            font.Write (buffer, 0, buffer.Length);

            font.Seek (0, System.IO.SeekOrigin.Begin); // MOVE TO BEGINNING

            return font;
            
        }

        #endregion 

    }

}
