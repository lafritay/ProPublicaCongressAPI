using Newtonsoft.Json;
using ProPublicaCongressAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProPublicaCongressAPI
{
    public class ProPublicaCongressApiClient
    {
        private readonly string apiKey;

        private const string apiBaseUrl = "https://api.propublica.org/congress/";
        private const string membersUrl = "v1/{0}/{1}/members.json"; // 0 = congress, 1 = chamber
        private const string specificMemberUrl = "v1/members/{0}.json"; // 0 = member-id
        private const string newMembersUrl = "v1/members/new.json";
        private const string currentMembersUrl = "v1/members/{0}/{1}/current.json"; // 0 = chamber, 1 = state
        private const string currentHouseMembersUrl = "v1/members/{0}/{1}/{2}/current.json"; // 0 = chamber, 1 = state, 2 = district
        private const string memberVotesUrl = "v1/members/{0}/votes.json"; // 0 = member-id
        private const string compareMemberVotesUrl = "v1/members/{0}/votes/{1}/{2}/{3}.json"; // 0 = first-member-id, 1 = second-member-id, 2 = congress, 3 = chamber
        private const string compareMemberBillSponsorshipsUrl = "v1/members/{0}/bills/{1}/{2}/{3}.json"; // 0 = first-member-id, 1 = second-member-id, 2 = congress, 3 = chamber
        private const string memberCosponsoredBillsUrl = "v1/members/{0}/bills/{1}.json"; // 0 = member-id, 1 = type
        private const string voteRollCallUrl = "v1/{0}/{1}/sessions/{2}/votes/{3}.json"; // 0 = congress, 1 = chamber, 2 = session-number, 3 = roll-call-number
        private const string votesByTypeUrl = "v1/{0}/{1}/votes/{2}.json"; // 0 = congress, 1 = chamber, 2 = vote-type
        private const string votesByDateUrl = "v1/{0}/votes/{1}/{2}.json"; // 0 = chamber, 1 = year, 2 = month
        private const string senateNominationsUrl = "v1/{0}/nominations.json"; // 0 = congress
        private const string recentBillsUrl = "v1/{0}/{1}/bills/{2}.json"; // 0 = congress, 1 = chamber, 2 = bill-type
        private const string recentBillsByMemberUrl = "v1/members/{0}/bills/{1}.json"; // 0 = member-id, 1 = bill-type
        private const string specificBillUrl = "v1/{0}/bills/{1}.json"; // 0 = congress, 1 = bill-id
        private const string specificBillDetailsUrl = "v1/{0}/bills/{1}/{type}.json"; // 0 = congress, 1 = details-type
        private const string billCosponsorsUrl = "v1/{0}/bills/{1}/cosponsors.json"; // 0 = congress, 1 = bill-id
        private const string recentNominationsByTypeUrl = "v1/{0}/nominees/{1}.json"; // 0 = congress, 1 = nomination-type
        private const string specificNominationUrl = "v1/{0}/nominees/{1}.json"; // 0 = congress, 1 = nominee-id
        private const string nomineesByStateUrl = "v1/{0}/nominees/state/{1}.json"; // 0 = congress, 1 = state
        private const string statePartyCountUrl = "v1/states/members/party.json";
        private const string committeesUrl = "v1/{0}/{1}/committees/{2}.json"; // 0 = congress, 1 = chamber, 2 = committee-id

        public ProPublicaCongressApiClient(string apiKey)
        {
            this.apiKey = apiKey;
            AutoMapperConfiguration.Initialize();
        }

        public async Task<Contracts.MemberBillsCosponsoredContainer> GetBillsCosponsoredByMember(string memberId, CosponsorBillType type)
        {
            if (String.IsNullOrWhiteSpace(memberId))
            {
                throw new ArgumentNullException("memberId", "Member ID is required.");
            }

            string url = apiBaseUrl + String.Format(memberCosponsoredBillsUrl, memberId, type.ToString().ToLower());

            var contract = await GetAndMapSingleDataAsync<
                InternalModels.MemberBillsCosponsoredContainer,
                Contracts.MemberBillsCosponsoredContainer>(url);

            return contract;
        }

        public async Task<Contracts.MemberBillSponsorshipComparisonContainer> CompareMemberBillSponsorships(string firstMemberId, string secondMemberId, int congress, Chamber chamber)
        {
            if (String.IsNullOrWhiteSpace(firstMemberId))
            {
                throw new ArgumentNullException("firstMemberId", "First Member ID is required.");
            }

            if (String.IsNullOrWhiteSpace(secondMemberId))
            {
                throw new ArgumentNullException("secondMemberId", "Second Member ID is required.");
            }

            string url = apiBaseUrl + String.Format(compareMemberBillSponsorshipsUrl, firstMemberId, secondMemberId, congress, chamber.ToString().ToLower());

            var contract = await GetAndMapSingleDataAsync<
                InternalModels.MemberBillSponsorshipComparisonContainer,
                Contracts.MemberBillSponsorshipComparisonContainer>(url);

            return contract;
        }

        public async Task<IReadOnlyCollection<MemberVoteComparison>> CompareMemberVotes(string firstMemberId, string secondMemberId, int congress, Chamber chamber)
        {
            if (String.IsNullOrWhiteSpace(firstMemberId))
            {
                throw new ArgumentNullException("firstMemberId", "First Member ID is required.");
            }

            if (String.IsNullOrWhiteSpace(secondMemberId))
            {
                throw new ArgumentNullException("secondMemberId", "Second Member ID is required.");
            }

            string url = apiBaseUrl + String.Format(compareMemberVotesUrl, firstMemberId, secondMemberId, congress, chamber.ToString().ToLower());

            var contract = await GetAndMapMultipleDataAsync<InternalModels.MemberVoteComparison, Contracts.MemberVoteComparison>(url);

            return contract;
        }

        public async Task<Contracts.MemberVotesContainer> GetMemberVotesAsync(string memberId)
        {
            if (String.IsNullOrWhiteSpace(memberId))
            {
                throw new ArgumentNullException("memberId", "Member ID is required.");
            }

            string url = apiBaseUrl + String.Format(memberVotesUrl, memberId);

            var contract = await GetAndMapSingleDataAsync<InternalModels.MemberVotesContainer, Contracts.MemberVotesContainer>(url);

            return contract;
        }

        public async Task<IReadOnlyCollection<Contracts.CurrentMember>> GetCurrentMembersAsync(Chamber chamber, string state, int? district = null)
        {
            if (chamber == Chamber.Unknown)
            {
                throw new ArgumentException("Chamber must be 'House' or 'Senate'.");
            }

            if (String.IsNullOrWhiteSpace(state))
            {
                throw new ArgumentNullException("state", "State is required.");
            }

            string url = apiBaseUrl;

            if (district.HasValue)
            {
                url += String.Format(currentHouseMembersUrl, chamber.ToString().ToLower(), state, district);
            }
            else
            {
                url += String.Format(currentMembersUrl, chamber.ToString().ToLower(), state);
            }

            var contract = await GetAndMapMultipleDataAsync<InternalModels.CurrentMember, Contracts.CurrentMember>(url);

            return contract;
        }

        public async Task<Contracts.NewMembersContainer> GetNewMembersAsync()
        {
            string url = apiBaseUrl + newMembersUrl;

            var contract = await GetAndMapSingleDataAsync<InternalModels.NewMembersContainer, Contracts.NewMembersContainer>(url);

            return contract;
        }

        /// <summary>
        /// Returns a collection of members in whatever Congress and Chamber (House or Senate) is passed as parameters. Use the MemberUrl in the response to get more details about a specific member.
        /// </summary>
        /// <param name="congress">Number of Congress to query (ie. 2017 is the 115th Congress).</param>
        /// <param name="chamber">Chamber of Congress such as "house" or "senate".</param>
        /// <returns></returns>
        public async Task<Contracts.MembersContainer> GetMembersAsync(int congress, Chamber chamber)
        {
            if (chamber == Chamber.Unknown)
            {
                throw new ArgumentException("Chamber must be 'House' or 'Senate'.");
            }

            string url = apiBaseUrl + String.Format(membersUrl, congress, chamber.ToString().ToLower());

            var contract = await GetAndMapSingleDataAsync<InternalModels.MembersContainer, Contracts.MembersContainer>(url);

            return contract;
        }

        /// <summary>
        /// Returns details about a specific member of congress identified by the passed Member Id.
        /// </summary>
        /// <param name="memberId">Member Id retrieved from the list of Members.</param>
        /// <returns>A specific Member of Congress.</returns>
        public async Task<Contracts.Member> GetMemberAsync(string memberId)
        {
            if (String.IsNullOrWhiteSpace(memberId))
            {
                throw new ArgumentNullException("memberId", "Member ID is required.");
            }

            string url = apiBaseUrl + String.Format(specificMemberUrl, memberId);

            var contract = await GetAndMapSingleDataAsync<InternalModels.Member, Contracts.Member>(url);

            return contract;
        }

        private async Task<IReadOnlyCollection<TInternal>> GetAndCheckInternalDataAsync<TInternal>(string url)
        {
            var result = await GetDataAsync<TInternal>(url);

            if (result == null || result.Results == null || result.Results.Count == 0)
            {
                return null;
            }

            return result.Results;
        }

        private async Task<TContract> GetAndMapSingleDataAsync<TInternal, TContract>(string url)
        {
            var result = await GetAndCheckInternalDataAsync<TInternal>(url);

            if (result == null)
            {
                return default(TContract);
            }

            var contract = AutoMapperConfiguration.Mapper.Map<TInternal, TContract>(result.ElementAt(0));

            return contract;
        }

        private async Task<IReadOnlyCollection<TContract>> GetAndMapMultipleDataAsync<TInternal, TContract>(string url)
        {
            var result = await GetAndCheckInternalDataAsync<TInternal>(url);

            if (result == null)
            {
                return new List<TContract>().AsReadOnly();
            }

            var contract = AutoMapperConfiguration.Mapper.Map<
                IReadOnlyCollection<TInternal>,
                IReadOnlyCollection<TContract>>(result);

            return contract;
        }

        /// <summary>
        /// Returns some data from the ProPublica Congress API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<ApiResponse<T>> GetDataAsync<T>(string url)
        {
            Debug.Assert(!String.IsNullOrWhiteSpace(url));
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);
            var membersJson = await httpClient.GetStringAsync(url);
            var resultInterface = JsonConvert.DeserializeObject<ApiResponse<T>>(membersJson);
            return resultInterface;
        }
    }
}