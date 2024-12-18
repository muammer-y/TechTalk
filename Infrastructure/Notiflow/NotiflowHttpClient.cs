using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Notiflow;

public interface INotiflowHttpClient
{
    Task<NotiflowMailResponse?> SendMailRequest(MailRequestModel model, CancellationToken cancellationToken = default);
}

/// <summary>
/// <see cref="https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory"/>
/// <para />
/// <see cref="https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines"/>
/// </summary>
public class NotiflowHttpClient : INotiflowHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly ILogger<NotiflowHttpClient> _logger;

    public NotiflowHttpClient(HttpClient httpClient, ILogger<NotiflowHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<NotiflowMailResponse?> SendMailRequest(MailRequestModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/sendSmtpMail", model, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("{Method} Failed. Model {@Model} {ResponseBody}", nameof(SendMailRequest), model, responseBody);
                return default;
            }

            return await response.Content.ReadFromJsonAsync<NotiflowMailResponse>(options: _serializerOptions, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in {Method} for model {@Model}", nameof(SendMailRequest), model);
            return default;
        }
    }
}
