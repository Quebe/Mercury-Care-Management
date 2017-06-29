/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberProblemStatementIdentified_Insert' AND type = 'P'))
  DROP PROCEDURE dbo.MemberProblemStatementIdentified_Insert
GO      
*/

CREATE PROCEDURE dbo.MemberProblemStatementIdentified_Insert
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId                BIGINT,
      @problemStatementId      BIGINT,
      @isRequired                 BIT,
                         
      @senderObjectType       VARCHAR (120),
      @senderObjectId          BIGINT,
      @eventObjectType        VARCHAR (120),
      @eventObjectId           BIGINT,
      @eventInstanceId         BIGINT,
      @eventDescription       VARCHAR (999),      
      
      @priority                  INT,

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
        
        DECLARE @memberProblemStatementIdentifiedId AS BIGINT

				DECLARE @modifiedDate AS DATETIME

        /* LOCAL VARIABLES ( END ) */
        

				SET @modifiedDate = GETDATE ()


				-- IDENTIFY EXISTING MEMBER PROBLEM STATEMENT TO ATTACH SENDER TO

        SELECT @memberProblemStatementIdentifiedId = MemberProblemStatementIdentifiedId 
        
           FROM MemberProblemStatementIdentified 
           
           WHERE (MemberId = @memberId)
					 
						AND (ProblemStatementId = @problemStatementId)
						
						AND (CompletionDate IS NULL)
           
        
        IF (@memberProblemStatementIdentifiedId IS NULL) 
        
          BEGIN

						-- NO EXISTING MEMBER PROBLEM STATEMENT EXISTS, CREATE NEW ONE
          

              INSERT INTO dbo.MemberProblemStatementIdentified (

									MemberId, ProblemStatementId, IdentifiedDate, IsRequired, MemberCaseId, CompletionDate,
                
                  CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                  ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                    
              VALUES (

								  @memberId, @problemStatementId, @modifiedDate, @isRequired, NULL, NULL,


                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate)
                    
              SET @memberProblemStatementIdentifiedId = @@IDENTITY
                
            
          END
          
          
        IF (@memberProblemStatementIdentifiedId IS NOT NULL) 
        
          BEGIN
        
            INSERT INTO dbo.MemberProblemStatementIdentifiedSender (

                  MemberProblemStatementIdentifiedId, 
									
									SenderObjectType, SenderObjectId, EventObjectType, EventObjectId, EventInstanceId, EventDescription, Priority,

                  CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                  ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                  
              VALUES (

                  @memberProblemStatementIdentifiedId, @senderObjectType, @senderObjectId , @eventObjectType, @eventObjectId, @eventInstanceId, @eventDescription, @priority,

                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate)
                                  
                  
          END
           
   
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_Insert TO PUBLIC
GO          
*/