/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ParseDelimitedTextField' AND type = 'P'))
  DROP PROCEDURE dbo.ParseDelimitedTextField
GO      
*/

CREATE PROCEDURE dbo.ParseDelimitedTextField
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

				SET @endPosition = CHARINDEX (@delimiter, @sourceString, 1)

				IF (@endPosition > 0) 

					BEGIN

						SET @fieldText = SUBSTRING (@sourceString, 1, @endPosition - 1)

					END

				ELSE

					BEGIN

						SET @fieldText = @sourceString

					END

    
				SET @returnString = SUBSTRING (@sourceString, @endposition + 1, LEN (@sourceString))

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.ParseDelimitedTextField TO PUBLIC
GO          
*/





	