using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Data {

    // Summary:
    //     Class representing a filter description   
    [DataContract (Name="DataFilterDescriptor")]
    public class FilterDescriptor {

        #region Private Properties

        [DataMember (Name = "IgnoreValue")]
        private Object ignoredValue = null;

        [DataMember (Name = "IsCaseSensitive")]
        private Boolean isCaseSensitive = false;

        [DataMember (Name = "Operator")]
        private Enumerations.DataFilterOperator filterOperator = Mercury.Server.Data.Enumerations.DataFilterOperator.IsEqualTo;

        [DataMember (Name = "PropertyPath")]
        private String propertyPath = String.Empty;

        [DataMember (Name = "Parameter")]
        private Parameter parameter = null;

        #endregion 


        #region Public Properties

        //
        // Summary:
        //     Gets or sets the value for the right operand that turns off this FilterDescriptor
        public object IgnoredValue { get { return ignoredValue; } set { ignoredValue = value; } }
        //
        // Summary:
        //     Gets or sets a value indicating whether the FilterDescriptor is case sensitive
        //     for string values
        public bool IsCaseSensitive { get { return isCaseSensitive; } set { isCaseSensitive = value; } }
        //
        // Summary:
        //     Gets or sets the filter operator
        public Enumerations.DataFilterOperator Operator { get { return filterOperator; } set { filterOperator = value; } }
        //
        // Summary:
        //     Gets or sets the name of the property path used as the left operand
        public string PropertyPath { get { return propertyPath; } set { propertyPath = value; } }
        //
        // Summary:
        //     Gets or sets the right operand
        public Parameter Parameter { get { return parameter; } set { parameter = value; } }




        public String SqlCriteriaString (String prefix) {

            prefix = prefix + ((String.IsNullOrEmpty (prefix)) ? String.Empty : ".");

            String sqlString = String.Empty;

            String valueString = String.Empty;


            if ((parameter.Value == null) && (filterOperator == Mercury.Server.Data.Enumerations.DataFilterOperator.IsEqualTo)) {

                sqlString = prefix + propertyPath + " IS NULL";

            }

            else if ((parameter.Value == null) && (filterOperator == Mercury.Server.Data.Enumerations.DataFilterOperator.IsNotEqualTo)) {

                sqlString = prefix + propertyPath + " IS NOT NULL";

            }

            else {

                if (parameter.Value is String) {

                    #region String Values

                    valueString = ((String) parameter.Value).Replace ("'", "''");

                    switch (filterOperator) {

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.Contains: valueString = " LIKE '%" + valueString + "%'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.StartsWith: valueString = " LIKE '" + valueString + "%'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.EndsWith: valueString = " LIKE '%" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsEqualTo: valueString = " = '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsNotEqualTo: valueString = " <> '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsGreaterThan: valueString = " > '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsGreaterThanOrEqualTo: valueString = " >= '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsLessThan: valueString = " < '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsLessThanOrEqualTo: valueString = " <= '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsContainedIn:

                            // SPECIAL HANDLING, LEFT OPERAND MUST BE CONTAINED IN THE RIGHT ONE 

                            sqlString = "(CHARINDEX (" + prefix + propertyPath + ", '" + valueString + "', 0) > 0)";

                            return sqlString;

                        default: return String.Empty; // RETURN UNSUPPORTED

                    } // switch (filterOperator) {

                    sqlString = prefix + PropertyPath + valueString;

                    #endregion

                }

                else if ((parameter.Value is Int16) || (parameter.Value is Int32) || (parameter.Value is Int64) || (parameter.Value is Double)) {

                    #region Int16/Int32/Int64 and Double Values

                    valueString = Convert.ToString (parameter.Value);

                    switch (filterOperator) {

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsEqualTo: valueString = " = " + valueString; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsNotEqualTo: valueString = " <> " + valueString; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsGreaterThan: valueString = " > " + valueString; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsGreaterThanOrEqualTo: valueString = " >= " + valueString; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsLessThan: valueString = " < " + valueString; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsLessThanOrEqualTo: valueString = " <= " + valueString; break;

                        default: return String.Empty; // RETURN UNSUPPORTED

                    } // switch (filterOperator) {

                    sqlString = prefix + PropertyPath + valueString;

                    #endregion

                }

                else if ((parameter.Value is DateTime) || (parameter.Value is DateTime?)) {

                    #region DateTime/DateTime?


                    if (parameter.Value is DateTime?) {

                        if ((!((DateTime?)parameter.Value).HasValue) && (filterOperator == Mercury.Server.Data.Enumerations.DataFilterOperator.IsEqualTo)) {

                            return prefix + propertyPath + " IS NULL";

                        }

                        else if ((!((DateTime?)parameter.Value).HasValue) && (filterOperator == Mercury.Server.Data.Enumerations.DataFilterOperator.IsNotEqualTo)) {

                            return prefix + propertyPath + " IS NOT NULL";

                        }

                        else if ((!((DateTime?)parameter.Value).HasValue)) { return String.Empty; } // UNSUPPORTED


                        else { 

                            valueString = ((DateTime?) parameter.Value).Value.ToString ("MM/dd/yyyy");

                        }

                    }

                    else {

                        valueString = ((DateTime) parameter.Value).ToString ("MM/dd/yyyy");

                    }

                    switch (filterOperator) {

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsEqualTo: valueString = " = '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsNotEqualTo: valueString = " <> '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsGreaterThan: valueString = " > '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsGreaterThanOrEqualTo: valueString = " >= '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsLessThan: valueString = " < '" + valueString + "'"; break;

                        case Mercury.Server.Data.Enumerations.DataFilterOperator.IsLessThanOrEqualTo: valueString = " <= '" + valueString + "'"; break;

                        default: return String.Empty; // RETURN UNSUPPORTED

                    } // switch (filterOperator) {

                    sqlString = prefix + "CONVERT (DATE, " + PropertyPath + ") " + valueString;

                    #endregion

                }

                else {

                    System.Diagnostics.Debug.WriteLine ("[FilterDescriptor.SqlCriteriaString] Unhandled Filter: " + PropertyPath + " = " + Convert.ToString (parameter.Value));

                }

            }

            return sqlString;

        }

        #endregion 


        #region Constructors 
        
        public FilterDescriptor () { /* DO NOTHING */ }

        //
        // Summary:
        //     Constructor that creates a System.Windows.Data.Parameter instance and sets
        //     its System.Windows.Data.Parameter.Value property to the provided filterValue.
        //
        // Parameters:
        //   propertyPath:
        //     The path to be used for System.Windows.Data.FilterDescriptor.PropertyPath.
        //
        //   filterOperator:
        //     The System.Windows.Data.FilterOperator to be used for System.Windows.Data.FilterDescriptor.Operator.
        //
        //   filterValue:
        //     The value to be used by a new System.Windows.Data.Parameter instance. Do
        //     not specify a System.Windows.Data.Parameter; just supply the value to be
        //     used. A new System.Windows.Data.Parameter will be created, setting its System.Windows.Data.Parameter.ParameterName
        //     to "Value" and its System.Windows.Data.Parameter.Value to the filterValue
        //     supplied.
        public FilterDescriptor (String forPropertyPath, Enumerations.DataFilterOperator forFilterOperator, Object forFilterValue) {

            propertyPath = forPropertyPath;

            filterOperator = forFilterOperator;

            parameter = new Parameter ();

            parameter.Name = "Value";

            parameter.Value = forFilterValue;

            return;
            

        }

        #endregion 

    }

}