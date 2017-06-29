/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ParseQuotedTextField' AND type = 'P'))
  DROP PROCEDURE dbo.ParseQuotedTextField
GO      
*/

CREATE PROCEDURE dbo.ParseQuotedTextField
    /* STORED PROCEDURE - INPUTS (BEGIN) */

			@sourceString VARCHAR (8000),

    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE

			@fieldText VARCHAR (8000) OUTPUT,

			@endPosition INT OUTPUT,

			@returnString VARCHAR (8000) OUTPUT

    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

				DECLARE @startPosition AS INTEGER


				SET @startPosition = CHARINDEX ('"', @sourceString, 1)

				SET @endPosition = CHARINDEX ('"', @sourceString, @startPosition + 1)

				IF ((@endPosition - @startPosition - 1) > 0) 

					BEGIN

						SET @fieldText = SUBSTRING (@sourceString, @startPosition + 1, (@endPosition - @startPosition - 1))

					END

				ELSE

					BEGIN

						SET @fieldText = ''

					END

    
				SET @returnString = SUBSTRING (@sourceString, @endposition + 1, LEN (@sourceString))

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.ParseQuotedTextField TO PUBLIC
GO          
*/





	