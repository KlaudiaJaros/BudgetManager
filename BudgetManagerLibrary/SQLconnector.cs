using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    public class SQLconnector : IDataConnection
    {
        // TODO - Make the method actually save to the database.
        /// <summary>
        /// Saves a new budget to the database.
        /// </summary>
        /// <param name="budget">The budget information.</param>
        /// <returns>The budget information, including the unique identifier.</returns>
        public Budget CreateBudget(Budget budget)
        {
            budget.Id = 1;
            return budget;
        }
    }
}
