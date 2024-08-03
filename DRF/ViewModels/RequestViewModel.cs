﻿namespace DRF.ViewModels
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public string ProgramTitle { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public string ThirdPartyOrganization { get; set; }
        public string CurrentStatus { get; set; }
    }
}
