namespace Infrastructure.Notiflow;

public class NotiflowResponse<T>
{
    public T? Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public int ResponseCode { get; set; }
}
