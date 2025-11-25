namespace OpenUSSD.models;

public class MenuNode
{
    public string Id { get; init; }
    public string Title { get; set; }
    public IList<MenuOption> Options { get; } = new List<MenuOption>();
    public bool IsTerminal { get; set; }


    public MenuNode(string id, string title)
    {
        Id = id;
        Title = title;
    }
}