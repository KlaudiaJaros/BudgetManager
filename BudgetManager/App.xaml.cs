﻿using BudgetManagerLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// WPF creates a main method once you hit run. To write a main method yourself, you have to right-click on App.xaml 
        /// in the solution explorer, select Properties and Change 'Build Action' to 'Page' (initial value is 'ApplicationDefinition')
        /// Then change the code below.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // establish database connections:
            GlobalConfig.InitialiseConnections(true, true);
            var application = new App();
            /* To run from the Main Window: */
            
            //application.InitializeComponent();
            //application.Run();

            /* To run from a chosen window: */
            application.Run(new CreateNewBudgetWindow());


        }
    }
}
