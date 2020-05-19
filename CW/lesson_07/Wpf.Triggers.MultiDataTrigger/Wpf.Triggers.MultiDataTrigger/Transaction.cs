using System;

namespace Wpf.Triggers.MultiDataTrigger
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

        public bool IsExpenseAllowable => IsExpense && Math.Abs(money) < 50.0;

        public bool IsIncome => money > 0.0;

        public double Money => money;
    }
}