using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Work {

    [Serializable]
    public class RoutingRule : CoreConfigurationObject {

        #region Private Properties

        private Int64 defaultWorkQueueId;

        private SortedList<Int32, RoutingRuleDefinition> rules = new SortedList<Int32, RoutingRuleDefinition> ();

        #endregion


        #region Public Properties

        public Int64 DefaultWorkQueueId { get { return defaultWorkQueueId; } set { defaultWorkQueueId = value; } }

        public SortedList<Int32, RoutingRuleDefinition> Rules { get { return rules; } set { rules = value; } }

        #endregion


        #region Constructors

        public RoutingRule (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public RoutingRule (Application applicationReference, Server.Application.RoutingRule serverRoutingRule) {

            base.BaseConstructor (applicationReference, serverRoutingRule);


            defaultWorkQueueId = serverRoutingRule.DefaultWorkQueueId;


            rules = new SortedList<Int32, RoutingRuleDefinition> ();

            foreach (Int32 currentRuleSequence in serverRoutingRule.Rules.Keys) {

                rules.Add (currentRuleSequence, new RoutingRuleDefinition (applicationReference, serverRoutingRule.Rules[currentRuleSequence]));

            }


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.RoutingRule serverRoutingRule) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverRoutingRule);


            serverRoutingRule.DefaultWorkQueueId = defaultWorkQueueId;



            // COPY, DON'T REFERENCE

            serverRoutingRule.Rules = new Dictionary<Int32, Server.Application.RoutingRuleDefinition> ();

            foreach (Int32 currentRuleSequence in rules.Keys) {

                serverRoutingRule.Rules.Add (currentRuleSequence, (Server.Application.RoutingRuleDefinition)rules[currentRuleSequence].ToServerObject ());

            }


            return;

        }

        public override Object ToServerObject () {

            Server.Application.RoutingRule serverRoutingRule = new Server.Application.RoutingRule ();

            MapToServerObject (serverRoutingRule);

            return serverRoutingRule;

        }

        public RoutingRule Copy () {

            Server.Application.RoutingRule serverRoutingRule = (Server.Application.RoutingRule)ToServerObject ();

            RoutingRule copiedRoutingRule = new RoutingRule (application, serverRoutingRule);

            return copiedRoutingRule;

        }

        public Boolean IsEqual (RoutingRule compareRoutingRule) {

            Boolean isEqual = base.IsEqual (compareRoutingRule);


            if (defaultWorkQueueId != compareRoutingRule.DefaultWorkQueueId) { isEqual = false; }

            if (rules.Count != compareRoutingRule.Rules.Count) { isEqual = false; }

            if (isEqual) {

                foreach (Int32 currentSequence in rules.Keys) {

                    if (compareRoutingRule.rules.ContainsKey (currentSequence)) {

                        if (!rules[currentSequence].IsEqual (compareRoutingRule.Rules[currentSequence])) { isEqual = false; }

                    }

                    else { isEqual = false; }

                    if (!isEqual) { break; }

                }

            }

            return isEqual;

        }

        #endregion 

        
        #region Public Methods

        public Boolean RuleExists (RoutingRuleDefinition ruleDefinition) {

            Boolean exists = false;

            foreach (Int32 currentSequence in rules.Keys) {

                exists = rules[currentSequence].IsEqual (ruleDefinition);

                if (exists) { break; }

            }

            return exists;

        }

        public void AppendRule (RoutingRuleDefinition ruleDefinition) {

            if (!RuleExists (ruleDefinition)) {

                ruleDefinition.RoutingRuleId = Id;

                ruleDefinition.Sequence = rules.Keys.Count + 1;

                rules.Add (ruleDefinition.Sequence, ruleDefinition);

            }

            return;

        }

        #endregion


    }

}
