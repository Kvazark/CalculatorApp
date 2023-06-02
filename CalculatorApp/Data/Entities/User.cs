namespace CalculatorApp.Entities;

public class User
{
    public User()
    {
        
    }

    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Role { get; set; }
    
    public List<MatchOperation> Operations { get; set; } = new List<MatchOperation>();
}