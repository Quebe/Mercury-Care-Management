/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ProblemStatement_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ProblemStatement_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ProblemStatement_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @problemStatementId              BIGINT,
      @problemStatementName           VARCHAR (060),
      @problemStatementDescription    VARCHAR (999),
      
      @problemDomainId				   BIGINT,
      @problemClassId            BIGINT,
--      @problemCategoryId				 BIGINT,
--      @problemSubcategoryId			 BIGINT,      
      
      @definingCharacteristics VARCHAR (999),
      @relatedFactors          VARCHAR (999),
      
      @defaultCarePlanId BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.ProblemStatement WHERE ((ProblemStatementId = @problemStatementId) AND (@problemStatementId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ProblemStatement
              SET
                ProblemStatementName = @problemStatementName,
                ProblemStatementDescription = @problemStatementDescription,
                                
								ProblemDomainId = @problemDomainId,
								ProblemClassId = @problemClassId,
--								ProblemCategoryId = @problemCategoryId,
--								ProblemSubcategoryId = @problemSubcategoryId,
								
								DefiningCharacteristics = @definingCharacteristics,
								RelatedFactors = @relatedFactors,
								
								DefaultCarePlanId = @defaultCarePlanId,
                                
                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ProblemStatementId = @problemStatementId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.ProblemStatement (
                ProblemStatementName, ProblemStatementDescription, 
                
                ProblemDomainId, ProblemClassId, -- ProblemCategoryId, ProblemSubcategoryId,
                
                DefiningCharacteristics, RelatedFactors,
                
                DefaultCarePlanId,
                
                Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @problemStatementName, @problemStatementDescription, 
                
                @problemDomainId, @problemClassId, -- @problemCategoryId, @problemSubcategoryId, 
                
                @definingCharacteristics, @relatedFactors,
                
                @defaultCarePlanId,
                
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