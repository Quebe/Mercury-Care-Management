/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberService_SelectSampleByService' AND type = 'P'))
  DROP PROCEDURE MemberService_SelectSampleByService
GO      
*/

CREATE PROCEDURE dbo.MemberService_SelectSampleByService
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceId            BIGINT,
      @sampleSize              INT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        DECLARE @sqlStatement AS VARCHAR (8000)
        
        SET @sqlStatement = ''
        
        SET @sqlStatement = @sqlStatement + 'SELECT TOP ' + LTRIM (@sampleSize) + ' MemberService.*, Member.EntityName AS MemberName, Member.BirthDate '
        
        SET @sqlStatement = @sqlStatement + '  FROM MemberService '
        
        SET @sqlStatement = @sqlStatement + '    JOIN dal.MemberEntity AS Member ON dal.ConvertMemberIdToSource (MemberService.MemberId) = Member.ExternalMemberId'
        
        SET @sqlStatement = @sqlStatement + '  WHERE ServiceId = ' + LTRIM (@serviceId) + ' ORDER BY EventDate DESC, MemberServiceId'
        
        EXEC (@sqlStatement)
          
    END    
              
    