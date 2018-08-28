using Battleship.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Battleship.Models
{
    [ProtoContract]
    public class Cell : INotifyPropertyChanged
    {
        public Cell()
        {
            Location = "";
            GridState = CellState.Water;
        }
        [ProtoIgnore]
        private CellState gridState;

        [ProtoMember(1)]
        public string Location { get; set; }
        [ProtoMember(2)]
        public CellState GridState
        {
            get { return gridState; }
            set
            {
                gridState = value;
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
