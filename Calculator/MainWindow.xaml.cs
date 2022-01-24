using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _lastNumber, _result;
        private SelectedOperator _selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            acBtn.Click += AcBtn_Click;
            ToggleNegativeBtn.Click += ToggleNegativeBtn_Click;
            percentBtn.Click += PercentBtn_Click;
            equalBtn.Click += EqualBtn_Click;
        }

        private void EqualBtn_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (!double.TryParse(resultLbl.Content.ToString(), out newNumber)) return;

            switch (_selectedOperator)
            {
                case SelectedOperator.Addition:
                    _result = SimpleMath.Add(_lastNumber, newNumber);
                    break;
                case SelectedOperator.Substraction:
                    _result = SimpleMath.Substract(_lastNumber, newNumber);
                    break;
                case SelectedOperator.Multiplication:
                    _result = SimpleMath.Multiply(_lastNumber, newNumber);
                    break;
                case SelectedOperator.Division:
                    _result = SimpleMath.Divide(_lastNumber, newNumber);
                    break;
            }

            _selectedOperator = SelectedOperator.Undefined;
            resultLbl.Content = _result;
        }

        private void PercentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(resultLbl.Content.ToString(), out _lastNumber)) return;

            _lastNumber /= 100;

            resultLbl.Content = _lastNumber;
        }

        private void ToggleNegativeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(resultLbl.Content.ToString(), out _lastNumber)) return;

            _lastNumber *= -1;

            resultLbl.Content = _lastNumber;
        }

        private void AcBtn_Click(object sender, RoutedEventArgs e)
        {
            resultLbl.Content = "0";
            _lastNumber = 0;
            _result = 0;
            _selectedOperator = SelectedOperator.Undefined;
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(resultLbl.Content.ToString(), out _lastNumber)) return;

            if (_selectedOperator != SelectedOperator.Undefined)
            {
                EqualBtn_Click(sender, e);
            }

            resultLbl.Content = 0;

            if (sender == devideBtn)
                _selectedOperator = SelectedOperator.Division;
            else if (sender == multiplicationBtn)
                _selectedOperator = SelectedOperator.Multiplication;
            else if (sender == minusBtn)
                _selectedOperator = SelectedOperator.Substraction;
            else if (sender == plusBtn)
                _selectedOperator = SelectedOperator.Addition;
        }

        private void dotBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLbl.Content.ToString().Contains("."))
                resultLbl.Content = $"{resultLbl.Content}.";
        }

        private void NumberBtn_Click(object sender, RoutedEventArgs e)
        {
            double currNum = 0;
            if (!double.TryParse(resultLbl.Content.ToString(), out currNum)) return;

            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (currNum == 0)
            {
                resultLbl.Content = $"{selectedValue}";
            }
            else
            {
                resultLbl.Content = $"{resultLbl.Content}{selectedValue}";
            }
        }
    }

    public enum SelectedOperator { Undefined, Addition, Substraction, Multiplication, Division }

    public class SimpleMath
    {
        public static double Add(double a, double b, params double[] nums)
        {
            double res = a + b;
            foreach (double num in nums) res += num;
            return res;
        }
        public static double Substract(double a, double b)
        {
            return a - b;
        }
        public static double Multiply(double a, double b, params double[] nums)
        {
            double res = a * b;
            foreach (double num in nums) res *= num;
            return res;
        }
        public static double Divide(double a, double b)
        {
            return a / b;
        }
    }
}
