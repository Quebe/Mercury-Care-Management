-- DROP VIEW ActiveProspectiveMembership

CREATE VIEW [dal].ActiveProspectiveMembership AS 

SELECT 

    Member.MemberId,
    
    Member.Gender, 
    
    Member.BirthDate,
    
    Member.EthnicityId,
    
    Program.InsurerId,
    
    MemberEnrollment.ProgramId,

    MemberEnrollmentCoverage.BenefitPlanId,
    
    MemberEnrollment.SponsorId,
    
    CAST (MemberEnrollment.EffectiveDate AS DATETIME) AS EnrollmentEffectiveDate
        
  FROM 
  
    dbo.Member AS Member

      JOIN (SELECT MemberId, MIN (EffectiveDate) As MinimumEffectiveDate
      
          FROM dbo.MemberEnrollment 
        
					WHERE GETDATE () <= TerminationDate
					
					GROUP BY MemberId
					
				) AS [FirstEnrollment]
        
        ON Member.MemberId = [FirstEnrollment].MemberId
        

			JOIN dbo.MemberEnrollment AS MemberEnrollment
			
				ON Member.MemberId = MemberEnrollment.MemberId
				
				AND [FirstEnrollment].MemberId = MemberEnrollment.MemberId
				
				AND [FirstEnrollment].MinimumEffectiveDate = MemberEnrollment.EffectiveDate        
				
				AND GETDATE () <= MemberEnrollment.EffectiveDate
                

      JOIN dbo.MemberEnrollmentCoverage AS MemberEnrollmentCoverage
      
				ON MemberEnrollment.MemberEnrollmentId = MemberEnrollmentCoverage.MemberEnrollmentId

        AND GETDATE () BETWEEN MemberEnrollmentCoverage.EffectiveDate AND MemberEnrollmentCoverage.TerminationDate
				
        
      JOIN dbo.Program AS Program
      
        ON MemberEnrollment.ProgramId = Program.ProgramId