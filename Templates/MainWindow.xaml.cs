using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Templates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Properties vom Typ ObservableCollection informieren die GUI automatisch über Veränderungen (z.B. neuer Listeneintrag). Sie eignen sich besonders gut
        //für eine Bindung an ein ItemControl (z.B. ComboBox, ListBox, DataGrid, ...)
        public ObservableCollection<Person> PersonenListe { get; set; } = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();

            //Erstellen von Bsp-Daten
            PersonenListe.Add(new Person() { Vorname = "Otto", Nachname = "Schmidt", Alter = 36 });
            PersonenListe.Add(new Person() { Vorname = "Hans", Nachname = "Fischer", Alter = 50 });
            PersonenListe.Add(new Person() { Vorname = "Maria", Nachname = "Müller", Alter = 78 });

            //Setzen des DataContext des Fensters auf sich selbst (Einfache Bindungen zu Properties in dieser Datei möglich)
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button funktioniert");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Erhöhung des Alters der Person im DataContextes des StackPanels
            (splDataContextBsp.DataContext as Person).Alter++;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Ausgabe einer Person
            MessageBox.Show((lbxPersonen.SelectedItem as Person).Vorname);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Hinzufügen einer neuen Person
            PersonenListe.Add(new Person() { Vorname = "Jürgen", Nachname = "Schimdt", Alter = 23 });
        }
    }
}
