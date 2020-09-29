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
    public class TextConnector : IDataConnection
    {
        // csv/txt file name :
        private string fileName;

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

        public Budget SaveBudget(Budget budget)
        {
            fileName = "List of all budgets.csv";
            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            // convert budget into csv list :
            List<string> lines = new List<string>();
            lines.Add($"BUDGET,{ budget.Id },{ budget.Name },{budget.Balance}");

            File.AppendAllLines(path, lines);

        return budget;
        }

        public Entry SaveEntry(Entry entry)
        {
            fileName = "Budget no " + entry.BudgetID + ".csv";

            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            // convert expense into csv list :
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
        public void deleteEntry(Entry entry)
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

        // TODO: implement interface methods below

        public List<Budget> GetBudgets()
        {
            throw new NotImplementedException();
        }

        public List<Entry> GetEntries()
        {
            throw new NotImplementedException();
        }

        public List<Entry> GetEntriesByDate(int budgetId)
        {
            throw new NotImplementedException();
        }

        public decimal GetExpensesByMonth(int month, int budgetId)
        {
            throw new NotImplementedException();
        }
    }
}
