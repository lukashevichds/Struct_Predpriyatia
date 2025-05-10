using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_Predpriyatia
{
    public class PremiyaException : Exception
    {
        public PremiyaException(string message) : base(message) { }
    }
    public class OkladException : Exception
    {
        public OkladException(string message) : base(message) { }
    }
    public class Employee
    {
        public string FIO { get; set; }
        public string Position { get; set; }
        private decimal salary;

        public decimal Oklad
        {
            get => salary;
            set
            {
                if (value < 0)
                    throw new OkladException($"Невозможно создать сотрудника - указан отрицательный оклад: {value}");
                salary = value;
            }
        }
        public virtual decimal CalculateSalary()
        {
            try
            {
                return Oklad;
            }
            catch(OkladException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
    public class StaffEmployee : Employee
    {
        public decimal Premiya { get; set;}
        public override decimal CalculateSalary()
        {
            try
            {
                if (Premiya < 0)
                    throw new PremiyaException("Премия не может быть отрицательной.");
                return Oklad + Premiya;
            }
            catch (PremiyaException ex)
            {
                Console.WriteLine(ex.Message);
                return Oklad;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
                return 0;
            }
        }
    }
    public class ContractEmployee : Employee
    {
        public override decimal CalculateSalary()
        {
            try
            {
                return Oklad;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
                return 0;
            }
        }
    }
    public class Department
    {
        public string Name { get; set; }
        public int EmployeeCount { get; set; }
    }
    public class Company
    {
        public string Name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Employee employee1 = new StaffEmployee { FIO = "Иванов И.И.", Position = "Менеджер", Oklad = 50000, Premiya = -5000 };
                Console.WriteLine($"Зарплата сотрудника: {employee1.CalculateSalary()}");

                Employee employee2 = new ContractEmployee { FIO = "Петров И.И.", Position = "Разработчик", Oklad = -30000 };
                Console.WriteLine($"Зарплата сотрудника: {employee2.CalculateSalary()}");

                Employee employee3 = new Employee { FIO = "Сидоров И.И.", Position = "Аналитик", Oklad = -30000 };
                Console.WriteLine($"Зарплата сотрудника: {employee2.CalculateSalary()}");
            }
            catch (OkladException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
