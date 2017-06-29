/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberAuthorizedServiceDetail_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberAuthorizedServiceDetail_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberAuthorizedServiceDetail_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberAuthorizedServiceId         BIGINT,     
      @authorizedServiceDefinitionId     BIGINT,
      @eventDate               DATETIME, 
      @authorizationId           BIGINT,     
      @authorizationNumber      VARCHAR (020),    
      @externalAuthorizationId  VARCHAR (030), 
      @authorizationLine            INT,
      @memberId                  BIGINT,     
      @referringProviderId                BIGINT,     
      @serviceProviderId                BIGINT,     
      @category                 VARCHAR (030),     
      @subcategory              VARCHAR (030),     
      @serviceType              VARCHAR (030),     
      
      @status                   VARCHAR (030),   
      @receivedDate            DATETIME,
      @referralDate            DATETIME,
      @effectiveDate           DATETIME,
      @terminationDate         DATETIME,
      @serviceDate             DATETIME,

      @principalDiagnosisCode   VARCHAR (006),     
      @principalDiagnosisVersion    INT,
      @diagnosisCode            VARCHAR (006),     
      @diagnosisVersion             INT,
      @revenueCode              VARCHAR (004),     
      @procedureCode            VARCHAR (005),     
      @modifierCode             VARCHAR (002),  
      @specialtyName            VARCHAR (060),
      @ndcCode                     CHAR (011),
      
      @description              VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF NOT EXISTS (SELECT * FROM dbo.MemberAuthorizedServiceDetail WHERE MemberAuthorizedServiceId = @memberAuthorizedServiceId AND authorizedServiceDefinitionId = @authorizedServiceDefinitionId AND AuthorizationId = @authorizationId AND AuthorizationLine = @authorizationLine)
          
          BEGIN
          
            INSERT INTO dbo.MemberAuthorizedServiceDetail (
            
                MemberAuthorizedServiceId, AuthorizedServiceDefinitionId, EventDate, AuthorizationId, AuthorizationNumber, ExternalAuthorizationId, AuthorizationLine,
                
                MemberId, ReferringProviderId, ServiceProviderId, Category, Subcategory, ServiceType, Status, ReceivedDate, ReferralDate, EffectiveDate, TerminationDate, ServiceDate,
                
                PrincipalDiagnosisCode, PrincipalDiagnosisVersion, DiagnosisCode, DiagnosisVersion, RevenueCode, ProcedureCode, ModifierCode, SpecialtyName, NdcCode, Description
             
              )
                
            VALUES (

                @memberAuthorizedServiceId, @authorizedServiceDefinitionId, @eventDate, @authorizationId, @authorizationNumber, @externalAuthorizationId, @authorizationLine,
                
                @memberId, @referringProviderId, @serviceProviderId, @category, @subcategory, @serviceType, @status, @receivedDate, @referralDate, @effectiveDate, @terminationDate, @serviceDate,
                
                @principalDiagnosisCode, @principalDiagnosisVersion, @diagnosisCode, @diagnosisVersion, @revenueCode, @procedureCode, @modifierCode, @specialtyName, @ndcCode, @description
                
            )

           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberAuthorizedServiceDetail_InsertUpdate TO PUBLIC
GO          
*/