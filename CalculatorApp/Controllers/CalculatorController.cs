using CalculatorApp.Models;
using CalculatorApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController : ControllerBase
{
    private readonly ILogger logger;
    private readonly ICalculator _calculator;

    public CalculatorController(ILogger<CalculatorController> logger, ICalculator calculator)
    {
        this.logger = logger;
        this._calculator = calculator;
    }

    [HttpGet("plus")]
    public async Task<Result<double>> Plus(double num1, double num2)
    {
        // var numbers = CheckNumbers(new List<string> {num1, num2});
        // return Ok(numbers.Sum());
        var result = _calculator.Sum(num1, num2);
        return new Result<double>(await result);
    }

    [HttpGet("minus")]
    public async Task<double> Subtraction(double num1, double num2)
    {
        var result = await _calculator.Subtraction(num1, num2);
        return result;
    }

    [HttpGet("divide")]
    public async Task<Result<double>> Divide(double num1, double num2)
    {
        var result = _calculator.Divide(num1, num2);
        return new Result<double>(await result);
    }

    [HttpGet("multiply")]
    public async Task<ActionResult> Multiply(double num1, double num2)
    {
        var result = _calculator.Multiply(num1, num2);
        return Ok(result.Result);
    }

    [HttpGet("pow")]
    public async Task<ActionResult> Pow(double num1, double num2)
    {
        var result = _calculator.Pow(num1, num2);
        return Ok(result.Result);
    }

    [HttpGet("extractTheRoot")]
    public async Task<ActionResult> ExtractTheRoot(double num1, double num2)
    {
        var result = _calculator.ExtractTheRoot(num1, num2);
        return Ok(result.Result);
    }

    [HttpGet("equation")]
    public async Task<ActionResult> Equation(string line)
    {
        var result = _calculator.Equation(line);
        return Ok(result.Result);
    }
}