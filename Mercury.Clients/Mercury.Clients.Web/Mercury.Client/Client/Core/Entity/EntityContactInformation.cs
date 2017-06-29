using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Entity {

    [Serializable]
    public class EntityContactInformation : CoreObject {

        #region Private Properties

        private Int64 entityId = 0;

        private Server.Application.EntityContactType contactType = Server.Application.EntityContactType.NotSpecified;

        private Int32 contactSequence = 1;

        private String number = String.Empty;

        private String numberExtension = String.Empty;

        private String email = String.Empty;

        private DateTime effectiveDate = new DateTime (1900, 01, 01);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties

        public Int64 EntityId { get { return entityId; } set { entityId = value; } }

        public Server.Application.EntityContactType ContactType { get { return contactType; } set { contactType = value; } }

        public Int32 ContactTypeInt32 { get { return ((Int32)contactType); } }

        public String ContactTypeDescription { get { return Server.CommonFunctions.EnumerationToString (contactType); } }

        public Int32 ContactSequence { get { return contactSequence; } set { contactSequence = value; } }

        public String Number { get { return number; } set { number = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ContactNumber); } }

        public String NumberExtension { get { return numberExtension; } set { numberExtension = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ContactExtension); } }

        public String NumberFormatted {

            get {

                if (String.IsNullOrWhiteSpace (number)) { return String.Empty; }

                String numberFormatted = number;

                numberFormatted = numberFormatted.Replace (" ", "");

                numberFormatted = numberFormatted.Replace ("(", "");

                numberFormatted = numberFormatted.Replace (")", "");

                numberFormatted = numberFormatted.Replace ("-", "");

                if (numberFormatted.Length < 10) { numberFormatted = "000" + numberFormatted; }

                String formatPattern = @"(\d{3})(\d{3})(\d{4})";

                numberFormatted = System.Text.RegularExpressions.Regex.Replace (numberFormatted, formatPattern, "($1) $2-$3");

                if (numberExtension.Trim ().Length > 0) {

                    numberFormatted = numberFormatted + " " + numberExtension.Trim ();

                }

                return numberFormatted;

            }

        }

        public String Email { get { return email; } set { email = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.ContactEmail); } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String TerminationDateDescription { get { return ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")); } }

        #endregion

        
        #region Constructors

        public EntityContactInformation (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public EntityContactInformation (Application applicationReference, Server.Application.EntityContactInformation serverEntityContactInformation) {

            BaseConstructor (applicationReference, serverEntityContactInformation);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.EntityContactInformation serverEntityContactInformation) {

            base.BaseConstructor (applicationReference, serverEntityContactInformation);


            entityId = serverEntityContactInformation.EntityId;

            contactType = serverEntityContactInformation.ContactType;

            contactSequence = serverEntityContactInformation.ContactSequence;

            number = serverEntityContactInformation.Number;

            numberExtension = serverEntityContactInformation.NumberExtension;

            email = serverEntityContactInformation.Email;

            effectiveDate = serverEntityContactInformation.EffectiveDate;

            terminationDate = serverEntityContactInformation.TerminationDate;


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.EntityContactInformation serverEntityContactInformation) {

            base.MapToServerObject ((Server.Application.CoreObject)serverEntityContactInformation);


            serverEntityContactInformation.EntityId = entityId;

            serverEntityContactInformation.ContactType = contactType;

            serverEntityContactInformation.ContactSequence = contactSequence;

            serverEntityContactInformation.Number = number;

            serverEntityContactInformation.NumberExtension = numberExtension;

            serverEntityContactInformation.Email = email;

            serverEntityContactInformation.EffectiveDate = effectiveDate;

            serverEntityContactInformation.TerminationDate = terminationDate;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.EntityContactInformation serverEntityContactInformation = new Server.Application.EntityContactInformation ();

            MapToServerObject (serverEntityContactInformation);

            return serverEntityContactInformation;

        }

        public EntityContactInformation Copy () {

            Server.Application.EntityContactInformation serverEntityContactInformation = (Server.Application.EntityContactInformation)ToServerObject ();

            EntityContactInformation copiedEntityContactInformation = new EntityContactInformation (application, serverEntityContactInformation);

            return copiedEntityContactInformation;

        }

        public Boolean IsEqual (EntityContactInformation compareEntityContactInformation) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareEntityContactInformation);


            isEqual &= (entityId == compareEntityContactInformation.EntityId);

            isEqual &= (contactType == compareEntityContactInformation.ContactType);


            isEqual &= (contactSequence == compareEntityContactInformation.ContactSequence);

            isEqual &= (number == compareEntityContactInformation.Number);

            isEqual &= (numberExtension== compareEntityContactInformation.NumberExtension);

            isEqual &= (email == compareEntityContactInformation.Email);


            isEqual &= (effectiveDate == compareEntityContactInformation.EffectiveDate);

            isEqual &= (terminationDate == compareEntityContactInformation.TerminationDate);

            
            return isEqual;

        }

        #endregion 

    }

}
