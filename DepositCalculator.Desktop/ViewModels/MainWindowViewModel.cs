using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DepositCalculator.Domain.Abstractions;

namespace DepositCalculator.Desktop.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly ICalculatorService _calculatorService;
    
    private decimal _depositAmount;
    private int _depositTermMonths;
    private decimal _monthRate;
    private bool _isCapitalization;

    private decimal _depositProfit;
    private decimal _finalAmount;
    private string _selectedCurrency;

    public ObservableCollection<string> Currencies
    {
        get
        {
            var currencies = new ObservableCollection<string> { "₴", "$", "€" };
            SelectedCurrency = currencies.First();
            return currencies;
        }
    }

    public ICommand CalculateButtonCommand { get; }

    public MainWindowViewModel(ICalculatorService calculatorService)
    {
        _calculatorService = calculatorService;
        CalculateButtonCommand = new RelayCommand(CalculateOnClick, _ => true);
    }

    public decimal DepositAmount
    {
        get => _depositAmount;
        set => SetField(ref _depositAmount, value);
    }

    public int DepositTermMonths
    {
        get => _depositTermMonths;
        set => SetField(ref _depositTermMonths, value);
    }

    public decimal MonthRate
    {
        get => _monthRate;
        set => SetField(ref _monthRate, value);
    }
    
    public bool IsCapitalization
    {
        get => _isCapitalization;
        set => SetField(ref _isCapitalization, value);
    }

    public decimal DepositProfit
    {
        get => _depositProfit;
        set => SetField(ref _depositProfit, value);
    }
    
    public decimal FinalAmount
    {
        get => _finalAmount;
        set => SetField(ref _finalAmount, value);
    }
    
    public string SelectedCurrency
    {
        get => _selectedCurrency;
        set => SetField(ref _selectedCurrency, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void CalculateOnClick(object sender)
    {
        if (DepositAmount == 0 || DepositTermMonths == 0 || MonthRate == 0) return;
        
        FinalAmount = _calculatorService
            .CalculateFinalAmount(DepositAmount, DepositTermMonths, MonthRate, IsCapitalization);
        DepositProfit = FinalAmount - DepositAmount;
    }
    
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}