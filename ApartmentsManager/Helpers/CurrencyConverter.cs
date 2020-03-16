using System;
using Windows.Globalization.NumberFormatting;
using Windows.UI.Xaml.Data;

namespace ApartmentsManager.Helpers
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var cf = new CurrencyFormatter("GBP") {Mode = CurrencyFormatterMode.UseSymbol, IsGrouped = true};
            return cf.FormatInt(System.Convert.ToUInt32(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }
    }
}
