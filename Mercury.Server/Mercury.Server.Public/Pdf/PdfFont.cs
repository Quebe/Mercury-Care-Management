using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Pdf {

    public class PdfFont {
        
        #region Private Properties

        private Int32 pdfObjectId = 0;

        private String subtype = String.Empty;

        private String baseFont = String.Empty;

        private Int32 firstCharacter = 0;

        private Int32 lastCharacter = 255;

        private PdfFontDescriptor fontDescriptor = new PdfFontDescriptor ();

        #endregion 


        #region Public Properties

        public Int32 PdfObjectId { get { return pdfObjectId; } set { pdfObjectId = value; } }

        public String Subtype { get { return subtype; } set { subtype = value; } }

        public String BaseFont { get { return baseFont; } set { baseFont = value; } }

        public Int32 FirstCharacter { get { return firstCharacter; } set { firstCharacter = value; } }

        public Int32 LastCharacter { get { return lastCharacter; } set { lastCharacter = value; } }

        public PdfFontDescriptor FontDescriptor { get { return fontDescriptor; } set { fontDescriptor = value; } }

        #endregion 


        #region Constructors 

        public PdfFont () { /* DO NOTHING */ }

        public PdfFont (PdfFontDescriptor forFontDescriptor) {

            fontDescriptor = forFontDescriptor;

            return;

        }

        #endregion 

    }

}
