using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract5.Classes
{
    public class EmployeeBase
    {
        #region PUBLIC FIELDS

        //public const string connectionString = "Data Source=localhost;Initial Catalog=Corporation;User ID=corp_user;Password=123456";
        public ObservableCollection<Employee> Employees { get; set; }

        #endregion

        #region PRIVATE FIELDS
        private string connectionString;
        private readonly int CHAR_MIN = 65;
        private readonly int CHAR_MAX = 90;
        private readonly Random rnd = new Random();
        private List<Department> departmentsList;

        #endregion

        public EmployeeBase(List<Department> departmentsList)
        {
            this.departmentsList = departmentsList;

            if (IsEmpty())
            {
                GenerateList(50);
                FillTableDB();
            }
            else LoadFromTableDB();
        }

        #region PUBLIC METHODS

        public void LoadFromTableDB()
        {
            Employees = new ObservableCollection<Employee>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = "SELECT * FROM Employees";
                var command = new SqlCommand(sqlQuery, conn);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee(reader["FirstName"].ToString(),
                                                        reader["LastName"].ToString(),
                                                        (int)reader["Age"],
                                                        (bool)reader["IsActive"],
                                                        (int)reader["StageYears"], 
                                                        departmentsList.Find(x => x.GetDepartmentID() == (int)reader["Dep_ID"]));

                            employee.SetEmployeeID((int)reader["ID"]);
                            Employees.Add(employee);
                        }
                    }
                }
            }
        }

        public void Add(Employee newEmp)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = $@"INSERT INTO Employees (FirstName, LastName, Age, StageYears, IsActive, Dep_ID) 
                                     VALUES ('{newEmp.FirstName}', '{newEmp.LastName}', {newEmp.Age}, {newEmp.StageYears}, 
                                    '{newEmp.IsActive}', (SELECT TOP (1) ID FROM Departments WHERE ID = {newEmp.Dep.GetDepartmentID()}))";

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                if (command.ExecuteNonQuery() > 0)
                    Employees.Add(newEmp);
            }
        }

        public void Update(Employee newEmp, int index)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = $@"UPDATE Employees SET FirstName = '{newEmp.FirstName}', 
                                                          LastName = '{newEmp.LastName}',
                                                          Age = {newEmp.Age},
                                                          StageYears = {newEmp.StageYears},
                                                          Dep_ID = {newEmp.Dep.GetDepartmentID()},
                                                          IsActive = '{newEmp.IsActive}' WHERE ID = {newEmp.getEmployeeID()}";

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                if (command.ExecuteNonQuery() > 0)
                    Employees[index] = newEmp;
            }
        }

        public void Delete(Employee delEmp)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = $@"DELETE FROM Employees WHERE ID = {delEmp.getEmployeeID()}";

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                if (command.ExecuteNonQuery() > 0)
                    Employees.Remove(delEmp);
            }
        }

        #endregion

        #region PRIVATE METHODS

        private bool IsEmpty()
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

        private void FillTableDB()
        {
            foreach (var e in Employees)
                Add(e);
        }

        private void ClearTableDB()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "DELETE FROM Employees";
                SqlCommand command = new SqlCommand(sqlQuery, conn);
                command.ExecuteNonQuery();
            }
        }

        private void GenerateList(int count)
        {
            Employees = new ObservableCollection<Employee>();

            for(int i = 1; i <= count; i++)
            {
                Employees.Add(new Employee(GenerateName(rnd.Next(10, 20)),
                                            GenerateName(rnd.Next(10, 20)),
                                            GenerateAge(), GenerateActiveStatus(),
                                            GenerateStageYears(),
                                            departmentsList[rnd.Next(0, departmentsList.Count - 1)]));
            }
        }

        private string GenerateName(int amount)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= amount; i++)
                sb.Append((char)rnd.Next(CHAR_MIN, CHAR_MAX));

            return sb.ToString();
        }

        private int GenerateAge()
        {
            return rnd.Next(20, 60);
        }

        private bool GenerateActiveStatus()
        {
            return rnd.Next(0, 1) == 0 ? false : true;
        }

        private int GenerateStageYears()
        {
            return rnd.Next(1, 20);
        }

        #endregion
    }
}
