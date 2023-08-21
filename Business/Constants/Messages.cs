using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string ProductsListed = "Ürünler listelendi.";
        public static string MaintenanceTime = "Bakım saati!";
        public static string GetData = "Data getirildi.";
        public static string AuthorizationDenied = "AuthorizationDenied";
        public static string UserRegistered = "UserRegistered";
        public static string UserNotFound = "UserNotFound";
        public static string PasswordError = "PasswordError";
        public static string SuccessfulLogin= "SuccessfulLogin";
        public static string UserAlreadyExists = "UserAlreadyExists";
        public static string AccessTokenCreated = "AccessTokenCreated";
    }
}
