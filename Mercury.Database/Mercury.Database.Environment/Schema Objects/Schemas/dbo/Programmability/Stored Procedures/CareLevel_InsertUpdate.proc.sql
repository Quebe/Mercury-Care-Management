﻿
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareLevel_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareLevel_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareLevel_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careLevelId               BIGINT,
      @careLevelName            VARCHAR (060),
      @careLevelDescription     VARCHAR (999),
      
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

        IF EXISTS (SELECT * FROM dbo.CareLevel WHERE ((CareLevelId = @careLevelId) AND (@careLevelId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareLevel
              SET
                CareLevelName = @careLevelName,
                CareLevelDescription = @careLevelDescription,
                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CareLevelId = @careLevelId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareLevel (CareLevelName, CareLevelDescription, ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @careLevelName, @careLevelDescription, @extendedProperties, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CareLevel_InsertUpdate TO PUBLIC
GO          
*/