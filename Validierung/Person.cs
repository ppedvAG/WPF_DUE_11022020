using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validierung
{
    public class Person : IDataErrorInfo
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value.All(x => Char.IsLetter(x)) & value.Length > 0)
                    name = value;
                else
                    throw new Exception("Bitte gib nur Buchstaben ein.");
            }
        }

        private int alter;
        public int Alter
        {
            get { return alter; }
            set { alter = value; }
        }

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Alter):
                        if (Alter < 0 | Alter > 150) return "Bitte gib dein WAHRES Alter an.";
                        break;
                    default:
                        break;
                }

                return "";
            }
        }
    }
}
