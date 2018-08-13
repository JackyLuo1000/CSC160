using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Models
{
    class Cell : INotifyPropertyChanged
    {

        public Cell()
        {
            IsAlive = false;
            NextState = false;
        }
        //Private field for isAlive
        private bool isAlive;

        //Sets the property of IsAlive and Notifies changes
        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                isAlive = value;
                FieldChanged();
            }
        }

        //Property for the next state of cell
        public bool NextState { get; set; }
        

        //Lets the binding know that the property has changed
        public event PropertyChangedEventHandler PropertyChanged;

        private void FieldChanged([CallerMemberName]string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        /// <summary>
        /// Changes the the IsAlive state each time the method runs
        /// </summary>
        public void Toggle()
        {
            IsAlive = !IsAlive;
        }

        
    }
}
