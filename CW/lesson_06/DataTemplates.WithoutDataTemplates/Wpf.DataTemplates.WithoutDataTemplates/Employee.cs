namespace Wpf.DataTemplates.WithoutDataTemplates
{
    internal sealed class Employee
    {
        private readonly int age;
        private readonly string name;
        private readonly string surname;

        public Employee(string name, string surname, int age)
        {
            this.age = age;
            this.name = name;
            this.surname = surname;
        }

        public int Age => age;

        public bool IsActive { get; set; }

        public string Name => name;

        public string Surname => surname;

        public override string ToString()
        {
            return $"{name} {surname} ({age}) {(IsActive ? "Active" : "Not Active")}";
        }
    }
}