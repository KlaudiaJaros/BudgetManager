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
using System.Xml.Schema;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for ManageBudgetsWindow.xaml
    /// </summary>
    public partial class ManageBudgetsWindow : Window
    {
        // To display budgets from the data connection in the Combobox:
        private List<Budget> budgets = new List<Budget>();

        public ManageBudgetsWindow()
        {
            InitializeComponent();
            // Display information about the current connection:
            if (MainWindow.sql && MainWindow.txt)
                connectionInfo.Text = "Database and TextFile Connection";
            else if (MainWindow.txt)
                connectionInfo.Text = "TextFile Connection";
            else if (MainWindow.sql)
                connectionInfo.Text = "Database Connection";
            else
            {
                connectionInfo.Text = "No connection!";
                connectionInfo.Foreground = Brushes.Red;
            }
            // Load data:    
            LoadBudgets();
            existingBudgets.ItemsSource = budgets;
        }

        /// <summary>
        /// A method that gets a list of all budgets that are stored in the SQL database or txt file and saves it in the budgets list property of this class.
        /// Note: if a budget was saved in txt mode only, it won't be visible in txt and db mode!
        /// </summary>
        private void LoadBudgets()
        {
            if (GlobalConfig.sqlConnection && GlobalConfig.textConnection)
                budgets = GlobalConfig.SQLConnection.GetBudgets();
            else if (GlobalConfig.textConnection)
                budgets = GlobalConfig.TextFileConnection.GetBudgets();
            else if (GlobalConfig.sqlConnection)
                budgets = GlobalConfig.SQLConnection.GetBudgets();
        }

        /// <summary>
        /// A method that opens AddExpenses window provided the user selected a budget from the ComboBox.
        /// </summary>
        private void NewExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            Budget Budget = new Budget(null, 0);
            if (existingBudgets.SelectedItem==null)
            {
                MessageBox.Show("You haven't selected a budget!");
            }
            else
            {
                Budget = (Budget)existingBudgets.SelectedItem;
                AddExpenses win = new AddExpenses(Budget);
                win.Show();
                this.Close();  
            }
        }
        /// <summary>
        /// A method that opens AddIncome window provided the user selected a budget from the ComboBox.
        /// </summary>
        private void NewIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            Budget Budget = new Budget(null, 0);
            if (existingBudgets.SelectedItem == null)
            {
                MessageBox.Show("You haven't selected a budget!");
            }
            else
            {
                Budget = (Budget)existingBudgets.SelectedItem;
                AddIncome win = new AddIncome(Budget);
                win.Show();
                this.Close();
            }
        }

        /// <summary>
        /// A method that opens BudgetViewer window provided the user selected a budget from the ComboBox.
        /// </summary>
        private void ViewBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            Budget Budget = new Budget(null, 0);
            if (existingBudgets.SelectedItem == null)
            {
                MessageBox.Show("You haven't selected a budget!");
            }
            else
            {
                Budget = (Budget)existingBudgets.SelectedItem;
                BudgetViewer view = new BudgetViewer(Budget);
                view.Show();
                this.Close();
            }
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
