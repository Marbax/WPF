using System;
using System.Windows.Markup;

namespace Wpf.MarkupExtensions.Custom.Today
{
    internal sealed class TodayExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return DateTime.Now.DayOfWeek.ToString();
        }
    }
}