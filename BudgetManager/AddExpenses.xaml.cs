using BudgetManagerLibrary;
using BudgetManagerLibrary.Business_Objects;
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
        private Budget Budget { get; set; } = new Budget(null, 0);
        public AddExpenses(Budget newBudget)
        {
            InitializeComponent();
            Budget = newBudget;
        }

        private void AddNewExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: connect frequency

            if (ValidateForm())
            {
                double amount = double.Parse(expenseAmount.Text);
                amount -= amount * 2; // ?
                Entry newExpense  = new Entry(expenseName.Text, amount, expenseCategory.Text, expenseDate.SelectedDate, Budget.Id );

                /* Create a expense entry for all existing connections: */
                // temporary expense instance:
                Entry ExpenseEntry = new Entry();
                // sql:
                ExpenseEntry = GlobalConfig.SQLConnection.SaveEntry(newExpense);
                // edit budget:
                decimal newBalance = Budget.Balance + Convert.ToDecimal(ExpenseEntry.Amount);
                GlobalConfig.SQLConnection.EditBudgetBalance(Budget.Id, newBalance);

                // text file:
                // pass the modified expense instance (sql added an unique ID to it) to the text file saver:
                GlobalConfig.TextFileConnection.SaveEntry(ExpenseEntry);
                // edit budget balance in the text file:
                GlobalConfig.TextFileConnection.EditBudgetBalance(Budget.Id, newBalance);

                Button anotherExpense = (Button)sender;
                if (anotherExpense.Name == "NewExpenseButton")
                {
                    // reset values:
                    expenseAmount.Text = "0";
                    expenseName.Text = "My Expense";
                    expenseCategory.SelectedItem = null;
                    expenseDate.SelectedDate = null;
                    expenseFrequency.SelectedItem = null;
                }
                else
                {
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();
                }
            }
        }

        /// <summary>
        /// A method that checks every field from the AddExpenses form and validates the user input. If the input is incorrect, an appropriate message box will be dispayed.
        /// </summary>
        /// <returns> True if the form was filled correctly, otherwise false. </returns>
        private bool ValidateForm()
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

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
