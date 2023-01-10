using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;


namespace PL
{
    public class Converts
    {
        public class convertAddProduct : IValueConverter
        {
            public object Convert(
     object value,
     Type targetType,
     object parameter,
     CultureInfo culture)
            {
                string stringValue = (string)value;
                if (stringValue == "customer")
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            public object ConvertBack(
     object value,
     Type targetType,
     object parameter,
     CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
    public class convertGoToCart : IValueConverter
    {

        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            string stringValue = (string)value;
            if (stringValue == "customer")
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}



