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

        private void createBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            if (validateForm())
            {
                decimal balance = decimal.Parse(startingBalance.Text);
                Budget newBudget = new Budget(budgetName.Text, balance);

                /* create a budget entry for all existing connections: */
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
                    // TODO: go to the AddExpenses window
                    AddExpenses window = new AddExpenses(budgetEntry.Id);
                    window.Show();
                    this.Close();
                    
                }
                else
                {
                    // TODO: go to the BudgetViewer window
                    
                }

            }
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: go to the Main window
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();

        }

        private bool validateForm()
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
                // integer range: -2,147,483,648 to 2,147,483,647
            }
            // assumption: balance can be negative

            if (budgetName.Text.Length == 0 || budgetName.Text.Length > 100)
            {
                output = false;
                MessageBox.Show("Invalid name. Please enter a valid name with 1-100 characters.");
            }

            return output;
        }

        private void addExpensesTrue_Checked(object sender, RoutedEventArgs e)
        {
            addExpense = true;
        }

        private void addExpensesFalse_Checked(object sender, RoutedEventArgs e)
        {
            addExpense = false;
        }
    }
}
