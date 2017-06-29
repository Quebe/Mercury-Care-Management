/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorerNode_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.DataExplorerNode_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.DataExplorerNode_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @dataExplorerNodeId                BIGINT,
      @dataExplorerNodeName             VARCHAR (060),
      @dataExplorerNodeDescription      VARCHAR (999),

			@dataExplorerId    BIGINT,


			@nodeType INT,
			@parentDataExplorerNodeId BIGINT,
      
      @enabled                    BIT,
      @visible                    BIT,

      @extendedProperties         XML,
      
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

        IF EXISTS (SELECT * FROM dbo.DataExplorerNode WHERE DataExplorerNodeId = @dataExplorerNodeId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.DataExplorerNode
              SET
                DataExplorerNodeName = @dataExplorerNodeName,
                DataExplorerNodeDescription = @dataExplorerNodeDescription,

								DataExplorerId = @dataExplorerId,

								NodeType = @nodeType,

								ParentDataExplorerNodeId = @parentDataExplorerNodeId,

								Enabled = @enabled,
                Visible = @visible,

                ExtendedProperties = @extendedProperties,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                DataExplorerNodeId = @dataExplorerNodeId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.DataExplorerNode (
                DataExplorerNodeName, DataExplorerNodeDescription, 

								DataExplorerId, NodeType, ParentDataExplorerNodeId,
                
                Enabled, Visible, 
                
                ExtendedProperties,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @dataExplorerNodeName, @dataExplorerNodeDescription, 

								@dataExplorerId, @nodeType, @parentDataExplorerNodeId,
          
                @enabled, @visible, 
                
                @extendedProperties,

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