CREATE INDEX PharmacyClaim_PaidDateNdc
	
	ON [dbo].[PharmacyClaim] ([PaidDate],[NationalDrugCode])
	
	INCLUDE ([PharmacyClaimId],[MemberId],[PrescriberProviderId],[ServiceDate],[Units],[Status])
