CREATE INDEX [WorkQueueItem_CountByQueueStatus] 

  ON WorkQueueItem (WorkQueueId, AssignedToSecurityAuthorityId, CompletionDate)    


