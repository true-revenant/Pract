using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;

namespace ED_WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EDService : IServiceED
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["cStr"].ConnectionString;
        //private string connectionString = "Data Source=localhost;Initial Catalog=Corporation;User ID=corp_user;Password=123456";

        public int AddEmployee(Employee empl)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = $@"INSERT INTO Employees (FirstName, LastName, Age, StageYears, IsActive, Dep_ID) 
                                     VALUES ('{empl.FirstName}', '{empl.LastName}', {empl.Age}, {empl.StageYears}, 
                                    '{empl.IsActive}', (SELECT TOP (1) ID FROM Departments WHERE ID = {empl.Dep.ID}))";

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                return command.ExecuteNonQuery();
                //if (command.ExecuteNonQuery() > 0)
                //    Employees.Add(newEmp);
            }
        }

        public List<Department> LoadDepartments()
        {
            var res = new List<Department>();
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
                            var depart = new Department()
                            {
                                Title = reader["Title"].ToString(),
                                IsActive = (bool)reader["IsActive"],
                                ID = (int)reader["ID"]
                            };

                            //var depart = new Department(reader["Title"].ToString(), (bool)reader["IsActive"]);
                            //depart.SetDepartmentID((int)reader["ID"]);
                            res.Add(depart);
                        }
                    }
                    return res;
                }
            }
        }

        public ObservableCollection<Employee> LoadEmployees(List<Department> departmentsList)
        {
            var res = new ObservableCollection<Employee>();
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
                                                        departmentsList.Find(x => x.ID == (int)reader["Dep_ID"]));

                            employee.SetEmployeeID((int)reader["ID"]);
                            res.Add(employee);
                        }
                    }
                    return res;
                }
            }
        }

        public int RemoveEmployee(Employee empl)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = $@"DELETE FROM Employees WHERE ID = {empl.getEmployeeID()}";

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                return command.ExecuteNonQuery();
                //if (command.ExecuteNonQuery() > 0)
                //    Employees.Remove(delEmp);
            }
        }

        public Employee UpdateEmployee(Employee empl)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlQuery = $@"UPDATE Employees SET FirstName = '{empl.FirstName}', 
                                                          LastName = '{empl.LastName}',
                                                          Age = {empl.Age},
                                                          StageYears = {empl.StageYears},
                                                          Dep_ID = {empl.Dep.ID},
                                                          IsActive = '{empl.IsActive}' WHERE ID = {empl.getEmployeeID()}";

                SqlCommand command = new SqlCommand(sqlQuery, conn);
                if (command.ExecuteNonQuery() > 0) return empl;
                else return null;
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region NON IMPLEMENTED LOCAL



        #endregion
    }
}
