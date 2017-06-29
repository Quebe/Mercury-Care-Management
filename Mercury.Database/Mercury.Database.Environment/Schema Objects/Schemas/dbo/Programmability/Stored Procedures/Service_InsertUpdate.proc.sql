/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Service_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Service_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.Service_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceId               BIGINT,
      @serviceName            VARCHAR (060),
      @serviceDescription     VARCHAR (999),
      @serviceType                INT,
      @serviceClassification      INT,
      @lastPaidDate          DATETIME,

			@setType                    INT,
			@setWithinDays              INT,

      @extendedProperties         XML,

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

        IF EXISTS (SELECT * FROM dbo.Service WHERE ServiceId = @serviceId AND (@serviceId <> 0))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.Service
              SET
                ServiceName = @serviceName,
                ServiceDescription = @serviceDescription,
                ServiceType = @serviceType,
                ServiceClassification = @serviceClassification,
                LastPaidDate = @lastPaidDate,

								SetType = @setType,
								SetWithinDays = @setWithinDays,
	                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ServiceId = @serviceId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.Service (ServiceName, ServiceDescription, ServiceType, ServiceClassification, LastPaidDate, 
            
								SetType, SetWithinDays,
            
								Enabled, Visible, ExtendedProperties,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @serviceName, @serviceDescription, @serviceType, @serviceClassification, @lastPaidDate, 
                
                @setType, @setWithinDays,
                
                @enabled, @visible, @extendedProperties,

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