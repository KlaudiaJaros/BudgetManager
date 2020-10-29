using BudgetManagerLibrary.Business_Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BudgetManagerLibrary 
{
    /// <summary>
    /// A class to manage writing and reading to and from a text file.
    /// </summary>
    public class TextConnector : IDataConnection
    {
        private string fileName;

        /// <summary>
        /// Delete a budget from a text file by providing its ID. This method deletes individual budget's file and erases it from the list of all budgets.
        /// </summary>
        /// <param name="budgetId">Budget ID</param>
        public void DeleteBudget(int budgetId)
        {
            // delete budget's text file with expenses and incomes:
            fileName = "Budget no " + budgetId + ".csv";
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            // delete budget entry from all budgets txt file:
            fileName = "List of all budgets.csv";
            path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            
            if (File.Exists(path))
            {
                string targetLine = "BUDGET," + budgetId.ToString();
                string[] lines = File.ReadAllLines(path);
                List<string> newLines = new List<string>();

                foreach (string line in lines)
                {
                    if (!line.Contains(targetLine))
                    {
                        newLines.Add(line);
                    }
                }
                File.Delete(path);
                File.AppendAllLines(path, newLines);
            }
        }
        /// <summary>
        /// Edit budget's balance in the list of all budgets.
        /// </summary>
        /// <param name="budgetId">Budget's ID</param>
        /// <param name="budgetBalance">New budget balance</param>
        public void EditBudgetBalance(int budgetId, decimal budgetBalance)
        {
            fileName = "List of all budgets.csv";
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            if (File.Exists(path))
            {
                string search = "BUDGET," + budgetId.ToString();
                string[] lines = File.ReadAllLines(path);
                List<string> newLines = new List<string>();

                foreach (string line in lines)
                {
                    if (!line.Contains(search))
                    {
                        newLines.Add(line);
                    }
                    else if (line.Contains(search))
                    {
                        string[] words = line.Split(',');
                        newLines.Add($"BUDGET,{budgetId},{words[2]},{budgetBalance}");
                    }
                }
                File.Delete(path);
                File.AppendAllLines(path, newLines);
            }
        }

        /// <summary>
        /// Save a new budget to a text file and add it to the list of all budgets.
        /// </summary>
        /// <param name="budget">Budget object</param>
        /// <returns>Saved Budget object to save</returns>
        public Budget SaveBudget(Budget budget)
        {
            fileName = "List of all budgets.csv";
            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            // convert budget into csv list and save to a file:
            List<string> lines = new List<string>();
            lines.Add($"BUDGET,{ budget.Id },{ budget.Name },{budget.Balance}");
            if (!File.Exists(path))
            {
                File.AppendAllLines(path, lines);
            }
            else
            {
                File.AppendAllLines(path, lines);
            }
             return budget;
        }
        /// <summary>
        /// Save a new Entry to a budget file.
        /// </summary>
        /// <param name="entry">Entry object to save</param>
        /// <returns>Saved Entry object</returns>
        public Entry SaveEntry(Entry entry)
        {
            fileName = "Budget no " + entry.BudgetID + ".csv";

            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            // convert entry into csv list :
            List<string> lines = new List<string>();
            if (entry.Amount>0)
            {
                lines.Add($"INCOME,{ entry.Id },{ entry.Name },{ entry.Amount},{ entry.Category},{ entry.Date},{ entry.BudgetID}");
            }
            else
            {
                lines.Add($"EXPENSE,{ entry.Id },{ entry.Name },{ entry.Amount},{ entry.Category},{ entry.Date},{ entry.BudgetID}");
            }            
            // save to file:
            File.AppendAllLines(path, lines);
            return entry;
        }
        /// <summary>
        /// Delete a specified Entry from budget's txt file.
        /// </summary>
        /// <param name="entry">Entry object to be deleted</param>
        public void DeleteEntry(Entry entry)
        {
            fileName = "Budget no " + entry.BudgetID + ".csv";
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            if (File.Exists(path))
            {
                string targetLineExpense = "EXPENSE," + entry.Id.ToString();
                string targetLineIncome = "INCOME," + entry.Id.ToString();
                string[] lines = File.ReadAllLines(path);
                List<string> newLines = new List<string>();

                foreach (string line in lines)
                {
                    if (!line.Contains(targetLineExpense) & !line.Contains(targetLineIncome))
                    {
                        newLines.Add(line);
                    }
                }
                File.Delete(path);
                File.AppendAllLines(path, newLines);
            }
        }
        /// <summary>
        /// Get a list of budgets currently saved using text files.
        /// </summary>
        /// <returns>A list of existing budgets in the text file connection</returns>
        public List<Budget> GetBudgets()
        {
            List<Budget> budgets = new List<Budget>();

            fileName = "List of all budgets.csv";
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] separated = line.Split(',');
                    Budget b = new Budget(Int32.Parse(separated[1]), separated[2], decimal.Parse(separated[3]));
                    budgets.Add(b);
                }
                return budgets;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get a list of entries from a specified budget sorted by date (descending).
        /// </summary>
        /// <param name="budgetId">Budget's ID</param>
        /// <returns>A list of Entry objects</returns>
        public List<Entry> GetEntriesByDate(int budgetId)
        {
            List<Entry> entries = new List<Entry>();

            fileName = "Budget no " + budgetId.ToString() + ".csv";
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] separated = line.Split(',');
                    Entry e = new Entry(Int32.Parse(separated[1]), separated[2], double.Parse(separated[3]), separated[4], DateTime.Parse(separated[5]), Int32.Parse(separated[6]));
                    // TODO: sort by date
                    entries.Add(e);
                }
                return entries;
            }
            else
            {
                return null;
            }
        }
    
        /// <summary>
        /// Return a total spent by month from a specific budget.
        /// </summary>
        /// <param name="month">Month described using an integer value</param>
        /// <param name="budgetId">Budget's ID</param>
        /// <returns></returns>
        public decimal GetSpendByMonth(int month, int budgetId)
        {
            fileName = "Budget no " + budgetId.ToString() + ".csv";
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            decimal total = 0;

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    string[] separated = line.Split(',');
                    string[] date = separated[5].Split('/');
                    if (date[1].Equals(month.ToString()) || date[1].Equals("0" + month.ToString()))
                    {
                        decimal amount = decimal.Parse(separated[3]);
                        if (amount<0)
                            total += amount;
                    }
                }
                return total;
            }
            else
                return 0;
        }
    }
}
