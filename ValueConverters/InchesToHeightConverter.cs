using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace IronKingdomsUnleashedCharacterSheet.ValueConverters
{
    public class InchesToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(int))
                throw new ArgumentException("value must be of type 'int'");
            int iVal = (int)value;
            return string.Format("{0}'-{1}\"", iVal / 12, iVal % 12);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(string))
                throw new ArgumentException("value must be of type 'string'");
            var str = (string)value;
            if (Regex.IsMatch(str, @"^\d+'\-\d+"""))
            {
                str = Regex.Replace(str, @"[^\d]+", " ").Trim();
                string[] parts = str.Split(' ');
                int iVal = int.Parse(parts[0]) * 12 + int.Parse(parts[1]);
                return iVal;
            }
            else
                throw new ArgumentException("value must contain a height in 0'-0\" format.");
        }
    }
}
