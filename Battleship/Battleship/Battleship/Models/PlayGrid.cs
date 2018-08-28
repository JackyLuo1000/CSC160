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
    public class PlayGrid
    {
        public PlayGrid()
        {
            PlayerGrid = new Cell[10, 10];
            IsHidden = true;
            HiddenGrid = new Cell[10, 10];
            SetBaseCells();
            this.PlayerSave = new List<Cell>();
            this.HiddenSave = new List<Cell>();
        }

        public PlayGrid(string fresh)
        {
            PlayerGrid = new Cell[10, 10];
            IsHidden = true;
            HiddenGrid = new Cell[10, 10];
            Ships = new Ship[]
            {
                new Ship() { Name = ShipNames.Carrier },
                new Ship() { Name = ShipNames.Battleship },
                new Ship() { Name = ShipNames.Destroyer },
                new Ship() { Name = ShipNames.Submarine },
                new Ship() { Name = ShipNames.Patrol_Boat }
            };
            GameEnd = false;
            SetBaseCells();
            this.PlayerSave = new List<Cell>();
            this.HiddenSave = new List<Cell>();
        }

        public PlayGrid(PlayGrid grid)
        {
            this.PlayerGrid = grid.PlayerGrid;
            this.IsHidden = grid.IsHidden;
            this.HiddenGrid = grid.HiddenGrid;
            this.Ships = grid.Ships;
            this.GameEnd = grid.GameEnd;
            this.PlayerSave = grid.PlayerSave;
            this.HiddenSave = grid.HiddenSave;
        }

        [ProtoMember(1)]
        public bool IsHidden { get; set; }
        [ProtoIgnore]
        public Cell[ , ] PlayerGrid { get; set; }
        [ProtoIgnore]
        public Cell[ , ] HiddenGrid { get; set; }
        [ProtoMember(4)]
        public Ship[] Ships { get; set; }
        [ProtoMember(5)]
        public bool GameEnd { get; set; }

        [ProtoMember(2)]
        public List<Cell> PlayerSave { get; set; }
        [ProtoMember(3)]
        public List<Cell> HiddenSave { get; set; }

        private void SetBaseCells()
        {
            for(int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 10; r++)
                {
                    PlayerGrid[c, r] = new Cell();
                }
            }
            for (int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 10; r++)
                {
                    HiddenGrid[c, r] = new Cell();
                }
            }
        }

        public void CheckEndGame()
        {
            GameEnd = true;
            foreach(Ship s in Ships)
            {
                if (!s.IsSunk)
                {
                    GameEnd = false;
                    break;
                }
            }
        }

        public void SetSaves()
        {
            for(int c = 0; c < 10; c++)
            {
                for(int r = 0; r < 10; r++)
                {
                    PlayerSave.Add(PlayerGrid[c, r]);
                    HiddenSave.Add(HiddenGrid[c, r]);
                }
            }
        }

        public void LoadSaves()
        {
            int column = 0;
            int row = 0;
            string[] coordinates;
            foreach (Cell c in PlayerSave)
            {

                coordinates = c.Location.Split(", ");
                column = int.Parse(coordinates[0]);
                row = int.Parse(coordinates[1]);
                PlayerGrid[column, row] = c;
            }
            foreach (Cell c in HiddenSave)
            {
                coordinates = c.Location.Split(", ");
                column = int.Parse(coordinates[0]);
                row = int.Parse(coordinates[1]);
                HiddenGrid[column, row] = c;
            }
        }
    }
}
