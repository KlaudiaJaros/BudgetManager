using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    class Income
    {
        private string Name { get; set; }
        private double Amount { get; set; }
        private string Category { get; set; }
        private bool IsMonthly { get; set; }
        private bool IsYearly { get; set; }
        private int BudgetID { get; set; }

        public Income(string name, double amount, string category, int budget)
        {
            Name = name;
            Amount = amount;
            Category = category;
            BudgetID = budget;
        }
    }
}
