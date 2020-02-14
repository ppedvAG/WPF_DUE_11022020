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
        public CustomCommand NewCmd { get; set; }
        public CustomCommand EditCmd { get; set; }
        public CustomCommand DeleteCmd { get; set; }
        public CustomCommand CloseCmd { get; set; }
        public ObservableCollection<Model.Person> PersonenListe { get { return Model.Person.PersonenListe; } }

        public ListViewModel()
        {
            NewCmd = new CustomCommand
                (
                    p => true,
                    p => 
                    {
                        View.DetailView neuePersonDialog = new View.DetailView();

                        neuePersonDialog.DataContext = new DetailViewModel() { AktuellePerson = new Model.Person() };

                        if (neuePersonDialog.ShowDialog() == true)
                            Model.Person.PersonenListe.Add((neuePersonDialog.DataContext as DetailViewModel).AktuellePerson);
                    }
                );

            EditCmd = new CustomCommand
                (
                    p => p is Model.Person,
                    p => 
                    {
                        View.DetailView aenderePersonDialog = new View.DetailView();

                        aenderePersonDialog.DataContext = new DetailViewModel() { AktuellePerson = new Model.Person(p as Model.Person) };

                        if (aenderePersonDialog.ShowDialog() == true)
                            Model.Person.PersonenListe[Model.Person.PersonenListe.IndexOf(p as Model.Person)] = (aenderePersonDialog.DataContext as DetailViewModel).AktuellePerson;
                    }
                );

            DeleteCmd = new CustomCommand
                (
                    p => p is Model.Person,
                    p => 
                    {
                        if (MessageBox.Show($"Soll {(p as Model.Person).Vorname} wirklich gelöscht werden?", "Löschen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            Model.Person.PersonenListe.Remove(p as Model.Person);
                    }
                );

            CloseCmd = new CustomCommand
                (
                    p => true,
                    p => Application.Current.Shutdown()
                );
        }
    }
}
