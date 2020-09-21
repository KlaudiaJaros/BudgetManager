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
        private Budget budget { get; set; } = new Budget(null, 0);
        public BudgetViewer(Budget passedBudget)
        {
            InitializeComponent();

            budget = passedBudget;
            budgetNameBox.Text = budget.Name;
            budgetBalanceBox.Text = budget.Balance.ToString();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: return to MainWindow
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void addExpensesButton_Click(object sender, RoutedEventArgs e)
        {
            AddExpenses win = new AddExpenses(budget);
            win.Show();
            this.Close();
            // TODO: fix the window connection
        }

        private void deleteBudget_Click(object sender, RoutedEventArgs e)
        {
            // Configure the message box to be displayed
            string messageBoxText = "Do you want to delete budget " + budget.ToString() + "?";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    GlobalConfig.SQLConnection.DeleteBudget(budget.Id);
                    GlobalConfig.TextFileConnection.DeleteBudget(budget.Id);
                    
                    System.Windows.MessageBox.Show("Budget deleted successfully.");
                                    
                    // TODO: return to MainWindow
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();

                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    // ...
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    // ...
                    break;
            }
        }

        
    }
}
