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

    public class DateToAgeInMonthsString : System.Windows.Data.IValueConverter {

        public Object Convert (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            String convertedValue = String.Empty;

            if ((value != null) && (value is DateTime)) {

                Int32 currentAge = 0;

                DateTime birthDate = (DateTime) value;

                DateTime birthDay;


                currentAge = DateTime.Today.Month - birthDate.Month;

                currentAge = currentAge + (12 * (DateTime.Today.Year - birthDate.Year));

                birthDay = new DateTime (DateTime.Today.Year, birthDate.Month, (((birthDate.Month == 2) && (birthDate.Day == 29)) ? 28 : birthDate.Day));

                if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

                convertedValue = currentAge.ToString () + "m";

            }

            return convertedValue;

        }

        public Object ConvertBack (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            throw new ArgumentException ("DateToAgeInMonthsString.ConvertBack not implemented.");

        }

    }

}
