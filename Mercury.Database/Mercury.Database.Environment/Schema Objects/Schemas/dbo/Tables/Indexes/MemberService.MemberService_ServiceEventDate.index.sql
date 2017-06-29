CREATE INDEX [MemberService_ServiceEventDate]

	ON [dbo].MemberService (ServiceId, EventDate)
	
	INCLUDE (MemberServiceId, MemberId);


