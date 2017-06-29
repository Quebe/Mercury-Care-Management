CREATE INDEX [MemberEnrollmentCoverage_EffectiveTermination]
    ON MemberEnrollmentCoverage (EffectiveDate, TerminationDate)
		INCLUDE (MemberEnrollmentCoverageId, MemberEnrollmentId, BenefitPlanId)


