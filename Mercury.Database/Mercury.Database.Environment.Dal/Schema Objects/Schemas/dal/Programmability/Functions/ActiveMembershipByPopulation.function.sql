CREATE FUNCTION dal.ActiveMembershipByPopulation (@populationId BIGINT) RETURNS TABLE AS 

	RETURN SELECT 

			Member.MemberId,

			Member.Gender,

			Member.BirthDate,

			Member.EthnicityId,

			Program.InsurerId,

			Program.ProgramId,

			MemberEnrollmentCoverage.BenefitPlanId,

			MemberEnrollment.EffectiveDate AS EnrollmentEffectiveDate
			
		
		FROM 

			dbo.PopulationMembership 

				JOIN dbo.Member

					ON PopulationMembership.MemberId = Member.MemberId

				JOIN dbo.MemberEnrollment

					ON Member.MemberId = MemberEnrollment.MemberId

					AND GETDATE () BETWEEN MemberEnrollment.EffectiveDate AND MemberEnrollment.TerminationDate

				JOIN dbo.MemberEnrollmentCoverage

					ON MemberEnrollment.MemberEnrollmentId = MemberEnrollmentCoverage.MemberEnrollmentId

					AND GETDATE () BETWEEN MemberEnrollmentCoverage.EffectiveDate AND MemberEnrollmentCoverage.TerminationDate

				JOIN dbo.Program

					ON MemberEnrollment.ProgramId = Program.ProgramId

		WHERE 

			PopulationMembership.PopulationId = @populationId