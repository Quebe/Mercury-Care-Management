using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace Mercury.Web.Application.Forms.FormEditor {

    public class EntityControlMemberItemTemplate : System.Web.UI.ITemplate {

        public void InstantiateIn (System.Web.UI.Control containerControl) {

            System.Web.UI.WebControls.Table itemTable = new System.Web.UI.WebControls.Table ();

            itemTable.Width = Unit.Pixel (500);

            itemTable.DataBinding += new EventHandler(ItemTable_OnDataBinding);

            TableRow itemTableRow = new TableRow ();

            itemTable.Rows.Add (itemTableRow);

            TableCell itemTableCell;


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (260);

            itemTableCell.Attributes.Add ("style", "min-width: 150px;");

            itemTableRow.Cells.Add (itemTableCell);


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (80);

            itemTableCell.Attributes.Add ("style", "min-width: 75px;");

            itemTableCell.Attributes.Add ("align", "center");

            itemTableRow.Cells.Add (itemTableCell);


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (80);

            itemTableCell.Attributes.Add ("style", "min-width: 75px;");

            itemTableCell.Attributes.Add ("align", "center");

            itemTableRow.Cells.Add (itemTableCell);


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (80);

            itemTableCell.Attributes.Add ("style", "min-width: 75px;");

            itemTableCell.Attributes.Add ("align", "center");

            itemTableRow.Cells.Add (itemTableCell);


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (80);

            itemTableCell.Attributes.Add ("style", "min-width: 75px;");

            itemTableCell.Attributes.Add ("align", "center");

            itemTableRow.Cells.Add (itemTableCell);

            containerControl.Controls.Add (itemTable);

            return;

        }

        protected void ItemTable_OnDataBinding (Object sender, EventArgs eventArgs) {

            System.Web.UI.WebControls.Table itemTable = (System.Web.UI.WebControls.Table) sender;

            Telerik.Web.UI.RadComboBoxItem currentItem = (Telerik.Web.UI.RadComboBoxItem) itemTable.BindingContainer;
            
            System.Data.DataRowView currentRow = (System.Data.DataRowView) currentItem.DataItem;

            itemTable.Rows[0].Cells[0].Text = ((String) currentRow["MemberName"]);

            itemTable.Rows[0].Cells[1].Text = ((String) currentRow["BirthDate"]);

            itemTable.Rows[0].Cells[2].Text = ((String) currentRow["CurrentAge"]);

            itemTable.Rows[0].Cells[3].Text = ((String) currentRow["Gender"]);

            itemTable.Rows[0].Cells[4].Text = ((String) currentRow["Enrolled"]);

            return;

        }


    }

}
