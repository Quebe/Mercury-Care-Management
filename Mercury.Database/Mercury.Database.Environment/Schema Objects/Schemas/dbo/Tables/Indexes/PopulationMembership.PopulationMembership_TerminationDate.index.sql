
CREATE INDEX PopulationMembership_TerminationDate ON PopulationMembership (TerminationDate, PopulationId) INCLUDE (PopulationMembershipId, MemberId)