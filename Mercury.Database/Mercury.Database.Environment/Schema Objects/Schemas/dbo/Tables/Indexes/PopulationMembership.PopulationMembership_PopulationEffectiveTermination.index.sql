CREATE INDEX [PopulationMembership_PopulationEffectiveTermination]
	
	ON [dbo].[PopulationMembership] ([PopulationId],[EffectiveDate],[TerminationDate])

	INCLUDE ([PopulationMembershipId],[MemberId],[AnchorDate])


