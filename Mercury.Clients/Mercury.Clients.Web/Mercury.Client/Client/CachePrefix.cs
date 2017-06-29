using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client {

    public class CachePrefix {

        public const String SecurityAuthorityDictionary = "Application.SecurityAuthorityDictionary";

        public const String SecurityAuthority = "Application.SecurityAuthority.";

        public const String SecurityAuthorityByName = "Application.SecurityAuthority.ByName.";


        public const String EnvironmentDictionary = "Application.EnvironmentDictionary";


        public const String Entity = "Application.Core.Entity.";

        public const String EntityAddress = "Application.Core.Entity.EntityAddress."; // SINGLE ENTITY ADDRESS

        public const String EntityAddresses = "Application.Core.Entity.EntityAddresses."; // COLLECTION ENTITY ADDRESS


        public const String EntityContactInformation = "Application.Core.Entity.EntityContactInformation."; // SINGLE ENTITY CONACT INFORMATION

        public const String EntityContactInformations = "Application.Core.Entity.EntityContactInformations."; // COLLECTION ENTITY CONTACT INFORMATION

    
        public const String Member = "Application.Core.Member.";

        public const String MemberDemographics = "Application.Core.Member.Demographics.";


        public const String MemberEnrollment = "Application.Core.Member.Enrollment."; // SINGLE ENROLLMENT 

        public const String MemberEnrollments = "Application.Core.Member.Enrollments."; // COLLECTION OF ENROLLMENTS

        public const String MemberEnrollmentCoverage = "Application.Core.Member.Enrollment.EnrollmentCoverage."; // SINGLE COVERAGE RECORD

        public const String MemberEnrollmentCoverages = "Application.Core.Member.Enrollment.EnrollmentCoverages."; // COVERAGE COLLECTION

        public const String MemberEnrollmentPcp = "Application.Core.Member.Enrollment.Pcp."; // SINGLE PCP ASSIGNMENT

        public const String MemberEnrollmentPcps = "Application.Core.Member.Enrollment.Pcp."; // PCP ASSIGNMENT COVERAGES

        public const String MemberEnrollmentTplCob = "Application.Core.Member.Enrollment.TplCob."; // SINGLE ENROLLMENT 

        public const String MemberEnrollmentTplCobs = "Application.Core.Member.Enrollments.TplCob."; // COLLECTION OF ENROLLMENTS


        public const String MemberRelationship = "Application.Core.Member.MemberRelationship."; // SINGLE RELATIONSHIP

        public const String MemberRelationships = "Application.Core.Member.MemberRelationships."; // COLLECTION OF RELATIONSHIPS


        public const String WorkOutcome = "Application.Core.Work.WorkOutcome.";

        public const String WorkQueueItemsGetByViewPage = "Application.Core.Work.WorkQueueItems.ByViewPage."; // COLLECTION

    }

}
