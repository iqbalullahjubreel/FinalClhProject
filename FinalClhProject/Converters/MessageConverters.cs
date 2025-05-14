//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.Converters
{
    public class RoleToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Messagers.MessageRoleType)value == Messagers.MessageRoleType.User
                ? Colors.LightGreen : Colors.DarkGrey;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }

    public class RoleToPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString().ToLower() switch
            {
                "user" => LayoutOptions.End,        
                "assistant" => LayoutOptions.Start, 
                _ => LayoutOptions.Start
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
