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
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
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
                AddExpenses win = new AddExpenses(budget);
                win.Show();
                this.Close();  
                // TODO: fix the window connection
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
                AddIncome win = new AddIncome(budget);
                win.Show();
                this.Close();
                    //TODO: fix the window connection
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
                BudgetViewer view = new BudgetViewer(budget);
                view.Show();
                this.Close();
            }
        }
    }
}
