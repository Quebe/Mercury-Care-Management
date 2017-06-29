using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms {

    [Serializable]
    [DataContract (Name = "FormCompileMessage")]
    public class CompileMessage {

        #region Private Properties

        [DataMember (Name = "MessageType")]
        private Enumerations.FormCompileMessageType messageType;

        [DataMember (Name = "Line")]
        private Int32 line;

        [DataMember (Name = "Column")]
        private Int32 column;

        [DataMember (Name = "Description")]
        private String description;

        [DataMember (Name = "ControlId")]
        private String controlId;

        [DataMember (Name = "ControlType")]
        private String controlType;

        [DataMember (Name = "ControlName")]
        private String controlName;


        #endregion


        #region Public Properties

        public Enumerations.FormCompileMessageType MessageType { get { return messageType; } set { messageType = value; } }

        public Int32 Line { get { return line; } set { line = value; } }

        public Int32 Column { get { return column; } set { column = value; } }

        public String Description { get { return description; } set { description = value; } }

        public String ControlId { get { return controlId; } set { controlId = value; } }

        public String ControlType { get { return controlType; } set { controlType = value; } }

        public String ControlName { get { return controlName; } set { controlName = value; } }

        #endregion 


        #region Constructor

        public CompileMessage (Enumerations.FormCompileMessageType compileMessageType, String message, Control control) {

            messageType = compileMessageType;

            description = message;

            controlId = control.Id.ToString ();

            controlType = control.ControlType.ToString ();

            controlName = control.Name;

            return;

        }

        public CompileMessage (Enumerations.FormCompileMessageType compileMessageType, Int32 errorLine, Int32 errorColumn, String message, Control control) {

            messageType = compileMessageType;

            line = errorLine;

            column = errorColumn;

            description = message;

            controlId = control.Id.ToString ();

            controlType = control.ControlType.ToString ();

            controlName = control.Name;

            return;

        }

        #endregion 

    }

}
