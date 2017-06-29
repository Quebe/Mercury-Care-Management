/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Printer_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Printer_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.Printer_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @printerId              BIGINT,
      @printerName           VARCHAR (060),
      @printerDescription    VARCHAR (999),
      
      @printServerName VARCHAR (255),
      @printQueueName  VARCHAR (255),
      
      @extendedProperties          XML,

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

        IF EXISTS (SELECT * FROM dbo.Printer WHERE ((PrinterId = @printerId) AND (@printerId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.Printer
              SET
                PrinterName = @printerName,
                PrinterDescription = @printerDescription,
                                
                PrintServerName = @printServerName,
                PrintQueueName = @printQueueName,
                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                PrinterId = @printerId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.Printer (
                PrinterName, PrinterDescription, PrintServerName, PrintQueueName, ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @printerName, @printerDescription, @printServerName, @printQueueName, @extendedProperties, @enabled, @visible, 

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