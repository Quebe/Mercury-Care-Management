﻿CREATE INDEX PopulationServiceEventThreshold_ServiceEventId 

  ON PopulationServiceEventThreshold (PopulationServiceEventId, PopulationServiceEventThresholdId) INCLUDE (RelativeDateValue, RelativeDateQualifier)