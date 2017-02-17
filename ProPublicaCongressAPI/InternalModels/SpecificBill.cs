using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProPublicaCongressAPI.InternalModels
{
    internal class SpecificBill
    {
        [JsonProperty("datetime")]
        public int Congress { get; set; }

        [JsonProperty("bill")]
        public string BillNumber { get; set; }

        [JsonProperty("bill_uri")]
        public string BillDetailUrl { get; set; }

        [JsonProperty("title")]
        public string BillTitle { get; set; }

        [JsonProperty("sponsor")]
        public string SponsorMemberName { get; set; }

        [JsonProperty("sponsor_uri")]
        public string SponsorMemberDetailUrl { get; set; }

        [JsonProperty("gpo_pdf_uri")]
        public string BillDocumentPdfUrl { get; set; }

        [JsonProperty("introduced_date")]
        public string DateIntroduced { get; set; }

        [JsonProperty("cosponsors")]
        public int CosponsorCount { get; set; }

        [JsonProperty("primary_subject")]
        public string PrimarySubject { get; set; }

        [JsonProperty("committees")]
        public string Committees { get; set; }

        [JsonProperty("latest_major_action_date")]
        public string DateLatestMajorAction { get; set; }

        [JsonProperty("latest_major_action")]
        public string LatestMajorAction { get; set; }

        [JsonProperty("house_passage_vote")]
        public string DateHousePassageVote { get; set; }

        [JsonProperty("senate_passage_vote")]
        public string DateSenatePassageVote { get; set; }

        [JsonProperty("actions")]
        public IReadOnlyCollection<SpecificBillAction> Actions { get; set; }

        [JsonProperty("votes")]
        public IReadOnlyCollection<SpecificBillVoteSummary> Votes { get; set; }
    }
}