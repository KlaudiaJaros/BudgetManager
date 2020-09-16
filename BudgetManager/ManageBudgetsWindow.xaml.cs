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
        // display those in the combobox:
        public List<Budget> budgets = new List<Budget>();

        private int budgetId;

        public ManageBudgetsWindow()
        {
            InitializeComponent();

            //TemporaryData()
            LoadBudgets();
            existingBudgets.ItemsSource = budgets;

            
        }
        public void TemporaryData()
        {
            budgets.Add(new Budget("My budget", 50));
            budgets.Add(new Budget("Hello", 60));
        }

        public void LoadBudgets()
        {
            budgets = GlobalConfig.SQLConnection.GetBudgets();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: return to MainWindow
        }

        private void NewExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            Budget budget = new Budget(null, 0);
            if (existingBudgets.SelectedItem==null)
            {
                MessageBox.Show("You haven't selected a budget!");
            }
            else
            {
                budget = (Budget)existingBudgets.SelectedItem;
                AddExpenses win = new AddExpenses(budget.Id);
                this.Content = win.Content;
                win.Show();
                
            }


        }

        private void NewIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            Budget budget = new Budget(null, 0);
            if (existingBudgets.SelectedItem == null)
            {
                MessageBox.Show("You haven't selected a budget!");
            }
            else
            {
                budget = (Budget)existingBudgets.SelectedItem;
                // // TODO: AddIncome Window and pass the budget id
            }
        }

        private void ViewBudgetButton_Click(object sender, RoutedEventArgs e)
        {
            Budget budget = new Budget(null, 0);
            if (existingBudgets.SelectedItem == null)
            {
                MessageBox.Show("You haven't selected a budget!");
            }
            else
            {
                budget = (Budget)existingBudgets.SelectedItem;
                // // TODO: viewBudgetWindow with the selected budget
            }
        }
    }
}
