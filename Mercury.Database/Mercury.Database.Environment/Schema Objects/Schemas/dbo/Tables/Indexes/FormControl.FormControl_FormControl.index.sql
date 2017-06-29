CREATE NONCLUSTERED INDEX FormControl_FormControl ON [dbo].[FormControl] 
( FormId ASC, ControlId ASC, ControlName ASC)
INCLUDE (ControlTitle, Enabled, Visible) 
