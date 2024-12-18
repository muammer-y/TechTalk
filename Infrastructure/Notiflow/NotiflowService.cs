namespace Infrastructure.Notiflow;

public interface INotiflowService
{
    Task<NotiflowMailResponse?> SendMail(MailRequestModel mailRequest);
}

public class NotiflowService : INotiflowService
{
    private readonly INotiflowHttpClient _notiflowClient;

    public NotiflowService(INotiflowHttpClient notiflowClient)
    {
        _notiflowClient = notiflowClient;
    }

    public async Task<NotiflowMailResponse?> SendMail(MailRequestModel mailRequest)
    {
        var isValid = await ValidateRequest(mailRequest);

        if (!isValid)
        {
            return default;
        }

        return await _notiflowClient.SendMailRequest(mailRequest);
    }

    private async Task<bool> ValidateRequest(MailRequestModel model)
    {
        if (model == null)
        {
            return false;
        }

        return true;
    }
}
