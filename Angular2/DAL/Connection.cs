using System.Data;
using System.Data.SqlClient;
using Angular2.Models;

namespace Angular2.DAL
{
    public class Connection
    {
        public readonly string _connectionString;
        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }
        private SqlConnection con;
        public void connection()
        {
            con = new SqlConnection(_connectionString);
        }
        public List<Employee> GetStudents()
        {
            connection();
            SqlCommand cmd = new SqlCommand("SelectEmployee", con);
            List<Employee> employees = new List<Employee>();
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                employees.Add(new Employee()
                {
                    EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                    Name = Convert.ToString(dr["Name"]),
                    Email = Convert.ToString(dr["Email"]),
                    Phone = Convert.ToString(dr["Phone"]),
                });
            }
            con.Close();
            return employees;
        }
        public bool Add(Employee emp)
        {
            connection();
            SqlCommand cmd = new SqlCommand("InsertUpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("EmployeeID", emp.EmployeeID);
            cmd.Parameters.AddWithValue("Name", emp.Name);
            cmd.Parameters.AddWithValue("Email", emp.Email);
            cmd.Parameters.AddWithValue("Phone", emp.Phone);
            cmd.Parameters.AddWithValue("Action", "Insert");
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0) return true;
            else return false;

        }
        public bool Update(Employee employee)
        {
            connection();
            SqlCommand cmd = new SqlCommand("InsertUpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("EmployeeID", employee.EmployeeID);
            cmd.Parameters.AddWithValue("name", employee.Name);
            cmd.Parameters.AddWithValue("email", employee.Email);
            cmd.Parameters.AddWithValue("Phone", employee.Phone);
            cmd.Parameters.AddWithValue("Action", "Update");
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 0) return true;
            else return false;

        }
        public bool Delete(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("EmployeeID", id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 0) return true;
            else return false;
        }
    }
}
