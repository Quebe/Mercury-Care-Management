CREATE NONCLUSTERED INDEX WorkQueueItemWorkflowStep_WorkQueueItemId 
    ON WorkQueueItemWorkflowStep (WorkQueueItemId, StepDate, StepSequence)


