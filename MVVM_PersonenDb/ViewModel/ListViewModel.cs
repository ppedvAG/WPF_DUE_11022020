using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_PersonenDb.ViewModel
{
    public class ListViewModel
    {
        //Command-Properties
        public CustomCommand NewCmd { get; set; }
        public CustomCommand EditCmd { get; set; }
        public CustomCommand DeleteCmd { get; set; }
        public CustomCommand CloseCmd { get; set; }
        //Listen-Property, welche auf die Liste des Models verlinkt
        public ObservableCollection<Model.Person> PersonenListe { get { return Model.Person.PersonenListe; } }

        public ListViewModel()
        {
            //Command-Definitionen
            //Hinzufügen einer neuen Person
            NewCmd = new CustomCommand
                (
                    //CanExe: kann immer ausgeführt werden
                    p => true,
                    //Exe:
                    p => 
                    {
                        //Instanzieren eines neuen DetailViews
                        View.DetailView neuePersonDialog = new View.DetailView();
                        //Zuweisung eines neuen DetailViewModels als dessen DataContext
                        //und Zuweisung einer neuen Person in die 'AktuellePerson'-Property des neuen DetailViewModels
                        neuePersonDialog.DataContext = new DetailViewModel() { AktuellePerson = new Model.Person() };

                        //Aufruf des DetailViews mit Überprüfung auf dessen DialogResult (wird true, wenn der Benutzer OK klickt)
                        if (neuePersonDialog.ShowDialog() == true)
                            //Hinzufügen der neuen Person zu Liste
                            Model.Person.PersonenListe.Add((neuePersonDialog.DataContext as DetailViewModel).AktuellePerson);
                    }
                );

            //Ändern einer bestehenden Person
            EditCmd = new CustomCommand
                (
                    //CanExe: Kann ausgeführt werden, wenn der Parameter (der im DataGrid ausgewählte Eintrag) eine Person ist.
                    //Fungiert als Null-Prüfung
                    p => p is Model.Person,
                   //Exe:
                   p => 
                    {
                        //Vgl. NeuCmd (s.o.)
                        View.DetailView aenderePersonDialog = new View.DetailView();

                        //Zuweisung einer Kopie der ausgewählten Person in die 'AktuellePerson'-Property des neuen DetailViewModels
                        aenderePersonDialog.DataContext = new DetailViewModel() { AktuellePerson = new Model.Person(p as Model.Person) };
                        //Ändern des Titels des neuen DetailViews
                        (aenderePersonDialog as View.DetailView).Title = "Ändere " + (p as Model.Person).Vorname + " " + (p as Model.Person).Nachname;

                        if (aenderePersonDialog.ShowDialog() == true)
                            //Austausch der (veränderten) Person-Kopie mit dem Original in der Liste
                            Model.Person.PersonenListe[Model.Person.PersonenListe.IndexOf(p as Model.Person)] = (aenderePersonDialog.DataContext as DetailViewModel).AktuellePerson;
                    }
                );

            //Löschen einer Person
            DeleteCmd = new CustomCommand
                (
                    //CanExe: s.o.
                    p => p is Model.Person,
                    //Exe:
                    p => 
                    {
                        //Anzeige einer MessageBox, ob Löschvorgang wirklich gewollt ist
                        if (MessageBox.Show($"Soll {(p as Model.Person).Vorname} wirklich gelöscht werden?", "Löschen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            //Löschen der ausgewählten Person
                            Model.Person.PersonenListe.Remove(p as Model.Person);
                    }
                );

            //SChließen des Programms
            CloseCmd = new CustomCommand
                (
                    //CanExe: kann immer ausgeführt werden
                    p => true,
                    //Exe: Schließen der Applikation
                    p => Application.Current.Shutdown()
                );
        }
    }
}
