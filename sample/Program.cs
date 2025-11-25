using OpenUSSD.Core;
using OpenUSSD.Actions;
using OpenUSSD.models;
using Sample.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the USSD menu using MenuBuilder
var menu = new MenuBuilder("demo_bank_menu")
    // Root menu
    .SetRoot("main")
    .AddNode("main", "Welcome to Demo Bank\n1. Check Balance\n2. Transfer Money\n3. Vote\n4. View Products")

    // Balance check flow
    .AddOption("main", "1", "Check Balance", actionKey: nameof(BalanceCheckHandler))

    // Transfer flow
    .AddOption("main", "2", "Transfer Money", targetStep: "transfer_recipient")
    .AddNode("transfer_recipient", "Enter recipient phone number:")
    .AddOption("transfer_recipient", "*", "Any input", actionKey: "transfer")

    // Voting flow
    .AddOption("main", "3", "Vote", targetStep: "vote_menu")
    .AddNode("vote_menu", "Vote for your candidate:\n1. Candidate A\n2. Candidate B\n3. Candidate C")
    .AddOption("vote_menu", "1", "Candidate A", actionKey: "vote")
    .AddOption("vote_menu", "2", "Candidate B", actionKey: "vote")
    .AddOption("vote_menu", "3", "Candidate C", actionKey: "vote")

    // Products menu with pagination example
    .AddOption("main", "4", "View Products", targetStep: "products")
    .AddNode("products", "Our Products:\n1. Product A - GHS 10\n2. Product B - GHS 20\n3. Product C - GHS 30\n4. Product D - GHS 40\n5. Product E - GHS 50")

    .Build();

// Configure USSD SDK with custom options
var ussdOptions = new UssdOptions
{
    SessionTimeout = TimeSpan.FromMinutes(5),
    BackCommand = "0",
    HomeCommand = "#",
    EnablePagination = true,
    ItemsPerPage = 5,
    InvalidInputMessage = "Invalid input. Please try again.",
    DefaultEndMessage = "Thank you for using our service."
};

builder.Services.AddUssdSdk(menu, ussdOptions);

// Register action handlers
builder.Services.AddActionHandler<VotingActionHandler>();
builder.Services.AddActionHandler<BalanceCheckHandler>();
builder.Services.AddActionHandler<TransferHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// Minimal API endpoint for USSD
app.MapPost("/ussd", async (UssdRequestDto request, UssdApp ussdApp) =>
{
    var response = await ussdApp.HandleRequestAsync(request);
    return Results.Ok(response);
})
.WithName("PostUSSD")
.WithOpenApi();

app.Run();
