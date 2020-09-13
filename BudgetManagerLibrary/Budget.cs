using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
     public class Budget
    {
        private string Name { get; set; }
        private double Balance { get; set; }
        public int Id { get; set; }
        private List<Expense> Expenses = new List<Expense>();

        public Budget(string name, double balance)
        {
            Name = name;
            Balance = balance;
        }
        public Budget(string name, double balance, int id)
        {
            Name = name;
            Balance = balance;
            Id = id;
        }

    }
}
