using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Work {

    [Serializable]
    public class WorkTeam : CoreConfigurationObject {

        #region Private Properties

        private Server.Application.WorkTeamType workTeamType = Server.Application.WorkTeamType.WorkTeam;

        private List<Server.Application.WorkTeamMembership> membership = new List<Server.Application.WorkTeamMembership> ();

        #endregion


        #region Public Properties

        public Server.Application.WorkTeamType WorkTeamType { get { return workTeamType; } set { workTeamType = value; } }

        public List<Server.Application.WorkTeamMembership> Membership { get { return membership; } set { membership = value; } }

        #endregion


        #region Constructors

        public WorkTeam (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public WorkTeam (Application applicationReference, Server.Application.WorkTeam serverWorkTeam) {

            BaseConstructor (applicationReference, serverWorkTeam);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.WorkTeam serverWorkTeam) {

            base.BaseConstructor (applicationReference, serverWorkTeam);


            workTeamType = serverWorkTeam.WorkTeamType;


            // MAKE COPY OF COLLECTION, NOT DIRECT ASSIGNMENT

            membership = new List<Server.Application.WorkTeamMembership> ();

            foreach (Server.Application.WorkTeamMembership currentServerMembership in serverWorkTeam.Membership) {

                membership.Add (application.CopyWorkTeamMembership (currentServerMembership));

            }

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.WorkTeam serverWorkTeam) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverWorkTeam);


            serverWorkTeam.WorkTeamType = workTeamType;


            // COPY, DON'T REFERENCE            

            serverWorkTeam.Membership = new Server.Application.WorkTeamMembership[membership.Count];

            Int64 currentMembershipIndex = 0;

            foreach (Server.Application.WorkTeamMembership currentMembership in membership) {

                serverWorkTeam.Membership[currentMembershipIndex] = application.CopyWorkTeamMembership (currentMembership);

                currentMembershipIndex = currentMembershipIndex + 1;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.WorkTeam serverWorkTeam = new Server.Application.WorkTeam ();

            MapToServerObject (serverWorkTeam);

            return serverWorkTeam;

        }

        public WorkTeam Copy () {

            Server.Application.WorkTeam serverWorkTeam = (Server.Application.WorkTeam)ToServerObject ();

            WorkTeam copiedWorkTeam = new WorkTeam (application, serverWorkTeam);

            return copiedWorkTeam;


        }

        public Boolean IsEqual (WorkTeam compareWorkTeam) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareWorkTeam);


            isEqual &= (workTeamType == compareWorkTeam.WorkTeamType);

            isEqual &= (membership.Count == compareWorkTeam.Membership.Count);


            if (isEqual) {

                // CYCLE THROUGH ALL MEMBERSHIP, COMPARING ASSIGNED PERMISSIONS

                foreach (Server.Application.WorkTeamMembership currentMembership in membership) {

                    if (compareWorkTeam.ContainsMembership (currentMembership.SecurityAuthorityId, currentMembership.UserAccountId)) {

                        Server.Application.WorkTeamMembership compareMembership = compareWorkTeam.MembershipGet (currentMembership.SecurityAuthorityId, currentMembership.UserAccountId);

                        if (compareMembership == null) { isEqual = false; break; }


                        isEqual &= (currentMembership.WorkTeamRole == compareMembership.WorkTeamRole);

                        if (!isEqual) { break; }

                    }

                    else { isEqual = false; break; }

                }

            }


            return isEqual;

        }



        public Boolean ContainsMembership (Int64 securityAuthorityId, String userAccountId) {

            Boolean exists = false;

            foreach (Server.Application.WorkTeamMembership currentMembership in membership) {

                if ((currentMembership.SecurityAuthorityId == securityAuthorityId) && (currentMembership.UserAccountId == userAccountId)) {

                    exists = true;

                    break;

                }

            }

            return exists;

        }

        public void AddMembership (Int64 securityAuthorityId, String securityAuthorityName, String userAccountId, String userAccountName, String userDisplayName, Server.Application.WorkTeamRole workTeamRole) {

            if (!ContainsMembership (securityAuthorityId, userAccountId)) {

                Server.Application.WorkTeamMembership newMembership = new Server.Application.WorkTeamMembership ();

                newMembership.WorkTeamId = Id;

                newMembership.SecurityAuthorityId = securityAuthorityId;

                newMembership.SecurityAuthorityName = securityAuthorityName;

                newMembership.UserAccountId = userAccountId;

                newMembership.UserAccountName = userAccountName;

                newMembership.UserDisplayName = userDisplayName;

                newMembership.WorkTeamRole = workTeamRole;

                newMembership.CreateAccountInfo = this.createAccountInfo;

                newMembership.ModifiedAccountInfo = this.modifiedAccountInfo;

                membership.Add (newMembership);

            }

            return;

        }
        
        public Server.Application.WorkTeamMembership MembershipGet (Int64 securityAuthorityId, String userAccountId) {

            Server.Application.WorkTeamMembership foundMembership = null;

            foreach (Server.Application.WorkTeamMembership currentMembership in membership) {

                if ((currentMembership.SecurityAuthorityId == securityAuthorityId) && (currentMembership.UserAccountId == userAccountId)) {

                    foundMembership = currentMembership;

                    break;

                }

            }

            return foundMembership;

        }

        public Server.Application.WorkTeamMembership MembershipGetForSession () {

            Server.Application.WorkTeamMembership foundMembership = null;

            foreach (Server.Application.WorkTeamMembership currentMembership in membership) {

                if ((currentMembership.SecurityAuthorityId == application.Session.SecurityAuthorityId) && (currentMembership.UserAccountId == application.Session.UserAccountId)) {

                    foundMembership = currentMembership;

                    break;

                }

            }

            return foundMembership;

        }

        #endregion

    }

}
