using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Imaging {

    public class YCbCrByte {

        #region Private Properties

        private Byte y = 0;

        private Byte cb = 0;

        private Byte cr = 0;

        #endregion 


        #region Public Properties

        public Byte Y { get { return y; } set { y = value; } }

        public Byte Cb { get { return cb; } set { cb = value; } }

        public Byte Cr { get { return cr; } set { cr = value; } }

        #endregion 


        #region Constructors

        public YCbCrByte (System.Drawing.Color color) {

            y = Convert.ToByte (System.Math.Min (System.Math.Abs (color.R * 2104 + color.G * 4130 + color.B * 802 + 4096 + 131072) >> 13, 235));

            cb = Convert.ToByte (System.Math.Min (System.Math.Abs (color.R * -1214 + color.G * -2384 + color.B * 3598 + 4096 + 1048576) >> 13, 240));

            cr = Convert.ToByte (System.Math.Min (System.Math.Abs (color.R * -3598 + color.G * -3013 + color.B * -585 + 4096 + 1048576) >> 13, 240));

            return;

        }

        #endregion 

    }

}
