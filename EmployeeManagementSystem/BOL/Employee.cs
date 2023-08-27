namespace BOL
{
    public enum Department {HR, SALES, FINANCE, MARKETING, SUPPORT , IT }

    public class Employee
    {
        private int id;
        private string empname;
        private string designation;
        private Department department;
        private string city;
        private double salary;
        private DateOnly joiningdate;

        public Employee(int id, string empname, string designation,
            Department department, string city, double salary, DateOnly joiningdate) { 

            this.id = id;
            this.empname = empname;
            this.designation = designation;
            this.department = department;
            this.city = city;
            this.salary = salary;   
            this.joiningdate = joiningdate;
        }

        public Employee() { 
        
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string EMPNAME
        {
            get { return this.empname; }
            set { this.empname = value; }
        }

        public string DESIGNATION
        {
            get { return this.designation; }
            set { this.designation = value; }
        }

        public Department DEPARTMENT
        {
            get { return this.department; }
            set { this.department = value; }
        }

        public string CITY
        {
            get { return this.city; }
            set { this.city = value; }
        }

        public double SALARY
        {
            get { return this.salary; }
            set { this.salary = value; }
        }

        public DateOnly JOININGDATE {

            get { return this.joiningdate; }
            set { this.joiningdate = value; }        
        }

        public override string ToString()
        {
            return " | " + this.id + " | " + this.empname + " | " + this.designation + " | " 
                 + this.department + " | " + this.city + " | " + this.salary + " | " + this.joiningdate + " | ";
        }
    }
}