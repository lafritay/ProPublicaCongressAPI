using Newtonsoft.Json;
using ProPublicaCongressAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private const string memberVotesUrl = "v1/members/{0}/votes.json"; // 0 = member-id
        private const string compareMemberVotesUrl = "v1/members/{0}/votes/{1}/{2}/{3}.json"; // 0 = first-member-id, 1 = second-member-id, 2 = congress, 3 = chamber
        private const string compareMemberBillsUrl = "v1/members/{0}/bills/{1}/{2}/{3}.json"; // 0 = first-member-id, 1 = second-member-id, 2 = congress, 3 = chamber
        private const string memberCosponsoredBillsUrl = "v1/members/{0}/bills/{type}.json"; // 0 = member-id, 1 = type
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

        /// <summary>
        /// Returns a collection of members in whatever Congress and Chamber (House or Senate) is passed as parameters. Use the MemberUrl in the response to get more details about a specific member.
        /// </summary>
        /// <param name="congress">Number of Congress to query (ie. 2017 is the 115th Congress).</param>
        /// <param name="chamber">Chamber of Congress such as "house" or "senate".</param>
        /// <returns></returns>
        public async Task<Contracts.MembersContainer> GetMembersAsync(int congress, string chamber)
        {
            string url = apiBaseUrl + String.Format(membersUrl, congress, chamber.ToString().ToLower());

            var result = await GetDataAsync<InternalModels.MembersContainer>(url);

            if (result == null || result.Results == null || result.Results.Count == 0)
            {
                return null;
            }

            var contract = AutoMapperConfiguration.Mapper.Map<InternalModels.MembersContainer, Contracts.MembersContainer>(result.Results.ElementAt(0));

            return contract;
        }

        /// <summary>
        /// Returns details about a specific member of congress identified by the passed Member Id.
        /// </summary>
        /// <param name="memberId">Member Id retrieved from the list of Members.</param>
        /// <returns>A specific Member of Congress.</returns>
        public async Task<Contracts.Member> GetMemberAsync(string memberId)
        {
            string url = apiBaseUrl + String.Format(specificMemberUrl, memberId);

            var result = await GetDataAsync<InternalModels.Member>(url);

            if (result == null || result.Results == null || result.Results.Count == 0)
            {
                return null;
            }

            var contract = AutoMapperConfiguration.Mapper.Map<InternalModels.Member, Contracts.Member>(result.Results.ElementAt(0));

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
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);
            var membersJson = await httpClient.GetStringAsync(url);
            var resultInterface = JsonConvert.DeserializeObject<ApiResponse<T>>(membersJson);
            return resultInterface;
        }
    }
}
