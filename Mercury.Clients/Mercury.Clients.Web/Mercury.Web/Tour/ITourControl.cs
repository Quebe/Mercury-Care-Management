using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Web.Tour {

    public interface ITourControl {

        #region Public Properties

        Boolean HasNext { get; }

        Boolean HasPrevious { get; }

        #endregion 


        #region Events

        event TourControlPageChangedEventHandler TourPageChanged; // THIS EVENT GOES FROM THE TOUR PAGE TO THE MASTER PAGE

        void TourPrevious_OnClick (Object sender, EventArgs e); // THIS EVENT GOES FROM THE MASTER PAGE TO THE TOUR PAGE

        void TourNext_OnClick (Object sender, EventArgs e); // THIS EVENT GOES FROM THE MASTER PAGE TO THE TOUR PAGE
        
        #endregion 



    }

    public delegate void TourControlPageChangedEventHandler (Object sender, TourControlPageChangedEventArgs  e);

    public class TourControlPageChangedEventArgs : EventArgs {

        private String tourUserControl = String.Empty;

        private String focusControlClientId = String.Empty;

        private String responseScript = String.Empty;

        public String TourUserControl { get { return tourUserControl; } set { tourUserControl = value; } }

        public String FocusControlClientId { get { return focusControlClientId; } set { focusControlClientId = value; } }

        public String ResponseScript { get { return responseScript; } set { responseScript = value; } }

        public TourControlPageChangedEventArgs (String forTourUserControl, String forFocusControlClientId) { 
            
            tourUserControl = forTourUserControl;

            focusControlClientId = forFocusControlClientId;

            return;
        
        }

    }

}