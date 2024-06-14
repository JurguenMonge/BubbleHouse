using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FrontEnd.Convers
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int estado = (int)value;
            if (estado == 1)
            {
                return "Disponible";
            }
            else if (estado == 2)
            {
                return "Agotado";
            }
            else
            {
                return ""; // Manejar otro estado si es necesario
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
