using BugTrackingApp.service.model;
using BugTrackingApp.ui.utils;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BugTrackingApp.ui.model.converter
{
    /// <summary>
    /// Конвертер айди пользователя в описание
    /// </summary>
    class UserByCommentIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            User user = UserUtils.getUserForComment((int)value);
            return UIUtils.getUserDescr(user);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
