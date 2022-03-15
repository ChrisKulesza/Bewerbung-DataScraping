using NLog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StBK_ToolGetDetails.Service;

public class RetryHandler : DelegatingHandler
{
    private readonly int _retryCount;
    private readonly double _retryDelay;
    private static Logger _logger = LogManager.GetCurrentClassLogger();

    // Strongly consider limiting the number of retries - "retry forever" is
    // probably not the most user friendly way you could respond to "the
    // network cable got pulled out."
    public RetryHandler(HttpMessageHandler innerHandler, int retryCount, double retryDelay)
        : base(innerHandler)
    {
        _retryCount = retryCount;
        _retryDelay = retryDelay;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage? response;
        var exceptions = new List<Exception>();
        for (int i = 0; i <= _retryCount; i++)
        {
            try
            {
                response = await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                response = null;
            }

            // check if response was successfully
            if (response is { IsSuccessStatusCode: true })
            {
                if (i > 0)
                    _logger.Warn("The RetryHandler ran {0} times before the server response was positive.", i);

                return response;
            }
            // response != successfully wait and try again
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(_retryDelay), cancellationToken);
            }
        }

        // ToDo:    RETRY_HANDLER | Email senden, wenn nach so und so vielen Versuchen kein erfolgreicher Post abgesetzt werden konnte.
        // ToDo:    RETRY_HANDLER | Logging Retries when retry attempts fail
        throw new AggregateException($"Could not send request after {_retryCount} retries.", exceptions);
    }
}
