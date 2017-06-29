/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ProblemClass_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ProblemClass_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ProblemClass_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @problemClassId              BIGINT,
      @problemClassName           VARCHAR (060),
      @problemClassDescription    VARCHAR (999),
      
      @problemDomainId           BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.ProblemClass WHERE ((ProblemClassId = @problemClassId) AND (@problemClassId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ProblemClass
              SET
                ProblemClassName = @problemClassName,
                ProblemClassDescription = @problemClassDescription,
                
                ProblemDomainId = @problemDomainId,
                                
                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ProblemClassId = @problemClassId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.ProblemClass (
                ProblemClassName, ProblemClassDescription, ProblemDomainId, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @problemClassName, @problemClassDescription, @problemDomainId, @enabled, @visible, 

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