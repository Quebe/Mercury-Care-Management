using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Work {

    public partial class WorkQueueMonitor : System.Web.UI.Page {

        #region Private Session States

        private Mercury.Client.Application MercuryApplication { get { return Master.MercuryApplication; } }

        private String SessionCachePrefix { get { return Master.SessionCachePrefix; } }


        private Client.Core.Work.WorkQueue WorkQueueSelected {

            get {

                Client.Core.Work.WorkQueue workQueue = (Client.Core.Work.WorkQueue)Session[SessionCachePrefix + "WorkQueueSelected"];

                return workQueue;

            }

            set { Session[SessionCachePrefix + "WorkQueueSelected"] = value; }

        }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            if (!IsPostBack) {

                // IF FIRST REQUEST, READ FROM QUERY STRING

                Int64 selectedWorkQueueId = 0;

                Int64.TryParse (Convert.ToString (Request.QueryString["WorkQueueId"]), out selectedWorkQueueId);

                WorkQueueSelected = MercuryApplication.WorkQueueGet (selectedWorkQueueId, true);


                // LOAD QUEUE SUMMARY

                WorkQueueMonitorSummaryGrid.DataSource = MercuryApplication.WorkQueueMonitorSummary ();

                InitializeWorkQueueAgingChart ();

            }

            else {


                // FORCE CLIENT-SIDE REPAINT AFTER AJAX CONTROL UPDATES

                Master.TelerikAjaxManagerControl.ResponseScripts.Add ("WorkQueueMonitor_OnPaint ();");

            }

            return;

        }

        #endregion


        #region Initializations

        private void InitializeWorkQueueAgingChart () {

            WorkQueueAgingName.Text = WorkQueueSelected.Name;


            Dictionary<String, Int64> availableItems = MercuryApplication.WorkQueueMonitorAgingAvailable (WorkQueueSelected.Id);

            Dictionary<String, Int64> openItems = MercuryApplication.WorkQueueMonitorAging (WorkQueueSelected.Id);


            System.Data.DataTable itemsTable = new System.Data.DataTable ();

            itemsTable.Columns.Add ("Key");

            itemsTable.Columns.Add ("AvailableCount");

            itemsTable.Columns.Add ("OpenCount");


            foreach (String currentKey in availableItems.Keys) {

                itemsTable.Rows.Add (currentKey, availableItems[currentKey], openItems[currentKey]);

            }


            //WorkQueueAgingChart.DataSource = itemsTable;

            //WorkQueueAgingChart.DataBind ();


            //WorkQueueAgingChart.PlotArea.Appearance.Dimensions.Margins = new Telerik.Charting.Styles.ChartMargins (5, 5, 105, 50);


            //for (Int32 currentSeriesIndex = 0; currentSeriesIndex <= 1; currentSeriesIndex++) {

            //    WorkQueueAgingChart.Series[currentSeriesIndex].Type = Telerik.Charting.ChartSeriesType.Bar;

            //    WorkQueueAgingChart.Series[currentSeriesIndex].Appearance.Shadow.Blur = 5;

            //    WorkQueueAgingChart.Series[currentSeriesIndex].Appearance.Shadow.Distance = 3;

                           
            //    for (Int32 currentItemIndex = 0; currentItemIndex <= 6; currentItemIndex++) {

            //        WorkQueueAgingChart.Series[currentSeriesIndex].Items[currentItemIndex].Appearance.FillStyle.MainColor = System.Drawing.Color.Red;

            //        WorkQueueAgingChart.Series[currentSeriesIndex].Items[currentItemIndex].Appearance.FillStyle.SecondColor = System.Drawing.Color.DarkRed;

            //    }

            //    WorkQueueAgingChart.Series[currentSeriesIndex].Items[7].Appearance.FillStyle.MainColor = System.Drawing.Color.Blue;

            //    WorkQueueAgingChart.Series[currentSeriesIndex].Items[7].Appearance.FillStyle.SecondColor = System.Drawing.Color.DarkBlue;

            //    for (Int32 currentItemIndex = 8; currentItemIndex <= 10; currentItemIndex++) {

            //        WorkQueueAgingChart.Series[currentSeriesIndex].Items[currentItemIndex].Appearance.FillStyle.MainColor = System.Drawing.Color.Yellow;

            //        WorkQueueAgingChart.Series[currentSeriesIndex].Items[currentItemIndex].Appearance.FillStyle.SecondColor = System.Drawing.Color.SandyBrown;

            //    }

            //    for (Int32 currentItemIndex = 11; currentItemIndex <= 13; currentItemIndex++) {

            //        WorkQueueAgingChart.Series[currentSeriesIndex].Items[currentItemIndex].Appearance.FillStyle.MainColor = System.Drawing.Color.Green;

            //        WorkQueueAgingChart.Series[currentSeriesIndex].Items[currentItemIndex].Appearance.FillStyle.SecondColor = System.Drawing.Color.DarkGreen;

            //    }

            //}


            //WorkQueueAgingChart.PlotArea.Appearance.Dimensions.Margins.Top = new Telerik.Charting.Styles.Unit (10, Telerik.Charting.Styles.UnitType.Pixel);

            //WorkQueueAgingChart.PlotArea.Appearance.Dimensions.Margins.Right = new Telerik.Charting.Styles.Unit (10, Telerik.Charting.Styles.UnitType.Pixel);

            //WorkQueueAgingChart.PlotArea.Appearance.Dimensions.Margins.Bottom = new Telerik.Charting.Styles.Unit (100, Telerik.Charting.Styles.UnitType.Pixel);




            System.Data.DataView itemsView = new System.Data.DataView (itemsTable);

            WorkQueueAgingChartAsp.Series["AvailableSeries"].Points.DataBindXY (itemsView, "Key", itemsView, "AvailableCount");

            WorkQueueAgingChartAsp.Series["OpenSeries"].Points.DataBindXY (itemsView, "Key", itemsView, "OpenCount");



            for (Int32 currentSeriesIndex = 0; currentSeriesIndex <= 1; currentSeriesIndex++) {

                for (Int32 currentItemIndex = 0; currentItemIndex <= 6; currentItemIndex++) {

                    WorkQueueAgingChartAsp.Series[currentSeriesIndex].Points[currentItemIndex].Color = System.Drawing.Color.Red;

                    WorkQueueAgingChartAsp.Series[currentSeriesIndex].Points[currentItemIndex].BackSecondaryColor = System.Drawing.Color.DarkRed;

                }

                WorkQueueAgingChartAsp.Series[currentSeriesIndex].Points[7].Color = System.Drawing.Color.Blue;

                WorkQueueAgingChartAsp.Series[currentSeriesIndex].Points[7].BackSecondaryColor = System.Drawing.Color.DarkBlue;

                for (Int32 currentItemIndex = 8; currentItemIndex <= 10; currentItemIndex++) {

                    WorkQueueAgingChartAsp.Series[currentSeriesIndex].Points[currentItemIndex].Color = System.Drawing.Color.Yellow;

                    WorkQueueAgingChartAsp.Series[currentSeriesIndex].Points[currentItemIndex].BackSecondaryColor = System.Drawing.Color.SandyBrown;

                }

                for (Int32 currentItemIndex = 11; currentItemIndex <= 13; currentItemIndex++) {

                    WorkQueueAgingChartAsp.Series[currentSeriesIndex].Points[currentItemIndex].Color = System.Drawing.Color.Green;

                    WorkQueueAgingChartAsp.Series[currentSeriesIndex].Points[currentItemIndex].BackSecondaryColor = System.Drawing.Color.DarkGreen;

                }

            }


            return;

        }

        #endregion


        #region Control Events

        protected void WorkQueueMonitorSummaryGrid_OnSelectedIndexChanged (Object sender, EventArgs e) {

            if (WorkQueueMonitorSummaryGrid.SelectedItems.Count == 0) { return; }

            Telerik.Web.UI.GridDataItem dataItem = (Telerik.Web.UI.GridDataItem) WorkQueueMonitorSummaryGrid.SelectedItems[0];


            Int64 selectedWorkQueueId = Convert.ToInt64 (dataItem.GetDataKeyValue ("Id"));

            WorkQueueSelected = MercuryApplication.WorkQueueGet (selectedWorkQueueId, true);


            // LOAD QUEUE SUMMARY

            WorkQueueMonitorSummaryGrid.DataSource = MercuryApplication.WorkQueueMonitorSummary ();

            InitializeWorkQueueAgingChart ();

            return;

        }

        protected void WorkQueueSummaryRefresh_OnClick (Object sender, EventArgs e) {

            WorkQueueMonitorSummaryGrid.DataSource = MercuryApplication.WorkQueueMonitorSummary ();

            WorkQueueMonitorSummaryGrid.Rebind (); // MCM-1157: MISSING REBIND STATEMENT

            InitializeWorkQueueAgingChart ();

            return;

        }

        #endregion 

    }

}
