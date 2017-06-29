CREATE INDEX [MemberEnrollmentCoverage_BenefitPlanEffective]
    ON [dbo].MemberEnrollmentCoverage
	(BenefitPlanId, EffectiveDate, TerminationDate) INCLUDE (MemberEnrollmentId, MemberEnrollmentCoverageId)


