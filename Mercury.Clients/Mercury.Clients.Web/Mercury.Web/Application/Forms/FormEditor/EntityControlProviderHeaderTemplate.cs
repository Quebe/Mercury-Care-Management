using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace Mercury.Web.Application.Forms.FormEditor {

    public class EntityControlProviderHeaderTemplate : System.Web.UI.ITemplate {

        public void InstantiateIn (System.Web.UI.Control containerControl) {

            System.Web.UI.WebControls.Table headerTable = new System.Web.UI.WebControls.Table ();

            headerTable.Width = Unit.Pixel (700);

            TableRow headerTableRow = new TableRow ();

            headerTable.Rows.Add (headerTableRow);

            TableCell headerTableCell;


            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (300);

            headerTableCell.Text = "Name";

            headerTableRow.Cells.Add (headerTableCell);


            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (90);

            headerTableCell.Attributes.Add ("align", "center");

            headerTableCell.Text = "Federal Id";

            headerTableRow.Cells.Add (headerTableCell);


            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (90);

            headerTableCell.Attributes.Add ("align", "center");

            headerTableCell.Text = "NPI";

            headerTableRow.Cells.Add (headerTableCell);


            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (220);

            headerTableCell.Text = "Primary Specialty";

            headerTableCell.Attributes.Add ("style", "white-space: nowrap");

            headerTableRow.Cells.Add (headerTableCell);


            containerControl.Controls.Add (headerTable);

            return;

        }


    }

}
