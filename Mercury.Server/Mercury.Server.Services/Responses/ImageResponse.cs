using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Mercury.Server.Services.Responses {

    [DataContract (Name = "ImageResponse")]
    public class ImageResponse : ResponseBase {

        #region Public Properties

        [DataMember (Name = "ImageName")]
        private String imageName = String.Empty;

        [DataMember (Name = "Extension")]
        private String extension;

        [DataMember (Name = "MimeType")]
        private String mimeType;

        [DataMember (Name = "ImageBase64")]
        private String imageBase64 = String.Empty;


        #endregion 


        #region Public Properties

        public String ImageName { get { return imageName; } set { imageName = value; } }

        public String Extension { get { return extension; } set { extension = value; } }

        public String MimeType { get { return mimeType; } set { mimeType = value; } }

        public String ImageBase64 { get { return imageBase64; } set { imageBase64 = value; } }

        public System.IO.MemoryStream Image {

            get {

                if (String.IsNullOrWhiteSpace (imageBase64)) { return new System.IO.MemoryStream (); }

                return new System.IO.MemoryStream (Convert.FromBase64String (imageBase64)); 
            
            }

            set { imageBase64 = Convert.ToBase64String (value.ToArray ()); }

        }

        #endregion 


        #region Public Constructors

        public ImageResponse () { /* DO NOTHING */ }

        public ImageResponse (Public.ImageStream imageStream) {

            if (imageStream.Image != null) {

                imageName = imageStream.Name;

                mimeType = imageStream.MimeType;

                Image = imageStream.Image;

            }

            return;

        }

        #endregion 

    }

}