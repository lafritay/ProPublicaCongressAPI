using Newtonsoft.Json;

namespace ProPublicaCongressAPI.InternalModels
{
    internal class SpecificBillAction
    {
        [JsonProperty("datetime")]
        public string DateTimeOccurred { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}