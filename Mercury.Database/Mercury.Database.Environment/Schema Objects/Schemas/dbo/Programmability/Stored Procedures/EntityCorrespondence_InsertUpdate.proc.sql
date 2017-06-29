/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityCorrespondence_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE EntityCorrespondence_InsertUpdate
GO      
*/

CREATE PROCEDURE EntityCorrespondence_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityCorrespondenceId          BIGINT,
      @entityId                        BIGINT,
      @correspondenceId                BIGINT,
      @correspondenceName             VARCHAR (060),
      @correspondenceVersion          DECIMAL (20, 08),
      
      @entityFormId       BIGINT,
      @relatedEntityId    BIGINT,
      @relatedObjectType VARCHAR (120),
      @relatedObjectId    BIGINT,
      
      @readyToSendDate DATETIME,
      @sentDate        DATETIME,
      @receivedDate    DATETIME,
      @returnedDate    DATETIME,
      
      @contactType           INT,
      @entityAddressId    BIGINT,
      @entityContactInformationId BIGINT,
      @attention         VARCHAR (060),
      @addressLine1      VARCHAR (055),
      @addressLine2      VARCHAR (055),
      @addressCity       VARCHAR (030),
      @addressState         CHAR (002),
      @addressZipCode       CHAR (005),
      @addressZipPlus4      CHAR (004),
      @addressPostalCode VARCHAR (015),
      @contactFaxNumber  VARCHAR (020),
      @contactEmail      VARCHAR (060),
      
      @remarks                   VARCHAR (999),
      
      @automationId UNIQUEIDENTIFIER, 
      @automationStatus        INT,
      @automationDate     DATETIME,
      @automationException VARCHAR (099),
      
      
      @extendedProperties XML,
            
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

        IF EXISTS (SELECT * FROM EntityCorrespondence WHERE EntityCorrespondenceId = @entityCorrespondenceId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE EntityCorrespondence
              SET
                EntityId = @entityId,
                CorrespondenceId = @correspondenceId,
                CorrespondenceName = @correspondenceName,
                CorrespondenceVersion = @correspondenceVersion,
                          
                EntityFormId      = @entityFormId,                        
                RelatedEntityId   = @relatedEntityId,
                RelatedObjectType = @relatedObjectType,
                RelatedObjectId   = @relatedObjectId,                          
                          
                ReadyToSendDate = @readyToSendDate,
                SentDate = @sentDate,
                ReceivedDate = @receivedDate,
                ReturnedDate = @returnedDate,
      
                ContactType = @contactType,
                EntityAddressId = @entityAddressId,
                EntityContactInformationId = @entityContactInformationId,
                Attention = @attention,
                AddressLine1 = @addressLine1,
                AddressLine2 = @addressLine2,
                AddressCity = @addressCity,
                AddressState = @addressState,
                AddressZipCode = @addressZipCode,
                AddressZipPlus4 = @addressZipPlus4,
                AddressPostalCode = @addressPostalCode,
                ContactFaxNumber = @contactFaxNumber,
                ContactEmail = @contactEmail,
                
                Remarks = @remarks,
                
                AutomationId = @automationId,
                AutomationStatus = @automationStatus,
                AutomationDate = @automationDate,
                AutomationException = @automationException,
                
                
                ExtendedProperties = @extendedProperties,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                EntityCorrespondenceId = @entityCorrespondenceId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO EntityCorrespondence (
                EntityId, CorrespondenceId, CorrespondenceName, CorrespondenceVersion,
                
                EntityFormId, RelatedEntityId, RelatedObjectType, RelatedObjectId,
                
                ReadyToSendDate, SentDate, ReceivedDate, ReturnedDate,
                
                ContactType, EntityAddressId, EntityContactInformationId, Attention, AddressLine1, AddressLine2, AddressCity, AddressState, AddressZipCode, AddressZipPlus4, AddressPostalCode, 
                
                ContactFaxNumber, ContactEmail,
                
                Remarks,
                
                AutomationId, AutomationStatus, AutomationDate, AutomationException,
                
                ExtendedProperties,
                
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @entityId, @correspondenceId, @correspondenceName, @correspondenceVersion,
                
                @entityFormId, @relatedEntityId, @relatedObjectType, @relatedObjectId,
                
                @readyToSendDate, @sentDate, @receivedDate, @returnedDate,
                
                @contactType, @entityAddressId, @entityContactInformationId, @attention, @addressLine1, @addressLine2, @addressCity, @addressState, @addressZipCode, @addressZipPlus4, @addressPostalCode, 
                
                @contactFaxNumber, @contactEmail,
                
                @remarks,
                
                @automationId, @automationStatus, @automationDate, @automationException,
                
                @extendedProperties,
               
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 

