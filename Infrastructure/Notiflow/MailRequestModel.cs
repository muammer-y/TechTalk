namespace Infrastructure.Notiflow;

public class MailRequestModel
{
    public string FromAddress { get; set; }
    public string FromDisplayName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<string> ToEmailAddress { get; set; } = [];
}
