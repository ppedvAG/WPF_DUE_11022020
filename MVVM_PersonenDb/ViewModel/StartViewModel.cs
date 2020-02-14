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
        public int AnzahlPersonen { get { return Model.Person.PersonenListe.Count; } }
        public CustomCommand OeffneDbCmd { get; set; }
        public CustomCommand LadeDbCmd { get; set; }

        public StartViewModel()
        {
            LadeDbCmd = new CustomCommand
                (
                    p => this.AnzahlPersonen == 0,
                    p =>
                    { 
                        Model.Person.LadePersonenAusDb();
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnzahlPersonen"));
                    }
                );

            OeffneDbCmd = new CustomCommand
                (
                    p => this.AnzahlPersonen > 0,
                    p => 
                    {
                        View.ListView DbAnsicht = new View.ListView();
                        DbAnsicht.DataContext = new ListViewModel();

                        DbAnsicht.Show();

                        (p as View.StartView).Close();
                    }
                );
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
