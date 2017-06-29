CREATE INDEX [MemberEnrollment_ProgramEffectiveTermination]

	ON [dbo].[MemberEnrollment] ([ProgramId],[EffectiveDate],[TerminationDate])

	INCLUDE ([MemberEnrollmentId],[MemberId])


