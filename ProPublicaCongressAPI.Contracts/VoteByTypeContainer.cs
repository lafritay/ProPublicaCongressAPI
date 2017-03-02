using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProPublicaCongressAPI.Contracts
{
    public class VoteByTypeContainer
    {
        public int Congress { get; set; }
        
        public string Chamber { get; set; }
        
        public int NumberOfResults { get; set; }
        
        public int Offset { get; set; }
        
        public IReadOnlyCollection<VoteByTypeMember> Members { get; set; }
    }
}