/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareMeasureClass_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareMeasureClass_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareMeasureClass_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careMeasureClassId              BIGINT,
      @careMeasureClassName           VARCHAR (060),
      @careMeasureClassDescription    VARCHAR (999),
      
      @careMeasureDomainId           BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.CareMeasureClass WHERE ((CareMeasureClassId = @careMeasureClassId) AND (@careMeasureClassId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareMeasureClass
              SET
                CareMeasureClassName = @careMeasureClassName,
                CareMeasureClassDescription = @careMeasureClassDescription,
                
                CareMeasureDomainId = @careMeasureDomainId,
                                
                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CareMeasureClassId = @careMeasureClassId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareMeasureClass (
                CareMeasureClassName, CareMeasureClassDescription, CareMeasureDomainId, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @careMeasureClassName, @careMeasureClassDescription, @careMeasureDomainId, @enabled, @visible, 

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