﻿/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityAddress_SelectByEntity' AND type = 'P'))
  DROP PROCEDURE dal.EntityAddress_SelectByEntity
GO      
*/

CREATE PROCEDURE dal.EntityAddress_SelectByEntity
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId      BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT EntityAddress.* FROM dbo.EntityAddress AS EntityAddress WHERE EntityId = @entityId

    END        