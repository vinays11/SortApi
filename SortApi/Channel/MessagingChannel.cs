namespace SortApi.Channel
{
    using Microsoft.Extensions.Logging;
    using SortApi.Dtos.Request;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;

    public class MessagingChannel
    {
        private const int MaxMessagesInChannel = 100;
        private readonly Channel<SortRequest> _channel;
        private readonly ILogger<MessagingChannel> _logger;
        public MessagingChannel(ILogger<MessagingChannel> logger)
        {
            var options = new BoundedChannelOptions(MaxMessagesInChannel)
            {
                SingleWriter = false,
                SingleReader = true
            };
            
            _channel = Channel.CreateBounded<SortRequest>(options);
            _logger = logger;
        }

        public async Task EnqueueSortOperation(SortRequest sortRequest, CancellationToken ct = default)
        {
            while (await _channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                if (_channel.Writer.TryWrite(sortRequest))
                {
                    _logger.LogInformation("Written sort request to the message channel");
                    return;
                }
            }
        }

        public IAsyncEnumerable<SortRequest> ReadAllAsync(CancellationToken ct = default) =>
            _channel.Reader.ReadAllAsync(ct);
    }
}
