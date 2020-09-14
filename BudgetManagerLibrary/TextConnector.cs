using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace BudgetManagerLibrary 
{
    public class TextConnector : IDataConnection
    {
        // csv file :
        private const string fileName = "Budget.csv";

        public Budget CreateBudget(Budget budget)
        {
            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            
            // convert budget into csv list :
            List<string> lines = new List<string>();
            lines.Add($"{ budget.Id },{ budget.Name },{ budget.Balance}");

            // TODO: fill it in
            // check if the file exists:
            if (!File.Exists(path))
            {
               
            }
            
            // save to file:
            File.WriteAllLines(path, lines);

        return budget;
        }
    }
}
