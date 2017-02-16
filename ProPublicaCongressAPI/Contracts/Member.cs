using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ProPublicaCongressAPI.Contracts
{
    public class Member
    {
        public string MemberId { get; set; }

        public int ThomasId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string HomeUrl { get; set; }

        public string TimesTopicsUrl { get; set; }

        public string TimesTag { get; set; }

        public int GovTrackId { get; set; }

        public int CspanId { get; set; }

        public int IcpsrId { get; set; }

        public string TwitterAccount { get; set; }

        public string FacebookAccount { get; set; }

        public long? FacebookId { get; set; }

        public string YoutubeAccount { get; set; }

        public string GoogleEntityId { get; set; }

        public string RssUrl { get; set; }

        public string HomeDomain { get; set; }

        public string CurrentParty { get; set; }

        public DateTime MostRecentVoteDate { get; set; }

        public IReadOnlyCollection<MemberRole> Roles { get; set; }
    }
}