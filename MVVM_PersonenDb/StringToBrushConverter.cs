using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MVVM_PersonenDb
{
    //Konverter zur Darstellung der Hintergrundfarbe in der DataGrid-Spalte 'Lieblingsfarbe'
    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Mittels der unten verwendeten ConvertFromString-Methode können Brushes aus Strings erstellt werden
            BrushConverter converter = new BrushConverter();
            Brush brush = (Brush)converter.ConvertFromString(value.ToString());
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Ein Weg Target->Source existiert nicht
            throw new NotImplementedException();
        }
    }
}
