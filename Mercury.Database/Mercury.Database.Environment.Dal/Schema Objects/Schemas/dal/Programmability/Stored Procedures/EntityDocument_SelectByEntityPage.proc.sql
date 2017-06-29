/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityDocument_SelectByEntityPage' AND type = 'P'))
  DROP PROCEDURE dal.EntityDocument_SelectByEntityPage
GO      
*/

CREATE PROCEDURE dal.EntityDocument_SelectByEntityPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId             BIGINT,
      @initialRow              INT,
      @count                   INT
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  
  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
                
        SELECT *

          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY ModifiedDate DESC, DocumentId DESC) AS RowNumber, *

              FROM (

                SELECT 
                
                    'Form' AS DocumentType, 

                    EntityFormId AS EntityDocumentId, 
                    
                    FormId AS DocumentId,
                    
                    FormName AS DocumentName,
                    
                    EntityFormId AS EntityFormId,

                    EntityId AS EntityId,

                    CONVERT (CHAR (08), ModifiedDate, 112) + '.' + REPLACE (CONVERT (CHAR (10), ModifiedDate, 108), ':', '') AS Version,
                    
                    CAST (0 AS INT) AS ContactType,
    
                    NULL AS ReadyToSendDate,

                    NULL AS SentDate,
                    
                    NULL AS ReceivedDate,

                    NULL AS ReturnedDate,
                    
                    CAST (0 AS BIT) AS HasImage,

                    CreateAuthorityName,
                  
                    CreateAccountId,

                    CreateDate,

                    CreateAccountName,

                    ModifiedAuthorityName,
                
                    ModifiedAccountId,

                    ModifiedAccountName,

                    ModifiedDate

                  FROM dbo.EntityForm 

                  WHERE (EntityId = @entityId)
                                                     
                UNION ALL SELECT 

                    'Correspondence' AS DocumentType, 

                    EntityCorrespondence.EntityCorrespondenceId AS EntityDocumentId,
                    
                    EntityCorrespondence.CorrespondenceId AS DocumentId,
                    
                    EntityCorrespondence.CorrespondenceName AS DocumentName,               
                    
                    EntityCorrespondence.EntityFormId AS EntityFormId,     
                    
                    EntityCorrespondence.EntityId AS EntityId,
                    
                    EntityCorrespondence.CorrespondenceVersion AS Version,
                    
                    EntityCorrespondence.ContactType AS ContactType,

                    EntityCorrespondence.ReadyToSendDate,

                    EntityCorrespondence.SentDate AS SentDate,
                    
                    EntityCorrespondence.ReceivedDate AS ReceivedDate,

                    EntityCorrespondence.ReturnedDate AS ReturnedDate,
                    
                    CAST (CASE WHEN (EntityCorrespondenceImage.EntityCorrespondenceId IS NULL) THEN 0 ELSE 1 END AS BIT) AS HasImage,

                    EntityCorrespondence.CreateAuthorityName,
                  
                    EntityCorrespondence.CreateAccountId,

                    EntityCorrespondence.CreateDate,

                    EntityCorrespondence.CreateAccountName,

                    EntityCorrespondence.ModifiedAuthorityName,
                
                    EntityCorrespondence.ModifiedAccountId,

                    EntityCorrespondence.ModifiedAccountName,

                    EntityCorrespondence.ModifiedDate

                  FROM EntityCorrespondence
                  
                    LEFT JOIN EntityCorrespondenceImage ON EntityCorrespondence.EntityCorrespondenceId = EntityCorrespondenceImage.EntityCorrespondenceId
                      
                  WHERE EntityCorrespondence.EntityId = @entityId
                  
                ) AS EntityDocument

            ) AS EntityDocumentPage

        WHERE EntityDocumentPage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
                  
	    END    
              
