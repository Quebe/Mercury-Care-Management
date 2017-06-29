CREATE INDEX [WorkQueueItem_SelectByAssignedPage]
    
    ON WorkQueueItem (AssignedToSecurityAuthorityId, AssignedToUserAccountId, CompletionDate, WorkQueueItemId, MilestoneDate, DueDate, LastWorkedDate, AddedDate)


