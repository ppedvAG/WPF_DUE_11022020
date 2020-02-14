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
        public Model.Person AktuellePerson { get; set; }
        public CustomCommand OkCmd { get; set; }
        public CustomCommand CancelCmd { get; set; }

        public DetailViewModel()
        {
            OkCmd = new CustomCommand
                (
                    p => true,
                    p =>
                    {
                        if (MessageBox.Show("Soll die Person abgespeichert werden?", "Speichern?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            (p as View.DetailView).DialogResult = true;
                            (p as View.DetailView).Close();
                        }
                    }
                );
            CancelCmd = new CustomCommand
                (
                    p => true,
                    p => (p as View.DetailView).Close()
                );
        }
    }
}
