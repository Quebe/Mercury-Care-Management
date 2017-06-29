using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.ValueConverters {

    public class GenderDescriptionFormatter : System.Windows.Data.IValueConverter {

        public Object Convert (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            String convertedValue = String.Empty;

            if (value != null) {

                switch (value.ToString ()) {

                    case "M": convertedValue = "Male"; break;

                    case "F": convertedValue = "Female"; break;

                    default: convertedValue = "Unknown"; break;

                }

            }

            return convertedValue;

        }

        public Object ConvertBack (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            throw new ArgumentException ("GenderDescriptionFormatter.ConvertBack not implemented.");

        }

    }

}
