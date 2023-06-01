namespace CalculatorApp.Models;

public class Result<R>
{
    public R OkResult { get; set; }
    public Error ErrorResult { get; set; }
    
    public Result (R okRes)
    {
        OkResult = okRes;
        ErrorResult = null;
    }

    public Result(Error error)
    {
        OkResult = default;
        ErrorResult = error;
    }
}