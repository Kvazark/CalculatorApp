using CalculatorApp.Models;

namespace CalculatorApp.Services;
public class Calculator : ICalculator
{
    public async Task<double> Sum(double num1, double num2)
    {
        double result = num1+num2;
        return result;
    }
    public async Task<double> Subtraction(double num1, double num2)
    {
        double result = num1-num2;
        return result;
    }
    public async Task<double> Divide(double num1, double num2)
    {
        if (num2 == 0) throw new ArgumentException("Делить на ноль нельзя!");
        return num1 / num2;
    }

    public async Task<double> Multiply(double num1, double num2)
    {
        var result = num1 * num2;
        return result;
    }
    public async Task<double> Pow(double num1, double num2)
    {
        var result = Math.Pow(num1, num2);
        return result;
    }
    public async Task<double> ExtractTheRoot(double num1, double num2)
    {
        var result =Math.Pow( num1 ,1/ num2);
        return result;
    }
    public async Task<double> Equation(string line)
    {
        var result = CalculatorString.CalculateMathExpression(line);
        return await result;
    }

}
