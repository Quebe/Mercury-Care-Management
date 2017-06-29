CREATE INDEX [Authentication_Idx_SessionToken]
    ON audit.Authentication
	(SessionToken, LogonDateTime, LogoffDateTime)


