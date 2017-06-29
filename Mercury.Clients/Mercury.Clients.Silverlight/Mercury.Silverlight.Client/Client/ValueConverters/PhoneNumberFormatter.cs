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

    public class PhoneNumberFormatter : System.Windows.Data.IValueConverter {

        public Object Convert (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            String numberString = (value != null) ? value.ToString () : String.Empty;
                
            numberString = System.Text.RegularExpressions.Regex.Replace (numberString, @".*?(\d{3}).*?(\d{3}).*?(\d{4}).*", "($1) $2-$3");

            return numberString;

        }

        public Object ConvertBack (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            String numberString = value.ToString ();

            numberString.Replace ("(", "");

            numberString.Replace (")", "");

            numberString.Replace ("-", "");

            numberString.Replace (" ", "");

            return numberString;

        }
                

    }

}
