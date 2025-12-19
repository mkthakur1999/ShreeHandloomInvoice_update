using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace ShreeHandloomInvoice.Data
{
    public static class DbHelper
    {
        private static readonly string DbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "ShreeHandloomInvoice",
                "data",
                "invoice.db");

        public static SqliteConnection GetConnection()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath));
            return new SqliteConnection($"Data Source={DbPath}");
        }
    }
}
