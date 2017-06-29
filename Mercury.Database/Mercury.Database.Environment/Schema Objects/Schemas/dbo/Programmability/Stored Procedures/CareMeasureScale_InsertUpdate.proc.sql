
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareMeasureScale_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareMeasureScale_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareMeasureScale_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careMeasureScaleId               BIGINT,
      @careMeasureScaleName            VARCHAR (060),
      @careMeasureScaleDescription     VARCHAR (999),
      
      @scaleLabel1 VARCHAR (060),
      @scaleLabel2 VARCHAR (060),
      @scaleLabel3 VARCHAR (060),
      @scaleLabel4 VARCHAR (060),
      @scaleLabel5 VARCHAR (060),

      @extendedProperties          XML,
      
      @enabled                       BIT,
      @visible                       BIT,
            
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

        IF EXISTS (SELECT * FROM dbo.CareMeasureScale WHERE ((CareMeasureScaleId = @careMeasureScaleId) AND (@careMeasureScaleId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareMeasureScale
              SET
                CareMeasureScaleName = @careMeasureScaleName,
                CareMeasureScaleDescription = @careMeasureScaleDescription,
                
                ScaleLabel1 = @scaleLabel1,
                ScaleLabel2 = @scaleLabel2,
                ScaleLabel3 = @scaleLabel3,
                ScaleLabel4 = @scaleLabel4,
                ScaleLabel5 = @scaleLabel5,
                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CareMeasureScaleId = @careMeasureScaleId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareMeasureScale (CareMeasureScaleName, CareMeasureScaleDescription, 
            
								ScaleLabel1, ScaleLabel2, ScaleLabel3, ScaleLabel4, ScaleLabel5, 
            
								ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @careMeasureScaleName, @careMeasureScaleDescription, 
                
								@scaleLabel1, @scaleLabel2, @scaleLabel3, @scaleLabel4, @scaleLabel5, 
                
                @extendedProperties, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CareMeasureScale_InsertUpdate TO PUBLIC
GO          
*/