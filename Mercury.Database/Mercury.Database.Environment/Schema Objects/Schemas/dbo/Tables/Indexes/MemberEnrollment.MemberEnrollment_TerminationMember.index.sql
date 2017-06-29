CREATE INDEX [MemberEnrollment_TerminationMember]
    ON dbo.MemberEnrollment
	(TerminationDate, MemberId) INCLUDE (EffectiveDate, ProgramId, MemberEnrollmentId)


