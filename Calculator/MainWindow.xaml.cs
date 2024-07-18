using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; // Для TextCompositionEventArgs

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private string input = string.Empty;
        private double result = 0.0;
        private char lastOperation = ' ';
        private string operationHistory = string.Empty;
        private bool isResultDisplayed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string buttonContent = button.Content.ToString();

            if (IsValidInput(buttonContent))
            {
                if (isResultDisplayed)
                {
                    input = result.ToString();
                    isResultDisplayed = false;
                }

                if (buttonContent == "=")
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        double num;
                        double.TryParse(input, out num);

                        PerformOperation(num);

                        operationHistory += $"{input} = {result}";
                        ShowBox.Text = operationHistory;
                        ResultBox.Text = result.ToString();

                        input = string.Empty;
                        lastOperation = ' ';
                        isResultDisplayed = true;
                        operationHistory = string.Empty;
                    }
                }
                else if (buttonContent == "C")
                {
                    Clear();
                }
                else
                {
                    if (lastOperation == ' ')
                    {
                        result = 0.0;
                    }

                    input += buttonContent;
                    ResultBox.Text = input;
                }
            }
        }

        private void PerformOperation(double num)
        {
            switch (lastOperation)
            {
                case '+':
                    result += num;
                    break;
                case '-':
                    result -= num;
                    break;
                case 'X':
                    result *= num;
                    break;
                case '/':
                    if (num != 0)
                        result /= num;
                    else
                        ResultBox.Text = "Division by Zero Error!";
                    break;
                default:
                    result = num;
                    break;
            }
        }

        private void Clear()
        {
            input = string.Empty;
            result = 0.0;
            lastOperation = ' ';
            operationHistory = string.Empty;
            ShowBox.Text = string.Empty;
            ResultBox.Text = string.Empty;
            isResultDisplayed = false;
        }

        private bool IsValidInput(string input)
        {
            // Проверка, что ввод является числом, оператором или '=' (для вычисления)
            double num;
            return double.TryParse(input, out num) || "+-X/=".Contains(input);
        }

        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "." || "+-X/=".Contains(e.Text)))
            {
                e.Handled = true;
            }
        }
    }
}
