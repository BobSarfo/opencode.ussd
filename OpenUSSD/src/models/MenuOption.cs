namespace OpenUSSD.models;

public class MenuOption
{
    public string Input { get; set; } = string.Empty; 
    public string Label { get; set; } = string.Empty; 
    public string? TargetStep { get; set; }
    public string? ActionKey { get; set; }
}