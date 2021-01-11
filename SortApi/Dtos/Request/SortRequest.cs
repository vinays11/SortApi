namespace SortApi.Dtos.Request
{
    using System;
    public class SortRequest
    {
        public Guid Id { get; set; }
        public int[] Input { get; set; }
        public DateTime RequestedTime { get; set; }
    }
}
