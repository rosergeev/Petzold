using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Data;

namespace WhatSizeWithBindingConverter
{
    public class FormattedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IFormattable &&
                parameter is string &&
                !string.IsNullOrEmpty(parameter as string) &&
                targetType == typeof(string))
            {
                if (string.IsNullOrEmpty(language))
                    return (value as IFormattable).ToString(parameter as string, null);
                return (value as IFormattable).ToString(parameter as string, new CultureInfo(language));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
