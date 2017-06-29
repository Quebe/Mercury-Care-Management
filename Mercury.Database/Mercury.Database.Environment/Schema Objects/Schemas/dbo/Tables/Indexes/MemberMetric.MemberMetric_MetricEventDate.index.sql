CREATE INDEX [MemberMetric_MetricEventDate]
ON [dbo].MemberMetric (MetricId, EventDate) INCLUDE ([MemberMetricId],[MemberId]);


