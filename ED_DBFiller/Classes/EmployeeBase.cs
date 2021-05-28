using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_DBFiller
{
    public static class EmployeeBase
    {
        #region PRIVATE FIELDS
        
        private static readonly int CHAR_MIN = 65;
        private static readonly int CHAR_MAX = 90;
        private static readonly Random rnd = new Random();
        private static string connectionString = ConfigurationManager.ConnectionStrings["cStr"].ConnectionString;

        #endregion

        #region PUBLIC METHODS

        public static void FillTableDB(int rec_count, List<DepStruct> depList)
        {
            for (int i = 0; i < rec_count; i++)
                AddRecord(GenerateName(15), GenerateName(20), GenerateAge(), GenerateStageYears(), GenerateActiveStatus(), GetRandomDepTitle(depList));
        }

        private static void AddRecord(string fn, string ln, int age, int stageYears, bool isActive, string depTitle)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = $@"INSERT INTO Employees (FirstName, LastName, Age, StageYears, IsActive, Dep_ID) 
                                     VALUES ('{fn}', '{ln}', {age}, {stageYears}, 
                                    '{isActive}', (SELECT TOP (1) ID FROM Departments WHERE Title = '{depTitle}'))";

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region PRIVATE METHODS
        private static bool IsEmpty()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "SELECT * FROM Employees";
                var command = new SqlCommand(sqlQuery, conn);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) return false;
                    else return true;
                }
            }
        }

        public static void ClearTableDB()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "DELETE FROM Employees";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                command.ExecuteNonQuery();
            }
        }

        private static string GetRandomDepTitle(List<DepStruct> depList)
        {
            return depList[rnd.Next(0, depList.Count - 1)].title;
        }

        private static string GenerateName(int amount)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= amount; i++)
                sb.Append((char)rnd.Next(CHAR_MIN, CHAR_MAX));

            return sb.ToString();
        }

        private static int GenerateAge()
        {
            return rnd.Next(20, 60);
        }

        private static bool GenerateActiveStatus()
        {
            return rnd.Next(0, 1) == 0 ? false : true;
        }

        private static int GenerateStageYears()
        {
            return rnd.Next(1, 20);
        }

        #endregion
    }
}
