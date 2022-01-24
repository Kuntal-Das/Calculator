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
        private double lastNumber, result;

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
            //throw new NotImplementedException();
        }

        private void PercentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(resultLbl.Content.ToString(), out lastNumber)) return;

            lastNumber /= 100;

            resultLbl.Content = lastNumber;
        }

        private void ToggleNegativeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(resultLbl.Content.ToString(), out lastNumber)) return;

            lastNumber *= -1;

            resultLbl.Content = lastNumber;
        }

        private void AcBtn_Click(object sender, RoutedEventArgs e)
        {
            resultLbl.Content = "0";
        }

        private void sevenBtn_Click(object sender, RoutedEventArgs e)
        {
            int result;
            if (!Int32.TryParse(resultLbl.Content.ToString(), out result)) return;

            if (result == 0)
            {
                resultLbl.Content = "7";
            }
            else
            {
                resultLbl.Content = $"{resultLbl.Content}7";
            }
        }
    }
}
