using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Reporting {

    public interface IReportingServer {

        #region Public Methods

        ImageStream Render (WebService.WebServiceHostConfiguration webServiceHostConfiguration, String reportName, Dictionary<String, String> reportParameters, String format, Dictionary<String, String> extendedProperties);

        #endregion 

    }

}
