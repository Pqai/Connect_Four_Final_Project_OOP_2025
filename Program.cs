using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Connect_Four_Final_Project_OOP_2025

{

    abstract class Player
    {
        public char Disc { get; set; }
        public string Name { get; set; }

        public Player(char disc, string name)
        {
            Disc = disc;
            Name = name;
        }

        public abstract int GetMove();


    }

    class HumanPlayer : Player
    {
        public HumanPlayer(char disc, string name) : base(disc, name)
        {

        }

        public override int GetMove()
        {
            Console.WriteLine($"{Name}, enter a column number (1 - 7): ");
            int column;
            while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7)
            {
                Console.Write("Invalid Input. Please enter a number between 1 and 7: ");
            }
            return column;
        }
    }

    class ComputerPlayer : Player
    {
        private Random random;//How the computer will place pieces by random

        public ComputerPlayer(char disc, string name) : base(disc, name)
        {
            random = new Random();
        }

        public override int GetMove()
        {
            int column = random.Next(1, 7);//look into this
            Console.WriteLine($"{Name} chooses column {column}");
            return column;
        }
    }


    class ConnectFour
    {
        private Player player1;
        private Player player2;
        private Player currentPlayer;
        public bool gameOver = false;
        private static char[,] board;
        private const int Rows = 6;
        private const int Columns = 7;

        public ConnectFour()
        {
            board = new char[Rows, Columns];
            InitBoard();
            PlayerSetup();
        }

        private void InitBoard()
        {

            for (int row = 0; row < Rows; row++)
            {
                for(int column = 0; column < Columns; column++)
                {
                    board[row, column] = ' ';
                }
            }
        }

        private Player CreatePlayer(char disc, string defaultName)
        {
            while (true)
            {
                Console.Write("Enter 1 for Human or 2 for Computer: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.Write($"Enter name for {defaultName}: ");
                    string name = Console.ReadLine();
                    return new HumanPlayer(disc, string.IsNullOrWhiteSpace(name) ? defaultName : name);
                }
                else if(input == "2")
                {
                    return new ComputerPlayer(disc, "Computer " + defaultName.Split(' ')[1]);
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        private void PlayerSetup()
        {
            Console.WriteLine("Player 1 - Choose type:");
            player1 = CreatePlayer('X', "Player 1");

            Console.WriteLine("Player 2 - Choose type:");
            player2 = CreatePlayer('O', "Player 2");

            currentPlayer = player1;
        }

        public void BeginGame()
        {

            gameOver = false;
            InitBoard();

            while (!gameOver)
            {
                PlayATurn();

                if (IsItConnectFour())
                {
                    gameOver = true;
                    DrawBoard();
                    Console.WriteLine($"{currentPlayer.Name} wins!");
                }
                else if (IsBoardFull())
                {
                    gameOver = true;
                    DrawBoard();
                    Console.WriteLine("The game is a draw");
                }
                else
                {
                    //the code to switch players
                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                }
            }
        }

        private bool IsBoardFull()
        {
            for(int col = 0; col < Columns; col++)
            {
                if (board[0, col] == ' ') return false;
            }
            return true;
        }

        private bool IsItConnectFour()
        {
            return CheckHorizontalWin() || CheckVerticalWin() || CheckDiagonalWin();
        }

        private bool CheckHorizontalWin()
        {
            for (int row = 0; row < Rows; row++)
            {
                for(int col = 0; col < Columns - 3; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row, col + 1] &&
                        board[row, col] == board[row, col + 2] &&
                        board[row, col] == board[row, col + 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckVerticalWin()
        {
            for (int col = 0; col < Columns; col++)
            {
                for (int row = 0; row < Rows - 3; row++)
                {
                    char disc = board[row, col];
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col] &&
                        board[row, col] == board[row + 2, col] &&
                        board[row, col] == board[row + 3, col])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckDiagonalWin()
        {
            //diagonal to the right
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 0; col < Columns - 3; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col + 1] &&
                        board[row, col] == board[row + 2, col + 2] &&
                        board[row, col] == board[row + 3, col + 3])
                    {
                        return true;
                    }
                }
            }
            //diagonal left checking
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 3; col < Columns; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col -1] &&
                        board[row, col] == board[row + 2, col - 2] &&
                        board[row, col] == board[row + 3, col - 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void PlayATurn()
        {
            DrawBoard();
            int column = currentPlayer.GetMove() - 1;

            //checking if a column is full
            if (board[0, column] != ' ')
            {
                Console.WriteLine("Column is full! Please choose a different column.");
                PlayATurn();
                return;
            }

            for (int row = Rows - 1; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = currentPlayer.Disc;
                    return;
                }
            }
        }

        private void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine(" 1 2 3 4 5 6 7");

            for(int row = 0; row< Rows ; row++)
            {
                Console.Write("|");
                for(int col = 0; col < Columns; col++)
                {
                    Console.Write(board[row, col]);
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.Clear();
                ConnectFour game = new ConnectFour();
                game.BeginGame();
                //after game ends
                playAgain = AskToPlayAgain();

            }
            Console.WriteLine("Thank you for playing You many now press any key to exit");
            Console.ReadKey();
        }

        private static bool AskToPlayAgain()
        {
            Console.WriteLine("\nWould you like to play again (Y/N)");
            while (true)
            {
                string input = Console.ReadLine()?.Trim().ToUpper();

                if (input == "Y" || input == "YES")
                    return true;
                else
                {
                    return false;
                }
            }
        }
    }
}