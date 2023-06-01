using System.Text;
using System.Text.RegularExpressions;

namespace CalculatorApp.Models;

public class CalculatorString
{
    public static async Task<double> CalculateMathExpression(string mathExpression)
        {
            mathExpression = mathExpression.Replace(".", ",");
            string regexPattern = @"[A-Za-z!@#$%&<>?;:""'|\]\`[{}]"; 

            if (Regex.Match(mathExpression, regexPattern).Success)
            {
                throw new ArgumentException("Недопустимый ввод!");
            }

            CheckParenthesis(mathExpression);

            var numbers = new Stack<double>();
            var operators = new Stack<char>();

            var validMathOperators = "+-/*^";

            for (int i = 0; i < mathExpression.Length; i++)
            {
                var currChar = mathExpression[i];
                if (currChar == '(')
                {
                    operators.Push(currChar);
                }
                else if (currChar == ')')
                {
                    while (operators.Peek() != '(')
                    {
                        var currOperator = operators.Pop();
                        var secondNumber = numbers.Pop();
                        var firstNumber = numbers.Pop();

                        var value = Operation(currOperator, firstNumber, secondNumber);

                        numbers.Push(await value);
                    }
                    operators.Pop();
                }
                else if (validMathOperators.Contains(currChar))
                {
                    while (operators.Count > 0 && OperatorsPriority(operators.Peek()) >= OperatorsPriority(currChar) && operators.Count < numbers.Count)
                    {
                        var currOperator = operators.Pop();
                        var secondNumber = numbers.Pop();
                        var firstNumber = numbers.Pop();

                        var value = Operation(currOperator, firstNumber, secondNumber);

                        numbers.Push(await value);
                    }

                    operators.Push(currChar);
                }
                else if (char.IsDigit(currChar) || currChar == '.' || currChar == ',')
                {
                    var currNumber = new StringBuilder();
                    while (char.IsDigit(currChar) || currChar == '.' || currChar == ',')
                    {
                        if (operators.Count > 0 && operators.Count >= numbers.Count && mathExpression[i - 1] == '-')
                        {
                            currNumber.Append(operators.Pop());
                        }
                        currNumber.Append(currChar);

                        i++;

                        if (i == mathExpression.Length)
                        {
                            break;
                        }

                        currChar = mathExpression[i];
                    }
                    i--;
                    numbers.Push(double.Parse(currNumber.ToString()));
                }
            }

            while (operators.Count() > 0)
            {
                var currOperator = operators.Pop();
                var secondNumber = numbers.Pop();
                var firstNumber = numbers.Pop();

                var value = Operation(currOperator, firstNumber, secondNumber);

                numbers.Push(await value);
            }

            return numbers.Pop();
        }
        private static void CheckParenthesis(string expression)
        {
            int i = 0;
            foreach (char c in expression)
            {
                switch (c)
                {
                    case '(': i++; break;
                    case ')': i--; break;
                }
                if (i < 0)
                    throw new ArgumentException("Не хватает '('", nameof(expression));
            }
            if (i > 0)
                throw new ArgumentException("Не хватает ')'", nameof(expression));
        }

        private static async Task<double> Operation(char mathOperator, double firstNumber, double secondNumber)
        {
            switch (mathOperator)
            {
                case '+': return firstNumber + secondNumber;
                case '-': return firstNumber - secondNumber;
                case '*': return firstNumber * secondNumber;
                case '/':
                {
                    if (secondNumber == 0)
                    {
                        throw new ArgumentException("Делить на ноль нельзя!");
                    }
                    return firstNumber / secondNumber;
                }
                case '^': return Math.Pow(firstNumber, secondNumber);
                default: throw new ArgumentException("Недопустимый математический оператор!");
            }
        }

        private static double OperatorsPriority(char mathOperator)
        {
            if (mathOperator == '+')
            {
                return 1;
            }
            else if (mathOperator == '-')
            {
                return 1;
            }
            else if (mathOperator == '*')
            {
                return 2;
            }
            else if (mathOperator == '/')
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
}