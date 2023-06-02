namespace CalculatorApp.DTO;

public class OperationDto
{
    public Guid Id { get; set; }
    public string OperationName { get; set;}
    public string UserId { get; set; }
    public string Result { get; set; }
}