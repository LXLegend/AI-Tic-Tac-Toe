using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MiniMax
{
    // the max depth the algorithm will recurse to in order to determine the next step- if the board gets big this number needs to stay low
    static int maxDepth = GameSettings.Difficulty;

    // function to check if there are any moves that can be made on the board
    public static bool MovesLeft(int[,] boardState)
    {
        for (int x = 0; x < boardState.GetLength(0); x++)
            for (int y = 0; y < boardState.GetLength(1); y++)
                if (boardState[x, y] == 0)
                    return true;
        return false;
    }

    // Function that calls itse;f recursively to determine and score the best board state. Takes a boardstate, a depth, and a bool as parameters
    static int MiniMaxFunc(int[,] boardState, int depth, bool isMax)
    {
        // This section determines the score of the board passed in. If it is an end state the function returns the score, or zero if it is a draw.
        int score = Evaluate(boardState);

        if (score == boardState.GetLength(0) * boardState.GetLength(1))
        {
            return score;
        }

        if (score == -boardState.GetLength(0) * boardState.GetLength(1))
        {
            return score;
        }

        if (!MovesLeft(boardState))
        {
            return 0;
        }

        // checks to see if the board state needs to be a max or a min, and then finds the optimal board state and returns the score
        if (isMax)
        {
            int best = -boardState.GetLength(0) * boardState.GetLength(1);

            for (int x = 0; x < boardState.GetLength(0); x++)
            {
                for (int y = 0; y < boardState.GetLength(1); y++)
                {
                    if (boardState[x, y] == 0)
                    {
                        boardState[x, y] = 1;

                        if (depth < maxDepth)
                            best = Mathf.Max(best, MiniMaxFunc(boardState, depth + 1, !isMax));
                        else
                        {
                            boardState[x, y] = 0;
                            return 0;
                        }

                        boardState[x, y] = 0;
                    }
                }
            }
            return best;
        }

        else
        {
            int best = boardState.GetLength(0) * boardState.GetLength(1);

            for (int x = 0; x < boardState.GetLength(0); x++)
            {
                for (int y = 0; y < boardState.GetLength(1); y++)
                {
                    if (boardState[x, y] == 0)
                    {
                        boardState[x, y] = 2;

                        if (depth < maxDepth)
                            best = Mathf.Min(best, MiniMaxFunc(boardState, depth + 1, !isMax));
                        else
                        {
                            boardState[x, y] = 0;
                            return 0;
                        }


                        boardState[x, y] = 0;
                    }
                }
            }
            return best;
        }
    }

    public static int Evaluate(int[,] boardState)
    {
        for (int x = 0; x < boardState.GetLength(0); x++)
        {
            for (int y = 0; y < boardState.GetLength(1) - 2; y++)
            {
                if (boardState[x, y] == boardState[x, y + 1] && boardState[x, y + 1] == boardState[x, y + 2])
                {
                    if (boardState[x, y] == 1)
                        return boardState.GetLength(0) * boardState.GetLength(1);
                    else if (boardState[x, y] == 2)
                    {
                        return -boardState.GetLength(0) * boardState.GetLength(1);
                    }
                }
            }

        }

        for (int y = 0; y < boardState.GetLength(1); y++)
        {
            for (int x = 0; x < boardState.GetLength(0) - 2; x++)
            {
                if (boardState[x, y] == boardState[x + 1, y] && boardState[x + 1, y] == boardState[x + 2, y])
                {
                    if (boardState[x, y] == 1)
                        return boardState.GetLength(0) * boardState.GetLength(1);
                    else if (boardState[x, y] == 2)
                    {
                        return -boardState.GetLength(0) * boardState.GetLength(1);
                    }
                }
            }

        }

        for (int x = 0; x < boardState.GetLength(0) - 2; x++)
        {
            for (int y = 0; y < boardState.GetLength(1) - 2; y++)
            {
                if (boardState[x, y] == boardState[x + 1, y + 1] && boardState[x + 1, y + 1] == boardState[x + 2, y + 2])
                {
                    if (boardState[x, y] == 1)
                        return boardState.GetLength(0) * boardState.GetLength(1);
                    else if (boardState[x, y] == 2)
                    {
                        return -boardState.GetLength(0) * boardState.GetLength(1);
                    }
                }
            }

        }

        for (int x = 0; x < boardState.GetLength(0) - 2; x++)
        {
            for (int y = boardState.GetLength(1) - 1; y > 1; y--)
            {
                if (boardState[x, y] == boardState[x + 1, y - 1] && boardState[x + 1, y - 1] == boardState[x + 2, y - 2])
                {
                    if (boardState[x, y] == 1)
                        return boardState.GetLength(0) * boardState.GetLength(1);
                    else if (boardState[x, y] == 2)
                    {
                        return -boardState.GetLength(0) * boardState.GetLength(1);
                    }
                }
            }

        }
        return 0;
    }

    // calls the minimax function and returns the best move for the player
    public static int[] FindBestMove(int[,] boardState, int player)
    {
        int bestVal = player == 1 ? -boardState.GetLength(0) * boardState.GetLength(1) - 1 : boardState.GetLength(0) * boardState.GetLength(1) + 1;

        int[] bestMove = { -1, -1 };

        for (int x = 0; x < boardState.GetLength(0); x++)
        {
            for (int y = 0; y < boardState.GetLength(1); y++)
            {
                if (boardState[x, y] == 0)
                {
                    boardState[x, y] = player;

                    int moveVal = MiniMaxFunc(boardState, 0, player == 2);

                    boardState[x, y] = 0;

                    if ((moveVal > bestVal && player == 1) || (moveVal < bestVal && player == 2))
                    {
                        bestMove[0] = x;
                        bestMove[1] = y;
                        bestVal = moveVal;
                    }
                }
            }
        }

        return bestMove;
    }
}
