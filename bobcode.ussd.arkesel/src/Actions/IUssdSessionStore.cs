using bobcode.ussd.arkesel.models;

namespace bobcode.ussd.arkesel.Actions;

public interface IUssdSessionStore
{
    Task<UssdSession?> GetAsync(string sessionId, CancellationToken ct = default);
    Task SetAsync(UssdSession session, TimeSpan ttl, CancellationToken ct = default);
    Task RemoveAsync(string sessionId, CancellationToken ct = default);
}