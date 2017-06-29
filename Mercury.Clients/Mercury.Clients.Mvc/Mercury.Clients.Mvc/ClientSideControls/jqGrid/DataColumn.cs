using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.ClientSideControls.jqGrid {

    public class DataColumn {

        #region Private Properties

        private String columnName = String.Empty;

        private String label = String.Empty;

        private String indexName = String.Empty;

        private String dataField = String.Empty;

        private String jsonMap = String.Empty;


        private Int32 columnWidth = 0;

        private Boolean columnWidthFixed = false;

        private String alignHeader = String.Empty;

        private String alignCell = String.Empty;

        private String cellCssClasses = String.Empty;

        private String formatter = String.Empty;


        private Boolean isResizable = true;

        private Boolean isSortable = false;

        private Boolean isVisible = true;

        #endregion


        #region Public Properties

        public String ColumnName { get { return columnName; } set { columnName = value; } }

        public String Label { get { return label; } set { label = value; } }

        public String IndexName { get { return indexName; } set { indexName = value; } }

        public String DataField { get { return dataField; } set { dataField = value; } }

        public String JsonMap { get { return jsonMap; } set { jsonMap = value; } }

        
        public Int32 ColumnWidth { get { return columnWidth; } set { columnWidth = value; } }

        public Boolean ColumnWidthFixed { get { return columnWidthFixed; } set { columnWidthFixed = value; } }

        public String AlignHeader { get { return alignHeader; } set { alignHeader = value; } }

        public String AlignCell { get { return alignCell; } set { alignCell = value; } }

        public String CellCssClasses { get { return cellCssClasses; } set { cellCssClasses = value; } }

        public String Formatter { get { return formatter; } set { formatter = value; } }


        public Boolean IsResizable { get { return isResizable; } set { isResizable = value; } }

        public Boolean IsSortable { get { return isSortable; } set { isSortable = value; } }

        public Boolean IsVisible { get { return isVisible; } set { isVisible = value; } }

        #endregion


        #region Constructors

        public DataColumn (String forColumnName) {

            ColumnName = forColumnName;

            return;

        }

        public DataColumn (String forColumnName, String forLabel, String forDataField, Boolean forIsVisible = true) {

            ColumnName = forColumnName;

            Label = forLabel;

            DataField = forDataField;

            isVisible = forIsVisible;

            return;

        }

        #endregion


    }

}