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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for BudgetViewer.xaml
    /// </summary>
    public partial class BudgetViewer : Window
    {
        private Budget Budget { get; set; } = new Budget(null, 0);
        private List<Entry> Entries = new List<Entry>();

        public BudgetViewer(Budget passedBudget)
        {
            InitializeComponent();

            Budget = passedBudget;
            budgetNameBox.Text = Budget.Name;
            budgetBalanceBox.Text = Budget.Balance.ToString("F");
            // display 'spent this month' :
            DateTime dateTime = DateTime.Now;
            decimal spent= GlobalConfig.SQLConnection.GetExpensesByMonth(dateTime.Month, Budget.Id);
            spent = 0 - spent;
            monthlySpend.Text = spent.ToString("F");

            decimal averageSpent = spent / dateTime.Day;
            averageDay.Text = averageSpent.ToString("F");

            LoadData();
            dataGrid.DataContext = Entries; 
        } 

        private void LoadData()
        {
            Entries = GlobalConfig.SQLConnection.GetEntriesByDate(Budget.Id);
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void AddExpensesButton_Click(object sender, RoutedEventArgs e)
        {
            AddExpenses win = new AddExpenses(Budget);
            win.Show();
            this.Close();
        }
        private void AddIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            AddIncome income = new AddIncome(Budget);
            income.Show();
            this.Close();
        }

        private void DeleteBudget_Click(object sender, RoutedEventArgs e)
        {
            // Configure the message box to be displayed:
            string messageBoxText = "Do you want to delete budget " + Budget.ToString() + "?";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box:
            MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results:
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button, call deletion methods:
                    GlobalConfig.SQLConnection.DeleteBudget(Budget.Id);
                    GlobalConfig.TextFileConnection.DeleteBudget(Budget.Id);
                    System.Windows.MessageBox.Show("Budget deleted successfully.");
                                    
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    break;
            }
        }
    }
}
