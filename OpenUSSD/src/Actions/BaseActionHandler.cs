using OpenUSSD.models;

namespace OpenUSSD.Actions;

/// <summary>
/// Base class for action handlers providing common functionality
/// </summary>
public abstract class BaseActionHandler : IActionHandler
{
    /// <summary>
    /// Unique key identifying this action handler
    /// </summary>
    public abstract string Key { get; }

    /// <summary>
    /// Handles the action and returns the result
    /// </summary>
    public abstract Task<UssdStepResult> HandleAsync(UssdContext context);

    /// <summary>
    /// Helper method to continue the session with a message
    /// </summary>
    protected UssdStepResult Continue(string message, string? nextStep = null)
        => new UssdStepResult
        {
            Message = message,
            ContinueSession = true,
            NextStep = nextStep
        };

    /// <summary>
    /// Helper method to end the session with a message
    /// </summary>
    protected UssdStepResult End(string message)
        => new UssdStepResult
        {
            Message = message,
            ContinueSession = false
        };

    /// <summary>
    /// Helper method to navigate to home menu
    /// </summary>
    protected UssdStepResult GoHome()
        => new UssdStepResult
        {
            GoHome = true,
            ContinueSession = true
        };

    /// <summary>
    /// Helper method to get session data
    /// </summary>
    protected T? GetSessionData<T>(UssdContext context, string key)
    {
        if (context.Session.Data.TryGetValue(key, out var value) && value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    /// <summary>
    /// Helper method to set session data
    /// </summary>
    protected void SetSessionData(UssdContext context, string key, object? value)
    {
        context.Session.Data[key] = value;
    }
}
