namespace SortApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using SortApi.Channel;
    using SortApi.Dtos;
    using SortApi.Dtos.Request;
    using SortApi.Repository;

    [Route("[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly MessagingChannel _messagingChannel;
        private readonly ILogger<SortController> _logger;
        private readonly IRepository _repository;

        public SortController(
            MessagingChannel messagingChannel,
            IRepository repository,
            ILogger<SortController> logger)
        {
            _messagingChannel = messagingChannel;
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var results = await _repository.Get();
            return JsonConvert.SerializeObject(results);
        }

        [HttpGet("{requestId}")]
        public async Task<string> Get(Guid requestId)
        {
            var result = await _repository.Get(requestId);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public async Task<string> Sort([FromBody] int[] inputArray)
        {
            var requestGuid = Guid.NewGuid();
            var request = new SortRequest
            {
                Id = requestGuid,
                Input = inputArray,
                RequestedTime = DateTime.Now
            };

            await _messagingChannel.EnqueueSortOperation(request);

            var result = new SortResponse
            {
                Id = requestGuid,
                Input = inputArray,
                Status = "Pending",
                Duration = null,
                Output = null
            };
            return JsonConvert.SerializeObject(result);
        }
    }
}
