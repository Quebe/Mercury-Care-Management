

CREATE INDEX PopulationMembershipServiceEvent_MemberServiceId

  ON PopulationMembershipServiceEvent (MemberServiceId, PopulationMembershipId) INCLUDE (PopulationMembershipServiceEventId)
  
