using System;
using System.Collections.Generic;

namespace ProPublicaCongressAPI.Contracts
{
    public class SpecificBill
    {
        public int Congress { get; set; }
        public string BillNumber { get; set; }
        public string BillDetailUrl { get; set; }
        public string BillTitle { get; set; }
        public string SponsorMemberName { get; set; }
        public string SponsorMemberDetailUrl { get; set; }
        public string BillDocumentPdfUrl { get; set; }
        public DateTime DateIntroduced { get; set; }
        public int CosponsorCount { get; set; }
        public string PrimarySubject { get; set; }
        public string Committees { get; set; }
        public DateTime DateLatestMajorAction { get; set; }
        public string LatestMajorAction { get; set; }
        public DateTime? DateHousePassageVote { get; set; }
        public DateTime? DateSenatePassageVote { get; set; }
        public IReadOnlyCollection<SpecificBillAction> Actions { get; set; }
        public IReadOnlyCollection<SpecificBillVoteSummary> Votes { get; set; }
    }
}