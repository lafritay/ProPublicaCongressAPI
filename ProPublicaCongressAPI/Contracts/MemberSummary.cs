using Newtonsoft.Json;

namespace ProPublicaCongressAPI.Contracts
{
    public class MemberSummary
    {
        public string Id { get; set; }
        public string MemberDetailUrl { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Party { get; set; }
        public string TwitterAccount { get; set; }
        public string FacebookAccount { get; set; }
        public long? FacebookId { get; set; }
        public string GoogleEntityId { get; set; }
        public string HomeUrl { get; set; }
        public string RssUrl { get; set; }
        public string HomeDomain { get; set; }
        public string DwNominate { get; set; }
        public string IdealPoint { get; set; }
        public int Seniority { get; set; }
        public int NextElection { get; set; }
        public int? TotalVotes { get; set; }
        public int? MissedVotes { get; set; }
        public int? TotalPresent { get; set; }
        public string State { get; set; }
        public int District { get; set; }
        public double PercentageOfVotesMissed { get; set; }
        public double PercentageOVotesWithParty { get; set; }
    }
}