namespace tic_tac_toe;

class Program
{
    static readonly char[,] board = new char[3, 3];
    static char currentPlayer = 'O';
    static string playerName1;
    static string currentPlayerName;

    static void Main(string[] args)
    {
        Console.WriteLine("Inserisci il nome del giocatore");
        playerName1 = Console.ReadLine();
        currentPlayerName = playerName1;

        StartNewGame();
    }

    static void StartNewGame()
    {
        CreateBoard();
        currentPlayer = 'O';
        string? currentPlayerName = playerName1;
        string choice = "";

        CreateBoard();
        bool IsGameWon = false;

        while (!IsGameWon && !IsBoardFull() && choice != "0")
        {
            PrintBoard();
            if (currentPlayer == 'X')
            {
                var move = AIMove();
                board[move.row, move.col] = currentPlayer;
            }
            else
            {
                PlayerMove(playerName1);
            }
            IsGameWon = CheckWin();
            SwitchPlayer(ref currentPlayerName, playerName1);
        }

        PrintBoard();

        if (IsGameWon)
        {
            SwitchPlayer(ref currentPlayerName, playerName1);
            Console.WriteLine($"Giocatore {currentPlayerName} ha vinto!");
            Console.WriteLine("Inserisci un opazione");
            Console.WriteLine("1. Nuova partita");
            Console.WriteLine("0. Esci");
        }
        else
        {
            Console.WriteLine("La partita finisce in parità");
            Console.WriteLine("Scegli un opazione");
            Console.WriteLine("1. Nuova partita");
            Console.WriteLine("2. Esci");
        }
        choice = Console.ReadLine();

        while (true)
        {
            if (choice == "1")
            {
                StartNewGame();
                break;
            }
            else if (choice == "0")
            {
                Console.WriteLine("Arrivederci");
                break;
            }
            else
            {
                Console.WriteLine("Opzione inserita non valida, scegli 1 per una nuova partita, scegli 0 per uscire");
                choice = Console.ReadLine();
            }
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

    static void SwitchPlayer(ref string currentName, string name1)
    {
        currentPlayer = currentPlayer == 'O' ? 'X' : 'O';
        currentName = currentName == name1 ? "Computer" : name1;
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

    static int AlphaBeta(char[,] board, int count, int alpha, int beta, bool isMaximizing)
    {
        if (CheckWin()) 
        {
            return isMaximizing ? -1 : 1; 
        }

        if (IsBoardFull()) 
        {
            return 0; 
        }

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == ' ')
                {
                    board[row, col] = isMaximizing ? 'X' : 'O';

                    int score = AlphaBeta(board, count + 1, alpha, beta, !isMaximizing);

                    board[row, col] = ' ';

                    if (isMaximizing)
                    {
                        bestScore = Math.Max(bestScore, score);
                        alpha = Math.Max(alpha, score);
                    }
                    else
                    {
                        bestScore = Math.Min(bestScore, score);
                        beta = Math.Min(beta, score);
                    }

                    if (beta <= alpha)
                    {
                        return bestScore;
                    }
                }
            }
        }

        return bestScore;
    }


    static int Minimax(bool isMaximizing) // Poco efficente, sostituita con AlphaBeta()
    {
        if (CheckWin())
        {
            return isMaximizing ? -1 : 1;
        }

        if (IsBoardFull())
        {
            return 0;
        }

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        board[row, col] = currentPlayer;
                        int score = Minimax(false);
                        board[row, col] = ' ';
                        bestScore = Math.Max(bestScore, score);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int row = 0; row < 3; row++){
                for(int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        board[row, col] = currentPlayer;
                        int score = Minimax(true);
                        board[row, col] = ' ';
                        bestScore = Math.Min(bestScore, score);
                    }
                }
            }
            currentPlayer = currentPlayer == 'A' ? 'O' : 'X';
            return bestScore;
        }
    }

    static (int row, int col) AIMove()
    {
        int bestScore = int.MinValue;
        (int row, int col) bestMove = (-1, -1);
        for(int row = 0; row < 3; row++)
        {
            for(int col =0; col < 3; col++)
            {
                if (board[row, col] == ' ')
                {
                    board[row, col] = 'X';
                    int score = AlphaBeta(board, 0, int.MinValue, int.MaxValue, false);
                    board[row, col] = ' ';
                    if(score > bestScore)
                    {
                        bestScore = score;
                        bestMove = (row, col);
                    }
                }
            }
        }
        return bestMove;
    }
}

