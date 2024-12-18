namespace Infrastructure.Notiflow;

public class NotiflowMailResponse : NotiflowResponse<NotiflowMailResponseData>
{
}

public class NotiflowMailResponseData
{
    public string PostId { get; set; }
    public bool Success { get; set; }
    public List<string>? Errors { get; set; }
    public string DetailedMessage { get; set; }
    public string? TransactionId { get; set; }
}