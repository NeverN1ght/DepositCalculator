using System;
using DepositCalculator.Domain.Abstractions;

namespace DepositCalculator.Domain.Services;

public class CalculatorService : ICalculatorService
{
    private const int PercentageDelimiter = 100;
    
    public decimal CalculateFinalAmount(decimal initialAmount, int termMonth, decimal monthRate, bool capitalization = false) 
        => capitalization
            ? initialAmount * (decimal) Math.Pow(1 + (double) monthRate / PercentageDelimiter, termMonth)
            : initialAmount + initialAmount * monthRate / PercentageDelimiter * termMonth;
}