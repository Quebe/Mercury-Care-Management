
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberServiceDetailSet_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberServiceDetailSet_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberServiceDetailSet_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberServiceId           BIGINT,     
      @setDefinitionId           BIGINT,
      @detailMemberServiceId     BIGINT,     
      @memberId                  BIGINT,     
      @parentServiceId           BIGINT,     
      @serviceId                 BIGINT,     
      @serviceName              VARCHAR (060),     
      @serviceType                  INT,
      @eventDate               DATETIME    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF NOT EXISTS (SELECT * FROM dbo.MemberServiceDetailSet WHERE MemberServiceId = @memberServiceId AND ServiceSetDefinitionId = @setDefinitionId AND DetailMemberServiceId = @detailMemberServiceId)
          
          BEGIN
          
            INSERT INTO dbo.MemberServiceDetailSet (
            
                MemberServiceId, ServiceSetDefinitionId, DetailMemberServiceId, MemberId, ParentServiceId, ServiceId, ServiceName, ServiceType, EventDate
              
              )
                
            VALUES (
            
                @memberServiceId, @setDefinitionId, @detailMemberServiceId, @memberId, @parentServiceId, @serviceId, @serviceName, @serviceType, @eventDate 
                
            )

           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberServiceDetailSet_InsertUpdate TO PUBLIC
GO          
*/