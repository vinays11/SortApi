namespace SortApi.BackgroundService
{
    using Microsoft.Extensions.Logging;
    using SortApi.Channel;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using System.Threading;
    using SortApi.Repository;
    using System;

    public class SortService : BackgroundService
    {
        private readonly ILogger<SortService> _logger;
        private readonly MessagingChannel _messagingChannel;
        private readonly IRepository _repository;
        public SortService(
            ILogger<SortService> logger,
            MessagingChannel messageChannel,
            IRepository repository
            )
        {
            _logger = logger;
            _messagingChannel = messageChannel;
            _repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var inputRequest in _messagingChannel.ReadAllAsync())
            {
                try
                {
                    await _repository.Sort(inputRequest);
                    _logger.LogInformation($"Successfully sorted the input request. Id: {inputRequest.Id}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occured while trying to sort the input array. Id: {inputRequest.Id}. " +
                        $"Error Message: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
