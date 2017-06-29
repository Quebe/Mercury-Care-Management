/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EnvironmentAccess_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.EnvironmentAccess_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.EnvironmentAccess_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @environmentId         BIGINT, 
      @securityAuthorityId   BIGINT,
      @securityGroupId       VARCHAR (060),
      @isGranted             BIT,
      @isDenied              BIT,
      
      @modifiedAuthorityName VARCHAR (060),
      @modifiedAccountId     VARCHAR (060),
      @modifiedAccountName   VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

		  IF ((@isGranted = 0) AND (@isDenied = 0))  
		    BEGIN 
  
          DELETE FROM EnvironmentAccess 
              WHERE 
                    EnvironmentId       = @environmentId
                AND SecurityAuthorityId = @securityAuthorityId
                AND SecurityGroupId     = @securityGroupId

        END

      ELSE
        BEGIN

          IF EXISTS (SELECT * FROM EnvironmentAccess WHERE EnvironmentId = @environmentId AND SecurityAuthorityId = @securityAuthorityId AND SecurityGroupId = @securityGroupId) 
            BEGIN
              -- EXISTING RECORD, UPDATE
              
              UPDATE EnvironmentAccess
                SET
                  IsGranted = CASE WHEN (@isDenied = 1) THEN 0 ELSE @isGranted END,
                  IsDenied  = @isDenied,
        
                  ModifiedAuthorityName = @modifiedAuthorityName,
                  ModifiedAccountId     = @modifiedAccountId,
                  ModifiedAccountName   = @modifiedAccountName,
                  ModifiedDate          = GETDATE ()
                  
                WHERE 
                      EnvironmentId       = @environmentId
                  AND SecurityAuthorityId = @securityAuthorityId
                  AND SecurityGroupId     = @securityGroupId
            
            END
            
          ELSE -- NO EXISTING RECORD, INSERT NEW
            BEGIN
            
              INSERT INTO EnvironmentAccess (EnvironmentId, SecurityAuthorityId, SecurityGroupId, IsGranted, IsDenied,
                  CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                  ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                  
              VALUES (@environmentId, @securityAuthorityId, @securityGroupId, 
                  CASE WHEN (@isDenied = 1) THEN 0 ELSE @isGranted END, @isDenied, 
                  
                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
             
            END                       
      END

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.EnvironmentAccess_InsertUpdate TO PUBLIC
GO          
*/