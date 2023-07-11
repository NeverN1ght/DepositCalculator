using System.Globalization;
using System.Windows.Controls;

namespace DepositCalculator.Desktop.Validation;

public class DepositTermValidationRule : ValidationRule
{
    private const int DepositTermMinimalValue = 1;
    private const int DepositTermMaximalValue = 5 * 12;

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (int.TryParse(value.ToString(), out var depositTerm) 
            && depositTerm is >= DepositTermMinimalValue and <= DepositTermMaximalValue)
        {
            return ValidationResult.ValidResult;
        }

        return new ValidationResult(false, "Invalid value");
    }
}