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
        public const string Local_User_Details = "User Details";

        public const string Booking_View_Model = "Hotel room View Model";
        public const string Booking_Order_Details = "Booking Order Details";
    }
}
