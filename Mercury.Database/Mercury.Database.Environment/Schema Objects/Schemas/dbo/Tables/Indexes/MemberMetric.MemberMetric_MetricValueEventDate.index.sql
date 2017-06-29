CREATE NONCLUSTERED INDEX MemberMetric_MetricValueEventDate

	ON [dbo].[MemberMetric] ([MetricId],[MetricValue],[EventDate])

	INCLUDE ([MemberId])