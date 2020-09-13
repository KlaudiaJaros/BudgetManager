using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BudgetManagerLibrary
{
    /// <summary>
    /// This is a public static class with all configurations. Visible to anyone.
    /// </summary>
    public static class GlobalConfig
    {
        /// <summary>
        /// This is a list of anything that implements the IDataConnection interface. In this case, I am storing database connections and 
        /// text files connections, because I want my app to have the option to save to both.
        /// You want the list setter to be private, otherwise anyone could change it.
        /// </summary>
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        /// <summary>
        /// Based on which parameter is true, this method establishes either a database connection or a text file connection.
        /// You can change this method parameters to add other sources, for now just a database and a text file.
        /// </summary>
        public static void InitialiseConnections (bool database, bool textFiles)
        {
            // separate if statements, because if both parameters are true, the app will save to both, which I want 
            if (database)
            {
                // TODO - set up the sql connector properly
                SQLconnector sql = new SQLconnector();
                Connections.Add(sql);
            }
            if (textFiles)
            {
                // TODO - create the text connection
                TextConnector text = new TextConnector();
                Connections.Add(text);
            }
        }

        /// <summary>
        /// returns a connection string from App.config
        /// </summary>
        /// <param name="Connection Name"></param>
        /// <returns></returns>
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
