CREATE INDEX PopulationMembership_PopulationTerminationDateMember
ON [dbo].PopulationMembership (PopulationId, TerminationDate, MemberId) INCLUDE (PopulationMembershipId)



