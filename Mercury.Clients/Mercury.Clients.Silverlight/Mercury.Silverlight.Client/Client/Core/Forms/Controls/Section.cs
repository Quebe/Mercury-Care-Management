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

    public class Section : Control {

        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Section;

            return;

        }

        public Section (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Section (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl) {

            InitializeControl (applicationReference);

            parent = parentControl;

            InsertNewControl (0, new SectionColumn (Application));

            return;

        }

        public Section (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlSection serverSection) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverSection);

            ChildServerControlsToLocal (this, serverSection);

            return;

        }

        #endregion


        #region Public Methods

        public override Boolean AllowChildControl (Server.Application.FormControlType childControlType) {

            if (childControlType != Server.Application.FormControlType.SectionColumn) { return false; }

            return true;

        }

        #endregion 

    }

}
