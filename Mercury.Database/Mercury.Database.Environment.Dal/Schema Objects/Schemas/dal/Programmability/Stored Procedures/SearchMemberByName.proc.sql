/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'SearchMemberByName' AND type = 'P'))
  DROP PROCEDURE dal.SearchMemberByName
GO      
*/


CREATE PROCEDURE [dal].SearchMemberByName

    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberName VARCHAR (060),
      @birthDate  DATETIME, 
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
        
        SET @memberName = REPLACE (@memberName, ',', ', ')
        
        SET @memberName = REPLACE (@memberName, '  ', ' ')
        
        SET @memberName = @memberName + '%'  
        
        SELECT 
        
          MemberId
          
          INTO #MemberSearch
      
          FROM 
            dbo.Member JOIN dbo.Entity ON Member.EntityId = Entity.EntityId
            
          WHERE 
          
						Entity.EntityName LIKE @memberName
						
            AND ((@birthDate IS NULL) OR (@birthDate = Member.BirthDate))                       
      
        IF (@returnResults != 0) 
          BEGIN
          
						SELECT Member.*, Entity.EntityName, 
						
							CASE WHEN (MemberEnrollment.MemberEnrollmentId IS NOT NULL) THEN 1 ELSE 0 END AS CurrentlyEnrolled
           
              FROM 
                #MemberSearch AS MemberSearch
                
                  JOIN dbo.Member AS Member ON MemberSearch.MemberId = Member.MemberId
                    
                  JOIN dbo.Entity AS Entity ON Member.EntityId = Entity.EntityId

                  LEFT JOIN dbo.MemberEnrollment 
                  
                    ON Member.MemberId = MemberEnrollment.MemberId
                    
                    AND GETDATE () BETWEEN MemberEnrollment.EffectiveDate AND MemberEnrollment.TerminationDate

              ORDER BY Entity.EntityName
           
          END
          
        ELSE
          BEGIN
            SELECT COUNT (1) AS CountOf FROM #MemberSearch
  
        END
        
              
    END     