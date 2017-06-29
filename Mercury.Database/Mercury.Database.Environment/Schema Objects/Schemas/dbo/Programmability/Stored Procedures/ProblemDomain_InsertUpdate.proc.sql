/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ProblemDomain_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ProblemDomain_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ProblemDomain_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @problemDomainId              BIGINT,
      @problemDomainName           VARCHAR (060),
      @problemDomainDescription    VARCHAR (999),
      
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

        IF EXISTS (SELECT * FROM dbo.ProblemDomain WHERE ((ProblemDomainId = @problemDomainId) AND (@problemDomainId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ProblemDomain
              SET
                ProblemDomainName = @problemDomainName,
                ProblemDomainDescription = @problemDomainDescription,
                                
                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ProblemDomainId = @problemDomainId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.ProblemDomain (
                ProblemDomainName, ProblemDomainDescription, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @problemDomainName, @problemDomainDescription, @enabled, @visible, 

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