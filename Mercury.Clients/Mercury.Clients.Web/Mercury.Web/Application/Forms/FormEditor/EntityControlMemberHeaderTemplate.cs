using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace Mercury.Web.Application.Forms.FormEditor {

    public class EntityControlMemberHeaderTemplate : System.Web.UI.ITemplate {

        public void InstantiateIn (System.Web.UI.Control containerControl) {

            System.Web.UI.WebControls.Table headerTable = new System.Web.UI.WebControls.Table ();

            headerTable.Width = Unit.Pixel (500);

            TableRow headerTableRow = new TableRow ();

            headerTable.Rows.Add (headerTableRow);

            TableCell headerTableCell;


            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (260);

            headerTableCell.Attributes.Add ("style", "min-width: 150px;");

            headerTableCell.Text = "Name";

            headerTableRow.Cells.Add (headerTableCell);


            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (80);

            headerTableCell.Attributes.Add ("style", "min-width: 75px;");

            headerTableCell.Attributes.Add ("align", "center");

            headerTableCell.Text = "Birth Date"; 
            
            headerTableRow.Cells.Add (headerTableCell);


            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (80);

            headerTableCell.Attributes.Add ("style", "min-width: 75px;");

            headerTableCell.Attributes.Add ("align", "center");

            headerTableCell.Text = "Age";

            headerTableRow.Cells.Add (headerTableCell);


            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (80);

            headerTableCell.Attributes.Add ("style", "min-width: 75px;");

            headerTableCell.Attributes.Add ("align", "center");

            headerTableCell.Text = "Gender";

            headerTableRow.Cells.Add (headerTableCell);



            headerTableCell = new TableCell ();

            headerTableCell.Width = Unit.Pixel (80);

            headerTableCell.Attributes.Add ("style", "min-width: 75px;");

            headerTableCell.Attributes.Add ("align", "center");

            headerTableCell.Text = "Enrolled";

            headerTableRow.Cells.Add (headerTableCell);


            containerControl.Controls.Add (headerTable);

            return;

        }


    }

}
