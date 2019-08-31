using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public delegate void HelloFunctionDelegate(string Message);
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> empList = new List<Employee>();
            empList.Add(new Employee() { ID = 101, Name = "Marry", Salary = 5000, Experience = 5 });
            empList.Add(new Employee() { ID = 101, Name = "Mike", Salary = 4000, Experience = 4 });
            empList.Add(new Employee() { ID = 101, Name = "John", Salary = 6000, Experience = 6 });
            empList.Add(new Employee() { ID = 101, Name = "Todd", Salary = 3000, Experience = 3 });

            Employee.PromoteEmployee(empList, emp => emp.Experience > 5);

            Employee.PromoteEmployee(empList, delegate (Employee emp)
            {
                return emp.Experience > 5;
            });

            Console.ReadKey();
        }
    }

    delegate bool IsPromotable(Employee empl);

    class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int Experience { get; set; }

        public static void PromoteEmployee(List<Employee> employeeList, IsPromotable isEligiblePromote)
        {
            foreach (var employee in employeeList)
            {
                if (isEligiblePromote(employee))
                {
                    Console.WriteLine(employee.Name + " promoted");
                }
            }
        }
    }
}
