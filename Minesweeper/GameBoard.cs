using System.Collections.Generic;

namespace Minesweeper
{
    /// <summary>
    /// A data structure for storing a collection of cells in the game
    /// </summary>
    public class GameBoard
    {
        public readonly List<Cell> Cells;
        public int NumOfMines;
        public int Width;
        public int Height;

        public GameBoard(int numOfMines, int width, int height)
        {
            NumOfMines = numOfMines;
            Width = width;
            Height = height;
            Cells = new List<Cell>();
        }
    }
}