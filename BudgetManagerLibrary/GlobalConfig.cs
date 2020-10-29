using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BudgetManagerLibrary
{
    /// <summary>
    /// This is a public static class with specific configurations for this application.
    /// </summary>
    public static class GlobalConfig
    {
        /* Properties to store a database connection and a text file connection, because this app has the option to save to both: */
        public static IDataConnection SQLConnection { get; private set; }
        public static IDataConnection TextFileConnection { get; private set; }
        /* Booleans to easily check what connections are established: */
        public static bool sqlConnection;
        public static bool textConnection;

        /// <summary>
        /// Based on which parameter is true, this method establishes either a database connection or a text file connection.
        /// You can change this method parameters to add other sources, for now just a database and a text file.
        /// </summary>
        public static void InitialiseConnections(bool SQLBool, bool textFileBool)
        {
            // separate if statements, because if both parameters are true, the app will save to both:
            if (SQLBool)
            {
                SQLconnector sql = new SQLconnector();
                SQLConnection = sql;
                sqlConnection = true;
            }
            if (textFileBool)
            {
                TextConnector text = new TextConnector();
                TextFileConnection= text;
                textConnection = true;
            }
        }

        /// <summary>
        /// Returns a connection string from App.config.
        /// </summary>
        /// <param name="Connection Name">In this case, a database name you want to connect to.</param>
        /// <returns> Connection string. </returns>
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
