﻿using BudgetManagerLibrary;
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
        private Budget Budget = new Budget(null, 0); // budget to save the expenses to
        private static int id = 1; // id for text only connection
        public AddExpenses(Budget newBudget)
        {
            InitializeComponent();
            Budget = newBudget;
            expenseDate.Value = DateTime.Now;
        }

        private void AddNewExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                double amount = double.Parse(expenseAmount.Text);
                amount -= amount * 2; // expenses have a negative amount
                Entry newExpense  = new Entry(expenseName.Text, amount, expenseCategory.Text, expenseDate.Value, Budget.Id );

                /* Create a expense entry for all existing connections: */
                // temporary expense instance:
                Entry ExpenseEntry = new Entry();
                // edit budget:
                decimal newBalance = Budget.Balance + Convert.ToDecimal(newExpense.Amount);
                Budget.Balance = newBalance;

                // sql and text:
                if (GlobalConfig.sqlConnection && GlobalConfig.textConnection)
                {
                    // sql:
                    ExpenseEntry = GlobalConfig.SQLConnection.SaveEntry(newExpense);
                    GlobalConfig.SQLConnection.EditBudgetBalance(Budget.Id, newBalance);
                    // text: pass the modified expense instance (sql added an unique ID to it) to the text file saver:
                    GlobalConfig.TextFileConnection.SaveEntry(ExpenseEntry);
                    // edit budget balance in the text file:
                    GlobalConfig.TextFileConnection.EditBudgetBalance(Budget.Id, newBalance);
                }
                // sql:
                else if  (GlobalConfig.sqlConnection)
                {
                    ExpenseEntry = GlobalConfig.SQLConnection.SaveEntry(newExpense);
                    GlobalConfig.SQLConnection.EditBudgetBalance(Budget.Id, newBalance);
                 
                }
                // text file:
                else if (GlobalConfig.textConnection)
                {
                    newExpense.Id = id;
                    id++;
                    GlobalConfig.TextFileConnection.SaveEntry(newExpense);
                    // edit budget balance in the text file:
                    GlobalConfig.TextFileConnection.EditBudgetBalance(Budget.Id, newBalance);
                }

                
                Button anotherExpense = (Button)sender;
                if (anotherExpense.Name == "NewExpenseButton")
                {
                    // reset values:
                    expenseAmount.Text = "0";
                    expenseName.Text = "My Expense";
                    expenseCategory.SelectedItem = null;
                    expenseDate.Value = DateTime.Now;
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

            if (expenseDate.Value == null)
            {
                output = false;
                MessageBox.Show("Please select a date.");
            }

            /* TODO: connect frequency in the future
            if (expenseFrequency.SelectedItem == null)
            {
                output = false;
                MessageBox.Show("Please choose frequency.");
            } */

            return output;
        }

        /// <summary>
        /// Return button event handler.
        /// </summary>
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
