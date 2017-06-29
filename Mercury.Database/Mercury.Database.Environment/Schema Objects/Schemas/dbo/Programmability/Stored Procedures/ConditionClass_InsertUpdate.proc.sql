/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ConditionClass_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ConditionClass_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ConditionClass_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @conditionClassId              BIGINT,
      @conditionClassName           VARCHAR (060),
      @conditionClassDescription    VARCHAR (999),
      
      -- @conditionDomainId           BIGINT,
      
      @enabled                    BIT,
      @visible                    BIT,

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

        IF EXISTS (SELECT * FROM dbo.ConditionClass WHERE ((ConditionClassId = @conditionClassId) AND (@conditionClassId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ConditionClass
              SET
                ConditionClassName = @conditionClassName,
                ConditionClassDescription = @conditionClassDescription,
                
                -- ConditionDomainId = @conditionDomainId,
                                
                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ConditionClassId = @conditionClassId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.ConditionClass (
                ConditionClassName, ConditionClassDescription, -- ConditionDomainId, 
								
								Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @conditionClassName, @conditionClassDescription, -- @conditionDomainId, 
								
								@enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/