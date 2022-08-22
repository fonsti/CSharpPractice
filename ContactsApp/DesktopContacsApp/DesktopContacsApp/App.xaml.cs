using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DesktopContacsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string dbName = "Contacts.db";
        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databasePath = System.IO.Path.Combine(folderPath, dbName);
    }
}
