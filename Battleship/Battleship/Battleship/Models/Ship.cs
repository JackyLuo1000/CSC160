using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Enums;
using ProtoBuf;

namespace Battleship.Models
{
    [ProtoContract]
    public class Ship
    {
        public Ship()
        {
            ShipCells = new List<Cell>();
            Name = ShipNames.Carrier;
        }

        public Ship(ShipNames name)
        {
            ShipCells = new List<Cell>();
            Name = name;
            IsSunk = false;
        }
        [ProtoMember(1)]
        public List<Cell> ShipCells { get; set; }
        [ProtoMember(2)]
        public ShipNames Name { get; set; }
        [ProtoMember(3)]
        public bool IsSunk { get; set; }

        public void CheckSunk()
        {
            IsSunk = true;
            foreach(Cell c in ShipCells)
            {
                if(c.GridState == CellState.Ship)
                {
                    IsSunk = false;
                    break;
                }
            }
        }
    }
}
