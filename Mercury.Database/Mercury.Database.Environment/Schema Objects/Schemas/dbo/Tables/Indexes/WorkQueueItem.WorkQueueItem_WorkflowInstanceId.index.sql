
CREATE INDEX WorkQueueItem_WorkflowInstanceId ON WorkQueueItem (WorkflowInstanceId) INCLUDE (WorkQueueItemId)
