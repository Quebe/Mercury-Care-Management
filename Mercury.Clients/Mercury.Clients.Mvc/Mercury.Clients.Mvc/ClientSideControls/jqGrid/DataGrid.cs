using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.ClientSideControls.jqGrid {

    public class DataGrid {

        #region Private Properties

        private String name = String.Empty;

        private Boolean hasPager = false;

        private Boolean isRowSelectEnabled = false;

        private Boolean allowMultiselect = false;

        private Int32 selectedRowId = 0;

        
        private List<DataColumn> columns = new List<DataColumn> ();

        private Object dataSource = null;


        private Boolean roundedCorners = false;

        private String loadingText = String.Empty;

        private Boolean widthResizeToContainer = false;

        private Boolean heightResizeToContainer = false;

        #endregion


        #region Public Properties

        public String Name { get { return name; } set { name = value; } }

        public Boolean HasPager { get { return hasPager; } set { hasPager = value; } }

        public Boolean IsRowSelectEnabled { get { return isRowSelectEnabled; } set { isRowSelectEnabled = value; } }

        public Boolean AllowMultiselect { get { return allowMultiselect; } set { allowMultiselect = value; } }

        public Int32 SelectedRowId { get { return selectedRowId; } set { selectedRowId = value; } }


        public List<DataColumn> Columns { get { return columns; } set { columns = value; } }

        public Object DataSource { get { return dataSource; } set { dataSource = value; } }


        public Boolean RoundedCorners { get { return roundedCorners; } set { roundedCorners = value; } }

        public Boolean WidthResizeToContainer { get { return widthResizeToContainer; } set { widthResizeToContainer = value; } }

        public Boolean HeightResizeToContainer { get { return heightResizeToContainer; } set { heightResizeToContainer = value; } }

        #endregion


        #region Constructors

        public DataGrid (String forName) {

            Name = forName;

            return;

        }

        #endregion


        #region Render

        public String Render () {

            System.Text.StringBuilder content = new System.Text.StringBuilder ();


            // RENDER DOM ELEMENTS

            content.AppendLine ("<table id=\"" + name + "\" />");

            if (hasPager) {

                content.AppendLine ("<div id=\"" + name + "Pager\" />");

            }

            if (IsRowSelectEnabled) {

                content.AppendLine ("<input id=\"" + name + "SelectedRowId\" name = \"" + name + "SelectedRowId\" type=\"hidden\" value=\"" + selectedRowId.ToString () + "\" />");

            }


            // RENDER JQGRID SCRIPT

            content.AppendLine ("<script id=\"" + name + "_Script\" type=\"text/javascript\">");

            content.AppendLine ("$(document).ready(function () {");


            #region Build Data Source Item Array for Grid reference

            if ((dataSource != null) && (dataSource is IEnumerable<Object>)) {

                content.AppendLine ("var " + name + "Data = [");


                Boolean isFirstItem = true;

                foreach (Object currentItem in (dataSource as IEnumerable<Object>)) {

                    Boolean isFirstColumn = true;

                    if (!isFirstItem) { content.Append (", "); }

                    else { isFirstItem = false; }

                    content.Append ("{");

                    for (Int32 currentColumnIndex = 0; currentColumnIndex < columns.Count; currentColumnIndex++) {

                        DataColumn currentColumn = columns[currentColumnIndex];

                        // ONLY INCLUDE COLUMNS THAT HAVE DATA ITEMS

                        if (!String.IsNullOrWhiteSpace (currentColumn.DataField)) {

                            System.Reflection.PropertyInfo currentProperty = currentItem.GetType ().GetProperty (currentColumn.DataField);

                            if (currentProperty != null) {

                                String currentPropertyValue = currentProperty.GetValue (currentItem, null).ToString ();

                                if (!isFirstColumn) { content.Append (","); }

                                else { isFirstColumn = false; }

                                content.Append (" " + currentColumn.ColumnName + ": \"" + currentPropertyValue + "\"");

                            }

                        }

                    }

                    content.AppendLine ("}");

                }

                content.AppendLine ("];");

            }

            #endregion 


            content.AppendLine ("$(\"#" + name + "\").jqGrid({");

            if (hasPager) {

                content.AppendLine ("pager: \"" + name + "Pager\", ");

            }


            content.AppendLine ("datatype: \"local\", ");

            content.AppendLine ("data: " + name + "Data, ");


            #region Render Columns

            if (columns.Count > 0) {

                content.AppendLine ("colModel: [");

                for (Int32 currentColumnIndex = 0; currentColumnIndex < columns.Count; currentColumnIndex ++) {

                    if (currentColumnIndex != 0) { content.AppendLine (", "); }

                    DataColumn currentDataColumn = columns [currentColumnIndex];

                    // NAME

                    content.Append ("{ name: \"" + currentDataColumn.ColumnName + "\"");

                    // LABEL

                    if (!String.IsNullOrWhiteSpace (currentDataColumn.Label)) { content.Append (", label: \"" + currentDataColumn.Label + "\""); }

                    // INDEX 

                    if (!String.IsNullOrWhiteSpace (currentDataColumn.IndexName)) { content.Append (", label: \"" + currentDataColumn.IndexName + "\""); }

                    // JSONMAP

                    if (!String.IsNullOrWhiteSpace (currentDataColumn.JsonMap)) { content.Append (", label: \"" + currentDataColumn.JsonMap + "\""); }

                    // COLUMN WIDTH

                    if (currentDataColumn.ColumnWidth > 0) { content.Append (", width: " + currentDataColumn.ColumnWidth.ToString ()); }

                    // COLUMN WIDTH FIXED

                    if (currentDataColumn.ColumnWidthFixed) { content.Append (", fixed: true"); }

                    // ALIGN CELL CONTENT (DEFAULT LEFT)

                    if (!String.IsNullOrWhiteSpace (currentDataColumn.AlignCell)) { content.Append (", align: \"" + currentDataColumn.AlignCell + "\""); }

                    // CSS CLASSES

                    if (!String.IsNullOrWhiteSpace (currentDataColumn.CellCssClasses)) { content.Append (", classes: \"" + currentDataColumn.CellCssClasses + "\""); }

                    // RESIZABLE 

                    if (!currentDataColumn.IsResizable) { content.Append (", resizable: false"); }

                    // SORTABLE

                    if (!currentDataColumn.IsSortable) { content.Append (", sortable: false"); }

                    // VISIBLE

                    if (!currentDataColumn.IsVisible) { content.Append (", hidden: true"); }

                    // FORMATTER

                    if (!String.IsNullOrWhiteSpace (currentDataColumn.Formatter)) { content.Append (", formatter: \"" + currentDataColumn.Formatter + "\""); }

                    content.AppendLine (" }");

                }

                content.AppendLine ("], ");

            }

            #endregion 


            #region Enable Row Selection Option

            if (isRowSelectEnabled) {

                content.AppendLine ("multiselect: true, multiboxonly: true, ");

                if (!allowMultiselect) {

                    content.Append ("onSelectRow: function (rowId) { ");

                    content.Append ("$('#" + name + "').resetSelection ();");

                    content.Append ("$('#" + name + "').setSelection (rowId, false);");

                    content.Append ("$('#" + name + "SelectedRowId').val (rowId);");

                    content.AppendLine ("}, ");

                }

            }

            #endregion 


            content.AppendLine ("terminator: \"\"");

            content.AppendLine ("});"); //jqGrid [END]
            

            #region Multiselect Options

            if ((isRowSelectEnabled) && (selectedRowId != 0)) {

                content.AppendLine ("$('#" + name + "').setSelection (" + selectedRowId + ", false);");

            }

            if ((isRowSelectEnabled) && (!allowMultiselect)) {

                content.AppendLine ("$(\"#cb_" + name + "\").hide();");

            }

            #endregion 


            #region Style Options

            if (!roundedCorners) {

                content.AppendLine ("$(\"#gbox_" + name + ".ui-corner-all\").removeClass(\"ui-corner-all\");");

                if (hasPager) {

                    content.AppendLine ("$(\"#" + name + "Pager.ui-corner-bottom\").removeClass(\"ui-corner-bottom\");");

                }

            }

            #endregion 


            #region Grid Resize Function Assignment

            if ((widthResizeToContainer) || (heightResizeToContainer)) {

                content.AppendLine ("$(window).resize(function () { ");


                if (widthResizeToContainer) {

                    content.AppendLine ("var adjustedWidth = $(\"#gbox_" + name + "\").parent().width() - 2; ");

                    content.Append ("try { ");

                    content.Append ("$(\"#" + name + "\").setGridWidth(adjustedWidth);");

                    content.AppendLine (" } catch (exception) { }");

                }

                if (heightResizeToContainer) {

                    content.AppendLine ("var adjustedHeight = $(\"#gbox_" + name + "\").parent().height(); ");

                    // SUBTRACT OUT "CAPTION" IF VISIBLE

                    content.AppendLine ("if ($(\"#gview_" + name + " .ui-jqgrid-titlebar :visible\").length != 0) {");

                    content.AppendLine ("    adjustedHeight = adjustedHeight - $(\"#gview_" + name + " .ui-jqgrid-titlebar :visible\").outerHeight(); }");

                    // SUBTRACT OUT HEADER 

                    content.AppendLine ("adjustedHeight = adjustedHeight - ($(\"#gview_" + name + " .ui-jqgrid-hdiv\").outerHeight()); ");

                    // SUBTRACT OUT PAGER 

                    content.AppendLine ("if ($(\"#" + name + "Pager\").length != 0) { ");

                    content.AppendLine ("    adjustedHeight = adjustedHeight - $(\"#" + name + "Pager\").outerHeight(); }");

                    // SUBTRACT OUT BORDERS

                    content.AppendLine ("adjustedHeight = adjustedHeight - 2;");

                    content.Append ("try { ");

                    content.Append ("$(\"#" + name + "\").setGridHeight(adjustedHeight);");

                    content.AppendLine (" } catch (exception) { }");

                }


                content.AppendLine ("});");

            }

            #endregion 


            content.AppendLine ("$(window).resize();"); // force redraw

            content.AppendLine ("});"); // document.ready

            content.AppendLine ("</script>");


            return content.ToString ();

        }

        #endregion

    }

}