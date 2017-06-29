/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContactInformation_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dal.EntityContactInformation_InsertUpdate
GO      
*/

CREATE PROCEDURE dal.EntityContactInformation_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityContactInformationId  BIGINT,
      @entityId                    BIGINT,

      @contactType                    INT,
      @contactSequence								INT,

      @contactNumber            VARCHAR (020), 
      @contactExtension         VARCHAR (020),
      @contactEmail							VARCHAR (060),
        
      @effectiveDate          DATETIME,
      @terminationDate        DATETIME,
      
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
        
        
        -- CHECK FOR OVERLAPPING EXISTING SEGMENT
        
        DECLARE @existingEntityContactInformationId              AS BIGINT
        
        DECLARE @existingEntityContactInformationEffectiveDate   AS DATETIME
        
        DECLARE @existingEntityContactInformationTerminationDate AS DATETIME
        
               
        SELECT @existingEntityContactInformationId = EntityContactInformation.EntityContactInformationId, @existingEntityContactInformationEffectiveDate = EffectiveDate, @existingEntityContactInformationTerminationDate = TerminationDate
        
          FROM dbo.EntityContactInformation AS EntityContactInformation
          
          WHERE 
          
						EntityId = @entityId
            
            AND (ContactType = @contactType)
            
            AND (ContactSequence = @contactSequence)
            
            AND (EffectiveDate <= @terminationDate) AND (TerminationDate >= @effectiveDate)
            
            AND (EntityContactInformationId <> @entityContactInformationId)
            
            
        IF ((@existingEntityContactInformationId IS NULL) AND (@entityContactInformationId != 0) AND (@terminationDate >= @effectiveDate)) 
        
          BEGIN 
          
            UPDATE dbo.EntityContactInformation 
            
              SET 
              
                EntityId = @entityId,
                
                ContactType = @contactType,
                ContactSequence = @contactSequence,
                ContactNumber = @contactNumber, 
                ContactExtension = @contactExtension,

                EffectiveDate = @effectiveDate,
                TerminationDate = @terminationDate,

                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName

              WHERE EntityContactInformationId = @entityContactInformationId -- NO SUPPORT FOR EXTERNAL ADDRESSES
             
          END
          
        ELSE IF ((@existingEntityContactInformationId IS NULL) AND (@entityContactInformationId = 0) AND (@terminationDate >= @effectiveDate)) 
        
          BEGIN
          
            INSERT dbo.EntityContactInformation (
            
                EntityId, ContactType, ContactSequence, ContactNumber, ContactExtension, ContactEmail,
                
                EffectiveDate, TerminationDate,
                
                CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate,
                
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
              VALUES (
              
                @entityId, @contactType, @contactSequence, @contactNumber, @contactExtension, @contactEmail,
                
                @effectiveDate, @terminationDate,
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
                
            
          END
          
        -- ELSE OVERLAP DETECTED, DO NOT SAVE
        
        
           
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityContactInformation_Insert TO PUBLIC
GO          
*/