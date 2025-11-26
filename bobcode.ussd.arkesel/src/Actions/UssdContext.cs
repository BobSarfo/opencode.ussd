using bobcode.ussd.arkesel.models;

namespace bobcode.ussd.arkesel.Actions;

public class UssdContext
{
    public UssdRequestDto Request { get; set; }
    public UssdSession Session { get; set; }
    public string? ContextActionKey { get; set; }


    public UssdContext(UssdRequestDto req,UssdSession session)
    {
        Request = req; Session = session;
    }
}