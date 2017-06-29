/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityAddress_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dal.EntityAddress_InsertUpdate
GO      
*/

CREATE PROCEDURE dal.EntityAddress_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityAddressId             BIGINT,
      @entityId                    BIGINT,

      @addressType                                    INT,

      @line1                                      VARCHAR (055), 
      @line2                                      VARCHAR (055),
      @city                                       VARCHAR (030),
      @state                                         CHAR (002),
      @zipCode                                       CHAR (005),
      @zipPlus4                                      CHAR (004),
      @postalCode                                 VARCHAR (015),
      @county                                     VARCHAR (030),

      @longitude                                  DECIMAL (9, 6),
      @latitude                                   DECIMAL (9, 6),
        
      @effectiveDate          DATETIME,
      @terminationDate        DATETIME,
      
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
        
        
        -- CHECK FOR OVERLAPPING EXISTING SEGMENT
        
        DECLARE @existingEntityAddressId              AS BIGINT
        
        DECLARE @existingEntityAddressEffectiveDate   AS DATETIME
        
        DECLARE @existingEntityAddressTerminationDate AS DATETIME
        
               
        SELECT @existingEntityAddressId = EntityAddress.EntityAddressId, @existingEntityAddressEffectiveDate = EffectiveDate, @existingEntityAddressTerminationDate = TerminationDate
        
          FROM dbo.EntityAddress AS EntityAddress
          
          WHERE 
          
						EntityId = @entityId
            
            AND (AddressType = @addressType)
            
            AND (EffectiveDate <= @terminationDate) AND (TerminationDate >= @effectiveDate)
            
            AND (EntityAddressId <> @entityAddressId)
            
            
        IF ((@existingEntityAddressId IS NULL) AND (@entityAddressId != 0) AND (@terminationDate >= @effectiveDate)) 
        
          BEGIN 
          
            UPDATE dbo.EntityAddress 
            
              SET 
              
                EntityId = @entityId,
                AddressType = @addressType,

                Line1 = @line1,
                Line2 = @line2,
                City  = @city,
                State = @state,
                ZipCode = @zipCode,
                ZipPlus4 = @zipPlus4,
                PostalCode = @postalCode,
                County = @county,

                Longitude = @longitude,
                Latitude = @latitude,

                EffectiveDate = @effectiveDate,
                TerminationDate = @terminationDate,

                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName

              WHERE EntityAddressId = @entityAddressId -- NO SUPPORT FOR EXTERNAL ADDRESSES
             
          END
          
        ELSE IF ((@existingEntityAddressId IS NULL) AND (@entityAddressId = 0) AND (@terminationDate >= @effectiveDate)) 
        
          BEGIN
          
            INSERT dbo.EntityAddress (
            
                EntityId, AddressType, Line1, Line2, City, State, ZipCode, ZipPlus4, PostalCode, County, 
                
                Longitude, Latitude, EffectiveDate, TerminationDate,
                
                CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate,
                
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
              VALUES (
              
                @entityId, @addressType, @line1, @line2, @city, @state, @zipCode, @zipPlus4, @postalCode, @county,
                
                @longitude, @latitude, @effectiveDate, @terminationDate,
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
                
            
          END
          
        -- ELSE OVERLAP DETECTED, DO NOT SAVE
        
        
           
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityAddress_Insert TO PUBLIC
GO          
*/