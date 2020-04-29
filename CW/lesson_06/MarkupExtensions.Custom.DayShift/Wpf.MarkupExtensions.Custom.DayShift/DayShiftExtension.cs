using System;
using System.Windows.Markup;

namespace Wpf.MarkupExtensions.Custom.DayShift
{
    internal sealed class DayShiftExtension : MarkupExtension
    {
        private readonly int shift;

        public DayShiftExtension() :
            this(shift: 0)
        {
        }

        public DayShiftExtension(int shift)
        {
            this.shift = shift;
        }

        public bool UpperCase { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            string dayOfweek = DateTime.Now.AddDays(shift).DayOfWeek.ToString();

            if (UpperCase)
            {
                dayOfweek = dayOfweek.ToUpper();
            }

            return dayOfweek;
        }
    }
}