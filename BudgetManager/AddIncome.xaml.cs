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
    /// Interaction logic for AddIncome.xaml
    /// </summary>
    public partial class AddIncome : Window
    {
        private Budget Budget = new Budget(null, 0);
        public static int id=1;
        public AddIncome(Budget newBudget)
        {
            InitializeComponent();
            Budget = newBudget;
            incomeDate.Value = DateTime.Now;
        }
        private void AddNewIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                double amount = double.Parse(incomeAmount.Text);
                Entry newIncome = new Entry(incomeName.Text, amount, incomeCategory.Text, incomeDate.Value, Budget.Id);

                /* Create a expense entry for all existing connections: */
                // temporary expense instance:
                Entry incomeEntry = new Entry();
                // edit budget balance (add Income to it) :
                decimal newBalance = Budget.Balance + Convert.ToDecimal(newIncome.Amount);
                Budget.Balance = newBalance;


                // sql and text:
                if (GlobalConfig.sqlConnection && GlobalConfig.textConnection)
                {
                    // sql:
                    incomeEntry = GlobalConfig.SQLConnection.SaveEntry(newIncome);
                    GlobalConfig.SQLConnection.EditBudgetBalance(Budget.Id, newBalance);
                    // text: pass the modified expense instance (sql added an unique ID to it) to the text file saver:
                    GlobalConfig.TextFileConnection.SaveEntry(incomeEntry);
                    // edit budget balance in the text file:
                    GlobalConfig.TextFileConnection.EditBudgetBalance(Budget.Id, newBalance);
                }
                // sql:
                else if (GlobalConfig.sqlConnection)
                {
                    incomeEntry = GlobalConfig.SQLConnection.SaveEntry(newIncome);
                    GlobalConfig.SQLConnection.EditBudgetBalance(Budget.Id, newBalance);

                }
                // text file:
                else if (GlobalConfig.textConnection)
                {
                    newIncome.Id = id;
                    id++;
                    GlobalConfig.TextFileConnection.SaveEntry(newIncome);
                    // edit budget balance in the text file:
                    GlobalConfig.TextFileConnection.EditBudgetBalance(Budget.Id, newBalance);
                }

                Button income = (Button)sender;
                if (income.Name == "NewIncomeButton")
                {
                    // reset values:
                    incomeAmount.Text = "0";
                    incomeName.Text = "My Income";
                    incomeDate.Value = DateTime.Now;
                }
                else
                {
                    BudgetViewer window = new BudgetViewer(Budget);
                    window.Show();
                    this.Close();
                }
            }
        }

        /// <summary>
        /// A method that checks every field from the AddIncome form and validates the user input. If the input is incorrect, an appropriate message box will be dispayed.
        /// </summary>
        /// <returns> True if the form was filled correctly, otherwise false. </returns>
        private bool ValidateForm()
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
            }

            if (incomeName.Text.Length == 0 || incomeName.Text.Length > 50)
            {
                output = false;
                MessageBox.Show("Invalid name. Please enter a valid name with 1-50 characters.");
            }
            if (incomeCategory.SelectedItem == null)
            {
                output = false;
                MessageBox.Show("Please select a category.");
            }
            if (incomeDate.Value == null)
            {
                output = false;
                MessageBox.Show("Please select a date.");
            }

            /* TODO: connect frequency in the future
            if (incomeFrequency.SelectedItem == null)
            {
                output = false;
                MessageBox.Show("Please choose frequency.");
            } */

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
