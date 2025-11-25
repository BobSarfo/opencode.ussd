using OpenUSSD.Actions;
using OpenUSSD.models;

namespace Sample.Handlers;

/// <summary>
/// Example action handler for processing money transfers
/// Demonstrates multi-step flow with session data
/// </summary>
public class TransferHandler : BaseActionHandler
{
    public override string Key => "transfer";

    public override Task<UssdStepResult> HandleAsync(UssdContext context)
    {
        var input = context.Request.UserData;
        
        // Get current step from session
        var transferStep = GetSessionData<string>(context, "transfer_step") ?? "recipient";
        
        switch (transferStep)
        {
            case "recipient":
                // Store recipient phone number
                SetSessionData(context, "recipient", input);
                SetSessionData(context, "transfer_step", "amount");
                return Task.FromResult(Continue("Enter amount to transfer:", "transfer_amount"));

            case "amount":
                // Validate and store amount
                if (!decimal.TryParse(input, out var amount) || amount <= 0)
                {
                    return Task.FromResult(Continue("Invalid amount. Please enter a valid amount:", "transfer_amount"));
                }

                SetSessionData(context, "amount", amount);
                SetSessionData(context, "transfer_step", "confirm");

                var recipient = GetSessionData<string>(context, "recipient");
                return Task.FromResult(Continue(
                    $"Confirm transfer:\nTo: {recipient}\nAmount: GHS {amount:F2}\n1. Confirm\n2. Cancel",
                    "transfer_confirm"
                ));

            case "confirm":
                if (input == "1")
                {
                    // Process transfer
                    var recipientPhone = GetSessionData<string>(context, "recipient");
                    var transferAmount = GetSessionData<decimal>(context, "amount");

                    // In a real application, you would process the transfer here
                    // await _transferService.ProcessTransferAsync(context.Session.Msisdn, recipientPhone, transferAmount);

                    // Clear transfer session data
                    SetSessionData(context, "transfer_step", null);
                    SetSessionData(context, "recipient", null);
                    SetSessionData(context, "amount", null);

                    return Task.FromResult(End($"Transfer of GHS {transferAmount:F2} to {recipientPhone} successful!\nThank you."));
                }
                else
                {
                    // Clear transfer session data
                    SetSessionData(context, "transfer_step", null);
                    SetSessionData(context, "recipient", null);
                    SetSessionData(context, "amount", null);

                    return Task.FromResult(GoHome());
                }

            default:
                return Task.FromResult(End("An error occurred. Please try again."));
        }
    }
}

