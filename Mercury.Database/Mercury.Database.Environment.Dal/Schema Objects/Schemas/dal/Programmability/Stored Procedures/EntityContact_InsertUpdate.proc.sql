
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContact_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dal.EntityContact_InsertUpdate
GO      
*/

CREATE PROCEDURE dal.EntityContact_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityContactId                 BIGINT,
      @entityId                        BIGINT,
      @entityContactInformationId      BIGINT,
            
      @relatedEntityId    BIGINT,
      @relatedObjectType VARCHAR (120),
      @relatedObjectId    BIGINT,      
      @scriptEntityFormId BIGINT,
      
      @contactDate                     DATETIME,
      @contactDirection                INT,
      @contactType                     INT,
      @successful                      BIT,
      @contactOutcome                  INT,
      
      @contactRegardingId         BIGINT,
      @regarding                 VARCHAR (120),
      @remarks                   VARCHAR (999),
      @contactedByName           VARCHAR (060),
      
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

        IF EXISTS (SELECT * FROM dbo.EntityContact WHERE EntityContactId = @entityContactId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.EntityContact
              SET
                EntityId = @entityId,
                EntityContactInformationId = @entityContactInformationId,

                RelatedEntityId   = @relatedEntityId,
                RelatedObjectType = @relatedObjectType,
                RelatedObjectId   = @relatedObjectId,   
                ScriptEntityFormId = @scriptEntityFormId,                       
                                          
                ContactDate = @contactDate,
                ContactDirection = @contactDirection,
                ContactType = @contactType,
                Successful = @successful,
                ContactOutcome = @contactOutcome,
                
                ContactRegardingId = @contactRegardingId,
                Regarding = @regarding,
                Remarks = @remarks,
                ContactedByName = @contactedByName,
                
                ExtendedProperties = @extendedProperties,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                EntityContactId = @entityContactId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.EntityContact (
            
                EntityId, EntityContactInformationId, RelatedEntityId, RelatedObjectType, RelatedObjectId, ScriptEntityFormId,
                
                ContactDate, ContactDirection, ContactType, Successful, ContactOutcome, ContactRegardingId, Regarding, Remarks, ContactedByName, 

                ExtendedProperties,
                
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
            
                @entityId, @entityContactInformationId, @relatedEntityId, @relatedObjectType, @relatedObjectId, @scriptEntityFormId,
                
                @contactDate, @contactDirection, @contactType, @successful, @contactOutcome, @contactRegardingId, @regarding, @remarks, @contactedByName, 

                @extendedProperties,
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityContact_InsertUpdate TO PUBLIC
GO          
*/