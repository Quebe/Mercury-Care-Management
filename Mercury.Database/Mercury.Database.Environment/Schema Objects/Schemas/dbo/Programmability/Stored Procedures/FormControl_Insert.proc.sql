/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'FormControl_Insert' AND type = 'P'))
  DROP PROCEDURE FormControl_Insert
GO      
*/

CREATE PROCEDURE FormControl_Insert
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @formId                 BIGINT,
      @entityFormId           BIGINT,

      @controlId             VARCHAR (040),
      @parentId              VARCHAR (040),
      @controlIndex              INT,
      @controlName           VARCHAR (060),
      @controlTitle          VARCHAR (060),
      @controlType               INT,
      
      @tabIndex             SMALLINT,
      @enabled                   BIT,
      @visible                   BIT,
      
      @style                     XML,
      @extendedProperties        XML,
      @dataBindings              XML,
      @eventHandlers             XML,
      @value                     XML
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF ((@entityFormId IS NULL) OR (@entityFormId = 0)) 
        
          BEGIN

            INSERT INTO FormControl (
            
                FormId, ControlId, ParentId, ControlIndex,
                ControlName, ControlTitle, ControlType, 
                
                TabIndex, Enabled, Visible, Style, ExtendedProperties, DataBindings, EventHandlers, Value

            )
                
            VALUES (
            
              @formId, @controlId, @parentId, @controlIndex,
              @controlName, @controlTitle, @controlType, 
              @tabIndex, @enabled, @visible, @style, @extendedProperties, @dataBindings, @eventHandlers, @value
                                        
            )
            
          END
          
        ELSE

          BEGIN        
        
            INSERT INTO EntityFormControl (
            
                EntityFormId, ControlId, ParentId, ControlIndex,
                ControlName, ControlTitle, ControlType, 
                
                TabIndex, Enabled, Visible, Style, ExtendedProperties, DataBindings, EventHandlers, Value

            )
                
            VALUES (
            
              @entityFormId, @controlId, @parentId, @controlIndex,
              @controlName, @controlTitle, @controlType, 
              @tabIndex, @enabled, @visible, @style, @extendedProperties, @dataBindings, @eventHandlers, @value
                                        
            )
            
          END

       
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON FormControl_Insert TO PUBLIC
GO          
*/