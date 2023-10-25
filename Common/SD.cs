using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class SD
    {
        public const string Role_SuperAdmin = "SuperAdmin";
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";
        public const string Role_Employee = "Employee";

        public const string Status_Pending = "Pending";
        public const string Status_Booked = "Booked";
        public const string Status_CheckedIn = "CheckedIn";
        public const string Status_CheckedOut_Completed = "CheckedOut";
        public const string Status_NoShow = "NoShow";
        public const string Status_Cancelled = "Cancelled";

        public const string Local_Token = "Liberty Villa Authorization key is used in authenticating application users into the application and likewise its it used in authorizing each of the users for proper management and security purpose";
        public const string Local_Api_BaseUrl = "https://localhost:7086/";
        public const string Local_Liberty_Client_Url = "https://localhost:7212/";
        public const string Local_User_Details = "User Details";

        public const string Booking_View_Model = "Hotel room View Model";
        public const string Booking_Order_Details = "Booking Order Details";
        public const string Login_Details = "Login Details";
        public const string Paystack_Order_Details = "Paystack Object";


        public const string Stripe_Api_Secrete_key = "sk_test_51NjW6HCgduLK1UTT1odoOdJnpvEg7044jTH9N3zbB2qh0AuGr9a7v7fXJILyL7lVVXuigq3zYFk4KoY5MA7ZygwO00olNtzZKx";
        public const string NLog_WebApi_Config_File = "@C:\\Users\\user\\Desktop\\VillaProject\\LibertyVilla\\LibertyVilla_WebApi\\NLog.config";
        //public const string NLog_Blazor_Server_Config_File = "@C:\\Users\\user\\Desktop\\VillaProject\\LibertyVilla\\LibertyVilla_Server\\NLog.config";
    }
}
