﻿CREATE INDEX ClaimLine_ProcedureModifier ON dbo.ClaimLine (ProcedureCode, ModifierCode1) INCLUDE (ClaimId)