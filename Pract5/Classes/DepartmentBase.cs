using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract5.Classes
{
    public class DepartmentBase
    {
        #region PUBLIC FIELDS
        public const string connectionString = "Data Source=localhost;Initial Catalog=Corporation;User ID=corp_user;Password=123456";
        public List<Department> Departments;
        #endregion

        #region PRIVATE FIELDS
        private readonly int CHAR_MIN = 65;
        private readonly int CHAR_MAX = 90;
        private readonly Random rnd = new Random();
        #endregion

        public DepartmentBase()
        {
            if (IsEmpty())
            {
                GenerateList(15);
                FillTableDB();
            }
            else LoadFromTableDB();
        }

        #region PUBLIC METHODS

        public void LoadFromTableDB()
        {
            Departments = new List<Department>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "SELECT * FROM Departments";
                var command = new SqlCommand(sqlQuery, conn);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var depart = new Department(reader["Title"].ToString(), (bool)reader["IsActive"]);
                            depart.SetDepartmentID((int)reader["ID"]);
                            Departments.Add(depart);
                        }
                    }
                }
            }
        }

        public void Add(Department newDep)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = $"INSERT INTO Departments(Title, IsActive) VALUES ('{newDep.Title}', '{newDep.IsActive}')";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                if (command.ExecuteNonQuery() > 0)
                    Departments.Add(newDep);
            }
        }

        #endregion

        #region PRIVATE METHODS
        private void ClearTableDB()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "DELETE FROM Departments";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                command.ExecuteNonQuery();
            }
        }

        private void FillTableDB()
        {
            foreach (var d in Departments)
                Add(d);
        }

        private bool IsEmpty()
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

        private void GenerateList(int count)
        {
            Departments = new List<Department>();

            for (int i = 1; i <= count; i++)
            {
                Department dep = new Department(GenerateTitle(30), GenerateActiveStatus());

                Departments.Add(dep);
            }
        }

        private string GenerateTitle(int amount)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= amount; i++)
                sb.Append((char)rnd.Next(CHAR_MIN, CHAR_MAX));

            return sb.ToString();
        }

        private bool GenerateActiveStatus()
        {
            return rnd.Next(0, 1) == 0 ? false : true;
        }
        #endregion
    }
}
