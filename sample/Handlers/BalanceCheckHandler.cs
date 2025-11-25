using OpenUSSD.Actions;
using OpenUSSD.models;

namespace Sample.Handlers;

/// <summary>
/// Example action handler for checking account balance
/// </summary>
public class BalanceCheckHandler : BaseActionHandler
{
    public override string Key => nameof(BalanceCheckHandler); //"check_balance";

    public override Task<UssdStepResult> HandleAsync(UssdContext context)
    {
        // In a real application, you would fetch the balance from a database or API
        // var balance = await _accountService.GetBalanceAsync(context.Session.Msisdn);

        // For demo purposes, we'll use a mock balance
        var balance = 150.50m;

        // Store in session for potential future use
        SetSessionData(context, "last_balance_check", DateTime.UtcNow);

        return Task.FromResult(End($"Your current balance is: GHS {balance:F2}\nThank you for using our service."));
    }
}

