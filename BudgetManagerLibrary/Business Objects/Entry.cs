using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary.Business_Objects
{
    /// <summary>
    /// A class to store information about an entry to a budget. 
    /// </summary>
    public class Entry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Category { get; set; }
        public int BudgetID { get; set; }
        
        // constructors:
        public Entry(int id, string name, double amount, string category, DateTime? date, int budgetID)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            BudgetID = budgetID;
        }
        public Entry(string name, double amount, string category, DateTime? date, int budgetID)
        {
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            BudgetID = budgetID;
        }
        public Entry()
        {

        }
        /// <summary>
        /// Displays an entry by its name.
        /// </summary>
        /// <returns>Entry's name.</returns>
        public override string ToString()
        {
            string trimName = Name.TrimEnd();
            return trimName;
        }
    }
}
