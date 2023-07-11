using System.Globalization;
using System.Windows.Controls;

namespace DepositCalculator.Desktop.Validation;

public class DepositRateValidationRule : ValidationRule
{
    private const double DepositRateMaximalValue = 100.0;

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (double.TryParse(value.ToString(), out var depositRate) 
            && depositRate is > 0 and <= DepositRateMaximalValue)
        {
            return ValidationResult.ValidResult;
        }

        return new ValidationResult(false, "Invalid value");
    }
}