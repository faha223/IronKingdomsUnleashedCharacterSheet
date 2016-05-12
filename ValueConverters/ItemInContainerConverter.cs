using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace IronKingdomsUnleashedCharacterSheet.ValueConverters
{
    class ItemInContainerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object value = values[0];
            var collection = values[1] as IEnumerable<object>;
            if (collection == null)
                return false;
            return collection.Contains(value);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
