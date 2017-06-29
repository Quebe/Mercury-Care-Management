/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareMeasureDomain_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareMeasureDomain_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareMeasureDomain_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careMeasureDomainId              BIGINT,
      @careMeasureDomainName           VARCHAR (060),
      @careMeasureDomainDescription    VARCHAR (999),
      
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

        IF EXISTS (SELECT * FROM dbo.CareMeasureDomain WHERE ((CareMeasureDomainId = @careMeasureDomainId) AND (@careMeasureDomainId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareMeasureDomain
              SET
                CareMeasureDomainName = @careMeasureDomainName,
                CareMeasureDomainDescription = @careMeasureDomainDescription,
                                
                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CareMeasureDomainId = @careMeasureDomainId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareMeasureDomain (
                CareMeasureDomainName, CareMeasureDomainDescription, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @careMeasureDomainName, @careMeasureDomainDescription, @enabled, @visible, 

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