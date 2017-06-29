CREATE INDEX [MemberEnrollmentCoverage_BenefitPlanTermination]
    ON [dbo].MemberEnrollmentCoverage
	(BenefitPlanId, TerminationDate) INCLUDE (MemberEnrollmentId, MemberEnrollmentCoverageId)


