namespace CalculatorApp.Entities;

public class MatchOperation
{
    public Guid Id { get; set; }
    public string OperationName { get; set; }
    public string Result { get; set; }
    
    public string? UserId { get; set; }
}