/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Form_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE Form_InsertUpdate
GO      
*/

CREATE PROCEDURE Form_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @formId                  BIGINT,
      @entityId                BIGINT,
      @entityFormId            BIGINT,
      @formName               VARCHAR (060),
      @controlId              VARCHAR (040),
      @formDescription        VARCHAR (999),
      @entityType                 INT,
      @entityObjectId          BIGINT,
    
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

        IF (@entityFormId IS NULL)
       
          BEGIN 

            IF EXISTS (SELECT * FROM Form WHERE FormId = @formId)
              BEGIN
                -- EXISTING RECORD, UPDATE
                
                UPDATE Form
                  SET
                    FormName = @formName,
                    FormDescription = @formDescription,
                    EntityType = @entityType,

                    ModifiedAuthorityName = @modifiedAuthorityName,
                    ModifiedAccountId     = @modifiedAccountId,
                    ModifiedAccountName   = @modifiedAccountName,
                    ModifiedDate          = GETDATE ()
                    
                  WHERE 
                    FormId = @formId

              END
              
            ELSE -- NO EXISTING RECORD, INSERT NEW
              BEGIN
              
                INSERT INTO Form (
                                            FormName,    FormControlId, FormDescription, EntityType,

                    CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                    ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                    
                VALUES (
                                            @formName, @controlId, @formDescription, @entityType,

                    @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                    @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
               
              END                       
              
            END
            
          ELSE
          
            BEGIN

              IF EXISTS (SELECT * FROM EntityForm WHERE EntityFormId = @entityformId)
                BEGIN
                  -- EXISTING RECORD, UPDATE
                  
                  UPDATE EntityForm
                    SET
                      EntityId        = @entityId,
                      FormId          = @formId,
                      FormName        = @formName,
                      FormControlId   = @controlId,
                      FormDescription = @formDescription,
                      EntityType      = @entityType,
                      EntityObjectId  = @entityObjectId,

                      ModifiedAuthorityName = @modifiedAuthorityName,
                      ModifiedAccountId     = @modifiedAccountId,
                      ModifiedAccountName   = @modifiedAccountName,
                      ModifiedDate          = GETDATE ()
                      
                    WHERE 
                      EntityFormId = @entityFormId

                END
                
              ELSE -- NO EXISTING RECORD, INSERT NEW
                BEGIN
                
                  INSERT INTO EntityForm (EntityId, FormId, FormName, FormControlId, FormDescription, EntityType, EntityObjectId,

                      CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                      ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                      
                  VALUES (@entityId, @formId, @formName, @controlId, @formDescription, @entityType, @entityObjectId,

                      @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                      @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
                 
                END                       
              
            END
            
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON Form_InsertUpdate TO PUBLIC
GO          
*/