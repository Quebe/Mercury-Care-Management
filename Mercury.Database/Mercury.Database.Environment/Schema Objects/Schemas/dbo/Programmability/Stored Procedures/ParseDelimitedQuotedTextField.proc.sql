/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ParseDelimitedQuotedTextField' AND type = 'P'))
  DROP PROCEDURE dbo.ParseDelimitedQuotedTextField
GO      
*/

CREATE PROCEDURE dbo.ParseDelimitedQuotedTextField
    /* STORED PROCEDURE - INPUTS (BEGIN) */

			@sourceString VARCHAR (8000),

			@delimiter CHAR (1),

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


				EXEC ParseDelimitedTextField @sourceString, @delimiter, @fieldText OUT, @endPosition OUT, @returnString OUT

				if (LEN (@fieldText) > 1) 

					BEGIN

						SET @fieldText = RIGHT (@fieldText, LEN (@fieldText) -1)

						SET @fieldText = LEFT (@fieldText, LEN (@fieldText) -1)

					END

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.ParseDelimitedQuotedTextField TO PUBLIC
GO          
*/





	