using System.Threading.Tasks;
using bobcode.ussd.arkesel.Actions;

namespace bobcode.ussd.arkesel.models;

public abstract class UssdStep
{
    public string Name { get; set; } = string.Empty;
    public abstract Task<UssdStepResult> ExecuteAsync(UssdContext ctx);
}