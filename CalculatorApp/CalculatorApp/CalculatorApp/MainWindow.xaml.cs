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

namespace CalculatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double lastNumber, result;
        private SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentButton.Click += PercentButton_Click;
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        resultLabel.Content = lastNumber + newNumber;
                        break;
                    case SelectedOperator.Subtraction:
                        resultLabel.Content = lastNumber - newNumber;
                        break;
                    case SelectedOperator.Multiplication:
                        resultLabel.Content = lastNumber * newNumber;
                        break;
                    case SelectedOperator.Division:
                        resultLabel.Content = lastNumber / newNumber;
                        break;
                    default:
                        break;
                }
            }
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = (tempNumber / 100);
                if (lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            lastNumber = 0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multiplyButton)
            {
                selectedOperator = SelectedOperator.Multiplication;
            }
            if (sender == divideButton)
            {
                selectedOperator = SelectedOperator.Division;
            }
            if (sender == subtractButton)
            {
                selectedOperator = SelectedOperator.Subtraction;
            }
            if (sender == addButton)
            {
                selectedOperator = SelectedOperator.Addition;
            }
        }

        private void decimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultLabel.Content.ToString().Contains(","))
            {
                // Do nothing
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content},";
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            } else
            {
                resultLabel.Content = $"{resultLabel.Content.ToString()}{selectedValue}";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
}
