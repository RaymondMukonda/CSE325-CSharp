namespace ConnectFour.Models;

public class GameState
{
    public const int Width = 7;
    public const int Height = 6;

    public int[] Board { get; } = new int[Width * Height];
    public int CurrentPlayer { get; private set; } = 1;
    public int Winner { get; private set; }
    public bool IsDraw { get; private set; }
    public int Player1Wins { get; private set; }
    public int Player2Wins { get; private set; }
    public int Player1Streak { get; private set; }
    public int Player2Streak { get; private set; }
    public List<string> MoveHistory { get; } = [];

    public bool PlayPiece(int column)
    {
        if (Winner != 0 || IsDraw || column < 0 || column >= Width)
        {
            return false;
        }

        for (var row = Height - 1; row >= 0; row--)
        {
            var index = (row * Width) + column;
            if (Board[index] != 0)
            {
                continue;
            }

            Board[index] = CurrentPlayer;
            MoveHistory.Insert(
                0,
                $"Player {CurrentPlayer}: column {column + 1}, row {row + 1}");

            if (HasWon(row, column))
            {
                Winner = CurrentPlayer;
                if (Winner == 1)
                {
                    Player1Wins++;
                    Player1Streak++;
                    Player2Streak = 0;
                }
                else
                {
                    Player2Wins++;
                    Player2Streak++;
                    Player1Streak = 0;
                }
            }
            else if (MoveHistory.Count == Board.Length)
            {
                IsDraw = true;
                Player1Streak = 0;
                Player2Streak = 0;
            }
            else
            {
                CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
            }

            return true;
        }

        return false;
    }

    public bool IsColumnFull(int column)
    {
        return Board[column] != 0;
    }

    public void ResetRound()
    {
        Array.Clear(Board);
        MoveHistory.Clear();
        CurrentPlayer = 1;
        Winner = 0;
        IsDraw = false;
    }

    public void ResetMatch()
    {
        ResetRound();
        Player1Wins = 0;
        Player2Wins = 0;
        Player1Streak = 0;
        Player2Streak = 0;
    }

    private bool HasWon(int row, int column)
    {
        var player = CurrentPlayer;

        return Count(row, column, 0, 1, player) +
               Count(row, column, 0, -1, player) > 2 ||
               Count(row, column, 1, 0, player) +
               Count(row, column, -1, 0, player) > 2 ||
               Count(row, column, 1, 1, player) +
               Count(row, column, -1, -1, player) > 2 ||
               Count(row, column, 1, -1, player) +
               Count(row, column, -1, 1, player) > 2;
    }

    private int Count(int row, int column, int rowStep, int columnStep, int player)
    {
        var count = 0;
        row += rowStep;
        column += columnStep;

        while (row >= 0 &&
               row < Height &&
               column >= 0 &&
               column < Width &&
               Board[(row * Width) + column] == player)
        {
            count++;
            row += rowStep;
            column += columnStep;
        }

        return count;
    }
}
