
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
using System.Windows.Forms.VisualStyles;
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
    public partial class MainWindow 
    {
        // txt and sql connectors booleans, by default both are true:
        public static bool txt = true;
        public static bool sql = true;

        public MainWindow()
        {
            InitializeComponent();
            // update checkboxes:
            txtCheck.IsChecked = txt;
            dbCheck.IsChecked = sql;
        }

        private void ManageBudgets_Click(object sender, RoutedEventArgs e)
        {
            /* Initialise the database connections (sql database, text file): */
            GlobalConfig.InitialiseConnections(sql, txt);
            ManageBudgetsWindow window = new ManageBudgetsWindow();            
            window.Show();
            this.Close();
        }

        private void CreateNewBudget_Click(object sender, RoutedEventArgs e)
        {
            /* Initialise the database connections (sql database, text file): */
            GlobalConfig.InitialiseConnections(sql, txt);
            CreateNewBudgetWindow window = new CreateNewBudgetWindow();
            window.Show();
            this.Close();
        }

        /* Show/Hide text and database connections options. */
        private void OptionsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtCheck.IsVisible)
                txtCheck.Visibility = Visibility.Hidden;
            else
                txtCheck.Visibility = Visibility.Visible;

            if(dbCheck.IsVisible)
                dbCheck.Visibility = Visibility.Hidden;
            else
                dbCheck.Visibility = Visibility.Visible;
        }

        /* Text and database connection options as checkboxes: */
        private void txtCheck_Checked(object sender, RoutedEventArgs e)
        {
            txt = true;
        }

        private void dbCheck_Checked(object sender, RoutedEventArgs e)
        {
            sql = true;
        }

        private void dbCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            sql = false;
        }

        private void txtCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            txt = false;
        }
    }
}
