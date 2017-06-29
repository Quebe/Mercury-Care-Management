CREATE INDEX [MemberEnrollment_ProgramEffectiveMember]

    ON [dbo].MemberEnrollment

		(ProgramId, EffectiveDate, MemberId, MemberEnrollmentId) 

-- USED IN THE LINKING FOR PREVIOUS/NEXT MEMBER ENROLLMENT


