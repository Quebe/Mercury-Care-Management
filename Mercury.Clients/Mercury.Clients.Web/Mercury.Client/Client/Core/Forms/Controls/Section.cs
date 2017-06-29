using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Section : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        private Boolean pageBreakAfterSection = false;

        #endregion


        #region Public Properties

        public Boolean PageBreakAfterSection { get { return pageBreakAfterSection; } set { pageBreakAfterSection = value; } }

        #endregion


        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Section;

            return;

        }

        public Section (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl) {

            InitializeControl (applicationReference);

            parent = parentControl;

            InsertNewControl (0, new SectionColumn (Application));

            return;

        }

        public Section (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlSection serverSection) {

            BaseConstructor (applicationReference, parentControl, serverSection);

            InitializeControl (applicationReference);

            pageBreakAfterSection = serverSection.PageBreakAfterSection;

            ChildServerControlsToLocal (this, serverSection);

            return;

        }
 
        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlSection)serverControl).PageBreakAfterSection = pageBreakAfterSection;

            return;
        }

        #endregion


        public override Boolean AllowChildControl (Mercury.Server.Application.FormControlType childControlType) {

            if (childControlType != Mercury.Server.Application.FormControlType.SectionColumn) { return false; }

            return true;

        }

    }

}
