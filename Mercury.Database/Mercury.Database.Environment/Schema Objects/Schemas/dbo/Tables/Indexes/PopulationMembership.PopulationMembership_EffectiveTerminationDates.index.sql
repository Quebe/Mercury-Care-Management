﻿
CREATE INDEX PopulationMembership_EffectiveTerminationDates ON PopulationMembership (EffectiveDate, TerminationDate) INCLUDE (PopulationMembershipId)
