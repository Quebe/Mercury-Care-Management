
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Population_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Population_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.Population_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @populationId               BIGINT,
      @populationName            VARCHAR (060),
      @populationDescription     VARCHAR (999),
      @populationTypeId           BIGINT,
      
      @allowProspective              BIT,
      @initialAnchorDate             INT,
      
      @enabled                       BIT,
      @visible                       BIT,
      
      @onBeforeMembershipAddWorkflowId       BIGINT,
      
      @onMembershipAddActionId               BIGINT,
      @onMembershipAddActionParameters          XML,
      @onMembershipAddActionDescription     VARCHAR (099),
      
      @onBeforeMembershipTerminateWorkflowId   BIGINT,
      
      @onMembershipTerminateActionId           BIGINT,
      @onMembershipTerminateActionParameters      XML,
      @onMembershipTerminateActionDescription VARCHAR (099),      
      
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

        IF EXISTS (SELECT * FROM dbo.Population WHERE PopulationId = @populationId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.Population
              SET
                PopulationName = @populationName,
                PopulationDescription = @populationDescription,
                PopulationTypeId = @populationTypeId,
                
                AllowProspective  = @allowProspective,
                InitialAnchorDate = @initialAnchorDate,
                
                Enabled = @enabled,
                Visible = @visible,
               
                OnBeforeMembershipAddWorkflowId = @onBeforeMembershipAddWorkflowId,
               
                OnMembershipAddActionId = @onMembershipAddActionId,
                OnMembershipAddActionParameters = @onMembershipAddActionParameters,
                OnMembershipAddActionDescription = @onMembershipAddActionDescription,
                
                OnBeforeMembershipTerminateWorkflowId = @onBeforeMembershipTerminateWorkflowId,
                
                OnMembershipTerminateActionId = @onMembershipTerminateActionId,
                OnMembershipTerminateActionParameters = @onMembershipTerminateActionParameters,
                OnMembershipTerminateActionDescription = @onMembershipTerminateActionDescription,
                
                ExtendedProperties = @extendedProperties,
                          
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                PopulationId = @populationId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.Population (PopulationName, PopulationDescription, PopulationTypeId, AllowProspective, InitialAnchorDate, Enabled, Visible, 
            
                OnBeforeMembershipAddWorkflowId, OnMembershipAddActionId, OnMembershipAddActionParameters, OnMembershipAddActionDescription,
                OnBeforeMembershipTerminateWorkflowId, OnMembershipTerminateActionId, OnMembershipTerminateActionParameters, OnMembershipTerminateActionDescription,
                
                ExtendedProperties,
               
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @populationName, @populationDescription, @populationTypeId, @allowProspective, @initialAnchorDate, @enabled, @visible, 

                @onBeforeMembershipAddWorkflowId, @onMembershipAddActionId, @onMembershipAddActionParameters, @onMembershipAddActionDescription,
                @onBeforeMembershipTerminateWorkflowId, @onMembershipTerminateActionId,  @onMembershipTerminateActionParameters, @onMembershipTerminateActionDescription,
                
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