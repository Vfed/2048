using System;

namespace _2048
{
    class Program
    {
        static void ShowLabel()
        {

            Random r = new Random();
            int[] color2 = new int[4] { r.Next(14) + 1, r.Next(14) + 1, r.Next(0, 14) + 1, r.Next(14) + 1 };
            string[,] label = new string[5, 4]{
                { "   22   ","   00   ","     4  ","   88   "},
                { "  2  2  ","  0  0  ","    44  ","  8  8  "},
                { "    2   ","  0  0  ","   4 4  ","   88   "},
                { "   2    ","  0  0  ","  4444  ","  8  8  "},
                { "  2222  ","   00   ","     4  ","   88   "} };

            Console.WriteLine("");
            for (int i = 0; i < 5; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < 4; j++)
                {
                    ToConsole("/",9);
                    ToConsole(label[i, j], color2[j]);
                    
                }
                ToConsole("/\n",9);
            }
        }
        static bool CanPlay(int[,] matrix)
        {
            bool canPlay = false;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
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

                    if (i < matrix.GetLength(0) - 1)
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
        static int[,] AddNew(int[,] matrix, int newElems)
        {
            bool hasZero = false;
            Random rand = new Random();
            int zeroCount = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        zeroCount++;
                        hasZero = true;
                    }
                }
            }

            if (newElems >= zeroCount)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 0)
                        {
                            matrix[i, j] = 2;
                        }
                    }
                }
            }
            else
            {
                for (int k = 0; k < newElems; k++)
                {
                    if (hasZero)
                    {
                        int step = rand.Next(zeroCount) + 1;
                        do
                        {
                            for (int i = 0; i < matrix.GetLength(0); i++)
                            {
                                for (int j = 0; j < matrix.GetLength(1); j++)
                                {
                                    if (matrix[i, j] == 0)
                                    {
                                        --step;
                                    }

                                    if (step == 0 && matrix[i, j] == 0)
                                    {
                                        matrix[i, j] = 2;
                                    }
                                }
                            }
                        } while (step > 0);
                    }
                }
            }
            return matrix;
        }
        static int[,] Move(int[,] matrix, out int plusScore, int newElems, out bool escape )
        {
            escape = true;
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
                case ConsoleKey.Backspace:
                    escape = false;
                    return matrix;
                default:
                    break;
            }

            bool isMoved = false;

            switch (move)
            {
                case 1:
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        for (int i = 0; i < matrix.GetLength(0) - 1; i++)
                        {
                            for (int k = i; k >= 0; k--)
                            {
                                if (matrix[k, j] == matrix[k + 1, j] && matrix[k, j] > 0)
                                {
                                    matrix[k, j] += matrix[k + 1, j];
                                    matrix[k + 1, j] = 0;
                                    plusScore += matrix[k, j];

                                    isMoved = true;
                                }

                                if (matrix[k, j] == 0 && matrix[k + 1, j] > 0)
                                {
                                    matrix[k, j] = matrix[k + 1, j];
                                    matrix[k + 1, j] = 0;
                                    isMoved = true;
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        for (int i = matrix.GetLength(0) - 1; i > 0; i--)
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

                                if (matrix[i, k] == matrix[i, k - 1] && matrix[i, k] > 0)
                                {
                                    isMoved = true;
                                    matrix[i, k] += matrix[i, k - 1];
                                    matrix[i, k - 1] = 0;
                                    plusScore += matrix[i, k];
                                }
                                if (matrix[i, k] == 0 && matrix[i, k - 1] > 0)
                                {
                                    isMoved = true;
                                    matrix[i, k] = matrix[i, k - 1];
                                    matrix[i, k - 1] = 0;
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
                if (isMoved)
                {
                    matrix = AddNew(matrix, newElems);
                }
                return matrix;
            }
        }
        static int[,] Init(int size)
        {
            int[,] matrixOut = new int[size, size];
            for (int i = 0; i < matrixOut.GetLength(0); i++)
            {
                for (int j = 0; j < matrixOut.GetLength(1); j++)
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
            int[] colors = new int[14];
            int num = 2;

            for (int i = 0; i < 14; i++)
            {
                colors[i] = num;
                num += num;
            }
            
            string tab = "~~~"; matrix.GetLength(1);
            for (int i = 0; i < (matrix.GetLength(1)) * 8; i++)
            {
                tab += "~";
            }
            ToConsole("\n\t"+tab+"\n" , 14);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("\t");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ToConsole("| \t ",12);
                }
                ToConsole(" |\n",12);
                Console.Write("\t");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {

                    if (matrix[i, j] > 0)
                    {
                        ToConsole("| ",3);
                        for (int k = 0; k < 14; k++)
                        {
                            if (colors[k] == matrix[i, j])
                            {
                                ToConsole( matrix[i, j]+"" ,k+1);
                                break;
                            }
                        }
                        Console.Write("\t ");
                    }
                    else
                    {
                        ToConsole("| \t ",3);
                    }
                }
                ToConsole(" |\n",3);
                Console.Write("\t");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ToConsole("| \t ",6);
                }
                ToConsole(" |",6);
                ToConsole("\n\t" + tab + "\n", 14); 
            }
        }
        static int matrixSize()
        {
            string error = "";
            int size = 0;
            bool sizeCheck = false;
            do
            {
                Console.Clear();
                ShowLabel();
                if (error.Length > 0)
                {
                    Console.WriteLine("\n\t" + error);
                }
                error = "";
                Console.Write("\n\t Set fild size (4-10) :");
                sizeCheck = Int32.TryParse(Console.ReadLine(), out size);
                if (size < 4 || size > 10)
                {
                    error = " Wrong enter? try again ; ";
                    sizeCheck = false;
                }
            }
            while (!sizeCheck);

            return size;
        }
        static void gameOptions(out int size, out int newElems)
        {
            string error = "";
            size = 4;
            newElems = 1;
            int chose = 0;
            bool checkEnter = false;
            do
            {
                Console.Clear();
                ShowLabel();
                if (error.Length > 0)
                {
                    Console.WriteLine("\n\t " + error);
                }
                error = "";
                Console.WriteLine("\n\t Chose Game Options ( Field size / New elements frequency ) : \n");
                Console.WriteLine("\t\t1. Field 4x4, New elements 1 ;");
                Console.WriteLine("\t\t2. Field 5x5, New elements 2 ;");
                Console.WriteLine("\t\t3. Field 6x6, New elements 3 ;");
                Console.WriteLine("\t\t4. Field 10x10, New elements 100 ;");
                Console.WriteLine("\t\t5. I'll do it myself !!! ;");
                checkEnter = Int32.TryParse(Console.ReadLine(), out chose);

                if (!checkEnter)
                {
                    error = "Wrong enter? try again ; ";
                    continue;
                }

                switch (chose)
                {
                    case 1:
                        size = 4;
                        newElems = 1;
                        return;
                    case 2:
                        size = 5;
                        newElems = 2;
                        return;
                    case 3:
                        size = 6;
                        newElems = 3;
                        return;
                    case 4:
                        size = 10;
                        newElems = 100;
                        return;
                    case 5:
                        size = matrixSize();
                        newElems = newElemsNumber();
                        return;
                    default:
                        error = "Wrong enter? try again ; ";
                        continue;
                }

            }
            while (!checkEnter);
        }
        static int newElemsNumber()
        {
            int count = 0;
            bool countCheck = false;
            do
            {
                Console.Write("\n\t Set new elements apear count (1-100) :");
                countCheck = Int32.TryParse(Console.ReadLine(), out count);
                if (count < 1 || count > 100)
                {
                    Console.WriteLine("\n\t Wrong enter? try again ; ");
                    countCheck = false;
                }
            }
            while (!countCheck);

            return count;
        }
        static void ToConsole(string str, int col = 15)
        {
            if (col < 16 && col >= 0)
            {
                Console.ForegroundColor = (ConsoleColor)col;
                Console.Write(str);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)col;
                Console.Write(str);
                Console.ResetColor();
            }
            
        }
        static void Main(string[] args)
        {
            int bestScore = 0;
            bool canMove = true;
            int[,] matrix;
            int size = 0;
            int newCount = 0;
            ConsoleKeyInfo key;
            do
            {
                gameOptions(out size, out newCount);
                matrix = Init(size);
                int score = 0;
                do
                {
                    Console.Clear();
                    canMove = CanPlay(matrix);
                    if (canMove)
                    {
                        if (bestScore < score)
                        {
                            bestScore = score;
                        }
                        ShowLabel();
                        Console.WriteLine("\n\t   Score : " + score + "\t Best Score : " + bestScore);
                        Console.WriteLine("\n\t   Controls : Up(W), Down(S), Left(A), Right(D), End Game (Backspace) ;");
                        Show(matrix);
                        matrix = Move(matrix, out int plusScore, newCount, out canMove);
                        score += plusScore;
                    }
                } while (canMove);
                do
                {
                    Console.Clear();
                    ShowLabel();
                    if (bestScore <= score)
                    {
                        bestScore = score;

                        ToConsole("\n\t Your Score is the BEST : " + score + "\n", 3);
                    }
                    else
                    {
                        ToConsole("\t Your score :" + score + "\n\t Best Score : " + bestScore + "\n", 3);
                    }
                    Show(matrix);
                    Console.WriteLine("\n\t Press Enter - Restar ;");
                    Console.WriteLine("\n\t Press Esc - Exit ;");
                    key = Console.ReadKey();
                } while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter);
            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
