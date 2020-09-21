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
    /// Interaction logic for CreateNewBudgetWindow.xaml
    /// </summary>
    public partial class CreateNewBudgetWindow : Window
    {
        private bool addExpense;

        public CreateNewBudgetWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A method that creates a budget and saves it to a SQL db and a text file provided the form was filled correctly.
        /// </summary>
        private void CreateBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                decimal balance = decimal.Parse(startingBalance.Text);
                Budget newBudget = new Budget(budgetName.Text, balance);

                /* Create a budget entry for all existing connections: */
                // temporary budget instance:
                Budget budgetEntry = new Budget(null, 0);
                // sql:
                budgetEntry = GlobalConfig.SQLConnection.SaveBudget(newBudget);
                // text file:
                // pass the modified budget instance (sql added an unique ID to it) to the text file saver:
                GlobalConfig.TextFileConnection.SaveBudget(budgetEntry);

                // reset values:
                startingBalance.Text = "0";
                budgetName.Text = "My Budget";

                if (addExpense)
                {
                    AddExpenses window = new AddExpenses(budgetEntry);
                    window.Show();
                    this.Close();
                }
                else
                {
                    BudgetViewer view = new BudgetViewer(budgetEntry);
                    view.Show();
                    this.Close();
                }
            }
        }

        /// <summary>
        /// A method that checks every field from the CreateNewBudgetWindow form and validates the user input. If the input is incorrect, an appropriate message box will be dispayed.
        /// </summary>
        /// <returns> True if the form was filled correctly, otherwise false. </returns>
        private bool ValidateForm()
        {
            bool output = true;
            double balance = 0;

            // TryParse takes in a string and tries to parse it into an integer. If successful, it will save the number in
            // the 'out' variable in this case 'balance' and the boolean will return true. If unsuccessful, boolean value becomes false.
            bool balanceValidNumber = double.TryParse(startingBalance.Text, out balance);
            
            if (!balanceValidNumber)
            {
                output = false;
                MessageBox.Show("Invalid starting balance. Please enter a valid number.");
            }
            // assumption: balance can be negative

            if (budgetName.Text.Length == 0 || budgetName.Text.Length > 100)
            {
                output = false;
                MessageBox.Show("Invalid name. Please enter a valid name with 1-100 characters.");
            }
            return output;
        }

        private void AddExpensesTrue_Checked(object sender, RoutedEventArgs e)
        {
            addExpense = true;
        }

        private void AddExpensesFalse_Checked(object sender, RoutedEventArgs e)
        {
            addExpense = false;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
