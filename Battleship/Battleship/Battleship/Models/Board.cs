using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Models
{
    [ProtoContract]
    public class Board
    {
        public Board()
        {
            PlayerGrid = new PlayGrid();
            ComputerGrid = new PlayGrid();
        }
        [ProtoMember(1)]
        public PlayGrid PlayerGrid { get; set; }
        [ProtoMember(2)]
        public PlayGrid ComputerGrid { get; set; }
    }
}
