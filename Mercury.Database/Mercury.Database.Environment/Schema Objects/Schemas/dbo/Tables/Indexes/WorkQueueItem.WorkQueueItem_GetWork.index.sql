CREATE INDEX [WorkQueueItem_GetWork]
    
    ON WorkQueueItem (WorkQueueId, AssignedToSecurityAuthorityId, CompletionDate, MilestoneDate, DueDate, LastWorkedDate, AddedDate, WorkQueueItemId)


