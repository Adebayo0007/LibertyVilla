﻿using System.Security.Principal;

namespace LibertyVilla.Client.ViewModel
{
    public class HomeVM
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int NoOfNights { get; set; } = 1;
    }
}