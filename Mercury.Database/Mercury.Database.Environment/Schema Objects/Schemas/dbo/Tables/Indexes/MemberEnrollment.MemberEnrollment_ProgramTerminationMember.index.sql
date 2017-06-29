CREATE INDEX [MemberEnrollment_ProgramTerminationMember]

    ON [dbo].MemberEnrollment

		(ProgramId, TerminationDate, MemberId, MemberEnrollmentId) 

-- USED IN THE LINKING FOR PREVIOUS/NEXT MEMBER ENROLLMENT


