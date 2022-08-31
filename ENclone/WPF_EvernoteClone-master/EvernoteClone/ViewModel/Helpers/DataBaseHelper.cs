using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DataBaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public static bool Insert<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rowsAdded = conn.Insert(item);
                if (rowsAdded > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool Update<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rowsAdded = conn.Update(item);
                if (rowsAdded > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rowsAdded = conn.Delete(item);
                if (rowsAdded > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static List<T> Read<T>() where T : new()
        {
            List<T> result;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                result = conn.Table<T>().ToList();
            }

            return result;
        }
    }
}
