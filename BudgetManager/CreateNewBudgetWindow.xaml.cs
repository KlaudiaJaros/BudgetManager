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
        public CreateNewBudgetWindow()
        {
            InitializeComponent();
        }

        private void createBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            if (validateForm())
            {
                double balance = double.Parse(startingBalance.Text);
                Budget newBudget = new Budget(budgetName.Text, balance);

                foreach(IDataConnection db in GlobalConfig.Connections)
                {
                    db.CreateBudget(newBudget);
                }

                // reset values:
                startingBalance.Text = "0";
                budgetName.Text = "My budget";
            }
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
           // MainWindow main = new MainWindow();
            //this.Content = main.Content;
        }

        private bool validateForm()
        {
            bool output = true;
            int balance = 0;

            // TryParse takes in a string and tries to parse it into an integer. If successful, it will save the number in
            // the 'out' variable in this case 'balance' and the boolean will return true. If unsuccessful, boolean value becomes false.
            bool balanceValidNumber = int.TryParse(startingBalance.Text, out balance);
            
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
    }
}
