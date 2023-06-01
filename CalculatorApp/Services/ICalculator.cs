namespace CalculatorApp.Services;

public interface ICalculator
{
    public Task<double> Sum(double num1, double num2);
    public Task<double>  Subtraction(double num1, double num2);
    public Task<double>  Divide(double num1, double num2);
    public Task<double>  Multiply(double num1, double num2);
    public Task<double>  Pow(double num1, double num2);
    public Task<double>  ExtractTheRoot(double num1, double num2);
    public Task<double> Equation(string line);
}