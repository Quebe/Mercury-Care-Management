/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareMeasure_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareMeasure_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareMeasure_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careMeasureId              BIGINT,
      @careMeasureName           VARCHAR (060),
      @careMeasureDescription    VARCHAR (999),
      
      @careMeasureClassId            BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.CareMeasure WHERE ((CareMeasureId = @careMeasureId) AND (@careMeasureId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareMeasure
              SET
                CareMeasureName = @careMeasureName,
                CareMeasureDescription = @careMeasureDescription,
                                
								CareMeasureClassId = @careMeasureClassId,
                                
                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CareMeasureId = @careMeasureId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareMeasure (
                CareMeasureName, CareMeasureDescription, 
                
                CareMeasureClassId, 
                
                Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @careMeasureName, @careMeasureDescription, 
                
                @careMeasureClassId, 
                
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