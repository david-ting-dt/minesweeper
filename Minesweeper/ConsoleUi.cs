using System;
using System.Linq;
using Minesweeper.Exceptions;
using Minesweeper.GameActions;
using Minesweeper.Interfaces;

namespace Minesweeper
{
    /// <summary>
    /// Provides implementations of the user interaction methods
    /// </summary>
    public class ConsoleUi : IGameUi
    {
        private readonly IGameBoardRenderer _gameBoardRenderer;

        public ConsoleUi() { }

        public ConsoleUi(IGameBoardRenderer gameBoardRenderer)
        {
            _gameBoardRenderer = gameBoardRenderer;
        }

        public GameBoard GetDimension()
        {
            Console.Write("Please enter the width and height (e.g. 5 5): ");
            try
            {
                var input = Console.ReadLine();
                return ParseToGameBoard(input);
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input: must be two positive integers (e.g. 5 5)");
                throw;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Invalid Input: input cannot be empty (e.g. 5 5)");
                throw;
            }
        }

        private static GameBoard ParseToGameBoard(string input)
        {
            var values = input.Split().Select(int.Parse).ToList();
            if (values.Count != 2 || values.Any(d => d < 0))
                throw new InvalidInputException("Invalid Input: must be two positive integers (e.g. 5 5)");
            return new GameBoard(values[0], values[1]);
        }

        public int GetNumOfMines()
        {
            Console.Write("Please enter the number of mines: ");
            try
            {
                var input = Console.ReadLine();
                if (input == "")
                    throw new NullReferenceException();
                return int.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input: must be positive integers (e.g. 5)");
                throw;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Invalid Input: input cannot be empty (e.g. 5)");
                throw;
            }
        }

        public GameAction GetUserAction()
        {
            Console.Write("Command ('r'/'f') and coordinate (e.g. 2 3): ");
            try
            {
                var input = Console.ReadLine()?.Split();
                return ParseToAction(input);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Invalid Input: input cannot be empty (e.g. r 2 2)");
                throw;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input: Coordinate value must be positive integers (e.g. r 2 2)");
                throw;
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static GameAction ParseToAction(string[] input)
        {
            if (input.Length != 3)
                throw new InvalidInputException("Invalid Inputs: must contain 3 values (e.g. r 2 2)");
            
            int x = int.Parse(input[1]), y = int.Parse(input[2]);
            if (x < 0 || y < 0)
                throw new InvalidInputException("Invalid Input: coordinate values must be positive integers (e.g. r 2 2)");
            var coordinate = new Coordinate(x, y);

            var actionInput = input[0].ToLower();
            if (actionInput != "r" && actionInput != "f")
                throw new InvalidInputException("Invalid Input: incorrect command input " +
                                                "(i.e. 'r' - reveal, 'f' - flag/unflag");
            var action = actionInput == "r" ? (GameAction) new RevealAction(coordinate) : new FlagAction(coordinate);


            return action;
        }

        public void DisplayGameBoard(GameBoard gameBoard)
        {
            _gameBoardRenderer.Render(gameBoard);
        }

        public void PrintResult(bool isWon)
        {
            Console.WriteLine(isWon ? "You win!" : "You lose!");
        }
    }
}