CREATE NONCLUSTERED INDEX WorkQueueItemWorkflowStep_StepDate 
    ON WorkQueueItemWorkflowStep (StepDate, WorkQueueItemId, StepSequence)


