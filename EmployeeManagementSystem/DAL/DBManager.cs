using BOL;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DBManager
    {
        public static string connString = @"server=localhost;port=3306;user=root; password=root123;database=dotnetprojectd";

        public static List<Employee> getAllEmployees() {

            List<Employee> empList = new List<Employee>();

            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = connString;

            try {
            
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;

                string query = "SELECT * FROM employees";

                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string empname = reader["empname"].ToString();
                    string designation = reader["designation"].ToString();
                    Department department = Enum.Parse<Department>(reader["department"].ToString());
                    string city = reader["city"].ToString();
                    double salary = double.Parse(reader["salary"].ToString());
                    //DateOnly joiningdate = DateOnly.Parse(reader["joiningdate"].ToString().Substring(0,10));
                    //DateOnly joiningdate = DateOnly.Parse(reader["joiningdate"].ToString());

                    //DateTime joiningdate = DateTime.ParseExact(reader["joiningdate"].ToString(),
                    //"yyyy-MM-dd", CultureInfo.InvariantCulture);

                    DateOnly joiningdate = DateOnly.FromDateTime(DateTime.Parse(reader["joiningdate"].ToString()));

                    Employee newEmp = new Employee(id, empname, designation, department, city, salary, joiningdate);

                    empList.Add(newEmp);
                }
                
            }
            catch (Exception e) {

                Console.WriteLine(e.Message);
            }
            finally { conn.Close(); }
            
            return empList;
        }//end of getAllEmployees Method

        public static void insertEmployee(Employee newEmp) {

            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = connString;

            try {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection= conn;

                string query = "insert into employees values('"+newEmp.ID+"', '"+newEmp.EMPNAME+"', '" +
                    newEmp.DESIGNATION+"', '"+newEmp.DEPARTMENT+"', '"+newEmp.CITY+"', '"+newEmp.SALARY+"','"+newEmp.JOININGDATE.ToString("yyyy-MM-dd")+"')";

                cmd.CommandText = query;

                cmd.ExecuteNonQuery();

            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally { conn.Close(); }

        }//end of insertEmployee Method

        public static void deleteEmployee(int id) {

            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = connString;

            try {
            
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection= conn;

                string query = "DELETE FROM employees where id = " + id;

                cmd.CommandText = query;
                    
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) {

                Console.WriteLine(e.Message);
            }
            finally{ 

                conn.Close();            
            }        
        }//end of DeleteEmployee Method

        public static Employee GetEmployeeById(int id) {

            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = connString;

            Employee foundEmp = new Employee();

            try
            {
                  conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;

                string query = "SELECT * FROM employees where id = " +id;

                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int empid = int.Parse(reader["id"].ToString());
                    string empname = reader["empname"].ToString();
                    string designation = reader["designation"].ToString();
                    Department department = Enum.Parse<Department>(reader["department"].ToString());
                    string city = reader["city"].ToString();
                    double salary = double.Parse(reader["salary"].ToString());
                    //DateOnly joiningdate = DateOnly.Parse(reader["joiningdate"].ToString().Substring(0, 10));
                    DateOnly joiningdate = DateOnly.FromDateTime(DateTime.Parse(reader["joiningdate"].ToString()));
                    
                    foundEmp = new Employee(empid, empname, designation, department, city, salary, joiningdate);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally {

                conn.Close();
            }
            return foundEmp;
        }//end of getEmpById Method


        public static void updateEmployee(Employee emp) {

            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = connString;

            Employee foundEmp = new Employee();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = conn;

                string query = "UPDATE employees SET empname = '" + emp.EMPNAME + "', designation = '" + emp.DESIGNATION
                    + "', department = '" + emp.DEPARTMENT + "', city = '" + emp.CITY + "', salary = '" + emp.SALARY +
                    "', joiningdate = '"+emp.JOININGDATE+" WHERE id = " + emp.ID;

                cmd.CommandText = query;

                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

                conn.Close();
            }
           
        }//end of updateEmployee Method

    }
}