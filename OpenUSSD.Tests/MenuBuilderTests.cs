using OpenUSSD.Actions;
using OpenUSSD.models;

namespace OpenUSSD.Tests;

public class MenuBuilderTests
{
    [Fact]
    public void Build_WithValidMenu_ReturnsMenu()
    {
        // Arrange
        var builder = new MenuBuilder("test-menu");

        // Act
        var menu = builder
            .AddNode("main", "Welcome")
            .AddOption("main", "1", "Option 1", "step1")
            .AddNode("step1", "Step 1")
            .Build();

        // Assert
        Assert.NotNull(menu);
        Assert.Equal("test-menu", menu.Id);
        Assert.Equal(2, menu.Nodes.Count);
    }

    [Fact]
    public void AddNode_CreatesNodeWithCorrectProperties()
    {
        // Arrange
        var builder = new MenuBuilder("test-menu");

        // Act
        var menu = builder
            .AddNode("main", "Welcome Message", isTerminal: false)
            .Build();

        // Assert
        var node = menu.GetNode("main");
        Assert.Equal("main", node.Id);
        Assert.Equal("Welcome Message", node.Title);
        Assert.False(node.IsTerminal);
    }

    [Fact]
    public void AddOption_AddsOptionToNode()
    {
        // Arrange
        var builder = new MenuBuilder("test-menu");

        // Act
        var menu = builder
            .AddNode("main", "Main Menu")
            .AddOption("main", "1", "First Option", "step1")
            .AddOption("main", "2", "Second Option", actionKey: "action1")
            .AddNode("step1", "Step 1")
            .Build();

        // Assert
        var node = menu.GetNode("main");
        Assert.Equal(2, node.Options.Count);
        Assert.Equal("1", node.Options[0].Input);
        Assert.Equal("First Option", node.Options[0].Label);
        Assert.Equal("step1", node.Options[0].TargetStep);
        Assert.Equal("action1", node.Options[1].ActionKey);
    }

    [Fact]
    public void AddOption_ThrowsException_WhenNodeDoesNotExist()
    {
        // Arrange
        var builder = new MenuBuilder("test-menu");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            builder.AddOption("nonexistent", "1", "Option"));
    }

    [Fact]
    public void SetRoot_SetsRootId()
    {
        // Arrange
        var builder = new MenuBuilder("test-menu");

        // Act
        var menu = builder
            .AddNode("custom-root", "Custom Root")
            .SetRoot("custom-root")
            .Build();

        // Assert
        Assert.Equal("custom-root", menu.RootId);
    }

    [Fact]
    public void Build_ThrowsException_WhenRootNodeDoesNotExist()
    {
        // Arrange
        var builder = new MenuBuilder("test-menu")
            .SetRoot("nonexistent");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void AddOptions_AddsMultipleOptions()
    {
        // Arrange
        var builder = new MenuBuilder("test-menu");

        // Act
        var menu = builder
            .AddNode("main", "Main Menu")
            .AddOptions("main",
                ("1", "Option 1", "step1", null),
                ("2", "Option 2", "step2", null),
                ("3", "Option 3", null, "action1"))
            .AddNode("step1", "Step 1")
            .AddNode("step2", "Step 2")
            .Build();

        // Assert
        var node = menu.GetNode("main");
        Assert.Equal(3, node.Options.Count);
    }

    [Fact]
    public void Build_CreatesComplexMenuStructure()
    {
        // Arrange & Act
        var menu = new MenuBuilder("voting-app")
            .AddNode("main", "Welcome to Voting App")
            .AddOption("main", "1", "Vote for Product A", actionKey: "vote_a")
            .AddOption("main", "2", "Vote for Product B", actionKey: "vote_b")
            .AddOption("main", "3", "View Results", "results")
            .AddNode("results", "Results will be displayed here", isTerminal: true)
            .Build();

        // Assert
        Assert.Equal(2, menu.Nodes.Count);
        Assert.True(menu.GetNode("results").IsTerminal);
        Assert.False(menu.GetNode("main").IsTerminal);
    }
}

