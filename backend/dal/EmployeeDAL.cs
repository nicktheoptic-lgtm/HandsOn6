using MySql.Data.MySqlClient;
using Model;
using System.Collections.Generic;
using System;

namespace DAL
{
    public class EmployeeDAL
    {
        private string _connString = "Server=db;Database=dev;Uid=root;Pwd=a;";

        public List<EmployeeDTO> GetAll()
        {
            var employees = new List<EmployeeDTO>();
            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string sql = @"SELECT e.*, d.DepartmentName, d.OfficeNumber 
                               FROM Employees e 
                               JOIN Departments d ON e.DepartmentID = d.DepartmentID";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeDTO
                        {
                            EmployeeID = reader.GetInt32("EmployeeID"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString("Address"),
                            PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString("PhoneNumber"),
                            YearsOfService = reader.IsDBNull(reader.GetOrdinal("YearsOfService")) ? (int?)null : reader.GetInt32("YearsOfService"),
                            DepartmentID = reader.GetInt32("DepartmentID"),
                            Department = new DepartmentDTO
                            {
                                DepartmentID = reader.GetInt32("DepartmentID"),
                                DepartmentName = reader.GetString("DepartmentName"),
                                OfficeNumber = reader.GetString("OfficeNumber")
                            }
                        });
                    }
                }
            }
            return employees;
        }

        public EmployeeDTO GetById(int id)
        {
            return GetAll().Find(e => e.EmployeeID == id);
        }

        public int Update(EmployeeDTO emp)
        {
            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string sql = @"UPDATE Employees 
                               SET FirstName=@f, LastName=@l, Address=@a, PhoneNumber=@p, YearsOfService=@y 
                               WHERE EmployeeID=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@f", emp.FirstName);
                    cmd.Parameters.AddWithValue("@l", emp.LastName);
                    cmd.Parameters.AddWithValue("@a", emp.Address);
                    cmd.Parameters.AddWithValue("@p", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@y", emp.YearsOfService);
                    cmd.Parameters.AddWithValue("@id", emp.EmployeeID);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Create(EmployeeDTO emp)
        {
            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string sql = @"INSERT INTO Employees (FirstName, LastName, Address, PhoneNumber, YearsOfService, DepartmentID) 
                               VALUES (@f, @l, @a, @p, @y, @d); 
                               SELECT LAST_INSERT_ID();";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@f", emp.FirstName);
                    cmd.Parameters.AddWithValue("@l", emp.LastName);
                    cmd.Parameters.AddWithValue("@a", emp.Address);
                    cmd.Parameters.AddWithValue("@p", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@y", emp.YearsOfService);
                    cmd.Parameters.AddWithValue("@d", emp.DepartmentID);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public int Delete(int id)
        {
            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string sql = "DELETE FROM Employees WHERE EmployeeID = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    } // Closes EmployeeDAL Class
} // Closes DAL Namespace