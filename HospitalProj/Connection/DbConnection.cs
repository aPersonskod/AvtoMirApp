using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows;

namespace HospitalProj.Connection
{
    public static class DbConnection
    {
        private static string filePath = "Olena_hosp.accdb";
        private static string con = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Persist Security Info=False;";

        public static List<List<object>> DoSqlCommand(this string sqlQuery, int columns)
        {
            var data = new List<List<object>>();
            
            using (var conn = new OleDbConnection(con))
            {
                conn.Open();
                var cmd = new OleDbCommand(sqlQuery, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var list = new List<object>();
                    for(var i = 0; i < columns; i++)
                        list.Add(reader[i]); 
                    data.Add(list);
                }
            }

            return data;
        }

        public static void DoSqlCommand(this string sqlQuery)
        {
            try
            {
                using (var conn = new OleDbConnection(con))
                {
                    conn.Open();
                    var cmd = new OleDbCommand(sqlQuery, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static void Show(this string message, string header = "Сообщение")
        {
            MessageBox.Show(message, header);
        }
    }
}