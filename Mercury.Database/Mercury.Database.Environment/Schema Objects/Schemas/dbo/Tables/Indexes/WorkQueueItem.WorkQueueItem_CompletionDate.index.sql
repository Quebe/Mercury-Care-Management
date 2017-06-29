CREATE INDEX [WorkQueueItem_CompletionDate]
ON [dbo].[WorkQueueItem] ([CompletionDate])
INCLUDE ([WorkQueueId],[ConstraintDate],[WorkTimeRestrictions],[AssignedToSecurityAuthorityId])


