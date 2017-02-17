using Newtonsoft.Json;

namespace ProPublicaCongressAPI.InternalModels
{
    internal class SpecificBillDetailAmendment
    {
        [JsonProperty("amentment_number")]
        public string AmendmentNumber { get; set; }

        [JsonProperty("sponsor_id")]
        public string SponsorMemberId { get; set; }

        [JsonProperty("introduced_date")]
        public string DateIntroduced { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("latest_major_action_date")]
        public string DateLatestMajorAction { get; set; }

        [JsonProperty("latest_major_action")]
        public string LatestMajorAction { get; set; }
    }
}