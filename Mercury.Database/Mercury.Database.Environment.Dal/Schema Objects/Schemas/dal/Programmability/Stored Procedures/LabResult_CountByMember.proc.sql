/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'LabResult_CountByMember' AND type = 'P'))
  DROP PROCEDURE dal.LabResult_CountByMember
GO      
*/

CREATE PROCEDURE dal.LabResult_CountByMember
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        DECLARE @labResultCount AS INT

        /* LOCAL VARIABLES ( END ) */

        SELECT @labResultCount = COUNT (1)
         
            FROM dbo.LabResult
            
            WHERE (LabResult.MemberId = @memberId)
            
        RETURN @labResultCount
              
    END    
              
    