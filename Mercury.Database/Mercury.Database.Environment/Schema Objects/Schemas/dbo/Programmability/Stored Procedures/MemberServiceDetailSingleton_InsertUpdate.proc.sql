
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberServiceDetailSingleton_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberServiceDetailSingleton_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberServiceDetailSingleton_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberServiceId           BIGINT,     
      @singletonDefinitionId     BIGINT,
      @eventDate               DATETIME, 
      @claimId                   BIGINT,     
      @externalClaimId          VARCHAR (020),     
      @claimLine                    INT,
      @memberId                  BIGINT,     
      @providerId                BIGINT,     
      @claimType                VARCHAR (030),     
      @claimStatus              VARCHAR (030),   
      @claimDateFrom           DATETIME,
      @claimDateThru           DATETIME,
      @serviceDateFrom         DATETIME,
      @serviceDateThru         DATETIME,
      @admissionDate           DATETIME,
      @dischargeDate           DATETIME,
      @receivedDate            DATETIME,
      @paidDate                DATETIME,     
      @billType                 VARCHAR (003),     
      @principalDiagnosisCode   VARCHAR (006),     
      @principalDiagnosisVersion    INT,
      @diagnosisCode            VARCHAR (006),     
      @diagnosisVersion             INT,
      @Icd9ProcedureCode        VARCHAR (006),     
      @LocationCode             VARCHAR (002),     
      @revenueCode              VARCHAR (004),     
      @procedureCode            VARCHAR (005),     
      @modifierCode             VARCHAR (002),  
      @specialtyName            VARCHAR (060),
      @isPcpClaim                   BIT,
      @ndcCode                     CHAR (011),
      @units                    DECIMAL (20, 08),
      @deaClassification           CHAR (001),
      @therapeuticClassification   CHAR (006),
      @labLoincCode             VARCHAR (007),
      @labName                  VARCHAR (060),
      @labValue                 DECIMAL (20, 08),
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

        IF NOT EXISTS (SELECT * FROM dbo.MemberServiceDetailSingleton WHERE MemberServiceId = @memberServiceId AND ServiceSingletonDefinitionId = @singletonDefinitionId AND EventDate = @eventDate AND ClaimId = @claimId AND ClaimLine = @claimLine)
          
          BEGIN
          
            INSERT INTO dbo.MemberServiceDetailSingleton (
            
                MemberServiceId, ServiceSingletonDefinitionId, EventDate, ClaimId, ExternalClaimId, ClaimLine, 
                MemberId, ProviderId, ClaimType, ClaimStatus ,ClaimDateFrom, ClaimDateThru, ServiceDateFrom, ServiceDateThru, 
                AdmissionDate, DischargeDate, ReceivedDate, PaidDate, BillType, PrincipalDiagnosisCode, PrincipalDiagnosisVersion, DiagnosisCode, DiagnosisVersion,
                Icd9ProcedureCode, LocationCode, RevenueCode, ProcedureCode, ModifierCode, SpecialtyName, IsPcpClaim,
                
                NdcCode, Units, DeaClassification, TherapeuticClassification, LabLoincCode, LabName, LabValue, ServiceDescription
              
              )
                
            VALUES (
            
                @memberServiceId, @singletonDefinitionId, @eventDate, @claimId, @externalClaimId, @claimLine, 
                @memberId, @providerId, @claimType, @claimStatus, @claimDateFrom, @claimDateThru, @serviceDateFrom, @serviceDateThru, 
                @admissionDate, @dischargeDate, @receivedDate, @paidDate, @billType, @principalDiagnosisCode, @principalDiagnosisVersion, @diagnosisCode, @diagnosisVersion,
                @Icd9ProcedureCode, @LocationCode, @revenueCode, @procedureCode, @modifierCode, @specialtyName, @isPcpClaim,
                @ndcCode, @units, @deaClassification, @therapeuticClassification, @labLoincCode, @labName, @labValue, @description
                
            )

           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberServiceDetailSingleton_InsertUpdate TO PUBLIC
GO          
*/