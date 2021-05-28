using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_DBFiller
{
    public static class DepartmentBase
    {
        #region PRIVATE FIELDS
        private static readonly int CHAR_MIN = 65;
        private static readonly int CHAR_MAX = 90;
        private static readonly Random rnd = new Random();
        private static string connectionString = ConfigurationManager.ConnectionStrings["cStr"].ConnectionString;
        #endregion

        public static List<DepStruct> departments = new List<DepStruct>();

        private static void AddRecord(string title, bool activeStatus)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                DepStruct ds = new DepStruct()
                {
                    title = GenerateTitle(30),
                    isActive = GenerateActiveStatus()
                };
                string sqlQuery = $"INSERT INTO Departments(Title, IsActive) VALUES ('{ds.title}', '{ds.isActive}')";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                departments.Add(ds);
                command.ExecuteNonQuery();
            }
        }

        #region PUBLIC METHODS

        public static void ClearTableDB()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "DELETE FROM Departments";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                command.ExecuteNonQuery();
            }
        }

        public static void FillTableDB(int rec_count)
        {
            for (int i = 0; i < rec_count; i++)
            {
                AddRecord(GenerateTitle(30), GenerateActiveStatus());
            }
        }

        #endregion

        #region PRIVATE FIELDS

        private static bool IsEmpty()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "SELECT * FROM Departments";
                var command = new SqlCommand(sqlQuery, conn);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) return false;
                    else return true;
                }
            }
        }

        private static string GenerateTitle(int amount)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= amount; i++)
                sb.Append((char)rnd.Next(CHAR_MIN, CHAR_MAX));

            return sb.ToString();
        }

        private static bool GenerateActiveStatus()
        {
            return rnd.Next(0, 1) == 0 ? false : true;
        }

        #endregion
    }

    public struct DepStruct
    {
        public string title;
        public bool isActive;
    }
}
