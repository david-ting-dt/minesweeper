using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    /// <summary>
    /// Object that stores the information of the playing game board
    /// </summary>
    public class GameBoard
    {
        public readonly List<Cell> Cells;
        public int NumOfMines { get; }
        public int Width { get; }
        public int Height { get; }

        public GameBoard(int numOfMines, int width, int height)
        {
            NumOfMines = numOfMines;
            Width = width;
            Height = height;
            Cells = new List<Cell>();
        }

        public void LoadCells()
        {
            for (var y = 1; y <= Height; y++)
            {
                for (var x = 1; x <= Width; x++)
                {
                    var coordinate = new Coordinate(x, y);
                    Cells.Add(new Cell(coordinate));
                }
            }
        }

        public void PlantMine(int index)
        {
            Cells[index].PlantMine();
        }

        public bool IsMinePlanted(int index)
        {
            return Cells[index].IsMine;
        }

        public void SetAllCellAdjacentMineCount()
        {
            foreach (var cell in Cells)
            {
                var cellAdjacentMineCount = GetAdjacentMineCount(cell);
                cell.AdjacentMineCount = cellAdjacentMineCount;
            }
        }

        private int GetAdjacentMineCount(Cell cell)
        {
            int x = cell.X, y = cell.Y;

            var neighbours = Cells
                    .Where(c => c.X >= (x - 1) && c.X <= (x + 1) 
                                   && c.Y >= (y - 1) && c.Y <= (y + 1));
            var currentCell = Cells.Where(c => c.X == x && c.Y == y);

            return neighbours.Except(currentCell).Count(n => n.IsMine);
        }
    }
}