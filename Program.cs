using System;

namespace _2048
{
    class Program
    {
        static bool CanPlay(int[,] matrix) 
        {
            bool canPlay = false;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; i < matrix.GetLength(1); i++)
                {
                    if (matrix[i, j] == 0) 
                    { 
                        canPlay = true;
                    }

                    if (j < matrix.GetLength(1) - 1) 
                    {
                        if (matrix[i, j] == matrix[i, j + 1]) 
                        {
                            canPlay = true;
                        }
                    }

                    if (i < matrix.GetLength(0)- 1)
                    {
                        if (matrix[i, j] == matrix[i + 1, j])
                        {
                            canPlay = true;
                        }
                    }
                }
            }

            return canPlay;
        }
        static int[,] AddNew(int[,] matrix)
        {
            bool hasZero = false;
            Random rand = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0) 
                    {
                        hasZero = true;
                        break;
                    }
                }
                if (hasZero) 
                {
                    break;
                }
            }
            if (hasZero)
            {
                int step = rand.Next(33)+1;
                do
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (matrix[i, j] == 0)
                            {
                                step--;
                            }

                            if (step == 0)
                            {
                                matrix[i, j] = 2;
                            }
                        }
                    }
                } while (step > 0);
            }
            return matrix;
        }
        static int[,] Move(int[,] matrix, out int plusScore )
        {
            plusScore = 0;
            int move = 0;
            ConsoleKeyInfo moveKey = Console.ReadKey();

            switch (moveKey.Key)
            {
                case ConsoleKey.W:
                    move = 1;
                    break;
                case ConsoleKey.S:
                    move = 2;
                    break;
                case ConsoleKey.A:
                    move = 3;
                    break;
                case ConsoleKey.D:
                    move = 4;
                    break;
                default:
                    break;
            }

            bool isMoved = false;

            switch (move)
            {
                case 1:
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        for (int i = 0; i < matrix.GetLength(0)-1; i++)
                        {
                            for (int k = i; k >=0 ; k--)
                            {
                                if (matrix[k, j] == 0 && matrix[k + 1, j] > 0) 
                                {
                                    matrix[k, j] = matrix[k + 1, j];
                                    matrix[k + 1, j] = 0;
                                    isMoved = true;
                                }
                                
                                if (matrix[k, j] == matrix[k + 1, j] && matrix[k, j] > 0)
                                {
                                    matrix[k, j] += matrix[k + 1, j];
                                    matrix[k + 1, j] = 0;
                                    plusScore += matrix[k, j];
                                    isMoved = true;
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        for (int i = matrix.GetLength(0)-1; i > 0 ; i--)
                        {
                            for (int k = 1; k <= i; k++)
                            {
                                if (matrix[k, j] == 0 && matrix[k - 1, j] > 0)
                                {
                                    isMoved = true;
                                    matrix[k, j] = matrix[k - 1, j];
                                    matrix[k - 1, j] = 0;
                                }

                                if (matrix[k, j] == matrix[k - 1, j] && matrix[k, j] > 0)
                                {

                                    isMoved = true;
                                    matrix[k, j] += matrix[k - 1, j];
                                    matrix[k - 1, j] = 0;
                                    plusScore += matrix[k, j];
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                        {
                            for (int k = j; k >= 0; k--)
                            {
                                if (matrix[i, k] == 0 && matrix[i, k + 1] > 0)
                                {
                                    isMoved = true;
                                    matrix[i, k] = matrix[i, k + 1];
                                    matrix[i, k + 1] = 0;
                                }

                                if (matrix[i, k] == matrix[i, k + 1] && matrix[i, k] > 0)
                                {

                                    isMoved = true;
                                    matrix[i, k] += matrix[i, k + 1];
                                    matrix[i, k + 1] = 0;
                                    plusScore += matrix[i, k];
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = matrix.GetLength(1) - 1; j > 0; j--)
                        {
                            for (int k = 1; k <= j; k++)
                            {
                                if (matrix[i, k] == 0 && matrix[i, k - 1] > 0)
                                {
                                    isMoved = true;
                                    matrix[i, k] = matrix[i, k - 1];
                                    matrix[i, k - 1] = 0;
                                }

                                if (matrix[i, k] == matrix[i, k - 1] && matrix[i, k] > 0)
                                {
                                    isMoved = true;
                                    matrix[i, k] += matrix[i, k - 1];
                                    matrix[i, k - 1] = 0;
                                    plusScore += matrix[i, k];
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            if (move == 0)
            {
                return matrix;
            }
            else
            {
                matrix = AddNew(matrix);
                return matrix;
            }
        }
        static int[,] Init()
        {
            int[,] matrixOut = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrixOut[i, j] = 0;
                }
            }
            Random rand = new Random();
            bool isReady = false;
            int k = 2;
            do
            {
                int x = rand.Next(4);
                int y = rand.Next(4);
                if (matrixOut[x, y] == 0) 
                {
                    matrixOut[x, y] += 2;
                    k--;
                }
                if (k == 0) 
                {
                    isReady = true;
                }
            }
            while (!isReady);
            return matrixOut;
        }

        static void Show(int[,] matrix) 
        {

            string tab = "~~~"; matrix.GetLength(1);
            for (int i = 0; i < (matrix.GetLength(1))*8; i++)
            {
                tab += "~";
            }

            Console.WriteLine("\n"+ tab);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("| \t ");
                }
                Console.WriteLine(" |");

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        Console.Write("| " + matrix[i, j] + "\t ");
                    }
                    else
                    {
                        Console.Write("| \t ");
                    }
                }
                Console.WriteLine(" |");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("| \t ");
                }
                Console.WriteLine(" |\n"+tab);
            }
        }
        static void Main(string[] args)
        {
            int bestScore = 0;
            bool canMove = true;
            int[,] matrix = new int[4,4];

            ConsoleKeyInfo key;
            do
            {
                matrix = Init();
                int score = 0;
                do
                {
                    Console.Clear();
                    Show(matrix);
                    Console.WriteLine("\n\t Score : " + score + "\n");
                    matrix = Move(matrix, out int plusScore);
                    score += plusScore;
                    canMove = CanPlay(matrix);
                } while (canMove);
                
                if (bestScore < score)
                {
                    bestScore = score;
                    Console.WriteLine("Your score is the BEST :");
                    Console.WriteLine("Your score is :" + score);
                }
                else 
                {
                    Console.WriteLine("Your score :" + score + "\nBest Score :" + bestScore);
                }
                
                Console.WriteLine("Press any - Restar ;");
                Console.WriteLine("Press Esc - Exit ;");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
