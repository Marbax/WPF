namespace Wpf.Triggers.DataTrigger
{
    internal sealed class Transaction
    {
        private readonly string description;
        private readonly double money;

        public Transaction(string description, double money)
        {
            this.description = description;
            this.money = money;
        }

        public string Description => description;

        public bool IsExpense => money < 0.0;

        public bool IsIncome => money > 0.0;

        public double Money => money;
    }
}