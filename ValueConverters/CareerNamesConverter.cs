﻿using IronKingdomsUnleashedCharacterSheet.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace IronKingdomsUnleashedCharacterSheet.ValueConverters
{
    public class CareerNamesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
                return string.Join(", ", ((IEnumerable<object>)value).Select(c => ((Career)c).Name));
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
