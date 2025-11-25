namespace OpenUSSD.models;

/// <summary>
/// Represents a USSD menu structure with multiple nodes
/// </summary>
public class Menu
{
    /// <summary>
    /// Unique identifier for this menu
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Collection of menu nodes indexed by their ID
    /// </summary>
    public Dictionary<string, MenuNode> Nodes { get; } = new();

    /// <summary>
    /// ID of the root/starting node
    /// </summary>
    public string RootId { get; set; } = "main";

    public Menu(string id) => Id = id;

    /// <summary>
    /// Gets a menu node by ID
    /// </summary>
    /// <param name="id">The node ID</param>
    /// <returns>The menu node</returns>
    /// <exception cref="KeyNotFoundException">Thrown when node ID doesn't exist</exception>
    public MenuNode GetNode(string id)
    {
        if (!Nodes.TryGetValue(id, out var node))
        {
            throw new KeyNotFoundException($"Menu node '{id}' not found in menu '{Id}'.");
        }
        return node;
    }

    /// <summary>
    /// Tries to get a menu node by ID
    /// </summary>
    /// <param name="id">The node ID</param>
    /// <param name="node">The menu node if found</param>
    /// <returns>True if node exists, false otherwise</returns>
    public bool TryGetNode(string id, out MenuNode? node)
    {
        return Nodes.TryGetValue(id, out node);
    }

    /// <summary>
    /// Checks if a node exists in the menu
    /// </summary>
    public bool HasNode(string id) => Nodes.ContainsKey(id);
}
