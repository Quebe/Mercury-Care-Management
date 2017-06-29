
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberMetric_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberMetric_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberMetric_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberMetricId         BIGINT,
      @memberId                BIGINT,
      @metricId               BIGINT,
      @eventDate             DATETIME,
      @metricValue            DECIMAL (20, 08),
      @addedManually              BIT,
      
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

        IF EXISTS (SELECT * FROM dbo.MemberMetric WHERE MemberMetricId = @memberMetricId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.MemberMetric
              SET
                MemberId = @memberId,
                MetricId = @metricId,
                EventDate = @eventDate,
                MetricValue = @metricValue,
                AddedManually = @addedManually,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                MemberMetricId = @memberMetricId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.MemberMetric (MemberId, MetricId, EventDate, MetricValue, AddedManually, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @memberId, @metricId, @eventDate, @metricValue, @addedManually,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberMetric_InsertUpdate TO PUBLIC
GO          
*/