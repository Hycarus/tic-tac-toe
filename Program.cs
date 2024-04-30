namespace tic_tac_toe;

class Program
{
    static readonly char[,] board = new char[3, 3];
    static char currentPlayer = 'O';
    

    static void Main(string[] args)
    {
        Console.WriteLine("Inserisci il nome del giocatore A");
        string? playerName1 = Console.ReadLine();
        Console.WriteLine("Inserisci il nome del giocatore B");
        string? playerName2 = Console.ReadLine();
        string? currentPlayerName = playerName1;

        CreateBoard();
        bool IsGameWon = false;

        while(!IsGameWon && !IsBoardFull())
        {
            PrintBoard();
            PlayerMove(currentPlayerName);
            IsGameWon = CheckWin();
            SwitchPlayer(ref currentPlayerName, playerName1, playerName2);
        }

        PrintBoard();

        if (IsGameWon)
        {
            SwitchPlayer(ref currentPlayerName, playerName1, playerName2);
            Console.WriteLine($"Giocatore {currentPlayerName} ha vinto!");
        }
        else
        {
            Console.WriteLine("La partita finisce in parità");
        }
    }

    static void CreateBoard()
    {
        for(int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 3; col++)
            {
                board[row, col] = ' ';
            }
        }
    }

    static void PrintBoard()
    {
        Console.WriteLine("# | 1 | 2 | 3 |");
        Console.WriteLine("-----------------");
        for(int row = 0; row < 3; row++)
        {
            Console.Write($"{row + 1} | ");
            for(int col = 0; col < 3; col++)
            {
                Console.Write(board[row, col] == ' ' ? " " : board[row, col].ToString());
                if(col < 2)
                {
                    Console.Write(" | ");
                }
            }
            Console.WriteLine(" |");
            if(row < 2)
            {
                Console.WriteLine("-----------------");
            }
        }
    }

    static bool IsBoardFull()
    {
        for(int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 3; col++)
            {
                if (board[row, col] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }

    static void SwitchPlayer(ref string currentName, string name1, string name2)
    {
        currentPlayer = currentPlayer == 'O' ? 'X' : 'O';
        currentName = currentName == name1 ? name2 : name1;
    }

    static void PlayerMove(string name)
    {
        int row, col;

        while (true)
        {
            Console.WriteLine($"\n{name} inserisci la tua mossa (ad esempio: 1-2)");
            string input = Console.ReadLine();
            string[] parts = input.Split('-');

            if(parts.Length == 2 && int.TryParse(parts[0], out row) && int.TryParse(parts[1], out col))
            {
                row--;
                col--;

                if(row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ')
                {
                    board[row, col] = currentPlayer;
                    break;
                }
                else
                {
                    Console.WriteLine("Mossa non valida, prova di nuovo");
                }
            }
            else
            {
                Console.WriteLine("Mossa non valida, prova di nuovo");
            }
        }
    }

    static bool CheckWin()
    {
        for(int row = 0; row < 3; row++)
        {
            if (board[row, 0] == currentPlayer && board[row, 1] == currentPlayer && board[row, 2] == currentPlayer)
            {
                return true;
            }
        }

        for(int col = 0; col < 3; col++)
        {
            if (board[0, col] == currentPlayer && board[1, col] == currentPlayer && board[2, col] == currentPlayer)
            {
                return true;
            }
        }

        if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
        {
            return true;
        }

        if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
        {
            return true;
        }

        return false;
    }

    
}

