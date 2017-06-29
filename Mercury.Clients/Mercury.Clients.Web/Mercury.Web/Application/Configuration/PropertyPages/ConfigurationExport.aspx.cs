using System;
using System.Collections;
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

    public partial class ConfigurationExport : System.Web.UI.Page {

        protected Mercury.Client.Application application;

        protected System.Xml.XmlDocument configurationDocumentXml;

        protected String configurationDocument;


        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        protected void Page_Load (object sender, EventArgs e) {

            if (Session["Mercury.Application"] == null) { Response.RedirectLocation = "/SessionExpired.aspx"; return; }

            application = (Mercury.Client.Application) Session["Mercury.Application"];

            if (!application.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationImportExport)) {

                if (!IsPostBack) { Server.Transfer ("/PermissionDenied.aspx"); }

                else { Response.RedirectLocation = "/PermissionDenied.aspx"; }

                return;

            }

            if (!IsPostBack) {

                String configurationType = Request.QueryString["ConfigurationType"];

                String fileName = "MercuryConfiguration.Xml";

                Int64 configurationId = 0;

                Int64.TryParse (Request.QueryString ["ConfigurationId"], out configurationId);

                if ((String.IsNullOrEmpty (configurationType)) || (configurationId == 0)) { Server.Transfer ("/PermissionDenied.aspx"); return; }


                Client.Core.CoreObject exportObject = null;

                switch (configurationType) {

                    case "ReportingServer": exportObject = MercuryApplication.ReportingServerGet (configurationId, false); break;

                    case "FaxServer": exportObject = MercuryApplication.FaxServerGet (configurationId, false); break;

                    case "NoteType": exportObject = MercuryApplication.NoteTypeGet (configurationId, false); break;

                    case "ContactRegarding": exportObject = MercuryApplication.ContactRegardingGet (configurationId, false); break;

                    case "Correspondence": exportObject = MercuryApplication.CorrespondenceGet (configurationId, false); break;

                    case "Form": exportObject = MercuryApplication.FormGet (configurationId); break;

                    case "Workflow": exportObject = MercuryApplication.WorkflowGet(configurationId, false); break;

                    case "WorkQueue": exportObject = MercuryApplication.WorkQueueGet(configurationId, false); break;

                    case "WorkQueueView": exportObject = MercuryApplication.WorkQueueViewGet(configurationId, false); break;

                    case "WorkOutcome": exportObject = MercuryApplication.WorkOutcomeGet(configurationId, false); break;

                    case "RoutingRule": exportObject = MercuryApplication.RoutingRuleGet(configurationId); break;

                    case "Service": exportObject = MercuryApplication.MedicalServiceGet(configurationId, false); break;

                    case "Metric": exportObject = MercuryApplication.MetricGet (configurationId, false); break;

                    case "AuthorizedService": exportObject = MercuryApplication.AuthorizedServiceGet (configurationId); break;

                    case "PopulationType": exportObject = MercuryApplication.PopulationTypeGet(configurationId, false); break;

                    case "Population": exportObject = MercuryApplication.PopulationGet(configurationId, false); break;

                    case "CareMeasureScale": exportObject = MercuryApplication.CareMeasureScaleGet (configurationId, false); break;

                    case "CareMeasure": exportObject = MercuryApplication.CareMeasureGet (configurationId, false); break;

                    case "CareIntervention": exportObject = MercuryApplication.CareInterventionGet (configurationId, false); break;

                    case "CarePlan": exportObject = MercuryApplication.CarePlanGet (configurationId, false); break;

                    case "CareOutcome": exportObject = MercuryApplication.CareOutcomeGet (configurationId, false); break;

                    case "ProblemStatement": exportObject = MercuryApplication.ProblemStatementGet (configurationId, false); break;
                                            
                    default: Server.Transfer ("/PermissionDenied.aspx"); break;

                }

                if (exportObject == null) { Server.Transfer ("/PermissionDenied.aspx"); return; }

                System.Xml.XmlDocument xmlDocument = MercuryApplication.CoreObject_XmlSerialize ((Mercury.Server.Application.CoreObject)exportObject.ToServerObject ());

                configurationDocument = xmlDocument.OuterXml;
                   

                switch (configurationType) { 

                    case "ProblemStatement": fileName = ((Client.Core.Individual.ProblemStatement) exportObject).Classification + exportObject.Name + ".xml"; break;

                    default: fileName = exportObject.Name + ".xml"; break;

                }

                Response.Clear ();

                Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                Response.ContentType = "application/octet-stream";

                Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                Response.End ();

                //    case "WorkTeam":

                //        #region Work Team

                //        configurationDocument = application.XmlSerializeWorkTeam (configurationId).InnerXml;

                //        if ((application.LastException) == null) {

                //            fileName = application.WorkTeamGet (configurationId, false).Name + ".xml";

                //            Response.Clear ();

                //            Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                //            Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                //            Response.ContentType = "application/octet-stream";

                //            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                //            Response.End ();

                //        }

                //        else { Server.Transfer ("/PermissionDenied.aspx"); }

                //        #endregion

                //        break;

                //    case "WorkQueue":

                //        #region Work Queue

                //         configurationDocument = application.XmlSerializeWorkQueue(configurationId).InnerXml;

                //        if ((application.LastException) == null) {

                //            fileName = application.WorkQueueGet (configurationId, false).Name + ".xml";

                //            Response.Clear ();

                //            Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                //            Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                //            Response.ContentType = "application/octet-stream";

                //            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                //            Response.End ();

                //        }

                //        else { Server.Transfer ("/PermissionDenied.aspx"); }

                //        #endregion

                //        break;

                //    case "RoutingRule":

                //        #region Work Queue

                //        configurationDocument = application.XmlSerializeRoutingRule (configurationId).InnerXml;

                //        if ((application.LastException) == null) {

                //            fileName = application.RoutingRuleGet (configurationId).Name + ".xml";

                //            Response.Clear ();

                //            Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                //            Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                //            Response.ContentType = "application/octet-stream";

                //            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                //            Response.End ();

                //        }

                //        else { Server.Transfer ("/PermissionDenied.aspx"); }

                //        #endregion

                //        break;

                //    case "Metric":

                //        #region Metric

                //        configurationDocument = application.XmlSerializeMetric (configurationId).InnerXml;

                //        if ((application.LastException) == null) {

                //            fileName = application.MetricGet (configurationId).Name + ".xml";

                //            Response.Clear ();

                //            Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                //            Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                //            Response.ContentType = "application/octet-stream";

                //            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                //            Response.End ();

                //        }

                //        else { Server.Transfer ("/PermissionDenied.aspx"); }

                //        #endregion

                //        break;

                //    case "Service":

                //        #region Service

                //        configurationDocument = application.XmlSerializeService (configurationId).InnerXml;

                //        if ((application.LastException) == null) {

                //            fileName = application.MedicalServiceGet (configurationId, false).Name + ".xml";

                //            Response.Clear ();

                //            Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                //            Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                //            Response.ContentType = "application/octet-stream";

                //            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                //            Response.End ();

                //        }

                //        else { Server.Transfer ("/PermissionDenied.aspx"); }

                //        #endregion 

                //        break;

                //    case "PopulationType":

                //        #region PopulationType

                //        configurationDocument = application.XmlSerializePopulationType (configurationId).InnerXml;

                //        if ((application.LastException) == null) {

                //            fileName = application.PopulationTypeGet (configurationId, false).Name + ".xml";

                //            Response.Clear ();

                //            Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                //            Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                //            Response.ContentType = "application/octet-stream";

                //            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                //            Response.End ();

                //        }

                //        else { Server.Transfer ("/PermissionDenied.aspx"); }

                //        #endregion

                //        break;

                //    case "Population":

                //        #region Population

                //        configurationDocument = application.XmlSerializePopulation (configurationId).InnerXml;

                //        if ((application.LastException) == null) {

                //            fileName = application.PopulationGet (configurationId, false).Name + ".xml";

                //            Response.Clear ();

                //            Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                //            Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                //            Response.ContentType = "application/octet-stream";

                //            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                //            Response.End ();

                //        }

                //        else { Server.Transfer ("/PermissionDenied.aspx"); }

                //        #endregion

                //        break;

                //    case "Form":

                //        #region Form

                //        configurationDocument = application.XmlSerializeForm (configurationId).InnerXml;

                //        if ((application.LastException) == null) {

                //            fileName = application.FormGet (configurationId).Name + ".xml";

                //            Response.Clear ();

                //            Response.AddHeader ("Content-Disposition", "attachment; filename=" + fileName);

                //            Response.AddHeader ("Content-Length", configurationDocument.Length.ToString ());

                //            Response.ContentType = "application/octet-stream";

                //            Response.OutputStream.Write (new System.Text.ASCIIEncoding ().GetBytes (configurationDocument.ToCharArray ()), 0, configurationDocument.Length);

                //            Response.End ();

                //        }

                //        else { Server.Transfer ("/PermissionDenied.aspx"); }

                //        #endregion

                //        break;

                //    default: 

                //        Server.Transfer ("/PermissionDenied.aspx"); 

                //        break;

                //}

            }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

    }

}
