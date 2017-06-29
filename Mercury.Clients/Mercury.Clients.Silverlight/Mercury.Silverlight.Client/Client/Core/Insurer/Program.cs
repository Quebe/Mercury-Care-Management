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

namespace Mercury.Client.Core.Insurer {

    public class Program : CoreObject {

        #region Private Properties

        private Int64 insurerId = 0;

        private Int64 insuranceTypeId = 0;

        private Int64 bankAccountId = 0;


        private Insurer insurer = null;

        private Server.Application.InsuranceType insuranceType = null;

        #endregion


        #region Public Properties

        public Int64 InsurerId { get { return insurerId; } set { insurerId = value; } }

        public Int64 InsuranceTypeId { get { return insuranceTypeId; } set { insuranceTypeId = value; } }

        public Int64 BankAccountId { get { return bankAccountId; } set { bankAccountId = value; } }

        #endregion


        #region Public Object Properties

        public Insurer Insurer {

            get {

                if ((insurer == null) && (!serverRequests.Contains ("Insurer"))) {

                    serverRequests.Add ("Insurer");

                    GlobalProgressBarShow ("Insurer");

                    Application.InsurerGet (insurerId, true, InsurerGetCompleted);

                }

                return insurer;

            }

        }

        public Server.Application.InsuranceType InsuranceType {

            get {

                if ((insuranceType == null) && (!serverRequests.Contains ("InsuranceType"))) {

                    serverRequests.Add ("InsuranceType");

                    GlobalProgressBarShow ("InsuranceType");

                    Application.InsuranceTypeGet (insuranceTypeId, true, InsuranceTypeGetCompleted);

                }

                return insuranceType;

            }

        }

        #endregion


        #region Property Data Binding Callbacks

        private void InsurerGetCompleted (Object sender, Server.Application.InsurerGetCompletedEventArgs e) {

            serverRequests.Remove ("Insurer");

            GlobalProgressBarHide ("Insurer");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                insurer = new Insurer (Application, e.Result);

                NotifyPropertyChanged ("Insurer");

            }

            return;

        }

        private void InsuranceTypeGetCompleted (Object sender, Server.Application.InsuranceTypeGetCompletedEventArgs e) {

            serverRequests.Remove ("InsuranceType");

            GlobalProgressBarHide ("InsuranceType");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                insuranceType = e.Result;

                NotifyPropertyChanged ("InsuranceType");

            }

            return;

        }

        #endregion 


        #region Constructors

        public Program (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Program (Application applicationReference, Server.Application.Program serverProgram) {

            BaseConstructor (applicationReference, serverProgram);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Program serverProgram) {

            base.BaseConstructor (applicationReference, serverProgram);


            insurerId = serverProgram.InsurerId;

            insuranceTypeId = serverProgram.InsuranceTypeId;

            bankAccountId = serverProgram.BankAccountId;


            return;

        }

        #endregion

    }

}
