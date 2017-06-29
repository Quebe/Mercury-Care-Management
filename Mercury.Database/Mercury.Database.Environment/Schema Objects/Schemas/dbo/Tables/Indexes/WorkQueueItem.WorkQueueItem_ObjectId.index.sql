
CREATE INDEX WorkQueueItem_ItemObjectId ON WorkQueueItem (ItemObjectId, ItemObjectType) INCLUDE (WorkQueueItemId, WorkQueueItemName)
