CREATE INDEX [LabResult_ReportedDateLoinc]
	
	ON [dbo].[LabResult] ([ReportedDate],[Loinc])

	INCLUDE ([LabResultId],[MemberId],[ProviderId])