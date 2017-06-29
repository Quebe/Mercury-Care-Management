using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Mercury.Client.Converters {

    public class ServerCollectionToClient {

        #region Entity

        public static ObservableCollection<Client.Core.Entity.EntityAddress> EntityAddressCollection (Application mercuryApplication, ObservableCollection<Server.Application.EntityAddress> serverEntityAddresses) {

            ObservableCollection<Client.Core.Entity.EntityAddress> clientEntityAddresses = new ObservableCollection<Mercury.Client.Core.Entity.EntityAddress> ();


            foreach (Server.Application.EntityAddress currentServerEntityAddress in serverEntityAddresses) {

                Client.Core.Entity.EntityAddress entityAddress = new Mercury.Client.Core.Entity.EntityAddress (mercuryApplication, currentServerEntityAddress);

                clientEntityAddresses.Add (entityAddress);

            }


            return clientEntityAddresses;

        }

        public static ObservableCollection<Client.Core.Entity.EntityContactInformation> EntityContactInformationCollection (Application mercuryApplication, ObservableCollection<Server.Application.EntityContactInformation> serverEntityContactInformations) {

            ObservableCollection<Client.Core.Entity.EntityContactInformation> clientEntityContactInformations = new ObservableCollection<Mercury.Client.Core.Entity.EntityContactInformation> ();


            foreach (Server.Application.EntityContactInformation currentServerEntityContactInformation in serverEntityContactInformations) {

                Client.Core.Entity.EntityContactInformation entityContactInformation = new Mercury.Client.Core.Entity.EntityContactInformation (mercuryApplication, currentServerEntityContactInformation);

                clientEntityContactInformations.Add (entityContactInformation);

            }


            return clientEntityContactInformations;

        }

        #endregion 


        #region Member

        public static ObservableCollection<Client.Core.Member.MemberEnrollment> MemberEnrollmentCollection (Application mercuryApplication, ObservableCollection<Server.Application.MemberEnrollment> serverMemberEnrollments) {

            ObservableCollection<Client.Core.Member.MemberEnrollment> clientMemberEnrollments = new ObservableCollection<Mercury.Client.Core.Member.MemberEnrollment> ();


            foreach (Server.Application.MemberEnrollment currentServerMemberEnrollment in serverMemberEnrollments) {

                Client.Core.Member.MemberEnrollment memberEnrollment = new Mercury.Client.Core.Member.MemberEnrollment (mercuryApplication, currentServerMemberEnrollment);

                clientMemberEnrollments.Add (memberEnrollment);

            }


            return clientMemberEnrollments;

        }

        public static ObservableCollection<Client.Core.Member.MemberEnrollmentCoverage> MemberEnrollmentCoverageCollection (Application mercuryApplication, ObservableCollection<Server.Application.MemberEnrollmentCoverage> serverMemberEnrollmentCoverages) {

            ObservableCollection<Client.Core.Member.MemberEnrollmentCoverage> clientMemberEnrollmentCoverages = new ObservableCollection<Mercury.Client.Core.Member.MemberEnrollmentCoverage> ();


            foreach (Server.Application.MemberEnrollmentCoverage currentServerMemberEnrollmentCoverage in serverMemberEnrollmentCoverages) {

                Client.Core.Member.MemberEnrollmentCoverage memberEnrollmentCoverage = new Mercury.Client.Core.Member.MemberEnrollmentCoverage (mercuryApplication, currentServerMemberEnrollmentCoverage);

                clientMemberEnrollmentCoverages.Add (memberEnrollmentCoverage);

            }


            return clientMemberEnrollmentCoverages;

        }

        public static ObservableCollection<Client.Core.Member.MemberEnrollmentPcp> MemberEnrollmentPcpCollection (Application mercuryApplication, ObservableCollection<Server.Application.MemberEnrollmentPcp> serverMemberEnrollmentPcps) {

            ObservableCollection<Client.Core.Member.MemberEnrollmentPcp> clientMemberEnrollmentPcps = new ObservableCollection<Mercury.Client.Core.Member.MemberEnrollmentPcp> ();


            foreach (Server.Application.MemberEnrollmentPcp currentServerMemberEnrollmentPcp in serverMemberEnrollmentPcps) {

                Client.Core.Member.MemberEnrollmentPcp memberEnrollmentPcp = new Mercury.Client.Core.Member.MemberEnrollmentPcp (mercuryApplication, currentServerMemberEnrollmentPcp);

                clientMemberEnrollmentPcps.Add (memberEnrollmentPcp);

            }


            return clientMemberEnrollmentPcps;

        }

        public static ObservableCollection<Client.Core.Member.MemberRelationship> MemberRelationshipCollection (Application mercuryApplication, ObservableCollection<Server.Application.MemberRelationship> serverMemberRelationships) {

            ObservableCollection<Client.Core.Member.MemberRelationship> clientMemberRelationships = new ObservableCollection<Mercury.Client.Core.Member.MemberRelationship> ();


            foreach (Server.Application.MemberRelationship currentServerMemberRelationship in serverMemberRelationships) {

                Client.Core.Member.MemberRelationship memberRelationship = new Mercury.Client.Core.Member.MemberRelationship (mercuryApplication, currentServerMemberRelationship);

                clientMemberRelationships.Add (memberRelationship);

            }


            return clientMemberRelationships;

        }

        #endregion 


        #region Work

        //public static ObservableCollection<Client.Core.MedicalServices.MemberService> MemberServiceCollection (Application mercuryApplication, ObservableCollection<Server.Application.MemberService> serverMemberServices) {

        //    ObservableCollection<Client.Core.MedicalServices.MemberService> clientMemberServices = new ObservableCollection<Mercury.Client.Core.MedicalServices.MemberService> ();


        //    foreach (Server.Application.MemberService currentServerMemberService in serverMemberServices) {

        //        Client.Core.MedicalServices.MemberService workQueueItem = new Mercury.Client.Core.MedicalServices.MemberService (mercuryApplication, currentServerMemberService);

        //        clientMemberServices.Add (workQueueItem);

        //    }


        //    return clientMemberServices;

        //}

        //public static ObservableCollection<Client.Core.MedicalServices.MemberServiceDetailSingleton> MemberServiceDetailSingletonCollection (Application mercuryApplication, ObservableCollection<Server.Application.MemberServiceDetailSingleton> serverMemberServiceDetailSingletons) {

        //    ObservableCollection<Client.Core.MedicalServices.MemberServiceDetailSingleton> clientMemberServiceDetailSingletons = new ObservableCollection<Mercury.Client.Core.MedicalServices.MemberServiceDetailSingleton> ();


        //    foreach (Server.Application.MemberServiceDetailSingleton currentServerMemberServiceDetailSingleton in serverMemberServiceDetailSingletons) {

        //        Client.Core.MedicalServices.MemberServiceDetailSingleton detail = new Mercury.Client.Core.MedicalServices.MemberServiceDetailSingleton (mercuryApplication, currentServerMemberServiceDetailSingleton);

        //        clientMemberServiceDetailSingletons.Add (detail);

        //    }


        //    return clientMemberServiceDetailSingletons;

        //}

        public static ObservableCollection<Client.Core.Work.WorkQueue> WorkQueueCollection (Application mercuryApplication, ObservableCollection<Server.Application.WorkQueue> serverWorkQueues) {

            ObservableCollection<Client.Core.Work.WorkQueue> clientWorkQueues = new ObservableCollection<Mercury.Client.Core.Work.WorkQueue> ();


            foreach (Server.Application.WorkQueue currentServerWorkQueue in serverWorkQueues) {

                Client.Core.Work.WorkQueue workQueue = new Mercury.Client.Core.Work.WorkQueue (mercuryApplication, currentServerWorkQueue);

                clientWorkQueues.Add (workQueue);

            }


            return clientWorkQueues;

        }

        public static ObservableCollection<Client.Core.Work.WorkQueueItem> WorkQueueItemCollection (Application mercuryApplication, ObservableCollection<Server.Application.WorkQueueItem> serverWorkQueueItems) {

            ObservableCollection<Client.Core.Work.WorkQueueItem> clientWorkQueueItems = new ObservableCollection<Mercury.Client.Core.Work.WorkQueueItem> ();


            foreach (Server.Application.WorkQueueItem currentServerWorkQueueItem in serverWorkQueueItems) {

                Client.Core.Work.WorkQueueItem workQueueItem = new Mercury.Client.Core.Work.WorkQueueItem (mercuryApplication, currentServerWorkQueueItem);

                clientWorkQueueItems.Add (workQueueItem);

            }


            return clientWorkQueueItems;

        }

        public static ObservableCollection<Client.Core.Work.WorkQueueItemSender> WorkQueueItemSenderCollection (Application mercuryApplication, ObservableCollection<Server.Application.WorkQueueItemSender> serverWorkQueueItemSenders) {

            ObservableCollection<Client.Core.Work.WorkQueueItemSender> clientWorkQueueItemSenders = new ObservableCollection<Mercury.Client.Core.Work.WorkQueueItemSender> ();


            foreach (Server.Application.WorkQueueItemSender currentServerWorkQueueItemSender in serverWorkQueueItemSenders) {

                Client.Core.Work.WorkQueueItemSender workQueueItemSender = new Mercury.Client.Core.Work.WorkQueueItemSender (mercuryApplication, currentServerWorkQueueItemSender);

                clientWorkQueueItemSenders.Add (workQueueItemSender);

            }


            return clientWorkQueueItemSenders;

        }

        //public static ObservableCollection<Client.Core.Work.WorkTeam> WorkTeamCollection (Application mercuryApplication, ObservableCollection<Server.Application.WorkTeam> serverWorkTeams) {

        //    ObservableCollection<Client.Core.Work.WorkTeam> clientWorkTeams = new ObservableCollection<Mercury.Client.Core.Work.WorkTeam> ();


        //    foreach (Server.Application.WorkTeam currentServerWorkTeam in serverWorkTeams) {

        //        Client.Core.Work.WorkTeam workTeam = new Mercury.Client.Core.Work.WorkTeam (mercuryApplication, currentServerWorkTeam);

        //        clientWorkTeams.Add (workTeam);

        //    }


        //    return clientWorkTeams;

        //}

        #endregion 

    }

}
