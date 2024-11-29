using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows;
using Npgsql;

namespace HospitalProj.Connection
{
    public static class DbConnection
    {
        private static string filePath = "Olena_hosp.accdb";
        //private static string con = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Persist Security Info=False;";
        //private static string con = $"Host=localhost;Port=5432;Username=postgres;Password=password;Database=ISPsycho";
        private static string con = $"Host=localhost;Port=5432;Username=postgres;Password=password;Database=ISPsycho";

        public static List<List<object>> DoSqlCommand(this string sqlQuery, int columns)
        {
            var data = new List<List<object>>();
            
            using (var conn = new NpgsqlConnection(con))
            {
                conn.Open();
                var cmd = new NpgsqlCommand(sqlQuery, conn);
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
                using (var conn = new NpgsqlConnection(con))
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand(sqlQuery, conn);
                    cmd.ExecuteNonQuery();
                    AllInfo.Instance.Refresh();
                }
            }
            catch (Exception e)
            {
                e.Message.Show("Ошибка");
            }
        }
        
        public static void Show(this string message, string header = "Сообщение")
        {
            MessageBox.Show(message, header);
        }
    }
}