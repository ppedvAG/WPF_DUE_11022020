using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVVM_PersonenDb
{
    //Konverter zur Darstellung der 'Geschlechts'-Property
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //In XAML (vgl. DetailView) wird den RadioButtons ein Enumerator-Zustand zugeordnet, welcer über den ConverterParameter an diese
            //Klasse übergeben wird. Hier findet dann ein Vergleich mit dem Eintrag in dem Model-Objekt statt. Bei Übereinstimmtung wird der
            //entsprechende Radiobutton angewählt
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Wenn ein Radiobutton anegwählt wird, wird der Parameter an das Model-Objekt übergeben, ansonsten passiert nicht (Binding.DoNothing)
            return (bool)value ? parameter : Binding.DoNothing;
        }
    }
}
