using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_PersonenDb.ViewModel
{
    public class DetailViewModel
    {
        //Property, welche die neue oder zu bearbeitende Person beinhaltet
        public Model.Person AktuellePerson { get; set; }
        //Command-Properties
        public CustomCommand OkCmd { get; set; }
        public CustomCommand CancelCmd { get; set; }

        public DetailViewModel()
        {
            //OK-Command (Bestätigung)
            OkCmd = new CustomCommand
                (
                    //CanExe: Kann immer ausgeführt werden. Eine Prüfung auf die Validierung der einzelnen EIngabefelder findet schon in der GUI (vgl. DetailView) statt.
                    p => true,
                    //Exe:
                    p =>
                    {
                        //Nachfrage auf Korrektheit der Daten per MessageBox
                        if (MessageBox.Show("Soll die Person abgespeichert werden?", "Speichern?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            //Setzen des DialogResults des Views (welches per Parameter übergeben wurde) auf true, damit das LIstView weiß, dass es weiter
                            //machen kann (d.h. die neuen Person einfügen bzw. austauschen)
                            (p as View.DetailView).DialogResult = true;
                            //Schließen des Views
                            (p as View.DetailView).Close();
                        }
                    }
                );
            //Abbruch-Cmd
            CancelCmd = new CustomCommand
                (
                    //CanExe: Kann immer ausgeführt werden
                    p => true,
                   //Exe: Schließen des Fensters
                   p => (p as View.DetailView).Close()
                );
        }
    }
}
