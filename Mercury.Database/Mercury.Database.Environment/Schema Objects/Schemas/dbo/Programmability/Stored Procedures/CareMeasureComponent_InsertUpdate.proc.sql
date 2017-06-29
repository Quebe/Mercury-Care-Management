/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareMeasureComponent_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareMeasureComponent_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareMeasureComponent_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careMeasureComponentId              BIGINT,
      @careMeasureComponentName           VARCHAR (099), -- EXTENDED NAME
      @careMeasureComponentDescription    VARCHAR (999),
      
      @careMeasureId                BIGINT,
      @careMeasureScaleId           BIGINT,
      @tag VARCHAR (020),
      
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

        IF EXISTS (SELECT * FROM dbo.CareMeasureComponent WHERE ((CareMeasureComponentId = @careMeasureComponentId) AND (@careMeasureComponentId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareMeasureComponent
              SET
                CareMeasureComponentName = @careMeasureComponentName,
                CareMeasureComponentDescription = @careMeasureComponentDescription,
                
                CareMeasureId = @careMeasureId,
                CareMeasureScaleId = @careMeasureScaleId,
                Tag = @tag,
                                
                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CareMeasureComponentId = @careMeasureComponentId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareMeasureComponent (
                CareMeasureComponentName, CareMeasureComponentDescription, CareMeasureId, CareMeasureScaleId, Tag, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @careMeasureComponentName, @careMeasureComponentDescription, @careMeasureId, @careMeasureScaleId, @tag, @enabled, @visible, 

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