using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Pdf {

    public class PdfFontDescriptor {

        #region Private Properties

        private Int32 pdfObjectId = 0;

        private String fontName = String.Empty;

        private Int32 fontFileObjectId = 0;


        #endregion 


        #region Public Properties

        public Int32 PdfObjectId { get { return pdfObjectId; } set { pdfObjectId = value; } }

        public String FontName { get { return fontName; } set { fontName = value; } }

        public Int32 FontFileObjectId { get { return fontFileObjectId; } set { fontFileObjectId = value; } }

        #endregion 


        #region Constructors 

        public PdfFontDescriptor () { /* DO NOTHING */ }

        public PdfFontDescriptor (String descriptor) {


            return;

        }

        #endregion 


    }

}
