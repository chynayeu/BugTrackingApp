using BugTrackingApp.ui.utils;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BugTrackingApp.ui.model.converter
{
    /// <summary>
    /// Конвертер айди пользователя в описание
    /// </summary>
    class UserNameIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return UIUtils.getUserDescrById((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
