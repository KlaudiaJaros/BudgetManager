
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {           
            InitializeComponent();
        }

        private void manageBudgets_Click(object sender, RoutedEventArgs e)
        {
            // TODO: go to ManageBudgets Window
        }

        private void createNewBudget_Click(object sender, RoutedEventArgs e)
        {
            // TODO: go to CreateNewBudgetWindow

            //CreateNewBudgetWindow win2 = new CreateNewBudgetWindow();
            //this.Content = win2.Content;
            //win2.Show();
        }
    }
}
