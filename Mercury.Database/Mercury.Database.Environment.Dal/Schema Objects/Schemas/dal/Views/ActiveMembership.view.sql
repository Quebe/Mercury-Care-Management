-- DROP VIEW dal.ActiveMembership

CREATE VIEW [dal].[ActiveMembership] AS 

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

      JOIN dbo.MemberEnrollment AS MemberEnrollment
      
        ON Member.MemberId = MemberEnrollment.MemberId
        
        AND GETDATE () BETWEEN MemberEnrollment.EffectiveDate AND MemberEnrollment.TerminationDate
        

      JOIN dbo.MemberEnrollmentCoverage AS MemberEnrollmentCoverage
      
				ON MemberEnrollment.MemberEnrollmentId = MemberEnrollmentCoverage.MemberEnrollmentId

        AND GETDATE () BETWEEN MemberEnrollmentCoverage.EffectiveDate AND MemberEnrollmentCoverage.TerminationDate
				
        
      JOIN dbo.Program AS Program
      
        ON MemberEnrollment.ProgramId = Program.ProgramId