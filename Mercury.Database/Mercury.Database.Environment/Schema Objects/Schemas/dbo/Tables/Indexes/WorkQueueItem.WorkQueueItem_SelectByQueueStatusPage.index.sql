CREATE INDEX [WorkQueueItem_SelectByQueueStatusPage]
    ON [dbo].WorkQueueItem
(
	[WorkQueueId] ASC,
	[AssignedToSecurityAuthorityId] ASC,
	[CompletionDate] ASC,
	[MilestoneDate] ASC,
	[DueDate] ASC,
	[LastWorkedDate] ASC,
	[AddedDate] ASC,
	[WorkQueueItemId] ASC
)


