using BugTrackingApp.service.model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BugTrackingApp.ui.model.converter
{
    /// <summary>
    /// Конвертер Названия тикета по айди и проекту
    /// </summary>
    class TicketIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ProjectUtils.assignProject.name + "-" + (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
