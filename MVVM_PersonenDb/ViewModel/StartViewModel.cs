using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_PersonenDb.ViewModel
{
    public class StartViewModel : INotifyPropertyChanged
    {
        //Im ViewModel-Teil eines MVVM-Programms werden Klassen definiert, welche als Verbindungsstück zwischen den Views und den Modelklassen fungieren.
        //Diese Klassen sind die einzigen Programmteile, welche Referenzen auf Model-Klassen beinhalten. Sie selbst sind jeweils einem View zugrordnet,
        //mit welchem sie (nur) über den DataContext des jeweilgen Views verbunden sind.

        //Command-Properties
        public CustomCommand OeffneDbCmd { get; set; }
        public CustomCommand LadeDbCmd { get; set; }
        //Property zur Repräsentation der Anzahl der geladenen Personen (Getter verlinkt an die Model-Klasse)
        public int AnzahlPersonen { get { return Model.Person.PersonenListe.Count; } }

        //Konstruktor
        public StartViewModel()
        {
            //Befüllung der Commands
            LadeDbCmd = new CustomCommand
                (
                    //CanExe: Cmd kann ausgeführt werden, wenn die Anzahl der geladenen Personen = 0 ist
                    p => this.AnzahlPersonen == 0,
                    //Exe: führe Methode aus Model aus und informiere die GUI über Veränderung in der 'AnzahlPersonen'-Property
                    p =>
                    { 
                        Model.Person.LadePersonenAusDb();
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnzahlPersonen"));
                    }
                );

            OeffneDbCmd = new CustomCommand
                (
                    //CanExe: Cmd kann ausgeführt werden, wenn die Anzahl der geladenen Personen > 0 ist
                    p => this.AnzahlPersonen > 0,
                    //Exe:
                    p => 
                    {
                        //Instanzierung eines neunen ListViews
                        View.ListView DbAnsicht = new View.ListView();
                        //Zuweisung eines neuen ListViewModels als DataContext des neuen ListViews
                        DbAnsicht.DataContext = new ListViewModel();
                        //Anzeigen des neuen ListViews
                        DbAnsicht.Show();
                        //Schließen dieses Fensters (welches über den CommandParameter übergeben wurde)
                        (p as View.StartView).Close();
                    }
                );
        }
        //Event, welches die GUI über Veränderungen informiert
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
