using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client {

    public class CommonFunctions {

        public static SortedDictionary<String, Int64> ReferenceDictionarySortByValue (Dictionary<Int64, String> sourceDictionary) {

            SortedDictionary<String, Int64> sortedDictionary = new SortedDictionary<String, Int64> ();

            foreach (Int64 currentKey in sourceDictionary.Keys) {

                sortedDictionary.Add (sourceDictionary[currentKey], currentKey);

            }

            return sortedDictionary;

        }

    }

}
