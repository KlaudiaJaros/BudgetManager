using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BudgetManagerLibrary
{
    public class SQLconnector : IDataConnection
    {
        /// <summary>
        /// Saves a new budget to the database.
        /// </summary>
        /// <param name="budget">The budget information.</param>
        /// <returns>The budget information, including the unique identifier.</returns>
        public Budget CreateBudget(Budget budget)
        {
            // establish the connection:
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("BudgetManagerDB")))
            {
                var p = new DynamicParameters();
                /* passing the parameters for the stored procedure in the server manager spBudget_Insert  */
                /* first, store all parameters to be passed in a var p: */
                p.Add("@budgetName", budget.Name);
                p.Add("@balance", budget.Balance);
                // we are getting the id back here:
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                /* using Execute method, pass the stored procedure name, var p (all of the parameters) and a command type: */
                connection.Execute("dbo.spBudget_Insert", p, commandType: CommandType.StoredProcedure);

                /* save the id in the budget using Get<dataType>(@SQL parameter name) : */
                budget.Id = p.Get<int>("@id");

                return budget;
            }

        }
    }
}
