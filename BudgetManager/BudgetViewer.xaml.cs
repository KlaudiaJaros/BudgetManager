﻿using BudgetManagerLibrary;
using BudgetManagerLibrary.Business_Objects;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            


            decimal[] spendByCategory = new decimal[12];
            double temp = 0;
            string maxSpentCategory = "";

            foreach (Entry e in Entries)
            {
                string category = e.Category;
                if (category != null)
                    category = category.TrimEnd();
                switch (category)
                {
                    case "Accommodation":
                        spendByCategory[0] = spendByCategory[0] +  (Decimal)e.Amount;
                        break;
                    case "Phone, Internet, TV":
                        spendByCategory[1] = spendByCategory[1] + (Decimal)e.Amount;
                        break;
                    case "Electricity and gas":
                        spendByCategory[2] = spendByCategory[2] + (Decimal)e.Amount;
                        break;
                    case "Groceries":
                        spendByCategory[3] = spendByCategory[3] + (Decimal)e.Amount;
                        break;
                    case "Shopping":
                        spendByCategory[4] = spendByCategory[4] + (Decimal)e.Amount;
                        break;
                    case "Eating Out":
                        spendByCategory[5] = spendByCategory[5] + (Decimal)e.Amount;
                        break;
                    case "Nights out":
                        spendByCategory[6] = spendByCategory[6] + (Decimal)e.Amount;
                        break;
                    case "Transport":
                        spendByCategory[7] = spendByCategory[7] + (Decimal)e.Amount;
                        break;
                    case "Holiday":
                        spendByCategory[8] = spendByCategory[8] + (Decimal)e.Amount;
                        break;
                    case "Entertainment":
                        spendByCategory[9] = spendByCategory[9] + (Decimal)e.Amount;
                        break;
                    case "Sports and Wellbeing":
                        spendByCategory[10] = spendByCategory[10] + (Decimal)e.Amount;
                        break;
                    case "Other payments":
                        spendByCategory[11] = spendByCategory[11] + (Decimal)e.Amount;
                        break;
                    default:
                        spendByCategory[11] = spendByCategory[11] + (Decimal)e.Amount;
                        break;

                }

                if (e.Amount < temp)
                {
                    temp = e.Amount;
                    maxSpentCategory = e.Category;
                }
            }
            
            maxCategory.Text = maxSpentCategory; // maybe add the value too? but must change the layout

            SeriesCollection piechartData = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Accommodation",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(0)},
                    DataLabels = true,
                    LabelPoint = labelPoint,                    

                },
                new PieSeries
                {
                    Title = "Phone, Internet, TV",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(1)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Electricity and gas",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(2)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Groceries",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(3)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Shopping",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(4)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Eating Out",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(5)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Nights Out",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(6)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Transport",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(7)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Holiday",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(8)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Entertainment",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(9)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Sports and Wellbeing",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(10)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Other payments",
                    Values = new ChartValues<decimal> {spendByCategory.ElementAt(11)},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
            };
            pieChart.Series = piechartData;
            // colours settings:
            pieChart.SeriesColors = new ColorsCollection();
            pieChart.SeriesColors.Add(Color.FromRgb(255, 210, 33)); // acc
            pieChart.SeriesColors.Add(Color.FromRgb(66, 245, 236)); // phone
            pieChart.SeriesColors.Add(Color.FromRgb(250, 57, 238)); // electr
            pieChart.SeriesColors.Add(Color.FromRgb(129, 255, 125)); // grocc
            pieChart.SeriesColors.Add(Color.FromRgb(112, 138, 255)); // shopp
            pieChart.SeriesColors.Add(Color.FromRgb(250, 88, 60)); // eating
            pieChart.SeriesColors.Add(Color.FromRgb(133, 51, 255)); // nights
            pieChart.SeriesColors.Add(Color.FromRgb(175, 219, 26)); // transport
            pieChart.SeriesColors.Add(Color.FromRgb(250, 131, 20)); // holiday
            pieChart.SeriesColors.Add(Color.FromRgb(255, 84, 101)); //enter
            pieChart.SeriesColors.Add(Color.FromRgb(212, 156, 255)); // sport
            pieChart.SeriesColors.Add(Color.FromRgb(154, 156, 161)); //other

        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
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

        private void DeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            bool deleteEntry = false;

            // Configure the message box to be displayed:
            string messageBoxText = "Are you sure you want to delete this entry? Note: the budget balance and your summary will be affected.";
            string caption = "Warning";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box:
            MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results:
            switch (result)
            {
                case MessageBoxResult.Yes:
                    deleteEntry = true;
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    break;
            }

            var selectedCells = dataGrid.SelectedCells;
            int index = dataGrid.SelectedIndex;
            if (selectedCells.Count==5 & deleteEntry)
            {
                int entryId = 0;
                var entry = (Entry)dataGrid.SelectedCells.ElementAt(4).Item;
                entryId = entry.Id;
                Console.WriteLine(entryId);
                GlobalConfig.SQLConnection.deleteEntry(entryId);
                // edit budget balance:
                decimal newBalance = Budget.Balance - (Decimal)entry.Amount;
                GlobalConfig.SQLConnection.EditBudgetBalance(entry.BudgetID, newBalance);
                Budget.Balance = newBalance;
                budgetBalanceBox.Text = Budget.Balance.ToString("F");
                // edit datagrid display:
                Entries.RemoveAt(index);
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = Entries;
            }
            else if (deleteEntry)
            {
                System.Windows.MessageBox.Show("Please select a row with the entry you wish to delete.");
            }            
        }
    }
}
