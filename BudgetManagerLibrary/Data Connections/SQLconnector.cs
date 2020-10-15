using BudgetManagerLibrary.Business_Objects;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BudgetManagerLibrary
{
    public class SQLconnector : IDataConnection
    {
        // CHANGE DATABASE NAME if using a different one:
        private const string DatabaseName = "BudgetManagerDB";


        /// <summary>
        /// Saves a new budget to the database.
        /// </summary>
        /// <param name="budget">The budget information.</param>
        /// <returns>The budget information, including the unique identifier.</returns>
        public Budget SaveBudget(Budget budget)
        {
            // establish the connection:
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
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

        public List<Budget> GetBudgets()
        {
            List<Budget> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
            {
                output = connection.Query<Budget>("dbo.spBudget_GetAll").ToList();
            }
            return output;
        }

        public void EditBudgetBalance(int budgetId, decimal budgetBalance)
        {
            // establish the connection:
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
            {
                var p = new DynamicParameters();
                /* first, store all parameters to be passed in a var p: */
                p.Add("@budgetId", budgetId);
                p.Add("@newBalance", budgetBalance);

                /* using Execute method, pass the stored procedure name, var p (all of the parameters) and a command type: */
                connection.Execute("dbo.spEditBudgetBalance", p, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteBudget(int budgetId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@delBudgetId", budgetId);

                connection.Execute("dbo.spDeleteBudgetAll", p, commandType: CommandType.StoredProcedure);
            }
        }
        public List<Entry> GetEntries()
        {
            List<Entry> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
            {
                output = connection.Query<Entry>("dbo.spEntry_GetAll").ToList();
            }
            return output;
        }

        public List<Entry> GetEntriesByDate(int budgetId)
        {
            List<Entry> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
            {
                var p = new DynamicParameters();
                /* first, store all parameters to be passed in a var p: */
                p.Add("@budgetId", budgetId);

                output = connection.Query<Entry>("dbo.spEntry_GetByDate", p, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public Entry SaveEntry(Entry entry)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@entryName", entry.Name);
                p.Add("@entryAmount", entry.Amount);
                p.Add("@entryCategory", entry.Category);
                p.Add("@entryDate", entry.Date);
                p.Add("@entryBudgetId", entry.BudgetID);
                // get the ID back:
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spEntry_Insert", p, commandType: CommandType.StoredProcedure);
                entry.Id = p.Get<int>("@id");

                return entry;
            }
        }

        public decimal GetSpendByMonth(int month, int budgetId)
        {
            decimal monthlySpent = 0;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@month", month);
                p.Add("@budgetId", budgetId);
                p.Add("@expenses", 0, dbType: DbType.Decimal, direction: ParameterDirection.Output);

                connection.Execute("dbo.spGetExpensesByMonth", p, commandType: CommandType.StoredProcedure);
                try
                {
                    monthlySpent = p.Get<decimal>("@expenses");
                    return monthlySpent;
                }
                catch
                {
                    return 0;
                }                
            }
        }

        public void DeleteEntry(Entry entry )
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(DatabaseName)))
            {
                var p = new DynamicParameters();
                p.Add("@delEntryId", entry.Id);

                connection.Execute("dbo.spDeleteEntry", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
