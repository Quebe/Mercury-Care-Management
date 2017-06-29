
CREATE NONCLUSTERED INDEX [EntityCorrespondence_SentDate]
  ON EntityCorrespondence (SentDate, CorrespondenceId, EntityId, AddressZipCode) INCLUDE (EntityCorrespondenceId, ReadyToSendDate)
  