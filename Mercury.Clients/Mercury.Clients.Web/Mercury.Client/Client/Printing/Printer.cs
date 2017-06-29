using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Printing {

    [Serializable]
    public class Printer : Core.CoreConfigurationObject {

        #region Private Properties

        private String printServerName = String.Empty;

        private String printQueueName = String.Empty;

        #endregion 


        #region Public Properties

        public String PrintServerName { get { return printServerName; } set { printServerName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String PrintQueueName { get { return printQueueName; } set { printQueueName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        #endregion

        

        #region Constructors

        public Printer (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Printer (Application applicationReference, Server.Application.Printer serverObject) {

            BaseConstructor (applicationReference, serverObject);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Printer serverObject) {

            base.BaseConstructor (applicationReference, serverObject);


            printServerName = serverObject.PrintServerName;

            printQueueName = serverObject.PrintQueueName;


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Printer serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);


            serverObject.PrintServerName = printServerName;

            serverObject.PrintQueueName = printQueueName;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.Printer serverObject = new Server.Application.Printer ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public Printer Copy () {

            Server.Application.Printer serverObject = (Server.Application.Printer)ToServerObject ();

            Printer copiedObject = new Printer (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (Printer compareObject) {

            Boolean isEqual = base.IsEqual ((Core.CoreConfigurationObject)compareObject);


            isEqual &= (printServerName == compareObject.PrintServerName);

            isEqual &= (printQueueName == compareObject.PrintQueueName);


            return isEqual;

        }

        #endregion

    }

}

