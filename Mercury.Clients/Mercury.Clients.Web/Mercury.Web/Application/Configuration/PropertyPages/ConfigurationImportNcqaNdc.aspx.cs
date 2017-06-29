using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Configuration.Windows {

    public partial class ConfigurationImportNcqaNdc : System.Web.UI.Page {

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }
        

        String SessionCachePrefix = String.Empty;

        String SessionCacheSuffix = String.Empty;

        protected Mercury.Client.Application application;

        protected System.Data.DataTable ndcImportTable;



        protected void Page_Load (object sender, EventArgs e) {

            application = MercuryApplication;

            if (!application.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationImportExport)) {

                if (!IsPostBack) { Server.Transfer ("/PermissionDenied.aspx"); }

                else { Response.RedirectLocation = "/PermissionDenied.aspx"; }

                return;

            }


            if ((application != null) && (!Page.IsPostBack)) {

                #region Initial Page Load

                SessionCachePrefix = Form.Name;

                PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", "");

                SessionCacheSuffix = PageInstanceId.Text;

                #endregion

            } // Initial Page Load

            else { // Postback

                SessionCachePrefix = Form.Name;

                SessionCacheSuffix = PageInstanceId.Text;

                ndcImportTable = (System.Data.DataTable) Session[SessionCachePrefix + "NdcImportTable" + SessionCacheSuffix];

            }

            return;

        }



        protected void ButtonUploadFile_OnClick (Object sender, EventArgs eventArgs) {

            Boolean validFile = true;
            

            ndcImportTable = new DataTable ();

            List<String> ndcCategory = new List<String> ();


            SaveResponseLabel.Text = String.Empty;

            if (TelerikUpload.UploadedFiles.Count != 1) {

                validFile = false;

                SaveResponseLabel.Text = "Invalid number of files detected.";

            }

            else {

                try {

                    System.IO.StreamReader textReader = new System.IO.StreamReader (TelerikUpload.UploadedFiles [0].InputStream);


                    // 2009 FORMAT 

                    //ndcImportTable.Columns.Add ("ndc_code");

                    //ndcImportTable.Columns.Add ("brand_name");

                    //ndcImportTable.Columns.Add ("generic_product_name");

                    //ndcImportTable.Columns.Add ("route");

                    //ndcImportTable.Columns.Add ("category");

                    //ndcImportTable.Columns.Add ("obsolete_date"); 

                    //ndcImportTable.Columns.Add ("drug_id");

                    // 2011 FORMAT

                    ndcImportTable.Columns.Add ("NDC Code");

                    ndcImportTable.Columns.Add ("Brand Name");

                    ndcImportTable.Columns.Add ("Generic Product Name");

                    ndcImportTable.Columns.Add ("Route");

                    ndcImportTable.Columns.Add ("Category");

                    ndcImportTable.Columns.Add ("Drug ID");


                    String header = textReader.ReadLine (); // read header line

                    Dictionary <Int32, String> headerColumns = new Dictionary<Int32,String> ();


                    Int32 currentColumnIndex = 0;

                    foreach (String currentColumnName in header.Split ('\t')) {

                        headerColumns.Add (currentColumnIndex, currentColumnName);

                        currentColumnIndex = currentColumnIndex + 1;

                    }





                    String inputLine = textReader.ReadLine ();
                    
                    while (inputLine != null) {

                        currentColumnIndex = 0;

                        System.Data.DataRow importRow = ndcImportTable.Rows.Add (String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);

                        foreach (String currentColumn in inputLine.Split ('\t')) {

                            importRow[headerColumns[currentColumnIndex]] = currentColumn;

                            currentColumnIndex = currentColumnIndex + 1;

//                            if (currentColumnIndex == importRow.Table.Columns.Count) { break; }

                        }

                        inputLine = textReader.ReadLine  ();

                    }


                }

                catch (Exception applicationException) {

                    System.Diagnostics.Debug.WriteLine (applicationException.Message);

                    validFile = false;

                    SaveResponseLabel.Text = "Invalid Configuration File.";

                }

            }


            if (!validFile) {

                FileUploadSection.Style.Add ("display", "inline");

                ConfigurationProcessSection.Style.Add ("display", "none");

            }

            else {

                TelerikUpload.UploadedFiles.Clear ();

                Session[SessionCachePrefix + "NdcImportTable" + SessionCacheSuffix] = ndcImportTable;


                System.Data.DataTable importResultsTable = new DataTable ();

                importResultsTable.Columns.Add ("ObjectType");

                importResultsTable.Columns.Add ("ObjectName");

                importResultsTable.Columns.Add ("Success");

                importResultsTable.Columns.Add ("Exception");

                importResultsTable.Columns.Add ("Id");

                foreach (System.Data.DataRow currentRow in ndcImportTable.Rows) {

                    String categoryName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase (currentRow["category"].ToString ());

                    categoryName = categoryName.Replace ("\"", "");

                    String serviceName = "Medication - " + categoryName;

                    if (!ndcCategory.Contains (serviceName)) {

                        ndcCategory.Add (serviceName);

                        importResultsTable.Rows.Add (

                            "Singleton", 

                            serviceName,

                            String.Empty, 
                            
                            String.Empty, 
                            
                            application.MedicalServiceGetIdByName (serviceName)

                        );

                    }

                }

                System.Data.DataView importResultsView = new DataView (importResultsTable);

                importResultsView.Sort = "ObjectType, ObjectName";

                ImportResultsGrid.DataSource = importResultsView;

                ImportResultsGrid.Rebind ();

                ButtonImport.Visible = true;

            }

            return;

        }

        protected void ButtonImport_OnClick (Object sender, EventArgs eventArgs) {

            System.Data.DataView sortedNdcView = new DataView (ndcImportTable);

            // sortedNdcView.Sort = "category, ndc_code"; // 2009

            sortedNdcView.Sort = "[Category], [NDC Code]"; // 2011

            Boolean allowOverwrite = false;

            Boolean allowMerge = true;

            Boolean processNdc = false;

            Boolean serviceProcess = true;



            String currentServiceName = String.Empty;

            Client.Core.MedicalServices.ServiceSingleton currentSingleton = null;

            Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition currentSingletonDefinition = null;



            System.Data.DataTable importResultsTable = new DataTable ();

            importResultsTable.Columns.Add ("ObjectType");

            importResultsTable.Columns.Add ("ObjectName");

            importResultsTable.Columns.Add ("Success");

            importResultsTable.Columns.Add ("Exception");

            importResultsTable.Columns.Add ("Id");



            foreach (System.Data.DataRow currentRow in sortedNdcView.ToTable ().Rows) {

                String categoryName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase (currentRow["category"].ToString ());

                categoryName = categoryName.Replace ("\"", "");

                String serviceName = "Medication - " + categoryName;

                if (currentServiceName != serviceName) {

                    #region New Services based on Change in Medication Name

                    if (currentSingleton != null) {

                        #region If Working on Previous Service, Save before Continuing

                        if (currentSingletonDefinition != null) {

                            if (!String.IsNullOrEmpty (currentSingletonDefinition.NdcCodeCriteria)) {

                                currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("}{", ", ");

                                currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("{", "");

                                currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("}", "");

                                currentSingletonDefinition.NdcCodeCriteria.Trim ();

                                if (!String.IsNullOrWhiteSpace (currentSingletonDefinition.NdcCodeCriteria)) {

                                    currentSingleton.Definitions.Add (currentSingletonDefinition);

                                }
                        
                            }

                        }

                        Mercury.Server.Application.ObjectSaveResponse saveResponse = application.MedicalServiceSave (currentSingleton);

                        importResultsTable.Rows.Add (

                            "Singleton",

                            currentSingleton.Name,

                            saveResponse.Success,

                            (saveResponse.HasException) ? saveResponse.Exception.Message : String.Empty,

                            saveResponse.Id

                        );

                        #endregion 

                    }

                    serviceProcess = true;

                    currentServiceName = serviceName;

                    currentSingleton = application.MedicalServiceSingletonGet (application.MedicalServiceGetIdByName (currentServiceName));

                    if (currentSingleton == null) {

                        serviceProcess = true;

                        currentSingleton = new Mercury.Client.Core.MedicalServices.ServiceSingleton (application);

                        currentSingleton.Name = currentServiceName;

                        currentSingleton.Description = currentServiceName;

                        currentSingleton.ServiceType = Mercury.Server.Application.MedicalServiceType.Singleton;

                        currentSingleton.ServiceClassification = Mercury.Server.Application.ServiceClassification.Medication;

                    }

                    else {

                        serviceProcess = allowOverwrite || allowMerge;

                        if (allowOverwrite) {

                            foreach (Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition currentDefinition in currentSingleton.Definitions) {

                                currentDefinition.Enabled = false;

                            }

                        }


                    }

                    currentSingletonDefinition = null;

                    #endregion

                }


                if (currentSingletonDefinition != null) {

                    #region Current Definition Exists for Appending, check Length for cutoff

                    if (currentSingletonDefinition.NdcCodeCriteria.Length > 800) {

                        currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("}{", ", ");

                        currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("{", "");

                        currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("}", "");

                        currentSingletonDefinition.NdcCodeCriteria.Trim ();

                        if (!String.IsNullOrWhiteSpace (currentSingletonDefinition.NdcCodeCriteria)) {

                            currentSingleton.Definitions.Add (currentSingletonDefinition);

                        }
                        
                        currentSingletonDefinition = null;

                    }

                    #endregion 

                }


                if (currentSingletonDefinition == null) { // MUST REMAIN AS ITS OWN VALIDATION SINCE A CUTOFF WILL SET CURRENT BACK TO NULL
                    
                    #region No Current Definition to Append To, Create New Definition

                    currentSingletonDefinition = new Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition (MercuryApplication);

                    currentSingletonDefinition.DataSourceType = Mercury.Server.Application.ServiceDataSourceType.Pharmacy;

                    #endregion
                }

                if (serviceProcess) {

                    // VALIDATE THAT THE SINGLETON DOES NOT ALREADY CONTAIN THE NDC IN ANOTHER DEFINITION

                    if (!currentSingleton.ContainsNdc (currentRow["NDC Code"].ToString ().Trim ())) {

                        // 2009: currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria + "{" + (currentRow["ndc_code"].ToString ().Trim ()) + "}";

                        currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria + "{" + (currentRow["NDC Code"].ToString ().Trim ()) + "}"; // 2011

                    }

                }

            }


            if ((currentSingleton != null) && (currentSingletonDefinition != null)) {

                #region SAVE ANY LEFT OVER SERVICE AND DEFINITION FROM LOOP

                currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("}{", ", ");

                currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("{", "");

                currentSingletonDefinition.NdcCodeCriteria = currentSingletonDefinition.NdcCodeCriteria.Replace ("}", "");

                currentSingletonDefinition.NdcCodeCriteria.Trim ();

                if (!String.IsNullOrWhiteSpace (currentSingletonDefinition.NdcCodeCriteria)) {

                    currentSingleton.Definitions.Add (currentSingletonDefinition);

                }


                Mercury.Server.Application.ObjectSaveResponse saveResponse = application.MedicalServiceSave (currentSingleton);

                importResultsTable.Rows.Add (

                    "Singleton",

                    currentSingleton.Name,

                    saveResponse.Success,

                    (saveResponse.HasException) ? saveResponse.Exception.Message : String.Empty,

                    saveResponse.Id

                );

                #endregion 

            }

            System.Data.DataView importResultsView = new DataView (importResultsTable);

            importResultsView.Sort = "ObjectType, ObjectName";

            ImportResultsGrid.DataSource = importResultsView;

            ImportResultsGrid.Rebind ();

            ButtonImport.Visible = false;


            return;

        }

        protected void ButtonClose_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

        }

    }

}

