using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingDemo_A.Models
{
    class Dragon : INotifyPropertyChanged
    {
        private string name;
        private int age;
        private string scaleColor;
        private bool canFly;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                FieldChanged();
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                FieldChanged();
            }
        }
        public string ScaleColor
        {
            get { return scaleColor; }
            set
            {
                scaleColor = value;
                FieldChanged();
            }
        }
        public bool CanFly
        {
            get { return canFly; }
            set
            {
                canFly = value;
                FieldChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FieldChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
