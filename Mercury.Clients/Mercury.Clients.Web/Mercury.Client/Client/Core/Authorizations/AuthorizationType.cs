using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Authorizations {

    [Serializable]
    public class AuthorizationType : CoreObject {

        #region Private Properties

        private Int64 categoryId;

        private String category;

        private Int64 subcategoryId;

        private String subcategory;

        private String serviceType;

        #endregion


        #region Public Properties

        public Int64 CategoryId { get { return categoryId; } set { categoryId = value; } }

        public String Category { get { return category; } set { category = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Name); } }

        public Int64 SubcategoryId { get { return subcategoryId; } set { subcategoryId = value; } }

        public String Subcategory { get { return subcategory; } set { subcategory = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Name); } }

        public String ServiceType { get { return serviceType; } set { serviceType = Server.CommonFunctions.SetValueMaxLength (value.Trim (), Server.Data.DataTypeConstants.Name); } }


        public String AuthorizationTypeText { get { return category + " - " + subcategory + " - " + serviceType; } }

        #endregion


        #region Constructors

        public AuthorizationType (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public AuthorizationType (Application applicationReference, Server.Application.AuthorizationType serverAuthorizationType) {

            base.BaseConstructor (applicationReference, serverAuthorizationType);


            categoryId = serverAuthorizationType.CategoryId;

            category = serverAuthorizationType.Category;

            subcategoryId = serverAuthorizationType.SubcategoryId;

            subcategory = serverAuthorizationType.Subcategory;

            serviceType = serverAuthorizationType.ServiceType;


            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.AuthorizationType serverAuthorizationType) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverAuthorizationType);


            serverAuthorizationType.CategoryId = categoryId;

            serverAuthorizationType.Category = category;

            serverAuthorizationType.SubcategoryId = subcategoryId;

            serverAuthorizationType.Subcategory = subcategory;

            serverAuthorizationType.ServiceType = serviceType;

            serverAuthorizationType.Description = description;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.AuthorizationType serverAuthorizationType = new Server.Application.AuthorizationType ();

            MapToServerObject (serverAuthorizationType);

            return serverAuthorizationType;

        }

        public AuthorizationType Copy () {

            Server.Application.AuthorizationType serverAuthorizationType = (Server.Application.AuthorizationType)ToServerObject ();

            AuthorizationType copiedAuthorizationType = new AuthorizationType (application, serverAuthorizationType);

            return copiedAuthorizationType;

        }

        public Boolean IsEqual (AuthorizationType compareAuthorizationType) {

            Boolean isEqual = base.IsEqual (compareAuthorizationType);


            if (categoryId != compareAuthorizationType.CategoryId) { isEqual = false; }

            if (category != compareAuthorizationType.Category) { isEqual = false; }

            if (subcategoryId != compareAuthorizationType.SubcategoryId) { isEqual = false; }

            if (subcategory != compareAuthorizationType.Subcategory) { isEqual = false; }

            if (serviceType != compareAuthorizationType.ServiceType) { isEqual = false; }


            return isEqual;

        }

        #endregion 

    }

}
