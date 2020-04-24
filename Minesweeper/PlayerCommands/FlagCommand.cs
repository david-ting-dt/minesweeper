using System.Collections.Generic;
using Minesweeper.Enums;
using Minesweeper.Exceptions;

namespace Minesweeper.PlayerCommands
{
    public class FlagCommand : PlayerCommand
    {
        public FlagCommand(Coordinate coordinate) : base(coordinate) {}

        public override void Execute(GameBoard gameBoard)
        {
            var cell = gameBoard.GetCell(Coordinate);
            if (cell.CellState == CellState.Revealed)
                throw new InvalidMoveException("Invalid move: Cannot flag cell that is already revealed.");
            cell.CellState = cell.CellState == CellState.Flagged ? CellState.Unrevealed : CellState.Flagged;
        }
    }
}