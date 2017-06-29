CREATE INDEX MemberServiceDetailSet_ParentService
ON MemberServiceDetailSet (ParentServiceId) INCLUDE (DetailMemberServiceId, MemberServiceId)


