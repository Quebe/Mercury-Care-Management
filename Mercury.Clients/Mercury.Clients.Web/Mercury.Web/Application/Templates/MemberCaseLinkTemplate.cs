using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Web.Application.Templates {

    public class MemberCaseLinkTemplate : System.Web.UI.ITemplate {

        #region Private Properties

        private System.Web.UI.LiteralControl anchor = null;

        #endregion


        #region Public Methods

        public void InstantiateIn (System.Web.UI.Control container) {

            anchor = new System.Web.UI.LiteralControl ();

            anchor.ID = "MemberCaseLinkAnchor";

            anchor.DataBinding += new EventHandler (MemberCaseLinkTemplate_DataBinding);

            container.Controls.Add (anchor);

        }

        public void MemberCaseLinkTemplate_DataBinding (Object sender, EventArgs e) {

            System.Web.UI.LiteralControl controlInstance = (System.Web.UI.LiteralControl)sender;

            if (!(controlInstance.NamingContainer is Telerik.Web.UI.GridDataItem)) { return; }

            Telerik.Web.UI.GridDataItem container = (Telerik.Web.UI.GridDataItem)controlInstance.NamingContainer;

            Client.Core.CoreObject coreObject = (Client.Core.CoreObject)container.DataItem;

            controlInstance.Text = CommonFunctions.CaseAnchor (coreObject.Id, coreObject.Id.ToString ());

            return;

        }

        #endregion

    }

}