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

namespace Mercury.Client.Core.Forms.Controls {

    public class SectionColumn : Control {

        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.SectionColumn;

            return;

        }

        public SectionColumn (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public SectionColumn (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlSectionColumn serverControl) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverControl);

            ChildServerControlsToLocal (this, serverControl);

            return;

        }

        #endregion

        public override Boolean AllowChildControl (Server.Application.FormControlType childControlType) {

            if (childControlType == Server.Application.FormControlType.Undefined) { return false; }

            if (childControlType == Server.Application.FormControlType.Form) { return false; }

            if (childControlType == Server.Application.FormControlType.SectionColumn) { return false; }

            return true;

        }

    }

}
