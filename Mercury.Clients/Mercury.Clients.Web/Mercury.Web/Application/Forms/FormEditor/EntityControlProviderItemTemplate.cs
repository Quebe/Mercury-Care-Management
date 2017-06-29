using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace Mercury.Web.Application.Forms.FormEditor {

    public class EntityControlProviderItemTemplate : System.Web.UI.ITemplate {

        public void InstantiateIn (System.Web.UI.Control containerControl) {

            System.Web.UI.WebControls.Table itemTable = new System.Web.UI.WebControls.Table ();

            itemTable.Width = Unit.Pixel (800);

            itemTable.DataBinding += new EventHandler (ItemTable_OnDataBinding);

            TableRow itemTableRow = new TableRow ();

            itemTable.Rows.Add (itemTableRow);

            TableCell itemTableCell;


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (300);

            itemTableCell.Attributes.Add ("style", "max-width: 300px; white-space: normal; overflow: hidden");

            itemTableRow.Cells.Add (itemTableCell);


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (100);

            itemTableCell.Attributes.Add ("align", "center");

            itemTableRow.Cells.Add (itemTableCell);


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (100);

            itemTableCell.Attributes.Add ("align", "center");

            itemTableRow.Cells.Add (itemTableCell);


            itemTableCell = new TableCell ();

            itemTableCell.Width = Unit.Pixel (300);

            itemTableCell.Attributes.Add ("style", "max-width: 300px; white-space: normal; overflow: hidden");

            itemTableRow.Cells.Add (itemTableCell);


            containerControl.Controls.Add (itemTable);

            return;

        }

        protected void ItemTable_OnDataBinding (Object sender, EventArgs eventArgs) {

            System.Web.UI.WebControls.Table itemTable = (System.Web.UI.WebControls.Table) sender;

            Telerik.Web.UI.RadComboBoxItem currentItem = (Telerik.Web.UI.RadComboBoxItem) itemTable.BindingContainer;

            System.Data.DataRowView currentRow = (System.Data.DataRowView) currentItem.DataItem;

            itemTable.Rows[0].Cells[0].Text = ((String) currentRow["ProviderName"]);

            itemTable.Rows[0].Cells[1].Text = ((String) currentRow["FederalTaxId"]);

            itemTable.Rows[0].Cells[2].Text = ((String) currentRow["NationalProviderId"]);

            itemTable.Rows[0].Cells[3].Text = ((String) currentRow["PrimarySpecialtyName"]);

            return;

        }


    }

}
