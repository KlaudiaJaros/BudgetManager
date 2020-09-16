using BudgetManagerLibrary;
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
using System.Windows.Shapes;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for AddIncome.xaml
    /// </summary>
    public partial class AddIncome : Window
    {
        private int budgetId;
        public AddIncome(int newBudgetId)
        {
            InitializeComponent();
            // TODO: connect the actual budget ID !!
            budgetId = newBudgetId;
        }
        private void finaliseButton_Click(object sender, RoutedEventArgs e)
        {


            if (validateForm())
            {
                double amount = double.Parse(incomeAmount.Text);
                Income newExpense = new Income(incomeName.Text, amount, incomeDate.SelectedDate, budgetId);

                /* create a expense entry for all existing connections: */
                // temporary expense instance:
                Income incomeEntry = new Income();
                // sql:
                incomeEntry = GlobalConfig.SQLConnection.SaveIncome(newExpense);
                // text file:
                // pass the modified expense instance (sql added an unique ID to it) to the text file saver:
                GlobalConfig.TextFileConnection.SaveIncome(incomeEntry);

                // reset values:
                incomeAmount.Text = "0";
                incomeName.Text = "My Income";
                incomeDate.SelectedDate = null;
                incomeFrequency.SelectedItem = null;


                // TODO: open ViewBudgetWindow
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
        }
        private bool validateForm()
        {
            bool output = true;
            double amount = 0;

            // TryParse takes in a string and tries to parse it into an integer. If successful, it will save the number in
            // the 'out' variable in this case 'amount' and the boolean will return true. If unsuccessful, boolean value becomes false.
            bool amountValidNumber = double.TryParse(incomeAmount.Text, out amount);

            if (!amountValidNumber || amount <= 0)
            {
                output = false;
                MessageBox.Show("Invalid amount. Please enter a valid number.");
                // double range
            }

            if (incomeName.Text.Length == 0 || incomeName.Text.Length > 50)
            {
                output = false;
                MessageBox.Show("Invalid name. Please enter a valid name with 1-50 characters.");
            }

            if (incomeDate.SelectedDate == null)
            {
                output = false;
                MessageBox.Show("Please select a date.");
            }

            if (incomeFrequency.SelectedItem == null)
            {
                output = false;
                MessageBox.Show("Please choose frequency.");
            }

            return output;
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
