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
    /// Interaction logic for ManageBudgetsWindow.xaml
    /// </summary>
    public partial class ManageBudgetsWindow : Window
    {
        // To display budgets from SQL db in the Combobox:
        public List<Budget> budgets = new List<Budget>();

        public ManageBudgetsWindow()
        {
            InitializeComponent();

            LoadBudgets();
            existingBudgets.ItemsSource = budgets;
        }

        /// <summary>
        /// A method that saves a list of all budgets that are stored in the SQL database in the budgets property of this class.
        /// </summary>
        public void LoadBudgets()
        {
            budgets = GlobalConfig.SQLConnection.GetBudgets();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
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
    }
}
