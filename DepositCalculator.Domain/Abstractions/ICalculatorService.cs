namespace DepositCalculator.Domain.Abstractions;

public interface ICalculatorService
{
    decimal CalculateFinalAmount(decimal initialAmount, int termMonth, decimal monthRate, bool capitalization);
}