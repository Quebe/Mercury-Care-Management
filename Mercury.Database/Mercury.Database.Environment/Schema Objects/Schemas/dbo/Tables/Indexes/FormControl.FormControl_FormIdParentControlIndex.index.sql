CREATE INDEX [FormControl_FormIdParentControlIndex]
    ON [dbo].FormControl
	(FormId, ParentId, ControlIndex) INCLUDE (ControlId)


