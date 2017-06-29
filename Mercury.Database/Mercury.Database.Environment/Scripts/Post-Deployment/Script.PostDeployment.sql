/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- SEED DATE QUALIFIER (BEGIN) 

PRINT '-- SEED DATE TYPE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.DateQualifier WHERE DateQualifier = 0) INSERT INTO enum.DateQualifier (DateQualifier, DateQualifierName) VALUES (0, 'Days')
    
	IF NOT EXISTS (SELECT * FROM enum.DateQualifier WHERE DateQualifier = 1) INSERT INTO enum.DateQualifier (DateQualifier, DateQualifierName) VALUES (1, 'Months')
    
	IF NOT EXISTS (SELECT * FROM enum.DateQualifier WHERE DateQualifier = 2) INSERT INTO enum.DateQualifier (DateQualifier, DateQualifierName) VALUES (2, 'Years')
     
PRINT '-- SEED DATE QUALIFIER ( END ) '

-- SEED DATE QUALIFIER ( END ) 


-- SEED WORK TEAM TYPE (BEGIN) 

PRINT '-- SEED WORK TEAM TYPE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.WorkTeamType WHERE WorkTeamType = 0) INSERT INTO enum.WorkTeamType (WorkTeamType, WorkTeamTypeName) VALUES (0, 'Work Team')
    
	IF NOT EXISTS (SELECT * FROM enum.WorkTeamType WHERE WorkTeamType = 1) INSERT INTO enum.WorkTeamType (WorkTeamType, WorkTeamTypeName) VALUES (1, 'Care Team')
        
PRINT '-- SEED WORK TEAM TYPE ( END ) '

-- SEED WORK TEAM TYPE ( END ) 


-- SEED WORK TEAM ROLE (BEGIN) 

PRINT '-- SEED WORK TEAM ROLE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.WorkTeamRole WHERE WorkTeamRole = 0) INSERT INTO enum.WorkTeamRole (WorkTeamRole, WorkTeamRoleName) VALUES (0, 'Member')
    
	IF NOT EXISTS (SELECT * FROM enum.WorkTeamRole WHERE WorkTeamRole = 1) INSERT INTO enum.WorkTeamRole (WorkTeamRole, WorkTeamRoleName) VALUES (1, 'Manager')
        
PRINT '-- SEED WORK TEAM ROLE ( END ) '

-- SEED WORK TEAM ROLE ( END ) 


-- SEED WORK QUEUE PERMISSION (BEGIN) 

PRINT '-- SEED WORK QUEUE PERMISSION (BEGIN) '

	IF NOT EXISTS (SELECT * FROM enum.WorkQueuePermission WHERE WorkQueuePermission = 0) INSERT INTO enum.WorkQueuePermission (WorkQueuePermission, WorkQueuePermissionName) VALUES (0, 'Denied')
    
    IF NOT EXISTS (SELECT * FROM enum.WorkQueuePermission WHERE WorkQueuePermission = 1) INSERT INTO enum.WorkQueuePermission (WorkQueuePermission, WorkQueuePermissionName) VALUES (1, 'View')
        
	IF NOT EXISTS (SELECT * FROM enum.WorkQueuePermission WHERE WorkQueuePermission = 2) INSERT INTO enum.WorkQueuePermission (WorkQueuePermission, WorkQueuePermissionName) VALUES (2, 'Work')
    
    IF NOT EXISTS (SELECT * FROM enum.WorkQueuePermission WHERE WorkQueuePermission = 3) INSERT INTO enum.WorkQueuePermission (WorkQueuePermission, WorkQueuePermissionName) VALUES (3, 'Self Assign')

    IF NOT EXISTS (SELECT * FROM enum.WorkQueuePermission WHERE WorkQueuePermission = 4) INSERT INTO enum.WorkQueuePermission (WorkQueuePermission, WorkQueuePermissionName) VALUES (4, 'Manage')

PRINT '-- SEED WORK QUEUE PERMISSION ( END ) '

-- SEED WORK QUEUE PERMISSION ( END ) 


-- SEED ENTITY TYPE (BEGIN) 

PRINT '-- SEED ENTITY TYPE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.EntityType WHERE EntityType = 0) INSERT INTO enum.EntityType (EntityType, EntityTypeName) VALUES (0, 'Not Specified')
    
	IF NOT EXISTS (SELECT * FROM enum.EntityType WHERE EntityType = 1) INSERT INTO enum.EntityType (EntityType, EntityTypeName) VALUES (1, 'Member')
    
	IF NOT EXISTS (SELECT * FROM enum.EntityType WHERE EntityType = 2) INSERT INTO enum.EntityType (EntityType, EntityTypeName) VALUES (2, 'Provider')
 
	IF NOT EXISTS (SELECT * FROM enum.EntityType WHERE EntityType = 3) INSERT INTO enum.EntityType (EntityType, EntityTypeName) VALUES (3, 'Sponsor')
    
	IF NOT EXISTS (SELECT * FROM enum.EntityType WHERE EntityType = 4) INSERT INTO enum.EntityType (EntityType, EntityTypeName) VALUES (4, 'Insurer')

	IF NOT EXISTS (SELECT * FROM enum.EntityType WHERE EntityType = 5) INSERT INTO enum.EntityType (EntityType, EntityTypeName) VALUES (5, 'Pharmacy')
    
	IF NOT EXISTS (SELECT * FROM enum.EntityType WHERE EntityType = 6) INSERT INTO enum.EntityType (EntityType, EntityTypeName) VALUES (6, 'Drug Manufacturer or Firm')

PRINT '-- SEED ENTITY TYPE ( END ) '

-- SEED ENTITY TYPE ( END ) 



-- SEED ACTIVITY SCHEDULE TYPE (BEGIN) 

PRINT '-- SEED ACTIVITY SCHEDULE TYPE (BEGIN) '

	IF NOT EXISTS (SELECT * FROM enum.ActivityScheduleType WHERE ActivitySchedule = 0) INSERT INTO enum.ActivityScheduleType (ActivitySchedule, ActivityScheduleName) VALUES (0, 'By Frequency')

	IF NOT EXISTS (SELECT * FROM enum.ActivityScheduleType WHERE ActivitySchedule = 1) INSERT INTO enum.ActivityScheduleType (ActivitySchedule, ActivityScheduleName) VALUES (1, 'Monthly')

	IF NOT EXISTS (SELECT * FROM enum.ActivityScheduleType WHERE ActivitySchedule = 2) INSERT INTO enum.ActivityScheduleType (ActivitySchedule, ActivityScheduleName) VALUES (2, 'Quarterly')

	IF NOT EXISTS (SELECT * FROM enum.ActivityScheduleType WHERE ActivitySchedule = 3) INSERT INTO enum.ActivityScheduleType (ActivitySchedule, ActivityScheduleName) VALUES (3, 'Yearly')

	IF NOT EXISTS (SELECT * FROM enum.ActivityScheduleType WHERE ActivitySchedule = 4) INSERT INTO enum.ActivityScheduleType (ActivitySchedule, ActivityScheduleName) VALUES (4, 'Birth Month')

	IF NOT EXISTS (SELECT * FROM enum.ActivityScheduleType WHERE ActivitySchedule = 5) INSERT INTO enum.ActivityScheduleType (ActivitySchedule, ActivityScheduleName) VALUES (5, 'Calendar Month')

PRINT '-- SEED ACTIVITY SCHEDULE TYPE ( END ) '

-- SEED ACTIVITY SCHEDULE TYPE ( END )



-- SEED ADDRESS TYPE (BEGIN) 

PRINT '-- SEED ADDRESS TYPE (BEGIN) '

	IF NOT EXISTS (SELECT * FROM enum.AddressType WHERE AddressType = 0) INSERT INTO enum.AddressType (AddressType, AddressTypeName) VALUES (0, 'Not Specified')

	IF NOT EXISTS (SELECT * FROM enum.AddressType WHERE AddressType = 1) INSERT INTO enum.AddressType (AddressType, AddressTypeName) VALUES (1, 'Physical Address')

	IF NOT EXISTS (SELECT * FROM enum.AddressType WHERE AddressType = 31) INSERT INTO enum.AddressType (AddressType, AddressTypeName) VALUES (31, 'Mailing  Address')

	IF NOT EXISTS (SELECT * FROM enum.AddressType WHERE AddressType = 77) INSERT INTO enum.AddressType (AddressType, AddressTypeName) VALUES (77, 'Service Location')

	IF NOT EXISTS (SELECT * FROM enum.AddressType WHERE AddressType = 101) INSERT INTO enum.AddressType (AddressType, AddressTypeName) VALUES (101, 'Alternate Physical Address')

	IF NOT EXISTS (SELECT * FROM enum.AddressType WHERE AddressType = 131) INSERT INTO enum.AddressType (AddressType, AddressTypeName) VALUES (131, 'Alternate Mailing Address')

	IF NOT EXISTS (SELECT * FROM enum.AddressType WHERE AddressType = 201) INSERT INTO enum.AddressType (AddressType, AddressTypeName) VALUES (201, 'Corrected Physical Address')

	IF NOT EXISTS (SELECT * FROM enum.AddressType WHERE AddressType = 231) INSERT INTO enum.AddressType (AddressType, AddressTypeName) VALUES (231, 'Corrected Mailing Address')
        
PRINT '-- SEED ADDRESS TYPE ( END ) '

-- SEED ADDRESS TYPE ( END )


-- SEED CONTACT OUTCOME ENUMERATION (BEGIN)

PRINT '-- SEED CONTACT OUTCOME ENUMERATION (BEGIN)'

  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 0) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (0, 'Not Specified')    

  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 1) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (1, 'Success')

  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 2) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (2, 'No Answer')

  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 3) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (3, 'Left Message')
  
  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 4) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (4, 'Busy')
  
  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 5) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (5, 'Wrong Number')
  
  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 6) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (6, 'Disconnected')
  
  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 7) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (7, 'Refused')
  
  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 8) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (8, 'Rescheduled')
  
  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 9) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (9, 'Not Available')

  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 10) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (10, 'Language Barrier')
  
  IF NOT EXISTS (SELECT * FROM enum.ContactOutcome WHERE ContactOutcome = 11) INSERT INTO enum.ContactOutcome (ContactOutcome, ContactOutcomeName) VALUES (11, 'Deceased')

PRINT '-- SEED CONTACT OUTCOME ENUMERATION ( END )'    
    
-- SEED CONTACT OUTCOME ENUMERATION ( END )


-- SEED SERVICE EVENT STATUS (BEGIN) 

PRINT '-- SEED SERVICE EVENT STATUS (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.ServiceEventStatus WHERE Status = 0) INSERT INTO enum.ServiceEventStatus (Status, StatusName) VALUES (0, 'Compliant or No Change') 
    
	IF NOT EXISTS (SELECT * FROM enum.ServiceEventStatus WHERE Status = 1) INSERT INTO enum.ServiceEventStatus (Status, StatusName) VALUES (1, 'Open')
    
	IF NOT EXISTS (SELECT * FROM enum.ServiceEventStatus WHERE Status = 2) INSERT INTO enum.ServiceEventStatus (Status, StatusName) VALUES (2, 'Open - Informational')
 
	IF NOT EXISTS (SELECT * FROM enum.ServiceEventStatus WHERE Status = 3) INSERT INTO enum.ServiceEventStatus (Status, StatusName) VALUES (3, 'Open - Warning')
    
	IF NOT EXISTS (SELECT * FROM enum.ServiceEventStatus WHERE Status = 4) INSERT INTO enum.ServiceEventStatus (Status, StatusName) VALUES (4, 'Open - Critical')
    
PRINT '-- SEED SERVICE EVENT STATUS ( END ) '

-- SEED SERVICE EVENT STATUS ( END ) 


-- SEED BILL TYPE FREQUENCY TYPE (BEGIN) 

PRINT '-- SEED BILL TYPE FREQUENCY TYPE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFrequency WHERE Frequency = 1) INSERT INTO dbo.BillTypeFrequency (Frequency, FrequencyName) VALUES (1, 'Admit through Discharge Claim')
    
	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFrequency WHERE Frequency = 2) INSERT INTO dbo.BillTypeFrequency (Frequency, FrequencyName) VALUES (2, 'Interim - First Claim')
    
	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFrequency WHERE Frequency = 3) INSERT INTO dbo.BillTypeFrequency (Frequency, FrequencyName) VALUES (3, 'Interim - Continuing Claim')
 
	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFrequency WHERE Frequency = 4) INSERT INTO dbo.BillTypeFrequency (Frequency, FrequencyName) VALUES (4, 'Interim - Last Claim')
    
	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFrequency WHERE Frequency = 5) INSERT INTO dbo.BillTypeFrequency (Frequency, FrequencyName) VALUES (5, 'Late Charge Only')
	
	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFrequency WHERE Frequency = 6) INSERT INTO dbo.BillTypeFrequency (Frequency, FrequencyName) VALUES (6, 'Adjustment of Prior Claim')
    
	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFrequency WHERE Frequency = 7) INSERT INTO dbo.BillTypeFrequency (Frequency, FrequencyName) VALUES (7, 'Replacement of Prior Claim')

	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFrequency WHERE Frequency = 8) INSERT INTO dbo.BillTypeFrequency (Frequency, FrequencyName) VALUES (8, 'Void/Cancel of Prior Claim')

PRINT '-- SEED BILL TYPE FREQUENCY TYPE ( END ) '

-- SEED BILL TYPE FREQUENCY TYPE ( END ) 


-- SEED BILL TYPE FACILITY TYPE BILL CLASSIFICATION TYPE (BEGIN) 

PRINT '-- SEED BILL TYPE FACILITY TYPE BILL CLASSIFICATION TYPE (BEGIN) '

	-- HOSPITAL

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 11) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (11, 'Hospital', 'Inpatient')
  
  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 12) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (12, 'Hospital', 'Hospital Based or Inpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 13) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (13, 'Hospital', 'Outpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 14) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (14, 'Hospital', 'Other')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 15) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (15, 'Hospital', 'Intermediate Care, Level 1')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 16) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (16, 'Hospital', 'Intermediate Care, Level 2')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 17) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (17, 'Hospital', 'Intermediate Care, Level 3')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 18) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (18, 'Hospital', 'Swing Beds')

	-- SKILLED NURSING FACILITY 

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 21) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (21, 'Skilled Nursing Facility', 'Inpatient')
  
  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 22) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (22, 'Skilled Nursing Facility', 'Hospital Based or Inpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 23) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (23, 'Skilled Nursing Facility', 'Outpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 24) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (24, 'Skilled Nursing Facility', 'Other')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 25) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (25, 'Skilled Nursing Facility', 'Intermediate Care, Level 1')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 26) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (26, 'Skilled Nursing Facility', 'Intermediate Care, Level 2')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 27) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (27, 'Skilled Nursing Facility', 'Intermediate Care, Level 3')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 28) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (28, 'Skilled Nursing Facility', 'Swing Beds')

	-- HOME HEALTH

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 31) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (31, 'Home Health', 'Inpatient')
  
  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 32) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (32, 'Home Health', 'Hospital Based or Inpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 33) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (33, 'Home Health', 'Outpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 34) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (34, 'Home Health', 'Other')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 35) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (35, 'Home Health', 'Intermediate Care, Level 1')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 36) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (36, 'Home Health', 'Intermediate Care, Level 2')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 37) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (37, 'Home Health', 'Intermediate Care, Level 3')

	-- CHRISTIAN SCIENCE (HOSPITAL)
		
  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 41) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (41, 'Christian Science (Hospital)', 'Inpatient')
  
  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 42) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (42, 'Christian Science (Hospital)', 'Hospital Based or Inpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 43) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (43, 'Christian Science (Hospital)', 'Outpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 44) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (44, 'Christian Science (Hospital)', 'Other')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 45) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (45, 'Christian Science (Hospital)', 'Intermediate Care, Level 1')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 46) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (46, 'Christian Science (Hospital)', 'Intermediate Care, Level 2')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 47) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (47, 'Christian Science (Hospital)', 'Intermediate Care, Level 3')

		-- CHRSITIAN SCIENCE (EXTENDED CARE)

	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 51) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (51, 'Christian Science (Extended Care)', 'Inpatient')
  
  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 52) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (52, 'Christian Science (Extended Care)', 'Hospital Based or Inpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 53) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (53, 'Christian Science (Extended Care)', 'Outpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 54) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (54, 'Christian Science (Extended Care)', 'Other')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 55) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (55, 'Christian Science (Extended Care)', 'Intermediate Care, Level 1')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 56) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (56, 'Christian Science (Extended Care)', 'Intermediate Care, Level 2')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 57) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (57, 'Christian Science (Extended Care)', 'Intermediate Care, Level 3')
	
	-- INTERMEDIATE CARE
	
	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 61) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (61, 'Intermediate Care', 'Inpatient')
  
  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 62) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (62, 'Intermediate Care', 'Hospital Based or Inpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 63) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (63, 'Intermediate Care', 'Outpatient')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 64) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (64, 'Intermediate Care', 'Other')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 65) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (65, 'Intermediate Care', 'Intermediate Care, Level 1')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 66) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (66, 'Intermediate Care', 'Intermediate Care, Level 2')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 67) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (67, 'Intermediate Care', 'Intermediate Care, Level 3')

	-- CLINIC
	
	IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 71) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (71, 'Clinic', 'Rural Health')
  
  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 72) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (72, 'Clinic', 'Hospital Based or Independent Renal Dialysis Center')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 73) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (73, 'Clinic', 'Free Standing')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 74) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (74, 'Clinic', 'Other Rahbilitation Facility (ORF)')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 75) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (75, 'Clinic', 'Intermediate Care, Level 1')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 76) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (76, 'Clinic', 'Intermediate Care, Level 2')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 77) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (77, 'Clinic', 'Intermediate Care, Level 3')

  IF NOT EXISTS (SELECT * FROM dbo.BillTypeFacilityTypeBillClassification WHERE FacilityTypeBillClassification = 79) INSERT INTO dbo.BillTypeFacilityTypeBillClassification (FacilityTypeBillClassification, FacilityTypeName, BillClassificationName) VALUES (79, 'Clinic', 'Other')

PRINT '-- SEED BILL TYPE FACILITY TYPE BILL CLASSIFICATION TYPE ( END ) '

-- SEED BILL TYPE FACILITY TYPE BILL CLASSIFICATION TYPE ( END ) 


-- SEED BILL TYPE (BEGIN) 

INSERT INTO BillType (BillType, BillTypeName, BillTypeDescription, FacilityTypeName, BillClassificationName, FrequencyName)

	SELECT 

			CAST (RTRIM (BillTypeFacilityTypeBillClassification.FacilityTypeBillClassification) + RTRIM (BillTypeFrequency.Frequency) AS VARCHAR (003)) AS BillType,
			
			CAST (BillTypeFacilityTypeBillClassification.FacilityTypeName + ' - ' + BillTypeFacilityTypeBillClassification.BillClassificationName + ' - ' + RTRIM (BillTypeFrequency.Frequency) AS VARCHAR (060)) AS BillTypeName,
			
			CAST (BillTypeFacilityTypeBillClassification.FacilityTypeName + ' - ' + BillTypeFacilityTypeBillClassification.BillClassificationName + ' - ' + BillTypeFrequency.FrequencyName AS VARCHAR (060)) AS BillTypeDescription,
			
			BillTypeFacilityTypeBillClassification.FacilityTypeName,
			
			BillTypeFacilityTypeBillClassification.BillClassificationName,
			
			BillTypeFrequency.FrequencyName

		FROM 
		
			BillTypeFacilityTypeBillClassification
			
				JOIN BillTypeFrequency
		
					ON (1 = 1)
					
				LEFT JOIN BillType
					
					ON CAST (RTRIM (BillTypeFacilityTypeBillClassification.FacilityTypeBillClassification) + RTRIM (BillTypeFrequency.Frequency) AS VARCHAR (003)) = BillType.BillType
					
		WHERE BillType.BillType IS NULL
		
-- SEED BILL TYPE ( END )		


-- SEED CLAIM TYPE (BEGIN) 

PRINT '-- SEED CLAIM TYPE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.ClaimType WHERE ClaimType = 0) INSERT INTO enum.ClaimType (ClaimType, ClaimTypeName) VALUES (0, 'Not Specified')
    
	IF NOT EXISTS (SELECT * FROM enum.ClaimType WHERE ClaimType = 1) INSERT INTO enum.ClaimType (ClaimType, ClaimTypeName) VALUES (1, 'Professional')
    
	IF NOT EXISTS (SELECT * FROM enum.ClaimType WHERE ClaimType = 2) INSERT INTO enum.ClaimType (ClaimType, ClaimTypeName) VALUES (2, 'Institutional')
 
	IF NOT EXISTS (SELECT * FROM enum.ClaimType WHERE ClaimType = 3) INSERT INTO enum.ClaimType (ClaimType, ClaimTypeName) VALUES (3, 'Dental')
    
	IF NOT EXISTS (SELECT * FROM enum.ClaimType WHERE ClaimType = 4) INSERT INTO enum.ClaimType (ClaimType, ClaimTypeName) VALUES (4, 'Pharmacy')
	
	IF NOT EXISTS (SELECT * FROM enum.ClaimType WHERE ClaimType = 5) INSERT INTO enum.ClaimType (ClaimType, ClaimTypeName) VALUES (5, 'Laboratory')
    
PRINT '-- SEED CLAIM TYPE ( END ) '

-- SEED CLAIM TYPE ( END ) 


-- SEED CLAIM STATUS (BEGIN) 

PRINT '-- SEED CLAIM STATUS (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = -1) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (-1, 'Void') 

  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = 0) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (0, 'Not Specified') 

  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = 1) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (1, 'Open') 

  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = 2) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (2, 'Pend') 
	
  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = 3) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (3, 'Ready to Deny') 

  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = 4) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (4, 'Ready to Pay') 
	
  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = 5) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (5, 'Denied') 

  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = 6) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (6, 'Paid') 
	
  IF NOT EXISTS (SELECT * FROM enum.ClaimStatus WHERE ClaimStatus = 7) INSERT INTO enum.ClaimStatus (ClaimStatus, ClaimStatusName) VALUES (7, 'Reversed') 
	        
PRINT '-- SEED CLAIM STATUS ( END ) '

-- SEED CLAIM STATUS ( END ) 


-- SEED CLAIM SERVICE PLACE (BEGIN) 

PRINT '-- SEED CLAIM SERVICE PLACE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '11') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (11, 'Office', 'Office') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '12') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (12, 'Home', 'Home') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '21') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (21, 'Inpatient Hospital', 'Inpatient Hospital') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '22') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (22, 'Outpatient Hospital', 'Outpatient Hospital') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '23') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (23, 'Emergency Room - Hospital', 'Emergency Room - Hospital') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '24') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (24, 'Ambulatory Surgical Center', 'Ambulatory Surgical Center') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '25') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (25, 'Birthing Center', 'Birthing Center') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '26') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (26, 'Military Treatment Facility', 'Military Treatment Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '31') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (31, 'Skilled Nursing Facility', 'Skilled Nursing Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '32') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (32, 'Nursing Facility', 'Nursing Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '33') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (33, 'Custodial Care Facility', 'Custodial Care Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '34') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (34, 'Hospice', 'Hospice') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '41') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (41, 'Ambulance - Land', 'Ambulance - Land') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '42') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (42, 'Ambulance - Air and Water', 'Ambulance - Air and Water') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '50') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (50, 'Federally Qualified Health Center', 'Federally Qualified Health Center') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '51') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (51, 'Inpatient Psychiatric Facility', 'Inpatient Psychiatric Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '52') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (52, 'Psychiatric Facility Partial Hospitalization', 'Psychiatric Facility Partial Hospitalization') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '53') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (53, 'Community Mental Health Center', 'Community Mental Health Center') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '54') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (54, 'Intermediate Care Facility/Mentally Retarded', 'Intermediate Care Facility/Mentally Retarded') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '55') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (55, 'Residential Substance Abuse Treatment Facility', 'Residential Substance Abuse Treatment Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '56') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (56, 'Psychiatric Residential Treatment Center', 'Psychiatric Residential Treatment Center') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '60') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (60, 'Mass Immunization Center', 'Mass Immunization Center') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '61') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (61, 'Comprehensive Inpatient Rehabilitation Facility', 'Comprehensive Inpatient Rehabilitation Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '62') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (62, 'Comprehensive Outpatient Rehabilitation Facility', 'Comprehensive Outpatient Rehabilitation Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '65') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (65, 'End Stage Renal Disease Treatment Facility', 'End Stage Renal Disease Treatment Facility') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '71') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (71, 'State or Local Public Health Clinic', 'State or Local Public Health Clinic') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '72') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (72, 'Rural Health Clinic', 'Rural Health Clinic') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '81') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (81, 'Independent Laboratory', 'Independent Laboratory') 

  IF NOT EXISTS (SELECT * FROM dbo.ServicePlace WHERE ServicePlace = '99') INSERT INTO dbo.ServicePlace (ServicePlace, ServicePlaceName, ServicePlaceDescription) VALUES (99, 'Other Unlisted Facility', 'Other Unlisted Facility') 

PRINT '-- SEED CLAIM SERVICE PLACE ( END ) '

-- SEED CLAIM SERVICE PLACE ( END ) 


-- SEED DISPENSE AS WRITTEN (BEGIN) 

PRINT '-- SEED DISPENSE AS WRITTEN (BEGIN) '

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 0) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (0, 'Not Specified')

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 1) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (1, 'Substitution Not Allowed by Prescriber')

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 2) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (2, 'Substitution Allowed-Patient Requested Product Dispensed')	

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 3) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (3, 'Substitution Allowed-Pharmacist Requested Product Dispensed')

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 4) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (4, 'Substitution Allowed-Generic Drug Not in Stock')

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 5) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (5, 'Substitution Allowed-Brand Drug Dispensed as a Generic')

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 6) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (6, 'Override')

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 7) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (7, 'Substitution Not Allowed-Brand Drug Mandated by Law')

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 8) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (8, 'Substitution Allowed-Generic Not Available in Marketplace')

	IF NOT EXISTS (SELECT * FROM enum.DispenseAsWritten WHERE DispenseAsWritten = 9) INSERT INTO enum.DispenseAsWritten (DispenseAsWritten, DispenseAsWrittenName) VALUES (9, 'Other')
        
PRINT '-- SEED DISPENSE AS WRITTEN ( END ) '

-- SEED DISPENSE AS WRITTEN ( END )


-- SEED INGREDIENT COST BASIS (BEGIN) 

PRINT '-- SEED INGREDIENT COST BASIS (BEGIN) '

	IF NOT EXISTS (SELECT * FROM enum.IngredientCostBasis WHERE IngredientCostBasis = 0) INSERT INTO enum.IngredientCostBasis (IngredientCostBasis, IngredientCostBasisName) VALUES (0, 'Not Specified')

	IF NOT EXISTS (SELECT * FROM enum.IngredientCostBasis WHERE IngredientCostBasis = 1) INSERT INTO enum.IngredientCostBasis (IngredientCostBasis, IngredientCostBasisName) VALUES (1, 'AWP (Average Wholesale Price)')

	IF NOT EXISTS (SELECT * FROM enum.IngredientCostBasis WHERE IngredientCostBasis = 2) INSERT INTO enum.IngredientCostBasis (IngredientCostBasis, IngredientCostBasisName) VALUES (2, 'Local Wholesaler')	

	IF NOT EXISTS (SELECT * FROM enum.IngredientCostBasis WHERE IngredientCostBasis = 3) INSERT INTO enum.IngredientCostBasis (IngredientCostBasis, IngredientCostBasisName) VALUES (3, 'Direct')

	IF NOT EXISTS (SELECT * FROM enum.IngredientCostBasis WHERE IngredientCostBasis = 4) INSERT INTO enum.IngredientCostBasis (IngredientCostBasis, IngredientCostBasisName) VALUES (4, 'EAC (Estimated Acquisition Cost)')

	IF NOT EXISTS (SELECT * FROM enum.IngredientCostBasis WHERE IngredientCostBasis = 5) INSERT INTO enum.IngredientCostBasis (IngredientCostBasis, IngredientCostBasisName) VALUES (5, 'Acquisition')

	IF NOT EXISTS (SELECT * FROM enum.IngredientCostBasis WHERE IngredientCostBasis = 6) INSERT INTO enum.IngredientCostBasis (IngredientCostBasis, IngredientCostBasisName) VALUES (6, 'MAC (Maximum Allowable Cost)')

	IF NOT EXISTS (SELECT * FROM enum.IngredientCostBasis WHERE IngredientCostBasis = 7) INSERT INTO enum.IngredientCostBasis (IngredientCostBasis, IngredientCostBasisName) VALUES (7, 'Usual & Customary')

PRINT '-- SEED INGREDIENT COST BASIS ( END ) '

-- SEED INGREDIENT COST BASIS ( END )


-- SEED AUTHORIZATION STATUS (BEGIN) 

PRINT '-- SEED AUTHORIZATION STATUS (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.AuthorizationStatus WHERE AuthorizationStatus = -1) INSERT INTO enum.AuthorizationStatus (AuthorizationStatus, AuthorizationStatusName) VALUES (-1, 'Void') 

  IF NOT EXISTS (SELECT * FROM enum.AuthorizationStatus WHERE AuthorizationStatus = 0) INSERT INTO enum.AuthorizationStatus (AuthorizationStatus, AuthorizationStatusName) VALUES (0, 'Not Specified') 
        
PRINT '-- SEED AUTHORIZATION STATUS ( END ) '

-- SEED AUTHORIZATION STATUS ( END ) 


-- SEED ICD-9 PROCEDURE TYPE (BEGIN) 

PRINT '-- SEED ICD-9 PROCEDURE TYPE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.Icd9ProcedureType WHERE Icd9ProcedureType = 0) INSERT INTO enum.Icd9ProcedureType (Icd9ProcedureType, Icd9ProcedureTypeName) VALUES (0, 'ICD-9 Procedure') 
    
	IF NOT EXISTS (SELECT * FROM enum.Icd9ProcedureType WHERE Icd9ProcedureType = 1) INSERT INTO enum.Icd9ProcedureType (Icd9ProcedureType, Icd9ProcedureTypeName) VALUES (1, 'Principal ICD-9 Procedure')
            
PRINT '-- SEED ICD-9 PROCEDURE TYPE ( END ) '

-- SEED ICD-9 PROCEDURE TYPE ( END ) 


-- SEED DIAGNOSIS TYPE (BEGIN) 

PRINT '-- SEED DIAGNOSIS TYPE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.DiagnosisType WHERE DiagnosisType = 0) INSERT INTO enum.DiagnosisType (DiagnosisType, DiagnosisTypeName) VALUES (0, 'Diagnosis') 
    
	IF NOT EXISTS (SELECT * FROM enum.DiagnosisType WHERE DiagnosisType = 1) INSERT INTO enum.DiagnosisType (DiagnosisType, DiagnosisTypeName) VALUES (1, 'Principal Diagnosis')
    
	IF NOT EXISTS (SELECT * FROM enum.DiagnosisType WHERE DiagnosisType = 2) INSERT INTO enum.DiagnosisType (DiagnosisType, DiagnosisTypeName) VALUES (2, 'Admitting Diagnosis')
 
	IF NOT EXISTS (SELECT * FROM enum.DiagnosisType WHERE DiagnosisType = 3) INSERT INTO enum.DiagnosisType (DiagnosisType, DiagnosisTypeName) VALUES (3, 'External Cause of Injury')

	IF NOT EXISTS (SELECT * FROM enum.DiagnosisType WHERE DiagnosisType = 4) INSERT INTO enum.DiagnosisType (DiagnosisType, DiagnosisTypeName) VALUES (4, 'Discharge Diagnosis')
        
PRINT '-- SEED DIAGNOSIS TYPE ( END ) '

-- SEED DIAGNOSIS TYPE ( END ) 


-- SEED ETHNICITY (BEGIN)

PRINT '-- SEED ETHNICITY (BEGIN)'

IF NOT EXISTS (SELECT * FROM Ethnicity WHERE EthnicityId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT Ethnicity ON
    
    INSERT INTO Ethnicity (EthnicityId, EthnicityName, EthnicityDescription, HipaaCode) VALUES (0, 'Not Specified', 'Not Specified', '7')
    
    SET IDENTITY_INSERT Ethnicity OFF
    
  END

IF NOT EXISTS (SELECT * FROM Ethnicity WHERE EthnicityName = 'Asian or Pacific Islander') 

    INSERT INTO Ethnicity (EthnicityName, EthnicityDescription, HipaaCode) VALUES ('Asian or Pacific Islander', 'Asian or Pacific Islander', 'A')

IF NOT EXISTS (SELECT * FROM Ethnicity WHERE EthnicityName = 'Black') 

    INSERT INTO Ethnicity (EthnicityName, EthnicityDescription, HipaaCode) VALUES ('Black', 'Black', 'B')
    
IF NOT EXISTS (SELECT * FROM Ethnicity WHERE EthnicityName = 'Caucasian') 

    INSERT INTO Ethnicity (EthnicityName, EthnicityDescription, HipaaCode) VALUES ('Caucasian', 'Caucasian', 'C')

IF NOT EXISTS (SELECT * FROM Ethnicity WHERE EthnicityName = 'Hispanic') 

    INSERT INTO Ethnicity (EthnicityName, EthnicityDescription, HipaaCode) VALUES ('Hispanic', 'Hispanic', 'H')

IF NOT EXISTS (SELECT * FROM Ethnicity WHERE EthnicityName = 'American Indian or Alaskan Native') 

    INSERT INTO Ethnicity (EthnicityName, EthnicityDescription, HipaaCode) VALUES ('American Indian or Alaskan Native', 'American Indian or Alaskan Native', 'I')

IF NOT EXISTS (SELECT * FROM Ethnicity WHERE EthnicityName = 'Black (Non-Hispanic)') 

    INSERT INTO Ethnicity (EthnicityName, EthnicityDescription, HipaaCode) VALUES ('Black (Non-Hispanic)', 'Black (Non-Hispanic)', 'N')

IF NOT EXISTS (SELECT * FROM Ethnicity WHERE EthnicityName = 'White (Non-Hispanic)') 

    INSERT INTO Ethnicity (EthnicityName, EthnicityDescription, HipaaCode) VALUES ('White (Non-Hispanic)', 'White (Non-Hispanic)', 'O')


PRINT '-- SEED ETHNICITY ( END )'  
  
-- SEED ETHNICITY ( END )
    

-- SEED LANGUAGE (BEGIN)

PRINT '-- SEED LANGUAGE (BEGIN)'

IF NOT EXISTS (SELECT * FROM Language WHERE LanguageId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT Language ON
    
    INSERT INTO Language (LanguageId, LanguageName, LanguageDescription) VALUES (0, 'Not Specified', 'NotSpecified')
    
    SET IDENTITY_INSERT Language OFF
    
  END

PRINT '-- SEED LANGUAGE ( END )'  
  
-- SEED LANGUAGE ( END )
    
    
-- SEED MARITAL STATUS (BEGIN)

PRINT '-- SEED MARITAL STATUS (BEGIN)'

	SET IDENTITY_INSERT MaritalStatus ON

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 0) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (0, 'Not Specified', 'R')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 1) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (1, 'Single', 'S')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 2) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (2, 'Registered Domestic Partner', 'B')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 3) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (3, 'Divorced', 'D')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 4) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (4, 'Married', 'M')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 5) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (5, 'Unreported', 'R')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 6) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (6, 'Separated', 'S')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 7) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (7, 'Unmarried (Single or Divorced or Widowed)', 'U')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 8) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (8, 'Widowed', 'W')

	IF NOT EXISTS (SELECT * FROM MaritalStatus WHERE MaritalStatusId = 9) INSERT INTO MaritalStatus (MaritalStatusId, MaritalStatusName, HipaaCode) VALUES (9, 'Legally Separated', 'X')

	SET IDENTITY_INSERT MaritalStatus OFF

PRINT '-- SEED MARITAL STATUS ( END )'  
  
-- SEED MARITAL STATUS ( END )
    
    
-- SEED CITIZENSHIP (BEGIN)

PRINT '-- SEED CITIZENSHIP (BEGIN)'

	SET IDENTITY_INSERT Citizenship ON

	IF NOT EXISTS (SELECT * FROM Citizenship WHERE CitizenshipId = 0) INSERT INTO Citizenship (CitizenshipId, CitizenshipName, HipaaCode) VALUES (0, 'Not Specified', '')

	IF NOT EXISTS (SELECT * FROM Citizenship WHERE CitizenshipId = 1) INSERT INTO Citizenship (CitizenshipId, CitizenshipName, HipaaCode) VALUES (1, 'U.S. Citizen', '1')

	IF NOT EXISTS (SELECT * FROM Citizenship WHERE CitizenshipId = 2) INSERT INTO Citizenship (CitizenshipId, CitizenshipName, HipaaCode) VALUES (2, 'Non-Resident Alien', '2')

	IF NOT EXISTS (SELECT * FROM Citizenship WHERE CitizenshipId = 3) INSERT INTO Citizenship (CitizenshipId, CitizenshipName, HipaaCode) VALUES (3, 'Resident Alien', '3')

	IF NOT EXISTS (SELECT * FROM Citizenship WHERE CitizenshipId = 4) INSERT INTO Citizenship (CitizenshipId, CitizenshipName, HipaaCode) VALUES (4, 'Illegal Alien', '4')

	IF NOT EXISTS (SELECT * FROM Citizenship WHERE CitizenshipId = 5) INSERT INTO Citizenship (CitizenshipId, CitizenshipName, HipaaCode) VALUES (5, 'Alien', '5')

	IF NOT EXISTS (SELECT * FROM Citizenship WHERE CitizenshipId = 6) INSERT INTO Citizenship (CitizenshipId, CitizenshipName, HipaaCode) VALUES (6, 'U.S. Citizen - Non-Resident', '6')

	IF NOT EXISTS (SELECT * FROM Citizenship WHERE CitizenshipId = 7) INSERT INTO Citizenship (CitizenshipId, CitizenshipName, HipaaCode) VALUES (7, 'U.S. Citizen - Resident', '7')

	SET IDENTITY_INSERT Citizenship OFF

PRINT '-- SEED CITIZENSHIP ( END )'  
  
-- SEED CITIZENSHIP ( END )
    
       
-- SEED INSURANCE TYPE (BEGIN)

PRINT '-- SEED INSURANCE TYPE (BEGIN)'

	SET IDENTITY_INSERT InsuranceType ON

	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 0) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (0, 'Not Specified', '')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 1) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (1, 'Auto Insurance Policy', 'AP')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 2) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (2, 'Commercial', 'C1')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 3) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (3, 'Consolidated Omnibus Budget Reconciliation Act (COBRA)', 'CO')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 4) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (4, 'Group Policy', 'GP')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 5) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (5, 'Health Maintenance Organization (HMO)', 'HM')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 6) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (6, 'Medicare Risk', 'HN')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 7) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (7, 'Individual Policy', 'IP')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 8) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (8, 'Medicare Part A', 'MA')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 9) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (9, 'Medicare Part B', 'MB')
	
	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 10) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (10, 'Medicaid', 'MC')

	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 11) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (11, 'Preferred Provider Organization (PPO)', 'PR')

	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 12) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (12, 'Point of Service (POS)', 'PS')

	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 13) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (13, 'Supplemental Policy', 'SP')

	IF NOT EXISTS (SELECT * FROM InsuranceType WHERE InsuranceTypeId = 14) INSERT INTO InsuranceType (InsuranceTypeId, InsuranceTypeName, HipaaCode) VALUES (14, 'Workers Compensation', 'WC')

	SET IDENTITY_INSERT InsuranceType OFF

PRINT '-- SEED INSURANCE TYPE ( END )'  
  
-- SEED INSURANCE TYPE ( END )

    
-- SEED COVERAGE TYPE (BEGIN)

PRINT '-- SEED COVERAGE TYPE (BEGIN)'

	SET IDENTITY_INSERT CoverageType ON

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 0) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (0, 'Not Specified', '')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 1) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (1, 'Preventitive Care/Wellness', 'AG')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 2) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (2, '24 Hour Care', 'AH')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 3) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (3, 'Medicare Risk', 'AJ')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 4) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (4, 'Mental Health', 'AK')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 5) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (5, 'Dental Capitation', 'DCP')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 6) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (6, 'Dental', 'DEN')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 7) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (7, 'Exclusive Provider Organization', 'EPO')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 8) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (8, 'Facility', 'FAC')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 9) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (9, 'Hearing', 'HE')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 10) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (10, 'Health (hospital and professional coverage)', 'HLT')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 11) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (11, 'Health Maintenance Organization', 'HMO')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 12) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (12, 'Long-Term Care', 'LTC')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 13) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (13, 'Long-Term Disability', 'LTD')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 14) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (14, 'Major Medical', 'MM')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 15) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (15, 'Mail Order Drug', 'MOD')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 16) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (16, 'Prescription Drug', 'PDG')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 17) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (17, 'Point of Service', 'POS')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 18) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (18, 'Preferred Provider Organization', 'PPO')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 19) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (19, 'Practitioners', 'PRA')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 20) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (20, 'Short-Term Disability', 'STD')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 21) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (21, 'Utilization Review', 'UR')

	IF NOT EXISTS (SELECT * FROM CoverageType WHERE CoverageTypeId = 22) INSERT INTO CoverageType (CoverageTypeId, CoverageTypeName, HipaaCode) VALUES (22, 'Vision', 'VIS')
	
	SET IDENTITY_INSERT CoverageType OFF

PRINT '-- SEED COVERAGE TYPE ( END )'  
  
-- SEED COVERAGE TYPE ( END )


-- SEED COVERAGE LEVEL (BEGIN)

PRINT '-- SEED COVERAGE LEVEL (BEGIN)'

	SET IDENTITY_INSERT CoverageLevel ON

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 0) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (0, 'Not Specified', '')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 1) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (1, 'Children Only', 'CHD')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 2) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (2, 'Dependents Only', 'DEP')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 3) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (3, 'Employee and One Dependent', 'E1D')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 4) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (4, 'Employee and Two Dependents', 'E2D')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 5) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (5, 'Employee and Three Dependents', 'E3D')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 6) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (6, 'Employee and One or More Dependents', 'E5D')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 7) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (7, 'Employee and Two or More Dependents', 'E6D')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 8) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (8, 'Employee and Three or More Dependents', 'E7D')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 9) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (9, 'Employee and Four or More Dependents', 'E8D')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 10) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (10, 'Employee and Five or More Dependents', 'E9D')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 11) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (11, 'Employee and Children', 'ECH')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 12) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (12, 'Employee Only', 'EMP')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 13) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (13, 'Employee and Spouse', 'ESP')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 14) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (14, 'Family', 'FAM')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 15) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (15, 'Individual', 'IND')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 16) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (16, 'Spouse and Children', 'SPC')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 17) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (17, 'Spouse Only', 'SPO')

	IF NOT EXISTS (SELECT * FROM CoverageLevel WHERE CoverageLevelId = 18) INSERT INTO CoverageLevel (CoverageLevelId, CoverageLevelName, HipaaCode) VALUES (18, 'Two Party', 'TWO')
	
	SET IDENTITY_INSERT CoverageLevel OFF

PRINT '-- SEED COVERAGE LEVEL ( END )'  
  
-- SEED COVERAGE LEVEL ( END )



-- SEED POPULATION SERVICE EVENT ANCHOR DATE (BEGIN)

PRINT '-- SEED POPULATION SERVICE EVENT ANCHOR DATE (BEGIN)'

	IF NOT EXISTS (SELECT * FROM enum.PopulationServiceEventAnchorDate WHERE AnchorDate = 0) INSERT INTO enum.PopulationServiceEventAnchorDate (AnchorDate, AnchorDateName) VALUES (0, 'Population Anchor Date');

	IF NOT EXISTS (SELECT * FROM enum.PopulationServiceEventAnchorDate WHERE AnchorDate = 1) INSERT INTO enum.PopulationServiceEventAnchorDate (AnchorDate, AnchorDateName) VALUES (1, 'Previous Service Date');

	IF NOT EXISTS (SELECT * FROM enum.PopulationServiceEventAnchorDate WHERE AnchorDate = 2) INSERT INTO enum.PopulationServiceEventAnchorDate (AnchorDate, AnchorDateName) VALUES (2, 'Previous Service Event');

	IF NOT EXISTS (SELECT * FROM enum.PopulationServiceEventAnchorDate WHERE AnchorDate = 3) INSERT INTO enum.PopulationServiceEventAnchorDate (AnchorDate, AnchorDateName) VALUES (3, 'Age by Years');

	IF NOT EXISTS (SELECT * FROM enum.PopulationServiceEventAnchorDate WHERE AnchorDate = 4) INSERT INTO enum.PopulationServiceEventAnchorDate (AnchorDate, AnchorDateName) VALUES (4, 'Age By Months');

PRINT '-- POPULATION SERVICE EVENT ANCHOR DATE ( END )'  
  
-- SEED POPULATION SERVICE EVENT ANCHOR DATE ( END )


IF NOT EXISTS (SELECT * FROM NoteType WHERE NoteTypeId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT NoteType ON
    
    INSERT INTO NoteType (NoteTypeId, NoteTypeName, NoteTypeDescription) VALUES (0, 'Not Specified', 'Not Specified')
    
    SET IDENTITY_INSERT NoteType OFF
    
  END
  
  
IF NOT EXISTS (SELECT * FROM Workflow WHERE WorkflowId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT Workflow ON
    
    INSERT INTO Workflow (WorkflowId, WorkflowName, WorkflowDescription, EntityType, ActionVerb, AssemblyName, AssemblyPath, AssemblyClassName, WorkflowParameters) VALUES (0, '', '', 0, '', '', '', '', '<Parameters />')
    
    SET IDENTITY_INSERT Workflow OFF
    
  END
  
  
-- WORK QUEUE VIEW MUST PRECEDE WORK QUEUE BECAUSE OF THE FOREIGN KEY CONSTRAINT 
  
IF NOT EXISTS (SELECT * FROM WorkQueueView WHERE WorkQueueViewId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT WorkQueueView ON
    
    INSERT INTO WorkQueueView (WorkQueueViewId, WorkQueueViewName, WorkQueueViewDescription) VALUES (0, 'Not Specified', 'Not Specified')
    
    SET IDENTITY_INSERT WorkQueueView OFF
    
  END
  
  
IF NOT EXISTS (SELECT * FROM WorkQueue WHERE WorkQueueId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT WorkQueue ON
    
    INSERT INTO WorkQueue (WorkQueueId, WorkQueueName, WorkQueueDescription) VALUES (0, 'Not Specified', 'Not Specified')
    
    SET IDENTITY_INSERT WorkQueue OFF
    
  END
  
  
  
IF NOT EXISTS (SELECT * FROM PopulationType WHERE PopulationTypeId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT PopulationType ON
    
    INSERT INTO PopulationType (PopulationTypeId, PopulationTypeName, PopulationTypeDescription) VALUES (0, 'Not Specified', 'Not Specified')
    
    SET IDENTITY_INSERT PopulationType OFF
    
  END


  
IF NOT EXISTS (SELECT * FROM WorkOutcome WHERE WorkOutcomeId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT WorkOutcome ON
    
    INSERT INTO WorkOutcome (WorkOutcomeId, WorkOutcomeName, WorkOutcomeDescription) VALUES (0, 'Not Specified', 'Not Specified')
    
    SET IDENTITY_INSERT WorkOutcome OFF
    
  END
  
IF NOT EXISTS (SELECT * FROM WorkOutcome WHERE WorkOutcomeName = 'Unhandled Exception Occurred') 
  
  BEGIN 
  
    INSERT INTO WorkOutcome (WorkOutcomeName, WorkOutcomeDescription) VALUES ('Unhandled Exception Occurred', 'Unhandled Exception Occurred')
   
  END
  
IF NOT EXISTS (SELECT * FROM WorkOutcome WHERE WorkOutcomeName = 'Completed Successfully') 
  
  BEGIN 
  
    INSERT INTO WorkOutcome (WorkOutcomeName, WorkOutcomeDescription) VALUES ('Completed Successfully', 'Completed Successfully')
   
  END
  
-- SEED (0) RECORDS ( END )

  
/* ENVIRONMENT PERMISSIONS (BEGIN) */

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = '.EnvironmentAdministrator')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('.EnvironmentAdministrator', 'Environment Administrator');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Review', 'Configuration Management Module');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Configuration.FormDesigner')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Configuration.FormDesigner', 'Form Designer');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Role.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Role.Review', 'Environment Role Review');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Role.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Role.Manage', 'Environment Role Manage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.ImportExport')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.ImportExport', 'Configuration Import and Export');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.AuthorizedService.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.AuthorizedService.Review', 'Authorized Service Review');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.AuthorizedService.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.AuthorizedService.Manage', 'Authorized Service Manage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.MedicalService.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.MedicalService.Review', 'Medical Service Review');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.MedicalService.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.MedicalService.Manage', 'MedicalServiceManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Metric.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Metric.Review', 'MetricReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Metric.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Metric.Manage', 'MetricManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Correspondence.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Correspondence.Review', 'CorrespondenceReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Correspondence.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Correspondence.Manage', 'CorrespondenceManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Workflow.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Workflow.Review', 'WorkflowReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Workflow.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Workflow.Manage', 'WorkflowManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.WorkTeam.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.WorkTeam.Review', 'WorkTeamReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.WorkTeam.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.WorkTeam.Manage', 'WorkTeamManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.WorkQueue.Review')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.WorkQueue.Review', 'WorkQueueReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.WorkQueue.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.WorkQueue.Manage', 'WorkQueueManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.WorkQueueView.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.WorkQueueView.Review', 'Work Queue View Review');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.WorkQueueView.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.WorkQueueView.Manage', 'Work Queue View Manage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.WorkOutcome.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.WorkOutcome.Review', 'WorkOutcomeReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.WorkOutcome.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.WorkOutcome.Manage', 'WorkOutcomeManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.RoutingRule.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.RoutingRule.Review', 'RoutingRuleReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.RoutingRule.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.RoutingRule.Manage', 'RoutingRuleManage');

	
IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Condition.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Condition.Review', 'ConditionReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Condition.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Condition.Manage', 'ConditionManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Population.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Population.Review', 'PopulationReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Population.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Population.Manage', 'PopulationManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.ProblemStatement.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.ProblemStatement.Review', 'ProblemStatementReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.ProblemStatement.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.ProblemStatement.Manage', 'ProblemStatementManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'ConfigurationManagement.CareMeasureScale.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('ConfigurationManagement.CareMeasureScale.Review', 'CareMeasureScaleReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'ConfigurationManagement.CareMeasureScale.Manage')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('ConfigurationManagement.CareMeasureScale.Manage', 'CareMeasureScaleManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.CarePlan.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.CarePlan.Review', 'CarePlanReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.CarePlan.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.CarePlan.Manage', 'CarePlanManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.CareLevel.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.CareLevel.Review', 'CareLevelReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.CareLevel.Manage')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.CareLevel.Manage', 'CareLevelManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.CareOutcome.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.CareOutcome.Review', 'CareOutcomeReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.CareOutcome.Manage')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.CareOutcome.Manage', 'CareOutcomeManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Form.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Form.Review', 'FormReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.ConfigurationManagement.Form.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.ConfigurationManagement.Form.Manage', 'FormManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Service.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Service.Review', 'ServiceReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Service.Manage')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Service.Manage', 'ServiceManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Metric.Review')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Metric.Review', 'MetricReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Metric.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Metric.Manage', 'MetricManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Case.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Case.Review', 'CaseReview');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Case.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Case.Manage', 'CaseManage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Action.Contact')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Action.Contact', 'Environment.Member.Action.Contact');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Action.SendCorrespondence')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Action.SendCorrespondence', 'Environment.Member.Action.SendCorrespondence');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Action.DataEnterForm')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Action.DataEnterForm', 'Environment.Member.Action.DataEnterForm');



IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Address.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Address.Manage', 'Environment.Member.Address.Manage');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.ContactInformation.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.ContactInformation.Manage', 'Environment.Member.ContactInformation.Manage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Note.Read')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Note.Read', 'Environment.Member.Note.Read');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Note.Add')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Note.Add', 'Environment.Member.Note.Add');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Note.Modify')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Note.Modify', 'Environment.Member.Note.Modify');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Note.Append')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Note.Append', 'Environment.Member.Note.Append');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Member.Note.Terminate')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Member.Note.Terminate', 'Environment.Member.Note.Terminate');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Action.Contact')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Action.Contact', 'Environment.Provider.Action.Contact');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Action.SendCorrespondence')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Action.SendCorrespondence', 'Environment.Provider.Action.SendCorrespondence');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Action.DataEnterForm')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Action.DataEnterForm', 'Environment.Provider.Action.DataEnterForm');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Address.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Address.Manage', 'Environment.Provider.Address.Manage');
  
IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.ContactInformation.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.ContactInformation.Manage', 'Environment.Provider.ContactInformation.Manage');


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Note.Read')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Note.Read', 'Environment.Provider.Note.Read');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Note.Add')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Note.Add', 'Environment.Provider.Note.Add');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Note.Modify')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Note.Modify', 'Environment.Provider.Note.Modify');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Note.Append')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Note.Append', 'Environment.Provider.Note.Append');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Environment.Provider.Note.Terminate')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Environment.Provider.Note.Terminate', 'Environment.Provider.Note.Terminate');



IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Search.Member.OptionalBirthDate')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Search.Member.OptionalBirthDate', 'SearchMemberOptionalBirthDate');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Search.Results.ExtraLarge')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Search.Results.ExtraLarge', 'SearchResultsExtraLarge');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'Search.Results.Large')
  
  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('Search.Results.Large', 'SearchResultsLarge');
        
/* ENVIRONMENT PERMISSIONS ( END ) */


/* DEFAULT ROLES (BEGIN) */

--IF NOT EXISTS (SELECT * FROM Role WHERE RoleName = 'Environment Administrator')

  --BEGIN 
  
    --INSERT INTO Role (RoleName, RoleDescription) VALUES ('Environment Administrator', 'Environment Administrator');

    --INSERT INTO RolePermission (RoleId, PermissionId, IsGranted, IsDenied) 
      --SELECT RoleId, PermissionId, 1, 0
        --FROM Role JOIN Permission ON (1 = 1) WHERE RoleName = 'Environment Administrator' AND Permission = '.EnvironmentAdministrator'
        
  --END      

/* DEFAULT ROLES ( END ) */




/* WWF 3.0 SCHEMA (BEGIN) */

-- Copyright (c) Microsoft Corporation.  All rights reserved.

SET NOCOUNT ON

--
-- ROLE state_persistence_users
--
declare @localized_string_AddRole_Failed nvarchar(256)
set @localized_string_AddRole_Failed = N'Failed adding the ''state_persistence_users'' role'

DECLARE @ret int, @Error int
IF NOT EXISTS( SELECT 1 FROM [dbo].[sysusers] WHERE name=N'state_persistence_users' and issqlrole=1 )
 BEGIN

	EXEC @ret = sp_addrole N'state_persistence_users'

	SELECT @Error = @@ERROR

	IF @ret <> 0 or @Error <> 0
		RAISERROR( @localized_string_AddRole_Failed, 16, -1 )
 END
GO


--
-- TABLE InstanceState
--
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InstanceState]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[InstanceState]
GO
CREATE TABLE [dbo].[InstanceState] (
	[uidInstanceID] [uniqueidentifier] NOT NULL ,
	[state] [image] NULL ,
	[status] [int] NULL ,
	[unlocked] [int] NULL ,
	[blocked] [int] NULL ,
	[info] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[modified] [datetime] NOT NULL,
	[ownerID] [uniqueidentifier] NULL ,
	[ownedUntil] [datetime] NULL,
	[nextTimer] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE  UNIQUE CLUSTERED  INDEX [IX_InstanceState] ON [dbo].[InstanceState]([uidInstanceID]) ON [PRIMARY]
-- CREATE  NONCLUSTERED  INDEX [IX_InstanceState_Ownership] ON [dbo].[InstanceState]([ownerID],[ownedUntil])
GO


--
-- TABLE CompletedScope
--
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[CompletedScope]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[CompletedScope]
GO
CREATE TABLE [dbo].[CompletedScope] (
	[uidInstanceID] [uniqueidentifier] NOT NULL,
	[completedScopeID] [uniqueidentifier] NOT NULL,
	[state] [image] NOT NULL,
	[modified] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE  NONCLUSTERED  INDEX [IX_CompletedScope] ON [dbo].[CompletedScope]([completedScopeID]) ON [PRIMARY]
GO
CREATE  NONCLUSTERED  INDEX [IX_CompletedScope_InstanceID] ON [dbo].[CompletedScope]( [uidInstanceID] )
GO


DBCC TRACEON (1204)


/* WWF 3.0 SCHEMA ( END ) */


/* WWF 3.0 LOGIC (BEGIN) */

-- Copyright (c) Microsoft Corporation.  All rights reserved.

--
-- PROCEDURE InsertInstanceState
--
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[InsertInstanceState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[InsertInstanceState]
GO
Create Procedure [dbo].[InsertInstanceState]
@uidInstanceID uniqueidentifier,
@state image,
@status int,
@unlocked int,
@blocked int,
@info ntext,
@ownerID uniqueidentifier = NULL,
@ownedUntil datetime = NULL,
@nextTimer datetime,
@result int output,
@currentOwnerID uniqueidentifier output
As
    declare @localized_string_InsertInstanceState_Failed_Ownership nvarchar(256)
    set @localized_string_InsertInstanceState_Failed_Ownership = N'Instance ownership conflict'
    set @result = 0
    set @currentOwnerID = @ownerID
    declare @now datetime
    set @now = GETUTCDATE()

    SET TRANSACTION ISOLATION LEVEL READ COMMITTED
    set nocount on

    IF @status=1 OR @status=3
    BEGIN
	DELETE FROM [dbo].[InstanceState] WHERE uidInstanceID=@uidInstanceID AND ((ownerID = @ownerID AND ownedUntil>=@now) OR (ownerID IS NULL AND @ownerID IS NULL ))
	if ( @@ROWCOUNT = 0 )
	begin
		set @currentOwnerID = NULL
    		select  @currentOwnerID=ownerID from [dbo].[InstanceState] Where uidInstanceID = @uidInstanceID
		if ( @currentOwnerID IS NOT NULL )
		begin	-- cannot delete the instance state because of an ownership conflict
			-- RAISERROR(@localized_string_InsertInstanceState_Failed_Ownership, 16, -1)				
			set @result = -2
			return
		end
	end
	else
	BEGIN
		DELETE FROM [dbo].[CompletedScope] WHERE uidInstanceID=@uidInstanceID
	end
    END
    
    ELSE BEGIN

  	    if not exists ( Select 1 from [dbo].[InstanceState] Where uidInstanceID = @uidInstanceID )
		  BEGIN
			  --Insert Operation
			  IF @unlocked = 0
			  begin
			     Insert into [dbo].[InstanceState] 
			     Values(@uidInstanceID,@state,@status,@unlocked,@blocked,@info,@now,@ownerID,@ownedUntil,@nextTimer) 
			  end
			  else
			  begin
			     Insert into [dbo].[InstanceState] 
			     Values(@uidInstanceID,@state,@status,@unlocked,@blocked,@info,@now,null,null,@nextTimer) 
			  end
		  END
		  
		  ELSE BEGIN

				IF @unlocked = 0
				begin
					Update [dbo].[InstanceState]  
					Set state = @state,
						status = @status,
						unlocked = @unlocked,
						blocked = @blocked,
						info = @info,
						modified = @now,
						ownedUntil = @ownedUntil,
						nextTimer = @nextTimer
					Where uidInstanceID = @uidInstanceID AND ((ownerID = @ownerID AND ownedUntil>=@now) OR (ownerID IS NULL AND @ownerID IS NULL ))
					if ( @@ROWCOUNT = 0 )
					BEGIN
						-- RAISERROR(@localized_string_InsertInstanceState_Failed_Ownership, 16, -1)
						select @currentOwnerID=ownerID from [dbo].[InstanceState] Where uidInstanceID = @uidInstanceID  
						set @result = -2
						return
					END
				end
				else
				begin
					Update [dbo].[InstanceState]  
					Set state = @state,
						status = @status,
						unlocked = @unlocked,
						blocked = @blocked,
						info = @info,
						modified = @now,
						ownerID = NULL,
						ownedUntil = NULL,
						nextTimer = @nextTimer
					Where uidInstanceID = @uidInstanceID AND ((ownerID = @ownerID AND ownedUntil>=@now) OR (ownerID IS NULL AND @ownerID IS NULL ))
					if ( @@ROWCOUNT = 0 )
					BEGIN
						-- RAISERROR(@localized_string_InsertInstanceState_Failed_Ownership, 16, -1)
						select @currentOwnerID=ownerID from [dbo].[InstanceState] Where uidInstanceID = @uidInstanceID  
						set @result = -2
						return
					END
				end
				
		  END


    END
		RETURN
Return
Go
GRANT EXECUTE ON [dbo].[InsertInstanceState] TO state_persistence_users
GO


--
-- PROCEDURE RetrieveAllInstanceDescriptions
-- 
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RetrieveAllInstanceDescriptions]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RetrieveAllInstanceDescriptions]
GO
Create Procedure [dbo].[RetrieveAllInstanceDescriptions]
As
	SELECT uidInstanceID, status, blocked, info, nextTimer
	FROM [dbo].[InstanceState]
GO

--
-- PROCEDURE UnlockInstanceState
--
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UnlockInstanceState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UnlockInstanceState]
GO
Create Procedure [dbo].[UnlockInstanceState]
@uidInstanceID uniqueidentifier,
@ownerID uniqueidentifier = NULL
As

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
set nocount on

		Update [dbo].[InstanceState]  
		Set ownerID = NULL,
			ownedUntil = NULL
		Where uidInstanceID = @uidInstanceID AND ((ownerID = @ownerID AND ownedUntil>=GETUTCDATE()) OR (ownerID IS NULL AND @ownerID IS NULL ))
Go
GRANT EXECUTE ON [dbo].[UnlockInstanceState] TO state_persistence_users
GO


--
-- PROCEDURE RetrieveInstanceState
--
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RetrieveInstanceState]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[RetrieveInstanceState]
GO
Create Procedure [dbo].[RetrieveInstanceState]
@uidInstanceID uniqueidentifier,
@ownerID uniqueidentifier = NULL,
@ownedUntil datetime = NULL,
@result int output,
@currentOwnerID uniqueidentifier output
As
Begin
    declare @localized_string_RetrieveInstanceState_Failed_Ownership nvarchar(256)
    set @localized_string_RetrieveInstanceState_Failed_Ownership = N'Instance ownership conflict'
    set @result = 0
    set @currentOwnerID = @ownerID

	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRANSACTION
	
    -- Possible workflow status: 0 for executing; 1 for completed; 2 for suspended; 3 for terminated; 4 for invalid

	if @ownerID IS NOT NULL	-- if id is null then just loading readonly state, so ignore the ownership check
	begin
		  Update [dbo].[InstanceState]  
		  set	ownerID = @ownerID,
				ownedUntil = @ownedUntil
		  where uidInstanceID = @uidInstanceID AND (    ownerID = @ownerID 
													 OR ownerID IS NULL 
													 OR ownedUntil<GETUTCDATE()
													)
		  if ( @@ROWCOUNT = 0 )
		  BEGIN
			-- RAISERROR(@localized_string_RetrieveInstanceState_Failed_Ownership, 16, -1)
			select @currentOwnerID=ownerID from [dbo].[InstanceState] Where uidInstanceID = @uidInstanceID 
			if (  @@ROWCOUNT = 0 )
				set @result = -1
			else
				set @result = -2
			GOTO DONE
		  END
	end
	
    Select state from [dbo].[InstanceState]  
    Where uidInstanceID = @uidInstanceID
    
	set @result = @@ROWCOUNT;
    if ( @result = 0 )
	begin
		set @result = -1
		GOTO DONE
	end
	
DONE:
	COMMIT TRANSACTION
	RETURN

End
Go
GRANT EXECUTE ON [dbo].[RetrieveInstanceState] TO state_persistence_users
GO


--
-- PROCEDURE RetrieveNonblockingInstanceStateIds
--
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[RetrieveNonblockingInstanceStateIds]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[RetrieveNonblockingInstanceStateIds]
GO
CREATE PROCEDURE [dbo].[RetrieveNonblockingInstanceStateIds]
@ownerID uniqueidentifier = NULL,
@ownedUntil datetime = NULL,
@now datetime
AS
    SELECT uidInstanceID FROM [dbo].[InstanceState] WITH (TABLOCK,UPDLOCK,HOLDLOCK)
    WHERE blocked=0 AND status<>1 AND status<>3 AND status<>2 -- not blocked and not completed and not terminated and not suspended
 		AND ( ownerID IS NULL OR ownedUntil<GETUTCDATE() )
    if ( @@ROWCOUNT > 0 )
    BEGIN
        -- lock the table entries that are returned
        Update [dbo].[InstanceState]  
        set ownerID = @ownerID,
	    ownedUntil = @ownedUntil
        WHERE blocked=0 AND status<>1 AND status<>3 AND status<>2
 		AND ( ownerID IS NULL OR ownedUntil<GETUTCDATE() )
	
    END
GO
GRANT EXECUTE ON [dbo].[RetrieveNonblockingInstanceStateIds] TO state_persistence_users
GO

--
-- PROCEDURE RetrieveANonblockingInstanceStateId
--
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[RetrieveANonblockingInstanceStateId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[RetrieveANonblockingInstanceStateId]
GO
CREATE PROCEDURE [dbo].[RetrieveANonblockingInstanceStateId]
@ownerID uniqueidentifier = NULL,
@ownedUntil datetime = NULL,
@uidInstanceID uniqueidentifier = NULL output,
@found bit = NULL output
AS
 BEGIN
		--
		-- Guarantee that no one else grabs this record between the select and update
		SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
		BEGIN TRANSACTION

SET ROWCOUNT 1
		SELECT	@uidInstanceID = uidInstanceID
		FROM	[dbo].[InstanceState] WITH (updlock) 
		WHERE	blocked=0 
		AND	status NOT IN ( 1,2,3 )
 		AND	( ownerID IS NULL OR ownedUntil<GETUTCDATE() )
SET ROWCOUNT 0

		IF @uidInstanceID IS NOT NULL
		 BEGIN
			UPDATE	[dbo].[InstanceState]  
			SET		ownerID = @ownerID,
					ownedUntil = @ownedUntil
			WHERE	uidInstanceID = @uidInstanceID

			SET @found = 1
		 END
		ELSE
		 BEGIN
			SET @found = 0
		 END

		COMMIT TRANSACTION
 END
GO
GRANT EXECUTE ON [dbo].[RetrieveANonblockingInstanceStateId] TO state_persistence_users
GO

--
-- PROCEDURE RetrieveExpiredTimerIds
--
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[RetrieveExpiredTimerIds]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[RetrieveExpiredTimerIds]
GO
CREATE PROCEDURE [dbo].[RetrieveExpiredTimerIds]
@ownerID uniqueidentifier = NULL,
@ownedUntil datetime = NULL,
@now datetime
AS
    SELECT uidInstanceID FROM [dbo].[InstanceState]
    WHERE nextTimer<@now AND status<>1 AND status<>3 AND status<>2 -- not blocked and not completed and not terminated and not suspended
        AND ((unlocked=1 AND ownerID IS NULL) OR ownedUntil<GETUTCDATE() )
GO
GRANT EXECUTE ON [dbo].[RetrieveExpiredTimerIds] TO state_persistence_users
GO

--
-- PROCEDURE InsertCompletedScope
--
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[InsertCompletedScope]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InsertCompletedScope]
GO
CREATE PROCEDURE [dbo].[InsertCompletedScope]
@instanceID uniqueidentifier,
@completedScopeID uniqueidentifier,
@state image
As

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

		UPDATE [dbo].[CompletedScope] WITH(ROWLOCK UPDLOCK) 
		    SET state = @state,
		    modified = GETUTCDATE()
		    WHERE completedScopeID=@completedScopeID 

		IF ( @@ROWCOUNT = 0 )
		BEGIN
			--Insert Operation
			INSERT INTO [dbo].[CompletedScope] WITH(ROWLOCK)
			VALUES(@instanceID, @completedScopeID, @state, GETUTCDATE()) 
		END

		RETURN
RETURN
GO
GRANT EXECUTE ON [dbo].[InsertCompletedScope] TO state_persistence_users
GO


--
-- PROCEDURE DeleteCompletedScope
--
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[DeleteCompletedScope]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DeleteCompletedScope]
GO
CREATE PROCEDURE [dbo].[DeleteCompletedScope]
@completedScopeID uniqueidentifier
AS
DELETE FROM [dbo].[CompletedScope] WHERE completedScopeID=@completedScopeID
Go
GRANT EXECUTE ON [dbo].[DeleteCompletedScope] TO state_persistence_users
GO

--
-- PROCEDURE RetrieveCompletedScope
--
IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[RetrieveCompletedScope]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[RetrieveCompletedScope]
GO
CREATE PROCEDURE RetrieveCompletedScope
@completedScopeID uniqueidentifier,
@result int output
AS
BEGIN
    SELECT state FROM [dbo].[CompletedScope] WHERE completedScopeID=@completedScopeID
	set @result = @@ROWCOUNT;
End
GO
GRANT EXECUTE ON [dbo].[RetrieveCompletedScope] TO state_persistence_users
GO


DBCC TRACEON (1204)

/* WWF LOGIC 3.0 ( END ) */


/* WWF SCHEMA 3.5 (BEGIN) */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

declare @localized_string_AddRole_Failed nvarchar(256)
set @localized_string_AddRole_Failed = N'Failed adding the ''persistenceUsers'' role'

DECLARE @ret int, @Error int
IF NOT EXISTS( SELECT 1 FROM [dbo].[sysusers] WHERE name=N'persistenceUsers' and issqlrole=1 )
 BEGIN

	EXEC @ret = sp_addrole N'persistenceUsers'

	SELECT @Error = @@ERROR

	IF @ret <> 0 or @Error <> 0
		RAISERROR( @localized_string_AddRole_Failed, 16, -1 )
 END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InstanceData]') AND type in (N'U'))
	DROP TABLE [dbo].[InstanceData]
GO
CREATE TABLE [dbo].[InstanceData](
	[id] [uniqueidentifier] NOT NULL,	
	[instance] [image] NULL,
	[instanceXml] [xml] NULL,
	[created] [datetime] NOT NULL,
	[lastUpdated] [datetime] NOT NULL,
	[lockOwner] [uniqueidentifier] NULL,
	[lockExpiration] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX id_index
	ON [dbo].[InstanceData] ([id])
	WITH IGNORE_DUP_KEY
GO


/* WWF SCHEMA 3.5 ( END ) */


/* WWF LOGIC 3.5 (BEGIN) */

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--
-- DeleteInstance
--
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteInstance]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DeleteInstance]
GO
CREATE PROCEDURE DeleteInstance
	@id uniqueIdentifier,
	@hostId uniqueIdentifier,
	@lockTimeout int,
	@result int output
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @now datetime;
	SET @now = getutcdate();

	DELETE FROM [dbo].[InstanceData]
		WHERE (id = @id) AND ((@lockTimeout < 0) OR ((lockOwner = @hostId) AND (lockExpiration >= @now)));

	IF @@rowcount = 1
		SET @result = 0; -- Success
	ELSE
	BEGIN
		IF EXISTS (SELECT 1 FROM [dbo].[InstanceData] WHERE id = @id)
			SET @result = 2; -- Could not acquire lock
		ELSE
			SET @result = 1; -- Instance not found
	END
END
GO
GRANT EXECUTE ON [dbo].[DeleteInstance] TO persistenceUsers
GO

--
-- LoadInstance
--
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoadInstance]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[LoadInstance]
GO
CREATE PROCEDURE LoadInstance
	@id uniqueidentifier,
	@lockInstance bit,
	@hostId uniqueidentifier,
	@lockTimeout int,
	@result int output
AS
BEGIN
	SET NOCOUNT ON;

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

	DECLARE @createdTransaction bit;
	
	DECLARE @now datetime, @lockExpiration datetime;
	SET @now = getutcdate();
	SET @result = 0;

	IF (@lockTimeout < 0) OR (@lockInstance = 'FALSE')
	BEGIN
		SELECT 
			'instance' = instance,
			'instanceXml' = instanceXml,
			'isXml' = CASE
				WHEN instanceXml is NOT NULL THEN 1
				ELSE 0
			END
			FROM [dbo].[InstanceData]
			WHERE id = @id;

		IF @@rowcount = 0
			SET @result = 1; -- Instance not found
	END
	ELSE
	BEGIN
		IF @lockTimeout = 0
			SET @lockExpiration = '9999-12-31T23:59:59';
		ELSE 
			SET @lockExpiration = dateadd(second, @lockTimeout, @now);

		IF @@trancount = 0
		BEGIN
			SET @createdTransaction = 'TRUE';
			BEGIN TRANSACTION;
		END

		UPDATE [dbo].[InstanceData] SET
			lockOwner = @hostId,
			lockExpiration = @lockExpiration
			WHERE (id = @id) AND ((lockOwner is NULL) OR (lockOwner = @hostId) OR (lockExpiration < @now));

		IF @@rowcount = 1
		BEGIN
			SELECT 
				instance,
				instanceXml,
				'isXml' = CASE
					WHEN instanceXml is NOT NULL THEN 1
					ELSE 0
				END
				FROM [dbo].[InstanceData]
				WHERE id = @id;

			IF @@error <> 0
			BEGIN
				ROLLBACK TRANSACTION
				RETURN
			END
		END
		ELSE
		BEGIN 
			IF EXISTS (SELECT 1 FROM [dbo].[InstanceData] WHERE id = @id)
				SET @result = 2; -- Could not acquire lock
			ELSE
				SET @result = 1; -- Instance not found
		END

		IF @createdTransaction = 'TRUE'
			COMMIT TRANSACTION
	END
END
GO
GRANT EXECUTE ON [dbo].[LoadInstance] TO persistenceUsers
GO

--
-- InsertInstance
--
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertInstance]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[InsertInstance]
GO
CREATE PROCEDURE InsertInstance
	@id uniqueidentifier,
	@instance image = NULL,
	@instanceXml xml = NULL,
	@unlockInstance bit,
	@hostId uniqueidentifier,
	@lockTimeout int,
	@result int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

	SET @result = 0;

	DECLARE @now datetime, @lockExpiration datetime, @newOwner uniqueidentifier;
	SET @now = getutcdate();
	
	IF @lockTimeout < 0 OR @unlockInstance = 'TRUE'
	BEGIN
		SET @lockExpiration = NULL;
		SET @newOwner = NULL;
	END
	ELSE 
	BEGIN
		SET @newOwner = @hostId;

		IF @lockTimeout = 0
			SET @lockExpiration = '9999-12-31T23:59:59';
		ELSE
			SET @lockExpiration = dateadd(second, @lockTimeout, @now);
	END

	INSERT INTO [dbo].[InstanceData] (id, instance, instanceXml, created, lastUpdated, lockOwner, lockExpiration)
		VALUES (@id, @instance, @instanceXml, @now, @now, @newOwner, @lockExpiration);

	IF @@rowcount = 0
	BEGIN
		IF EXISTS(SELECT 1 FROM [dbo].[InstanceData] WHERE id = @id)
			SET @result = 1; -- The instance already existed.
		ELSE
			SET @result = 2; -- Some other non-fatal error caused us not to insert
	END
END
GO
GRANT EXECUTE ON [dbo].[InsertInstance] TO persistenceUsers
GO

--
-- UpdateInstance
--
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateInstance]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UpdateInstance]
GO
CREATE PROCEDURE UpdateInstance
	@id uniqueidentifier,
	@instance image = NULL,
	@instanceXml xml = NULL,
	@unlockInstance bit,
	@hostId uniqueidentifier,
	@lockTimeout int,
	@result int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

	SET @result = 0;

	DECLARE @now datetime, @lockExpiration datetime, @newOwner uniqueidentifier;
	SET @now = getutcdate();
	
	IF @lockTimeout < 0 OR @unlockInstance = 'TRUE'
	BEGIN
		SET @lockExpiration = NULL;
		SET @newOwner = NULL;
	END
	ELSE 
	BEGIN
		SET @newOwner = @hostId;

		IF @lockTimeout = 0
			SET @lockExpiration = '9999-12-31T23:59:59';
		ELSE
			SET @lockExpiration = dateadd(second, @lockTimeout, @now);
	END

	UPDATE [dbo].[InstanceData] SET
		instance = @instance,
		instanceXml = @instanceXml,
		lastUpdated = @now,
		lockOwner = @newOwner,
		lockExpiration = @lockExpiration
		WHERE (id = @id) AND ((@lockTimeout < 0) OR ((lockOwner = @hostId) AND (lockExpiration >= @now)));

	IF @@rowcount = 0
	BEGIN
		IF EXISTS(SELECT 1 FROM [dbo].[InstanceData] WHERE id = @id)
			SET @result = 2; -- Did not have lock
		ELSE
			SET @result = 1; -- Instance was not found in the database for update
	END
END
GO
GRANT EXECUTE ON [dbo].[UpdateInstance] TO persistenceUsers
GO

--
-- UnlockInstance
--
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UnlockInstance]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[UnlockInstance]
GO
CREATE PROCEDURE UnlockInstance
	@id uniqueIdentifier,
	@hostId uniqueIdentifier,
	@lockTimeout int,
	@result int output
AS
BEGIN
	SET NOCOUNT ON;

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	DECLARE @now datetime;
	SET @now = getutcdate();

	UPDATE [InstanceData] SET
		lockOwner = NULL,
		lockExpiration = NULL
		WHERE (id = @id) AND ((@lockTimeout < 0) OR ((lockOwner = @hostId) AND (lockExpiration >= @now)));

	IF @@rowcount = 1
		SET @result = 0; -- Success
	ELSE
	BEGIN
		IF EXISTS(SELECT 1 FROM [dbo].[InstanceData] WHERE id = @id)
			SET @result = 2; -- Did not have lock
		ELSE
			SET @result = 1; -- Instance not found
	END
END
GO
GRANT EXECUTE ON [dbo].[UnlockInstance] TO persistenceUsers
GO

/* WWF LOGIC 3.5 ( END ) */


/* WWF SCHEMA 4.0 (BEGIN) */
set ansi_nulls on
set quoted_identifier on
set nocount on
go

if not exists( select 1 from [dbo].[sysusers] where name=N'System.Activities.DurableInstancing.InstanceStoreUsers' and issqlrole=1 )
	create role [System.Activities.DurableInstancing.InstanceStoreUsers]
go

if not exists( select 1 from [dbo].[sysusers] where name=N'System.Activities.DurableInstancing.WorkflowActivationUsers' and issqlrole=1 )
	create role [System.Activities.DurableInstancing.WorkflowActivationUsers]
go

if not exists( select 1 from [dbo].[sysusers] where name=N'System.Activities.DurableInstancing.InstanceStoreObservers' and issqlrole=1 )
	create role [System.Activities.DurableInstancing.InstanceStoreObservers]
go

if not exists (select * from sys.schemas where name = N'System.Activities.DurableInstancing')
	exec ('create schema [System.Activities.DurableInstancing]')
go

if exists (select * from sys.views where object_id = object_id(N'[System.Activities.DurableInstancing].[InstancePromotedProperties]'))
      drop view [System.Activities.DurableInstancing].[InstancePromotedProperties]
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[InstancesTable]') and type in (N'U'))
	drop table [System.Activities.DurableInstancing].[InstancesTable]
go

create table [System.Activities.DurableInstancing].[InstancesTable]
(
	[Id] uniqueidentifier not null,
	[SurrogateInstanceId] bigint identity not null,
	[SurrogateLockOwnerId] bigint null,
	[PrimitiveDataProperties] varbinary(max) default null,
	[ComplexDataProperties] varbinary(max) default null,
	[WriteOnlyPrimitiveDataProperties] varbinary(max) default null,
	[WriteOnlyComplexDataProperties] varbinary(max) default null,
	[MetadataProperties] varbinary(max) default null,
	[DataEncodingOption] tinyint default 0,
	[MetadataEncodingOption] tinyint default 0,
	[Version] bigint not null,
	[PendingTimer] datetime null,
	[CreationTime] datetime not null,
	[LastUpdated] datetime default null,
	[WorkflowHostType] uniqueidentifier null,
	[ServiceDeploymentId] bigint null,
	[SuspensionExceptionName] nvarchar(450) default null,
	[SuspensionReason] nvarchar(max) default null,
	[BlockingBookmarks] nvarchar(max) default null,
	[LastMachineRunOn] nvarchar(450) default null,
	[ExecutionStatus] nvarchar(450) default null,
	[IsInitialized] bit default 0,
	[IsSuspended] bit default 0,
	[IsReadyToRun] bit default 0,
	[IsCompleted] bit default 0
)
go

create unique clustered index [CIX_InstancesTable]
	on [System.Activities.DurableInstancing].[InstancesTable] ([SurrogateInstanceId])
	with (allow_page_locks = off)
go

create unique nonclustered index [NCIX_InstancesTable_Id]
	on [System.Activities.DurableInstancing].[InstancesTable] ([Id])
	include ([Version], [SurrogateLockOwnerId], [IsCompleted])
	with (allow_page_locks = off)
go

create nonclustered index [NCIX_InstancesTable_SurrogateLockOwnerId]
	on [System.Activities.DurableInstancing].[InstancesTable] ([SurrogateLockOwnerId])
	with (allow_page_locks = off)
go

create nonclustered index NCIX_InstancesTable_LastUpdated
	on [System.Activities.DurableInstancing].[InstancesTable] ([LastUpdated])
	with (allow_page_locks = off)
go

create nonclustered index [NCIX_InstancesTable_CreationTime]
	on [System.Activities.DurableInstancing].[InstancesTable] ([CreationTime])
	with (allow_page_locks = off)
go

create nonclustered index [NCIX_InstancesTable_SuspensionExceptionName]
	on [System.Activities.DurableInstancing].[InstancesTable] ([SuspensionExceptionName])
	with (allow_page_locks = off)
go

create nonclustered index [NCIX_InstancesTable_ServiceDeploymentId]
	on [System.Activities.DurableInstancing].[InstancesTable] ([ServiceDeploymentId])
	with (allow_page_locks = off)
go

create nonclustered index [NCIX_InstancesTable_WorkflowHostType]
	on [System.Activities.DurableInstancing].[InstancesTable] ([WorkflowHostType])
	with (allow_page_locks = off)
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[RunnableInstancesTable]') and type in (N'U'))
	drop table [System.Activities.DurableInstancing].[RunnableInstancesTable]
go

create table [System.Activities.DurableInstancing].[RunnableInstancesTable]
(
	[SurrogateInstanceId] bigint not null,		
	[WorkflowHostType] uniqueidentifier null,
	[ServiceDeploymentId] bigint null,
	[RunnableTime] datetime not null
)
go

create unique clustered index [CIX_RunnableInstancesTable_SurrogateInstanceId]
	on [System.Activities.DurableInstancing].[RunnableInstancesTable] ([SurrogateInstanceId])
	with (ignore_dup_key = on, allow_page_locks = off)
go

create nonclustered index [NCIX_RunnableInstancesTable]
	on [System.Activities.DurableInstancing].[RunnableInstancesTable] ([WorkflowHostType], [RunnableTime])
	with (allow_page_locks = off)
go

create nonclustered index [NCIX_RunnableInstancesTable_RunnableTime]
	on [System.Activities.DurableInstancing].[RunnableInstancesTable] ([RunnableTime]) include ([WorkflowHostType], [ServiceDeploymentId])
	with (allow_page_locks = off)
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[KeysTable]') and type in (N'U'))
	drop table [System.Activities.DurableInstancing].[KeysTable]
go

create table [System.Activities.DurableInstancing].[KeysTable]
(
	[Id] uniqueidentifier not null,
	[SurrogateKeyId] bigint identity not null,
	[SurrogateInstanceId] bigint null,
	[EncodingOption] tinyint null,
	[Properties] varbinary(max) null,
	[IsAssociated] bit
) 
go

create unique clustered index [CIX_KeysTable]
	on [System.Activities.DurableInstancing].[KeysTable] ([Id])	
	with (ignore_dup_key = on, allow_page_locks = off)
go

create nonclustered index [NCIX_KeysTable_SurrogateInstanceId]
	on [System.Activities.DurableInstancing].[KeysTable] ([SurrogateInstanceId])
	with (allow_page_locks = off)
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[LockOwnersTable]') and type in (N'U'))
	drop table [System.Activities.DurableInstancing].[LockOwnersTable]
go

create table [System.Activities.DurableInstancing].[LockOwnersTable]
(
	[Id] uniqueidentifier not null,
	[SurrogateLockOwnerId] bigint identity not null,
	[LockExpiration] datetime not null,
	[WorkflowHostType] uniqueidentifier null,
	[MachineName] nvarchar(128) not null,
	[EnqueueCommand] bit not null,
	[DeletesInstanceOnCompletion] bit not null,
	[PrimitiveLockOwnerData] varbinary(max) default null,
	[ComplexLockOwnerData] varbinary(max) default null,
	[WriteOnlyPrimitiveLockOwnerData] varbinary(max) default null,
	[WriteOnlyComplexLockOwnerData] varbinary(max) default null,
	[EncodingOption] tinyint default 0
)
go

create unique clustered index [CIX_LockOwnersTable]
	on [System.Activities.DurableInstancing].[LockOwnersTable] ([SurrogateLockOwnerId])
	with (allow_page_locks = off)
go

create unique nonclustered index [NCIX_LockOwnersTable_Id]
	on [System.Activities.DurableInstancing].[LockOwnersTable] ([Id])
	with (ignore_dup_key = on, allow_page_locks = off)

create nonclustered index [NCIX_LockOwnersTable_LockExpiration]
	on [System.Activities.DurableInstancing].[LockOwnersTable] ([LockExpiration]) include ([WorkflowHostType], [MachineName])
	with (allow_page_locks = off)
go

create nonclustered index [NCIX_LockOwnersTable_WorkflowHostType]
	on [System.Activities.DurableInstancing].[LockOwnersTable] ([WorkflowHostType])
	with (allow_page_locks = off)
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[InstanceMetadataChangesTable]') and type in (N'U'))
	drop table [System.Activities.DurableInstancing].[InstanceMetadataChangesTable]
go

create table [System.Activities.DurableInstancing].[InstanceMetadataChangesTable]
(
	[SurrogateInstanceId] bigint not null,
	[ChangeTime] bigint identity,
	[EncodingOption] tinyint not null,
	[Change] varbinary(max) not null
)
go

create unique clustered index [CIX_InstanceMetadataChangesTable]
	on [System.Activities.DurableInstancing].[InstanceMetadataChangesTable] ([SurrogateInstanceId], [ChangeTime])
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[ServiceDeploymentsTable]') and type in (N'U'))
	drop table [System.Activities.DurableInstancing].[ServiceDeploymentsTable]
go

create table [System.Activities.DurableInstancing].[ServiceDeploymentsTable]
(
	[Id] bigint identity not null,
	[ServiceDeploymentHash] uniqueidentifier not null,
	[SiteName] nvarchar(max) not null,
	[RelativeServicePath] nvarchar(max) not null,
	[RelativeApplicationPath] nvarchar(max) not null,
	[ServiceName] nvarchar(max) not null,
	[ServiceNamespace] nvarchar(max) not null,
)
go

create unique clustered index [CIX_ServiceDeploymentsTable]
	on [System.Activities.DurableInstancing].[ServiceDeploymentsTable] ([Id])
	with (allow_page_locks = off)
go

create unique nonclustered index [NCIX_ServiceDeploymentsTable_ServiceDeploymentHash]
	on [System.Activities.DurableInstancing].[ServiceDeploymentsTable] ([ServiceDeploymentHash])
	with (ignore_dup_key = on, allow_page_locks = off)
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[InstancePromotedPropertiesTable]') and type in (N'U'))
	drop table [System.Activities.DurableInstancing].[InstancePromotedPropertiesTable]
go

create table [System.Activities.DurableInstancing].[InstancePromotedPropertiesTable]
(
	  [SurrogateInstanceId] bigint not null,
      [PromotionName] nvarchar(400) not null,
      [Value1] sql_variant null,
      [Value2] sql_variant null,
      [Value3] sql_variant null,
      [Value4] sql_variant null,
      [Value5] sql_variant null,
      [Value6] sql_variant null,
      [Value7] sql_variant null,
      [Value8] sql_variant null,
      [Value9] sql_variant null,
      [Value10] sql_variant null,
      [Value11] sql_variant null,
      [Value12] sql_variant null,
      [Value13] sql_variant null,
      [Value14] sql_variant null,
      [Value15] sql_variant null,
      [Value16] sql_variant null,
      [Value17] sql_variant null,
      [Value18] sql_variant null,
      [Value19] sql_variant null,
      [Value20] sql_variant null,
      [Value21] sql_variant null,
      [Value22] sql_variant null,
      [Value23] sql_variant null,
      [Value24] sql_variant null,
      [Value25] sql_variant null,
      [Value26] sql_variant null,
      [Value27] sql_variant null,
      [Value28] sql_variant null,
      [Value29] sql_variant null,
      [Value30] sql_variant null,
      [Value31] sql_variant null,
      [Value32] sql_variant null,
      [Value33] varbinary(max) null,
      [Value34] varbinary(max) null,
      [Value35] varbinary(max) null,
      [Value36] varbinary(max) null,
      [Value37] varbinary(max) null,
      [Value38] varbinary(max) null,
      [Value39] varbinary(max) null,
      [Value40] varbinary(max) null,
      [Value41] varbinary(max) null,
      [Value42] varbinary(max) null,
      [Value43] varbinary(max) null,
      [Value44] varbinary(max) null,
      [Value45] varbinary(max) null,
      [Value46] varbinary(max) null,
      [Value47] varbinary(max) null,
      [Value48] varbinary(max) null,
      [Value49] varbinary(max) null,
      [Value50] varbinary(max) null,
      [Value51] varbinary(max) null,
      [Value52] varbinary(max) null,
      [Value53] varbinary(max) null,
      [Value54] varbinary(max) null,
      [Value55] varbinary(max) null,
      [Value56] varbinary(max) null,
      [Value57] varbinary(max) null,
      [Value58] varbinary(max) null,
      [Value59] varbinary(max) null,
      [Value60] varbinary(max) null,
      [Value61] varbinary(max) null,
      [Value62] varbinary(max) null,
      [Value63] varbinary(max) null,
      [Value64] varbinary(max) null
)
go

create unique clustered index [CIX_InstancePromotedPropertiesTable]
	on [System.Activities.DurableInstancing].[InstancePromotedPropertiesTable] ([SurrogateInstanceId], [PromotionName])
	with (allow_page_locks = off)
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[SqlWorkflowInstanceStoreVersionTable]') and type in (N'U'))
	drop table [System.Activities.DurableInstancing].[SqlWorkflowInstanceStoreVersionTable]
go

create table [System.Activities.DurableInstancing].[SqlWorkflowInstanceStoreVersionTable]
(
	[Major] bigint,
	[Minor] bigint,
	[Build] bigint,
	[Revision] bigint,
	[LastUpdated] datetime
)
go

create unique clustered index [CIX_SqlWorkflowInstanceStoreVersionTable]
	on [System.Activities.DurableInstancing].[SqlWorkflowInstanceStoreVersionTable] ([Major], [Minor], [Build], [Revision])
go

insert into [System.Activities.DurableInstancing].[SqlWorkflowInstanceStoreVersionTable]
values (4, 0, 0, 0, getutcdate())

if exists (select * from sys.views where object_id = object_id(N'[System.Activities.DurableInstancing].[Instances]'))
      drop view [System.Activities.DurableInstancing].[Instances]
go

create view [System.Activities.DurableInstancing].[Instances] as
      select [InstancesTable].[Id] as [InstanceId],
             [PendingTimer],
             [CreationTime],
             [LastUpdated] as [LastUpdatedTime],
             [InstancesTable].[ServiceDeploymentId],
             [SuspensionExceptionName],
             [SuspensionReason],
             [BlockingBookmarks] as [ActiveBookmarks],
             case when [LockOwnersTable].[LockExpiration] > getutcdate()
				then [LockOwnersTable].[MachineName]
				else null
				end as [CurrentMachine],
             [LastMachineRunOn] as [LastMachine],
             [ExecutionStatus],
             [IsInitialized],
             [IsSuspended],
             [IsCompleted],
             [InstancesTable].[DataEncodingOption] as [EncodingOption],
             [PrimitiveDataProperties] as [ReadWritePrimitiveDataProperties],
             [WriteOnlyPrimitiveDataProperties],
             [ComplexDataProperties] as [ReadWriteComplexDataProperties],
             [WriteOnlyComplexDataProperties]
      from [System.Activities.DurableInstancing].[InstancesTable]
      left outer join [System.Activities.DurableInstancing].[LockOwnersTable]
      on [InstancesTable].[SurrogateLockOwnerId] = [LockOwnersTable].[SurrogateLockOwnerId]
go

grant select on object::[System.Activities.DurableInstancing].[Instances] to [System.Activities.DurableInstancing.InstanceStoreObservers]
go

grant delete on object::[System.Activities.DurableInstancing].[Instances] to [System.Activities.DurableInstancing.InstanceStoreUsers]
go

if exists (select * from sys.views where object_id = object_id(N'[System.Activities.DurableInstancing].[ServiceDeployments]'))
      drop view [System.Activities.DurableInstancing].[ServiceDeployments]
go

create view [System.Activities.DurableInstancing].[ServiceDeployments] as
      select [Id] as [ServiceDeploymentId],
             [SiteName],
             [RelativeServicePath],
             [RelativeApplicationPath],
             [ServiceName],
             [ServiceNamespace]
      from [System.Activities.DurableInstancing].[ServiceDeploymentsTable]
go

grant select on object::[System.Activities.DurableInstancing].[ServiceDeployments] to [System.Activities.DurableInstancing.InstanceStoreObservers]
go

grant delete on object::[System.Activities.DurableInstancing].[ServiceDeployments] to [System.Activities.DurableInstancing.InstanceStoreUsers]
go

create view [System.Activities.DurableInstancing].[InstancePromotedProperties] with schemabinding as
      select [InstancesTable].[Id] as [InstanceId],
			 [InstancesTable].[DataEncodingOption] as [EncodingOption],
			 [PromotionName],
			 [Value1],
			 [Value2],
			 [Value3],
			 [Value4],
			 [Value5],
			 [Value6],
			 [Value7],
			 [Value8],
			 [Value9],
			 [Value10],
			 [Value11],
			 [Value12],
			 [Value13],
			 [Value14],
			 [Value15],
			 [Value16],
			 [Value17],
			 [Value18],
			 [Value19],
			 [Value20],
			 [Value21],
			 [Value22],
			 [Value23],
			 [Value24],
			 [Value25],
			 [Value26],
			 [Value27],
			 [Value28],
			 [Value29],
			 [Value30],
			 [Value31],
			 [Value32],
			 [Value33],
			 [Value34],
			 [Value35],
			 [Value36],
			 [Value37],
			 [Value38],
			 [Value39],
			 [Value40],
			 [Value41],
			 [Value42],
			 [Value43],
			 [Value44],
			 [Value45],
			 [Value46],
			 [Value47],
			 [Value48],
			 [Value49],
			 [Value50],
			 [Value51],
			 [Value52],
			 [Value53],
			 [Value54],
			 [Value55],
			 [Value56],
			 [Value57],
			 [Value58],
			 [Value59],
			 [Value60],
			 [Value61],
			 [Value62],
			 [Value63],
			 [Value64]
    from [System.Activities.DurableInstancing].[InstancePromotedPropertiesTable]
    join [System.Activities.DurableInstancing].[InstancesTable]
    on [InstancePromotedPropertiesTable].[SurrogateInstanceId] = [InstancesTable].[SurrogateInstanceId]
go

grant select on object::[System.Activities.DurableInstancing].[InstancePromotedProperties] to [System.Activities.DurableInstancing.InstanceStoreObservers]
go

/* WWF SCHEMA 4.0 ( END ) */


/* WWF LOGIC 4.0 (BEGIN) */

set ansi_nulls on
set quoted_identifier on
set nocount on
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[InsertRunnableInstanceEntry]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[InsertRunnableInstanceEntry]
go

create procedure [System.Activities.DurableInstancing].[InsertRunnableInstanceEntry]
	@surrogateInstanceId bigint,
	@workflowHostType uniqueidentifier,
	@serviceDeploymentId bigint, 
	@isSuspended bit,
	@isReadyToRun bit,
	@pendingTimer datetime
AS
begin    
	set nocount on;
	set transaction isolation level read committed;
	set xact_abort on;	
	
	declare @runnableTime datetime
	
	if (@isSuspended  = 0)
	begin
		if (@isReadyToRun = 1)
		begin
			set @runnableTime = getutcdate()					
		end
		else if (@pendingTimer is not null)
		begin
			set @runnableTime = @pendingTimer
		end
	end
		
	if (@runnableTime is not null and @workflowHostType is not null)
	begin	
		insert into [RunnableInstancesTable]
			([SurrogateInstanceId], [WorkflowHostType], [ServiceDeploymentId], [RunnableTime])
			values( @surrogateInstanceId, @workflowHostType, @serviceDeploymentId, @runnableTime)
	end
end
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[RecoverInstanceLocks]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[RecoverInstanceLocks]
go

create procedure [System.Activities.DurableInstancing].[RecoverInstanceLocks]
as
begin
	set nocount on;
	set transaction isolation level read committed;
	set xact_abort on;
	set deadlock_priority low;
    
	declare @now as datetime
	set @now = getutcdate()	
	
	insert into [RunnableInstancesTable] ([SurrogateInstanceId], [WorkflowHostType], [ServiceDeploymentId], [RunnableTime])
		select top (1000) instances.[SurrogateInstanceId], instances.[WorkflowHostType], instances.[ServiceDeploymentId], @now
		from [LockOwnersTable] lockOwners with (readpast) inner loop join
			 [InstancesTable] instances with (readpast)
				on instances.[SurrogateLockOwnerId] = lockOwners.[SurrogateLockOwnerId]
			where 
				lockOwners.[LockExpiration] <= @now and
				instances.[IsInitialized] = 1 and
				instances.[IsSuspended] = 0

	delete from [LockOwnersTable] with (readpast)
	from [LockOwnersTable] lockOwners
	where [LockExpiration] <= @now
	and not exists
	(
		select top (1) 1
		from [InstancesTable] instances with (nolock)
		where instances.[SurrogateLockOwnerId] = lockOwners.[SurrogateLockOwnerId]
	)
end
go

grant execute on [System.Activities.DurableInstancing].[RecoverInstanceLocks] to [System.Activities.DurableInstancing.InstanceStoreUsers]
go

grant execute on [System.Activities.DurableInstancing].[RecoverInstanceLocks] to [System.Activities.DurableInstancing.WorkflowActivationUsers]
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[ParseBinaryPropertyValue]') and type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	drop function [System.Activities.DurableInstancing].[ParseBinaryPropertyValue]
go

create function [System.Activities.DurableInstancing].[ParseBinaryPropertyValue] (@startPosition int, @length int, @concatenatedKeyProperties varbinary(max))
returns varbinary(max)
as
begin
	if (@length > 0)
		return substring(@concatenatedKeyProperties, @startPosition + 1, @length)
	return null
end
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[GetExpirationTime]') and type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	drop function [System.Activities.DurableInstancing].[GetExpirationTime]
go

create function [System.Activities.DurableInstancing].[GetExpirationTime] (@offsetInMilliseconds bigint)
returns datetime
as
begin

	if (@offsetInMilliseconds is null)
	begin
		return null
	end

	declare @hourInMillisecond bigint
	declare @offsetInHours bigint
	declare @remainingOffsetInMilliseconds bigint
	declare @expirationTimer datetime

	set @hourInMillisecond = 60*60*1000
	set @offsetInHours = @offsetInMilliseconds / @hourInMillisecond
	set @remainingOffsetInMilliseconds = @offsetInMilliseconds % @hourInMillisecond

	set @expirationTimer = getutcdate()
	set @expirationTimer = dateadd (hour, @offsetInHours, @expirationTimer)
	set @expirationTimer = dateadd (millisecond,@remainingOffsetInMilliseconds, @expirationTimer)

	return @expirationTimer

end
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[CreateLockOwner]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[CreateLockOwner]
go

create procedure [System.Activities.DurableInstancing].[CreateLockOwner]
	@lockOwnerId uniqueidentifier,
	@lockTimeout int,
	@workflowHostType uniqueidentifier,
	@enqueueCommand bit,
	@deleteInstanceOnCompletion bit,	
	@primitiveLockOwnerData varbinary(max),
	@complexLockOwnerData varbinary(max),
	@writeOnlyPrimitiveLockOwnerData varbinary(max),
	@writeOnlyComplexLockOwnerData varbinary(max),
	@encodingOption tinyint,
	@machineName nvarchar(128)
as
begin
	set nocount on
	set transaction isolation level read committed
	set xact_abort on;	
	
	begin transaction
	
	declare @lockAcquired bigint
	declare @lockExpiration datetime
	declare @now datetime
	declare @result int
	declare @surrogateLockOwnerId bigint
	
	set @result = 0
	
	exec @lockAcquired = sp_getapplock @Resource = 'InstanceStoreLock', @LockMode = 'Shared', @LockTimeout = 10000
		
	if (@lockAcquired < 0)
	begin
		select @result as 'Result'
		set @result = 13
	end
	
	if (@result = 0)
	begin
		set @now = getutcdate()
		
		if (@lockTimeout = 0)
			set @lockExpiration = '9999-12-31T23:59:59';
		else 
			set @lockExpiration = dateadd(second, @lockTimeout, getutcdate());
		
		insert into [LockOwnersTable] ([Id], [LockExpiration], [MachineName], [WorkflowHostType], [EnqueueCommand], [DeletesInstanceOnCompletion], [PrimitiveLockOwnerData], [ComplexLockOwnerData], [WriteOnlyPrimitiveLockOwnerData], [WriteOnlyComplexLockOwnerData], [EncodingOption])
		values (@lockOwnerId, @lockExpiration, @machineName, @workflowHostType, @enqueueCommand, @deleteInstanceOnCompletion, @primitiveLockOwnerData, @complexLockOwnerData, @writeOnlyPrimitiveLockOwnerData, @writeOnlyComplexLockOwnerData, @encodingOption)
		
		set @surrogateLockOwnerId = scope_identity()
	end
	
	if (@result != 13)
		exec sp_releaseapplock @Resource = 'InstanceStoreLock'
	
	if (@result = 0)
	begin
		commit transaction
		select 0 as 'Result', @surrogateLockOwnerId
	end
	else
		rollback transaction
end
go

grant execute on [System.Activities.DurableInstancing].[CreateLockOwner] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

grant execute on [System.Activities.DurableInstancing].[CreateLockOwner] to [System.Activities.DurableInstancing.WorkflowActivationUsers] 
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[DeleteLockOwner]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[DeleteLockOwner]
go

create procedure [System.Activities.DurableInstancing].[DeleteLockOwner]
	@surrogateLockOwnerId bigint
as
begin
	set nocount on
	set transaction isolation level read committed
	set deadlock_priority low
	set xact_abort on;	
	
	begin transaction
	
	declare @lockAcquired bigint
	declare @result int
	set @result = 0
	
	exec @lockAcquired = sp_getapplock @Resource = 'InstanceStoreLock', @LockMode = 'Shared', @LockTimeout = 10000
		
	if (@lockAcquired < 0)
	begin
		select @result as 'Result'
		set @result = 13
	end
	
	if (@result = 0)
	begin
		update [LockOwnersTable]
		set [LockExpiration] = '2000-01-01T00:00:00'
		where [SurrogateLockOwnerId] = @surrogateLockOwnerId
	end
	
	if (@result != 13)
		exec sp_releaseapplock @Resource = 'InstanceStoreLock' 
	
	if (@result = 0)
	begin
		commit transaction
		select 0 as 'Result'
	end
	else
		rollback transaction
end
go

grant execute on [System.Activities.DurableInstancing].[DeleteLockOwner] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

grant execute on [System.Activities.DurableInstancing].[DeleteLockOwner] to [System.Activities.DurableInstancing.WorkflowActivationUsers] 
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[ExtendLock]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[ExtendLock]
go
create procedure [System.Activities.DurableInstancing].[ExtendLock]
	@surrogateLockOwnerId bigint,
	@lockTimeout int	
as
begin
	set nocount on
	set transaction isolation level read committed
	set xact_abort on;	
	
	begin transaction	
	
	declare @now datetime
	declare @newLockExpiration datetime
	declare @result int
	
	set @now = getutcdate()
	set @result = 0
	
	if (@lockTimeout = 0)
		set @newLockExpiration = '9999-12-31T23:59:59'
	else
		set @newLockExpiration = dateadd(second, @lockTimeout, @now)
	
	update [LockOwnersTable]
	set [LockExpiration] = @newLockExpiration
	where ([SurrogateLockOwnerId] = @surrogateLockOwnerId) and
		  ([LockExpiration] > @now)
	
	if (@@rowcount = 0) 
	begin
		if exists (select * from [LockOwnersTable] where ([SurrogateLockOwnerId] = @surrogateLockOwnerId))
		begin
			exec [System.Activities.DurableInstancing].[DeleteLockOwner] @surrogateLockOwnerId
			set @result = 11
		end
		else
			set @result = 12
	end
	
	select @result as 'Result'
	commit transaction
end
go

grant execute on [System.Activities.DurableInstancing].[ExtendLock] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

grant execute on [System.Activities.DurableInstancing].[ExtendLock] to [System.Activities.DurableInstancing.WorkflowActivationUsers] 
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[AssociateKeys]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[AssociateKeys]
go
create procedure [System.Activities.DurableInstancing].[AssociateKeys]
	@surrogateInstanceId bigint,
	@keysToAssociate xml = null,
	@concatenatedKeyProperties varbinary(max) = null,
	@encodingOption tinyint,
	@singleKeyId uniqueidentifier
as
begin	
	set nocount on
	set transaction isolation level read committed
	set xact_abort on;	
	
	declare @badKeyId uniqueidentifier
	declare @numberOfKeys int
	declare @result int
	declare @keys table([KeyId] uniqueidentifier, [Properties] varbinary(max))
	
	set @result = 0
	
	if (@keysToAssociate is not null)
	begin
		insert into @keys
		select T.Item.value('@KeyId', 'uniqueidentifier') as [KeyId],
			   [System.Activities.DurableInstancing].[ParseBinaryPropertyValue](T.Item.value('@StartPosition', 'int'), T.Item.value('@BinaryLength', 'int'), @concatenatedKeyProperties) as [Properties]
	    from @keysToAssociate.nodes('/CorrelationKeys/CorrelationKey') as T(Item)
		option (maxdop 1)

		select @numberOfKeys = count(1) from @keys
		
		insert into [KeysTable] ([Id], [SurrogateInstanceId], [IsAssociated])
		select [KeyId], @surrogateInstanceId, 1
		from @keys as [Keys]
		
		if (@@rowcount != @numberOfKeys)
		begin
			select top 1 @badKeyId = [Keys].[KeyId] 
			from @keys as [Keys]
			join [KeysTable] on [Keys].[KeyId] = [KeysTable].[Id]
			where [KeysTable].[SurrogateInstanceId] != @surrogateInstanceId
			
			if (@@rowcount != 0)
			begin
				select 3 as 'Result', @badKeyId
				return 3
			end
		end
		
		update [KeysTable]
		set [Properties] = [Keys].[Properties],
			[EncodingOption] = @encodingOption
		from @keys as [Keys]
		join [KeysTable] on [Keys].[KeyId] = [KeysTable].[Id]
		where [KeysTable].[EncodingOption] is null
	end
	
	if (@singleKeyId is not null)
	begin
InsertSingleKey:
		update [KeysTable]
		set [Properties] = @concatenatedKeyProperties,
			[EncodingOption] = @encodingOption
		where ([Id] = @singleKeyId) and ([SurrogateInstanceId] = @surrogateInstanceId)
			  
		if (@@rowcount != 1)
		begin
			if exists (select [Id] from [KeysTable] where [Id] = @singleKeyId)
			begin
				select 3 as 'Result', @singleKeyId
				return 3
			end
			
			insert into [KeysTable] ([Id], [SurrogateInstanceId], [IsAssociated], [Properties], [EncodingOption])
			values (@singleKeyId, @surrogateInstanceId, 1, @concatenatedKeyProperties, @encodingOption)
			
			if (@@rowcount = 0)
				goto InsertSingleKey
		end
	end
end
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[CompleteKeys]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[CompleteKeys]
go
create procedure [System.Activities.DurableInstancing].[CompleteKeys]
	@surrogateInstanceId bigint,
	@keysToComplete xml = null
as
begin	
	set nocount on
	set transaction isolation level read committed
	set xact_abort on;	
	
	declare @badKeyId uniqueidentifier
	declare @numberOfKeys int
	declare @result int
	declare @keyIds table([KeyId] uniqueidentifier)
	
	set @result = 0
	
	if (@keysToComplete is not null)
	begin
		insert into @keyIds
		select T.Item.value('@KeyId', 'uniqueidentifier')
		from @keysToComplete.nodes('//CorrelationKey') as T(Item)
		option(maxdop 1)
		
		select @numberOfKeys = count(1) from @keyIds
		
		update [KeysTable]
		set [IsAssociated] = 0
		from @keyIds as [KeyIds]
		join [KeysTable] on [KeyIds].[KeyId] = [KeysTable].[Id]
		where [SurrogateInstanceId] = @surrogateInstanceId
		
		if (@@rowcount != @numberOfKeys)
		begin
			select top 1 @badKeyId = [MissingKeys].[MissingKeyId]
			from
				(select [KeyIds].[KeyId] as [MissingKeyId] 
				 from @keyIds as [KeyIds]
				 except
				 select [Id] from [KeysTable] where [SurrogateInstanceId] = @surrogateInstanceId) as MissingKeys
		
			select 4 as 'Result', @badKeyId
			return 4
		end
	end
end
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[FreeKeys]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[FreeKeys]
go
create procedure [System.Activities.DurableInstancing].[FreeKeys]
	@surrogateInstanceId bigint,
	@keysToFree xml = null
as
begin	
	set nocount on
	set transaction isolation level read committed
	set xact_abort on;	
	
	declare @badKeyId uniqueidentifier
	declare @numberOfKeys int
	declare @result int
	declare @keyIds table([KeyId] uniqueidentifier)
	
	set @result = 0
	
	if (@keysToFree is not null)
	begin
		insert into @keyIds
		select T.Item.value('@KeyId', 'uniqueidentifier')
		from @keysToFree.nodes('//CorrelationKey') as T(Item)
		option(maxdop 1)
		
		select @numberOfKeys = count(1) from @keyIds
		
		delete [KeysTable]
		from @keyIds as [KeyIds]
		join [KeysTable] on [KeyIds].[KeyId] = [KeysTable].[Id]
		where [SurrogateInstanceId] = @surrogateInstanceId

		if (@@rowcount != @numberOfKeys)
		begin
			select top 1 @badKeyId = [MissingKeys].[MissingKeyId] from
				(select [KeyIds].[KeyId] as [MissingKeyId]
				 from @keyIds as [KeyIds]
				 except
				 select [Id] from [KeysTable] where [SurrogateInstanceId] = @surrogateInstanceId) as MissingKeys
		
			select 4 as 'Result', @badKeyId
			return 4
		end
	end
end
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[CreateInstance]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[CreateInstance]
go
create procedure [System.Activities.DurableInstancing].[CreateInstance]
	@instanceId uniqueidentifier,
	@surrogateLockOwnerId bigint,
	@workflowHostType uniqueidentifier,
	@serviceDeploymentId bigint,
	@surrogateInstanceId bigint output,
	@result int output
as
begin
	set nocount on
	set transaction isolation level read committed
	set xact_abort on;	
	
	set @surrogateInstanceId = 0
	set @result = 0
	
	begin try
		insert into [InstancesTable] ([Id], [SurrogateLockOwnerId], [CreationTime], [WorkflowHostType], [ServiceDeploymentId], [Version])
		values (@instanceId, @surrogateLockOwnerId, getutcdate(), @workflowHostType, @serviceDeploymentId, 1)
		
		set @surrogateInstanceId = scope_identity()		
	end try
	begin catch
		if (error_number() != 2601)
		begin
			set @result = 99
			select @result as 'Result'
		end
	end catch
end
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[LockInstance]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[LockInstance]
go
create procedure [System.Activities.DurableInstancing].[LockInstance]
	@instanceId uniqueidentifier,
	@surrogateLockOwnerId bigint,
	@handleInstanceVersion bigint,
	@handleIsBoundToLock bit,
	@surrogateInstanceId bigint output,
	@lockVersion bigint output,
	@result int output
as
begin
	set nocount on
	set transaction isolation level read committed
	set xact_abort on;	
	
	declare @isCompleted bit
	declare @currentLockOwnerId bigint
	declare @currentVersion bigint

TryLockInstance:
	set @currentLockOwnerId = 0
	set @surrogateInstanceId = 0
	set @result = 0
	
	update [InstancesTable]
	set [SurrogateLockOwnerId] = @surrogateLockOwnerId,
		@lockVersion = [Version] = case when ([InstancesTable].[SurrogateLockOwnerId] is null or 
											  [InstancesTable].[SurrogateLockOwnerId] != @surrogateLockOwnerId)
									then [Version] + 1
									else [Version]
								  end,
		@surrogateInstanceId = [SurrogateInstanceId]
	from [InstancesTable]
	left outer join [LockOwnersTable] on [InstancesTable].[SurrogateLockOwnerId] = [LockOwnersTable].[SurrogateLockOwnerId]
	where ([InstancesTable].[Id] = @instanceId) and
		  ([InstancesTable].[IsCompleted] = 0) and
		  (
		   (@handleIsBoundToLock = 0 and
		    (
		     ([InstancesTable].[SurrogateLockOwnerId] is null) or
		     ([LockOwnersTable].[SurrogateLockOwnerId] is null) or
			  (
		       ([LockOwnersTable].[LockExpiration] < getutcdate()) and
               ([LockOwnersTable].[SurrogateLockOwnerId] != @surrogateLockOwnerId)
			  )
		    )
		   ) or 
		   (
			(@handleIsBoundToLock = 1) and
		    ([LockOwnersTable].[SurrogateLockOwnerId] = @surrogateLockOwnerId) and
		    ([LockOwnersTable].[LockExpiration] > getutcdate()) and
		    ([InstancesTable].[Version] = @handleInstanceVersion)
		   )
		  )
	
	if (@@rowcount = 0)
	begin
		if not exists (select * from [LockOwnersTable] where ([SurrogateLockOwnerId] = @surrogateLockOwnerId) and ([LockExpiration] > getutcdate()))
		begin
			if exists (select * from [LockOwnersTable] where [SurrogateLockOwnerId] = @surrogateLockOwnerId)
				set @result = 11
			else
				set @result = 12
			
			select @result as 'Result'
			return 0
		end
		
		select @currentLockOwnerId = [SurrogateLockOwnerId],
			   @isCompleted = [IsCompleted],
			   @currentVersion = [Version]
		from [InstancesTable]
		where [Id] = @instanceId
	
		if (@@rowcount = 1)
		begin
			if (@isCompleted = 1)
				set @result = 7
			else if (@currentLockOwnerId = @surrogateLockOwnerId)
			begin
				if (@handleIsBoundToLock = 1)
					set @result = 10
				else
					set @result = 14
			end
			else if (@handleIsBoundToLock = 0)
				set @result = 2
			else
				set @result = 6
		end
		else if (@handleIsBoundToLock = 1)
			set @result = 6
	end

	if (@result != 0 and @result != 2)
		select @result as 'Result', @instanceId, @currentVersion
	else if (@result = 2)
	begin
		select @result as 'Result', @instanceId, [LockOwnersTable].[Id], [LockOwnersTable].[EncodingOption], [PrimitiveLockOwnerData], [ComplexLockOwnerData]
		from [LockOwnersTable]
		join [InstancesTable] on [InstancesTable].[SurrogateLockOwnerId] = [LockOwnersTable].[SurrogateLockOwnerId]
		where [InstancesTable].[SurrogateLockOwnerId] = @currentLockOwnerId and
			  [InstancesTable].[Id] = @instanceId
		
		if (@@rowcount = 0)
			goto TryLockInstance
	end
end
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[UnlockInstance]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[UnlockInstance]
go
create procedure [System.Activities.DurableInstancing].[UnlockInstance]
	@surrogateLockOwnerId bigint,
	@instanceId uniqueidentifier,
	@handleInstanceVersion bigint
as
begin
	set nocount on
	set transaction isolation level read committed		
	set xact_abort on;	
	begin transaction
	
	declare @pendingTimer datetime
	declare @surrogateInstanceId bigint
	declare @workflowHostType uniqueidentifier
	declare @serviceDeploymentId bigint
	declare @enqueueCommand bit	
	declare @isReadyToRun bit	
	declare @isSuspended bit
	declare @now datetime
	
	set @now = getutcdate()
		
	update [InstancesTable]
	set [SurrogateLockOwnerId] = null,
	    @surrogateInstanceId = [SurrogateInstanceId],
	    @workflowHostType = [WorkflowHostType],
   	    @serviceDeploymentId = [ServiceDeploymentId],
	    @pendingTimer = [PendingTimer],
	    @isReadyToRun =  [IsReadyToRun],
	    @isSuspended = [IsSuspended]
	where [Id] = @instanceId and
		  [SurrogateLockOwnerId] = @surrogateLockOwnerId and
		  [Version] = @handleInstanceVersion
	
	exec [System.Activities.DurableInstancing].[InsertRunnableInstanceEntry] @surrogateInstanceId, @workflowHostType, @serviceDeploymentId, @isSuspended, @isReadyToRun, @pendingTimer    
	
	commit transaction
end
go

grant execute on [System.Activities.DurableInstancing].[UnlockInstance] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[DetectRunnableInstances]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[DetectRunnableInstances]
go

create procedure [System.Activities.DurableInstancing].[DetectRunnableInstances]
	@workflowHostType uniqueidentifier
as
begin
	set nocount on
	set transaction isolation level read committed	
	set xact_abort on;	
	set deadlock_priority low
	
	declare @nextRunnableTime datetime

	select top 1 @nextRunnableTime = [RunnableInstancesTable].[RunnableTime]
			  from [RunnableInstancesTable] with (readpast)
			  where [WorkflowHostType] = @workflowHostType
			  order by [WorkflowHostType], [RunnableTime]
			  
	select 0 as 'Result', @nextRunnableTime, getutcdate()
end
go

grant execute on [System.Activities.DurableInstancing].[DetectRunnableInstances] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[GetActivatableWorkflowsActivationParameters]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[GetActivatableWorkflowsActivationParameters]
go

create procedure [System.Activities.DurableInstancing].[GetActivatableWorkflowsActivationParameters]
	@machineName nvarchar(128)
as
begin
	set nocount on
	set transaction isolation level read committed	
	set xact_abort on;	
	set deadlock_priority low
	
	declare @now datetime
	set @now = getutcdate()
	
	select 0 as 'Result'
	
	select top(1000) serviceDeployments.[SiteName], serviceDeployments.[RelativeApplicationPath], serviceDeployments.[RelativeServicePath]
	from (
		select distinct [ServiceDeploymentId], [WorkflowHostType]
		from [RunnableInstancesTable] with (readpast)
		where [RunnableTime] <= @now
		) runnableWorkflows inner join [ServiceDeploymentsTable] serviceDeployments
		on runnableWorkflows.[ServiceDeploymentId] = serviceDeployments.[Id]
	where not exists (
						select top (1) 1
						from [LockOwnersTable] lockOwners
						where lockOwners.[LockExpiration] > @now
						and lockOwners.[MachineName] = @machineName
						and lockOwners.[WorkflowHostType] = runnableWorkflows.[WorkflowHostType]
					  )
end
go

grant execute on [System.Activities.DurableInstancing].[GetActivatableWorkflowsActivationParameters] to [System.Activities.DurableInstancing.InstanceStoreUsers]
go

grant execute on [System.Activities.DurableInstancing].[GetActivatableWorkflowsActivationParameters] to [System.Activities.DurableInstancing.WorkflowActivationUsers]
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[LoadInstance]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[LoadInstance]
go
create procedure [System.Activities.DurableInstancing].[LoadInstance]
	@surrogateLockOwnerId bigint,
	@operationType tinyint,
	@handleInstanceVersion bigint,
	@handleIsBoundToLock bit,
	@keyToLoadBy uniqueidentifier = null,
	@instanceId uniqueidentifier = null,
	@keysToAssociate xml = null,
	@encodingOption tinyint,
	@concatenatedKeyProperties varbinary(max) = null,
	@singleKeyId uniqueidentifier,
	@operationTimeout int
as
begin
	set nocount on
	set transaction isolation level read committed	
	set xact_abort on;		
	set deadlock_priority low
	begin transaction
	
	declare @result int
	declare @lockAcquired bigint
	declare @isInitialized bit
	declare @createKey bit
	declare @createdInstance bit
	declare @keyIsAssociated bit
	declare @loadedByKey bit
	declare @now datetime
	declare @surrogateInstanceId bigint

	set @createdInstance = 0
	set @isInitialized = 0
	set @keyIsAssociated = 0
	set @result = 0
	set @surrogateInstanceId = null
	
	exec @lockAcquired = sp_getapplock @Resource = 'InstanceStoreLock', @LockMode = 'Shared', @LockTimeout = @operationTimeout
	
	if (@lockAcquired < 0)
	begin
		set @result = 13
		select @result as 'Result'
	end
	
	if (@result = 0)
	begin
		set @now = getutcdate()

		if (@operationType = 0) or (@operationType = 2)
		begin
MapKeyToInstanceId:
			set @loadedByKey = 0
			set @createKey = 0
			
			select @surrogateInstanceId = [SurrogateInstanceId],
				   @keyIsAssociated = [IsAssociated]
			from [KeysTable]
			where [Id] = @keyToLoadBy
			
			if (@@rowcount = 0)
			begin
				if (@operationType = 2)
				begin
					set @result = 4
					select @result as 'Result', @keyToLoadBy 
				end
					set @createKey = 1
			end
			else if (@keyIsAssociated = 0)
			begin
				set @result = 8
				select @result as 'Result', @keyToLoadBy
			end
			else
			begin
				select @instanceId = [Id]
				from [InstancesTable]
				where [SurrogateInstanceId] = @surrogateInstanceId

				if (@@rowcount = 0)
					goto MapKeyToInstanceId

				set @loadedByKey = 1
			end
		end
	end

	if (@result = 0)
	begin
LockOrCreateInstance:
		exec [System.Activities.DurableInstancing].[LockInstance] @instanceId, @surrogateLockOwnerId, @handleInstanceVersion, @handleIsBoundToLock, @surrogateInstanceId output, null, @result output
														  
		if (@result = 0 and @surrogateInstanceId = 0)
		begin
			if (@loadedByKey = 1)
				goto MapKeyToInstanceId
			
			if (@operationType > 1)
			begin
				set @result = 1
				select @result as 'Result', @instanceId as 'InstanceId'
			end
			else
			begin				
				exec [System.Activities.DurableInstancing].[CreateInstance] @instanceId, @surrogateLockOwnerId, null, null, @surrogateInstanceId output, @result output
			
				if (@result = 0 and @surrogateInstanceId = 0)
					goto LockOrCreateInstance
				else if (@surrogateInstanceId > 0)
					set @createdInstance = 1
			end
		end
		else if (@result = 0)
		begin
			delete from [RunnableInstancesTable]
			where [SurrogateInstanceId] = @surrogateInstanceId
		end
	end
		
	if (@result = 0)
	begin
		if (@createKey = 1) 
		begin
			select @isInitialized = [IsInitialized]
			from [InstancesTable]
			where [SurrogateInstanceId] = @surrogateInstanceId
			
			if (@isInitialized = 1)
			begin
				set @result = 5
				select @result as 'Result', @instanceId
			end
			else
			begin													
				insert into [KeysTable] ([Id], [SurrogateInstanceId], [IsAssociated])
				values (@keyToLoadBy, @surrogateInstanceId, 1)
				
				if (@@rowcount = 0)
				begin
					if (@createdInstance = 1)
					begin
						delete [InstancesTable]
						where [SurrogateInstanceId] = @surrogateInstanceId
					end
					else
					begin
						update [InstancesTable]
						set [SurrogateLockOwnerId] = null
						where [SurrogateInstanceId] = @surrogateInstanceId
					end
					
					goto MapKeyToInstanceId
				end
			end
		end
		else if (@loadedByKey = 1 and not exists(select [Id] from [KeysTable] where ([Id] = @keyToLoadBy) and ([IsAssociated] = 1)))
		begin
			set @result = 8
			select @result as 'Result', @keyToLoadBy
		end
		
		if (@operationType > 1 and not exists(select [Id] from [InstancesTable] where ([Id] = @instanceId) and ([IsInitialized] = 1)))
		begin
			set @result = 1
			select @result as 'Result', @instanceId as 'InstanceId'
		end
		
		if (@result = 0)
			exec @result = [System.Activities.DurableInstancing].[AssociateKeys] @surrogateInstanceId, @keysToAssociate, @concatenatedKeyProperties, @encodingOption, @singleKeyId
		
		-- Ensure that this key's data will never be overwritten.
		if (@result = 0 and @createKey = 1)
		begin
			update [KeysTable]
			set [EncodingOption] = @encodingOption
			where [Id] = @keyToLoadBy
		end
	end
	
	if (@result != 13)
		exec sp_releaseapplock @Resource = 'InstanceStoreLock'
		
	if (@result = 0)
	begin
		select @result as 'Result',
			   [Id],
			   [SurrogateInstanceId],
			   [PrimitiveDataProperties],
			   [ComplexDataProperties],
			   [MetadataProperties],
			   [DataEncodingOption],
			   [MetadataEncodingOption],
			   [Version],
			   [IsInitialized],
			   @createdInstance
		from [InstancesTable]
		where [SurrogateInstanceId] = @surrogateInstanceId
		
		if (@createdInstance = 0)
		begin
			select @result as 'Result',
				   [EncodingOption],
				   [Change]
			from [InstanceMetadataChangesTable]
			where [SurrogateInstanceId] = @surrogateInstanceId
			order by([ChangeTime])
			
			if (@@rowcount = 0)
			select @result as 'Result', null, null
				
			select @result as 'Result',
				   [Id],
				   [IsAssociated],
				   [EncodingOption],
				   [Properties]
			from [KeysTable] with (index(NCIX_KeysTable_SurrogateInstanceId))
			where ([KeysTable].[SurrogateInstanceId] = @surrogateInstanceId)
			
			if (@@rowcount = 0)
				select @result as 'Result', null, null, null, null
		end

		commit transaction
	end
	else if (@result = 2 or @result = 14)
		commit transaction
	else
		rollback transaction
end
go

grant execute on [System.Activities.DurableInstancing].[LoadInstance] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[TryLoadRunnableInstance]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[TryLoadRunnableInstance]
go

create procedure [System.Activities.DurableInstancing].[TryLoadRunnableInstance]
	@surrogateLockOwnerId bigint,
	@workflowHostType uniqueidentifier,
	@operationType tinyint,
	@handleInstanceVersion bigint,
	@handleIsBoundToLock bit,
	@encodingOption tinyint,	
	@operationTimeout int
as
begin
	set nocount on
	set transaction isolation level read committed	
	set xact_abort on;	
	set deadlock_priority low
	begin tran
	
	declare @instanceId uniqueIdentifier
	declare @now datetime
	set @now = getutcdate()
	
	select top (1) @instanceId = instances.[Id]
	from [RunnableInstancesTable] runnableInstances with (readpast, updlock)
		inner loop join [InstancesTable] instances with (readpast, updlock)
		on runnableInstances.[SurrogateInstanceId] = instances.[SurrogateInstanceId]
	where runnableInstances.[WorkflowHostType] = @workflowHostType
		  and 
	      runnableInstances.[RunnableTime] <= @now
    
    if (@@rowcount = 1)
    begin
		select 0 as 'Result', cast(1 as bit)				
		exec [System.Activities.DurableInstancing].[LoadInstance] @surrogateLockOwnerId, @operationType, @handleInstanceVersion, @handleIsBoundToLock, null, @instanceId, null, @encodingOption, null, null, @operationTimeout
    end	
    else
    begin
		select 0 as 'Result', cast(0 as bit)
    end
    
    if (@@trancount > 0)
    begin
		commit tran
    end
end
go

grant execute on [System.Activities.DurableInstancing].[TryLoadRunnableInstance] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[DeleteInstance]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[DeleteInstance]
go
create procedure [System.Activities.DurableInstancing].[DeleteInstance]
	@surrogateInstanceId bigint = null
as
begin	
	set nocount on
	set transaction isolation level read committed		
	set xact_abort on;	
	
	delete [InstancePromotedPropertiesTable]
	where [SurrogateInstanceId] = @surrogateInstanceId
		
	delete [KeysTable]
	where [SurrogateInstanceId] = @surrogateInstanceId
		
	delete [InstanceMetadataChangesTable]
	where [SurrogateInstanceId] = @surrogateInstanceId

	delete [RunnableInstancesTable] 
	where [SurrogateInstanceId] = @surrogateInstanceId

	delete [InstancesTable] 
	where [SurrogateInstanceId] = @surrogateInstanceId

end
go

grant execute on [System.Activities.DurableInstancing].[DeleteInstance] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

if exists (select * from sys.triggers where object_id = OBJECT_ID(N'[System.Activities.DurableInstancing].[DeleteInstanceTrigger]'))
	drop trigger [System.Activities.DurableInstancing].[DeleteInstanceTrigger]
go

create trigger [System.Activities.DurableInstancing].[DeleteInstanceTrigger] on [System.Activities.DurableInstancing].[Instances]
instead of delete
as
begin	
	if (@@rowcount = 0)
		return
		
	set nocount on
	set transaction isolation level read committed		
	set xact_abort on;	
	
	declare @surrogateInstanceIds table ([SurrogateInstanceId] bigint primary key)		
	
	insert into @surrogateInstanceIds
	select [SurrogateInstanceId]
	from deleted as [DeletedInstances]
	join [InstancesTable] on [InstancesTable].[Id] = [DeletedInstances].[InstanceId]
	
	delete [InstancePromotedPropertiesTable]
	from @surrogateInstanceIds as [InstancesToDelete]
	inner merge join [System.Activities.DurableInstancing].[InstancePromotedPropertiesTable] on [InstancePromotedPropertiesTable].[SurrogateInstanceId] = [InstancesToDelete].[SurrogateInstanceId]
	
	delete [KeysTable]
	from @surrogateInstanceIds as [InstancesToDelete]
	inner loop join [System.Activities.DurableInstancing].[KeysTable] on [KeysTable].[SurrogateInstanceId] = [InstancesToDelete].[SurrogateInstanceId]
	
	delete from [InstanceMetadataChangesTable]
	from @surrogateInstanceIds as [InstancesToDelete]
	inner merge join [System.Activities.DurableInstancing].[InstanceMetadataChangesTable] on [InstanceMetadataChangesTable].[SurrogateInstanceId] = [InstancesToDelete].[SurrogateInstanceId]
	
	delete [RunnableInstancesTable]
	from @surrogateInstanceIds as [InstancesToDelete]
	inner loop join [System.Activities.DurableInstancing].[RunnableInstancesTable] on [RunnableInstancesTable].[SurrogateInstanceId] = [InstancesToDelete].[SurrogateInstanceId]
	
	delete [InstancesTable]
	from @surrogateInstanceIds as [InstancesToDelete]
	inner merge join [System.Activities.DurableInstancing].[InstancesTable] on [InstancesTable].[SurrogateInstanceId] = [InstancesToDelete].[SurrogateInstanceId]
end
go	

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[CreateServiceDeployment]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[CreateServiceDeployment]
go
create procedure [System.Activities.DurableInstancing].[CreateServiceDeployment]	
	@serviceDeploymentHash uniqueIdentifier,
	@siteName nvarchar(max),
	@relativeServicePath nvarchar(max),
	@relativeApplicationPath nvarchar(max),
	@serviceName nvarchar(max),
    @serviceNamespace nvarchar(max),
    @serviceDeploymentId bigint output
as
begin
	set nocount on
	set transaction isolation level read committed		
	set xact_abort on;	
	
		--Create or select the service deployment id
		insert into [ServiceDeploymentsTable]
			([ServiceDeploymentHash], [SiteName], [RelativeServicePath], [RelativeApplicationPath], [ServiceName], [ServiceNamespace])
			values (@serviceDeploymentHash, @siteName, @relativeServicePath, @relativeApplicationPath, @serviceName, @serviceNamespace)
			
		if (@@rowcount = 0)
		begin		
			select @serviceDeploymentId = [Id]
			from [ServiceDeploymentsTable]
			where [ServiceDeploymentHash] = @serviceDeploymentHash		
		end
		else			
		begin
			set @serviceDeploymentId = scope_identity()		
		end	
		
		select 0 as 'Result', @serviceDeploymentId		
end	
go

grant execute on [System.Activities.DurableInstancing].[CreateServiceDeployment] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[SaveInstance]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[SaveInstance]
go
create procedure [System.Activities.DurableInstancing].[SaveInstance]
	@instanceId uniqueidentifier,
	@surrogateLockOwnerId bigint,
	@handleInstanceVersion bigint,
	@handleIsBoundToLock bit,
	@primitiveDataProperties varbinary(max),
	@complexDataProperties varbinary(max),
	@writeOnlyPrimitiveDataProperties varbinary(max),
	@writeOnlyComplexDataProperties varbinary(max),
	@metadataProperties varbinary(max),
	@metadataIsConsistent bit,
	@encodingOption tinyint,
	@timerDurationMilliseconds bigint,
	@suspensionStateChange tinyint,
	@suspensionReason nvarchar(max),
	@suspensionExceptionName nvarchar(450),
	@keysToAssociate xml,
	@keysToComplete xml,
	@keysToFree xml,
	@concatenatedKeyProperties varbinary(max),
	@unlockInstance bit,
	@isReadyToRun bit,
	@isCompleted bit,
	@singleKeyId uniqueidentifier,
	@lastMachineRunOn nvarchar(450),
	@executionStatus nvarchar(450),
	@blockingBookmarks nvarchar(max),
	@workflowHostType uniqueidentifier,
	@serviceDeploymentId bigint,
	@operationTimeout int
as
begin
	set nocount on
	set transaction isolation level read committed		
	set xact_abort on;	

	declare @currentInstanceVersion bigint
	declare @deleteInstanceOnCompletion bit
	declare @enqueueCommand bit
	declare @isSuspended bit
	declare @lockAcquired bigint
	declare @metadataUpdateOnly bit
	declare @now datetime
	declare @result int
	declare @surrogateInstanceId bigint
	declare @pendingTimer datetime
	
	set @result = 0
	set @metadataUpdateOnly = 0
	
	exec @lockAcquired = sp_getapplock @Resource = 'InstanceStoreLock', @LockMode = 'Shared', @LockTimeout = @operationTimeout
		
	if (@lockAcquired < 0)
	begin
		select @result as 'Result'
		set @result = 13
	end
	
	set @now = getutcdate()
	
	if (@primitiveDataProperties is null and @complexDataProperties is null and @writeOnlyPrimitiveDataProperties is null and @writeOnlyComplexDataProperties is null)
		set @metadataUpdateOnly = 1

LockOrCreateInstance:
	if (@result = 0)
	begin
		exec [System.Activities.DurableInstancing].[LockInstance] @instanceId, @surrogateLockOwnerId, @handleInstanceVersion, @handleIsBoundToLock, @surrogateInstanceId output, @currentInstanceVersion output, @result output
															  
		if (@result = 0 and @surrogateInstanceId = 0)
		begin
			exec [System.Activities.DurableInstancing].[CreateInstance] @instanceId, @surrogateLockOwnerId, @workflowHostType, @serviceDeploymentId, @surrogateInstanceId output, @result output
			
			if (@result = 0 and @surrogateInstanceId = 0)
				goto LockOrCreateInstance
			
			set @currentInstanceVersion = 1
		end
	end
	
	if (@result = 0)
	begin
		select @enqueueCommand = [EnqueueCommand],
			   @deleteInstanceOnCompletion = [DeletesInstanceOnCompletion]
		from [LockOwnersTable]
		where ([SurrogateLockOwnerId] = @surrogateLockOwnerId)
		
		if (@isCompleted = 1 and @deleteInstanceOnCompletion = 1)
		begin
			exec [System.Activities.DurableInstancing].[DeleteInstance] @surrogateInstanceId
			goto Finally
		end
		
		update [InstancesTable] 
		set @instanceId = [InstancesTable].[Id],
			@workflowHostType = [WorkflowHostType] = 
					case when (@workflowHostType is null)
						then [WorkflowHostType]
						else @workflowHostType 
					end,
			@serviceDeploymentId = [ServiceDeploymentId] = 
					case when (@serviceDeploymentId is null)
						then [ServiceDeploymentId]
						else @serviceDeploymentId 
					end,
			@pendingTimer = [PendingTimer] = 
					case when (@metadataUpdateOnly = 1)
						then [PendingTimer]
						else [System.Activities.DurableInstancing].[GetExpirationTime](@timerDurationMilliseconds)
					end,
			@isReadyToRun = [IsReadyToRun] = 
					case when (@metadataUpdateOnly = 1)
						then [IsReadyToRun]
						else @isReadyToRun
					end,
			@isSuspended = [IsSuspended] = 
					case when (@suspensionStateChange = 0) then [IsSuspended]
						 when (@suspensionStateChange = 1) then 1
						 else 0
					end,
			[SurrogateLockOwnerId] = case when (@unlockInstance = 1 or @isCompleted = 1)
										then null
										else @surrogateLockOwnerId
									 end,
			[PrimitiveDataProperties] = case when (@metadataUpdateOnly = 1)
										then [PrimitiveDataProperties]
										else @primitiveDataProperties
									   end,
			[ComplexDataProperties] = case when (@metadataUpdateOnly = 1)
										then [ComplexDataProperties]
										else @complexDataProperties
									   end,
			[WriteOnlyPrimitiveDataProperties] = case when (@metadataUpdateOnly = 1)
										then [WriteOnlyPrimitiveDataProperties]
										else @writeOnlyPrimitiveDataProperties
									   end,
			[WriteOnlyComplexDataProperties] = case when (@metadataUpdateOnly = 1)
										then [WriteOnlyComplexDataProperties]
										else @writeOnlyComplexDataProperties
									   end,
			[MetadataProperties] = case
									 when (@metadataIsConsistent = 1) then @metadataProperties
									 else [MetadataProperties]
								   end,
			[SuspensionReason] = case
									when (@suspensionStateChange = 0) then [SuspensionReason]
									when (@suspensionStateChange = 1) then @suspensionReason
									else null
								 end,
			[SuspensionExceptionName] = case
									when (@suspensionStateChange = 0) then [SuspensionExceptionName]
									when (@suspensionStateChange = 1) then @suspensionExceptionName
									else null
								 end,
			[IsCompleted] = @isCompleted,
			[IsInitialized] = case
								when (@metadataUpdateOnly = 0) then 1
								else [IsInitialized]
							  end,
			[DataEncodingOption] = case
									when (@metadataUpdateOnly = 0) then @encodingOption
									else [DataEncodingOption]
								   end,
			[MetadataEncodingOption] = case
									when (@metadataIsConsistent = 1) then @encodingOption
									else [MetadataEncodingOption]
								   end,
			[BlockingBookmarks] = case
									when (@metadataUpdateOnly = 0) then @blockingBookmarks
									else [BlockingBookmarks]
								  end,
			[LastUpdated] = @now,
			[LastMachineRunOn] = case
									when (@metadataUpdateOnly = 0) then @lastMachineRunOn
									else [LastMachineRunOn]
								 end,
			[ExecutionStatus] = case
									when (@metadataUpdateOnly = 0) then @executionStatus
									else [ExecutionStatus]
								end
		from [InstancesTable]		
		where ([InstancesTable].[SurrogateInstanceId] = @surrogateInstanceId)
	
		if (@@rowcount = 0)
		begin
			set @result = 99
			select @result as 'Result' 
		end
		else
		begin
			if (@keysToAssociate is not null or @singleKeyId is not null)
				exec @result = [System.Activities.DurableInstancing].[AssociateKeys] @surrogateInstanceId, @keysToAssociate, @concatenatedKeyProperties, @encodingOption, @singleKeyId
			
			if (@result = 0 and @keysToComplete is not null)
				exec @result = [System.Activities.DurableInstancing].[CompleteKeys] @surrogateInstanceId, @keysToComplete
			
			if (@result = 0 and @keysToFree is not null)
				exec @result = [System.Activities.DurableInstancing].[FreeKeys] @surrogateInstanceId, @keysToFree
			
			if (@result = 0) and (@metadataUpdateOnly = 0)
			begin
				delete from [InstancePromotedPropertiesTable]
				where [SurrogateInstanceId] = @surrogateInstanceId
			end
			
			if (@result = 0)
			begin
				if (@metadataIsConsistent = 1)
				begin
					delete from [InstanceMetadataChangesTable]
					where [SurrogateInstanceId] = @surrogateInstanceId
				end
				else if (@metadataProperties is not null)
				begin
					insert into [InstanceMetadataChangesTable] ([SurrogateInstanceId], [EncodingOption], [Change])
					values (@surrogateInstanceId, @encodingOption, @metadataProperties)
				end
			end
			
			if (@result = 0 and @unlockInstance = 1 and @isCompleted = 0)
				exec [System.Activities.DurableInstancing].[InsertRunnableInstanceEntry] @surrogateInstanceId, @workflowHostType, @serviceDeploymentId, @isSuspended, @isReadyToRun, @pendingTimer				
		end
	end

Finally:
	if (@result != 13)
		exec sp_releaseapplock @Resource = 'InstanceStoreLock'
	
	if (@result = 0)
		select @result as 'Result', @currentInstanceVersion

	return @result
end
go

grant execute on [System.Activities.DurableInstancing].[SaveInstance] to [System.Activities.DurableInstancing.InstanceStoreUsers] 
go

if exists (select * from sys.triggers where object_id = OBJECT_ID(N'[System.Activities.DurableInstancing].[DeleteServiceDeploymentTrigger]'))
	drop trigger [System.Activities.DurableInstancing].[DeleteServiceDeploymentTrigger]
go

create trigger [System.Activities.DurableInstancing].[DeleteServiceDeploymentTrigger] on [System.Activities.DurableInstancing].[ServiceDeployments]
instead of delete
as
begin	
	if (@@rowcount = 0)
		return	
		
	set nocount on
	set transaction isolation level read committed		
	set xact_abort on;	
	
	declare @lockAcquired bigint
	declare @candidateDeploymentIdsPass1 table([Id] bigint primary key)
	
	exec @lockAcquired = sp_getapplock @Resource = 'InstanceStoreLock', @LockMode = 'Exclusive', @LockTimeout = 25000
	
	if (@lockAcquired < 0)
		return

	insert into @candidateDeploymentIdsPass1
	select [ServiceDeploymentId] from deleted
	except
	select [ServiceDeploymentId] from [InstancesTable]
	
	delete [ServiceDeploymentsTable]
	from [ServiceDeploymentsTable]
	join @candidateDeploymentIdsPass1 as [OrphanedIds] on [OrphanedIds].[Id] = [ServiceDeploymentsTable].[Id]
	
	exec sp_releaseapplock @Resource = 'InstanceStoreLock'
end
go	

if exists (select * from sys.objects where object_id = object_id(N'[System.Activities.DurableInstancing].[InsertPromotedProperties]') and type in (N'P', N'PC'))
	drop procedure [System.Activities.DurableInstancing].[InsertPromotedProperties]
go

create procedure [System.Activities.DurableInstancing].[InsertPromotedProperties]
	@instanceId uniqueidentifier,
	@promotionName nvarchar(400),
	@value1 sql_variant = null,
	@value2 sql_variant = null,
	@value3 sql_variant = null,
	@value4 sql_variant = null,
	@value5 sql_variant = null,
	@value6 sql_variant = null,
	@value7 sql_variant = null,
	@value8 sql_variant = null,
	@value9 sql_variant = null,
	@value10 sql_variant = null,
	@value11 sql_variant = null,
	@value12 sql_variant = null,
	@value13 sql_variant = null,
	@value14 sql_variant = null,
	@value15 sql_variant = null,
	@value16 sql_variant = null,
	@value17 sql_variant = null,
	@value18 sql_variant = null,
	@value19 sql_variant = null,
	@value20 sql_variant = null,
	@value21 sql_variant = null,
	@value22 sql_variant = null,
	@value23 sql_variant = null,
	@value24 sql_variant = null,
	@value25 sql_variant = null,
	@value26 sql_variant = null,
	@value27 sql_variant = null,
	@value28 sql_variant = null,
	@value29 sql_variant = null,
	@value30 sql_variant = null,
	@value31 sql_variant = null,
	@value32 sql_variant = null,
	@value33 varbinary(max) = null,
	@value34 varbinary(max) = null,
	@value35 varbinary(max) = null,
	@value36 varbinary(max) = null,
	@value37 varbinary(max) = null,
	@value38 varbinary(max) = null,
	@value39 varbinary(max) = null,
	@value40 varbinary(max) = null,
	@value41 varbinary(max) = null,
	@value42 varbinary(max) = null,
	@value43 varbinary(max) = null,
	@value44 varbinary(max) = null,
	@value45 varbinary(max) = null,
	@value46 varbinary(max) = null,
	@value47 varbinary(max) = null,
	@value48 varbinary(max) = null,
	@value49 varbinary(max) = null,
	@value50 varbinary(max) = null,
	@value51 varbinary(max) = null,
	@value52 varbinary(max) = null,
	@value53 varbinary(max) = null,
	@value54 varbinary(max) = null,
	@value55 varbinary(max) = null,
	@value56 varbinary(max) = null,
	@value57 varbinary(max) = null,
	@value58 varbinary(max) = null,
	@value59 varbinary(max) = null,
	@value60 varbinary(max) = null,
	@value61 varbinary(max) = null,
	@value62 varbinary(max) = null,
	@value63 varbinary(max) = null,
	@value64 varbinary(max) = null
as
begin
	set nocount on
	set transaction isolation level read committed		
	set xact_abort on;	

	declare @surrogateInstanceId bigint

	select @surrogateInstanceId = [SurrogateInstanceId]
	from [InstancesTable]
	where [Id] = @instanceId

	insert into [System.Activities.DurableInstancing].[InstancePromotedPropertiesTable]
	values (@surrogateInstanceId, @promotionName, @value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8,
			@value9, @value10, @value11, @value12, @value13, @value14, @value15, @value16, @value17, @value18, @value19,
			@value20, @value21, @value22, @value23, @value24, @value25, @value26, @value27, @value28, @value29, @value30,
			@value31, @value32, @value33, @value34, @value35, @value36, @value37, @value38, @value39, @value40, @value41,
			@value42, @value43, @value44, @value45, @value46, @value47, @value48, @value49, @value50, @value51, @value52,
			@value53, @value54, @value55, @value56, @value57, @value58, @value59, @value60, @value61, @value62, @value63,
			@value64)
end
go

grant execute on [System.Activities.DurableInstancing].[InsertPromotedProperties] to [System.Activities.DurableInstancing.InstanceStoreUsers]
go

/* WWF LOGIC 4.0 ( END ) */

