/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberMetric_AddManual' AND type = 'P'))
  DROP PROCEDURE dbo.MemberMetric_AddManual
GO      
*/

CREATE PROCEDURE dbo.MemberMetric_AddManual
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId                BIGINT,
      @metricId                BIGINT,
      @eventDate             DATETIME,
      @metricValue            DECIMAL (020, 08),
      
      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF NOT EXISTS (SELECT * FROM dbo.MemberMetric WHERE (MemberId = @memberId) AND (MetricId = @metricId) AND (EventDate = @eventDate))
          BEGIN

            INSERT INTO dbo.MemberMetric (MemberId, MetricId, MetricValue, EventDate, AddedManually, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @memberId, @metricId, @metricValue, @eventDate, 1,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberMetric_AddManual TO PUBLIC
GO          
*/