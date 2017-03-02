using System;

namespace ProPublicaCongressAPI.Contracts
{
    public class RecentBill
    {
        public string BillNumber { get; set; }

        public string BillDetailUrl { get; set; }

        public string BillTitle { get; set; }

        public string SponsorMemberDetailUrl { get; set; }

        public DateTime DateIntroduced { get; set; }

        public int CosponsorCount { get; set; }

        public string Committees { get; set; }

        public string PrimarySubject { get; set; }

        public DateTime DateLatestMajorAction { get; set; }

        public string LatestMajorAction { get; set; }
    }
}