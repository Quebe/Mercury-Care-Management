using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Action.EventArguments {

    public class PopulationEventArguments : EventArguments {

        #region 

        public Int64 PopulationId { get { return Convert.ToInt64 (Arguments["PopulationId"]); } set { Arguments["PopulationId"] = value; } }

        public String PopulationName { get { return (String) Arguments ["PopulationName"]; } set { Arguments ["PopulationName"] = value; } }

        public Int64 PopulationMembershipId { get { return Convert.ToInt64 (Arguments["PopulationMembershipId"]); } set { Arguments["PopulationMembershipId"] = value; } }

//        public Int64 MemberId { get { return Convert.ToInt64 (Arguments["MemberId"]); } set { Arguments["MemberId"] = value; } }

        public String SenderObjectType { get { return (String) Arguments["SenderObjectType"]; } set { Arguments["SenderObjectType"] = value; } }

        public Int64 SenderId { get { return Convert.ToInt64 (Arguments["SenderId"]); } set { Arguments["SenderId"] = value; } }

        #endregion


        #region Constructors

        public PopulationEventArguments () {

            Arguments.Add ("PopulationId", Convert.ToInt64 (0));

            Arguments.Add ("PopulationName", String.Empty);

            Arguments.Add ("PopulationMembershipId", Convert.ToInt64 (0));

//            Arguments.Add ("MemberId", Convert.ToInt64 (0));

            Arguments.Add ("SenderObjectType", String.Empty);

            Arguments.Add ("SenderId", Convert.ToInt64 (0));

            return;

        }

        #endregion

    }

}
