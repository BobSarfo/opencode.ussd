using System.Threading.Tasks;
using OpenUSSD.Actions;

namespace OpenUSSD.models;

public abstract class UssdStep
{
    public string Name { get; set; } = string.Empty;
    public abstract Task<UssdStepResult> ExecuteAsync(UssdContext ctx);
}