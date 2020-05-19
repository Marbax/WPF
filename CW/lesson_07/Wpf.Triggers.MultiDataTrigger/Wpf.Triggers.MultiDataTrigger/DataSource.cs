using System.Collections.Generic;

namespace Wpf.Triggers.MultiDataTrigger
{
    internal sealed class DataSource
    {
        private readonly IEnumerable<Transaction> transactions;

        public DataSource()
        {
            transactions = new[]
            {
                new Transaction("Tips", 25.0),
                new Transaction("Coffee", -6.5),
                new Transaction("Groceries", -33.27),
                new Transaction("Tips", 20.0),
                new Transaction("Tips", 13.0),
                new Transaction("Shoes", -76.0),
                new Transaction("Dinner", -25.0),
                new Transaction("Video Game", -59.99)
            };
        }

        public IEnumerable<Transaction> Transactions => transactions;
    }
}