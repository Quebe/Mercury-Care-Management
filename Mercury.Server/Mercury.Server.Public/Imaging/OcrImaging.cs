using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Public.Imaging {

    public class OcrImaging {

        #region Public Static Methods

        private static Double Distance3dDouble (Double x1, Double y1, Double z1, Double x2, Double y2, Double z2) {

            //Our end result
            Double result = 0;

            //Take x2-x1, then square it
            double part1 = Math.Pow ((x2 - x1), 2);

            //Take y2-y1, then sqaure it
            double part2 = Math.Pow ((y2 - y1), 2);

            //Take z2-z1, then square it
            double part3 = Math.Pow ((z2 - z1), 2);

            //Add both of the parts together
            double underRadical = part1 + part2 + part3;

            //Get the square root of the parts
            result = Math.Sqrt (underRadical);

            //Return our result
            return result;

        }

        public static System.Drawing.Bitmap DropoutColors (System.Drawing.Bitmap sourceImage, System.Drawing.Color referenceColor, System.Drawing.Color thresholdColor) {

            DateTime startTime = DateTime.Now;

            System.Drawing.Bitmap destinationImage = new System.Drawing.Bitmap (sourceImage.Width, sourceImage.Height);

            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage (destinationImage);

            graphics.FillRectangle (new System.Drawing.SolidBrush (System.Drawing.Color.White), new System.Drawing.Rectangle (0, 0, destinationImage.Width, destinationImage.Height));

            System.Diagnostics.Debug.WriteLine ("DropoutColors.Fill: " + DateTime.Now.Subtract (startTime).TotalMilliseconds);

            YCbCrByte referencePoint = new YCbCrByte (referenceColor);

            YCbCrByte thresholdPoint = new YCbCrByte (thresholdColor);


            // 3d Space Threshold Distance

            // Double thresholdDistance = Distance3dDouble (referencePoint.Y, referencePoint.Cb, referencePoint.Cr, thresholdPoint.Y, thresholdPoint.Cb, thresholdPoint.Cr);


            // Integer-based Threshold Distance

            Int32 thresholdDistanceY = Math.Abs (thresholdPoint.Y - referencePoint.Y);

            Int32 thresholdDistanceCb = Math.Abs (thresholdPoint.Cb - referencePoint.Cb);

            Int32 thresholdDistanceCr = Math.Abs (thresholdPoint.Cr - referencePoint.Cr);


            for (Int32 currentY = 0; currentY < sourceImage.Height; currentY++) {

                for (Int32 currentX = 0; currentX < sourceImage.Width; currentX++) {

                    System.Drawing.Color currentPixel = sourceImage.GetPixel (currentX, currentY);

                    Server.Public.Imaging.YCbCrByte transformedPixel = new Server.Public.Imaging.YCbCrByte (currentPixel);


                    #region 3d Space Distance (floating point)

                    //Double pixelDistance = Distance3dDouble (referencePoint.Y, referencePoint.Cb, referencePoint.Cr, transformedPixel.Y, transformedPixel.Cb, transformedPixel.Cr);

                    //if (pixelDistance > thresholdDistance) { destinationImage.SetPixel (currentX, currentY, System.Drawing.Color.White); }
                    
                    //else { destinationImage.SetPixel (currentX, currentY, System.Drawing.Color.Black); }

                    #endregion 


                    #region Integer-based Distance Formula;


                    Int32 pixelDistanceY = Math.Abs (transformedPixel.Y - referencePoint.Y);

                    Int32 pixelDistanceCb = Math.Abs (transformedPixel.Cb - referencePoint.Cb);

                    Int32 pixelDistanceCr = Math.Abs (transformedPixel.Cr - referencePoint.Cr);

                    Boolean isWithinThreshold = (pixelDistanceY <= thresholdDistanceY) && (pixelDistanceCb <= thresholdDistanceCb) && (pixelDistanceCr <= thresholdDistanceCr);


                    if (isWithinThreshold) { destinationImage.SetPixel (currentX, currentY, System.Drawing.Color.Black); }

                    // else { destinationImage.SetPixel (currentX, currentY, System.Drawing.Color.White); }

                    #endregion 

                }

            }

            System.Diagnostics.Debug.WriteLine ("DropoutColors: " + DateTime.Now.Subtract (startTime).TotalMilliseconds);

            return destinationImage;

        }

        public static System.Drawing.Color AverageColor (System.Drawing.Bitmap image) {

            Int32 colorR = 0;

            Int32 colorG = 0;

            Int32 colorB = 0;

            Int32 totalPixels = 0;


            for (Int32 currentY = 0; currentY < image.Height; currentY++) {

                for (Int32 currentX = 0; currentX < image.Width; currentX++) {

                    System.Drawing.Color currentPixel = image.GetPixel (currentX, currentY);

                    colorR += currentPixel.R;

                    colorG += currentPixel.G;

                    colorB += currentPixel.B;

                    totalPixels = totalPixels + 1;

                }

            }


            colorR = colorR / totalPixels;

            colorG = colorG / totalPixels;

            colorB = colorB / totalPixels;
            

            return System.Drawing.Color.FromArgb (colorR, colorG, colorB);

        }

        #endregion 

    }

}
