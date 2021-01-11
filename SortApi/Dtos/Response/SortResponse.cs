namespace SortApi.Dtos
{
    using Newtonsoft.Json;
    using System;

    public class SortResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("duration")]
        public TimeSpan? Duration { get; set; }
        
        [JsonProperty("input")]
        public int[] Input { get; set; }

        [JsonProperty("output")]
        public int[] Output { get; set; }
    }
}
