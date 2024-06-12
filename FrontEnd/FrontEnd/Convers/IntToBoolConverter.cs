using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FrontEnd.Convers
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue != 2; // Devuelve true si el valor no es 2, false si es 2
            }
            return false; // Valor por defecto
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? 1 : 2; // Devuelve 1 si el valor es true, 2 si es false
            }
            return Binding.DoNothing; // En caso de que no sea un valor booleano
        }

    }
}
