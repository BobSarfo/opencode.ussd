using OpenUSSD.Actions;
using OpenUSSD.models;

namespace Sample.Handlers;

/// <summary>
/// Example action handler for processing voting selections
/// </summary>
public class VotingActionHandler : BaseActionHandler
{
    public override string Key => "vote";

    public override Task<UssdStepResult> HandleAsync(UssdContext context)
    {
        // Get the candidate from user input
        var candidate = context.Request.UserData;

        // Store the vote in session
        SetSessionData(context, "vote", candidate);
        SetSessionData(context, "timestamp", DateTime.UtcNow);

        // In a real application, you would save the vote to a database here
        // await _voteRepository.SaveVoteAsync(context.Session.Msisdn, candidate);

        return Task.FromResult(End($"Thank you for voting for candidate {candidate}!\nYour vote has been recorded."));
    }
}

