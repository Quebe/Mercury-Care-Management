﻿using System;
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

    public class TerminationDateToStringFormatter : System.Windows.Data.IValueConverter {

        public Object Convert (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            String convertedValue = String.Empty;

            if ((value != null) && (value is DateTime)) {

                if (((DateTime) value) == new DateTime (9999, 12, 31)) { convertedValue = "< active >"; }

                else { convertedValue = ((DateTime) value).ToString ("MM/dd/yyyy"); }

            }

            return convertedValue;

        }

        public Object ConvertBack (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            throw new ArgumentException ("DateToStringFormatter.ConvertBack not implemented.");

        }

    }

}
