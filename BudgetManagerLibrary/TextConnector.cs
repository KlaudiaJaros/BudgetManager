using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    public class TextConnector : IDataConnection
    {
        // TODO - Make it actually save to a text file.
        /// <summary>
        /// Saves a new budget to a text file.
        /// </summary>
        /// <param name="budget"> The budget information.</param>
        /// <returns> The budget information and a unique identifier. </returns>
        public Budget CreateBudget(Budget budget)
        {
            budget.Id = 1;
            return budget;
        }
    }
}
