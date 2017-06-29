
  CREATE INDEX [EntityCorrespondence_EntityId]
  ON EntityCorrespondence (EntityId, SentDate) INCLUDE (EntityCorrespondenceId, CorrespondenceId)