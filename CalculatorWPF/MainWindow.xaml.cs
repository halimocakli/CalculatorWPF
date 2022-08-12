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

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double lastNumber;
        private double result;

        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();

            //C_Button.Click += C_Button_Click;
            //NegativePositiveButton.Click += NegativePositiveButton_Click;
            //PercentageButton.Click += PercentageButton_Click;
            //EqualButton.Click += PercentageButton_Click;
        }

        private void C_Button_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBlock.Text = "0";
            result = 0;
            lastNumber = 0;
        }

        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            int selectedNumber = int.Parse((sender as Button).Uid.ToString());

            if (ResultTextBlock.Text.ToString() == "0")
            {
                ResultTextBlock.Text = $"{selectedNumber}";
            }
            else
            {
                ResultTextBlock.Text = $"{ResultTextBlock.Text}{selectedNumber}";
            }
        }

        private void NegativePositiveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(ResultTextBlock.Text.ToString(), out lastNumber))
                {
                    lastNumber = lastNumber * (-1);
                    ResultTextBlock.Text = lastNumber.ToString();
                }
            }

            catch (InvalidCastException)
            {
                Console.WriteLine("Cannot convert the value to double.");
            }
        }

        private void CommaButtonClick(object sender, RoutedEventArgs e)
        {
            if (ResultTextBlock.Text.ToString().Contains(","))
            {
                // Bu if koşulu sonrasında herhangi bir işlem yapılmasını istemiyoruz
            }
            else
            {
                ResultTextBlock.Text = $"{ResultTextBlock.Text},";
            }
        }

        private void PercentageButtonClick(object sender, RoutedEventArgs e)
        {
            // 50 + 5% = 50 + 2.5 = 52.5
            // 80 + 10% = 80 + 8 = 88
            try
            {
                double tempNumber;

                if (double.TryParse(ResultTextBlock.Text.ToString(), out tempNumber))
                {
                    tempNumber /= 100;

                    if (lastNumber != 0)
                    {
                        tempNumber *= lastNumber;
                    }

                    ResultTextBlock.Text = tempNumber.ToString();
                }
            }

            catch (InvalidCastException)
            {
                Console.WriteLine("Cannot convert the value to double.");
            }
        }
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double newNumber;

                if (double.TryParse(ResultTextBlock.Text.ToString(), out newNumber))
                {
                    switch (selectedOperator)
                    {
                        case SelectedOperator.Addition:
                            result = SimpleMath.Add(lastNumber, newNumber);
                            break;
                        case SelectedOperator.Substruction:
                            result = SimpleMath.Substruct(lastNumber, newNumber);
                            break;
                        case SelectedOperator.Multiplication:
                            result = SimpleMath.Multiply(lastNumber, newNumber);
                            break;
                        case SelectedOperator.Division:
                            result = SimpleMath.Divide(lastNumber, newNumber);
                            break;
                        default:
                            break;
                    }

                    ResultTextBlock.Text = result.ToString();
                }
            }

            catch (InvalidCastException)
            {
                Console.WriteLine("Cannot convert the value to double.");
            }
        }
        private void OperationButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Matematiksel operatörlerin string'ten double'a dönüşümünü kontrol ediyoruz. 
                if (double.TryParse(ResultTextBlock.Text.ToString(), out lastNumber))
                {
                    ResultTextBlock.Text = "0";
                }

                if (sender == MultiplicationButton)
                {
                    selectedOperator = SelectedOperator.Multiplication;
                }

                if (sender == DivisionButton)
                {
                    selectedOperator = SelectedOperator.Division;
                }

                if ((sender as Button).Uid == "80")
                {
                    selectedOperator = SelectedOperator.Addition;
                }

                if (sender == SubstructionButton)
                {
                    selectedOperator = SelectedOperator.Substruction;
                }
            }

            catch (InvalidCastException)
            {
                Console.WriteLine("Cannot convert the string value to double.");
            }
        }
    }

    // Matematiksel operasyonları temsil etmek üzere oluşturulan Enumarator yapısı
    public enum SelectedOperator
    {
        Addition,
        Substruction,
        Multiplication,
        Division
    }
}
