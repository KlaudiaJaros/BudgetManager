using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    class Expense
    {
        private string Name { get; set; }
        private double Amount { get; set; }
        private string Category { get; set; }
        private bool IsMonthly { get; set; }
        private bool IsYearly { get; set; }
    }
}
