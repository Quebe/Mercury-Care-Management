/*
IF EXISTS (SELECT id FROM sysobjects WHERE (name = 'SearchMemberById' AND type = 'P'))
  DROP PROCEDURE dal.SearchMemberById
GO      
*/

CREATE PROCEDURE [dal].SearchMemberById

    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @programMemberId VARCHAR (060),
      
      @returnResults BIT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
        BEGIN TRY 
        
            DROP TABLE #MemberSearch
            
        END TRY
        
        BEGIN CATCH
        
            -- DO NOTHING
        
        END CATCH
                
        SELECT 
        
          MemberEnrollment.MemberId,
          
          MemberEnrollment.MemberEnrollmentId, 
          
					MemberEnrollment.ProgramMemberId
          
          
          INTO #MemberSearch
      
          FROM 
           
              dbo.MemberEnrollment
           
          WHERE 
            (MemberEnrollment.ProgramMemberId = @programMemberId)
            
      
        IF (@returnResults != 0) 
        
          BEGIN
          
						
						SELECT Member.*, Entity.EntityName, 
						
							CASE WHEN (MemberEnrollment.MemberEnrollmentId IS NOT NULL) THEN 1 ELSE 0 END AS CurrentlyEnrolled
           
              FROM 
                #MemberSearch AS MemberSearch
                
                  JOIN dbo.Member AS Member ON MemberSearch.MemberId = Member.MemberId
                    
                  JOIN dbo.Entity AS Entity ON Member.EntityId = Entity.EntityId

                  LEFT JOIN dbo.MemberEnrollment AS MemberEnrollment
                  
                    ON Member.MemberId = MemberEnrollment.MemberId
                    
                    AND GETDATE () BETWEEN MemberEnrollment.EffectiveDate AND MemberEnrollment.TerminationDate

              ORDER BY Entity.EntityName
           
          END
          
        ELSE
        
          BEGIN
          
            SELECT COUNT (1) AS CountOf FROM #MemberSearch
  
					END
        
              
    END     