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

    public class ExtendedPropertyValue : System.Windows.Data.IValueConverter {

        public Object Convert (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            String propertyName = String.Empty;

            Object propertyValue = null;

            String propertyDataType = "String";


            String parameterInput = System.Convert.ToString (parameter);

            if ((value is System.Collections.Generic.Dictionary<String, String>) && (parameterInput.Length > 0)) {

                System.Collections.Generic.Dictionary<String, String> extendedProperties = (System.Collections.Generic.Dictionary<String, String>) value;

                propertyName = parameterInput.Split ('|')[0];

                if (extendedProperties.ContainsKey (propertyName)) {

                    if (parameterInput.Split ('|').Length > 1) {

                        propertyDataType = parameterInput.Split ('|')[1];

                    }

                    switch (propertyDataType) {

                        default: propertyValue = extendedProperties[propertyName]; break;

                    }

                }

            }

            return propertyValue;

        }

        public Object ConvertBack (Object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            throw new ArgumentException ("DateToStringFormatter.ConvertBack not implemented.");

        }

    }

}
