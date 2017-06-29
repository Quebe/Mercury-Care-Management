CREATE INDEX [MemberEnrollment_EffectiveTermination]
    ON [dbo].MemberEnrollment (EffectiveDate, TerminationDate) 
		INCLUDE (MemberEnrollmentId, MemberId, ProgramId)


