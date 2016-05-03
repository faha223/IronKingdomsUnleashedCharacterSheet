﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace IronKingdomsUnleashedCharacterSheet.ValueConverters
{
    class EnumStringCleaner : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace("_", " ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
