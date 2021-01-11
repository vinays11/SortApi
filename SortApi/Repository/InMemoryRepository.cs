namespace SortApi.Repository
{
    using Microsoft.Extensions.Logging;
    using SortApi.Dtos;
    using SortApi.Dtos.Request;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class InMemoryRepository : IRepository
    {
        private readonly ILogger<InMemoryRepository> _logger;
        private readonly ConcurrentDictionary<Guid, SortResponse> _inMemoryDataStore = new ConcurrentDictionary<Guid, SortResponse>();

        public InMemoryRepository(ILogger<InMemoryRepository> logger)
        {
            _logger = logger;
        }

        public async Task<ICollection<SortResponse>> Get()
        {
            var results = await Task.FromResult(_inMemoryDataStore.Values).ConfigureAwait(false);
            return results;
        }

        public async Task<SortResponse> Get(Guid requestId)
        {
            var availableArrays = await Task.FromResult(_inMemoryDataStore).ConfigureAwait(false);
            var result = availableArrays.GetValueOrDefault(requestId);
            return result;
        }

        public async Task Sort(SortRequest request)
        {
            var inputArray = request.Input;
            var result = new SortResponse
            {
                Id = request.Id,
                Input = inputArray,
                Status = "Completed",
                Duration = DateTime.Now.Subtract(request.RequestedTime),
            };
            var outputArray = new int[inputArray.Length];
            Array.Copy(inputArray, outputArray, inputArray.Length);
            Array.Sort(outputArray);
            result.Output = outputArray;

            await Task.FromResult(_inMemoryDataStore.TryAdd(request.Id, result)).ConfigureAwait(false);
        }
    }
}
