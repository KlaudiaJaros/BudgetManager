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
    /// Interaction logic for AddExpenses.xaml
    /// </summary>
    public partial class AddExpenses : Window
    {
        private int budgetId;
        public AddExpenses(int newBudgetId)
        {
            InitializeComponent();
            // TODO: connect the actual budget ID !!
            budgetId = newBudgetId;

        }

        private void finaliseExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: connect frequency

            if (validateForm())
            {
                double amount = double.Parse(expenseAmount.Text);
                Expense newExpense  = new Expense(expenseName.Text, amount, expenseCategory.Text, expenseDate.SelectedDate, budgetId );

                /* create a expense entry for all existing connections: */
                // temporary expense instance:
                Expense expenseEntry = new Expense();
                // sql:
                expenseEntry = GlobalConfig.SQLConnection.SaveExpense(newExpense);
                // text file:
                // pass the modified expense instance (sql added an unique ID to it) to the text file saver:
                GlobalConfig.TextFileConnection.SaveExpense(expenseEntry);

                // reset values:
                expenseAmount.Text = "0";
                expenseName.Text = "My Expense";
                expenseCategory.SelectedItem = null;
                expenseDate.SelectedDate = null;
                expenseFrequency.SelectedItem = null;

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
            bool amountValidNumber = double.TryParse(expenseAmount.Text, out amount);

            if (!amountValidNumber || amount<=0)
            {
                output = false;
                MessageBox.Show("Invalid amount. Please enter a valid number.");
                // double range
            }

            if (expenseName.Text.Length == 0 || expenseName.Text.Length > 50)
            {
                output = false;
                MessageBox.Show("Invalid name. Please enter a valid name with 1-50 characters.");
            }

            if (expenseCategory.SelectedItem == null)
            {
                output = false;
                MessageBox.Show("Please select a category.");
            }

            if (expenseDate.SelectedDate == null)
            {
                output = false;
                MessageBox.Show("Please select a date.");
            }

            if (expenseFrequency.SelectedItem == null)
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
