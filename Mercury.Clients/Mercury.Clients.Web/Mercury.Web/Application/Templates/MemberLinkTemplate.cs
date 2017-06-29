using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Web.Application.Templates {

    public class MemberLinkTemplate : System.Web.UI.ITemplate {

        #region Private Properties

        private System.Web.UI.LiteralControl anchor = null;

        #endregion 


        #region Public Methods

        public void InstantiateIn (System.Web.UI.Control container) {

            anchor = new System.Web.UI.LiteralControl ();

            anchor.ID = "MemberLinkAnchor";

            anchor.DataBinding += new EventHandler(MemberLinkTemplate_DataBinding);

            container.Controls.Add (anchor);

        }

        public void MemberLinkTemplate_DataBinding (Object sender, EventArgs e) {

            System.Web.UI.LiteralControl controlInstance = (System.Web.UI.LiteralControl) sender;

            if (!(controlInstance.NamingContainer is Telerik.Web.UI.GridDataItem)) { return; }

            Telerik.Web.UI.GridDataItem container = (Telerik.Web.UI.GridDataItem)controlInstance.NamingContainer;

            Client.Core.Member.Member member = (Client.Core.Member.Member)container.DataItem;

            controlInstance.Text = CommonFunctions.MemberProfileAnchor (member.Id, member.Name);

            return;

        }

        #endregion 

    }

}