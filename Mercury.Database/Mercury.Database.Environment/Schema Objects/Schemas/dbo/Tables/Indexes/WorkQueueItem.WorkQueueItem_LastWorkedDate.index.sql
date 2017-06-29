
CREATE NONCLUSTERED INDEX [WorkQueueItem_LastWorkedDate]
ON [dbo].[WorkQueueItem] ([LastWorkedDate])
INCLUDE ([WorkQueueItemId],[WorkQueueId],[CompletionDate])