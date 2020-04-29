using System.Collections.Generic;

namespace Wpf.DataTemplates.WithoutDataTemplates
{
    internal sealed class DataSource
    {
        private readonly IEnumerable<Employee> employees;

        public DataSource()
        {
            employees = new[]
            {
                new Employee("Amy", "Carter", 23),
                new Employee("Craig", "Joy", 24) { IsActive = true },
                new Employee("Mary", "Crawford", 30),
                new Employee("Jamie", "Cole", 29)
            };
        }

        public IEnumerable<Employee> Employees => employees;
    }
}