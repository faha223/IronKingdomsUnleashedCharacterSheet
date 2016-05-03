using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace IronKingdomsUnleashedCharacterSheet.ValueConverters
{
    public class WeightToPoundsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(int))
                throw new ArgumentException("value must be of type 'int'");
            int iVal = (int)value;
            return string.Format("{0} lbs", (int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(string))
                throw new ArgumentException("value must be of type 'string'");
            string str = (string)value;
            str = str.Trim();
            if(Regex.IsMatch(str, @"\d+"))
            {
                str = Regex.Replace(str, @"[^\d]", string.Empty);
                return int.Parse(str);
            }
            throw new ArgumentException("value is in incorrect format");
        }
    }
}
