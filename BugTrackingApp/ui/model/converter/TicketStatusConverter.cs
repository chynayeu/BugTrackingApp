using BugTrackingApp.service.model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BugTrackingApp.ui.model.converter
{
    /// <summary>
    /// Конвертер статуса тикета в текст
    /// </summary>
    class TicketStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((TicketUtils.TicketStatus)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
