using System;

namespace Csharp_Collections_NoughtsCrosses
{
    public class Game
    {
        private Square[,] _board = new Square[3, 3];

        public void PlayGame()
        {
            Player player = Player.Crosses;

            bool @continue = true;
            while (@continue)
            {
                DisplayBoard();

                @continue = PlayerMove(player);
                if (!@continue)
                {
                    return;
                }

                player = 3 - player; // swap player between X and O

                @continue = CheckWin();
            }
        }

        void DisplayBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" " + _board[i, j]);
                }
                Console.WriteLine();
            }
        }

        bool PlayerMove(Player player)
        {
            Console.Write($"{player} : Enter row comma column, eg. 3,3 > ");
            string input = Console.ReadLine();

            string[] parts = input.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }

            int.TryParse(parts[0], out int row);
            int.TryParse(parts[1], out int column);

            if (row < 1 || row > 3 || column < 1 || column > 3)
            {
                return false;
            }

            if (_board[row - 1, column - 1].Owner != Player.Noone)
            {
                Console.WriteLine("Square is already occupied");
                return false;
            }
            
            _board[row - 1, column - 1] = new Square(player);
            return true;
        }

        bool CheckWin()
        {
            int cross = 0;
            int nought = 0;

            #region Horizontal
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0].Owner == Player.Crosses && _board[i, 1].Owner == Player.Crosses && _board[i, 2].Owner == Player.Crosses)
                {
                    cross++;
                }
                if (_board[i, 0].Owner == Player.Noughts && _board[i, 1].Owner == Player.Noughts && _board[i, 2].Owner == Player.Noughts)
                {
                    nought++;
                }
            }
            #endregion

            #region Vertical
            for (int i = 0; i < 3; i++)
            {
                if (_board[0, i].Owner == Player.Crosses && _board[1, i].Owner == Player.Crosses && _board[2, i].Owner == Player.Crosses)
                {
                    cross++;
                }
                if (_board[0, i].Owner == Player.Noughts && _board[1, i].Owner == Player.Noughts && _board[2, i].Owner == Player.Noughts)
                {
                    nought++;
                }
            }
            #endregion
            
            #region Asses
            if (cross == 1)
            {
                DisplayBoard();
                Console.WriteLine("Cross win!");
                return false;
            }
            
            if (nought == 1)
            {
                DisplayBoard();
                Console.WriteLine("Cross win!");
                return false;
            }
            #endregion

            return true;
        }
    }
}