namespace OpenUSSD.models;

public class UssdSession
{
    public string SessionId { get; init; }
    public string Msisdn { get; init; }
    public string UserId { get; init; }
    public string Network { get; init; }
    public int Level { get; set; } = 1;
    public int Part { get; set; } = 1;
    public string CurrentStep { get; set; } = "main";
    public DateTime ExpireAt { get; set; }
    public IDictionary<string, object?> Data { get; } = new Dictionary<string, object?>();


    public UssdSession(string sessionId, string msisdn, string userId, string network)
    {
        SessionId = sessionId;
        Msisdn = msisdn;
        UserId = userId;
        Network = network;
        ExpireAt = DateTime.UtcNow.AddMinutes(2);
    }
}