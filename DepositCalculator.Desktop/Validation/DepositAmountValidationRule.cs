using System.Globalization;
using System.Windows.Controls;

namespace DepositCalculator.Desktop.Validation;

public class DepositAmountValidationRule : ValidationRule
{
    private const int DepositMinimalValue = 1;
    private const int DepositMaximalValue = 1_000_000_000;
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (decimal.TryParse(value.ToString(), out var depositAmount) 
            && depositAmount is >= DepositMinimalValue and <= DepositMaximalValue)
        {
            return ValidationResult.ValidResult;
        }

        return new ValidationResult(false, "Invalid value");
    }
}