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

namespace Mercury.Server {

    public class CommonFunctions {

        #region Property Functions

        public static String SetValueInRange (String value, String allowedValues, String defaultValue) {

            String returnValue = String.Empty;

            String[] valueList = allowedValues.Split (';');

            for (Int32 currentIndex = 0; currentIndex < valueList.Length; currentIndex++) {

                if (value == valueList[currentIndex]) {

                    returnValue = value;

                    break;

                }

            }

            if (String.IsNullOrEmpty (returnValue)) { returnValue = defaultValue; }

            return returnValue;

        }

        public static String SetValueMaxLength (String value, Int32 maxLength) {

            if (value != null) { value.Trim (); } else { value = String.Empty; }

            return value.Substring (0, (value.Length > maxLength) ? maxLength : value.Length);

        }

        #endregion 
        

        public static Boolean IsGuid (String guid) {

            Boolean isGuid = false;

            if (!String.IsNullOrEmpty (guid)) {

                System.Text.RegularExpressions.Regex expression = new System.Text.RegularExpressions.Regex (@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

                isGuid = expression.IsMatch (guid);

            }

            return isGuid;

        }
        
        public static String EnumerationToString (Object value) {

            String enumerationString = value.ToString ();

            if (!String.IsNullOrEmpty (enumerationString)) {

                enumerationString = System.Text.RegularExpressions.Regex.Replace (enumerationString, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

                enumerationString = enumerationString.Trim ();

            }

            return enumerationString;

        }

        public static String PascalString (String value) {

            String enumerationString = value;

            if (!String.IsNullOrEmpty (enumerationString)) {

                enumerationString = System.Text.RegularExpressions.Regex.Replace (enumerationString, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

                enumerationString = enumerationString.Trim ();

            }

            return enumerationString;

        }

        public static Int32 CurrentAge (DateTime? forBirthDate) {

            if (!forBirthDate.HasValue) { return -1; }

            Int32 currentAge = 0;

            DateTime birthDay;


            currentAge = DateTime.Today.Year - forBirthDate.Value.Year;

            birthDay = new DateTime (DateTime.Today.Year, forBirthDate.Value.Month, (((forBirthDate.Value.Month == 2) && (forBirthDate.Value.Day == 29)) ? 28 : forBirthDate.Value.Day));

            if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

            return currentAge;

        }

    }

}
