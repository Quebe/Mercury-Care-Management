CREATE INDEX PopulationMembership_MemberPopulationTerminationEffective

	ON [dbo].PopulationMembership (MemberId, PopulationId, TerminationDate, EffectiveDate) 

	INCLUDE (PopulationMembershipId, IdentifyingEventServiceId, TerminatingEventServiceId)



