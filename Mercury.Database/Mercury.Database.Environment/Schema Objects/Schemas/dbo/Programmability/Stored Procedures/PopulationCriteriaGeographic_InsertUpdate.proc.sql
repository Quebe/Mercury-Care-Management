
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationCriteriaGeographic_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationCriteriaGeographic_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.PopulationCriteriaGeographic_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @criteriaGeographicId      BIGINT,
      @populationId              BIGINT,

      @state                  CHAR (002),
      @city                VARCHAR (030),
      @county              VARCHAR (030),
      @zipCode                CHAR (005),
      
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

        IF EXISTS (SELECT * FROM dbo.PopulationCriteriaGeographic WHERE PopulationCriteriaGeographicId = @criteriaGeographicId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.PopulationCriteriaGeographic
              SET
                State = @state,
                City = @city,
                County = @county,
                ZipCode = @zipCode,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                PopulationCriteriaGeographicId = @criteriaGeographicId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.PopulationCriteriaGeographic (PopulationId, State, City, County, ZipCode,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @populationId, @state, @city, @county, @zipCode,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.PopulationCriteriaGeographic_InsertUpdate TO PUBLIC
GO          
*/